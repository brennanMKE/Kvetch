<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Kvetch Reborn" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" Runat="Server">
<!-- Inspired by the original Kvetch.com by Derek Powazek -->
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="content" Runat="Server">

    <kvetch:CommentRotator ID="cr1" runat="server" />
    <kvetch:TopicSelector ID="ts1" runat="server" />
    <kvetch:CommentEntry ID="ce1" runat="server" />

</asp:Content>

