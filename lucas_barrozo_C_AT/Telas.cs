using P.Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lucas_barrozo_C_AT
{
    
    public static class Telas
    {
        public static Dados Dados = new Dados();
        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Quem faz aniversário hoje:\n");
            
            AniversarianteDoDia();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Escolha uma das opcoes abaixo: ");
            Console.WriteLine("1 - Pesquisar Pessoa ");
            Console.WriteLine("2 - Adicionar Pessoas ");
            Console.WriteLine("3 - Editar Pessoa ");
            Console.WriteLine("4 - Deletar ");
            Console.WriteLine("5 - Sair ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    BuscaPessoa();
                    break;
                case 2:
                    CadastrarPessoa();
                    break;
                case 3:
                    //EditarPessoa();
                    break;
                case 4:
                    //Deletar();
                    break;
                case 5:
                    Console.WriteLine("Saindo do programa");
                    break;
                default:
                    Console.WriteLine("escolha uma das opcoes");
                    break;
            }
        }

       private static void AniversarianteDoDia()
        {
            DateTime hoje = DateTime.Today;
            var aniversarioHoje = Dados.BuscarPessoas(hoje);
            if (aniversarioHoje.Count() == 0)
            {
                Console.WriteLine("Nenhum aniversario hoje.");
            } else
            {
                foreach (var pessoa in aniversarioHoje)
                {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobreNome);
                }
            }
        }

        private static void BuscaPessoa()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome da pessoa que deseja buscar:");
            string[] nomeESobrenome = Console.ReadLine().Split(' ');
            string nome = nomeESobrenome[0];

            var pessoasEncontradas = Dados.BuscarPessoas(nome);

            if (pessoasEncontradas.Count() == 0)
            {
                Console.WriteLine("Nenhum usuario Encontrado");
            } else
            {
                Console.WriteLine("\nUsuarios encontrados: \n");

                foreach (var pessoa in pessoasEncontradas)
                {
                    Console.WriteLine(pessoa.Id + " - " + pessoa.nome + " " + pessoa.sobreNome);
                }
                Console.WriteLine("\n Digite o número da pessoa que deseja ter mais detalhes: ");
                int escolha = int.Parse(Console.ReadLine());

                foreach( var pessoa in pessoasEncontradas)
                {
                    if (pessoa.Id == escolha)
                    {
                        Console.WriteLine(Dados.BuscarPessoaPorId(escolha));
                    }
                }
            }

            VoltarProMenu();
        }

        public static void CadastrarPessoa()
        {
            Console.Clear();

            Console.WriteLine("Entre com o nome: ");
            String nome = Console.ReadLine();
            Console.WriteLine("Entre com o sobrenome:");
            String sobrenome = Console.ReadLine();

            DateTime aniversario = RecebeETransformaData();

            var pessoas = Dados.BuscarPessoas();

            Pessoa p = new Pessoa(nome, sobrenome, aniversario);

            foreach (var pessoa in pessoas)
            {
                Pessoa ultimo = pessoas.Last(x => x.Id == pessoa.Id);
                p.Id = ultimo.Id + 1;
            }

            Console.WriteLine(p);

            Console.WriteLine("\n Dados corretos (s/n)?");
            string opcao = Console.ReadLine();
            if (opcao == "s")
            {
                Console.WriteLine("Pessoa, sendo salva...");
                Dados.Salvar(p);
            } else
            {
                CadastrarPessoa();
            }
            VoltarProMenu();
        }

        public static DateTime RecebeETransformaData()
        {
            Console.WriteLine("Entre com a data de nascimento em dd/mm/yyyy: ");
            string data = Console.ReadLine();

            DateTime dataTransforma = new DateTime();

            if(data.Contains("/"))
            {
                string[] dataDividida = data.Split('/');
                int ano = int.Parse(dataDividida[2]);
                int mes = int.Parse(dataDividida[1]);
                int dia = int.Parse(dataDividida[0]);

                dataTransforma = new DateTime(ano, mes, dia);
            } else
            {
                Console.WriteLine("Entre com uma data valida");
                VoltarProMenu();

            }
            return dataTransforma;
        }

        public static void VoltarProMenu()
        {
            Console.WriteLine("Aperte qualquer botao para voltar");
            Console.ReadLine();
            Menu();
        }
    }
}
