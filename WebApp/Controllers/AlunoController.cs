using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        public IEnumerable<Aluno> Get()
        {
            Aluno aluno = new Aluno();
            return aluno.ListarAlunos();
        }

        // GET: api/Aluno/5
        public Aluno Get(int id)
        {

            Aluno aluno = new Aluno();
            return aluno.ListarAlunos().Where(x => x.id == id).FirstOrDefault();
        }
        // POST: api/Aluno
        public List<Aluno> Post(Aluno aluno)            
        {
            Aluno _alunos = new Aluno();
            _alunos.Inserir(aluno);
            return _alunos.ListarAlunos();
        }
     

        // PUT: api/Aluno/5
        public Aluno Put(int id, [FromBody]Aluno aluno)
        {
            Aluno _aluno = new Aluno();

            return _aluno.Atualizar(id, aluno);
        }

        // DELETE: api/Aluno/5
        public bool Delete(int id)
        {
            Aluno _aluno = new Aluno();

            return _aluno.Deletar(id);
        }
    }
}
