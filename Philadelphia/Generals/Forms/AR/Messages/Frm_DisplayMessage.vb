Imports System.Data.SqlClient

Public Class Frm_DisplayMessage
    Public Sub New(ByVal pSer As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            Dim vSQlCommand As New SqlCommand
            vSQlCommand.CommandText = " Select Name, TDate, Message From Users Inner Join Usr_Messages " & _
                                      " On Users.Code = Usr_Messages.FromUsr " & _
                                      " Where Ser = '" & pSer & "'"

            vSQlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlCommand.ExecuteReader
            Dim vRowCounter As Integer = 0
            Do While vSqlReader.Read
                Txt_From.Text = Trim(vSqlReader(0))
                
                TXT_TDate.Value = vSqlReader(2)
                Txt_Message.Text = Trim(vSqlReader(3))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Dim vSqlString As String
            vSqlString = " Update Usr_Messages Set Mark = 'N' " & _
                         " Where Ser = '" & pSer & "'"

            cControls.fSendData(vSqlString, Me.Name)
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class