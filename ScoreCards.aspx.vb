Public Class ScoreCards
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Loadcombo()

            If Session("ScoreTeam") IsNot Nothing Then
                ' one time filter passed to this page - we kill
                ' session, since it persits
                ' viewstate is per page life time
                ViewState("ScoreTeam") = Session("ScoreTeam")
                Session("ScoreTeam") = Nothing
            End If
            LoadData()
        End If

    End Sub


    Sub Loadcombo()

        Dim rstTeams As DataTable = MyRst("SELECT ID, Team FROM Teams ORDER BY Team")

        cboTeamA.DataSource = rstTeams
        cboTeamA.DataBind()
        cboTeamA.Items.Insert(0, New ListItem("Select West Team", ""))

        cboTeamB.DataSource = rstTeams
        cboTeamB.DataBind()
        cboTeamB.Items.Insert(0, New ListItem("Select East Team", ""))

    End Sub


    Sub LoadData()
        Dim strSQL As String

        If ViewState("ScoreTeam") IsNot Nothing Then
            ' filter only teams to id
            Dim TeamID As Integer = ViewState("ScoreTeam")

            Dim TeamName As String = MyRst($"SELECT * FROM Teams WHERE ID = {TeamID}").Rows(0)("Team")
            Me.myhead.InnerText = $"Score Cards for {TeamName}"

            strSQL = $"select * from vScoreCards WHERE TeamA = {TeamID} OR TeamB = {TeamID}
                       ORDER BY EventDate, TeamAT"

        Else
            strSQL = $"select * from vScoreCards 
                       ORDER BY EventDate, TeamAT"

        End If

        GVCards.DataSource = MyRst(strSQL)
        GVCards.DataBind()

    End Sub


    Protected Sub cmdView_Click(sender As Object, e As EventArgs)
        ' bad name - this is edit, not view!!!!
        Dim cmdBtn As LinkButton = sender
        Dim gRow As GridViewRow = cmdBtn.NamingContainer
        Dim intPK As Integer = GVCards.DataKeys(gRow.RowIndex).Item("ID")

        MyEdit.MyTable = "ScoreCards"
        MyEdit.MyPk = intPK
        MyEdit.PopEdit(cmdBtn.ClientID)


    End Sub

    Protected Sub cmdShowCard_Click(sender As Object, e As EventArgs)

        Dim cmdView As LinkButton = sender
        Dim gRow As GridViewRow = cmdView.NamingContainer
        Dim intPK As Integer = GVCards.DataKeys(gRow.RowIndex).Item("ID")
        Session("ScoreCard") = intPK

        Dim rstCard As DataTable =
            MyRst($"SELECT * FROM ScoreCards WHERE ID = {intPK}")

        With rstCard.Rows(0)
            If IsDBNull(.Item("TeamA")) Or IsDBNull(.Item("TeamB")) Then

                Call MyToast2c(Page, cmdView.ClientID,
                               "Enter Team A and B",
                               "You must enter team A and team B<br/>before using scorecard",
                               "6000")
                Return

            End If
        End With

        Response.Redirect("~/TeamChoose")


    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click

        Dim cmdAdd As LinkButton = sender
        Dim rstData As DataTable = MyRst("SELECT * FROM ScoreCards WHERE ID = 0")
        Dim NewRow As DataRow = rstData.NewRow
        NewRow("User_ID") = CuserID
        NewRow("EventDate") = DateTime.Today

        rstData.Rows.Add(NewRow)
        General.WriteNewRecord(rstData) ' write reocrd to database, pk is now set

        Dim intPK As Integer = NewRow("ID")

        MyEdit.MyTable = "ScoreCards"
        MyEdit.MyPk = intPK

        If ViewState("ScoreTeam") IsNot Nothing Then
            cboTeamA.Text = ViewState("ScoreTeam")

        End If
        MyEdit.HideCancel = True


        MyEdit.PopEdit(cmdNew.ClientID)




    End Sub

    Protected Sub cboTeamA_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub cboTeamB_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub MyEdit_MySaveTrigger()

        LoadData()  ' show edit changes in grid view 

    End Sub

    Protected Sub MyEdit_MyDeleteTrigger()

        LoadData() ' show deletes in grid view

    End Sub


End Class