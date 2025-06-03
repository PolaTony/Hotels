Imports Infragistics.Win
Imports Infragistics.Win.UltraWinListView
Imports System.Data.SqlClient

Public Class Frm_MessagesMain_A

#Region " Declaration                                                           "
    Dim vQueryMessages As String = "N"

#End Region
    Private Sub Frm_MessagesMain_A_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TXT_FromSummaryDate.Value = Now
        Txt_ToSummaryDate.Value = Now

        sLoadStatusList()
        sQueryMessages()
    End Sub

    Private Sub sQueryMessages()
        Try
            Dim vFromDate, vToDate, vToDate_PlusOneDay As String

            If Not TXT_FromSummaryDate.Value Is Nothing Then
                vFromDate = "'" & Format(TXT_FromSummaryDate.Value, "MM-dd-yyyy") & "'"
            Else
                vFromDate = "NULL"
            End If

            vToDate_PlusOneDay = Txt_ToSummaryDate.DateTime.AddDays(1)
            vToDate = "'" & Format(CDate(vToDate_PlusOneDay), "MM-dd-yyyy") & "'"

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select From_Emp_Code,                         " & _
            "        Employees.DescA,                       " & _
            "        TDate,                                 " & _
            "        Message,                               " & _
            "        HaveAttachments                        " & _
            " From   Inbox INNER JOIN Inbox_To              " & _
            " ON     Inbox.Code = Inbox_To.IN_Code          " & _
            " INNER  JOIN Employees                         " & _
            " ON     Employees.Code = Inbox_To.To_Emp_Code  " & _
            " Where  1 = 1                                  " & _
            " And    To_Emp_Code = '" & vUsrCode & "'       " & _
            " And    IsRead = 'N'                           " & _
            " Order  By TDate                               "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Messages.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Messages.Rows.SetCount(vRowCounter + 1)

                If vSqlReader.IsDBNull(0) = False Then
                    DTS_Messages.Rows(vRowCounter)("From_Code") = vSqlReader(0)
                Else
                    DTS_Messages.Rows(vRowCounter)("From_Code") = ""
                End If

                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Messages.Rows(vRowCounter)("From_Desc") = vSqlReader(1)
                Else
                    DTS_Messages.Rows(vRowCounter)("From_Desc") = ""
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Messages.Rows(vRowCounter)("TDate") = Trim(vSqlReader(2))
                Else
                    DTS_Messages.Rows(vRowCounter)("TDate") = Nothing
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Messages.Rows(vRowCounter)("Message") = vSqlReader(3)
                Else
                    DTS_Messages.Rows(vRowCounter)("Message") = ""
                End If

                If vSqlReader(4) = "N" Then
                    GRD_Messages.Rows(vRowCounter).Cells("Attachment").Appearance.Image = Btn_White.Appearance.Image
                End If

                GRD_Messages.Rows(vRowCounter).Appearance.FontData.Bold = DefaultableBoolean.True

                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            vsqlCommand.CommandText = _
            " Select From_Emp_Code,                         " & _
            "        Employees.DescA as Emp_Desc,           " & _
            "        TDate,                                 " & _
            "        Message,                               " & _
            "        IsRead                                 " & _
            " From   Inbox                                  " & _
            " Select From_Emp_Code,                         " & _
            "        Employees.DescA,                       " & _
            "        TDate,                                 " & _
            "        Message,                               " & _
            "        IsRead                                 " & _
            " From   Inbox INNER JOIN Inbox_To              " & _
            " ON     Inbox.Code = Inbox_To.IN_Code          " & _
            " INNER  JOIN Employees                         " & _
            " ON     Employees.Code = Inbox_To.To_Emp_Code  " & _
            " Where  1 = 1                                  " & _
            " And    To_Emp_Code = '" & vUsrCode & "'       " & _
            " And    IsRead = 'Y'                           " & _
            " And   (TDate >= " & vFromDate & " Or " & vFromDate & " Is NULL) " & vbCrLf & _
            " And    TDate < " & vToDate & _
            " Order  By TDate                               "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            vSqlReader = vsqlCommand.ExecuteReader

            Do While vSqlReader.Read
                DTS_Messages.Rows.SetCount(vRowCounter + 1)

                If vSqlReader.IsDBNull(0) = False Then
                    DTS_Messages.Rows(vRowCounter)("From_Code") = vSqlReader(0)
                Else
                    DTS_Messages.Rows(vRowCounter)("From_Code") = ""
                End If

                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Messages.Rows(vRowCounter)("From_Desc") = vSqlReader(1)
                Else
                    DTS_Messages.Rows(vRowCounter)("From_Desc") = ""
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Messages.Rows(vRowCounter)("TDate") = Trim(vSqlReader(2))
                Else
                    DTS_Messages.Rows(vRowCounter)("TDate") = Nothing
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Messages.Rows(vRowCounter)("Message") = vSqlReader(3)
                Else
                    DTS_Messages.Rows(vRowCounter)("Message") = ""
                End If

                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            GRD_Messages.UpdateData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Btn_NewMessage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Btn_NewMessage.Click, Btn_NewMessage2.Click

        Dim vNewMessage As New Frm_NewMessage_A("Normal")
        vNewMessage.Show()
    End Sub

    Private Sub GRD_Messages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GRD_Messages.Click
        If Not GRD_Messages.ActiveRow Is Nothing Then
            If GRD_Messages.ActiveRow.Cells("Message").IsActiveCell Then
                Dim vDisplayMessage As New Frm_DisplayMessage(GRD_Messages.ActiveRow.Cells("Ser").Value)
                vDisplayMessage.ShowDialog()

                sQueryMessages()
            End If
        End If
    End Sub

    Private Sub sLoadStatusList()
        Try
            Dim vListViewItem As UltraListViewItem
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand

            vsqlCommand.CommandText = _
            " Select Code, DescA, Active_Status From  Employees " & _
            " Where  Active_Status In ('ON', 'AW') " & _
            " Order By Active_Status Desc "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Lst_Users.Items.Clear()

            Do While vSqlReader.Read
                vListViewItem = Lst_Users.Items.Add(vSqlReader(0), vSqlReader(1))
                If vSqlReader(2) = "ON" Then
                    vListViewItem.Appearance.Image = Pic_Online.Image
                ElseIf vSqlReader(2) = "AW" Then
                    vListViewItem.Appearance.Image = Pic_Away.Image
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        sLoadStatusList()
    End Sub

End Class