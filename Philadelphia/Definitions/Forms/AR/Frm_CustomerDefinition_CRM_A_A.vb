Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_CustomerDefinition_CRM_A_A

    Declare Function Wow64DisableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Declare Function Wow64EnableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Private osk As String = "C:\Windows\System32\osk.exe"

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
        sQuerySummary()
        sLoadCountries()
        'sLoadCustomersTypes()
        sLoadDealStatus()
        sLoadConclusion()
        sLoadBranches()
        sLoadDepartments()

        Try
            'cControls.sLoadSettings(Me.Name, Grd_Summary)
            Grd_Summary.DisplayLayout.Override.FilterUIProvider = UltraGridFilterUIProvider1
            'Txt_PackUnit.Items.Clear()
            Dim vSQlcommand As New SqlCommand
            Dim vGenerateNewCode As String = ""

            vSQlcommand.CommandText =
            " Select IsNull(AutomaticallyGenerateCustomerCode, 'Y')   " &
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
        sHide_ToolbarMain_Tools()

        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "التفاصيل")
            sQuerySummary()
        Else
            sSecurity()
            'sLoadCustomersTypes()
            'sLoadDealTypes()
        End If

        If vcFrmLevel.vRecPos > 0 Then
            vcFrmLevel.vParentFrm.sPrintRec(vcFrmLevel.vRecPos)
        Else
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If
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
    Handles Txt_Address.KeyUp, Txt_Countries.KeyUp,
     Txt_Deduction.KeyUp, Txt_FirstTimeDeal.KeyUp,
    Txt_Remarks.KeyUp, Txt_Email1.KeyUp, Txt_Email2.KeyUp, Txt_ReceiverId.KeyUp,
    Txt_MTel.KeyUp, Txt_ContactPersonName.KeyUp, Txt_ContactPersonAddress.KeyUp, Txt_ContactPersonMTel.KeyUp, Txt_ContactPersonRemarks.KeyUp, Txt_Desc.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub

    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Address.ValueChanged, Txt_Countries.ValueChanged,
    Txt_Deduction.ValueChanged, Txt_FirstTimeDeal.ValueChanged,
    Txt_Remarks.ValueChanged, Txt_Email1.ValueChanged, Txt_Email2.ValueChanged, Txt_ReceiverId.ValueChanged, Txt_MTel.ValueChanged, Txt_ContactPersonName.ValueChanged,
    Txt_ContactPersonAddress.ValueChanged,
    Txt_ContactPersonMTel.ValueChanged, Txt_ContactPersonRemarks.ValueChanged, Txt_Desc.TextChanged, Txt_Branch.ValueChanged, Txt_CustomerType.ValueChanged, Txt_CreditLimit.ValueChanged,
    Chk_StopDeal.CheckedChanged, Txt_BuildingNum.ValueChanged

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
            Case "11"
                Dim old As Long
                If Environment.Is64BitOperatingSystem Then
                    If Wow64DisableWow64FsRedirection(old) Then
                        Process.Start(osk)
                        Wow64EnableWow64FsRedirection(old)
                    End If
                Else
                    Process.Start(osk)
                End If
        End Select
    End Sub
