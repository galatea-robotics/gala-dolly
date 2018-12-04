﻿using System;
using Galatea;

namespace Gala.Dolly.Test
{
    using Galatea.Diagnostics;

    internal class TestDebugger : Galatea.Runtime.Services.Debugger
    {
        /// <summary>
        /// Handles expected Galatea Core Exceptions, typically by logging them.
        /// </summary>
        /// <param name="ex">
        /// A run-time <see cref="TeaException"/>.
        /// </param>
        protected override void HandleTeaException(TeaException ex, IProvider provider)
        {
            HandleTeaException(ex, provider, false);
        }
        protected override void HandleTeaException(TeaException ex, IProvider provider, bool throwException)
        {
            string msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            Log(DebuggerLogLevel.Error, msg);
            Log(DebuggerLogLevel.StackTrace, ex.StackTrace, true);

            // Throw 
            if (throwException) throw (ex);
        }

        /// <summary>
        /// Handles unexpected System Errors, typically by logging them, and then
        /// re-throwing them.
        /// </summary>
        /// <param name="ex">
        /// A run-time <see cref="System.Exception"/>.
        /// </param>
        protected override void ThrowSystemException(Exception ex, IProvider provider)
        {
            Log(DebuggerLogLevel.Critical, ex.Message);
            Log(DebuggerLogLevel.StackTrace, ex.StackTrace, true);

            throw (ex);
        }
    }
}