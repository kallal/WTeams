<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MyEdit.ascx.vb" Inherits="WTeams.MyEdit" %>
<%@ Register Src="~/Conrols/MyDialog.ascx" TagPrefix="uc1" TagName="MyDialog" %>

<style>
           .MyHideBtn {display:none}

</style>
<div id="ucDiv" runat="server" >

    <div id="myrender" runat="server" style="width: 300px; height: 150px; border: solid 2px; display:none">
        Title
        <br />
        <hr style="background-color: black; height: 1px" />
        Body
        <br />
        <hr style="background-color: black; height: 1px" />
        <div style="float: right">
            <h3>Editor</h3>
        </div>
    </div>

</div>

<asp:Button ID="cmdSave" runat="server" Text="Save" style="display:none"
    OnClick="cmdSave_Click"
    />

<asp:Button ID="cmdDelete" runat="server" Text="Delete" style="display:none"
    OnClick="cmdDelete_Click"
    />



    <script>
        HideCancel2 = "0"

        function <%= Me.ID %>ucPOP(Sbtn) {
            var btn = $('#' + Sbtn)
            var ucCtrlID = '<%= Me.ID  %>'
            var ucTitle = '<%= Me.Title  %>'
            var ucPos = <%= Me.Position %>
            var ucPopDiv = '<%= Me.PopDiv %>' 
            var ucWidth = '<%= Me.Width %>' 
            var ucDismissable = <%= Me.Dissmissable %>
            var ucSaveBut = '#' + '<%= cmdSave.ClientID %>'
            var ucDeleteBut = '#' + '<%= cmdDelete.ClientID %>'

            var ucHide = '<%= Me.HideCancel %>'
            var ucCancelClass = ""


            if (HideCancel2 == "1") {
                ucCancelClass = "MyHideBtn"
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

            var sDialog = "#" + ucPopDiv
            var myDialog = $(sDialog)
            // var dismiss = false
            myDialog.dialog({
                autoOpen: false,
                modal: true,
                position: dpos,
                closeText: "",
                title: ucTitle,
                width: ucWidth,
                dialogClass: "dialogWithDropShadow",
                buttons: [
                    {
                        id: ucCtrlID + "_MyOkBtn",
                        text: "Save",
                        click: function () {
                            myDialog.dialog('close')
                            $(ucSaveBut).click()
                        }
                    },
                    {
                        id: ucCtrlID + "_MyCancel",
                        text: "Cancel",
                        class: ucCancelClass,
                        click: function () {
                            myDialog.dialog('close')
                        }
                    },
                    {
                        id: ucCtrlID + "_MyDelete",
                        text: "Delete",
                        click: function () {
                            mydeletec(this,myDialog)

                        }
                    }
                ],
                create: function (e, ui) {

                    var atobb = $('<span>&nbsp;</span><i class="fa fa-floppy-o fa-lg"></i><span>&nbsp;</span>')
                    atobb.prependTo("#" + ucCtrlID + "_MyOkBtn")

                    var atobb = $('<span>&nbsp;</span><i class="fa fa-arrow-circle-left fa-lg"></i><span>&nbsp;</span>')
                    atobb.prependTo("#" + ucCtrlID + "_MyCancel")
                    $("#" + ucCtrlID + "_MyCancel").attr("style","margin-left:15px")

                    var atobb = $('<span>&nbsp;</span><i class="fa fa-trash-o fa-lg"></i><span>&nbsp;</span>')
                    atobb.prependTo("#" + ucCtrlID + "_MyDelete")
                    $("#" + ucCtrlID + "_MyDelete").attr("style","margin-left:15px")

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
            var tExisting = tBar.html();
            tBar.html("")
            var tHTML = "<div style=\"float:left;margin-right:10px;\"><img src='/Content/teams2s.png' id='myNewImage' width='32px' /></div>"
            tBar.prepend(tHTML);
            tBar.append("<div style=\"float:left;margin-top:8px;color:#253B82;font-weight:bold\">" + tExisting + "</div>")

            myDialog.dialog('open')

        }

        function mydeletec(btn, parentDialog) {
            var myDialog = $('#mydeldiv')
            
            myDialog.dialog({
                autoOpen: false,
                modal: true,
                position:{ my: "left top ", at: "bottom right", of: btn },
                closeText: "",
                title: "Delete Record",
                width: "300px",
                dialogClass: "dialogWithDropShadow",
                buttons: [
                    {
                        id: "MyDelButton",
                        text: "Delete",
                        click: function () {
                            myDialog.dialog('close')
                            parentDialog.dialog('close')
                            var btnDel = $("#<%= cmdDelete.ClientID %>")
                            btnDel.click()
                        }
                    },
                    {
                        id: "MyCancel",
                        text: "Cancel",
                        click: function () {
                            myDialog.dialog('close')
                        }
                    }
                ],
                create: function (e, ui) {

                    var atobb = $('<span>&nbsp;</span><i class="fa fa-trash-o fa-lg"></i><span>&nbsp;</span>')
                    atobb.prependTo("#MyDelButton")

                    $("#MyCancel").attr("style", "margin-left:15px")

                },
                close: function () {
                    myDialog.dialog('destroy');
                },
                open: function (e, ui) {
                    var vTitle2 = $(this).parent().find('.ui-dialog-titlebar')
                    var tExisting = vTitle2.html();
                    vTitle2.html("")
                    var tHTML = "<div style=\"float:left;margin-right:10px;\"><img src='/Content/teams2s.png' id='myNewImage' width='32px' /></div>"
                    vTitle2.prepend(tHTML);
                    var tHTML2 = "<div style=\"float:left;margin-top:8px;color:#253B82;font-weight:bold\">Delete Record</div>"

                    vTitle2.html(tHTML).append(tHTML2)
                //    var tHTML = "<div style=\"float:left;margin-right:10px;\"><img src='/Content/teams2s.png' id='myNewImage' width='32px' /></div>Delete Record"
                //    var tHTML2 = "<div style=\"float:left;margin-top:8px;color:#253B82;font-weight:bold\">Delete Record</div>"

                //    vTitle2.html(tHTML)
                }
            })

            myDialog.dialog('open')

        }

    </script>
      <div id="mydeldiv" style="display:none">
          <h1 style="font-size:medium"><i>Delete this Record</i></h1>
          <h1 style="font-size:small"><i>(This can't be un-done)</i></h1>


      </div>

