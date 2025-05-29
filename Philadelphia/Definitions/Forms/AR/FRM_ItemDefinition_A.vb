Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports System.IO
Imports System.Drawing.Printing

Public Class FRM_ItemDefinition_A
    Private Declare Function ShellEx Lib "shell32.dll" Alias "ShellExecuteA" (
    ByVal hWnd As Integer, ByVal lpOperation As String,
    ByVal lpFile As String, ByVal lpParameters As String,
    ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

    Declare Function Wow64DisableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Declare Function Wow64EnableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Private osk As String = "C:\Windows\System32\osk.exe"
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

        sQuerySummaryMain()
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

        'Tab_Main.Tabs("Tab_Details").Selected = True


        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "«· ›«’Ì·")
        Else
            sSecurity()
            'vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "»ÕÀ")
        End If

        sLoad_Colors()

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

        cControls.sSaveSettings(Me.Name, Grd_Summary)
    End Sub
    Private Sub FRM_Users_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Me.ParentForm.MdiChildren.Length = 1 Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, False, "", True)
        End If
    End Sub
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_DescA.TextChanged, Txt_DescL.TextChanged, Txt_Remarks.ValueChanged,
            TXT_ItemCategoryDesc.ValueChanged, Txt_PackUnitDesc.ValueChanged,
            Txt_CustomerDesc.ValueChanged, Txt_BarCode.ValueChanged, Txt_DemandPoint.ValueChanged,
            Chk_IsActive.CheckedChanged, Chk_GlassFromOutSide.CheckedChanged,
            Txt_Thickness.ValueChanged, Txt_SPrice.ValueChanged,
            Txt_CutPrice.ValueChanged, Txt_RinsingPrice.ValueChanged, Txt_OvenPrice.ValueChanged,
            Txt_DoubleGlass.ValueChanged, Txt_LiminitedPrice.ValueChanged, Txt_Colors.ValueChanged

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

        If sender.Name = "Chk_GlassFromOutSide" Then
            If Chk_GlassFromOutSide.Checked Then
                Chk_GlassFromOutSide.Tag = "Y"
            Else
                Chk_GlassFromOutSide.Tag = "N"
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
    Private Sub ToolBar_Main_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case e.Tool.Key
            Case "Btn_ExportToExcel"
                sExportToExcel()
            Case "Btn_ShowKeyBoard"
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

                'If fValidate_BarCode() Then
                '    sSave_BarCode()
                'Else
                '    Return False
                'End If

                'If fValidateProductFormula() Then
                '    sSaveProductFormula()
                'Else
                '    Return False
                'End If

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

            'If fValidate_BarCode() Then
            '    sSave_BarCode()
            'Else
            '    Return False
            'End If

            'If fValidateProductFormula() Then
            '    sSaveProductFormula()
            'Else
            '    Return False
            'End If

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
        If cControls.fReturnValue(" Select IsNull(UseDepartmentsInItemCode, 'N') From Controls ", Me.Name) = "Y" Then
            Txt_Str_Code.ReadOnly = True
        End If

        'sQueryBarCode()
        'sQueryProductFormula()
        sQueryPackUnit()
        sQueryDetails()
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
                    If vFetchRec > cControls.fCount_Rec(" From Items Where Company_Code = " & vCompanyCode) Then
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
            vFetchRec = cControls.fCount_Rec(" From Items Where Company_Code = " & vCompanyCode)
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
            "( Select Items.Code, " &
            "         Items.DescA as Itm_DescA,                          " &
            "         Items.Remarks,                                     " &
            "         Items.BarCode,                                     " &
            "         Cat_Ser,                                           " &
            "         Categories.DescA as Cat_DescA,                     " &
            "         PU_Code,                                           " &
            "         Pack_Unit.DescA as PU_DescA,                       " &
            "         DemandPoint,                                       " &
            "         IsActive,                                          " &
            "         IsGlassFromOutSide,                                " &
            "         SPrice,                                            " &
            "         Thickness,                                         " &
            "         CutPrice,                                          " &
            "         RinsingPrice,                                      " &
            "         OvenPrice,                                         " &
            "         DoubleGlassPrice,                                  " &
            "         LiminatedPrice,                                    " &
            "         Color_Code,                                        " &
            "         Items.DescL,                                       " &
            "         Items.Picture,                                     " &
            "         ROW_Number() Over (Order By Items.Code) as  RecPos " &
            " From Items LEFT JOIN Categories                            " &
            " On Items.Cat_Ser = Categories.Ser                          " &
            "                                                            " &
            " LEFT Join Pack_Unit                                       " &
            " On Items.PU_Code = Pack_Unit.Code                          " &
            "                                                            " &
            " Where Items.Company_Code = " & vCompanyCode & "   )        " &
            " Select * From MyItems                                      " &
            " Where 1 = 1                                                " &
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(21) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(21))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(21))

                'Code
                Txt_Code.Text = Trim(vSqlReader(0))

                'Desc
                Txt_DescA.Text = Trim(vSqlReader(1))

                'Remarks
                If vSqlReader.IsDBNull(2) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader(2))
                Else
                    Txt_Remarks.Text = ""
                End If

                'BarCode()
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_BarCode.Value = vSqlReader(3)
                Else
                    Txt_BarCode.Text = ""
                End If

                'Cat_Ser
                If vSqlReader.IsDBNull(4) = False Then
                    TXT_ItemCategoryCode.Text = Trim(vSqlReader(4))
                Else
                    TXT_ItemCategoryCode.Text = ""
                End If

                'Cat_Desc
                If vSqlReader.IsDBNull(5) = False Then
                    TXT_ItemCategoryDesc.Text = Trim(vSqlReader(5))
                Else
                    TXT_ItemCategoryDesc.Text = ""
                End If

                'PackUnit_Code
                If vSqlReader.IsDBNull(6) = False Then
                    Txt_PackUnitCode.Text = Trim(vSqlReader(6))
                Else
                    Txt_PackUnitCode.Text = ""
                End If

                'PackUnit_Desc
                If vSqlReader.IsDBNull(7) = False Then
                    Txt_PackUnitDesc.Text = Trim(vSqlReader(7))
                Else
                    Txt_PackUnitDesc.Text = ""
                End If

                'DemandPoint
                If vSqlReader.IsDBNull(8) = False Then
                    Txt_DemandPoint.Value = Trim(vSqlReader(8))
                Else
                    Txt_DemandPoint.Text = ""
                End If

                'IsActive
                If vSqlReader.IsDBNull(9) = False Then
                    If vSqlReader(9) = "N" Then
                        Chk_IsActive.Checked = False
                    Else
                        Chk_IsActive.Checked = True
                    End If
                End If

                'IsGlassFromOutSide
                If vSqlReader.IsDBNull(10) = False Then
                    If vSqlReader(10) = "N" Then
                        Chk_GlassFromOutSide.Checked = False
                    Else
                        Chk_GlassFromOutSide.Checked = True
                    End If
                End If

                'SPrice
                If vSqlReader.IsDBNull(11) = False Then
                    Txt_SPrice.Value = Trim(vSqlReader(11))
                Else
                    Txt_SPrice.Value = Nothing
                End If

                'Thickness
                If vSqlReader.IsDBNull(12) = False Then
                    Txt_Thickness.Value = Trim(vSqlReader(12))
                Else
                    Txt_Thickness.Value = Nothing
                End If

                'CutPrice
                If vSqlReader.IsDBNull(13) = False Then
                    Txt_CutPrice.Value = Trim(vSqlReader(13))
                Else
                    Txt_CutPrice.Value = Nothing
                End If

                'RinsingPrice
                If vSqlReader.IsDBNull(14) = False Then
                    Txt_RinsingPrice.Value = Trim(vSqlReader(14))
                Else
                    Txt_RinsingPrice.Value = Nothing
                End If

                'OvenPrice
                If vSqlReader.IsDBNull(15) = False Then
                    Txt_OvenPrice.Value = Trim(vSqlReader(15))
                Else
                    Txt_OvenPrice.Value = Nothing
                End If

                'DoubleGlass
                If vSqlReader.IsDBNull(16) = False Then
                    Txt_DoubleGlass.Value = Trim(vSqlReader(16))
                Else
                    Txt_DoubleGlass.Value = Nothing
                End If

                'LiminatedPrice
                If vSqlReader.IsDBNull(17) = False Then
                    Txt_LiminitedPrice.Value = Trim(vSqlReader(17))
                Else
                    Txt_LiminitedPrice.Value = Nothing
                End If

                'Color
                If vSqlReader.IsDBNull(18) = False Then
                    Txt_Colors.Value = vSqlReader(18)
                Else
                    Txt_Colors.SelectedIndex = -1
                End If

                'DescL
                If vSqlReader.IsDBNull(19) = False Then
                    Txt_DescL.Text = Trim(vSqlReader(19))
                Else
                    Txt_DescL.Text = ""
                End If

                'Picture
                If vSqlReader.IsDBNull(20) = False Then
                    Dim arrayImage() As Byte = CType(vSqlReader(20), Byte())
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
            sQueryDetails()

            vQuery = "N"
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
    End Sub

    Private Sub sFilterItems(ByVal pDesc As String)
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Top 5 DescA  From Items Where Company_Code = " & vCompanyCode &
            " Where DescA Like '%" & pDesc & "%' " &
            " Order By DescA "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_DescA.AutoCompleteCustomSource.Clear()
            Do While vSqlReader.Read
                Txt_DescA.AutoCompleteCustomSource.Add(Trim(vSqlReader(0)))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoad_Colors()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, DescA  From Colors "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Colors.Items.Clear()

            Do While vSqlReader.Read
                Txt_Colors.Items.Add(vSqlReader(0), vSqlReader(1))
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
                    " Delete From Item_Details Where Item_Code = '" & Txt_Code.Text & "'" &
                    " Delete From Product_Formula Where Item_Code = '" & Txt_Code.Text & "'" &
                    " Delete From Items_Images Where Item_Code = '" & Txt_Code.Text & "'" &
                    " Delete From Items Where Code = '" & Txt_Code.Text & "'" &
                    " Insert Into Items_Log (           Item_Code,                        DescA,            Type,        Emp_Code,      TDate,             ComputerName,                                                        IPAddress  ) " &
                    "               Values  ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_DescA.Text) & "',   'D',  '" & vUsrCode & "',  GetDate(),  '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "') "
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
        Txt_DescA.Clear()
        Txt_DescL.Clear()
        Txt_Remarks.Clear()
        Txt_Thickness.Value = Nothing
        Txt_SPrice.Value = Nothing
        Txt_CutPrice.Value = Nothing
        Txt_RinsingPrice.Value = Nothing
        Txt_OvenPrice.Value = Nothing
        Txt_DoubleGlass.Value = Nothing
        Txt_LiminitedPrice.Value = Nothing

        TXT_ItemCategoryCode.Text = ""
        TXT_ItemCategoryDesc.Text = ""
        Txt_PackUnitCode.Text = ""
        Txt_PackUnitDesc.Text = ""
        Txt_CustomerCode.Text = ""
        Txt_CustomerDesc.Text = ""
        Txt_AccountSer.Text = ""
        Txt_AccountDesc.Text = ""
        Txt_BarCode.Text = ""
        Txt_Store.SelectedIndex = -1
        Txt_Str_Code.Text = ""
        Txt_StrDesc.Text = ""

        Txt_Colors.SelectedIndex = -1

        'If cControls.fCount_Rec(" From Cost_Center ") = 1 Then
        '    Txt_Str_Code.Text = cControls.fReturnValue(" Select Code From Cost_Center ", Me.Name)
        '    Txt_StrDesc.Text = cControls.fReturnValue(" Select DescA From Cost_Center ", Me.Name)
        'End If

        PictureBox1.Image = PictureBox1.InitialImage

        Txt_DemandPoint.Value = Nothing
        Txt_Commission.Value = Nothing

        Chk_IsActive.Checked = True
        Chk_GlassFromOutSide.Checked = False
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
        Txt_Str_Code.ReadOnly = False
        Txt_Code.Select()

        'Here I load the Auto Add Pack Units
        'sLoad_Auto_PackUnits()

        Dim x As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCode, 'Y') From Controls ", Me.Name)
        If x = "Y" Then
            sNewCode()
        End If
    End Sub
    Private Sub sNewCode()

        Dim vSqlString As String

        'vNumberOfDigits_InItemCode = cControls.fReturnValue(" Select IsNull(NumberOfDigitsInItemCode, 6) From Controls ", Me.Name)

        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Items "
        Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name)
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

        If pType = "Accounts" Then
            vLovReturn1 = ""
            VLovReturn2 = ""
            vLovReturn3 = ""
            vSelectedSortedList_1.Clear()
            Dim Frm_Lov As New FRM_LovGeneral_A(" Select Ser, DescA From Financial_Definitions_Tree Where 1 = 1 " &
                                                     " And Company_Code = '" & vCompanyCode & "'", "«·œ·Ì· «·„Õ«”»Ì")
            Frm_Lov.ShowDialog()

            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                Txt_AccountSer.Text = vLovReturn1
                Txt_AccountDesc.Text = VLovReturn2
            End If
        Else
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
                ElseIf pType = "Raw_Materials" Then
                    Grd_ProductFormula.ActiveRow.Cells("Code").Value = vLovReturn1
                    Grd_ProductFormula.ActiveRow.Cells("DescA").Value = VLovReturn2

                    If Grd_ProductFormula.ActiveRow.Cells("DML").Value = "NI" Then
                        Grd_ProductFormula.ActiveRow.Cells("DML").Value = "I"
                    ElseIf Grd_ProductFormula.ActiveRow.Cells("DML").Value = "N" Then
                        Grd_ProductFormula.ActiveRow.Cells("DML").Value = "U"
                    End If

                    Grd_ProductFormula.ActiveRow.Cells("PackUnit").Value = cControls.fReturnValue(" Select PU_Code From Raw_Materials Where Code = '" & Trim(vLovReturn1) & "' ", Me.Name)
                    Grd_ProductFormula.ActiveRow.Cells("Cat_Desc").Value = cControls.fReturnValue(" Select Categories_RawMaterials.DescA " &
                                                                                                  " From Raw_Materials INNER JOIN Categories_RawMaterials   " &
                                                                                                  " On Categories_RawMaterials.Ser = Raw_Materials.Cat_Ser  " &
                                                                                                  "                                                         " &
                                                                                                  " Where Raw_Materials.Code = '" & Trim(vLovReturn1) & "' ", Me.Name)
                ElseIf pType = "Categories" Then
                    'Here I Check first if the selected Category is Parent, then Exit
                    Dim vCode As String = cControls.fReturnValue(" Select Code From Categories Where Ser = " & vLovReturn1, Me.Name)

                    If cControls.fIsExist(" From Categories Where Parent_Code = '" & vCode & "' ", Me.Name) = True Then
                        vcFrmLevel.vParentFrm.sForwardMessage("94", Me)
                        Exit Sub
                    End If

                    If cControls.fReturnValue(" Select IsNull(UseCategoriesInItemCode, 'N') From Controls ", Me.Name) = "Y" Then
                        If vMasterBlock = "N" Or vMasterBlock = "U" Then
                            vcFrmLevel.vParentFrm.sForwardMessage("101", Me)
                            Exit Sub
                        End If
                    End If

                    TXT_ItemCategoryCode.Text = vLovReturn1
                    TXT_ItemCategoryDesc.Text = VLovReturn2

                    If cControls.fReturnValue(" Select IsNull(UseCategoriesInItemCode, 'N') From Controls ", Me.Name) = "Y" Then
                        Dim vSqlString As String
                        vSqlString = " Select Count (*) + 1 From Items Where Cat_Ser = " & vLovReturn1

                        Dim vNumberOfDigits_InItemCode As Int16 = cControls.fReturnValue(" Select IsNull(NumberOfDigitsInItemCode, 6) From Controls ", Me.Name)
                        Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(vNumberOfDigits_InItemCode, "0") & "-" & vLovReturn3
                    End If
                ElseIf pType = "Customers" Then
                    Txt_CustomerCode.Text = vLovReturn1
                    Txt_CustomerDesc.Text = VLovReturn2
                ElseIf pType = "Stores" Then
                    Txt_Str_Code.Text = vLovReturn1
                    Txt_StrDesc.Text = VLovReturn2
                ElseIf pType = "Details_PU" Then
                    Grd_PackUnit.ActiveRow.Cells("PU_Code").Value = vLovReturn1
                    Grd_PackUnit.ActiveRow.Cells("DescA").Value = VLovReturn2

                    If Grd_PackUnit.ActiveRow.Cells("DML").Value = "NI" Then
                        Grd_PackUnit.ActiveRow.Cells("DML").Value = "I"
                    ElseIf Grd_PackUnit.ActiveRow.Cells("DML").Value = "N" Then
                        Grd_PackUnit.ActiveRow.Cells("DML").Value = "U"
                    End If

                End If
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
                sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))

                'ToolBar_Main.Tools("Btn_CopyItems").SharedProps.Visible = False
                ToolBar_Main.Tools("Btn_ShowKeyBoard").SharedProps.Visible = False
                ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = True
                ToolBar_Main.Tools("Btn_FinalItems_Cost").SharedProps.Visible = False
                ToolBar_Main.Tools("Btn_FinalItems_Cost_WithDetails").SharedProps.Visible = False
            Else
                vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "»ÕÀ")
                If Grd_Summary.Selected.Rows.Count > 0 Then
                    If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                        sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
                    Else
                        sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
                    End If
                Else
                    sNewRecord()
                End If

                sSecurity()

                'ToolBar_Main.Tools("Btn_CopyItems").SharedProps.Visible = True
                ToolBar_Main.Tools("Btn_ShowKeyBoard").SharedProps.Visible = True
                ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = False
                ToolBar_Main.Tools("Btn_FinalItems_Cost").SharedProps.Visible = False
                ToolBar_Main.Tools("Btn_FinalItems_Cost_WithDetails").SharedProps.Visible = False
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
                If Trim(vSqlReader(0)) = "Ctrl_SalesPrices" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Btn_SalesPrices.Enabled = True
                    Else
                        Btn_SalesPrices.Enabled = False
                    End If
                End If

                If Trim(vSqlReader(0)) = "Ctrl_PPrice" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Txt_DoubleGlass.ReadOnly = False
                    Else
                        Txt_DoubleGlass.ReadOnly = True
                    End If
                End If

                If Trim(vSqlReader(0)) = "Ctrl_PPrice" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Txt_RinsingPrice.ReadOnly = False
                    Else
                        Txt_RinsingPrice.ReadOnly = True
                    End If
                End If

                If Trim(vSqlReader(0)) = "Ctrl_Desc" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Txt_DescA.ReadOnly = False
                    Else
                        Txt_DescA.ReadOnly = True
                    End If
                End If

                If Trim(vSqlReader(0)) = "Ctrl_SalesDed" Then
                    If Trim(vSqlReader(1)) = "Y" Then
                        Txt_Thickness.ReadOnly = False
                    Else
                        Txt_Thickness.ReadOnly = True
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

        If cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCode, 'Y') From Controls ", Me.Name) <> "Y" Then
            If vMasterBlock = "I" Then
                If cControls.fCount_Rec(" From Items Where Code = '" & Txt_Code.Text & "'") > 0 Then
                    vcFrmLevel.vParentFrm.sForwardMessage("12", Me)
                    Txt_Code.Select()
                    Txt_Code.SelectAll()
                    Return False
                End If
            End If
        End If

        If Txt_DescA.Text.Length = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
            Txt_DescA.Select()
            Return False
        End If

        'If cControls.fCount_Rec(" From Items Where DescA = '" & Trim(Txt_DescA.Text) & "' And Code <> '" & Trim(Txt_Code.Text) & "'") > 0 Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("82", Me)
        '    Txt_DescA.Select()
        '    Return False
        'End If


        'If TXT_ItemCategoryDesc.Text = "" Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("56", Me)
        '    TXT_ItemCategoryCode.Select()
        '    Return False
        'End If

        'If Txt_PackUnitDesc.Text = "" Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("55", Me)
        '    Txt_PackUnitCode.Select()
        '    Return False
        'End If


        If Txt_Thickness.Value Is Nothing Then
            vcFrmLevel.vParentFrm.sForwardMessage("156", Me)
            Txt_Thickness.Select()
            Return False
        End If

        If Txt_Colors.SelectedIndex = -1 Then
            vcFrmLevel.vParentFrm.sForwardMessage("164", Me)
            Txt_Colors.Select()
            Return False
        End If

        'If Txt_CutPrice.Value Is Nothing Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("162", Me)
        '    Txt_CutPrice.Select()
        '    Return False
        'End If

        If Txt_SPrice.Value Is Nothing Then
            vcFrmLevel.vParentFrm.sForwardMessage("157", Me)
            Txt_SPrice.Select()
            Return False
        End If

        If Txt_RinsingPrice.Value Is Nothing Then
            vcFrmLevel.vParentFrm.sForwardMessage("158", Me)
            Txt_RinsingPrice.Select()
            Return False
        End If

        If Txt_OvenPrice.Value Is Nothing Then
            vcFrmLevel.vParentFrm.sForwardMessage("159", Me)
            Txt_OvenPrice.Select()
            Return False
        End If

        'If Txt_DoubleGlass.Value Is Nothing Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("160", Me)
        '    Txt_DoubleGlass.Select()
        '    Return False
        'End If

        'If Txt_LiminitedPrice.Value Is Nothing Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("161", Me)
        '    Txt_LiminitedPrice.Select()
        '    Return False
        'End If


        'If Txt_ProviderDesc.Text = "" Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("54", Me)
        '    Txt_ProviderDesc.Select()
        '    Return False
        'End If

        'If vMasterBlock = "I" Then
        '    If Trim(Txt_BarCode.Text).Length > 0 Then
        '        If cControls.fIsExist(" From Items Where BarCode = '" & Trim(Txt_BarCode.Text) & "'", Me.Name) = True Then
        '            vcFrmLevel.vParentFrm.sForwardMessage("16", Me)
        '            Txt_BarCode.SelectAll()
        '            Return False
        '        End If
        '    End If
        'End If

        Return True
    End Function
    Private Sub sSaveMain()
        'If fValidateMain() = False Then
        '    Return
        'End If

        Dim vSPrice, vThickness, vCutPrice, vRinsingPrice, vOvenPrice, vDoubleGlassPrice, vLimintedPrice, vDemandPoint, vColor As String

        Dim vSqlCommand As String = ""

        'SPrice
        If Not IsDBNull(Txt_SPrice.Value) Then
            If Not Txt_SPrice.Value = Nothing Then
                vSPrice = Trim(Txt_SPrice.Value)
            Else
                vSPrice = "NULL"
            End If
        Else
            vSPrice = "NULL"
        End If

        'Thickness
        If Not IsDBNull(Txt_Thickness.Value) Then
            If Not Txt_Thickness.Value = Nothing Then
                vThickness = Trim(Txt_Thickness.Value)
            Else
                vThickness = "NULL"
            End If
        Else
            vThickness = "NULL"
        End If

        'RinsingPrice
        If Not IsDBNull(Txt_CutPrice.Value) Then
            If Not Txt_CutPrice.Value = Nothing Then
                vCutPrice = Trim(Txt_CutPrice.Value)
            Else
                vCutPrice = "NULL"
            End If
        Else
            vCutPrice = "NULL"
        End If

        'RinsingPrice
        If Not IsDBNull(Txt_RinsingPrice.Value) Then
            If Not Txt_RinsingPrice.Value = Nothing Then
                vRinsingPrice = Trim(Txt_RinsingPrice.Value)
            Else
                vRinsingPrice = "NULL"
            End If
        Else
            vRinsingPrice = "NULL"
        End If

        'OvenPrice
        If Not IsDBNull(Txt_OvenPrice.Value) Then
            If Not Txt_OvenPrice.Value = Nothing Then
                vOvenPrice = Trim(Txt_OvenPrice.Value)
            Else
                vOvenPrice = "NULL"
            End If
        Else
            vOvenPrice = "NULL"
        End If

        'DoubleGlass
        If Not IsDBNull(Txt_DoubleGlass.Value) Then
            If Not Txt_DoubleGlass.Value = Nothing Then
                vDoubleGlassPrice = Trim(Txt_DoubleGlass.Value)
            Else
                vDoubleGlassPrice = "NULL"
            End If
        Else
            vDoubleGlassPrice = "NULL"
        End If

        'DoubleGlass
        If Not IsDBNull(Txt_LiminitedPrice.Value) Then
            If Not Txt_LiminitedPrice.Value = Nothing Then
                vLimintedPrice = Trim(Txt_LiminitedPrice.Value)
            Else
                vLimintedPrice = "NULL"
            End If
        Else
            vLimintedPrice = "NULL"
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

        'Provider_Code
        If Txt_CustomerDesc.Text = "" Then
            vCustomer_Code = "NULL"
        Else
            vCustomer_Code = " '" & Trim(Txt_CustomerCode.Text) & "' "
        End If

        If Txt_Colors.SelectedIndex = -1 Then
            vColor = "NULL"
        Else
            vColor = " '" & Txt_Colors.Value & "' "
        End If

        If vMasterBlock = "I" Then

            Dim x As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCode, 'Y') From Controls ", Me.Name)
            If x = "Y" Then
                sNewCode()
            End If

            vSqlCommand = " Insert Into Items  (              Code,                          DescA,                          DescL,                 Thickness,          SPrice,          CutPrice,          RinsingPrice,           OvenPrice,         DoubleGlassPrice,          LiminatedPrice,                  Remarks,                     BarCode,                DemandPoint,              IsActive,                  IsGlassFromOutSide,              Company_Code,       Color_Code     )" &
                                      " Values ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_DescA.Text) & "', '" & Trim(Txt_DescL.Text) & "', " & vThickness & ", " & vSPrice & ", " & vCutPrice & ", " & vRinsingPrice & ", " & vOvenPrice & ", " & vDoubleGlassPrice & ", " & vLimintedPrice & ", '" & Trim(Txt_Remarks.Text) & "', '" & Txt_BarCode.Text & "', " & vDemandPoint & ", '" & Chk_IsActive.Tag & "', '" & Chk_GlassFromOutSide.Tag & "', " & vCompanyCode & ", " & vColor & " )"

            sFillSqlStatmentArray(vSqlCommand)

            'Here I will Create the Log File
            vSqlCommand = " Insert Into Items_Log  (          Item_Code,                          DescA,                             Cat_Ser,                                   PU_Code,                  Provider_Code,                  Remarks,                     BarCode,                DemandPoint,              IsActive,                SPrice,          Company_Code,    CostCenter_Code,   Type,        Emp_Code,         TDate,             ComputerName,                                                        IPAddress    )" &
                                      "     Values ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_DescA.Text) & "', '" & Trim(TXT_ItemCategoryCode.Text) & "', '" & Trim(Txt_PackUnitCode.Text) & "', " & vCustomer_Code & ", '" & Trim(Txt_Remarks.Text) & "', '" & Txt_BarCode.Text & "', " & vDemandPoint & ", '" & Chk_IsActive.Tag & "',  " & vSPrice & ", " & vCompanyCode & ", " & vColor & ",      'C',   '" & vUsrCode & "',    GetDate(),  '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "') "

            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Items " &
                          " Set   DescA         = '" & Trim(Txt_DescA.Text) & "', " &
                          "       DescL         = '" & Trim(Txt_DescL.Text) & "', " &
                          "       Remarks       = '" & Trim(Txt_Remarks.Text) & "', " &
                          "       BarCode       = '" & Txt_BarCode.Text & "', " &
                          "       DemandPoint   =  " & vDemandPoint & ", " &
                          "       IsActive      = '" & Chk_IsActive.Tag & "', " &
                          "       IsGlassFromOutSide = '" & Chk_GlassFromOutSide.Tag & "', " &
                          "       SPrice        =  " & vSPrice & ", " &
                          "       Thickness     =  " & vThickness & ", " &
                          "       CutPrice      =  " & vCutPrice & ", " &
                          "       RinsingPrice  =  " & vRinsingPrice & ", " &
                          "       OvenPrice     =  " & vOvenPrice & ", " &
                          "       DoubleGlassPrice = " & vDoubleGlassPrice & ", " &
                          "       LiminatedPrice   = " & vLimintedPrice & ", " &
                          "       Color_Code    =  " & vColor &
                          " Where Code          = '" & Txt_Code.Text & "' "
            sFillSqlStatmentArray(vSqlCommand)

            vSqlCommand = " Insert Into Items_Log  (              Item_Code,                     DescA,                             Cat_Ser,                                   PU_Code,                  Provider_Code,                   Remarks,                     BarCode,                DemandPoint,              IsActive,              SPrice,            Company_Code,   CostCenter_Code,  Type,      Emp_Code,           TDate,             ComputerName,                                                        IPAddress )" &
                                      "     Values ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_DescA.Text) & "', '" & Trim(TXT_ItemCategoryCode.Text) & "', '" & Trim(Txt_PackUnitCode.Text) & "', " & vCustomer_Code & ", '" & Trim(Txt_Remarks.Text) & "', '" & Txt_BarCode.Text & "', " & vDemandPoint & ", '" & Chk_IsActive.Tag & "',  " & vSPrice & ", " & vCompanyCode & ", " & vColor & ",   'U',  '" & vUsrCode & "',    GetDate(),  '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "')"

            sFillSqlStatmentArray(vSqlCommand)
        End If
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub TXT_All_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) _
    Handles TXT_ItemCategoryCode.EditorButtonClick, Txt_PackUnitCode.EditorButtonClick,
    Txt_CustomerCode.EditorButtonClick, Txt_AccountSer.EditorButtonClick,
    Txt_Details_PU.EditorButtonClick, Txt_Str_Code.EditorButtonClick,
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
    Private Sub Txt_ProviderCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_CustomerCode.Validating
        If Txt_CustomerCode.Text <> "" Then
            If cControls.fCount_Rec(" From Customers Where Code = '" & Trim(Txt_CustomerCode.Text) & "'") = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                Txt_CustomerCode.Select()
                e.Cancel = True
            Else
                Txt_CustomerDesc.Text = cControls.fReturnValue(" Select DescA From Customers Where Code = '" & Trim(Txt_CustomerCode.Text) & "'", Me.Name)
                e.Cancel = False
            End If
        Else
            Txt_CustomerDesc.Text = ""
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
    Private Sub Txt_BarCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_BarCode.Validating
        If vMasterBlock = "I" Then
            If Trim(Txt_BarCode.Text).Length > 0 Then
                If cControls.fCount_Rec(" From Items Where BarCode = '" &
                    Trim(Txt_BarCode.Text) & "'") > 0 Then
                    vcFrmLevel.vParentFrm.sForwardMessage("16", Me)
                    Txt_BarCode.SelectAll()
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
        End If
    End Sub
    Private Sub Txt_All_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Txt_Code.Enter, Txt_Remarks.Enter, Txt_DoubleGlass.Enter,
        TXT_ItemCategoryCode.Enter, TXT_ItemCategoryDesc.Enter, Txt_PackUnitCode.Enter,
        Txt_PackUnitDesc.Enter, Txt_CustomerCode.Enter, Txt_CustomerDesc.Enter,
        Txt_RinsingPrice.Enter, Txt_Thickness.Enter, Txt_Commission.Enter, Txt_DescA.Enter

        vFocus = "Master"
    End Sub
    Private Sub Txt_Code_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
    Handles Txt_Code.KeyUp, Txt_Remarks.KeyUp, Txt_DoubleGlass.KeyUp,
            Txt_BarCode.KeyUp, TXT_ItemCategoryCode.KeyUp, Txt_PackUnitCode.KeyUp,
            Txt_CustomerCode.KeyUp, Txt_DemandPoint.KeyUp, Txt_AccountSer.KeyUp,
            Txt_AccountDesc.KeyUp, Txt_SPrice.KeyUp, Txt_RinsingPrice.KeyUp, Txt_Thickness.KeyUp,
            Txt_Commission.KeyUp, Txt_DescA.KeyUp, Txt_Details_PU.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyData = Keys.F12 Then
            Dim ee As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs
            TXT_All_EditorButtonClick(sender, ee)
        End If
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
    Private Sub Btn_SalesPrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SalesPrices.Click
        'If Trim(Txt_DescA.Text) <> "" Then
        '    Dim Frm_SalesPrice As New Frm_SalesPrices(Trim(Txt_Code.Text), "", Trim(Txt_DescA.Text))
        '    Frm_SalesPrice.ShowDialog()
        'End If
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

                If Not Grd_PackUnit.ActiveRow Is Nothing Then
                    'Dim Frm_SalesPrice As New Frm_SalesPrices(Trim(Txt_Code.Text), "", Trim(Txt_DescA.Text), Grd_PackUnit.ActiveRow.Cells("Ser").Value)
                    'Frm_SalesPrice.ShowDialog()
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
                                     "         Values           ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", (@FileData), '" & vRow.Cells("DescL").Text & "', '" & vRow.Cells("FileName").Text & "', '" & vFileType & "')"

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
            Dim vCodeFilter, vOrder As String
            Dim vFullText() As String

            vFullText = Trim(Txt_FndByDesc.Text).Split


            If pCode = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Items.Code Like '%" & pCode & "%'"
            End If

            'If pDesc = "" Then
            '    vDescFilter = ""
            'Else
            '    vDescFilter = " And Items.DescA Like '%" & pDesc & "%'"
            'End If

            'Here I check if the code is auto generated then I Order the item wz code else is order by Desc
            Dim x As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCode, 'Y') From Controls ", Me.Name)
            If x = "Y" Then
                vOrder = " Order By Items.Code "
            Else
                vOrder = " Order By Items.DescA "
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText =
            "  Select Items.Code, " &
            "         Items.DescA as Item_DescA,                        " &
            "         Items.SPrice,                                     " &
            "         Categories.DescA as Cat_DescA,                    " &
            "         Pack_Unit.DescA as PU_DescA,                      " &
            "         IsNull(IsActive, 'Y'),                            " &
            "         Thickness,                                        " &
            "         Colors.DescA as Color_Desc,                       " &
            "         RinsingPrice,                                    " &
            "         OvenPrice,                                        " &
            "         Items.Remarks                                     " &
            "                                                           " &
            " From Items LEFT Join Categories                           " &
            " On Items.Cat_Ser = Categories.Ser                         " &
            "                                                           " &
            " LEFT Join Pack_Unit                                      " &
            " On Items.PU_Code = Pack_Unit.Code                         " &
            "                                                           " &
            " LEFT JOIN Colors                                          " &
            " ON Colors.Code = Items.Color_Code                         " &
            "                                                           " &
            " Where 1 = 1                                               " &
            " And Items.Company_Code = " & vCompanyCode &
            sFndByCategories() &
            vCodeFilter &
            fReturnSplitValues(vFullText)


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
                    DTS_Summary.Rows(vRowCounter)("SPrice") = vSqlReader(2)
                Else
                    DTS_Summary.Rows(vRowCounter)("SPrice") = Nothing
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("Category") = Trim(vSqlReader(3))
                Else
                    DTS_Summary.Rows(vRowCounter)("Category") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Summary.Rows(vRowCounter)("PackUnit") = Trim(vSqlReader(4))
                Else
                    DTS_Summary.Rows(vRowCounter)("PackUnit") = ""
                End If

                If vSqlReader(5) = "Y" Then
                    DTS_Summary.Rows(vRowCounter)("Status") = "‰‘ÿ"
                Else
                    DTS_Summary.Rows(vRowCounter)("Status") = "€Ì— ‰‘ÿ"
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Summary.Rows(vRowCounter)("Thickness") = vSqlReader(6)
                Else
                    DTS_Summary.Rows(vRowCounter)("Thickness") = Nothing
                End If

                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Summary.Rows(vRowCounter)("Color_Desc") = Trim(vSqlReader(7))
                Else
                    DTS_Summary.Rows(vRowCounter)("Color_Desc") = ""
                End If

                If vSqlReader.IsDBNull(8) = False Then
                    DTS_Summary.Rows(vRowCounter)("RinsingPrice") = vSqlReader(8)
                Else
                    DTS_Summary.Rows(vRowCounter)("RinsingPrice") = Nothing
                End If

                If vSqlReader.IsDBNull(9) = False Then
                    DTS_Summary.Rows(vRowCounter)("OvenPrice") = vSqlReader(9)
                Else
                    DTS_Summary.Rows(vRowCounter)("OvenPrice") = Nothing
                End If

                If vSqlReader.IsDBNull(10) = False Then
                    DTS_Summary.Rows(vRowCounter)("Remarks") = Trim(vSqlReader(10))
                Else
                    DTS_Summary.Rows(vRowCounter)("Remarks") = ""
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
            '    If cBase.fCount_Rec(" From Items Where Code = '" & vRow("Code") & "' And Ser <> '00'") > 0 Then
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
                                      " From Items " &
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
            If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                'sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
            Else
                'sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
            End If
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
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Code").Hidden = True
            'Grd_Summary.DisplayLayout.Bands(0).Columns("SPrice").Hidden = True
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Thickness").Hidden = True
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Color_Desc").Hidden = True
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Status").Hidden = True
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Remarks").Hidden = True

            vFileName = SaveFileDialog1.FileName
            UltraGridExcelExporter1.Export(Grd_Summary, vFileName & ".xls")

            'Grd_Summary.DisplayLayout.Bands(0).Columns("Code").Hidden = False
            'Grd_Summary.DisplayLayout.Bands(0).Columns("SPrice").Hidden = False
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Thickness").Hidden = False
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Color_Desc").Hidden = False
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Status").Hidden = False
            'Grd_Summary.DisplayLayout.Bands(0).Columns("Remarks").Hidden = False
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

            'Return "And Contains (Items.DescA, 'FormsOf (THESAURUS, John)' )"

            'If pFullText.Length = 1 Then
            '    Return " And Items.DescA Like '%" & pFullText(0) & "%' "
            'End If

            For vCount = 0 To pFullText.Length - 1
                If vItems.Length > 0 Then
                    vItems += " And """ & pFullText(vCount) & "*" & """"
                Else
                    vItems += """" & pFullText(vCount) & "*" & """"
                End If
            Next

            vReturnedValue = " And Contains (Items.DescA, '" & vItems & "' ) "

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
            sFndByCategories = " And Items.Cat_Ser In  (" & vItemValues & ")"
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

                    vSqlString = " Update Items Set Picture = (@image) Where Code = '" & Trim(Txt_Code.Text) & "'"

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

    Private Sub Txt_Back_Click(sender As Object, e As EventArgs) Handles Txt_Back.Click
        Tab_Main.Tabs("Tab_Summary").Selected = True
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
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

    Private Sub Btn_ExportToExcel_Click(sender As Object, e As EventArgs) Handles Btn_ExportToExcel.Click
        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            Exit Sub
        End If

        sExportToExcel()
    End Sub
End Class