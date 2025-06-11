Imports Infragistics.Win.UltraWinSchedule

Public Class Frm_Public_Holiday_A
#Region " Declaration                                                                               "
    Dim vSqlStatment(0) As String
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSqlString, vGetSerial As String
    Dim vQuery As String = "N"
    Dim vCustomerCode, vDealSer As String
    Private lastEditedAppointment As Infragistics.Win.UltraWinSchedule.Appointment = Nothing
    Private pendingDateChange As Boolean = False

#End Region

#Region " Form Level                                                                                "
    Public Sub New()
        InitializeComponent()
        UltraCalendarInfo1.Appointments.Clear()
        sQueryHolidaysAppointments()
    End Sub
    Private Sub sFillSqlStatmentArray(ByVal pSqlstring As String)
        If vSqlStatment(UBound(vSqlStatment)) = "" Then
            vSqlStatment(UBound(vSqlStatment)) = pSqlstring
        Else
            ReDim Preserve vSqlStatment(UBound(vSqlStatment) + 1)
            vSqlStatment(UBound(vSqlStatment)) = pSqlstring
        End If
    End Sub
    Private Sub sEmptySqlStatmentArray()
        ReDim Preserve vSqlStatment(0)
        vSqlStatment(0) = ""
    End Sub

#End Region