#End Region
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fIfsaveNeeded() As Boolean
        If vMasterBlock = "I" Or vMasterBlock = "U" Then
            Return True
        End If

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If

            For Each vChildRow As UltraGridRow In vRow.ChildBands("Band 1").Rows
                If vChildRow.Cells("DML").Value = "I" Or vChildRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        Next

        For Each vRow In Grd_Representatives.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next

        For Each vRow In Grd_SalesPrices.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next
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
                If fSaveValidation() = True Then
                    sSaveMain()
                    sSave_MainPicture()
                Else
                    Return False
                End If

                'If fValidateAppointments() Then
                '    sSaveAppointments()
                'Else
                '    Return False
                'End If

                If fValidateDetails() Then
                    sSaveDetails_Main()
                Else
                    Return False
                End If

                If fValidateRepresentatives() Then
                    sSaveRepresentatives()
                Else
                    Return False
                End If

                If fValidate_SalesTypes() Then
                    sSave_SalesTypes()
                Else
                    Return False
                End If
            Else
                vMasterBlock = "NI"
                DTS_Details.Rows.Clear()
                DTS_Representatives.Rows.Clear()
                Return True
            End If
        Else
            If fSaveValidation() = True Then
                sSaveMain()
                sSave_MainPicture()
            Else
                Return False
            End If

            'If fValidateAppointments() Then
            '    sSaveAppointments()
            'Else
            '    Return False
            'End If

            If fValidateDetails() Then
                sSaveDetails_Main()
            Else
                Return False
            End If

            If fValidateRepresentatives() Then
                sSaveRepresentatives()
            Else
                Return False
            End If

            If fValidate_SalesTypes() Then
                sSave_SalesTypes()
            Else
                Return False
            End If
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            vMasterBlock = "N"
            'sQueryAppointments()
            sQueryMain()
            sQueryRepresentatives()
            sQuery_SalesTypes()

            'sNewRecord()
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
                    If vFetchRec > cControls.fCount_Rec(" From Customers                                             " &
                                                        " Where 1 = 1                                                " &
                                                        " And Customers.Company_Code = " & vCompanyCode) Then

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
            vFetchRec = cControls.fCount_Rec(" From Customers                                             " &
                                             " Where 1 = 1                                                " &
                                             " And Customers.Company_Code = " & vCompanyCode)
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
            "                          Customers.Street, " &
            "                          Customers.Country, " &
            "                          Customers.CustomerType,  " &
            "                          Customers.FirstTimeDeal, " &
            "                          Customers.Remarks, " &
            "                          Customers.Email1, " &
            "                          Customers.Email2, " &
            "                          Customers.ReceiverId, " &
            "                          Customers.MTel, " &
            "                          Customers.StopDeal, " &
            "                          Customers.StopPay, " &
            "                          Customers.CreditLimit,   " &
            "                          Customers.BuildingNum,   " &
            "                          ContactPersonName,       " &
            "                          ContactPersonAddress, " &
            "                          ContactPersonMTel, " &
            "                          ContactPersonRemarks, " &
            "                          Customers.Picture,              " &
            "                          ROW_Number() Over (Order By Customers.Code) RecPos " &
            "                          From Customers LEFT JOIN Financial_Definitions_Tree         " &
            "                          ON Financial_Definitions_Tree.Ser = Customers.Act_Ser  " &
            "                          Where 1 = 1                                                " &
            "                          And Customers.Company_Code = " & vCompanyCode & ") " &
            "                                                                             " &
            "                          Select * From MyCustomers  " &
            "                          Where 1 = 1 " &
                                      vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(20) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(20))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(20))

                'Code
                Txt_Code.Text = Trim(vSqlReader(0))

                'Name
                Txt_Desc.Text = Trim(vSqlReader(1))

                'Address
                If vSqlReader.IsDBNull(2) = False Then
                    Txt_Address.Text = Trim(vSqlReader(2))
                Else
                    Txt_Address.Text = ""
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

                'FirstTimeDeal
                If vSqlReader.IsDBNull(5) = False Then
                    Txt_FirstTimeDeal.Text = Trim(vSqlReader(5))
                Else
                    Txt_FirstTimeDeal.Text = Nothing
                End If

                'Remarks
                If vSqlReader.IsDBNull(6) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader(6))
                Else
                    Txt_Remarks.Text = ""
                End If

                'Email1
                If vSqlReader.IsDBNull(7) = False Then
                    Txt_Email1.Text = Trim(vSqlReader(7))
                Else
                    Txt_Email1.Text = ""
                End If

                'Email2
                If vSqlReader.IsDBNull(8) = False Then
                    Txt_Email2.Text = Trim(vSqlReader(8))
                Else
                    Txt_Email2.Text = ""
                End If

                'ReceiverId
                If vSqlReader.IsDBNull(9) = False Then
                    Txt_ReceiverId.Text = Trim(vSqlReader(9))
                Else
                    Txt_ReceiverId.Text = ""
                End If

                'MTel
                If vSqlReader.IsDBNull(10) = False Then
                    Txt_MTel.Text = Trim(vSqlReader(10))
                Else
                    Txt_MTel.Text = ""
                End If

                'StopDeal
                If vSqlReader.IsDBNull(11) = False Then
                    Chk_StopDeal.Tag = Trim(vSqlReader(11))
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
                If vSqlReader.IsDBNull(12) = False Then
                    Chk_StopPay.Tag = Trim(vSqlReader(12))
                    If Chk_StopPay.Tag = "Y" Then
                        Chk_StopPay.Checked = True
                    Else
                        Chk_StopPay.Checked = False
                    End If
                Else
                    Chk_StopPay.Tag = "N"
                End If

                'CreditLimit
                If vSqlReader.IsDBNull(13) = False Then
                    Txt_CreditLimit.Value = Trim(vSqlReader(13))
                Else
                    Txt_CreditLimit.Value = Nothing
                End If

                'Building Num
                If vSqlReader.IsDBNull(14) = False Then
                    Txt_BuildingNum.Text = Trim(vSqlReader(14))
                Else
                    Txt_BuildingNum.Text = ""
                End If

                'ContactPersonName
                If vSqlReader.IsDBNull(15) = False Then
                    Txt_ContactPersonName.Text = Trim(vSqlReader(15))
                Else
                    Txt_ContactPersonName.Text = ""
                End If

                'ContactPersonAddress
                If vSqlReader.IsDBNull(16) = False Then
                    Txt_ContactPersonAddress.Text = Trim(vSqlReader(16))
                Else
                    Txt_ContactPersonAddress.Text = ""
                End If

                'ContactPersonMTel
                If vSqlReader.IsDBNull(17) = False Then
                    Txt_ContactPersonMTel.Text = Trim(vSqlReader(17))
                Else
                    Txt_ContactPersonMTel.Text = ""
                End If

                'ContactPersonRemarks
                If vSqlReader.IsDBNull(18) = False Then
                    Txt_ContactPersonRemarks.Text = Trim(vSqlReader(18))
                Else
                    Txt_ContactPersonRemarks.Text = ""
                End If

                'Picture
                If vSqlReader.IsDBNull(19) = False Then
                    Dim arrayImage() As Byte = CType(vSqlReader(19), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)
                    PictureBox1.Image = Image.FromStream(ms)
                Else
                    PictureBox1.Image = PictureBox1.InitialImage
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Txt_Code.ReadOnly = True

            'sQueryAppointments()
            sQueryMain()
            sQueryRepresentatives()

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
            vsqlCommand.CommandText =
            " Select Code, Desc" & vLang & " From Countries " &
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
            vsqlCommand.CommandText = " Select Branches.Code,                   " &
                                      "        Branches.DescA                   " &
                                      " From Employees INNER JOIN Profiles      " &
                                      " ON   Profiles.Code = Employees.Profile  " &
                                      " INNER JOIN Profiles_Branches            " &
                                      " ON   Profiles.Code = Profiles_Branches.Prf_Code " &
                                      " INNER JOIN Branches                     " &
                                      " On   Branches.Code = Profiles_Branches.Branch_Code " &
                                      " And  Branches.Company_Code = Profiles_Branches.Company_Code " &
                                      " Where  Employees.Code = " & vUsrCode &
                                      " And    Branches.Company_Code = " & vCompanyCode &
                                      " And    Enabled        = 'Y' " &
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

    Private Sub sLoadDepartments()
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, DescA From Cost_Center " &
            " Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader

            Txt_Departments.Items.Clear()

            Do While vSqlReader.Read
                Txt_Departments.Items.Add(vSqlReader(0), vSqlReader(1))
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
            vsqlCommand.CommandText =
            " Select Top 5 DescA  From Customers " &
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

        sEmptySqlStatmentArray()

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
                            vSqlstring =
                            " Delete From Customers_Representatives " &
                            " Where  CT_Code    = '" & Txt_Code.Text & "'" &
                            " And    Ser        = '" & Grd_Representatives.ActiveRow.Cells("Ser").Value & "'"

                            sFillSqlStatmentArray(vSqlstring)
                        End If
                    End If
                ElseIf Grd_Details.Focused Then
                    If Not Grd_Details.ActiveRow Is Nothing Then
                        If Grd_Details.ActiveRow.Band.Key = "Band 0" Then
                            If Grd_Details.ActiveRow.Cells("DML").Value = "I" Or Grd_Details.ActiveRow.Cells("DML").Value = "NI" Then
                                Grd_Details.ActiveRow.Delete(False)
                                Exit Sub
                            ElseIf Grd_Details.ActiveRow.Cells("DML").Value = "N" Or Grd_Details.ActiveRow.Cells("DML").Value = "U" Then
                                vSqlstring =
                                " Delete From Customers_Deals_Details_Attachments " &
                                " Where  CT_Code    = '" & Trim(Txt_Code.Text) & "'" &
                                " And    Deal_Ser   = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'"

                                sFillSqlStatmentArray(vSqlstring)

                                vSqlstring =
                                " Delete From Customers_Deals_Details " &
                                " Where  CT_Code   = '" & Trim(Txt_Code.Text) & "'" &
                                " And    Deal_Ser  = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'"

                                sFillSqlStatmentArray(vSqlstring)

                                vSqlstring =
                                " Delete From Customers_Deals_Main " &
                                " Where  CT_Code   = '" & Trim(Txt_Code.Text) & "'" &
                                " And    Ser       = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'"

                                sFillSqlStatmentArray(vSqlstring)
                            End If
                        ElseIf Grd_Details.ActiveRow.Band.Key = "Band 1" Then
                            vSqlstring =
                            " Delete From Customers_Deals_Details_Attachments " &
                            " Where  CT_Code    = '" & Trim(Txt_Code.Text) & "'" &
                            " And    Deal_Ser   = '" & Grd_Details.ActiveRow.ParentRow.Cells("Ser").Value & "'" &
                            " And    APT_Code   = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'"

                            sFillSqlStatmentArray(vSqlstring)

                            vSqlstring =
                            " Delete From Customers_Deals_Details " &
                            " Where  CT_Code   = '" & Trim(Txt_Code.Text) & "'" &
                            " And    Deal_Ser  = '" & Grd_Details.ActiveRow.ParentRow.Cells("Ser").Value & "'" &
                            " And    Ser       = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'"

                            sFillSqlStatmentArray(vSqlstring)

                        End If
                    End If
                ElseIf Grd_SalesPrices.Focused Then
                    If Not Grd_SalesPrices.ActiveRow Is Nothing Then
                        If Grd_SalesPrices.ActiveRow.Cells("DML").Value = "I" Or Grd_SalesPrices.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_SalesPrices.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_SalesPrices.ActiveRow.Cells("DML").Value = "N" Or Grd_SalesPrices.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring =
                            " Delete From Customers_SalesTypes_Departments " &
                            " Where  Customer_Code    = '" & Txt_Code.Text & "'" &
                            " And    Code        = '" & Grd_SalesPrices.ActiveRow.Cells("Code").Value & "'"

                            sFillSqlStatmentArray(vSqlstring)
                        End If
                    End If
                ElseIf vFocus = "Master" Then
                    vSqlstring = " Delete From Customers_Deals_Details_Attachments Where CT_Code = '" & Txt_Code.Text & "'"
                    sFillSqlStatmentArray(vSqlstring)

                    vSqlstring = " Delete From Customers_Deals_Details Where CT_Code = '" & Txt_Code.Text & "'"
                    sFillSqlStatmentArray(vSqlstring)

                    vSqlstring = " Delete From Customers_Deals_Main Where CT_Code = '" & Txt_Code.Text & "'"
                    sFillSqlStatmentArray(vSqlstring)

                    vSqlstring = " Delete From Customers_Representatives Where CT_Code = '" & Txt_Code.Text & "'"
                    sFillSqlStatmentArray(vSqlstring)

                    vSqlstring = " Delete From Customers Where Code = '" & Txt_Code.Text & "'"
                    sFillSqlStatmentArray(vSqlstring)
                End If

                If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then

                    If Grd_Representatives.Focused Then
                        Grd_Representatives.ActiveRow.Delete(False)

                        'For Each vRow In Grd_Details.Rows
                        '    vRow.Cells("LCost").Value = (fUpdateRow(vRow) / fSumTotal() * fSumDistributed()) + fUpdateRow(vRow)
                        'Next
                    ElseIf Grd_Details.Focused Then
                        Grd_Details.ActiveRow.Delete(False)
                    ElseIf Grd_SalesPrices.Focused Then
                        Grd_SalesPrices.ActiveRow.Delete(False)

                    ElseIf vFocus = "Master" Then
                        vMasterBlock = "N"

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
        If fSaveAll(True) = False Then
            Return
        End If

        Tab_Main.Tabs("Tab_Details").Selected = True

        Txt_Code.Clear()
        Txt_Desc.Clear()

        Txt_Address.Clear()
        Txt_BuildingNum.Clear()

        Txt_Countries.Clear()
        'Txt_NatureDetails.Clear()
        Txt_CustomerType.Clear()
        Txt_CustomerType.ReadOnly = False

        Txt_Deduction.Value = Nothing
        Txt_FirstTimeDeal.Value = Nothing
        Txt_Remarks.Clear()
        Txt_Email1.Clear()
        Txt_Email2.Clear()
        Txt_ReceiverId.Clear()
        Txt_MTel.Clear()

        Txt_CreditLimit.Value = Nothing

        Txt_Branch.SelectedIndex = -1

        If Txt_Branch.Items.Count = 1 Then
            Txt_Branch.SelectedIndex = 0
        End If

        PictureBox1.Image = PictureBox1.InitialImage
        Txt_ContactPersonName.Clear()
        Txt_ContactPersonAddress.Clear()
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
        DTS_SalesPrices.Rows.Clear()

        Dim vAutoCode As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCustomerCode, 'Y') From Controls_SI_GL ", Me.Name)
        If vAutoCode = "Y" Then
            sNewCode()
        End If

        Txt_Desc.Select()

        vMasterBlock = "NI"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")
    End Sub
    Private Sub sNewCode()
        Dim vSqlCommand As String
        vSqlCommand = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Customers "

        Txt_Code.Text = cControls.fReturnValue(vSqlCommand, Me.Name)
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
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "التفاصيل")
            sQuerySummary(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text), Trim(Txt_FndByTel.Text), Txt_FndByRegion.Value)

            ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = True
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
            If Grd_Summary.Selected.Rows.Count > 0 Then
                sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
            Else
                sNewRecord()
            End If

            ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = False

            sSecurity()
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
            sPrint_With_Regions()
        Else
            sPrint_With_SalesMen()
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
    Private Sub sPrint_With_SalesMen()

        vClear = False
        If fSaveAll(True) = False Then
            Return
        End If
        vClear = True

        vSortedList.Clear()
        Dim vCompany As String = cControls.fReturnValue(" Select DescA From Companies " &
                                                        " Where  Code = " & vCompanyCode, Me.Name)
        Dim vSqlString As String
        vSqlString =
        "  Select Customers.Code as Customer_Code,                                " &
        "         Customers.DescA as Customer_Desc,                               " &
        "         Customers.Address as Address1,                               " &
        "         Customers.MTel as MTel1,                                    " &
        "         Customers.SalesMan_Code,                                    " &
        "         Employees.DescA as SalesMan_Desc                            " &
        " From Customers Inner Join Employees                                 " &
        " On Employees.Code = Customers.SalesMan_Code                         " &
        " Where 1 = 1                                                         " &
        sFndByCustomers("Customers")

        vSortedList.Add("DT_CustomerDetails", vSqlString)

        vSqlString =
       " Select DescA as CompanyName, Picture From Companies " &
       " Where  Code = " & vCompanyCode

        vSortedList.Add("DT_Header", vSqlString)

        If vLang = "A" Then
            Dim vRep_Preview As New FRM_ReportPreviewL("بيانات العملاء", vSortedList, New DS_CustomerDetails, New Rep_CustomerDetails_With_SalesMen)
            vRep_Preview.MdiParent = Me.MdiParent
            vRep_Preview.Show()
        Else
            Dim vRep_Preview As New FRM_ReportPreviewL("Customers Details", vSortedList, New DS_CustomerDetails, New Rep_CustomerDetails_With_SalesMen_L)
            vRep_Preview.MdiParent = Me.MdiParent
            vRep_Preview.Show()
        End If


        vMasterBlock = "N"
    End Sub
    Private Sub sPrint_With_Regions()
        'If vMasterBlock = "NI" Then
        '    Return
        'End If

        vClear = False
        If fSaveAll(True) = False Then
            Return
        End If
        vClear = True

        vSortedList.Clear()
        Dim vCompany As String = cControls.fReturnValue(" Select DescA From Companies " &
                                                        " Where  Code = " & vCompanyCode, Me.Name)
        Dim vSqlString As String
        vSqlString =
        "  Select Customers.Code as Customer_Code,                           " &
        "         Customers.DescA as Customer_Desc,                          " &
        "         Customers.Address as Address1,                               " &
        "         Customers.MTel as MTel1,                                    " &
        "         Customers.SalesMan_Code,                                    " &
        "         Regions.DescA as Region                                 " &
        " From Customers Left Join Regions                                  " &
        " On Regions.Code = Customers.Region_Code                           " &
        " Where 1 = 1                                                       " &
        sFndByCustomers("Customers")

        vSortedList.Add("DT_CustomerDetails", vSqlString)

        vSqlString =
       " Select DescA as CompanyName, Picture From Companies " &
       " Where  Code = " & vCompanyCode

        vSortedList.Add("DT_Header", vSqlString)

        If vLang = "A" Then
            Dim vRep_Preview As New FRM_ReportPreviewL("بيانات العملاء", vSortedList, New DS_CustomerDetails, New Rep_CustomerDetails_With_Region)
            vRep_Preview.MdiParent = Me.MdiParent
            vRep_Preview.Show()
        Else
            Dim vRep_Preview As New FRM_ReportPreviewL("Customers Details", vSortedList, New DS_CustomerDetails, New Rep_CustomerDetails_With_Region_L)
            vRep_Preview.MdiParent = Me.MdiParent
            vRep_Preview.Show()
        End If


        vMasterBlock = "N"
    End Sub
