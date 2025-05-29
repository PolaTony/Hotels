Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource

Public Class Frm_Customers_Log_A
#Region " Declaration                                                                    "
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As New cFrmLevelVariables_A
    'Dim vcFrmLevel As Object = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".cFrmLevelVariables_" & vLang)
    Dim vSqlStatment(0) As String
    Dim vIsAdmin As String
    Dim vQuery As String = "N"
    Dim vFocus As String = "Master"
    Dim vSortedList As New SortedList
    Dim vClear As Boolean = True
    'Dim vPictureMode As String = "NI"
#End Region
#Region " Form Level                                                                     "
#Region " My Form                                                                        "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sQuerySummaryMain()
        sLoadCountries()
        sLoadCustomersTypes()
        sLoadSalesTypes()
        sLoadDealStatus()
        sLoadConclusion()
        sLoadBranches()
        sLoadSalesMen()
        sLoadRegions()

        Try
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

        'sQueryUser(vUsrCode)
        vMasterBlock = "NI"
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

        'Txt_AccountCode.Text = pCode
        Txt_AccountCode.Text = cControls.fReturnValue(" Select Ser From Financial_Definitions_Tree Where Code = '" & pCode & "' " & " And Company_Code = " & vCompanyCode, Me.Name)

        Txt_AccountDesc.Text = pDesc
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
            sQuerySummaryMain()
        Else
            'sLoadCustomersTypes()
            'sLoadDealTypes()
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, True, True, True, True, "", False, False, "بحث")
        End If

        If vcFrmLevel.vRecPos > 0 Then
            vcFrmLevel.vParentFrm.sPrintRec(vcFrmLevel.vRecPos)
        Else
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If
    End Sub
    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing

    End Sub
    Private Sub FRM_Users_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Me.ParentForm.MdiChildren.Length = 1 Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, False, "", True)
        End If
    End Sub

    Private Sub Txt_All_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
    Handles Txt_address.KeyUp, Txt_Countries.KeyUp, _
     Txt_Deduction.KeyUp, Txt_SalesType.KeyUp, Txt_FirstTimeDeal.KeyUp, _
    Txt_Remarks.KeyUp, Txt_Email1.KeyUp, Txt_Email2.KeyUp, Txt_Fax.KeyUp, Txt_FTel.KeyUp, _
    Txt_MTel.KeyUp, Txt_ContactPersonName.KeyUp, Txt_ContactPersonAddress.KeyUp, _
    Txt_ContactPersonFTel.KeyUp, Txt_ContactPersonMTel.KeyUp, Txt_ContactPersonRemarks.KeyUp, Txt_Desc.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub

    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_address.ValueChanged, Txt_Countries.ValueChanged, _
    Txt_Deduction.ValueChanged, Txt_SalesType.ValueChanged, Txt_FirstTimeDeal.ValueChanged, _
    Txt_Remarks.ValueChanged, Txt_Email1.ValueChanged, Txt_Email2.ValueChanged, Txt_Fax.ValueChanged, _
    Txt_FTel.ValueChanged, Txt_MTel.ValueChanged, Txt_ContactPersonName.ValueChanged, _
    Txt_ContactPersonAddress.ValueChanged, Txt_ContactPersonFTel.ValueChanged, _
    Txt_ContactPersonMTel.ValueChanged, Txt_ContactPersonRemarks.ValueChanged, Txt_Desc.TextChanged, _
    Txt_AccountCode.ValueChanged, Txt_AccountDesc.ValueChanged, Txt_SalesMen.ValueChanged, _
    Txt_FirstBalance.ValueChanged, Txt_FB_Type.ValueChanged, Txt_Branch.ValueChanged, _
    Txt_Regions.ValueChanged, Txt_CustomerType.ValueChanged, Txt_CreditLimit.ValueChanged, _
    Chk_StopDeal.CheckedChanged

        If sender.name = "" Then
            Return
        End If

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If

        If sender.name = "Chk_StopDeal" Then
            If sender.Checked = True Then
                sender.Tag = "Y"
            Else
                sender.Tag = "N"
            End If
        End If

        'If sender.Name = "Txt_CustomerType" Then
        '    If Txt_CustomerType.SelectedIndex <> -1 Then
        '        If vQuery = "N" Then
        '            Dim vSqlString As String
        '            vSqlString = " Select Count (*) + 1 From Customers Where CustomerType = " & Trim(Txt_CustomerType.Value)

        '            Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(6, "0") & "-" & Trim(Txt_CustomerType.Value)
        '        End If
        '    End If
        'End If

        'If vQuery = "N" Then
        '    If sender.Name = "Txt_Desc" Then
        '        'If Txt_Desc.Text.Length > 6 Then
        '        sFilterCustomers(Txt_Desc.Text)
        '        'End If
        '    End If
        'End If
    End Sub
    Private Sub ToolBar_Main_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case e.Tool.Key
            Case "Btn_ExportToExcel"
                sExportToExcel()
        End Select
    End Sub
#End Region
#Region " DataBase                                                                       "

