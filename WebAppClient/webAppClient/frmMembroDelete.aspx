﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true" CodeBehind="frmMembroDelete.aspx.cs" Inherits="WebAppClient.frmMembroDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="LTurma" runat="server" Text="TODOS OS DADOS DESTE MEMBRO SERÃO EXLCUÍDOS!" Font-Bold="True"></asp:Label>
        <br />
        <br />
        <asp:Button ID="BConfirmar" runat="server" OnClick="BConfirmar_Click" Text="Confirmar" Width="100px" />
&nbsp;<asp:Button ID="BCancelar" runat="server" OnClick="BCancelar_Click" Text="Voltar" Width="100px" />
        <br />
        <br />
        <asp:Label ID="LResult" runat="server" Text="Resultado: "></asp:Label>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="AfterBody" runat="server">
</asp:Content>
