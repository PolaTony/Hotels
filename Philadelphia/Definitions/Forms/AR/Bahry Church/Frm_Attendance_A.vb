Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinEditors

Public Class Frm_Attendance_A
#Region " Declaration                                                                    "
    Dim vMasterBlock As String = "NI"
    Dim vDetailsBlock As String = "NI"

    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSqlStatment(0) As String
    Dim vFocus As String = "Master"
    Dim vSortedList As New SortedList
    Dim vQuery As String = "N"
#End Region
#Region " Form Level                                                                     "
#Region " My Form                                                                        "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            vMasterBlock = "NI"
            'sQuerySummaryMain()
            'Txt_TDate.Value = cControls.fReturnValue(" Select GetDate() ", Me.Name)

            Txt_EmpCode.Text = vUsrCode
            Txt_EmpDesc.Text = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & vUsrCode & "'", Me.Name)
            sNewCode()
            sLoad_CustomersTypes()

            Txt_SummaryDate.Value = cControls.fReturnValue(" Select GetDate() ", Me.Name)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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
    Private Sub FRM_Users_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            vcFrmLevel.vParentFrm = Me.ParentForm

            If Tab_Main.Tabs("Tab_Summary").Selected = True Then
                vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
            Else
                vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", True, False, "بحث")
            End If

            If vcFrmLevel.vRecPos > 0 Then
                vcFrmLevel.vParentFrm.sPrintRec(vcFrmLevel.vRecPos)
            Else
                vcFrmLevel.vParentFrm.sPrintRec("")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If fSaveAll(True) = False Then
            e.Cancel = True
        Else
            e.Cancel = False
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If
    End Sub
    Private Sub FRM_Users_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Me.ParentForm.MdiChildren.Length = 1 Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, False, "", True)
        End If
    End Sub
    Private Sub ToolBar_Main_ToolClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case Trim(e.Tool.Key)
            Case "Btn_FilterByDate"
                sQuerySummaryMain()
        End Select

    End Sub
#End Region
#Region " Tab Management                                                                 "
    Private Sub Tab_Main_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Main.SelectedTabChanging
        Try
            If Tab_Main.Tabs("Tab_Details").Selected = True Then
                If fSaveAll(True) = False Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Tab_Main_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        Try
            If Tab_Main.Tabs("Tab_Summary").Selected = True Then
                vcFrmLevel.vParentFrm = Me.ParentForm
                vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
                'sQuerySummaryMain()
            Else
                vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
                If Grd_Summary.Selected.Rows.Count > 0 Then
                    If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                        sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
                    Else
                        'sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
                    End If
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub sChangeTab()
        If Tab_Main.Tabs("Tab_Summary").Selected Then
            Tab_Main.Tabs("Tab_Details").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", True, False, "بحث")
        Else
            Tab_Main.Tabs("Tab_Summary").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
        End If
    End Sub
#End Region
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fIfsaveNeeded() As Boolean
        If vMasterBlock = "U" Then
            Return True
        End If

        If vDetailsBlock = "I" Then
            Return True
        End If

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        'If Txt_Status.Text = "مقفل" Then
        '    Return True
        'End If

        If fIfsaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        If pAskMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                sSaveMain()

                sSaveDetails()
            Else
                vMasterBlock = "NI"
                DTS_Details.Rows.Clear()
                Return True
            End If
        Else
            sSaveMain()

            sSaveDetails()
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            sSetFlagsUpdate()
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            Return True
        End If
    End Function
    Private Sub sSetFlagsUpdate()
        vMasterBlock = "N"
        vDetailsBlock = "N"

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            vRow.Cells("DML").Value = "N"
        Next
    End Sub
