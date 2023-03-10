using Microsoft.Data.SqlClient;
using System.Data;
using WSClinica.Connection;
using WSClinica.Models;

namespace WSClinica.Services
{
    public class SalaService
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        ConnectionBd _connectionBd = null;
        Sala resultado = null;

        /// <summary>
        /// metodo para listar sala
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<Sala> GetAll(int opcion, int id, string nombre)
        {
            _connectionBd = new ConnectionBd();
            List<Sala> salaList = new List<Sala>();
            using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[SelectSala]";
                if (opcion != 0)
                {
                    _command.Parameters.AddWithValue("@Opcion", SqlDbType.Int).Value = opcion;
                }
                if (id != 0)
                {
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                }
                if (nombre != "")
                {
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = nombre;
                }

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {

                    Sala salas = new Sala();
                    salas.Id = Convert.ToInt32(dr["Id"]);
                    salas.Nombre = dr["Nombre"].ToString()!;
                    salas.Id_centro_atencion = Convert.ToInt32(dr["Id_centro_atencion"].ToString());
                    salas.Disponible_Desde = Convert.ToDateTime(dr["Disponible_Desde"].ToString());
                    salas.Disponible_Hasta = Convert.ToDateTime(dr["Disponible_Hasta"].ToString());
                    

                    salaList.Add(salas);
                }
                _connection.Close();
            }
            return salaList;
        }

        /// <summary>
        /// metodo para agregar sala
        /// </summary>
        /// <param name="salas"></param>
        /// <returns></returns>
        public string AddSala(Sala salas)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Sala();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[InsertSala]";
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = salas.Nombre;
                    _command.Parameters.AddWithValue("@Id_centro_atencion", SqlDbType.Int).Value = salas.Id_centro_atencion;
                    _command.Parameters.AddWithValue("@Disponible_Desde", SqlDbType.Int).Value = salas.Disponible_Desde;
                    _command.Parameters.AddWithValue("@Disponible_Hasta", SqlDbType.Int).Value = salas.Disponible_Hasta;
                    _connection.Open();
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Sala Almacenada";

            }
            catch (Exception ex)
            {
                return "Error Sala No Almacenada " + ex;

            }
        }

        /// <summary>
        /// metodo par actualizar sala
        /// </summary>
        /// <param name="salas"></param>
        /// <returns></returns>
        public string UpdateSala(Sala salas)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Sala();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[UpdateSala]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = salas.Id;
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = salas.Nombre;
                    _command.Parameters.AddWithValue("@Id_centro_atencion", SqlDbType.Int).Value = salas.Id_centro_atencion;
                    _command.Parameters.AddWithValue("@Disponible_Desde", SqlDbType.Int).Value = salas.Disponible_Desde;
                    _command.Parameters.AddWithValue("@Disponible_Hasta", SqlDbType.Int).Value = salas.Disponible_Hasta;
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Sala Actualizada";

            }
            catch (Exception ex)
            {
                return "Error Sala No Actualizada " + ex;

            }
        }

        /// <summary>
        /// metodo para eliminar sala
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteSala(int Id)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Sala();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[DeleteSala]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Id;
                    _connection.Open();
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Sala Eliminada";

            }
            catch (Exception ex)
            {
                return "Error Sala No Eliminada " + ex;

            }
        }
    }
}