#Region " Query                                                                          "
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)

        Dim vFetchRec As Integer
        If pCode = Nothing Then
            If pIsGoTo = False Then
                If pRecPos = Nothing Then
                    vFetchRec = 1
                Else
                    vFetchRec = vcFrmLevel.vRecPos + pRecPos
                    If vFetchRec > cControls.fCount_Rec(" From Customers_Logs Where Company_Code = " & vCompanyCode) Then
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
            vFetchRec = cControls.fCount_Rec(" From Customers_Logs Where Company_Code = " & vCompanyCode)

        End If

        Dim vFetchCondition As String
        If pCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And MyCustomers_Log.Code = '" & Trim(pCode) & "'"
        End If

        vQuery = "Y"
        Try

            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText = _
            " With MyCustomers_Log as ( " & _
            "   Select Customers_Logs.Customer_Code, " & _
            "          Customers_Logs.DescA, " & _
            "          Customers_Logs.Address, " & _
            "          Customers_Logs.Country, " & _
            "          Customers_Logs.CustomerType,  " & _
            "          Customers_Logs.SalesType, " & _
            "          Customers_Logs.FirstTimeDeal, " & _
            "          Customers_Logs.Remarks, " & _
            "          Customers_Logs.Email1, " & _
            "          Customers_Logs.Email2, " & _
            "          Customers_Logs.Fax, " & _
            "          Customers_Logs.FTel, " & _
            "          Customers_Logs.MTel, " & _
            "          Customers_Logs.StopDeal, " & _
            "          Customers_Logs.StopPay, " & _
            "          Financial_Definitions_Tree.Ser,  " & _
            "          Financial_Definitions_Tree.DescA as Act_Desc, " & _
            "          Customers_Logs.FirstBalance, " & _
            "          Customers_Logs.FB_Type,       " & _
            "          Customers_Logs.Branch_Code,   " & _
            "          Customers_Logs.SalesMan_Code, " & _
            "          Customers_Logs.Region_Code,   " & _
            "          Customers_Logs.CreditLimit,   " & _
            "          Customers_Logs.ContactPersonName, " & _
            "          Customers_Logs.ContactPersonAddress, " & _
            "          Customers_Logs.ContactPersonFTel, " & _
            "          Customers_Logs.ContactPersonMTel, " & _
            "          Customers_Logs.ContactPersonRemarks, " & _
            "          Customers_Logs.Picture,              " & _
            "          Customers_Logs.Code,                 " & _
            "                                               " & _
            "          -- Log Details                                         " & vbCrLf & _
            "          Employees.DescA as Emp_Desc,                           " & _
            "          Customers_Logs.TDate,                                       " & _
            "          Customers_Logs.ComputerName,                                " & _
            "          Customers_Logs.IPAddress,                                   " & _
            "          Customers_Logs.Type,                                        " & _
            "          ROW_Number() Over (Order By Customers_Logs.Code) RecPos " & _
            "                                                                       " & _
            "          From Customers_Logs LEFT JOIN Financial_Definitions_Tree         " & _
            "          ON Financial_Definitions_Tree.Ser = Customers_Logs.Act_Ser  " & _
            "          LEFT JOIN Employees                                         " & _
            "          ON Employees.Code = Customers_Logs.Emp_Code                       " & _
            "          Where 1 = 1                              )                  " & _
            "          Select * From MyCustomers_Log  " & _
            "          Where 1 = 1 " & _
                                      vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(35) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(35))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(35))

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

                'Country
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_Countries.Value = vSqlReader(3)
                Else
                    Txt_Countries.SelectedIndex = -1
                End If

                'Customer Type
                If vSqlReader.IsDBNull(4) = False Then
                    Txt_CustomerType.Value = vSqlReader(4)
                Else
                    Txt_CustomerType.SelectedIndex = -1
                End If

                'Deal Type
                If vSqlReader.IsDBNull(5) = False Then
                    Txt_SalesType.Value = vSqlReader(5)
                Else
                    Txt_SalesType.SelectedIndex = -1
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

                'Fax
                If vSqlReader.IsDBNull(10) = False Then
                    Txt_Fax.Text = Trim(vSqlReader(10))
                Else
                    Txt_Fax.Text = ""
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
                If vSqlReader.IsDBNull(13) = False Then
                    Chk_StopDeal.Tag = Trim(vSqlReader(13))
                    If Chk_StopDeal.Tag = "Y" Then
                        Chk_StopDeal.Checked = True
                    Else
                        Chk_StopDeal.Checked = False
                    End If
                Else
                    Chk_StopDeal.Tag = "N"
                    Chk_StopDeal.Checked = False
                End If

                'StopPay
                If vSqlReader.IsDBNull(14) = False Then
                    Chk_StopPay.Tag = Trim(vSqlReader(14))
                    If Chk_StopPay.Tag = "Y" Then
                        Chk_StopPay.Checked = True
                    Else
                        Chk_StopPay.Checked = False
                    End If
                Else
                    Chk_StopPay.Tag = "N"
                End If

                'Act_Ser
                If vSqlReader.IsDBNull(15) = False Then
                    Txt_AccountCode.Text = Trim(vSqlReader(15))
                Else
                    Txt_AccountCode.Text = ""
                End If

                'Act_Desc
                If vSqlReader.IsDBNull(16) = False Then
                    Txt_AccountDesc.Text = Trim(vSqlReader(16))
                    'Btn_Create_CustomerAccount.Enabled = False
                Else
                    Txt_AccountDesc.Text = ""
                    'Btn_Create_CustomerAccount.Enabled = True
                End If

                'FirstBalance
                If vSqlReader.IsDBNull(17) = False Then
                    Txt_FirstBalance.Text = Trim(vSqlReader(17))
                Else
                    Txt_FirstBalance.Text = ""
                End If

                'FB_Type
                If vSqlReader.IsDBNull(18) = False Then
                    Txt_FB_Type.Value = Trim(vSqlReader(18))
                Else
                    Txt_FB_Type.Value = Nothing
                End If

                'Branch_Code
                If vSqlReader.IsDBNull(19) = False Then
                    Txt_Branch.Value = Trim(vSqlReader(19))
                Else
                    Txt_Branch.Value = Nothing
                End If

                'SalesMan_Code
                If vSqlReader.IsDBNull(20) = False Then
                    Txt_SalesMen.Value = Trim(vSqlReader(20))
                Else
                    Txt_SalesMen.SelectedIndex = -1
                End If

                'Region
                If vSqlReader.IsDBNull(21) = False Then
                    Txt_Regions.Value = Trim(vSqlReader(21))
                Else
                    Txt_Regions.SelectedIndex = -1
                End If

                'Branch_Code
                If vSqlReader.IsDBNull(22) = False Then
                    Txt_CreditLimit.Value = Trim(vSqlReader(22))
                Else
                    Txt_CreditLimit.Value = Nothing
                End If

                'ContactPersonName
                If vSqlReader.IsDBNull(23) = False Then
                    Txt_ContactPersonName.Text = Trim(vSqlReader(23))
                Else
                    Txt_ContactPersonName.Text = ""
                End If

                'ContactPersonAddress
                If vSqlReader.IsDBNull(24) = False Then
                    Txt_ContactPersonAddress.Text = Trim(vSqlReader(24))
                Else
                    Txt_ContactPersonAddress.Text = ""
                End If

                'ContactPersonFTel
                If vSqlReader.IsDBNull(25) = False Then
                    Txt_ContactPersonFTel.Text = Trim(vSqlReader(25))
                Else
                    Txt_ContactPersonFTel.Text = ""
                End If

                'ContactPersonMTel
                If vSqlReader.IsDBNull(26) = False Then
                    Txt_ContactPersonMTel.Text = Trim(vSqlReader(26))
                Else
                    Txt_ContactPersonMTel.Text = ""
                End If

                'ContactPersonRemarks
                If vSqlReader.IsDBNull(27) = False Then
                    Txt_ContactPersonRemarks.Text = Trim(vSqlReader(27))
                Else
                    Txt_ContactPersonRemarks.Text = ""
                End If

                'Picture
                If vSqlReader.IsDBNull(28) = False Then
                    Dim arrayImage() As Byte = CType(vSqlReader(28), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)
                    PictureBox1.Image = Image.FromStream(ms)

                Else
                    PictureBox1.Image = PictureBox1.InitialImage
                End If

                '-- Log Details
                'Emp_Desc
                If vSqlReader.IsDBNull(30) = False Then
                    Txt_EmployeeDesc.Text = Trim(vSqlReader(30))
                Else
                    Txt_EmployeeDesc.Text = ""
                End If

                If vSqlReader.IsDBNull(31) = False Then
                    Txt_TDate.Text = Trim(vSqlReader(31))
                Else
                    Txt_TDate.Text = Nothing
                End If

                If vSqlReader.IsDBNull(32) = False Then
                    Txt_ComputerName.Text = Trim(vSqlReader(32))
                Else
                    Txt_ComputerName.Text = ""
                End If

                If vSqlReader.IsDBNull(33) = False Then
                    Txt_IPAddress.Text = Trim(vSqlReader(33))
                Else
                    Txt_IPAddress.Text = ""
                End If

                If vSqlReader.IsDBNull(34) = False Then
                    If vSqlReader(34) = "C" Then
                        Txt_Type.Text = "انشاء"
                    ElseIf vSqlReader(34) = "U" Then
                        Txt_Type.Text = "تعديل"
                    ElseIf vSqlReader(34) = "D" Then
                        Txt_Type.Text = "الغاء"
                    End If
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Txt_Code.ReadOnly = True

            'sQueryAppointments()
            sQueryMain()
            sQueryRepresentatives()
            'Txt_CustomerType.ReadOnly = True

            vQuery = "N"
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
    End Sub
    Private Sub sLoadCountries()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Code, Desc" & vLang & " From Countries " & _
            " Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Countries.Items.Clear()

            Do While vSqlReader.Read
                Txt_Countries.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            Txt_Countries.SelectedIndex = -1
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoadCustomersTypes()
        Try
            Txt_CustomerType.Items.Clear()
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA From Customers_Types Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            Do While vSqlReader.Read
                Txt_CustomerType.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    'Private Sub sLoadDealTypes()
    '    Try
    '        Txt_DealType.Items.Clear()
    '        Dim vsqlCommand As New SqlClient.SqlCommand
    '        vsqlCommand.CommandText = " Select Code, DescA From Deal_Types Order By Code "

    '        vsqlCommand.Connection = cControls.vSqlConn
    '        cControls.vSqlConn.Open()
    '        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
    '        Do While vSqlReader.Read
    '            Txt_DealType.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
    '        Loop
    '        cControls.vSqlConn.Close()
    '        vSqlReader.Close()
    '    Catch ex As Exception
    '        cControls.vSqlConn.Close()
    '        MessageBox.Show(ex.Message)
    '    End Try

    'End Sub
    Private Sub sLoadDealStatus()
        Try
            Txt_DealStatus.Items.Clear()
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA From Deal_Status Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            Do While vSqlReader.Read
                Txt_DealStatus.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub sLoadConclusion()
        Try
            Txt_Conclusion.Items.Clear()
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA From Conclusion Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            Do While vSqlReader.Read
                Txt_Conclusion.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub sLoadBranches()
        Try
            Txt_Branch.Items.Clear()
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Branches.Code,                   " & _
                                      "        Branches.DescA                   " & _
                                      " From Employees INNER JOIN Profiles      " & _
                                      " ON   Profiles.Code = Employees.Profile  " & _
                                      " INNER JOIN Profiles_Branches            " & _
                                      " ON   Profiles.Code = Profiles_Branches.Prf_Code " & _
                                      " INNER JOIN Branches                     " & _
                                      " On   Branches.Code = Profiles_Branches.Branch_Code " & _
                                      " And  Branches.Company_Code = Profiles_Branches.Company_Code " & _
                                      " Where  Employees.Code = " & vUsrCode & _
                                      " And    Branches.Company_Code = " & vCompanyCode & _
                                      " And    Enabled        = 'Y' " & _
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
    Private Sub sLoadSalesMen()
        Try
            Txt_SalesMen.Items.Clear()
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA From Employees " & _
                                      " Where IsSalesMan = 'Y' " & _
                                      " And   IsNull(IsActive, 'Y')   = 'Y' " & _
                                      " Order By DescA "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

            Do While vSqlReader.Read
                Txt_SalesMen.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
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
            vsqlCommand.CommandText = _
            " Select Code, DescA  From Regions "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Regions.Items.Clear()
            Txt_FndByRegion.Items.Clear()

            Txt_FndByRegion.Items.Add("All", "الكل")

            Do While vSqlReader.Read
                Txt_Regions.Items.Add(vSqlReader(0), vSqlReader(1))
                Txt_FndByRegion.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoadSalesTypes()
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Code, DescA From Sales_Types " & _
            " Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_SalesType.Items.Clear()

            Do While vSqlReader.Read
                Txt_SalesType.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sFilterCustomers(ByVal pDesc As String)
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Top 5 DescA  From Customers " & _
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
#End Region
#Region " Delete                                                                         "
    Public Sub sDelete()

        If vMasterBlock = "I" Or vMasterBlock = "NI" Then

            If Grd_Representatives.Focused Then
                'If Not Grd_Details.ActiveRow Is Nothing Then
                Grd_Representatives.ActiveRow.Delete(False)
                Exit Sub
                'End If
            ElseIf Grd_Details.Focused Then
                If Grd_Details.ActiveRow.Band.Key = "Band 0" Then
                    Grd_Details.ActiveRow.Delete(False)
                End If
            ElseIf vFocus = "Master" Then
                sNewRecord()
                Exit Sub
            End If
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                Dim vSqlstring As String
                If Grd_Representatives.Focused Then
                    If Not Grd_Representatives.ActiveRow Is Nothing Then
                        If Grd_Representatives.ActiveRow.Cells("DML").Value = "I" Or Grd_Representatives.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_Representatives.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_Representatives.ActiveRow.Cells("DML").Value = "N" Or Grd_Representatives.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring = _
                            " Delete From Customers_Representatives " & _
                            " Where  CT_Code    = '" & Txt_Code.Text & "'" & _
                            " And    Ser        = '" & Grd_Representatives.ActiveRow.Cells("Ser").Value & "'"
                        End If
                    End If
                ElseIf Grd_Details.Focused Then
                    If Not Grd_Details.ActiveRow Is Nothing Then
                        If Grd_Details.ActiveRow.Band.Key = "Band 0" Then
                            If Grd_Details.ActiveRow.Cells("DML").Value = "I" Or Grd_Details.ActiveRow.Cells("DML").Value = "NI" Then
                                Grd_Details.ActiveRow.Delete(False)
                                Exit Sub
                            ElseIf Grd_Details.ActiveRow.Cells("DML").Value = "N" Or Grd_Details.ActiveRow.Cells("DML").Value = "U" Then
                                vSqlstring = _
                                " Delete From Customers_Deals_Details_Attachments " & _
                                " Where  CT_Code    = '" & Trim(Txt_Code.Text) & "'" & _
                                " And    Deal_Ser   = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'" & _
                                "                                                       " & _
                                " Delete From Customers_Deals_Details " & _
                                " Where  CT_Code   = '" & Trim(Txt_Code.Text) & "'" & _
                                " And    Deal_Ser  = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'" & _
                                "                                                       " & _
                                " Delete From Customers_Deals_Main " & _
                                " Where  CT_Code   = '" & Trim(Txt_Code.Text) & "'" & _
                                " And    Ser       = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'"
                            End If
                        ElseIf Grd_Details.ActiveRow.Band.Key = "Band 1" Then
                            vSqlstring = _
                            " Delete From Customers_Deals_Details_Attachments " & _
                            " Where  CT_Code    = '" & Trim(Txt_Code.Text) & "'" & _
                            " And    Deal_Ser   = '" & Grd_Details.ActiveRow.ParentRow.Cells("Ser").Value & "'" & _
                            " And    APT_Code   = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'" & _
                            "                                                       " & _
                            " Delete From Customers_Deals_Details " & _
                            " Where  CT_Code   = '" & Trim(Txt_Code.Text) & "'" & _
                            " And    Deal_Ser  = '" & Grd_Details.ActiveRow.ParentRow.Cells("Ser").Value & "'" & _
                            " And    Ser       = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'"

                        End If
                    End If
                ElseIf vFocus = "Master" Then
                    vSqlstring = _
                    " Delete From Customers_Deals_Details_Attachments Where CT_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Customers_Deals_Details Where CT_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Customers_Deals_Main Where CT_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Customers_Representatives Where CT_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Financial_Definitions_Tree Where Ser = ( Select Act_Ser From Customers Where Code = '" & Trim(Txt_Code.Text) & "' )" & _
                    " Delete From Customers Where Code = '" & Txt_Code.Text & "'"
                End If



                If cControls.fSendData(vSqlstring, Me.Name) > 0 Then

                    If Grd_Representatives.Focused Then
                        Grd_Representatives.ActiveRow.Delete(False)

                        'For Each vRow In Grd_Details.Rows
                        '    vRow.Cells("LCost").Value = (fUpdateRow(vRow) / fSumTotal() * fSumDistributed()) + fUpdateRow(vRow)
                        'Next
                    ElseIf Grd_Details.Focused Then
                        Grd_Details.ActiveRow.Delete(False)

                    ElseIf vFocus = "Master" Then
                        sNewRecord()
                    End If
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

        Tab_Main.Tabs("Tab_Details").Selected = True

        Txt_Desc.Clear()
        Txt_address.Clear()
        Txt_Countries.Clear()
        'Txt_NatureDetails.Clear()
        Txt_CustomerType.Clear()
        Txt_CustomerType.ReadOnly = False

        Txt_Deduction.Value = Nothing
        Txt_SalesType.Clear()
        Txt_FirstTimeDeal.Value = Nothing
        Txt_Remarks.Clear()
        Txt_Email1.Clear()
        Txt_Email2.Clear()
        Txt_Fax.Clear()
        Txt_FTel.Clear()
        Txt_MTel.Clear()

        Txt_AccountCode.Clear()
        Txt_AccountDesc.Clear()
        Txt_AccountCode.Text = Nothing
        Txt_FirstBalance.Value = Nothing

        Txt_CreditLimit.Value = Nothing

        Txt_FB_Type.SelectedIndex = -1
        Txt_Branch.SelectedIndex = -1
        Txt_SalesMen.SelectedIndex = -1
        Txt_Regions.SelectedIndex = -1

        If Txt_Branch.Items.Count = 1 Then
            Txt_Branch.SelectedIndex = 0
        End If

        PictureBox1.Image = PictureBox1.InitialImage
        Txt_ContactPersonName.Clear()
        Txt_ContactPersonAddress.Clear()
        Txt_ContactPersonFTel.Clear()
        Txt_ContactPersonMTel.Clear()
        Txt_ContactPersonRemarks.Clear()
        'Chk_IsProvider.Checked = False
        'Chk_IsCustomer.Checked = False
        'Chk_IsEmployee.Checked = False
        Chk_StopDeal.Checked = False
        Chk_StopPay.Checked = False

        Txt_Code.ReadOnly = False

        DTS_Details.Rows.Clear()
        DTS_Representatives.Rows.Clear()

        Dim vAutoCode As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCustomerCode, 'Y') From Controls_SI_GL ", Me.Name)
        If vAutoCode = "Y" Then
            sNewCode()
        End If
        'sQueryDetails()

        vMasterBlock = "NI"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")
    End Sub
    Private Sub sNewCode()
        Dim vSqlCommand As String
        vSqlCommand = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Customers "

        Txt_Code.Text = cControls.fReturnValue(vSqlCommand, Me.Name).PadLeft(6, "0")
    End Sub
