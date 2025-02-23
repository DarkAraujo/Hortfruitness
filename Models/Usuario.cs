using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM2PAraujo.Models {

    public class Usuario {
        private static int _nextId = 1;
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; } // CPF como identificador único
        public string Email { get; set; } // Outras informações podem ser adicionadas

        public Usuario(string nome, string cpf, string email) {
            Id = _nextId++;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }
    }
}
