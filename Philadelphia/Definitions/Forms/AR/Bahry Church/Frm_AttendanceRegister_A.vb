Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinExplorerBar
Imports Infragistics.Win.UltraWinDataSource

Public Class Frm_AttendanceRegister_A
#Region " Declaration                                                                      "
    Dim vSqlStatment(0) As String
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vDataRow As UltraDataRow
    Dim vTableCode, vRM_Code As String
    Dim vPrice, vDeliveryCost, vPayedValue, vRemaining As Decimal
    Dim vMasterBlock As String = "NI"
    Dim vSaveStatus As String
    Dim vCode As String
    Dim vGetShiftNumber As String
    Dim vPrintedNumber As String
    Dim vLineCount As Integer
    Dim vIsDelivery As Boolean
    Dim vSettings As Settings
    Dim vFocus As String
    Dim vSortedList As New SortedList
    Dim vJor_Code As String
#End Region

#Region " Form Level                                                                       "
#Region " My Form                                                                          "
    Public Sub New(ByVal pTableCode As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        vTableCode = pTableCode
    End Sub

    Public Sub New(ByVal pTableCode As String, ByVal pRM_Code As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        vTableCode = pTableCode
        vRM_Code = "'" & pRM_Code & "'"

        vMasterBlock = "N"
        sQuerySalesItems()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Frm_DoctorsDefinitions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        vcFrmLevel.vParentFrm = Me.ParentForm

        'sLoadSettings()
        Try

            Txt_FndByCode.Appearance.BackColor = Color.SeaShell
            Txt_FndByDesc.Appearance.BackColor = Color.SeaShell

            Txt_EmpDesc.Text = vUsrName
            Txt_TDate.Value = DateTime.Now
            TXT_FromSummaryDate.Value = Now
            Txt_ToSummaryDate.Value = Now
            'Txt_CostCenter_Code.Value = cControls.fReturnValue(" Select CostCenter_Code From Employees Where Code = '" & vUsrCode & "' ", Me.Name)
            'Txt_CostCenter_Desc.Value = cControls.fReturnValue(" Select DescA From Cost_Center Where Code = '" & Txt_CostCenter_Code.Value & "' ", Me.Name)

            'sLoadLogo()
            vMasterBlock = "NI"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Frm_ElGohary_POS3_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm

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

    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'If fSaveAll(True) = False Then
        '    e.Cancel = True
        'Else
        '    e.Cancel = False
        '    vcFrmLevel.vParentFrm.sPrintRec("")
        'End If

        'sSaveSettings()
    End Sub
    Private Sub ToolBar_Main_ToolClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case e.Tool.Key
            Case "ItemsCount"
                sItemsCount()
        End Select
    End Sub
    Private Sub sLoadLogo()
        'Here I Load the Logo Picture of the Company
        Dim vSQlcommand As New SqlCommand
        vSQlcommand.CommandText = " Select Picture From Company "

        vSQlcommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
        Do While vSqlReader.Read
            Dim arrayImage() As Byte = CType(vSqlReader(0), Byte())
            Dim ms As New IO.MemoryStream(arrayImage)
            PictureBox1.Image = Image.FromStream(ms)

            Dim scale_factor As Single = 0.45

            ' Get the source bitmap.
            Dim bm_source As New Bitmap(PictureBox1.Image)

            ' Make a bitmap for the result.
            Dim bm_dest As New Bitmap( _
                CInt(bm_source.Width * scale_factor), _
                CInt(bm_source.Height * scale_factor))

            ' Make a Graphics object for the result Bitmap.
            Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)

            ' Copy the source image into the destination bitmap.
            gr_dest.DrawImage(bm_source, 0, 0, _
                bm_dest.Width + 1, _
                bm_dest.Height + 1)

            ' Display the result.
            PictureBox1.Image = bm_dest
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub
#End Region
#Region " Navigation                                                                       "
#Region " Explorer Bar                                                                     "
#Region " Load                                                                             "
    Private Sub sLoadExplorer()
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Frequently_Used.Ser, " & _
            "        Code, " & _
            "        IsNull(DescA, 'غير معرف') " & _
            " From Frequently_Used Inner Join Categories " & _
            " On Frequently_Used.Act_Code = Categories.Code " & _
            " Order By Frequently_Used.Ser "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Exp_Categories.Groups.Clear()
            Exp_Categories.Groups.Add("FU", "مفضل")

            Do While vSqlReader.Read
                'If vLang = "A" Then
                Dim vItem As Object = Exp_Categories.Groups("FU").Items.Add(vSqlReader(1), vSqlReader(2))
                vItem.Tag = vSqlReader(0)
                'Else
                'Dim vItem As Object = Exp_Categories.Groups("FU").Items.Add(vSqlReader(1), vSqlReader(3))
                'vItem.Tag = vSqlReader(0)
                'End If
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

        'Try
        '    Dim vsqlCommand As New SqlCommand
        '    vsqlCommand.CommandText = _
        '    " Select Ser,                   " & _
        '    "        Code,                  " & _
        '    "        IsNull(DescA, 'غير معرف')     " & _
        '    " From  Categories              " & _
        '    " Where Parent_Code is NULL "


        '    vsqlCommand.Connection = cControls.vSqlConn
        '    cControls.vSqlConn.Open()
        '    Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
        '    'Exp_Categories.Groups.Clear()
        '    Exp_Categories.Groups.Add("0", "الأساسي")
        '    'Here I Will add the "All" Node first.. so that the user can search in all items
        '    Dim vAll_Node As Object = Exp_Categories.Groups("0").Items.Add("All", "الكل")
        '    vAll_Node.tag = "All"

        '    Do While vSqlReader.Read
        '        'If vLang = "A" Then
        '        Dim vItem As Object = Exp_Categories.Groups("0").Items.Add(vSqlReader(1), vSqlReader(2))
        '        vItem.Tag = vSqlReader(0)
        '        'Else
        '        'Dim vItem As Object = Exp_Categories.Groups("0").Items.Add(vSqlReader(1), vSqlReader(3))
        '        'vItem.Tag = vSqlReader(0)
        '        'End If

        '    Loop
        '    cControls.vSqlConn.Close()
        '    vSqlReader.Close()

        '    For Each vItem As UltraExplorerBarItem In Exp_Categories.Groups("0").Items
        '        If cControls.fCount_Rec(" From Categories Where Parent_Code = '" & vItem.Key & "'") > 0 Then
        '            vItem.Settings.AppearancesSmall.Appearance.BackColor = Color.White
        '            vItem.Settings.AppearancesSmall.Appearance.BackColor2 = Color.Wheat
        '            vItem.Settings.AppearancesSmall.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        '            vItem.ToolTipText = fLoadToolTip(vItem.Key)
        '            'vItem.Tag = "P"
        '        Else
        '            'vItem.Tag = "C"
        '        End If
        '    Next
        '    Exp_Categories.Groups("0").Selected = True
        'Catch ex As Exception
        '    cControls.vSqlConn.Close()
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub
    Private Sub sLoadItemsinGroup(ByVal pGroup As UltraExplorerBarGroup)
        'Exp_Categories.Groups.Add(pGroup)
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Categories.Ser,        " & _
            "        Code,                  " & _
            "        IsNull(DescA, 'غير معرف') " & _
            "        From Frequently_Used Inner Join Categories " & _
            " On Frequently_Used.Act_Code = Categories.Code " & _
            " Order By Frequently_Used.Ser "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Exp_Categories.Groups(pGroup.Index).Items.Clear()
            Do While vSqlReader.Read
                Dim vItem As UltraExplorerBarItem
                'If vLang = "A" Then
                vItem = Exp_Categories.Groups(pGroup.Key).Items.Add(vSqlReader(1), vSqlReader(2))
                'Else
                'vItem = Exp_Categories.Groups(pGroup.Key).Items.Add(vSqlReader(1), vSqlReader(3))
                'End If
                'vItem.Tag = "C"
                vItem.Tag = vSqlReader(0)
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
        'Exp_Categories.SelectedGroup = pGroup
    End Sub
    Private Function fLoadToolTip(ByVal vItemKey As String) As String
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select IsNull(DescA, 'غير معرف')     " & _
            " From Categories       " & _
            " Where Parent_Code = '" & vItemKey & "'" & _
            " Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vToolTip As String
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Do While vSqlReader.Read
                If vToolTip <> "" Then
                    vToolTip += vbCrLf
                End If
                'If vLang = "A" Then
                vToolTip += vSqlReader(0)
                'Else
                'vToolTip += vSqlReader(1)
                'End If
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Return vToolTip
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Function
#End Region
#Region " Navigation                                                                       "
    Private Sub Exp_Categories_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Exp_Categories.CheckedItem Is Nothing Then
                If cControls.fCount_Rec(" From Categories Where Parent_Code = '" & Exp_Categories.CheckedItem.Key & "'") = 0 Then
                    vLovReturn1 = Trim(Exp_Categories.CheckedItem.Key)
                    VLovReturn2 = Trim(Exp_Categories.CheckedItem.Text)
                    vLovReturn3 = Trim(Exp_Categories.CheckedItem.Tag)
                    'Me.Close()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Exp_Categories_GroupClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinExplorerBar.GroupEventArgs)
        If e.Group.Key = "FU" Then
            For x As Int16 = Exp_Categories.Groups.Count - 1 To 2 Step -1
                If Exp_Categories.Groups(x).Index > e.Group.Index Then
                    If Exp_Categories.Groups(x).Index <> e.Group.Index Then
                        Exp_Categories.Groups.RemoveAt(x)
                    End If
                End If
            Next
        Else
            For x As Int16 = Exp_Categories.Groups.Count - 1 To 0 Step -1
                If Exp_Categories.Groups(x).Index > e.Group.Index Then
                    If Exp_Categories.Groups(x).Index <> e.Group.Index Then
                        Exp_Categories.Groups.RemoveAt(x)
                    End If
                End If
            Next
        End If
        'Txt_AdjustCode.Clear()
        'Txt_AdjustDesc.Clear()
        'Opt_Adjust_Type.CheckedIndex = -1
        'Opt_Adjust_Effect.CheckedIndex = -1
        'Txt_FirstBalance.Value = Nothing
        'Opt_FB_type.CheckedIndex = -1
        If e.Group.Key = "FU" Then
            sLoadItemsinGroup(Exp_Categories.SelectedGroup)
        End If
        'Exp_Categories.Groups.Remove(vGroup)
    End Sub
    Private Sub Exp_Categories_ItemCheckStateChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinExplorerBar.ItemEventArgs)
        If Not Exp_Categories.CheckedItem Is Nothing Then
            For Each vGroup As UltraExplorerBarGroup In Exp_Categories.Groups
                If vGroup.Key = Exp_Categories.CheckedItem.Key Then
                    Exit Sub
                End If
            Next
            If cControls.fCount_Rec(" From Categories Where Parent_Code = '" & Exp_Categories.CheckedItem.Key & "'") > 0 Then
                'vGroup = Exp_Categories.Groups.
                Dim vGroup As UltraExplorerBarGroup = Exp_Categories.Groups.Add(Exp_Categories.CheckedItem.Key, Exp_Categories.CheckedItem.Text)
                Try
                    Dim vsqlCommand As New SqlCommand
                    vsqlCommand.CommandText = _
                    " Select Ser, Code, IsNull(DescA, 'غير معرف') From Categories " & _
                    " Where Parent_Code = '" & e.Item.Key & "'" & _
                    " Order By Code "

                    vsqlCommand.Connection = cControls.vSqlConn
                    cControls.vSqlConn.Open()
                    Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader

                    Do While vSqlReader.Read
                        'If vLang = "A" Then
                        Dim vItem As Object = Exp_Categories.Groups(e.Item.Key).Items.Add(vSqlReader(1), vSqlReader(2))
                        vItem.Tag = vSqlReader(0)
                        'Else
                        'Dim vItem As Object = Exp_Categories.Groups(e.Item.Key).Items.Add(vSqlReader(1), vSqlReader(3))
                        'vItem.Tag = vSqlReader(0)
                        'End If
                    Loop
                    cControls.vSqlConn.Close()
                    vSqlReader.Close()
                    For Each vItem As UltraExplorerBarItem In Exp_Categories.Groups(e.Item.Key).Items
                        If cControls.fCount_Rec(" From Categories Where Parent_Code = '" & vItem.Key & "'") > 0 Then
                            vItem.Settings.AppearancesSmall.Appearance.BackColor = Color.White
                            vItem.Settings.AppearancesSmall.Appearance.BackColor2 = Color.Wheat
                            vItem.Settings.AppearancesSmall.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
                            vItem.ToolTipText = fLoadToolTip(vItem.Key)
                            'vItem.Tag = "P"
                        Else
                            'vItem.Tag = "C"
                        End If
                    Next
                Catch ex As Exception
                    cControls.vSqlConn.Close()
                    MessageBox.Show(ex.Message)
                End Try
                vGroup.Selected = True
            Else
                sQueryItemsInSelectedCategory_ByCode(Exp_Categories.CheckedItem.Tag)
            End If
            If Exp_Categories.CheckedItem.Group.Key = "FU" Then
                'Btn_Add.Visible = False
                Btn_RemoveFromFavorite.Visible = True
            Else
                'Btn_Add.Visible = True
                Btn_RemoveFromFavorite.Visible = False
            End If
        End If
    End Sub