#End Region
#Region " Security                                                                       "
    Private Sub sSecurity()
        Try
            'Here I check if there is a authorization found 4 this screen
            'If cControls.fCount_Rec(" From   Profiles_Controls Inner Join Employees " & _
            '                        " ON     Employees.Profile = Profiles_Controls.Prf_Code " & _
            '                        " Where  Employees.Code = '" & vUsrCode & "'            " & _
            '                        " And    Mod_Code       = 'SI'                          ") = 0 Then

            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", True, False, "بحث")

            'Else
            vcFrmLevel.vParentFrm = Me.ParentForm

            'Here I load the authorization 4 the Btn Controls in the ToolBars
            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText =
            " Select Ctrl_Code, Enabled " &
            " From   Profiles_Controls INNER JOIN Employees " &
            " ON     Employees.Profile = Profiles_Controls.Prf_Code " &
            " Where  Employees.Code = '" & vUsrCode & "'            " &
            " And    Mod_Code       = 'Customers_Def'                   " &
            " And    Type           = 'Btn'                         "

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader

            Do While vSqlReader.Read
                If Trim(vSqlReader(1)) = "Y" Then
                    vcFrmLevel.vParentFrm.ToolBar_Main.Tools(Trim(vSqlReader(0))).SharedProps.Enabled = True
                Else
                    vcFrmLevel.vParentFrm.ToolBar_Main.Tools(Trim(vSqlReader(0))).SharedProps.Enabled = False
                End If
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()


            'Here I load the authorization For the controls in the screen
            vSQlcommand.CommandText =
            " Select Ctrl_Code, Enabled " &
            " From   Profiles_Controls INNER JOIN Employees " &
            " ON     Employees.Profile = Profiles_Controls.Prf_Code " &
            " Where  Employees.Code = '" & vUsrCode & "'            " &
            " And    Mod_Code       = 'Customers_Def'            " &
            " And    Type           = 'Ctrl'                         "

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            vSqlReader = vSQlcommand.ExecuteReader

            Do While vSqlReader.Read

                If Trim(vSqlReader(0)) = "Ctrl_SalesTypes_Departments" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Dim vRow As UltraGridRow
                        For Each vRow In Grd_SalesPrices.Rows
                            vRow.Activation = Activation.AllowEdit
                        Next
                        Grd_SalesPrices.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom
                    Else
                        Dim vRow As UltraGridRow
                        For Each vRow In Grd_SalesPrices.Rows
                            vRow.Activation = Activation.NoEdit
                        Next
                        Grd_SalesPrices.DisplayLayout.Override.AllowAddNew = AllowAddNew.No
                    End If
                End If
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sLoadPackUnits")
        End Try
    End Sub
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

        If Txt_MTel.Text.Length = 0 Then
            MessageBox.Show("تأكد من ادخال التليفون")
            Txt_MTel.Select()
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

        Return True
    End Function
    Private Sub sSaveMain()

        Dim vFirstTimeDeal, vDeduction, vFirstBalance, vFB_Type, vAct_Ser, vCountry, vCustomerType, vSalesType, vSalesMan, vRegion, vCreditLimit, vStopDeal As String
        Dim vSqlCommand As String = ""

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

            vSqlCommand = " Insert Into Customers     (             Code,                           Desc" & vLang & ",             Street,                          BuildingNum,                 Country,          CustomerType,         FirstTimeDeal,                     Remarks,                          Email1,                          Email2,                          ReceiverId,                        MTel,                   StopDeal,                   StopPay,                   CreditLimit,          Company_Code,             ContactPersonName,                    ContactPersonAddress,                    ContactPersonMTel,                    ContactPersonRemarks) " &
                                  " Values            ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_Address.Text) & "', '" & Trim(Txt_BuildingNum.Text) & "', " & vCountry & ", " & vCustomerType & ", " & vFirstTimeDeal & " , '" & Trim(Txt_Remarks.Text) & "', '" & Trim(Txt_Email1.Text) & "', '" & Trim(Txt_Email2.Text) & "', '" & Trim(Txt_ReceiverId.Text) & "', '" & Trim(Txt_MTel.Text) & "', '" & Chk_StopDeal.Tag & "', '" & Chk_StopPay.Tag & "', " & vCreditLimit & ", " & vCompanyCode & ", '" & Txt_ContactPersonName.Text & "', '" & Txt_ContactPersonAddress.Text & "', '" & Txt_ContactPersonMTel.Text & "', '" & Txt_ContactPersonRemarks.Text & "')"

            sFillSqlStatmentArray(vSqlCommand)

            'Here I will Create the Log File
            vSqlCommand = " Insert Into Customers_Logs     (             Customer_Code,                  Desc" & vLang & ",             Address,                Country,          CustomerType,         FirstTimeDeal,                    Remarks,                          Email1,                          Email2,                          Fax,                                   MTel,                   StopDeal,                   StopPay,                Company_Code,             ContactPersonName,                    ContactPersonAddress,                    ContactPersonMTel,                    ContactPersonRemarks,            Type,        Emp_Code,         TDate,             ComputerName,                                                        IPAddress    )" &
                          "                     Values     ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_Address.Text) & "', " & vCountry & ", " & vCustomerType & ", " & vFirstTimeDeal & " , '" & Trim(Txt_Remarks.Text) & "', '" & Trim(Txt_Email1.Text) & "', '" & Trim(Txt_Email2.Text) & "', '" & Trim(Txt_ReceiverId.Text) & "', '" & Trim(Txt_MTel.Text) & "', '" & Chk_StopDeal.Tag & "', '" & Chk_StopPay.Tag & "', " & vCompanyCode & ", '" & Txt_ContactPersonName.Text & "', '" & Txt_ContactPersonAddress.Text & "', '" & Txt_ContactPersonMTel.Text & "', '" & Txt_ContactPersonRemarks.Text & "', 'C',   '" & vUsrCode & "',    GetDate(),  '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "') "

            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Customers " &
                          " Set   DescA                 = '" & Trim(Txt_Desc.Text) & "', " & vbCrLf &
                          "       Street                = '" & Trim(Txt_Address.Text) & "', " & vbCrLf &
                          "       BuildingNum           = '" & Trim(Txt_BuildingNum.Text) & "', " & vbCrLf &
                          "       Country               =  " & vCountry & ", " & vbCrLf &
                          "       CustomerType          =  " & vCustomerType & ", " & vbCrLf &
                          "       FirstTimeDeal         =  " & vFirstTimeDeal & ", " & vbCrLf &
                          "       Remarks               = '" & Trim(Txt_Remarks.Text) & "', " & vbCrLf &
                          "       Email1                = '" & Trim(Txt_Email1.Text) & "', " & vbCrLf &
                          "       Email2                = '" & Trim(Txt_Email2.Text) & "', " & vbCrLf &
                          "       ReceiverId            = '" & Trim(Txt_ReceiverId.Text) & "', " & vbCrLf &
                          "       MTel                  = '" & Trim(Txt_MTel.Text) & "', " & vbCrLf &
                          "       StopDeal              = '" & Chk_StopDeal.Tag & "',  " & vbCrLf &
                          "       StopPay               = '" & Chk_StopPay.Tag & "',   " & vbCrLf &
                          "       ContactPersonName     = '" & Txt_ContactPersonName.Text & "', " & vbCrLf &
                          "       ContactPersonAddress  = '" & Txt_ContactPersonAddress.Text & "', " & vbCrLf &
                          "       ContactPersonMTel     = '" & Txt_ContactPersonMTel.Text & "', " & vbCrLf &
                          "       ContactPersonRemarks  = '" & Txt_ContactPersonRemarks.Text & "'  " & vbCrLf &
                          " Where Code                  = '" & Txt_Code.Text & "'"

            sFillSqlStatmentArray(vSqlCommand)

            'Here I will Create the Log File
            vSqlCommand = " Insert Into Customers_Logs     (             Customer_Code,                  Desc" & vLang & ",             Address,                Country,          CustomerType,         FirstTimeDeal,                    Remarks,                          Email1,                          Email2,                          Fax,                                   MTel,                   StopDeal,                   StopPay,                 Company_Code,             ContactPersonName,                    ContactPersonAddress,                    ContactPersonMTel,                    ContactPersonRemarks,          Type,        Emp_Code,         TDate,             ComputerName,                                                        IPAddress    )" &
                          "                     Values     ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_Address.Text) & "', " & vCountry & ", " & vCustomerType & ", " & vFirstTimeDeal & " , '" & Trim(Txt_Remarks.Text) & "', '" & Trim(Txt_Email1.Text) & "', '" & Trim(Txt_Email2.Text) & "', '" & Trim(Txt_ReceiverId.Text) & "', '" & Trim(Txt_MTel.Text) & "', '" & Chk_StopDeal.Tag & "', '" & Chk_StopPay.Tag & "', " & vCompanyCode & ", '" & Txt_ContactPersonName.Text & "', '" & Txt_ContactPersonAddress.Text & "', '" & Txt_ContactPersonMTel.Text & "', '" & Txt_ContactPersonRemarks.Text & "', 'U',   '" & vUsrCode & "',    GetDate(),  '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "') "

            sFillSqlStatmentArray(vSqlCommand)

        End If
    End Sub
    Private Function sSave_Pictures() As Boolean
        Dim vSqlString As String
        Dim vGetSerial As String
        Dim vCounter As Integer = 0

        Try
            cControls.fSendData(" Delete From Customers_Negotiations_Attachments " &
                               " Where CT_Code = '" & Trim(Txt_Code.Text) & "'" &
                               " AND   NG_Code = '", Me.Name)

            For Each vItem As Object In vSelectedSortedList_1.Keys

                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Pictures " &
                             " Where Customer_Code = '" & Trim(Txt_Code.Text) & "'"

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                Dim ms As New System.IO.MemoryStream
                Dim arrPicture() As Byte

                vSqlString = " Insert Into Customers_Pictures (         Customer_Code,            Ser,          Picture,                    DescA ) " &
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
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Appointments " &
                             " Where CT_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Customers_Appointments  (          CT_Code,                    Ser,              TDate,                          DescL,                                   Brief,                   Finished,           DealStatus)" &
                               "                       Values      ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", " & vDate & ", '" & Trim(vRow.Cells("DescL").Text) & "', '" & Trim(vRow.Cells("Brief").Text) & "', " & vFinished & ", " & vDealStatus & ")"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Customers_Appointments " &
                              " Set     TDay         =  " & vDate & "," &
                              "         Subject      = '" & Trim(vRow.Cells("Subject").Text) & "', " &
                              "         Description  = '" & Trim(vRow.Cells("DescL").Text) & "', " &
                              "         Finished     =  " & vFinished & ", " &
                              "         DealStatus   =  " & vDealStatus &
                              " Where   CT_Code      = '" & Txt_Code.Text & "'" &
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
            vsqlCommand.CommandText =
            " Select Customers_Appointments.Ser,        " &
            "        Customers_Appointments.TDay,      " &
            "        Customers_Appointments.Subject,      " &
            "        Customers_Appointments.Description,      " &
            "        Customers_Appointments.Finished,    " &
            "        Employees.DescA as CreatedBy_Desc,   " &
            "        Customers_Appointments.DealStatus   " &
            " From   Customers_Appointments INNER JOIN Employees  " &
            " ON     Employees.Code = Customers_Appointments.CreatedBy  " &
            " Where  CT_Code = '" & Trim(Txt_Code.Text) & "'" &
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

