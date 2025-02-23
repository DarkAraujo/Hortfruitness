﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Linq;
using PIM2PAraujo.Models;

class usersDB {
    private static int _nextId = 1;

    public int Id { get; set; } // Identificador único
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public usersDB(string name, string username, string password, string email) {
        Id = _nextId++;
        Name = name;
        Username = username;
        Password = password;
        Email = email;
    }
}

class Program {

    static List<usersDB> usersDB = [
        new("Dark Araujo", "dk", "dk", "dk@example.com")
    ];

    static List<Atendente> atendentes = [

        new Atendente("João", "12345678901"),
        new Atendente("Maria", "98765432109")
    ];

    List<Usuario> usuarios = [
        new Usuario("Carlos", "12345678901", "carlos@example.com"),
        new Usuario("Ana", "98765432109", "ana@example.com")
    ];

    public static List<Produto> produtos = [];

    public static List<Relatorio> relatorios = [
        new Relatorio{Atendente = atendentes[0], Documento = "12345678901", Total = 100.50m, DataHora = "01/01/2023 10:30" },
        new Relatorio{Atendente = atendentes[1], Documento = "98765432109", Total = 200.75m, DataHora = "01/01/2023 11:15" },
        new Relatorio{Atendente = atendentes[0], Documento = "NULO", Total = 150.25m, DataHora = "01/01/2023 12:45" }
    ];


    // Variável estática para armazenar o usuário logado
    private static usersDB _usuarioLogado;

    // Propriedade pública para acessar o usuário logado de forma controlada
    public static usersDB UsuarioLogado {
        get {
            if (_usuarioLogado == null) {
                throw new InvalidOperationException("Nenhum usuário está logado.");
            }
            return _usuarioLogado;
        }
        private set {
            _usuarioLogado = value;
        }
    }

    static void Main(string[] args) {


        Logo();
        LoginMenu();
    }

    static void Logo() {
        Console.Clear();

        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine("              __  __           __  _    ____           _ __                                      ");
        Console.WriteLine("             / / / /___  _____/ /_(_/  / _________  __(_/ /_ ____  _______________				");
        Console.WriteLine("            / /_/ / __ \\/ ___/ __/ /  / /_/ ___/ / / / / __// __ \\/ _ \\/ ___/ ___/			");
        Console.WriteLine("           / __  / /_/ / /  / /_/ /  / __/ /  / /_/ / / /_ / / / /  __(__  (__  )  				");
        Console.WriteLine("          /_/ /_/\\____/_/   \\__/_/  /_/ /_/   \\__,_/_/\\__//_/ /_/\\___/____/____/   		    \n");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
    }

    static void LoginMenu() {
        Console.Clear();
        Logo();

        Console.WriteLine("Digite seu user: ");
        var username = Console.ReadLine();
        if (username == null) {
            Console.WriteLine("Digite um user válido");
            Console.ReadKey();
            LoginMenu();
        }

        Console.WriteLine("Digite sua senha");
        string password = Console.ReadLine();
        if (password == null) {
            Console.WriteLine("Digite uma senha válida");
            Console.ReadKey();
            LoginMenu();
        }

        var user = usersDB.Find(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                      p.Password.Equals(password, StringComparison.OrdinalIgnoreCase));

        if (user != null) {
            UsuarioLogado = user;
            Console.WriteLine($"Login realizado com sucesso! Bem vindo(a), {username}!");
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();

            Admin();

        } else {
            Console.WriteLine("Usuário não encontrado!");
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
            LoginMenu();

        }

    }

    static void Admin() {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Clear();

        Logo();
        Console.WriteLine($"\t\tSeja Bem Vindo, {UsuarioLogado.Name}");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine("1 | VENDAS");
        Console.WriteLine("2 | ESTOQUE");
        Console.WriteLine("3 | CLIENTE");
        Console.WriteLine("4 | FORNECEDORES");
        Console.WriteLine("5 | USUARIOS");
        Console.WriteLine("6 | INFORMACOES DO SISTEMA");
        Console.WriteLine("7 | SAIR");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine("Qual opcao voce deseja acessar? ");

        int option;

        if (!int.TryParse(Console.ReadLine(), out option)) {
            Console.WriteLine("Digite uma opção válida!");
            Console.WriteLine("Pressione qualquer tecla para continuar..");
            Admin();
        }


        switch (option) {
            case 1:
                Vendas();
                Admin();
                break;

            case 2:
                EstoqueMenu();
                Admin();
                break;


            case 7:
                Console.Clear();
                Logout();
                break;

            default:
                Admin();
                break;
        }
    }

