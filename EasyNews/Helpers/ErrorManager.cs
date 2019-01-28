using System;
using System.Diagnostics;
using System.Windows;

namespace EasyNews.Helpers
{
    /// <summary>
    /// A class for displaying errors to the user.
    /// </summary>
    public class ErrorManager
    {
        /// <summary>
        /// Lazy singleton instance of the ErrorManager.
        /// </summary>
        private static readonly Lazy<ErrorManager> LazyErrorManager = new Lazy<ErrorManager>(() => new ErrorManager());
        /// <summary>
        /// The public instance, that is accessed from other classes.
        /// </summary>
        public static ErrorManager Instance { get { return LazyErrorManager.Value; } }
        /// <summary>
        /// Empty constructor
        /// </summary>
        private ErrorManager()
        {

        }

        /// <summary>
        /// Display an error with a certain message to the user.
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <returns>The result of the MessageBox that is shown</returns>
        public MessageBoxResult DisplayError(string message)
        {
            Trace.WriteLine($"\n\tError {message} occured");
            return MessageBox.Show($"{message}", "Error occured!", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }
    }
}