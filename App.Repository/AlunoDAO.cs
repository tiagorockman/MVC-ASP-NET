using App.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace App.Repository
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
        public List<AlunoDTO> ListarAlunosDB(int? id = null)
        {

            try
            {

                var listaAlunos = new List<AlunoDTO>();
                IDbCommand dbCommand = connection.CreateCommand();
                if (id == null)
                    dbCommand.CommandText = "SELECT * FROM TB_ALUNOS";
                else
                    dbCommand.CommandText = "SELECT * FROM TB_ALUNOS where id = " + id;

                IDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    var aluno = new AlunoDTO()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        nome = reader["nome"].ToString(),
                        sobrenome = reader["sobrenome"].ToString(),
                        telefone = reader["telefone"].ToString(),
                        data = reader["data"].ToString(),
                        RA = Convert.ToInt32(reader["RA"]),
                    };

                    listaAlunos.Add(aluno);
                }
                return listaAlunos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }


        }

        public void InserirAlunoDB(AlunoDTO aluno)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public void AtualizarDB(int id, AlunoDTO aluno)
        {
            try
            {
                IDbCommand updatedbCommand = connection.CreateCommand();
                updatedbCommand.CommandText = @"UPDATE TB_ALUNOS SET nome = @nome,
                                                sobrenome = @sobrenome, 
                                                telefone = @telefone, 
                                                data = @data
                                                RA = @RA
                                                WHERE ID = @id";

                IDbDataParameter parameterNome = new SqlParameter("nome", aluno.nome);
                updatedbCommand.Parameters.Add(parameterNome);
                IDbDataParameter parameterSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                updatedbCommand.Parameters.Add(parameterSobrenome);
                IDbDataParameter parameterTel = new SqlParameter("telefone", aluno.telefone);
                updatedbCommand.Parameters.Add(parameterTel);
                IDbDataParameter parameterData = new SqlParameter("data", aluno.data);
                updatedbCommand.Parameters.Add(parameterData);
                IDbDataParameter parameterRA = new SqlParameter("RA", aluno.RA);
                updatedbCommand.Parameters.Add(parameterRA);

                IDbDataParameter parameterId = new SqlParameter("id", id);
                updatedbCommand.Parameters.Add(parameterId);

                updatedbCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

        }

        public void DeletarDB(int id)
        {
            try
            {
                IDbCommand updatedbCommand = connection.CreateCommand();
                updatedbCommand.CommandText = @"Delete from TB_ALUNOS WHERE ID = @id";

                IDbDataParameter parameterId = new SqlParameter("id", id);
                updatedbCommand.Parameters.Add(parameterId);

                updatedbCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

        }
    }


}