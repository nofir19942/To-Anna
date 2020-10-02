using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Drawing;
using System.Collections;

namespace Common
{
        [Serializable]
        public class Arr_Animal_Color
        {
            public string Animal;
            public int ColorR;
            public int ColorG;
            public int ColorB;
        }

        [Serializable]
        public class arrLabel_Color_Animal_Counter
        {
            public Arr_Animal_Color[] arr_animal_color;
        }


        public interface ICommon
        {
            void add_Client(Arr_Animal_Color[] arr_Animal_Color);
            arrLabel_Color_Animal_Counter getData(string Color,String Animal);
        }
}
