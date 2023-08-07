Imports System.Data.SqlClient

Module General


    Public Function GetConStr() As String

        Return My.Settings.DB


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




End Module
