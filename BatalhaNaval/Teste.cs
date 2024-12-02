using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatalhaNaval
{
    internal class Teste
    {
        static void Main(string[] args)
        {
            try
            {
                BatalhaNaval jogo = new BatalhaNaval();
                jogo.IniciarJogo();

                Console.WriteLine("\nObrigado por jogar Batalha Naval! Até a próxima.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nOcorreu um erro durante a execução do jogo: {ex.Message}");
                Console.WriteLine("Por favor, tente novamente mais tarde.");
            }
        }
    }
}