#End Region
#Region " Query                                                                          "
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
                    If vFetchRec > cControls.fCount_Rec(" From Attendance ") Then
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
            vFetchRec = cControls.fCount_Rec(" From Attendance ")
        End If

        Dim vFetchCondition As String
        If pItemCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And MyAttendance.Code = '" & Trim(pItemCode) & "'"
        End If

        vQuery = "Y"
        Try
            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText = _
            " With MyAttendance as                                         " & _
            "( Select Attendance.Code,                                     " & _
            "         Emp_Code,                                            " & _
            "         Employees.DescA,                                     " & _
            "         TDate,                                               " & _
            "         Attendance.Remarks,                                    " & _
            "         CustomerType,                                         " & _
            "         ROW_Number() Over (Order By Attendance.Code) as  RecPos " & _
            " From Attendance Inner Join Employees                     " & _
            " On   Attendance.Emp_Code = Employees.Code       )         " & _
            " Select * From MyAttendance                                   " & _
            " Where 1 = 1                                                 " & _
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(6) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(6))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(6))

                'Code
                Txt_Code.Text = Trim(vSqlReader(0))

                'Emp_Code
                If vSqlReader.IsDBNull(1) = False Then
                    Txt_EmpCode.Text = Trim(vSqlReader(1))
                Else
                    Txt_EmpCode.Text = ""
                End If

                'Emp_Desc
                If vSqlReader.IsDBNull(2) = False Then
                    Txt_EmpDesc.Text = Trim(vSqlReader(2))
                Else
                    Txt_EmpDesc.Text = ""
                End If

                'TDate
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_TDate.Text = Trim(vSqlReader(3))
                Else
                    Txt_TDate.Text = Nothing
                End If

                'Remarks
                If vSqlReader.IsDBNull(4) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader(4))
                Else
                    Txt_Remarks.Text = ""
                End If

                'Customer Type
                If vSqlReader.IsDBNull(5) = False Then
                    Txt_CustomersTypes.Value = vSqlReader(5)
                Else
                    Txt_CustomersTypes.SelectedIndex = -1
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Txt_CustomersTypes.ReadOnly = True

            sQueryDetails()

            vQuery = "N"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try

        vMasterBlock = "N"
        vDetailsBlock = "N"
    End Sub

    Private Sub sLoadMembers_New()
        Try

            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = _
                " Select Code, DescA  From Members "


            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vRow As UltraGridRow
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            Dim vRowCounter As Integer
            vRowCounter = 0
            DTS_Details.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Details.Rows.SetCount(vRowCounter + 1)
                DTS_Details.Rows(vRowCounter)("Customer_Code") = Trim(vSqlReader(0))
                DTS_Details.Rows(vRowCounter)("Customer_Desc") = Trim(vSqlReader(1))

                DTS_Details.Rows(vRowCounter)("Attendance") = False

                DTS_Details.Rows(vRowCounter)("SerNum") = DTS_Details.Rows(vRowCounter).Index + 1
                DTS_Details.Rows(vRowCounter)("DML") = "NI"
                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try
    End Sub
    Private Sub sLoad_CustomersTypes()
        Txt_CustomersTypes.Items.Clear()
        Dim vsqlCommand As New SqlClient.SqlCommand
        vsqlCommand.CommandText = " Select Code, DescA From Customers_Types Order By Code "

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

        Do While vSqlReader.Read
            Txt_CustomersTypes.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub
#End Region
#Region " Delete                                                                         "
    Public Sub sDelete()

        If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
            Dim vSqlstring As String
            If vMasterBlock = "I" Or vMasterBlock = "NI" Then
                sNewRecord()
                Exit Sub
            ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
                sEmptySqlStatmentArray()

                vSqlstring = _
                " Delete From Attendance_Details " & _
                " Where       ATT_Code       = '" & Trim(Txt_Code.Text) & "'"

                sFillSqlStatmentArray(vSqlstring)

                vSqlstring = _
                " Delete From Attendance Where Code = '" & Trim(Txt_Code.Text) & "' "

                sFillSqlStatmentArray(vSqlstring)

                If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then
                    sNewRecord()
                    vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                End If
            End If
        End If
    End Sub