#End Region
#Region " sOpenLov                                                                       "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String)
        vLovReturn1 = ""
        VLovReturn2 = ""
        'Dim Frm_Lov As New FRM_LovGeneral(pSqlStatment, pTitle)
        'Frm_Lov.ShowDialog()
        'If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
        '    sQuery(pItemCode:=vLovReturn1)
        'End If
    End Sub
#End Region
#Region " Tab Mangment                                                                   "
    Private Sub Tab_Main_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Main.SelectedTabChanging
        If Tab_Main.Tabs("Tab_Details").Selected = True Then

        End If
    End Sub
    Private Sub Tab_Main_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm = Me.ParentForm
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
            sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text), Trim(Txt_FndByTel.Text), Txt_FndByRegion.Value)

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
#Region " Print                                                                          "
    Public Sub sPrint()
        If cControls.fCount_Rec(" From  Reports Where Code = 'Aqua'") = 1 Then
            'sPrint_With_Regions()
        Else
            'sPrint_With_SalesMen()
        End If
    End Sub
    Private Function sFndByCustomers(ByVal pTableName As String) As String
        Dim vLstItem As Object
        Dim vItemValues As String = ""
        Dim vRow As UltraGridRow

        If Not Grd_Summary.Rows.GetFilteredInNonGroupByRows Is Nothing Then
            For Each vRow In Grd_Summary.Rows.GetFilteredInNonGroupByRows
                If Trim(vItemValues.Length) > 0 Then
                    vItemValues += ", "
                End If
                vItemValues += "'" & vRow.Cells("Code").Text & "'"
            Next

            sFndByCustomers = " And " & pTableName & ".Code  In  (" & vItemValues & ")"
        Else
            sFndByCustomers = ""
        End If
    End Function

