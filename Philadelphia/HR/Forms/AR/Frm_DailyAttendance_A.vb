Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_DailyAttendance_A
#Region " Declaration                                                                           "
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSQlStatment(0) As String
    Dim vHours_In_Decimals As Decimal
    Dim vTime_Split As String()
    Dim vMinutes

#End Region
#Region " Form Level                                                                            "
#Region " My Form                                                                               "
    Private Sub sFillSqlStatmentArray(ByVal pSqlstring As String)
        If vSQlStatment(UBound(vSQlStatment)) = "" Then
            vSQlStatment(UBound(vSQlStatment)) = pSqlstring
        Else
            ReDim Preserve vSQlStatment(UBound(vSQlStatment) + 1)
            vSQlStatment(UBound(vSQlStatment)) = pSqlstring
        End If
    End Sub
    Private Sub sEmptySqlStatmentArray()
        ReDim Preserve vSQlStatment(0)
        vSQlStatment(0) = ""
    End Sub
    Private Sub FRM_AddDed_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm
        sHide_ToolbarMain_Tools()
    End Sub

    Private Sub Frm_AddTrans_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TXT_FromSummaryDate.Value = Now.AddDays(-30)
        Txt_ToSummaryDate.Value = Now
    End Sub
    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If fSaveAll(True) = False Then
            e.Cancel = True
        Else
            e.Cancel = False
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If

        'cControls.sSaveSettings(Me.Name, Grd_Summary)
    End Sub
#End Region
#Region " DataBase                                                                              "
#Region " Save                                                                                  "
    Private Function fIsSaveNeeded() As Boolean
        Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow

        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            For Each vRow In GRD_AddTransaction.Rows
                If vRow.Cells("DML").Value = "I" Then
                    Return True
                End If
            Next
        Else
            For Each vRow In Grd_Summary.Rows
                If vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function
    Public Function fSaveAll(ByVal pASkMe As Boolean) As Boolean
        If fIsSaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()

        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            If pASkMe Then
                If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                    sSaveMaster()

                    If fValidateDetails() Then
                        sSaveDetails()
                    Else
                        Return False
                    End If
                Else
                    DTS_Att.Rows.Clear()
                    Return True
                End If
            Else
                sSaveMaster()

                If fValidateDetails() Then
                    sSaveDetails()
                Else
                    Return False
                End If
            End If
        Else
            If pASkMe Then
                If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                    sSaveSummary()
                Else
                    Return True
                End If
            Else
                sSaveSummary()
            End If

        End If

        If cControls.fSendData(vSQlStatment, Me.Name) > 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)

            'vMasterBlock = "NI"
            If Tab_Main.Tabs("Tab_Details").Selected = True Then
                DTS_Att.Rows.Clear()
            Else
                sQuerySummary()
            End If

            Return True
        End If

    End Function

#End Region
#Region " Query                                                                                  "
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pItemCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)
        If fSaveAll(True) = False Then
            Return
        End If

        Dim vFetchRec As Integer
        If pItemCode = Nothing Then
            If pIsGoTo = False Then
                If pRecPos = Nothing Then
                    vFetchRec = 1
                Else
                    vFetchRec = vcFrmLevel.vRecPos + pRecPos
                    If vFetchRec > cControls.fCount_Rec(" From Employees ") Then
                        vcFrmLevel.vParentFrm.sForwardMessage("33", Me)
                        Return
                    End If
                    If vFetchRec = 0 Then
                        vcFrmLevel.vParentFrm.sForwardMessage("34", Me)
                        vFetchRec = 1
                    End If

                End If
            End If
        End If
        If pRecPos = -2 Then
            vFetchRec = cControls.fCount_Rec(" From Employees ")
        End If

        Dim vFetchCondition As String
        If pItemCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And Code = '" & Trim(pItemCode) & "'"
        End If
        Try

            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText =
            " With MyAtt as (      " & vbCrLf &
            " Select Code,               " & vbCrLf &
            "        ROW_Number() Over (Order By Code) RecPos " &
            "                            " &
            "        From Attendance     " &
            "        Where 1 = 1         " &
            "        And   Attendance.Company_Code = " & vCompanyCode & "   )         " & vbCrLf &
            "                             " &
            "        Select * From MyAtt  " &
            "        Where 1 = 1 " &
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(1) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(1))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(1))

                'Code
                Txt_Code.Text = Trim(vSqlReader(0))

            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            sQueryDetails()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        'vMasterBlock = "N"
    End Sub
