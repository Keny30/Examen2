using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI.WebControls;

namespace EXAMENPRACTICA.Clases
{
    public class Usuario
    {

        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }

        public Usuario() { }

        public static Usuario Consultar(int UsuarioID) {
            Usuario usuarioFiltrado = new Usuario();
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SELECT UsuarioID,Nombre,CorreoElectronico,Telefono  FROM Usuarios WHERE UsuarioID = @UsuarioID", connection) { 
                CommandType = CommandType.Text
                };

                try
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Retrieve the value from the SqlDataReader
                        usuarioFiltrado.UsuarioID = Convert.ToInt32(reader["UsuarioID"]);
                        usuarioFiltrado.Nombre = reader["Nombre"].ToString();
                        usuarioFiltrado.CorreoElectronico = reader["CorreoElectronico"].ToString();
                        usuarioFiltrado.Telefono = reader["Telefono"].ToString();
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
            return usuarioFiltrado;
        }

        public static int Agregar(string Nombre, string CorreoElectronico, string Telefono)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (Nombre, CorreoElectronico, Telefono) VALUES (@Nombre, @CorreoElectronico, @Telefono)", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", Telefono));

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
        public static int Modificar(int UsuarioID, string Nombre, string CorreoElectronico, string Telefono)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Nombre = @Nombre, CorreoElectronico = @CorreoElectronico, Telefono = @Telefono WHERE UsuarioID = @UsuarioID", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", Telefono));

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
        public static int Borrar(int UsuarioID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID", Conn)
                    {
                        CommandType = CommandType.Text
                    };
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
    }
}