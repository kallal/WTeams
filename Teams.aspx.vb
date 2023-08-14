Public Class Teams
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LoadData()

        End If


    End Sub


    Sub LoadData()

        lstDivision.DataSource = MyRst("SELECT ID, Division FROM Divisions ORDER BY Division")
        lstDivision.DataBind()


        GVTeams.DataSource = MyRst("SELECT * FROM 
                                    vTeams ORDER BY Team ")
        GVTeams.DataBind()

    End Sub


    Protected Sub cmdEdit_Click(sender As Object, e As EventArgs)

        Dim cmdEdit As LinkButton = sender
        Dim gRow As GridViewRow = cmdEdit.NamingContainer
        Dim pk As Integer = GVTeams.DataKeys(gRow.RowIndex).Item("ID")

        Dim rstRates As DataTable =
            MyRst($"SELECT * FROM TeamRates WHERE Team_ID = {pk}")

        GVA.DataSource = rstRates
        GVA.DataBind()
        Session("rstRates") = rstRates

        MyEdit.MyTable = "Teams"
        MyEdit.MyPk = pk
        MyEdit.PopEdit(cmdEdit.ClientID)


    End Sub

    Protected Sub cmdShowCards_Click(sender As Object, e As EventArgs)

        ' jump to score cards, but with a filter to this team.
        Dim cmdEdit As LinkButton = sender
        Dim gRow As GridViewRow = cmdEdit.NamingContainer
        Dim pk As Integer = GVTeams.DataKeys(gRow.RowIndex).Item("ID")

        Session("ScoreTeam") = pk

        Response.Redirect("ScoreCards")

    End Sub

    Protected Sub MyEdit_MySaveTrigger()
        ' the one record is saved by the edit control
        ' but we have to save gridview data
        Dim rstRates As DataTable = Session("rstRates")

        For Each gRow As GridViewRow In GVA.Rows
            Dim cRate As Decimal = 0
            Dim txtPrice As TextBox = gRow.FindControl("Price")
            rstRates.Rows(gRow.RowIndex)("Rate") = txtPrice.Text

        Next
        General.SaveData(rstRates, rstRates.TableName)


        LoadData()      ' re-load the grid to reflect any edits

    End Sub

    Protected Sub cmdNew_Click(sender As Object, e As EventArgs)

        Dim cmdAdd As LinkButton = sender
        Dim rstData As DataTable = MyRst("SELECT * FROM Teams WHERE ID = 0")
        Dim NewRow As DataRow = rstData.NewRow

        NewRow("Division_ID") = 1 ' default to west
        rstData.Rows.Add(NewRow)
        General.WriteNewRecord(rstData) ' write reocrd to database, pk is now set

        Dim intPK As Integer = NewRow("ID")

        ' create rows for child table TeamRates
        Dim rstRates As DataTable = MyRst("SELECT * FROM TeamRates WHERE ID = 0")
        For Each sRateLetter In sHeadings
            Dim RateRow = rstRates.NewRow
            RateRow("Team_ID") = intPK
            RateRow("RateType") = sRateLetter
            RateRow("Rate") = 0.0
            rstRates.Rows.Add(RateRow)
        Next
        General.SaveData(rstRates, rstRates.TableName)

        ' now pop the editor
        rstRates =      ' reload that data again - I want the PK's set
            MyRst($"SELECT * FROM TeamRates WHERE Team_ID = {intPK}")

        GVA.DataSource = rstRates
        GVA.DataBind()
        Session("rstRates") = rstRates

        MyEdit.MyTable = "Teams"
        MyEdit.MyPk = intPK
        MyEdit.HideCancel = True

        MyEdit.PopEdit(cmdNew.ClientID)


    End Sub

    Protected Sub MyEdit_MyDeleteTrigger()

        Dim TeamID As Integer = MyEdit.MyPk
        Dim strSQL = $"DELETE FROM ScoreCards WHERE TeamA = {TeamID} OR TeamB = {TeamID}"
        Dim cmdSQL As New SqlCommand(strSQL)
        MyRstPE(cmdSQL)

        LoadData()

    End Sub

End Class