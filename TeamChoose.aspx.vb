Imports System.Security.Cryptography

Public Class TeamChoose
    Inherits System.Web.UI.Page

    Const sHeadings As String = "ABCDEFGHKL"

    Dim CuserID As Integer = 1      ' fake logon user id for testing
    Dim sCardID As Integer = 0

    Dim rstData As New DataTable

    Dim rstScoreCard As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("TeamATotal") = 0.0
            Session("TeamBTotal") = 0.0

            Session("ScoreCard") = 1
            sCardID = Session("ScoreCard")
            rstScoreCard = MyRst($"SELECT * FROM ScoreCards WHERE ID = {sCardID}")
            LoadCombo()
            LoadData()

        Else
            sCardID = Session("ScoreCard")
        End If


    End Sub
    Sub LoadSummary(Optional A As Boolean = False, Optional B As Boolean = False)

        Dim strSQL As String
        Dim cmdSQL As SqlCommand

        Dim TeamATotal As Decimal = Session("TeamATotal")
        Dim TeamBTotal As Decimal = Session("TeamBTotal")


        If A Then
            strSQL =
                "SELECT *, (SelCount * Rate) as SelTotal FROM vChoices 
                WHERE Team_ID = @TeamID 
                AND (ScoreCardID is null OR ScoreCardID = @ScoreCardID)
                ORDER BY RateType"

            cmdSQL = New SqlCommand(strSQL)
            cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = cboTeamA.SelectedItem.Value
            cmdSQL.Parameters.Add("@ScoreCardID", SqlDbType.Int).Value = sCardID


            Dim rstA As DataTable = MyRstP(cmdSQL)
            GVA.DataSource = rstA
            GVA.DataBind()
            Dim lblSub As Label = GVA.FooterRow.FindControl("SelTotalSum")
            TeamATotal = rstA.Compute("Sum(SelTotal)", "")
            Session("TeamATotal") = TeamATotal
            lblSub.Text = String.Format("{0:c2}", TeamATotal)

        End If


        If B Then
            strSQL =
                "SELECT *, (SelCount * Rate) as SelTotal FROM vChoices 
                WHERE Team_ID = @TeamID 
                AND (ScoreCardID is null OR ScoreCardID = @ScoreCardID)
                ORDER BY RateType"

            cmdSQL = New SqlCommand(strSQL)
            cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = cboTeamB.SelectedItem.Value
            cmdSQL.Parameters.Add("@ScoreCardID", SqlDbType.Int).Value = sCardID

            Dim rstB As DataTable = MyRstP(cmdSQL)
            GVB.DataSource = rstB
            GVB.DataBind()
            Dim lblSub As Label = GVB.FooterRow.FindControl("SelTotalSum")

            TeamBTotal = rstB.Compute("Sum(SelTotal)", "")
            Session("TeamBTotal") = TeamBTotal
            lblSub.Text = String.Format("{0:c2}", TeamBTotal)

            lblSub.Text = String.Format("{0:c2}", TeamBTotal, "")
        End If

        ' Grand total
        lblTotal.Text = String.Format("{0:c2}", TeamATotal + TeamBTotal)


    End Sub

    Sub LoadCombo()

        Dim rstTeams As DataTable = MyRst("SELECT ID, Team FROM Teams ORDER BY Team")
        cboTeamA.DataSource = rstTeams
        cboTeamA.DataBind()
        cboTeamA.Items.Insert(0, "Select West Team")

        cboTeamB.DataSource = rstTeams
        cboTeamB.DataBind()
        cboTeamB.Items.Insert(0, "Select East Team")
        If Not IsDBNull(rstScoreCard.Rows(0)("TeamA")) Then
            cboTeamA.Items.FindByValue(rstScoreCard.Rows(0)("TeamA")).Selected = True
            GAHead.InnerText = cboTeamA.SelectedItem.Text
        End If
        If Not IsDBNull(rstScoreCard.Rows(0)("TeamB")) Then
            cboTeamB.Items.FindByValue(rstScoreCard.Rows(0)("TeamB")).Selected = True
            GBHead.InnerText = cboTeamB.SelectedItem.Text
        End If


    End Sub


    Sub LoadData()

        Dim rstTeamA As New DataTable

        If cboTeamA.SelectedIndex > 0 Then
            rstTeamA = MyRst($"select * from vCardChoices
                              where TeamID = {cboTeamA.SelectedItem.Value} and ScoreCardID = {sCardID}")

        End If

        Dim rstTeamB As New DataTable
        If cboTeamB.SelectedIndex > 0 Then
            rstTeamB = MyRst($"select * from vCardChoices
                              where TeamID = {cboTeamB.SelectedItem.Value} and ScoreCardID = {sCardID}")

        End If

        rstData.Columns.Add("id", GetType(Integer))

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
            OneRow("id") = iRow

            ' get left side (TeamA) data
            For Each sHead In sHeadings
                Dim sFilter As String = $"RowID = {iRow} AND RateType = '{sHead}'"
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
        LoadSummary(True, True)



    End Sub

    Protected Sub s1_CheckedChanged(sender As Object, e As EventArgs)


        Dim uA As Boolean = False
        Dim uB As Boolean = False

        Dim chkSel As CheckBox = sender
        Dim lRow As ListViewItem = chkSel.NamingContainer
        Dim RowID As Integer = lRow.DataItemIndex + 1
        Dim TeamID As Integer = 0
        Dim TeamRateID As Integer = 0

        Dim ckColID As Integer = chkSel.ID.Substring(1)
        Dim sHead As String = ""
        If ckColID <= sHeadings.Length Then     ' this is TeamA
            TeamID = cboTeamA.SelectedItem.Value
            sHead = sHeadings.Substring(ckColID - 1, 1)
            uA = True
        Else
            TeamID = cboTeamB.SelectedItem.Value
            sHead = sHeadings.Substring(ckColID - sHeadings.Length - 1, 1)
            uB = True
        End If

        If chkSel.Checked Then
            ' add row to selected
            ' get teamRateID this is a dlookup on letter like "A" to rate id
            Dim rstTeamRate As DataTable =
                MyRst($"SELECT id FROM TeamRates WHERE RateType = '{sHead}' AND Team_ID = {TeamID}")


            Dim strSQL = "INSERT INTO CardChoices (ScoreCardID, TeamID, RowID, TeamRateID)
                         VALUES (@CardID, @TeamID, @RowID, @TeamRateID)"
            Dim cmdSQL As New SqlCommand(strSQL)
            cmdSQL.Parameters.Add("@CardID", SqlDbType.Int).Value = sCardID
            cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = TeamID
            cmdSQL.Parameters.Add("@RowID", SqlDbType.Int).Value = RowID
            cmdSQL.Parameters.Add("@TeamRateiD", SqlDbType.Int).Value = rstTeamRate.Rows(0)("ID")
            MyRstPE(cmdSQL)
            LoadSummary(uA, uB)     ' reload the pricing table, but ONLY grid with changes

        Else
            ' remove row from selected
            Dim strSQL As String =
                "DELETE FROM CardChoices 
                FROM CardChoices
                JOIN TeamRates ON TeamRates.ID = CardChoices.TeamRateID
                 WHERE RowID = @RowID AND ScoreCardID = @CardID And RateType = @RateType"
            Dim cmdSQL As New SqlCommand(strSQL)
            cmdSQL.Parameters.Add("@RowID", SqlDbType.Int).Value = RowID
            cmdSQL.Parameters.Add("@CardID", SqlDbType.Int).Value = sCardID
            cmdSQL.Parameters.Add("@RateType", SqlDbType.NVarChar).Value = sHead
            MyRstPE(cmdSQL)
            LoadSummary(uA, uB)

        End If

    End Sub

    Protected Sub cboTeamA_SelectedIndexChanged(sender As Object, e As EventArgs)

        If cboTeamA.SelectedIndex = 0 Then Return

        GAHead.InnerText = cboTeamA.SelectedItem.Text

        Dim strSQL As String =
            "UPDATE ScoreCards SET TeamA = @TeamID WHERE
            ID = @ID"
        Dim cmdSQL As New SqlCommand(strSQL)
        cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = cboTeamA.SelectedItem.Value
        cmdSQL.Parameters.Add("@ID", SqlDbType.Int).Value = sCardID
        MyRstPE(cmdSQL)
        LoadData()

    End Sub

    Protected Sub cboTeamB_SelectedIndexChanged(sender As Object, e As EventArgs)

        If cboTeamB.SelectedIndex = 0 Then Return


        GBHead.InnerText = cboTeamB.SelectedItem.Text

        Dim strSQL As String =
            "UPDATE ScoreCards SET TeamB = @TeamID WHERE
            ID = @ID"
        Dim cmdSQL As New SqlCommand(strSQL)
        cmdSQL.Parameters.Add("@TeamID", SqlDbType.Int).Value = cboTeamB.SelectedItem.Value
        cmdSQL.Parameters.Add("@ID", SqlDbType.Int).Value = sCardID
        MyRstPE(cmdSQL)
        LoadData()

    End Sub

    Protected Sub GVA_RowDataBound(sender As Object, e As GridViewRowEventArgs)


    End Sub


End Class