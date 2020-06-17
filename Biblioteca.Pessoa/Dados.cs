using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Biblioteca.Pessoa
{
    public class Dados : InterfaceDados
    {
        public IEnumerable<Pessoa> BuscarPessoas()
        {
            string nomeDoArquivo = RecebeArquivo();

            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(nomeDoArquivo);

            //busca pessoas individualmente
            string[] pessoas = resultado.Split(';');

            List<Pessoa> listaPessoasEncontradas = new List<Pessoa>();

            for (int i = 0; i < pessoas.Length -1; i++)
            {
                string[] dadosDaPessoa = pessoas[i].Split(',');

                int id = int.Parse(dadosDaPessoa[0]);
                string nome = dadosDaPessoa[1];
                string sobreNome = dadosDaPessoa[2];
                DateTime dataDeAniversario = Convert.ToDateTime(dadosDaPessoa[3]);

                //cria objeto pessoa com dados acima
                Pessoa pessoa = new Pessoa(id, nome, sobreNome, dataDeAniversario);

                listaPessoasEncontradas.Add(pessoa);
            }

            return listaPessoasEncontradas;
        }

        public string RecebeArquivo()
        {
            var caminho = Environment.SpecialFolder.Desktop;

            string arquivoDesktop = Environment.GetFolderPath(caminho);
            string nomeDoArquivo = @"\lucas_barrozo_repositorio.txt";

            return arquivoDesktop + nomeDoArquivo;
        }

        public void Deletar(int id)
        {
            var pessoas = BuscarPessoas();
            List<Pessoa> listaPessoas = new List<Pessoa>();
            foreach (var pessoa in pessoas)
            {
                if (id != pessoa.Id)
                {
                    listaPessoas.Add(pessoa);
                }
            }
            DeletaECria(listaPessoas);
        }

        public void DeletaECria(List<Pessoa> listaPessoas)
        {
            string nomeDoArquivo = RecebeArquivo();
            File.Delete(RecebeArquivo());
            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            foreach (var pessoa in listaPessoas)
            {
                Salvar(pessoa);
            }
        }

        public void Salvar(Pessoa pessoa)
        {
            CriarPessoa(pessoa);
        }

        public void CriarPessoa(Pessoa pessoa)
        {
            string arquivo = RecebeArquivo();

            string format = $"{pessoa.Id},{pessoa.nome},{pessoa.sobreNome},{pessoa.birth};";
        }

        public void Editar(Pessoa p)
        {
            var todasPessoas = BuscarPessoas();
            List<Pessoa> listaPessoas = new List<Pessoa>();
            foreach (var pessoa in todasPessoas)
            {
                if (p.Id == pessoa.Id)
                {
                    listaPessoas.Add(p);
                }
                else
                {
                    listaPessoas.Add(pessoa);
                }
            }
            DeletaECria(listaPessoas);
        }

        public IEnumerable<Pessoa> BuscarPessoas(string nome)
        {
            return (from x in BuscarPessoas()
                    where x.nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                    orderby x.nome
                    select x);
        }

        public IEnumerable<Pessoa> BuscarPessoas(DateTime data)
        {
            return (from x in BuscarPessoas()
                    where x.birth.Day == data.Day && x.birth.Month == data.Month
                    orderby x.birth
                    select x);
        }

        public bool PessoaExistente(Pessoa pessoa)
        {
            var id = pessoa.Id;

            var pessoasEncontradas = BuscarPessoaPorId(id);

            if (pessoasEncontradas != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Pessoa BuscarPessoaPorId(int id)
        {

            return (from x in BuscarPessoas()
                    where x.Id == id
                    select x).FirstOrDefault();
        }
    }
}
