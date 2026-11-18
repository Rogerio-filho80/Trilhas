using AppBanco.Entities;

namespace AppBanco;

class Program
{
    static void Main(string[] args)
    {
        Tela.MenuPrincipal();

        while (Tela.Menu != Tela.MenuSair)
        {
            if (Tela.Menu == Tela.MenuCriarBanco)
            {
                try { Console.Clear(); } catch { }
                Console.WriteLine("Criar banco");
                try { Console.ReadLine(); } catch { }
            }
            else
            {
                try { Console.Clear(); } catch { }
                string caminhoArquivo = Tela.ArquivosXML[Tela.Menu - 1].FullName;

                Banco banco = new Banco();
                string retorno = banco.CarregarBanco(caminhoArquivo);

                if (retorno == "")
                {
                    try { Console.Clear(); } catch { }
                    Console.WriteLine(banco.ToString());
                    try { Console.ReadLine(); } catch { }
                    
                }
                else
                {
                    try { Console.Clear(); } catch { }
                    Console.WriteLine("Erro ao carregar arquivo. Descrição do erro:");
                    Console.WriteLine(retorno);
                }

                try { Console.ReadLine(); } catch { }
            }

            Tela.MenuPrincipal();
        }
    }
}