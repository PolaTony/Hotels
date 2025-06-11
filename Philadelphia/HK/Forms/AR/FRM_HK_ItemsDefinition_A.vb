Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports System.IO
Imports System.Drawing.Printing

Public Class FRM_HK_ItemsDefinition_A
    Private Declare Function ShellEx Lib "shell32.dll" Alias "ShellExecuteA" (
    ByVal hWnd As Integer, ByVal lpOperation As String,
    ByVal lpFile As String, ByVal lpParameters As String,
    ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

#Region " Declaration                                                                    "
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As New cFrmLevelVariables_A
    'Dim vcFrmLevel As Object = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".cFrmLevelVariables_" & vLang)
    Dim vSqlStatment(0) As String
    Dim vFocus As String = "Master"
    Dim vQuery As String = "N"
    Dim vSortedList As New SortedList
    Dim vClear As Boolean = True
#End Region
#Region " Form Level                                                                     "
#Region " My Form                                                                        "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'cControls.sLoadSettings(Me.Name, Grd_Summary)
        Grd_Summary.DisplayLayout.Override.FilterUIProvider = UltraGridFilterUIProvider1
        'sQueryUser(vUsrCode)

        vMasterBlock = "NI"
        'sLoadPackUnits()
        'sLoadStores()
        'sLoadColors()
        'sLoadSize()
        'sQuerySummaryMain()
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

        'Tab_Main.Tabs("Tab_Details").Selected = True


        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "«· ›«’Ì·")
        Else
            sSecurity()
            'vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "»ÕÀ")
        End If

        If vcFrmLevel.vRecPos > 0 Then
            vcFrmLevel.vParentFrm.sPrintRec(vcFrmLevel.vRecPos)
        Else
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If

        sHide_ToolbarMain_Tools()

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

        cControls.sSaveSettings(Me.Name, Grd_Summary)
    End Sub
    Private Sub FRM_Users_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Me.ParentForm.MdiChildren.Length = 1 Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, False, "", True)
        End If
    End Sub
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Remarks.ValueChanged, Txt_Price.ValueChanged,
            TXT_ItemCategoryDesc.ValueChanged, Txt_PackUnitDesc.ValueChanged, Txt_BarCode.ValueChanged, Txt_DemandPoint.ValueChanged,
            Chk_IsActive.CheckedChanged, Txt_Commission.ValueChanged,
            Txt_Desc.TextChanged, Txt_AccountDesc.TextChanged,
            Txt_Store.ValueChanged, Txt_FirstBalance.ValueChanged

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If

        If sender.Name = "Chk_IsActive" Then
            If Chk_IsActive.Checked Then
                Chk_IsActive.Tag = "Y"
                Chk_IsActive.Text = "‰‘ÿ"
            Else
                Chk_IsActive.Tag = "N"
                Chk_IsActive.Text = "€Ì— ‰‘ÿ"
            End If
        End If

    End Sub
    Private Sub sCheckIfAutoCreateItemCode()
        Try
            'Txt_PackUnit.Items.Clear()
            Dim vSQlcommand As New SqlCommand
            Dim vGenerateNewCode As String = ""

            vSQlcommand.CommandText =
            " Select IsNull(AutomaticallyGenerateCode, 'Y'), " &
            "        IsNull(IsSalesPricesEnabled, 'N') " &
            " From Controls "

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
                Txt_Code.Appearance = Txt_Code.Appearances(0)
                sNewCode()
            Else
                Txt_Code.ReadOnly = False
            End If
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sLoadPackUnits")
        End Try
    End Sub
    Private Sub ToolBar_Main_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs)
        Select Case e.Tool.Key
            Case "Btn_ItemMovement"
                sItemMovement()
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

        Dim vRow As UltraGridRow
        For Each vRow In Grd_ItemDetails.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next

        For Each vRow In Grd_ProductFormula.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next

        For Each vRow In Grd_PackUnit.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next

        For Each vRow In Grd_Details.Rows
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
                If fValidateMain() Then
                    sSaveMain()
                    sSave_MainPicture()
                Else
                    Return False
                End If

                If fValidate_BarCode() Then
                    sSave_BarCode()
                Else
                    Return False
                End If

                If fValidateProductFormula() Then
                    sSaveProductFormula()
                Else
                    Return False
                End If

                If fValidatePackUnit() Then
                    sSavePackUnit()
                Else
                    Return False
                End If

                If fValidateSave() Then
                    If sSaveDetails() Then
                        sQueryDetails()
                    End If
                End If
            Else
                Return True
            End If
        Else
            If fValidateMain() Then
                sSaveMain()
                sSave_MainPicture()
            Else
                Return False
            End If

            If fValidate_BarCode() Then
                sSave_BarCode()
            Else
                Return False
            End If

            If fValidateProductFormula() Then
                sSaveProductFormula()
            Else
                Return False
            End If

            If fValidatePackUnit() Then
                sSavePackUnit()
            Else
                Return False
            End If

            If fValidateSave() Then
                If sSaveDetails() Then
                    sQueryDetails()
                End If
            End If
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            sSetFlagsUpdate()
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            Return True
        End If
    End Function
    Private Sub sSetFlagsUpdate()
        'vMasterBlock = "NI"
        'DTS_ItemDetails.Rows.Clear()
        'DTS_Formula.Rows.Clear()
        'DTS_PackUnit.Rows.Clear()
        'sNewRecord()

        vMasterBlock = "N"

        'sQueryBarCode()
        'sQueryProductFormula()
        'sQueryPackUnit()
        'sQueryDetails()
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
                    If vFetchRec > cControls.fCount_Rec(" From  HK_Items " &
                                                        " Where Company_Code = " & vCompanyCode) Then

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
            vFetchRec = cControls.fCount_Rec(" From  HK_Items " &
                                             " Where Company_Code = " & vCompanyCode)
        End If

        Dim vFetchCondition As String
        If pItemCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And MyItems.Code = '" & Trim(pItemCode) & "'"
        End If

        'Here I set vQuery = "Y" to not load auto complete in Desc Field
        vQuery = "Y"

        Try

            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText =
            " With MyItems as " &
            "( Select HK_Items.Code, " &
            "         HK_Items.DescA as Item_Desc,                           " &
            "         HK_Items.Remarks,                                     " &
            "         HK_Items.Price,                                       " &
            "         HK_Items.BarCode,                                     " &
            "         Cat_Ser,                                           " &
            "         Categories.DescA as Cat_DescA,                     " &
            "         PU_Code,                                           " &
            "         Pack_Unit.DescA as PU_DescA,                       " &
            "         DemandPoint,                                       " &
            "         IsActive,                                          " &
            "         HK_Items.Picture,                                     " &
            "         HK_Items.Balance,                               " &
            "         ROW_Number() Over (Order By HK_Items.Code) as  RecPos " &
            " From HK_Items LEFT JOIN Categories                            " &
            " On HK_Items.Cat_Ser = Categories.Ser                          " &
            "                                                            " &
            " Inner Join Pack_Unit                                       " &
            " On HK_Items.PU_Code = Pack_Unit.Code                          " &
            "                                                            " &
            " Where HK_Items.Company_Code = " & vCompanyCode & "   )        " &
            " Select * From MyItems                                      " &
            " Where 1 = 1                                                " &
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If IsDBNull(vSqlReader("RecPos")) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader("RecPos"))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader("RecPos"))

                'Code
                Txt_Code.Text = Trim(vSqlReader("Code")).PadLeft(4, "0")

                'Desc
                Txt_Desc.Text = Trim(vSqlReader("Item_Desc"))

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader("Remarks"))
                Else
                    Txt_Remarks.Text = ""
                End If

                'Price
                If IsDBNull(vSqlReader("Price")) = False Then
                    Txt_Price.Value = Trim(vSqlReader("Price"))
                Else
                    Txt_Price.Value = Nothing
                End If

                'BarCode()
                If IsDBNull(vSqlReader("BarCode")) = False Then
                    Txt_BarCode.Value = vSqlReader("BarCode")
                Else
                    Txt_BarCode.Text = ""
                End If

                'Cat_Ser
                If IsDBNull(vSqlReader("Cat_Ser")) = False Then
                    TXT_ItemCategoryCode.Text = Trim(vSqlReader("Cat_Ser"))
                Else
                    TXT_ItemCategoryCode.Text = ""
                End If

                'Cat_Desc
                If IsDBNull(vSqlReader("Cat_DescA")) = False Then
                    TXT_ItemCategoryDesc.Text = Trim(vSqlReader("Cat_DescA"))
                Else
                    TXT_ItemCategoryDesc.Text = ""
                End If

                'PackUnit_Code
                If IsDBNull(vSqlReader("PU_Code")) = False Then
                    Txt_PackUnitCode.Text = Trim(vSqlReader("PU_Code"))
                Else
                    Txt_PackUnitCode.Text = ""
                End If

                'PackUnit_Desc
                If IsDBNull(vSqlReader("PU_DescA")) = False Then
                    Txt_PackUnitDesc.Text = Trim(vSqlReader("PU_DescA"))
                Else
                    Txt_PackUnitDesc.Text = ""
                End If

                'DemandPoint
                If IsDBNull(vSqlReader("DemandPoint")) = False Then
                    Txt_DemandPoint.Value = Trim(vSqlReader("DemandPoint"))
                Else
                    Txt_DemandPoint.Text = ""
                End If

                'Balance
                If IsDBNull(vSqlReader("Balance")) = False Then
                    Txt_FirstBalance.Value = Trim(vSqlReader("Balance"))
                Else
                    Txt_FirstBalance.Text = ""
                End If

                'IsActive
                If IsDBNull(vSqlReader("IsActive")) = False Then
                    If vSqlReader("IsActive") = "N" Then
                        Chk_IsActive.Checked = False
                    Else
                        Chk_IsActive.Checked = True
                    End If
                End If

                'Picture
                If IsDBNull(vSqlReader("Picture")) = False Then
                    Dim arrayImage() As Byte = CType(vSqlReader("Picture"), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)
                    PictureBox1.Image = Image.FromStream(ms)

                Else
                    PictureBox1.Image = Nothing
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Txt_Code.ReadOnly = True

            'sQueryBarCode()
            'sQueryProductFormula()
            'sQueryPackUnit()
            'sQueryDetails()

            vQuery = "N"
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
    End Sub
    'Private Sub sLoadItems()
    '    Try
    '        Dim vRowCounter As Integer
    '        Dim vsqlCommand As New SqlCommand
    '        vsqlCommand.CommandText = _
    '        " Select DescA  From HK_Items " & _
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
    Private Sub sFilterItems(ByVal pDesc As String)
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Top 5 DescA  From HK_Items Where Company_Code = " & vCompanyCode &
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
    Private Sub sLoadStores()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
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

