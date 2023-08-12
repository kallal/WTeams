<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TestMyEdit.aspx.vb" Inherits="WTeams.TestMyEdit" %>

<%@ Register Src="~/Conrols/MyEdit.ascx" TagPrefix="uc1" TagName="MyEdit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <asp:Button ID="Button1" runat="server" Text="Button"
        CssClass="btn myshadow"
        OnClick="Button1_Click" ClientIDMode="Static"
        
        />
    <input id="cTest" type="button" value="test call button"
        onclick="MyEditucPOP('Button1');return false;"
        />


    <asp:Button ID="Button2" runat="server" Text="get div test" />


    <div id="testedit" runat="server" style="display:none" clientidmode="static" >

        <h3>This is edit area</h3>
        <asp:TextBox ID="txtTeam" runat="server" f="Team" ></asp:TextBox>
    </div>

    <uc1:MyEdit runat="server" id="MyEdit" MyEditArea="testedit" 
        PopDiv="testedit" 
        Position="Right" 
        Title="Edit Team"
        Width="600px"
        OnMySaveTrigger="MyEdit_MySaveTrigger"
        />

</asp:Content>
