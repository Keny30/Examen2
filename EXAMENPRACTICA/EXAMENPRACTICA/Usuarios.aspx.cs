using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using EXAMENPRACTICA.Clases;

namespace EXAMENPRACTICA
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                LlenarGrid();
                Llenartipos();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            string UsuarioID = datagrid.Rows[rowIndex].Cells[1].Text;
            string Nombre =  datagrid.Rows[rowIndex].Cells[2].Text;
            string CorreoElectronico = datagrid.Rows[rowIndex].Cells[3].Text;
            string Telefono = datagrid.Rows[rowIndex].Cells[4].Text;

            hide_UsuarioID.Text = UsuarioID;
            textbox_Nombre.Text = Nombre;
            textbox_CorreoElectronico.Text = CorreoElectronico;
            textbox_Telefono.Text = Telefono;

        }

        public void alertas(String texto)
        {
            string message = texto;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }
        protected void LlenarGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM Usuarios"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            datagrid.DataSource = dt;
                            datagrid.DataBind();  // actualizar el grid view
                        }
                    }
                }
            }
        }


        protected void Llenartipos()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT UsuarioID, Nombre, CorreoElectronico, Telefono from Usuarios"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            DropDownList1.DataSource = dt;

                            DropDownList1.DataTextField = dt.Columns["Nombre"].ToString();
                            DropDownList1.DataValueField = dt.Columns["UsuarioID"].ToString();
                            DropDownList1.DataBind();
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            int UsuarioID = 0;
            Usuario usuarioFiltrado = new Usuario();
            if (hide_UsuarioID.Text !="") 
            {
                UsuarioID = int.Parse(hide_UsuarioID.Text);
                usuarioFiltrado = Clases.Usuario.Consultar(UsuarioID);

                if (usuarioFiltrado.UsuarioID > 0)
                {
                    alertas("El usuario ya existe en la base de datos");
                    return;
                }
            }

            if (CamposTextoValidos())
            {
                int resultado = Clases.Usuario.Agregar(textbox_Nombre.Text, textbox_CorreoElectronico.Text, textbox_Telefono.Text);

                if (resultado > 0)
                {
                    alertas("Usuario ha sido ingresado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar el Usuario, por favor verifique los datos");

                }
            }
            else {
                alertas("Verifique que los datos ingresados esten correctos");
            }

          
        }
        protected void BorrarUsuario(object sender, EventArgs e)
        {
            if (hide_UsuarioID.Text != "")
            {
                int UsuarioID = int.Parse(hide_UsuarioID.Text);

                int resultado = Clases.Usuario.Borrar(UsuarioID);

                if (resultado > 0)
                {
                    alertas("Usuario ha sido borrado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar el usuario");

                }
            }
           else { alertas("Debe seleccionar un usuario"); }
        }
        protected void RefrescarUsuario(object sender, EventArgs e)
        {
            InicializarCamposTexto();
            LlenarGrid();
        }   
        protected void ModificarUsuario(object sender, EventArgs e)
        {
            if (hide_UsuarioID.Text != "")
            {
                int UsuarioID = int.Parse(hide_UsuarioID.Text);
                int resultado = Clases.Usuario.Modificar(UsuarioID, textbox_Nombre.Text, textbox_CorreoElectronico.Text, textbox_Telefono.Text);

                if (resultado > 0)
                {
                    alertas("Usuario ha sido modificado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar tipo");

                }
            }
            else { alertas("Debe seleccionar un usuario"); }
        }
        protected void ConsultarUsuario(object sender, EventArgs e)
        {
            if (hide_UsuarioID.Text != "") {
                int UsuarioID = int.Parse(hide_UsuarioID.Text);
                Usuario usuarioFiltrado = Clases.Usuario.Consultar(UsuarioID);

                if (usuarioFiltrado.UsuarioID > 0)
                {
                    List<Usuario> usuarios = new List<Usuario>();
                    usuarios.Add(usuarioFiltrado);
                    datagrid.DataSource = usuarios;
                    datagrid.DataBind();  // actualizar el grid view
                }
                else
                {
                    alertas("Error al consultar el usuario");

                }
            }
            else { alertas("Debe seleccionar un usuario"); }

        }

        private void InicializarCamposTexto() {
            textbox_Nombre.Text = string.Empty;
            textbox_CorreoElectronico.Text = string.Empty;
            textbox_Telefono.Text = string.Empty;
            hide_UsuarioID.Text = string.Empty;
        }
        private bool CamposTextoValidos() {
            bool estado = true;
            if (textbox_Nombre.Text == string.Empty
                || textbox_CorreoElectronico.Text == string.Empty
                || textbox_Telefono.Text == string.Empty)
            {
                estado = false; 
            }
            
            return estado;
        }
        
    }
}