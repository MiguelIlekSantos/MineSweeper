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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione uma opção.");
                return;
            }

            int choiceComboBox1 = Convert.ToInt32(comboBox1.SelectedItem);

            Form1 form1 = new Form1(choiceComboBox1);
            form1.Show();
            this.Hide(); 
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
