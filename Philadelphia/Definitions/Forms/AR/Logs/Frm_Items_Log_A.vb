Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource

Public Class Frm_Items_Log_A
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

        'sQueryUser(vUsrCode)

        vMasterBlock = "NI"
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
        'Tab_Main.Tabs("Tab_Details").Selected = True

        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
            sQuerySummaryMain()
        Else
            'sSecurity()
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
            Txt_Desc.TextChanged, Txt_AccountDesc.TextChanged, _
            Txt_Store.ValueChanged, Txt_CostCenter_Desc.TextChanged

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

    Private Sub ToolBar_Main_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case e.Tool.Key
            Case "Btn_CopyItems"
                'sCopyItems()
            Case "Btn_ItemMovement"
                'sItemMovement()
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
                    If vFetchRec > cControls.fCount_Rec(" From Items_Log Where Company_Code = " & vCompanyCode) Then
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
            vFetchRec = cControls.fCount_Rec(" From Items_Log Where Company_Code = " & vCompanyCode)
        End If

        Dim vFetchCondition As String
        If pCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And My_ItemsLog.Code = '" & Trim(pCode) & "'"
        End If

        'Here I set vQuery = "Y" to not load auto complete in Desc Field
        vQuery = "Y"

        Try

            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText = _
            " With My_ItemsLog as " & _
            "( Select Items_Log.Code,                                    " & _
            "         Items_Log.Item_Code,                               " & _
            "         Items_Log.DescA as Itm_DescA,                      " & _
            "         Items_Log.Remarks,                                 " & _
            "         Items_Log.Price,                                   " & _
            "         BarCode,                                           " & _
            "         Cat_Ser,                                           " & _
            "         Categories.DescA as Cat_DescA,                     " & _
            "         PU_Code,                                           " & _
            "         Pack_Unit.DescA as PU_DescA,                       " & _
            "         Provider_Code,                                     " & _
            "         Providers.DescA as Provider_DescA,                 " & _
            "         Financial_Definitions_Tree.Code as Account_Code,   " & _
            "         Financial_Definitions_Tree.DescA,                  " & _
            "         Items_Log.Account_Ser,                                 " & _
            "         DemandPoint,                                       " & _
            "         Items_Log.IsActive,                                " & _
            "         IsRaw,                                             " & _
            "         SPrice,                                            " & _
            "         CPrice,                                            " & _
            "         Items_Log.Deduction,                                   " & _
            "         Items_Log.Commission,                                  " & _
            "         Items_Log.Store_Code,                                  " & _
            "         Items_Log.CostCenter_Code,                             " & _
            "         Cost_Center.DescA as CostCenter_Desc,              " & _
            "         Items_Log.Picture,                                     " & _
            "                                                                " & _
            "         -- Log Details                                         " & vbCrLf & _
            "         Employees.DescA as Emp_Desc,                           " & _
            "         Items_Log.TDate,                                       " & _
            "         Items_Log.ComputerName,                                " & _
            "         Items_Log.IPAddress,                                   " & _
            "         Items_Log.Type,                                        " & _
            "         ROW_Number() Over (Order By Items_Log.Code) as  RecPos " & _
            " From Items_Log LEFT JOIN Categories                           " & _
            " On Items_Log.Cat_Ser = Categories.Ser                          " & _
            " Inner Join Pack_Unit                                       " & _
            " On Items_Log.PU_Code = Pack_Unit.Code                          " & _
            " LEFT Join Providers                                       " & _
            " On Items_Log.Provider_Code = Providers.Code                    " & _
            " Left Join Financial_Definitions_Tree                       " & _
            " On Items_Log.Account_Ser = Financial_Definitions_Tree.Ser      " & _
            " Left Join Cost_Center                                      " & _
            " On Cost_Center.Code = Items_Log.CostCenter_Code                " & _
            " LEFT JOIN Employees                                         " & _
            " ON Employees.Code = Items_Log.Emp_Code               )       " & _
            "                                                             " & _
            " Select * From My_ItemsLog                                      " & _
            " Where 1 = 1                                                " & _
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(31) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(31))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(31))

                'Code
                Txt_Code.Text = Trim(vSqlReader(1))

                'Desc
                Txt_Desc.Text = Trim(vSqlReader(2))

                'Remarks
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader(3))
                Else
                    Txt_Remarks.Text = ""
                End If

                'Price
                If vSqlReader.IsDBNull(4) = False Then
                    Txt_Price.Value = Trim(vSqlReader(4))
                Else
                    Txt_Price.Value = Nothing
                End If

                'BarCode()
                If vSqlReader.IsDBNull(5) = False Then
                    Txt_BarCode.Value = vSqlReader(5)
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
                If vSqlReader.IsDBNull(6) = False Then
                    TXT_ItemCategoryCode.Text = Trim(vSqlReader(6))
                Else
                    TXT_ItemCategoryCode.Text = ""
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

                'Store_Code
                If vSqlReader.IsDBNull(22) = False Then
                    Txt_Store.Value = vSqlReader(22)
                Else
                    Txt_Store.SelectedIndex = -1
                End If

                'CostCenter_Code
                If vSqlReader.IsDBNull(23) = False Then
                    Txt_CostCenter_Code.Text = Trim(vSqlReader(23))
                Else
                    Txt_CostCenter_Code.Text = ""
                End If

                'CostCenter_Desc
                If vSqlReader.IsDBNull(24) = False Then
                    Txt_CostCenter_Desc.Text = Trim(vSqlReader(24))
                Else
                    Txt_CostCenter_Desc.Text = ""
                End If

                'Picture
                If vSqlReader.IsDBNull(25) = False Then
                    Dim arrayImage() As Byte = CType(vSqlReader(25), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)
                    PictureBox1.Image = Image.FromStream(ms)

                Else
                    PictureBox1.Image = PictureBox1.InitialImage
                End If

                '-- Log Details
                'Emp_Desc
                If vSqlReader.IsDBNull(26) = False Then
                    Txt_EmployeeDesc.Text = Trim(vSqlReader(26))
                Else
                    Txt_EmployeeDesc.Text = ""
                End If

                If vSqlReader.IsDBNull(27) = False Then
                    Txt_TDate.Text = Trim(vSqlReader(27))
                Else
                    Txt_TDate.Text = Nothing
                End If

                If vSqlReader.IsDBNull(28) = False Then
                    Txt_ComputerName.Text = Trim(vSqlReader(28))
                Else
                    Txt_ComputerName.Text = ""
                End If

                If vSqlReader.IsDBNull(29) = False Then
                    Txt_IPAddress.Text = Trim(vSqlReader(29))
                Else
                    Txt_IPAddress.Text = ""
                End If

                If vSqlReader.IsDBNull(30) = False Then
                    If vSqlReader(30) = "C" Then
                        Txt_Type.Text = "انشاء"
                    ElseIf vSqlReader(30) = "U" Then
                        Txt_Type.Text = "تعديل"
                    ElseIf vSqlReader(30) = "D" Then
                        Txt_Type.Text = "الغاء"
                    End If
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Txt_Code.ReadOnly = True

            'sQueryBarCode()
            'sQueryProductFormula()
            'sQueryPackUnit()

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
                'sNewRecord()
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
                            " Delete From Items_BarCode " & _
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
                    " Delete From Items_BarCode Where Item_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Items Where Code = '" & Txt_Code.Text & "'" & _
                    " Insert Into Items_Log (           Item_Code,                        DescA,            Type,        Emp_Code,      TDate,             ComputerName,                                                        IPAddress  ) " & _
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
                        'sNewRecord()
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
            'ElseIf pType = "Categories" Then


            'If vLang = "A" Then
            '    Dim Frm_LovTree As New FRM_LovCategoryA
            '    Frm_LovTree.ShowDialog()
            'Else
            '    Dim Frm_LovTree As New FRM_LovCategoryL
            '    Frm_LovTree.ShowDialog()
            'End If

            'TXT_ItemCategoryCode.Tag = vLovReturn3
        ElseIf pType = "Accounts" Then
            vLovReturn1 = ""
            VLovReturn2 = ""
            vLovReturn3 = ""
            vSelectedSortedList_1.Clear()
            Dim Frm_Lov As New FRM_LovGeneral_A(" Select Ser, DescA From Financial_Definitions_Tree Where 1 = 1 " & _
                                                     " And Company_Code = '" & vCompanyCode & "'", "الدليل المحاسبي")
            Frm_Lov.ShowDialog()

            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                Txt_AccountSer.Text = vLovReturn1
                Txt_AccountDesc.Text = VLovReturn2
            End If
        ElseIf pType = "Cost_Center" Then
            vLovReturn1 = ""
            VLovReturn2 = ""
            vLovReturn3 = ""

            Dim Frm_Lov As New Frm_CostCenterTreeA
            Frm_Lov.ShowDialog()

            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                Txt_CostCenter_Code.Text = vLovReturn1
                Txt_CostCenter_Desc.Text = VLovReturn2

                If cControls.fReturnValue(" Select IsNull(UseDepartmentsInItemCode, 'N') From Controls ", Me.Name) = "Y" Then
                    Txt_Code.Text = cControls.fReturnValue(" Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Items Where CostCenter_Code = '" & Trim(Txt_CostCenter_Code.Text) & "' ", Me.Name)

                End If
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

        End If
    End Sub
    Private Sub Tab_Main_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm = Me.ParentForm
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
            sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))

            'ToolBar_Main.Tools("Btn_CopyItems").SharedProps.Visible = False
            'ToolBar_Main.Tools("Btn_ItemMovement").SharedProps.Visible = True
            'ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = True
        Else
            vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, True, True, True, True, "", False, False, "بحث")
            If Grd_Summary.Selected.Rows.Count > 0 Then
                If Grd_Summary.Selected.Rows.Count > 0 Then
                    sQuery(pCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
                Else
                    'sNewRecord()
                End If
            Else
                'sNewRecord()
            End If

            'sSecurity()

            'ToolBar_Main.Tools("Btn_CopyItems").SharedProps.Visible = True
            'ToolBar_Main.Tools("Btn_ItemMovement").SharedProps.Visible = False
            'ToolBar_Main.Tools("Btn_ExportToExcel").SharedProps.Visible = False
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
        "         Cost_Center.DescA as Department_Desc,                       " & _
        "'" & vCompany & "' as Company                                        " & _
        " From Items LEft Join Categories                                    " & _
        " On Categories.Ser = Items.Cat_Ser                                   " & _
        " Left Join Providers                                                 " & _
        " On Providers.Code = Items.Provider_Code                             " & _
        " LEFT JOIN Cost_Center                                               " & _
        " ON Cost_Center.Code = Items.CostCenter_Code                         " & _
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
            vSQlcommand.CommandText = _
            " Select Ctrl_Code, Enabled " & _
            " From   Profiles_Controls INNER JOIN Employees " & _
            " ON     Employees.Profile = Profiles_Controls.Prf_Code " & _
            " Where  Employees.Code = '" & vUsrCode & "'            " & _
            " And    Mod_Code       = 'Items_Def'                   " & _
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
            vSQlcommand.CommandText = _
            " Select Ctrl_Code, Enabled " & _
            " From   Profiles_Controls INNER JOIN Employees " & _
            " ON     Employees.Profile = Profiles_Controls.Prf_Code " & _
            " Where  Employees.Code = '" & vUsrCode & "'            " & _
            " And    Mod_Code       = 'Items_Def'                          " & _
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
                        Txt_Price.ReadOnly = False
                    Else
                        Txt_Price.ReadOnly = True
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

        Dim vNotAllowSalesPriceLowerThanPurchasePrice As String = cControls.fReturnValue(" Select IsNull(NotAllowSalesPriceLowerThanPurchasePrice, 'N') From Controls ", Me.Name)

        If vNotAllowSalesPriceLowerThanPurchasePrice = "Y" Then
            If Not IsDBNull(Txt_Price.Value) And Not IsDBNull(Txt_SPrice.Value) Then
                If Not Txt_Price.Value = Nothing And Not Txt_SPrice.Value = Nothing Then
                    If Txt_Price.Value >= Txt_SPrice.Value Then
                        vcFrmLevel.vParentFrm.sForwardMessage("86", Me)
                        Txt_Price.Select()
                        Return False
                    End If
                End If
            End If
        End If

        'If Not IsDBNull(Txt_Price.Value) Then
        '    If cControls.fCount_Rec(" From Items_PU_SalesTypes Where Item_Code = '" & Trim(Txt_Code.Text) & "'" & _
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
        'If fValidateMain() = False Then
        '    Return
        'End If

        Dim vPrice, vSPrice, vCPrice, vDeduction, vDemandPoint, vCommission, vAccount_Ser, vCostCenter, vProvider_Code, vStore As String
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

        'Account Ser
        If Txt_AccountDesc.Text = "" Then
            vAccount_Ser = "NULL"
        Else
            vAccount_Ser = "'" & Trim(Txt_AccountSer.Tag) & "'"
        End If

        'Cost Center
        If Txt_CostCenter_Desc.Text = "" Then
            vCostCenter = "NULL"
        Else
            vCostCenter = "'" & Trim(Txt_CostCenter_Code.Text) & "'"
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
                'sNewCode()
            End If

            vSqlCommand = " Insert Into Items  (              Code,                          DescA,                             Cat_Ser,                                   PU_Code,                  Provider_Code,           Account_Ser,                   Remarks,                Price,               BarCode,                DemandPoint,              IsActive,                  IsRaw,               SPrice,           CPrice,        Deduction,          Commission,           Company_Code,       Store_Code,       CostCenter_Code )" & _
                                      " Values ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(TXT_ItemCategoryCode.Text) & "', '" & Trim(Txt_PackUnitCode.Text) & "', " & vProvider_Code & ", " & vAccount_Ser & ", '" & Trim(Txt_Remarks.Text) & "'," & vPrice & ", '" & Txt_BarCode.Text & "', " & vDemandPoint & ", '" & Chk_IsActive.Tag & "',  '" & Chk_IsRaw.Tag & "', " & vSPrice & ", " & vCPrice & ", " & vDeduction & ", " & vCommission & ", " & vCompanyCode & ", " & vStore & ", " & vCostCenter & " )"
            sFillSqlStatmentArray(vSqlCommand)

            'Here I will Create the Log File
            vSqlCommand = " Insert Into Items_Log  (        Item_Code,                          DescA,                             Cat_Ser,                                   PU_Code,                  Provider_Code,           Account_Ser,                   Remarks,                Price,               BarCode,                DemandPoint,              IsActive,                  IsRaw,               SPrice,           CPrice,        Deduction,          Commission,           Company_Code,       Store_Code,       CostCenter_Code,      Type,        Emp_Code,         TDate,             ComputerName,                                                        IPAddress    )" & _
                                      "     Values ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(TXT_ItemCategoryCode.Text) & "', '" & Trim(Txt_PackUnitCode.Text) & "', " & vProvider_Code & ", " & vAccount_Ser & ", '" & Trim(Txt_Remarks.Text) & "'," & vPrice & ", '" & Txt_BarCode.Text & "', " & vDemandPoint & ", '" & Chk_IsActive.Tag & "',  '" & Chk_IsRaw.Tag & "', " & vSPrice & ", " & vCPrice & ", " & vDeduction & ", " & vCommission & ", " & vCompanyCode & ", " & vStore & ", " & vCostCenter & ",      'C',   '" & vUsrCode & "',    GetDate(),  '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "') "
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
                          "       Store_Code    =  " & vStore & ", " & _
                          "       CostCenter_Code = " & vCostCenter & _
                          " Where Code          = '" & Txt_Code.Text & "'"
            sFillSqlStatmentArray(vSqlCommand)

            vSqlCommand = " Insert Into Items_Log  (              Item_Code,                     DescA,                             Cat_Ser,                                   PU_Code,                  Provider_Code,           Account_Ser,                   Remarks,                Price,               BarCode,                DemandPoint,              IsActive,                  IsRaw,               SPrice,           CPrice,        Deduction,          Commission,           Company_Code,       Store_Code,       CostCenter_Code,  Type,      Emp_Code,           TDate,             ComputerName,                                                        IPAddress )" & _
                                      "     Values ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', '" & Trim(TXT_ItemCategoryCode.Text) & "', '" & Trim(Txt_PackUnitCode.Text) & "', " & vProvider_Code & ", " & vAccount_Ser & ", '" & Trim(Txt_Remarks.Text) & "'," & vPrice & ", '" & Txt_BarCode.Text & "', " & vDemandPoint & ", '" & Chk_IsActive.Tag & "',  '" & Chk_IsRaw.Tag & "', " & vSPrice & ", " & vCPrice & ", " & vDeduction & ", " & vCommission & ", " & vCompanyCode & ", " & vStore & ", " & vCostCenter & ",   'U',  '" & vUsrCode & "',    GetDate(),  '" & My.Computer.Name & "', '" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString & "')"
            sFillSqlStatmentArray(vSqlCommand)
        End If
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub TXT_All_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) _
    Handles TXT_ItemCategoryCode.EditorButtonClick, Txt_PackUnitCode.EditorButtonClick, _
    Txt_ProviderCode.EditorButtonClick, Txt_AccountSer.EditorButtonClick, _
    Txt_Details_PU.EditorButtonClick, Txt_CostCenter_Code.EditorButtonClick

        Dim vTitle As String

        If sender.readOnly = True Then
            Exit Sub
        End If

        If sender.name = "TXT_ItemCategoryCode" Then
            If vLang = "A" Then
                vTitle = "التصنيفات"
            Else
                vTitle = "Categories"
            End If

            sOpenLov(" Select Ser, DescA From Categories Where 1 = 1 ", vTitle, "Categories", "Categories", " Select Code, DescA From Categories Where Parent_Code = '")
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
        ElseIf sender.name = "Txt_CostCenter_Code" Then
            If vLang = "A" Then
                vTitle = "مراكز التكلفة"
            Else
                vTitle = "Cost Centers"
            End If

            sOpenLov(" Select Code, DescA From Cost_Center Where 1 = 1", vTitle, "Cost_Center")
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

    Private Sub Btn_ItemProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ItemProperties.Click
        If Not Txt_Desc.Text = "" Then
            Dim Frm_ItmPrp As New Frm_ItemProperties(Trim(Txt_Code.Text), "00", Txt_Desc.Text)
            Frm_ItmPrp.ShowDialog()
        Else
            vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
            Txt_Desc.Select()
        End If
    End Sub
    Private Sub Btn_Add_Category_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
        'If Trim(Txt_Desc.Text) <> "" Then
        '    Dim Frm_SalesPrice As New Frm_SalesPrices(Trim(Txt_Code.Text), "", Trim(Txt_Desc.Text))
        '    Frm_SalesPrice.ShowDialog()
        'End If
    End Sub
