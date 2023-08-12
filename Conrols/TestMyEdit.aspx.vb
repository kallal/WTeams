Public Class TestMyEdit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        MyEdit.MyTable = "Teams"
        MyEdit.MyPk = 1
        MyEdit.PopEdit(Me.Button1.ClientID)

    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click



    End Sub

    Protected Sub MyEdit_MySaveTrigger()

    End Sub


End Class