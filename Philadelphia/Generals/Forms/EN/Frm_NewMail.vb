Imports System.Net.Mail

Public Class Frm_NewMail
    Dim vCompleteFileName As String
    Dim vFileName As String

    Public Sub New(ByVal pCompleteFileName As String, ByVal pFileName As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        vCompleteFileName = pCompleteFileName
        vFileName = pFileName

        Lbl_Attachments_Desc.Text = pCompleteFileName
    End Sub

    Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton1.Click
        If Trim(Txt_To.Text) = "" Then
            MessageBox.Show("Enter the Recipient To this Mail")
            Txt_To.Select()
            Exit Sub
        End If

        Dim EmailMessage As New MailMessage
        Try
            Dim vFrom As String = cControls.fReturnValue(" Select Email From Configure_Email ", Me.Name)
            If vFrom = "" Then
                MessageBox.Show("Enter Your Mail From Configure Mail")
                Exit Sub
            End If

            Dim vSMTP As String = cControls.fReturnValue(" Select SMTP From Configure_Email ", Me.Name)
            If vSMTP = "" Then
                MessageBox.Show("Enter SMTP From Configure Mail")
                Exit Sub
            End If

            Dim vPassword As String = cControls.fReturnValue(" Select Password From Configure_Email ", Me.Name)
            If vPassword = "" Then
                MessageBox.Show("Enter Password From Configure Mail")
                Exit Sub
            End If

            Dim vPort As String = cControls.fReturnValue(" Select Port From Configure_Email ", Me.Name)
            If vPort = "" Then
                MessageBox.Show("Enter Port From Configure Mail")
                Exit Sub
            End If

            EmailMessage.From = New MailAddress(vFrom)
            EmailMessage.To.Add(Trim(Txt_To.Text))
            If Trim(Txt_CC.Text).Length > 0 Then
                EmailMessage.CC.Add(Trim(Txt_CC.Text))
            End If

            If Trim(Txt_Bcc.Text).Length > 0 Then
                EmailMessage.Bcc.Add(Trim(Txt_Bcc.Text))
            End If

            EmailMessage.Subject = Trim(Txt_Subject.Text)

            EmailMessage.Body = Trim(Txt_Body.Text)

            Dim vAttachment As New System.Net.Mail.Attachment(vCompleteFileName)
            EmailMessage.Attachments.Add(vAttachment)

            Dim Smtp As New SmtpClient(vSMTP)
            Smtp.Port = vPort
            Smtp.Credentials = New System.Net.NetworkCredential(vFrom, vPassword)
            Smtp.Timeout = 3000000
            Smtp.EnableSsl = True
            Smtp.Send(EmailMessage)

            MessageBox.Show("Message Sent Successfully")

            Dim vSqlString As String = " Insert Into SentMails ( TDate,         FromMail,               ToMail,                         CC,                         Bcc,                      Subject,                          Message,                AttachedFile,           CompleteFileName )  " & _
                                       "                Values ( GetDate(), '" & vFrom & "', '" & Trim(Txt_To.Text) & "', '" & Trim(Txt_CC.Text) & "', '" & Trim(Txt_CC.Text) & "', '" & Trim(Txt_Subject.Text) & "', '" & Trim(Txt_Body.Text) & "', '" & vFileName & "', '" & vCompleteFileName & "' )"

            cControls.fSendData(vSqlString, Me.Name)

            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub UltraButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraButton2.Click
        Me.Close()
    End Sub
End Class