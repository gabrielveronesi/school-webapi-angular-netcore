namespace SmartSchool.API.Models
{
    public class AlunoCurso
    {
        public AlunoCurso() { }
        public AlunoCurso(int alunoId, int cursoId)
        {
            
            this.AlunoId = alunoId;
            this.CursoId = cursoId;
            
        }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }


    }
}