#Region " Deals                                                                          "
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
                    vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Deals_Main  " & vbCrLf &
                                 " Where CT_Code = '" & Txt_Code.Text & "'"
                    vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                    vSqlString = " Insert Into Customers_Deals_Main  (          CT_Code,                    Ser,                TDate,                           DescA,                Conclusion,             Finished)" & vbCrLf &
                                              "                 Values('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ",  " & vDate & ", '" & Trim(vRow.Cells("DescL").Text) & "'," & vConclusion & ", " & vFinished & ")"

                    sFillSqlStatmentArray(vSqlString)
                    vCounter += 1
                ElseIf vRow.Cells("DML").Value = "U" Then
                    vSqlString = " Update   Customers_Deals_Main " & vbCrLf &
                                  " Set     TDate    =  " & vDate & "," & vbCrLf &
                                  "         DescA    = '" & Trim(vRow.Cells("DescL").Text) & "', " & vbCrLf &
                                  "         Conclusion = " & vConclusion & ", " &
                                  "         Finished = " & vFinished & vbCrLf &
                                  " Where   CT_Code       = '" & Txt_Code.Text & "'" & vbCrLf &
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
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Deals_Details " &
                             " Where CT_Code = '" & Txt_Code.Text & "'" &
                             " And   Deal_Ser = '" & pRow.Cells("Ser").Text & "'"

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Customers_Deals_Details  (          CT_Code,                    Ser,              TDate,                          DescA,                                   Brief,                   Finished,           DealStatus)" &
                               "                       Values      ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", " & vDate & ", '" & Trim(vRow.Cells("DescL").Text) & "', '" & Trim(vRow.Cells("Brief").Text) & "', " & vFinished & ", " & vDealStatus & ")"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Customers_Deals_Details " &
                              " Set     Subject      = '" & Trim(vRow.Cells("Subject").Text) & "', " &
                              "         Description  = '" & Trim(vRow.Cells("DescL").Text) & "', " &
                              "         DealStatus   =  " & vDealStatus &
                              " Where   CT_Code      = '" & Txt_Code.Text & "'" &
                              " And     Deal_Ser     =  " & pRow.Cells("Ser").Text &
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
            vsqlCommand.CommandText =
            " Select Ser,                                                          " & vbCrLf &
            "        TDate,                                                         " & vbCrLf &
            "        DescA,                                                      " & vbCrLf &
            "        Conclusion,                                                    " &
            "        Finished                                                      " & vbCrLf &
            "        From Customers_Deals_Main                                          " & vbCrLf &
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
            vsqlCommand.CommandText =
            " Select Customers_Deals_Details.Ser,        " &
            "        Customers_Deals_Details.TDay,      " &
            "        Customers_Deals_Details.Subject,      " &
            "        Customers_Deals_Details.Description,      " &
            "        Employees.DescA as CreatedBy_Desc,   " &
            "        Customers_Deals_Details.DealStatus   " &
            " From   Customers_Deals_Details INNER JOIN Employees  " &
            " ON     Employees.Code = Customers_Deals_Details.CreatedBy  " &
            " Where  CT_Code = '" & Trim(Txt_Code.Text) & "'" &
            " And    Deal_Ser = '" & pRow.Item("Ser") & "'" &
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
                        Dim vNewFrm As New Frm_Public_Holiday_A()
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
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Customers_Representatives " &
                             " Where CT_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Customers_Representatives  (          CT_Code,                    Ser,                            DescA,                                     Address,                                   FTel,                                     MTel,                                   Fax,                                   Email,                                    Position             )" &
                               "                       Values      ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("DescL").Text) & "', '" & Trim(vRow.Cells("Address").Text) & "', '" & Trim(vRow.Cells("FTel").Text) & "', '" & Trim(vRow.Cells("MTel").Text) & "', '" & Trim(vRow.Cells("Fax").Text) & "', '" & Trim(vRow.Cells("Email").Text) & "', '" & Trim(vRow.Cells("Position").Text) & "' )"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Customers_Representatives " &
                              " Set     DescA        = '" & Trim(vRow.Cells("DescL").Text) & "', " &
                              "         Address      = '" & Trim(vRow.Cells("Address").Text) & "', " &
                              "         FTel         = '" & Trim(vRow.Cells("FTel").Text) & "', " &
                              "         MTel         = '" & Trim(vRow.Cells("MTel").Text) & "', " &
                              "         Fax          = '" & Trim(vRow.Cells("Fax").Text) & "', " &
                              "         Email        = '" & Trim(vRow.Cells("Email").Text) & "', " &
                              "         Position     = '" & Trim(vRow.Cells("Position").Text) & "' " &
                              " Where   CT_Code      = '" & Txt_Code.Text & "'" &
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
            vsqlCommand.CommandText =
            " Select Customers_Representatives.Ser,         " &
            "        Customers_Representatives.DescA,       " &
            "        Customers_Representatives.Address,      " &
            "        Customers_Representatives.FTel,        " &
            "        Customers_Representatives.MTel,        " &
            "        Customers_Representatives.Fax,         " &
            "        Customers_Representatives.Email,       " &
            "        Customers_Representatives.Position     " &
            " From   Customers_Representatives              " &
            " Where  CT_Code = '" & Trim(Txt_Code.Text) & "'" &
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
#Region " SalesTypes Departments                                                         "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidate_SalesTypes() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_SalesPrices.Rows
            If IsDBNull(vRow.Cells("SalesType").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("100", Me)
                vRow.Cells("SalesType").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("CostCenter").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("125", Me)
                vRow.Cells("CostCenter").Selected = True
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub sSave_SalesTypes()

        Dim vSqlString, vGetSerial As String
        Dim vRow As UltraGridRow
        Dim vCounter As Integer = 0
        Dim vPrice, vDeduction, vIsPercent, vAccount_Ser As String
        Grd_SalesPrices.UpdateData()

        For Each vRow In Grd_SalesPrices.Rows

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Customers_SalesTypes_Departments "

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Customers_SalesTypes_Departments (        Customer_Code,              Company_Code,                     ST_Code,                            CostCenter_Code,                                  Remarks             )" &
                             " Values                                       ('" & Trim(Txt_Code.Text) & "', " & vCompanyCode & ", " & vRow.Cells("SalesType").Value & ", '" & vRow.Cells("CostCenter").Value & "', '" & Trim(vRow.Cells("Remarks").Text) & "')"

                sFillSqlStatmentArray(vSqlString)

                'vSqlString = " Insert Into Items_PU_SalesTypes_Log (      Item_Code,                              ST_Code,                    Price,            Deduction,          IsPercent,      TDate,         Emp_Code,              ComputerName,                                                   IpAddress ) " & _
                '             "                           Values ('" & vItemCode & "', '" & vRow.Cells("SalesType").Value & "', " & vPrice & ", " & vDeduction & ", " & vIsPercent & ", GetDate(), '" & vUsrCode & "', '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "'  ) "

                'sFillSqlStatmentArray(vSqlString)

            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update Customers_SalesTypes_Departments " &
                             " Set    ST_Code         = '" & vRow.Cells("SalesType").Value & "', " &
                             "        CostCenter_Code = '" & vRow.Cells("CostCenter").Value & "', " &
                             "        Remarks         = '" & Trim(vRow.Cells("Remarks").Text) & "' " &
                             " Where  Code            = '" & vRow.Cells("Code").Value & "' " &
                             " And    Company_Code    =  " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

                'vSqlString = " Insert Into Items_PU_SalesTypes_Log (      Item_Code,                       ST_Code,                    Price,            Deduction,          IsPercent,      TDate,         Emp_Code,              ComputerName,                                                   IpAddress ) " & _
                '             "                           Values ('" & vItemCode & "', '" & vRow.Cells("SalesType").Value & "', " & vPrice & ", " & vDeduction & ", " & vIsPercent & ", GetDate(), '" & vUsrCode & "', '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "'  ) "

                'sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQuery_SalesTypes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code,                                          " &
            "        ST_Code,                                       " &
            "        CostCenter_Code,                               " &
            "        Remarks                                        " &
            " From   Customers_SalesTypes_Departments               " &
            " Where  Customer_Code = '" & Trim(Txt_Code.Text) & "'  " &
            " And    Company_Code  =  " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            'DTS_SalesPrices.Rows.Clear()
            Do While vSqlReader.Read
                DTS_SalesPrices.Rows.SetCount(vRowCounter + 1)
                'Code
                DTS_SalesPrices.Rows(vRowCounter)("Code") = vSqlReader(0)

                'SalesType
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_SalesPrices.Rows(vRowCounter)("SalesType") = vSqlReader(1)
                End If

                'Account_Ser
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_SalesPrices.Rows(vRowCounter)("CostCenter") = Trim(vSqlReader(2))
                Else
                    DTS_SalesPrices.Rows(vRowCounter)("CostCenter") = ""
                End If

                'Account_Desc
                If vSqlReader.IsDBNull(3) = False Then
                    DTS_SalesPrices.Rows(vRowCounter)("Remarks") = Trim(vSqlReader(3))
                Else
                    DTS_SalesPrices.Rows(vRowCounter)("Remarks") = ""
                End If

                DTS_SalesPrices.Rows(vRowCounter)("DML") = "N"
                'Grd_SalesPrices.Rows(vRowCounter).Cells("SalesType").Activation = Activation.NoEdit
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Grd_SalesPrices.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub Grd_SalesPrices_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_SalesPrices.CellChange
        Try
            If sender.ActiveRow.Cells("DML").Value = "NI" Then
                sender.ActiveRow.Cells("DML").Value = "I"
            ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
                sender.ActiveRow.Cells("DML").Value = "U"
            End If

            Grd_SalesPrices.UpdateData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_SalesPrices_BeforeCellDeactivate(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Grd_SalesPrices.BeforeCellDeactivate
        If Grd_SalesPrices.ActiveRow.Cells("CostCenter").Activated Then
            If Not IsDBNull(Grd_SalesPrices.ActiveRow.Cells("CostCenter").Value) Then
                Dim vRow As UltraGridRow
                For Each vRow In Grd_SalesPrices.Rows
                    If Not vRow Is Grd_SalesPrices.ActiveRow Then
                        If vRow.Cells("CostCenter").Value = Grd_SalesPrices.ActiveRow.Cells("CostCenter").Value Then
                            vcFrmLevel.vParentFrm.sForwardMessage("137", Me)
                            e.Cancel = True
                        End If
                    End If
                Next
            End If
        End If
    End Sub
#End Region
#End Region
#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummary(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "", Optional ByVal pTel As String = "", Optional ByVal pRegion As String = "")
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
            " Select  Customers.Code,                              " &
            "         Customers.DescA,                                   " &
            "         Countries.DescA as Country_Desc,                   " &
            "         Customers.CustomerType as CustomerType_Desc,        " &
            "         Sales_Types.DescA as SalesType_Desc,                 " &
            "         Customers.FirstTimeDeal,                           " &
            "         Customers.Address,                                 " &
            "         Customers.Ftel,                                    " &
            "         Customers.MTel,                                    " &
            "         CardNumber                                         " &
            " From Customers LEFT JOIN Countries                         " &
            " ON Countries.Code = Customers.Country                      " &
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

                If vSqlReader.IsDBNull(9) = False Then
                    DTS_Summary.Rows(vRowCounter)("CardNumber") = Trim(vSqlReader(9))
                Else
                    DTS_Summary.Rows(vRowCounter)("CardNumber") = ""
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

    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_FndByCode.ValueChanged, Txt_FndByTel.ValueChanged, Txt_FndByRegion.ValueChanged, Txt_FndByDesc.ValueChanged

        sQuerySummary(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text), Trim(Txt_FndByTel.Text), Trim(Txt_FndByRegion.Value))

    End Sub

    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick

        If Grd_Summary.Selected.Rows.Count > 0 Then
            'sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
        Else
            sNewRecord()
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SelectPicture.Click
        Try
            If vMasterBlock = "NI" Then
                Return
            End If
            If vMasterBlock = "I" Then
                If fSaveAll(False) = False Then
                    Return
                End If
            End If
            OpenFileDialog1.Filter = "JPEGS|*.jpg|GIFS|*.gif|Bitmaps|*.bmp|all files|*.*"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.ShowDialog()
            If Not OpenFileDialog1.FileName = "" Then
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
                cControls.sSaveImage(Image.FromFile(OpenFileDialog1.FileName), "Company")

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

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

    Private Sub Txt_CustomerType_DeleteSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_CustomerType_DeleteSelect.Click
        Txt_CustomerType.SelectedIndex = -1
    End Sub

    Private Sub Txt_Back_Click(sender As Object, e As EventArgs) Handles Txt_Back.Click
        Tab_Main.Tabs("Tab_Summary").Selected = True
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

    Private Sub Btn_ExportToExcel_Click(sender As Object, e As EventArgs) Handles Btn_ExportToExcel.Click
        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            Exit Sub
        End If

        sExportToExcel()
    End Sub
End Class