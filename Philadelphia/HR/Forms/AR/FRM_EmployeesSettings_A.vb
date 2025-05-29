Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinTree
Imports Infragistics.Win.UltraWinEditors
Imports Infragistics.Win.UltraWinGrid

Public Class FRM_EmployeesSettings_A
#Region " Variables Declaration                                                         "
    Dim vBankMaster As String = "NI"
    Dim vSqlStatment(0) As String
    Dim vFocus As String
    Dim vSortedType As New SortedList
    Dim vSortedEffect As New SortedList
    Dim vSelected As Boolean = False
    Dim vCheckNodesChange As Boolean
    Dim MyStack As New Stack
    Dim vcFrmlevel As New cFrmLevelVariables_A
    Dim vWeekly_TimeOff As String = "N"
#End Region
#Region " My Form                                                                       "
#Region " Form Level                                                                    "

    Private Sub FRM_FinancialSetup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try

            If Not vcFrmlevel Is Nothing Then

                vcFrmlevel.vParentFrm = Me.ParentForm

                If Tab_Main.Tabs("Tab_Nationality").Selected Then
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_ContractTypes").Selected Then
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_Departments").Selected Then
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                End If

            End If

        Catch ex As Exception

            'MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub FRM_FinancialSetupL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sQueryNationality()
    End Sub
    Private Sub FRM_FinancialSetupL_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        Try
            If fSaveAll(True) = False Then
                e.Cancel = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sFillSqlStatmentArray(ByVal vSqlstring As String)
        If vSqlStatment(UBound(vSqlStatment)) = "" Then
            vSqlStatment(UBound(vSqlStatment)) = vSqlstring
        Else
            ReDim Preserve vSqlStatment(UBound(vSqlStatment) + 1)
            vSqlStatment(UBound(vSqlStatment)) = vSqlstring
        End If
    End Sub
    Private Sub sEmptySqlStatmentArray()
        ReDim vSqlStatment(0)
    End Sub

#End Region
#Region " DataBase                                                                      "
#Region " Query                                                                         "
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pItemCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)
        If Tab_Main.Tabs("Tab_Banks").Selected Then
            'sQueryBankMain(pRecPos, pItemCode, pIsGoTo)
        End If
    End Sub
