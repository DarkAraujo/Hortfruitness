﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM2PAraujo.Models {
    public class Atendente {
        private static int _nextId = 1;
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; } // Outras informações podem ser adicionadas

        public Atendente(string nome, string cpf) {
            Id = _nextId++;
            Nome = nome;
            Cpf = cpf;
        }
    }

}
