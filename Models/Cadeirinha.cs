using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

//Classe para criar as cadeirinhas

namespace NewCadeirinhaIoT.Models
{
    public class Cadeirinha //Nome em portugues pra dar identidade ao projeto
    {
        public char Type { get; set; } //Armazena X ou V
        public char Side { get; set; } //Armazena E ou D
        public int Width { get; set; } //Comprimento em relacao ao eixo de apoio traseiro
        public int ID { get; private set; }
        public int AD { get; set; }
        
                
        public Cadeirinha(char type, char side, int width, int id)
        {
            Type = type;
            Side = side;
            Width = width;
            ID = id;
        }

        public static List<Cadeirinha> GenerateCadeirinhas(string plcString)
        {
            int id = 0;
            string cadPattern = @"(V|X)HL(E|D)(\d{2,4})";
            var cadeirinhas = new List<Cadeirinha>();
            foreach (Match match in Regex.Matches(plcString, cadPattern))
            {
                char type = char.Parse(match.Groups[1].ToString());
                char side = char.Parse(match.Groups[2].ToString());
                int width = int.Parse(match.Groups[3].ToString());
                cadeirinhas.Add(new Cadeirinha(type, side, width, id));
                id++;
            }
            return cadeirinhas;
        }
    }

}
