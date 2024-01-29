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
    public partial class FormInicio : Form
    {
        private Form1 form1;

        private void FormInicio_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Focus();
            this.KeyPreview = true;

        }

        public FormInicio(Form1 form1)
        {
            this.form1 = form1;

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

            Form1 form1 = new Form1(choiceComboBox1, choiceComboBox2);
            form1.Show();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choiceComboBox2 = Convert.ToString(colorSchemeName.SelectedItem);

            if(choiceComboBox2 == "Chrome"){
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;
                colorSchemeName.Text = "Chrome";
            }
            else if (choiceComboBox2 == "Dark")
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
                colorSchemeName.Text = "Dark";
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void btnPlaySection_Click(object sender, EventArgs e)
        {
            playSection.Visible = true;
        }

        private void btnTutorialSection_Click(object sender, EventArgs e)
        {
            playSection.Visible = false;
        }


    }
}
