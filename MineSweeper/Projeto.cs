﻿using System;
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

        //int[,] matriz = new int[20, 20];
        //Button[,] buttons = new Button[20, 20];

        int[,] matriz;
        Button[,] buttons;

        int contador, contadorBombas;

        const string colorOne = "#E5C29F"; // skin lighter
        const string colorTwo = "#D7B899"; // skin darker 
        const string colorThree = "#9be014"; //dark green
        const string colorFour = "#BFE17D";  //light green
        Color colorFirst;
        Color colorSecond;
        Color colorThird;
        Color colorFourth;


        int tamanhoBtnAndSpace = 40;
        int spaceBetweenBtns = 32;

        public Form1(int gridSize)
        {
            InitializeComponent();

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

            colorFirst = ColorTranslator.FromHtml(colorOne);
            colorSecond = ColorTranslator.FromHtml(colorTwo);
            colorThird = ColorTranslator.FromHtml(colorThree);
            colorFourth = ColorTranslator.FromHtml(colorFour);

            this.Text = "Mine Whisper";
            this.Icon = new Icon("bomb.ico");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

            for (int a = 0; a < gridSize; a++)
            {
                for (int b = 0; b < gridSize; b++)
                {
                    int posX, posY;

                    posX = 10 + spaceBetweenBtns * a;
                    posY = 10 + spaceBetweenBtns * b;


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

        private void verifyWinning()
        {
            contador = 0;
            int contadorEspaçoFaltando = gridSize * gridSize - contadorBombas;

            for (int i = 0; i <= gridSizeLimit; i++)
            {
                for (int j = 0; j <= gridSizeLimit; j++)
                {

                    if (buttons[i, j].ForeColor == Color.Black)
                    {
                        contador++;
                    }
                }
            }

            if (contador == contadorEspaçoFaltando)
            {
                DialogResult win;
                win = MessageBox.Show("Parabéns você venceu.\n Deseja recomeçar ?", "Vitória", MessageBoxButtons.YesNo);
                if (win == DialogResult.Yes)
                {
                    //Application.Restart();
                    Form1 newGame = new Form1(gridSize);
                    newGame.Show();
                    this.Close();  
                }
                else
                {
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
                if (((Button)sender).ForeColor != Color.Black)
                {
                    if (((Button)sender).ForeColor == Color.Red)
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
                        ((Button)sender).ForeColor = Color.Red;
                        ((Button)sender).BackColor = Color.Red;
                    }
                }
            }

            if (e.Button == MouseButtons.Left && ((Button)sender).ForeColor != Color.Red)
            {
                if (((Button)sender).Text == "-1")
                {
                    DialogResult resposta;
                    resposta = MessageBox.Show("Você clicou em uma bomba.\n Deseja recomeçar ?", "Derrota", MessageBoxButtons.YesNo);
                    if (resposta == DialogResult.Yes)
                    {
                        //Application.Restart();
                        Form1 newGame = new Form1(gridSize);
                        newGame.Show();
                        this.Close();
                    }
                    else
                    {   
                        Program.MainFormInicio.Show();
                        this.Hide();
                    }
                }
                else if (((Button)sender).Text != "-1" && ((Button)sender).Text != " ")
                {

                    if (posicaoX % 2 == 0 && posicaoY % 2 != 0 || posicaoX % 2 == 1 && posicaoY % 2 == 0)
                    {
                        ((Button)sender).ForeColor = Color.Black;
                        ((Button)sender).BackColor = colorFirst;
                    }
                    else
                    {
                        ((Button)sender).ForeColor = Color.Black;
                        ((Button)sender).BackColor = colorSecond;
                    }


                    verifyWinning();
                }
                else if (((Button)sender).Text == " ")
                {

                    ((Button)sender).ForeColor = Color.Black;
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
                    if (buttons[i, j].ForeColor != Color.Black)
                    {
                        buttons[i, j].ForeColor = Color.Black;
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

            if (buttons[posicaoY, posicaoX].ForeColor == Color.Red)
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
                    if (buttons[i, j].ForeColor == Color.Red)
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