#End Region
#End Region
#Region " Master                                                                         "
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

        If cControls.fCount_Rec(" From Customers Where DescA = '" & Trim(Txt_Desc.Text) & "' And Code <> '" & Trim(Txt_Code.Text) & "'") > 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("12", Me)
            Txt_Desc.Select()
            Return False
        End If

        'If Txt_Countries.SelectedIndex = -1 Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("61", Me)
        '    Txt_Countries.Select()
        '    Return False
        'End If

        'If Txt_CustomerType.SelectedIndex = -1 Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("62", Me)
        '    Txt_CustomerType.Select()
        '    Return False
        'End If

        If Txt_Branch.SelectedIndex = -1 Then
            vcFrmLevel.vParentFrm.sForwardMessage("73", Me)
            Txt_Branch.Select()
            Return False
        End If

        'Here I check if Journal Entry is Required or no..
        'If cControls.fReturnValue(" Select IsNull(AutoGL, 'N') From Controls_SI_GL ", Me.Name) = "Y" Then
        '    If Txt_AccountDesc.Text = "" Then
        '        vcFrmLevel.vParentFrm.sForwardMessage("68", Me)
        '        Txt_AccountCode.Select()
        '        Return False
        '    End If
        'End If

        If Not IsDBNull(Txt_FirstBalance.Value) Then
            If Not Txt_FirstBalance.Value = Nothing Then
                If Txt_FB_Type.SelectedIndex = -1 Then
                    vcFrmLevel.vParentFrm.sForwardMessage("93", Me)
                    Txt_FB_Type.Select()
                    Return False
                End If
            End If
        End If

        Return True
    End Function
    Private Sub sSaveMain()
        If fSaveValidation() = False Then
            Return
        End If
        Dim vFirstTimeDeal, vDeduction, vFirstBalance, vFB_Type, vAct_Ser, vBranch, vCountry, vCustomerType, vSalesType, vSalesMan, vRegion, vCreditLimit, vStopDeal As String
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

        If Txt_AccountDesc.Text <> "" Then
            vAct_Ser = Txt_AccountCode.Text
        Else
            vAct_Ser = "NULL"
        End If

        If Txt_Countries.SelectedIndex <> -1 Then
            vCountry = "'" & Txt_Countries.Value & "'"
        Else
            vCountry = "NULL"
        End If

        If Txt_CustomerType.SelectedIndex <> -1 Then
            vCustomerType = "'" & Txt_CustomerType.Value & "'"
        Else
            vCustomerType = "NULL"
        End If

        If Txt_SalesType.SelectedIndex <> -1 Then
            vSalesType = "'" & Txt_SalesType.Value & "'"
        Else
            vSalesType = "NULL"
        End If

        If Txt_Branch.SelectedIndex <> -1 Then
            vBranch = "'" & Txt_Branch.Value & "'"
        Else
            vBranch = "NULL"
        End If

        If Txt_SalesMen.SelectedIndex <> -1 Then
            vSalesMan = "'" & Txt_SalesMen.Value & "'"
        Else
            vSalesMan = "NULL"
        End If

        If Txt_Regions.SelectedIndex <> -1 Then
            vRegion = "'" & Txt_Regions.Value & "'"
        Else
            vRegion = "NULL"
        End If

        If Not Txt_CreditLimit.Value = Nothing Then
            vCreditLimit = Txt_CreditLimit.Value
        Else
            vCreditLimit = "NULL"
        End If

        If vMasterBlock = "I" Then
            Dim vAutoCode As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCustomerCode, 'Y') From Controls_SI_GL ", Me.Name)
            If vAutoCode = "Y" And Txt_CustomerType.SelectedIndex = -1 Then
                sNewCode()
            End If

            vSqlCommand = " Insert Into Customers     (             Code,                           Desc" & vLang & ",             Address,                Country,          CustomerType,         SalesType,         FirstTimeDeal,                    Remarks,                          Email1,                          Email2,                          Fax,                          FTel,                          MTel,                   StopDeal,                   StopPay,                 Act_Ser,          FirstBalance,          FB_Type,          Branch_Code,       SalesMan_Code,     Region_Code,     CreditLimit,          Company_Code,             ContactPersonName,                    ContactPersonAddress,                    ContactPersonFTel,                    ContactPersonMTel,                    ContactPersonRemarks) " & _
                                  " Values            ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_address.Text) & "', " & vCountry & ", " & vCustomerType & ", " & vSalesType & ", " & vFirstTimeDeal & " , '" & Trim(Txt_Remarks.Text) & "', '" & Trim(Txt_Email1.Text) & "', '" & Trim(Txt_Email2.Text) & "', '" & Trim(Txt_Fax.Text) & "', '" & Trim(Txt_FTel.Text) & "', '" & Trim(Txt_MTel.Text) & "', '" & Chk_StopDeal.Tag & "', '" & Chk_StopPay.Tag & "', " & vAct_Ser & ", " & vFirstBalance & ", " & vFB_Type & ", " & vBranch & ", " & vSalesMan & ", " & vRegion & ", " & vCreditLimit & ", " & vCompanyCode & ", '" & Txt_ContactPersonName.Text & "', '" & Txt_ContactPersonAddress.Text & "', '" & Txt_ContactPersonFTel.Text & "', '" & Txt_ContactPersonMTel.Text & "', '" & Txt_ContactPersonRemarks.Text & "')"

            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Customers " & _
                          " Set   DescA                 = '" & Trim(Txt_Desc.Text) & "'," & _
                          "       Address               = '" & Trim(Txt_address.Text) & "'," & _
                          "       Country               =  " & vCountry & ", " & _
                          "       CustomerType          =  " & vCustomerType & ", " & _
                          "       SalesType              =  " & vSalesType & ",    " & _
                          "       FirstTimeDeal         =  " & vFirstTimeDeal & ", " & _
                          "       Remarks               = '" & Trim(Txt_Remarks.Text) & "', " & _
                          "       Email1                = '" & Trim(Txt_Email1.Text) & "', " & _
                          "       Email2                = '" & Trim(Txt_Email2.Text) & "', " & _
                          "       Fax                   = '" & Trim(Txt_Fax.Text) & "', " & _
                          "       FTel                  = '" & Trim(Txt_FTel.Text) & "', " & _
                          "       MTel                  = '" & Trim(Txt_MTel.Text) & "', " & _
                          "       StopDeal              = '" & Chk_StopDeal.Tag & "',  " & _
                          "       StopPay               = '" & Chk_StopPay.Tag & "',   " & _
                          "       Act_Ser               =  " & vAct_Ser & ", " & _
                          "       FirstBalance          =  " & vFirstBalance & ", " & _
                          "       FB_Type               =  " & vFB_Type & ", " & _
                          "       Branch_Code           =  " & vBranch & ", " & _
                          "       SalesMan_Code         =  " & vSalesMan & ", " & _
                          "       Region_Code           =  " & vRegion & ", " & _
                          "       CreditLimit           =  " & vCreditLimit & ", " & _
                          "       ContactPersonName     = '" & Txt_ContactPersonName.Text & "', " & _
                          "       ContactPersonAddress  = '" & Txt_ContactPersonAddress.Text & "', " & _
                          "       ContactPersonFTel     = '" & Txt_ContactPersonFTel.Text & "', " & _
                          "       ContactPersonMTel     = '" & Txt_ContactPersonMTel.Text & "', " & _
                          "       ContactPersonRemarks  = '" & Txt_ContactPersonRemarks.Text & "'  " & _
                          " Where Code                  = '" & Txt_Code.Text & "'"

            sFillSqlStatmentArray(vSqlCommand)

            vSqlCommand = " Update Financial_Definitions_Tree " & _
                           " Set DescA = '" & Trim(Txt_Desc.Text) & "', " & _
                           "     FB = " & vFirstBalance & ", " & _
                           "     FB_Type = " & vFB_Type & _
                           " Where Ser = " & vAct_Ser

            sFillSqlStatmentArray(vSqlCommand)

        End If
    End Sub
    Private Function sSave_Pictures() As Boolean
        Dim vSqlString As String
        Dim vGetSerial As String
        Dim vCounter As Integer = 0

        Try
            cControls.fSendData(" Delete From Customers_Negotiations_Attachments " & _
                               " Where CT_Code = '" & Trim(Txt_Code.Text) & "'" & _
                               " AND   NG_Code = '", Me.Name)

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
#End Region
#Region " Details                                                                        "
#Region " Appointments                                                                   "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidateAppointments() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            If IsDBNull(vRow.Cells("TDate").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("53", Me)
                vRow.Cells("TDate").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveAppointments()

        Dim vRow As UltraGridRow
        Grd_Details.UpdateData()
        'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
        Dim vCounter As Integer = 0
        Dim vDate, vSqlString, vGetSerial, vFinished, vDealStatus As String

        For Each vRow In Grd_Details.Rows

            vDate = "'" & Format(vRow.Cells("TDate").Value, "MM-dd-yyyy HH:mm") & "'"

            If vRow.Cells("Finished").Text = True Then
                vFinished = "'Y'"
            Else
                vFinished = "'N'"
            End If

            If Not IsDBNull(vRow.Cells("DealStatus").Value) Then
                If Not vRow.Cells("DealStatus").Value = "" Then
                    vDealStatus = vRow.Cells("DealStatus").Value
                Else
                    vDealStatus = "NULL"
                End If
            Else
                vDealStatus = "NULL"
            End If


            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Appointments " & _
                             " Where CT_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Customers_Appointments  (          CT_Code,                    Ser,              TDate,                          DescL,                                   Brief,                   Finished,           DealStatus)" & _
                               "                       Values      ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", " & vDate & ", '" & Trim(vRow.Cells("DescL").Text) & "', '" & Trim(vRow.Cells("Brief").Text) & "', " & vFinished & ", " & vDealStatus & ")"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Customers_Appointments " & _
                              " Set     TDay         =  " & vDate & "," & _
                              "         Subject      = '" & Trim(vRow.Cells("Subject").Text) & "', " & _
                              "         Description  = '" & Trim(vRow.Cells("DescL").Text) & "', " & _
                              "         Finished     =  " & vFinished & ", " & _
                              "         DealStatus   =  " & vDealStatus & _
                              " Where   CT_Code      = '" & Txt_Code.Text & "'" & _
                              " And     Ser          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryAppointments()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Customers_Appointments.Ser,        " & _
            "        Customers_Appointments.TDay,      " & _
            "        Customers_Appointments.Subject,      " & _
            "        Customers_Appointments.Description,      " & _
            "        Customers_Appointments.Finished,    " & _
            "        Employees.DescA as CreatedBy_Desc,   " & _
            "        Customers_Appointments.DealStatus   " & _
            " From   Customers_Appointments INNER JOIN Employees  " & _
            " ON     Employees.Code = Customers_Appointments.CreatedBy  " & _
            " Where  CT_Code = '" & Trim(Txt_Code.Text) & "'" & _
            " Order  By TDay       "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Details.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Details.Rows.SetCount(vRowCounter + 1)
                DTS_Details.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                DTS_Details.Rows(vRowCounter)("TDate") = Trim(vSqlReader(1))
                DTS_Details.Rows(vRowCounter)("Subject") = Trim(vSqlReader(2))
                DTS_Details.Rows(vRowCounter)("DescL") = Trim(vSqlReader(3))

                If vSqlReader.IsDBNull(4) = False Then
                    If vSqlReader(4) = "Y" Then
                        DTS_Details.Rows(vRowCounter)("Finished") = True
                    Else
                        DTS_Details.Rows(vRowCounter)("Finished") = False
                    End If
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Details.Rows(vRowCounter)("CreatedBy") = Trim(vSqlReader(5))
                Else
                    DTS_Details.Rows(vRowCounter)("CreatedBy") = ""
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Details.Rows(vRowCounter)("DealStatus") = Trim(vSqlReader(6))
                Else
                    DTS_Details.Rows(vRowCounter)("DealStatus") = ""
                End If

                DTS_Details.Rows(vRowCounter)("DML") = "N"
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

