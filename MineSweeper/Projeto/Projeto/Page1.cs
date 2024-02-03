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
    public partial class Page1 : Form
    {

        private void Page1_Load(object sender, EventArgs e)
        {

        }

        public Page1()
        {
            

            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            comboBox1.Items.Add("10");
            comboBox1.Items.Add("15");
            comboBox1.Items.Add("20");
            comboBox1.Items.Add("25");

            colorSchemeName.Items.Add("Chrome");
            colorSchemeName.Items.Add("Dark");



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || colorSchemeName.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione uma opção em ambas as boxes.");
                return;
            }

            int choiceComboBox1 = Convert.ToInt32(comboBox1.SelectedItem);
            string choiceComboBox2 = Convert.ToString(colorSchemeName.SelectedItem);

            Projeto form1 = new Projeto(choiceComboBox1, choiceComboBox2);
            form1.KeyDown += new KeyEventHandler(Form1_KeyDown);
            form1.WindowState = FormWindowState.Maximized;
            form1.FormBorderStyle = FormBorderStyle.None;
            form1.Focus();
            form1.KeyPreview = true;
            form1.Show();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void colorSchemeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choiceComboBox2 = Convert.ToString(colorSchemeName.SelectedItem);

            if (choiceComboBox2 == "Chrome")
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
            else if (choiceComboBox2 == "Dark")
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
        }




    }
}
