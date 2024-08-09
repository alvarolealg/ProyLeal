<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="Proyecto1.DetalleArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    

    <div class="container">
        <div class="header">
            <h1>
                <asp:Label Text="" ID="lblNombre" runat="server" />
            </h1>
            <asp:Image ID="imgFoto" runat="server" />
        </div>
        <div>
            <p>
                <asp:Label Text="" ID="lblDescripcion" runat="server" />
            </p>
        </div>
    </div>
</asp:Content>
