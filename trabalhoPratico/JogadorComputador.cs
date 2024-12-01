using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhoPratico
{
    internal class JogadorComputador
    {
        private char[,] tabuleiro;
        private int pontuacao, numTirosDados;
        private Posicao[] posTirosDados;
        private Random r = new Random();

        public JogadorComputador(int linhas, int colunas)
        {
            this.tabuleiro = new char[linhas, colunas];
            this.pontuacao = 0;
            this.numTirosDados = 0;
            this.posTirosDados = new Posicao[100];

            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    tabuleiro[i, j] = 'A';
                }
            }
        }
        public char[,] Tabuleiro
        {
            get { return tabuleiro; }
            set { tabuleiro = value; }
        }
        public int Pontuacao
        {
            get { return pontuacao; }
            set { pontuacao = value; }
        }
        public int NumTirosDados
        {
            get { return numTirosDados; }
            set { numTirosDados = value; }
        }
        public Posicao[] PosTirosDados
        {
            get { return posTirosDados; }
            set { posTirosDados = value; }
        }
        public bool AdicionarEmbarcacao(Embarcacao embarcacaoNova, Posicao posicaoInicial)
        {
            int posicaoFinal = posicaoInicial.Coluna + (embarcacaoNova.Tamanho - 1);
            for (int i = posicaoInicial.Coluna; i <= posicaoFinal; i++)
                if (Tabuleiro[posicaoInicial.Linha, i] != 'A')
                    return false;
            if (posicaoFinal > 9)
                return false;
            else
            {
                for (int i = posicaoInicial.Coluna; i <= posicaoFinal; i++)
                {
                    Tabuleiro[posicaoInicial.Linha, i] = embarcacaoNova.Nome[0].ToUpper();
                }
                return true;
            }
        }
        public Posicao EscolherAtaque()
        {
            Posicao posicaoNova;
            bool validacao = false;

            while (!validacao)
            {
                int linha = r.Next(10);
                int coluna = r.Next(10);
                posicaoNova = new Posicao(linha, coluna);

                int cont = 0;
                foreach (Posicao p in PosTirosDados)
                {
                    if (p != null && p.Linha == linha && p.Coluna == coluna) cont++;
                }
                if (cont == 0)
                {
                    PosTirosDados[NumTirosDados] = posicaoNova;
                    NumTirosDados++;
                    validacao = true;
                }
            }
            return posicaoNova;
        }
        public bool ReceberAtaque(Posicao posTiroDado)
        {
            if (tabuleiro[posTiroDado.Linha, posTiroDado.Coluna] != 'A' && tabuleiro[posTiroDado.Linha, posTiroDado.Coluna] != 'X')
            {
                tabuleiro[posTiroDado.Linha, posTiroDado.Coluna] = 'T';
                return true;
            }
            else
            {
                tabuleiro[posTiroDado.Linha, posTiroDado.Coluna] = 'X';
                return false;
            }
        }
        public void ImprimirTabuleiroJogador()
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    Console.Write($"{tabuleiro[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        public void ImprimirTabuleiroAdversario()
        {
            for (int i = 0; i < tabuleiro.GetLength(0); i++)
            {
                for (int j = 0; j < tabuleiro.GetLength(1); j++)
                {
                    if (tabuleiro[i, j] == 'A' || tabuleiro[i, j] == 'T' || tabuleiro[i, j] == 'X')
                        Console.Write($"{tabuleiro[i, j]} ");
                    else
                        Console.Write("A ");
                }
                Console.WriteLine();
            }
        }
    }
}
