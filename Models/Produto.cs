using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM2PAraujo.Models {

    public class Produto {
        private static int _nextId = 1;
        public int Cod { get; set; }
        public string Nome { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public float Quantidade { get; set; }

        public Produto(int cod, string nome, decimal valorCompra, decimal valorVenda, float quantidade) {
            Cod = cod;
            Nome = nome;
            ValorCompra = valorCompra;
            ValorVenda = valorVenda;
            Quantidade = quantidade;
        }
    }
}
