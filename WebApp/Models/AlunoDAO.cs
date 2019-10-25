using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
      
    public class AlunoDAO
    {
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private IDbConnection connection;

        public AlunoDAO()
        {
            connection = new SqlConnection(stringConexao);
            connection.Open();
        }


        //LISTA ALUNO DO DB
        public List<Aluno> ListarAlunosDB()
        {
           
            var listaAlunos = new List<Aluno>();
            IDbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = "SELECT * FROM TB_ALUNOS";

            IDataReader reader = dbCommand.ExecuteReader();
            while (reader.Read())
            {
                Aluno aluno = new Aluno();
                aluno.id = Convert.ToInt32(reader["id"]);
                aluno.nome = reader["nome"].ToString();
                aluno.sobrenome = reader["sobrenome"].ToString();
                aluno.telefone = reader["telefone"].ToString();
                aluno.data = reader["data"].ToString();
                aluno.RA = Convert.ToInt32(reader["RA"]);

                listaAlunos.Add(aluno);
            }

            connection.Close();
            connection.Dispose();

            return listaAlunos;
        }

        public void InserirAlunoDB(Aluno aluno)
        {
            IDbCommand insertdbCommand = connection.CreateCommand();
            insertdbCommand.CommandText = "Insert into TB_ALUNOS (nome, sobrenome, telefone, data, RA) values (@nome, @sobrenome, @telefone, @data, @RA)";

            IDbDataParameter parameterNome = new SqlParameter("nome", aluno.nome);
            insertdbCommand.Parameters.Add(parameterNome);
            IDbDataParameter parameterSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
            insertdbCommand.Parameters.Add(parameterSobrenome);
            IDbDataParameter parameterTel = new SqlParameter("telefone", aluno.telefone);
            insertdbCommand.Parameters.Add(parameterTel);
            IDbDataParameter parameterData = new SqlParameter("data", aluno.data);
            insertdbCommand.Parameters.Add(parameterData);
            IDbDataParameter parameterRA = new SqlParameter("RA", aluno.RA);
            insertdbCommand.Parameters.Add(parameterRA);

            insertdbCommand.ExecuteNonQuery();
        }
    }


}