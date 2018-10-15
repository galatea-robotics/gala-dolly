using System;

namespace Gala.Dolly.UI.Diagnostics
{
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(Exception exception, string errorMessage)
        {
            _exception = exception;
            _errorMessage = errorMessage;
        }

        public Exception Exception { get { return _exception; } }

        public string ErrorMessage { get { return _errorMessage; } }

        private readonly Exception _exception;
        private readonly string _errorMessage;
    }
}