#End Region
#Region " Save                                                                          "
    Private Function fIsSaveNeeded() As Boolean
        Dim vRow As UltraGridRow
        If Tab_Main.Tabs("Tab_Nationality").Selected Then
            For Each vRow In Grd_Nationality.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_ContractTypes").Selected Then
            For Each vRow In Grd_ContractTypes.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_Departments").Selected Then
            For Each vRow In Grd_Departments.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_BankCodes").Selected Then
            For Each vRow In Grd_BankCodes.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_DeductionsTypes").Selected Then
            For Each vRow In Grd_DeductionsTypes.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_Systems").Selected Then
            For Each vRow In GRD_HR_Systems.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_Performance").Selected Then
            For Each vRow In GRD_Performance.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_Week_TimeOff").Selected Then
            If vWeekly_TimeOff = "U" Then
                Return True
            End If
        End If
        Return False
    End Function
    Private Function fCheckMainNodesChange() As Boolean
        'Dim vTreeNode As TreeNode
        'For Each vTreeNode In Tre_Adjust.Nodes
        '    If vTreeNode.Tag = "I" Or vTreeNode.Tag = "U" Then
        '        Return True
        '    End If
        '    If vTreeNode.Nodes.Count > 0 Then
        '        fCheckChildNodesChange(vTreeNode)
        '        If vCheckNodesChange = True Then
        '            Return True
        '        End If
        '    End If
        'Next
    End Function
    Private Function fCheckChildNodesChange(ByVal pTreeNode As TreeNode) As Boolean
        Dim vTreeNode As TreeNode
        For Each vTreeNode In pTreeNode.Nodes
            If vTreeNode.Tag = "I" Or vTreeNode.Tag = "U" Then
                vCheckNodesChange = True
                Return True
            End If

            If vTreeNode.Nodes.Count > 0 Then
                fCheckChildNodesChange(vTreeNode)
            End If
        Next
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        If fIsSaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        Dim vRowCounter As Integer
        If pAskMe = True Then
            If vcFrmlevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                Select Case Tab_Main.SelectedTab.Key
                    Case "Tab_Nationality"
                        If fValidateNationality() Then
                            sSaveNationality()
                        Else
                            Return False
                        End If
                    Case "Tab_ContractTypes"
                        If fValidateContractTypes() Then
                            sSaveContractTypes()
                        Else
                            Return False
                        End If
                    Case "Tab_BankCodes"
                        If fValidateBankCode() Then
                            sSaveBankCode()
                        Else
                            Return False
                        End If
                    Case "Tab_Departments"
                        If fValidateDepartments() Then
                            sSaveDepartments()
                        Else
                            Return False
                        End If
                    Case "Tab_DeductionsTypes"
                        If fValidate_DeductionsTypes() Then
                            sSave_DeductionsTypes()
                        Else
                            Return False
                        End If
                    Case "Tab_Systems"
                        If fValidate_HR_Systems() Then
                            sSave_HR_Systems()
                        Else
                            Return False
                        End If
                    Case "Tab_Performance"
                        If fValidate_Performance() Then
                            sSave_Performance()
                        Else
                            Return False
                        End If
                    Case "Tab_Week_TimeOff"
                        sSave_Weekend()
                End Select
            Else
                sSetFlagsUpdate()
                Return True
            End If
        Else
            Select Case Tab_Main.SelectedTab.Key
                Case "Tab_Nationality"
                    If fValidateNationality() Then
                        sSaveNationality()
                    Else
                        Return False
                    End If
                Case "Tab_ContractTypes"
                    If fValidateContractTypes() Then
                        sSaveContractTypes()
                    Else
                        Return False
                    End If
                Case "Tab_Departments"
                    If fValidateDepartments() Then
                        sSaveDepartments()
                    Else
                        Return False
                    End If
                Case "Tab_BankCodes"
                    If fValidateBankCode() Then
                        sSaveBankCode()
                    Else
                        Return False
                    End If
                Case "Tab_DeductionsTypes"
                    If fValidate_DeductionsTypes() Then
                        sSave_DeductionsTypes()
                    Else
                        Return False
                    End If
                Case "Tab_Systems"
                    If fValidate_HR_Systems() Then
                        sSave_HR_Systems()
                    Else
                        Return False
                    End If
                Case "Tab_Performance"
                    If fValidate_Performance() Then
                        sSave_Performance()
                    Else
                        Return False
                    End If
                Case "Tab_Week_TimeOff"
                    sSave_Weekend()
            End Select
        End If

        vRowCounter = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            sEmptySqlStatmentArray()

            If Not vcFrmlevel Is Nothing Then
                vcFrmlevel.vParentFrm.sForwardMessage("7", Me)
            End If

            sSetFlagsUpdate()
            Return True
        End If
    End Function
    Private Sub sSetFlagsUpdate()
        If Tab_Main.Tabs("Tab_Nationality").Selected Then
            sQueryNationality()
        ElseIf Tab_Main.Tabs("Tab_ContractTypes").Selected Then
            sQueryContractTypes()
        ElseIf Tab_Main.Tabs("Tab_Departments").Selected Then
            sQueryDepartments()
        ElseIf Tab_Main.Tabs("Tab_BankCodes").Selected Then
            sQueryBankCodes()
        ElseIf Tab_Main.Tabs("Tab_DeductionsTypes").Selected Then
            sQuery_DeductionsTypes()
        ElseIf Tab_Main.Tabs("Tab_Systems").Selected Then
            sQuery_HR_Systems()
        ElseIf Tab_Main.Tabs("Tab_Performance").Selected Then
            sQuery_Performance()
        ElseIf Tab_Main.Tabs("Tab_Week_TimeOff").Selected Then
            vWeekly_TimeOff = "N"
        End If
    End Sub
    Private Sub sUpdateMainNodes()
        'Dim vTreeNode As TreeNode
        'For Each vTreeNode In Tre_Adjust.Nodes
        '    If vTreeNode.Tag = "I" Or vTreeNode.Tag = "U" Then
        '        vTreeNode.Tag = "N"
        '    End If
        '    If vTreeNode.Nodes.Count > 0 Then
        '        sUpdateChildNodes(vTreeNode)
        '    End If
        'Next
    End Sub
    Private Sub sUpdateChildNodes(ByVal pTreeNode As TreeNode)
        Dim vTreeNode As TreeNode
        For Each vTreeNode In pTreeNode.Nodes
            If vTreeNode.Tag = "I" Or vTreeNode.Tag = "U" Then
                vTreeNode.Tag = "N"
            End If
            If vTreeNode.Nodes.Count > 0 Then
                sUpdateChildNodes(vTreeNode)
            End If
        Next
    End Sub
