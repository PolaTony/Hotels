Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinEditors
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_CustomerDefinition_A
#Region " Declaration                                                                    "
    Dim vMasterBlock As String = "NI"
    Dim vPictureMode As String = "NI"
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSqlStatment(0) As String
    Dim vIsAdmin As String
    Dim vQuery As String = "N"
#End Region
#Region " Form Level                                                                     "
#Region " My Form                                                                        "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'sQuerySummary()
        'sQueryUser(vUsrCode)
        Try
            cControls.sLoadSettings(Me.Name, Grd_Summary)
            'Txt_PackUnit.Items.Clear()
            Dim vSQlcommand As New SqlCommand
            Dim vGenerateNewCode As String = ""

            vSQlcommand.CommandText = _
            " Select IsNull(AutomaticallyGenerateCustomerCode, 'Y')   " & _
            " From Controls_SI_GL "

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

        Tab_Main.Tabs("Tab_Details").Selected = True
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
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "«· ›«’Ì·")
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", True, False, "»ÕÀ")
        End If

        sLoadSalesTypes()
        sLoadRegions()

        If vcFrmLevel.vRecPos > 0 Then
            vcFrmLevel.vParentFrm.sPrintRec(vcFrmLevel.vRecPos)
        Else
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If

        Dim vTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
        vTool = vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Chk_Help")
        sIsHelpEnabled(vTool.Checked)
    End Sub
    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If fSaveAll(True) = False Then
            e.Cancel = True
        Else
            e.Cancel = False
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If

        cControls.sSaveSettings(Me.Name, Grd_Summary)
    End Sub
    Private Sub FRM_Users_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Me.ParentForm.MdiChildren.Length = 1 Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, False, "", True)
        End If
    End Sub

    Private Sub Txt_All_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
    Handles Txt_Desc.KeyUp, Txt_address.KeyUp, Txt_Nature.KeyUp, Txt_NatureDetails.KeyUp,
    Txt_FirstBalance.KeyUp, Txt_Deduction.KeyUp, Txt_DealType.KeyUp, Txt_FirstTimeDeal.KeyUp,
    Txt_Remarks.KeyUp, Txt_Email1.KeyUp, Txt_Email2.KeyUp, Txt_ReceiverId.KeyUp, Txt_FTel.KeyUp,
    Txt_MTel.KeyUp, Txt_ContactPersonName.KeyUp, Txt_ContactPersonAddress.KeyUp,
    Txt_ContactPersonFTel.KeyUp, Txt_ContactPersonMTel.KeyUp, Txt_ContactPersonRemarks.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub

    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    Txt_Desc.TextChanged, Txt_address.ValueChanged, Txt_Nature.ValueChanged, Txt_NatureDetails.ValueChanged,
    Txt_FirstBalance.ValueChanged, Txt_FB_Type.ValueChanged, Txt_Deduction.ValueChanged, Txt_DealType.ValueChanged,
    Txt_FirstTimeDeal.ValueChanged, Txt_Remarks.ValueChanged, Txt_Email1.ValueChanged, Txt_Email2.ValueChanged,
    Txt_ReceiverId.ValueChanged, Txt_FTel.ValueChanged, Txt_MTel.ValueChanged, Txt_SalesManDesc.ValueChanged,
    Txt_ContactPersonName.ValueChanged, Txt_ContactPersonAddress.ValueChanged, Txt_ContactPersonFTel.ValueChanged,
    Txt_ContactPersonMTel.ValueChanged, Txt_ContactPersonRemarks.ValueChanged, Txt_Region.ValueChanged

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If

        'If vQuery = "N" Then
        '    If sender.Name = "Txt_Desc" Then
        '        'If Txt_Desc.Text.Length > 6 Then
        '        sFilterCustomers(Txt_Desc.Text)
        '        'End If
        '    End If
        'End If
    End Sub
    Private Sub CHK_IsSuperVisor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles _
                Chk_StopDeal.CheckedChanged, Chk_StopPay.CheckedChanged, Chk_IsProvider.CheckedChanged, Chk_IsEmployee.CheckedChanged, Chk_IsCustomer.CheckedChanged

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
    Private Sub Txt_SalesManCode_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) Handles Txt_SalesManCode.EditorButtonClick
        If sender.name = "Txt_SalesManCode" Then
            sOpenLov(" Select Code, DescA From Employees Where IsSalesman = 'Y' ", "«·„‰œÊ»Ì‰")
        End If
    End Sub
