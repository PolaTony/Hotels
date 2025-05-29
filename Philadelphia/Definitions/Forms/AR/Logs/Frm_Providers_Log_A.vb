Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinDataSource


Public Class Frm_Providers_Log_A
#Region " Declaration                                                                    "
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As New cFrmLevelVariables_A
    'Dim vcFrmLevel As Object = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".cFrmLevelVariables_" & vLang)
    Dim vSqlStatment(0) As String
    Dim vIsAdmin As String
    Dim vSortedList As New SortedList
    Dim vQuery As String = "N"
#End Region
#Region " Form Level                                                                     "
#Region " My Form                                                                        "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'sQuerySummary()
        'sQueryUser(vUsrCode)
        sLoadProvidersTypes()
        Try
            'Txt_PackUnit.Items.Clear()
            Dim vSQlcommand As New SqlCommand
            Dim vGenerateNewCode As String = ""

            vSQlcommand.CommandText = _
            " Select IsNull(AutomaticallyGenerateProviderCode, 'Y')   " & _
            " From Controls_PI "

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read
                If vSqlReader(0) = "Y" Then
                    vGenerateNewCode = "Y"
                Else
                    vGenerateNewCode = "N"
                End If
                'If vSqlReader(1) = "Y" Then
                '    Grd_ItemDetails.Rows.Band.Columns("SalesPrices").Hidden = False
                'Else
                '    Grd_ItemDetails.Rows.Band.Columns("SalesPrices").Hidden = True
                'End If
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            If vGenerateNewCode = "Y" Then
                Txt_Code.ReadOnly = True
                'Txt_Code.Appearance = Txt_Code.Appearances(0)
                sNewCode()
            Else
                Txt_Code.ReadOnly = False
            End If
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sLoadPackUnits")
        End Try

        'Tab_Main.Tabs("Tab_Details").Selected = True
        Txt_Desc.Select()
        vMasterBlock = "NI"
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

        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
            sQuerySummary()
        Else
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, True, True, True, True, "", False, False, "بحث")
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
        Handles Txt_Desc.KeyUp, Txt_address.KeyUp, Txt_Nature.KeyUp, Txt_NatureDetails.KeyUp, _
        Txt_FirstBalance.KeyUp, Txt_Deduction.KeyUp, Txt_DealType.KeyUp, Txt_FirstTimeDeal.KeyUp, _
        Txt_Remarks.KeyUp, Txt_Email1.KeyUp, Txt_Email2.KeyUp, Txt_Fax.KeyUp, Txt_FTel.KeyUp, _
        Txt_MTel.KeyUp, Txt_ContactPersonName.KeyUp, Txt_ContactPersonAddress.KeyUp, _
        Txt_ContactPersonFTel.KeyUp, Txt_ContactPersonMTel.KeyUp, Txt_ContactPersonRemarks.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Desc.TextChanged, Txt_address.ValueChanged, Txt_Nature.ValueChanged, Txt_NatureDetails.ValueChanged, _
    Txt_FirstBalance.ValueChanged, Txt_Deduction.ValueChanged, Txt_DealType.ValueChanged, _
    Txt_FirstTimeDeal.ValueChanged, Txt_FB_Type.ValueChanged, Txt_Remarks.ValueChanged, Txt_Email1.ValueChanged, _
    Txt_Email2.ValueChanged, Txt_Fax.ValueChanged, Txt_FTel.ValueChanged, Txt_MTel.ValueChanged, _
    Txt_ContactPersonName.ValueChanged, Txt_ContactPersonAddress.ValueChanged, _
    Txt_ContactPersonFTel.ValueChanged, Txt_ContactPersonMTel.ValueChanged, _
    Txt_ContactPersonRemarks.ValueChanged, Txt_ProviderType.ValueChanged, _
    Chk_StopPay.CheckedChanged, Chk_StopDealWithItems.CheckedChanged

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If

        If sender.name = "Chk_StopPay" Or sender.name = "Chk_StopDealWithItems" Then
            If sender.Checked = True Then
                sender.Tag = "Y"
            Else
                sender.Tag = "N"
            End If
        End If

        'If sender.Name = "Txt_ProviderType" Then
        '    If Txt_ProviderType.SelectedIndex <> -1 Then
        '        If vQuery = "N" Then
        '            Dim vSqlString As String
        '            vSqlString = " Select Count (*) + 1 From Providers Where ProviderType = " & Trim(Txt_ProviderType.Value)

        '            Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(4, "0") & "-" & Trim(Txt_ProviderType.Value)
        '        End If
        '    End If
        'End If

        'If vQuery = "N" Then
        '    If sender.Name = "Txt_Desc" Then
        '        'If Txt_Desc.Text.Length > 6 Then
        '        sFilterProviders(Txt_Desc.Text)
        '        'End If
        '    End If
        'End If

    End Sub
    Private Sub CHK_IsSuperVisor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles Chk_IsProvider.CheckedChanged, Chk_IsCustomer.CheckedChanged, Chk_IsEmployee.CheckedChanged, _
                 Chk_StopPay.CheckedChanged

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

        If sender.Name = "Chk_IsProvider" Then
            If sender.Checked = True Then
                Chk_IsCustomer.Checked = False
                Chk_IsEmployee.Checked = False
                Txt_Desc.ReadOnly = False
                sNewRecord()
            End If
        End If
        If sender.Name = "Chk_IsCustomer" Then
            If sender.Checked = True Then
                Chk_IsProvider.Checked = False
                Chk_IsEmployee.Checked = False
                Txt_Desc.ReadOnly = False
                sNewRecord()
            End If
        End If
        If sender.Name = "Chk_IsEmployee" Then
            If sender.Checked = True Then
                Chk_IsProvider.Checked = False
                Chk_IsCustomer.Checked = False
                Txt_Desc.ReadOnly = False
                sNewRecord()
            End If
        End If
    End Sub
    Private Sub ToolBar_Main_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case e.Tool.Key
            Case "Btn_ExportToExcel"
                sExportToExcel()
        End Select
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
        If vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Save").SharedProps.Enabled = False Then
            Return True
        End If

        If fIfsaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        If pAskMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                'If fSaveValidation() = True Then
                '    sSaveMain()
                'Else
                '    Return False
                'End If
            Else
                Return True
            End If
        Else
            'If fSaveValidation() = True Then
            '    sSaveMain()
            'Else
            '    Return False
            'End If
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            vMasterBlock = "N"
            'sNewRecord()
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            'Btn_Create_ProviderAccount.Enabled = True
            Return True
        End If
    End Function
