using System;
using System.Collections.Generic;

namespace SmartSchool.API.Models
{
    public class Disciplina
    {
        public Disciplina() { }
        public Disciplina(int id,
                          string nome,
                          int professorId,
                          int cursoId)
        {
            this.Id = id;
            this.Nome = nome;
            this.ProfessorId = professorId;
            this.CursoId = cursoId;
            
        }
        // ? -> Significa que pode ser Null.. não precisa de um valor.
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }
        public int? Nota { get; set; } = null;
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public int? PrerequisitoId { get; set; } = null;
        //Relacionamento - Quando você for fazer uma matéria CALCULO 2, ele precisa terminar o CALCULO 1, CALCULO 1 é pré requisito pro CALCULO 2
        public Disciplina Prerequisito { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        //A minha disciplina precisa ser cadastrada para um determinado curso
    
        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }


}