using SmartSchool.API.Models;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class; //O paramêtro que for passado no ADD sempre vai ter que ser uma classe/ o que passar como parametro vai ser o tipo base
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         bool SaveChanges();
    
        //ALUNOS
        public Aluno[] GetAllAlunos(bool includeProfessor = false);
        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

        //PROFESSORES
        public Professor[] GetAllProfessores(bool includeAlunos = false);
        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
        public Professor GetProfessorById(int professorId, bool includeProfessor = false);


        

    }
}