#End Region
#Region " Delete                                                                         "
    Public Sub sDelete()
        If vMasterBlock = "I" Or vMasterBlock = "NI" Then

            If Grd_ItemDetails.Focused Then
                'If Not Grd_Details.ActiveRow Is Nothing Then
                Grd_ItemDetails.ActiveRow.Delete(False)
                Exit Sub
                'End If
            ElseIf vFocus = "Master" Then
                sNewRecord()
                Exit Sub
            End If
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                Dim vSqlstring As String
                If Grd_ItemDetails.Focused Then
                    If Not Grd_ItemDetails.ActiveRow Is Nothing Then
                        If Grd_ItemDetails.ActiveRow.Cells("DML").Value = "I" Or Grd_ItemDetails.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_ItemDetails.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_ItemDetails.ActiveRow.Cells("DML").Value = "N" Or Grd_ItemDetails.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring =
                            " Delete From Items_BarCode " &
                            " Where  Item_Code    = '" & Txt_Code.Text & "'" &
                            " And    Ser        = '" & Grd_ItemDetails.ActiveRow.Cells("Code").Value & "'"
                        End If
                    End If
                ElseIf Grd_ProductFormula.Focused Then
                    If Not Grd_ProductFormula.ActiveRow Is Nothing Then
                        If Grd_ProductFormula.ActiveRow.Cells("DML").Value = "I" Or Grd_ProductFormula.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_ProductFormula.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_ProductFormula.ActiveRow.Cells("DML").Value = "N" Or Grd_ProductFormula.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring =
                            " Delete From Product_Formula " &
                            " Where  Item_Code   = '" & Txt_Code.Text & "'" &
                            " And    Ser         = '" & Grd_ProductFormula.ActiveRow.Cells("Ser").Value & "'"
                        End If
                    End If
                ElseIf Grd_PackUnit.Focused Then
                    If Not Grd_PackUnit.ActiveRow Is Nothing Then
                        If Grd_PackUnit.ActiveRow.Cells("DML").Value = "I" Or Grd_PackUnit.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_PackUnit.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_PackUnit.ActiveRow.Cells("DML").Value = "N" Or Grd_PackUnit.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring =
                            " Delete From Items_PackUnit " &
                            " Where  Item_Code   = '" & Txt_Code.Text & "'" &
                            " And    Ser         = '" & Grd_PackUnit.ActiveRow.Cells("Ser").Value & "'"
                        End If
                    End If
                ElseIf vFocus = "Master" Then
                    vSqlstring =
                    " Delete From HK_Items Where Code = '" & Txt_Code.Text & "'" &
                    " Insert Into Items_Log (           Item_Code,                        DescA,            Type,        Emp_Code,      TDate,             ComputerName,                                                        IPAddress  ) " &
                    "               Values  ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "',   'D',  '" & vUsrCode & "',  GetDate(),  '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "') "
                End If

                If cControls.fSendData(vSqlstring, Me.Name) > 0 Then
                    Dim vRow As UltraGridRow

                    If Grd_ItemDetails.Focused Then
                        Grd_ItemDetails.ActiveRow.Delete(False)

                        'For Each vRow In Grd_Details.Rows
                        '    vRow.Cells("LCost").Value = (fUpdateRow(vRow) / fSumTotal() * fSumDistributed()) + fUpdateRow(vRow)
                        'Next
                    ElseIf Grd_ProductFormula.Focused Then
                        Grd_ProductFormula.ActiveRow.Delete(False)
                    ElseIf Grd_PackUnit.Focused Then
                        Grd_PackUnit.ActiveRow.Delete(False)
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
        'sOpenLov("Select Code, Name From Users", "«·„ÊŸ›Ì‰")
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
        Txt_Remarks.Clear()
        Txt_Price.Value = Nothing

        TXT_ItemCategoryCode.Text = ""
        TXT_ItemCategoryDesc.Text = ""
        Txt_PackUnitCode.Text = ""
        Txt_PackUnitDesc.Text = ""
        Txt_AccountSer.Text = ""
        Txt_AccountDesc.Text = ""
        Txt_BarCode.Text = ""
        Txt_FirstBalance.Text = ""
        Txt_Store.SelectedIndex = -1

        PictureBox1.Image = PictureBox1.InitialImage

        Txt_DemandPoint.Value = Nothing
        Chk_IsActive.Checked = True
        'sLoadItems()

        vMasterBlock = "NI"
        vFocus = "Master"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")
        DTS_ItemDetails.Rows.Clear()
        DTS_PackUnit.Rows.Clear()
        DTS_Formula.Rows.Clear()
        DTS_Details.Rows.Clear()

        Txt_Code.ReadOnly = False
        Txt_Code.Select()

        'Here I load the Auto Add Pack Units
        'sLoad_Auto_PackUnits()

        'Dim x As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCode, 'Y') From Controls ", Me.Name)
        'If x = "Y" Then
        sNewCode()
        'End If
    End Sub
    Private Sub sNewCode()

        Dim vSqlString As String

        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From HK_Items "
        Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(4, "0")
    End Sub
    Private Sub sLoad_Auto_PackUnits()
        Try
            'Txt_PackUnit.Items.Clear()
            Dim vRowCounter As Integer
            Dim vSQlcommand As New SqlCommand

            vSQlcommand.CommandText = " Select Code, DescA From Pack_Unit Where Auto = 'Y' "

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            vRowCounter = 0
            DTS_ItemDetails.Rows.Clear()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader

            Do While vSqlReader.Read
                DTS_PackUnit.Rows.SetCount(vRowCounter + 1)
                DTS_PackUnit.Rows(vRowCounter)("PU_Code") = vSqlReader(0)
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_PackUnit.Rows(vRowCounter)("DescA") = vSqlReader(1)
                End If
                'Txt_PackUnit.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sLoadPackUnits")
        End Try

    End Sub

#End Region
#Region " sOpenLov                                                                       "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String, ByVal pType As String, Optional ByVal pTableName As String = "", Optional ByVal pAdditionalString As String = "")
        vLovReturn1 = ""
        VLovReturn2 = ""
        vLovReturn3 = ""

        'If pType = "Raw_Materials" Then
        '    If vLang = "A" Then
        '        Dim Frm_RawMaterials_Lov As New Frm_RawMaterials_Lov()
        '        Frm_RawMaterials_Lov.ShowDialog()
        '    Else
        '        Dim Frm_RawMaterials_Lov As New Frm_RawMaterials_Lov
        '        Frm_RawMaterials_Lov.ShowDialog()
        '    End If

        '    If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
        '        Grd_ProductFormula.ActiveRow.Cells("Code").Value = Trim(vLovReturn1)
        '        Grd_ProductFormula.ActiveRow.Cells("DescA").Value = Trim(VLovReturn2)
        '        Grd_ProductFormula.ActiveRow.Cells("PackUnit").Value = cControls.fReturnValue(" Select PU_Code From Raw_Materials Where Code = '" & Trim(vLovReturn1) & "'", Me.Name)
        '    End If

        If vLang = "A" Then
            Dim Frm_Lov As New FRM_LovGeneral_A(pSqlStatment, pTitle, pTableName, pAdditionalString)
            Frm_Lov.ShowDialog()
        Else
            Dim Frm_Lov As New FRM_LovGeneral_L(pSqlStatment, pTitle, pTableName, pAdditionalString)
            Frm_Lov.ShowDialog()
        End If

        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then

            If pType = "Pack_Units" Then
                Txt_PackUnitCode.Text = vLovReturn1
                Txt_PackUnitDesc.Text = VLovReturn2
            ElseIf pType = "Categories" Then
                'Here I Check first if the selected Category is Parent, then Exit
                Dim vCode As String = cControls.fReturnValue(" Select Code From Categories Where Ser = " & vLovReturn1, Me.Name)

                If cControls.fIsExist(" From Categories Where Parent_Code = '" & vCode & "' ", Me.Name) = True Then
                    vcFrmLevel.vParentFrm.sForwardMessage("94", Me)
                    Exit Sub
                End If

                TXT_ItemCategoryCode.Text = vLovReturn1
                TXT_ItemCategoryDesc.Text = VLovReturn2
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
        Try
            If Tab_Main.Tabs("Tab_Summary").Selected = True Then
                vcFrmLevel.vParentFrm = Me.ParentForm
                vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "«· ›«’Ì·")
                sQuerySummaryMain()
                'sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))

                'ToolBar_Main.Tools("Btn_CopyItems").SharedProps.Visible = False
                Btn_Back.Visible = False
            Else
                vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "»ÕÀ")
                Btn_Back.Visible = True

                'If Grd_Summary.Selected.Rows.Count > 0 Then
                '    If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                '        sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
                '    Else
                '        sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
                '    End If
                'Else
                '    sNewRecord()
                'End If

                'sSecurity()

                'ToolBar_Main.Tools("Btn_CopyItems").SharedProps.Visible = True
                'ToolBar_Main.Tools("Btn_ItemMovement").SharedProps.Visible = False
                'ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = False
                'ToolBar_Main.Tools("Btn_FinalItems_Cost").SharedProps.Visible = True
                'ToolBar_Main.Tools("Btn_FinalItems_Cost_WithDetails").SharedProps.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Sub sChangeTab()
        If Tab_Main.Tabs("Tab_Summary").Selected Then
            Tab_Main.Tabs("Tab_Details").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "»ÕÀ")
        Else
            Tab_Main.Tabs("Tab_Summary").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "«· ›«’Ì·")
        End If
    End Sub
#End Region
#Region " Print                                                                          "

    Private Function sFndByItems(ByVal pTableName As String) As String
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

            sFndByItems = " And " & pTableName & ".Code  In  (" & vItemValues & ")"
        Else
            sFndByItems = ""
        End If
    End Function

