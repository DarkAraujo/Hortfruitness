using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM2PAraujo.Models
{
    public class Relatorio
    {
        // Propriedades
        private static int _nextId = 1;

        public int Id { get; set; }
        public string Atendente { get; set; }
        public string Documento { get; set; }
        public decimal Total { get; set; }
        public string DataHora { get; set; }

        public Relatorio()
        {
            Id = _nextId++;
        }

        public void ExibirInformacoes()
        {
            /*Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Atendente: {Atendente}");
            Console.WriteLine($"Documento: {Documento}");
            Console.WriteLine($"Total: {Total:C}");
            Console.WriteLine($"Data/Hora: {DataHora}");*/


            // Exibe o relatório
            

            if (Documento != "NULO") Documento = Program.FormatCPF(Documento);
            Console.WriteLine($" {Id,-5} {Atendente,-14} {Documento,-16} {Total,-10} {DataHora}");

        }

    }
}
