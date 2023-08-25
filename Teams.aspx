<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Teams.aspx.vb" Inherits="WTeams.Teams" %>

<%@ Register Src="~/Conrols/MyEdit.ascx" TagPrefix="uc1" TagName="MyEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding:30px">


        <div style="margin-left:auto;margin-right:auto;max-width:80vh">
        <div style="float:left">
            <img src="Content/teams2s.png" />
        </div>
        <div style="float:left;margin-top:5px">
            <h1 id="myhead1" runat="server">Teams</h1>
        </div>
        <div style="clear:both"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

        <asp:GridView ID="GVTeams" 
            runat="server" AutoGenerateColumns="False" 
            CssClass="table table-hover"
            DataKeyNames="ID" 
            >
            <Columns>
                <asp:BoundField DataField="Team" HeaderText="Team" ItemStyle-Width="12em"  />
                <asp:BoundField DataField="Division" HeaderText="Division"  />
                <asp:BoundField DataField="Notes" HeaderText="Notes" ItemStyle-Width="18em"  />

                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="8em" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdEdit"
                            ToolTip="Edit team and rates"
                            runat="server"
                            OnClick="cmdEdit_Click"
                            CssClass="btn myshadow"            
                            >&nbsp;<span class="fa fa-pencil-square-o fa-lg"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Score Cards" ItemStyle-Width="12em" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdShowCards"
                            ToolTip="View Score Cards for this Team"
                            runat="server"
                            OnClick="cmdShowCards_Click"
                            CssClass="btn myshadow"            
                            >&nbsp;<span class="fa fa-id-card-o fa-lg"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>

        </asp:GridView>

        <asp:LinkButton ID="cmdNew"
            ToolTip="Add new team"
            runat="server"
            OnClick="cmdNew_Click"
            CssClass="btn myshadow"><span class="fa fa-plus-square fa-lg"></span>&nbsp;&nbsp;Add Team
        </asp:LinkButton>


        <br />
        <br />


        <div id="myeditarea" runat="server"  style="display:none" clientidmode="static">
            <div style="float:left">
                <div style="float:left">
                    <h5>Team</h5>
                    <asp:TextBox ID="txtTeam" runat="server" f="Team" />
                </div>
                <div style="float:left;margin-left:20px">
                    <h5>Division</h5>
                    <asp:ListBox ID="lstDivision" runat="server" f="Division_ID" Width="120px" 
                        DataValueField="ID"
                        DataTextField="Division"
                        >
                    </asp:ListBox>
                </div>
                <div style="clear:both">
                    <h5>Notes</h5>
                    <asp:TextBox ID="txtNotes" runat="server" 
                        TextMode="MultiLine" Height="66px" Width="281px"
                        f="Notes" />            
                </div>
            </div>
            <div style="float:left;margin-left:35px">
                        <asp:GridView ID="GVA" runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames="ID"
                            CssClass="table table-striped-columns">
                            <Columns>
                                <asp:BoundField DataField="RateType" HeaderText="" />
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox ID="Price" runat="server" TextMode="Number" Width="120px"  style="text-align: right"
                                                Text='<%# Eval("Rate") %>'></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

            </div>
        </div>

        <uc1:MyEdit runat="server" ID="MyEdit" 
            PopDiv="myeditarea" 
            Position="Center"
            Width="42em"
            Title="Edit Team"
            OnMySaveTrigger="MyEdit_MySaveTrigger"
            OnMyDeleteTrigger="MyEdit_MyDeleteTrigger"
            />


            </ContentTemplate>
        </asp:UpdatePanel>


        </div>
 


    </div>
</asp:Content>