#End Region
#Region " Security                                                                       "
    Private Sub sSecurity()
        Try
            'Here I check if there is a authorization found 4 this screen
            'If cControls.fCount_Rec(" From   Controls_Profiles Inner Join Employees " & _
            '                        " ON     Employees.Profile = Controls_Profiles.Prf_Code " & _
            '                        " Where  Employees.Code = '" & vUsrCode & "'            " & _
            '                        " And    Mod_Code       = 'SI'                          ") = 0 Then

            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", True, False, "»ÕÀ")

            'Else
            vcFrmLevel.vParentFrm = Me.ParentForm

            'Here I load the authorization 4 the Btn Controls in the ToolBars
            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText =
            " Select Ctrl_Code, Enabled " &
            " From   Controls_Profiles INNER JOIN Employees " &
            " ON     Employees.Profile = Controls_Profiles.Prf_Code " &
            " Where  Employees.Code = '" & vUsrCode & "'            " &
            " And    Mod_Code       = 'Items_Def'                   " &
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


            'Here I load the authorization 4 the controls in the screen
            vSQlcommand.CommandText =
            " Select Ctrl_Code, Enabled " &
            " From   Controls_Profiles INNER JOIN Employees " &
            " ON     Employees.Profile = Controls_Profiles.Prf_Code " &
            " Where  Employees.Code = '" & vUsrCode & "'            " &
            " And    Mod_Code       = 'Items_Def'                          " &
            " And    Type           = 'Ctrl'                         "

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            vSqlReader = vSQlcommand.ExecuteReader

            Do While vSqlReader.Read

                If Trim(vSqlReader(0)) = "Ctrl_PPrice" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Txt_Price.ReadOnly = False
                    Else
                        Txt_Price.ReadOnly = True
                    End If
                End If

                If Trim(vSqlReader(0)) = "Ctrl_Desc" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Txt_Desc.ReadOnly = False
                    Else
                        Txt_Desc.ReadOnly = True
                    End If
                End If

                If Trim(vSqlReader(0)) = "Ctrl_Cat" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        TXT_ItemCategoryCode.ReadOnly = False
                    Else
                        TXT_ItemCategoryCode.ReadOnly = True
                    End If
                End If

                If Trim(vSqlReader(0)) = "Ctrl_IsActive" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Chk_IsActive.Enabled = True
                    Else
                        Chk_IsActive.Enabled = False
                    End If
                End If

            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            'End If

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sLoadPackUnits")
        End Try
    End Sub
#End Region

#Region " Help                                                                           "
    Public Sub sIsHelpEnabled(ByVal pEnabled As Boolean)

        'UltraToolTipManager1.Enabled = pEnabled

    End Sub
#End Region
#End Region

#Region " Master                                                                         "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidateMain() As Boolean
        If Txt_Code.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("3", Me)
            Txt_Code.Select()
            Return False
        End If

        If vMasterBlock = "I" Then
            If cControls.fCount_Rec(" From HK_Items Where Code = '" & Txt_Code.Text & "'") > 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("12", Me)
                Txt_Code.Select()
                Txt_Code.SelectAll()
                Return False
            End If
        End If

        If Txt_Desc.Text.Length = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
            Txt_Desc.Select()
            Return False
        End If

        If cControls.fCount_Rec(" From HK_Items Where DescA = '" & Trim(Txt_Desc.Text) & "' And Code <> '" & Trim(Txt_Code.Text) & "'") > 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("82", Me)
            Txt_Desc.Select()
            Return False
        End If

        If TXT_ItemCategoryDesc.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("56", Me)
            TXT_ItemCategoryCode.Select()
            Return False
        End If

        If Txt_PackUnitDesc.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("55", Me)
            Txt_PackUnitCode.Select()
            Return False
        End If

        'If Txt_ProviderDesc.Text = "" Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("54", Me)
        '    Txt_ProviderDesc.Select()
        '    Return False
        'End If

        If vMasterBlock = "I" Then
            If Trim(Txt_BarCode.Text).Length > 0 Then
                If cControls.fIsExist(" From HK_Items Where BarCode = '" & Trim(Txt_BarCode.Text) & "'", Me.Name) = True Then
                    vcFrmLevel.vParentFrm.sForwardMessage("16", Me)
                    Txt_BarCode.SelectAll()
                    Return False
                End If
            End If
        End If

        'If Grd_ItemDetails.Rows.Count = 0 Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("52", Me)
        '    Return False
        'End If
        Return True
    End Function
    Private Sub sSaveMain()
        'If fValidateMain() = False Then
        '    Return
        'End If

        Dim vPrice, vDemandPoint As String

        Dim vSqlCommand As String = ""

        'Price
        If Not IsDBNull(Txt_Price.Value) Then
            If Not Txt_Price.Value = Nothing Then
                vPrice = Trim(Txt_Price.Value)
            Else
                vPrice = "NULL"
            End If
        Else
            vPrice = "NULL"
        End If

        'DemandPoint
        If Not IsDBNull(Txt_DemandPoint.Value) Then
            If Not Txt_DemandPoint.Value = Nothing Then
                vDemandPoint = Trim(Txt_DemandPoint.Value)
            Else
                vDemandPoint = "NULL"
            End If
        Else
            vDemandPoint = "NULL"
        End If

        If vMasterBlock = "I" Then

            sNewCode()

            vSqlCommand = " Insert Into HK_Items  (              Code,                          DescA,                             Cat_Ser,                                   PU_Code,                           Remarks,                 Price,               BarCode,                DemandPoint,              IsActive,            Company_Code )" &
                                      " Values ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(TXT_ItemCategoryCode.Text) & "', '" & Trim(Txt_PackUnitCode.Text) & "', '" & Trim(Txt_Remarks.Text) & "', " & vPrice & ", '" & Txt_BarCode.Text & "', " & vDemandPoint & ", '" & Chk_IsActive.Tag & "', " & vCompanyCode & " )"

            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   HK_Items " &
                          " Set   DescA         = '" & Trim(Txt_Desc.Text) & "', " &
                          "       Cat_Ser       = '" & Trim(TXT_ItemCategoryCode.Text) & "', " &
                          "       PU_Code       = '" & Trim(Txt_PackUnitCode.Text) & "', " &
                          "       Remarks       = '" & Trim(Txt_Remarks.Text) & "', " &
                          "       Price         =  " & vPrice & ", " &
                          "       BarCode       = '" & Txt_BarCode.Text & "', " &
                          "       DemandPoint   =  " & vDemandPoint & ", " &
                          "       Balance       =  " & Txt_FirstBalance.Text & ", " &
                          "       IsActive      = '" & Chk_IsActive.Tag & "' " &
                          "                                            " &
                          " Where Code          = '" & Txt_Code.Text & "' "

            sFillSqlStatmentArray(vSqlCommand)

        End If
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub TXT_All_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) _
    Handles TXT_ItemCategoryCode.EditorButtonClick, Txt_PackUnitCode.EditorButtonClick, Txt_AccountSer.EditorButtonClick,
    Txt_Details_PU.EditorButtonClick,
    Txt_Items.EditorButtonClick

        Try
            Dim vTitle As String

            If sender.readOnly = True Then
                Exit Sub
            End If

            If sender.name = "TXT_ItemCategoryCode" Then
                If vLang = "A" Then
                    vTitle = "«· ’‰Ì›« "
                Else
                    vTitle = "Categories"
                End If

                sOpenLov(" Select Ser, DescA From Categories Where 1 = 1 ", vTitle, "Categories", "Categories", " Select Code, DescA From Categories Where Parent_Code = '")
            ElseIf sender.name = "Txt_PackUnitCode" Then
                If vLang = "A" Then
                    vTitle = "ÊÕœ«  «· ⁄»∆…"
                Else
                    vTitle = "Pack Units"
                End If

                sOpenLov(" Select Code, DescA From Pack_Unit Where 1 = 1", vTitle, "Pack_Units")
            ElseIf sender.name = "Txt_CustomerCode" Then
                If vLang = "A" Then
                    vTitle = "«·⁄„·«¡"
                Else
                    vTitle = "Customers"
                End If

                sOpenLov(" Select Code, DescA From Customers Where 1 = 1 And Company_Code = " & vCompanyCode, vTitle, "Customers")
            ElseIf sender.name = "Txt_Details_PU" Then
                If vLang = "A" Then
                    vTitle = "ÊÕœ«  «· ⁄»∆…"
                Else
                    vTitle = "Pack Units"
                End If

                sOpenLov(" Select Code, DescA From Pack_Unit Where 1 = 1", vTitle, "Details_PU")
            ElseIf sender.name = "Txt_AccountSer" Then
                sOpenLov("", "Accounts", "Accounts")
            ElseIf sender.name = "Txt_ColorCode" Then
                If vLang = "A" Then
                    vTitle = "«·√·Ê«‰"
                Else
                    vTitle = "Colors"
                End If

                sOpenLov(" Select Code, DescA From Colors Where 1 = 1", vTitle, "Colors")
            ElseIf sender.name = "Txt_Items" Then
                If vLang = "A" Then
                    vTitle = "«·„Ê«œ «·Œ«„"
                Else
                    vTitle = "Raw Materials"
                End If

                sOpenLov(" Select Code, DescA From Raw_Materials Where 1 = 1 And IsActive = 'Y' ", vTitle, "Raw_Materials")
            ElseIf sender.name = "Txt_Str_Code" Then
                If vLang = "A" Then
                    vTitle = "«·„Œ«“‰"
                Else
                    vTitle = "Stores"
                End If

                sOpenLov(" Select Code, DescA From Stores Where 1 = 1", vTitle, "Stores")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub TXT_ItemCategoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXT_ItemCategoryCode.Validating
        If TXT_ItemCategoryCode.Text <> "" Then
            If cControls.fCount_Rec(" From Categories Where Ser = '" & Trim(TXT_ItemCategoryCode.Text) & "'") = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                TXT_ItemCategoryCode.Select()
                e.Cancel = True
            Else
                Dim vCode As String = cControls.fReturnValue(" Select Code From Categories Where Ser = " & Trim(TXT_ItemCategoryCode.Text), Me.Name)
                If cControls.fIsExist(" From Categories Where Parent_Code = '" & vCode & "' ", Me.Name) Then
                    vcFrmLevel.vParentFrm.sForwardMessage("32", Me)
                    TXT_ItemCategoryCode.Select()
                    e.Cancel = True
                Else
                    TXT_ItemCategoryDesc.Text = cControls.fReturnValue(" Select DescA From Categories Where Ser = " & Trim(TXT_ItemCategoryCode.Text), Me.Name)
                    'TXT_ItemCategoryCode.Tag = cControls.fReturnValue(" Select Ser From Categories Where Code = '" & Trim(TXT_ItemCategoryCode.Text) & "'", Me.Name)
                    e.Cancel = False
                End If
            End If
        Else
            TXT_ItemCategoryDesc.Text = ""
        End If
    End Sub
    Private Sub Txt_PackUnitCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_PackUnitCode.Validating
        If Txt_PackUnitCode.Text <> "" Then
            If cControls.fCount_Rec(" From Pack_Unit Where Code = '" & Trim(Txt_PackUnitCode.Text) & "'") = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                Txt_PackUnitCode.Select()
                e.Cancel = True
            Else
                Txt_PackUnitDesc.Text = cControls.fReturnValue(" Select DescA From Pack_Unit Where Code = '" & Trim(Txt_PackUnitCode.Text) & "'", Me.Name)
                e.Cancel = False
            End If
        Else
            Txt_PackUnitDesc.Text = ""
        End If
    End Sub

    Private Sub Txt_AccountSer_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_AccountSer.Validating
        If Txt_AccountSer.Text <> "" Then
            If cControls.fCount_Rec(" From Financial_Definitions_Tree Where Code = '" & Trim(Txt_AccountSer.Text) & "'") = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                Txt_AccountSer.Select()
                e.Cancel = True
            Else
                Txt_AccountDesc.Text = cControls.fReturnValue(" Select DescA From Financial_Definitions_Tree Where Code = '" & Trim(Txt_AccountSer.Text) & "'", Me.Name)
                Txt_AccountSer.Tag = cControls.fReturnValue(" Select Ser From Financial_Definitions_Tree Where Code = '" & Trim(Txt_AccountSer.Text) & "'", Me.Name)
                e.Cancel = False
            End If
        Else
            Txt_AccountDesc.Text = ""
        End If
    End Sub

    Private Sub Txt_All_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Txt_Code.Enter, Txt_Remarks.Enter, Txt_Price.Enter,
        TXT_ItemCategoryCode.Enter, TXT_ItemCategoryDesc.Enter, Txt_PackUnitCode.Enter,
        Txt_PackUnitDesc.Enter, Txt_Commission.Enter, Txt_Desc.Enter

        vFocus = "Master"
    End Sub
    Private Sub Txt_Code_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
    Handles Txt_Code.KeyUp, Txt_Remarks.KeyUp, Txt_Price.KeyUp,
            Txt_BarCode.KeyUp, TXT_ItemCategoryCode.KeyUp, Txt_PackUnitCode.KeyUp, Txt_DemandPoint.KeyUp, Txt_AccountSer.KeyUp,
            Txt_AccountDesc.KeyUp,
            Txt_Commission.KeyUp, Txt_Desc.KeyUp, Txt_Details_PU.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyData = Keys.F12 Then
            Dim ee As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs
            TXT_All_EditorButtonClick(sender, ee)
        End If
    End Sub

    Private Sub Btn_ItemProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ItemProperties.Click
        'If Not Txt_Desc.Text = "" Then
        '    Dim Frm_ItmPrp As New Frm_ItemProperties(Trim(Txt_Code.Text), "00", Txt_Desc.Text)
        '    Frm_ItmPrp.ShowDialog()
        'Else
        '    vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
        '    Txt_Desc.Select()
        'End If
    End Sub
    Private Sub Btn_Add_Category_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Add_Category.Click
        vLovReturn1 = ""
        VLovReturn2 = ""

        Dim vNewCategory As New Frm_Add_Category
        vNewCategory.ShowDialog()

        If vLovReturn1 <> "" Then
            TXT_ItemCategoryCode.Text = vLovReturn1
            TXT_ItemCategoryDesc.Text = VLovReturn2
        End If

    End Sub
    Private Sub Btn_SelectPicture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SelectPicture.Click
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
                'PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
                'cControls.sSaveImage(Image.FromFile(OpenFileDialog1.FileName), "Company")
                sSave_MainPicture()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Details                                                                        "
