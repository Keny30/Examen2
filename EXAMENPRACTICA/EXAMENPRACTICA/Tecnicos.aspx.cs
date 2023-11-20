using EXAMENPRACTICA.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EXAMENPRACTICA
{
    public partial class Tecnicos : System.Web.UI.Page
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
            string TecnicoID = datagrid.Rows[rowIndex].Cells[1].Text;
            string Nombre = datagrid.Rows[rowIndex].Cells[2].Text;
            string Especialidad = datagrid.Rows[rowIndex].Cells[3].Text;

            hide_TecnicoID.Text = TecnicoID;
            textbox_Nombre.Text = Nombre;
            textbox_Especialidad.Text = Especialidad;

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
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM Tecnicos"))
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
                using (SqlCommand cmd = new SqlCommand("SELECT TecnicoID,Nombre,Especialidad from Tecnicos"))
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
                            DropDownList1.DataValueField = dt.Columns["TecnicoID"].ToString();
                            DropDownList1.DataBind();
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            int TecnicoID = 0;
            Tecnico tecnicoFiltrado = new Tecnico();
            if (hide_TecnicoID.Text != "")
            {
                TecnicoID = int.Parse(hide_TecnicoID.Text);
                tecnicoFiltrado = Clases.Tecnico.Consultar(TecnicoID);

                if (tecnicoFiltrado.TecnicoID > 0)
                {
                    alertas("El tecnico ya existe en la base de datos");
                    return;
                }
            }

            if (CamposTextoValidos())
            {
                int resultado = Clases.Tecnico.Agregar(textbox_Nombre.Text, textbox_Especialidad.Text);

                if (resultado > 0)
                {
                    alertas("Tecnico ha sido ingresado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar el Tecnico, por favor verifique los datos");

                }
            }
            else
            {
                alertas("Verifique que los datos ingresados esten correctos");
            }


        }
        protected void BorrarTecnico(object sender, EventArgs e)
        {
            if (hide_TecnicoID.Text != "")
            {
                int TecnicoID = int.Parse(hide_TecnicoID.Text);

                int resultado = Clases.Tecnico.Borrar(TecnicoID);

                if (resultado > 0)
                {
                    alertas("Tecnico ha sido borrado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar el Tecnico");

                }
            }
            else { alertas("Debe seleccionar un Tecnico"); }
        }
        protected void RefrescarTecnico(object sender, EventArgs e)
        {
            InicializarCamposTexto();
            LlenarGrid();
        }
        protected void ModificarTecnico(object sender, EventArgs e)
        {
            if (hide_TecnicoID.Text != "")
            {
                int TecnicoID = int.Parse(hide_TecnicoID.Text);
                int resultado = Clases.Tecnico.Modificar(TecnicoID, textbox_Nombre.Text, textbox_Especialidad.Text);

                if (resultado > 0)
                {
                    alertas("Tecnico ha sido modificado con exito");
                    InicializarCamposTexto();
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar tipo");

                }
            }
            else { alertas("Debe seleccionar un Tecnico"); }
        }
        protected void ConsultarTecnico(object sender, EventArgs e)
        {
            if (hide_TecnicoID.Text != "")
            {
                int TecnicoID = int.Parse(hide_TecnicoID.Text);
                Tecnico tecnicoFiltrado = Clases.Tecnico.Consultar(TecnicoID);

                if (tecnicoFiltrado.TecnicoID > 0)
                {
                    List<Tecnico> tecnicos = new List<Tecnico>();
                    tecnicos.Add(tecnicoFiltrado);
                    datagrid.DataSource = tecnicos;
                    datagrid.DataBind();  // actualizar el grid view
                }
                else
                {
                    alertas("Error al consultar el Tecnico");

                }
            }
            else { alertas("Debe seleccionar un Tecnico"); }

        }

        private void InicializarCamposTexto()
        {
            textbox_Nombre.Text = string.Empty;
            textbox_Especialidad.Text = string.Empty;
            hide_TecnicoID.Text = string.Empty;
        }
        private bool CamposTextoValidos()
        {
            bool estado = true;
            if (textbox_Nombre.Text == string.Empty
                || textbox_Especialidad.Text == string.Empty)
            {
                estado = false;
            }

            return estado;
        }

    }
}
