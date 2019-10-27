using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno aluno = new Aluno();
                return Ok(aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // GET: api/Aluno
        public IHttpActionResult Get()
        {
            try
            {
                var aluno = new AlunoDAO();
                return Ok(aluno.ListarAlunosDB());
            }
            catch (Exception ex)
            {
                throw new Exception($"Ero ao Listar Aluno: Erro => { ex.Message }");
            }

        }

        // GET: api/Aluno/5[
        [HttpGet]
        [Route("Recuperar/{id}")]
        public AlunoDTO Get(int id)
        {

            Aluno aluno = new Aluno();
            return aluno.ListarAlunos().Where(x => x.id == id).FirstOrDefault();
        }

        [HttpGet]
        [Route("Recuperar/{id}/{nome}/{sobrenome=andrade}")]
        public AlunoDTO Get(int id, string nome, string sobrenome)
        {

            Aluno aluno = new Aluno();
            return aluno.ListarAlunos().Where(x => x.id == id && x.nome == nome && x.sobrenome == sobrenome).FirstOrDefault();
        }

        [HttpGet]
        //data: expressão regular sequencia de 0 e 4 numeros de 0 a 9 e outra sequencia de 0 a 9 com 2 numeros ou seja ex: 2019-02
        //nome: precisa de no minimo 5 caracteres para começar a pesquisar
        [Route(@"RecuperarPordataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        public IHttpActionResult Get(string data, string nome)
        {
            try
            {
                Aluno aluno = new Aluno();
                IEnumerable<AlunoDTO> alunos = aluno.ListarAlunos().Where(x => x.data == data || x.nome == nome);

                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // POST: api/Aluno
        public IHttpActionResult Post(AlunoDTO aluno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                AlunoDAO _alunos = new AlunoDAO();
                _alunos.InserirAlunoDB(aluno);
                return Ok(_alunos.ListarAlunosDB());
            }
            catch (Exception ex)
            {
                throw new Exception($"Ero ao Inserir Aluno: Erro => { ex.Message }");
            }

        }


        // PUT: api/Aluno/5
        public List<AlunoDTO> Atualizar(int id, [FromBody]AlunoDTO aluno)
        {
            try
            {
                AlunoDAO _aluno = new AlunoDAO();

                _aluno.AtualizarDB(id, aluno);

                return _aluno.ListarAlunosDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ero ao Atualizar Aluno: Erro => { ex.Message }");
            }
        }

        // DELETE: api/Aluno/5
        public bool Delete(int id)
        {
            try
            {
                AlunoDAO _aluno = new AlunoDAO();

                _aluno.DeletarDB(id);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Ero ao Atualizar Aluno: Erro => { ex.Message }");
            }
        }
    }
}