#Region " Items                                                                          "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidate_BarCode() As Boolean
        Grd_ItemDetails.UpdateData()
        Dim vRow, vRow2 As UltraGridRow
        For Each vRow In Grd_ItemDetails.Rows

            If IsDBNull(vRow.Cells("BarCode").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("85", Me)
                vRow.Cells("BarCode").Selected = True
                Return False
            End If

            If vRow.Cells("DML").Value = "I" Then
                If cControls.fIsExist(" From Items_BarCode Where BarCode = '" & Trim(vRow.Cells("BarCode").Text) & "'", Me.Name) = True Then
                    vcFrmLevel.vParentFrm.sForwardMessage("16", Me)
                    vRow.Cells("BarCode").Selected = True
                    Return False
                End If
            End If

            If vRow.Cells("DML").Value = "U" Then
                If cControls.fIsExist(" From Items_BarCode Where BarCode = '" & Trim(vRow.Cells("BarCode").Text) & "' And Item_Code <> '" & Trim(Txt_Code.Text) & "' And Ser <> " & vRow.Index, Me.Name) = True Then
                    vcFrmLevel.vParentFrm.sForwardMessage("16", Me)
                    vRow.Cells("BarCode").Selected = True
                    Return False
                End If
            End If

            For Each vRow2 In Grd_ItemDetails.Rows
                If Not vRow.Index = vRow2.Index Then
                    If vRow.Cells("BarCode").Text = vRow2.Cells("BarCode").Text Then
                        vcFrmLevel.vParentFrm.sForwardMessage("16", Me)
                        vRow2.Cells("BarCode").Selected = True
                        Return False
                    End If
                End If
            Next
        Next
        Return True
    End Function
    Private Sub sSave_BarCode()
        'If fValidate_BarCode() = False Then
        '    Return
        'End If

        Dim vRow As UltraGridRow
        Dim vSqlString, vPrice, vSPrice, vPackUnit, vGetSerial, vAdditional_Inf1, vAdditional_Inf2 As String
        Grd_ItemDetails.UpdateData()
        Dim vCounter As Integer = 0
        For Each vRow In Grd_ItemDetails.Rows

            If IsDBNull(vRow.Cells("PackUnit").Tag) Then
                vPackUnit = "NULL"
            Else
                vPackUnit = "'" & vRow.Cells("PackUnit").Value & "'"
            End If

            If IsDBNull(vRow.Cells("Additional_Inf").Value) Then
                vAdditional_Inf1 = "NULL"
            Else
                vAdditional_Inf1 = vRow.Cells("Additional_Inf").Value
            End If

            If IsDBNull(vRow.Cells("Additional_Inf2").Value) Then
                vAdditional_Inf2 = "NULL"
            Else
                vAdditional_Inf2 = vRow.Cells("Additional_Inf2").Value
            End If

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Items_BarCode " &
                             " Where  Item_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name) + vCounter
                vGetSerial = CStr(vGetSerial).PadLeft(3, "0")

                vSqlString = " Insert Into Items_BarCode  (         Item_Code,               Ser,                                  BarCode,                   Additional_Inf,            Additional_Inf2,                           Remarks             )" &
                             "                  Values   ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("BarCode").Text) & "', " & vAdditional_Inf1 & ",  " & vAdditional_Inf2 & ", '" & Trim(vRow.Cells("Remarks").Text) & "')"
                sFillSqlStatmentArray(vSqlString)
                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Items_BarCode " &
                              " Set     BarCode      = '" & Trim(vRow.Cells("BarCode").Text) & "', " &
                              "         Additional_Inf = " & vAdditional_Inf1 & ", " &
                              "         Additional_Inf2 = " & vAdditional_Inf2 & ", " &
                              "         Remarks      = '" & vRow.Cells("Remarks").Text & "' " &
                              " Where   Item_Code    = '" & Trim(Txt_Code.Text) & "'" &
                              " And     Ser          =  " & vRow.Cells("Code").Text
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryBarCode()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = " Select Ser, " &
                                      "        BarCode, " &
                                      "        Additional_Inf, " &
                                      "        Additional_Inf2, " &
                                      "        Remarks  " &
                                      " From  Items_BarCode " &
                                      " Where Item_Code = '" & Trim(Txt_Code.Text) & "'" &
                                      " Order By Ser                      "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_ItemDetails.Rows.Clear()
            Do While vSqlReader.Read
                DTS_ItemDetails.Rows.SetCount(vRowCounter + 1)
                DTS_ItemDetails.Rows(vRowCounter)("Code") = vSqlReader(0)
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_ItemDetails.Rows(vRowCounter)("BarCode") = vSqlReader(1)
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_ItemDetails.Rows(vRowCounter)("Additional_Inf") = vSqlReader(2)
                Else
                    DTS_ItemDetails.Rows(vRowCounter)("Additional_Inf") = Nothing
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_ItemDetails.Rows(vRowCounter)("Additional_Inf2") = vSqlReader(3)
                Else
                    DTS_ItemDetails.Rows(vRowCounter)("Additional_Inf2") = Nothing
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_ItemDetails.Rows(vRowCounter)("Remarks") = vSqlReader(4)
                Else
                    DTS_ItemDetails.Rows(vRowCounter)("Remarks") = ""
                End If

                DTS_ItemDetails.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_ItemDetails.UpdateData()

            Dim vRow As UltraGridRow
            For Each vRow In Grd_ItemDetails.Rows
                sQueryToolTip(vRow)
            Next
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sQueryToolTip(ByVal pRow As UltraGridRow)
        Dim vsqlCommand As New SqlClient.SqlCommand
        vsqlCommand.CommandText =
                       " Select Item_Properties.DescA, " &
                       "        Item_Properties_Elements.DescA, " &
                       "        Items_ItemProperties_Elements.ItmPrp_Code, " &
                       "        Items_ItemProperties_Elements.ItemPrpEle_Code " &
                       " From Items_ItemProperties_Elements Inner Join Item_Properties_Elements " &
                       " On   Items_ItemProperties_Elements.ItemPrpEle_Code = Item_Properties_Elements.Code " &
                       " And  Items_ItemProperties_Elements.ItmPrp_Code     = Item_Properties_Elements.ItmPrp_Code " &
                       " Inner Join Item_Properties " &
                       " On Item_Properties_Elements.ItmPrp_Code = Item_Properties.Code" &
                       " Where Items_ItemProperties_Elements.Item_Code = '" & Trim(Txt_Code.Text) & "'" &
                       " Order By Items_ItemProperties_Elements.Code "

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
        pRow.ToolTipText = ""
        Do While vSqlReader.Read
            pRow.ToolTipText += Trim(vSqlReader(0)) & " : " & Trim(vSqlReader(1))
            pRow.ToolTipText += vbCrLf
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub Grd_ItemDetails_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs)
        If Txt_PackUnitDesc.Text <> "" Then
            e.Row.Cells("PackUnit").Tag = Txt_PackUnitCode.Text
            e.Row.Cells("PackUnit").Value = Txt_PackUnitDesc.Text
        End If
    End Sub
    Private Sub Grd_ItemDetails_BeforeCellDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Grd_ItemDetails.BeforeCellDeactivate
        If Grd_ItemDetails.ActiveRow.Cells("BarCode").Activated Then
            If Grd_ItemDetails.ActiveRow.Cells("BarCode").Text.Length > 0 Then
                Dim vRow As UltraGridRow
                For Each vRow In Grd_ItemDetails.Rows
                    If Not vRow Is Grd_ItemDetails.ActiveRow Then
                        If vRow.Cells("BarCode").Text = Grd_ItemDetails.ActiveRow.Cells("BarCode").Text Then
                            vcFrmLevel.vParentFrm.sForwardMessage("16", Me)
                            e.Cancel = True
                            Exit Sub
                        Else
                            e.Cancel = False
                        End If
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub Grd_ItemDetails_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_ItemDetails.CellChange
        If Grd_ItemDetails.ActiveRow.Cells("DML").Value = "NI" Then
            Grd_ItemDetails.ActiveRow.Cells("DML").Value = "I"
        ElseIf Grd_ItemDetails.ActiveRow.Cells("DML").Value = "N" Then
            Grd_ItemDetails.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub Grd_ItemDetails_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs)
        'If Grd_ItemDetails.ActiveRow.Cells("Properties").Activated Then
        '    If Not IsDBNull(Grd_ItemDetails.ActiveRow.Cells("DescA").Value) Then
        '        Dim Frm_ItmPrp As New Frm_ItemProperties(Trim(Txt_Code.Text), Grd_ItemDetails.ActiveRow.Cells("Code").Text, Grd_ItemDetails.ActiveRow.Cells("DescA").Text)
        '        Frm_ItmPrp.ShowDialog()
        '        sQueryToolTip(Grd_ItemDetails.ActiveRow)
        '    Else
        '        vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
        '        Grd_ItemDetails.ActiveRow.Cells("DescA").Selected = True
        '    End If
        'End If

    End Sub
    Private Sub Grd_ItemDetails_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_ItemDetails.Enter
        vFocus = "Items"
    End Sub
