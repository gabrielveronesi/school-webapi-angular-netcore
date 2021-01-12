using System.Collections.Generic;

namespace SmartSchool.API.Models
{
    public class Curso
    {
        public Curso() {}

        public Curso(int id,
                     string nome) 
        {
                this.Id = id;
                this.Nome = nome;      
        }
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}

//IEnumerable é uma interface que marca as classes que desejam implementá-la para que se saiba que ela possa ter iterável através de um iterador. Obviamente que isso só deve ser usado em objetos que seja sequências de dados, caso contrário não faz sentido iterar ali. O método é usado para obter o iterador. Na maioria das vezes a implementação deste método é igual ou muito parecida.