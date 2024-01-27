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

        public FormInicio(Form1 form1)
        {
            this.form1 = form1;

            InitializeComponent();

            comboBox1.Items.Add("10");
            comboBox1.Items.Add("15");
            comboBox1.Items.Add("20");
            comboBox1.Items.Add("25");

            comboBox2.Items.Add("Chrome");
            comboBox2.Items.Add("Dark");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione uma opção em ambas as boxes.");
                return;
            }

            int choiceComboBox1 = Convert.ToInt32(comboBox1.SelectedItem);
            string choiceComboBox2 = Convert.ToString(comboBox2.SelectedItem);

            Form1 form1 = new Form1(choiceComboBox1, choiceComboBox2);
            form1.Show();
            this.Hide(); 
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choiceComboBox2 = Convert.ToString(comboBox2.SelectedItem);

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormInicio_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


    }
}
