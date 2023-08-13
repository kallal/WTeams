Imports System.Data.Common
Imports System.Data.SqlClient

Module General

    Public Const sHeadings As String = "ABCDEFGHKL"
    Public CuserID As Integer = 1      ' fake logon user id for testing


    Public Function GetConStr() As String

        'Return My.Settings.DB
        Return ConfigurationManager.ConnectionStrings("WTeams.My.MySettings.DB").ConnectionString


    End Function

    Public Function MyRst(strSQL As String) As DataTable


        Dim rstData As New DataTable
        Using conn As New SqlConnection(GetConStr)
            Using cmdSQL As New SqlCommand(strSQL, conn)
                conn.Open()
                rstData.Load(cmdSQL.ExecuteReader)
                rstData.TableName = strSQL
            End Using
        End Using
        Return rstData
    End Function

    Public Function MyRstP(cmdSQL As SqlCommand) As DataTable


        Dim rstData As New DataTable
        Using conn As New SqlConnection(GetConStr)
            Using (cmdSQL)
                cmdSQL.Connection = conn
                conn.Open()
                rstData.Load(cmdSQL.ExecuteReader)
            End Using
        End Using

        Return rstData

    End Function

    Public Sub MyRstPE(cmdSQL As SqlCommand)


        Dim rstData As New DataTable
        Using conn As New SqlConnection(GetConStr)
            Using (cmdSQL)
                cmdSQL.Connection = conn
                conn.Open()
                cmdSQL.ExecuteNonQuery()
            End Using
        End Using

    End Sub



    Public Sub MyExecute(strSQL As String)

        Using conn As New SqlConnection(GetConStr)
            Using cmdSQL As New SqlCommand(strSQL, conn)
                conn.Open()
                cmdSQL.ExecuteNonQuery()
            End Using
        End Using

    End Sub


    Public Sub fLoader(F As HtmlGenericControl, rst As DataRow)

        For Each c As System.Web.UI.Control In F.Controls
            Select Case c.GetType
                Case GetType(TextBox)
                    Dim ctlC As TextBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            If ctlC.TextMode = TextBoxMode.Date Then
                                ctlC.Text = IIf(IsDBNull(rst(ctlC.Attributes("f"))), "", CDate(rst(ctlC.Attributes("f"))).ToString("yyyy-MM-dd"))
                            Else
                                ctlC.Text = IIf(IsDBNull(rst(ctlC.Attributes("f"))), "", rst(ctlC.Attributes("f")))
                            End If
                        End If
                    End If
                Case GetType(Label)
                    Dim ctlC As Label = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            ctlC.Text = IIf(IsDBNull(rst(ctlC.Attributes("f"))), "", rst(ctlC.Attributes("f")))
                        End If
                    End If
                Case GetType(DropDownList)
                    Dim ctlC As DropDownList = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            ctlC.Text = IIf(IsDBNull(rst(ctlC.Attributes("f"))), "", rst(ctlC.Attributes("f")))
                            'ctlC.SelectedValue = IIf(IsDBNull(rst(ctlC.Attributes("f"))), "", rst(ctlC.Attributes("f")))
                        End If
                    End If
                Case GetType(ListBox)
                    Dim ctlC As ListBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            ctlC.Text = IIf(IsDBNull(rst(ctlC.Attributes("f"))), "", rst(ctlC.Attributes("f")))
                            'ctlC.SelectedValue = IIf(IsDBNull(rst(ctlC.Attributes("f"))), "", rst(ctlC.Attributes("f")))
                        End If
                    End If
                Case GetType(CheckBox)
                    Dim ctlC As CheckBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            ctlC.Checked = IIf(IsDBNull(rst(ctlC.Attributes("f"))), False, rst(ctlC.Attributes("f")))
                        End If
                    End If
                Case GetType(RadioButtonList)
                    Dim ctlC As RadioButtonList = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            ctlC.SelectedValue = rst(ctlC.Attributes("f"))
                        End If
                    End If
            End Select
        Next

    End Sub

    Public Sub fclear(F As HtmlGenericControl)

        For Each c As System.Web.UI.Control In F.Controls
            Select Case c.GetType
                Case GetType(TextBox)
                    Dim ctlC As TextBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        ctlC.Text = ""
                    End If
                Case GetType(Label)
                    Dim ctlC As Label = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        ctlC.Text = ""
                    End If

                Case GetType(DropDownList)
                    Dim ctlC As DropDownList = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        ctlC.Text = ""
                        ctlC.SelectedItem.Text = ""
                        ctlC.SelectedItem.Value = ""
                    End If
                Case GetType(ListBox)
                    Dim ctlC As ListBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        ctlC.Text = ""
                        ctlC.SelectedItem.Text = ""
                        ctlC.SelectedItem.Value = ""
                    End If
                Case GetType(CheckBox)
                    Dim ctlC As CheckBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        ctlC.Checked = False
                    End If
                Case GetType(RadioButtonList)
                    Dim ctlC As RadioButtonList = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        ctlC.SelectedValue = -1
                    End If
            End Select
        Next

    End Sub



    Public Sub fWriter(f As HtmlGenericControl,
                       fPK As Integer, strTable As String,
                       Optional strCon As String = "")

        'If strCon = "" Then strCon = My.Settings.TEST4
        ' opposte of fLoader - write a data form to table 
        Dim rstData As New DataTable
        Dim strSQL = "Select * FROM " & strTable & " WHERE ID = " & fPK
        Dim cmdSQL As New SqlCommand(strSQL)

        Using conn As New SqlConnection(strCon)
            cmdSQL.Connection = conn
            Using cmdSQL
                conn.Open()
                rstData.Load(cmdSQL.ExecuteReader)
            End Using
        End Using

        Dim rst As DataRow = rstData.Rows(0)

        ' send conrols to this one data row

        For Each c As System.Web.UI.Control In f.Controls
            Select Case c.GetType
                Case GetType(TextBox)
                    Dim ctlC As TextBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = IIf(ctlC.Text = "", DBNull.Value, ctlC.Text)
                        End If
                    End If
                Case GetType(Label)
                    Dim ctlC As Label = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = IIf(ctlC.Text = "", DBNull.Value, ctlC.Text)
                        End If
                    End If
                Case GetType(DropDownList)
                    Dim ctlC As DropDownList = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = IIf(ctlC.Text = "", DBNull.Value, ctlC.SelectedValue)
                        End If
                    End If
                Case GetType(ListBox)
                    Dim ctlC As ListBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = IIf(ctlC.Text = "", DBNull.Value, ctlC.SelectedValue)
                        End If
                    End If
                Case GetType(CheckBox)
                    Dim ctlC As CheckBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = ctlC.Checked
                        End If
                    End If

                Case GetType(RadioButtonList)
                    Dim ctlC As RadioButtonList = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = ctlC.SelectedValue
                        End If
                    End If
            End Select
        Next

        ' data row is filled, write out changes
        Using conn As New SqlConnection(strCon)
            Using cmdSQL
                cmdSQL.Connection = conn
                conn.Open()
                Dim da As New SqlDataAdapter(cmdSQL)
                Dim daU As New SqlCommandBuilder(da)
                da.Update(rstData)
            End Using
        End Using

    End Sub


    Public Sub fWriterW(f As HtmlGenericControl, rst As DataRow)

        For Each c As System.Web.UI.Control In f.Controls
            Select Case c.GetType
                Case GetType(TextBox)
                    Dim ctlC As TextBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = IIf(ctlC.Text = "", DBNull.Value, ctlC.Text)
                        End If
                    End If
                Case GetType(Label)
                    Dim ctlC As Label = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = IIf(ctlC.Text = "", DBNull.Value, ctlC.Text)
                        End If
                    End If
                Case GetType(DropDownList)
                    Dim ctlC As DropDownList = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = IIf(ctlC.Text = "", DBNull.Value, ctlC.SelectedValue)
                        End If
                    End If
                Case GetType(ListBox)
                    Dim ctlC As ListBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = IIf(ctlC.Text = "", DBNull.Value, ctlC.SelectedValue)
                        End If
                    End If
                Case GetType(CheckBox)
                    Dim ctlC As CheckBox = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = ctlC.Checked
                        End If
                    End If

                Case GetType(RadioButtonList)
                    Dim ctlC As RadioButtonList = c
                    If Not ctlC.Attributes("f") Is Nothing Then
                        If rst.Table.Columns.Contains(ctlC.Attributes("f")) Then
                            rst(ctlC.Attributes("f")) = ctlC.SelectedValue
                        End If
                    End If


            End Select
        Next

    End Sub

    Function SaveData(rstData As DataTable, strSQL As String) As Integer

        Dim iResult As Integer = 0

        ' data row is filled, write out changes
        Using conn As New SqlConnection(GetConStr)
            Using cmdSQL As New SqlCommand(strSQL, conn)
                cmdSQL.Connection = conn
                conn.Open()
                Dim da As New SqlDataAdapter(cmdSQL)
                Dim daU As New SqlCommandBuilder(da)

                da.InsertCommand = daU.GetInsertCommand().Clone()
                da.InsertCommand.CommandText += "; Select ID = SCOPE_IDENTITY()"
                da.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord

                da.Update(rstData)


            End Using
        End Using
        Return iResult

    End Function

    Sub WriteNewRecord(rstData As DataTable)


        ' Write this record to database, PK will be set

        Using conn As New SqlConnection(GetConStr)
            Using cmdSQL As New SqlCommand(rstData.TableName, conn)
                cmdSQL.Connection = conn
                conn.Open()
                Dim da As New SqlDataAdapter(cmdSQL)
                Dim daU As New SqlCommandBuilder(da)

                da.InsertCommand = daU.GetInsertCommand().Clone()
                da.InsertCommand.CommandText += "; SELECT ID = SCOPE_IDENTITY()"
                da.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord
                da.Update(rstData)

            End Using
        End Using

    End Sub

    Public Function MySqlExecute(strSQL As String) As String
        ' general "easey" run sql action query (update, insert etc)

        Dim sError As String = ""
        Using conn As New SqlConnection(GetConStr)
            Using cmdSQL As New SqlCommand(strSQL, conn)
                conn.Open()
                Try
                    cmdSQL.ExecuteNonQuery()
                Catch ex As Exception
                    sError = ex.Message
                End Try
            End Using
        End Using

        Return sError

    End Function

    Sub MyToast2c(my As Page, ctrlBeside As String, Heading As String,
                strText As String,
                Optional strDelay As String = "3000")

        ' same as Mytoast, but a js function called toastcall() MUST be placed on the page
        ' any master page will have this function
        ' ctrlBesite - do NOT pass # - so Button1 you pass "Button1"

        Dim strScipt As String =
            "toastcall('@ctrlBeside','@Heading','@Text','@strDelay');"

        strScipt = strScipt.Replace("@ctrlBeside", ctrlBeside)
        strScipt = strScipt.Replace("@Heading", Heading)
        strScipt = strScipt.Replace("@Text", strText)
        strScipt = strScipt.Replace("@strDelay", strDelay)


        ScriptManager.RegisterStartupScript(my, my.GetType(), "mytoast", strScipt, True)

    End Sub



End Module
