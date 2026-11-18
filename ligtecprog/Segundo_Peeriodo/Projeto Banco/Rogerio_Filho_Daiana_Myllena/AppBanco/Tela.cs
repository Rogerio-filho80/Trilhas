using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.IO;

namespace AppBanco
{
    public class Tela
    {
        public static int MenuCriarBanco{get; set;}
        public static int Menu{get; set;}
        public static int MenuSair{get; set; }
        public static FileInfo[] ArquivosXML {get; set; }
        public static string ImprimirCampo(string Texto, int Comprimento)
        {
            string Retorno;

            if (Texto.Length > Comprimento)
            {
                Retorno = Texto.Substring(0, Comprimento);
            }
            else
            {
                Retorno = Texto;
                int NumeroEspaco = Comprimento - Texto.Length;

                for (int I = 0; I < NumeroEspaco; I++)
                {
                    Retorno = Retorno + " ";
                }
            }

            return Retorno;
        }

        public static string ImprimirValor(double Valor, int Comprimento)
        {
            string sValor = Valor.ToString("F2");
            string Retorno;

            if (sValor.Length > Comprimento)
            {
                Retorno = sValor.Substring(sValor.Length - Comprimento, Comprimento);
            }
            else
            {
                Retorno = "";
                int NumeroEspaco = Comprimento - sValor.Length;

                for (int I = 0; I < NumeroEspaco; I++)
                {
                    Retorno = Retorno + " ";
                }

                Retorno = Retorno + sValor;
            }

            return Retorno;
        }

        public static string RetornaCaixaCaracter(string C, int Comprimento)
        {
            string Retorno = "";

            for (int I = 0; I < Comprimento; I++)
            {
                Retorno = Retorno + C;
            }

            return Retorno;
        }
        public static void MenuPrincipal()
        {
            int OpcaoMenu =0;
            try { Console.Clear(); } catch { }
            Console.WriteLine("|" + RetornaCaixaCaracter("-",78) + "|");
            Console.WriteLine("|" + ImprimirCampo("App Banco Versão 1.0",78) + "|" );
            Console.WriteLine("|" + RetornaCaixaCaracter("-",78) + "|");
            Console.WriteLine("|" + ImprimirCampo("Lista de Bancos exixtentes",78) + "|" );
            Console.WriteLine("|" + RetornaCaixaCaracter("-",78) + "|");


            string caminho = Directory.GetCurrentDirectory();
            caminho = caminho + @"\Dados\";

            DirectoryInfo dirInfo = new DirectoryInfo(caminho);
            Console.WriteLine("|" + RetornaCaixaCaracter("-",78) + "|");


            ArquivosXML = dirInfo.GetFiles("*.xml");

            foreach (FileInfo Arquivo in ArquivosXML)
            {
                OpcaoMenu++;
                Console.WriteLine("|" + ImprimirCampo(OpcaoMenu.ToString(),3) + " - " + ImprimirCampo (Arquivo.Name, 70) + "|");
            }
            Console.WriteLine("|" + RetornaCaixaCaracter("-",78) + "|");
            Console.WriteLine("|" + ImprimirCampo("Lista de Bancos exixtentes",78) + "|" );

            // Opção de criar novo banco
            OpcaoMenu++;
            MenuCriarBanco = OpcaoMenu;
            Console.WriteLine("|" + ImprimirCampo(OpcaoMenu.ToString(),3) + " - " + ImprimirCampo("Criar um novo banco", 72) + "|");

            OpcaoMenu++;
            MenuSair = OpcaoMenu;
            Console.WriteLine("|" + ImprimirCampo(OpcaoMenu.ToString(),3) + " - " + ImprimirCampo("Fechar o sistema", 72) + "|");
            Console.WriteLine("|" + RetornaCaixaCaracter("-" , 78) + "|");

            Console.Write("Escolha uma opcao: ");
            string entrada = Console.ReadLine() ?? "";
            if (int.TryParse(entrada, out int opcao))
            {
                Menu = opcao;
            }
            else
            {
                Menu = MenuSair;
            }
        }
    }
}