#Region " Calender Info                                                                             "
    Private Function fValidate_Insert_PublicHoliday(ByVal pStartDate As String, ByVal pEndDate As String, Optional pDataKey As Int16 = 0)


        Dim vSqlString As String =
                            " from  Public_Holidays " &
                            " Where StartDate    <= '" & Format(CDate(pEndDate), "MM-dd-yyyy") & "' " &
                            " And   EndDate      >= '" & Format(CDate(pStartDate), "MM-dd-yyyy") & "' " &
                            " And   Ser          <>  " & pDataKey &
                            " And   Company_Code  =  " & vCompanyCode

        If cControls.fIsExist(vSqlString, Me.Name) = True Then
            MessageBox.Show("هذا التاريخ مستخدم من قبل، يرجى اختيار تاريخ آخر",
                "تنبيه",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Return False
        End If

        Return True

    End Function

    Private Sub UltraCalendarInfo1_BeforeAppointmentAdded(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinSchedule.CancelableAppointmentEventArgs) Handles UltraCalendarInfo1.BeforeAppointmentAdded
        If vQuery = "Y" Then
            Return
        End If

        lastEditedAppointment = e.Appointment

        vStartDate = e.Appointment.StartDateTime

        Dim customDialog As New Frm_Dialog_Appointment()
        customDialog.ShowDialog()

        If vEndDate = Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        If Not fValidate_Insert_PublicHoliday(vStartDate, vEndDate) Then
            e.Cancel = True
            Exit Sub
        End If

        vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Public_Holidays " &
              " Where 1 = 1 " &
              " And   Company_Code = " & vCompanyCode

        vGetSerial = cControls.fReturnValue(vSqlString, Me.Name)

        Dim vIsRemoved As String

        Dim vStartDateFormatted As String = "'" & Format(CDate(vStartDate), "MM-dd-yyyy") & "' "

        Dim vEndDateFormatted As String = "'" & Format(CDate(vEndDate), "MM-dd-yyyy") & "' "

        If lastEditedAppointment.IsRemoved Then
            vIsRemoved = "'Y'"
        Else
            vIsRemoved = "'N'"
        End If

        Dim vDateDiff As Integer = (CDate(vEndDate) - CDate(vStartDate)).Days + 1

        vSqlString = "INSERT INTO Public_Holidays (" &
             "Ser, Company_Code, User_Code, StartDate, EndDate, Real_TimeOff) " &
             "VALUES (" &
             vGetSerial & ", " &
             vCompanyCode & ", " &
             vUsrCode & ", " &
             vStartDateFormatted & ", " &
             vEndDateFormatted & ", " &
             vDateDiff & " - dbo.fn_Get_Week_TimeOff_Days_In_Month(" & vStartDateFormatted & ", " & vEndDateFormatted & ", " & vCompanyCode & ")" &
             ")"

        Dim vRowCounter As Integer = cControls.fSendData(vSqlString, Me.Name)
        If vRowCounter = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("50", Me.Name)
        Else
            lastEditedAppointment.EndDateTime = CDate(vEndDate).AddDays(1)
            lastEditedAppointment.DataKey = vGetSerial
            lastEditedAppointment.Appearance.FontData.SizeInPoints = 13
            lastEditedAppointment.Appearance.FontData.Name = "Droid Arabic Kufi"

        End If

    End Sub
    Private Sub UltraCalendarInfo1_BeforeAppointmentRemoved(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinSchedule.CancelableAppointmentEventArgs) Handles UltraCalendarInfo1.BeforeAppointmentRemoved
        Try
            If e.Appointment.DataKey = Nothing Then
                Return
            End If

            sEmptySqlStatmentArray()

            vSqlString = " Delete From Public_Holidays " &
                         " Where   Ser = " & e.Appointment.DataKey &
                         " And     Company_Code = " & vCompanyCode

            sFillSqlStatmentArray(vSqlString)

            Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)

            If vRowCounter = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("50", Me.Name)
            End If
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء تنفيذ العملية:" & vbCrLf & ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub UltraCalendarInfo1_BeforeDisplayAppointmentDialog(sender As Object, e As Infragistics.Win.UltraWinSchedule.DisplayAppointmentDialogEventArgs) Handles UltraCalendarInfo1.BeforeDisplayAppointmentDialog
        e.Cancel = True

        'Dim customDialog As New Frm_Dialog_Appointment()
        'customDialog.ShowDialog()
    End Sub
    Private Sub UltraCalendarInfo1_CalendarInfoChanged(sender As Object, e As Infragistics.Win.UltraWinSchedule.CalendarInfoChangedEventArgs) Handles UltraCalendarInfo1.CalendarInfoChanged
        If e.PropChangeInfo.ToString.Contains("Appointment.Subject") Then
            Dim vSqlString As String
            vSqlString = " Update Public_Holidays " &
                         " Set    DescA = '" & lastEditedAppointment.Subject & "' " &
                         "                        " &
                         " Where Ser = " & lastEditedAppointment.DataKey &
                         " And   Company_Code = " & vCompanyCode

            cControls.fSendData(vSqlString, Me.Name)
        End If

        If e.PropChangeInfo.ToString.Contains("Appointment.StartDateTime") Or e.PropChangeInfo.ToString.Contains("Appointment.EndDateTime") Then

            pendingDateChange = True
            debounceTimer.Stop()
            debounceTimer.Start()
        End If
    End Sub
    Private Sub UltraCalendarInfo1_AfterSelectedAppointmentsChange(sender As Object, e As EventArgs) Handles UltraCalendarInfo1.AfterSelectedAppointmentsChange
        If UltraCalendarInfo1.SelectedAppointments.Count > 0 Then
            lastEditedAppointment = UltraCalendarInfo1.SelectedAppointments(0)
        End If
    End Sub
    Private Sub debounceTimer_Tick(sender As Object, e As EventArgs) Handles debounceTimer.Tick
        debounceTimer.Stop()

        If pendingDateChange Then
            pendingDateChange = False
            Try
                If Not fValidate_Insert_PublicHoliday(lastEditedAppointment.StartDateTime, lastEditedAppointment.EndDateTime.AddDays(-1), lastEditedAppointment.DataKey) Then
                    sQueryHolidaysAppointments()
                    Exit Sub
                End If

                Dim vDateDiff As Integer = (CDate(lastEditedAppointment.EndDateTime) - CDate(lastEditedAppointment.StartDateTime)).Days

                Dim vSqlString As String
                vSqlString = " Update Public_Holidays " &
                             " Set   StartDate = '" & Format(lastEditedAppointment.StartDateTime, "MM-dd-yyyy") & "', " &
                             "       EndDate   = '" & Format(lastEditedAppointment.EndDateTime.AddDays(-1), "MM-dd-yyyy") & "', " &
                             "       Real_TimeOff = " & vDateDiff & " - dbo.fn_Get_Week_TimeOff_Days_In_Month('" & Format(lastEditedAppointment.StartDateTime, "MM-dd-yyyy") & "', '" & Format(lastEditedAppointment.EndDateTime.AddDays(-1), "MM-dd-yyyy") & "', " & vCompanyCode & ")" &
                             " Where Ser = " & lastEditedAppointment.DataKey &
                             " And   Company_Code = " & vCompanyCode

                cControls.fSendData(vSqlString, Me.Name)
            Catch ex As Exception
                MessageBox.Show("حدث خطأ أثناء تنفيذ العملية:" & vbCrLf & ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    'Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
    '    Me.Close()
    'End Sub
#End Region

#Region " DataBase                                                                                  "
    Private Sub sQueryHolidaysAppointments()
        Try
            vQuery = "Y"

            UltraCalendarInfo1.Appointments.Clear()

            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Ser,             " &
                                      " StartDate,              " &
                                      " EndDate,                " &
                                      " DescA                   " &
                                      "                          " &
                                      " From Public_Holidays     " &
                                      " Where   1 = 1            " &
                                      " And   Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            Do While vSqlReader.Read
                Dim vAppointment As New Infragistics.Win.UltraWinSchedule.Appointment(vSqlReader(1), CDate(vSqlReader(2)).AddDays(1))
                vAppointment.Appearance.FontData.SizeInPoints = 13
                vAppointment.Appearance.FontData.Name = "Droid Arabic Kufi"

                vAppointment.DataKey = vSqlReader("Ser")

                vAppointment.Subject = vSqlReader("DescA")

                vQuery = "Y"

                UltraCalendarInfo1.Appointments.Add(vAppointment)
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            vQuery = "N"

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show("حدث خطأ أثناء تنفيذ العملية:" & vbCrLf & ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Btn_Add_Holiday_Click(sender As Object, e As EventArgs) Handles Btn_Add_Holiday.Click
        vQuery = "Y"
        vStartDate = Nothing
        vEndDate = Nothing

        Dim customDialog As New Frm_Dialog_Appointment_Desc()
        customDialog.Txt_StartDate.Value = Nothing
        customDialog.Txt_EndDate.Value = Nothing
        customDialog.ShowDialog()

        If vStartDate = Nothing Then
            Exit Sub
        End If

        If Not fValidate_Insert_PublicHoliday(vStartDate, vEndDate) Then
            Exit Sub
        End If

        vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Public_Holidays " &
                     " Where 1 = 1          " &
                     " And   Company_Code = " & vCompanyCode

        vGetSerial = cControls.fReturnValue(vSqlString, Me.Name)

        Dim vStartDateFormatted As String = "'" & Format(CDate(vStartDate), "MM-dd-yyyy") & "' "

        Dim vEndDateFormatted As String = "'" & Format(CDate(vEndDate), "MM-dd-yyyy") & "' "

        Dim vDateDiff As Integer = (CDate(vEndDate) - CDate(vStartDate)).Days + 1

        vSqlString = "INSERT INTO Public_Holidays (" &
             "Ser, Company_Code, User_Code, StartDate, EndDate, DescA, Real_TimeOff) " &
             "VALUES (" &
             vGetSerial & ", " &
             vCompanyCode & ", " &
             vUsrCode & ", " &
             vStartDateFormatted & ", " &
             vEndDateFormatted & ", '" &
             vHolidayDesc & "', " &
             vDateDiff & " - dbo.fn_Get_Week_TimeOff_Days_In_Month(" & vStartDateFormatted & ", " & vEndDateFormatted & ", " & vCompanyCode & ")" &
             ")"

        Dim vRowCounter As Integer = cControls.fSendData(vSqlString, Me.Name)

        If vRowCounter = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("50", Me.Name)
        Else

            Dim vAppointment As New Appointment(vStartDate, CDate(vEndDate).AddDays(1))
            vAppointment.Appearance.FontData.SizeInPoints = 13
            vAppointment.Appearance.FontData.Name = "Droid Arabic Kufi"

            vAppointment.Subject = vHolidayDesc
            vAppointment.DataKey = vGetSerial
            UltraCalendarInfo1.Appointments.Add(vAppointment)

        End If
        vQuery = "N"
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Frm_Public_Holidays_A_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm
        vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", False, False, "التفاصيل")

        sHide_ToolbarMain_Tools()
    End Sub

    Private Sub sHide_ToolbarMain_Tools()
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_New").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Save").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Delete").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Print").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_NextRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_PreviousRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_LastRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_FirstRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_CloseWindow").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_GotoRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_ChangeUser").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Themes").SharedProps.Visible = True
    End Sub

#End Region
End Class