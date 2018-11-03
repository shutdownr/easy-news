/* 
 TIMER
            var timer = new DispatcherTimer();
            timer.Tick += (sender, args) =>
            {
                ErrorManager.Instance.DisplayError("MyCustomError");
                timer.Stop();
            };
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Start();

 
 
 */