#End Region
#End Region
#Region " sOpenLov                                                                              "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String)
        vLovReturn1 = ""
        VLovReturn2 = ""
        'Dim Frm_Lov As New FRM_LovTreeL(pSqlStatment, pTitle)
        'Frm_Lov.ShowDialog()

        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            If pTitle = "الموظفين" Then
                GRD_AddTransaction.ActiveRow.Cells("EMP_Code").Value = vLovReturn1
                GRD_AddTransaction.ActiveRow.Cells("EMP_Desc").Value = VLovReturn2
            Else
                GRD_AddTransaction.ActiveRow.Cells("TYPE_Code").Value = vLovReturn1
                GRD_AddTransaction.ActiveRow.Cells("TYPE_Desc").Value = VLovReturn2
            End If
        End If
    End Sub
#End Region
#Region " Tab Mangment                                                                           "
    Private Sub Tab_Main_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Main.SelectedTabChanging
        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            If fSaveAll(True) = False Then
                e.Cancel = True
            Else
                e.Cancel = False
                sNewRecord()
            End If
        End If
    End Sub
    Private Sub Tab_Main_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm = Me.ParentForm
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, True, False, False, False, False, "", False, False, "التفاصيل")
            sQuerySummary()
        ElseIf Tab_Main.Tabs("Tab_Details").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
            'If Grd_Summary.Selected.Rows.Count > 0 Then
            '    sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
            'Else
            '    sNewRecord()
            'End If
        End If
    End Sub
#End Region
#Region " New Record                                                                            "
    Public Sub sNewRecord()
        Try
            If fSaveAll(True) = False Then
                Return
            End If

            Tab_Main.Tabs("Tab_Details").Selected = True

            vcFrmLevel.vRecPos = 0
            vcFrmLevel.vParentFrm.sPrintRec("")

            'vMasterBlock = "NI"

            DTS_Att.Rows.Clear()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sNewCode()
        Dim vSqlString As String
        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From  Attendance " &
                     " Where Company_Code = " & vCompanyCode

        Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name)

    End Sub
#End Region
#End Region

#Region " Master                                                                                "
    Private Sub sSaveMaster()
        Dim vSqlString As String

        'If vMasterBlock = "I" Then
        'sNewCode()

        'vSqlString = " Insert Into Attendance (         Code,           TDate,        Emp_Code,          Company_Code ) " &
        '                 " Values                 ( " & Txt_Code.Text & ", GetDate(), " & vUsrCode & ", " & vCompanyCode & ") "

        '    sFillSqlStatmentArray(vSqlString)
        'End If
    End Sub
