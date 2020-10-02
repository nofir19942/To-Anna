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
using System.Collections;
using Common;
namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HttpChannel chnl = new HttpChannel(1234);
            ChannelServices.RegisterChannel(chnl, false);

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(ServerPart),
                "_Server_",
                WellKnownObjectMode.Singleton);
        }
    }

    class ServerPart : MarshalByRefObject, ICommon
    {
        private List<Arr_Animal_Color> commonList = new List<Arr_Animal_Color>();
        private static string[] arrColors = { "Red", "Green", "Blue" };
        private static string[] arrNames = { "Cat", "Dog", "Flower" };
        public void add_Client(Arr_Animal_Color[] arr_Animal_Color)
        {
            for (int i = 0; i < arr_Animal_Color.Length; i++)
            {
                commonList.Add(arr_Animal_Color[i]);
            }
        }

        public arrLabel_Color_Animal_Counter getData(string Color,string Animal)
        {
            List<Arr_Animal_Color> tempList = new List<Arr_Animal_Color>();
            for (int i = 0; i < commonList.Count; i++)
            {
                if(arrColors[0] == Color && commonList[i].ColorR > 0 && commonList[i].Animal == Animal)
                {
                    tempList.Add(commonList[i]);    
                }
                if (arrColors[1] == Color && commonList[i].ColorG > 0 && commonList[i].Animal == Animal)
                {
                    tempList.Add(commonList[i]);
                }
                if (arrColors[2] == Color && commonList[i].ColorB > 0 && commonList[i].Animal == Animal)
                {
                    tempList.Add(commonList[i]);
                }
            }
            arrLabel_Color_Animal_Counter temp = new arrLabel_Color_Animal_Counter();
            temp.arr_animal_color = new Arr_Animal_Color[tempList.Count];
            for (int i = 0; i < tempList.Count; i++)
            {
                temp.arr_animal_color[i] = tempList[i];
            }
            return temp;
        }
    }
}