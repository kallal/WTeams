Public Class MySetup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim strSQL2 As String = TextBox1.Text
        Dim cmds() As String = Split(strSQL2, "GO")
        Dim sErrors As String = ""
        Using conn = New SqlConnection(GetConStr)
            Using cmdSQL As New SqlCommand("", conn)
                conn.Open()
                For Each strCMD As String In cmds
                    cmdSQL.CommandText = strCMD

                    Try
                        cmdSQL.ExecuteNonQuery()
                    Catch ex As Exception
                        sErrors &= vbCrLf & ex.Message

                    End Try

                Next
            End Using
        End Using

        TextBox1.Text = "done" & sErrors




    End Sub

    Protected Sub cmdShowCon_Click(sender As Object, e As EventArgs)

        Dim s As String = GetConStr()

        TextBox1.Text = s



    End Sub


End Class