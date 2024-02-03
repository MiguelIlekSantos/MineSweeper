using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Page1 page1;
        Page2 page2;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Focus();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (page1 == null || page1.IsDisposed)
            {
                if (page2 != null && !page2.IsDisposed)
                {
                    page2.Close();
                }
                page1 = new Page1();
                page1.FormClosed += Page1_FormClosed;
                page1.TopLevel = false;
                page1.Parent = miniWindows;
                page1.Dock = DockStyle.Fill;
                page1.FormBorderStyle = FormBorderStyle.None;
                page1.Show();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (page2 == null || page2.IsDisposed)
            {
                if (page1 != null && !page1.IsDisposed)
                {
                    page1.Close();
                }
                page2 = new Page2();
                page2.FormClosed += page2_FormClosed;
                page2.TopLevel = false;
                page2.Parent = miniWindows;
                page2.Dock = DockStyle.Fill;
                page2.FormBorderStyle = FormBorderStyle.None;
                page2.Show();
            }
        }

        private void Page1_FormClosed(object sender, EventArgs e)
        {
            miniWindows.Controls.Remove(page1);
        }

        private void page2_FormClosed(object sender, EventArgs e)
        {
            miniWindows.Controls.Remove(page2);
        }

        



       
    }
}
