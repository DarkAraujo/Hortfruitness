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
        public bool Ativo { get; set; }

        public Produto(int cod, string nome, decimal valorCompra, decimal valorVenda, float quantidade, bool ativo) {
            Cod = cod;
            Nome = nome;
            ValorCompra = valorCompra;
            ValorVenda = valorVenda;
            Quantidade = quantidade;
            Ativo = ativo;
        }

        public void ExibirDetalhes() {
            Console.WriteLine($"ID: {Cod}");
            Console.WriteLine($"Produto: {Nome}");
            Console.WriteLine($"Valor de Compra: {ValorCompra:C}");
            Console.WriteLine($"Valor de Venda: {ValorVenda:C}");
            Console.WriteLine($"Quantidade: {Quantidade}");
            Console.WriteLine($"Status: {(Ativo ? "Ativo" : "Inativo")}");


            // Exibe o relatório

            //Console.WriteLine($" {Cod,-5} {Nome,-14} {ValorCompra,-16} {ValorVenda,-10} {Quantidade}");

        }
    }
}
