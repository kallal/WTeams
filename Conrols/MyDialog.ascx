<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MyDialog.ascx.vb" Inherits="WTeams.MyDialog" %>


<div id="ucDiv" runat="server" >

    <div id="myrender" runat="server" style="width: 300px; height: 150px; border: solid 2px; display:none">
        Title
        <br />
        <hr style="background-color: black; height: 1px" />
        Body
        <br />
        <hr style="background-color: black; height: 1px" />
        <div style="float: right">

            <asp:Button ID="ButtonDummy1" runat="server" Text="Ok" CssClass="mybt2" />
            <asp:Button ID="ButtonDummy2" runat="server" Text="Cancel" CssClass="mybt2" />

        </div>
    </div>

    <div id="DivArea" runat="server" style="display:none">
</div>

    <script>


        $(document).ready(function () {
            var ucButtonID = '<%= Me.ButtonID %>'
            $("[id^='" + ucButtonID + "']").click(function () {
                return <%= Me.ID %>ucPOP(this)
            });
        });

        var ucMyPopDialogOK = false
        function <%= Me.ID %>ucPOP(btn) {

            var ucCtrlID = '<%= Me.ID  %>'
            var ucTitle = '<%= Me.Title  %>'
            var ucBody = '<%= Me.Body %>'
            var ucPos = <%= Me.Position %>
            var ucPopDiv = '<%= Me.PopDiv %>' 
            var ucWidth = '<%= Me.Width %>' 
            var ucBokText = '<%= Me.BOkText %>'
            var ucOkIcon = '<%= Me.IconOkS %>'
            var ucCancelText = "<%= Me.BCancelText %>"
            var ucCancelIcon = '<%= Me.IconCancelS %>'
            var ucDismissable = <%= Me.Dissmissable %>

            if (ucMyPopDialogOK) {
                ucMyPopDialogOK = false
                    return true
            }

            var dPos = ""
            if (ucPos == 3) {
                dpos = { my: "right top ", at: "bottom right", of: btn }
            }
            else if (ucPos == 2) {
                dpos = { my: "left top ", at: "bottom right", of: btn }
            }
            else if (ucPos == 1) {
                dpos = { my: "center", at: "center", of: window }
            }

            var sDialog = "#<%= DivArea.ClientID %>"
            if (ucPopDiv != "") {
                sDialog = "#" + ucPopDiv
            }
            var myDialog = $(sDialog)

            if (ucPopDiv == "") {
                myDialog.html(ucBody)
            }

            var ucOkCss = ""
            if (ucBokText == "") {
                ucOkCss = "MyHideBtn"
            }
            var ucCancelCss = ""
            if (ucCancelText == "") {
                ucCancelCss = "MyHideBtn"
            }
            // var dismiss = false
            myDialog.dialog({
                autoOpen: false,
                modal: <%= Me.Modal %>,
                appendTo: "form",
                position: dpos,
                closeText: "",
                title: ucTitle,
                width: ucWidth,
                appendTo:"form",
                dialogClass: "dialogWithDropShadow",
                buttons: [
                    {
                        id: ucCtrlID + "_MyOkBtn",
                        text: ucBokText,
                        class: ucOkCss,
                        click: function () {
                            myDialog.dialog('close')
                            ucMyPopDialogOK = true
                            btn.click()
                        }
                    },
                    {
                        id: ucCtrlID + "_MyCancel",
                        text: ucCancelText,
                        class: ucCancelCss,
                        click: function () {
                            myDialog.dialog('close')
                        }
                    }
                ],
                create: function (e, ui) {

                    if (ucOkIcon != "") {
                        var atobb = $('<span>&nbsp;</span><i class="fa ' + ucOkIcon + '"></i>')
                        atobb.prependTo("#" + ucCtrlID + "_MyOkBtn");
                    }
                    

                    if (ucCancelIcon != "") {
                        var atobb = $('<span>&nbsp;</span><i class="fa ' + ucCancelIcon + '"></i>')
                        atobb.prependTo("#" + ucCtrlID + "_MyCancel");
                    }
                },
                close: function () {
                    myDialog.dialog('destroy');
                },
                open: function (event, ui) {
                    if (ucDismissable == true) {
                        $('.ui-widget-overlay').bind('click', function () {
                            myDialog.dialog('close');
                        }); 
                    }
                }

            })



            var tBar = $(".ui-dialog-title")
            if (tBar.length > 1) {

            }
            else {
                var tExisting = tBar.html();
                tBar.html("")
                var tHTML = "<div style=\"float:left;margin-right:10px;\"><img src='<%= ResolveURL("~/Content/teams2s.png") %>' id='myNewImage' width='32px' /></div>"
                tBar.prepend(tHTML);
                tBar.append("<div style=\"float:left;margin-top:8px;color:#253B82;font-weight:bold\">" + tExisting + "</div>")

            }

            myDialog.dialog('open')


            //var mydg2 = $(".dialogWithDropShadow")
            
            //var bcancel = mydg2.find("#MyCancel")
            //bcancel.removeAttr('style')
            //bcancel.removeAttr('class')
            //bcancel.addClass('burkebuttdg')

            //bokbut = mydg2.find("#MyOkBtn")
            //bokbut.removeAttr('style')
            //bokbut.removeAttr('class')
            //bokbut.addClass('burkebuttdg')
            //bokbut.css('margin-right', '20px')
            return false
        }

    </script>
    </div>

