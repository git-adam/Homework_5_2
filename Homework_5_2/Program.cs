using System;
using System.IO;
using System.Windows.Forms;

namespace Homework_5_2
{
    static class Program
    {
        public static string FilePath =
            Path.Combine(Environment.CurrentDirectory, "employees.txt");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
