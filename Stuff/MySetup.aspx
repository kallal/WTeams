<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MySetup.aspx.vb" Inherits="WTeams.MySetup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .myshadow
        {box-shadow: 8px 8px 8px 0px gray}
        .myshadow {border-radius:10px}
        .myshadow:hover {background-color:#789eca;transition: 0.5s;}
    </style>


</head>
<body>
    <form id="form1" runat="server">

        <asp:Button ID="Button1" runat="server" Text="Process SQL" CssClass="btn myshadow" />
        <br />

        <asp:Button ID="cmdShowCon" runat="server" Text="Show connection"
            OnClick="cmdShowCon_Click"
            />
        <br />
        <br />
                <div>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" 
                style="min-width:none;width:90%;height:400px"
                
                ></asp:TextBox>
        </div>


    </form>
</body>
</html>
