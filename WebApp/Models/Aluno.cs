using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public int RA { get; set; }

        public List<Aluno> ListarAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Start/JsonBase.txt");
            var json = File.ReadAllText(caminhoArquivo);
            var listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);
            return listaAlunos;
        }

        public bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Start/JsonBase.txt");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);

            return true;

        }

        public Aluno Inserir(Aluno Aluno)
        {
            var listaAlunos = this.ListarAlunos();
            var maxId = listaAlunos.Max(a => a.id);
            Aluno.id = maxId + 1;
            listaAlunos.Add(Aluno);

            ReescreverArquivo(listaAlunos);
            return Aluno;
        }

        public Aluno Atualizar(int id, Aluno aluno)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(a => a.id == aluno.id);
            if(itemIndex >= 0)
            {
                aluno.id = id;
                listaAlunos[itemIndex] = aluno;
            }
            else
            {
                return null;
            }

            ReescreverArquivo(listaAlunos);
            return aluno;

        }

        public bool Deletar(int id)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(a => a.id == id);
            if (itemIndex >= 0)
            {           
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverArquivo(listaAlunos);
            return true;

        }

        //Alunos aluno = new Alunos();
        //aluno.id = 1;
        //aluno.nome = "Julia";
        //aluno.sobrenome = "Will";
        //aluno.telefone = "3138818056";
        //aluno.RA = 256858;

        //Alunos aluno1 = new Alunos();
        //aluno1.id = 2;
        //aluno1.nome = "Marta";
        //aluno1.sobrenome = "Jeff";
        //aluno1.telefone = "3138818556";
        //aluno1.RA = 256958;

        //List<Alunos> listaAlunos = new List<Alunos>();

        //listaAlunos.Add(aluno);
        //listaAlunos.Add(aluno1);
    }
}