#End Region
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fIfsaveNeeded() As Boolean
        If vMasterBlock = "I" Or vMasterBlock = "U" Then
            Return True
        End If

        If vPictureMode = "I" Or vPictureMode = "U" Then
            Return True
        End If
        Return False
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        If fIfsaveNeeded() = False Then
            Return True
        End If
        Dim vSavePictures As Boolean

        sEmptySqlStatmentArray()
        If pAskMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                If fSaveValidation() = True Then
                    sSaveMain()
                    vSavePictures = sSave_Pictures()
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Else
            If fSaveValidation() = True Then
                sSaveMain()
                vSavePictures = sSave_Pictures()
            Else
                Return False
            End If
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Or vSavePictures Then
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            'vMasterBlock = "NI"
            'vPictureMode = "NI"
            'sNewRecord()

            vMasterBlock = "N"

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
                    If vFetchRec > cControls.fCount_Rec(" From Customers Where Company_Code = " & vCompanyCode) Then
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
            vFetchRec = cControls.fCount_Rec(" From Customers Where Customers.Company_Code = " & vCompanyCode)
        End If

        Dim vFetchCondition As String
        If pItemCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And Code = '" & Trim(pItemCode) & "'"
        End If

        vQuery = "Y"
        Try

            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText =
            " With MyCustomers as ( Select Customers.Code, " &
            "                          Customers.DescA, " &
            "                          Customers.Address, " &
            "                          Customers.Nature, " &
            "                          Customers.NatureDetails, " &
            "                          Customers.Deduction, " &
            "                          Customers.FirstTimeDeal, " &
            "                          Customers.Remarks, " &
            "                          Customers.Email1, " &
            "                          Customers.Email2, " &
            "                          Customers.ReceiverId, " &
            "                          Customers.FTel, " &
            "                          Customers.MTel, " &
            "                          Customers.StopDeal, " &
            "                          Customers.StopPay, " &
            "                          Customers.SalesMan_Code, " &
            "                          Employees.DescA as Emp_Desc, " &
            "                          Financial_Definitions_Tree.Ser,  " &
            "                          Financial_Definitions_Tree.Code as Act_Code,  " &
            "                          Financial_Definitions_Tree.DescA as Act_Desc, " &
            "                          Customers.FirstBalance, " &
            "                          Customers.FB_Type,       " &
            "                          Customers.ContactPersonName, " &
            "                          Customers.ContactPersonAddress, " &
            "                          Customers.ContactPersonFTel, " &
            "                          Customers.ContactPersonMTel, " &
            "                          Customers.ContactPersonRemarks, " &
            "                          Customers.Region_Code,          " &
            "                          ROW_Number() Over (Order By Customers.Code) RecPos " &
            "                          From Customers Left Join Employees      " &
            "                          On Customers.SalesMan_Code = Employees.Code  " &
            "                          LEFT JOIN Financial_Definitions_Tree         " &
            "                          ON Financial_Definitions_Tree.Ser = Customers.Act_Ser  " &
            "                          Where 1 = 1                                  " &
            "                          And Customers.Company_Code = " & vCompanyCode & " ) " &
            "                          Select * From MyCustomers  " &
            "                          Where 1 = 1 " &
                                      vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(28) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(28))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(28))

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

                'Nature
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_Nature.Value = vSqlReader(3)
                End If

                'NatureDetails
                If vSqlReader.IsDBNull(4) = False Then
                    Txt_NatureDetails.Value = Trim(vSqlReader(4))
                Else
                    Txt_NatureDetails.Value = Nothing
                End If

                'Deduction
                If vSqlReader.IsDBNull(5) = False Then
                    Txt_Deduction.Text = Trim(vSqlReader(5))
                Else
                    Txt_Deduction.Text = ""
                End If

                'DealType
                'If vSqlReader.IsDBNull(6) = False Then
                '    Dim vValueList As Infragistics.Win.ValueListItem
                '    For Each vValueList In Txt_DealType.Items
                '        If vValueList.DataValue = Trim(vSqlReader(6)) Then
                '            Txt_DealType.SelectedItem = vValueList
                '        End If
                '    Next
                'End If

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

                'ReceiverId
                If vSqlReader.IsDBNull(10) = False Then
                    Txt_ReceiverId.Text = Trim(vSqlReader(10))
                Else
                    Txt_ReceiverId.Text = ""
                End If

                'FTel
                If vSqlReader.IsDBNull(11) = False Then
                    Txt_FTel.Text = Trim(vSqlReader(11))
                Else
                    Txt_FTel.Text = ""
                End If

                'MTel
                If vSqlReader.IsDBNull(12) = False Then
                    Txt_MTel.Text = Trim(vSqlReader(12))
                Else
                    Txt_MTel.Text = ""
                End If

                'StopDeal
                Chk_StopDeal.Tag = Trim(vSqlReader(13))
                If Chk_StopDeal.Tag = "Y" Then
                    Chk_StopDeal.Checked = True
                Else
                    Chk_StopDeal.Checked = False
                End If

                'StopPay
                Chk_StopPay.Tag = Trim(vSqlReader(14))
                If Chk_StopPay.Tag = "Y" Then
                    Chk_StopPay.Checked = True
                Else
                    Chk_StopPay.Checked = False
                End If

                'SalesMan_Code
                If vSqlReader.IsDBNull(15) = False Then
                    Txt_SalesManCode.Text = Trim(vSqlReader(15))
                Else
                    Txt_SalesManCode.Text = ""
                End If

                'SalesMan_Desc
                If vSqlReader.IsDBNull(16) = False Then
                    Txt_SalesManDesc.Text = Trim(vSqlReader(16))
                Else
                    Txt_SalesManDesc.Text = ""
                End If

                'Act_Ser
                If vSqlReader.IsDBNull(17) = False Then
                    Txt_AccountCode.Tag = Trim(vSqlReader(17))
                Else
                    Txt_AccountCode.Tag = Nothing
                End If

                'Act_Code
                If vSqlReader.IsDBNull(18) = False Then
                    Txt_AccountCode.Text = Trim(vSqlReader(18))
                Else
                    Txt_AccountCode.Text = ""
                End If

                'Act_Desc
                If vSqlReader.IsDBNull(19) = False Then
                    Txt_AccountDesc.Text = Trim(vSqlReader(19))
                Else
                    Txt_AccountDesc.Text = ""
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

                'Region
                If vSqlReader.IsDBNull(27) = False Then
                    Txt_Region.Value = Trim(vSqlReader(27))
                Else
                    Txt_Region.Value = Nothing
                End If

            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Txt_Code.ReadOnly = True

            vQuery = "N"
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
        vPictureMode = "N"
    End Sub
    Private Sub sLoadSalesTypes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, DescA  From Sales_Types Where Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Nature.Items.Clear()
            Do While vSqlReader.Read
                Txt_Nature.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoadCustomers()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select DescA  From Customers Where Company_Code = " & vCompanyCode &
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
    Private Sub sLoadRegions()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, DescA  From Regions "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Region.Items.Clear()
            Do While vSqlReader.Read
                Txt_Region.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sFilterCustomers(ByVal pDesc As String)
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Top 5 DescA  From Customers Where Company_Code = " & vCompanyCode &
            " Where DescA Like '%" & pDesc & "%' " &
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
#End Region
#Region " Delete                                                                         "
    Public Sub sDelete()
        If vMasterBlock = "I" Then
            sNewRecord()
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                Dim vSqlstring As String

                sEmptySqlStatmentArray()

                vSqlstring = " Delete From Customers_Pictures Where Customer_Code = '" & Trim(Txt_Code.Text) & "'"

                sFillSqlStatmentArray(vSqlstring)

                vSqlstring = " Delete From Customers Where Code = '" & Trim(Txt_Code.Text) & "'"

                sFillSqlStatmentArray(vSqlstring)

                If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then
                    vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                    vMasterBlock = "NI"
                    vPictureMode = "NI"
                    sNewRecord()
                End If
            End If
        End If
    End Sub
