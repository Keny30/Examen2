<%@ Page Title="" Language="C#" MasterPageFile="~/menu.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="EXAMENPRACTICA.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center">
    <h1> Catalogo de Usuarios </h1>
    <p>&nbsp;</p>
</div>
<div>
    <br />
    <br />
    <asp:GridView runat="server" ID="datagrid" PageSize="10" HorizontalAlign="Center"
        CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
        RowStyle-CssClass="rows" AllowPaging="True" OnRowCommand="GridView1_RowCommand">
        <Columns>
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnAddRecord"  
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" 
                             runat="server" Text="Seleccionar" />
            </ItemTemplate>
        </asp:TemplateField> 
    </Columns>
        </asp:GridView>

    <br />
    <br />
    <br />

</div>
<div class="container text-center">
   <asp:TextBox runat="server" ID="hide_UsuarioID" style="display: none;"></asp:TextBox>
   <!-- UsuarioID: <asp:TextBox ID="textbox_UsuarioID" class="form-control" runat="server"></asp:TextBox> -->
    Nombre: <asp:TextBox ID="textbox_Nombre" class="form-control" runat="server"></asp:TextBox>
    Correo Electronico: <asp:TextBox ID="textbox_CorreoElectronico" class="form-control" runat="server"></asp:TextBox>
    Telefono: <asp:TextBox ID="textbox_Telefono" class="form-control" runat="server"></asp:TextBox>
   <!--  <div class="mb-3">
     <label for="exampleInputPassword1" class="form-label">Tipo de Articulo</label>
     <asp:DropDownList ID="DropDownList1" class="form-control" runat="server"></asp:DropDownList>
     </div> -->

</div>
    <br />
<div class="container text-center">

    <asp:Button ID="Button1" class="btn btn-outline-primary" runat="server" Text="Agregar" OnClick="Button1_Click" />
    <asp:Button ID="Button2" class="btn btn-outline-secondary" runat="server" Text="Borrar" OnClick="BorrarUsuario"/>
    <asp:Button ID="Button3" runat="server" class="btn btn-outline-danger" Text="Modificar"  OnClick="ModificarUsuario"/>
    <asp:Button ID="Bconsulta" runat="server" class="btn btn-outline-danger" Text="Consultar" OnClick="ConsultarUsuario" />
    <asp:Button ID="Brefresh" runat="server" class="btn btn-outline-warning" Text="Refresh" OnClick="RefrescarUsuario" />
    

</div>
</asp:Content>
