Imports System.Data.SqlClient
Public Class Frm_EmployeeDefinition_L

#Region " Declaration                                                                    "
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As Object = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".cFrmLevelVariables_" & vLang)
    Dim vSqlStatment(0) As String
    Dim vIsAdmin As String
#End Region
#Region " Form Level                                                                     "
#Region " My Form                                                                        "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'sQuerySummary()
        'sQueryUser(vUsrCode)
        sLoadProfiles()
        'sLoadSalesTypes()
        'sLoadBoxes()
        'sLoadStores()
        sLoadBranches()

        vMasterBlock = "NI"
        Txt_FndBy_IsActive.SelectedIndex = 0

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
        vcFrmLevel.vParentFrm = Me.ParentForm

        Tab_Main.Tabs("Tab_Details").Selected = True

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

        'Dim vTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
        'vTool = vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Chk_Help")
        'sIsHelpEnabled(vTool.Checked)
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
    Private Sub Txt_All_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
        Handles Txt_Desc.KeyUp, Txt_address.KeyUp, Txt_DealType.KeyUp, _
        Txt_FirstTimeDeal.KeyUp, Txt_Remarks.KeyUp, Txt_Email1.KeyUp, Txt_Email2.KeyUp, _
         Txt_FTel.KeyUp, Txt_MTel.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Desc.ValueChanged, Txt_address.ValueChanged, Txt_DealType.ValueChanged, _
            Txt_FirstTimeDeal.ValueChanged, Txt_Remarks.ValueChanged, Txt_Email1.ValueChanged, _
            Txt_FTel.ValueChanged, Txt_MTel.ValueChanged, Txt_UserName.ValueChanged, _
            Txt_Password.ValueChanged, Txt_Email2.ValueChanged, Txt_Profiles.ValueChanged, _
            Txt_Box.ValueChanged, Txt_Store.ValueChanged, Txt_Branch.ValueChanged, _
            Txt_SalesType.ValueChanged, Txt_CostCenter.ValueChanged, _
            Chk_IsActive.CheckedChanged, Chk_CanEditInAllRecords.CheckedChanged

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If

        If sender.Name = "Chk_IsActive" Then
            If Chk_IsActive.Checked Then
                Chk_IsActive.Tag = "Y"
                Chk_IsActive.Text = "نشط"
            Else
                Chk_IsActive.Tag = "N"
                Chk_IsActive.Text = "غير نشط"
            End If
        End If
    End Sub
    Private Sub CHK_IsSuperVisor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles Chk_StopDeal.CheckedChanged, Chk_IsSalesMan.CheckedChanged

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If

        If sender.Checked Then
            sender.Tag = "Y"
        Else
            sender.Tag = "N"
        End If
    End Sub
