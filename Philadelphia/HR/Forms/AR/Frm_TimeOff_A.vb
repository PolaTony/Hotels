Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_TimeOff_A

#Region " Declaration                                                                           "

    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSQlStatment(0) As String
    Dim vHours_In_Decimals As Decimal
    Dim vTime_Split As String()
    Dim vMinutes

#End Region

#Region " Form Level                                                                            "

#Region "My Form                                                                         "
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
        vcFrmLevel.vParentFrm.sEnableTools(True, False, False, True, False, False, False, False, "", False, False, "التفاصيل")

        sHide_ToolbarMain_Tools()

        sQuerySummary()
    End Sub
    Private Sub Frm_AddTrans_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TXT_FromSummaryDate.Value = Now.AddDays(-30)
        Txt_ToSummaryDate.Value = Now
    End Sub

#End Region

#Region "DataBase"

#Region "Save                                                                         "
    Private Function fIsSaveNeeded() As Boolean
        Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow

        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            For Each vRow In GRD_Details.Rows
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
                    If fValidateDetails() Then
                        sSaveDetails()
                    Else
                        Return False
                    End If
                Else
                    DTS_Details.Rows.Clear()
                    Return True
                End If
            Else
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
                DTS_Details.Rows.Clear()
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
                GRD_Details.ActiveRow.Cells("EMP_Code").Value = vLovReturn1
                GRD_Details.ActiveRow.Cells("EMP_Desc").Value = VLovReturn2

                sLoad_EmployeeInfo(GRD_Details.ActiveRow)
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
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, False, False, False, False, "", False, False, "بحث")
            Txt_Back.Visible = True
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

            DTS_Details.Rows.Clear()

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
    Private Sub sLoad_EmployeeInfo(ByVal pRow As UltraGridRow)
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Employees.DescA,          " &
                                      "        Salary,                   " &
                                      "        Departments.DescA         " &
                                      "                                  " &
                                      " From   Employees Left Join Departments " &
                                      " ON     Employees.Department_Code = Departments.Code " &
                                      " And    Employees.Company_Code = Departments.Company_Code " &
                                      "                                  " &
                                      " Where  Employees.Code = '" & Trim(pRow.Cells("Emp_Code").Text) & "' " &
                                      " And    Employees.Company_Code = " & vCompanyCode

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

        ' Update the data in the grid before validation
        GRD_Details.UpdateData()

        ' Loop through each row in the grid
        For Each vRow As Infragistics.Win.UltraWinGrid.UltraGridRow In GRD_Details.Rows

            ' Read values from the current row
            Dim vEmpDesc As String = vRow.Cells("Emp_Desc").Text
            Dim vEmpCode As Object = vRow.Cells("Emp_Code").Value
            Dim vFromDateObj As Object = vRow.Cells("FromDate").Value
            Dim vToDateObj As Object = vRow.Cells("ToDate").Value
            Dim vTimeOffType As Object = vRow.Cells("TimeOff_Type").Value

            ' Check if employee name is empty
            If vEmpDesc = "" Then
                vcFrmLevel.vParentFrm.sForwardMessage("51", Me)
                vRow.Cells("Emp_Code").Selected = True
                Return False
            End If

            ' Check if FromDate or ToDate is missing
            If IsDBNull(vFromDateObj) OrElse IsDBNull(vToDateObj) Then
                vcFrmLevel.vParentFrm.sForwardMessage("53", Me)
                If IsDBNull(vFromDateObj) Then
                    vRow.Cells("FromDate").Selected = True
                Else
                    vRow.Cells("ToDate").Selected = True
                End If
                Return False
            End If

            ' Convert to Date
            Dim vFromDate As Date = CDate(vFromDateObj)
            Dim vToDate As Date = CDate(vToDateObj)

            ' FromDate should not be after ToDate
            If vFromDate > vToDate Then
                vcFrmLevel.vParentFrm.sForwardMessage("172", Me)
                vRow.Cells("FromDate").Selected = True
                Return False
            End If

            ' Check if there is overlapping time off
            If Not fValidate_Insert_TimeOff(vFromDate, vToDate, vEmpCode) Then
                vcFrmLevel.vParentFrm.sForwardMessage("177", Me)
                vRow.Cells("FromDate").Selected = True
                Return False
            End If

            ' Check for conflict with public holidays
            If Not fValidate_Insert_PublicHoliday(vFromDate, vToDate) Then
                vcFrmLevel.vParentFrm.sForwardMessage("178", Me)
                vRow.Cells("FromDate").Selected = True
                Return False
            End If

            ' Check if time off type is selected
            If IsDBNull(vTimeOffType) Then
                vcFrmLevel.vParentFrm.sForwardMessage("171", Me)
                vRow.Cells("TimeOff_Type").Selected = True
                Return False
            End If

            ' If the time off is paid, check remaining annual balance
            If vTimeOffType.ToString() = "P" Then
                Dim vSqlString = $" SELECT dbo.fn_Get_Week_TimeOff_Days_In_Month('{Format(vFromDateObj, "MM-dd-yyyy")}', '{Format(vToDateObj, "MM-dd-yyyy")}', {vCompanyCode}) "
                Dim vWeekTimeOffDays As Integer = cControls.fReturnValue(vSqlString, Me.Name)
                Dim vDateDiffDays As Integer = DateDiff(DateInterval.Day, vFromDate, vToDate) + 1 - vWeekTimeOffDays
                Dim vCurrentYear As Integer = vFromDate.Year

                Dim vRemainingDays As Integer = fRemaining_Annual_TimeOff_Days(vCurrentYear, vEmpCode)

                ' If not enough paid days left, show message and block the request
                If Not (vRemainingDays - vDateDiffDays >= 0) Then
                    vcFrmLevel.vParentFrm.sForwardMessage("179", Me)
                    vRow.Cells("TimeOff_Type").Selected = True
                    Return False
                End If
            End If
        Next

        ' If all checks passed
        Return True
    End Function
    Private Sub sSaveDetails()
        Try
            Dim vFromDate As String
            Dim vToDate As String
            Dim vDateDiff As String

            Dim vSqlString As String
            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
            Dim vCounter As Int16
            Dim vGetCode As Integer

            GRD_Details.UpdateData()


            For Each vRow In GRD_Details.Rows

                vFromDate = " '" & Format(vRow.Cells("FromDate").Value, "MM-dd-yyyy") & "' "
                vToDate = " '" & Format(vRow.Cells("ToDate").Value, "MM-dd-yyyy") & "' "
                vDateDiff = DateDiff(DateInterval.Day, vRow.Cells("FromDate").Value, vRow.Cells("ToDate").Value) + 1

                If vRow.Cells("DML").Value = "I" Then

                    vSqlString = " Select IsNull(Max(Code), 0) + 1 From TimeOff " &
                                 " Where  Company_Code  = " & vCompanyCode

                    vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                    vSqlString = " Insert Into TimeOff   (      Code,          Company_Code,                     Emp_Code,                   FromDate,         ToDate,         User_Code,                      Remarks,                              TimeOff_Type,                               Real_DaysOff ) " &
                                 "             Values    ( " & vGetCode & ", " & vCompanyCode & ", '" & vRow.Cells("Emp_Code").Value & "', " & vFromDate & ", " & vToDate & ", " & vUsrCode & ", '" & vRow.Cells("Remarks").Text & "', '" & vRow.Cells("TimeOff_Type").Value & "', " & vDateDiff & " - dbo.fn_Get_Week_TimeOff_Days_In_Month(" & vFromDate & ", " & vToDate & ", " & vCompanyCode & ") )"

                    sFillSqlStatmentArray(vSqlString)

                    vCounter += 1

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
                            " Delete From TimeOff " &
                            " Where  Code       = " & Grd_Summary.ActiveRow.Cells("Ser").Value &
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
    Private Sub GRD_Addtransaction_BeforeCellDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GRD_Details.BeforeCellDeactivate
        If GRD_Details.ActiveRow.Cells("EMP_Code").Activated Then
            If IsDBNull(GRD_Details.ActiveRow.Cells("EMP_Code").Value) = False Then
                If cControls.fCount_Rec(" From Employees Where Code = '" & GRD_Details.ActiveRow.Cells("EMP_Code").Value & "' " &
                                        " And IsActive = 'Y' And Company_Code = " & vCompanyCode) > 0 Then

                    sLoad_EmployeeInfo(GRD_Details.ActiveRow)
                Else
                    vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                    e.Cancel = True
                    'sOpenLov("Select Code, Name From Users", "الموظفين")
                End If
            End If
        End If

    End Sub
    Private Sub GRD_AddTransaction_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles GRD_Details.CellChange
        If IsDBNull(GRD_Details.ActiveRow.Cells("DML").Value) Then
            Exit Sub
        End If

        If GRD_Details.ActiveRow.Cells("DML").Value = "NI" Then
            GRD_Details.ActiveRow.Cells("DML").Value = "I"
        ElseIf GRD_Details.ActiveRow.Cells("DML").Value = "N" Then
            GRD_Details.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub Grd_Additions_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRD_Details.KeyUp
        Try
            GRD_Details.UpdateData()

            If e.KeyData = Keys.Enter Then
                If GRD_Details.ActiveRow.Cells("Emp_Code").Activated = True Then
                    If IsDBNull(GRD_Details.ActiveRow.Cells("Emp_Code").Value) = False Then
                        If GRD_Details.ActiveRow.Cells("Emp_Code").Text <> "" Then
                            If cControls.fCount_Rec(" From Employees " &
                            " Where Code = '" & GRD_Details.ActiveRow.Cells("Emp_Code").Value & "' " &
                            " And   Company_Code = " & vCompanyCode) = 0 Then

                                'Grd_Details.ActiveRow.Cells("Item_Code").SelectAll()
                                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                                GRD_Details.ActiveRow.Cells("Emp_Desc").Value = DBNull.Value
                                GRD_Details.ActiveRow.Cells("Salary").Value = DBNull.Value
                                GRD_Details.ActiveRow.Cells("Department_Desc").Value = DBNull.Value
                            Else
                                Do
                                    GRD_Details.PerformAction(UltraGridAction.NextCell)
                                Loop Until GRD_Details.DisplayLayout.Bands(0).Columns(GRD_Details.ActiveCell.Column.Index).CellActivation = Activation.AllowEdit

                                GRD_Details.PerformAction(UltraGridAction.EnterEditMode)
                            End If
                        End If
                    End If
                ElseIf GRD_Details.ActiveRow.Cells("Remarks").Activated = True Then
                    GRD_Details.PerformAction(UltraGridAction.NextRow)
                    GRD_Details.ActiveRow.Cells("Emp_Code").Selected = True
                    GRD_Details.ActiveRow.Cells("Emp_Code").Activate()
                    GRD_Details.PerformAction(UltraGridAction.EnterEditMode)
                Else
                    Do
                        GRD_Details.PerformAction(UltraGridAction.NextCell)
                    Loop Until GRD_Details.DisplayLayout.Bands(0).Columns(GRD_Details.ActiveCell.Column.Index).CellActivation = Activation.AllowEdit

                    GRD_Details.PerformAction(UltraGridAction.EnterEditMode)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sClear()
        DTS_Details.Rows.Clear()
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
            " Select TimeOff.Code,                                               " & vbCrLf &
            "        Emp_Code,                                                   " & vbCrLf &
            "        Employees.DescA as Emp_Desc,                                " & vbCrLf &
            "        FromDate,                                                   " & vbCrLf &
            "        ToDate,                                                     " & vbCrLf &
            "        TimeOff_Type as TimeOff_Type,                               " & vbCrLf &
            "        TimeOff.Remarks,                                            " & vbCrLf &
            "        Users.DescA as User_Desc,                                   " & vbCrLf &
            "        Entry_Date,                                                 " & vbCrLf &
            "        TimeOff.Real_DaysOff                                        " & vbCrLf &
            "                                                                    " & vbCrLf &
            "        From TimeOff Left Join Employees                            " & vbCrLf &
            "        On   TimeOff.Emp_Code     = Employees.code                  " & vbCrLf &
            "        And  TimeOff.Company_Code = Employees.Company_Code          " & vbCrLf &
            "                                                                    " & vbCrLf &
            "        Left Join Users                                             " & vbCrLf &
            "        On TimeOff.User_Code = Users.code                           " & vbCrLf &
            "        And TimeOff.Company_Code = Users.Company_Code               " & vbCrLf &
            "                                                                    " & vbCrLf &
            "        Where 1 = 1                                                 " & vbCrLf &
            " And   (FromDate >= " & vFromDate & " Or " & vFromDate & " Is NULL) " & vbCrLf &
            " And    ToDate < " & vToDate_PlusOneDay &
            " And    TimeOff.Company_Code = " & vCompanyCode

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)

                'Code
                DTS_Summary.Rows(vRowCounter)("Ser") = vSqlReader("Code")

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

                'FromDate
                If IsDBNull(vSqlReader("FromDate")) = False Then
                    DTS_Summary.Rows(vRowCounter)("FromDate") = vSqlReader("FromDate")
                Else
                    DTS_Summary.Rows(vRowCounter)("FromDate") = Nothing
                End If

                'ToDate
                If IsDBNull(vSqlReader("ToDate")) = False Then
                    DTS_Summary.Rows(vRowCounter)("ToDate") = vSqlReader("ToDate")
                Else
                    DTS_Summary.Rows(vRowCounter)("ToDate") = Nothing
                End If

                'TimeOff_Type
                If IsDBNull(vSqlReader("TimeOff_Type")) = False Then
                    DTS_Summary.Rows(vRowCounter)("TimeOff_Type") = vSqlReader("TimeOff_Type")
                Else
                    DTS_Summary.Rows(vRowCounter)("TimeOff_Type") = Nothing
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                Else
                    DTS_Summary.Rows(vRowCounter)("Remarks") = Nothing
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

                'Real_DaysOff
                If IsDBNull(vSqlReader("Real_DaysOff")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Real_DaysOff") = vSqlReader("Real_DaysOff")
                Else
                    DTS_Summary.Rows(vRowCounter)("Real_DaysOff") = Nothing
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
            Dim vFromDate As String
            Dim vToDate As String


            Dim vSqlString As String
            Grd_Summary.UpdateData()
            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
            For Each vRow In Grd_Summary.Rows

                vFromDate = " '" & Format(vRow.Cells("FromDate").Value, "MM-dd-yyyy") & "' "
                vToDate = " '" & Format(vRow.Cells("ToDate").Value, "MM-dd-yyyy") & "' "
                Dim vDateDiff As String = DateDiff(DateInterval.Day, vRow.Cells("FromDate").Value, vRow.Cells("ToDate").Value) + 1

                If vRow.Cells("DML").Value = "U" Then

                    If CDate(vRow.Cells("FromDate").Value) > CDate(vRow.Cells("ToDate").Value) Then
                        vcFrmLevel.vParentFrm.sForwardMessage("172", Me)
                        vRow.Cells("FromDate").Selected = True
                        Exit Sub
                    End If

                    If Not fValidate_Insert_TimeOff(CDate(vRow.Cells("FromDate").Value), CDate(vRow.Cells("ToDate").Value), vRow.Cells("EMP_Code").Value, vRow.Cells("Ser").Value) Then
                        vcFrmLevel.vParentFrm.sForwardMessage("177", Me)
                        vRow.Cells("FromDate").Selected = True
                        Exit Sub
                    End If

                    If Not fValidate_Insert_PublicHoliday(CDate(vRow.Cells("FromDate").Value), CDate(vRow.Cells("ToDate").Value)) Then
                        vcFrmLevel.vParentFrm.sForwardMessage("178", Me)
                        vRow.Cells("FromDate").Selected = True
                        Exit Sub
                    End If

                    ' If the time off is paid, check remaining annual balance
                    If vRow.Cells("TimeOff_Type").Value.ToString() = "P" Then
                        vSqlString = $" SELECT dbo.fn_Get_Week_TimeOff_Days_In_Month({vFromDate}, {vToDate}, {vCompanyCode}) "
                        Dim vWeekTimeOffDays As Integer = cControls.fReturnValue(vSqlString, Me.Name)
                        Dim vDateDiffDays As Integer = vDateDiff - vWeekTimeOffDays
                        Dim vCurrentYear As Integer = CDate(vRow.Cells("FromDate").Value).Year

                        Dim vRemainingDays As Integer = fRemaining_Annual_TimeOff_Days(vCurrentYear, vRow.Cells("Emp_Code").Value, vRow.Cells("Ser").Text)

                        ' If not enough paid days left, show message and block the request
                        If Not (vRemainingDays - vDateDiffDays >= 0) Then
                            vcFrmLevel.vParentFrm.sForwardMessage("179", Me)
                            vRow.Cells("TimeOff_Type").Selected = True
                            Exit Sub
                        End If
                    End If

                    vSqlString = " Update TimeOff " &
                                 " Set Remarks      = '" & vRow.Cells("Remarks").Text & "', " &
                                 "     FromDate     = " & vFromDate & ", " &
                                 "     ToDate       = " & vToDate & ", " &
                                 "     Emp_Code     = '" & vRow.Cells("Emp_Code").Value & "', " &
                                 "     TimeOff_Type = '" & vRow.Cells("TimeOff_Type").Value & "', " &
                                 "     Real_DaysOff =  " & vDateDiff & " - dbo.fn_Get_Week_TimeOff_Days_In_Month(" & vFromDate & ", " & vToDate & ", " & vCompanyCode & ")" &
                                 "                                         " &
                                 " Where Code    = " & vRow.Cells("Ser").Text &
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
                        Grd_Summary.ActiveRow.Cells("EMP_Code").Selected = True
                        Grd_Summary.ActiveRow.Cells("EMP_Code").Activate()
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
    Private Sub GRD_Summary_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) _
        Handles Grd_Summary.ClickCellButton

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
                    " Delete From TimeOff " &
                    " Where  1 = 1 " &
                    " And    Company_Code = " & vCompanyCode &
                    " And    Code        = '" & sender.ActiveRow.Cells("Ser").Value & "'"

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

            DTS_Details.Rows.Clear()

            Dim i As Integer = 0
            Dim vBasic_WorkTime_min As Int16 = 510
            Dim vDay_WorkTime_Min, vOverTime_Min As Int16
            Dim vHour, vMin As Int16
            Dim vFinal_OverTime As DateTime

            Dim vDate() As String

            Dim vRow As UltraGridRow
            For Each vRow In vGrd.Rows

                DTS_Details.Rows.Add()

                DTS_Details.Rows(i)("Emp_Code") = vRow.Cells("AC-No.").Text
                DTS_Details.Rows(i)("Emp_Desc") = vRow.Cells("Name").Text

                vDate = vRow.Cells("Date").Text.Split("/")
                vRow.Cells("Date").Value = vDate(1) & "-" & vDate(0) & "-" & vDate(2)

                DTS_Details.Rows(i)("TDate") = vRow.Cells("Date").Value

                vRow.Cells("Clock In").Value = vRow.Cells("Clock In").Text.Replace("ص", "AM")
                vRow.Cells("Clock In").Value = vRow.Cells("Clock In").Text.Replace("م", "PM")

                DTS_Details.Rows(i)("SignIn") = vRow.Cells("Clock In").Text

                vRow.Cells("Clock Out").Value = vRow.Cells("Clock Out").Text.Replace("ص", "AM")
                vRow.Cells("Clock Out").Value = vRow.Cells("Clock Out").Text.Replace("م", "PM")

                DTS_Details.Rows(i)("SignOut") = vRow.Cells("Clock Out").Text

                vRow.Cells("Work Time").Value = vRow.Cells("Work Time").Text.Replace("ص", "AM")
                vRow.Cells("Work Time").Value = vRow.Cells("Work Time").Text.Replace("م", "PM")

                DTS_Details.Rows(i)("NetHours") = vRow.Cells("Work Time").Value

                Dim vReg As New Regex("\d+")
                Dim vTime_Split As String() = vRow.Cells("Work Time").Text.Split(":") 'to split the time into hours and minutes
                Dim vMinutes = vReg.Match(vTime_Split(1)) 'to erase any chars in minutes

                vDay_WorkTime_Min = (vTime_Split(0) * 60) + vMinutes.ToString 'Here will get the total workTime by minutes
                vOverTime_Min = vDay_WorkTime_Min - 510
                vHour = Math.Truncate(vOverTime_Min / 60)
                vMin = Math.Abs(vOverTime_Min) Mod 60

                If vOverTime_Min > 0 Then
                    vFinal_OverTime = vHour & ":" & vMin
                    DTS_Details.Rows(i)("OverTime") = "1-1-1900 " & vFinal_OverTime
                Else
                    vFinal_OverTime = Math.Abs(vHour) & ":" & vMin
                    DTS_Details.Rows(i)("LessTime") = "1-1-1900 " & vFinal_OverTime
                End If


                If Not IsDBNull(DTS_Details.Rows(i)("NetHours_Num")) Then
                    DTS_Details.Rows(i)("OverTime_Num") = DTS_Details.Rows(i)("NetHours_Num") - 8.5
                End If

                DTS_Details.Rows(i)("DML") = "I"

                i += 1
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Btn_ImportFromExcel_Click(sender As Object, e As EventArgs)
        If GRD_Details.Rows.Count > 0 Then
            MessageBox.Show("لا يمكن الاستيراد من الاكسل الان")
            Exit Sub
        End If

        sImportFromExcel()
    End Sub

    Private Sub Txt_Back_Click(sender As Object, e As EventArgs) Handles Txt_Back.Click
        Tab_Main.Tabs("Tab_Summary").Selected = True
    End Sub

    Private Sub GRD_Details_ClickCellButton(sender As Object, e As CellEventArgs) Handles GRD_Details.ClickCellButton
        GRD_Details.ActiveRow.Delete(False)
    End Sub

    Private Function fValidate_Insert_PublicHoliday(ByVal pStartDate As String, ByVal pEndDate As String, Optional pDataKey As Int16 = 0)

        Dim vSqlString As String =
            " from  Public_Holidays " &
            " Where StartDate    <= '" & Format(CDate(pEndDate), "MM-dd-yyyy") & "' " &
            " And   EndDate      >= '" & Format(CDate(pStartDate), "MM-dd-yyyy") & "' " &
            " And   Ser          <>  " & pDataKey &
            " And   Company_Code  =  " & vCompanyCode


        If cControls.fIsExist(vSqlString, Me.Name) Then
            Return False
        End If

        Return True

    End Function

    Private Function fValidate_Insert_TimeOff(ByVal pStartDate As String, ByVal pEndDate As String, ByVal pEmpCode As Integer, Optional pCode As Integer = 0)

        Dim vSqlString As String =
            " from  TimeOff          " &
            " Where Emp_Code      =  " & pEmpCode &
            " And   FromDate     <= '" & Format(CDate(pEndDate), "MM-dd-yyyy") & "' " &
            " And   ToDate       >= '" & Format(CDate(pStartDate), "MM-dd-yyyy") & "' " &
            " And   Company_Code  =  " & vCompanyCode &
            " And   Code         <>  " & pCode

        If cControls.fIsExist(vSqlString, Me.Name) Then
            Return False
        End If

        Return True

    End Function

    Private Function fRemaining_Annual_TimeOff_Days(ByVal pYear As Integer, ByVal pEmpCode As Integer, Optional pCode As Integer = 0)

        Dim vSqlString As String =
        $" SELECT dbo.fn_Get_Remaining_Paid_TimeOff_For_Year({pYear}, {pEmpCode}, {vCompanyCode}, {pCode}) "

        Return cControls.fReturnValue(vSqlString, Me.Name)

    End Function

End Class