#End Region
#End Region
#Region " Buttons                                                                          "

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        If Grd_Sales.Rows.Count > 0 Then
            sEmptySqlStatmentArray()
            Dim vSqlString As String
            vSqlString = " Delete From Resturant_Meals_Details Where RM_Code = " & vRM_Code
            sFillSqlStatmentArray(vSqlString)

            vSqlString = " Delete From Resturant_Meals Where Code = " & vRM_Code
            sFillSqlStatmentArray(vSqlString)

            vSqlString = "Update Tables Set IsBusy = 'N', RM_Code = ''  Where Code = " & vTableCode
            sFillSqlStatmentArray(vSqlString)

            cControls.fSendData(vSqlStatment, Me.Name)
            Me.Close()
        Else
            Me.Close()
        End If

    End Sub

    Private Sub Btn_AddToFavorite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_AddToFavorite.Click
        If Not Exp_Categories.CheckedItem Is Nothing Then
            'If Exp_Categories.CheckedItem.Tag = "C" Then
            If Exp_Categories.CheckedItem.Group.Key <> "FU" Then
                If cControls.fCount_Rec(" From Frequently_Used Where Act_Code = '" & Exp_Categories.CheckedItem.Key & "'") > 0 Then
                    If vLang = "L" Then
                        MessageBox.Show(" Account is already add, Select another Item ")
                    Else
                        MessageBox.Show("هذا الحساب قد تمت أضافته من قبل")
                    End If
                Else
                    Dim vSqlString As String
                    vSqlString = " Insert Into Frequently_Used ( Act_Code ) " & _
                                 " Values                      ( '" & Exp_Categories.CheckedItem.Key & "')"
                    If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                        If vLang = "A" Then
                            MessageBox.Show("الحساب " & Exp_Categories.CheckedItem.Text & " أضيف بنجاح ", "أضافة", MessageBoxButtons.OK)
                        Else
                            MessageBox.Show("Item " & Exp_Categories.CheckedItem.Text & " Successfully added ", "Add", MessageBoxButtons.OK)
                        End If
                    End If
                End If
            Else
                If vLang = "A" Then
                    MessageBox.Show("هذا الحساب قد تمت أضافته من قبل")
                Else
                    MessageBox.Show(" Account is already add, Select another Item ")
                End If
            End If
            'Else
            '    MessageBox.Show(" You can't add this Account because it's contain another Accounts ")
            'End If
        Else
            If vLang = "A" Then
                MessageBox.Show(" أختر الحساب أولاً")
            Else
                MessageBox.Show(" Select Account First ")
            End If
        End If
    End Sub

    Private Sub Btn_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_RemoveFromFavorite.Click
        If Not Exp_Categories.CheckedItem Is Nothing Then
            Dim vSqlString As String
            vSqlString = " Delete From Frequently_Used Where Act_Code = '" & Trim(Exp_Categories.CheckedItem.Key) & "'"

            If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                Exp_Categories.Groups("FU").Items.RemoveAt(Exp_Categories.CheckedItem.Index)
            End If
        End If
    End Sub
#End Region
#Region " Grid                                                                             " 
    Private Sub Grd_Sales_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles Grd_Sales.AfterRowInsert
        Dim vCount As Integer
        For Each vRow As UltraGridRow In Grd_Sales.Rows
            If vRow.Cells("Item_Desc").Text <> "" Then
                vCount += 1
            End If
        Next
        Txt_RowCount.Text = vCount
    End Sub
    Private Sub Grd_Sales_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Sales.CellChange
        Try
            Grd_Sales.UpdateData()

            If sender.ActiveRow.Cells("DML").Value = "NI" Then
                sender.ActiveRow.Cells("DML").Value = "I"
            ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
                sender.ActiveRow.Cells("DML").Value = "U"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
#End Region
#Region " Tab Management                                                                   "
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
                vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "التفاصيل")
                sQuerySummaryMain()
            Else
                vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
                If Grd_Summary.Selected.Rows.Count > 0 Then
                    If Grd_Summary.ActiveRow.Band.Index = 0 Then
                        'sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
                        'Else
                        sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
                    End If
                Else
                    sNewRecord()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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
