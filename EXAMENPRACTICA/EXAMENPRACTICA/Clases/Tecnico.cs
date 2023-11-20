using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace EXAMENPRACTICA.Clases
{
    public class Tecnico
    {
        public int TecnicoID { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }

        public Tecnico() { }

        public static Tecnico Consultar(int TecnicoID)
        {
            Tecnico tecnicoFiltrado = new Tecnico();
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("SELECT TecnicoID,Nombre,Especialidad FROM Tecnicos WHERE TecnicoID = @TecnicoID", connection)
                {
                    CommandType = CommandType.Text
                };

                try
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", TecnicoID));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Retrieve the value from the SqlDataReader
                        tecnicoFiltrado.TecnicoID = Convert.ToInt32(reader["TecnicoID"]);
                        tecnicoFiltrado.Nombre = reader["Nombre"].ToString();
                        tecnicoFiltrado.Especialidad = reader["Especialidad"].ToString();
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
            return tecnicoFiltrado;
        }

        public static int Agregar(string Nombre, string Especialidad)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Tecnicos (Nombre, Especialidad) VALUES (@Nombre, @Especialidad)", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Especialidad", Especialidad));

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
        public static int Modificar(int TecnicoID, string Nombre, string Especialidad)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Tecnicos SET Nombre = @Nombre, Especialidad = @Especialidad WHERE TecnicoID = @TecnicoID", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", TecnicoID));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Especialidad", Especialidad));

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
        public static int Borrar(int TecnicoID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Tecnicos WHERE TecnicoID = @TecnicoID", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", TecnicoID));


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