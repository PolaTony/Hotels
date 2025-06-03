Public Class Frm_SentMailHistory_L

    Private Sub Frm_SentMailHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TXT_FromSummaryDate.Value = Now
        Txt_ToSummaryDate.Value = Now
    End Sub

    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles TXT_FromSummaryDate.ValueChanged, Txt_ToSummaryDate.ValueChanged

        sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
    End Sub

    Private Sub sQuerySummaryMain(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            Dim vCodeFilter, vDescFilter, vStatus As String

            Dim vFromDate, vToDate, vToDate_PlusOneDay As String

            If Not TXT_FromSummaryDate.Value Is Nothing Then
                vFromDate = "'" & Format(TXT_FromSummaryDate.Value, "MM-dd-yyyy") & "'"
            Else
                vFromDate = "NULL"
            End If

            vToDate_PlusOneDay = Txt_ToSummaryDate.DateTime.AddDays(1)
            vToDate = "'" & Format(CDate(vToDate_PlusOneDay), "MM-dd-yyyy") & "'"

            If DTS_Summary.Band.Columns.Count = 0 Then
                Return
            End If

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
            " Where  1 = 1                              " & _
            " And (TDate >= " & vFromDate & " Or " & vFromDate & " Is NULL) " & vbCrLf & _
            " And TDate < " & vToDate & _
            " Order By TDate                                        "


            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)
                DTS_Summary.Rows(vRowCounter)("Code") = Trim(vSqlReader(0))

                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate") = Trim(vSqlReader(1))
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Summary.Rows(vRowCounter)("From") = Trim(vSqlReader(2))
                Else
                    DTS_Summary.Rows(vRowCounter)("From") = ""
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("To") = vSqlReader(3)
                Else
                    DTS_Summary.Rows(vRowCounter)("To") = Nothing
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Summary.Rows(vRowCounter)("CC") = Trim(vSqlReader(4))
                Else
                    DTS_Summary.Rows(vRowCounter)("CC") = ""
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Summary.Rows(vRowCounter)("Bcc") = Trim(vSqlReader(5))
                Else
                    DTS_Summary.Rows(vRowCounter)("Bcc") = ""
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Summary.Rows(vRowCounter)("Subject") = Trim(vSqlReader(6))
                Else
                    DTS_Summary.Rows(vRowCounter)("Subject") = ""
                End If

                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Summary.Rows(vRowCounter)("Message") = Trim(vSqlReader(7))
                Else
                    DTS_Summary.Rows(vRowCounter)("Message") = ""
                End If

                If vSqlReader.IsDBNull(8) = False Then
                    DTS_Summary.Rows(vRowCounter)("AttachedFile") = Trim(vSqlReader(8))
                Else
                    DTS_Summary.Rows(vRowCounter)("AttachedFile") = ""
                End If

                If vSqlReader.IsDBNull(9) = False Then
                    DTS_Summary.Rows(vRowCounter)("CompleteFileName") = Trim(vSqlReader(9))
                Else
                    DTS_Summary.Rows(vRowCounter)("CompleteFileName") = ""
                End If

                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()

            'sGetTotalCurrentRows()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuerySummaryMain")
        End Try
    End Sub


    Private Sub Grd_Summary_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick
        If Not Grd_Summary.ActiveRow Is Nothing Then
            Dim vMailPreview As New Frm_MailPreview(Grd_Summary.ActiveRow.Cells("Code").Text)
            vMailPreview.ShowDialog()
        End If
    End Sub
End Class