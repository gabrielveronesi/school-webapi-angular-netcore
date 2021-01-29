using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.V1.Dtos;
using SmartSchool.API.Models;
using System.Threading.Tasks;
using SmartSchool.API.Helpers;

namespace SmartSchool.API.V1.Controllers
{
    ///<sumary>
    /// Versão 1 do meu controlador de alunos
    ///</sumary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        //quando eu instanciar uma controller do tipo aluno vou passar como parametro o meu contexto -> _context

        public readonly IRepository _repo;

        private readonly IMapper _mapper;

        public AlunoController(IRepository repo,
                               IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        
        [HttpGet]
        //api/v1/aluno?pageNumber=1&pageSize=5
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams) //Quando eu utilizo o [FromQuery] é como se tivesse passado um BODY pra buscar alunos. SÓ PRA NAO DAR ERRO
        {
            //Porque trabalhar com task? Ganho de performace, sempre usar async
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPag, alunos.PageSize, alunos.TotalCount, alunos.TotalPages); //ELe sempre vai me mostrar a Pagina Atual, o Tamanho de Items, o Total de Items e o TOtal de pagionas 

            return Ok(alunosResult);
        }
        //api/aluno/1
        /// <summary>
        /// Método responsavel para retornar 1 aluno pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            
            var alunoDto = _mapper.Map<AlunoRegistrarDto>(aluno);
            return Ok(alunoDto);
        }

        [HttpGet("{getRegister}")]
        public IActionResult GetRegister()
        {
           return Ok(new AlunoRegistrarDto());
        }



        [HttpGet("ByDisciplina/{id}")]
        public async Task<IActionResult> GetByDisciplicaId(int id)
        {
            var result = await _repo.GetAllAlunosByDisciplinaIdAsync(id, false);
           return Ok(result);
        }






        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        //Escolha o PUT se o que você pretende é fazer uma atualização completa do seu recurso ou o PATCH se você quiser atualizar apenas um subconjunto dos dados do seu recurso.

        [HttpPatch("{id}")] //alterar
        public IActionResult Patch(int id, AlunoPatchDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoPatchDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }


//TROCAR ESTADO
 [HttpPatch("{id}/trocarEstado")] //alterar
        public IActionResult trocarEstado(int id, TrocaEstadoDto trocaEstado)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            aluno.Ativo = trocaEstado.Estado;

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                var msn = aluno.Ativo ? "ativado" : "desativado";
                return Ok(new { message = $"Aluno {msn} com sucesso!"});
            }

            return BadRequest("Aluno não atualizado");
        }



        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");


            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado!");
            }

            return BadRequest("Aluno não deletado");
        }


    }
}