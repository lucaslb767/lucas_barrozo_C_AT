using System;
using System.Collections.Generic;
using System.Text;

namespace Pessoa.Biblioteca
{
    public interface InterfaceDados
    {
        IEnumerable<Pessoa> BuscarPessoas();

        void Deletar(int id);

        void Editar(Pessoa p);

        IEnumerable<Pessoa> BuscarPessoas( string nome);

        IEnumerable<Pessoa> BuscarPessoas(DateTime data);

        void DeletaECria(List<Pessoa> listaPessoas);

        void Salvar(Pessoa pessoa);

        bool PessoaExistente(Pessoa pessoa);

        Pessoa BuscarPessoaPorId(int id);

        string RecebeArquivo();

        void CriarPessoa(Pessoa pessoa);
    }
}
