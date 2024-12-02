using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatalhaNaval
{
    internal class JogadorHumano
    {
        private char[,] tabuleiro;
        private int pontuacao;
        private int numTiroDados;
        private Posicao[] posTiroDados;
        private string nickname;

        public JogadorHumano(int linhas, int colunas, string nome)
        {
            this.tabuleiro = new char[linhas, colunas];
            for (int l = 0; l < linhas; l++)
            {
                for (int c = 0; c < colunas; c++)
                {
                    tabuleiro[l, c] = 'A';  // 'A' representa água (posição vazia)
                }
            }
            this.pontuacao = 0;
            this.numTiroDados = 0;
            this.posTiroDados = new Posicao[100]; // Armazenar até 100 tiros dados
            this.nickname = GerarNickname(nome);
        }

        public int Pontuacao
        {
            get { return pontuacao; }
            set { pontuacao = value; }
        }
        public int NumTiroDados
        {
            get { return numTiroDados; }
            set { numTiroDados = value; }
        }
        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }
        public char[,] Tabuleiro
        {
            get { return tabuleiro; }
            set { tabuleiro = value; }
        }
        public Posicao[] PosTiroDados
        {
            get { return posTiroDados; }
            set { posTiroDados = value; }
        }

        public string GerarNickname(string nome)
        {
            string[] separador = nome.Split(' ');
            string iniciais = "";

            for (int i = 0; i < separador.Length - 1; i++)
            {
                iniciais += separador[i][0].ToString().ToUpper();
            }

            string ultnome = separador[separador.Length - 1];
            string nickname = ultnome + "." + iniciais;
            return nickname;
        }

        public Posicao EscolherAtaque()
        {
            Console.WriteLine("\nEscolha as coordenadas do tiro:");
            Console.WriteLine("\tLinha:");
            int linha = int.Parse(Console.ReadLine());
            Console.WriteLine("\tColuna:");
            int coluna = int.Parse(Console.ReadLine());
            Posicao posicaoTeste = new Posicao(linha, coluna);

            bool posicaoValida = false;
            while (!posicaoValida)
            {
                // Verifica se a posição está dentro dos limites do tabuleiro
                if (linha < 0 || linha >= tabuleiro.GetLength(0) || coluna < 0 || coluna >= tabuleiro.GetLength(1))
                {
                    Console.WriteLine("\nPosição fora dos limites da matriz!");
                    Console.WriteLine("Escolha novamente.");
                    linha = int.Parse(Console.ReadLine());
                    coluna = int.Parse(Console.ReadLine());
                    posicaoTeste = new Posicao(linha, coluna);
                }
                else
                {
                    // Verifica se a posição já foi atingida anteriormente
                    bool jaTirada = false;
                    foreach (var tiro in posTiroDados)
                    {
                        if (tiro != null && tiro.Equals(posicaoTeste))
                        {
                            jaTirada = true;
                            break;
                        }
                    }

                    if (jaTirada)
                    {
                        Console.WriteLine("\nPosição já informada anteriormente!");
                        Console.WriteLine("Escolha novamente.");
                        linha = int.Parse(Console.ReadLine());
                        coluna = int.Parse(Console.ReadLine());
                        posicaoTeste = new Posicao(linha, coluna);
                    }
                    else
                    {
                        posTiroDados[numTiroDados++] = posicaoTeste; // Armazena o tiro
                        posicaoValida = true;
                    }
                }
            }

            return posicaoTeste;
        }

        public bool ReceberAtaque(Posicao posicaoTiro)
        {
            if (tabuleiro[posicaoTiro.Linha, posicaoTiro.Coluna] != 'A')
            {
                tabuleiro[posicaoTiro.Linha, posicaoTiro.Coluna] = 'X'; // Marca como atingido
                return true;
            }
            else
            {
                tabuleiro[posicaoTiro.Linha, posicaoTiro.Coluna] = 'O'; // Marca como água
                return false;
            }
        }

        public void ImprimeTabuleiro()
        {
            for (int l = 0; l < tabuleiro.GetLength(0); l++)
            {
                for (int c = 0; c < tabuleiro.GetLength(1); c++)
                {
                    Console.Write($"{tabuleiro[l, c]} ");
                }
                Console.WriteLine();
            }
        }

        public void ImprimirTabuleiroAdversario()
        {
            for (int l = 0; l < tabuleiro.GetLength(0); l++)
            {
                for (int c = 0; c < tabuleiro.GetLength(1); c++)
                {
                    if (tabuleiro[l, c] == 'A') // Mostra apenas as células que ainda não foram atingidas
                        Console.Write("A ");
                    else
                        Console.Write($"{tabuleiro[l, c]} ");
                }
                Console.WriteLine();
            }
        }

        public bool AdicionarEmbarcacao(Embarcacao embarcacao, Posicao posicaoInicial, bool horizontal)
        {
            if (horizontal)
            {
                if (posicaoInicial.Coluna + embarcacao.Tamanho > tabuleiro.GetLength(1)) return false;

                for (int i = 0; i < embarcacao.Tamanho; i++)
                {
                    if (tabuleiro[posicaoInicial.Linha, posicaoInicial.Coluna + i] != 'A')
                        return false;
                }

                for (int i = 0; i < embarcacao.Tamanho; i++)
                {
                    tabuleiro[posicaoInicial.Linha, posicaoInicial.Coluna + i] = embarcacao.Nome[0];
                }
            }
            else
            {
                if (posicaoInicial.Linha + embarcacao.Tamanho > tabuleiro.GetLength(0)) return false;

                for (int i = 0; i < embarcacao.Tamanho; i++)
                {
                    if (tabuleiro[posicaoInicial.Linha + i, posicaoInicial.Coluna] != 'A')
                        return false;
                }

                for (int i = 0; i < embarcacao.Tamanho; i++)
                {
                    tabuleiro[posicaoInicial.Linha + i, posicaoInicial.Coluna] = embarcacao.Nome[0];
                }
            }

            return true;
        }
    }
}
