    using System;
    using System.Collections.Generic;
    using System.Data;
    using AppBanco.Models;

    namespace AppBanco.Entities
    {
        public class Conta
        {
            public Enums.TipoConta Tipo { get; set; }
            public Correntista Correntista { get; set; }
            public string Numero { get; set; }
            public double Saldo { get; private set; }
            public List<Movimento> Movimentos { get; set; }

            public Conta()
            {
                Movimentos = new List<Movimento>();
            }

            public void AddMovimento(Movimento m)
            {
                Movimentos.Add(m);

                if (m.Tipo == Enums.TipoMovimento.Credito)
                {
                    Saldo = Saldo + m.Valor;
                }
                else
                {
                    Saldo = Saldo - m.Valor;
                }
            }

            public void RetornaMovimento(int index)
            {
                Movimentos.RemoveAt(index);
            }

            public override string ToString()
            {
                string retorno = "Numero...........:" + Numero + "\n" +
                                "Tipo de Conta ...:" + Tipo + "\n" +
                                "--------------------------------------" + "\n" +
                                "Correntista" + "\n" +
                                "--------------------------------------" + "\n" +
                                Correntista.ToString() + "\n" +
                                "--------------------------------------" + "\n" +
                                "Saldo da Conta ......:" + Saldo.ToString("F2") + "\n" +
                                "--------------------------------------";

                return retorno;
            }

            public void CarregarContaXML(string XMLConta)
            {
                string sTipo = RetornaTagXML(XMLConta, "Tipo");

                if (sTipo == "Corrente")
                {
                    Tipo = Enums.TipoConta.Corrente;
                }
                else
                {
                    Tipo = Enums.TipoConta.Poupanca;
                }

                Numero = RetornaTagXML(XMLConta, "Numero");

                string DadosCorrentista = RetornaTagXML(XMLConta, "Correntista");

                Correntista = new Correntista();
                Correntista.Nome = RetornaTagXML(DadosCorrentista, "Nome");
                Correntista.CPF = RetornaTagXML(DadosCorrentista, "CPF");
                Correntista.Endereco = RetornaTagXML(DadosCorrentista, "Endereco");
                string numeroEndereco = RetornaTagXML(DadosCorrentista, "NumeroEndereco");
                if (string.IsNullOrWhiteSpace(numeroEndereco))
                {
                    numeroEndereco = RetornaTagXML(DadosCorrentista, "Numero");
                }
                Correntista.Numero = int.TryParse(numeroEndereco, out int n) ? n : 0;
                Correntista.Estado = RetornaTagXML(DadosCorrentista, "Estado");
                Correntista.Cidade = RetornaTagXML(DadosCorrentista, "Cidade");
                Correntista.Telefone = RetornaTagXML(DadosCorrentista, "Telefone");

                string DadosMovimentos = RetornaTagXML(XMLConta, "Movimentos");

                int contaMovimento = 1;
                string XMLMovimento = RetornaTagXML(DadosMovimentos, "Movimento" + contaMovimento);

                while (XMLMovimento != "")
                {
                    // Criar um objeto de movimento
                    Movimento mov = new Movimento();

                    // Carregar o XML
                    mov.CarregarMovimentoXML(XMLMovimento);

                    // Adicionar a coleção
                    AddMovimento(mov);
                    contaMovimento++;
                    XMLMovimento = RetornaTagXML(DadosMovimentos, "Movimento" + contaMovimento);
                }
            }

            private string RetornaTagXML(string XML, string tag)
            {
                string retorno = "";

                if (!string.IsNullOrEmpty(XML))
                {
                    string tagInicio = "<" + tag + ">";
                    string tagFim = "</" + tag + ">";

                    int pi = XML.IndexOf(tagInicio);

                    if (pi >= 0)
                    {
                        pi += tagInicio.Length;
                        int pf = XML.IndexOf(tagFim, pi);

                        if (pf > pi)
                        {
                            retorno = XML.Substring(pi, pf - pi);
                        }
                    }
                }
                return retorno;
            }
            public string GerarXML()
            {
                string XML = "                     <Tipo>" + Tipo + "</Tipo>\n" +
                             "                     <Numero>" + Numero + "</Numero>\n" +
                             "                     <Saldo>" + Saldo.ToString("F2") + "</Saldo>\n" +
                             "                     <Correntista>\n" +
                             "                       <Nome>" + Correntista.Nome + "</Nome>\n" +
                             "                       <CPF>" + Correntista.CPF + "</CPF>\n" +
                             "                       <Endereco>" + Correntista.Endereco + "</Endereco>\n" +
                             "                       <NumeroEndereco>" + Correntista.Numero + "</NumeroEndereco>\n" +
                             "                       <Estado>" + Correntista.Estado + "</Estado>\n" +
                             "                       <Cidade>" + Correntista.Cidade + "</Cidade>\n" +
                             "                       <Telefone>" + Correntista.Telefone + "</Telefone>\n" +
                             "                     </Correntista>\n" +
                             "                   </Movimentox>\n";

                int contador = 1;
                foreach(Movimento mov in Movimentos)
                {
                    XML = XML + "                           <Movimento" + contador + ">\n";
                    XML = XML + mov.GerarXML();
                    XML = XML + "                           </Movimento" + contador + ">\n";
                    contador++;
                }
                XML = XML + "                       </Movimentos>\n";
                return XML;
            }
        }   
    }