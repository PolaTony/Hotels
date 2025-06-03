Imports System.Data.SqlClient

Public Class Frm_ConfigureMail

    Private Sub Frm_ConfigureMail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sQuery()
    End Sub

    Private Sub sQuery()
        Dim vSQlcommand As New SqlCommand
        vSQlcommand.CommandText = _
        " Select SMTP, Email, Password, Port From Configure_Email "

        vSQlcommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
        Do While vSqlReader.Read
            If vSqlReader.IsDBNull(0) = False Then
                Txt_SMTP.Text = Trim(vSqlReader(0))
            Else
                Txt_SMTP.Text = ""
            End If

            If vSqlReader.IsDBNull(1) = False Then
                Txt_Email.Text = Trim(vSqlReader(1))
            Else
                Txt_Email.Text = ""
            End If

            If vSqlReader.IsDBNull(2) = False Then
                Txt_Password.Text = Trim(vSqlReader(2))
            Else
                Txt_Password.Text = ""
            End If

            If vSqlReader.IsDBNull(3) = False Then
                Txt_Port.Text = Trim(vSqlReader(3))
            Else
                Txt_Port.Text = ""
            End If
        Loop

        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub

    Private Sub Btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Save.Click
        Dim vSqlString As String = " Update Configure_Email " & _
                                   " Set    SMTP  = '" & Trim(Txt_SMTP.Text) & "', " & _
                                   "        Email = '" & Trim(Txt_Email.Text) & "', " & _
                                   "        Password = '" & Trim(Txt_Password.Text) & "', " & _
                                   "        Port  = " & Trim(Txt_Port.Text)

        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
            Me.Close()
        End If
    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub
End Class