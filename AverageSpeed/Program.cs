namespace AverageSpeed
{
    using System;
    using System.Windows.Forms;
    using AverageSpeed;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var view = new AverageSpeedForm())
            using (new AverageSpeedController(view))
            {
                    Application.Run(view);
            }
        }
    }
}
