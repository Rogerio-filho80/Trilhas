using System;
using System.Collections.Generic;
using AppBanco.Models;

namespace AppBanco.Entities
{
    public class Movimento
    {
        public DateTime Data { get; set; }
        public Enums.TipoMovimento Tipo { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }

        public Movimento()
        {
            Tipo = Enums.TipoMovimento.Credito;
            Data = DateTime.Now;
            Valor = 0;
            Descricao = string.Empty;
        }

        public override string ToString()
        {
            return "Data      : " + Data.ToString("dd/MM/yyyy") + "\n" +
                   "Tipo      : " + Tipo + "\n" +
                   "Descricao : " + Descricao + "\n" +
                   "Valor     : " + Valor.ToString("F2") + "\n";
        }

        public string CarregarMovimentoXML(string xmlMovimento)
        {
            string dataMovimento = RetornaTagXML(xmlMovimento, "Data");
            string tipoMovimento = RetornaTagXML(xmlMovimento, "TipoMovimento");
            string descricao = RetornaTagXML(xmlMovimento, "Descricao");
            string valor = RetornaTagXML(xmlMovimento, "Valor");

            if (!DateTime.TryParse(dataMovimento, out DateTime data))
            {
                return "Data inválida no movimento.";
            }

            if (!double.TryParse(valor, System.Globalization.NumberStyles.Any,
                                 new System.Globalization.CultureInfo("pt-BR"), out double valorConvertido))
            {
                return "Valor inválido no movimento.";
            }

            Data = data;
            Tipo = tipoMovimento == "Credito" ? Enums.TipoMovimento.Credito : Enums.TipoMovimento.Debito;
            Descricao = descricao;
            Valor = valorConvertido;

            return "";
        }

        private string RetornaTagXML(string xml, string tag)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "";
            }

            string abertura = "<" + tag + ">";
            string fechamento = "</" + tag + ">";

            int inicio = xml.IndexOf(abertura);
            if (inicio < 0)
            {
                return "";
            }

            inicio += abertura.Length;
            int fim = xml.IndexOf(fechamento, inicio);
            if (fim < 0)
            {
                return "";
            }

            return xml.Substring(inicio, fim - inicio);
        }

        public string GerarXML()
        {
            return "                              <Data>" + Data.ToString("dd/MM/yyyy") + "</Data>\n" +
                   "                              <TipoMovimento>" + Tipo + "</TipoMovimento>\n" +
                   "                              <Descricao>" + Descricao + "</Descricao>\n" +
                   "                              <Valor>" + Valor.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) + "</Valor>\n";
        }
    }
}