#End Region
#Region " Find                                                                           "
    Public Sub sFind()
        sOpenLov("Select Code, Name From Users", "الموظفين")
    End Sub
#End Region
#End Region
#Region " New Record                                                                     "
    Public Sub sNewRecord()
        Try
            If fSaveAll(True) = False Then
                Return
            End If

            Tab_Main.Tabs("Tab_Details").Selected = True

            sNewCode()

            Txt_Classes.SelectedIndex = -1

            DTS_Details.Rows.Clear()

            Txt_TDate.Value = cControls.fReturnValue(" Select GetDate() ", Me.Name)

            Txt_EmpCode.Text = vUsrCode
            Txt_EmpDesc.Text = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & vUsrCode & "'", Me.Name)

            Txt_Remarks.Text = ""

            Txt_CustomersTypes.SelectedIndex = -1
            Txt_CustomersTypes.ReadOnly = False

            vMasterBlock = "I"
            vDetailsBlock = "NI"

            vcFrmLevel.vRecPos = 0
            vcFrmLevel.vParentFrm.sPrintRec("")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sNewCode()
        Dim vSqlCommand As String
        vSqlCommand = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Attendance "

        Txt_Code.Text = cControls.fReturnValue(vSqlCommand, Me.Name).PadLeft(4, "0")
    End Sub
#End Region
#Region " sOpenLov                                                                       "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String, Optional ByVal pTableName As String = "", Optional ByVal pAdditionalString As String = "")
        vLovReturn1 = ""
        VLovReturn2 = ""
        vLovReturn3 = ""
        Dim vPU As String
        If pTitle = "العملاء" Then
            Dim Frm_Lov As New FRM_LovTreeA(pSqlStatment, pTitle)
            Frm_Lov.ShowDialog()
            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                Grd_Details.ActiveRow.Cells("Customer_Code").Value = Trim(vLovReturn1)
                Grd_Details.ActiveRow.Cells("Customer_Desc").Value = Trim(VLovReturn2)

                Grd_Details.ActiveRow.Cells("Value").Value = DBNull.Value
                Grd_Details.ActiveRow.Cells("Remarks").Value = DBNull.Value

            End If
        Else
            'Dim Frm_Lov As New FRM_LovTreeL(pSqlStatment, pTitle, pTableName, pAdditionalString)
            'Frm_Lov.ShowDialog()
            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                If pTitle = "الموظفين" Then
                    'Txt_EmpCode.Text = vLovReturn1
                    'Txt_EmpDesc.Text = VLovReturn2
                ElseIf pTitle = "الفروع" Then
                    'Txt_BranchCode.Text = vLovReturn1
                    'Txt_BranchDesc.Text = VLovReturn2
                End If
            End If
        End If
    End Sub
#End Region
#Region " Print                                                                          "

#End Region
#End Region

#Region " Master                                                                         "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Sub sSaveMain()
        Try
            'If fValidateMain() = False Then
            '    Return
            'End If
            Dim vSqlCommand As String = ""
            Dim vDate As String

            If Not Txt_TDate.Value = Nothing Then
                vDate = "'" & Format(Txt_TDate.Value, "MM-dd-yyyy HH:mm") & "'"
            Else
                vDate = "NULL"
            End If

            If vMasterBlock = "I" Then

                sNewCode()

                vSqlCommand = " Insert Into Attendance       (              Code,                          Remarks,               TDate,                   Emp_Code,                        CustomerType          ) " & _
                              "                 Values       ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Remarks.Text) & "', " & vDate & ", '" & Trim(Txt_EmpCode.Text) & "', " & Txt_CustomersTypes.Value & " ) "
                sFillSqlStatmentArray(vSqlCommand)

            ElseIf vMasterBlock = "U" Then
                vSqlCommand = " Update   Attendance " & _
                              " Set   TDate             =  " & vDate & ", " & _
                              "       Emp_Code          = '" & Trim(Txt_EmpCode.Text) & "'," & _
                              "       Remarks           = '" & Trim(Txt_Remarks.Text) & "' " & _
                              " Where Code              = '" & Txt_Code.Text & "'"
                sFillSqlStatmentArray(vSqlCommand)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub TXT_All_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) Handles _
     Txt_Customers.EditorButtonClick
        Try
            If sender.name = "Txt_BranchCode" Then
                'sOpenLov(" Select Code, DescA From Branches ", "الفروع")
            ElseIf sender.name = "Txt_Customers" Then
                sOpenLov(" Select Code, DescA From Customers Where 1 = 1 ", "العملاء")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_TDate.ValueChanged, Txt_Remarks.ValueChanged

        Try
            If vMasterBlock = "NI" Then
                vMasterBlock = "I"
            ElseIf vMasterBlock = "N" Then
                vMasterBlock = "U"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#End Region
