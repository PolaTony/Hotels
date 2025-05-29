Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Public Class Frm_ItemDefinition_L

#Region " Declaration                                                                    "
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As Object = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".cFrmLevelVariables_" & vLang)
    Dim vSqlStatment(0) As String
    Dim vFocus As String = "Master"
    Dim vQuery As String = "N"
    Dim vSortedList As New SortedList
    Dim vClear As Boolean = True
#End Region
#Region " Form Level                                                                     "
#Region " My Form                                                                        "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'sQueryUser(vUsrCode)

        vMasterBlock = "NI"
        sLoadPackUnits()
        sLoadStores()
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

        Tab_Main.Tabs("Tab_Details").Selected = True


        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "التفاصيل")
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
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
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Remarks.ValueChanged, Txt_Price.ValueChanged, _
            TXT_ItemCategoryDesc.ValueChanged, Txt_PackUnitDesc.ValueChanged, _
            Txt_ProviderDesc.ValueChanged, Txt_BarCode.ValueChanged, Txt_DemandPoint.ValueChanged, _
            Chk_IsActive.CheckedChanged, Chk_IsRaw.CheckedChanged, Txt_SPrice.ValueChanged, Txt_CPrice.ValueChanged, _
            Txt_Deduction.ValueChanged, Txt_Commission.ValueChanged, _
            Txt_Desc.TextChanged, Txt_AccountDesc.TextChanged, Txt_Store.ValueChanged

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

        If sender.Name = "Chk_IsRaw" Then
            If Chk_IsRaw.Checked Then
                Chk_IsRaw.Tag = "Y"
                'Chk_IsRaw.Text = "ماده خام"
            Else
                Chk_IsRaw.Tag = "N"
                'Chk_IsRaw.Text = "غير نشط"
            End If
        End If

        'If vQuery = "N" Then
        '    If sender.Name = "Txt_Desc" Then
        '        'If Txt_Desc.Text.Length > 6 Then
        '        sFilterItems(Txt_Desc.Text)
        '        'End If
        '    End If
        'End If

    End Sub
    Private Sub sCheckIfAutoCreateItemCode()
        Try
            'Txt_PackUnit.Items.Clear()
            Dim vSQlcommand As New SqlCommand
            Dim vGenerateNewCode As String = ""

            vSQlcommand.CommandText = _
            " Select IsNull(AutomaticallyGenerateCode, 'Y'), " & _
            "        IsNull(IsSalesPricesEnabled, 'N') " & _
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
        Return False
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
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
        sQueryBarCode()
        sQueryProductFormula()
        sQueryPackUnit()
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
            vSQlcommand.CommandText = _
            " With MyItems as " & _
            "( Select Items.Code, " & _
            "         Items.DescA as Itm_DescA,                          " & _
            "         Items.Remarks,                                     " & _
            "         Items.Price,                                       " & _
            "         BarCode,                                           " & _
            "         Cat_Ser,                                           " & _
            "         Categories.DescA as Cat_DescA,                     " & _
            "         PU_Code,                                           " & _
            "         Pack_Unit.DescA as PU_DescA,                       " & _
            "         Provider_Code,                                     " & _
            "         Providers.DescA as Provider_DescA,                 " & _
            "         Financial_Definitions_Tree.Code as Account_Code,   " & _
            "         Financial_Definitions_Tree.DescA,                  " & _
            "         Items.Account_Ser,                                 " & _
            "         DemandPoint,                                       " & _
            "         IsActive,                                          " & _
            "         IsRaw,                                             " & _
            "         SPrice,                                            " & _
            "         CPrice,                                            " & _
            "         Items.Deduction,                                   " & _
            "         Items.Commission,                                  " & _
            "         Items.Store_Code,                                  " & _
            "         Items.Picture,                                     " & _
            "         ROW_Number() Over (Order By Items.Code) as  RecPos " & _
            " From Items Inner Join Categories                           " & _
            " On Items.Cat_Ser = Categories.Ser                          " & _
            " Inner Join Pack_Unit                                       " & _
            " On Items.PU_Code = Pack_Unit.Code                          " & _
            " LEFT Join Providers                                       " & _
            " On Items.Provider_Code = Providers.Code                    " & _
            " Left Join Financial_Definitions_Tree                       " & _
            " On Items.Account_Ser = Financial_Definitions_Tree.Ser      " & _
            " Where Items.Company_Code = " & vCompanyCode & "   )          " & _
            " Select * From MyItems                                      " & _
            " Where 1 = 1                                                " & _
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(23) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(23))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(23))

                'Code
                Txt_Code.Text = Trim(vSqlReader(0))

                'Desc
                Txt_Desc.Text = Trim(vSqlReader(1))

                'Remarks
                If vSqlReader.IsDBNull(2) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader(2))
                Else
                    Txt_Remarks.Text = ""
                End If

                'Price
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_Price.Value = Trim(vSqlReader(3))
                Else
                    Txt_Price.Value = Nothing
                End If

                'BarCode()
                If vSqlReader.IsDBNull(4) = False Then
                    Txt_BarCode.Value = vSqlReader(4)
                Else
                    Txt_BarCode.Text = ""
                End If

                'Cat_Code
                'If vSqlReader.IsDBNull(5) = False Then
                '    TXT_ItemCategoryCode.Text = Trim(vSqlReader(5))
                'Else
                '    TXT_ItemCategoryCode.Text = ""
                'End If

                'Cat_Ser
                If vSqlReader.IsDBNull(5) = False Then
                    TXT_ItemCategoryCode.Text = Trim(vSqlReader(5))
                Else
                    TXT_ItemCategoryCode.Text = ""
                End If

                'Cat_Desc
                If vSqlReader.IsDBNull(6) = False Then
                    TXT_ItemCategoryDesc.Text = Trim(vSqlReader(6))
                Else
                    TXT_ItemCategoryDesc.Text = ""
                End If

                'PackUnit_Code
                If vSqlReader.IsDBNull(7) = False Then
                    Txt_PackUnitCode.Text = Trim(vSqlReader(7))
                Else
                    Txt_PackUnitCode.Text = ""
                End If

                'PackUnit_Desc
                If vSqlReader.IsDBNull(8) = False Then
                    Txt_PackUnitDesc.Text = Trim(vSqlReader(8))
                Else
                    Txt_PackUnitDesc.Text = ""
                End If

                'Provider_Code
                If vSqlReader.IsDBNull(9) = False Then
                    Txt_ProviderCode.Text = Trim(vSqlReader(9))
                Else
                    Txt_ProviderCode.Text = ""
                End If

                'Provider_Desc
                If vSqlReader.IsDBNull(10) = False Then
                    Txt_ProviderDesc.Text = Trim(vSqlReader(10))
                Else
                    Txt_ProviderDesc.Text = ""
                End If

                'Account_Code
                If vSqlReader.IsDBNull(11) = False Then
                    Txt_AccountSer.Text = Trim(vSqlReader(11))
                Else
                    Txt_AccountSer.Text = ""
                End If

                'Account_Desc
                If vSqlReader.IsDBNull(12) = False Then
                    Txt_AccountDesc.Text = Trim(vSqlReader(12))
                Else
                    Txt_AccountDesc.Text = ""
                End If

                'Account_Ser
                If vSqlReader.IsDBNull(13) = False Then
                    Txt_AccountSer.Tag = Trim(vSqlReader(13))
                Else
                    Txt_AccountSer.Tag = Nothing
                End If

                'DemandPoint
                If vSqlReader.IsDBNull(14) = False Then
                    Txt_DemandPoint.Value = Trim(vSqlReader(14))
                Else
                    Txt_DemandPoint.Text = ""
                End If

                'IsActive
                If vSqlReader.IsDBNull(15) = False Then
                    If vSqlReader(15) = "N" Then
                        Chk_IsActive.Checked = False
                    Else
                        Chk_IsActive.Checked = True
                    End If
                End If

                'IsRaw
                If vSqlReader.IsDBNull(16) = False Then
                    If vSqlReader(16) = "N" Then
                        Chk_IsRaw.Checked = False
                    Else
                        Chk_IsRaw.Checked = True
                    End If
                Else
                    Chk_IsRaw.Checked = False
                End If

                'SPrice
                If vSqlReader.IsDBNull(17) = False Then
                    Txt_SPrice.Value = Trim(vSqlReader(17))
                Else
                    Txt_SPrice.Value = Nothing
                End If

                'CPrice
                If vSqlReader.IsDBNull(18) = False Then
                    Txt_CPrice.Value = Trim(vSqlReader(18))
                Else
                    Txt_CPrice.Value = Nothing
                End If

                'Deduction
                If vSqlReader.IsDBNull(19) = False Then
                    Txt_Deduction.Value = Trim(vSqlReader(19))
                Else
                    Txt_Deduction.Value = Nothing
                End If

                'Commission
                If vSqlReader.IsDBNull(20) = False Then
                    Txt_Commission.Value = Trim(vSqlReader(20))
                Else
                    Txt_Commission.Value = Nothing
                End If

                'Store_Code
                If vSqlReader.IsDBNull(21) = False Then
                    Txt_Store.Value = vSqlReader(21)
                Else
                    Txt_Store.SelectedIndex = -1
                End If

                'Picture
                If vSqlReader.IsDBNull(22) = False Then
                    Dim arrayImage() As Byte = CType(vSqlReader(22), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)
                    PictureBox1.Image = Image.FromStream(ms)

                Else
                    PictureBox1.Image = PictureBox1.InitialImage
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Txt_Code.ReadOnly = True

            sQueryBarCode()
            sQueryProductFormula()
            sQueryPackUnit()

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
    '        " Select DescA  From Items " & _
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
            vsqlCommand.CommandText = _
            " Select Top 5 DescA  From Items Where Company_Code = " & vCompanyCode & _
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
                            vSqlstring = _
                            " Delete From Item_Details " & _
                            " Where  Item_Code    = '" & Txt_Code.Text & "'" & _
                            " And    Ser        = '" & Grd_ItemDetails.ActiveRow.Cells("Code").Value & "'"
                        End If
                    End If
                ElseIf Grd_ProductFormula.Focused Then
                    If Not Grd_ProductFormula.ActiveRow Is Nothing Then
                        If Grd_ProductFormula.ActiveRow.Cells("DML").Value = "I" Or Grd_ProductFormula.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_ProductFormula.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_ProductFormula.ActiveRow.Cells("DML").Value = "N" Or Grd_ProductFormula.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring = _
                            " Delete From Product_Formula " & _
                            " Where  Item_Code   = '" & Txt_Code.Text & "'" & _
                            " And    Ser         = '" & Grd_ProductFormula.ActiveRow.Cells("Ser").Value & "'"
                        End If
                    End If
                ElseIf Grd_PackUnit.Focused Then
                    If Not Grd_PackUnit.ActiveRow Is Nothing Then
                        If Grd_PackUnit.ActiveRow.Cells("DML").Value = "I" Or Grd_PackUnit.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_PackUnit.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_PackUnit.ActiveRow.Cells("DML").Value = "N" Or Grd_PackUnit.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring = _
                            " Delete From Items_PackUnit " & _
                            " Where  Item_Code   = '" & Txt_Code.Text & "'" & _
                            " And    Ser         = '" & Grd_PackUnit.ActiveRow.Cells("Ser").Value & "'"
                        End If
                    End If
                ElseIf vFocus = "Master" Then
                    vSqlstring = _
                    " Delete From Item_Details Where Item_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Product_Formula Where Item_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Items Where Code = '" & Txt_Code.Text & "'"
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
        'sOpenLov("Select Code, Name From Users", "الموظفين")
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
        Txt_SPrice.Value = Nothing
        TXT_ItemCategoryCode.Text = ""
        TXT_ItemCategoryDesc.Text = ""
        Txt_PackUnitCode.Text = ""
        Txt_PackUnitDesc.Text = ""
        Txt_ProviderCode.Text = ""
        Txt_ProviderDesc.Text = ""
        Txt_AccountSer.Text = ""
        Txt_AccountDesc.Text = ""
        Txt_BarCode.Text = ""
        Txt_Store.SelectedIndex = -1

        PictureBox1.Image = PictureBox1.InitialImage

        Txt_CPrice.Value = Nothing
        Txt_Deduction.Value = Nothing
        Txt_DemandPoint.Value = Nothing
        Txt_Commission.Value = Nothing
        Chk_IsActive.Checked = True
        Chk_IsRaw.Checked = False
        'sLoadItems()

        vMasterBlock = "NI"
        vFocus = "Master"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")
        DTS_ItemDetails.Rows.Clear()
        DTS_PackUnit.Rows.Clear()
        DTS_Formula.Rows.Clear()
        Txt_Code.ReadOnly = False
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
        Dim vNumberOfDigits_InItemCode As Int16

        If cControls.fReturnValue(" Select IsNull(UseCategoriesInItemCode, 'N') From Controls ", Me.Name) = "Y" Then
            vSqlString = " Select Count (*) + 1 From Items Where Cat_Ser = " & Trim(TXT_ItemCategoryCode.Text)

            vNumberOfDigits_InItemCode = cControls.fReturnValue(" Select IsNull(NumberOfDigitsInItemCode, 6) From Controls ", Me.Name)
            Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(vNumberOfDigits_InItemCode, "0") & "-" & vLovReturn3
            Exit Sub
        End If

        vNumberOfDigits_InItemCode = cControls.fReturnValue(" Select IsNull(NumberOfDigitsInItemCode, 6) From Controls ", Me.Name)

        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Items "
        Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(vNumberOfDigits_InItemCode, "0")
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

        If pType = "Items" Then
            If vLang = "A" Then
                Dim Frm_Items As New Frm_ItemsLov
                Frm_Items.ShowDialog()
            Else
                Dim Frm_Items As New Frm_ItemsLov_L
                Frm_Items.ShowDialog()
            End If

            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                Grd_ProductFormula.ActiveRow.Cells("Code").Value = Trim(vLovReturn1)
                Grd_ProductFormula.ActiveRow.Cells("DescA").Value = Trim(VLovReturn2)
                Grd_ProductFormula.ActiveRow.Cells("PackUnit").Value = cControls.fReturnValue(" Select PU_Code From Items Where Code = '" & Trim(vLovReturn1) & "'", Me.Name)
            End If
        ElseIf pType = "Categories" Then
            If cControls.fReturnValue(" Select IsNull(UseCategoriesInItemCode, 'N') From Controls ", Me.Name) = "Y" Then
                If vMasterBlock = "N" Or vMasterBlock = "U" Then
                    vcFrmLevel.vParentFrm.sForwardMessage("101", Me)
                    Exit Sub
                End If
            End If

            If vLang = "A" Then
                Dim Frm_LovTree As New FRM_LovCategoryA
                Frm_LovTree.ShowDialog()
            Else
                Dim Frm_LovTree As New FRM_LovCategoryL
                Frm_LovTree.ShowDialog()
            End If

            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                TXT_ItemCategoryCode.Text = vLovReturn1
                TXT_ItemCategoryDesc.Text = VLovReturn2

                If cControls.fReturnValue(" Select IsNull(UseCategoriesInItemCode, 'N') From Controls ", Me.Name) = "Y" Then
                    Dim vSqlString As String
                    vSqlString = " Select Count (*) + 1 From Items Where Cat_Ser = " & vLovReturn1

                    Dim vNumberOfDigits_InItemCode As Int16 = cControls.fReturnValue(" Select IsNull(NumberOfDigitsInItemCode, 6) From Controls ", Me.Name)
                    Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(vNumberOfDigits_InItemCode, "0") & "-" & vLovReturn3
                End If
                'TXT_ItemCategoryCode.Tag = vLovReturn3
            End If
        ElseIf pType = "Accounts" Then
            vLovReturn1 = ""
            VLovReturn2 = ""
            vLovReturn3 = ""
            vSelectedSortedList_1.Clear()

            If vLang = "A" Then
                Dim Frm_Lov As New FRM_LovTreeA(True)
                Frm_Lov.ShowDialog()
            Else
                Dim Frm_Lov As New FRM_LovTreeA(True)
                Frm_Lov.ShowDialog()
            End If

            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                Txt_AccountSer.Text = vLovReturn1
                Txt_AccountDesc.Text = VLovReturn2
                Txt_AccountSer.Tag = vLovReturn3
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
                ElseIf pType = "Providers" Then
                    Txt_ProviderCode.Text = vLovReturn1
                    Txt_ProviderDesc.Text = VLovReturn2
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
        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm = Me.ParentForm
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "التفاصيل")
            sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))

            ToolBar_Main.Tools("Btn_CopyItems").SharedProps.Visible = False
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
            If Grd_Summary.Selected.Rows.Count > 0 Then
                If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                    sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
                Else
                    sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
                End If
            Else
                sNewRecord()
            End If

            ToolBar_Main.Tools("Btn_CopyItems").SharedProps.Visible = True
        End If
    End Sub
    Public Sub sChangeTab()
        If Tab_Main.Tabs("Tab_Summary").Selected Then
            Tab_Main.Tabs("Tab_Details").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
        Else
            Tab_Main.Tabs("Tab_Summary").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
        End If
    End Sub