#End Region
#Region " Details                                                                               "
#Region " DataBase                                                                              "
#Region " Query                                                                                 "
    Private Sub sQueryDetails()
        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText =
            " Select Ser,                                            " & vbCrLf &
            "        Attendance_Details.Emp_Code,                    " & vbCrLf &
            "        TDate,                                          " & vbCrLf &
            "        SignIn,                                         " & vbCrLf &
            "        SignOut,                                        " & vbCrLf &
            "        NetHours,                                       " & vbCrLf &
            "        OverTime,                                       " & vbCrLf &
            "        Attendance_Details.Remarks,                     " & vbCrLf &
            "        Approved_OT                                     " & vbCrLf &
            "                                                        " & vbCrLf &
            "        From Attendance_Details Left Join Employees     " & vbCrLf &
            "        On   Attendance_Details.Emp_Code = Employees.code   " & vbCrLf &
            "                                                        " & vbCrLf &
            "        Where 1 = 1                                     " & vbCrLf &
            "        And   Att_Code = " & Txt_Code.Text & vbCrLf &
            "        And   Attendance_Details.Company_Code = " & vCompanyCode


            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_Att.Rows.Clear()

            Dim vRow As UltraGridRow

            Do While vSqlReader.Read
                DTS_Att.Rows.SetCount(vRowCounter + 1)

                vRow = GRD_AddTransaction.Rows(vRowCounter)
                vRow.Activation = Activation.AllowEdit

                'Ser
                DTS_Att.Rows(vRowCounter)("Ser") = vSqlReader(0)

                'Emp_Code
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Att.Rows(vRowCounter)("Emp_Code") = vSqlReader(1)
                Else
                    DTS_Att.Rows(vRowCounter)("Emp_Code") = Nothing
                End If

                'TDate
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Att.Rows(vRowCounter)("TDate") = vSqlReader(2)
                Else
                    DTS_Att.Rows(vRowCounter)("TDate") = Nothing
                End If

                'SignIn
                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Att.Rows(vRowCounter)("SignIn") = CDate(vSqlReader(3))
                Else
                    DTS_Att.Rows(vRowCounter)("SignIn") = Nothing
                End If

                'SignOut
                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Att.Rows(vRowCounter)("SignOut") = vSqlReader(4)
                Else
                    DTS_Att.Rows(vRowCounter)("SignOut") = Nothing
                End If

                'Net Hours
                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Att.Rows(vRowCounter)("NetHours") = vSqlReader(5)
                Else
                    DTS_Att.Rows(vRowCounter)("NetHours") = Nothing
                End If

                'Over Time
                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Att.Rows(vRowCounter)("OverTime") = vSqlReader(6)
                Else
                    DTS_Att.Rows(vRowCounter)("OverTime") = Nothing
                End If

                'Remarks
                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Att.Rows(vRowCounter)("Remarks") = vSqlReader(7)
                Else
                    DTS_Att.Rows(vRowCounter)("Remarks") = Nothing
                End If

                'Approved_OT
                If vSqlReader.IsDBNull(8) = False Then
                    DTS_Att.Rows(vRowCounter)("Approved_OT") = vSqlReader(8)
                Else
                    DTS_Att.Rows(vRowCounter)("Approved_OT") = False
                End If

                'DML
                DTS_Att.Rows(vRowCounter)("DML") = "N"

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#Region " Save                                                                                  "
    Private Function fValidateDetails() As Boolean
        Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
        For Each vRow In GRD_AddTransaction.Rows
            'If IsDBNull(vRow.Cells("EMP_Desc").Value) Then
            '    vcFrmLevel.vParentFrm.sForwardMessage("3", Me)
            '    vRow.Cells("EMP_Code").Selected = True
            '    Return False
            'End If

            If IsDBNull(vRow.Cells("TDate").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("53", Me)
                vRow.Cells("TDate").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("SignIn").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("54", Me)
                vRow.Cells("SignIn").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("SignOut").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("54", Me)
                vRow.Cells("SignOut").Selected = True
                Return False
            End If
        Next

        Return True
    End Function
    Private Sub sSaveDetails()
        Try
            Dim vDate, vOverTime, vLessTime As String

            Dim vSqlString As String
            GRD_AddTransaction.UpdateData()
            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
            For Each vRow In GRD_AddTransaction.Rows

                vDate = " '" & Format(vRow.Cells("TDate").Value, "MM-dd-yyyy") & "' "

                If IsDBNull(vRow.Cells("OverTime").Value) Then
                    vOverTime = "NULL"
                Else
                    vOverTime = " '" & Format(vRow.Cells("OverTime").Value, "MM-dd-yyyy HH:mm") & "' "
                End If

                If IsDBNull(vRow.Cells("LessTime").Value) Then
                    vLessTime = "NULL"
                Else
                    vLessTime = " '" & Format(vRow.Cells("LessTime").Value, "MM-dd-yyyy HH:mm") & "' "
                End If

                If vRow.Cells("DML").Value = "I" Then
                    vSqlString = " Insert Into Attendance_Details (      Company_Code,                     Emp_Code,                  TDate,                              SignIn,                                                            SignOut,                                                          NetHours,                                                                           NetHours_Num,                OverTime,          LessTime,                                               OverTime_Num,                                                  LessTime_Num,                           Approved_OT,                              Approved_LT,                              Is_OverTime_Modified,                              Is_LessTime_Modified     ) " &
                                 "                      Values    ( " & vCompanyCode & ", '" & vRow.Cells("Emp_Code").Value & "', " & vDate & " , '" & Format(vRow.Cells("SignIn").Value, "MM-dd-yyyy HH:mm") & "' , '" & Format(vRow.Cells("SignOut").Value, "MM-dd-yyyy HH:mm") & "', '" & Format(vRow.Cells("NetHours").Value, "MM-dd-yyyy HH:mm") & "', " & fConvert_Time_To_Decimal(vRow.Cells("NetHours").Text) & ", " & vOverTime & ", " & vLessTime & ", " & fConvert_Time_To_Decimal(vRow.Cells("OverTime").Text) & ", " & fConvert_Time_To_Decimal(vRow.Cells("LessTime").Text) & ", '" & vRow.Cells("Approved_OT").Text & "', '" & vRow.Cells("Approved_LT").Text & "', '" & vRow.Cells("Is_OverTime_Modified").Text & "', '" & vRow.Cells("Is_LessTime_Modified").Text & "' )"

                    sFillSqlStatmentArray(vSqlString)

                ElseIf vRow.Cells("DML").Value = "U" Then
                    vSqlString = " Update Attendance_Details " &
                                 " Set Remarks = '" & vRow.Cells("Remarks").Text & "', " &
                                 "     OverTime = " & fIsNull(vRow.Cells("OverTime").Value, 0) & ", " &
                                 "     Approved_OT = '" & vRow.Cells("Approved_OT").Text & "' " &
                                 "                                                   " &
                                 " Where Ser = " & vRow.Cells("Ser").Text

                    sFillSqlStatmentArray(vSqlString)
                End If

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#Region " Delete                                                                                "
    Public Sub sDelete()
        Try
            If Not Grd_Summary.ActiveRow Is Nothing Then
                If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                    Dim vSqlstring As String

                    If Not Grd_Summary.ActiveRow Is Nothing Then
                        If Grd_Summary.ActiveRow.Cells("DML").Value = "I" Or Grd_Summary.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_Summary.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_Summary.ActiveRow.Cells("DML").Value = "N" Or Grd_Summary.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring =
                            " Delete From Attendance " &
                            " Where  Ser       = '" & Grd_Summary.ActiveRow.Cells("Ser").Value & "'"

                            If cControls.fSendData(vSqlstring, Me.Name) > 0 Then
                                Grd_Summary.ActiveRow.Delete(False)
                                vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                            "
    Private Sub GRD_Addtransaction_BeforeCellDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GRD_AddTransaction.BeforeCellDeactivate
        If GRD_AddTransaction.ActiveRow.Cells("EMP_Code").Activated Then
            If IsDBNull(GRD_AddTransaction.ActiveRow.Cells("EMP_Code").Value) = False Then
                If cControls.fCount_Rec("From Users Where Code = '" &
                                    GRD_AddTransaction.ActiveRow.Cells("EMP_Code").Value & "'") > 0 Then
                    GRD_AddTransaction.ActiveRow.Cells("EMP_Desc").Value = cControls.fReturnValue("Select Name From Users Where Code = '" & GRD_AddTransaction.ActiveRow.Cells("EMP_Code").Value & "'", Me.Name)
                Else
                    e.Cancel = True
                    sOpenLov("Select Code, Name From Users", "الموظفين")
                End If
            End If
        End If

    End Sub
    Private Sub GRD_AddTransaction_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles GRD_AddTransaction.CellChange
        If IsDBNull(GRD_AddTransaction.ActiveRow.Cells("DML").Value) Then
            Exit Sub
        End If

        If GRD_AddTransaction.ActiveRow.Cells("OverTime").Activated = True Then
            GRD_AddTransaction.ActiveRow.Cells("Is_OverTime_Modified").Value = "Y"
            GRD_AddTransaction.ActiveRow.Cells("OverTime").Appearance.BackColor = Color.LightPink
        ElseIf GRD_AddTransaction.ActiveRow.Cells("LessTime").Activated = True Then
            GRD_AddTransaction.ActiveRow.Cells("Is_LessTime_Modified").Value = "Y"
            GRD_AddTransaction.ActiveRow.Cells("LessTime").Appearance.BackColor = Color.LightPink
        End If

        If GRD_AddTransaction.ActiveRow.Cells("DML").Value = "NI" Then
            GRD_AddTransaction.ActiveRow.Cells("DML").Value = "I"
        ElseIf GRD_AddTransaction.ActiveRow.Cells("DML").Value = "N" Then
            GRD_AddTransaction.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub sClear()
        DTS_Att.Rows.Clear()
    End Sub
#End Region
#End Region
#Region " Summary                                                                               "
#Region " DataBase                                                                              "
#Region " Query                                                                                 "
    Private Sub sQuerySummary()
        If DTS_Summary.Band.Columns.Count = 0 Then
            Exit Sub
        End If

        Dim vFromDate, vToDate_PlusOneDay As String

        If Not TXT_FromSummaryDate.Value Is Nothing Then
            vFromDate = "'" & Format(TXT_FromSummaryDate.Value, "MM-dd-yyyy") & "'"
        Else
            vFromDate = "NULL"
        End If

        vToDate_PlusOneDay = " '" & Format(Txt_ToSummaryDate.DateTime.AddDays(1), "MM-dd-yyyy") & "' "

        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText =
            " Select Ser,                                            " & vbCrLf &
            "        Attendance_Details.Emp_Code,                    " & vbCrLf &
            "        TDate,                                          " & vbCrLf &
            "        SignIn,                                         " & vbCrLf &
            "        SignOut,                                        " & vbCrLf &
            "        NetHours,                                       " & vbCrLf &
            "        OverTime,                                       " & vbCrLf &
            "        LessTime,                                       " & vbCrLf &
            "        Attendance_Details.Remarks,                     " & vbCrLf &
            "        Approved_OT,                                    " & vbCrLf &
            "        Approved_LT,                                    " & vbCrLf &
            "        OverTime_Num,                                   " & vbCrLf &
            "        Is_OverTime_Modified,                           " & vbCrLf &
            "        Is_LessTime_Modified                            " & vbCrLf &
            "                                                        " & vbCrLf &
            "        From Attendance_Details Left Join Employees     " & vbCrLf &
            "        On   Attendance_Details.Emp_Code = Employees.code   " & vbCrLf &
            "                                                        " & vbCrLf &
            "        Where 1 = 1                                     " & vbCrLf &
            " And   (TDate >= " & vFromDate & " Or " & vFromDate & " Is NULL) " & vbCrLf &
            " And    TDate < " & vToDate_PlusOneDay &
            "        And   Attendance_Details.Company_Code = " & vCompanyCode


            Dim vRowCounter As Integer = 0
            Dim vRow As UltraGridRow

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)

                If Grd_Summary.Rows.Count = 0 Then
                    cControls.vSqlConn.Close()
                    Exit Sub
                End If

                vRow = Grd_Summary.Rows(vRowCounter)

                'Ser
                DTS_Summary.Rows(vRowCounter)("Ser") = vSqlReader(0)

                'Emp_Code
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Summary.Rows(vRowCounter)("Emp_Code") = vSqlReader(1)
                Else
                    DTS_Summary.Rows(vRowCounter)("Emp_Code") = Nothing
                End If

                'TDate
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate") = vSqlReader(2)
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                End If

                'SignIn
                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("SignIn") = CDate(vSqlReader(3))
                Else
                    DTS_Summary.Rows(vRowCounter)("SignIn") = Nothing
                End If

                'SignOut
                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Summary.Rows(vRowCounter)("SignOut") = vSqlReader(4)
                Else
                    DTS_Summary.Rows(vRowCounter)("SignOut") = Nothing
                End If

                'Net Hours
                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Summary.Rows(vRowCounter)("NetHours") = vSqlReader(5)
                Else
                    DTS_Summary.Rows(vRowCounter)("NetHours") = Nothing
                End If

                'Over Time
                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Summary.Rows(vRowCounter)("OverTime") = vSqlReader(6)

                    If vSqlReader(12) = "Y" Then 'First I Check if OverTime Modified 
                        vRow.Cells("OverTime").Appearance.BackColor = Color.LightPink
                    Else
                        If fIsNull(vSqlReader(11), 0) > 0.15 Then 'Here I Check if overTime plus 10 minute will change the color
                            vRow.Cells("OverTime").Appearance.BackColor = Color.Yellow
                        End If
                    End If
                Else
                    DTS_Summary.Rows(vRowCounter)("OverTime") = Nothing

                End If

                'Less Time
                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Summary.Rows(vRowCounter)("LessTime") = vSqlReader(7)

                    If vSqlReader(13) = "Y" Then 'First I Check if LessTime Modified 
                        vRow.Cells("LessTime").Appearance.BackColor = Color.LightPink
                    Else
                        vRow.Cells("LessTime").Appearance.BackColor = Color.FromArgb(255, 192, 128)
                    End If
                Else
                    DTS_Summary.Rows(vRowCounter)("LessTime") = Nothing
                End If

                'Remarks
                If vSqlReader.IsDBNull(8) = False Then
                    DTS_Summary.Rows(vRowCounter)("Remarks") = vSqlReader(8)
                Else
                    DTS_Summary.Rows(vRowCounter)("Remarks") = Nothing
                End If

                'Approved_OT
                If vSqlReader.IsDBNull(9) = False Then
                    DTS_Summary.Rows(vRowCounter)("Approved_OT") = vSqlReader(9)
                Else
                    DTS_Summary.Rows(vRowCounter)("Approved_OT") = False
                End If

                'Approved_LT
                If vSqlReader.IsDBNull(10) = False Then
                    DTS_Summary.Rows(vRowCounter)("Approved_LT") = vSqlReader(10)
                Else
                    DTS_Summary.Rows(vRowCounter)("Approved_LT") = False
                End If

                'DML
                DTS_Summary.Rows(vRowCounter)("DML") = "N"

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#Region " Save                                                                                  "
    Private Sub sSaveSummary()
        Try
            Dim vDate, vOverTime, vLessTime As String

            Dim vSqlString As String
            Grd_Summary.UpdateData()
            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
            For Each vRow In Grd_Summary.Rows

                vDate = " '" & Format(vRow.Cells("TDate").Value, "MM-dd-yyyy") & "' "

                If IsDBNull(vRow.Cells("OverTime").Value) Then
                    vOverTime = "NULL"
                Else
                    vOverTime = " '" & Format(vRow.Cells("OverTime").Value, "MM-dd-yyyy HH:mm") & "' "
                End If

                If IsDBNull(vRow.Cells("LessTime").Value) Then
                    vLessTime = "NULL"
                Else
                    vLessTime = " '" & Format(vRow.Cells("LessTime").Value, "MM-dd-yyyy HH:mm") & "' "
                End If

                If vRow.Cells("DML").Value = "U" Then
                    vSqlString = " Update Attendance_Details " &
                                 " Set OverTime = " & vOverTime & ", " &
                                 "     LessTime = " & vLessTime & ", " &
                                 "     OverTime_Num = " & fConvert_Time_To_Decimal(vRow.Cells("OverTime").Text) & ", " &
                                 "     LessTime_Num = " & fConvert_Time_To_Decimal(vRow.Cells("LessTime").Text) & ", " &
                                 "     Approved_OT = '" & vRow.Cells("Approved_OT").Text & "', " &
                                 "     Approved_LT = '" & vRow.Cells("Approved_LT").Text & "', " &
                                 "     Is_OverTime_Modified = '" & vRow.Cells("Is_OverTime_Modified").Text & "', " &
                                 "     Is_LessTime_Modified = '" & vRow.Cells("Is_LessTime_Modified").Text & "', " &
                                 "     Remarks  = '" & vRow.Cells("Remarks").Text & "' " &
                                 "                                                   " &
                                 " Where Ser = " & vRow.Cells("Ser").Text

                    sFillSqlStatmentArray(vSqlString)
                End If

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#End Region
#Region " Form Level                                                                            "

    Private Sub TXT_FromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles TXT_FromSummaryDate.ValueChanged, Txt_ToSummaryDate.ValueChanged

        sQuerySummary()
    End Sub
    Private Sub Grd_Summary_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Summary.CellChange
        Try
            If e.Cell.IsFilterRowCell Then
                Exit Sub
            End If

            If Grd_Summary.ActiveRow.Cells("OverTime").Activated = True Then
                Grd_Summary.ActiveRow.Cells("Is_OverTime_Modified").Value = "Y"
                Grd_Summary.ActiveRow.Cells("OverTime").Appearance.BackColor = Color.LightPink
            ElseIf Grd_Summary.ActiveRow.Cells("LessTime").Activated = True Then
                Grd_Summary.ActiveRow.Cells("Is_LessTime_Modified").Value = "Y"
                Grd_Summary.ActiveRow.Cells("LessTime").Appearance.BackColor = Color.LightPink
            End If

            If Grd_Summary.ActiveRow.Cells("DML").Value = "NI" Then
                Grd_Summary.ActiveRow.Cells("DML").Value = "I"
            ElseIf Grd_Summary.ActiveRow.Cells("DML").Value = "N" Then
                Grd_Summary.ActiveRow.Cells("DML").Value = "U"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Grd_Summary_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If Not Grd_Summary.ActiveRow Is Nothing Then
                If e.KeyData = Keys.Enter Then
                    If Grd_Summary.ActiveRow.Cells("Remarks").Activated = True Then
                        Grd_Summary.PerformAction(UltraGridAction.NextRow)
                        Grd_Summary.ActiveRow.Cells("TDate").Selected = True
                        Grd_Summary.ActiveRow.Cells("TDate").Activate()
                        Grd_Summary.PerformAction(UltraGridAction.EnterEditMode)
                    Else
                        Grd_Summary.PerformAction(UltraGridAction.PrevCell)
                        Grd_Summary.PerformAction(UltraGridAction.EnterEditMode)
                        'SendKeys.Send("{Tab}")
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If Grd_Summary.Selected.Rows.Count > 0 Then
            sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Ser").Value)
        Else
            sNewRecord()
        End If
        Tab_Main.Tabs("Tab_Details").Selected = True
    End Sub
