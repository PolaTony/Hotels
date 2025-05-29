Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_Additional_A
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

        sQuerySummary()
    End Sub

    Private Sub Frm_AddTrans_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TXT_FromSummaryDate.Value = Now.AddDays(-30)
        Txt_ToSummaryDate.Value = Now
    End Sub

#End Region
#Region " DataBase                                                                              "
#Region " Save                                                                                  "
    Private Function fIsSaveNeeded() As Boolean
        Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow

        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            For Each vRow In GRD_Additions.Rows
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
                    DTS_Addition.Rows.Clear()
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
            sSaveSummary()
        End If

        If cControls.fSendData(vSQlStatment, Me.Name) > 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)

            'vMasterBlock = "NI"
            If Tab_Main.Tabs("Tab_Details").Selected = True Then
                DTS_Addition.Rows.Clear()
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
                    If vFetchRec > cControls.fCount_Rec(" From Employees Where 1 = 1 And Company_Code = " & vCompanyCode) Then
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
            vFetchRec = cControls.fCount_Rec(" From Employees Where 1 = 1 And Company_Code = " & vCompanyCode)
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

        Dim Frm_Lov As New FRM_LovGeneral_A(pSqlStatment, pTitle)
        Frm_Lov.ShowDialog()

        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            If pTitle = "Emp" Then
                GRD_Additions.ActiveRow.Cells("EMP_Code").Value = vLovReturn1
                GRD_Additions.ActiveRow.Cells("EMP_Desc").Value = VLovReturn2

                sLoad_EmployeeInfo(GRD_Additions.ActiveRow)
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
            Txt_Back.Visible = False

        ElseIf Tab_Main.Tabs("Tab_Details").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, False, False, False, False, "", False, False, "بحث")            'If Grd_Summary.Selected.Rows.Count > 0 Then
            Txt_Back.Visible = True

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

            DTS_Addition.Rows.Clear()

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
    Private Sub TXT_All_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) _
        Handles Txt_Employees.EditorButtonClick

        If sender.readOnly = True Then
            Return
        End If

        If sender.name = "Txt_Employees" Then
            sOpenLov(" Select Code, DescA From Employees " &
                     " Where IsActive = 'Y' And Company_Code = " & vCompanyCode, "Emp")

        End If
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
            "        Approved                                        " & vbCrLf &
            "                                                        " & vbCrLf &
            "        From Attendance_Details Left Join Employees     " & vbCrLf &
            "        On   Attendance_Details.Emp_Code = Employees.code   " & vbCrLf &
            "        And  Attendance_Details.Company_Code = Employees.Company_Code " & vbCrLf &
            "                                                        " & vbCrLf &
            "        Where 1 = 1                                     " & vbCrLf &
            "        And   Att_Code = " & Txt_Code.Text & vbCrLf &
            "        And   Attendance_Details.Company_Code = " & vCompanyCode


            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_Addition.Rows.Clear()

            Dim vRow As UltraGridRow

            Do While vSqlReader.Read
                DTS_Addition.Rows.SetCount(vRowCounter + 1)

                vRow = GRD_Additions.Rows(vRowCounter)
                vRow.Activation = Activation.AllowEdit

                'Ser
                DTS_Addition.Rows(vRowCounter)("Ser") = vSqlReader(0)

                'Emp_Code
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Addition.Rows(vRowCounter)("Emp_Code") = vSqlReader(1)
                Else
                    DTS_Addition.Rows(vRowCounter)("Emp_Code") = Nothing
                End If

                'TDate
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Addition.Rows(vRowCounter)("TDate") = vSqlReader(2)
                Else
                    DTS_Addition.Rows(vRowCounter)("TDate") = Nothing
                End If

                'SignIn
                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Addition.Rows(vRowCounter)("SignIn") = CDate(vSqlReader(3))
                Else
                    DTS_Addition.Rows(vRowCounter)("SignIn") = Nothing
                End If

                'SignOut
                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Addition.Rows(vRowCounter)("SignOut") = vSqlReader(4)
                Else
                    DTS_Addition.Rows(vRowCounter)("SignOut") = Nothing
                End If

                'Net Hours
                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Addition.Rows(vRowCounter)("NetHours") = vSqlReader(5)
                Else
                    DTS_Addition.Rows(vRowCounter)("NetHours") = Nothing
                End If

                'Over Time
                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Addition.Rows(vRowCounter)("OverTime") = vSqlReader(6)
                Else
                    DTS_Addition.Rows(vRowCounter)("OverTime") = Nothing
                End If

                'Remarks
                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Addition.Rows(vRowCounter)("Remarks") = vSqlReader(7)
                Else
                    DTS_Addition.Rows(vRowCounter)("Remarks") = Nothing
                End If

                'Approved
                If vSqlReader.IsDBNull(8) = False Then
                    DTS_Addition.Rows(vRowCounter)("Approved") = vSqlReader(8)
                Else
                    DTS_Addition.Rows(vRowCounter)("Approved") = False
                End If

                'DML
                DTS_Addition.Rows(vRowCounter)("DML") = "N"

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
    Private Sub sLoad_EmployeeInfo(ByVal pRow As UltraGridRow)
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Employees.DescA,          " &
                                      "        Salary,                   " &
                                      "        Departments.DescA         " &
                                      "                                  " &
                                      " From   Employees Left Join Departments " &
                                      " ON     Employees.Department_Code = Departments.Code " &
                                      " And    Employees.Company_Code = Departments.Company_Code" &
                                      "                                  " &
                                      " Where  Employees.Code = '" & Trim(pRow.Cells("Emp_Code").Text) & "' " &
                                      " And Employees.Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

            Do While vSqlReader.Read
                pRow.Cells("Emp_Desc").Value = vSqlReader(0)
                pRow.Cells("Salary").Value = vSqlReader(1)
                pRow.Cells("Department_Desc").Value = vSqlReader(2)
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#Region " Save                                                                                  "
    Private Function fValidateDetails() As Boolean
        GRD_Additions.UpdateData()

        Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
        For Each vRow In GRD_Additions.Rows
            'If IsDBNull(vRow.Cells("EMP_Desc").Value) Then
            '    vcFrmLevel.vParentFrm.sForwardMessage("3", Me)
            '    vRow.Cells("EMP_Code").Selected = True
            '    Return False
            'End If

            If vRow.Cells("Emp_Desc").Text = "" Then
                vcFrmLevel.vParentFrm.sForwardMessage("51", Me)
                vRow.Cells("Emp_Code").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("TDate").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("53", Me)
                vRow.Cells("TDate").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("TValue").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("11", Me)
                vRow.Cells("TValue").Selected = True
                Return False
            End If
        Next

        Return True
    End Function
    Private Sub sSaveDetails()
        Try
            Dim vDate As String

            Dim vSqlString As String
            GRD_Additions.UpdateData()
            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
            For Each vRow In GRD_Additions.Rows

                vDate = " '" & Format(vRow.Cells("TDate").Value, "MM-dd-yyyy") & "' "

                If vRow.Cells("DML").Value = "I" Then
                    vSqlString = " Insert Into Additions (      Company_Code,                     Emp_Code,                  TDate,                     TValue,                 User_Code,                      Remarks ) " &
                                 "             Values    ( " & vCompanyCode & ", '" & vRow.Cells("Emp_Code").Value & "', " & vDate & ", " & vRow.Cells("TValue").Value & " , " & vUsrCode & ", '" & vRow.Cells("Remarks").Text & "' )"

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
                            " Where  Ser       = '" & Grd_Summary.ActiveRow.Cells("Ser").Value & "'" &
                            " And Company_Code = " & vCompanyCode

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
    Private Sub GRD_Addtransaction_BeforeCellDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GRD_Additions.BeforeCellDeactivate
        If GRD_Additions.ActiveRow.Cells("EMP_Code").Activated Then
            If IsDBNull(GRD_Additions.ActiveRow.Cells("EMP_Code").Value) = False Then
                If cControls.fCount_Rec(" From Employees Where Code = '" & GRD_Additions.ActiveRow.Cells("EMP_Code").Value & "' " &
                                        " And IsActive = 'Y' And Company_Code = " & vCompanyCode) > 0 Then

                    sLoad_EmployeeInfo(GRD_Additions.ActiveRow)
                Else
                    vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                    e.Cancel = True
                    'sOpenLov("Select Code, Name From Users", "الموظفين")
                End If
            End If
        End If

    End Sub
    Private Sub GRD_AddTransaction_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles GRD_Additions.CellChange
        If IsDBNull(GRD_Additions.ActiveRow.Cells("DML").Value) Then
            Exit Sub
        End If

        If GRD_Additions.ActiveRow.Cells("DML").Value = "NI" Then
            GRD_Additions.ActiveRow.Cells("DML").Value = "I"
        ElseIf GRD_Additions.ActiveRow.Cells("DML").Value = "N" Then
            GRD_Additions.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub Grd_Additions_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRD_Additions.KeyUp
        Try
            GRD_Additions.UpdateData()

            If e.KeyData = Keys.Enter Then
                If GRD_Additions.ActiveRow.Cells("Emp_Code").Activated = True Then
                    If IsDBNull(GRD_Additions.ActiveRow.Cells("Emp_Code").Value) = False Then
                        If GRD_Additions.ActiveRow.Cells("Emp_Code").Text <> "" Then
                            If cControls.fIsExist(" From Employees " &
                            " Where Code = '" & GRD_Additions.ActiveRow.Cells("Emp_Code").Value & "' " &
                            " And   Company_Code = " & vCompanyCode, Me.Name) = False Then

                                'Grd_Details.ActiveRow.Cells("Item_Code").SelectAll()
                                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                                GRD_Additions.ActiveRow.Cells("Emp_Desc").Value = DBNull.Value
                                GRD_Additions.ActiveRow.Cells("Salary").Value = DBNull.Value
                                GRD_Additions.ActiveRow.Cells("Department_Desc").Value = DBNull.Value
                            Else
                                Do
                                    GRD_Additions.PerformAction(UltraGridAction.NextCell)
                                Loop Until GRD_Additions.DisplayLayout.Bands(0).Columns(GRD_Additions.ActiveCell.Column.Index).CellActivation = Activation.AllowEdit

                                GRD_Additions.PerformAction(UltraGridAction.EnterEditMode)
                            End If
                        End If
                    End If
                ElseIf GRD_Additions.ActiveRow.Cells("Remarks").Activated = True Then
                    GRD_Additions.PerformAction(UltraGridAction.NextRow)
                    GRD_Additions.ActiveRow.Cells("Emp_Code").Selected = True
                    GRD_Additions.ActiveRow.Cells("Emp_Code").Activate()
                    GRD_Additions.PerformAction(UltraGridAction.EnterEditMode)
                Else
                    Do
                        GRD_Additions.PerformAction(UltraGridAction.NextCell)
                    Loop Until GRD_Additions.DisplayLayout.Bands(0).Columns(GRD_Additions.ActiveCell.Column.Index).CellActivation = Activation.AllowEdit

                    GRD_Additions.PerformAction(UltraGridAction.EnterEditMode)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GRD_Additions_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles GRD_Additions.AfterRowInsert
        Try
            If e.Row.Index <> 0 Then
                If Not IsDBNull(GRD_Additions.Rows(e.Row.Index - 1).Cells("TDate").Value) Then
                    e.Row.Cells("TDate").Value = GRD_Additions.Rows(e.Row.Index - 1).Cells("TDate").Value
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GRD_Additions_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) _
        Handles GRD_Additions.ClickCellButton, Grd_Summary.ClickCellButton

        'If Txt_Status.Text = "مقفل" Then
        '    Return
        'End If

        If sender.ActiveRow.Cells("Delete").Activated Then
            If sender.ActiveRow.Cells("DML").Value = "I" Or sender.ActiveRow.Cells("DML").Value = "NI" Then
                sender.ActiveRow.Delete(False)
                'sSumQuantity()

            ElseIf sender.ActiveRow.Cells("DML").Value = "N" Or sender.ActiveRow.Cells("DML").Value = "U" Then
                'First I Check if this Invoice is Submitted by another user then exist Immediatly
                'If cControls.fReturnValue(" Select Status From Job_Order " &
                '                          " Where Code = '" & Trim(Txt_Code.Text) & "' " &
                '                          " And   Company_Code = " & vCompanyCode, Me.Name) = "P" Then

                '    vcFrmLevel.vParentFrm.sForwardMessage("134", Me)
                '    Return
                'End If

                If vcFrmLevel.vParentFrm.sForwardMessage("133", Me) = MsgBoxResult.Yes Then
                    Dim vSqlstring As String =
                    " Delete From Additions " &
                    " Where  1 = 1 " &
                    " And    Company_Code = " & vCompanyCode &
                    " And    Ser        = '" & sender.ActiveRow.Cells("Ser").Value & "'"

                    If cControls.fSendData(vSqlstring, Me.Name) > 0 Then
                        'Here I Create the Log Data
                        'sEmptySqlStatmentArray()

                        'vSqlstring = " Select IsNull(Max(Code), 0) + 1 From  Employees_AllActions_Log "
                        'vLog_Code = cControls.fReturnValue(vSqlstring, Me.Name)

                        'vSqlstring = " Insert Into Employees_AllActions_Log (      Code,             Emp_Code,          TDate,              Action_Desc,          Action_Type,      Invoice_Type,          Invoice_Code,                  Main_Object_Code,                       Main_Object_Desc,                          Remarks,                    ComputerName )" &
                        '                 "                             Values   (" & vLog_Code & ", '" & vUsrCode & "', GetDate(),      'الغاء صنف من عرض سعر',   'D',               'JO',     '" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_CustomerCode.Text) & "', '" & Trim(Txt_CustomerDesc.Text) & "', '" & Trim(Txt_Remarks.Text) & "', '" & My.Computer.Name & "') "

                        'sFillSqlStatmentArray(vSqlstring)

                        'vSqlstring = " Insert Into Employees_AllActions_Log_Details (     Log_Code,       Ser,                                      Item_Code,             Action_Type )  " &
                        '             "                                      Values  (" & vLog_Code & ",     1, '" & Trim(Grd_Items.ActiveRow.Cells("Item_Code").Text) & "',  'الغاء' )"

                        'sFillSqlStatmentArray(vSqlstring)

                        'cControls.fSendData(vSQlStatment, Me.Name)
                        '-------------------------------------------

                        sender.ActiveRow.Delete(False)
                        'sSumQuantity()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub sClear()
        DTS_Addition.Rows.Clear()
    End Sub
    Private Sub GRD_Additions_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GRD_Additions.KeyPress
        If GRD_Additions.ActiveCell IsNot Nothing AndAlso GRD_Additions.ActiveCell.Column.Key = "TValue" Then
            ' Allow digits, control characters (like Backspace), and one dot
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
                e.Handled = True ' Cancel the keypress
            End If

            ' Prevent more than one dot
            If e.KeyChar = "." AndAlso GRD_Additions.ActiveCell.Text.Contains(".") Then
                e.Handled = True
            End If
        End If

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
            "        Emp_Code,                                       " & vbCrLf &
            "        Employees.DescA as Emp_Desc,                    " & vbCrLf &
            "        TDate,                                          " & vbCrLf &
            "        TValue,                                         " & vbCrLf &
            "        Users.DescA as User_Desc,                       " & vbCrLf &
            "        Entry_Date,                                     " & vbCrLf &
            "        Additions.Remarks                               " & vbCrLf &
            "                                                        " & vbCrLf &
            "        From Additions Left Join Employees     " & vbCrLf &
            "        On   Additions.Emp_Code = Employees.code   " & vbCrLf &
            "        And  Additions.Company_Code = Employees.Company_Code " &
            "                                                        " & vbCrLf &
            "        LEFT JOIN Users                                 " & vbCrLf &
            "        ON Users.Code = Additions.User_Code             " & vbCrLf &
            "        And Users.Company_Code = Additions.Company_Code " & vbCrLf &
            "                                                        " & vbCrLf &
            "        Where 1 = 1                                     " & vbCrLf &
            " And   (TDate >= " & vFromDate & " Or " & vFromDate & " Is NULL) " & vbCrLf &
            " And    TDate < " & vToDate_PlusOneDay &
            " And    Additions.Company_Code = " & vCompanyCode

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)

                'vRow = Grd_Summary.Rows(vRowCounter)
                'vRow.Activation = Activation.AllowEdit

                'Ser
                DTS_Summary.Rows(vRowCounter)("Ser") = vSqlReader("Ser")

                'Emp_Code
                If IsDBNull(vSqlReader("Emp_Code")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Emp_Code") = vSqlReader("Emp_Code")
                Else
                    DTS_Summary.Rows(vRowCounter)("Emp_Code") = Nothing
                End If

                'Emp_Desc
                If IsDBNull(vSqlReader("Emp_Desc")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = vSqlReader("Emp_Desc")
                Else
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = Nothing
                End If

                'TDate
                If IsDBNull(vSqlReader("TDate")) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate_Month") = vSqlReader("TDate")
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate_Month") = Nothing
                End If

                'TDate
                If IsDBNull(vSqlReader("TDate")) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate") = vSqlReader("TDate")
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                End If

                'TValue
                If IsDBNull(vSqlReader("TValue")) = False Then
                    DTS_Summary.Rows(vRowCounter)("TValue") = vSqlReader("TValue")
                Else
                    DTS_Summary.Rows(vRowCounter)("TValue") = Nothing
                End If

                'User_Desc
                If IsDBNull(vSqlReader("User_Desc")) = False Then
                    DTS_Summary.Rows(vRowCounter)("User_Desc") = vSqlReader("User_Desc")
                Else
                    DTS_Summary.Rows(vRowCounter)("User_Desc") = Nothing
                End If

                'Entry_Date
                If IsDBNull(vSqlReader("Entry_Date")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Entry_Date") = vSqlReader("Entry_Date")
                Else
                    DTS_Summary.Rows(vRowCounter)("Entry_Date") = Nothing
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                Else
                    DTS_Summary.Rows(vRowCounter)("Remarks") = Nothing
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
            Dim vDate As String

            Dim vSqlString As String
            Grd_Summary.UpdateData()
            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
            For Each vRow In Grd_Summary.Rows

                vDate = " '" & Format(vRow.Cells("TDate").Value, "MM-dd-yyyy") & "' "

                If vRow.Cells("DML").Value = "U" Then
                    vSqlString = " Update Additions " &
                                 " Set Remarks  = '" & vRow.Cells("Remarks").Text & "', " &
                                 "     TValue   = " & vRow.Cells("TValue").Value & ", " &
                                 "     TDate    = " & vDate & ", " &
                                 "     Emp_Code = '" & vRow.Cells("Emp_Code").Value & "' " &
                                 "                                                   " &
                                 " Where Ser    = " & vRow.Cells("Ser").Text &
                                 " And   Company_Code = " & vCompanyCode

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
        If e.Cell.IsFilterRowCell Then
            Exit Sub
        End If

        Try
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

            DTS_Addition.Rows.Clear()

            Dim i As Integer = 0
            Dim vBasic_WorkTime_min As Int16 = 510
            Dim vDay_WorkTime_Min, vOverTime_Min As Int16
            Dim vHour, vMin As Int16
            Dim vFinal_OverTime As DateTime

            Dim vDate() As String

            Dim vRow As UltraGridRow
            For Each vRow In vGrd.Rows
                'If vRow.Cells(0).Text = "" Or vRow.Cells(1).Text = "" Or vRow.Cells(2).Text = "" Then 'If there is no Item selected Or Width Or Lengh then ignore Line
                '    Continue For
                'End If

                DTS_Addition.Rows.Add()

                DTS_Addition.Rows(i)("Emp_Code") = vRow.Cells("AC-No.").Text
                DTS_Addition.Rows(i)("Emp_Desc") = vRow.Cells("Name").Text

                'If IsDate(vRow.Cells("Date").Value) Then
                vDate = vRow.Cells("Date").Text.Split("/")
                vRow.Cells("Date").Value = vDate(1) & "-" & vDate(0) & "-" & vDate(2)

                DTS_Addition.Rows(i)("TDate") = vRow.Cells("Date").Value
                'End If

                vRow.Cells("Clock In").Value = vRow.Cells("Clock In").Text.Replace("ص", "AM")
                vRow.Cells("Clock In").Value = vRow.Cells("Clock In").Text.Replace("م", "PM")

                DTS_Addition.Rows(i)("SignIn") = vRow.Cells("Clock In").Text

                vRow.Cells("Clock Out").Value = vRow.Cells("Clock Out").Text.Replace("ص", "AM")
                vRow.Cells("Clock Out").Value = vRow.Cells("Clock Out").Text.Replace("م", "PM")

                DTS_Addition.Rows(i)("SignOut") = vRow.Cells("Clock Out").Text

                vRow.Cells("Work Time").Value = vRow.Cells("Work Time").Text.Replace("ص", "AM")
                vRow.Cells("Work Time").Value = vRow.Cells("Work Time").Text.Replace("م", "PM")

                DTS_Addition.Rows(i)("NetHours") = vRow.Cells("Work Time").Value

                'Dim v As String = vRow.Cells("Work Time").Text.Split(":")
                Dim vReg As New Regex("\d+")
                Dim vTime_Split As String() = vRow.Cells("Work Time").Text.Split(":") 'to split the time into hours and minutes
                Dim vMinutes = vReg.Match(vTime_Split(1)) 'to erase any chars in minutes

                vDay_WorkTime_Min = (vTime_Split(0) * 60) + vMinutes.ToString 'Here will get the total workTime by minutes
                vOverTime_Min = vDay_WorkTime_Min - 510
                vHour = Math.Truncate(vOverTime_Min / 60)
                vMin = Math.Abs(vOverTime_Min) Mod 60

                If vOverTime_Min > 0 Then
                    vFinal_OverTime = vHour & ":" & vMin
                    DTS_Addition.Rows(i)("OverTime") = "1-1-1900 " & vFinal_OverTime
                Else
                    vFinal_OverTime = Math.Abs(vHour) & ":" & vMin
                    DTS_Addition.Rows(i)("LessTime") = "1-1-1900 " & vFinal_OverTime
                End If


                If Not IsDBNull(DTS_Addition.Rows(i)("NetHours_Num")) Then
                    DTS_Addition.Rows(i)("OverTime_Num") = DTS_Addition.Rows(i)("NetHours_Num") - 8.5
                End If

                DTS_Addition.Rows(i)("DML") = "I"

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

        vHours_In_Decimals = vTime_Split(0) & "." & CInt((vMinutes.ToString * 100 / 60)) 'to get the final work time in decimals
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

    Private Sub Btn_ImportFromExcel_Click(sender As Object, e As EventArgs)
        If GRD_Additions.Rows.Count > 0 Then
            MessageBox.Show("لا يمكن الاستيراد من الاكسل الان")
            Exit Sub
        End If

        sImportFromExcel()
    End Sub

    Private Sub Txt_Back_Click(sender As Object, e As EventArgs) Handles Txt_Back.Click
        Tab_Main.Tabs("Tab_Summary").Selected = True
    End Sub
    Private Sub Grd_Summary_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Grd_Summary.KeyPress
        If Grd_Summary.ActiveCell IsNot Nothing AndAlso Grd_Summary.ActiveCell.Column.Key = "TValue" Then
            ' Allow digits, control characters (like Backspace), and one dot
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
                e.Handled = True ' Cancel the keypress
            End If

            ' Prevent more than one dot
            If e.KeyChar = "." AndAlso Grd_Summary.ActiveCell.Text.Contains(".") Then
                e.Handled = True
            End If
        End If

    End Sub
End Class