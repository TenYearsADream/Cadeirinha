using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCadeirinhaIoT.Models
{
    public class Projetor
    {
        public int  Number { get; set; }
        public string IP { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double OffsetStation { get; set; }

        public Projetor(int number, string ip, double width, double height)
        {
            Number = number;
            IP = ip;
            Width = width;
            Height = height;
        }







    }
}
