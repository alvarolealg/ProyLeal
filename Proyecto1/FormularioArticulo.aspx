<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="Proyecto1.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server" />

    <div class="container mt-5">
        <div class="row">
            <div class="col-6">
                <div class="mb-3">
                    <label for="txtId" class="form-label">ID</label>
                    <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre: </label>
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtCodigo" class="form-label">Codigo: </label>
                    <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="ddlTipo" class="form-label">Tipo: </label>
                    <asp:DropDownList ID="ddlTipo" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="mb-3 ">
                    <label for="ddlMarca" class="form-label">Marca</label>
                    <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="row mt-4 d-flex flex-column justify-content-between align-items-center" style="height: 100vh">
                    <div class="col-12 d-flex justify-content-center">

                        <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" runat="server" />
                        <a href="ListadoProductos.aspx">Cancelar</a>
                        <asp:Button Text="Inactivar" ID="btnInactivar" OnClick="btnInactivar_Click" CssClass="btn btn-warning" runat="server" />
                    </div>
                </div>
            </div>

            <div class="col-6">
                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label">Descripcion: </label>
                    <asp:TextBox runat="server" TextMode="SingleLine" ID="txtDescripcion" Height="80px" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtPrecio" class="form-label">Precio: </label>
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtUnidades" class="form-label">Unidades:</label>
                    <asp:TextBox runat="server" TextMode="Number" ID="txtUnidades" CssClass="form-control" />

                </div>
                <asp:UpdatePanel ID="updatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <label for="txtImagenUrl" class="form-label">Url Imagen: </label>
                            <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                        </div>
                        <asp:Image ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png" runat="server" ID="imgArticulo" Width="60%" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>

</asp:Content>
