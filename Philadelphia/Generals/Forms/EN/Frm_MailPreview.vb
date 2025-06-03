Public Class Frm_MailPreview

    Dim vCode As String
    Public Sub New(ByVal pCode As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        vCode = pCode
        sQuery()
    End Sub

    Private Sub sQuery()
        Dim vsqlCommand As New SqlClient.SqlCommand
        Dim vRowCounter As Integer
        vsqlCommand.CommandText = _
        " Select Code,                              " & _
        "        TDate,                             " & _
        "        FromMail,                              " & _
        "        ToMail,                                " & _
        "        CC,                                " & _
        "        Bcc,                               " & _
        "        Subject,                           " & _
        "        Message,                           " & _
        "        AttachedFile,                       " & _
        "        CompleteFileName                   " & _
        " From   SentMails                          " & _
        " Where  Code = '" & vCode & "'     "
        

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

        Do While vSqlReader.Read
            'DTS_Summary.Rows(vRowCounter)("Code") = Trim(vSqlReader(0))

            'If vSqlReader.IsDBNull(1) = False Then
            '    DTS_Summary.Rows(vRowCounter)("TDate") = Trim(vSqlReader(1))
            'Else
            '    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
            'End If

            If vSqlReader.IsDBNull(2) = False Then
                Txt_From.Text = Trim(vSqlReader(2))
            Else
                Txt_From.Text = ""
            End If

            If vSqlReader.IsDBNull(3) = False Then
                Txt_To.Text = vSqlReader(3)
            Else
                Txt_To.Text = Nothing
            End If

            If vSqlReader.IsDBNull(4) = False Then
                Txt_CC.Text = Trim(vSqlReader(4))
            Else
                Txt_CC.Text = ""
            End If

            If vSqlReader.IsDBNull(5) = False Then
                Txt_Bcc.Text = Trim(vSqlReader(5))
            Else
                Txt_Bcc.Text = ""
            End If

            If vSqlReader.IsDBNull(6) = False Then
                Txt_Subject.Text = Trim(vSqlReader(6))
            Else
                Txt_Subject.Text = ""
            End If

            If vSqlReader.IsDBNull(7) = False Then
                Txt_Body.Text = Trim(vSqlReader(7))
            Else
                Txt_Body.Text = ""
            End If

            'If vSqlReader.IsDBNull(8) = False Then
            '    DTS_Summary.Rows(vRowCounter)("AttachedFile") = Trim(vSqlReader(8))
            'Else
            '    DTS_Summary.Rows(vRowCounter)("AttachedFile") = ""
            'End If

            If vSqlReader.IsDBNull(9) = False Then
                Lbl_Attachments_Desc.Text = Trim(vSqlReader(9))
            Else
                Lbl_Attachments_Desc.Text = ""
            End If

        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()

    End Sub
End Class