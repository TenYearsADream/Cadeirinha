using System;
using System.Collections.Generic;
using Windows.Storage;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.UI.Xaml;


namespace NewCadeirinhaIoT.Parameters
{
    public abstract class RaspberryConfig
    {
        public static string ConfigContent { get; set; }
        public static string Projetor { get; set; }
        public static string IP { get; set; }

        public static void Init()
        {            
            ReadConfigFile();
            do
            {
                Projetor = GetProjector();
            } while (Projetor == null);
            Debug.WriteLine("Configuracao completamente carregada");
        }

        public static async void ReadConfigFile()
        {
            Debug.WriteLine("Lendo arquivo de configuracao...");
            string fileName = "config.ini";
            try
            {
                StorageFolder fold = ApplicationData.Current.LocalFolder;
                StorageFile file = await fold.GetFileAsync(fileName);
                ConfigContent = await FileIO.ReadTextAsync(file);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);                                
            }

        }

        public static string GetParameter(string pattern)
        {
            Regex r;
            Match m;
            r = new Regex(pattern, RegexOptions.IgnoreCase);            
            m = r.Match(ConfigContent);
            return m.Groups["value"].Value;
        }

        public static string GetProjector()
        {
            return GetParameter(@"PROJETOR=(?<value>\w.+)");
        }

     


    }
}
