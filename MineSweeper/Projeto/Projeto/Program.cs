﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {

        public static FormInicio MainFormInicio { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form1 = new Form1(5);
            FormInicio formInicio = new FormInicio(form1);

            MainFormInicio = formInicio;

            Application.Run(formInicio);
        }

    }
}
