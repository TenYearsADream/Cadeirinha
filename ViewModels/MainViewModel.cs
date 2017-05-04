using NewCadeirinhaIoT.Models;
using NewCadeirinhaIoT.Parameters;
using NewCadeirinhaIoT.PLC;
using NewCadeirinhaIoT.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;

namespace NewCadeirinhaIoT.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public IList<Cadeirinha> cadeirinhas { get; set; }
        public IList<Arrow> arrows { get; set; }
        IList<Rectangle> rectangles = new List<Rectangle>();
        public Plc plc;
        public string parameters;
        public string popid { get; set; }
        public string lastPopid;
        const double screenWidth = 1280;//= this.ActualWidth; //Tamanho da tela
        const double screenHeight = 1024;// = this.ActualHeight;
        public int projetorNumber;//int.Parse(RaspberryConfig.Projetor);
        const int maxWidth = 2000; //Tamanho da projecao
        const int projectorWidth = 2000; //Comprimento da projecao na longarina // Piso //       
        public bool tcpParameters = false; // Verifica se utiliza conexao do PLC ou WebService     
        double screenRelation = screenWidth / maxWidth; //~0.64
        public readonly ParametersAPI _parametersApi;

        Projetor pj = new Projetor(1, "192.27.1.194", 0);
        Uri path2 = new Uri(@"ms-appx://Assets/test1.txt"); //Config File

        public MainViewModel()
        {
            arrows = new List<Arrow>();
            cadeirinhas = new List<Cadeirinha>()
            {
                new Cadeirinha('V','D',800,0),
                new Cadeirinha('V','D',200,1),
                new Cadeirinha('V','D',1500,1),
                new Cadeirinha('V','D',300,2)
            };            

            foreach(var c in cadeirinhas)
            {
                arrows.Add(new Arrow(c, 0.64, 1));
            }                       

            LoadedEvents();            
            DispatcherTimer dispatcher = new DispatcherTimer();
            dispatcher.Interval = new TimeSpan(0, 0, 15);
            dispatcher.Tick += VerifyConnection;
            dispatcher.Tick += VerifyPopId;
            dispatcher.Start();
        }
        private void LoadedEvents()
        {
            //RaspberryConfig.Init();
            //projetorNumber = int.Parse(RaspberryConfig.Projetor);            
            plc = new Plc("192.27.1.100", 0, 1);
            try
            {
                plc.PlcConnect();
                VerifyPopId(null, null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void VerifyConnection(object sender, object e)
        {
            if (!plc.Connected)
            {
                try
                {
                    plc.PlcConnect();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        
        private void VerifyPopId(object o, object e)
        {
            if (!plc.Connected) return;
            popid = plc.GetPopIdFromDb();
            if (lastPopid != popid)
            {
                Debug.WriteLine(string.Format("{1} >> NOVO POPID: {0} \n", popid, DateTime.Now.ToString("h:mm:ss")));
                NewVehicle();
            }
        }


        private async void NewVehicle()
        {
            //ClearScreen();
            lastPopid = popid;
            try
            {
                parameters = ParametersAPI.Get(popid);
                cadeirinhas = Cadeirinha.GenerateCadeirinhas(parameters);
                var cadeirinhas2 = await _parametersApi.GetParametersAsync(popid);
               // DrawArrowOnScreen();
                //DrawSquareOnScreen();
            }
            catch (WebException e)
            {
                Debug.WriteLine(":( Falha ao requisitar parametros" + e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(":( Falha ao requisitar parametros" + e.Message);
            }
        }

        //private void DrawArrowOnScreen()
        //{
        //    /* SOLUCAO DA SETA */
        //    Debug.WriteLine(">> Gerando setas cadeirinhas \n");
        //    foreach (Cadeirinha cad in cadeirinhas)
        //        arrows.Add(new Arrow(cad, screenRelation, pj.Number));

        //    foreach (Arrow arrow in arrows)
        //    {
        //        int mainGridRow = arrow.Direction == "up" ? 3 : 1;
        //        Grid.SetRow(arrow.Img, mainGridRow);
        //        MainGrid.Children.Add(arrow.Img);
        //        Debug.WriteLine(string.Format(">> Cadeirinha ID: {0}, Lado: {1}, Posicao: {2}", arrow.Cad.ID, arrow.Cad.Side, arrow.Cad.Width));
        //    }
        //    Debug.WriteLine(">> Desenhando cadeirinhas na tela");

        //}

        //public void DrawSquareOnScreen()
        //{
        //    /*SOLUCAO DO RETANGULO*/
        //    Debug.WriteLine(">> Gerando Retangulos...");
        //    foreach (Cadeirinha cad in cadeirinhas)
        //    {
        //        Rectangle r = Rect.GenerateRectangle(cad, screenRelation, pj.Number);
        //        rectangles.Add(r);
        //        int mainGridRow = cad.Side == 'E' ? 3 : 1;
        //        Grid.SetRow(r, mainGridRow);
        //        MainGrid.Children.Add(r);
        //    }
        //}

        //private void ClearScreen()
        //{
        //    foreach (Arrow a in arrows)
        //        MainGrid.Children.Remove(a.Img);

        //    arrows.Clear();
        //    cadeirinhas.Clear();

        //    //foreach (Rectangle r in rectangles)
        //    //    MainGrid.Children.Remove(r);

        //    //rectangles.Clear();
        //    //cadeirinhas.Clear();

        //    Debug.WriteLine(">> Apaganho cadeirinhas antigas \n");
        //}

    }

}