#End Region
#Region " Query                                                                          "
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)
        If fSaveAll(True) = False Then
            Return
        End If

        Dim vFetchRec As Integer
        If pCode = Nothing Then
            If pIsGoTo = False Then
                If pRecPos = Nothing Then
                    vFetchRec = 1
                Else
                    vFetchRec = vcFrmLevel.vRecPos + pRecPos
                    If vFetchRec > cControls.fCount_Rec(" From Providers_Log Where Company_Code = " & vCompanyCode) Then
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
            vFetchRec = cControls.fCount_Rec(" From Providers_Log Where Company_Code = " & vCompanyCode)
        End If

        Dim vFetchCondition As String
        If pCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And MyProviders_Log.Code = '" & Trim(pCode) & "'"
        End If

        vQuery = "Y"
        Try

            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText = _
            " With MyProviders_Log as                           " & _
            " ( Select Providers_Log.Code,                      " & _
            "          Providers_Log.Provider_Code,             " & _
            "          Providers_Log.DescA as Provider_Desc,        " & _
            "          Providers_Log.Address, " & _
            "          Providers_Log.Nature, " & _
            "          Providers_Log.NatureDetails, " & _
            "          Providers_Log.Deduction, " & _
            "          Providers_Log.DealType, " & _
            "          Providers_Log.FirstTimeDeal, " & _
            "          Providers_Log.Remarks, " & _
            "          Providers_Log.Email1, " & _
            "          Providers_Log.Email2, " & _
            "          Providers_Log.Fax, " & _
            "          Providers_Log.FTel, " & _
            "          Providers_Log.MTel, " & _
            "          Providers_Log.StopDeal, " & _
            "          Providers_Log.StopPay, " & _
            "          Providers_Log.StopDealWithItems, " & _
            "          Financial_Definitions_Tree.Ser,  " & _
            "          Financial_Definitions_Tree.DescA as Act_Desc, " & _
            "          Providers_Log.FirstBalance,            " & _
            "          Providers_Log.FB_Type,                 " & _
            "          Providers_Log.ContactPersonName, " & _
            "          Providers_Log.ContactPersonAddress, " & _
            "          Providers_Log.ContactPersonFTel, " & _
            "          Providers_Log.ContactPersonMTel, " & _
            "          Providers_Log.ContactPersonRemarks, " & _
            "          Providers_Log.ProviderType,         " & _
            "                                " & _
            "          -- Log Details                                         " & vbCrLf & _
            "          Employees.DescA as Emp_Desc,                           " & _
            "          Providers_Log.TDate,                                       " & _
            "          Providers_Log.ComputerName,                                " & _
            "          Providers_Log.IPAddress,                                   " & _
            "          Providers_Log.Type,                                        " & _
            "          ROW_Number() Over (Order By Providers_Log.Code) RecPos " & _
            "                                " & _
            "          From Providers_Log LEFT JOIN Financial_Definitions_Tree         " & _
            "          ON Financial_Definitions_Tree.Ser = Providers_Log.Act_Ser  " & _
            "          LEFT JOIN Employees                                         " & _
            "          ON Employees.Code = Providers_Log.Emp_Code                       " & _
            "          Where Providers_Log.Company_Code = " & vCompanyCode & " ) " & _
            "          Select * From MyProviders_Log  " & _
            "          Where 1 = 1 " & _
                                      vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(33) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(33))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(33))

                'Code
                Txt_Code.Text = Trim(vSqlReader(1))

                'Name
                Txt_Desc.Text = Trim(vSqlReader(2))

                'Address
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_address.Text = Trim(vSqlReader(3))
                Else
                    Txt_address.Text = ""
                End If

                'Nature
                If vSqlReader.IsDBNull(4) = False Then
                    Dim vValueList As Infragistics.Win.ValueListItem
                    For Each vValueList In Txt_Nature.Items
                        If vValueList.DataValue = Trim(vSqlReader(4)) Then
                            Txt_Nature.SelectedItem = vValueList
                        End If
                    Next
                End If

                'NatureDetails
                If vSqlReader.IsDBNull(5) = False Then
                    Txt_NatureDetails.Value = Trim(vSqlReader(5))
                Else
                    Txt_NatureDetails.Value = Nothing
                End If

                'Deduction
                If vSqlReader.IsDBNull(6) = False Then
                    Txt_Deduction.Text = Trim(vSqlReader(6))
                Else
                    Txt_Deduction.Text = ""
                End If

                'DealType
                If vSqlReader.IsDBNull(7) = False Then
                    Dim vValueList As Infragistics.Win.ValueListItem
                    For Each vValueList In Txt_DealType.Items
                        If vValueList.DataValue = Trim(vSqlReader(7)) Then
                            Txt_DealType.SelectedItem = vValueList
                        End If
                    Next
                End If

                'FirstTimeDeal
                If vSqlReader.IsDBNull(8) = False Then
                    Txt_FirstTimeDeal.Text = Trim(vSqlReader(8))
                Else
                    Txt_FirstTimeDeal.Text = Nothing
                End If

                'Remarks
                If vSqlReader.IsDBNull(9) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader(9))
                Else
                    Txt_Remarks.Text = ""
                End If

                'Email1
                If vSqlReader.IsDBNull(10) = False Then
                    Txt_Email1.Text = Trim(vSqlReader(10))
                Else
                    Txt_Email1.Text = ""
                End If

                'Email2
                If vSqlReader.IsDBNull(11) = False Then
                    Txt_Email2.Text = Trim(vSqlReader(11))
                Else
                    Txt_Email2.Text = ""
                End If

                'Fax
                If vSqlReader.IsDBNull(12) = False Then
                    Txt_Fax.Text = Trim(vSqlReader(12))
                Else
                    Txt_Fax.Text = ""
                End If

                'FTel
                If vSqlReader.IsDBNull(13) = False Then
                    Txt_FTel.Text = Trim(vSqlReader(13))
                Else
                    Txt_FTel.Text = ""
                End If

                'MTel
                If vSqlReader.IsDBNull(14) = False Then
                    Txt_MTel.Text = Trim(vSqlReader(14))
                Else
                    Txt_MTel.Text = ""
                End If

                'StopDeal
                'If vSqlReader.IsDBNull(14) = False Then
                '    Chk_StopDeal.Tag = Trim(vSqlReader(14))
                '    If Chk_StopDeal.Tag = "Y" Then
                '        Chk_StopDeal.Checked = True
                '    Else
                '        Chk_StopDeal.Checked = False
                '    End If
                'Else
                '    Chk_StopDeal.Checked = False
                'End If

                'StopPay
                If vSqlReader.IsDBNull(16) = False Then
                    Chk_StopPay.Tag = Trim(vSqlReader(16))
                    If Chk_StopPay.Tag = "Y" Then
                        Chk_StopPay.Checked = True
                    Else
                        Chk_StopPay.Checked = False
                    End If
                Else
                    Chk_StopPay.Checked = False
                End If

                'StopDealWithItems
                If vSqlReader.IsDBNull(17) = False Then
                    Chk_StopDealWithItems.Tag = Trim(vSqlReader(17))
                    If Chk_StopDealWithItems.Tag = "Y" Then
                        Chk_StopDealWithItems.Checked = True
                    Else
                        Chk_StopDealWithItems.Checked = False
                    End If
                Else
                    Chk_StopDealWithItems.Checked = False
                End If

                'Act_Ser
                If vSqlReader.IsDBNull(18) = False Then
                    Txt_AccountCode.Text = Trim(vSqlReader(18))
                Else
                    Txt_AccountCode.Text = ""
                End If

                'Act_Desc
                If vSqlReader.IsDBNull(19) = False Then
                    Txt_AccountDesc.Text = Trim(vSqlReader(19))
                    'Btn_Create_ProviderAccount.Enabled = False
                Else
                    Txt_AccountDesc.Text = ""
                    'Btn_Create_ProviderAccount.Enabled = True
                End If

                'FirstBalance
                If vSqlReader.IsDBNull(20) = False Then
                    Txt_FirstBalance.Text = Trim(vSqlReader(20))
                Else
                    Txt_FirstBalance.Text = ""
                End If

                'FB_Type
                If vSqlReader.IsDBNull(21) = False Then
                    Txt_FB_Type.Value = Trim(vSqlReader(21))
                Else
                    Txt_FB_Type.Value = Nothing
                End If

                'ContactPersonName
                If vSqlReader.IsDBNull(22) = False Then
                    Txt_ContactPersonName.Text = Trim(vSqlReader(22))
                Else
                    Txt_ContactPersonName.Text = ""
                End If

                'ContactPersonAddress
                If vSqlReader.IsDBNull(23) = False Then
                    Txt_ContactPersonAddress.Text = Trim(vSqlReader(23))
                Else
                    Txt_ContactPersonAddress.Text = ""
                End If

                'ContactPersonFTel
                If vSqlReader.IsDBNull(24) = False Then
                    Txt_ContactPersonFTel.Text = Trim(vSqlReader(24))
                Else
                    Txt_ContactPersonFTel.Text = ""
                End If

                'ContactPersonMTel
                If vSqlReader.IsDBNull(25) = False Then
                    Txt_ContactPersonMTel.Text = Trim(vSqlReader(25))
                Else
                    Txt_ContactPersonMTel.Text = ""
                End If

                'ContactPersonRemarks
                If vSqlReader.IsDBNull(26) = False Then
                    Txt_ContactPersonRemarks.Text = Trim(vSqlReader(26))
                Else
                    Txt_ContactPersonRemarks.Text = ""
                End If

                'Provider Type
                If vSqlReader.IsDBNull(27) = False Then
                    Txt_ProviderType.Value = vSqlReader(27)
                Else
                    Txt_ProviderType.SelectedIndex = -1
                End If

                '-- Log Details
                'Emp_Desc
                If vSqlReader.IsDBNull(28) = False Then
                    Txt_EmployeeDesc.Text = Trim(vSqlReader(28))
                Else
                    Txt_EmployeeDesc.Text = ""
                End If

                If vSqlReader.IsDBNull(29) = False Then
                    Txt_TDate.Text = Trim(vSqlReader(29))
                Else
                    Txt_TDate.Text = Nothing
                End If

                If vSqlReader.IsDBNull(30) = False Then
                    Txt_ComputerName.Text = Trim(vSqlReader(30))
                Else
                    Txt_ComputerName.Text = ""
                End If

                If vSqlReader.IsDBNull(31) = False Then
                    Txt_IPAddress.Text = Trim(vSqlReader(31))
                Else
                    Txt_IPAddress.Text = ""
                End If

                If vSqlReader.IsDBNull(32) = False Then
                    If vSqlReader(32) = "C" Then
                        Txt_Type.Text = "انشاء"
                    ElseIf vSqlReader(32) = "U" Then
                        Txt_Type.Text = "تعديل"
                    ElseIf vSqlReader(32) = "D" Then
                        Txt_Type.Text = "الغاء"
                    End If
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Txt_Code.ReadOnly = True
            Txt_ProviderType.ReadOnly = True

            vQuery = "N"
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
    End Sub
    'Private Sub sLoadProviders()
    '    Try
    '        Dim vRowCounter As Integer
    '        Dim vsqlCommand As New SqlCommand
    '        vsqlCommand.CommandText = _
    '        " Select DescA  From Providers " & _
    '        " Order By DescA "

    '        vsqlCommand.Connection = cControls.vSqlConn
    '        cControls.vSqlConn.Open()
    '        Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
    '        Txt_Desc.AutoCompleteCustomSource.Clear()
    '        Do While vSqlReader.Read
    '            Txt_Desc.AutoCompleteCustomSource.Add(Trim(vSqlReader(0)))
    '        Loop
    '        cControls.vSqlConn.Close()
    '        vSqlReader.Close()
    '    Catch ex As Exception
    '        cControls.vSqlConn.Close()
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub
    Private Sub sFilterProviders(ByVal pDesc As String)
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Top 5 DescA  From Providers Where Company_Code = " & vCompanyCode & _
            " Where DescA Like '%" & pDesc & "%' " & _
            " Order By DescA "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Desc.AutoCompleteCustomSource.Clear()
            Do While vSqlReader.Read
                Txt_Desc.AutoCompleteCustomSource.Add(Trim(vSqlReader(0)))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoadProvidersTypes()
        Try
            Txt_ProviderType.Items.Clear()
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA From Providers_Types Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            Do While vSqlReader.Read
                Txt_ProviderType.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
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
                vSqlstring = " Delete From Financial_Definitions_Tree Where Ser = ( Select Act_Ser From Providers Where Code = '" & Trim(Txt_Code.Text) & "' )" & _
                             " Delete From Providers Where Code = '" & Txt_Code.Text & "'"

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
        sOpenLov("Select Code, Name From Users", "الموظفين")
    End Sub
