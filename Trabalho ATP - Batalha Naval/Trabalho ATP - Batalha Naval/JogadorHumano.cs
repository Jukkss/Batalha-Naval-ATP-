using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_ATP___Batalha_Naval
{
    class JogadorHumano
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
                    tabuleiro[l, c] = 'A';
                }
            }
            this.pontuacao = 0;
            this.numTiroDados = 0;
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
            get { return PosTiroDados; }
            set { PosTiroDados = value; }
        }
        public string GerarNickname(string nome)
        {
            string[] separador = nome.Split(' ');
            string pnome = separador[0];
            string nickname = (pnome[0] + separador[separador.Length - 1]).ToLower();
            return nickname;
        }
        public Posicao EscolherAtaque()
        {
            Console.WriteLine("Escolha as coordenadas do tiro:");
            Console.WriteLine("Linha ?");
            int linha = int.Parse(Console.ReadLine());
            Console.WriteLine("Coluna ?");
            int coluna = int.Parse(Console.ReadLine());
            Posicao posicaoTeste = new Posicao(linha, coluna);
            bool posicaoMarcada = false;
            bool testePrincipal = false;

            while (testePrincipal == false)
            {
                bool testeDentroMatriz = false;
                bool testeNovaPosicao = false;
                int cont = 0;
                while (testeDentroMatriz == false)
                {
                    if (linha < 0 || linha > tabuleiro.GetLength(0) - 1 && coluna < 0 || coluna > tabuleiro.GetLength(1) - 1)
                    {
                        Console.WriteLine("\nPosição fora dos limites da matriz !!!");
                        Console.WriteLine("Refaça suas escolhas:");
                        Console.WriteLine("Escolha as coordenadas do tiro:");
                        Console.WriteLine("Linha ?");
                        linha = int.Parse(Console.ReadLine());
                        Console.WriteLine("Coluna ?");
                        coluna = int.Parse(Console.ReadLine());
                        posicaoTeste = new Posicao(linha, coluna);
                    }
                    else
                        testeDentroMatriz = true;
                }

                while (testeNovaPosicao == false)
                {

                    for (int i = 0; i < posTiroDados.Length && cont == 0; i++)
                    {
                        if (posTiroDados[i] == posicaoTeste)
                            cont++;
                    }
                }
                if (cont == 0)
                {
                    testeNovaPosicao = true;
                    testePrincipal = true;
                }
                else
                {
                    Console.WriteLine("\nPosição já informada anteriormente !!!");
                    Console.WriteLine("Refaça suas escolhas:");
                    Console.WriteLine("Escolha as coordenadas do tiro:");
                    Console.WriteLine("Linha ?");
                    linha = int.Parse(Console.ReadLine());
                    Console.WriteLine("Coluna ?");
                    coluna = int.Parse(Console.ReadLine());
                    posicaoTeste = new Posicao(linha, coluna);
                }

            }

            for (int i = 0; i < posTiroDados.Length && posicaoMarcada != true; i++)
            {
                if (posTiroDados[i] == null)
                {
                    posTiroDados[i] = posicaoTeste;
                    posicaoMarcada = true;
                }
            }
            return posicaoTeste;
        }
        public bool ReceberAtaque(Posicao posicaoTiro)
        {
            if (tabuleiro[posicaoTiro.Linha, posicaoTiro.Coluna] != 'A')
            {
                tabuleiro[posicaoTiro.Linha, posicaoTiro.Coluna] = 'X';
                return true;
            }
            else
            {
                tabuleiro[posicaoTiro.Linha, posicaoTiro.Coluna] = 'X';
                return false;
            }
        }
        public void ImprimeTabuleiro()
        {
            for (int l = 0; l < tabuleiro.GetLength(0); l++)
            {
                for (int c = 0; c < tabuleiro.GetLength(1); c++)
                {
                    Console.Write($"{tabuleiro[l, c]}\t");
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
                    if (tabuleiro[l, c] != 'A' && tabuleiro[l, c] != 'X')
                        Console.Write($"A\t");
                    else
                        Console.Write($"{tabuleiro[l, c]}\n");
                }
                Console.WriteLine();
            }
        }
        public bool AdicionarEmbarcacao(Embarcacao embarcacao, Posicao posicaoInicial)
        {
            bool testeSeNCabe = false;
            for (int i = posicaoInicial.Coluna, j = 0; i < tabuleiro.GetLength(0) && j <= embarcacao.Tamanho; i++, j++)
            {
                if (tabuleiro[posicaoInicial.Linha, i] != 'A')
                    testeSeNCabe = true;
            }
            if (testeSeNCabe)
                return false;
            else
            {
                for (int i = posicaoInicial.Coluna, j = 0; i < tabuleiro.GetLength(0) && j <= embarcacao.Tamanho; i++, j++)
                {
                    tabuleiro[posicaoInicial.Linha, i] = embarcacao.Nome[0];

                }
                return true;
            }
        }
    }
}
