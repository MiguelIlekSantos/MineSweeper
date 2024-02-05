using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Page2 : Form
    {
        private int totalContentHeight;

        public Page2()
        {
            InitializeComponent();
            AdicionarConteudoAoPanel();
            vScrollBar1.Scroll += VScrollBar1_Scroll;

            panel5.MouseWheel += panel5_MouseWheel;
            panel5.Width = this.Width;

        }

        private void AdicionarConteudoAoPanel()
        {
            Dictionary<string, string> LabelTexts = new Dictionary<string, string>
            {
                { "Passo1", "Passo 1: Abertura do Jogo" },
                { "Passo1Details", "Execute o aplicativo para iniciar o jogo. O tamanho do campo minado e o esquema de cores podem ser escolhidos no início." },
                { "Passo2", "Passo 2: Interface do Jogo" },
                { "Passo2Details", "A interface do jogo inclui um campo de quadrados representados por botões." },
                { "Passo3", "Passo 3: Interações com os quadrados" },
                { "Passo3Details", "Clique com o botão direito em um botão para marcar ou desmarcar uma possível mina. Botões marcados com uma bandeira são considerados minas. Cada clique com o botão direito alterna entre marcação e desmarcação. Clique com o botão esquerdo em um botão para revelar seu conteúdo. Números são exibidos, indicando quantas minas estão adjacentes ao botão. Um quadrado em branco indica que não há minas vizinhas. Se clicar em uma mina, o jogo termina. Clique no botão do meio para abrir automaticamente todos os botões ao redor se o número de bandeiras marcadas for igual ao número de minas ao redor. Isso é útil para acelerar o processo quando todas as minas ao redor de um botão são conhecidas." },
                { "Passo4", "Passo 4: Vitória ou Derrota" },
                { "Passo4Details", "Se todas as minas estiverem marcadas corretamente e todos os botões estiverem revelados, o jogador vence. Se clicar em uma mina, o jogador perde. Uma mensagem de vitória ou derrota é exibida, junto com o tempo decorrido durante o jogo." },
                { "Passo5", "Passo 5: Reiniciar o Jogo" },
                { "Passo5Details", "Após a vitória ou derrota, é possível reiniciar o jogo clicando em 'Sim' na mensagem. O jogo reiniciado mantém o mesmo tamanho de campo e esquema de cores." },
                { "Passo6", "Passo 6: Fechar o Jogo" },
                { "Passo6Details", "Pressione a tecla 'Esc' para fechar o jogo a qualquer momento." }
            };

            Dictionary<string, string> imgs = new Dictionary<string, string>
            {
                { "img1", "sizecolor.png" },
                { "img2", "play.png" },
                { "img3", "interacting.png"},
                { "img4", "lose.png"},
                { "img5", "win.png"}
            };
              

            string pasta_aplicacao = "";
            pasta_aplicacao = Application.StartupPath + @"\imgs\";

            int topOffset = 0;

            for (int a = 1; a <= LabelTexts.Count; a++)
            {
                string passoKey = string.Format("Passo{0}", a);
                string detailsKey = string.Format("{0}Details", passoKey);

                if (LabelTexts.ContainsKey(passoKey))
                {
                    panel5.Controls.Add(new Label()
                    {
                        Text = LabelTexts[passoKey],
                        Top = topOffset,
                        Width = panel5.Width / 2,
                        Height = panel5.Height / 15,
                        Font = new System.Drawing.Font("Microsoft YaHei", 15, FontStyle.Bold),
                        ForeColor = Color.Blue
                    });
                    topOffset += 40;
                }

                if (LabelTexts.ContainsKey(detailsKey))
                {
                    panel5.Controls.Add(new Label()
                    {
                        Text = LabelTexts[detailsKey],
                        Top = topOffset,
                        Width = panel5.Width + 300,
                        Height = panel5.Height / 5,
                        Font = new System.Drawing.Font("Arial", 12),
                        ForeColor = Color.Black
                    });
                    topOffset += 100;

                    
                    if(a == 1 || a == 2 || a == 3){
                        string key = imgs["img" + a.ToString()];
                        
                        PictureBox pictureBox = new PictureBox
                        {
                            Image = Image.FromFile(pasta_aplicacao + key), 
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Width = 600,
                            Height = 350,
                            Top = topOffset,
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        panel5.Controls.Add(pictureBox);
                        pictureBox.BringToFront();

                        topOffset += 360;

                    }
                    if(a == 5){
                        for (int b = 4; b <= 5; b++)
                        {
                            string key = imgs["img" + b.ToString()];

                            PictureBox pictureBox = new PictureBox
                            {
                                Image = Image.FromFile(pasta_aplicacao + key), 
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Width = 600,
                                Height = 350,
                                Top = topOffset,
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            panel5.Controls.Add(pictureBox);
                            pictureBox.BringToFront();

                            topOffset += 360;
                        }
                    }

                    // Armazena a altura total do conteúdo
                    totalContentHeight = topOffset;

                    // Ajusta a altura máxima da barra de rolagem
                    AdjustScrollBar();


                }

            }

            
            foreach (Control control in panel5.Controls)
            {
                if (control is Label)
                {
                    //((Label)control).AutoSize = true;
                }
            }
             

        
        }

        private void AdjustScrollBar()
        {
            // Ajusta a altura máxima da barra de rolagem
            int visibleHeight = panel5.Height;
            vScrollBar1.Maximum = visibleHeight;

            // Ajusta a pequena alteração da barra de rolagem
            vScrollBar1.SmallChange = 5;  // Ajuste conforme necessário
        }


        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            foreach (Control control in panel5.Controls)
            {
                control.Top = control.Top + e.OldValue - e.NewValue;
            }
        }

        private void panel5_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;

            if (vScrollBar1.Value - delta / 20 < vScrollBar1.Minimum)
            {
                return;
            }
            else if (vScrollBar1.Value - delta / 20 > vScrollBar1.Maximum)
            {
                return;
            }
            else
            {
                vScrollBar1.Value -= delta / 20;
            }

            foreach (Control control in panel5.Controls)
            {
                control.Top = control.Top + (delta / 20) * vScrollBar1.SmallChange;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }





    }
}