#End Region
#End Region
#Region " New Record                                                                     "
    Public Sub sNewRecord()
        If fSaveAll(True) = False Then
            Return
        End If

        Tab_Main.Tabs("Tab_Details").Selected = True
        Txt_Code.Clear()
        Txt_Desc.Clear()
        Txt_address.Clear()
        Txt_Nature.Clear()
        Txt_NatureDetails.Clear()

        Txt_ProviderType.Clear()
        Txt_ProviderType.ReadOnly = False

        Txt_FirstBalance.Value = Nothing
        Txt_Deduction.Value = Nothing
        Txt_DealType.Clear()
        Txt_FirstTimeDeal.Value = Nothing
        Txt_FB_Type.SelectedIndex = -1
        Txt_Remarks.Clear()
        Txt_Email1.Clear()
        Txt_Email2.Clear()
        Txt_Fax.Clear()
        Txt_FTel.Clear()
        Txt_MTel.Clear()
        Txt_AccountCode.Clear()
        Txt_AccountDesc.Clear()
        Txt_AccountCode.Tag = Nothing

        Txt_ContactPersonName.Clear()
        Txt_ContactPersonAddress.Clear()
        Txt_ContactPersonFTel.Clear()
        Txt_ContactPersonMTel.Clear()
        Txt_ContactPersonRemarks.Clear()
        Chk_IsProvider.Checked = False
        Chk_IsCustomer.Checked = False
        Chk_IsEmployee.Checked = False
        'Chk_StopDeal.Checked = False
        Chk_StopPay.Checked = False
        Chk_StopDealWithItems.Checked = False

        Txt_Code.ReadOnly = False
        Txt_Code.Select()

        Dim vAutoCode As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateProviderCode, 'Y') From Controls_PI ", Me.Name)
        If vAutoCode = "Y" Then
            sNewCode()
        End If
        'sLoadProviders()

        vMasterBlock = "NI"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")
    End Sub
    Private Sub sNewCode()
        Dim vSqlCommand As String
        vSqlCommand = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Providers "

        Txt_Code.Text = cControls.fReturnValue(vSqlCommand, Me.Name).PadLeft(4, "0")
    End Sub
    Public Sub sNewProviderFromFinancialSetup(ByVal pCode As String, ByVal pDesc As String, ByVal pFirstBalance As Decimal, ByVal pFB_Type As String)
        sNewRecord()

        Txt_Code.Text = pCode
        Txt_Code.ReadOnly = True
        Txt_Desc.Text = pDesc
        Txt_FirstBalance.Value = pFirstBalance
        If pFB_Type <> "NULL" Then
            Txt_FB_Type.Value = pFB_Type
        End If

        Txt_AccountCode.Text = pCode
        Txt_AccountCode.Tag = cControls.fReturnValue(" Select Ser From Financial_Definitions_Tree Where Code = '" & pCode & "' ", Me.Name)

        Txt_AccountDesc.Text = pDesc
    End Sub
