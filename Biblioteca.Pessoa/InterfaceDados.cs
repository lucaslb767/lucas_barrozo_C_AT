using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Pessoa
{
    public interface InterfaceDados
    {
        IEnumerable<Pessoa> BuscarPessoas();
    }
}
