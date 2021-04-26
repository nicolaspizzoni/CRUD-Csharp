using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace Funcionarios
{
    class DAO
    {
        static string serverName = "127.0.0.1";
        static string port = "5432";
        static string userName = "postgres";
        static string password = "unip2021";
        static string dataBase = "cadastro";

        NpgsqlConnection npgsqlConnection = null;
        string connString = null;

        public DAO()
        {
            connString = String.Format("Server={0}; Port={1}; User Id={2}; " + 
                "Password={3}; Database={4};", serverName, port, userName, password, dataBase);
        }

        public void inserirFuncionario(Funcionario funcionario)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connString))
                {
                    npgsqlConnection.Open();

                    string cmdinserir = String.Format("insert into funcionarios(nome, email, idade)" +
                        "values('{0}', '{1}', {2})", funcionario.nome, funcionario.email, funcionario.idade);
                    using(NpgsqlCommand npgsqlCommand = new NpgsqlCommand(cmdinserir, npgsqlConnection))
                    {
                        npgsqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch(NpgsqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public DataTable listarFuncionarios()
        {
            DataTable dt = new DataTable();
                try
            {
                using(npgsqlConnection = new NpgsqlConnection(connString)) //passando dados de conexão
                {
                    npgsqlConnection.Open(); //abrir a conexão
                    string sql = "SELECT * FROM funcionarios ORDER BY nome";

                    using(NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection))
                    {
                        npgsqlDataAdapter.Fill(dt);
                    }
                }
            }
            catch(NpgsqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                npgsqlConnection.Close();
            }
            return dt;

        }

        public void alterarFuncionario(Funcionario funcionario)
        {
            try
            {
                using(npgsqlConnection = new NpgsqlConnection(connString))
                {
                    npgsqlConnection.Open();

                    string sql = String.Format("UPDATE funcionarios SET nome = '{0}', " +
                        "email = '{1}', idade = {2} " + 
                        "WHERE id = {3}", funcionario.nome, funcionario.email, funcionario.idade, funcionario.id);

                    using(NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, npgsqlConnection))
                    {
                        npgsqlCommand.ExecuteNonQuery();
                    }
                    
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                npgsqlConnection.Close();
            }
        }
        public void excluirFuncionario(Funcionario funcionario)
        {
            try
            {
                using(npgsqlConnection = new NpgsqlConnection(connString))
                {
                    npgsqlConnection.Open();

                    string sql = String.Format("DELETE FROM funcionarios WHERE id = {0} ", funcionario.id);

                    using(NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sql, npgsqlConnection))
                    {
                        npgsqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch(NpgsqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                npgsqlConnection.Close();
            }
        }
    }
}
