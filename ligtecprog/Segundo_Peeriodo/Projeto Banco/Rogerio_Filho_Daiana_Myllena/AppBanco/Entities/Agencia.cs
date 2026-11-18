using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AppBanco.Entities
{
    public class Agencia
    {
        public string Numero { get; set; }
        public string NomeAgencia { get; set; }
        public string Endereco { get; set; }
        public string EnderecoNumero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public List<Conta> Contas { get; set; }

        public Agencia()
        {
            Contas = new List<Conta>();
        }

        public override string ToString()
        {
            string retorno = "Numero.......: " + Numero + "\n" +
                             "Nome Agencia.: " + NomeAgencia + "\n" +
                             "Endereco.....: " + Endereco + "\n" +
                             "EnderecoNumero.: " + EnderecoNumero + "\n" +
                             "Bairro.......: " + Bairro + "\n" +
                             "Cidade.......: " + Cidade + "\n" +
                             "Estado.......: " + Estado + "\n";
            return retorno;
        }

        public void AddConta(Conta c)
        {
            Contas.Add(c);
        }

        public void RemoverConta(int index)
        {
            Contas.RemoveAt(index);
        }

        public string CarregarAgenciaXML(string xmlAgencia)
        {
            string CadastroAgencia = RetornaTagXML(xmlAgencia, "AgenciaCadastro");
            Numero = RetornaTagXML(CadastroAgencia, "Numero");
            NomeAgencia = RetornaTagXML(CadastroAgencia, "NomeAgencia");
            Endereco = RetornaTagXML(CadastroAgencia, "EnderecoAgencia");
            EnderecoNumero = RetornaTagXML(CadastroAgencia, "EnderecoNumero");
            Bairro = RetornaTagXML(CadastroAgencia, "Bairro");
            Cidade = RetornaTagXML(CadastroAgencia, "Cidade");
            Estado = RetornaTagXML(CadastroAgencia, "Estado");

            int contaIndex = 1;
            string dadosConta = RetornaTagXML(xmlAgencia, "Conta" + contaIndex);
            while (dadosConta != "")
            {
                Conta cont = new Conta();
                cont.CarregarContaXML(dadosConta);
                AddConta(cont);

                contaIndex++;
                dadosConta = RetornaTagXML(xmlAgencia, "Conta" + contaIndex);
            }

            return "";
        }

        private string RetornaTagXML(string xml, string tag)
        {
            string retorno = "";

            if (!string.IsNullOrEmpty(xml))
            {
                string padrao = $"<\\s*{tag}\\s*>(.*?)</{tag}>";
                Match match = Regex.Match(xml, padrao, RegexOptions.Singleline);

                if (match.Success)
                {
                    retorno = match.Groups[1].Value;
                }
            }

            return retorno;
        }
        public string GerarXML()
        {
            string XML = "";
            XML =  XML +"              <AgenciaCadastro>\n";
            XML =  XML +"              <Numero>" + Numero +"</Numero>\n";
            XML =  XML +"              <NomeAgencia>" + Numero +"</NomeAgencia>\n";
            XML =  XML +"              <EnderecoAgencia>" + Numero +"</EnderecoAgencia>\n";
            XML =  XML +"              <EnderecoNumeeo>" + Numero +"</EnderecoNumero>\n";
            XML =  XML +"              <Bairro>" + Numero +"</Bairro>\n";
            XML =  XML +"              <Cidade>" + Numero +"</Cidade>\n";
            XML =  XML +"              <Estado>" + Numero +"</Estado>\n";
            XML =  XML +"              </AgenciaCadastro>\n";
            XML =  XML +"              </Agencias>\n";
            int ContagemConta = 1;
            foreach (Conta ag in Contas)
            {
                XML += "              <Conta" + ContagemConta + ">\n";

                string rXML = ag.GerarXML();
                if (rXML != "")
                {
                    XML += rXML;
                }

                XML += "              </Conta" + ContagemConta + ">\n";
                ContagemConta++;
            }

            XML += "            </Contas>\n";
            return XML;
        }
    }
}