#End Region
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fIfsaveNeeded() As Boolean
        If vMasterBlock = "I" Or vMasterBlock = "U" Then
            Return True
        End If
        Return False
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        If fIfsaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        If pAskMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                If fSaveValidation() = True Then
                    sSaveMain()
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Else
            If fSaveValidation() = True Then
                sSaveMain()
            Else
                Return False
            End If
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            vMasterBlock = "N"
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            Return True
        End If
    End Function
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
            vSQlcommand.CommandText = _
            " With MyEmployees as ( Select Code, " & _
            "                          DescA, " & _
            "                          Address, " & _
            "                          Nature, " & _
            "                          IsSalesMan, " & _
            "                          DealType, " & _
            "                          FirstTimeDeal, " & _
            "                          Remarks, " & _
            "                          Email1, " & _
            "                          Email2, " & _
            "                          Profile, " & _
            "                          Box_Code,     " & _
            "                          Store_Code,   " & _
            "                          CostCenter_Code,   " & _
            "                          Branch_Code,     " & _
            "                          FTel, " & _
            "                          MTel, " & _
            "                          StopDeal, " & _
            "                          UserName, " & _
            "                          Password, " & _
            "                          IsNull(IsActive, 'Y') as IsActive, " & _
            "                          CanEditInAllRecords, " & _
            "                          ROW_Number() Over (Order By Code) RecPos " & _
            "                          From Employees)     " & _
            "                          Select * From MyEmployees  " & _
            "                          Where 1 = 1 " & _
                                      vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(22) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(22))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(22))

                'Code
                Txt_Code.Text = Trim(vSqlReader(0))

                'Name
                Txt_Desc.Text = Trim(vSqlReader(1))

                'Address
                If vSqlReader.IsDBNull(2) = False Then
                    Txt_address.Text = Trim(vSqlReader(2))
                Else
                    Txt_address.Text = ""
                End If

                ''Nature
                'If vSqlReader.IsDBNull(3) = False Then
                '    Dim vValueList As Infragistics.Win.ValueListItem
                '    For Each vValueList In Txt_Nature.Items
                '        If vValueList.DataValue = Trim(vSqlReader(3)) Then
                '            Txt_Nature.SelectedItem = vValueList
                '        End If
                '    Next
                'End If

                'IsSalesMan
                If vSqlReader.IsDBNull(4) = False Then
                    If Trim(vSqlReader(4)) = "Y" Then
                        Chk_IsSalesMan.Checked = True
                    Else
                        Chk_IsSalesMan.Checked = False
                    End If
                Else
                    Chk_IsSalesMan.Checked = False
                End If

                'DealType
                If vSqlReader.IsDBNull(5) = False Then
                    Txt_DealType.Value = Trim(vSqlReader(5))
                Else
                    Txt_DealType.SelectedIndex = -1
                End If

                'FirstTimeDeal
                If vSqlReader.IsDBNull(6) = False Then
                    Txt_FirstTimeDeal.Text = Trim(vSqlReader(6))
                Else
                    Txt_FirstTimeDeal.Text = Nothing
                End If

                'Remarks
                If vSqlReader.IsDBNull(7) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader(7))
                Else
                    Txt_Remarks.Text = ""
                End If

                'Email1
                If vSqlReader.IsDBNull(8) = False Then
                    Txt_Email1.Text = Trim(vSqlReader(8))
                Else
                    Txt_Email1.Text = ""
                End If

                'Email2
                If vSqlReader.IsDBNull(9) = False Then
                    Txt_Email2.Text = Trim(vSqlReader(9))
                Else
                    Txt_Email2.Text = ""
                End If

                'Profiles
                If vSqlReader.IsDBNull(10) = False Then
                    Txt_Profiles.Value = Trim(vSqlReader(10))
                Else
                    Txt_Profiles.SelectedIndex = -1
                End If

                'Box_Code
                If vSqlReader.IsDBNull(11) = False Then
                    Txt_Box.Value = Trim(vSqlReader(11))
                Else
                    Txt_Box.SelectedIndex = -1
                End If

                'Store_Code
                If vSqlReader.IsDBNull(12) = False Then
                    Txt_Store.Value = vSqlReader(12)
                Else
                    Txt_Store.SelectedIndex = -1
                End If

                'CostCenter_Code
                'If vSqlReader.IsDBNull(13) = False Then
                '    Txt_CostCenter.Value = vSqlReader(13)
                'Else
                '    Txt_CostCenter.SelectedIndex = -1
                'End If

                'Store_Code
                If vSqlReader.IsDBNull(14) = False Then
                    Txt_Branch.Value = vSqlReader(14)
                Else
                    Txt_Branch.SelectedIndex = -1
                End If

                'FTel
                If vSqlReader.IsDBNull(15) = False Then
                    Txt_FTel.Text = Trim(vSqlReader(15))
                Else
                    Txt_FTel.Text = ""
                End If

                'MTel
                If vSqlReader.IsDBNull(16) = False Then
                    Txt_MTel.Text = Trim(vSqlReader(16))
                Else
                    Txt_MTel.Text = ""
                End If

                'StopDeal
                If vSqlReader.IsDBNull(17) = False Then
                    Chk_StopDeal.Tag = Trim(vSqlReader(17))
                    If Chk_StopDeal.Tag = "Y" Then
                        Chk_StopDeal.Checked = True
                    Else
                        Chk_StopDeal.Checked = False
                    End If
                End If

                'UserName
                If vSqlReader.IsDBNull(18) = False Then
                    Txt_UserName.Text = vSqlReader(18)
                Else
                    Txt_UserName.Text = ""
                End If

                'PassWord
                If vSqlReader.IsDBNull(19) = False Then
                    Txt_Password.Text = vSqlReader(19)
                Else
                    Txt_Password.Text = ""
                End If

                'IsActive
                If vSqlReader.IsDBNull(20) = False Then
                    If vSqlReader(20) = "N" Then
                        Chk_IsActive.Checked = False
                    Else
                        Chk_IsActive.Checked = True
                    End If
                End If

                'CanEditInAllRecords
                If vSqlReader.IsDBNull(21) = False Then
                    If vSqlReader(21) = "N" Then
                        Chk_CanEditInAllRecords.Checked = False
                    Else
                        Chk_CanEditInAllRecords.Checked = True
                    End If
                Else
                    Chk_CanEditInAllRecords.Checked = False
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
    End Sub
    Private Sub sLoadSalesTypes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Code, DescA  From Sales_Types "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_DealType.Items.Clear()
            Do While vSqlReader.Read
                Txt_DealType.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoadStores()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Code, DescA  From Stores "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Store.Items.Clear()
            Do While vSqlReader.Read
                Txt_Store.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoadProfiles()
        Txt_Profiles.Items.Clear()
        Dim vsqlCommand As New SqlClient.SqlCommand
        vsqlCommand.CommandText = " Select Code, DescA From Profiles Order By Code "

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
        Do While vSqlReader.Read
            Txt_Profiles.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()

    End Sub
    Private Sub sLoadBoxes()
        Txt_Box.Items.Clear()
        Dim vsqlCommand As New SqlClient.SqlCommand
        vsqlCommand.CommandText = " Select Code, DescA From Boxes Order By Code "

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

        Do While vSqlReader.Read
            Txt_Box.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub
    Private Sub sLoadBranches()
        Try
            Txt_Branch.Items.Clear()
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Branches.Code,                   " & _
                                      "        Branches.DescA                   " & _
                                      " From   Branches                         " & _
                                      " Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

            Do While vSqlReader.Read
                Txt_Branch.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            If Txt_Branch.Items.Count = 1 Then
                Txt_Branch.SelectedIndex = 0
            End If

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#Region " Delete                                                                         "
    Public Sub sDelete()
        If vMasterBlock = "I" Then
            sNewRecord()
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                Dim vSqlstring As String
                vSqlstring = " Delete From Employees Where Code = '" & Txt_Code.Text & "'"
                If cControls.fSendData(vSqlstring, Me.Name) > 0 Then
                    vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                    sNewRecord()
                End If
            End If
        End If
    End Sub
