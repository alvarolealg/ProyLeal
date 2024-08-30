<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="Proyecto1.ListadoProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="text-center">Listado de Productos</h1>

    <div class="d-flex justify-content-center">

        <div class="row w-50">
            <div class="col-8">
                <div class="mb-3">
                    <asp:Label ID="Filtrar" runat="server" Text="Filtrar"></asp:Label>
                    <asp:TextBox ID="txtFiltro" CssClass="form-control m1-2" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-4" style="display: flex; flex-direction: column; justify-content: flex-end; align-items: start;">
                <div class="mb-3">
                    <asp:Button Text="Limpiar Filtro" CssClass="btn btn-primary m1-2" ID="btnLimpiarFiltro" OnClick="btnLimpiarFiltro_Click" runat="server" />
                    <%-- <asp:CheckBox Text="Filtro Avanzado" CssClass="" ID="chkAvanzado"
                    AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" runat="server" />--%>
                </div>
            </div>
            <%-- <%if (chkAvanzado.Checked)
            {%>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                        <asp:ListItem Text="Producto" />
                        <asp:ListItem Text="Categoria" />
                        <asp:ListItem Text="Marca" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Criterio" runat="server" />
                    <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Filtro" runat="server" />
                    <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Estado" runat="server" />
                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                        <asp:ListItem Text="Todos" />
                        <asp:ListItem Text="Activo" />
                        <asp:ListItem Text="Inactivo" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Button Text="Buscar" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" />
                </div>
            </div>
        </div>
        <%  }%>--%>
        </div>
    </div>

    <div class="container">

        <asp:GridView runat="server" ID="dgvProductos" DataKeyNames="id" CssClass="table" AutoGenerateColumns="false"
            OnSelectedIndexChanged="dgvProductos_SelectedIndexChanged"
            OnPageIndexChanging="dgvProductos_PageIndexChanging"
            AllowPaging="true" PageSize="5">
            <Columns>
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Código" DataField="Codigo" />
                <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                <asp:BoundField HeaderText="Tipo" DataField="Tipo.descripcion" />
                <asp:BoundField HeaderText="Unidades" DataField="unidades" />
                <asp:BoundField HeaderText="Precio" DataField="precio" />
                <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
                <asp:CommandField HeaderText="Editar" ShowSelectButton="true" SelectText="✍" />
                <asp:CommandField HeaderText="Borrar" ShowSelectButton="true" SelectText="🗑️​" />
               <%-- <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button Text="✍" CommandName="Desactivar" CommandArgument='<%#Eval("Id") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button Text="🗑️" CommandName="Desactivar" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm('Seguro que deseas borrar este articulo?');" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <a href="FormularioArticulo.aspx" class="btn btn-primary">Agregar</a>
    </div>
</asp:Content>
