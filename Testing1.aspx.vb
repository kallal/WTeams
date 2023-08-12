Public Class Testing1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As DateTime
        Dim rstData As DataTable = MyRst("SELECT * from ScoreCards WHERE ID = 1")

        Debug.Print(CDate(rstData.Rows(0)("EventDate")).ToString("yyyy-MM-dd"))

        'dt = Date.Today
        'Debug.Print(Convert.ToString(dt))
        'TextBox1.Text = dt.ToString("yyyy-MM-dd")
    End Sub


End Class