using System;

namespace SmartSchool.API.V1.Dtos
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataIni { get; set; }
        public bool Ativo { get; set; }
    }

    //dto -> Data Trance Objects - Transferencia de dados no formato json para quem for consumir a api. Mudar a maneira do objeto ser transferido
    //Ajuda a não enviar muitas informações desnecessárias.
}