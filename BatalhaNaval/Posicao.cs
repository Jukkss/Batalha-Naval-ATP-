using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BatalhaNaval
{
    public class Posicao
    {
        private int linha;
        private int coluna;

        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }


        public int Linha
        {
            get { return linha; }
            set { linha = value; }
        }
        public int Coluna
        {
            get { return coluna; }
            set { coluna = value; }
        }

        public override string ToString()
        {
            return $"Linhaa:{linha} - Coluna:{coluna}";
        }

    }
}
