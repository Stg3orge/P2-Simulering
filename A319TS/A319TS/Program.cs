using System;
using System.Windows.Forms;

namespace A319TS
{
    class Program
    {
        [STAThread] // Single Threaded
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new GUIMain());
        }
    }
}