#End Region
#Region " Print                                                                          "
    Public Sub sPrint()
        'If vMasterBlock = "NI" Then
        '    Return
        'End If

        vClear = False
        If fSaveAll(True) = False Then
            Return
        End If
        vClear = True

        vSortedList.Clear()
        Dim vCompany As String = cControls.fReturnValue(" Select DescA From Company ", Me.Name)
        Dim vSqlString As String
        vSqlString = _
        "  Select Items.Code as Item_Code,                                    " & _
        "         Items.DescA as Item_Desc,                                   " & _
        "         Items.Provider_Code,                                        " & _
        "         Providers.DescA as Provider_Desc,                           " & _
        "         Items.Cat_Ser as Cat_Code,                                  " & _
        "         Categories.DescA as Cat_Desc,                               " & _
        "         Items.Price,                                                " & _
        "         Items.SPrice,                                               " & _
        "'" & vCompany & "' as Company                                        " & _
        " From Items Inner Join Categories                                    " & _
        " On Categories.Ser = Items.Cat_Ser                                   " & _
        " Left Join Providers                                                 " & _
        " On Providers.Code = Items.Provider_Code                             " & _
        " Where 1 = 1                                                         " & _
        sFndByItems("Items")

        vSortedList.Add("DT_ItemDetails", vSqlString)

        vSqlString = _
       " Select DescA as CompanyName, Picture From Company "

        vSortedList.Add("DT_Header", vSqlString)


        If vLang = "A" Then
            Dim vRep_Preview As New FRM_ReportPreviewL("قائمة الأصناف", vSortedList, New DS_ItemsDetails, New Rep_ItemsDetails)
            vRep_Preview.MdiParent = Me.MdiParent
            vRep_Preview.Show()
        Else
            Dim vRep_Preview As New FRM_ReportPreviewL("Items List", vSortedList, New DS_ItemsDetails, New Rep_ItemsDetails_L)
            vRep_Preview.MdiParent = Me.MdiParent
            vRep_Preview.Show()
        End If

        vMasterBlock = "N"
    End Sub
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

        If Txt_Desc.Text.Length = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
            Txt_Desc.Select()
            Return False
        End If

        If cControls.fCount_Rec(" From Items Where DescA = '" & Trim(Txt_Desc.Text) & "' And Code <> '" & Trim(Txt_Code.Text) & "'") > 0 Then
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
                If cControls.fIsExist(" From Items Where BarCode = '" & Trim(Txt_BarCode.Text) & "'", Me.Name) = True Then
                    vcFrmLevel.vParentFrm.sForwardMessage("16", Me)
                    Txt_BarCode.SelectAll()
                    Return False
                End If
            End If
        End If

        'If Not IsDBNull(Txt_Price.Value) And Not IsDBNull(Txt_SPrice.Value) Then
        '    If Txt_Price.Value >= Txt_SPrice.Value Then
        '        vcFrmLevel.vParentFrm.sForwardMessage("86", Me)
        '        Txt_Price.Select()
        '        Return False
        '    End If
        'End If

        'If Not IsDBNull(Txt_Price.Value) Then
        '    If cControls.fCount_Rec(" From Items_SalesTypes Where Item_Code = '" & Trim(Txt_Code.Text) & "'" & _
        '                            " And  Price <= '" & Trim(Txt_Price.Value) & "'") > 0 Then

        '        vcFrmLevel.vParentFrm.sForwardMessage("86", Me)
        '        Txt_Price.Select()
        '        Return False
        '    End If
        'End If

        'If Grd_ItemDetails.Rows.Count = 0 Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("52", Me)
        '    Return False
        'End If
        Return True
    End Function
    Private Sub sSaveMain()
        If fValidateMain() = False Then
            Return
        End If

        Dim vPrice, vSPrice, vCPrice, vDeduction, vDemandPoint, vCommission, vAccount_Ser, vProvider_Code, vStore As String
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

        'CPrice
        If Not IsDBNull(Txt_CPrice.Value) Then
            If Not Txt_CPrice.Value = Nothing Then
                vCPrice = Trim(Txt_CPrice.Value)
            Else
                vCPrice = "NULL"
            End If
        Else
            vCPrice = "NULL"
        End If

        'Deduction
        If Not IsDBNull(Txt_Deduction.Value) Then
            If Not Txt_Deduction.Value = Nothing Then
                vDeduction = Trim(Txt_Deduction.Value)
            Else
                vDeduction = "NULL"
            End If
        Else
            vDeduction = "NULL"
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

        'Commission
        If Not IsDBNull(Txt_Commission.Value) Then
            If Not Txt_Commission.Value = Nothing Then
                vCommission = Trim(Txt_Commission.Value)
            Else
                vCommission = "NULL"
            End If
        Else
            vCommission = "NULL"
        End If

        If Txt_AccountDesc.Text = "" Then
            vAccount_Ser = "NULL"
        Else
            vAccount_Ser = "'" & Trim(Txt_AccountSer.Tag) & "'"
        End If

        'Provider_Code
        If Txt_ProviderDesc.Text = "" Then
            vProvider_Code = "NULL"
        Else
            vProvider_Code = "'" & Trim(Txt_ProviderCode.Text) & "'"
        End If

        If Txt_Store.Value = Nothing Then
            vStore = "NULL"
        Else
            vStore = "'" & Txt_Store.Value & "'"
        End If

        If vMasterBlock = "I" Then

            Dim x As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCode, 'Y') From Controls ", Me.Name)
            If x = "Y" Then
                sNewCode()
            End If

            vSqlCommand = " Insert Into Items  (              Code,                          DescA,           Ser,                          Cat_Ser,                                   PU_Code,                  Provider_Code,           Account_Ser,                   Remarks,                Price,               BarCode,                DemandPoint,              IsActive,                  IsRaw,               SPrice,           CPrice,        Deduction,          Commission,           Company_Code,       Store_Code )" & _
                                      " Values ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '00',        '" & Trim(TXT_ItemCategoryCode.Text) & "', '" & Trim(Txt_PackUnitCode.Text) & "', " & vProvider_Code & ", " & vAccount_Ser & ", '" & Trim(Txt_Remarks.Text) & "'," & vPrice & ", '" & Txt_BarCode.Text & "', " & vDemandPoint & ", '" & Chk_IsActive.Tag & "',  '" & Chk_IsRaw.Tag & "', " & vSPrice & ", " & vCPrice & ", " & vDeduction & ", " & vCommission & ", " & vCompanyCode & ", " & vStore & " )"
            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Items " & _
                          " Set   DescA         = '" & Trim(Txt_Desc.Text) & "'," & _
                          "       Cat_Ser       = '" & Trim(TXT_ItemCategoryCode.Text) & "'," & _
                          "       PU_Code       = '" & Trim(Txt_PackUnitCode.Text) & "', " & _
                          "       Provider_Code =  " & vProvider_Code & ", " & _
                          "       Account_Ser   =  " & vAccount_Ser & ", " & _
                          "       Remarks       = '" & Trim(Txt_Remarks.Text) & "', " & _
                          "       Price         =  " & vPrice & ", " & _
                          "       BarCode       = '" & Txt_BarCode.Text & "'," & _
                          "       DemandPoint   =  " & vDemandPoint & ", " & _
                          "       IsActive      = '" & Chk_IsActive.Tag & "'," & _
                          "       IsRaw         = '" & Chk_IsRaw.Tag & "', " & _
                          "       SPrice        =  " & vSPrice & ", " & _
                          "       CPrice        =  " & vCPrice & ", " & _
                          "       Deduction     =  " & vDeduction & ", " & _
                          "       Commission    =  " & vCommission & ", " & _
                          "       Store_Code    =  " & vStore & _
                          " Where Code          = '" & Txt_Code.Text & "'"
            sFillSqlStatmentArray(vSqlCommand)
        End If
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub TXT_All_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) _
    Handles TXT_ItemCategoryCode.EditorButtonClick, Txt_PackUnitCode.EditorButtonClick, _
    Txt_ProviderCode.EditorButtonClick, Txt_AccountSer.EditorButtonClick, Txt_Details_PU.EditorButtonClick

        Dim vTitle As String

        If sender.name = "TXT_ItemCategoryCode" Then
            If vLang = "A" Then
                vTitle = "التصنيفات"
            Else
                vTitle = "Categories"
            End If

            sOpenLov(" Select Ser, DescA From Categories Where Parent_Code Is NULL ", vTitle, "Categories", "Categories", " Select Code, DescA From Categories Where Parent_Code = '")
        ElseIf sender.name = "Txt_PackUnitCode" Then
            If vLang = "A" Then
                vTitle = "وحدات التعبئة"
            Else
                vTitle = "Pack Units"
            End If

            sOpenLov(" Select Code, DescA From Pack_Unit Where 1 = 1", vTitle, "Pack_Units")
        ElseIf sender.name = "Txt_ProviderCode" Then
            If vLang = "A" Then
                vTitle = "الموردين"
            Else
                vTitle = "Providers"
            End If

            sOpenLov(" Select Code, DescA From Providers Where 1 = 1 And Company_Code = " & vCompanyCode, vTitle, "Providers")
        ElseIf sender.name = "Txt_Details_PU" Then
            If vLang = "A" Then
                vTitle = "وحدات التعبئة"
            Else
                vTitle = "Pack Units"
            End If

            sOpenLov(" Select Code, DescA From Pack_Unit Where 1 = 1", vTitle, "Details_PU")
        ElseIf sender.name = "Txt_AccountSer" Then
            sOpenLov("", "Accounts", "Accounts")
        End If
    End Sub
    Private Sub TXT_ItemCategoryCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXT_ItemCategoryCode.Validating
        If TXT_ItemCategoryCode.Text <> "" Then
            If cControls.fCount_Rec(" From Categories Where Ser = '" & Trim(TXT_ItemCategoryCode.Text) & "'") = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                TXT_ItemCategoryCode.Select()
                e.Cancel = True
            Else
                Dim vCode As String = cControls.fReturnValue(" Select Code From Categories Where Ser = " & Trim(TXT_ItemCategoryCode.Text), Me.Name)
                If cControls.fCount_Rec(" From Categories Where Parent_Code = '" & vCode & "' ") > 0 Then
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
    Private Sub Txt_ProviderCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_ProviderCode.Validating
        If Txt_ProviderCode.Text <> "" Then
            If cControls.fCount_Rec(" From Providers Where Code = '" & Trim(Txt_ProviderCode.Text) & "'") = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                Txt_ProviderCode.Select()
                e.Cancel = True
            Else
                Txt_ProviderDesc.Text = cControls.fReturnValue(" Select DescA From Providers Where Code = '" & Trim(Txt_ProviderCode.Text) & "'", Me.Name)
                e.Cancel = False
            End If
        Else
            Txt_ProviderDesc.Text = ""
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
                If cControls.fCount_Rec(" From Items Where BarCode = '" & _
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
    Handles Txt_Code.Enter, Txt_Remarks.Enter, Txt_Price.Enter, _
        TXT_ItemCategoryCode.Enter, TXT_ItemCategoryDesc.Enter, Txt_PackUnitCode.Enter, _
        Txt_PackUnitDesc.Enter, Txt_ProviderCode.Enter, Txt_ProviderDesc.Enter, _
        Txt_CPrice.Enter, Txt_Deduction.Enter, Txt_Commission.Enter, Txt_Desc.Enter

        vFocus = "Master"
    End Sub
    Private Sub Btn_ItemProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ItemProperties.Click
        If Not Txt_Desc.Text = "" Then
            Dim Frm_ItmPrp As New Frm_ItemProperties(Trim(Txt_Code.Text), "00", Txt_Desc.Text)
            Frm_ItmPrp.ShowDialog()
        Else
            vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
            Txt_Desc.Select()
        End If
    End Sub
    Private Sub Txt_Code_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
    Handles Txt_Code.KeyUp, Txt_Remarks.KeyUp, Txt_Price.KeyUp, _
            Txt_BarCode.KeyUp, TXT_ItemCategoryCode.KeyUp, Txt_PackUnitCode.KeyUp, _
            Txt_ProviderCode.KeyUp, Txt_DemandPoint.KeyUp, Txt_AccountSer.KeyUp, _
            Txt_AccountDesc.KeyUp, Txt_SPrice.KeyUp, Txt_CPrice.KeyUp, Txt_Deduction.KeyUp, _
            Txt_Commission.KeyUp, Txt_Desc.KeyUp, Txt_Details_PU.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyData = Keys.F12 Then
            Dim ee As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs
            TXT_All_EditorButtonClick(sender, ee)
        End If
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
        Dim vSqlString, vPrice, vSPrice, vPackUnit, vGetSerial As String
        Grd_ItemDetails.UpdateData()
        Dim vCounter As Integer = 0
        For Each vRow In Grd_ItemDetails.Rows

            If IsDBNull(vRow.Cells("PackUnit").Tag) Then
                vPackUnit = "NULL"
            Else
                vPackUnit = "'" & vRow.Cells("PackUnit").Value & "'"
            End If

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Items_BarCode " & _
                             " Where  Item_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name) + vCounter
                vGetSerial = CStr(vGetSerial).PadLeft(3, "0")

                vSqlString = " Insert Into Items_BarCode  (         Item_Code,               Ser,                              BarCode,                                    Remarks             )" & _
                             "                  Values   ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("BarCode").Text) & "', '" & Trim(vRow.Cells("Remarks").Text) & "')"
                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Items_BarCode " & _
                              " Set     BarCode      = '" & Trim(vRow.Cells("BarCode").Text) & "', " & _
                              "         Remarks      = '" & vRow.Cells("Remarks").Text & "' " & _
                              " Where   Item_Code    = '" & Trim(Txt_Code.Text) & "'" & _
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
            vsqlCommand.CommandText = " Select Ser, " & _
                                      "        BarCode, " & _
                                      "        Remarks  " & _
                                      " From  Items_BarCode " & _
                                      " Where Item_Code = '" & Trim(Txt_Code.Text) & "'" & _
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
                    DTS_ItemDetails.Rows(vRowCounter)("Remarks") = vSqlReader(2)
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
        vsqlCommand.CommandText = _
                       " Select Item_Properties.DescA, " & _
                       "        Item_Properties_Elements.DescA, " & _
                       "        Items_ItemProperties_Elements.ItmPrp_Code, " & _
                       "        Items_ItemProperties_Elements.ItemPrpEle_Code " & _
                       " From Items_ItemProperties_Elements Inner Join Item_Properties_Elements " & _
                       " On   Items_ItemProperties_Elements.ItemPrpEle_Code = Item_Properties_Elements.Code " & _
                       " And  Items_ItemProperties_Elements.ItmPrp_Code     = Item_Properties_Elements.ItmPrp_Code " & _
                       " Inner Join Item_Properties " & _
                       " On Item_Properties_Elements.ItmPrp_Code = Item_Properties.Code" & _
                       " Where Items_ItemProperties_Elements.Item_Code = '" & Trim(Txt_Code.Text) & "'" & _
                       " And   Items_ItemProperties_Elements.Item_Ser  = '" & pRow.Cells("Code").Value & "'" & _
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
        If Grd_ItemDetails.ActiveRow.Cells("Properties").Activated Then
            If Not IsDBNull(Grd_ItemDetails.ActiveRow.Cells("DescA").Value) Then
                Dim Frm_ItmPrp As New Frm_ItemProperties(Trim(Txt_Code.Text), Grd_ItemDetails.ActiveRow.Cells("Code").Text, Grd_ItemDetails.ActiveRow.Cells("DescA").Text)
                Frm_ItmPrp.ShowDialog()
                sQueryToolTip(Grd_ItemDetails.ActiveRow)
            Else
                vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
                Grd_ItemDetails.ActiveRow.Cells("DescA").Selected = True
            End If
        ElseIf Grd_ItemDetails.ActiveRow.Cells("SalesPrices").Activated Then
            Dim Frm_SalesPrice As New Frm_SalesPrices(Trim(Txt_Code.Text), Grd_ItemDetails.ActiveRow.Cells("Code").Text, Grd_ItemDetails.ActiveRow.Cells("DescA").Text)
            Frm_SalesPrice.ShowDialog()
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
        Dim vSqlString, vPackUnit, vValue, vGetSerial As String
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
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Product_Formula " & _
                        " Where Item_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name) + vCounter
                vGetSerial = CStr(vGetSerial).PadLeft(3, "0")

                vSqlString = " Insert Into Product_Formula  (             Item_Code,                    Ser,                     ChildItem_Code,                                 PackUnit,                       TValue,                          Remarks)" & _
                             "              Values          ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("Code").Text) & "', '" & Trim(vRow.Cells("PackUnit").Value) & "', " & vValue & ", '" & Trim(vRow.Cells("Remarks").Text) & "')"
                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Product_Formula " & _
                              " Set     ChildItem_Code    = '" & Trim(vRow.Cells("Code").Text) & "'," & _
                              "         PackUnit          = '" & vRow.Cells("PackUnit").Value & "', " & _
                              "         TValue            =  " & vValue & ", " & _
                              "         Remarks           = '" & Trim(vRow.Cells("Remarks").Text) & "' " & _
                              " Where   Item_Code         = '" & Trim(Txt_Code.Text) & "'" & _
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
            vsqlCommand.CommandText = " Select Product_Formula.Ser, " & _
                                      "        Product_Formula.ChildItem_Code, " & _
                                      "        Items.DescA,                 " & _
                                      "        Product_Formula.PackUnit,    " & _
                                      "        Pack_Unit.DescA,             " & _
                                      "        Product_Formula.TValue, " & _
                                      "        Product_Formula.Remarks               " & _
                                      " From Product_Formula Inner Join Items " & _
                                      " On Product_Formula.ChildItem_Code = Items.Code " & _
                                      " Inner Join Pack_Unit " & _
                                      " On Product_Formula.PackUnit = Pack_Unit.Code " & _
                                      " Where Item_Code = '" & Trim(Txt_Code.Text) & "'" & _
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
                    DTS_Formula.Rows(vRowCounter)("Remarks") = vSqlReader(6)
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
            vsqlCommand.CommandText = _
            " Select Code, DescA From Pack_Unit " & _
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
        If sender.name = "Txt_Items" Then
            sOpenLov("", "Items", "الأصناف")
        ElseIf sender.Name = "Txt_FormulaPackUnit" Then
            'sOpenLov(" Select Code, DescA From Pack_Unit Where 1 = 1", "وحدة التعبئة")
        End If
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
            vsqlCommand.CommandText = " Select Ser, PU_Code, DescA, PPrice, SPrice,  Remarks, Number, [Default] From Items_PackUnit " & _
                                      " Where Item_Code = '" & Trim(Txt_Code.Text) & "'" & _
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

                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Items_PackUnit " & _
                             " Where Item_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(2, "0") + vCounter

                vSqlString = " Insert Into Items_PackUnit    (          Item_Code,                 Ser,                                PU_Code,                                   DescA,                  PPrice,         SPrice,                           Remarks,                    Number,           [Default])" & _
                             " Values                        ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("PU_Code").Text) & "', '" & Trim(vRow.Cells("DescA").Text) & "'," & vPPrice & "," & vSPrice & ", '" & Trim(vRow.Cells("Remarks").Text) & "',   " & vNumber & ", " & vDefault & ")"
                sFillSqlStatmentArray(vSqlString)

                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update Items_PackUnit " & _
                             " Set    PU_Code      = '" & vRow.Cells("PU_Code").Text & "', " & _
                             "        DescA        = '" & vRow.Cells("DescA").Text & "', " & _
                             "        PPrice       =  " & vPPrice & ", " & _
                             "        SPrice       =  " & vSPrice & ", " & _
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "', " & _
                             "        Number       =  " & vNumber & ", " & _
                             "        [Default]    =  " & vDefault & _
                             " Where  Item_Code    = '" & Trim(Txt_Code.Text) & "' " & _
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
                            If cControls.fCount_Rec("From Pack_Unit " & _
                                " Where Code = '" & Grd_PackUnit.ActiveRow.Cells("PU_Code").Value & "'") = 0 Then

                                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                                Grd_PackUnit.ActiveRow.Cells("PU_Code").SelectAll()
                                Grd_PackUnit.ActiveRow.Cells("DescA").Value = ""
                            Else
                                Dim vSqlString As String = " Select DescA From Pack_Unit " & _
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
                'sOpenLov(" Select Code, DescA From Pack_Unit Where 1 = 1", "وحدة التعبئه")
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
                    Dim Frm_SalesPrice As New Frm_SalesPrices(Trim(Txt_Code.Text), "", Trim(Txt_Desc.Text), Grd_PackUnit.ActiveRow.Cells("Ser").Value)
                    Frm_SalesPrice.ShowDialog()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#End Region

#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummaryMain(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            Dim vCodeFilter, vDescFilter, vOrder As String
            If pCode = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Items.Code Like '%" & pCode & "%'"
            End If

            If pDesc = "" Then
                vDescFilter = ""
            Else
                vDescFilter = " And Items.DescA Like '%" & pDesc & "%'"
            End If

            'Here I check if the code is auto generated then I Order the item wz code else is order by Desc
            Dim x As String = cControls.fReturnValue("Select IsNull(AutomaticallyGenerateCode, 'Y') From Controls ", Me.Name)
            If x = "Y" Then
                vOrder = " Order By Items.Code "
            Else
                vOrder = " Order By Items.DescA "
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            "  Select Items.Code, " & _
            "         Items.DescA as Item_DescA,                          " & _
            "         Items.Price,                                       " & _
            "         Items.SPrice,                                      " & _
            "         Categories.DescA as Cat_DescA,                     " & _
            "         Pack_Unit.DescA as PU_DescA,                       " & _
            "         Providers.DescA as Provider_DescA,                 " & _
            "         IsNull(IsActive, 'Y')                              " & _
            " From Items INNER Join Categories " & _
            " On Items.Cat_Ser = Categories.Ser " & _
            " INNER Join Pack_Unit " & _
            " On Items.PU_Code = Pack_Unit.Code " & _
            " LEFT Join Providers " & _
            " On Providers.Code = Items.Provider_Code " & _
            " Where 1 = 1                                               " & _
            " And Items.Company_Code = " & vCompanyCode & _
            vCodeFilter & _
            vDescFilter & _
            vOrder

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
                    DTS_Summary.Rows(vRowCounter)("Price") = vSqlReader(2)
                Else
                    DTS_Summary.Rows(vRowCounter)("Price") = Nothing
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("SPrice") = vSqlReader(3)
                Else
                    DTS_Summary.Rows(vRowCounter)("SPrice") = Nothing
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Summary.Rows(vRowCounter)("Category") = Trim(vSqlReader(4))
                Else
                    DTS_Summary.Rows(vRowCounter)("Category") = ""
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Summary.Rows(vRowCounter)("PackUnit") = Trim(vSqlReader(5))
                Else
                    DTS_Summary.Rows(vRowCounter)("PackUnit") = ""
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Summary.Rows(vRowCounter)("Provider") = Trim(vSqlReader(6))
                Else
                    DTS_Summary.Rows(vRowCounter)("Provider") = ""
                End If

                If vSqlReader(7) = "N" Then
                    Grd_Summary.Rows(vRowCounter).Appearance.BackColor2 = Color.White
                    Grd_Summary.Rows(vRowCounter).Appearance.ForeColor = Color.LightBlue
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
            vsqlCommand.CommandText = " Select Ser, DescA, Price, Remarks " & _
                                      " From Items " & _
                                      " Where Code = '" & pRow("Code") & "'" & _
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
        sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
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

    Private Sub Btn_SalesPrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SalesPrices.Click
        If Trim(Txt_Desc.Text) <> "" Then
            Dim Frm_SalesPrice As New Frm_SalesPrices(Trim(Txt_Code.Text), "", Trim(Txt_Desc.Text))
            Frm_SalesPrice.ShowDialog()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
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
                'cControls.sSaveImage(Image.FromFile(OpenFileDialog1.FileName), "Company")
                sSave_MainPicture()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function sSave_MainPicture()
        Try
            If Not IsDBNull(UltraPictureBox1.Image) Then
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
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try
    End Function

    Private Sub sCopyItems()
        Try
            vLovReturn1 = ""
            VLovReturn2 = ""
            Dim Frm_Items As New Frm_ItemsLov
            Frm_Items.ShowDialog()
            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                sNewRecord()

                Dim vsqlCommand As New SqlClient.SqlCommand
                Dim vRowCounter As Integer
                vsqlCommand.CommandText = _
                " Select  Items.Code,                                        " & _
                "         Items.DescA,                                       " & _
                "         Items.Remarks,                                     " & _
                "         Items.Price,                                       " & _
                "         BarCode,                                           " & _
                "         Categories.Code as Cat_Code,                       " & _
                "         Cat_Ser,                                           " & _
                "         Categories.DescA as Cat_DescA,                     " & _
                "         PU_Code,                                           " & _
                "         Pack_Unit.DescA as PU_DescA,                       " & _
                "         Provider_Code,                                     " & _
                "         Providers.DescA as Provider_DescA,                 " & _
                "         Financial_Definitions_Tree.Code as Account_Code,   " & _
                "         Financial_Definitions_Tree.DescA,                  " & _
                "         Items.Account_Ser,                                 " & _
                "         DemandPoint,                                       " & _
                "         IsActive,                                          " & _
                "         IsRaw,                                             " & _
                "         SPrice,                                            " & _
                "         CPrice,                                            " & _
                "         Items.Deduction,                                   " & _
                "         Items.Commission,                                  " & _
                "         Items.Picture,                                     " & _
                "         ROW_Number() Over (Order By Items.Code) as  RecPos " & _
                " From Items Inner Join Categories                           " & _
                " On Items.Cat_Ser = Categories.Ser                          " & _
                " Inner Join Pack_Unit                                       " & _
                " On Items.PU_Code = Pack_Unit.Code                          " & _
                " LEFT Join Providers                                       " & _
                " On Items.Provider_Code = Providers.Code                    " & _
                " Left Join Financial_Definitions_Tree                       " & _
                " On Items.Account_Ser = Financial_Definitions_Tree.Ser      " & _
                " Where 1 = 1                                                " & _
                " And Items.Company_Code = " & vCompanyCode & _
                " And Items.Code = '" & vLovReturn1 & "' "

                vsqlCommand.Connection = cControls.vSqlConn
                cControls.vSqlConn.Open()
                Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
                vRowCounter = 0
                DTS_Summary.Rows.Clear()

                Do While vSqlReader.Read
                    DTS_Summary.Rows.SetCount(vRowCounter + 1)
                    'DTS_Summary.Rows(vRowCounter)("Code") = Trim(vSqlReader(0))

                    'Desc
                    Txt_Desc.Text = Trim(vSqlReader(1))

                    'Remarks
                    If vSqlReader.IsDBNull(2) = False Then
                        Txt_Remarks.Text = Trim(vSqlReader(2))
                    Else
                        Txt_Remarks.Text = ""
                    End If

                    'Price
                    If vSqlReader.IsDBNull(3) = False Then
                        Txt_Price.Value = Trim(vSqlReader(3))
                    Else
                        Txt_Price.Value = Nothing
                    End If

                    'BarCode()
                    If vSqlReader.IsDBNull(4) = False Then
                        Txt_BarCode.Value = vSqlReader(4)
                    Else
                        Txt_BarCode.Text = ""
                    End If

                    'Cat_Code
                    If vSqlReader.IsDBNull(5) = False Then
                        TXT_ItemCategoryCode.Text = Trim(vSqlReader(5))
                    Else
                        TXT_ItemCategoryCode.Text = ""
                    End If

                    'Cat_Ser
                    If vSqlReader.IsDBNull(6) = False Then
                        TXT_ItemCategoryCode.Tag = Trim(vSqlReader(6))
                    Else
                        TXT_ItemCategoryCode.Tag = ""
                    End If

                    'Cat_Desc
                    If vSqlReader.IsDBNull(7) = False Then
                        TXT_ItemCategoryDesc.Text = Trim(vSqlReader(7))
                    Else
                        TXT_ItemCategoryDesc.Text = ""
                    End If

                    'PackUnit_Code
                    If vSqlReader.IsDBNull(8) = False Then
                        Txt_PackUnitCode.Text = Trim(vSqlReader(8))
                    Else
                        Txt_PackUnitCode.Text = ""
                    End If

                    'PackUnit_Desc
                    If vSqlReader.IsDBNull(9) = False Then
                        Txt_PackUnitDesc.Text = Trim(vSqlReader(9))
                    Else
                        Txt_PackUnitDesc.Text = ""
                    End If

                    'Provider_Code
                    If vSqlReader.IsDBNull(10) = False Then
                        Txt_ProviderCode.Text = Trim(vSqlReader(10))
                    Else
                        Txt_ProviderCode.Text = ""
                    End If

                    'Provider_Desc
                    If vSqlReader.IsDBNull(11) = False Then
                        Txt_ProviderDesc.Text = Trim(vSqlReader(11))
                    Else
                        Txt_ProviderDesc.Text = ""
                    End If

                    'Account_Code
                    If vSqlReader.IsDBNull(12) = False Then
                        Txt_AccountSer.Text = Trim(vSqlReader(12))
                    Else
                        Txt_AccountSer.Text = ""
                    End If

                    'Account_Desc
                    If vSqlReader.IsDBNull(13) = False Then
                        Txt_AccountDesc.Text = Trim(vSqlReader(13))
                    Else
                        Txt_AccountDesc.Text = ""
                    End If

                    'Account_Ser
                    If vSqlReader.IsDBNull(14) = False Then
                        Txt_AccountSer.Tag = Trim(vSqlReader(14))
                    Else
                        Txt_AccountSer.Tag = Nothing
                    End If

                    'DemandPoint
                    If vSqlReader.IsDBNull(15) = False Then
                        Txt_DemandPoint.Value = Trim(vSqlReader(15))
                    Else
                        Txt_DemandPoint.Text = ""
                    End If

                    'IsActive
                    If vSqlReader.IsDBNull(16) = False Then
                        If vSqlReader(16) = "N" Then
                            Chk_IsActive.Checked = False
                        Else
                            Chk_IsActive.Checked = True
                        End If
                    End If

                    'IsRaw
                    If vSqlReader.IsDBNull(17) = False Then
                        If vSqlReader(17) = "N" Then
                            Chk_IsRaw.Checked = False
                        Else
                            Chk_IsRaw.Checked = True
                        End If
                    Else
                        Chk_IsRaw.Checked = False
                    End If

                    'SPrice
                    If vSqlReader.IsDBNull(18) = False Then
                        Txt_SPrice.Value = Trim(vSqlReader(18))
                    Else
                        Txt_SPrice.Value = Nothing
                    End If

                    'CPrice
                    If vSqlReader.IsDBNull(19) = False Then
                        Txt_CPrice.Value = Trim(vSqlReader(19))
                    Else
                        Txt_CPrice.Value = Nothing
                    End If

                    'Deduction
                    If vSqlReader.IsDBNull(20) = False Then
                        Txt_Deduction.Value = Trim(vSqlReader(20))
                    Else
                        Txt_Deduction.Value = Nothing
                    End If

                    'Commission
                    If vSqlReader.IsDBNull(21) = False Then
                        Txt_Commission.Value = Trim(vSqlReader(21))
                    Else
                        Txt_Commission.Value = Nothing
                    End If

                    vRowCounter += 1
                Loop
                cControls.vSqlConn.Close()
                vSqlReader.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ToolBar_Main_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case e.Tool.Key
            Case "Btn_CopyItems"
                sCopyItems()
        End Select
    End Sub
End Class