using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using Common;

namespace Client
{
    public partial class Form1 : Form
    {

        private ICommon myICommon2;
        private static string[] arrNames = { "Cat", "Dog", "Flower" };
        private static string[] arrColors = { "Red", "Green", "Blue" };

        private static Random myRand = new Random();
        const int LabelSize = 80;
        const int N = 14;
        private Label[] arrLabels = new Label[N];
        private Label[] arrLabelsResult = new Label[N];

        public Form1()
        {
            InitializeComponent();

            this.Text = arrNames[myRand.Next(3)];

            this.ClientColor_label.Text = arrColors[myRand.Next(3)];
            this.ClientColor_label.ForeColor = Color.FromName(this.ClientColor_label.Text);

            for (int i = 0; i < N; i++)
            {
                arrLabels[i] = new Label();
                arrLabels[i].Width = arrLabels[i].Height = LabelSize;
                arrLabels[i].Location = new Point(2 + (LabelSize + 2) * i, 27);
                arrLabels[i].Name = arrNames[myRand.Next(3)];
                arrLabels[i].Image = Image.FromFile("../.../" + arrLabels[i].Name + ".jpg");
                arrLabels[i].BorderStyle = BorderStyle.FixedSingle;

                switch (myRand.Next(3))
                {
                    case 0: arrLabels[i].BackColor = Color.FromArgb(myRand.Next(120, 256), 0, 0); break;
                    case 1: arrLabels[i].BackColor = Color.FromArgb(0, myRand.Next(120, 256), 0); break;
                    case 2: arrLabels[i].BackColor = Color.FromArgb(0, 0, myRand.Next(120, 256)); break;
                }
                this.Controls.Add(arrLabels[i]);

                arrLabelsResult[i] = new Label();
                arrLabelsResult[i].Width = arrLabelsResult[i].Height = LabelSize;
                arrLabelsResult[i].Location = new Point(2 + (LabelSize + 2) * i, 115);
                arrLabelsResult[i].BackColor = Color.White;
                arrLabelsResult[i].BorderStyle = BorderStyle.FixedSingle;

                this.Controls.Add(arrLabelsResult[i]);
            }
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel, false);

            myICommon2 = (ICommon)Activator.GetObject(typeof(ICommon),
                 "http://localhost:1234/_Server_");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Arr_Animal_Color[] arr_Animal_Color = new Arr_Animal_Color[arrLabels.Length];
            for (int i = 0; i < arrLabels.Length; i++)
            {
                arr_Animal_Color[i] = new Arr_Animal_Color();
                arr_Animal_Color[i].Animal = arrLabels[i].Name;
                arr_Animal_Color[i].ColorR = (int)arrLabels[i].BackColor.R;
                arr_Animal_Color[i].ColorG = (int)arrLabels[i].BackColor.G;
                arr_Animal_Color[i].ColorB = (int)arrLabels[i].BackColor.B;
            }
            myICommon2.add_Client(arr_Animal_Color);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            arrLabel_Color_Animal_Counter temp;
            temp = myICommon2.getData(this.ClientColor_label.Text, this.Text);
            for (int i = 0; i < temp.arr_animal_color.Length; i++)
            {
                arrLabelsResult[i].Image = Image.FromFile("../.../" + temp.arr_animal_color[i].Animal + ".jpg");
                arrLabelsResult[i].BackColor = Color.FromArgb(temp.arr_animal_color[i].ColorR, temp.arr_animal_color[i].ColorG, temp.arr_animal_color[i].ColorB);
            }

        }
    }
}



