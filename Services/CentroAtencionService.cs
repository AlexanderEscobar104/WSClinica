using Microsoft.Data.SqlClient;
using System.Data;
using WSClinica.Connection;
using WSClinica.Models;

namespace WSClinica.Services
{
    public class CentroAtencionService
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        ConnectionBd _connectionBd = null;
        CentroAtencion resultado = null;

        /// <summary>
        /// metodo para consultar los centros de atención
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<CentroAtencion> GetAll(int opcion, int id, string nombre)
        {
            _connectionBd = new ConnectionBd();
            List<CentroAtencion> centroAtencionList = new List<CentroAtencion>();
            //se establece nueva conexión 
            using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[SelectCentroAtencion]";
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
                //se abre la conexion
                _connection.Open();
                //se ejecuta el procedimiento
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    CentroAtencion centroAtencion = new CentroAtencion();
                    centroAtencion.Id = Convert.ToInt32(dr["Id"]);
                    centroAtencion.Nombre = dr["Nombre"].ToString()!;
                    centroAtencion.Direccion = dr["Direccion"].ToString()!;
                    centroAtencionList.Add(centroAtencion);
                }
                //se cierra conexión
                _connection.Close();
            }
            return centroAtencionList;
        }

        /// <summary>
        /// metodo para agregar nuevo centro de atención
        /// </summary>
        /// <param name="centroAtencion"></param>
        /// <returns></returns>
        public string AddCentroAtencion(CentroAtencion centroAtencion)
        {
            _connectionBd = new ConnectionBd();
            resultado = new CentroAtencion();

            try
            {
                //se establece nueva conexión 
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[InsertCentroAtencion]";
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = centroAtencion.Nombre;
                    _command.Parameters.AddWithValue("@Direccion", SqlDbType.VarChar).Value = centroAtencion.Direccion;
                    //se abre la conexion
                    _connection.Open();
                    //se ejecuta el procedimiento
                    SqlDataReader dr = _command.ExecuteReader();
                    //se cierra conexión
                    _connection.Close();
                }
                return "Centro Atencion Almacenada";

            }
            catch (Exception ex)
            {
                return "Error Centro Atencion No Almacenada " + ex;

            }
        }

        /// <summary>
        /// metodo para actualizar centro de atención
        /// </summary>
        /// <param name="centroAtencion"></param>
        /// <returns></returns>
        public string UpdateCentroAtencion(CentroAtencion centroAtencion)
        {
            _connectionBd = new ConnectionBd();
            resultado = new CentroAtencion();

            try
            {
                //se establece nueva conexión 
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[UpdateCentroAtencion]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = centroAtencion.Id;
                    _command.Parameters.AddWithValue("@Nombre", SqlDbType.VarChar).Value = centroAtencion.Nombre;
                    _command.Parameters.AddWithValue("@Direccion", SqlDbType.VarChar).Value = centroAtencion.Direccion;
                   //se abre la conexion
                    _connection.Open();
                    //se ejecuta el procedimiento
                    SqlDataReader dr = _command.ExecuteReader();
                    //se cierra conexión
                    _connection.Close();
                }
                return "Centro Atencion Actualizada";

            }
            catch (Exception ex)
            {
                return "Error Centro Atencion No Actualizada " + ex;

            }
        }

        /// <summary>
        /// metodo para eliminar el centro de atención
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteCentroAtencion(int Id)
        {
            _connectionBd = new ConnectionBd();
            resultado = new CentroAtencion();

            try
            {
                //se establece nueva conexión 
                using (_connection = new SqlConnection(_connectionBd.GetConnectionString()))
                {
                    _command = _connection.CreateCommand();
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = "[dbo].[DeleteCentroAtencion]";
                    _command.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Id;
                    //se abre la conexion
                    _connection.Open();
                    //se ejecuta el procedimiento
                    SqlDataReader dr = _command.ExecuteReader();
                    //se cierra conexión
                    _connection.Close();
                }
                return "Centro Atencion Eliminada";

            }
            catch (Exception ex)
            {
                return "Error Centro Atencion No Eliminada " + ex;

            }
        }

    }
}
