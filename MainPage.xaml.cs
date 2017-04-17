using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

using NewCadeirinhaIoT.Draw;
using NewCadeirinhaIoT.Parameters;
using NewCadeirinhaIoT.PLC;
using Windows.UI.Xaml;
using System.Net;

namespace NewCadeirinhaIoT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainPage : Page
    {        
        IList<Cadeirinha> cadeirinhas = new List<Cadeirinha>();
        IList<Arrow> arrows = new List<Arrow>();
        public Plc plc;        
        public string parameters;
        public string popid;
        public string lastPopid;
        double screenRelation = 0.64; //0.764;// screenWidth / maxWidth;         
        double screenWidth;//= this.ActualWidth; //Tamanho da tela
        double screenHeight;// = this.ActualHeight;
        public int projetorNumber = 0;//int.Parse(RaspberryConfig.Projetor);
        const int maxWidth = 2000; //Constante do maior numero da tel
        const int projectorWidth = 2000; //Comprimento da projecao na longarina // Piso //       
        public bool tcpParameters = false; // Verifica se utiliza conexao do PLC ou WebService     
       
        public MainPage()
        {            
            InitializeComponent();
            Loaded += LoadedEvents;
            DispatcherTimer dispatcher = new DispatcherTimer();            
            dispatcher.Interval = new TimeSpan(0, 0, 15);
            dispatcher.Tick += VerifyConnection;
            dispatcher.Tick += VerifyPopId;  //new EventHandler<object>(VerifyPopId);
            //dispatcher.Tick += GetFromDB;
            dispatcher.Start();

            //ParametersAPI.RunAsync();
            //var result = ParametersAPI.Get();  //Pega parametros via WebApi
            //Debug.WriteLine(result);      

        }
        private void LoadedEvents(object sender, RoutedEventArgs e)
        {
            //RaspberryConfig.Init();
            //projetorNumber = int.Parse(RaspberryConfig.Projetor);            
            plc = new Plc("192.27.1.100", 0, 1);
            try
            {
                plc.PlcConnect();
                VerifyPopId(null,null);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
       

        //Metodo para utilizar Service
        //private async void GetFromDB(object sender, object e)
        //{
        //    popid = "480924";
        //    CadService.ServiceClient cadsrv = new CadService.ServiceClient();            
        //    var res = await cadsrv.GetCadeirinhasAsync(popid);           
        //    foreach(var r in res)            
        //        Debug.WriteLine(r);         
        //}

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
            //parameters = plc.GetCadeirinhaParameters();
            //popid = Popid.GetFromString(parameters);
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
            }       
            catch(WebException e )
            {
                Debug.WriteLine(":( Falha ao requisitar parametros" + e.Message);                
            }                        
        }
                 
        private void DrawArrowOnScreen()
        {
            Debug.WriteLine(">> Gerando objetos cadeirinhas \n");
            foreach (Cadeirinha cad in cadeirinhas)
                arrows.Add(new Arrow(cad, screenRelation));

            foreach (Arrow arrow in arrows)
            {
                int mainGridRow = arrow.Direction == "up" ? 3 : 1;
                Grid.SetRow(arrow.Img, mainGridRow);
                MainGrid.Children.Add(arrow.Img);
                Debug.WriteLine(string.Format(">> Cadeirinha ID: {0}, Lado: {1}, Posicao: {2}", arrow.Cad.ID, arrow.Cad.Side, arrow.Cad.Width));
            }
            Debug.WriteLine(">> Desenhando cadeirinhas na tela");
        }

        private void ClearScreen()
        {
            foreach (Arrow a in arrows)
                MainGrid.Children.Remove(a.Img);

            arrows.Clear();
            cadeirinhas.Clear();
            
            Debug.WriteLine(">> Apaganho cadeirinhas antigas \n");
        }

    }
}