#End Region
#End Region
#Region " Product Formula                                                                "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidateProductFormula() As Boolean
        Grd_ProductFormula.UpdateData()
        Dim vRow As UltraGridRow
        For Each vRow In Grd_ProductFormula.Rows
            If IsDBNull(vRow.Cells("DescA").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("PackUnit").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("55", Me)
                vRow.Cells("PackUnit").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("Value").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("11", Me)
                vRow.Cells("Value").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveProductFormula()
        If fValidate_BarCode() = False Then
            Return
        End If
        Dim vRow As UltraGridRow
        Dim vSqlString, vValue, vGetSerial As String
        Grd_ItemDetails.UpdateData()
        Dim vCounter As Integer = 0
        For Each vRow In Grd_ProductFormula.Rows
            'If IsDBNull(vRow.Cells("PackUnit").Tag) Then
            '    vPackUnit = "NULL"
            'Else
            '    vPackUnit = "'" & vRow.Cells("PackUnit").Value & "'"
            'End If
            If IsDBNull(vRow.Cells("Value").Value) Then
                vValue = "NULL"
            Else
                vValue = vRow.Cells("Value").Value
            End If

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Product_Formula " &
                        " Where Item_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name) + vCounter
                vGetSerial = CStr(vGetSerial).PadLeft(3, "0")

                vSqlString = " Insert Into Product_Formula  (             Item_Code,                    Ser,                     RawMaterial_Code,                             PackUnit,                       TValue,                          Remarks)" &
                             "              Values          ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("Code").Text) & "', '" & Trim(vRow.Cells("PackUnit").Value) & "', " & vValue & ", '" & Trim(vRow.Cells("Remarks").Text) & "')"
                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Product_Formula " &
                              " Set     RawMaterial_Code    = '" & Trim(vRow.Cells("Code").Text) & "'," &
                              "         PackUnit          = '" & vRow.Cells("PackUnit").Value & "', " &
                              "         TValue            =  " & vValue & ", " &
                              "         Remarks           = '" & Trim(vRow.Cells("Remarks").Text) & "' " &
                              " Where   Item_Code         = '" & Trim(Txt_Code.Text) & "'" &
                              " And     Ser               = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryProductFormula()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = " Select Product_Formula.Ser, " &
                                      "        Product_Formula.RawMaterial_Code, " &
                                      "        Raw_Materials.DescA,                 " &
                                      "        Raw_Materials.PU_Code,    " &
                                      "        Pack_Unit.DescA,             " &
                                      "        Product_Formula.TValue, " &
                                      "        Categories_RawMaterials.DescA as Cat_Desc, " &
                                      "        Product_Formula.Remarks               " &
                                      "                                              " &
                                      " From Product_Formula Inner Join Raw_Materials " &
                                      " On Product_Formula.RawMaterial_Code = Raw_Materials.Code " &
                                      "                                               " &
                                      " Inner Join Pack_Unit " &
                                      " On Raw_Materials.PU_Code = Pack_Unit.Code " &
                                      " Left Join Categories_RawMaterials " &
                                      " On Categories_RawMaterials.Ser = Raw_Materials.Cat_Ser " &
                                      "                                                         " &
                                      " Where Item_Code = '" & Trim(Txt_Code.Text) & "'" &
                                      " Order By Ser                      "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Formula.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Formula.Rows.SetCount(vRowCounter + 1)
                DTS_Formula.Rows(vRowCounter)("Ser") = vSqlReader(0)
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Formula.Rows(vRowCounter)("Code") = vSqlReader(1)
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Formula.Rows(vRowCounter)("DescA") = vSqlReader(2)
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Formula.Rows(vRowCounter)("PackUnit") = vSqlReader(3)
                Else
                    DTS_Formula.Rows(vRowCounter)("PackUnit") = Nothing
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Formula.Rows(vRowCounter)("Value") = vSqlReader(5)
                Else
                    DTS_Formula.Rows(vRowCounter)("Value") = Nothing
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Formula.Rows(vRowCounter)("Cat_Desc") = vSqlReader(6)
                Else
                    DTS_Formula.Rows(vRowCounter)("Cat_Desc") = ""
                End If

                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Formula.Rows(vRowCounter)("Remarks") = vSqlReader(7)
                Else
                    DTS_Formula.Rows(vRowCounter)("Remarks") = ""
                End If

                DTS_Formula.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_ProductFormula.UpdateData()

            'Dim vRow As UltraGridRow
            'For Each vRow In Grd_ProductFormula.Rows
            '    sQueryToolTip(vRow)
            'Next
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoadPackUnits()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, DescA From Pack_Unit " &
            " Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_PackUnit.Items.Clear()

            Do While vSqlReader.Read
                Txt_PackUnit.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            Txt_PackUnit.SelectedIndex = -1
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub Grd_ProductFormula_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_ProductFormula.CellChange
        If Grd_ProductFormula.ActiveRow.Cells("DML").Value = "NI" Then
            Grd_ProductFormula.ActiveRow.Cells("DML").Value = "I"
        ElseIf Grd_ProductFormula.ActiveRow.Cells("DML").Value = "N" Then
            Grd_ProductFormula.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub Txt_Items_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) Handles Txt_Items.EditorButtonClick
        'If sender.name = "Txt_Items" Then
        '    sOpenLov("", "Items", "Items")
        'ElseIf sender.Name = "Txt_FormulaPackUnit" Then
        '    'sOpenLov(" Select Code, DescA From Pack_Unit Where 1 = 1", "ÊÕœ… «· ⁄»∆…")
        'End If
    End Sub