#End Region

#Region " Items                                                                          "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidateDetails() As Boolean
        Grd_Details.UpdateData()

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            If sValidateChildRows(vRow) = False Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Function sValidateChildRows(ByVal pRow As UltraGridRow) As Boolean
        For Each vRow As UltraGridRow In pRow.ChildBands("Band 1").Rows
            'If IsDBNull(vRow.Cells("Quantity").Value) Then
            '    vcFrmLevel.vParentFrm.sForwardMessage("21", Me)
            '    Tab_Details.Tabs("Tab_Items").Selected = True
            '    vRow.Cells("Quantity").Selected = True
            '    Return False
            'End If
        Next
        Return True
    End Function

    Private Sub sSaveDetails_Main()
        Try
            Dim vRow As UltraGridRow
            Grd_Details.UpdateData()

            'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
            Dim vCounter As Integer = 0

            Dim vSqlString, vDate, vConclusion, vFinished, vGetSerial As String

            For Each vRow In Grd_Details.Rows
                If Not IsDBNull(vRow.Cells("TDate").Value) Then
                    vDate = "'" & Format(vRow.Cells("TDate").Value, "MM-dd-yyyy") & "'"
                Else
                    vDate = "NULL"
                End If

                If Not IsDBNull(vRow.Cells("Conclusion").Value) Then
                    If Not vRow.Cells("Conclusion").Value = "" Then
                        vConclusion = vRow.Cells("Conclusion").Value
                    Else
                        vConclusion = "NULL"
                    End If
                Else
                    vConclusion = "NULL"
                End If

                If vRow.Cells("Finished").Text = True Then
                    vFinished = "'Y'"
                Else
                    vFinished = "'N'"
                End If

                If vRow.Cells("DML").Value = "I" Then
                    vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Deals_Main  " & vbCrLf & _
                                 " Where CT_Code = '" & Txt_Code.Text & "'"
                    vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                    vSqlString = " Insert Into Customers_Deals_Main  (          CT_Code,                    Ser,                TDate,                           DescA,                Conclusion,             Finished)" & vbCrLf & _
                                              "                 Values('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ",  " & vDate & ", '" & Trim(vRow.Cells("DescL").Text) & "'," & vConclusion & ", " & vFinished & ")"

                    sFillSqlStatmentArray(vSqlString)
                    vCounter += 1
                ElseIf vRow.Cells("DML").Value = "U" Then
                    vSqlString = " Update   Customers_Deals_Main " & vbCrLf & _
                                  " Set     TDate    =  " & vDate & "," & vbCrLf & _
                                  "         DescA    = '" & Trim(vRow.Cells("DescL").Text) & "', " & vbCrLf & _
                                  "         Conclusion = " & vConclusion & ", " & _
                                  "         Finished = " & vFinished & vbCrLf & _
                                  " Where   CT_Code       = '" & Txt_Code.Text & "'" & vbCrLf & _
                                  " And     Ser        = '" & vRow.Cells("Ser").Text & "'"
                    sFillSqlStatmentArray(vSqlString)
                End If

                If vRow.Cells("DML").Value = "I" Then
                    sSaveDetails_Childs(vRow, vGetSerial)
                ElseIf vRow.Cells("DML").Value = "N" Or vRow.Cells("DML").Value = "U" Then
                    sSaveDetails_Childs(vRow, vRow.Cells("Ser").Text)
                End If

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sSaveDetails_Childs(ByVal pRow As UltraGridRow, ByVal vSerial As String)
        If fValidateDetails() = False Then
            Return
        End If
        Dim vRow As UltraGridRow
        Grd_Details.UpdateData()

        'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
        Dim vCounter As Integer = 0
        Dim vDate, vSqlString, vGetSerial, vFinished, vDealStatus As String

        For Each vRow In pRow.ChildBands("Band 1").Rows

            If Not IsDBNull(vRow.Cells("DealStatus").Value) Then
                If Not vRow.Cells("DealStatus").Value = "" Then
                    vDealStatus = vRow.Cells("DealStatus").Value
                Else
                    vDealStatus = "NULL"
                End If
            Else
                vDealStatus = "NULL"
            End If


            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Deals_Details " & _
                             " Where CT_Code = '" & Txt_Code.Text & "'" & _
                             " And   Deal_Ser = '" & pRow.Cells("Ser").Text & "'"

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Customers_Deals_Details  (          CT_Code,                    Ser,              TDate,                          DescA,                                   Brief,                   Finished,           DealStatus)" & _
                               "                       Values      ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", " & vDate & ", '" & Trim(vRow.Cells("DescL").Text) & "', '" & Trim(vRow.Cells("Brief").Text) & "', " & vFinished & ", " & vDealStatus & ")"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Customers_Deals_Details " & _
                              " Set     Subject      = '" & Trim(vRow.Cells("Subject").Text) & "', " & _
                              "         Description  = '" & Trim(vRow.Cells("DescL").Text) & "', " & _
                              "         DealStatus   =  " & vDealStatus & _
                              " Where   CT_Code      = '" & Txt_Code.Text & "'" & _
                              " And     Deal_Ser     =  " & pRow.Cells("Ser").Text & _
                              " And     Ser          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryMain()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Ser,                                                          " & vbCrLf & _
            "        TDate,                                                         " & vbCrLf & _
            "        DescA,                                                      " & vbCrLf & _
            "        Conclusion,                                                    " & _
            "        Finished                                                      " & vbCrLf & _
            "        From Customers_Deals_Main                                          " & vbCrLf & _
            "        Where CT_Code =  '" & Trim(Txt_Code.Text) & "'"

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vRow As UltraGridRow
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Details.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Details.Rows.SetCount(vRowCounter + 1)
                DTS_Details.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Details.Rows(vRowCounter)("TDate") = Trim(vSqlReader(1))
                Else
                    DTS_Details.Rows(vRowCounter)("TDate") = Nothing
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Details.Rows(vRowCounter)("DescL") = Trim(vSqlReader(2))
                Else
                    DTS_Details.Rows(vRowCounter)("DescL") = ""
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Details.Rows(vRowCounter)("Conclusion") = Trim(vSqlReader(3))
                Else
                    DTS_Details.Rows(vRowCounter)("Conclusion") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    If vSqlReader(4) = "Y" Then
                        DTS_Details.Rows(vRowCounter)("Finished") = True
                    Else
                        DTS_Details.Rows(vRowCounter)("Finished") = False
                    End If
                Else
                    DTS_Details.Rows(vRowCounter)("Finished") = False
                End If

                DTS_Details.Rows(vRowCounter)("SerNum") = DTS_Details.Rows(vRowCounter).Index + 1
                DTS_Details.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Grd_Details.UpdateData()

            For Each vDataRow As UltraDataRow In DTS_Details.Rows
                Dim vChildBand As UltraDataBand = DTS_Details.Band.ChildBands(0)
                sQueryDetails(vDataRow, vChildBand)
            Next
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQueryDetails")
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sQueryDetails(ByVal pRow As UltraDataRow, ByVal pChildBand As UltraDataBand)
        Try
            Dim vChildRows As UltraDataRowsCollection = pRow.GetChildRows(pChildBand)
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Customers_Deals_Details.Ser,        " & _
            "        Customers_Deals_Details.TDay,      " & _
            "        Customers_Deals_Details.Subject,      " & _
            "        Customers_Deals_Details.Description,      " & _
            "        Employees.DescA as CreatedBy_Desc,   " & _
            "        Customers_Deals_Details.DealStatus   " & _
            " From   Customers_Deals_Details INNER JOIN Employees  " & _
            " ON     Employees.Code = Customers_Deals_Details.CreatedBy  " & _
            " Where  CT_Code = '" & Trim(Txt_Code.Text) & "'" & _
            " And    Deal_Ser = '" & pRow.Item("Ser") & "'" & _
            " Order  By TDay       "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vRow As UltraGridRow
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            'DTS_Details.Rows.Clear()
            Do While vSqlReader.Read
                vChildRows.SetCount(vRowCounter + 1)
                vChildRows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                vChildRows(vRowCounter)("TDate") = Trim(vSqlReader(1))
                'vChildRows(vRowCounter)("Item_Ser") = Trim(vSqlReader(2))

                If vSqlReader.IsDBNull(2) = False Then
                    vChildRows(vRowCounter)("Subject") = Trim(vSqlReader(2))
                Else
                    vChildRows(vRowCounter)("Subject") = ""
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    vChildRows(vRowCounter)("DescL") = Trim(vSqlReader(3))
                Else
                    vChildRows(vRowCounter)("DescL") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    vChildRows(vRowCounter)("CreatedBy") = Trim(vSqlReader(4))
                Else
                    vChildRows(vRowCounter)("CreatedBy") = ""
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    vChildRows(vRowCounter)("DealStatus") = Trim(vSqlReader(5))
                Else
                    vChildRows(vRowCounter)("DealStatus") = ""
                End If

                vChildRows(vRowCounter)("DML") = "N"
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

    Private Sub Grd_Details_BeforeRowExpanded(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CancelableRowEventArgs) Handles Grd_Details.BeforeRowExpanded
        Dim vChildBand As UltraDataBand = DTS_Details.Band.ChildBands(0)
        Dim vDataRow As UltraDataRow = DTS_Details.Rows(e.Row.Index)
        sQueryDetails(vDataRow, vChildBand)
        'sQueryProductFormula(vDataRow, vChildBand, Txt_StoreCode.Text)
    End Sub

    Private Sub Grd_Details_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles Grd_Details.AfterRowInsert
        Try
            If Grd_Details.ActiveRow.Band.Key = "Band 0" Then
                e.Row.Cells("SerNum").Value = e.Row.Index + 1

            ElseIf Grd_Details.ActiveRow.Band.Key = "Band 1" Then
                'e.Row.Cells("SerNum").Value = e.Row.Index + 1
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Grd_Details_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Details.CellChange
        If sender.ActiveRow.Cells("DML").Text = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Text = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If

        'If Grd_Appointments.ActiveRow.Cells("DealStatus").Activated Then
        '    MessageBox.Show("OK")
        'End If

        Grd_Details.UpdateData()
    End Sub
    Private Sub Grd_Details_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Details.Enter
        Try
            vFocus = "Details"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_Details_ClickCellButton(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Details.ClickCellButton

        Try
            If Grd_Details.ActiveRow.Band.Key = "Band 0" Then
                If Grd_Details.ActiveRow.Cells("Calender").Activated Then
                    If Grd_Details.ActiveRow.Cells("DML").Text = "NI" Or Grd_Details.ActiveRow.Cells("DML").Text = "I" Then
                        vcFrmLevel.vParentFrm.sForwardMessage("64", Me)
                        Return
                    End If

                    If vLang = "L" Then
                        'Dim vNewFrm As New Frm_Customers_Calender_L(Trim(Txt_Code.Text), Grd_Details.ActiveRow.Cells("Ser").Text)
                        'vNewFrm.ShowDialog()
                    Else
                        Dim vNewFrm As New Frm_Customers_Calender_A(Trim(Txt_Code.Text), Grd_Details.ActiveRow.Cells("Ser").Text)
                        vNewFrm.ShowDialog()
                    End If


                    'sQueryMain()

                End If
            ElseIf Grd_Details.ActiveRow.Band.Key = "Band 1" Then
                If Grd_Details.ActiveRow.Cells("Attachments").Activated Then
                    If vLang = "L" Then
                        'Dim vNewForm As New Frm_Attachments_Customers_Load_L(Trim(Txt_Code.Text), Grd_Details.ActiveRow.Cells("Ser").Text, Grd_Details.ActiveRow.ParentRow.Cells("Ser").Text)
                        'vNewForm.ShowDialog()
                    Else
                        Dim vNewForm As New Frm_Attachments_Customers_Load_A(Trim(Txt_Code.Text), Grd_Details.ActiveRow.Cells("Ser").Text, Grd_Details.ActiveRow.ParentRow.Cells("Ser").Text)
                        vNewForm.ShowDialog()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_Details_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Details.DoubleClick
        Try
            If Not Grd_Details.ActiveRow Is Nothing Then
                If Grd_Details.ActiveRow.Band.Key = "Band 1" Then
                    If Grd_Details.ActiveRow.Cells("Subject").Activated Or Grd_Details.ActiveRow.Cells("TDate").Activated Or Grd_Details.ActiveRow.Cells("DescL").Activated Then
                        If Grd_Details.ActiveRow.Cells("TDate").Text <> "" Then
                            If vLang = "L" Then
                                'Dim vFrm_CustomerDefinition As New Frm_Customers_Calender_OneAppointment_L(Trim(Txt_Code.Text), Grd_Details.ActiveRow.ParentRow.Cells("Ser").Text, Grd_Details.ActiveRow.Cells("Ser").Text)
                                'vFrm_CustomerDefinition.ShowDialog()
                            Else
                                Dim vFrm_CustomerDefinition As New Frm_Customers_Calender_OneAppointment_A(Trim(Txt_Code.Text), Grd_Details.ActiveRow.ParentRow.Cells("Ser").Text, Grd_Details.ActiveRow.Cells("Ser").Text)
                                vFrm_CustomerDefinition.ShowDialog()
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
#Region " Representatives                                                                "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidateRepresentatives() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            If IsDBNull(vRow.Cells("DescL").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("13", Me)
                vRow.Cells("DescL").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveRepresentatives()

        Dim vRow As UltraGridRow
        Grd_Details.UpdateData()
        'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
        Dim vCounter As Integer = 0
        Dim vSqlString, vGetSerial As String

        For Each vRow In Grd_Representatives.Rows

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Representatives " & _
                             " Where CT_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Customers_Representatives  (          CT_Code,                    Ser,                            DescA,                                     Address,                                   FTel,                                     MTel,                                   Fax,                                   Email,                                    Position             )" & _
                               "                       Values      ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("DescL").Text) & "', '" & Trim(vRow.Cells("Address").Text) & "', '" & Trim(vRow.Cells("FTel").Text) & "', '" & Trim(vRow.Cells("MTel").Text) & "', '" & Trim(vRow.Cells("Fax").Text) & "', '" & Trim(vRow.Cells("Email").Text) & "', '" & Trim(vRow.Cells("Position").Text) & "' )"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Customers_Representatives " & _
                              " Set     DescA        = '" & Trim(vRow.Cells("DescL").Text) & "', " & _
                              "         Address      = '" & Trim(vRow.Cells("Address").Text) & "', " & _
                              "         FTel         = '" & Trim(vRow.Cells("FTel").Text) & "', " & _
                              "         MTel         = '" & Trim(vRow.Cells("MTel").Text) & "', " & _
                              "         Fax          = '" & Trim(vRow.Cells("Fax").Text) & "', " & _
                              "         Email        = '" & Trim(vRow.Cells("Email").Text) & "', " & _
                              "         Position     = '" & Trim(vRow.Cells("Position").Text) & "' " & _
                              " Where   CT_Code      = '" & Txt_Code.Text & "'" & _
                              " And     Ser          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryRepresentatives()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Customers_Representatives.Ser,         " & _
            "        Customers_Representatives.DescA,       " & _
            "        Customers_Representatives.Address,      " & _
            "        Customers_Representatives.FTel,        " & _
            "        Customers_Representatives.MTel,        " & _
            "        Customers_Representatives.Fax,         " & _
            "        Customers_Representatives.Email,       " & _
            "        Customers_Representatives.Position     " & _
            " From   Customers_Representatives              " & _
            " Where  CT_Code = '" & Trim(Txt_Code.Text) & "'" & _
            " Order  By Ser       "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Representatives.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Representatives.Rows.SetCount(vRowCounter + 1)

                DTS_Representatives.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                DTS_Representatives.Rows(vRowCounter)("DescL") = Trim(vSqlReader(1))

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Representatives.Rows(vRowCounter)("Address") = Trim(vSqlReader(2))
                Else
                    DTS_Representatives.Rows(vRowCounter)("Address") = ""
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Representatives.Rows(vRowCounter)("FTel") = Trim(vSqlReader(3))
                Else
                    DTS_Representatives.Rows(vRowCounter)("FTel") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Representatives.Rows(vRowCounter)("MTel") = Trim(vSqlReader(4))
                Else
                    DTS_Representatives.Rows(vRowCounter)("MTel") = ""
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Representatives.Rows(vRowCounter)("Fax") = Trim(vSqlReader(5))
                Else
                    DTS_Representatives.Rows(vRowCounter)("Fax") = ""
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Representatives.Rows(vRowCounter)("Email") = Trim(vSqlReader(6))
                Else
                    DTS_Representatives.Rows(vRowCounter)("Email") = ""
                End If

                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Representatives.Rows(vRowCounter)("Position") = Trim(vSqlReader(7))
                Else
                    DTS_Representatives.Rows(vRowCounter)("Position") = ""
                End If

                DTS_Representatives.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Representatives.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQueryDetails")
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub Grd_Representatives_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Representatives.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If

        Grd_Representatives.UpdateData()
    End Sub
    Private Sub Grd_Representatives_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Representatives.Enter
        vFocus = "Details"
    End Sub

#End Region
#End Region
#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummaryMain(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "", Optional ByVal pTel As String = "", Optional ByVal pRegion As String = "")
        Try
            Dim vCodeFilter, vDescFilter, vTelFilter, vRegion As String
            If Txt_FndByCode.Text = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Customers.Code Like '%" & Trim(Txt_FndByCode.Text) & "%'"
            End If

            If Txt_FndByDesc.Text = "" Then
                vDescFilter = ""
            Else
                vDescFilter = " And Customers.DescA Like '%" & Trim(Txt_FndByDesc.Text) & "%'"
            End If

            If Txt_FndByTel.Text = "" Then
                vTelFilter = ""
            Else
                vTelFilter = " And Customers.FTel Like '%" & Trim(Txt_FndByTel.Text) & "%'"
            End If

            If Txt_FndByRegion.SelectedIndex = -1 Then
                vRegion = ""
            ElseIf Txt_FndByRegion.SelectedIndex = 0 Then
                vRegion = ""
            Else
                vRegion = " And Customers.Region_Code = " & Txt_FndByRegion.Value
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText =
            " Select Distinct  Customers_Logs.Customer_Code,                              " &
            "         Customers.DescA as Customer_Desc,                  " &
            "         Countries.DescA as Country_Desc,                   " &
            "         Customers_Types.DescA as CustomerType_Desc,        " &
            "         Sales_Types.DescA as SalesType_Desc,                 " &
            "         Customers.FirstTimeDeal,                           " &
            "         Customers.Address,                                 " &
            "         Customers.Ftel,                                    " &
            "         Customers.MTel                                    " &
            " From Customers_Logs LEFT JOIN Customers                     " &
            " ON   Customers.Code = Customers_Logs.Customer_Code          " &
            " LEFT JOIN Countries                                        " &
            " ON Countries.Code = Customers.Country                      " &
            " LEFT JOIN Customers_Types                                  " &
            " ON Customers_Types.Code = Customers.CustomerType           " &
            " LEFT JOIN Sales_Types                                       " &
            " ON Sales_Types.Code = Customers.SalesType                    " &
            " Where 1 = 1                                                " &
            vCodeFilter &
            vDescFilter &
            vTelFilter &
            vRegion

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
                    DTS_Summary.Rows(vRowCounter)("Country") = Trim(vSqlReader(2))
                Else
                    DTS_Summary.Rows(vRowCounter)("Country") = ""
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("CustomerType") = Trim(vSqlReader(3))
                Else
                    DTS_Summary.Rows(vRowCounter)("CustomerType") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Summary.Rows(vRowCounter)("DealType") = Trim(vSqlReader(4))
                Else
                    DTS_Summary.Rows(vRowCounter)("DealType") = ""
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Summary.Rows(vRowCounter)("FirstTimeDeal") = Trim(vSqlReader(5))
                Else
                    DTS_Summary.Rows(vRowCounter)("FirstTimeDeal") = Nothing
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Summary.Rows(vRowCounter)("Address") = Trim(vSqlReader(6))
                Else
                    DTS_Summary.Rows(vRowCounter)("Address") = ""
                End If

                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Summary.Rows(vRowCounter)("FTel") = Trim(vSqlReader(7))
                Else
                    DTS_Summary.Rows(vRowCounter)("FTel") = ""
                End If

                If vSqlReader.IsDBNull(8) = False Then
                    DTS_Summary.Rows(vRowCounter)("MTel") = Trim(vSqlReader(8))
                Else
                    DTS_Summary.Rows(vRowCounter)("MTel") = ""
                End If

                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()

            Txt_RowCount.Text = Grd_Summary.Rows.Count

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
            vsqlCommand.CommandText = " Select Customers_Logs.Code,          " & _
                                      "        Employees.DescA as Emp_Desc, " & _
                                      "        TDate,                   " & _
                                      "        ComputerName,            " & _
                                      "        IPAddress,                " & _
                                      "        Type                     " & _
                                      " From Customers_Logs INNER JOIN Employees " & _
                                      " ON   Employees.Code = Customers_Logs.Emp_Code " & _
                                      " Where Customer_Code = '" & pRow("Code") & "'" & _
                                      " Order By Customers_Logs.Code       "

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
    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged, _
            Txt_FndByTel.ValueChanged, Txt_FndByRegion.ValueChanged

        sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text), Trim(Txt_FndByTel.Text), Trim(Txt_FndByRegion.Value))

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

    Private Sub Grd_Summary_BeforeRowExpanded(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CancelableRowEventArgs) Handles Grd_Summary.BeforeRowExpanded
        Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
        Dim vRow As UltraDataRow = DTS_Summary.Rows(e.Row.Index)
        sQuerySummaryDetails(vRow, vChildBand)
    End Sub