#Region " DataBase                                                                         "
#Region " Query                                                                            "
    Private Sub sLoadBoxes()
        'Txt_Boxes.Items.Clear()

        'Dim vBox_Code As String = cControls.fReturnValue(" Select Box_Code From Employees Where Code = '" & vUsrCode & "' ", Me.Name)

        'If vBox_Code <> "" Then
        '    Dim vBox_Desc As String = cControls.fReturnValue(" Select DescA From Boxes Where Code = '" & vBox_Code & "' ", Me.Name)

        '    Txt_Boxes.Items.Add(vBox_Code, vBox_Desc)

        '    Txt_Boxes.SelectedIndex = 0

        '    Return
        'End If

        'Try
        '    Dim vRowCounter As Integer
        '    Dim vsqlCommand As New SqlCommand
        '    vsqlCommand.CommandText = _
        '    " Select Code, DescA  From Boxes "

        '    vsqlCommand.Connection = cControls.vSqlConn
        '    cControls.vSqlConn.Open()
        '    Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader

        '    Do While vSqlReader.Read
        '        Txt_Boxes.Items.Add(vSqlReader(0), vSqlReader(1))
        '    Loop
        '    cControls.vSqlConn.Close()
        '    vSqlReader.Close()
        'Catch ex As Exception
        '    cControls.vSqlConn.Close()
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub
    Private Sub sLoadSalesTypes()
        Txt_SalesTypes.Items.Clear()

        Dim vSalesType_Code As String = cControls.fReturnValue(" Select DealType From Employees Where Code = '" & vUsrCode & "' ", Me.Name)

        If vSalesType_Code <> "" Then
            Dim vSalesType_Desc As String = cControls.fReturnValue(" Select DescA From Sales_Types Where Code = '" & vSalesType_Code & "' ", Me.Name)

            Txt_SalesTypes.Items.Add(vSalesType_Code, vSalesType_Desc)

            Txt_SalesTypes.SelectedIndex = 0

            Return
        End If

        'Try
        '    Dim vsqlCommand As New SqlCommand
        '    vsqlCommand.CommandText = _
        '    " Select Code, DescA From Sales_Types " & _
        '    " Order By Code "

        '    vsqlCommand.Connection = cControls.vSqlConn
        '    cControls.vSqlConn.Open()
        '    Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
        '    Txt_SalesTypes.Items.Clear()

        '    Do While vSqlReader.Read
        '        Txt_SalesTypes.Items.Add(vSqlReader(0), vSqlReader(1))
        '    Loop
        '    cControls.vSqlConn.Close()
        'Catch ex As Exception
        '    cControls.vSqlConn.Close()
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub
    Private Sub sLoadStores()
        'Txt_Stores.Items.Clear()

        'Dim vStore_Code As String = cControls.fReturnValue(" Select Store_Code From Employees Where Code = '" & vUsrCode & "' ", Me.Name)

        'If vStore_Code <> "" Then
        '    Dim vStore_Desc As String = cControls.fReturnValue(" Select DescA From Stores Where Code = '" & vStore_Code & "' ", Me.Name)

        '    Txt_Stores.Items.Add(vStore_Code, vStore_Desc)

        '    Txt_Stores.SelectedIndex = 0

        '    Return
        'End If

        'Try
        '    Dim vRowCounter As Integer
        '    Dim vsqlCommand As New SqlCommand
        '    vsqlCommand.CommandText = _
        '    " Select Code, DescA  From Stores "

        '    vsqlCommand.Connection = cControls.vSqlConn
        '    cControls.vSqlConn.Open()
        '    Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader

        '    Do While vSqlReader.Read
        '        Txt_Stores.Items.Add(vSqlReader(0), vSqlReader(1))
        '    Loop
        '    cControls.vSqlConn.Close()
        '    vSqlReader.Close()
        'Catch ex As Exception
        '    cControls.vSqlConn.Close()
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub
    Private Sub sLoadCostCenteres()
        'Txt_CostCenteres.Items.Clear()

        'Dim vCostCenter_Code As String = cControls.fReturnValue(" Select CostCenter_Code From Employees Where Code = '" & vUsrCode & "' ", Me.Name)

        'If vCostCenter_Code <> "" Then
        '    Dim vCostCenter_Desc As String = cControls.fReturnValue(" Select DescA From Cost_Center Where Code = '" & vCostCenter_Code & "' ", Me.Name)

        '    Txt_CostCenteres.Items.Add(vCostCenter_Code, vCostCenter_Desc)

        '    Txt_CostCenteres.SelectedIndex = 0

        '    Return
        'End If

        'Try
        '    Dim vRowCounter As Integer
        '    Dim vsqlCommand As New SqlCommand
        '    vsqlCommand.CommandText = _
        '    " Select Code, DescA  From Cost_Center "

        '    vsqlCommand.Connection = cControls.vSqlConn
        '    cControls.vSqlConn.Open()
        '    Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader

        '    Do While vSqlReader.Read
        '        Txt_CostCenteres.Items.Add(vSqlReader(0), vSqlReader(1))
        '    Loop
        '    cControls.vSqlConn.Close()
        '    vSqlReader.Close()
        'Catch ex As Exception
        '    cControls.vSqlConn.Close()
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Sub sQueryItemsInSelectedCategory_ByCode(ByVal pItemName As String)
        Try
            Dim vCat_Ser, vItem_BarCode As String
            If pItemName = "" Then
                vCat_Ser = ""
            ElseIf pItemName = "All" Then
                vCat_Ser = ""
            Else
                vCat_Ser = " And   Cat_Ser = '" & Trim(pItemName) & "'"
            End If

            If Trim(Txt_FndByCode.Text).Length > 0 Then
                'First I Check If BarCode is Directly Found in Item Definision
                Dim vItem_Code As String
                If cControls.fIsExist(" From Items_BarCode Where BarCode = '" & Trim(Txt_FndByCode.Text) & "' ", Me.Name) = True Then

                    vItem_Code = cControls.fReturnValue(" Select Code From Items " & _
                                                        " INNER JOIN Items_BarCode " & _
                                                        " ON Items.Code = Items_BarCode.Item_Code " & _
                                                        " Where Items_BarCode.BarCode = '" & Trim(Txt_FndByCode.Text) & "' ", Me.Name)

                    If fCheckIf_Item_Exist_in_SalesList(Trim(vItem_Code)) Then
                        Return
                    End If

                    If Not fCheck_If_SalesPrice_NotZero(Trim(vItem_Code)) Then
                        Return
                    End If

                    vDataRow = DTS_Attendance.Rows.Add

                    vDataRow("Item_Code") = vItem_Code
                    vDataRow("Item_Desc") = cControls.fReturnValue(" Select DescA From Items Where Code = '" & vItem_Code & "' ", Me.Name)
                    vDataRow("Quantity") = 1
                    vDataRow("SPrice") = cControls.fReturnValue(" Select IsNull(SPrice, 0) From Items Where Code = '" & vItem_Code & "'", Me.Name)
                    'vDataRow("PayedCash") = cControls.fReturnValue(" Select IsNull(PayedCash, 0) From Items Where Code = '" & Grd_Items.ActiveRow.Cells("Item_Code").Text & "'", Me.Name)
                    vDataRow("DML") = "I"
                    Grd_Sales.Rows(vDataRow.Index).Cells("Quantity").Selected = True
                    Grd_Sales.Rows(vDataRow.Index).Cells("Quantity").Activate()
                    Grd_Sales.PerformAction(UltraGridAction.EnterEditMode)

                    Txt_RowCount.Text = Grd_Sales.Rows.Count
                    Txt_FndByCode.Clear()
                    Txt_FndByCode.Select()
                    Return

                ElseIf cControls.fIsExist("From Members " & _
                            " Where Code = '" & Trim(Txt_FndByCode.Text) & "' ", Me.Name) = True Then

                    If fCheckIf_Item_Exist_in_SalesList(Trim(Txt_FndByCode.Text)) Then
                        Return
                    End If

                    vDataRow = DTS_Attendance.Rows.Add

                    vDataRow("Item_Code") = Trim(Txt_FndByCode.Text)
                    vDataRow("Item_Desc") = cControls.fReturnValue(" Select DescA From Members Where Code = '" & Trim(Txt_FndByCode.Text) & "' ", Me.Name)
                    vDataRow("Time") = DateTime.Now

                    'vDataRow("PayedCash") = cControls.fReturnValue(" Select IsNull(PayedCash, 0) From Items Where Code = '" & Grd_Items.ActiveRow.Cells("Item_Code").Text & "'", Me.Name)
                    vDataRow("DML") = "I"

                    Txt_RowCount.Text = Grd_Sales.Rows.Count
                    Txt_FndByCode.Clear()
                    Txt_FndByCode.Select()
                    Return

                    'Second I Check If this Code Come From Mezan
                ElseIf Trim(Txt_FndByCode.Text).Length = 13 Then
                    'First I get the Code From the Mezan BarCode
                    vItem_BarCode = Trim(Txt_FndByCode.Text).Substring(1, 6)

                    If cControls.fIsExist(" From Items_BarCode Where BarCode = '" & vItem_BarCode & "' ", Me.Name) = True Then

                        vDataRow = DTS_Attendance.Rows.Add
                        vItem_Code = cControls.fReturnValue(" Select Code From Items " & _
                                                            " INNER JOIN Items_BarCode " & _
                                                            " ON Items.Code = Items_BarCode.Item_Code " & _
                                                            " Where Items_BarCode.BarCode = '" & vItem_BarCode & "' ", Me.Name)
                        vDataRow("Item_Code") = vItem_Code
                        vDataRow("Item_Desc") = cControls.fReturnValue(" Select DescA From Items Where Code = '" & vItem_Code & "' ", Me.Name)

                        'Here I Get the Quantity From the Mezan BarCode
                        Dim vLength As Integer = Trim(Txt_FndByCode.Text).Length
                        Dim vQuantity As String = Trim(Txt_FndByCode.Text).Substring(vLength - 6, 5)
                        vDataRow("Quantity") = vQuantity / 1000
                        vDataRow("SPrice") = cControls.fReturnValue(" Select IsNull(SPrice, 0) From Items Where Code = '" & vItem_Code & "'", Me.Name)
                        'vDataRow("PayedCash") = cControls.fReturnValue(" Select IsNull(PayedCash, 0) From Items Where Code = '" & Grd_Items.ActiveRow.Cells("Item_Code").Text & "'", Me.Name)
                        vDataRow("DML") = "I"
                        Grd_Sales.Rows(vDataRow.Index).Cells("Quantity").Selected = True
                        Grd_Sales.Rows(vDataRow.Index).Cells("Quantity").Activate()
                        Grd_Sales.PerformAction(UltraGridAction.EnterEditMode)

                        Txt_RowCount.Text = Grd_Sales.Rows.Count

                        Txt_FndByCode.Clear()
                        Txt_FndByCode.Select()
                        Return
                    End If
                End If
                Return

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub sQueryItemsInSelectedCategory_ByDesc(ByVal pItemName As String)
        Try
            Dim vItem As Infragistics.Win.ValueListItem
            Dim vSqlCommand As New SqlCommand
            Dim vRowCounter As Integer

            vSqlCommand.Connection = cControls.vSqlConn
            vSqlCommand.CommandText = _
            " Select Top 100  Code,              " & _
            "        IsNull(DescA, 'غير معرف') " & _
            " From   Members " & _
            " Where  1 = 1 " & _
            " And   DescA Like '%" & Trim(Txt_FndByDesc.Text) & "%'"


            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSqlCommand.ExecuteReader

            vRowCounter = 0
            DTS_Items.Rows.Clear()
            While vSqlReader.Read
                DTS_Items.Rows.SetCount(vRowCounter + 1)
                DTS_Items.Rows(vRowCounter)("Item_Code") = Trim(vSqlReader(0))
                DTS_Items.Rows(vRowCounter)("DescA") = Trim(vSqlReader(1))

                vRowCounter += 1
            End While
            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub sQuerySalesItems()
        Try
            Dim vSqlCommand As New SqlCommand
            Dim vRowCounter As Integer

            vSqlCommand.Connection = cControls.vSqlConn
            vSqlCommand.CommandText = _
            " Select Attendance_Details.Ser,       " & _
            "        Member_Code,                         " & _
            "        Members.DescA as Member_Desc,          " & _
            "        Att_Time,                       " & _
            "        IsNull(Half_Attendance, 'N')    " & _
            " From   Attendance_Details INNER JOIN Members   " & _
            " On     Members.Code = Attendance_Details.Member_Code  " & _
            " Where  ATT_Code = " & Trim(Txt_Code.Text)

            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSqlCommand.ExecuteReader

            vRowCounter = 0
            DTS_Attendance.Rows.Clear()
            While vSqlReader.Read
                DTS_Attendance.Rows.SetCount(vRowCounter + 1)
                DTS_Attendance.Rows(vRowCounter)("Ser") = vSqlReader(0)
                DTS_Attendance.Rows(vRowCounter)("Item_Code") = Trim(vSqlReader(1))
                DTS_Attendance.Rows(vRowCounter)("Item_Desc") = Trim(vSqlReader(2))
                DTS_Attendance.Rows(vRowCounter)("Time") = Trim(vSqlReader(3))

                If vSqlReader(4) = "Y" Then
                    DTS_Attendance.Rows(vRowCounter)("Half_Attendance") = True
                Else
                    DTS_Attendance.Rows(vRowCounter)("Half_Attendance") = False
                End If

                DTS_Attendance.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            End While
            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function fCheckIf_Item_Exist_in_SalesList(ByVal pItem_Code As String) As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Sales.Rows
            If vRow.Cells("Item_Code").Text = pItem_Code Then

                Txt_FndByCode.Clear()
                Txt_FndByCode.Select()
                Return True
            End If
        Next

        Return False
    End Function

    Private Function fCheck_If_SalesPrice_NotZero(ByVal pItem_Code As String) As Boolean
        Dim vReturn As String = cControls.fReturnValue(" Select SPrice From Items " & _
                               " Where Code = '" & Trim(pItem_Code) & "' ", Me.Name)

        If vReturn = "" Then
            Return False
        Else
            Return True
        End If

        Return False
    End Function
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pItemCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)
        If fSaveAll(True) = False Then
            Return
        End If

        'sLockInvoice(False)

        Dim vFetchRec As Integer
        If pItemCode = Nothing Then
            If pIsGoTo = False Then
                If pRecPos = Nothing Then
                    vFetchRec = 1
                Else
                    vFetchRec = vcFrmLevel.vRecPos + pRecPos
                    If vFetchRec > cControls.fCount_Rec(" From Attendance ") Then

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
            vFetchRec = cControls.fCount_Rec(" From Attendance ")
        End If

        Dim vFetchCondition As String
        If pItemCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And MyAttendance.Code = '" & Trim(pItemCode) & "'"
        End If

        Try
            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText = _
            " With MyAttendance as                               " & vbCrLf & _
            "( Select Attendance.Code,                           " & _
            "         Attendance.DescA as Att_Desc,              " & _
            "         Employees.DescA as Emp_Desc,               " & _
            "         TDate,                                      " & _
            "         ROW_Number() Over (Order By Attendance.Code) as  RecPos " & vbCrLf & _
            " From Attendance Inner Join Employees                         " & vbCrLf & _
            " On Attendance.Emp_Code = Employees.Code                      " & vbCrLf & _
            "                                                                     " & vbCrLf & _
            " Where 1 = 1                                     )                   " & vbCrLf & _
            " Select * From MyAttendance                                      " & vbCrLf & _
            " Where 1 = 1                                                         " & vbCrLf & _
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(4) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(4))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(4))

                'Code
                Txt_Code.Text = Trim(vSqlReader(0))

                'Desc
                If vSqlReader.IsDBNull(1) = False Then
                    Txt_Desc.Text = Trim(vSqlReader(1))
                Else
                    Txt_Desc.Text = ""
                End If

                'Emp_Desc
                If vSqlReader.IsDBNull(2) = False Then
                    Txt_EmpDesc.Text = Trim(vSqlReader(2))
                Else
                    Txt_EmpDesc.Text = ""
                End If

                'TDate
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_TDate.Text = Trim(vSqlReader(3))
                Else
                    Txt_TDate.Text = Nothing
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            sQuerySalesItems()

            Txt_RowCount.Text = Grd_Sales.Rows.Count

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
    End Sub
