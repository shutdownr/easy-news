using System;
using System.Diagnostics;
using System.Windows;

namespace EasyNews
{
    public class ErrorManager
    {
        private static readonly Lazy<ErrorManager> LazyErrorManager = new Lazy<ErrorManager>(() => new ErrorManager());

        public static ErrorManager Instance { get { return LazyErrorManager.Value; } }

        private ErrorManager()
        {

        }

        public MessageBoxResult DisplayError(string message)
        {
            Trace.WriteLine($"\n\tError {message} occured");
            return MessageBox.Show($"{message}", "Error occured!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}