#End Region
#Region " Find                                                                           "
    Public Sub sFind()
        sOpenLov("Select Code, Name From Employees ", "الموظفين")
    End Sub
#End Region
#End Region
#Region " New Record                                                                     "
    Public Sub sNewRecord()
        Tab_Main.Tabs("Tab_Details").Selected = True
        sNewCode()
        Txt_Desc.Clear()
        Txt_address.Clear()
        Txt_DealType.Clear()
        Txt_FirstTimeDeal.Value = Nothing
        Txt_Remarks.Clear()
        Txt_Email1.Clear()
        Txt_Email2.Clear()

        Txt_Profiles.SelectedIndex = -1
        Txt_Box.SelectedIndex = -1
        Txt_Store.SelectedIndex = -1
        Txt_SalesType.SelectedIndex = -1
        Txt_CostCenter.SelectedIndex = -1
        Txt_Branch.SelectedIndex = -1

        Txt_FTel.Clear()
        Txt_MTel.Clear()
        Txt_UserName.Clear()
        Txt_Password.Clear()
        Chk_StopDeal.Checked = False
        Chk_IsSalesMan.Checked = False
        Chk_IsActive.Checked = True

        Chk_CanEditInAllRecords.Checked = False

        vMasterBlock = "NI"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")
    End Sub
    Private Sub sNewCode()
        Dim vSqlCommand As String
        vSqlCommand = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Employees "

        Txt_Code.Text = cControls.fReturnValue(vSqlCommand, Me.Name).PadLeft(6, "0")
    End Sub
#End Region
#Region " sOpenLov                                                                       "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String)
        vLovReturn1 = ""
        VLovReturn2 = ""
        Dim Frm_Lov As New FRM_LovGeneral_A(pSqlStatment, pTitle)
        Frm_Lov.ShowDialog()
        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            sQuery(pItemCode:=vLovReturn1)
        End If
    End Sub
#End Region
#Region " Tab Mangment                                                                   "
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
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
            sQuerySummary()
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
            If Grd_Summary.Selected.Rows.Count > 0 Then
                sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
            Else
                sNewRecord()
            End If
        End If
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
#Region " Help                                                                           "
    Public Sub sIsHelpEnabled(ByVal pEnabled As Boolean)
        UltraToolTipManager1.Enabled = pEnabled
    End Sub