#End Region
#Region " Save                                                                             "
    Private Function fIfsaveNeeded() As Boolean
        'If vSaveStatus = "Save" Then
        If vMasterBlock = "I" Or vMasterBlock = "U" Then
            Return True
        End If

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Sales.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next
        Return False
        'ElseIf vSaveStatus = "Finish" Then
        'If Grd_Sales.Rows.Count > 0 Then
        '    Return True
        'End If
        'End If

    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean

        If fIfsaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        If pAskMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                If fValidate_Master() Then
                    If Not fSave_Master() Then
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                DTS_Attendance.Rows.Clear()
                Return True
            End If
        Else
            If fValidate_Master() Then
                If Not fSave_Master() Then
                    Return False
                End If
            Else
                Return False
            End If
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        'If vSaveStatus = "Finish" Then
        If vRowCounter > 0 Then

            'sPrint_Receipt()

            sSetFlagsUpdate()
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            Return True
        End If
        'ElseIf vSaveStatus = "Save" Then
        'If vRowCounter > 0 Then
        '    sSetFlagsUpdate()
        'End If
        'End If

    End Function
    Private Sub sSetFlagsUpdate()
        'sNewRecord()

        vMasterBlock = "N"
        sQuerySalesItems()

        'sNewCode()
        'Txt_TotalCash.Text = 0
        Txt_RowCount.Text = 0
        'sNewRecord()

        'If vSaveStatus = "Save" Then
        '    Dim vSqlString As String = " Update Tables Set IsBusy = 'Y', RM_Code = " & vRM_Code & " Where Code = '" & vTableCode & "'"

        '    cControls.fSendData(vSqlString, Me.Name)
        '    Me.Close()
        'ElseIf vSaveStatus = "Finish" Then

        '    cControls.fSendData("Update Tables Set IsBusy = 'N', RM_Code = NULL  Where Code = '" & vTableCode & "'", Me.Name)
        '    Me.Close()
        'End If
    End Sub

    Private Function fValidate_Master() As Boolean
        If Txt_Desc.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
            Txt_Desc.Select()
            Return False
        End If

        Return True
    End Function
    Private Function fSave_Master() As Boolean
        Dim vDate As String

        If Not Txt_TDate.Value = Nothing Then
            vDate = "'" & Format(Txt_TDate.Value, "MM-dd-yyyy HH:mm") & "'"
        Else
            vDate = "NULL"
        End If

        Dim vDebit_Ser, vCredit_Ser As String
        Dim vSqlString As String
        'vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From  Attendance "
        'vCode = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(6, "0")

        'If Not IsNumeric(vCode) Then
        '    MessageBox.Show("خطأ في الاتصال بالشبكة")
        '    Exit Function
        'End If

        If vMasterBlock = "I" Then
            vSqlString = " Insert Into Attendance (          Code,                          DescA,               TDate,          Emp_Code) " & _
                     "                Values  ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', " & vDate & ", '" & vUsrCode & "')"

            sFillSqlStatmentArray(vSqlString)
        ElseIf vMasterBlock = "U" Then
            vSqlString = " Update Attendance        " & _
                         " Set    DescA = '" & Trim(Txt_Desc.Text) & "', " & _
                         "        TDate =  " & vDate & _
                         " Where  Code  = '" & Trim(Txt_Code.Text) & "' "

            sFillSqlStatmentArray(vSqlString)
        End If

        If fValidateDetails() Then
            sSave_Details(vCode)
            Return True
        Else
            Return False
        End If
    End Function

    Private Function fValidateDetails() As Boolean
        Try
            If Grd_Sales.Rows.Count = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("52", Me)
                Return False
            End If

            Dim vRow As UltraGridRow
            For Each vRow In Grd_Sales.Rows

                'If cControls.fReturnValue(" Select Act_Ser From Items_SalesTypes Where Item_Code = '" & vRow.Cells("Item_Code").Text & "' And ST_Code = " & Txt_SalesTypes.Value, Me.Name) = "" Then
                '    vcFrmLevel.vParentFrm.sForwardMessage("68", Me)
                '    vRow.Cells("Item_Desc").Selected = True
                '    Return False
                'End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return True
    End Function
    Private Sub sSave_Details(ByVal pCode As String)
        Dim vSqlString, vGetSerial, vHalf_Attendance As String
        Dim vCounter As Int16
        Dim vRow As UltraGridRow

        For Each vRow In Grd_Sales.Rows
            If vRow.Cells("Half_Attendance").Value = True Then
                vHalf_Attendance = "'Y'"
            Else
                vHalf_Attendance = "'N'"
            End If

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Attendance_Details " & _
                             " Where Att_Code = " & Trim(Txt_Code.Text)

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Attendance_Details (          Att_Code,                  Ser,                         Member_Code,                            Att_Time,                 Half_Attendance )" & _
                             "                         Values ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & vRow.Cells("Item_Code").Text & "', '" & vRow.Cells("Time").Value & "', " & vHalf_Attendance & ")"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update Attendance_Details " & _
                             " Set   Half_Attendance = " & vHalf_Attendance & _
                             " Where Att_Code  = '" & Trim(Txt_Code.Text) & "'" & _
                             " And   Ser      = " & vRow.Cells("Ser").Value

                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Delete                                                                           "
    Public Sub sDelete()
        If vMasterBlock = "NI" Or vMasterBlock = "I" Then
            sNewRecord(False)
        Else
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                Dim vSqlString As String
                vSqlString = " Delete From Attendance_Details Where Att_Code = '" & Trim(Txt_Code.Text) & "' " & _
                             " Delete From Attendance Where Code = '" & Trim(Txt_Code.Text) & "' "

                If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                    sNewRecord(False)
                End If
            End If
        End If
    End Sub
#End Region
#End Region
#Region " New Record                                                                       "
    Public Sub sNewRecord(Optional ByVal pAskForSave As Boolean = True)

        If pAskForSave Then
            If fSaveAll(True) = False Then
                Return
            End If
        End If
        
        'Tab_Main.Tabs("Tab_Details").Selected = True

        sNewCode()
        'Txt_TDate.Value = Now
        'Txt_Desc.Text = "فاتورة بيع رقم  " & Txt_Code.Text
        'Txt_EmpCode.Text = vUsrCode
        Txt_EmpDesc.Text = vUsrName
        Txt_Desc.Text = ""
        Txt_TDate.Value = Now

        Txt_RowCount.Text = 0

        'vBox_Code = cControls.fReturnValue(" Select Box_Code From Employees Where Code = '" & Trim(Txt_EmpCode.Text) & "'", Me.Name)

        vMasterBlock = "NI"
        DTS_Attendance.Rows.Clear()

        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")

        Tab_Main.Tabs("Tab_Details").Selected = True
    End Sub
    Private Sub sNewCode()
        Dim vSqlString As String
        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From  Attendance "
        Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(6, "0")

    End Sub
