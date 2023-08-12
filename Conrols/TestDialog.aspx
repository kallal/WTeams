<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TestDialog.aspx.vb" Inherits="WTeams.TestDialog" %>

<%@ Register Src="~/Conrols/MyDialog.ascx" TagPrefix="uc1" TagName="MyDialog" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="Button1" runat="server" Text="Button"
        CssClass="btn myshadow"
        
        />

    <uc1:MyDialog runat="server" id="MyDialog" 
        body="This is the body text<br/>Next line of body" 
        ButtonID="MainContent_Button1" 
        Position="Center" 
        Title="My Title"
        IconOk="ThumbsUp"
        />

    <br />
    <br />
    <br />
    <br />
    <br />

    <asp:Button ID="Button2" runat="server" Text="Button" />

</asp:Content>
