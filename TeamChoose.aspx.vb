Imports System.EnterpriseServices
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Web.Services

Public Class TeamChoose
    Inherits System.Web.UI.Page


    Dim sCardID As Integer = 0

    Dim rstData As New DataTable

    Dim rstScoreCard As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("ScoreCard") Is Nothing Then
            ' no card Id passed, jump to teams
            Response.Redirect("~/Teams.aspx")
            Return
        End If

        sCardID = Session("ScoreCard")

        If Not IsPostBack Then

            Session("TeamATotal") = 0.0
            Session("TeamBTotal") = 0.0

            'LoadCombo()

            LoadData()
            LoadHeading()
            Session("rstScoreCard") = rstScoreCard
        Else
            rstScoreCard = Session("rstScoreCard")
        End If


    End Sub

    Sub LoadHeading()
        ' load up card details

        With rstScoreCard.Rows(0)
            Call fLoader(Me.InfoArea, rstScoreCard.Rows(0))
            GAHead.InnerText = .Item("TeamAT")
            GBHead.InnerText = .Item("TeamBT")
            lblEdate.Text = CDate(.Item("EventDate")).ToString("dddd MMMM dd yyyy")
            lblInfo.Text = .Item("EventInfo").ToString
        End With



    End Sub
    Sub LoadSummary(Optional A As Boolean = False,
                    Optional B As Boolean = False,
                    Optional slCardID As Integer = 0)

        Dim strSQL As String
        Dim cmdSQL As SqlCommand

        Dim TeamATotal As Decimal = Session("TeamATotal")
        Dim TeamBTotal As Decimal = Session("TeamBTotal")

        strSQL = "GetChoices"
        cmdSQL = New SqlCommand(strSQL)
        cmdSQL.CommandType = CommandType.StoredProcedure
        If A Then
            cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = rstScoreCard.Rows(0)("TeamA")
            cmdSQL.Parameters.Add("@ScoreCardID", SqlDbType.Int).Value = slCardID

            Dim rstA As DataTable = MyRstP(cmdSQL)
            GVA.DataSource = rstA
            GVA.DataBind()
            Try
                Dim lblSub As Label = GVA.FooterRow.FindControl("SelTotalSum")
                TeamATotal = rstA.Compute("Sum(SelTotal)", "")
                Session("TeamATotal") = TeamATotal
                lblSub.Text = String.Format("{0:c2}", TeamATotal)
            Catch ex As Exception

            End Try

        End If


        If B Then
            cmdSQL.Parameters.Clear()
            cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = rstScoreCard.Rows(0)("TeamB")
            cmdSQL.Parameters.Add("@ScoreCardID", SqlDbType.Int).Value = slCardID

            Dim rstB As DataTable = MyRstP(cmdSQL)
            GVB.DataSource = rstB
            GVB.DataBind()
            Try
                Dim lblSub As Label = GVB.FooterRow.FindControl("SelTotalSum")
                TeamBTotal = rstB.Compute("Sum(SelTotal)", "")
                Session("TeamBTotal") = TeamBTotal
                lblSub.Text = String.Format("{0:c2}", TeamBTotal)
            Catch ex As Exception

            End Try
        End If

        ' Grand total
        lblTotal.Text = String.Format("{0:c2}", TeamATotal + TeamBTotal)


    End Sub

    'Sub LoadCombo()

    '    Dim rstTeams As DataTable = MyRst("SELECT ID, Team FROM Teams ORDER BY Team")
    '    cboTeamA.DataSource = rstTeams
    '    cboTeamA.DataBind()
    '    cboTeamA.Items.Insert(0, "Select West Team")

    '    cboTeamB.DataSource = rstTeams
    '    cboTeamB.DataBind()
    '    cboTeamB.Items.Insert(0, "Select East Team")
    '    If Not IsDBNull(rstScoreCard.Rows(0)("TeamA")) Then
    '        cboTeamA.Items.FindByValue(rstScoreCard.Rows(0)("TeamA")).Selected = True
    '        GAHead.InnerText = cboTeamA.SelectedItem.Text
    '    End If
    '    If Not IsDBNull(rstScoreCard.Rows(0)("TeamB")) Then
    '        cboTeamB.Items.FindByValue(rstScoreCard.Rows(0)("TeamB")).Selected = True
    '        GBHead.InnerText = cboTeamB.SelectedItem.Text
    '    End If


    'End Sub


    Sub LoadData()


        rstScoreCard = MyRst($"SELECT * FROM vScoreCards WHERE ID = {sCardID}")

        Dim rstTeamA As New DataTable
        Dim strSQL As String = ""

        strSQL = $"select * from vCardChoices where TeamID = {rstScoreCard.Rows(0)("TeamA")} " &
                 $"AND ScoreCardID = {sCardID}"
        rstTeamA = MyRst(strSQL)


        Dim rstTeamB As New DataTable
        strSQL = $"select * from vCardChoices where TeamID = {rstScoreCard.Rows(0)("TeamB")} " &
                 $"AND ScoreCardID = {sCardID}"
        rstTeamB = MyRst(strSQL)

        rstData.Columns.Add("ID", GetType(Integer))

        Dim sHead As String = ""

        For Each sHead In sHeadings     ' left list
            Dim MyCol As New DataColumn(sHead, GetType(Boolean))
            MyCol.DefaultValue = False
            rstData.Columns.Add(MyCol)
        Next

        For Each sHead In sHeadings     ' right list
            Dim MyCol As New DataColumn(sHead.ToLower & sHead.ToLower, GetType(Boolean))
            MyCol.DefaultValue = False
            rstData.Columns.Add(MyCol)
        Next

        For iRow = 1 To 25
            Dim OneRow As DataRow = rstData.NewRow
            OneRow("ID") = iRow

            ' get left side (TeamA) data
            For Each sHead In sHeadings
                Dim sFilter As String = $"RowID = {iRow} And RateType = '{sHead}'"
                If rstTeamA.Select(sFilter).Length > 0 Then
                    OneRow(sHead) = True
                End If
            Next

            ' get right side (TeamB) data
            For Each sHead In sHeadings
                Dim sFilter As String = $"RowID = {iRow} AND RateType = '{sHead}'"
                If rstTeamB.Select(sFilter).Length > 0 Then
                    OneRow(sHead.ToLower & sHead.ToLower) = True
                End If
            Next

            rstData.Rows.Add(OneRow)
        Next

        LstSel.DataSource = rstData
        LstSel.DataBind()
        LoadSummary(True, True, sCardID)



    End Sub

    'Protected Sub s1_CheckedChanged(sender As Object, e As EventArgs)


    '    Dim uA As Boolean = False
    '    Dim uB As Boolean = False

    '    Dim chkSel As CheckBox = sender
    '    Dim lRow As ListViewItem = chkSel.NamingContainer
    '    Dim RowID As Integer = lRow.DataItemIndex + 1
    '    Dim TeamID As Integer = 0
    '    Dim TeamRateID As Integer = 0

    '    Dim TeamAID As Integer = rstScoreCard.Rows(0)("TeamA")
    '    Dim TeamBID As Integer = rstScoreCard.Rows(0)("TeamB")

    '    Dim ckColID As Integer = chkSel.ID.Substring(1)

    '    Dim sHead As String = ""
    '    If ckColID <= sHeadings.Length Then     ' this is TeamA
    '        TeamID = TeamAID
    '        sHead = sHeadings.Substring(ckColID - 1, 1)
    '        uA = True
    '    Else
    '        TeamID = TeamBID
    '        sHead = sHeadings.Substring(ckColID - sHeadings.Length - 1, 1)
    '        uB = True
    '    End If

    '    If chkSel.Checked Then
    '        ' add row to selected
    '        ' get teamRateID this is a dlookup on letter like "A" to rate id
    '        Dim rstTeamRate As DataTable =
    '            MyRst($"SELECT id FROM TeamRates WHERE RateType = '{sHead}' AND Team_ID = {TeamID}")


    '        Dim strSQL = "INSERT INTO CardChoices (ScoreCardID, TeamID, RowID, TeamRateID) " &
    '                     "VALUES (@CardID, @TeamID, @RowID, @TeamRateID)"
    '        Dim cmdSQL As New SqlCommand(strSQL)
    '        cmdSQL.Parameters.Add("@CardID", SqlDbType.Int).Value = sCardID
    '        cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = TeamID
    '        cmdSQL.Parameters.Add("@RowID", SqlDbType.Int).Value = RowID
    '        cmdSQL.Parameters.Add("@TeamRateiD", SqlDbType.Int).Value = rstTeamRate.Rows(0)("ID")
    '        MyRstPE(cmdSQL)
    '        LoadSummary(uA, uB, sCardID)     ' reload the pricing table, but ONLY grid with changes

    '    Else
    '        ' remove row from selected
    '        Dim strSQL As String =
    '            "DELETE FROM CardChoices FROM CardChoices " &
    '            "JOIN TeamRates ON TeamRates.ID = CardChoices.TeamRateID " &
    '            "WHERE RowID = @RowID AND ScoreCardID = @CardID And RateType = @RateType"
    '        Dim cmdSQL As New SqlCommand(strSQL)
    '        cmdSQL.Parameters.Add("@RowID", SqlDbType.Int).Value = RowID
    '        cmdSQL.Parameters.Add("@CardID", SqlDbType.Int).Value = sCardID
    '        cmdSQL.Parameters.Add("@RateType", SqlDbType.NVarChar).Value = sHead
    '        MyRstPE(cmdSQL)
    '        LoadSummary(uA, uB, sCardID)

    '    End If

    'End Sub

    Protected Sub cmdBack_Click(sender As Object, e As EventArgs)

        Response.Redirect("ScoreCards")


    End Sub

    'Protected Sub cboTeamA_SelectedIndexChanged(sender As Object, e As EventArgs)

    '    If cboTeamA.SelectedIndex = 0 Then Return

    '    GAHead.InnerText = cboTeamA.SelectedItem.Text

    '    Dim strSQL As String =
    '        "UPDATE ScoreCards SET TeamA = @TeamID WHERE
    '        ID = @ID"
    '    Dim cmdSQL As New SqlCommand(strSQL)
    '    cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = cboTeamA.SelectedItem.Value
    '    cmdSQL.Parameters.Add("@ID", SqlDbType.Int).Value = sCardID
    '    MyRstPE(cmdSQL)
    '    LoadData()

    'End Sub

    'Protected Sub cboTeamB_SelectedIndexChanged(sender As Object, e As EventArgs)

    '    If cboTeamB.SelectedIndex = 0 Then Return


    '    GBHead.InnerText = cboTeamB.SelectedItem.Text

    '    Dim strSQL As String =
    '        "UPDATE ScoreCards SET TeamB = @TeamID WHERE
    '        ID = @ID"
    '    Dim cmdSQL As New SqlCommand(strSQL)
    '    cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = cboTeamB.SelectedItem.Value
    '    cmdSQL.Parameters.Add("@ID", SqlDbType.Int).Value = sCardID
    '    MyRstPE(cmdSQL)
    '    LoadData()

    'End Sub

    <WebMethod(EnableSession:=True)>
    Public Shared Function MyCheckBox(ControlID As String,
                                     Checked As Boolean) As Dictionary(Of String, String)

        Dim sResult As New Dictionary(Of String, String)

        ' control has id of s1-s20 (20 columns), and then row 
        ' Format of control id = Container_ListBoxName_ControlID_RowID
        ' check box controls all start with "s", then 1 to 20 for colum
        Dim sValues() As String = ControlID.Split("_")
        Dim intCol As Integer = sValues(2).Substring(1)
        Dim intRow As Integer = sValues(3) + 1
        Dim GVSrow As Integer = 0


        Dim rstScoreCard As DataTable = HttpContext.Current.Session("rstScoreCard")

        Dim ScoreCardID As Integer = rstScoreCard.Rows(0)("ID")


        Dim TeamAID As Integer = rstScoreCard.Rows(0)("TeamA")
        Dim TeamBID As Integer = rstScoreCard.Rows(0)("TeamB")

        Dim CardID As Integer = rstScoreCard.Rows(0)("ID")
        Dim TeamID As Integer = 0
        'Dim strSQL As String = ""

        Dim sHead As String = ""
        If intCol <= sHeadings.Length Then     ' this is TeamA
            TeamID = TeamAID
            sHead = sHeadings.Substring(intCol - 1, 1)
            GVSrow = intCol - 1
        Else
            TeamID = TeamBID
            sHead = sHeadings.Substring(intCol - sHeadings.Length - 1, 1)
            GVSrow = intCol - sHeadings.Length - 1
        End If

        AddRemove(Checked, TeamID, sHead, intRow, CardID)

        ' now get totals
        dim cmdSQL As New SqlCommand("GetChoices")
        cmdSQL.CommandType = CommandType.StoredProcedure
        cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = TeamAID
        cmdSQL.Parameters.Add("@ScoreCardID", SqlDbType.Int).Value = ScoreCardID

        Dim rstGVA As DataTable = MyRstP(cmdSQL)
        Dim GVATotal As Decimal = rstGVA.Compute("Sum(SelTotal)", "")

        cmdSQL.Parameters.Clear()
        cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = TeamBID
        cmdSQL.Parameters.Add("@ScoreCardID", SqlDbType.Int).Value = ScoreCardID

        Dim rstGVB As DataTable = MyRstP(cmdSQL)
        Dim GVBTotal As Decimal = rstGVB.Compute("Sum(SelTotal)", "")
        Dim GrandTotal As Decimal = GVATotal + GVBTotal
        sResult.Add("#MainContent_lblTotal", fMoney(GrandTotal))

        ' now get grid values to update
        If intCol <= sHeadings.Length Then     ' this is TeamA
            sResult.Add($"#MainContent_GVA_SelCount_{GVSrow}", rstGVA.Rows(GVSrow)("SelCount")) ' update count
            Dim sRowTotal As String = fMoney(rstGVA.Rows(GVSrow)("SelTotal"))
            sResult.Add($"#MainContent_GVA_SelTotal_{GVSrow}", sRowTotal)
            sResult.Add($"#MainContent_GVA_SelTotalSum", fMoney(GVATotal))
        Else
            sResult.Add($"#MainContent_GVB_SelCount_{GVSrow}", rstGVB.Rows(GVSrow)("SelCount")) ' update count
            Dim sRowTotal As String = fMoney(rstGVB.Rows(GVSrow)("SelTotal"))
            sResult.Add($"#MainContent_GVB_SelTotal_{GVSrow}", sRowTotal)
            sResult.Add($"#MainContent_GVB_SelTotalSum", fMoney(GVBTotal))
        End If

        Return sResult

    End Function

    Public Shared Sub AddRemove(MyChecked As Boolean,
                                TeamID As Integer,
                                sHead As String,
                                RowID As Integer,
                                slCardID As Integer)

        ' add row to selected
        ' get teamRateID this is a dlookup on letter like "A" to rate id
        Dim rstTeamRate As DataTable =
                MyRst($"SELECT id FROM TeamRates 
                    WHERE RateType = '{sHead}' AND Team_ID = {TeamID}")
        Dim intTeamRateID = rstTeamRate.Rows(0)("ID")

        If MyChecked Then

            Dim strSQL = "INSERT INTO CardChoices (ScoreCardID, TeamID, RowID, TeamRateID)
                          VALUES (@CardID, @TeamID, @RowID, @TeamRateID)"

            Dim cmdSQL As New SqlCommand(strSQL)
            cmdSQL.Parameters.Add("@CardID", SqlDbType.Int).Value = slCardID
            cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = TeamID
            cmdSQL.Parameters.Add("@RowID", SqlDbType.Int).Value = RowID
            cmdSQL.Parameters.Add("@TeamRateiD", SqlDbType.Int).Value = intTeamRateID
            MyRstPE(cmdSQL)

        Else
            ' remove row from selected
            Dim strSQL As String =
                "DELETE FROM CardChoices FROM CardChoices " &
                "JOIN TeamRates ON TeamRates.ID = CardChoices.TeamRateID " &
                "WHERE RowID = @RowID AND ScoreCardID = @CardID And TeamRateID = @RateID"
            Dim cmdSQL As New SqlCommand(strSQL)
            cmdSQL.Parameters.Add("@RowID", SqlDbType.Int).Value = RowID
            cmdSQL.Parameters.Add("@CardID", SqlDbType.Int).Value = slCardID
            cmdSQL.Parameters.Add("@RateID", SqlDbType.Int).Value = intTeamRateID
            MyRstPE(cmdSQL)

        End If

    End Sub







End Class