#End Region
#Region " Delete                                                                        "
    Public Sub sDelete()

        Dim vSqlString As String
        If Tab_Main.Tabs("Tab_Nationality").Selected Then
            If Not Grd_Nationality.ActiveRow Is Nothing Then
                If Grd_Nationality.ActiveRow.Cells("DML").Value = "N" Or Grd_Nationality.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From Nationality " &
                                     " Where  Code = " & Grd_Nationality.ActiveRow.Cells("Ser").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_Nationality.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_Nationality.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_ContractTypes").Selected Then
            If Not Grd_ContractTypes.ActiveRow Is Nothing Then
                If Grd_ContractTypes.ActiveRow.Cells("DML").Value = "N" Or Grd_ContractTypes.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From Contract_Types " &
                                     " Where Code = " & Grd_ContractTypes.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_ContractTypes.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_ContractTypes.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_Departments").Selected Then
            If Not Grd_Departments.ActiveRow Is Nothing Then
                If Grd_Departments.ActiveRow.Cells("DML").Value = "N" Or Grd_Departments.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From Departments " &
                                     " Where  Code = " & Grd_Departments.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_Departments.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_Departments.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_BankCodes").Selected Then
            If Not Grd_BankCodes.ActiveRow Is Nothing Then
                If Grd_BankCodes.ActiveRow.Cells("DML").Value = "N" Or Grd_BankCodes.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From BankCodes " &
                                     " Where  Code = " & Grd_BankCodes.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_BankCodes.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_BankCodes.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_DeductionsTypes").Selected Then
            If Not Grd_DeductionsTypes.ActiveRow Is Nothing Then
                If Grd_DeductionsTypes.ActiveRow.Cells("DML").Value = "N" Or Grd_DeductionsTypes.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From DeductionsTypes " &
                                     " Where  Code = " & Grd_DeductionsTypes.ActiveRow.Cells("Ser").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_DeductionsTypes.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_DeductionsTypes.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_Systems").Selected Then
            If Not GRD_HR_Systems.ActiveRow Is Nothing Then
                If GRD_HR_Systems.ActiveRow.Cells("DML").Value = "N" Or GRD_HR_Systems.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From HR_Systems " &
                                     " Where  Code = '" & GRD_HR_Systems.ActiveRow.Cells("Ser").Value & "'" &
                                     " And Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            GRD_HR_Systems.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    GRD_HR_Systems.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_Performance").Selected Then
            If Not GRD_Performance.ActiveRow Is Nothing Then
                If GRD_Performance.ActiveRow.Cells("DML").Value = "N" Or GRD_Performance.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From Performance_Types " &
                                     " Where  Code = '" & GRD_Performance.ActiveRow.Cells("Code").Value & "'" &
                                     " And Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            GRD_Performance.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    GRD_Performance.ActiveRow.Delete(False)
                End If
            End If
        End If

    End Sub
#End Region
#End Region
#Region " New Record                                                                    "
    Public Sub sNewRecord()
        If Tab_Main.Tabs("Tab_Banks").Selected Then
            'sNewBankRecord()
        End If
    End Sub