#End Region
#Region " Print                                                                            "
    Public Sub sPrint_Receipt()
        Try
            'If Txt_Payed.Text = "" Then
            '    MessageBox.Show("أدخل المبلغ المدفوع", "خطأ")
            '    Return
            'End If
            'If CDec(Txt_Remaining.Text) < 0 Then
            '    MessageBox.Show("لا يمكن أن يكون المبلغ المدفوع أقل من أجمالي المبلغ", "خطأ")
            '    Return
            'End If


            'If fValidateSubmit() = False Then
            '    'Return
            '    vcFrmLevel.vParentFrm.sForwardMessage("40", Me)
            'End If
            Dim UltraPreview As New Infragistics.Win.Printing.UltraPrintPreviewDialog
            UltraPreview.Document = PrintDocument1
            'UltraPreview.Show()
            'PrintDocument1.PrinterSettings.PrinterName = "Printer1"
            PrintDocument1.Print()

            'PrintDocument1.PrinterSettings.PrinterName = "Printer2"
            'PrintDocument1.Print()

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        'LD 23-1-2009
        'Header & footer Variables
        Try
            Dim vResult As String
            vResult = cControls.fCount_Rec(" From  POS " & _
                                           " Where ShiftNum_Code = " & vGetShiftNumber & _
                                           " And Emp_Code = '" & vUsrCode & "' ")

            If vResult = 0 Then
                vPrintedNumber = 1
            Else
                vPrintedNumber = vResult + 1
            End If

            'vPrintedNumber = vCode
            Dim vBranch As String = cControls.fReturnValue(" Select DescA From Company ", Me.Name)
            Dim vSpaceCalc1 As Integer
            Dim vSpaceCalc2 As Integer
            Dim vDate As DateTime = System.DateTime.Now()
            Dim vDateStr As String = Format(vDate, "d/M/yyyy H:mm")
            Dim vCashier As String = "01"    'Trim(Mdl_Generals.vCashID)

            Dim vCustData(0) As String
            Dim vCusTel As String = "0124264564"   'Trim(Mdl_Generals.vTel)
            Dim vCusAdd As String = "49 Omar Lotfy St." 'Trim(Mdl_Generals.vAddress)

            Dim vSumTotal As Decimal

            Dim vStrBld As New System.Text.StringBuilder("")

            'vStrBld.AppendLine()
            'vStrBld.AppendLine()
            'vLineCount += 20

            'Header part


            vSpaceCalc1 = 23 - Trim(vBranch).Length
            If vSpaceCalc1 < 0 Then
                vBranch = vBranch.Substring(0, 31)
                vSpaceCalc1 = 0
            End If
            vSpaceCalc2 = 6 - Trim(vPrintedNumber).Length
            vStrBld.Append(Space(vSpaceCalc2) & vBranch & Space(vSpaceCalc1) & Space(2) & "رقم:" & vPrintedNumber)
            vStrBld.AppendLine()
            'vLineCount += 20

            vSpaceCalc1 = 3 - vCashier.Length
            vSpaceCalc2 = 20 - Format(vDate, "d/M/yyyy H:mm").Length
            If vSpaceCalc2 < 0 Then
                vDate = vDate.ToString.Substring(0, 20)
                vSpaceCalc2 = 0
            End If
            Dim vEmployeeName As String = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & vUsrCode & "'", Me.Name)

            vStrBld.Append(vEmployeeName & Space(vSpaceCalc1) & Space(vSpaceCalc2) & Space(1) & "التاريخ:" & vDateStr)
            vStrBld.AppendLine()


            vStrBld.Append("========================================")
            vStrBld.AppendLine()
            vStrBld.Append("الكمية" & Space(3) & "الصنف" & Space(20) & "السعر")
            vStrBld.AppendLine()
            vStrBld.Append("----------------------------------------")
            vStrBld.AppendLine()
            'vLineCount += 20
            'Body Variables
            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
            Dim vQty As String
            Dim vSupport As String
            Dim vPayed As String
            Dim vItem As String
            Dim vTotal As String
            Dim vPKU As String
            Dim vSpaceCalc3 As Integer
            Dim vSpaceCalc4 As Integer
            Dim vSpaceCalc5 As Integer
            Dim vItemString(0) As String
            Dim vFrstIndex As Integer
            Dim vLstIndex As Integer
            Dim vLine As String
            Dim vItemSpcAllow As Integer = 24
            Dim vPKUSpcAllow As Integer = 0
            'vLineCount += (12 * GRD_Orders.Rows.Count())

            'Body Part
            Grd_Sales.UpdateData()

            For Each vRow In Grd_Sales.Rows
                If vRow.Cells("Item_Desc").Text = "" Then
                    Continue For
                End If
                'vCount -= 1
                If vIsDelivery Then
                    If vRow.Index = 0 Then
                        Continue For
                    End If
                End If

                vQty = Format(Math.Round(vRow.Cells("Quantity").Value, 2), "###0.00")
                'vPrice = Format(Math.Round(vRow.Cells("Price").Value, 2), "###0.00")
                vPKU = vRow.Cells("PackUnit").Text.Trim()
                vItem = vRow.Cells("Item_Desc").Text.Trim()
                vTotal = Format(Math.Round(vRow.Cells("Total_SPrice").Value, 2), "###0.00")
                vSpaceCalc2 = 6 - Trim(vQty).Length
                vSpaceCalc4 = 7 - Trim(vTotal).Length

                If vItem.Length > vItemSpcAllow Then
                    vItem = vItem.Substring(0, vItemSpcAllow)
                End If
                If vPKU.Length > vPKUSpcAllow Then
                    vPKU = vPKU.Substring(0, vPKUSpcAllow)
                End If
                vSpaceCalc1 = vItemSpcAllow - vItem.Length
                vSpaceCalc3 = vPKUSpcAllow - vPKU.Length
                vLine = vPKU & Space(vSpaceCalc3) & Space(0) & vItem & Space(vSpaceCalc1) & Space(vSpaceCalc4) & vTotal & " x" & Space(2) & vQty & vbCrLf
                '---------------------------------
                'LD 1/9/2008
                'I add spaces according to the count of "لا" because these two letters take the 
                'space of only one character
                If vLine.Contains("لا") Then
                    vItemString(0) = vLine
                    ' vMatch = Strings.Filter(vItemString, "x", False, CompareMethod.Text)
                    While True
                        vSpaceCalc1 += 1
                        vFrstIndex = vItemString(0).IndexOf("لا")
                        vLstIndex = vItemString(0).LastIndexOf("لا")
                        If vFrstIndex = vLstIndex Then
                            Exit While
                        Else
                            vItemString(0) = vLine.Substring(0, vLstIndex - 2)
                            Continue While
                        End If
                    End While
                    vLine = vPKU & Space(vSpaceCalc3) & Space(1) & vItem & Space(vSpaceCalc1) & " " & Space(vSpaceCalc4) & vTotal & " x" & Space(vSpaceCalc2) & vQty & vbCrLf
                End If
                '------------------------------------
                vStrBld.Append(vLine)
            Next
            vStrBld.Append("========================================")
            vStrBld.AppendLine()

            'Footer Part
            Dim vPayedTotal, vSupportTotal, vElectronicServices, vSellerServices, vWinter, vAboveSupport, vTotal_Of_All As Decimal
            'If vIsDelivery Then
            '    vLineCount += 26
            'End If

            vSpaceCalc1 = 28 - Trim(vPayedTotal).Length
            'vSpaceCalc2 = 10 - Trim(vSupportTotal).Length

            vStrBld.Append("المجموع" & Space(3) & Space(vSpaceCalc1) & vPayedTotal)
            vStrBld.AppendLine()
            'vLineCount += 10

            'If Txt_Deduction.Text <> "" Then
            '    vDecTotal = Txt_Deduction.Text
            '    vDecTotal = Format(vDecTotal, "#####0.00")
            '    vDecTotal = vDecTotal
            '    vSpaceCalc1 = 8 - Trim(vDecTotal).Length
            '    vStrBld.Append(" % الخصم" & Space(22) & Space(vSpaceCalc1) & vDecTotal)
            '    vStrBld.AppendLine()
            'End If

            'If Txt_Deduction.Text <> "" Then
            '    vDecTotal = fSumTotalAfterDeduction() - vDeliveryCost
            '    vDecTotal = Format(vDecTotal, "#####0.00")
            '    vSpaceCalc1 = 8 - Trim(vDecTotal).Length
            '    vStrBld.Append("بعد الخصم" & Space(22) & Space(vSpaceCalc1) & vDecTotal)
            '    vStrBld.AppendLine()
            'End If

            vStrBld.Append("                                        ")
            vStrBld.AppendLine()

            'vLineCount += 10

            'If Txt_Payed.Text <> "" Then
            '    vPayedValue = CDec(Txt_Payed.Text)
            '    vPayedValue = Format(vPayedValue, "#####0.00")
            '    vSpaceCalc1 = 10 - Trim(vPayedValue).Length
            '    vStrBld.Append("المدفوع" & Space(22) & Space(vSpaceCalc1) & vPayedValue)
            '    vStrBld.AppendLine()
            '    'vLineCount += 10
            'End If

            'If Txt_Remaining.Text <> "" Then
            '    vRemaining = CDec(Txt_Remaining.Text)
            '    vRemaining = Format(vRemaining, "#####0.00")
            '    vSpaceCalc1 = 11 - Trim(vPayedValue).Length
            '    vStrBld.Append("الباقى" & Space(23) & Space(vSpaceCalc1) & vRemaining)
            '    vStrBld.AppendLine()
            '    vStrBld.AppendLine()
            '    'vLineCount += 30
            'End If

            vStrBld.Append(fGetFooter())
            vStrBld.AppendLine()
            vStrBld.Append("")
            vStrBld.AppendLine()
            'vLineCount += 20
            ' e.Graphics.DrawImage(ImageList1.Images(0), 135, 10)
            ' Dim vBrush As System.Drawing.Brushes
            'Dim vprintFont As New System.Drawing.Font("Simplified Arabic Fixed", 8, System.Drawing.FontStyle.Regular)
            'e.Graphics.DrawString("بيت ", vprintFont, System.Drawing.Brushes.Black, 5, 50)
            'Dim vSQlcommand As New SqlCommand
            'vSQlcommand.CommandText = " Select Picture From Company "
            'vSQlcommand.Connection = cControls.vSqlConn
            'cControls.vSqlConn.Open()
            'Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader

            'Dim vImage_X, vImage_Y As Integer
            'Do While vSqlReader.Read
            '    If vSqlReader.IsDBNull(0) = False Then
            '        Dim arrayImage() As Byte = CType(vSqlReader(0), Byte())
            '        Dim ms As New IO.MemoryStream(arrayImage)
            '        e.Graphics.DrawImage(Image.FromStream(ms), 3, 1)
            '        vImage_X = Image.FromStream(ms).Height
            '        vImage_Y = Image.FromStream(ms).Width
            '    End If
            'Loop

            Dim vprintFont As New System.Drawing.Font("Simplified Arabic Fixed", 8, System.Drawing.FontStyle.Regular)
            e.Graphics.DrawImage(PictureBox1.Image, 3, 1)
            e.Graphics.DrawString(vStrBld.ToString(), vprintFont, System.Drawing.Brushes.Black, 3, PictureBox1.Image.Height + 15)

            'Dim vRect As System.Drawing.Rectangle

            'e.Graphics.FillRectangle(Brushes.Red, New Rectangle(500, 500, 500, 500))
            'e.Graphics.RotateTransform(270)
            'Dim vprintFont1 As New System.Drawing.Font("Tahoma", 6, System.Drawing.FontStyle.Regular)
            'e.Graphics.DrawString("Powered by Xlab", vprintFont1, Brushes.Blue, 10, vLineCount)
            'vLineCount = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "PrintDocument1_PrintPage")
        End Try

    End Sub

    Private Function fArrangeLA(ByVal pLine As String, ByVal pSpace1 As Integer) As Integer
        'LD 1/9/2008
        'I add spaces according to the count of "لا" because these two letters take the 
        'space of only one character
        Try
            Dim vItemString(0) As String
            Dim vFrstIndex As Integer
            Dim vLstIndex As Integer
            If pLine.Contains("لا") Then
                vItemString(0) = pLine
                ' vMatch = Strings.Filter(vItemString, "x", False, CompareMethod.Text)
                While True
                    pSpace1 += 1
                    vFrstIndex = vItemString(0).IndexOf("لا")
                    vLstIndex = vItemString(0).LastIndexOf("لا")
                    If vFrstIndex = vLstIndex Then
                        Exit While
                    Else
                        vItemString(0) = pLine.Substring(0, vLstIndex - 2)
                        Continue While
                    End If
                End While
            End If
            If pSpace1 < 0 Then
                pSpace1 = 0
            End If
            Return pSpace1
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "fArrangeLA")
        End Try

    End Function
    Private Function fArrangeSpc(ByVal pLine As String, ByVal pSpace1 As Integer) As Integer

        Try
            Dim vItemString(0) As String
            Dim vFrstIndex As Integer
            Dim vLstIndex As Integer
            If pLine.Contains(" ") Then
                vItemString(0) = pLine
                While True
                    pSpace1 -= 1
                    vFrstIndex = vItemString(0).IndexOf(" ")
                    vLstIndex = vItemString(0).LastIndexOf(" ")
                    If vFrstIndex = vLstIndex Then
                        Exit While
                    Else
                        vItemString(0) = pLine.Substring(0, vLstIndex - 2)
                        Continue While
                    End If
                End While
            End If
            If pSpace1 < 0 Then
                pSpace1 = 0
            End If
            Return pSpace1
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "fArrangeSpc")
        End Try

    End Function
    Public Function fArrangeNewLine(ByVal pstring As String, ByVal pSpcAlwd As Integer) As String()
        Try
            Dim vret(0) As String
            Dim vStr(0) As String
            vret(0) = pstring
            Dim vdelimeter As String = " "
            Dim vline As String = ""
            Dim i As Integer = 0
            Dim vRowcount As Integer = 0
            Dim vStrCount As Integer
            Dim vActStrCount As Integer
            Dim vString As String
            Dim vForStr() As String
            Dim vForstring As String
            vStr = pstring.Split(vdelimeter)
            For Each vString In vStr
                If vString.Length = 0 Then
                    Continue For
                End If
                vStrCount += 1
            Next
            If pstring.Length > pSpcAlwd Then
                While True
                    vStr = pstring.Split(vdelimeter)
                    While True
                        If i = vStr.Length Then
                            Exit While
                        End If
                        If vStr(i).Length = 0 Then
                            i += 1
                            Continue While
                        End If

                        vline += " " + vStr(i)
                        If vline.Length <= pSpcAlwd Then
                            vret(vRowcount) = vline
                            If vStr.Length = i Then
                                Return vret
                            End If
                            i += 1
                            Continue While
                        Else
                            vline = ""
                            i = 0
                            Exit While
                        End If

                    End While

                    For Each vString In vret
                        vForStr = vString.Split(vdelimeter)
                        For Each vForstring In vForStr
                            If vForstring.Length = 0 Then
                                Continue For
                            End If
                            vActStrCount += 1
                        Next
                    Next
                    If vStrCount > vActStrCount Then
                        vActStrCount = 0
                        ReDim Preserve vret(UBound(vret) + 1)
                        vret(UBound(vret)) = pstring.Substring(vret(vRowcount).Length)
                        If vret(UBound(vret)).Length > pSpcAlwd Then
                            vRowcount += 1
                            pstring = vret(UBound(vret))
                            Continue While
                        Else
                            Exit While
                        End If
                    Else
                        Exit While
                    End If

                End While
            Else
                vret(0) = pstring
            End If
            vLineCount += vret.Length * 10
            Return vret

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "fArrangeNewLine")
        End Try

    End Function
    Private Function fGetHeader() As String
        Dim vHeader(0) As String
        Dim vreturn As String = ""
        Dim vStr As String
        Dim vSpcAlld As Integer
        Dim vSpcAlld1 As Integer
        Dim vSpcAlld2 As Integer
        vHeader(0) = "" 'MDL_DBase.fReturnValue("Select Header from  Control ", Me.Name)
        vHeader = fArrangeNewLine(Trim(vHeader(0)), 17)
        For Each vStr In vHeader
            vSpcAlld = 17 - vStr.Length
            vSpcAlld1 = vSpcAlld / 2
            vSpcAlld2 = vSpcAlld - vSpcAlld1
            vreturn += Space(10) & Space(vSpcAlld1) & vStr & Space(vSpcAlld2) & Space(10) & vbCrLf
        Next
        'vLineCount += 50
        Return vreturn
    End Function
    Private Function fGetFooter() As String
        Dim vFooter(0) As String
        Dim vreturn As String = ""
        Dim vStr As String
        Dim vSpcAlld As Integer
        Dim vSpcAlld1 As Integer
        Dim vSpcAlld2 As Integer
        vFooter(0) = " شكراً لزيارتكم" 'cControls.fReturnValue("Select Footer from  Control ", Me.Name) 

        vFooter = fArrangeNewLine(Trim(vFooter(0)), 30)

        For Each vStr In vFooter
            vSpcAlld = 30 - vStr.Length
            vSpcAlld1 = vSpcAlld / 2
            vSpcAlld2 = vSpcAlld - vSpcAlld1
            vreturn += Space(2) & Space(vSpcAlld1) & vStr & Space(vSpcAlld2) & Space(6) & vbCrLf
        Next
        Return vreturn

    End Function

    Public Function fReturnStringWithoutEnglish(ByVal pMsg As String) As String
        Dim ch As Char
        Dim vCounter As Integer = 0
        For Each ch In pMsg
            Dim ch1 As Integer = CInt(AscW(ch))
            If ch1 < &H621 Or ch1 > &H64A Then
                If ch1 = 32 Or ch1 = 49 Or ch1 = 50 Or ch1 = 51 Or ch1 = 52 Or ch1 = 53 Or ch1 = 54 Or ch1 = 55 Or ch1 = 56 Or ch1 = 57 Then
                    vCounter += 1
                Else
                    pMsg = pMsg.Remove(vCounter, 1)
                End If
            Else
                vCounter += 1
            End If
        Next
        Return pMsg
    End Function

    Private Sub sPrint_FinalShiftReport()
        Try
            'If Txt_Payed.Text = "" Then
            '    MessageBox.Show("أدخل المبلغ المدفوع", "خطأ")
            '    Return
            'End If
            'If CDec(Txt_Remaining.Text) < 0 Then
            '    MessageBox.Show("لا يمكن أن يكون المبلغ المدفوع أقل من أجمالي المبلغ", "خطأ")
            '    Return
            'End If


            'If fValidateSubmit() = False Then
            '    'Return
            '    vcFrmLevel.vParentFrm.sForwardMessage("40", Me)
            'End If
            Dim UltraPreview As New Infragistics.Win.Printing.UltraPrintPreviewDialog
            UltraPreview.Document = PrintDocument2
            'UltraPreview.Show()
            'PrintDocument2.PrinterSettings.PrinterName = "Printer1"
            PrintDocument2.Print()

            'PrintDocument1.PrinterSettings.PrinterName = "Printer2"
            'PrintDocument1.Print()

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub PrintDocument2_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage

        'LD 23-1-2009
        'Header & footer Variables
        Try

            Dim vSqlString, vGetShiftNumber As String

            vSqlString = " Select IsNull(Max(Ser), 1) From Shift_Numbers " & _
                         " Where Emp_Code = '" & vUsrCode & "'"
            vGetShiftNumber = cControls.fReturnValue(vSqlString, Me.Name)

            Dim vBranch As String = cControls.fReturnValue(" Select DescA From Company ", Me.Name)
            Dim vSpaceCalc1 As Integer
            Dim vSpaceCalc2 As Integer
            Dim vDate As DateTime = System.DateTime.Now()
            Dim vDateStr As String = Format(vDate, "d/M/yyyy H:mm")
            Dim vCashier As String = "01"    'Trim(Mdl_Generals.vCashID)

            Dim vCustData(0) As String
            Dim vCusTel As String = "0124264564"   'Trim(Mdl_Generals.vTel)
            Dim vCusAdd As String = "49 Omar Lotfy St." 'Trim(Mdl_Generals.vAddress)

            Dim vSumTotal As Decimal

            Dim vStrBld As New System.Text.StringBuilder("")

            'vStrBld.AppendLine()
            'vStrBld.AppendLine()
            'vLineCount += 20

            'Header part
            vSpaceCalc1 = 17 - Trim(vBranch).Length
            If vSpaceCalc1 < 0 Then
                vBranch = vBranch.Substring(0, 17)
                vSpaceCalc1 = 0
            End If
            vSpaceCalc2 = 6 - Trim(vPrintedNumber).Length
            vStrBld.Append(Space(vSpaceCalc2) & vBranch & Space(vSpaceCalc1) & Space(2) & "رقم:" & vPrintedNumber)
            vStrBld.AppendLine()
            'vLineCount += 20

            vSpaceCalc1 = 3 - vCashier.Length
            vSpaceCalc2 = 17 - Format(vDate, "d/M/yyyy H:mm").Length
            If vSpaceCalc2 < 0 Then
                vDate = vDate.ToString.Substring(0, 20)
                vSpaceCalc2 = 0
            End If
            Dim vEmployeeName As String = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & vUsrCode & "'", Me.Name)

            vStrBld.Append(vEmployeeName & Space(vSpaceCalc1) & Space(vSpaceCalc2) & Space(1) & "التاريخ:" & vDateStr)
            vStrBld.AppendLine()


            vStrBld.Append("========================================")
            vStrBld.AppendLine()
            vStrBld.Append("الكمية" & Space(3) & "الصنف" & Space(12) & "السعر")
            vStrBld.AppendLine()
            vStrBld.Append("----------------------------------------")
            vStrBld.AppendLine()
            'vLineCount += 20
            'Body Variables
            'Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow
            Dim vQty As String
            Dim vSupport As String
            Dim vTotal_Items_LCost As String
            Dim vItem As String
            Dim vTotal As String
            Dim vPKU As String
            Dim vSpaceCalc3 As Integer
            Dim vSpaceCalc4 As Integer
            Dim vSpaceCalc5 As Integer
            Dim vItemString(0) As String
            Dim vFrstIndex As Integer
            Dim vLstIndex As Integer
            Dim vLine As String
            Dim vItemSpcAllow As Integer = 14
            Dim vPKUSpcAllow As Integer = 0
            'vLineCount += (12 * GRD_Orders.Rows.Count())

            'Body Part
            Grd_Sales.UpdateData()

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Mov_Item_Stores.Item_Code,                             " & _
            "        Items.DescA as Item_Desc,                              " & _
            "        IsNull(Sum(QtyOut), 0) as Item_Balance,                            " & _
            "        IsNull(Sum(LCost), 0) as LCost_Total                              " & _
            " From   Mov_Item_Stores INNER JOIN Items                       " & _
            " ON     Items.Code = Mov_Item_Stores.Item_Code                 " & _
            " INNER  JOIN POS                                            " & _
            " ON     POS.Code = Mov_Item_Stores.POS_Code                  " & _
            "                                                           " & _
            " Where  POS.ShiftNum_Code = " & vGetShiftNumber & _
            " And    POS.Emp_Code = '" & vUsrCode & "'                       " & _
            " Group  BY Mov_Item_Stores.Item_Code, Items.DescA              "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

            'For Each vRow In Grd_Sales.Rows
            Do While vSqlReader.Read
                'If vRow.Cells("Item_Desc").Text = "" Then
                '    Continue For
                'End If
                'vCount -= 1
                'If vIsDelivery Then
                '    If vRow.Index = 0 Then
                '        Continue For
                '    End If
                'End If

                vQty = Format(Math.Round(vSqlReader(2), 2), "###0.00")
                'vPrice = Format(Math.Round(vRow.Cells("Price").Value, 2), "###0.00")
                vPKU = ""
                vItem = Trim(vSqlReader(1))
                vTotal = Format(Math.Round(vSqlReader(3), 2), "###0.00")
                vSpaceCalc2 = 6 - Trim(vQty).Length
                vSpaceCalc4 = 7 - Trim(vTotal).Length

                If vItem.Length > vItemSpcAllow Then
                    vItem = vItem.Substring(0, vItemSpcAllow)
                End If

                If vPKU.Length > vPKUSpcAllow Then
                    vPKU = vPKU.Substring(0, vPKUSpcAllow)
                End If

                vSpaceCalc1 = vItemSpcAllow - vItem.Length

                vSpaceCalc3 = vPKUSpcAllow - vPKU.Length
                vLine = vPKU & Space(vSpaceCalc3) & Space(0) & vItem & Space(vSpaceCalc1) & Space(vSpaceCalc4) & vTotal & " x" & Space(2) & vQty & vbCrLf
                '---------------------------------
                'LD 1/9/2008
                'I add spaces according to the count of "لا" because these two letters take the 
                'space of only one character
                If vLine.Contains("لا") Then
                    vItemString(0) = vLine
                    ' vMatch = Strings.Filter(vItemString, "x", False, CompareMethod.Text)
                    While True
                        vSpaceCalc1 += 1
                        vFrstIndex = vItemString(0).IndexOf("لا")
                        vLstIndex = vItemString(0).LastIndexOf("لا")
                        If vFrstIndex = vLstIndex Then
                            Exit While
                        Else
                            vItemString(0) = vLine.Substring(0, vLstIndex - 2)
                            Continue While
                        End If
                    End While
                    vLine = vPKU & Space(vSpaceCalc3) & Space(1) & vItem & Space(vSpaceCalc1) & " " & Space(vSpaceCalc4) & vTotal & " x" & Space(vSpaceCalc2) & vQty & vbCrLf
                End If
                '------------------------------------
                vStrBld.Append(vLine)

                vTotal_Items_LCost += vSqlReader(3)
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            vStrBld.Append("========================================")
            vStrBld.AppendLine()

            'Footer Part
            Dim vPayedTotal, vSupportTotal, vElectronicServices, vSellerServices, vWinter, vAboveSupport, vTotal_Of_All As Decimal
            'If vIsDelivery Then
            '    vLineCount += 26
            'End If
            vPayedTotal = vTotal_Items_LCost
            vPayedTotal = Format(vPayedTotal, "#####0.00")

            vSpaceCalc1 = 14 - Trim(vPayedTotal).Length
            'vSpaceCalc2 = 10 - Trim(vSupportTotal).Length

            vStrBld.Append("مجموع المبيعات" & Space(3) & Space(vSpaceCalc1) & vTotal_Items_LCost)
            vStrBld.AppendLine()
            'vLineCount += 10

            vStrBld.Append("========================================")
            vStrBld.AppendLine()

            vStrBld.Append("                                        ")
            vStrBld.AppendLine()

            vStrBld.Append("المصروفات" & Space(22))
            vStrBld.AppendLine()

            vStrBld.Append("========================================")
            vStrBld.AppendLine()

            vsqlCommand.CommandText = _
            " Select Expenses.DescA,                                    " & _
            "        Mov_Cash_Stores.Cash_Out                              " & _
            " From   Mov_Cash_Stores INNER JOIN Expense_Invoices                       " & _
            " ON     Expense_Invoices.Code = Mov_Cash_Stores.EX_Code     " & _
            " INNER  JOIN Expenses                                       " & _
            " ON     Expenses.Code = Expense_Invoices.Expense_Code       " & _
            " Where  Expense_Invoices.ShiftNum_Code = " & vGetShiftNumber


            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            vSqlReader = vsqlCommand.ExecuteReader

            Dim vTotal_Expenses As String

            Do While vSqlReader.Read
                'If vRow.Cells("Item_Desc").Text = "" Then
                '    Continue For
                'End If
                'vCount -= 1
                'If vIsDelivery Then
                '    If vRow.Index = 0 Then
                '        Continue For
                '    End If
                'End If

                vQty = Format(Math.Round(1, 2), "###0.00")
                'vPrice = Format(Math.Round(vRow.Cells("Price").Value, 2), "###0.00")
                vPKU = ""
                vItem = Trim(vSqlReader(0))
                vTotal = Format(Math.Round(vSqlReader(1), 2), "###0.00")
                vSpaceCalc2 = 6 - Trim(vQty).Length
                vSpaceCalc4 = 7 - Trim(vTotal).Length

                If vItem.Length > vItemSpcAllow Then
                    vItem = vItem.Substring(0, vItemSpcAllow)
                End If
                If vPKU.Length > vPKUSpcAllow Then
                    vPKU = vPKU.Substring(0, vPKUSpcAllow)
                End If
                vSpaceCalc1 = vItemSpcAllow - vItem.Length
                vSpaceCalc3 = vPKUSpcAllow - vPKU.Length
                vLine = vPKU & Space(vSpaceCalc3) & Space(0) & vItem & Space(vSpaceCalc1) & Space(vSpaceCalc4) & vTotal & " x" & Space(2) & vQty & vbCrLf
                '---------------------------------
                'LD 1/9/2008
                'I add spaces according to the count of "لا" because these two letters take the 
                'space of only one character
                If vLine.Contains("لا") Then
                    vItemString(0) = vLine
                    ' vMatch = Strings.Filter(vItemString, "x", False, CompareMethod.Text)
                    While True
                        vSpaceCalc1 += 1
                        vFrstIndex = vItemString(0).IndexOf("لا")
                        vLstIndex = vItemString(0).LastIndexOf("لا")
                        If vFrstIndex = vLstIndex Then
                            Exit While
                        Else
                            vItemString(0) = vLine.Substring(0, vLstIndex - 2)
                            Continue While
                        End If
                    End While
                    vLine = vPKU & Space(vSpaceCalc3) & Space(1) & vItem & Space(vSpaceCalc1) & " " & Space(vSpaceCalc4) & vTotal & " x" & Space(vSpaceCalc2) & vQty & vbCrLf
                End If
                '------------------------------------
                vStrBld.Append(vLine)

                vTotal_Expenses += vSqlReader(1)
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            vStrBld.Append("========================================")
            vStrBld.AppendLine()

            vStrBld.Append("مجموع المصروفات" & Space(8) & Space(vSpaceCalc1) & vTotal_Expenses)
            vStrBld.AppendLine()
            'vLineCount += 10

            vStrBld.Append("========================================")
            vStrBld.AppendLine()

            Dim vNet As String
            vNet = vTotal_Items_LCost - vTotal_Expenses

            vStrBld.Append("الصافي" & Space(10) & Space(vSpaceCalc1) & vNet)
            vStrBld.AppendLine()

            vStrBld.Append(fGetFooter())
            vStrBld.AppendLine()
            vStrBld.Append("")
            vStrBld.AppendLine()
            'vLineCount += 20
            ' e.Graphics.DrawImage(ImageList1.Images(0), 135, 10)
            ' Dim vBrush As System.Drawing.Brushes
            'Dim vprintFont As New System.Drawing.Font("Simplified Arabic Fixed", 8, System.Drawing.FontStyle.Regular)
            'e.Graphics.DrawString("بيت ", vprintFont, System.Drawing.Brushes.Black, 5, 50)
            'Dim vSQlcommand As New SqlCommand
            'vSQlcommand.CommandText = " Select Picture From Company "
            'vSQlcommand.Connection = cControls.vSqlConn
            'cControls.vSqlConn.Open()
            'Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader

            'Dim vImage_X, vImage_Y As Integer
            'Do While vSqlReader.Read
            '    If vSqlReader.IsDBNull(0) = False Then
            '        Dim arrayImage() As Byte = CType(vSqlReader(0), Byte())
            '        Dim ms As New IO.MemoryStream(arrayImage)
            '        e.Graphics.DrawImage(Image.FromStream(ms), 3, 1)
            '        vImage_X = Image.FromStream(ms).Height
            '        vImage_Y = Image.FromStream(ms).Width
            '    End If
            'Loop

            Dim vprintFont As New System.Drawing.Font("Simplified Arabic Fixed", 10, System.Drawing.FontStyle.Regular)
            e.Graphics.DrawString(vStrBld.ToString(), vprintFont, System.Drawing.Brushes.Black, 3, 1)

            'Dim vRect As System.Drawing.Rectangle

            'e.Graphics.FillRectangle(Brushes.Red, New Rectangle(500, 500, 500, 500))
            'e.Graphics.RotateTransform(270)
            'Dim vprintFont1 As New System.Drawing.Font("Tahoma", 6, System.Drawing.FontStyle.Regular)
            'e.Graphics.DrawString("Powered by Xlab", vprintFont1, Brushes.Blue, 10, vLineCount)
            'vLineCount = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "PrintDocument1_PrintPage")
        End Try

    End Sub
