using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pessoa.Biblioteca
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string nome { get; set; }
        public string sobreNome { get; set; }
        public DateTime birth { get; set; }

        public Pessoa()
        {
        }

        public Pessoa(int id, String Nome, String Sobrenome, DateTime aniversario)
        {
            Id = id;
            nome = Nome;
            sobreNome = Sobrenome;
            birth = aniversario;
        }

        public Pessoa(String Nome, String Sobrenome, DateTime data)
        {
            nome = Nome;
            sobreNome = Sobrenome;
            birth = data;
        }

        public int diasAteAniversario()
        {
            DateTime today = DateTime.Today;
            DateTime niver = new DateTime(today.Year, birth.Month, birth.Day);

            if (niver < today)
            {
                niver = niver.AddYears(1);
            }

            int faltam = (niver - today).Days;
            return faltam;
        }
        public override string ToString()
        {
            return " Nome Completo: " + nome + sobreNome +
                   "\n Data do Aniversario: " + birth.Day + "/" + birth.Month + "/" + birth.Year
                   + "\n Faltam " + diasAteAniversario() + " dias para esse aniversario";
        }
    }
}
