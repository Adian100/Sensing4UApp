// Name & Student ID: Adian Dzananovic, 30067523
// Task: AT1 - MVP that manages sensors for Sensing4U
// Date: 02/11/2025

using System;
using System.Windows.Forms;

namespace Sensing4UApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}