using System;
using Sharp7;
using System.Text;

namespace NewCadeirinhaIoT.PLC
{
    public class Plc : S7Client
    {
        public string IP { get; set; }
        public int Rack { get; set; }
        public int Slot { get; set; }
        public Plc(){}
        public Plc(string ip, int rack, int slot)
        {
            IP = ip;
            Rack = rack;
            Slot = slot;            
        }
        public int PlcConnect()
        {            
            if (!this.Connected)
            {                
                int res = ConnectTo(IP, Rack, Slot);
                if (res == 0)
                    return res;
                else
                    throw new Exception("Erro ao tentar conectar com o PLC");
            }
            throw new Exception("Ja existe uma conexao ativa com esse PLC...");
        }           


        public string GetCadeirinhaParameters()
        {
            int dbNumber = 133;
            int initFrom = 600;
            int sizeOfBuffer = 600;
            byte[] buff = new byte[sizeOfBuffer];            
            this.ReadArea(S7Consts.S7AreaDB, dbNumber, initFrom, sizeOfBuffer, S7Consts.S7WLByte, buff);
            return Encoding.UTF8.GetString(buff);
        } 

        public string GetPopIdFromDb()
        {
            int dbNumber = 250;
            int initFrom = 120;
            int sizeOfBuffer = 8;
            byte[] buff = new byte[sizeOfBuffer];
            this.ReadArea(S7Consts.S7AreaDB, dbNumber, initFrom, sizeOfBuffer , S7Consts.S7WLByte, buff);
            return Encoding.UTF8.GetString(buff);
        }

        


             
              
    }
}