#End Region
#End Region
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fSaveValidation() As Boolean
        If Txt_Desc.Text.Length = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("13", Me)
            Txt_Desc.Select()
            Return False
        End If

        If cControls.fCount_Rec(" From Employees Where DescA = '" & Trim(Txt_Desc.Text) & "' And Code <> '" & Trim(Txt_Code.Text) & "'") > 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("12", Me)
            Txt_Desc.Select()
            Return False
        End If

        If Not Chk_IsSalesMan.Checked Then
            If Txt_UserName.Text = "" Then
                vcFrmLevel.vParentFrm.sForwardMessage("14", Me)
                Txt_UserName.Select()
                Return False
            End If
            If Txt_Password.Text = "" Then
                vcFrmLevel.vParentFrm.sForwardMessage("15", Me)
                Txt_Password.Select()
                Return False
            End If
            If Txt_Profiles.SelectedIndex = -1 Then
                vcFrmLevel.vParentFrm.sForwardMessage("60", Me)
                Txt_Profiles.Select()
                Return False
            End If
        End If

        'If Chk_IsProvider.Checked = False And Chk_IsCustomer.Checked = False And Chk_IsEmployee.Checked = False Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("57", Me)
        '    Return False
        'End If
        Return True
    End Function
    Private Sub sSaveMain()
        If fSaveValidation() = False Then
            Return
        End If
        Dim vFirstTimeDeal, vFirstBalance, vDeduction, vDealType, vBox, vStore, vSalesType, vCostCenter, vProfile, vBranch, vCanEdit As String
        Dim vSqlCommand As String = ""

        If Not Txt_FirstTimeDeal.Value = Nothing Then
            vFirstTimeDeal = "'" & Format(Txt_FirstTimeDeal.Value, "MM-dd-yyyy") & "'"
        Else
            vFirstTimeDeal = "NULL"
        End If

        If Txt_DealType.Value = Nothing Then
            vDealType = "NULL"
        Else
            vDealType = "'" & Txt_DealType.Value & "'"
        End If

        If Txt_Store.Value = Nothing Then
            vStore = "NULL"
        Else
            vStore = "'" & Txt_Store.Value & "'"
        End If

        If Txt_Box.Value = Nothing Then
            vBox = "NULL"
        Else
            vBox = "'" & Txt_Box.Value & "'"
        End If

        If Txt_SalesType.Value = Nothing Then
            vSalesType = "NULL"
        Else
            vSalesType = "'" & Txt_SalesType.Value & "'"
        End If

        If Txt_CostCenter.Value = Nothing Then
            vCostCenter = "NULL"
        Else
            vCostCenter = "'" & Txt_CostCenter.Value & "'"
        End If

        If Txt_Profiles.SelectedIndex = -1 Then
            vProfile = "NULL"
        Else
            vProfile = "'" & Txt_Profiles.Value & "'"
        End If

        If Txt_Branch.Value = Nothing Then
            vBranch = "NULL"
        Else
            vBranch = "'" & Txt_Branch.Value & "'"
        End If

        If Chk_CanEditInAllRecords.Checked Then
            vCanEdit = "'Y'"
        Else
            vCanEdit = "'N'"
        End If

        If vMasterBlock = "I" Then

            sNewCode()

            vSqlCommand = " Insert Into Employees     (             Code,                              DescA,                      Address,                 DealType,         FirstTimeDeal,                    Remarks,                          Email1,                          Email2,                  Profile,          Box_Code,          Store_Code,     CostCenter_Code,      Branch_Code,              FTel,                          MTel,                        StopDeal,                   IsSalesMan,                  UserName,                  Password,                    IsActive,     CanEditInAllRecords) " & _
                                  " Values            ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_address.Text) & "'," & vDealType & ", " & vFirstTimeDeal & " , '" & Trim(Txt_Remarks.Text) & "', '" & Trim(Txt_Email1.Text) & "', '" & Trim(Txt_Email2.Text) & "',  " & vProfile & ", " & Trim(vBox) & ", " & vStore & ", " & vCostCenter & ", " & vBranch & ", '" & Trim(Txt_FTel.Text) & "', '" & Trim(Txt_MTel.Text) & "', '" & Chk_StopDeal.Tag & "', '" & Chk_IsSalesMan.Tag & "', '" & Txt_UserName.Text & "', '" & Txt_Password.Text & "', '" & Chk_IsActive.Tag & "', " & vCanEdit & " )"
            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Employees " & _
                          " Set   DescA                 = '" & Trim(Txt_Desc.Text) & "'," & _
                          "       Address               = '" & Trim(Txt_address.Text) & "'," & _
                          "       DealType              =  " & vDealType & ",    " & _
                          "       FirstTimeDeal         =  " & vFirstTimeDeal & ", " & _
                          "       Remarks               = '" & Trim(Txt_Remarks.Text) & "', " & _
                          "       Email1                = '" & Trim(Txt_Email1.Text) & "', " & _
                          "       Email2                = '" & Trim(Txt_Email2.Text) & "', " & _
                          "       Profile               = '" & Trim(Txt_Profiles.Value) & "', " & _
                          "       Store_Code            =  " & vStore & ", " & _
                          "       Box_Code              =  " & vBox & ", " & _
                          "       CostCenter_Code       =  " & vCostCenter & ", " & _
                          "       Branch_Code           =  " & vBranch & ", " & _
                          "       FTel                  = '" & Trim(Txt_FTel.Text) & "', " & _
                          "       MTel                  = '" & Trim(Txt_MTel.Text) & "', " & _
                          "       StopDeal              = '" & Chk_StopDeal.Tag & "',  " & _
                          "       IsSalesMan            = '" & Chk_IsSalesMan.Tag & "', " & _
                          "       UserName              = '" & Trim(Txt_UserName.Text) & "', " & _
                          "       PassWord              = '" & Trim(Txt_Password.Text) & "', " & _
                          "       IsActive              = '" & Chk_IsActive.Tag & "', " & _
                          "       CanEditInAllRecords   =  " & vCanEdit & _
                          " Where Code                  = '" & Txt_Code.Text & "'"
            sFillSqlStatmentArray(vSqlCommand)
        End If
    End Sub
