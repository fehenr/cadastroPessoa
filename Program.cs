using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
namespace ER2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PessoaFisica> ListaPf = new List<PessoaFisica>();
            List<PessoaJuridica> ListaPj = new List<PessoaJuridica>();


            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(@$"
=====================================
|  Bem vindo ao sistema de cadastro |
=====================================
");

            BarraCarregamento("Inicializando");

            string opcao;

            do
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(@$"
====================================
|   Escolha uma das opção abaixo   |
------------------------------------
|           Pessoa Fisica          |
|   1 - Cadastrar Pessoa Fisica    |
|   2 - Listar Pessoa Fisica       |
|   3 - Remover Pessoa Fisica      |
|                                  |
|          Pessoa Juridica         |
|   4 - Cadastrar Pessoa Juridica  |
|   5 - Listar Pessoa Juridica     |
|   6 - Remover Pessoa Juridica    |
|                                  |
|            0 - Sair              |
====================================
");
                Console.ResetColor();

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        PessoaFisica pf = new PessoaFisica();

                        PessoaFisica Novapf = new PessoaFisica();

                        Endereco endPf = new Endereco();

                        Console.WriteLine($"Digite seu endereço");
                        endPf.Endereco = Console.ReadLine();

                        Console.WriteLine($"Digite o número");
                        endPf.numero = int.Parse(Console.ReadLine());
                    
                        Console.WriteLine($"Digite seu complemento(ENTER caso não tenha)");
                        endPf.complemento = Console.ReadLine();
                        
                        Console.WriteLine($"Seu endereço é comercial? S/N");
                        string endComercial = Console.ReadLine().ToUpper();
                        
                        if (endComercial == "S")
                        {
                            endPf.enderecoComercial = true;
                        }else
                        {
                            endPf.enderecoComercial = false;
                        }

                        Novapf.Endereco = endPf;

                        Console.WriteLine($"Digite seu CPF (Apenas números)");
                        Novapf.cpf = Console.ReadLine();
                        
                        Console.WriteLine($"Digite seu nome");
                        Novapf.nome = Console.ReadLine();
                        
                        Console.WriteLine($"Digite seu rendimento mensal (Apenas números)");
                        Novapf.rendimento = float.Parse(Console.ReadLine());
                        
                        Console.WriteLine($"digite sua data de nascimento (AAAA-MM--DD)");
                        Novapf.DataNascimento = DateTime.Parse(Console.ReadLine());
                        

                        Console.WriteLine($@"Rua: {Novapf.Endereco.endereco2}, Numero: {Novapf.Endereco.numero}");

                        bool idadeValida = pf.ValidarDataNascimento(Novapf.DataNascimento);

                        if (idadeValida == true)
                        {
                            Console.WriteLine($"Cadastro Aprovado");
                            ListaPf.Add(Novapf);
                            Console.WriteLine(pf.PagarImposto(Novapf.rendimento));  
                        }
                        else
                        {
                            Console.WriteLine($"Não é possível cadastrar-se. Idade inferior a 18 anos.");
                        }

                        using (StreamWriter sw = new StreamWriter($"{Novapf.nome}.txt"))
                        {
                            sw.Write(Novapf.nome);
                        }


                        using (StreamReader sr = new StreamReader($"{Novapf.nome}.txt"))
                        {
                            string linha;
                             while ((linha = sr.ReadLine()) != null)
                             {
                                 
                                 Console.WriteLine($"{linha}");
                                 
                             }
                        }

                        break;

                    case "2":
                            foreach (var cadaItem in ListaPf)
                            {
                                Console.WriteLine($"{cadaItem.nome}, {cadaItem.cpf}, {cadaItem.Endereco.endereco2}");
                            }
                        break;

                    case "3":
                            Console.WriteLine($"Digite o CPF que deseja Remover");
                            string cpfProcurado = Console.ReadLine();

                           PessoaFisica pessoaEncontrada = ListaPf.Find(cadaItem => cadaItem.cpf == cpfProcurado);
                            
                            if (pessoaEncontrada != null)
                            {
                                ListaPf.Remove(pessoaEncontrada);
                                Console.WriteLine($"Cadastro removido");
                            }else
                            {
                                Console.WriteLine($"CPF não encontrado");
                            }

                        break;

                    case "4":

                        PessoaJuridica pj = new PessoaJuridica();

                        PessoaJuridica novaPj = new PessoaJuridica();

                        Endereco endPj = new Endereco();

                        Console.WriteLine($"Digite seu endereço");
                        endPj.Endereco = Console.ReadLine();

                        Console.WriteLine($"Digite o numero");
                        endPj.numero = int.Parse(Console.ReadLine());
                    
                        Console.WriteLine($"Digite seu complemento(ENTER caso não tenha)");
                        endPj.complemento = Console.ReadLine();
                        
                        Console.WriteLine($"Seu endereço é comercial? S/N");
                        string endComercialPj = Console.ReadLine().ToUpper();

                        if (endComercialPj == "S")
                        {
                            endPj.enderecoComercial = true;
                        }else
                        {
                            endPj.enderecoComercial = false;
                        }

                        novaPj.Endereco = endPj;

                        Console.WriteLine($"Digite seu CNPJ (Apenas números)");
                        novaPj.cnpj = Console.ReadLine();
                        
                        Console.WriteLine($"Digite sua razão social");
                        novaPj.RazaoSocial = Console.ReadLine();

                        Console.WriteLine($"Digite o nome da empresa");
                        novaPj.Nome = Console.ReadLine();
                        
                        Console.WriteLine($"Digite seu rendimento mensal (Apenas números)");
                        novaPj.rendimento = float.Parse(Console.ReadLine());

                            foreach (var item in pj.Ler())
                            {
                                Console.WriteLine($"Nome: {item.nome} - Razão Social:{item.RazaoSocial} - CNPJ: {item.cnpj}");
                            }


          
                        break;
                    
                    case "5":
                            foreach (var cadaItem in ListaPj)
                            {
                                Console.WriteLine($"{cadaItem.RazaoSocial}, {cadaItem.cnpj}, {cadaItem.Endereco.endereco2}");
                            }
                            

                        break;

                    case "6":
                            Console.WriteLine($"Digite o CNPJ que deseja Remover");
                            string cnpjProcurado = Console.ReadLine();

                           PessoaJuridica EmpresaEncontrada = ListaPj.Find(cadaItem => cadaItem.cnpj == cnpjProcurado);
                            
                            if (EmpresaEncontrada != null)
                            {
                                ListaPj.Remove(EmpresaEncontrada);
                                Console.WriteLine($"Cadastro removido");
                            }else
                            {
                                Console.WriteLine($"Cpf não encontrado");
                            }

                        break;



                    case "0":
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.DarkBlue;

                        Console.WriteLine($"Obrigado por utilizar nosso sistema!");

                        BarraCarregamento("Finalizando");

                        break;


                    default:
                        Console.WriteLine("Opção Inválida, digite uma opção válida");
                        break;
                }
            } while (opcao != "0");
        }

        static void BarraCarregamento(string textoCarregamento)
        {

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Grey;
            Console.Write(textoCarregamento);
            Thread.Sleep(500);



            for (var contador = 0; contador < 10; contador++)
            {

                System.Console.Write(".");
                Thread.Sleep(500);

            }
            Console.ResetColor();

        }

    }

}