#End Region
#Region " Find                                                                           "
    Public Sub sFind()
        sOpenLov("Select Code, Name From Users", "«·„ÊŸ›Ì‰")
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
        Txt_FirstBalance.Value = Nothing
        Txt_FB_Type.SelectedIndex = -1

        Txt_Deduction.Value = Nothing
        Txt_DealType.Clear()
        Txt_FirstTimeDeal.Value = Nothing
        Txt_Remarks.Clear()
        Txt_Email1.Clear()
        Txt_Email2.Clear()
        Txt_ReceiverId.Clear()
        Txt_FTel.Clear()
        Txt_MTel.Clear()

        Txt_SalesManCode.Clear()
        Txt_SalesManDesc.Clear()
        Txt_AccountCode.Clear()
        Txt_AccountDesc.Clear()
        Txt_AccountCode.Tag = Nothing

        Txt_ContactPersonName.Clear()
        Txt_ContactPersonAddress.Clear()
        Txt_ContactPersonFTel.Clear()
        Txt_ContactPersonMTel.Clear()
        Txt_ContactPersonRemarks.Clear()
        Txt_Region.Clear()
        Chk_IsProvider.Checked = False
        Chk_IsCustomer.Checked = False
        Chk_IsEmployee.Checked = False
        Chk_StopDeal.Checked = False
        Chk_StopPay.Checked = False

        Txt_Code.ReadOnly = False
        'sLoadCustomers()

        vMasterBlock = "NI"
        vPictureMode = "NI"
        vSelectedSortedList_1.Clear()
        vSelectedSortedList_2.Clear()
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")

        Dim x As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCustomerCode, 'Y') From Controls_SI_GL ", Me.Name)
        If x = "Y" Then
            sNewCode()
        End If
    End Sub
    Private Sub sNewCode()
        Dim vSqlCommand As String
        vSqlCommand = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Customers "

        Txt_Code.Text = cControls.fReturnValue(vSqlCommand, Me.Name).PadLeft(6, "0")
    End Sub
    Public Sub sNewCustomerFromFinancialSetup(ByVal pCode As String, ByVal pDesc As String, ByVal pFirstBalance As Decimal, ByVal pFB_Type As String)
        sNewRecord()

        Txt_Code.Text = pCode
        Txt_Code.ReadOnly = True
        Txt_Desc.Text = pDesc
        Txt_FirstBalance.Value = pFirstBalance
        If pFB_Type <> "NULL" Then
            Txt_FB_Type.Value = pFB_Type
        End If

        Txt_AccountCode.Text = pCode
        Txt_AccountCode.Tag = cControls.fReturnValue(" Select Ser From Financial_Definitions_Tree Where Code = '" & pCode & "' " & " And Company_Code = " & vCompanyCode, Me.Name)

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
            'sQuery(pItemCode:=vLovReturn1)
            If pTitle = "«·„ÊŸ›Ì‰" Then

            ElseIf pTitle = "«·„‰œÊ»Ì‰" Then
                Txt_SalesManCode.Text = vLovReturn1
                Txt_SalesManDesc.Text = VLovReturn2
            End If
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
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "«· ›«’Ì·")
            sQuerySummary(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "»ÕÀ")
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
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", True, False, "»ÕÀ")
        Else
            Tab_Main.Tabs("Tab_Summary").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "«· ›«’Ì·")
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
        If Txt_Code.Text.Length = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("3", Me)
            Txt_Code.Select()
            Return False
        End If

        If Txt_Desc.Text.Length = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("13", Me)
            Txt_Desc.Select()
            Return False
        End If

        If vMasterBlock = "I" Then
            If cControls.fCount_Rec(" From Customers Where Code = '" & Trim(Txt_Code.Text) & "'" & " And Company_Code = " & vCompanyCode) > 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("12", Me)
                Txt_Code.Select()
                Return False
            End If
        End If

        If cControls.fCount_Rec(" From Customers Where DescA = '" & Trim(Txt_Desc.Text) & "' And Code <> '" & Trim(Txt_Code.Text) & "'" & " And Company_Code = " & vCompanyCode) > 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("82", Me)
            Txt_Desc.Select()
            Return False
        End If

        'Here I check if Gournal Entry is Required or no..
        If cControls.fReturnValue(" Select IsNull(AutoGL, 'N') From Controls_SI_GL ", Me.Name) = "Y" Then
            If Txt_AccountDesc.Text = "" Then
                vcFrmLevel.vParentFrm.sForwardMessage("68", Me)
                Txt_AccountCode.Select()
                Return False
            End If
        End If

        If Not IsDBNull(Txt_FirstBalance.Value) Then
            If Not Txt_FirstBalance.Value = Nothing Then
                If Txt_FB_Type.SelectedIndex = -1 Then
                    vcFrmLevel.vParentFrm.sForwardMessage("93", Me)
                    Txt_FB_Type.Select()
                    Return False
                End If
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
        Dim vFirstTimeDeal, vFirstBalance, vFB_Type, vDeduction, vRegion, vSales_Man, vDeal_Type, vAct_Ser As String

        Dim vSqlCommand As String = ""

        If Not IsDBNull(Txt_FirstBalance.Value) Then
            If Txt_FirstBalance.Value = Nothing Then
                vFirstBalance = "NULL"
            Else
                vFirstBalance = Txt_FirstBalance.Value
            End If
        Else
            vFirstBalance = "NULL"
        End If

        If Txt_FB_Type.SelectedIndex <> -1 Then
            vFB_Type = "'" & Txt_FB_Type.SelectedItem.DataValue & "'"
        Else
            vFB_Type = "NULL"
        End If

        If Txt_Deduction.Value = Nothing Then
            vDeduction = "NULL"
        Else
            vDeduction = Txt_Deduction.Value
        End If

        If Not Txt_FirstTimeDeal.Value = Nothing Then
            vFirstTimeDeal = "'" & Format(Txt_FirstTimeDeal.Value, "MM-dd-yyyy") & "'"
        Else
            vFirstTimeDeal = "NULL"
        End If

        If Txt_SalesManDesc.Text = "" Then
            vSales_Man = "NULL"
        Else
            vSales_Man = "'" & Txt_SalesManCode.Text & "'"
        End If

        If Txt_Region.Value = Nothing Then
            vRegion = "NULL"
        Else
            vRegion = Txt_Region.Value
        End If

        If Txt_DealType.Value = Nothing Then
            vDeal_Type = "NULL"
        Else
            vDeal_Type = Txt_DealType.Value
        End If

        If Txt_AccountDesc.Text <> "" Then
            vAct_Ser = Txt_AccountCode.Tag
        Else
            vAct_Ser = "NULL"
        End If

        If vMasterBlock = "I" Then
            Dim x As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCustomerCode, 'Y') From Controls_SI_GL ", Me.Name)
            If x = "Y" Then
                sNewCode()
            End If

            vSqlCommand = " Insert Into Customers     (             Code,                              DescA,                      Address,                    Nature,                      NatureDetails,                Deduction,          DealType,           FirstTimeDeal,         Region_Code,                    Remarks,                          Email1,                          Email2,                     ReceiverId,                          FTel,                          MTel,                     StopDeal,                   StopPay,            SalesMan_Code,        Act_Ser,                 FirstBalance,          FB_Type,           ContactPersonName,                    ContactPersonAddress,                    ContactPersonFTel,                    ContactPersonMTel,                    ContactPersonRemarks,            Company_Code) " &
                                  " Values            ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_address.Text) & "', '" & Txt_Nature.Value & "', '" & Txt_NatureDetails.Text & "', " & vDeduction & ", " & vDeal_Type & ", " & vFirstTimeDeal & " ," & vRegion & ", '" & Trim(Txt_Remarks.Text) & "', '" & Trim(Txt_Email1.Text) & "', '" & Trim(Txt_Email2.Text) & "', '" & Trim(Txt_ReceiverId.Text) & "', '" & Trim(Txt_FTel.Text) & "', '" & Trim(Txt_MTel.Text) & "', '" & Chk_StopDeal.Tag & "', '" & Chk_StopPay.Tag & "'," & vSales_Man & ", " & vAct_Ser & ", " & vFirstBalance & ", " & vFB_Type & ", '" & Txt_ContactPersonName.Text & "', '" & Txt_ContactPersonAddress.Text & "', '" & Txt_ContactPersonFTel.Text & "', '" & Txt_ContactPersonMTel.Text & "', '" & Txt_ContactPersonRemarks.Text & "', " & vCompanyCode & ")"
            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Customers " &
                          " Set   DescA                 = '" & Trim(Txt_Desc.Text) & "'," &
                          "       Address               = '" & Trim(Txt_address.Text) & "'," &
                          "       Nature                = '" & Txt_Nature.Value & "', " &
                          "       NatureDetails         = '" & Txt_NatureDetails.Value & "', " &
                          "       Deduction             =  " & vDeduction & ",  " &
                          "       DealType              =  " & vDeal_Type & ",    " &
                          "       FirstTimeDeal         =  " & vFirstTimeDeal & ", " &
                          "       Region_Code           =  " & vRegion & ", " &
                          "       Remarks               = '" & Trim(Txt_Remarks.Text) & "', " &
                          "       Email1                = '" & Trim(Txt_Email1.Text) & "', " &
                          "       Email2                = '" & Trim(Txt_Email2.Text) & "', " &
                          "       ReceiverId            = '" & Trim(Txt_ReceiverId.Text) & "', " &
                          "       FTel                  = '" & Trim(Txt_FTel.Text) & "', " &
                          "       MTel                  = '" & Trim(Txt_MTel.Text) & "', " &
                          "       StopDeal              = '" & Chk_StopDeal.Tag & "',  " &
                          "       StopPay               = '" & Chk_StopPay.Tag & "',   " &
                          "       SalesMan_Code         =  " & vSales_Man & ", " &
                          "       Act_Ser               =  " & vAct_Ser & ", " &
                          "       FirstBalance          =  " & vFirstBalance & ", " &
                          "       FB_Type               =  " & vFB_Type & ", " &
                          "       ContactPersonName     = '" & Txt_ContactPersonName.Text & "', " &
                          "       ContactPersonAddress  = '" & Txt_ContactPersonAddress.Text & "', " &
                          "       ContactPersonFTel     = '" & Txt_ContactPersonFTel.Text & "', " &
                          "       ContactPersonMTel     = '" & Txt_ContactPersonMTel.Text & "', " &
                          "       ContactPersonRemarks  = '" & Txt_ContactPersonRemarks.Text & "'  " &
                          " Where Code                  = '" & Txt_Code.Text & "'"
            sFillSqlStatmentArray(vSqlCommand)
        End If

        sSave_Pictures()
    End Sub

    Private Function sSave_Pictures() As Boolean
        Dim vSqlString As String
        Dim vGetSerial As String
        Dim vCounter As Integer = 0

        Try
            cControls.fSendData(" Delete From Customers_Pictures Where Customer_Code = '" & Trim(Txt_Code.Text) & "'", Me.Name)

            For Each vItem As Object In vSelectedSortedList_1.Keys

                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Pictures " & _
                             " Where Customer_Code = '" & Trim(Txt_Code.Text) & "'"

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                Dim ms As New System.IO.MemoryStream
                Dim arrPicture() As Byte

                vSqlString = " Insert Into Customers_Pictures (         Customer_Code,            Ser,          Picture,                    DescA ) " & _
                             " Values                         ('" & Trim(Txt_Code.Text) & "'," & vGetSerial & ", (@image),    '" & vSelectedSortedList_2.Item(vItem) & "' )"

                Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)

                vSelectedSortedList_1.Item(vItem).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                'If ms.Length > MAX_IMAGE_SIZE Then
                '    MsgBox("image trop grosse")
                'End If
                arrPicture = ms.GetBuffer()
                ms.Flush()
                vMyCommand.Parameters.Add("@image", SqlDbType.Image).Value = arrPicture

                cControls.vSqlConn.Open()
                vMyCommand.ExecuteNonQuery()
                cControls.vSqlConn.Close()

                vCounter += 1
            Next

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()

            Return False
        End Try

    End Function