#Region " Details                                                                        "
#Region " Custmoers                                                                      "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Sub sSaveDetails()
        'If fValidateDetails() = False Then
        '    Return
        'End If
        Try
            Dim vRow As UltraGridRow
            Grd_Details.UpdateData()
            'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
            Dim vCounter As Integer = 0
            Dim vSqlString As String

            Dim vAttend As String

            For Each vRow In Grd_Details.Rows

                If vRow.Cells("Attendance").Text = True Then
                    vAttend = "'Y'"
                Else
                    vAttend = "'N'"
                End If

                If vDetailsBlock = "I" Then

                    vSqlString = " Insert Into Attendance_Details (             ATT_Code,                               Customer_Code,                                    Remarks,                   Attend  )" & _
                                   "                      Values  ('" & Trim(Txt_Code.Text) & "', '" & Trim(vRow.Cells("Customer_Code").Value) & "', '" & Trim(vRow.Cells("Remarks").Text) & "', " & vAttend & ")"

                    sFillSqlStatmentArray(vSqlString)

                Else
                    If vRow.Cells("DML").Value = "U" Then
                        'vSqlString = " Delete From Attendance_Details " & _
                        '             " Where   Student_Code = '" & vRow.Cells("Student_Code").Text & "' " & _
                        '             " And     Session_Code = '" & Txt_Sessions.Value & "' " & _

                        'sFillSqlStatmentArray(vSqlString)

                        vSqlString = " Update Attendance_Details " & _
                                     " Set    Remarks = '" & Trim(vRow.Cells("Remarks").Text) & "', " & _
                                     "        Attend  =  " & vAttend & _
                                     " Where  ATT_Code = '" & Trim(Txt_Code.Text) & "' " & _
                                     " And    Customer_Code = '" & Trim(vRow.Cells("Customer_Code").Value) & "' "

                        sFillSqlStatmentArray(vSqlString)

                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryDetails()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Customer_Code,                         " & _
            "        Customers.DescA,                       " & _
            "        Attend,                                " & _
            "        Attendance_Details.Remarks             " & _
            " From   Attendance_Details INNER JOIN Customers   " & _
            " ON     Customers.Code = Attendance_Details.Customer_Code  " & _
            " Where  ATT_Code = '" & Trim(Txt_Code.Text) & "'"

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Details.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Details.Rows.SetCount(vRowCounter + 1)
                DTS_Details.Rows(vRowCounter)("Customer_Code") = Trim(vSqlReader(0))
                DTS_Details.Rows(vRowCounter)("Customer_Desc") = Trim(vSqlReader(1))

                If vSqlReader.IsDBNull(2) = False Then
                    If vSqlReader(2) = "Y" Then
                        DTS_Details.Rows(vRowCounter)("Attendance") = True
                    Else
                        DTS_Details.Rows(vRowCounter)("Attendance") = False
                    End If
                Else
                    DTS_Details.Rows(vRowCounter)("Attend") = False
                End If

                DTS_Details.Rows(vRowCounter)("Remarks") = Trim(vSqlReader(3))
                DTS_Details.Rows(vRowCounter)("DML") = "N"

                DTS_Details.Rows(vRowCounter)("SerNum") = DTS_Details.Rows(vRowCounter).Index + 1

                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Details.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQueryDetails")
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub Grd_Details_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles Grd_Details.AfterRowInsert
        e.Row.Cells("SerNum").Value = e.Row.Index + 1
    End Sub
    Private Sub Grd_Details_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Details.CellChange
        Try
            If vDetailsBlock = "NI" Then
                vDetailsBlock = "I"
            ElseIf vDetailsBlock = "N" Then
                vDetailsBlock = "U"
            End If

            If sender.ActiveRow.Cells("DML").Value = "NI" Then
                sender.ActiveRow.Cells("DML").Value = "I"
            ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
                sender.ActiveRow.Cells("DML").Value = "U"
            End If

            Grd_Details.UpdateData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Grd_Details_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Details.KeyDown
        Try
            Grd_Details.UpdateData()

            If e.KeyData = Keys.Enter Then
                'If Grd_Details.ActiveRow.Cells("Customer_Code").Activated = True Then

                '    Grd_Details.PerformAction(UltraGridAction.PrevCell)
                '    Grd_Details.PerformAction(UltraGridAction.PrevCell)
                '    Grd_Details.PerformAction(UltraGridAction.EnterEditMode)
                'ElseIf Grd_Details.ActiveRow.Cells("Remarks").Activated = True Then
                Dim vCellName As String = Grd_Details.ActiveCell.Column.Key

                Grd_Details.PerformAction(UltraGridAction.NextRow)
                Grd_Details.ActiveRow.Cells(vCellName).Selected = True
                Grd_Details.ActiveRow.Cells(vCellName).Activate()
                Grd_Details.PerformAction(UltraGridAction.EnterEditMode)
            ElseIf e.KeyData = Keys.Up Then
                Dim vCellName As String = Grd_Details.ActiveCell.Column.Key

                Grd_Details.PerformAction(UltraGridAction.PrevRow)
                Grd_Details.ActiveRow.Cells(vCellName).Selected = True
                Grd_Details.ActiveRow.Cells(vCellName).Activate()
                Grd_Details.PerformAction(UltraGridAction.EnterEditMode)
            ElseIf e.KeyData = Keys.Down Then
                Dim vCellName As String = Grd_Details.ActiveCell.Column.Key

                Grd_Details.PerformAction(UltraGridAction.NextRow)
                Grd_Details.ActiveRow.Cells(vCellName).Selected = True
                Grd_Details.ActiveRow.Cells(vCellName).Activate()
                Grd_Details.PerformAction(UltraGridAction.EnterEditMode)
            End If
            'ElseIf e.KeyData = Keys.F12 Then
            'If Grd_Details.ActiveRow.Cells("Customer_Code").Activated = True Then
            '    sOpenLov(" Select Code, DescA From Customers Where 1 = 1 ", "العملاء")
            'End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GRD_Details_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Details.DoubleClick

    End Sub

