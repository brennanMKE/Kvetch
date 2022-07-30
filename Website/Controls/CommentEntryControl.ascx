<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentEntryControl.ascx.cs" Inherits="Controls_CommentEntryControl" %>

<table class="EntryForm">
<tr>
<td class="Label">
<asp:Label ID="lblComment" runat="server" Text="Comment:"></asp:Label>
</td>
<td class="Comment">
<asp:TextBox ID="tbComment" runat="server" TextMode="MultiLine"></asp:TextBox>
</td>
</tr>
<tr>
<td class="Label">
<asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
</td>
<td class="Name">
<asp:TextBox ID="tbName" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="2" class="Submit">
<asp:Button ID="btnSubmit" runat="server" Text="Kvetch!" 
    OnClientClick="return false;" UseSubmitBehavior="False" />
</td>
</tr>

</table>

<div class="Status" style="display: none;">
</div>