#End Region
#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummary(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try

            'If pCode = "" And pDesc = "" Then
            '    DTS_Summary.Rows.Clear()
            '    Return
            'End If

            'If pDesc.Length < 2 And pCode = "" Then
            '    Return
            'End If

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
            " Select Select Top 30 Code, " & _
            "         DescA,                                             " & _
            "         FirstTimeDeal,                                     " & _
            "         ROW_Number() Over (Order By Code) as  RecPos       " & _
            " From Customers                                             " & _
            " Where 1 = 1                                                " & _
            " And   Company_Code = " & vCompanyCode & _
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

                'If vSqlReader.IsDBNull(2) = False Then
                '    If vSqlReader(2) = "C" Then
                '        DTS_Summary.Rows(vRowCounter)("DealType") = "‰ﬁœÌ"
                '    ElseIf vSqlReader(2) = "D" Then
                '        DTS_Summary.Rows(vRowCounter)("DealType") = "¬Ã·"
                '    ElseIf vSqlReader(2) = "V" Then
                '        DTS_Summary.Rows(vRowCounter)("DealType") = "›Ì“«"
                '    End If
                'Else
                '    DTS_Summary.Rows(vRowCounter)("DealType") = Nothing
                'End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Summary.Rows(vRowCounter)("FirstTimeDeal") = Trim(vSqlReader(2))
                Else
                    DTS_Summary.Rows(vRowCounter)("FirstTimeDeal") = Nothing
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

    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged
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

    Private Sub Txt_AccountCode_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) Handles Txt_AccountCode.EditorButtonClick

        vLovReturn1 = ""
        VLovReturn2 = ""
        vLovReturn3 = ""

        'Dim Frm_Lov As New FRM_LovTreeA(vCompanyCode)
        'Frm_Lov.ShowDialog()

        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            Txt_AccountCode.Text = vLovReturn1
            Txt_AccountDesc.Text = VLovReturn2
            Txt_AccountCode.Tag = vLovReturn3

            Txt_FirstBalance.Value = cControls.fReturnValue(" Select FB From Financial_Definitions_Tree Where Ser = " & vLovReturn3, Me.Name)
        End If
    End Sub

    
End Class