#End Region
#End Region
#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummaryMain()
        Try
            'If DTS_Summary.Band.Columns.Count = 0 Then
            '    Return
            'End If
            Dim vDate As String

            Dim x As Integer = ToolBar_Main.Tools.Count
            If x > 0 Then
                Dim vStateButtonTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
                vStateButtonTool = ToolBar_Main.Tools("Btn_FilterByDate")
                If vStateButtonTool.Checked Then
                    vDate = " And Month(TDate) = '" & Txt_SummaryDate.DateTime.Month & "'" & _
                            " And Year(TDate) = '" & Txt_SummaryDate.DateTime.Year & "'"
                Else
                    vDate = ""
                End If
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Attendance.Code,                  " & _
            "        TDate,                 " & _
            "        Customers_Types.DescA,    " & _
            "        Attendance.Remarks                " & _
            " From   Attendance LEFT JOIN Customers_Types " & _
            " ON     Customers_Types.Code = Attendance.CustomerType " & _
            "                               " & _
            " Where 1 = 1                   " & _
            vDate & _
            " Order By TDate                "

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
                    DTS_Summary.Rows(vRowCounter)("CustomerType") = Trim(vSqlReader(2))
                Else
                    DTS_Summary.Rows(vRowCounter)("CustomerType") = ""
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("Remarks") = Trim(vSqlReader(3))
                Else
                    DTS_Summary.Rows(vRowCounter)("Remarks") = ""
                End If

                If Grd_Summary.Rows.Count > 0 Then
                    Grd_Summary.Rows(vRowCounter).Appearance.BackColor = Color.Wheat
                End If
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuerySummaryMain")
        End Try
    End Sub
    Private Sub sQuerySummaryDetails(ByVal pRow As UltraDataRow, ByVal pChildBand As UltraDataBand)
        Try
            Dim vChildRows As UltraDataRowsCollection = pRow.GetChildRows(pChildBand)
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Customers_Payment.Code,             " & _
            "        Customers_Payment.Customer_Code,    " & _
            "        Customers.DescA,                    " & _
            "        Customers_Payment.TValue,           " & _
            "        Customers_Payment.Remarks           " & _
            " From   Customers_Payment Inner Join Customers " & _
            " On     Customers_Payment.Customer_Code = Customers.Code " & _
            " Where  Month(TDate)           =  " & Txt_SummaryDate.DateTime.Month.ToString & _
            " And    Year(TDate)            =  " & Txt_SummaryDate.DateTime.Year.ToString & _
            " And    Customers.Branch_Code  = '" & Trim(pRow("Code")) & "'" & _
            " Order By Customers.DescA "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            'vChildRows.Clear()
            Do While vSqlReader.Read
                vChildRows.SetCount(vRowCounter + 1)
                vChildRows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                vChildRows(vRowCounter)("Customer_Code") = Trim(vSqlReader(1))
                vChildRows(vRowCounter)("Customer_Desc") = Trim(vSqlReader(2))
                If vSqlReader.IsDBNull(3) = False Then
                    vChildRows(vRowCounter)("Value") = Trim(vSqlReader(3))
                Else
                    vChildRows(vRowCounter)("Value") = Nothing
                End If
                If vSqlReader.IsDBNull(4) = False Then
                    vChildRows(vRowCounter)("Remarks") = Trim(vSqlReader(4))
                Else
                    vChildRows(vRowCounter)("Remarks") = ""
                End If
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQuerySummaryDetails")
        End Try
    End Sub
    Private Sub Grd_Summary_BeforeRowExpanded(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CancelableRowEventArgs) Handles Grd_Summary.BeforeRowExpanded
        'Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
        'Dim vRow As UltraDataRow = DTS_Summary.Rows(e.Row.Index)
        'sQuerySummaryDetails(vRow, vChildBand)
    End Sub
    Private Sub Txt_SummaryDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_SummaryDate.ValueChanged
        sQuerySummaryMain()
    End Sub
    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick
        If Grd_Summary.Selected.Rows.Count > 0 Then
            If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
            Else
                sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
            End If
        Else
            sNewRecord()
        End If
        Tab_Main.Tabs("Tab_Details").Selected = True
    End Sub
#End Region

    Private Sub Txt_CustomersTypes_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_CustomersTypes.ValueChanged
        If vQuery = "Y" Then
            Return
        End If

        If Txt_CustomersTypes.SelectedIndex <> -1 Then
            'sLoadCustomers_New()
        End If
    End Sub

    Private Sub Btn_Load_Members_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Load_Members.Click
        If vQuery = "Y" Then
            Return
        End If

        sLoadMembers_New()
    End Sub
End Class