#End Region
#End Region
    Private Sub sImportFromExcel()
        Try
            vGrd = Nothing

            Dim vFrm As New Frm_ImportFromExcel_A
            vFrm.ShowDialog()

            If vGrd Is Nothing Then
                Exit Sub
            End If

            'If fValidate_ImportFromExcel() = False Then
            '    Exit Sub
            'End If

            DTS_Att.Rows.Clear()

            Dim i As Integer = 0
            Dim vBasic_WorkTime_min As Int16 = 510
            Dim vDay_WorkTime_Min, vOverTime_Min As Int16
            Dim vHour, vMin As Int16
            Dim vFinal_OverTime As DateTime

            Dim vDate() As String

            Dim vNewRow As UltraGridRow
            Dim vRow As UltraGridRow
            For Each vRow In vGrd.Rows
                'If vRow.Cells(0).Text = "" Or vRow.Cells(1).Text = "" Or vRow.Cells(2).Text = "" Then 'If there is no Item selected Or Width Or Lengh then ignore Line
                '    Continue For
                'End If

                DTS_Att.Rows.Add()

                DTS_Att.Rows(i)("Emp_Code") = vRow.Cells("AC-No.").Text
                DTS_Att.Rows(i)("Emp_Desc") = vRow.Cells("Name").Text

                vNewRow = GRD_AddTransaction.Rows(i)

                'If IsDate(vRow.Cells("Date").Value) Then
                vDate = vRow.Cells("Date").Text.Split("/")
                vRow.Cells("Date").Value = vDate(1) & "-" & vDate(0) & "-" & vDate(2)

                DTS_Att.Rows(i)("TDate") = vRow.Cells("Date").Value
                'End If

                vRow.Cells("Clock In").Value = vRow.Cells("Clock In").Text.Replace("ص", "AM")
                vRow.Cells("Clock In").Value = vRow.Cells("Clock In").Text.Replace("م", "PM")

                DTS_Att.Rows(i)("SignIn") = vRow.Cells("Clock In").Text

                vRow.Cells("Clock Out").Value = vRow.Cells("Clock Out").Text.Replace("ص", "AM")
                vRow.Cells("Clock Out").Value = vRow.Cells("Clock Out").Text.Replace("م", "PM")

                DTS_Att.Rows(i)("SignOut") = vRow.Cells("Clock Out").Text

                vRow.Cells("Work Time").Value = vRow.Cells("Work Time").Text.Replace("ص", "AM")
                vRow.Cells("Work Time").Value = vRow.Cells("Work Time").Text.Replace("م", "PM")

                DTS_Att.Rows(i)("NetHours") = vRow.Cells("Work Time").Value

                'Dim v As String = vRow.Cells("Work Time").Text.Split(":")
                Dim vReg As New Regex("\d+")
                Dim vTime_Split As String() = vRow.Cells("Work Time").Text.Split(":") 'to split the time into hours and minutes
                Dim vMinutes = vReg.Match(vTime_Split(1)) 'to erase any chars in minutes

                vDay_WorkTime_Min = (vTime_Split(0) * 60) + vMinutes.ToString 'Here will get the total workTime by minutes
                vOverTime_Min = vDay_WorkTime_Min - 510
                vHour = Math.Truncate(vOverTime_Min / 60)
                vMin = Math.Abs(vOverTime_Min) Mod 60

                If vOverTime_Min >= 10 Then 'Here I Check if the overtime is over than 10 min will change the color
                    vNewRow.Cells("OverTime").Appearance.BackColor = Color.Yellow
                End If

                If vOverTime_Min > 0 Then
                    vFinal_OverTime = vHour & ":" & vMin
                    DTS_Att.Rows(i)("OverTime") = "1-1-1900 " & vFinal_OverTime
                Else
                    vFinal_OverTime = Math.Abs(vHour) & ":" & vMin
                    DTS_Att.Rows(i)("LessTime") = "1-1-1900 " & vFinal_OverTime

                    vNewRow.Cells("LessTime").Appearance.BackColor = Color.FromArgb(255, 192, 128)
                End If


                If Not IsDBNull(DTS_Att.Rows(i)("NetHours_Num")) Then
                    DTS_Att.Rows(i)("OverTime_Num") = DTS_Att.Rows(i)("NetHours_Num") - 8.5
                End If

                DTS_Att.Rows(i)("DML") = "I"

                i += 1
            Next

            'vMasterBlock = "I"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function fConvert_Time_To_Decimal(ByVal pValue As String)
        If pValue = "" Then
            Return 0
        End If

        Dim vReg As New Regex("\d+")
        vTime_Split = pValue.Split(":") 'to split the time into hours and minutes
        vMinutes = vReg.Match(vTime_Split(1)) 'to erase any chars in minutes

        vHours_In_Decimals = vTime_Split(0) & "." & CInt((vMinutes.ToString * 100 / 60)).ToString.PadLeft(2, "0") 'to get the final work time in decimals
        Return vHours_In_Decimals
    End Function
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

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Btn_ImportFromExcel_Click(sender As Object, e As EventArgs) Handles Btn_ImportFromExcel.Click
        If GRD_AddTransaction.Rows.Count > 0 Then
            MessageBox.Show("لا يمكن الاستيراد من الاكسل الان")
            Exit Sub
        End If

        sImportFromExcel()
    End Sub

    Private Sub Txt_Back_Click(sender As Object, e As EventArgs) Handles Txt_Back.Click
        Tab_Main.Tabs("Tab_Summary").Selected = True
    End Sub

    Private Sub Grd_Summary_FilterCellValueChanged(sender As Object, e As FilterCellValueChangedEventArgs) Handles Grd_Summary.FilterCellValueChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Grd_Summary_FilterRow(sender As Object, e As FilterRowEventArgs) Handles Grd_Summary.FilterRow
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class