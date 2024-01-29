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
        int gridSize;
        int gridSizeLimit;
        int[,] matriz;
        Button[,] buttons;
        int contador, contadorBombas;
        string colorChoice;
        int retry;
        string tempoDeJogo;
        bool jogoFinalizado = false;

        Color colorFirst;
        Color colorSecond;
        Color colorThird;
        Color colorFourth;
        Color colorFifth;
        Color colorSixth;
        Color colorSeventh;

        int tamanhoBtnAndSpace = 40;


        DateTime startTime;
        Timer timer;

        public Form1(int gridSize, string colorChoice)
        {
            InitializeComponent();

            Dictionary<string, string> Chrome = new Dictionary<string, string>
            {
                { "colorOne", "#E5C29F" }, // skin lighter
                { "colorTwo", "#D7B899" }, // skin darker
                { "colorThree", "#9be014" }, // dark green
                { "colorFour", "#BFE17D" }, // light green
                { "colorFive", "#FF0000" }, // red
                { "colorSix", "#000000" }, // black
                { "colorSeventh", "#f0f0f0" } // control
            };

            Dictionary<string, string> Dark = new Dictionary<string, string>
            {
                { "colorOne", "#747575" }, // gray
                { "colorTwo", "#747575" }, // gray
                { "colorThree", "#000000" }, // black
                { "colorFour", "#000000" }, // black
                { "colorFive", "#6efaea" }, // aqua blue
                { "colorSix", "#FFFFFF" }, // white
                { "colorSeventh", "#626363" } // gray
            };
            


            Dictionary<string, string> coresFinal = new Dictionary<string, string>(); ;
            this.colorChoice = colorChoice;

            if (colorChoice == "Chrome")
            {
                coresFinal = Chrome;
            }
            else if (colorChoice == "Dark")
            {
                coresFinal = Dark;
            }

            colorFirst = ColorTranslator.FromHtml(coresFinal["colorOne"]);
            colorSecond = ColorTranslator.FromHtml(coresFinal["colorTwo"]);
            colorThird = ColorTranslator.FromHtml(coresFinal["colorThree"]);
            colorFourth = ColorTranslator.FromHtml(coresFinal["colorFour"]);
            colorFifth = ColorTranslator.FromHtml(coresFinal["colorFive"]);
            colorSixth = ColorTranslator.FromHtml(coresFinal["colorSix"]);
            colorSeventh = ColorTranslator.FromHtml(coresFinal["colorSeventh"]);

            int novaLargura = tamanhoBtnAndSpace * gridSize;



            if (gridSize == 10)
            {
                this.Width = novaLargura - 10;
                this.Height = novaLargura - 10;
            }
            else if (gridSize == 15)
            {
                this.Width = novaLargura - 60;
                this.Height = novaLargura - 60;
            }
            else if (gridSize == 20)
            {
                this.Width = novaLargura - 110;
                this.Height = novaLargura - 100;
            }
            else if (gridSize == 25)
            {
                this.Width = novaLargura - 140;
                this.Height = novaLargura - 140;
            }

            this.gridSize = gridSize;
            gridSizeLimit = gridSize - 1;
            this.Text = "Mine Whisper";
            this.Icon = new Icon("bomb.ico");

            timer = new Timer();
            timer.Interval = 1000; 
            timer.Tick += Timer_Tick;
            startTime = DateTime.Now;
            timer.Start();

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            clock.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
            tempoDeJogo = string.Format("{0:D2}:{1:D2}:{2:D2}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Focus();
            this.KeyPreview = true;


            Random random = new Random();

            matriz = new int[gridSize, gridSize];
            buttons = new Button[gridSize, gridSize];


            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    matriz[i, j] = random.Next(0, 6);
                }
            }

            int totalWidth = tamanhoBtnAndSpace * gridSize;
            int totalHeight = tamanhoBtnAndSpace * gridSize;

            for (int a = 0; a < gridSize; a++)
            {
                for (int b = 0; b < gridSize; b++)
                {
                    int posX = (this.Width - totalWidth) / 2 + tamanhoBtnAndSpace * a;
                    int posY = (this.Height - totalHeight) / 2 + tamanhoBtnAndSpace * b;


                    Button novoBotao = new Button();
                    novoBotao.Text = numbers(a, b).ToString();
                    if (novoBotao.Text == "0")
                    {
                        novoBotao.Text = " ";
                    }

                    if (a % 2 == 0 && b % 2 != 0 || a % 2 == 1 && b % 2 == 0)
                    {
                        novoBotao.BackColor = colorThird;
                        novoBotao.ForeColor = colorThird;
                    }
                    else
                    {
                        novoBotao.BackColor = colorFourth;
                        novoBotao.ForeColor = colorFourth;
                    }

                    novoBotao.Size = new System.Drawing.Size(tamanhoBtnAndSpace, tamanhoBtnAndSpace);
                    novoBotao.Location = new System.Drawing.Point(posX, posY);
                    novoBotao.MouseDown += new MouseEventHandler(BotaoClicado);
                    novoBotao.Tag = new Coordenadas { X = a, Y = b };
                    bool isBold = novoBotao.Font.Bold;
                    novoBotao.Font = new Font(novoBotao.Font, novoBotao.Font.Style | FontStyle.Bold);
                    Controls.Add(novoBotao);

                    buttons[a, b] = novoBotao;
                }
            }

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (matriz[i, j] == 1)
                    {
                        ++contadorBombas;
                    }

                }
            }

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void verifyWinning()
        {
            if(jogoFinalizado){
                return;
            }

            contador = 0;
            int contadorEspaçoFaltando = gridSize * gridSize - contadorBombas;

            for (int i = 0; i <= gridSizeLimit; i++)
            {
                for (int j = 0; j <= gridSizeLimit; j++)
                {

                    if (buttons[i, j].ForeColor == colorSixth)
                    {
                        contador++;
                    }
                }
            }

            if (contador == contadorEspaçoFaltando)
            {
                timer.Stop();
                jogoFinalizado = true;

                DialogResult win;

                win = MessageBox.Show("Parabéns você venceu.\nSeu tempo foi : " + tempoDeJogo + "\n Deseja recomeçar ?", "Vitória", MessageBoxButtons.YesNo);
                if (win == DialogResult.Yes)
                {
                    Form1 newGame = new Form1(gridSize, colorChoice);
                    newGame.Show();
                    retry = 1;
                    this.Close();
                }
                else
                {
                    retry = 0;
                    Program.MainFormInicio.Show();
                    this.Hide();
                }
            }

        }

        private void BotaoClicado(object sender, MouseEventArgs e)
        {

            Coordenadas coordenadas = (Coordenadas)((Button)sender).Tag;
            int posicaoX = coordenadas.X;
            int posicaoY = coordenadas.Y;

            if (e.Button == MouseButtons.Right)
            {
                if (((Button)sender).ForeColor != colorSixth)
                {
                    if (((Button)sender).ForeColor == colorFifth)
                    {
                        if (posicaoX % 2 == 0 && posicaoY % 2 != 0 || posicaoX % 2 == 1 && posicaoY % 2 == 0)
                        {
                            ((Button)sender).ForeColor = colorThird;
                            ((Button)sender).BackColor = colorThird;
                        }
                        else
                        {
                            ((Button)sender).ForeColor = colorFourth;
                            ((Button)sender).BackColor = colorFourth;
                        }

                    }
                    else
                    {
                        ((Button)sender).ForeColor = colorFifth;
                        ((Button)sender).BackColor = colorFifth;
                    }
                }
            }

            if (e.Button == MouseButtons.Left && ((Button)sender).ForeColor != colorFifth)
            {
                if (((Button)sender).Text == "-1")
                {
                    DialogResult resposta;
                    resposta = MessageBox.Show("Você clicou em uma bomba.\n Deseja recomeçar ?", "Derrota", MessageBoxButtons.YesNo);
                    if (resposta == DialogResult.Yes)
                    {
                        Form1 newGame = new Form1(gridSize, colorChoice);
                        newGame.Show();
                        retry = 1;
                        this.Close();
                    }
                    else
                    {
                        retry = 0;
                        Program.MainFormInicio.Show();
                        this.Hide();
                    }
                }
                else if (((Button)sender).Text != "-1" && ((Button)sender).Text != " ")
                {

                    if (posicaoX % 2 == 0 && posicaoY % 2 != 0 || posicaoX % 2 == 1 && posicaoY % 2 == 0)
                    {
                        ((Button)sender).ForeColor = colorSixth;
                        ((Button)sender).BackColor = colorFirst;
                    }
                    else
                    {
                        ((Button)sender).ForeColor = colorSixth;
                        ((Button)sender).BackColor = colorSecond;
                    }


                    verifyWinning();
                }
                else if (((Button)sender).Text == " ")
                {

                    ((Button)sender).ForeColor = colorSixth;
                    if (posicaoX % 2 == 0 && posicaoY % 2 != 0 || posicaoX % 2 == 1 && posicaoY % 2 == 0)
                    {

                        ((Button)sender).BackColor = colorFirst;
                    }
                    else
                    {
                        ((Button)sender).BackColor = colorSecond;
                    }

                    verifyWinning();

                    openEveryZero(posicaoX, posicaoY);

                }
            }

            if (e.Button == MouseButtons.Middle)
            {
                int numMarks = middleClickCondition(posicaoX, posicaoY);
                if (((Button)sender).Text == " ")
                {
                    return;
                }
                int numBombsAround = Convert.ToInt32(((Button)sender).Text);

                if (numMarks == numBombsAround)
                {
                    middleClickAction(posicaoX, posicaoY);
                }

            }


        }
        public void openEveryZero(int posicaoX, int posicaoY)
        {
            for (int i = posicaoX - 1; i < posicaoX + 2; i++)
            {
                for (int j = posicaoY - 1; j < posicaoY + 2; j++)
                {
                    if (i < 0 || i > gridSizeLimit || j < 0 || j > gridSizeLimit)
                    {
                        return;
                    }
                    if (buttons[i, j].ForeColor != colorSixth)
                    {
                        buttons[i, j].ForeColor = colorSixth;
                        if (i % 2 == 0 && j % 2 != 0 || i % 2 == 1 && j % 2 == 0)
                        {

                            buttons[i, j].BackColor = colorFirst;
                        }
                        else
                        {
                            buttons[i, j].BackColor = colorSecond;
                        }

                        verifyWinning();
                        if (buttons[i, j].Text == " ")
                        {
                            openEveryZero(i, j);
                        }
                    }
                }
            }
            return;
        }



        public class Coordenadas
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private int numbers(int posicaoY, int posicaoX)
        {
            int minearound = 0;

            if (matriz[posicaoY, posicaoX] == 1)
            {
                minearound = -1;
                return minearound;
            }

            int startY = (posicaoY == 0) ? 0 : posicaoY - 1;
            int endY = (posicaoY == gridSizeLimit) ? gridSizeLimit : posicaoY + 1;
            int startX = (posicaoX == 0) ? 0 : posicaoX - 1;
            int endX = (posicaoX == gridSizeLimit) ? gridSizeLimit : posicaoX + 1;

            for (int i = startY; i <= endY; i++)
            {
                for (int j = startX; j <= endX; j++)
                {
                    if (matriz[i, j] == 1)
                    {
                        minearound++;
                    }
                }
            }

            return minearound;

        }

        private int middleClickCondition(int posicaoY, int posicaoX)
        {
            int minearound = 0;

            if (buttons[posicaoY, posicaoX].ForeColor == colorFifth)
            {
                return 0;
            }

            int startY = (posicaoY == 0) ? 0 : posicaoY - 1;
            int endY = (posicaoY == gridSizeLimit) ? gridSizeLimit : posicaoY + 1;
            int startX = (posicaoX == 0) ? 0 : posicaoX - 1;
            int endX = (posicaoX == gridSizeLimit) ? gridSizeLimit : posicaoX + 1;

            for (int i = startY; i <= endY; i++)
            {
                for (int j = startX; j <= endX; j++)
                {
                    if (buttons[i, j].ForeColor == colorFifth)
                    {
                        minearound++;
                    }
                }
            }
            return minearound;

        }
        private void middleClickAction(int posicaoY, int posicaoX)
        {
            int startY = (posicaoY == 0) ? 0 : posicaoY - 1;
            int endY = (posicaoY == gridSizeLimit) ? gridSizeLimit : posicaoY + 1;
            int startX = (posicaoX == 0) ? 0 : posicaoX - 1;
            int endX = (posicaoX == gridSizeLimit) ? gridSizeLimit : posicaoX + 1;

            for (int i = startY; i <= endY; i++)
            {
                for (int j = startX; j <= endX; j++)
                {
                    if (i >= 0 && i < gridSize && j >= 0 && j < gridSize)
                    {
                        MouseEventArgs fakeClick = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
                        BotaoClicado(buttons[i, j], fakeClick);
                    }
                }
            }

        }


    }
}