#End Region
#Region " Reports                                                                          "
    Private Sub sItemsCount()
        Try
            vSortedList.Clear()

            Dim vSqlString As String

            vSqlString = _
            " Select Mov_Item_Stores.Item_Code,                             " & _
            "        Items.DescA as Item_Desc,                              " & _
            "        Sum(QtyOut) as Balance                                 " & _
            " From   Mov_Item_Stores INNER JOIN Items                       " & _
            " ON     Items.Code = Mov_Item_Stores.Item_Code                 " & _
            " INNER  JOIN POS                                            " & _
            " ON     POS.Code = Mov_Item_Stores.POS_Code                  " & _
            " Where  POS.ShiftNum_Code = " & vGetShiftNumber & _
            " Group  BY Mov_Item_Stores.Item_Code, Items.DescA              "

            vSortedList.Add("DT_ItemMovement", vSqlString)

            Dim vRep_Preview As New FRM_ReportPreviewL("تقرير استهلاك الأصناف اليوم", vSortedList, New DS_ItemMovements, New Rep_DailyItems_Movement)
            vRep_Preview.MdiParent = Me.MdiParent
            vRep_Preview.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region
#End Region

#Region " Summary                                                                           "
    Private Sub sQuerySummaryMain()
        Try
            Dim vFromDate, vToDate, vToDate_PlusOneDay As String

            If Not TXT_FromSummaryDate.Value Is Nothing Then
                vFromDate = "'" & Format(TXT_FromSummaryDate.Value, "MM-dd-yyyy") & "'"
            Else
                vFromDate = "NULL"
            End If

            vToDate_PlusOneDay = Txt_ToSummaryDate.DateTime.AddDays(1)
            vToDate = "'" & Format(CDate(vToDate_PlusOneDay), "MM-dd-yyyy") & "'"


            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Attendance.Code,                                      " & _
            "        Employees.DescA as Emp_Desc,               " & _
            "        TDate                                      " & _
            " From   Attendance INNER JOIN Employees            " & _
            " ON     Employees.Code = Attendance.Emp_Code       " & _
            " Where 1 = 1                                                         " & vbCrLf & _
            " And (TDate >= " & vFromDate & " Or " & vFromDate & " Is NULL) " & vbCrLf & _
            " And  TDate < " & vToDate & _
            " Order By TDate  "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)
                DTS_Summary.Rows(vRowCounter)("Code") = Trim(vSqlReader(0))
                'If vSqlReader.IsDBNull(1) = False Then
                '    DTS_Summary.Rows(vRowCounter)("DescA") = Trim(vSqlReader(1))
                'End If

                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = vSqlReader(1)
                Else
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = ""
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate") = Trim(vSqlReader(2))
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                End If

                'If vSqlReader.IsDBNull(8) = False Then
                '    If vSqlReader(8) = "P" Then
                '        Grd_Summary.Rows(vRowCounter).Appearance.BackColor = Color.BurlyWood
                '    Else
                '        Grd_Summary.Rows(vRowCounter).Appearance.BackColor = Color.Cornsilk
                '    End If
                'End If

                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick
        If Grd_Summary.ActiveRow.IsGroupByRow Then
            Exit Sub
        End If

        If Grd_Summary.ActiveRow.Band.Index <> 0 Then
            Exit Sub
        End If

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
    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_FndByDesc.ValueChanged, _
            TXT_FromSummaryDate.ValueChanged, Txt_ToSummaryDate.ValueChanged

        If sender.name = "" Then
            Exit Sub
        End If

        sQuerySummaryMain()
    End Sub