#End Region

    Private Sub Btn_Calender_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Calender.Click
        If vMasterBlock = "NI" Or vMasterBlock = "I" Then
            vcFrmLevel.vParentFrm.sForwardMessage("64", Me)
            Return
        End If

        'Dim vNewFrm As New Frm_Customers_Calender(Trim(Txt_Code.Text))
        'vNewFrm.ShowDialog()

        sQueryAppointments()
    End Sub

    Private Function sSave_MainPicture()
        Try
            If Not IsDBNull(UltraPictureBox1.Image) Then
                Dim vSqlString As String
                Dim ms As New System.IO.MemoryStream
                Dim arrPicture() As Byte

                vSqlString = " Update Customers Set Picture = (@image) Where Code = '" & Trim(Txt_Code.Text) & "'"

                Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)

                PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                'If ms.Length > MAX_IMAGE_SIZE Then
                '    MsgBox("image trop grosse")
                'End If
                arrPicture = ms.GetBuffer()
                ms.Flush()
                vMyCommand.Parameters.Add("@image", SqlDbType.Image).Value = arrPicture

                cControls.vSqlConn.Open()
                vMyCommand.ExecuteNonQuery()
                cControls.vSqlConn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try
    End Function

    Private Sub Btn_SalesMan_DeleteSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Txt_SalesMen.SelectedIndex = -1
    End Sub

    Private Sub Btn_Regoin_DeleteSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Txt_Regions.SelectedIndex = -1
    End Sub

    Private Sub Txt_CustomerType_DeleteSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Txt_CustomerType.SelectedIndex = -1
    End Sub

    Private Sub Btn_Create_CustomerAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

        Dim vCustomers_Act As String = cControls.fReturnValue(" Select Customers_Act From Controls_SI_GL ", Me.Name)

        Dim vNode_Level As String = cControls.fReturnValue(" Select Node_Level From Financial_Definitions_Tree Where Code = '" & vCustomers_Act & "' ", Me.Name)

        Dim vSqlString As String
        vSqlString = " Select IsNull(Max(Convert(Bigint, Code)), 0) + 1 From Financial_Definitions_Tree "
        Dim vCode As String = cControls.fReturnValue(vSqlString, Me.Name)

        sEmptySqlStatmentArray()

        vSqlString = " Insert Into Financial_Definitions_Tree  (      Code,                      DescA,               Parent_Code,         Type,      Effect,      Node_Level,             Company_Code,   SourceFrom )" & _
                     "                                  Values ('" & vCode & "', '" & Trim(Txt_Desc.Text) & "', '" & vCustomers_Act & " ', 'D',         'B', " & vNode_Level + 1 & ", " & vCompanyCode & ",   'C' )"

        sFillSqlStatmentArray(vSqlString)

        Dim vAct_Ser As String = cControls.fReturnValue(" Select Ser From Financial_Definitions_Tree Where Code = '" & vCode & "' ", Me.Name)

        vSqlString = " Update Customers  " & _
                     " Set Act_Ser = (Select Ser From Financial_Definitions_Tree Where Code = '" & vCode & "' )" & _
                     " Where Code = '" & Trim(Txt_Code.Text) & "' "

        sFillSqlStatmentArray(vSqlString)

        If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then
            Txt_AccountCode.Text = cControls.fReturnValue(" Select Ser From Financial_Definitions_Tree Where Code = '" & vCode & "' ", Me.Name)
            Txt_AccountDesc.Text = Txt_Desc.Text

            vMasterBlock = "N"

            'Btn_Create_CustomerAccount.Enabled = False
        End If
    End Sub

    Private Sub Btn_ClearAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If vMasterBlock = "I" Then
            Txt_AccountCode.Text = ""
            Txt_AccountDesc.Text = ""
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            Dim vSqlString As String = " Update Customers Set Act_Ser = NULL Where Code = '" & Trim(Txt_Code.Text) & "' "

            If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                Txt_AccountCode.Text = ""
                Txt_AccountDesc.Text = ""

                vMasterBlock = "N"
            End If
        End If
    End Sub
End Class