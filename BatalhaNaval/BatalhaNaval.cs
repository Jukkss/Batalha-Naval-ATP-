using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BatalhaNaval
{
    public class BatalhaNaval
    {
        private JogadorHumano jogadorHumano;
        private JogadorComputador jogadorComputador;

        public void IniciarJogo()
        {
            Console.WriteLine("Bem-vindo ao jogo de Batalha Naval!");

            // Configurar jogador humano
            Console.Write("Digite seu nome completo: ");
            string nomeCompleto = Console.ReadLine();
            jogadorHumano = new JogadorHumano(10, 10, nomeCompleto);
            Console.WriteLine($"Seu nickname é: {jogadorHumano.Nickname}");

            // Configurar jogador computador
            jogadorComputador = new JogadorComputador(10, 10);

            // Posicionar embarcações
            Console.WriteLine("\nPosicione suas embarcações:");
            PosicionarEmbarcacoes(jogadorHumano);
            Console.WriteLine("\nComputador posicionando embarcações...");
            PosicionarEmbarcacoesComputador();

            // Loop principal do jogo
            Console.WriteLine("\nO jogo começou!");
            bool jogoAtivo = true;

            while (jogoAtivo)
            {
                // Turno do jogador humano
                Console.WriteLine("\nSeu tabuleiro:");
                jogadorHumano.ImprimeTabuleiro();

                Console.WriteLine("\nTabuleiro do Computador:");
                jogadorComputador.ImprimirTabuleiroAdversario();

                Posicao ataqueHumano = jogadorHumano.EscolherAtaque();
                bool acertou = jogadorComputador.ReceberAtaque(ataqueHumano);

                if (acertou)
                {
                    Console.WriteLine("Você acertou uma embarcação!");
                    jogadorHumano.Pontuacao++;
                }
                else
                {
                    Console.WriteLine("Você acertou na água!");
                }

                if (VerificarFimDeJogo())
                {
                    jogoAtivo = false;
                    break;
                }

                // Turno do computador
                Console.WriteLine("\nTurno do computador...");
                Posicao ataqueComputador = jogadorComputador.EscolherAtaque();
                bool computadorAcertou = jogadorHumano.ReceberAtaque(ataqueComputador);

                if (computadorAcertou)
                {
                    Console.WriteLine("O computador acertou uma embarcação sua!");
                    jogadorComputador.Pontuacao++;
                }
                else
                {
                    Console.WriteLine("O computador acertou na água!");
                }

                if (VerificarFimDeJogo())
                {
                    jogoAtivo = false;
                }
            }

            // Fim do jogo
            ExibirResultado();
        }

        private void PosicionarEmbarcacoes(JogadorHumano jogador)
        {
            var embarcacoes = new List<Embarcacao>
            {
                new Embarcacao("Submarino", 1),
                new Embarcacao("Hidroavião", 2),
                new Embarcacao("Cruzador", 3),
                new Embarcacao("Encouraçado", 4),
                new Embarcacao("Porta-aviões", 5)
            };

            foreach (var embarcacao in embarcacoes)
            {
                bool adicionada = false;
                while (!adicionada)
                {
                    Console.WriteLine($"Posicione a embarcação {embarcacao.Nome} (tamanho {embarcacao.Tamanho}):");
                    Console.Write("Digite a linha (0-9): ");
                    int linha = int.Parse(Console.ReadLine());
                    Console.Write("Digite a coluna (0-9): ");
                    int coluna = int.Parse(Console.ReadLine());

                    Console.WriteLine("Escolha a orientação (H para horizontal ou V para vertical): ");
                    string orientacao = Console.ReadLine().ToUpper();
                    bool horizontal = orientacao == "H";

                    Posicao posicao = new Posicao(linha, coluna);
                    adicionada = jogador.AdicionarEmbarcacao(embarcacao, posicao, horizontal);

                    if (!adicionada)
                    {
                        Console.WriteLine("Posição inválida! Tente novamente.");
                    }
                }
            }
        }

        private void PosicionarEmbarcacoesComputador()
        {
            var embarcacoes = new List<Embarcacao>
            {
                new Embarcacao("Submarino", 1),
                new Embarcacao("Hidroavião", 2),
                new Embarcacao("Cruzador", 3),
                new Embarcacao("Encouraçado", 4),
                new Embarcacao("Porta-aviões", 5)
            };

            Random random = new Random();
            foreach (var embarcacao in embarcacoes)
            {
                bool posicionada = false;
                while (!posicionada)
                {
                    // Gerar uma posição aleatória para o computador
                    int linha = random.Next(0, 10); // Random entre 0 e 9
                    int coluna = random.Next(0, 10); // Random entre 0 e 9
                    bool horizontal = random.Next(0, 2) == 0; // Random entre 0 e 1 para definir horizontal ou vertical

                    Posicao posicao = new Posicao(linha, coluna);
                    posicionada = jogadorComputador.AdicionarEmbarcacao(embarcacao, posicao, horizontal);
                }
            }
        }

        private bool VerificarFimDeJogo()
        {
            if (jogadorComputador.Pontuacao == 15)
            {
                Console.WriteLine("\nO computador venceu!");
                return true;
            }

            if (jogadorHumano.Pontuacao == 15)
            {
                Console.WriteLine($"\nParabéns, {jogadorHumano.Nickname}! Você venceu!");
                return true;
            }

            return false;
        }

        private void ExibirResultado()
        {
            Console.WriteLine("\nResultado Final:");
            Console.WriteLine($"{jogadorHumano.Nickname}: {jogadorHumano.Pontuacao} pontos");
            Console.WriteLine($"Computador: {jogadorComputador.Pontuacao} pontos");
        }
    }
}