#End Region
#Region " Tab Managment                                                                 "
    Private Sub Tab_Main_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Main.SelectedTabChanging
        Try
            If fSaveAll(True) = False Then
                e.Cancel = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Tab_Main_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        Try
            If Not vcFrmlevel.vParentFrm Is Nothing Then
                If Tab_Main.Tabs("Tab_Nationality").Selected Then
                    sQueryNationality()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_ContractTypes").Selected Then
                    sQueryContractTypes()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_Departments").Selected Then
                    sQueryDepartments()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_BankCodes").Selected Then
                    sQueryBankCodes()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_DeductionsTypes").Selected Then
                    sQuery_DeductionsTypes()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_Systems").Selected Then
                    sQuery_HR_Systems()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_Performance").Selected Then
                    sQuery_Performance()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_Week_TimeOff").Selected Then
                    sQuery_Weekend()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#End Region

#Region " Nationality                                                                   "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryNationality()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code,        " & vbCrLf &
                                      "        DescA,       " & vbCrLf &
                                      "        Remarks,     " & vbCrLf &
                                      "        Insurance    " & vbCrLf &
                                      "                     " & vbCrLf &
                                      " From Nationality    " & vbCrLf &
                                      " Where 1 = 1         " & vbCrLf &
                                      " And Company_Code =  " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Nationality.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Nationality.Rows.SetCount(vRowCounter + 1)

                'Ser
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_Nationality.Rows(vRowCounter)("Ser") = vSqlReader("Code")
                End If

                'Item
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_Nationality.Rows(vRowCounter)("Item") = vSqlReader("DescA")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_Nationality.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                'Insurance
                If IsDBNull(vSqlReader("Insurance")) = False Then
                    DTS_Nationality.Rows(vRowCounter)("Insurance") = vSqlReader("Insurance")
                End If

                DTS_Nationality.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

#Region " Save                                                                          "
    Private Function fValidateNationality() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Nationality.Rows
            If vRow.Cells("Item").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("35", Me)
                vRow.Cells("Item").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveNationality()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_Nationality.UpdateData()

        For Each vRow In Grd_Nationality.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Nationality " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Nationality       (       Code,          Company_Code,                    DescA,                              Remarks,                                     Insurance )" &
                             " Values                        ( " & vGetCode & "," & vCompanyCode & ",'" & vRow.Cells("Item").Text & "', '" & vRow.Cells("Remarks").Text & "', " & fIsNull(vRow.Cells("Insurance").Value, "NULL") & " )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update Nationality     " &
                             "                        " &
                             " Set    DescA        = '" & vRow.Cells("Item").Text & "', " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "', " &
                             "        Insurance    =  " & fIsNull(vRow.Cells("Insurance").Value, "NULL") &
                             "                        " &
                             " Where  Code         =  " & vRow.Cells("Ser").Value &
                             " And    Company_Code =  " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If

        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub GRD_Nationality_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Nationality.CellChange
        If Grd_Nationality.ActiveRow.Cells("DML").Value = "NI" Then
            Grd_Nationality.ActiveRow.Cells("DML").Value = "I"
        ElseIf Grd_Nationality.ActiveRow.Cells("DML").Value = "N" Then
            Grd_Nationality.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_Nationality_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Nationality.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_Nationality_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Nationality.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_Nationality_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Grd_Nationality.KeyPress
        If Grd_Nationality.ActiveCell IsNot Nothing AndAlso Grd_Nationality.ActiveCell.Column.Key = "Insurance" Then
            ' Allow digits, control characters (like Backspace), and one dot
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
                e.Handled = True ' Cancel the keypress
            End If

            ' Prevent more than one dot
            If e.KeyChar = "." AndAlso Grd_Nationality.ActiveCell.Text.Contains(".") Then
                e.Handled = True
            End If
        End If

    End Sub

#End Region

#End Region

#Region " Contract Types                                                                "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryContractTypes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA, Remarks From Contract_Types Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_ContractTypes.Rows.Clear()
            Do While vSqlReader.Read
                DTS_ContractTypes.Rows.SetCount(vRowCounter + 1)
                'Code
                If vSqlReader.IsDBNull(0) = False Then
                    DTS_ContractTypes.Rows(vRowCounter)("Code") = vSqlReader(0)
                End If
                'Item
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_ContractTypes.Rows(vRowCounter)("Item") = vSqlReader(1)
                End If
                'Remarks
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_ContractTypes.Rows(vRowCounter)("Remarks") = vSqlReader(2)
                End If
                DTS_ContractTypes.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region

