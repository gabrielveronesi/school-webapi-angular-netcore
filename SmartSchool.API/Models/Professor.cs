using System;
using System.Collections.Generic;

namespace SmartSchool.API.Models
{
    public class Professor
    {
        public Professor() { }

    //paramêtros passado para o Professor quer dizer que é obrigatorio na hora da criação!
        public Professor(int id,
                         int registro,
                         string nome, 
                         string sobrenome)
        {
            this.Id = id;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Registro = registro;
        }
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}