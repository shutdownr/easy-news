using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace EasyNews.Helpers
{
    public class ErrorManager
    {
        private static readonly Lazy<ErrorManager> LazyErrorManager = new Lazy<ErrorManager>(() => new ErrorManager());

        public static ErrorManager Instance { get { return LazyErrorManager.Value; } }

        private ErrorManager()
        {

        }

        // Not async, looks shit plz fix

        /// <summary>   Displays an error described by message. </summary>
        ///
        /// <remarks>   Tim, 15.10.2018. </remarks>
        ///
        /// <param name="message">  The message. </param>
        ///
        /// <returns>   A MessageBoxResult. </returns>

        public MessageBoxResult DisplayError(string message)
        {
            Trace.WriteLine($"\n\tError {message} occured");
            return MessageBox.Show($"{message}", "Error occured!", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }
    }
}