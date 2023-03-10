using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using WSClinica.Connection;
using WSClinica.Models;

namespace WSClinica.Services
{
    public class CitasService
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        ConnectionBd _connectionBd = null;
        Citas resultado = null;

        /// <summary>
        /// metodo para consultar las citas
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<Citas> GetAll(int opcion, int id, string estado)
        {
            _connectionBd = new ConnectionBd();
            List<Citas> citaList = new List<Citas>();
            SqlDataReader dr = null;
            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[SelectCitas]";
                    if (opcion != 0)
                    {
                        _command.Parameters.AddWithValue("@Opcion", SqlDbType.Int).Value = opcion;
                    }
                    if (id != 0)
                    {
                        _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                    }
                    if (estado != "")
                    {
                        _command.Parameters.AddWithValue("@Estado", SqlDbType.VarChar).Value = estado;
                    }
                    _connection.Open();
                    dr = _command.ExecuteReader();
                    while (dr.Read())
                    {
                        Citas citas = new Citas();
                        citas.Id = Convert.ToInt32(dr["Id"]);
                        citas.Fecha_inicio = Convert.ToDateTime(dr["Fecha_inicio"].ToString())!;
                        citas.Duracion_minutos = Convert.ToInt32(dr["Duracion_minutos"].ToString())!;
                        citas.Id_sala = Convert.ToInt32(dr["Id_sala"].ToString())!;
                        citas.Id_paciente = Convert.ToInt32(dr["Id_paciente"].ToString())!;
                        citas.Id_medico = Convert.ToInt32(dr["Id_medico"].ToString())!;
                        citas.Estado = dr["Estado"].ToString()!;
                        citaList.Add(citas);
                    }
                    //se cierra conexión
                    _connection.Close();
                }
                return citaList;
            }
            catch (Exception ex)
            {
                return null;

            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// metodo para consultar las citas de un paciente 
        /// </summary>
        /// <param name="Identificacion"></param>
        /// <param name="Id_centro_atencion"></param>
        /// <returns></returns>
        public List<CitasPaciente> GetAllCitasPaciente(string Identificacion, int Id_centro_atencion )
        {
            _connectionBd = new ConnectionBd();
            List<CitasPaciente> citaPacienteList = new List<CitasPaciente>();
            SqlDataReader dr = null;
            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[SelectCitasPaciente]";
                    if (Identificacion != "")
                    {
                        _command.Parameters.AddWithValue("@Identificacion", SqlDbType.VarChar).Value = Identificacion;
                    }
                    if (Id_centro_atencion != 0)
                    {
                        _command.Parameters.AddWithValue("@Id_centro_atencion", SqlDbType.Int).Value = Id_centro_atencion;
                    }
                    _connection.Open();
                    dr = _command.ExecuteReader();
                    while (dr.Read())
                    {
                        CitasPaciente citasPaciente = new CitasPaciente();
                        citasPaciente.Identificacion = dr["Identificacion"].ToString()!;
                        citasPaciente.Paciente = dr["Paciente"].ToString()!;
                        citasPaciente.Fecha_inicio = Convert.ToDateTime(dr["Fecha_inicio"].ToString())!;
                        citasPaciente.Duracion_minutos = Convert.ToInt32(dr["Duracion_minutos"].ToString())!;
                        citasPaciente.Estado = dr["Estado"].ToString()!;
                        citasPaciente.Sala = dr["Sala"].ToString()!;
                        citasPaciente.Medico = dr["Medico"].ToString()!;
                        citasPaciente.CentroAtencion = dr["CentroAtencion"].ToString()!;
                        citaPacienteList.Add(citasPaciente);
                    }
                    //se cierra conexión
                    _connection.Close();
                }
                return citaPacienteList;
            }
            catch (Exception ex)
            {
                return null;

            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// metodo para crear nuevas citas
        /// </summary>
        /// <param name="citas"></param>
        /// <returns></returns>
        public string AddCitas(Citas citas)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Citas();
            SqlDataReader dr = null;
            int id = 0;
            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[InsertCitas]";
                    _command.Parameters.AddWithValue("@Fecha_inicio", SqlDbType.VarChar).Value = citas.Fecha_inicio;
                    _command.Parameters.AddWithValue("@Duracion_minutos", SqlDbType.Int).Value = citas.Duracion_minutos;
                    _command.Parameters.AddWithValue("@Id_sala", SqlDbType.Int).Value = citas.Id_sala;
                    _command.Parameters.AddWithValue("@Id_paciente", SqlDbType.Int).Value = citas.Id_paciente;
                    _command.Parameters.AddWithValue("@Id_medico", SqlDbType.Int).Value = citas.Id_medico;
                    _command.Parameters.AddWithValue("@Estado", SqlDbType.VarChar).Value = citas.Estado;
                    _connection.Open();
                    dr = _command.ExecuteReader();
                    foreach (DbDataRecord dbDR in dr)
                    {
                        id = dbDR.GetInt32(0);
                        
                    }
                    //se cierra conexión
                    _connection.Close();
                }
                return "Cita Almacenada Id " + id;

            }
            catch (Exception ex)
            {
                return "Error Cita No Almacenada " + ex;

            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// metodo para actualizar citas
        /// </summary>
        /// <param name="citas"></param>
        /// <returns></returns>
        public string UpdateCitas(Citas citas)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Citas();
            SqlDataReader dr = null;
            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[UpdateCitas]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = citas.Id;
                    _command.Parameters.AddWithValue("@Fecha_inicio", SqlDbType.VarChar).Value = citas.Fecha_inicio;
                    _command.Parameters.AddWithValue("@Duracion_minutos", SqlDbType.VarChar).Value = citas.Duracion_minutos;
                    _command.Parameters.AddWithValue("@Id_sala", SqlDbType.VarChar).Value = citas.Id_sala;
                    _command.Parameters.AddWithValue("@Id_paciente", SqlDbType.VarChar).Value = citas.Id_paciente;
                    _command.Parameters.AddWithValue("@Id_medico", SqlDbType.VarChar).Value = citas.Id_medico;
                    _command.Parameters.AddWithValue("@Estado", SqlDbType.VarChar).Value = citas.Estado;
                    dr = _command.ExecuteReader();
                    //se cierra conexión
                    _connection.Close();
                }
                return "Cita Actualizada";

            }
            catch (Exception ex)
            {
                return "Error Cita No Actualizada " + ex;

            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// metodo para eliminar citas
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteCitas(int Id)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Citas();
            SqlDataReader dr = null;
            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[DeleteCitas]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Id;
                    _connection.Open();
                    dr = _command.ExecuteReader();
                    //se cierra conexión
                    _connection.Close();
                }
                return "Cita Eliminada";

            }
            catch (Exception ex)
            {
                return "Error Cita No Eliminada " + ex;

            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// metodo para cancelar citas
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string CancelCitas(int Id)
        {
            _connectionBd = new ConnectionBd();
            resultado = new Citas();
            SqlDataReader dr = null;
            try
            {
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[CancelCitas]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Id;
                    _connection.Open();
                     dr = _command.ExecuteReader();
                    //se cierra conexión
                    _connection.Close();
                }
                return "Cita Cancelada";

            }
            catch (Exception ex)
            {
                return "Error Cita No Cancelada " + ex;

            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
    }
}