#End Region
#End Region
#Region " Details                                                                        "


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
            vsqlCommand.CommandText =
            " Select Distinct Items.UserCode,                            " &
            "                 Items.DescA as Item_Desc,                  " &
            "                 Providers.DescA as Provider_Desc,          " &
            "                 Categories.DescA as Cat_Desc,              " &
            "                 Cost_Center.DescA as CostCenter_Desc      " &
            " From Items_Log LEFT JOIN Items                            " &
            " ON   Items_Log.Item_Code = Items.Code                     " &
            " LEFT JOIN Providers                                       " &
            " ON   Providers.Code = Items.Provider_Code                 " &
            " LEFT JOIN Categories                                      " &
            " ON   Categories.Ser = Items.Cat_Ser                       " &
            " LEFT JOIN Cost_Center                                     " &
            " ON   Cost_Center.Code = Items.CostCenter_Code             " &
            " Where 1 = 1                                               " &
            " And Items.Company_Code = " & vCompanyCode &
            vCodeFilter &
            vDescFilter &
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
                    DTS_Summary.Rows(vRowCounter)("Provider") = Trim(vSqlReader(2))
                Else
                    DTS_Summary.Rows(vRowCounter)("Provider") = ""
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("Category") = Trim(vSqlReader(3))
                Else
                    DTS_Summary.Rows(vRowCounter)("Category") = ""
                End If

                'If vSqlReader.IsDBNull(2) = False Then
                '    DTS_Summary.Rows(vRowCounter)("Price") = vSqlReader(2)
                'Else
                '    DTS_Summary.Rows(vRowCounter)("Price") = Nothing
                'End If

                'If vSqlReader.IsDBNull(3) = False Then
                '    DTS_Summary.Rows(vRowCounter)("SPrice") = vSqlReader(3)
                'Else
                '    DTS_Summary.Rows(vRowCounter)("SPrice") = Nothing
                'End If

                'If vSqlReader.IsDBNull(5) = False Then
                '    DTS_Summary.Rows(vRowCounter)("PackUnit") = Trim(vSqlReader(5))
                'Else
                '    DTS_Summary.Rows(vRowCounter)("PackUnit") = ""
                'End If

                'If vSqlReader.IsDBNull(7) = False Then
                '    DTS_Summary.Rows(vRowCounter)("CostCenter_Desc") = Trim(vSqlReader(7))
                'Else
                '    DTS_Summary.Rows(vRowCounter)("CostCenter_Desc") = ""
                'End If

                'If vSqlReader(8) = "N" Then
                '    Grd_Summary.Rows(vRowCounter).Appearance.BackColor2 = Color.White
                '    Grd_Summary.Rows(vRowCounter).Appearance.ForeColor = Color.LightBlue
                'End If
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
    Private Sub sQuerySummaryDetails(ByVal pRow As UltraDataRow, ByVal pChildBand As UltraDataBand, ByVal vGrdRow As UltraGridRow)
        Try
            Dim vChildRows As UltraDataRowsCollection = pRow.GetChildRows(pChildBand)
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = " Select Items_Log.Code,                    " & _
                                      "        Employees.DescA as Emp_Desc, " & _
                                      "        TDate,                   " & _
                                      "        ComputerName,            " & _
                                      "        IPAddress,                " & _
                                      "        Type                     " & _
                                      " From Items_Log INNER JOIN Employees " & _
                                      " ON   Employees.Code = Items_Log.Emp_Code " & _
                                      " Where Item_Code = '" & Trim(vGrdRow.Cells("Code").Text) & "'" & _
                                      " Order By Items_Log.Code       "

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

    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged
        sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
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
    Private Sub Grd_Summary_AfterRowFilterChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs)
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

    Private Sub UltraGrid1_BeforeRowExpanded(sender As Object, e As CancelableRowEventArgs) Handles Grd_Summary.BeforeRowExpanded
        Grd_Summary.UpdateData()

        Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
        Dim vRow As UltraDataRow = DTS_Summary.Rows(e.Row.Index)
        sQuerySummaryDetails(vRow, vChildBand, e.Row)
    End Sub
#End Region
End Class