#Region " Save                                                                          "
    Private Function fValidateContractTypes() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_ContractTypes.Rows
            If vRow.Cells("Item").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("35", Me)
                vRow.Cells("Item").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveContractTypes()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_ContractTypes.UpdateData()

        For Each vRow In Grd_ContractTypes.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Contract_Types " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Contract_Types (       Code,           Company_Code,                   DescA,                              Remarks            )" &
                             " Values                     ( " & vGetCode & ", " & vCompanyCode & ", '" & vRow.Cells("Item").Text & "', '" & vRow.Cells("Remarks").Text & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update Contract_Types  " &
                             "                        " &
                             " Set    DescA        = '" & vRow.Cells("Item").Text & "', " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "' " &
                             "                        " &
                             " Where  Code         =  " & vRow.Cells("Code").Value &
                             " And    Company_Code =  " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If

        Next

    End Sub
#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub GRD_ContractTypes_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_ContractTypes.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_ContractTypes_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_ContractTypes.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_ContractTypes_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_ContractTypes.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub
#End Region

#End Region

#Region " Departments                                                                   "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryDepartments()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA, Remarks From Departments Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Departments.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Departments.Rows.SetCount(vRowCounter + 1)
                'Code
                If vSqlReader.IsDBNull(0) = False Then
                    DTS_Departments.Rows(vRowCounter)("Code") = vSqlReader(0)
                End If
                'Item
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Departments.Rows(vRowCounter)("Item") = vSqlReader(1)
                End If
                'Remarks
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Departments.Rows(vRowCounter)("Remarks") = vSqlReader(2)
                End If
                DTS_Departments.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region

#Region " Save                                                                          "
    Private Function fValidateDepartments() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Departments.Rows
            If vRow.Cells("Item").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("35", Me)
                vRow.Cells("Item").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveDepartments()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_Departments.UpdateData()

        For Each vRow In Grd_Departments.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Departments " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Departments (      Code,          Company_Code,                     DescA,                             Remarks             )" &
                             " Values                  (" & vGetCode & "," & vCompanyCode & ",'" & vRow.Cells("Item").Text & "', '" & vRow.Cells("Remarks").Text & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update Departments  " &
                             "                     " &
                             " Set    DescA     = '" & vRow.Cells("Item").Text & "',   " &
                             "        Remarks   = '" & vRow.Cells("Remarks").Text & "' " &
                             "                     " &
                             " Where  Code      =  " & vRow.Cells("Code").Value & "    " &
                             " And Company_Code =  " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If
        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub GRD_Departments_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Departments.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_Departments_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Departments.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_Departments_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Departments.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub

#End Region

#End Region

#Region " BankCode                                                                      "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryBankCodes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA, Remarks From BankCodes Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_BankCodes.Rows.Clear()
            Do While vSqlReader.Read
                DTS_BankCodes.Rows.SetCount(vRowCounter + 1)
                'Code
                If vSqlReader.IsDBNull(0) = False Then
                    DTS_BankCodes.Rows(vRowCounter)("Code") = vSqlReader(0)
                End If
                'Item
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_BankCodes.Rows(vRowCounter)("Item") = vSqlReader(1)
                End If
                'Remarks
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_BankCodes.Rows(vRowCounter)("Remarks") = vSqlReader(2)
                End If
                DTS_BankCodes.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidateBankCode() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_BankCodes.Rows
            If vRow.Cells("Item").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("35", Me)
                vRow.Cells("Item").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveBankCode()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_BankCodes.UpdateData()

        For Each vRow In Grd_BankCodes.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From BankCodes " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into BankCodes  (        Code,          Company_Code,                    DescA,                              Remarks            )" &
                             " Values                 ( " & vGetCode & ", " & vCompanyCode & ",'" & vRow.Cells("Item").Text & "', '" & vRow.Cells("Remarks").Text & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update BankCodes       " &
                             "                        " &
                             " Set    DescA        = '" & vRow.Cells("Item").Text & "', " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "' " &
                             "                        " &
                             " Where  Code         =  " & vRow.Cells("Code").Value &
                             " And    Company_Code =  " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If

        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub GRD_BankCode_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_BankCodes.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_BankCode_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_BankCodes.KeyUp
        If e.KeyData = Keys.Delete Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_BankCode_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_BankCodes.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub
