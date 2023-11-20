using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace EXAMENPRACTICA.Clases
{
    public class Equipo
    {
        public int EquipoID { get; set; }
        public string TipoEquipo { get; set; }
        public string Modelo { get; set; }
        public int UsuarioID { get; set; }

        public Equipo() { }

        public static Equipo Consultar(int EquipoID)
        {
            Equipo equipoFiltrado = new Equipo();
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SELECT EquipoID,TipoEquipo,Modelo,UsuarioID  FROM Equipos WHERE EquipoID = @EquipoID", connection)
                {
                    CommandType = CommandType.Text
                };

                try
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@EquipoID", EquipoID));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Retrieve the value from the SqlDataReader
                        equipoFiltrado.EquipoID = Convert.ToInt32(reader["EquipoID"]);
                        equipoFiltrado.TipoEquipo = reader["TipoEquipo"].ToString();
                        equipoFiltrado.Modelo = reader["Modelo"].ToString();
                        equipoFiltrado.UsuarioID = int.Parse(reader["UsuarioID"].ToString());
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return equipoFiltrado;
        }

        public static int Agregar(string TipoEquipo, string Modelo, int UsuarioID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Equipos (TipoEquipo, Modelo, UsuarioID) VALUES (@TipoEquipo, @Modelo, @UsuarioID)", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@TipoEquipo", TipoEquipo));
                    cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;

        }
        public static int Modificar(int EquipoID, string TipoEquipo, string Modelo, int UsuarioID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Equipos SET TipoEquipo = @TipoEquipo, Modelo = @Modelo, UsuarioID = @UsuarioID WHERE EquipoID = @EquipoID", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@EquipoID", EquipoID));
                    cmd.Parameters.Add(new SqlParameter("@TipoEquipo", TipoEquipo));
                    cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;

        }
        public static int Borrar(int EquipoID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Equipos WHERE EquipoID = @EquipoID", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@EquipoID", EquipoID));


                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;


        }
    }
}