#End Region
#Region " sOpenLov                                                                       "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String)
        vLovReturn1 = ""
        VLovReturn2 = ""
        Dim Frm_Lov As New FRM_LovGeneral_A(pSqlStatment, pTitle)
        Frm_Lov.ShowDialog()
        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            sQuery(pCode:=vLovReturn1)
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
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
            sQuerySummary()

            'ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = True
        Else
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, True, True, True, True, "", False, False, "بحث")
            If Grd_Summary.Selected.Rows.Count > 0 Then
                sQuery(pCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
            Else
                sNewRecord()
            End If

            'ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = False
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

        'UltraToolTipManager1.Enabled = pEnabled

    End Sub
#End Region
#End Region
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    
    
#End Region
#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummary(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            Dim vCodeFilter, vDescFilter As String
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

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Distinct Providers_Log.Provider_Code, " & _
            "                 Providers.DescA,                       " & _
            "                 Providers.DealType,                    " & _
            "                 Providers.FirstTimeDeal                " & _
            "                                                        " & _
            " From   Providers_Log LEFT JOIN Providers               " & _
            " ON     Providers.Code = Providers_Log.Provider_Code    " & _
            " Where 1 = 1                                            " & _
            vCodeFilter & _
            vDescFilter & _
            " Order By DescA "

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

                If vSqlReader.IsDBNull(2) = False Then
                    If vSqlReader(2) = "C" Then
                        DTS_Summary.Rows(vRowCounter)("DealType") = "نقدي"
                    ElseIf vSqlReader(2) = "D" Then
                        DTS_Summary.Rows(vRowCounter)("DealType") = "آجل"
                    ElseIf vSqlReader(2) = "V" Then
                        DTS_Summary.Rows(vRowCounter)("DealType") = "فيزا"
                    End If
                Else
                    DTS_Summary.Rows(vRowCounter)("DealType") = Nothing
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("FirstTimeDeal") = Trim(vSqlReader(3))
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

            Txt_RowCount.Text = Grd_Summary.Rows.GetFilteredInNonGroupByRows.Count

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
            vsqlCommand.CommandText = " Select Providers_Log.Code,                    " & _
                                      "        Employees.DescA as Emp_Desc, " & _
                                      "        TDate,                   " & _
                                      "        ComputerName,            " & _
                                      "        IPAddress,                " & _
                                      "        Type                     " & _
                                      " From Providers_Log INNER JOIN Employees " & _
                                      " ON   Employees.Code = Providers_Log.Emp_Code " & _
                                      " Where Provider_Code = '" & pRow("Code") & "'" & _
                                      " Order By Providers_Log.Code       "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            'vChildRows.Clear()
            Do While vSqlReader.Read
                vChildRows.SetCount(vRowCounter + 1)
                vChildRows(vRowCounter)("Code") = vSqlReader(0)

                If vSqlReader.IsDBNull(1) = False Then
                    vChildRows(vRowCounter)("Emp_Desc") = vSqlReader(1)
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    vChildRows(vRowCounter)("TDate") = vSqlReader(2)
                Else
                    vChildRows(vRowCounter)("TDate") = Nothing
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    vChildRows(vRowCounter)("ComputerName") = vSqlReader(3)
                Else
                    vChildRows(vRowCounter)("ComputerName") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    vChildRows(vRowCounter)("IPAddress") = vSqlReader(4)
                Else
                    vChildRows(vRowCounter)("IPAddress") = ""
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    If vSqlReader(5) = "C" Then
                        vChildRows(vRowCounter)("Type") = "انشاء"
                    ElseIf vSqlReader(5) = "U" Then
                        vChildRows(vRowCounter)("Type") = "تعديل"
                    ElseIf vSqlReader(5) = "D" Then
                        vChildRows(vRowCounter)("Type") = "الغاء"
                    End If

                End If
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
        End Try
    End Sub

    Private Sub Grd_Summary_BeforeRowExpanded(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CancelableRowEventArgs) Handles Grd_Summary.BeforeRowExpanded
        Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
        Dim vRow As UltraDataRow = DTS_Summary.Rows(e.Row.Index)
        sQuerySummaryDetails(vRow, vChildBand)
    End Sub

    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged
        sQuerySummary(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
    End Sub

    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick

        If Grd_Summary.Selected.Rows.Count > 0 Then
            If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                'sQuery(pCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
                'Else
                '    sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
            Else
                If Grd_Summary.ActiveRow.Expanded Then
                    Grd_Summary.ActiveRow.CollapseAll()
                Else
                    Grd_Summary.ActiveRow.ExpandAll()
                End If
                Exit Sub
            End If
        End If
        Tab_Main.Tabs("Tab_Details").Selected = True
    End Sub

    Private Sub Grd_Summary_AfterRowFilterChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs) Handles Grd_Summary.AfterRowFilterChanged
        Txt_RowCount.Text = Grd_Summary.Rows.GetFilteredInNonGroupByRows.Count
    End Sub

    Private Sub sExportToExcel()

        Dim vFileName As String
        SaveFileDialog1.FileName = ""
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName.Length > 0 Then
            vFileName = SaveFileDialog1.FileName
            UltraGridExcelExporter1.Export(Grd_Summary, vFileName & ".xls")
        End If

    End Sub
#End Region

    Private Sub Txt_AccountCode_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) Handles Txt_AccountCode.EditorButtonClick
        vLovReturn1 = ""
        VLovReturn2 = ""
        vLovReturn3 = ""

        Dim Frm_Lov As New FRM_LovTreeA
        Frm_Lov.ShowDialog()

        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            Txt_AccountCode.Text = vLovReturn3
            Txt_AccountDesc.Text = VLovReturn2
            'Txt_AccountCode.Tag = vLovReturn3

            Txt_FirstBalance.Value = cControls.fReturnValue(" Select FB From Financial_Definitions_Tree Where Ser = " & vLovReturn3, Me.Name)


        End If
    End Sub

    Private Sub Btn_Create_ProviderAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If vMasterBlock = "NI" Then
            Return
        End If

        If vMasterBlock = "I" Then
            Return
        End If

        If cControls.fIsExist(" From Financial_Definitions_Tree Where DescA = '" & Trim(Txt_Desc.Text) & "' ", Me.Name) = True Then
            vcFrmLevel.vParentFrm.sForwardMessage("118", Me)
            Return
        End If

        Dim vProviders_Act As String = cControls.fReturnValue(" Select Providers_Act From Controls_PI ", Me.Name)

        Dim vNode_Level As String = cControls.fReturnValue(" Select Node_Level From Financial_Definitions_Tree Where Code = '" & vProviders_Act & "' ", Me.Name)

        Dim vSqlString As String
        vSqlString = " Select IsNull(Max(Convert(Bigint, Code)), 0) + 1 From Financial_Definitions_Tree "
        Dim vCode As String = cControls.fReturnValue(vSqlString, Me.Name)

        sEmptySqlStatmentArray()

        vSqlString = " Insert Into Financial_Definitions_Tree  (      Code,                      DescA,               Parent_Code,         Type,      Effect,      Node_Level,             Company_Code,    SourceFrom )" & _
                     "                                  Values ('" & vCode & "', '" & Trim(Txt_Desc.Text) & "', '" & vProviders_Act & " ', 'C',         'B', " & vNode_Level + 1 & ", " & vCompanyCode & ",    'P' )"

        sFillSqlStatmentArray(vSqlString)

        Dim vAct_Ser As String = cControls.fReturnValue(" Select Ser From Financial_Definitions_Tree Where Code = '" & vCode & "' ", Me.Name)

        vSqlString = " Update Providers  " & _
                     " Set Act_Ser = (Select Ser From Financial_Definitions_Tree Where Code = '" & vCode & "' )" & _
                     " Where Code = '" & Trim(Txt_Code.Text) & "' "

        sFillSqlStatmentArray(vSqlString)

        If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then
            Txt_AccountCode.Text = cControls.fReturnValue(" Select Ser From Financial_Definitions_Tree Where Code = '" & vCode & "' ", Me.Name)
            Txt_AccountDesc.Text = Txt_Desc.Text

            'Btn_Create_ProviderAccount.Enabled = False
        End If
    End Sub

    Private Sub Btn_ClearAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If vMasterBlock = "I" Then
            Txt_AccountCode.Text = ""
            Txt_AccountDesc.Text = ""
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            Dim vSqlString As String = " Update Providers Set Act_Ser = NULL Where Code = '" & Trim(Txt_Code.Text) & "' "

            If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                Txt_AccountCode.Text = ""
                Txt_AccountDesc.Text = ""

                vMasterBlock = "N"
            End If
        End If
    End Sub
End Class