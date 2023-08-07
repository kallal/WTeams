Public Class Testing1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        TextBox1.Text = "Processing done"

        System.Threading.Thread.Sleep(3000)



    End Sub


End Class