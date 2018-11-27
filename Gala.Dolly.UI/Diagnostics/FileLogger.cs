using System.IO;
using Galatea.Runtime;

namespace Gala.Dolly.UI.Diagnostics
{
    /// <summary>
    /// A component class that writes the Galatea.Runtime processes to a file.
    /// </summary>
    public class FileLogger : RuntimeComponent, Galatea.Diagnostics.IFileLogger
    {
        /// <summary>
        /// Gets a boolean indicating that the <see cref="FileLogger"/> is open and writing to a file.
        /// </summary>
        public bool IsLogging { get { return _isLogging; } }
        /// <summary>
        /// Opens the text file for log writing.
        /// </summary>
        /// <param name="fileName">
        /// A relative or absolute path for the file that the current FileStream object will
        /// encapsulate.
        /// </param>
        /// <param name="mode">
        /// A <see cref="System.IO.FileMode"/> constant that determines how to open or create the file.
        /// </param>
        public void StartLogging(string fileName, FileMode mode)
        {
            FileStream stream = null;

            // Initialize FileStream
            try
            {
                stream = new FileStream(fileName, mode);
                FileStream stream1 = stream;
                _writer = new StreamWriter(stream1);

                stream = null;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }

            // Set status
            _isLogging = true;
        }
        /// <summary>
        /// Closes the <see cref="StreamWriter"/> instance that is writing the log to the file.
        /// </summary>
        public void StopLogging()
        {
            // Dispose FileStream
            _writer.Close();

            // Set status
            _isLogging = false;
        }
        /// <summary>
        /// Writes a log message to the file using a <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="message">
        /// The log message to write to the file.
        /// </param>
        public void Log(string message)
        {
            // TODO:  Make so we can open the file and view the Log without stopping the application.
            _writer.WriteLine(message);
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="Gala.Dolly.UI.Diagnostics.FileLogger"/>
        /// and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged
        /// resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            _isLogging = false;

            _writer.Dispose();
            base.Dispose(disposing);
        }

        private StreamWriter _writer;
        private bool _isLogging;
    }
}