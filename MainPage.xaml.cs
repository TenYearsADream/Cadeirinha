using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using NewCadeirinhaIoT.Draw;
using NewCadeirinhaIoT.Parameters;
using NewCadeirinhaIoT.PLC;
using Windows.UI.Xaml;
using System.Net;
using NewCadeirinhaIoT.Models;
using Windows.UI.Xaml.Shapes;

namespace NewCadeirinhaIoT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainPage : Page
    {
        IList<Cadeirinha> cadeirinhas = new List<Cadeirinha>();
        IList<Arrow> arrows = new List<Arrow>();
        IList<Rectangle> rectangles = new List<Rectangle>();
        public Plc plc;
        public string parameters;
        public string popid;
        public string lastPopid;
        const double screenWidth = 1280;//= this.ActualWidth; //Tamanho da tela
        const double screenHeight = 1024;// = this.ActualHeight;
        public int projetorNumber = 0;//int.Parse(RaspberryConfig.Projetor);
        const int maxWidth = 2000; //Tamanho da projecao
        const int projectorWidth = 2000; //Comprimento da projecao na longarina // Piso //       
        public bool tcpParameters = false; // Verifica se utiliza conexao do PLC ou WebService     
        double screenRelation = screenWidth / maxWidth; //~0.64

        Projetor pj = new Projetor(1,"192.27.1.194",0,3000);

        public MainPage()
        {
            InitializeComponent();
            Loaded += LoadedEvents;
            DispatcherTimer dispatcher = new DispatcherTimer();
            dispatcher.Interval = new TimeSpan(0, 0, 15);
            dispatcher.Tick += VerifyConnection;
            dispatcher.Tick += VerifyPopId;
            dispatcher.Start();
        }
        private void LoadedEvents(object sender, RoutedEventArgs e)
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


        private void NewVehicle()
        {
            ClearScreen();
            TextPopid.Text = popid;
            lastPopid = popid;
            //parameters = parameters == "" ? new ParametersTest().GetRandom() : parameters;    
            try
            {
                parameters = ParametersAPI.Get(popid);
                cadeirinhas = Cadeirinha.GenerateCadeirinhas(parameters);
                DrawArrowOnScreen();
                //DrawSquareOnScreen();
            }
            catch (WebException e)
            {
                Debug.WriteLine(":( Falha ao requisitar parametros" + e.Message);
            }
        }

        private void DrawArrowOnScreen()
        {
            /* SOLUCAO DA SETA */
            Debug.WriteLine(">> Gerando setas cadeirinhas \n");
            foreach (Cadeirinha cad in cadeirinhas)
                arrows.Add(new Arrow(cad, screenRelation, pj.Number));

            foreach (Arrow arrow in arrows)
            {
                int mainGridRow = arrow.Direction == "up" ? 3 : 1;
                Grid.SetRow(arrow.Img, mainGridRow);
                MainGrid.Children.Add(arrow.Img);
                Debug.WriteLine(string.Format(">> Cadeirinha ID: {0}, Lado: {1}, Posicao: {2}", arrow.Cad.ID, arrow.Cad.Side, arrow.Cad.Width));
            }
            Debug.WriteLine(">> Desenhando cadeirinhas na tela");

        }

        public void DrawSquareOnScreen()
        {
            /*SOLUCAO DO RETANGULO*/
            Debug.WriteLine(">> Gerando Retangulos...");
            foreach (Cadeirinha cad in cadeirinhas)
            {
                Rectangle r = Rect.GenerateRectangle(cad, screenRelation, pj.Number);
                rectangles.Add(r);
                int mainGridRow = cad.Side == 'E' ? 3 : 1;
                Grid.SetRow(r, mainGridRow);
                MainGrid.Children.Add(r);
            }
        }

        private void ClearScreen()
        {
            foreach (Arrow a in arrows)
                MainGrid.Children.Remove(a.Img);

            arrows.Clear();
            cadeirinhas.Clear();

            //foreach (Rectangle r in rectangles)
            //    MainGrid.Children.Remove(r);

            //rectangles.Clear();
            //cadeirinhas.Clear();

            Debug.WriteLine(">> Apaganho cadeirinhas antigas \n");
        }

    }
}
