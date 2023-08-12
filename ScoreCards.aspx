<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ScoreCards.aspx.vb" Inherits="WTeams.ScoreCards" %>

<%@ Register Src="~/Conrols/MyEdit.ascx" TagPrefix="uc1" TagName="MyEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin-left:20%; margin-right:20%">

        <div style="float:left">
            <img src="Content/teams2s.png" />
        </div>
        <div style="float:left;margin-top:5px">
            <h1 id="myhead" runat="server">Score Cards</h1>
        </div>
        <div style="clear:both"></div>


        <asp:GridView ID="GVCards" runat="server"
              AutoGenerateColumns="False" 
            CssClass="table table-hover"
            DataKeyNames="ID" width="100%"            
            >
            <Columns>
                <asp:BoundField DataField="TeamAT" HeaderText="Team A"           />
                <asp:BoundField DataField="DivisionA" HeaderText="Division A"    />
                <asp:BoundField DataField="TeamBT" HeaderText="Team B"           />
                <asp:BoundField DataField="DivisionB" HeaderText="Division B"    />
                <asp:BoundField DataField="EventDate" HeaderText="Event Date" DataFormatString="{0:d}"   />
                <asp:TemplateField HeaderText="Event Info" ItemStyle-Width="220px">
                    <ItemTemplate>
                        <asp:TextBox ID="lblInfo" runat="server"  TextMode="MultiLine" Rows="3" Width="210px"
                            Text='<%# Eval("EventInfo") %>'
                            style="border:none;background-color:transparent"
                            ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Edit"  ItemStyle-HorizontalAlign="Left" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdView"
                            ToolTip="Edit Score Card Information"
                            runat="server"
                            OnClick="cmdView_Click"
                            CssClass="btn myshadow"            
                            >&nbsp;<span class="fa fa-pencil-square-o fa-lg"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Score Card" HeaderStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdShowCard"
                            ToolTip="Pick card choices"
                            runat="server"
                            OnClick="cmdShowCard_Click"
                            CssClass="btn myshadow"            
                            >&nbsp;<span class="fa fa-id-card-o fa-lg"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <asp:LinkButton ID="cmdNew"
            ToolTip="Add new Score Card"
            runat="server"
            OnClick="cmdNew_Click"
            CssClass="btn myshadow"><span class="fa fa-plus-square fa-lg"></span>&nbsp;&nbsp;Add Score Card
        </asp:LinkButton>

        <div id="myeditarea" runat="server" style="display: none" clientidmode="static">
            <br />
            <div>
                <h5 style="float:left">Event Date</h5>
                <asp:TextBox ID="txtEventDate" runat="server" style="margin-left:20px"
                    TextMode="Date"
                    f="EventDate"
                    ></asp:TextBox>
            </div>
            <div style="clear:both"></div>
            <br />
                <div style="float: left">
                <h5>Team A</h5>
                <asp:DropDownList ID="cboTeamA"
                    DataValueField="ID"
                    DataTextField="Team"
                    runat="server" Font-Size="Large" Width="180px"
                    f="TeamA"
                    >
                </asp:DropDownList>
            </div>

            <div style="float: left;margin-left:35px">
                <h5>Team B</h5>
                <asp:DropDownList ID="cboTeamB"
                    DataValueField="ID"
                    DataTextField="Team"
                    runat="server" Font-Size="Large" Width="180px"
                    f="TeamB"
                    >
                </asp:DropDownList>
            </div>
            <div style="clear:both"></div>

            <div>
                <br />
                <h5>Event Info</h5>
                <asp:TextBox ID="txtEventInfo" runat="server"
                    TextMode="MultiLine" style="width:500px"
                    Rows="5"                     Height="104px"
                    f="EventInfo"
                    ></asp:TextBox>
            </div>

        </div>

        <uc1:MyEdit runat="server" 
            ID="MyEdit" 
            PopDiv="myeditarea" 
            Title="Edit Score Card" 
            Width="30%" 
            Position="Center"
            OnMySaveTrigger="MyEdit_MySaveTrigger"
            OnMyDeleteTrigger="MyEdit_MyDeleteTrigger"
            />



    </div>




</asp:Content>
