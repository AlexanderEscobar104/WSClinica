using Microsoft.Data.SqlClient;
using System.Data;
using WSClinica.Connection;
using WSClinica.Models;

namespace WSClinica.Services
{
    public class MedicoService
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        ConnectionBd _connectionBd = null;
        Medico resultado = null;

        /// <summary>
        /// metodo para listar medicos
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="especialidad"></param>
        /// <returns></returns>
        public List<Medico> GetAll(int opcion, int id, string nombre, string apellido, string especialidad)
        {
            _connectionBd = new ConnectionBd();
            List<Medico> medicoList = new List<Medico>();
            using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[SelectMedico]";
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
                if (apellido != "")
                {
                    _command.Parameters.AddWithValue("@Apellido", SqlDbType.VarChar).Value = apellido;
                }
                if (especialidad != "")
                {
                    _command.Parameters.AddWithValue("@Especialidad", SqlDbType.VarChar).Value = especialidad;
                }
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {

                    Medico medicos = new Medico();
                    medicos.Id = Convert.ToInt32(dr["Id"]);
                    medicos.Nombre = dr["Nombre"].ToString()!;
                    medicos.Apellido = dr["Apellido"].ToString()!;
                    medicos.Numero_licencia = dr["Numero_licencia"].ToString()!;
                    medicos.Especialidad = dr["Especialidad"].ToString()!;
                    medicos.Telefono = dr["Telefono"].ToString()!;
                    medicos.Email = dr["Email"].ToString()!;
                    medicos.Estado = dr["Estado"].ToString()!;
                    medicoList.Add(medicos);
                }
                _connection.Close();
            }
            return medicoList;
        }

        /// <summary>
        /// metodo para agregar medicos
        /// </summary>
        /// <param name="medicos"></param>
        /// <returns></returns>
        public string AddMedico(Medico medicos)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Medico();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[InsertMedico]";
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = medicos.Nombre;
                    _command.Parameters.AddWithValue("@Apellido", SqlDbType.VarChar).Value = medicos.Apellido;
                    _command.Parameters.AddWithValue("@Numero_licencia", SqlDbType.VarChar).Value = medicos.Numero_licencia;
                    _command.Parameters.AddWithValue("@Especialidad", SqlDbType.VarChar).Value = medicos.Especialidad;
                    _command.Parameters.AddWithValue("@Telefono", SqlDbType.VarChar).Value = medicos.Telefono;
                    _command.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = medicos.Email;
                    _command.Parameters.AddWithValue("@Estado", SqlDbType.VarChar).Value = medicos.Estado;
                    _connection.Open();
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Medico Almacenada";

            }
            catch (Exception ex)
            {
                return "Error Medico No Almacenada " + ex;

            }
        }

        /// <summary>
        /// metodo para actualizar medico 
        /// </summary>
        /// <param name="medicos"></param>
        /// <returns></returns>
        public string UpdateMedico(Medico medicos)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Medico();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[UpdateMedico]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = medicos.Id;
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = medicos.Nombre;
                    _command.Parameters.AddWithValue("@Apellido", SqlDbType.VarChar).Value = medicos.Apellido;
                    _command.Parameters.AddWithValue("@Numero_licencia", SqlDbType.VarChar).Value = medicos.Numero_licencia;
                    _command.Parameters.AddWithValue("@Especialidad", SqlDbType.VarChar).Value = medicos.Especialidad;
                    _command.Parameters.AddWithValue("@Telefono", SqlDbType.VarChar).Value = medicos.Telefono;
                    _command.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = medicos.Email;
                    _command.Parameters.AddWithValue("@Estado", SqlDbType.VarChar).Value = medicos.Estado;
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Medico Actualizada";

            }
            catch (Exception ex)
            {
                return "Error Medico No Actualizada " + ex;

            }
        }

        /// <summary>
        /// metodo para eliminar medico 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteMedico(int Id)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Medico();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[DeleteMedico]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Id;
                    _connection.Open();
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Medico Eliminada";

            }
            catch (Exception ex)
            {
                return "Error Medico No Eliminada " + ex;

            }
        }
    }
}
