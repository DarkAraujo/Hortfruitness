using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM2PAraujo.Models {
    public class Atendente {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; } // Outras informações podem ser adicionadas

        public Atendente(int id, string nome, string cpf) {
            Id = id;
            Nome = nome;
            Cpf = cpf;
        }
    }

}