#End Region
#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummary(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            Dim vCodeFilter, vDescFilter, vIsActive_Filter As String

            If pCode = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Code Like '%" & pCode & "%'"
            End If

            If pDesc = "" Then
                vDescFilter = ""
            Else
                vDescFilter = " And DescA Like '%" & pDesc & "%'"
            End If

            If Not Txt_FndBy_IsActive.SelectedItem Is Nothing Then
                If Txt_FndBy_IsActive.SelectedItem.DataValue = "All" Then
                    vIsActive_Filter = ""
                Else
                    vIsActive_Filter = " And IsNull(IsActive, 'Y') = 'Y' "
                End If
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " With MyEmployees as " & _
            "( Select Employees.Code, " & _
            "         Employees.DescA,                                             " & _
            "         FirstTimeDeal,                                     " & _
            "         ROW_Number() Over (Order By Employees.Code) as  RecPos " & _
            " From Employees          " & _
            " Where 1 = 1             " & _
            vCodeFilter & _
            vDescFilter & _
            vIsActive_Filter & _
            " ) " & _
            " Select * From MyEmployees  "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)
                DTS_Summary.Rows(vRowCounter)("Code") = Trim(vSqlReader(0))

                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Summary.Rows(vRowCounter)("DescA") = Trim(vSqlReader(1))
                End If

                'If vSqlReader.IsDBNull(2) = False Then
                '    DTS_Summary.Rows(vRowCounter)("DealType") = Trim(vSqlReader(2))
                'Else
                '    DTS_Summary.Rows(vRowCounter)("DealType") = Nothing
                'End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Summary.Rows(vRowCounter)("FirstTimeDeal") = Trim(vSqlReader(2))
                Else
                    DTS_Summary.Rows(vRowCounter)("FirstTimeDeal") = Nothing
                End If
                'If vSqlReader(4) = "Y" Then
                '    DTS_Summary.Rows(vRowCounter)("IsProvider") = True
                'Else
                '    DTS_Summary.Rows(vRowCounter)("IsProvider") = False
                'End If
                'If vSqlReader(5) = "Y" Then
                '    DTS_Summary.Rows(vRowCounter)("IsCustomer") = True
                'Else
                '    DTS_Summary.Rows(vRowCounter)("IsCustomer") = False
                'End If
                'If vSqlReader(6) = "Y" Then
                '    DTS_Summary.Rows(vRowCounter)("IsEmployee") = True
                'Else
                '    DTS_Summary.Rows(vRowCounter)("IsEmployee") = False
                'End If
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

    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged, Txt_FndBy_IsActive.ValueChanged

        sQuerySummary(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
    End Sub

    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick

        If Grd_Summary.Selected.Rows.Count > 0 Then
            sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
        Else
            sNewRecord()
        End If
        Tab_Main.Tabs("Tab_Details").Selected = True
    End Sub
#End Region

End Class