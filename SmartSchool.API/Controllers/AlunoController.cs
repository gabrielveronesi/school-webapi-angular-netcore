using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        //quando eu instanciar uma controller do tipo aluno vou passar como parametro o meu contexto -> _context
        
        public readonly IRepository _repo;

        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            var alunosRetorno = new List<AlunoDto>();

            foreach (var aluno in alunos)
            {
                alunosRetorno.Add(new AlunoDto(){
                    Id = aluno.Id,
                    Matricula = aluno.Matricula,
                    Nome = $"{aluno.Nome} {aluno.Sobrenome}",
                    Telefone = aluno.Telefone,
                    DataNasc = aluno.DataNasc,
                    DataIni = aluno.DataIni,
                    Ativo = aluno.Ativo
                });
            }

            return Ok(alunosRetorno);
        }
        //api/aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
                _repo.Add(aluno);
                if(_repo.SaveChanges()) 
                {
                    return Ok(aluno);
                }

                return BadRequest("Aluno não cadastrado");
        }

         [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("O Aluno não foi encontrado");
                
                _repo.Update(aluno);
                if(_repo.SaveChanges()) 
                {
                    return Ok(aluno);
                }

                return BadRequest("Aluno não atualizado");
        }

         [HttpPatch("{id}")] //alterar
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("O Aluno não foi encontrado");
                
                _repo.Update(aluno);
                if(_repo.SaveChanges()) 
                {
                    return Ok(aluno);
                }

                return BadRequest("Aluno não atualizado");
        }

         [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("O Aluno não foi encontrado");
                
                
                _repo.Delete(alu);
                if(_repo.SaveChanges()) 
                {
                    return Ok("Aluno deletado!");
                }

                return BadRequest("Aluno não deletado");
        }
        

    }
}