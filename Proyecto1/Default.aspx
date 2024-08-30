<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Proyecto1.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>HOLA</h1>
        <p>El mejor lugar para tus compras</p>

        <div class="d-flex mx-auto">

            <div class="row row-cols-1 row-cols-md-2 g-4">
                <asp:Repeater runat="server" ID="repRepetidor">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card">
                                <img src="<%#Eval("imagenUrl") %>" class="card-img-top" alt="...">
                                <div class="card-body">
                                    <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                    <p class="card-text"><%#Eval("Descripcion") %></p>
                                    <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>">Ver Más</a>
                                </div>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