#End Region

#End Region

#Region " Deductions Types                                                              "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQuery_DeductionsTypes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select  DeductionsTypes.Code,  " &
                                      "         DeductionsTypes.DescA, " &
                                      "         DeductionsTypes.Remarks  " &
                                      "                                " &
                                      "         From DeductionsTypes   " &
                                      "                          " &
                                      " Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_DeductionsTypes.Rows.Clear()
            Do While vSqlReader.Read
                DTS_DeductionsTypes.Rows.SetCount(vRowCounter + 1)

                'Ser
                If vSqlReader.IsDBNull(0) = False Then
                    DTS_DeductionsTypes.Rows(vRowCounter)("Ser") = vSqlReader(0)
                End If

                'Item
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_DeductionsTypes.Rows(vRowCounter)("DescA") = vSqlReader(1)
                End If

                'Remarks
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_DeductionsTypes.Rows(vRowCounter)("Remarks") = vSqlReader(2)
                Else
                    DTS_DeductionsTypes.Rows(vRowCounter)("Remarks") = ""
                End If

                DTS_DeductionsTypes.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

#Region " Save                                                                          "
    Private Function fValidate_DeductionsTypes() As Boolean

        Dim vRow As UltraGridRow
        For Each vRow In Grd_DeductionsTypes.Rows
            If vRow.Cells("DescA").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("35", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If
        Next

        Return True
    End Function
    Private Sub sSave_DeductionsTypes()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_DeductionsTypes.UpdateData()

        For Each vRow In Grd_DeductionsTypes.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From DeductionsTypes " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into DeductionsTypes   (      Code,           Company_Code,                     DescA,                              Remarks )" &
                             " Values                        (" & vGetCode & ", " & vCompanyCode & ",'" & vRow.Cells("DescA").Text & "', '" & vRow.Cells("Remarks").Text & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update DeductionsTypes " &
                             "                        " &
                             " Set    DescA        = '" & vRow.Cells("DescA").Text & "', " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "' " &
                             "                        " &
                             " Where  Code         =  " & vRow.Cells("Ser").Value &
                             " And    Company_Code =  " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If
        Next

    End Sub
#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub Grd_DeductionsTypes_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_DeductionsTypes.CellChange
        If Grd_DeductionsTypes.ActiveRow.Cells("DML").Value = "NI" Then
            Grd_DeductionsTypes.ActiveRow.Cells("DML").Value = "I"
        ElseIf Grd_DeductionsTypes.ActiveRow.Cells("DML").Value = "N" Then
            Grd_DeductionsTypes.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub Grd_DeductionsTypes_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_DeductionsTypes.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub Grd_DeductionsTypes_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_DeductionsTypes.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub

#End Region

#End Region

#Region " HR Systems                                                                    "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQuery_HR_Systems()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select  Code,            " &
                                      "         DescA,           " &
                                      "         Daily_Hours,     " &
                                      "         TimeOff_Days,    " &
                                      "         OverTime_Ratio,  " &
                                      "         Remarks          " &
                                      "                          " &
                                      " From    HR_Systems       " &
                                      "                          " &
                                      " Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_HR_Systems.Rows.Clear()
            Do While vSqlReader.Read
                DTS_HR_Systems.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_HR_Systems.Rows(vRowCounter)("Ser") = vSqlReader("Code")
                End If

                'DescA
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_HR_Systems.Rows(vRowCounter)("Desc_A") = vSqlReader("DescA")
                End If

                'Daily_Hours
                If IsDBNull(vSqlReader("Daily_Hours")) = False Then
                    DTS_HR_Systems.Rows(vRowCounter)("Daily_Hours") = vSqlReader("Daily_Hours")
                End If

                'TimeOff_Days
                If IsDBNull(vSqlReader("TimeOff_Days")) = False Then
                    DTS_HR_Systems.Rows(vRowCounter)("TimeOff_Days") = vSqlReader("TimeOff_Days")
                End If

                'OverTime 
                If IsDBNull(vSqlReader("OverTime_Ratio")) = False Then
                    DTS_HR_Systems.Rows(vRowCounter)("OverTime_Ratio") = vSqlReader("OverTime_Ratio")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_HR_Systems.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                DTS_HR_Systems.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidate_HR_Systems() As Boolean

        Dim vRow As UltraGridRow
        For Each vRow In GRD_HR_Systems.Rows
            If vRow.Cells("Desc_A").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("4", Me)
                vRow.Cells("Desc_A").Selected = True
                Return False
            End If

            If vRow.Cells("Daily_Hours").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("173", Me)
                vRow.Cells("Daily_Hours").Selected = True
                Return False
            End If

            If vRow.Cells("TimeOff_Days").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("174", Me)
                vRow.Cells("TimeOff_Days").Selected = True
                Return False
            End If

            If vRow.Cells("OverTime_Ratio").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("176", Me)
                vRow.Cells("OverTime_Ratio").Selected = True
                Return False
            End If
        Next

        Return True
    End Function
    Private Sub sSave_HR_Systems()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetSerial As Integer

        GRD_HR_Systems.UpdateData()

        For Each vRow In GRD_HR_Systems.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From HR_Systems " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into HR_Systems   (         Code,            Company_Code,                      DescA,                              Daily_Hours,                              TimeOff_Days,                          OverTime_Ratio,                           Remarks )" &
                             " Values                   ('" & vGetSerial & "', " & vCompanyCode & ", '" & vRow.Cells("Desc_A").Text & "', " & vRow.Cells("Daily_Hours").Text & ", " & vRow.Cells("TimeOff_Days").Text & ", " & vRow.Cells("TimeOff_Days").Text & ", '" & vRow.Cells("Remarks").Text & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update HR_Systems       " &
                             " Set    DescA        = '" & vRow.Cells("Desc_A").Text & "',       " &
                             "        Daily_Hours   = " & vRow.Cells("Daily_Hours").Text & ",  " &
                             "        TimeOff_Days  = " & vRow.Cells("TimeOff_Days").Text & ", " &
                             "        OverTime_Ratio = " & vRow.Cells("OverTime_Ratio").Text & ", " &
                             "        Remarks       = '" & vRow.Cells("Remarks").Text & "'       " &
                             "                                                                   " &
                             " Where  Code          = " & vRow.Cells("Ser").Value &
                             " And    Company_Code  = " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If
        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub GRD_HR_Systems_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles GRD_HR_Systems.CellChange
        If GRD_HR_Systems.ActiveRow.Cells("DML").Value = "NI" Then
            GRD_HR_Systems.ActiveRow.Cells("DML").Value = "I"
        ElseIf GRD_HR_Systems.ActiveRow.Cells("DML").Value = "N" Then
            GRD_HR_Systems.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_HR_Systems_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRD_HR_Systems.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_HR_Systems_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles GRD_HR_Systems.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_HR_Systems_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GRD_HR_Systems.KeyPress
        If GRD_HR_Systems.ActiveCell IsNot Nothing AndAlso GRD_HR_Systems.ActiveCell.Column.Key = "Daily_Hours" Then
            ' Allow digits, control characters (like Backspace), and one dot
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
                e.Handled = True ' Cancel the keypress
            End If

            ' Prevent more than one dot
            If e.KeyChar = "." AndAlso GRD_HR_Systems.ActiveCell.Text.Contains(".") Then
                e.Handled = True
            End If
        End If

        If GRD_HR_Systems.ActiveCell IsNot Nothing AndAlso GRD_HR_Systems.ActiveCell.Column.Key = "TimeOff_Days" Then
            ' Allow digits, control characters (like Backspace), and one dot
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True ' Cancel the keypress
            End If
        End If
    End Sub
#End Region

#End Region

#Region " Performance                                                                    "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQuery_Performance()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select  Code,                  " &
                                      "         DescA,                 " &
                                      "         Remarks                " &
                                      "                                " &
                                      " From    Performance_Types      " &
                                      "                                " &
                                      " Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Performance.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Performance.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_Performance.Rows(vRowCounter)("Code") = vSqlReader("Code")
                End If

                'DescA
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_Performance.Rows(vRowCounter)("DescA") = vSqlReader("DescA")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_Performance.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                DTS_Performance.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidate_Performance() As Boolean

        Dim vRow As UltraGridRow
        For Each vRow In GRD_Performance.Rows
            If vRow.Cells("DescA").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("4", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If
        Next

        Return True
    End Function
    Private Sub sSave_Performance()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetSerial As Integer

        GRD_Performance.UpdateData()

        For Each vRow In GRD_Performance.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Performance_Types " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Performance_Types   (         Code,            Company_Code,                      DescA,                               Remarks ) " &
                             " Values                          ('" & vGetSerial & "', " & vCompanyCode & ", '" & vRow.Cells("DescA").Text & "', '" & vRow.Cells("Remarks").Text & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update Performance_Types       " &
                             " Set    DescA                = '" & vRow.Cells("DescA").Text & "', " &
                             "        Remarks              = '" & vRow.Cells("Remarks").Text & "' " &
                             "                                                                    " &
                             " Where  Code          = " & vRow.Cells("Code").Value &
                             " And    Company_Code  = " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If
        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub GRD_Performance_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles GRD_Performance.CellChange
        If GRD_Performance.ActiveRow.Cells("DML").Value = "NI" Then
            GRD_Performance.ActiveRow.Cells("DML").Value = "I"
        ElseIf GRD_Performance.ActiveRow.Cells("DML").Value = "N" Then
            GRD_Performance.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_Performance_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRD_Performance.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_Performance_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles GRD_Performance.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub

#End Region

#End Region

#Region " Weekend                                                                       "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQuery_Weekend()

        Try

            Dim vsqlCommand As New SqlCommand

            vsqlCommand.Connection = cControls.vSqlConn
            vsqlCommand.CommandText = " Select  Day_Of_Week,           " &
                                      "         Is_Checked             " &
                                      "                                " &
                                      " From    Week_Day_Off           " &
                                      "                                " &
                                      " Where 1 = 1                    " &
                                      " And   Company_Code = " & vCompanyCode

            cControls.vSqlConn.Open()

            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader

            Do While vSqlReader.Read

                For Each ctlr As Object In GroupBox_Weekend.Controls
                    If TypeOf ctlr Is CheckBox Then

                        If ctlr.Tag = vSqlReader("Day_Of_Week") Then
                            ctlr.Checked = vSqlReader("Is_Checked")
                            Exit For
                        End If

                    End If
                Next
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region " Save                                                                          "
    Private Sub sSave_Weekend()
        Try
            sEmptySqlStatmentArray()

            Dim vSqlString As String

            For Each ctlr As Object In GroupBox_Weekend.Controls
                If TypeOf ctlr Is CheckBox Then

                    vSqlString = " Update Week_Day_Off    " &
                                 " Set    Is_Checked    = '" & ctlr.Checked & "' " &
                                 "                         " &
                                 " Where  Day_Of_Week   = '" & ctlr.Tag & "' " &
                                 " And    Company_Code  =  " & vCompanyCode

                    sFillSqlStatmentArray(vSqlString)

                End If
            Next

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try
    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub CHK_ChangeAll_CheckedChanged(sender As Object, e As EventArgs) _
        Handles Chk_Saturday.CheckedChanged, Chk_Friday.CheckedChanged,
        Chk_Monday.CheckedChanged, Chk_Wednesday.CheckedChanged,
        Chk_Tuesday.CheckedChanged, Chk_Sunday.CheckedChanged,
        Chk_Thursday.CheckedChanged

        vWeekly_TimeOff = "U"

    End Sub

#End Region

#End Region

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub


End Class