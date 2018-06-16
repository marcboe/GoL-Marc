using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Game_of_Life_Marc
{
    public partial class Form1 : Form
    {
        //Timer
        public static System.Windows.Forms.Timer timer_1 = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();
            IniLiveArea();
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawCell);
            this.Invalidate();
            timer_1.Tick += new EventHandler(timer_1_Tick);
            
            
        }

        public Point MouseIni(Point p)
        {
            // Normierung auf Zeichenfeld,  5X5 Raster,   Oben Links ist 0/0
            // Location der Box :  260/10
            int KoordX = (p.X - this.Location.X - 260 - 5)/5;  // - 8 ist linker Rand
            int KoordY = (p.Y - this.Location.Y - 10 - 31)/5; // 31 ist oberer Rand
            p.X = KoordX;
            p.Y = KoordY;
            return p;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point p = new Point();
            p = MouseIni(Control.MousePosition);

            //Belegen und leeren
            if (LiveArea[p.X, p.Y].GetState())
                LiveArea[p.X, p.Y].SetState(false);
            else
                LiveArea[p.X, p.Y].SetState(true);
            this.Invalidate();
        }
        //Zeichnen
        public void DrawCell( object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics draw = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.Black);
            draw.Clear(System.Drawing.SystemColors.Control);

            for (int i =0; i < 160; i++)
            {
                for (int j = 0; j < 116; j++)
                {
                    if (LiveArea[i, j].GetState())
                        draw.FillRectangle(brush, i * 5 + 260, j * 5 + 10, 5, 5);

                }
            }
        }

       

        private Cell[,] LiveArea; 

        private void IniLiveArea()
        {
            LiveArea = new Cell[160, 116];
            for (int i=0; i<160; i++)
            {
                for (int j =0; j < 116; j++)
                    LiveArea[i, j] = new Cell();
            }
        }

        

        private void timer_1_Tick(object sender, EventArgs e)
        {
            animation();
        }

      

        //Animation
        private void animation()
        {
            //Nachbarn ermitteln
             for (int i = 0; i < 160; i++)
            {
                for (int k = 0; k< 116;k++)
                {
                    int x, y, count = 0;
                    for (int a= -1; a<=1;a++)
                    {
                        for (int b = -1; b <= 1; b++)
                        {
                            x = (i +160 + a) % 160;
                            y = (k + 116 + b) % 116;
                            if (!(a==0 && b ==0))
                            {
                                if (LiveArea[x, y].GetState())
                                    count++;
                            }
                        }
                    }
                      LiveArea[i,k].SetEnv(count);  
                }
                
            }
            // Lebensbedingungen Prüfen
            for (int i = 0; i < 160; i++)
            {
                for (int K =0;K <116; K++)
                {
                    if ((LiveArea[i, K].GetEnv() == 3) && !LiveArea[i, K].GetState())
                        LiveArea[i, K].SetState(true);
                    else
                    {
                        if (LiveArea[i,K].GetEnv() < 2 || LiveArea[i,K].GetEnv() >3 && LiveArea[i,K].GetState())
                            LiveArea[i,K].SetState(false);
                    }
                }
            }
            this.Invalidate();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer_1.Enabled)
            {
                timer_1.Enabled = true;
                timer_1.Start();
                button1.Text = "Stopp";
            }
            else
            {
                timer_1.Enabled = false;
                timer_1.Stop();
                button1.Text = "Start";
            }
        }
    }
}