#End Region
#End Region
#Region " Pack Unit                                                                      "
#Region " DataBase                                                                       "
#Region " Query                                                                          "
    Private Sub sQueryPackUnit()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Ser, PU_Code, DescA, PPrice, SPrice,  Remarks, Number, [Default] From Items_PackUnit " &
                                      " Where Item_Code = '" & Trim(Txt_Code.Text) & "'" &
                                      " Order By Ser "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_PackUnit.Rows.Clear()
            Do While vSqlReader.Read
                DTS_PackUnit.Rows.SetCount(vRowCounter + 1)
                'Ser
                If vSqlReader.IsDBNull(0) = False Then
                    DTS_PackUnit.Rows(vRowCounter)("Ser") = vSqlReader(0)
                End If

                'PU_Code
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_PackUnit.Rows(vRowCounter)("PU_Code") = vSqlReader(1)
                Else
                    DTS_PackUnit.Rows(vRowCounter)("PU_Code") = ""
                End If

                'DescA
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_PackUnit.Rows(vRowCounter)("DescA") = vSqlReader(2)
                End If

                'PPrice
                If vSqlReader.IsDBNull(3) = False Then
                    DTS_PackUnit.Rows(vRowCounter)("PPrice") = vSqlReader(3)
                Else
                    DTS_PackUnit.Rows(vRowCounter)("PPrice") = Nothing
                End If

                'SPrice
                If vSqlReader.IsDBNull(4) = False Then
                    DTS_PackUnit.Rows(vRowCounter)("SPrice") = vSqlReader(4)
                Else
                    DTS_PackUnit.Rows(vRowCounter)("SPrice") = Nothing
                End If

                'Remarks
                If vSqlReader.IsDBNull(5) = False Then
                    DTS_PackUnit.Rows(vRowCounter)("Remarks") = vSqlReader(5)
                Else
                    DTS_PackUnit.Rows(vRowCounter)("Remarks") = ""
                End If

                'Number
                If vSqlReader.IsDBNull(6) = False Then
                    DTS_PackUnit.Rows(vRowCounter)("Number") = vSqlReader(6)
                Else
                    DTS_PackUnit.Rows(vRowCounter)("Number") = Nothing
                End If

                'Local
                If vSqlReader.IsDBNull(7) = False Then
                    If vSqlReader(7) = "Y" Then
                        DTS_PackUnit.Rows(vRowCounter)("Default") = True
                    Else
                        DTS_PackUnit.Rows(vRowCounter)("Default") = False
                    End If
                End If

                DTS_PackUnit.Rows(vRowCounter)("DML") = "N"
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
    Private Function fValidatePackUnit() As Boolean
        Grd_PackUnit.UpdateData()
        Dim vRow As UltraGridRow
        Dim vCountDefault As Int16 = 0

        If Grd_PackUnit.Rows.Count > 0 Then
            For Each vRow In Grd_PackUnit.Rows
                If vRow.Cells("Default").Text = True Then
                    vCountDefault += 1
                End If
            Next


            If vCountDefault = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("55", Me)
                Return False
            End If

            If vCountDefault > 1 Then
                vcFrmLevel.vParentFrm.sForwardMessage("89", Me)
                Return False
            End If

            For Each vRow In Grd_PackUnit.Rows
                If vRow.Cells("DML").Text <> "NI" Then
                    If vRow.Cells("DescA").Text = "" Then
                        vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
                        vRow.Cells("DescA").Selected = True
                        Return False
                    End If
                    If vRow.Cells("Number").Text.Length = 0 Then
                        vcFrmLevel.vParentFrm.sForwardMessage("62", Me)
                        vRow.Cells("Number").Selected = True
                        Return False
                    End If
                End If

                'If Not IsDBNull(vRow.Cells("PPrice").Value) And Not IsDBNull(vRow.Cells("SPrice").Value) Then
                '    If vRow.Cells("PPrice").Value >= vRow.Cells("SPrice").Value Then
                '        vcFrmLevel.vParentFrm.sForwardMessage("86", Me)
                '        vRow.Cells("PPrice").Selected = True
                '        Return False
                '    End If
                'End If

                'If Not IsDBNull(vRow.Cells("PPrice").Value) Then
                '    If cControls.fCount_Rec(" From Items_PackUnit_SalesTypes " & _
                '                            " Where Item_Code = '" & Trim(Txt_Code.Text) & "'" & _
                '                            " And   PU_Ser    = '" & vRow.Cells("Ser").Value & "'" & _
                '                            " And   Price <= '" & vRow.Cells("PPrice").Value & "'") > 0 Then

                '        vcFrmLevel.vParentFrm.sForwardMessage("86", Me)
                '        vRow.Cells("PPrice").Selected = True
                '        Return False
                '    End If
                'End If
            Next
        End If

        Return True
    End Function
    Private Sub sSavePackUnit()
        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Grd_PackUnit.UpdateData()
        Dim vUnit, vNumber, vPPrice, vSPrice, vGetSerial, vDefault As String
        Dim vCounter As Integer = 0
        For Each vRow In Grd_PackUnit.Rows
            If IsDBNull(vRow.Cells("PPrice").Value) Then
                vPPrice = "NULL"
            Else
                vPPrice = vRow.Cells("PPrice").Value
            End If

            If IsDBNull(vRow.Cells("SPrice").Value) Then
                vSPrice = "NULL"
            Else
                vSPrice = vRow.Cells("SPrice").Value
            End If

            If IsDBNull(vRow.Cells("Number").Value) Then
                vNumber = "NULL"
            Else
                vNumber = vRow.Cells("Number").Value
            End If

            If vRow.Cells("Default").Text = True Then
                vDefault = "'Y'"
            Else
                vDefault = "'N'"
            End If

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Items_PackUnit " &
                             " Where Item_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(2, "0") + vCounter

                vSqlString = " Insert Into Items_PackUnit    (          Item_Code,                 Ser,                                PU_Code,                                   DescA,                  PPrice,         SPrice,                           Remarks,                    Number,           [Default])" &
                             " Values                        ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("PU_Code").Text) & "', '" & Trim(vRow.Cells("DescA").Text) & "'," & vPPrice & "," & vSPrice & ", '" & Trim(vRow.Cells("Remarks").Text) & "',   " & vNumber & ", " & vDefault & ")"
                sFillSqlStatmentArray(vSqlString)

                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update Items_PackUnit " &
                             " Set    PU_Code      = '" & vRow.Cells("PU_Code").Text & "', " &
                             "        DescA        = '" & vRow.Cells("DescA").Text & "', " &
                             "        PPrice       =  " & vPPrice & ", " &
                             "        SPrice       =  " & vSPrice & ", " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "', " &
                             "        Number       =  " & vNumber & ", " &
                             "        [Default]    =  " & vDefault &
                             " Where  Item_Code    = '" & Trim(Txt_Code.Text) & "' " &
                             " And    Ser          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub GRD_PackUnit_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_PackUnit.CellChange
        If Grd_PackUnit.ActiveRow.Cells("DML").Value = "NI" Then
            Grd_PackUnit.ActiveRow.Cells("DML").Value = "I"
        ElseIf Grd_PackUnit.ActiveRow.Cells("DML").Value = "N" Then
            Grd_PackUnit.ActiveRow.Cells("DML").Value = "U"
        End If

        If Grd_PackUnit.ActiveRow.Cells("Default").Activated Then
            If Grd_PackUnit.ActiveRow.Cells("Default").Text = True Then
                Dim vRow As UltraGridRow
                For Each vRow In Grd_PackUnit.Rows
                    If Not vRow Is Grd_PackUnit.ActiveRow Then
                        vRow.Cells("Default").Value = False

                        If vRow.Cells("DML").Value = "NI" Then
                            vRow.Cells("DML").Value = "I"
                        ElseIf vRow.Cells("DML").Value = "N" Then
                            vRow.Cells("DML").Value = "U"
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Grd_PackUnit_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_PackUnit.Enter
        vFocus = "PackUnit"
    End Sub
    Private Sub GRD_PackUnit_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_PackUnit.KeyUp

        Grd_PackUnit.UpdateData()
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        ElseIf e.KeyData = Keys.Enter Then
            If Not Grd_PackUnit.ActiveRow Is Nothing Then
                If Grd_PackUnit.ActiveRow.Cells("PU_Code").Activated = True Then
                    If IsDBNull(Grd_PackUnit.ActiveRow.Cells("PU_Code").Value) = False Then
                        If Grd_PackUnit.ActiveRow.Cells("PU_Code").Value <> "" Then
                            If cControls.fCount_Rec("From Pack_Unit " &
                                " Where Code = '" & Grd_PackUnit.ActiveRow.Cells("PU_Code").Value & "'") = 0 Then

                                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                                Grd_PackUnit.ActiveRow.Cells("PU_Code").SelectAll()
                                Grd_PackUnit.ActiveRow.Cells("DescA").Value = ""
                            Else
                                Dim vSqlString As String = " Select DescA From Pack_Unit " &
                                    "Where Code = '" & Grd_PackUnit.ActiveRow.Cells("PU_Code").Value & "'"
                                Grd_PackUnit.ActiveRow.Cells("DescA").Value = cControls.fReturnValue(vSqlString, Me.Name)
                                Grd_PackUnit.PerformAction(UltraGridAction.PrevCell)
                                Grd_PackUnit.PerformAction(UltraGridAction.PrevCell)
                                Grd_PackUnit.PerformAction(UltraGridAction.EnterEditMode)
                            End If

                        End If
                    End If
                ElseIf Grd_PackUnit.ActiveRow.Cells("Number").Activated = True Then
                    Grd_PackUnit.PerformAction(UltraGridAction.NextRow)
                    Grd_PackUnit.ActiveRow.Cells("PU_Code").Selected = True
                    Grd_PackUnit.ActiveRow.Cells("PU_Code").Activate()
                    Grd_PackUnit.PerformAction(UltraGridAction.EnterEditMode)
                Else
                    Grd_PackUnit.PerformAction(UltraGridAction.PrevCell)
                    Grd_PackUnit.PerformAction(UltraGridAction.EnterEditMode)
                End If
            End If
        ElseIf e.KeyData = Keys.F12 Then
            If Grd_PackUnit.ActiveRow.Cells("PU_Code").Activated = True Then
                'sOpenLov(" Select Code, DescA From Pack_Unit Where 1 = 1", "ÊÕœ… «· ⁄»∆Â")
            End If
        End If
    End Sub
    Private Sub Grd_PackUnit_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_PackUnit.ClickCellButton
        Try
            Grd_PackUnit.UpdateData()
            If Grd_PackUnit.ActiveRow.Cells("SalesPrices").Activated Then
                If Grd_PackUnit.ActiveRow.Cells("DescA").Text = "" Then
                    vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
                    Grd_PackUnit.ActiveRow.Cells("DescA").Selected = True
                    Return
                End If

                If Grd_PackUnit.ActiveRow.Cells("Ser").Text = "" Then
                    If fSaveAll(False) = False Then
                        Return
                    End If
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#End Region
#Region " Images                                                                         "
#Region " DataBase                                                                       "
#Region " Query                                                                          "
    Private Sub sQueryDetails()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText =
            " Select Ser,                   " &
            "        FileData,              " &
            "        DescA,                 " &
            "        CompleteFileName,      " &
            "        FileName,              " &
            "        FileType               " &
            " From   Items_Images         " &
            " Where  Item_Code  =       '" & Trim(Txt_Code.Text) & "'"

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vRow As UltraGridRow
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Details.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Details.Rows.SetCount(vRowCounter + 1)
                DTS_Details.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))

                'Picture
                If vSqlReader.IsDBNull(1) = False Then
                    'Dim vVar As Object = DTS_Details.Rows(vRowCounter)("Picture").GetType
                    Dim arrayImage() As Byte = CType(vSqlReader(1), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)

                    If vSqlReader.IsDBNull(4) = False Then
                        If Trim(vSqlReader(4)) = "Image" Then
                            DTS_Details.Rows(vRowCounter)("Picture") = Image.FromStream(ms)
                        End If
                    End If

                Else
                    DTS_Details.Rows(vRowCounter)("Picture") = Nothing
                End If

                'DescA
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Details.Rows(vRowCounter)("DescL") = Trim(vSqlReader(2))
                Else
                    DTS_Details.Rows(vRowCounter)("DescL") = ""
                End If

                'File Name
                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Details.Rows(vRowCounter)("CompleteFileName") = Trim(vSqlReader(3))
                Else
                    DTS_Details.Rows(vRowCounter)("CompleteFileName") = ""
                End If

                'File Name
                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Details.Rows(vRowCounter)("FileName") = Trim(vSqlReader(4))
                Else
                    DTS_Details.Rows(vRowCounter)("FileName") = ""
                End If

                'File Type
                If vSqlReader.IsDBNull(5) = False Then
                    If Trim(LCase(vSqlReader(5))) = ".bmp" Or
                        Trim(LCase(vSqlReader(5))) = ".jpg" Or
                        Trim(LCase(vSqlReader(5))) = ".jpeg" Or
                        Trim(LCase(vSqlReader(5))) = ".png" Or
                        Trim(LCase(vSqlReader(5))) = ".gif" Or
                        Trim(LCase(vSqlReader(5))) = "image" Then

                        DTS_Details.Rows(vRowCounter)("FileType") = "Image"
                    ElseIf Trim(LCase(vSqlReader(5))) = ".docx" Then
                        DTS_Details.Rows(vRowCounter)("FileType") = ".docx"
                    ElseIf Trim(LCase(vSqlReader(5))) = ".xls" Then
                        DTS_Details.Rows(vRowCounter)("FileType") = ".xls"
                    ElseIf Trim(LCase(vSqlReader(5))) = ".pdf" Then
                        DTS_Details.Rows(vRowCounter)("FileType") = ".pdf"
                    End If
                Else
                    DTS_Details.Rows(vRowCounter)("FileType") = ""
                End If

                DTS_Details.Rows(vRowCounter)("SerNum") = DTS_Details.Rows(vRowCounter).Index + 1
                DTS_Details.Rows(vRowCounter)("DML") = "N"

                'Grd_Details.Rows(vRowCounter).Height = 200

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
#Region " Save                                                                           "

    Private Function fValidateSave() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            'If vRow.Cells("FileName").Text = "" Then
            '    vRow.Cells("FileName").Selected = True
            '    MessageBox.Show("Select the file first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Return False
            'End If
        Next
        Return True
    End Function
    Private Function sSaveDetails() As Boolean
        Dim vSqlString As String
        Dim vGetSerial As String
        Dim vCounter As Integer = 0

        Try
            For Each vRow As UltraGridRow In Grd_Details.Rows
                'Call Upload Images Or File

                Dim Extension As String = System.IO.Path.GetExtension(vRow.Cells("FileName").Text)
                Dim vFileType As String

                Dim imageData As Byte()
                Dim sFileName As String

                Dim FileData() As Byte

                If vRow.Cells("CompleteFileName").Text <> "" Then
                    FileData = ReadFileData(vRow.Cells("CompleteFileName").Text)
                End If

                If vRow.Cells("FileName").Text <> "" Then
                    If vRow.Cells("DML").Value = "I" Then

                        vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Items_Images " &
                                     " Where Item_Code = '" & Trim(Txt_Code.Text) & "'"

                        vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                        vSqlString = " Insert Into Items_Images (           Item_Code,                  Ser,           FileData,                   DescA,                               FileName,                   FileType ) " &
                                     "                  Values           ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", (@FileData), '" & vRow.Cells("DescL").Text & "', '" & vRow.Cells("FileName").Text & "', '" & vFileType & "')"

                        Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)
                        vMyCommand.Parameters.AddWithValue("@FileData ", FileData)

                        cControls.vSqlConn.Open()
                        vMyCommand.ExecuteNonQuery()
                        cControls.vSqlConn.Close()

                        vcFrmLevel.vParentFrm.sForwardMessage("7", Me)

                        vCounter += 1
                    ElseIf vRow.Cells("DML").Value = "U" Then
                        Dim ms As New System.IO.MemoryStream
                        Dim arrPicture() As Byte

                        vSqlString = " Update Items_Images " &
                                     " Set    DescA = '" & vRow.Cells("DescL").Text & "',  " &
                                     "        FileData = (@FileData),                     " &
                                     "        FileName = '" & vRow.Cells("FileName").Text & "', " &
                                     "        FileType = '" & vFileType & "'" &
                                     " Where  Item_Code  = '" & Trim(Txt_Code.Text) & "'" &
                                     " And    Ser      = '" & vRow.Cells("Ser").Text & "'"

                        Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)
                        vMyCommand.Parameters.AddWithValue("@FileData ", FileData)

                        cControls.vSqlConn.Open()
                        vMyCommand.ExecuteNonQuery()
                        cControls.vSqlConn.Close()

                        vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
                    End If
                End If
            Next

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()

            Return False
        End Try

    End Function

    Private Function ReadFileData(ByVal filename As String) As Byte()
        Dim fs As New System.IO.FileStream(filename, IO.FileMode.Open)
        Dim br As New System.IO.BinaryReader(fs)

        Dim data() As Byte = br.ReadBytes(fs.Length)

        br.Close()
        fs.Close()

        Return (data)
    End Function

    Private Sub WriteFileData(ByVal pCompleteFileName As String, ByVal pFilename As String, ByVal pData As Byte())

        'Dim vFileName As String
        'For Each vFileName In Directory.GetFiles("C:\DotNet_eg_Files")
        '    If vFileName = pFilename Then
        '        GoTo Done
        '    End If
        'Next

        Dim fs As New System.IO.FileStream("C:\DotNet_eg_Files\" & pFilename, IO.FileMode.Create)
        Dim bw As New System.IO.BinaryWriter(fs)

        bw.Write(pData)

        bw.Close()
        fs.Close()

        Dim vProcess As New Process
        vProcess.StartInfo.FileName = "C:\DotNet_eg_Files\" & pFilename
        vProcess.StartInfo.CreateNoWindow = False
        vProcess.Start()


    End Sub
#End Region
#End Region
#Region " Grd                                                                            "
    Private Sub Grd_Details_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles Grd_Details.AfterRowInsert
        e.Row.Cells("SerNum").Value = e.Row.Index + 1
    End Sub

    Private Sub Grd_Details_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Details.CellChange
        If Grd_Details.ActiveRow.Cells("DML").Value = "NI" Then
            Grd_Details.ActiveRow.Cells("DML").Value = "I"
        ElseIf Grd_Details.ActiveRow.Cells("DML").Value = "N" Then
            Grd_Details.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub

    Private Sub Grd_Details_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Details.ClickCellButton
        Try
            'Grd_Details.ActiveRow.Cells("Picture").Value = Image.FromFile(OpenFileDialog1.FileName)
            'Grd_Details.ActiveRow.Height = 200

            'PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
            'Dim x As Integer = cControls.sSaveImage(Image.FromFile(OpenFileDialog1.FileName), "Company")
            Dim vFileName As String
            Dim vRowCounter, vIndex As Integer
            Dim vSqlString As String

            If Grd_Details.ActiveRow.Cells("AddNew").Activated Then
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    If OpenFileDialog1.FileNames.Length > 1 Then
                        vRowCounter = DTS_Details.Rows.Count
                        vIndex = 0
                        For Each vFileName In OpenFileDialog1.FileNames
                            DTS_Details.Rows.SetCount(vRowCounter + 1)
                            DTS_Details.Rows(vRowCounter)("FileName") = OpenFileDialog1.SafeFileNames(vIndex)
                            DTS_Details.Rows(vRowCounter)("CompleteFileName") = vFileName
                            DTS_Details.Rows(vRowCounter)("DescL") = OpenFileDialog1.SafeFileNames(vIndex)
                            DTS_Details.Rows(vRowCounter)("SerNum") = vRowCounter
                            DTS_Details.Rows(vRowCounter)("DML") = "I"
                            vRowCounter += 1
                            vIndex += 1
                        Next
                    Else
                        Grd_Details.ActiveRow.Cells("FileName").Value = OpenFileDialog1.SafeFileName
                        Grd_Details.ActiveRow.Cells("CompleteFileName").Value = OpenFileDialog1.FileName
                        Grd_Details.ActiveRow.Cells("DescL").Value = OpenFileDialog1.SafeFileName

                        If Grd_Details.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_Details.ActiveRow.Cells("DML").Value = "I"
                        ElseIf Grd_Details.ActiveRow.Cells("DML").Value = "N" Then
                            Grd_Details.ActiveRow.Cells("DML").Value = "U"
                        End If
                    End If

                    Grd_Details.DisplayLayout.Bands(0).AddNew()
                    'If OpenFileDialog1.FileName <> "" Then

                    'End If
                End If

            ElseIf Grd_Details.ActiveRow.Cells("View").Activated Then

                'Temporary I Check if the File path is exist and open it Through Process Function..
                If Grd_Details.ActiveRow.Cells("FileName").Text <> "" Then
                    If Grd_Details.ActiveRow.Cells("DML").Text = "I" Or Grd_Details.ActiveRow.Cells("DML").Text = "NI" Then
                        sOpenUnSavedFiles(Grd_Details.ActiveRow.Cells("CompleteFileName").Text)
                        Exit Sub
                    ElseIf Grd_Details.ActiveRow.Cells("DML").Text = "N" Or Grd_Details.ActiveRow.Cells("DML").Text = "U" Then
                        If Not Directory.Exists("C:\DotNet_eg_Files") Then
                            Directory.CreateDirectory("C:\DotNet_eg_Files")
                        End If

                        cControls.vSqlConn.Close()

                        Dim cmd As New SqlCommand("", cControls.vSqlConn)

                        cmd.CommandText = " Select FileData FROM Items_Images " &
                                          " WHERE Item_Code = '" & Trim(Txt_Code.Text) & "'" &
                                          " AND   Ser      = " & Grd_Details.ActiveRow.Cells("Ser").Value

                        cControls.vSqlConn.Open()

                        Dim rdr As SqlDataReader = cmd.ExecuteReader

                        If rdr.Read Then
                            WriteFileData(Grd_Details.ActiveRow.Cells("CompleteFileName").Text, Grd_Details.ActiveRow.Cells("FileName").Text, rdr("FileData"))
                        Else
                            MsgBox(Grd_Details.ActiveRow.Cells("FileName").Text & " not found")
                        End If

                        rdr.Close()
                        cControls.vSqlConn.Close()
                    End If
                Else
                    Exit Sub
                End If
            ElseIf Grd_Details.ActiveRow.Cells("Delete").Activated Then
                If Grd_Details.ActiveRow.Cells("DML").Text = "NI" Or Grd_Details.ActiveRow.Cells("DML").Text = "I" Then
                    Grd_Details.ActiveRow.Delete(False)

                ElseIf Grd_Details.ActiveRow.Cells("DML").Text = "N" Or Grd_Details.ActiveRow.Cells("DML").Text = "U" Then
                    'First I Check if this Invoice is Submitted by another user then exist Immediatly
                    'If cControls.fReturnValue(" Select Status From Sales_Invoices Where Code = '" & Trim(Txt_Code.Text) & "' ", Me.Name) = "P" Then
                    '    vcFrmLevel.vParentFrm.sForwardMessage("134", Me)
                    '    Return
                    'End If

                    If vcFrmLevel.vParentFrm.sForwardMessage("133", Me) = MsgBoxResult.Yes Then

                        sEmptySqlStatmentArray()

                        vSqlString =
                        " Delete From Items_Images " &
                        " Where  Item_Code     = '" & Txt_Code.Text & "' " &
                        " And    Ser         =  " & Grd_Details.ActiveRow.Cells("Ser").Text

                        sFillSqlStatmentArray(vSqlString)

                        If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then
                            Grd_Details.ActiveRow.Delete(False)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub downLoadFile(ByVal iFileId As Long, ByVal sFileName As String, ByVal sFileExtension As String)
        Dim strSql As String
        'For Document
        Try
            'Get image data from gridview column. 
            strSql = "Select FileData from Customers_Deals_Details_Attachments " &
                     " WHERE CT_Code = '" & Trim(Txt_Code.Text) & "'" &
                     " And   Ser = " & Grd_Details.ActiveRow.Cells("Ser").Value

            Dim sqlCmd As New SqlCommand(strSql, cControls.vSqlConn)

            'Get image data from DB
            cControls.vSqlConn.Open()
            Dim fileData As Byte() = DirectCast(sqlCmd.ExecuteScalar(), Byte())
            cControls.vSqlConn.Close()

            Dim sTempFileName As String = Application.StartupPath & "\" & sFileName

            If Not fileData Is Nothing Then
                'Read image data into a file stream 
                Using fs As New FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Write(fileData, 0, fileData.Length)
                    'Set image variable value using memory stream. 
                    fs.Flush()
                    fs.Close()
                End Using

                'Open File
                ' 10 = SW_SHOWDEFAULT
                ShellEx(Me.Handle, "Open", sFileName, "", "", 10)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function ReadFile(ByVal sPath As String) As Byte()
        'Initialize byte array with a null value initially. 
        Dim data As Byte() = Nothing

        'Use FileInfo object to get file size. 
        Dim fInfo As New FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length

        'Open FileStream to read file 
        Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)

        'Use BinaryReader to read file stream into byte array. 
        Dim br As New BinaryReader(fStream)

        'When you use BinaryReader, you need to supply number of bytes to read from file. 
        'In this case we want to read entire file. So supplying total number of bytes. 
        data = br.ReadBytes(CInt(numBytes))

        Return data
    End Function

    Private Sub sOpenUnSavedFiles(ByVal pFileName As String)

        Dim vProcess As New Process
        vProcess.StartInfo.FileName = pFileName
        vProcess.StartInfo.CreateNoWindow = False
        vProcess.Start()
    End Sub
