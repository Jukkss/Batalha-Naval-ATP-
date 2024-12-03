// Trabalho Prátco ATP - Desenvolvimento(Soares) - Revisão(Fróis e Lucas)
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

            Console.Write("Digite seu nome completo: ");
            string nomeCompleto = Console.ReadLine();
            jogadorHumano = new JogadorHumano(10, 10, nomeCompleto);
            Console.WriteLine($"Seu nickname é: {jogadorHumano.Nickname}");


            jogadorComputador = new JogadorComputador(10, 10);

            Console.WriteLine("\nPosicione suas embarcações:");
            PosicionarEmbarcacoes(jogadorHumano);
            Console.WriteLine("\nComputador posicionando embarcações...");
            PosicionarEmbarcacoesComputador();

            Console.WriteLine("\nO jogo começou!");
            bool jogoAtivo = true;
            StreamWriter arqE = new StreamWriter("jogadaas.txt", false, Encoding.UTF8);
            while (jogoAtivo)
            {
                Console.WriteLine("\nSeu tabuleiro:");
                jogadorHumano.ImprimeTabuleiro();

                Console.WriteLine("\nTabuleiro do Computador:");
                jogadorComputador.ImprimirTabuleiroAdversario();

                Posicao ataqueHumano = jogadorHumano.EscolherAtaque();
                bool acertou = jogadorComputador.ReceberAtaque(ataqueHumano);

                if (acertou)
                {
                    Console.WriteLine("Você acertou uma embarcação!");
                    arqE.WriteLine($"[Tiro {jogadorHumano.NumTiroDados}] - Acerto na posição({ataqueHumano})");
                    jogadorHumano.Pontuacao++;
                }
                else
                {
                    Console.WriteLine("Você acertou na água!");
                    arqE.WriteLine($"[Tiro {jogadorHumano.NumTiroDados}] - Erro na posição({ataqueHumano})");
                }

                if (VerificarFimDeJogo())
                {
                    jogoAtivo = false;
                    break;
                }
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
            arqE.Close();
            ExibirResultado();
        }
        private void PosicionarEmbarcacoes(JogadorHumano jogador)
        {
            Embarcacao[] embarcacoes = new Embarcacao[]
            {
                new Embarcacao("Submarino", 1),
                new Embarcacao("Submarino", 1),
                new Embarcacao("Submarino", 1),
                new Embarcacao("Submarino", 1),
                new Embarcacao("Hidroavião", 2),
                new Embarcacao("Hidroavião", 2),
                new Embarcacao("Hidroavião", 2),
                new Embarcacao("Cruzador", 3),
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
            string[] linhas = File.ReadAllLines("frotaComputador.txt");

            foreach (string linha in linhas)
            {
                string[] partes = linha.Split(';');
                string nome = partes[0];
                int linhaInicial = int.Parse(partes[1]);
                int colunaInicial = int.Parse(partes[2]);
                string orientacao = partes[3].ToUpper();
                bool horizontal = orientacao == "H";

                int tamanho = 0;

                if (nome == "Submarino")
                {
                    tamanho = 1;
                }
                else if (nome == "Hidroavião")
                {
                    tamanho = 2;
                }
                else if (nome == "Cruzador")
                {
                    tamanho = 3;
                }
                else if (nome == "Encouraçado")
                {
                    tamanho = 4;
                }
                else if (nome == "Porta-aviões")
                {
                    tamanho = 5;
                }
                else
                {
                    Console.WriteLine($"Aviso: Embarcação desconhecida no arquivo: {nome}. Linha ignorada.");
                    continue;
                }

                var embarcacao = new Embarcacao(nome, tamanho);

                Posicao posicao = new Posicao(linhaInicial, colunaInicial);
                bool adicionada = jogadorComputador.AdicionarEmbarcacao(embarcacao, posicao, horizontal);

                if (!adicionada)
                {
                    Console.WriteLine($"Não foi possível posicionar a embarcação {nome} na posição ({linhaInicial}, {colunaInicial}).");
                }
            }
        }
        private bool VerificarFimDeJogo()
        {
            if (jogadorComputador.Pontuacao == 25)
            {
                Console.WriteLine("\nO computador venceu!");
                return true;
            }

            if (jogadorHumano.Pontuacao == 25)
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


