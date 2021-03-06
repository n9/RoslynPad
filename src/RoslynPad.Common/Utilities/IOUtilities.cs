﻿using System;
using System.IO;
using System.Security;
using System.Threading.Tasks;

namespace RoslynPad.Utilities
{
    internal static class IOUtilities
    {
        public static void PerformIO(Action action)
        {
            PerformIO<object>(() =>
            {
                action();
                return null;
            });
        }

        public static T PerformIO<T>(Func<T> function, T defaultValue = default(T))
        {
            try
            {
                return function();
            }
            catch (Exception e) when (IsNormalIOException(e))
            {
            }

            return defaultValue;
        }

        public static async Task<T> PerformIOAsync<T>(Func<Task<T>> function, T defaultValue = default(T))
        {
            try
            {
                return await function().ConfigureAwait(false);
            }
            catch (Exception e) when (IsNormalIOException(e))
            {
            }

            return defaultValue;
        }

        public static bool IsNormalIOException(Exception e)
        {
            return e is IOException ||
                   e is SecurityException ||
                   e is ArgumentException ||
                   e is UnauthorizedAccessException ||
                   e is NotSupportedException ||
                   e is InvalidOperationException;
        }
    }
}