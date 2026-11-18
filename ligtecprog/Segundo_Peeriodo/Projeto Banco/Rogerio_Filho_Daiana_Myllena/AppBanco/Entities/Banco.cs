using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AppBanco.Entities
{
    public class Banco
    {
        public string Numero { get; set; }
        public string NomeBanco { get; set; }
        public List<Agencia> Agencias { get; set; }

        public Banco()
        {
            Agencias = new List<Agencia>();
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += "-----------------------------------------\n";
            retorno += "Numero.....: " + Numero + "\n";
            retorno += "Banco......: " + NomeBanco + "\n";
            retorno += "-----------------------------------------\n";

            foreach (Agencia ag in Agencias)
            {
                retorno += ag.ToString() + "\n";
                retorno += "-----------------------------------------\n";

                foreach (Conta c in ag.Contas)
                {
                    retorno += c.ToString() + "\n";
                    retorno += "-----------------------------------------\n";

                    foreach (Movimento m in c.Movimentos)
                    {
                        retorno += m.ToString() + "\n";
                        retorno += "-----------------------------------------\n";
                    }
                }
            }
            return retorno;
        }

        public void AddAgencia(Agencia ag)
        {
            Agencias.Add(ag);
        }

        public void RemoverAgencia(int index)
        {
            Agencias.RemoveAt(index);
        }

        // Retorna "" em caso de sucesso, ou a mensagem de erro.
        public string CarregarBanco(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
                return "Arquivo não encontrado.";

            string meuXML = File.ReadAllText(caminhoArquivo);

            if (string.IsNullOrWhiteSpace(meuXML))
                return "Arquivo inválido: está vazio.";

            string dadosBanco = ExtrairBloco(meuXML, "Banco");
            if (dadosBanco == "")
                return "Arquivo inválido: tag <Banco> não encontrada.";

            // Informações cadastrais do banco
            string cadastroBanco = RetornaTagXML(dadosBanco, "Bancocadastrado");
            if (string.IsNullOrWhiteSpace(cadastroBanco))
            {
                cadastroBanco = dadosBanco;
            }
            Numero = RetornaTagXML(cadastroBanco, "Numero");
            NomeBanco = RetornaTagXML(cadastroBanco, "NomeBanco");
            if (string.IsNullOrWhiteSpace(NomeBanco))
            {
                NomeBanco = RetornaTagXML(dadosBanco, "NomeBanco");
            }

            // Agências
            Agencias.Clear();
            string blocoAgencias = RetornaTagXML(dadosBanco, "Agencias");
            int ContaAgencia = 1;
            string dadosAgencia = RetornaTagXML(blocoAgencias, "Agencia" + ContaAgencia);

            while (dadosAgencia != "")
            {
                Agencia ag = new Agencia();
                ag.CarregarAgenciaXML(dadosAgencia);
                AddAgencia(ag);

                ContaAgencia++;
                dadosAgencia = RetornaTagXML(blocoAgencias, "Agencia" + ContaAgencia);
            }

            return "";
        }

        private string ExtrairBloco(string xml, string tag)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "";
            }

            int inicioTag = xml.IndexOf("<" + tag, StringComparison.OrdinalIgnoreCase);
            if (inicioTag < 0)
            {
                return "";
            }

            int inicioConteudo = xml.IndexOf(">", inicioTag);
            if (inicioConteudo < 0)
            {
                return "";
            }
            inicioConteudo++;

            int fimTag = xml.LastIndexOf("</" + tag + ">", StringComparison.OrdinalIgnoreCase);
            if (fimTag < inicioConteudo)
            {
                return "";
            }

            return xml.Substring(inicioConteudo, fimTag - inicioConteudo);
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
            XML = XML + "       <XML>\n";
            XML = XML + "       <Banco>\n";
            XML = XML + "           <Bancocadastrado>\n";
            XML = XML + "               <Numero>" + Numero + "</Numero>\n";
            XML = XML + "               <NomeBanco>" + NomeBanco + "</NomeBanco>\n";
            XML = XML + "           </Bancocadastrado>\n";
            XML = XML + "           <Agencias>\n";

            int ContaAgencia = 1;
            foreach (Agencia ag in Agencias)
            {
                XML = XML + "               <Agencia" + ContaAgencia + ">\n";
                string rXML = ag.GerarXML();
                if (rXML != "")
                {
                    XML = XML + rXML;
                }
                XML = XML + "               </Agencia" + ContaAgencia + ">\n";
                ContaAgencia++;
            }
            XML = XML + "           </Agencias>\n";
            XML = XML + "       </Banco>\n";
            XML = XML + "    </XML>\n";
            return XML;
        }
    }
}