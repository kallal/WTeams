<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TeamChoose.aspx.vb" Inherits="WTeams.TeamChoose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>
            input[type=checkbox] {
                accent-color: green;
            }

            th {
                text-align: center
            }

            td {
                text-align: center
            }
        </style>

    <div style="padding:30px">




        <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="float:left">
            <ContentTemplate>
                <div style="float:left;margin-right:20px">
                    <asp:LinkButton ID="cmdBack"
                        ToolTip="Back to Teams"
                        runat="server"
                        OnClick="cmdBack_Click"
                        CssClass="btn myshadow">&nbsp;<span class="fa fa-reply-all fa-lg">&nbsp;Back to Cards</span>
                    </asp:LinkButton>

                </div>



                <div id="InfoArea" runat="server" style="float: left">


                    <div style="float: left; text-align: center; width: 50%">
                        <asp:Label ID="lblTeamA" runat="server" f="TeamAT" Font-Size="X-Large"></asp:Label>
                    </div>

                    <div style="float: left; width: 40%; text-align: center">
                        <asp:Label ID="lblTeamB" runat="server" f="TeamBT" Font-Size="X-Large"></asp:Label>
                    </div>

                    <div style="clear: both"></div>

                    <div style="float: left">

                        <asp:ListView ID="LstSel" runat="server">

                            <ItemTemplate>
                                <tr style="">
                                    <td><%# Eval("ID") %></td>
                                    <td>
                                        <asp:CheckBox ID="s1" runat="server" Checked='<%# Bind("A") %>'
                                            AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s2" runat="server" Checked='<%# Bind("B") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s3" runat="server" Checked='<%# Bind("C") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s4" runat="server" Checked='<%# Bind("D") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s5" runat="server" Checked='<%# Bind("E") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s6" runat="server" Checked='<%# Bind("F") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s7" runat="server" Checked='<%# Bind("G") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s8" runat="server" Checked='<%# Bind("H") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s9" runat="server" Checked='<%# Bind("K") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s10" runat="server" Checked='<%# Bind("L") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:CheckBox ID="s11" runat="server" Checked='<%# Bind("aa") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s12" runat="server" Checked='<%# Bind("bb") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s13" runat="server" Checked='<%# Bind("cc") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s14" runat="server" Checked='<%# Bind("dd") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s15" runat="server" Checked='<%# Bind("ee") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s16" runat="server" Checked='<%# Bind("ff") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s17" runat="server" Checked='<%# Bind("gg") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s18" runat="server" Checked='<%# Bind("hh") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s19" runat="server" Checked='<%# Bind("kk") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                    <td>
                                        <asp:CheckBox ID="s20" runat="server" Checked='<%# Bind("ll") %>' AutoPostBack="true" OnCheckedChanged="s1_CheckedChanged" /></td>
                                </tr>
                            </ItemTemplate>

                            <LayoutTemplate>
                                <table id="itemPlaceholderContainer" runat="server" border="0" style=""
                                    class="table table-bordered table-striped-columns table-sm">
                                    <tr runat="server" style="">
                                        <th runat="server"></th>
                                        <th runat="server">A</th>
                                        <th runat="server">B</th>
                                        <th runat="server">C</th>
                                        <th runat="server">D</th>
                                        <th runat="server">E</th>
                                        <th runat="server">F</th>
                                        <th runat="server">G</th>
                                        <th runat="server">H</th>
                                        <th runat="server">K</th>
                                        <th runat="server">L</th>
                                        <th runat="server"></th>
                                        <th runat="server">a</th>
                                        <th runat="server">b</th>
                                        <th runat="server">c</th>
                                        <th runat="server">d</th>
                                        <th runat="server">e</th>
                                        <th runat="server">f</th>
                                        <th runat="server">g</th>
                                        <th runat="server">h</th>
                                        <th runat="server">k</th>
                                        <th runat="server">l</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>

                        </asp:ListView>

                    </div>
                </div>

                <div style="float: left; margin-left: 35px;">

                    <div style="float: left">
                        <h3 id="GAHead" runat="server"></h3>

                        <asp:GridView ID="GVA" runat="server"
                            AutoGenerateColumns="False" ShowFooter="true"
                            DataKeyNames="ID"
                            CssClass="table table-striped-columns">
                            <Columns>
                                <asp:BoundField DataField="RateType" HeaderText="" />
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <div style="text-align: right">
                                            <asp:Label ID="Price" runat="server"
                                                Text='<%# Eval("Rate", "{0:c2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="SelCount" runat="server"
                                            Text='<%# Eval("SelCount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <div style="text-align: right">
                                            <asp:Label ID="SelTotal" runat="server"
                                                Text='<%# Eval("SelTotal", "{0:c2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right">
                                            <asp:Label ID="SelTotalSum" runat="server"></asp:Label>
                                        </div>
                                    </FooterTemplate>

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div style="float: left; margin-left: 20px">
                        <h3 id="GBHead" runat="server"></h3>

                        <asp:GridView ID="GVB" runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames="ID"
                            CssClass="table table-striped-columns" ShowFooter="True">
                            <Columns>
                                <asp:BoundField DataField="RateType" HeaderText="" />
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <div style="text-align: right">
                                            <asp:Label ID="Price" runat="server"
                                                Text='<%# Eval("Rate", "{0:c2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="SelCount" runat="server"
                                            Text='<%# Eval("SelCount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <div style="text-align: right">
                                            <asp:Label ID="SelTotal" runat="server"
                                                Text='<%# Eval("SelTotal", "{0:c2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right">
                                            <asp:Label ID="SelTotalSum" runat="server"></asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>

                    <div style="clear: both"></div>

                    <h4>Total Amount</h4>
                    <asp:Label ID="lblTotal" runat="server" Text="Label">
                    </asp:Label>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="InfoArea2" runat="server" style="float:left;margin-left:35px">

            <div style="float:left">
                <h3>Event Date</h3>
                <asp:Label ID="lblEdate" runat="server" Font-Size="Larger">

                </asp:Label>
            </div>
            <div style="float:left;margin-left:35px">
                <h3>Event Info</h3>
                <asp:Label ID="lblInfo" runat="server" Font-Size="Larger" Width="350px"></asp:Label>
            </div>

        </div>


    </div>


</asp:Content>
