// Trabalho Prátco ATP - Desenvolvimento(Soares)
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BatalhaNaval
{
    public class Embarcacao
    {
        private string nome;
        private int tamanho;

        public Embarcacao(string nome, int tamanho)
        {
            this.nome = nome;
            this.tamanho = tamanho;
        }
        public char Sigla
        {
            get { return nome[0]; }
        }
        public int Tamanho
        {
            get { return tamanho; }
            set { tamanho = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
