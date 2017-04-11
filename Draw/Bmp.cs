using System;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;



//Metodo para adicionar a imagem da flecha no Programa

namespace NewCadeirinhaIoT.Draw

{
    public class Bmp
    {
        string Src { get; set; }
        public Bmp(string direction)
        {
            Src = "y_arrow_";
            Src += direction + ".png";
        }

        public BitmapImage GetBitMapSource()
        {
            Uri uri = new Uri("ms-appx:///Images//" + Src, UriKind.Absolute);
            return new BitmapImage(uri);
        }

    }
}


