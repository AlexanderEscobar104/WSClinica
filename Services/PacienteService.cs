using Microsoft.Data.SqlClient;
using System.Data;
using WSClinica.Connection;
using WSClinica.Models;

namespace WSClinica.Services
{
    public class PacienteService
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        ConnectionBd _connectionBd = null;
        Paciente resultado = null;

        /// <summary>
        /// medoto para listar paciente
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        public List<Paciente> GetAll(int opcion, int id, string nombre, string apellido, string identificacion)
        {
            _connectionBd = new ConnectionBd();
            List<Paciente> pacienteList = new List<Paciente>();
            using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[SelectPaciente]";
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
                if (identificacion != "")
                {
                    _command.Parameters.AddWithValue("@Identificacion", SqlDbType.VarChar).Value = identificacion;
                }
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {

                    Paciente pacientes = new Paciente();
                    pacientes.Id = Convert.ToInt32(dr["Id"]);
                    pacientes.Nombre = dr["Nombre"].ToString()!;
                    pacientes.Apellido = dr["Apellido"].ToString()!;
                    pacientes.Identificacion = dr["Identificacion"].ToString()!;
                    pacientes.Fecha_nacimiento = Convert.ToDateTime(dr["Fecha_nacimiento"].ToString())!;
                    pacientes.Genero = Convert.ToChar(dr["Genero"].ToString()!);
                    pacientes.Direccion = dr["Direccion"].ToString()!;
                    pacientes.Telefono = dr["Telefono"].ToString()!;
                    pacientes.Email = dr["Email"].ToString()!;
                    pacienteList.Add(pacientes);
                }
                _connection.Close();
            }
            return pacienteList;
        }

        /// <summary>
        /// metodo para agregar paciente
        /// </summary>
        /// <param name="pacientes"></param>
        /// <returns></returns>
        public string AddPaciente(Paciente pacientes)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Paciente();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[InsertPaciente]";
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = pacientes.Nombre;
                    _command.Parameters.AddWithValue("@Apellido", SqlDbType.VarChar).Value = pacientes.Apellido;
                    _command.Parameters.AddWithValue("@Identificacion", SqlDbType.VarChar).Value = pacientes.Identificacion;
                    _command.Parameters.AddWithValue("@Fecha_nacimiento", SqlDbType.DateTime).Value = pacientes.Fecha_nacimiento;
                    _command.Parameters.AddWithValue("@Genero", SqlDbType.VarChar).Value = pacientes.Genero;
                    _command.Parameters.AddWithValue("@Direccion", SqlDbType.VarChar).Value = pacientes.Direccion;
                    _command.Parameters.AddWithValue("@Telefono", SqlDbType.VarChar).Value = pacientes.Telefono;
                    _command.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = pacientes.Email;
                    _connection.Open();
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Paciente Almacenado";

            }
            catch (Exception ex)
            {
                return "Error Paciente No Almacenado " + ex;

            }
        }

        /// <summary>
        /// metodo para actualizar paciente
        /// </summary>
        /// <param name="pacientes"></param>
        /// <returns></returns>
        public string UpdatePaciente(Paciente pacientes)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Paciente();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[UpdatePaciente]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = pacientes.Id;
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = pacientes.Nombre;
                    _command.Parameters.AddWithValue("@Apellido", SqlDbType.VarChar).Value = pacientes.Apellido;
                    _command.Parameters.AddWithValue("@Identificacion", SqlDbType.VarChar).Value = pacientes.Identificacion;
                    _command.Parameters.AddWithValue("@Fecha_nacimiento", SqlDbType.DateTime).Value = pacientes.Fecha_nacimiento;
                    _command.Parameters.AddWithValue("@Genero", SqlDbType.VarChar).Value = pacientes.Genero;
                    _command.Parameters.AddWithValue("@Direccion", SqlDbType.VarChar).Value = pacientes.Direccion;
                    _command.Parameters.AddWithValue("@Telefono", SqlDbType.VarChar).Value = pacientes.Telefono;
                    _command.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = pacientes.Email;
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Paciente Actualizado";

            }
            catch (Exception ex)
            {
                return "Error Paciente No Actualizado " + ex;

            }
        }

        /// <summary>
        /// metodo para eliminar paciente
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeletePaciente(int Id)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Paciente();

            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[DeletePaciente]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Id;
                    _connection.Open();
                    SqlDataReader dr = _command.ExecuteReader();
                    _connection.Close();
                }
                return "Paciente Eliminado";

            }
            catch (Exception ex)
            {
                return "Error Paciente No Eliminado " + ex;

            }
        }
    }
}