#End Region
    Private Sub Grd_Items_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grd_Items.DoubleClick
        Try
            If fCheckIf_Item_Exist_in_SalesList(Grd_Items.ActiveRow.Cells("Item_Code").Text) Then
                Return
            End If

            Dim vRow As UltraGridRow
            'For Each vRow In Grd_Sales.Rows
            '    If vRow.Cells("Item_Code").Text = Grd_Items.ActiveRow.Cells("Item_Code").Text Then
            '        MessageBox.Show("لا يمكن تكرار الصنف")
            '        Exit Sub
            '    End If
            'Next

            vDataRow = DTS_Attendance.Rows.Add
            'DTS_Sales.Rows(vDataRow.Index)("Item_Code") = "Item_Code"
            vDataRow("Item_Code") = Grd_Items.ActiveRow.Cells("Item_Code").Text
            vDataRow("Item_Desc") = Grd_Items.ActiveRow.Cells("DescA").Text
            vDataRow("Time") = DateTime.Now

            Txt_RowCount.Text = Grd_Sales.Rows.Count
            'vDataRow("PayedCash") = cControls.fReturnValue(" Select IsNull(PayedCash, 0) From Items Where Code = '" & Grd_Items.ActiveRow.Cells("Item_Code").Text & "'", Me.Name)
            vDataRow("DML") = "I"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Brn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Brn_Delete.Click
        Try
            If Grd_Sales.Rows.Count > 0 Then
                If Grd_Sales.ActiveRow Is Nothing Then
                    Grd_Sales.Rows(Grd_Sales.Rows.Count - 1).Delete(False)
                Else
                    Grd_Sales.ActiveRow.Delete(False)
                    If Grd_Sales.Rows.Count > 0 Then
                        Grd_Sales.Rows(Grd_Sales.Rows.Count - 1).Cells("Quantity").Selected = True
                        Grd_Sales.Rows(Grd_Sales.Rows.Count - 1).Cells("Quantity").Activate()
                        Grd_Sales.PerformAction(UltraGridAction.EnterEditMode)
                    End If
                End If

                Txt_RowCount.Text = Grd_Sales.Rows.Count
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Btn_Finish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Finish.Click
        'vSaveStatus = "Finish"
        fSaveAll(False)
    End Sub

    Private Sub sSaveSettings()
        Try
            'vSettings.vMinimizeRibbon = ToolBar_Main.Ribbon.IsMinimized
            'vSettings.vExpMain_Width = Exp_Categories.Width
            'vSettings.vPanel1 = Panel1.Width

            Settings.Persist(vSettings, "C:\DotNet_Sales_Settings.txt")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sLoadSettings()
        Try
            vSettings = Settings.Load("C:\DotNet_Sales_Settings.txt")

            'ToolBar_Main.Ribbon.IsMinimized = vSettings.vMinimizeRibbon
            'Exp_Categories.Width = vSettings.vExpMain_Width
            'Panel1.Width = vSettings.vPanel1
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Grd_Sales_AfterExitEditMode(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Sales.AfterExitEditMode
        vFocus = "Grid"
    End Sub

    Private Sub Txt_AvailableSupport_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        vFocus = "Text"
    End Sub

    Private Sub Txt_FndByCode_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FndByCode.ValueChanged

        'If Exp_Categories.CheckedItem Is Nothing Then
        sQueryItemsInSelectedCategory_ByCode("")
        'Else
        'sQueryItemsInSelectedCategory(Exp_Categories.CheckedItem.Tag)
        'End If
    End Sub
    Private Sub Txt_FndByDesc_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FndByDesc.ValueChanged

        'If Exp_Categories.CheckedItem Is Nothing Then
        sQueryItemsInSelectedCategory_ByDesc("")
        'Else
        'sQueryItemsInSelectedCategory(Exp_Categories.CheckedItem.Tag)
        'End If
    End Sub

    Private Sub Grd_Sales_ClickCellButton(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Sales.ClickCellButton
        If Grd_Sales.ActiveRow.Cells("DML").Text = "NI" Or Grd_Sales.ActiveRow.Cells("DML").Text = "I" Then
            Grd_Sales.ActiveRow.Delete(False)
        Else
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                Dim vSqlString As String
                vSqlString = " Delete From Attendance_Details " & _
                             " Where  Att_Code = '" & Trim(Txt_Code.Text) & "' " & _
                             " And    Ser      =  " & Grd_Sales.ActiveRow.Cells("Ser").Text

                If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                    Grd_Sales.ActiveRow.Delete(False)
                End If
            End If
        End If

        Txt_RowCount.Text = Grd_Sales.Rows.Count
    End Sub

    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Desc.ValueChanged, Txt_TDate.ValueChanged
        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If
    End Sub

End Class