#End Region
#End Region
#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummaryMain(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            Dim vCodeFilter As String
            Dim vFullText() As String

            vFullText = Trim(Txt_FndByDesc.Text).Split

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText =
            "  Select HK_Items.Code, " &
            "         HK_Items.DescA as DescA,                             " &
            "         HK_Items.Price,                                      " &
            "         Categories.DescA as Cat_Desc,                     " &
            "         Pack_Unit.DescA as PU_Desc,                       " &
            "         IsNull(IsActive, 'Y') as Status                   " &
            "                                                           " &
            " From HK_Items LEFT Join Categories                           " &
            " On HK_Items.Cat_Ser = Categories.Ser                         " &
            "                                                           " &
            " INNER Join Pack_Unit                                      " &
            " On HK_Items.PU_Code = Pack_Unit.Code                         " &
            "                                                           " &
            " Where 1 = 1                                               " &
            " And HK_Items.Company_Code = " & vCompanyCode &
            sFndByCategories() &
            fReturnSplitValues(vFullText)


            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)
                DTS_Summary.Rows(vRowCounter)("Code") = Trim(vSqlReader("Code"))

                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_Summary.Rows(vRowCounter)("DescA") = Trim(vSqlReader("DescA"))
                End If

                If IsDBNull(vSqlReader("Price")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Price") = vSqlReader("Price")
                Else
                    DTS_Summary.Rows(vRowCounter)("Price") = Nothing
                End If

                If IsDBNull(vSqlReader("Cat_Desc")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Category") = Trim(vSqlReader("Cat_Desc"))
                Else
                    DTS_Summary.Rows(vRowCounter)("Category") = ""
                End If

                If IsDBNull(vSqlReader("PU_Desc")) = False Then
                    DTS_Summary.Rows(vRowCounter)("PackUnit") = Trim(vSqlReader("PU_Desc"))
                Else
                    DTS_Summary.Rows(vRowCounter)("PackUnit") = ""
                End If

                If vSqlReader("Status") = "Y" Then
                    DTS_Summary.Rows(vRowCounter)("Status") = "‰‘ÿ"
                Else
                    DTS_Summary.Rows(vRowCounter)("Status") = "€Ì— ‰‘ÿ"
                    'Grd_Summary.Rows(vRowCounter).Appearance.BackColor2 = Color.White
                    'Grd_Summary.Rows(vRowCounter).Appearance.ForeColor = Color.LightBlue
                End If

                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()

            Txt_RowCount.Text = Grd_Summary.Rows.Count
            'Dim vRow As UltraDataRow
            'Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
            'For Each vRow In DTS_Summary.Rows
            '    If cBase.fCount_Rec(" From HK_Items Where Code = '" & vRow("Code") & "' And Ser <> '00'") > 0 Then
            '        sQuerySummaryDetails(vRow, vChildBand)
            '    End If
            'Next
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
            vsqlCommand.CommandText = " Select Ser, DescA, Price, Remarks " &
                                      " From HK_Items " &
                                      " Where Code = '" & pRow("Code") & "'" &
                                      " Order By Ser       "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            'vChildRows.Clear()
            Do While vSqlReader.Read
                vChildRows.SetCount(vRowCounter + 1)
                vChildRows(vRowCounter)("Code") = vSqlReader(0)
                If vSqlReader.IsDBNull(1) = False Then
                    vChildRows(vRowCounter)("DescA") = vSqlReader(1)
                End If
                If vSqlReader.IsDBNull(2) = False Then
                    vChildRows(vRowCounter)("Price") = vSqlReader(2)
                Else
                    vChildRows(vRowCounter)("Price") = Nothing
                End If
                If vSqlReader.IsDBNull(3) = False Then
                    vChildRows(vRowCounter)("Remarks") = vSqlReader(3)
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
        End Try
    End Sub

    Private Sub Grd_Summary_BeforeRowExpanded(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CancelableRowEventArgs) Handles Grd_Summary.BeforeRowExpanded
        Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
        Dim vRow As UltraDataRow = DTS_Summary.Rows(e.Row.Index)
        sQuerySummaryDetails(vRow, vChildBand)
    End Sub
    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged
        'sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
    End Sub
    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick
        If Grd_Summary.Selected.Rows.Count > 0 Then
            sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
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

    Private Function fReturnSplitValues(ByVal pFullText() As String)
        Try
            Dim vCount As Integer
            Dim vReturnedValue As String = ""
            Dim vItems As String = ""

            If pFullText(0) = "" Then
                Return vReturnedValue
            End If

            'Return "And Contains (HK_Items.DescA, 'FormsOf (THESAURUS, John)' )"

            'If pFullText.Length = 1 Then
            '    Return " And HK_Items.DescA Like '%" & pFullText(0) & "%' "
            'End If

            For vCount = 0 To pFullText.Length - 1
                If vItems.Length > 0 Then
                    vItems += " And """ & pFullText(vCount) & "*" & """"
                Else
                    vItems += """" & pFullText(vCount) & "*" & """"
                End If
            Next

            vReturnedValue = " And Contains (HK_Items.DescA, '" & vItems & "' ) "

            Return vReturnedValue
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function
    Private Function sFndByCategories() As String
        Dim vLstItem As ValueListItem
        Dim vItemValues As String = ""

        If Txt_Categories_Search.CheckedItems.Count > 0 Then
            For Each vLstItem In Txt_Categories_Search.CheckedItems
                If Trim(vItemValues.Length) > 0 Then
                    vItemValues += ", "
                End If
                vItemValues += " '" & vLstItem.DataValue & "' "
            Next
            sFndByCategories = " And HK_Items.Cat_Ser In  (" & vItemValues & ")"
        Else
            sFndByCategories = ""
        End If
    End Function

#End Region

    Private Function sSave_MainPicture()
        Try
            If Not IsDBNull(PictureBox1.Image) Then
                If Not PictureBox1.Image Is Nothing Then
                    Dim vSqlString As String
                    Dim ms As New System.IO.MemoryStream
                    Dim arrPicture() As Byte

                    vSqlString = " Update HK_Items Set Picture = (@image) Where Code = '" & Trim(Txt_Code.Text) & "'"

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
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try
    End Function

    Private Sub sItemMovement()
        If Grd_Summary.Selected.Rows.Count = 0 Then
            Exit Sub
        End If

        'Dim vFrm_ItemMovement As New Frm_ItemsMovement_FollowUp_A(Grd_Summary.Selected.Rows(0).Cells("Code").Text, Grd_Summary.Selected.Rows(0).Cells("DescA").Text)
        'vFrm_ItemMovement.MdiParent = vcFrmLevel.vParentFrm
        'vFrm_ItemMovement.Show()
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

    Private Sub Txt_Back_Click(sender As Object, e As EventArgs) Handles Btn_Back.Click
        Tab_Main.Tabs("Tab_Summary").Selected = True
    End Sub

    Private Sub Btn_Add_PackUnit_Click(sender As Object, e As EventArgs) Handles Btn_Add_PackUnit.Click
        vLovReturn1 = ""
        VLovReturn2 = ""

        Dim vNewPackUnit As New Frm_Add_PackUnit
        vNewPackUnit.ShowDialog()

        If vLovReturn1 <> "" Then
            Txt_PackUnitCode.Text = vLovReturn1
            Txt_PackUnitDesc.Text = VLovReturn2
        End If
    End Sub
End Class