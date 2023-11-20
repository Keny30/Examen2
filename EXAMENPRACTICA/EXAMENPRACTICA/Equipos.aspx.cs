using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EXAMENPRACTICA.Clases;
using System.Drawing;

namespace EXAMENPRACTICA
{
    public partial class Equipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
                Llenartipos();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            string EquipoID = datagrid.Rows[rowIndex].Cells[1].Text;
            string TipoEquipo = datagrid.Rows[rowIndex].Cells[2].Text;
            string Modelo = datagrid.Rows[rowIndex].Cells[3].Text;
            string UsuarioID = datagrid.Rows[rowIndex].Cells[4].Text;

            hide_EquipoID.Text = EquipoID;
            textbox_TipoEquipo.Text = TipoEquipo;
            textbox_Modelo.Text = Modelo;
            DropDownList1.SelectedValue = UsuarioID;

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
                using (SqlCommand cmd = new SqlCommand("SELECT e.EquipoID,e.TipoEquipo,e.Modelo,e.UsuarioID,u.Nombre  FROM Equipos e INNER JOIN Usuarios u ON e.UsuarioId = u.UsuarioID "))
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
                using (SqlCommand cmd = new SqlCommand("SELECT UsuarioID, Nombre from Usuarios"))
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

            int EquipoID = 0;
            Equipo equipoFiltrado = new Equipo();
            if (hide_EquipoID.Text != "")
            {
                EquipoID = int.Parse(hide_EquipoID.Text);
                equipoFiltrado = Clases.Equipo.Consultar(EquipoID);

                if (equipoFiltrado.EquipoID > 0)
                {
                    alertas("El Equipo ya existe en la base de datos");
                    return;
                }
            }

            if (CamposTextoValidos())
            {
                int UsuarioID = int.Parse(DropDownList1.SelectedItem.Value.ToString());
                int resultado = Clases.Equipo.Agregar(textbox_TipoEquipo.Text, textbox_Modelo.Text, UsuarioID);

                if (resultado > 0)
                {
                    alertas("Equipo ha sido ingresado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar el Equipo, por favor verifique los datos");

                }
            }
            else
            {
                alertas("Verifique que los datos ingresados esten correctos");
            }


        }
        protected void BorrarEquipo(object sender, EventArgs e)
        {
            if (hide_EquipoID.Text != "")
            {
                int EquipoID = int.Parse(hide_EquipoID.Text);

                int resultado = Clases.Equipo.Borrar(EquipoID);

                if (resultado > 0)
                {
                    alertas("Equipo ha sido borrado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar el Equipo");

                }
            }
            else { alertas("Debe seleccionar un Equipo"); }
        }
        protected void RefrescarEquipo(object sender, EventArgs e)
        {
            InicializarCamposTexto();
            LlenarGrid();
        }
        protected void ModificarEquipo(object sender, EventArgs e)
        {
            if (hide_EquipoID.Text != "")
            {
                int EquipoID = int.Parse(hide_EquipoID.Text);
                int UsuarioID = int.Parse(DropDownList1.SelectedItem.Value.ToString());
                int resultado = Clases.Equipo.Modificar(EquipoID, textbox_TipoEquipo.Text, textbox_Modelo.Text, UsuarioID);

                if (resultado > 0)
                {
                    alertas("Equipo ha sido modificado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar tipo");

                }
            }
            else { alertas("Debe seleccionar un Equipo"); }
        }
        protected void ConsultarEquipo(object sender, EventArgs e)
        {
            if (hide_EquipoID.Text != "")
            {
                int EquipoID = int.Parse(hide_EquipoID.Text);
                Equipo equipoFiltrado = Clases.Equipo.Consultar(EquipoID);

                if (equipoFiltrado.EquipoID > 0)
                {
                    List<Equipo> equipos = new List<Equipo>();
                    equipos.Add(equipoFiltrado);
                    datagrid.DataSource = equipos;
                    datagrid.DataBind();  // actualizar el grid view
                }
                else
                {
                    alertas("Error al consultar el Equipo");

                }
            }
            else { alertas("Debe seleccionar un Equipo"); }

        }

        private void InicializarCamposTexto()
        {
            textbox_TipoEquipo.Text = string.Empty;
            textbox_Modelo.Text = string.Empty;
            textbox_UsuarioID.Text = string.Empty;
            hide_EquipoID.Text = string.Empty;
            DropDownList1.SelectedValue = null;
        }
        private bool CamposTextoValidos()
        {
            bool estado = true;
            if (textbox_TipoEquipo.Text == string.Empty
                || textbox_Modelo.Text == string.Empty
                || DropDownList1.SelectedValue == string.Empty
                )
            {
                estado = false;
            }

            return estado;
        }

    }

}
