// Developer Express Code Central Example:
// How to create a custom TreeList that allows you to group tree list columns in bands in the same manner as in AdvancedGridView
// 
// This example demonstrates how to create a custom tree list with the capability
// to create bands in the same manner as in AdvancedGridView.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3581
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyXtraTreeList {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
