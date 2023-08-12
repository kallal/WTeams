Public Class MyDialog
    Inherits System.Web.UI.UserControl

    Private m_ButtonID As String = ""
    Private m_ucTitle As String = ""
    Private m_ucBody As String = ""
    Private m_OkBText As String = "Ok"
    Private m_CancelBText As String = "Cancel"
    Private m_Pos As Integer = 1    ' 1 = center, 2  right of button, 3 - left of button
    Private m_PopDiv As String = ""
    Private m_Width As String = "330px"
    Private m_OkIcon As Integer = 0
    Private m_CancelIcon As Integer = 0
    Private m_Modal As String = "true"
    Private m_Dismissable As String = "false"



    Public Enum Positions
        Center = 1      ' Dialog center of page
        Right = 2       ' dialog to lower right of button
        Left = 3        ' dialog to lower left of button
    End Enum

    Public Enum IconList
        None
        Trash
        ThumbsUp
        ThumbsDown

    End Enum

    Private IConListS As String() =
              {"",
              "fa-trash-o fa-lg",
              "fa-thumbs-o-up fa-lg",
              "fa-thumbs-o-down fa-lg"}


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        myrender.Visible = False

    End Sub



    Public Property ButtonID As String
        Get
            Return m_ButtonID

        End Get
        Set(value As String)
            m_ButtonID = value
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

    Public Property Body As String
        Get
            Return "<font size=""4""><i>" & m_ucBody & "</font>"
        End Get
        Set(value As String)
            m_ucBody = value
        End Set
    End Property


    Public Property BOkText As String
        Get
            Dim sp3 As String = HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;")
            Dim sp1 As String = HttpUtility.HtmlDecode("&nbsp;")
            Dim sBtext As String
            sBtext = m_OkBText

            If Len(sBtext) >= 2 Then
                If Len(sBtext) = 2 Then
                    sBtext = sp3 & sBtext & sp3
                Else
                    sBtext = sp1 & sBtext & sp1
                End If
            End If

            Return sBtext

        End Get
        Set(value As String)
            m_OkBText = value
        End Set
    End Property

    Public Property BCancelText As String
        Get
            Dim sp3 As String = HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;")
            Dim sp1 As String = HttpUtility.HtmlDecode("&nbsp;")
            Dim sBtext As String
            sBtext = m_CancelBText

            If Len(sBtext) >= 2 Then
                If Len(sBtext) = 2 Then
                    sBtext = sp3 & sBtext & sp3
                Else
                    sBtext = sp1 & sBtext & sp1
                End If
            End If

            Return sBtext

        End Get

        Set(value As String)
            m_CancelBText = value
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

    <System.ComponentModel.Bindable(True)>
    Public Property IconOk As IconList
        Get
            Return m_OkIcon
        End Get
        Set(value As IconList)
            m_OkIcon = value
        End Set
    End Property

    Public Function IconOkS() As String

        Return IConListS(m_OkIcon)

    End Function

    <System.ComponentModel.Bindable(True)>
    Public Property IconCancel As IconList
        Get
            Return m_CancelIcon
        End Get
        Set(value As IconList)
            m_CancelIcon = value
        End Set
    End Property

    Public Function IconCancelS() As String

        Return IConListS(m_CancelIcon)

    End Function


    Public Property Modal As String
        Get
            Return m_Modal
        End Get
        Set(value As String)
            m_Modal = value
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

    Public Sub ShowDialog()


        Dim MyPopDiv As HtmlGenericControl = Me.Parent.FindControl(Me.PopDiv)
        MyPopDiv.Visible = True
        Dim sJava As String = $"{Me.ID}ucPOP(this)"

        Debug.Print(sJava)

        ScriptManager.RegisterStartupScript(Page, Page.GetType, "mypop2", sJava, True)


    End Sub



End Class