using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PointClassifier
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\Aryan\Documents\Visual Studio 2017\Projects\Perceptron\Debug\Perceptron.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void InitializePerceptron(short numberOfInputs, float threshold);
        [DllImport(@"C:\Users\Aryan\Documents\Visual Studio 2017\Projects\Perceptron\Debug\Perceptron.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern byte Train(float[] inputs, byte expectedOutput);
        [DllImport(@"C:\Users\Aryan\Documents\Visual Studio 2017\Projects\Perceptron\Debug\Perceptron.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern byte GetOutput(float[] inputs);

        bool trainingComplete = false;
        List<Ellipse> points = new List<Ellipse>();

        Graphics graphics;

        public Form1()
        {
            InitializeComponent();

            Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Initializing the window
            InitializeWindow();

            //Initializing the bot
            InitializePerceptron(2, (float)Math.Sqrt(Math.Pow(0.5f, 2)));

            graphics = this.CreateGraphics();
        }

        private void InitializeWindow()
        {
            this.Size = new Size(1280, 720);
            this.Text = "Point location classifier";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 5.0f);
            e.Graphics.DrawLine(pen, new Point(this.Width / 2, 0), new Point(this.Width / 2, this.Height));

            foreach(Ellipse el in points)
            {
                e.Graphics.FillEllipse(new SolidBrush(el.color), el.coords);
            }
        }

        private void mainButton_Click(object sender, EventArgs e)
        {
            if (!trainingComplete)
            {
                TrainBot();
                trainingComplete = true;
                mainButton.Text = "Generate Points";
            }
            else
            {
                GeneratePoints();
            }
        }

        private void TrainBot()
        {
            const short EPOCH = 20;
            Random rand = new Random();
            float accuracy = 0;

            while (accuracy < 95)
            {
                short correctGuesses = 0;
                for (int a = 0; a < EPOCH; ++a)
                {
                    float[] coords = { rand.Next(this.Width + 1), rand.Next(this.Height + 1) };
                    byte expected = Output(coords[0], coords[1]);

                    coords[0] = coords[0] / this.Width;
                    coords[1] = coords[1] / this.Height;
                    byte guess = Train(coords, expected);

                    if (guess == expected)
                        ++correctGuesses;
                }
                accuracy = ((float)correctGuesses / (float)EPOCH) * 100.0f;
            }
        }

        private byte Output(float x, float y)
        {
            //The equation of the line is x = Width / 2

            if (x - (this.Width / 2) < 0)
                return 0;

            return 1;
        }


        private void GeneratePoints()
        {
            const short NUM = 100;
            short correctGuesses = 0;
            Random rand = new Random();

            for (short a = 0; a < NUM; ++a)
            {
                int[] coords = { rand.Next(this.Width + 1), rand.Next(this.Height + 1)};

                float[] coordsNormalized = { (float)coords[0] / this.Width, (float)coords[1] / this.Height };
                byte output = GetOutput(coordsNormalized);

                if(output == 0)
                {
                    Ellipse pt = new Ellipse(Color.Green, new Rectangle(coords[0], coords[1] , 10, 10));
                    points.Add(pt);
                   graphics.FillEllipse(new SolidBrush(pt.color), pt.coords);
                    byte expected = Output(coords[0], coords[1]);
                    if (expected == 0)
                        correctGuesses++;
                    else
                        Train(coordsNormalized, expected);
                }
                else
                {
                    Ellipse pt = new Ellipse(Color.Red, new Rectangle(coords[0], coords[1], 10, 10));
                    points.Add(pt);
                    graphics.FillEllipse(new SolidBrush(pt.color), pt.coords);
                    byte expected = Output(coords[0], coords[1]);
                    if (expected == 1)
                        correctGuesses++;
                    else
                        Train(coordsNormalized, expected);
                }
            }

            Debug.Print((((float)correctGuesses / (float)NUM) * 100).ToString());
        }
    }
}