    // Método para logout
    public static void Logout() {
        if (_usuarioLogado != null) {
            // Limpa os dados sensíveis
            _usuarioLogado.Password = null;
            _usuarioLogado = null;
            Console.WriteLine("Logout realizado com sucesso.");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        } else {
            Console.WriteLine("Nenhum usuário está logado.");
        }
    }

    public static void Vendas() {
        Console.Clear();
        Logo();
        // Consolida relatórios com o mesmo ID
        var consolidado = relatorios
            .GroupBy(r => r.Id)
            .Select(g => new Relatorio {
                Id = g.Key,
                Atendente = g.First().Atendente,
                Documento = g.First().Documento,
                Total = g.Sum(r => r.Total),
                DataHora = g.First().DataHora
            }).ToList();

        Console.WriteLine("Relatório de Vendas:");
        Console.WriteLine(" ID  | ATENDENTE    | CPF/Cliente   | TOTAL   | DATA/HORA");

        foreach (var relatorio in consolidado) {
            relatorio.ExibirInformacoes();
        }


        // Calcula o total geral
        decimal totalGeral = consolidado.Sum(r => r.Total);

        Console.WriteLine("_____________________________________________________________________");
        Console.WriteLine($"Total Geral: {totalGeral:C}\n");
        Console.WriteLine("Pressione qualquer tecla para retornar ao menu de vendas...");
        Console.ReadKey();
    }

    public static void EstoqueMenu() {
        Console.Clear();
        Logo();

        ListarProduto();

        int option;
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine("1 | CADASTRAR NOVO PRODUTO");
        Console.WriteLine("2 | ATUALIZAR PRODUTO");
        Console.WriteLine("3 | REMOVER PRODUTO");
        Console.WriteLine("3 | RETORNAR AO MENU PRINCIPAL");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");
        Console.WriteLine("Qual opcao voce deseja acessar? ");

        if (!int.TryParse(Console.ReadLine(), out option)) {
            Console.WriteLine("Digite uma opção válida!");
            Console.WriteLine("Pressione qualquer tecla para continuar..");
            EstoqueMenu();
        }

        do {
            switch (option) {
                case 1:
                    CadastrarProduto();
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    Admin();
                    break;

                default:
                    EstoqueMenu();
                    break;
            }
        }while (option != 0);


    }

    public static void CadastrarProduto() {
        Console.Clear();
        Logo();

        Console.WriteLine("Cadastrar Produto:");
        
        Console.WriteLine("Digite o código do produto: ");
        int codProduto = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite o nome do produto: ");
        var nomeProduto = Console.ReadLine();

        Console.WriteLine("Digite o valor gasto por unidade do produto: ");
        decimal valorGastoProduto = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Digite o valor de venda por unidade do produto: ");
        decimal valorVendaProduto = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Digite a quantidade do produto: ");
        float quantidadeProduto = float.Parse(Console.ReadLine());

        Console.WriteLine("As informacoes acima estao corretas?");
        Console.WriteLine("S | SIM  /  N | NAO");
        ConsoleKeyInfo yesOrNo = Console.ReadKey();
        Console.WriteLine();

        while(true){
            switch (yesOrNo.Key) {
                case ConsoleKey.S:
                    Produto newProduct = new Produto(codProduto, nomeProduto, valorGastoProduto, valorVendaProduto, quantidadeProduto);
                    produtos.Add(newProduct);

                    Console.WriteLine("Sucesso! Produto registrado no sistema. Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    EstoqueMenu();
                    break;

                case ConsoleKey.N:
                    Console.WriteLine("Operacao cancelada! Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    break;
                default:
                    break;

            }
        }


    }

    public static void ListarProduto() {
        Console.Clear();
        Logo();

        Console.WriteLine("Lista de Produtos:");
        if (produtos.Count == 0) {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        foreach (var produto in produtos) {
            Console.WriteLine($"🔹 {produto.Cod} - {produto.Nome} | Compra: {produto.ValorCompra:C} | Venda: {produto.ValorVenda:C} | Quantidade: {produto.Quantidade}");
        }


    }

        public static string FormatCPF(string cpf) => cpf.Length == 11 ? $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}" : cpf;



}