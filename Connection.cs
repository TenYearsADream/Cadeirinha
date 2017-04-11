using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml;



namespace NewCadeirinhaIoT
{
    public class Connection
    {
        public StreamSocket socket = null;
        public StreamSocketListener listener;
        public MainPage screen;        
        public string Port { get; set; }

        string parametes;

        public Connection(MainPage m)
        {
            screen = m;
            listener = new StreamSocketListener();
            listener.ConnectionReceived += OnConnected;            
        }

        public async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Port = "1024";
            await listener.BindEndpointAsync(null, Port);
        }
        public async void OnConnected(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            try
            {
                if (socket != null)
                    socket.Dispose(); //fecha o socket anterior

                socket = args.Socket;
                Windows.Storage.Streams.Buffer bufferRx = new Windows.Storage.Streams.Buffer(600);
                string respostaOk = "OK";
                IBuffer bufferTx = Encoding.ASCII.GetBytes(respostaOk).AsBuffer();
                
                await socket.InputStream.ReadAsync(bufferRx, bufferRx.Capacity, InputStreamOptions.None);
                parametes = Encoding.ASCII.GetString(bufferRx.ToArray()) .Replace("\0", "");
                //this.textoPLC = System.Text.Encoding.ASCII.GetString(bufferRx.ToArray());                                
                //Debug.WriteLine("Rx: " + ", from: " + socket.Information.RemoteAddress);
                await socket.OutputStream.WriteAsync(bufferTx);  //Envia Ok como resposta  
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnConnected Socket Exception " + ex.ToString());
            }
        }
    }
}
