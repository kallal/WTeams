Public Class MyEdit
    Inherits System.Web.UI.UserControl

    Private m_ucTitle As String = ""
    Private _MyPk As String = ""

    Public Event MySaveTrigger As EventHandler
    Public Event MyDeleteTrigger As EventHandler

    Private m_Pos As Integer = 1    ' 1 = center, 2  right of button, 3 - left of button
    Private m_PopDiv As String = ""
    Private m_Width As String = "330px"
    Private m_Dismissable As String = "false"

    Public rstData As New DataTable

    Public HideCancel As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Enum Positions
        Center = 1      ' Dialog center of page
        Right = 2       ' dialog to lower right of button
        Left = 3        ' dialog to lower left of button
    End Enum

    Public Property MyTable As String
        Get
            Return ViewState($"{Me.ID}_MyTable")
        End Get
        Set(value As String)
            ViewState($"{Me.ID}_MyTable") = value
        End Set
    End Property


    <System.ComponentModel.Bindable(True)>
    Public Property Position As Positions
        Get
            Return m_Pos
        End Get
        Set(value As Positions)
            m_Pos = value
        End Set
    End Property

    Public Property PopDiv As String
        Get
            Return m_PopDiv
        End Get
        Set(value As String)
            m_PopDiv = value
        End Set
    End Property

    Public Property Width As String
        Get
            Return m_Width
        End Get
        Set(value As String)
            m_Width = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_ucTitle
        End Get
        Set(value As String)
            m_ucTitle = value
        End Set
    End Property



    Public Property MyPk As String
        Get
            _MyPk = ViewState(Me.ID + "_MyPk")
            Return _MyPk
        End Get
        Set(value As String)
            _MyPk = value
            ViewState(Me.ID + "_MyPk") = _MyPk
            LoadData()
        End Set
    End Property

    Public Property Dissmissable As String
        Get
            Return m_Dismissable.ToLower
        End Get
        Set(value As String)
            m_Dismissable = value.ToLower
        End Set
    End Property



    Public Sub LoadData()

        Dim strSQL As String = "SELECT * FROM " & Me.MyTable & " WHERE ID = " & Me.MyPk

        If MyPk <> 0 Then
            rstData = MyRst(strSQL)
            Session(Me.ID & "rstData") = rstData
        End If

        Dim eDiv = FindC(Page, Me.PopDiv)
        Call fLoader(eDiv, rstData.Rows(0))

    End Sub

    Public Sub Save()

        rstData = Session(Me.ID & "rstData")

        Dim eDiv As HtmlGenericControl = FindC(Page, PopDiv)

        Call fWriterW(eDiv, rstData.Rows(0))

        Call SaveData(rstData, rstData.TableName)

    End Sub

    Public Sub PopEdit(sButton As String)

        Dim sJava As String = $"{Me.ID}ucPOP('{sButton}')"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "uEdit", sJava, True)


    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As EventArgs)

        Save()

        RaiseEvent MySaveTrigger(Me, e)


    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs)

        If Me.MyPk > 0 Then

            Dim strSQL As String = $"DELETE FROM  {Me.MyTable} WHERE ID = {Me.MyPk}"
            Debug.Print($"Will delete - SQL = {strSQL}")
            General.MyExecute(strSQL)

            RaiseEvent MyDeleteTrigger(Me, e)

        End If

    End Sub

    Public Function FindC(ByVal root As Control, ByVal sFind As String) As Control
        If root.ID = sFind Then
            Return root
        Else
            Dim t As Control
            For Each c As Control In root.Controls
                t = FindC(c, sFind)
                If t IsNot Nothing Then
                    Return t
                End If
            Next
        End If

        Return Nothing
    End Function

    Public Function CancelClass() As String

        Dim sResult As String = ""

        If HideCancel Then
            sResult = "MyHideBtn"
        End If

        Return sResult

    End Function


End Class