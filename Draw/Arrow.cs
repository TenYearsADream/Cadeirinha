using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


//Classe para criar as flechas 

namespace NewCadeirinhaIoT.Draw

{
    public class Arrow 
    {
        private int ArrowHeight { get; set; }
        public string Direction { get; protected set; }
        public Cadeirinha Cad { get; set; }
        public Image Img = new Image();

        public Arrow(Cadeirinha cad)
        {
            ArrowHeight = 100;            
            Cad = cad;
            Direction = GetArrowDirection();
            Img.Source = new Bmp(Direction).GetBitMapSource();
            Img.Height = ArrowHeight;
            Img.Margin = new Thickness() { Left = Cad.Width - ArrowHeight / 2 };
            Img.HorizontalAlignment = HorizontalAlignment.Left;
        }
        public Arrow(Cadeirinha cad, double screenRelation) : this(cad)
        {            
            Img.Margin = new Thickness() { Left = CalculateArrowMargin(screenRelation) };            
        }

        private string GetArrowDirection()
        {
            if (Cad.Side == 'E')
                return "up";
            return "down";
        }

        private double CalculateArrowMargin(double screenResolution)
        {
            if (Cad.Width < ArrowHeight/2) //Hard Coded
                return 0;
            return (Cad.Width * screenResolution) - (ArrowHeight / 2);            
        }

        public void UpdateMargin(double margin)
        {
            if (margin < 160)
                Img.Margin = new Thickness() { Left = 0 };
            else
                Img.Margin = new Thickness() { Left = margin - (ArrowHeight / 2) };
        }

        public void SetArrowHeight(int height)
        {
            ArrowHeight = height;
        }      
    }


}