using NewCadeirinhaIoT.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;

namespace NewCadeirinhaIoT.Models
{
    public static class Rect
    {

        public  static Rectangle GenerateRectangle(Cadeirinha cad, double screenRelation, int projetor)
        {
            Rectangle rect = new Rectangle();
            rect.Height = 120; //HardCoded
            rect.Width = 30;  //HardCoded
            rect.Margin = CalculateRectMargin(cad, screenRelation, projetor);
            return rect;
        }

        private static Thickness CalculateRectMargin(Cadeirinha cad, double screenRelation, int projetor)
        {
            double pjRelation = (projetor - 1) * 2000;
            Thickness tk = new Thickness();
            tk.Left = (cad.Width * screenRelation) - (15 * screenRelation) - pjRelation;
            if (pjRelation < 0)
            {
                tk.Left = 0;
            }                   
            return tk;
        }


    }
}



