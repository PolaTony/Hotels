<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_All_POS_Invoices
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim UltraDataBand1 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Ser")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Item_Code")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Item_Ser")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Item_Desc")
        Dim UltraDataColumn5 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity")
        Dim UltraDataColumn6 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Price")
        Dim UltraDataColumn7 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Addition")
        Dim UltraDataColumn8 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Deduction")
        Dim UltraDataColumn9 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("LCost")
        Dim UltraDataColumn10 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Str_Code")
        Dim UltraDataColumn11 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Str_Desc")
        Dim UltraDataColumn12 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn13 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DescA")
        Dim UltraDataColumn14 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("TDate")
        Dim UltraDataColumn15 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Emp_Desc")
        Dim UltraDataColumn16 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Provider_Desc")
        Dim UltraDataColumn17 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Store_Desc")
        Dim UltraDataColumn18 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DML")
        Dim UltraDataColumn19 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SalesTypes_Desc")
        Dim UltraDataColumn20 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ShiftNum")
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_All_POS_Invoices))
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn20 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Code")
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn21 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("DescA")
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn22 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TDate")
        Dim UltraGridColumn23 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Emp_Desc")
        Dim UltraGridColumn24 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Provider_Desc")
        Dim UltraGridColumn25 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Store_Desc")
        Dim UltraGridColumn26 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("DML")
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SalesTypes_Desc")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ShiftNum")
        Dim UltraGridColumn27 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Band 1")
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 1", 0)
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Ser")
        Dim UltraGridColumn29 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Item_Code")
        Dim UltraGridColumn30 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Item_Ser")
        Dim UltraGridColumn31 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Item_Desc")
        Dim UltraGridColumn32 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity")
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn33 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price")
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn34 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Addition")
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn35 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Deduction")
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn36 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("LCost")
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn37 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Str_Code")
        Dim UltraGridColumn38 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Str_Desc")
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ScrollBarLook1 As Infragistics.Win.UltraWinScrollBar.ScrollBarLook = New Infragistics.Win.UltraWinScrollBar.ScrollBarLook()
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraToolbar1 As Infragistics.Win.UltraWinToolbars.UltraToolbar = New Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1")
        Dim LabelTool1 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim PopupMenuTool1 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tools")
        Dim LabelTool2 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Title")
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim LabelTool3 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Title")
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim LabelTool4 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim PopupMenuTool2 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tools")
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim StateButtonTool1 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("FilterByDate", "")
        Dim StateButtonTool2 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("FilterByProcessed", "")
        Dim StateButtonTool3 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("FilterByDate", "")
        Dim StateButtonTool4 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("FilterByProcessed", "")
        Me.DTS_Summary = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Txt_FndByDesc = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Txt_FndByCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.TXT_FromDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraPictureBox1 = New Infragistics.Win.UltraWinEditors.UltraPictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Btn_Ok = New Infragistics.Win.Misc.UltraButton()
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_ToDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Grd_Summary = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me._Panel1_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me.ToolBar_Main = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
        Me._Panel1_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._Panel1_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._Panel1_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        CType(Me.DTS_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Txt_FndByDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_FndByCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXT_FromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grd_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DTS_Summary
        '
        UltraDataColumn1.DataType = GetType(UInteger)
        UltraDataColumn5.DataType = GetType(Decimal)
        UltraDataColumn6.DataType = GetType(Decimal)
        UltraDataColumn7.DataType = GetType(Decimal)
        UltraDataColumn8.DataType = GetType(Decimal)
        UltraDataColumn9.DataType = GetType(Decimal)
        UltraDataBand1.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4, UltraDataColumn5, UltraDataColumn6, UltraDataColumn7, UltraDataColumn8, UltraDataColumn9, UltraDataColumn10, UltraDataColumn11})
        Me.DTS_Summary.Band.ChildBands.AddRange(New Object() {UltraDataBand1})
        UltraDataColumn14.DataType = GetType(Date)
        UltraDataColumn18.DefaultValue = "NI"
        UltraDataColumn20.DataType = GetType(Integer)
        Me.DTS_Summary.Band.Columns.AddRange(New Object() {UltraDataColumn12, UltraDataColumn13, UltraDataColumn14, UltraDataColumn15, UltraDataColumn16, UltraDataColumn17, UltraDataColumn18, UltraDataColumn19, UltraDataColumn20})
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Txt_FndByDesc)
        Me.Panel1.Controls.Add(Me.Txt_FndByCode)
        Me.Panel1.Controls.Add(Me.TXT_FromDate)
        Me.Panel1.Controls.Add(Me.UltraPictureBox1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Btn_Ok)
        Me.Panel1.Controls.Add(Me.Btn_Cancel)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Txt_ToDate)
        Me.Panel1.Controls.Add(Me.Grd_Summary)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Left)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Right)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Bottom)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Top)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 450)
        Me.Panel1.TabIndex = 519
        '
        'Txt_FndByDesc
        '
        Me.Txt_FndByDesc.AlwaysInEditMode = True
        Me.Txt_FndByDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance1.TextHAlignAsString = "Right"
        Me.Txt_FndByDesc.Appearance = Appearance1
        Me.Txt_FndByDesc.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_FndByDesc.Location = New System.Drawing.Point(221, 31)
        Me.Txt_FndByDesc.Name = "Txt_FndByDesc"
        Me.Txt_FndByDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_FndByDesc.Size = New System.Drawing.Size(187, 20)
        Me.Txt_FndByDesc.TabIndex = 510
        Me.Txt_FndByDesc.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_FndByDesc.Visible = False
        '
        'Txt_FndByCode
        '
        Me.Txt_FndByCode.AlwaysInEditMode = True
        Me.Txt_FndByCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance2.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance2.TextHAlignAsString = "Right"
        Me.Txt_FndByCode.Appearance = Appearance2
        Me.Txt_FndByCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_FndByCode.Location = New System.Drawing.Point(503, 30)
        Me.Txt_FndByCode.Name = "Txt_FndByCode"
        Me.Txt_FndByCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_FndByCode.Size = New System.Drawing.Size(117, 20)
        Me.Txt_FndByCode.TabIndex = 507
        Me.Txt_FndByCode.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_FndByCode.Visible = False
        '
        'TXT_FromDate
        '
        Appearance3.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance3.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance3.TextHAlignAsString = "Right"
        Me.TXT_FromDate.Appearance = Appearance3
        Me.TXT_FromDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TXT_FromDate.ButtonAppearance = Appearance4
        Me.TXT_FromDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.TXT_FromDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TXT_FromDate.DropDownAppearance = Appearance5
        Me.TXT_FromDate.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.TXT_FromDate.DropDownCalendarAlignment = Infragistics.Win.DropDownListAlignment.Left
        Me.TXT_FromDate.FormatString = "dd-MM-yyyy"
        Me.TXT_FromDate.Location = New System.Drawing.Point(12, 31)
        Me.TXT_FromDate.MaskInput = "{LOC}dd/mm/yyyy"
        Me.TXT_FromDate.Name = "TXT_FromDate"
        Me.TXT_FromDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TXT_FromDate.Size = New System.Drawing.Size(137, 20)
        Me.TXT_FromDate.SpinButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.TXT_FromDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.TXT_FromDate.TabIndex = 512
        Me.TXT_FromDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.TXT_FromDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.TXT_FromDate.Value = Nothing
        '
        'UltraPictureBox1
        '
        Me.UltraPictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty
        Me.UltraPictureBox1.Image = CType(resources.GetObject("UltraPictureBox1.Image"), Object)
        Me.UltraPictureBox1.Location = New System.Drawing.Point(709, 35)
        Me.UltraPictureBox1.Name = "UltraPictureBox1"
        Me.UltraPictureBox1.Size = New System.Drawing.Size(32, 28)
        Me.UltraPictureBox1.TabIndex = 508
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label6.Location = New System.Drawing.Point(626, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 509
        Me.Label6.Text = "بالكود"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label6.Visible = False
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label7.Location = New System.Drawing.Point(414, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 511
        Me.Label7.Text = "بالوصف"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label7.Visible = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label4.Location = New System.Drawing.Point(747, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 506
        Me.Label4.Text = "بحث"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label11.Location = New System.Drawing.Point(155, 35)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 513
        Me.Label11.Text = "من تاريخ"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Btn_Ok
        '
        Appearance6.Image = CType(resources.GetObject("Appearance6.Image"), Object)
        Appearance6.TextHAlignAsString = "Right"
        Me.Btn_Ok.Appearance = Appearance6
        Me.Btn_Ok.Location = New System.Drawing.Point(679, 409)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(75, 29)
        Me.Btn_Ok.TabIndex = 518
        Me.Btn_Ok.Text = "موافق"
        '
        'Btn_Cancel
        '
        Appearance7.Image = CType(resources.GetObject("Appearance7.Image"), Object)
        Appearance7.TextHAlignAsString = "Right"
        Me.Btn_Cancel.Appearance = Appearance7
        Me.Btn_Cancel.Location = New System.Drawing.Point(583, 409)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(75, 29)
        Me.Btn_Cancel.TabIndex = 519
        Me.Btn_Cancel.Text = "الغاء"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label1.Location = New System.Drawing.Point(155, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 525
        Me.Label1.Text = "الى تاريخ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_ToDate
        '
        Appearance8.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance8.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance8.TextHAlignAsString = "Right"
        Me.Txt_ToDate.Appearance = Appearance8
        Me.Txt_ToDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_ToDate.ButtonAppearance = Appearance9
        Me.Txt_ToDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_ToDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_ToDate.DropDownAppearance = Appearance10
        Me.Txt_ToDate.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_ToDate.DropDownCalendarAlignment = Infragistics.Win.DropDownListAlignment.Left
        Me.Txt_ToDate.FormatString = "dd-MM-yyyy"
        Me.Txt_ToDate.Location = New System.Drawing.Point(12, 57)
        Me.Txt_ToDate.MaskInput = "{LOC}dd/mm/yyyy"
        Me.Txt_ToDate.Name = "Txt_ToDate"
        Me.Txt_ToDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_ToDate.Size = New System.Drawing.Size(137, 20)
        Me.Txt_ToDate.SpinButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_ToDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.Txt_ToDate.TabIndex = 524
        Me.Txt_ToDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.Txt_ToDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_ToDate.Value = Nothing
        '
        'Grd_Summary
        '
        Me.Grd_Summary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grd_Summary.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Summary.DataSource = Me.DTS_Summary
        Appearance11.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance11.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.None
        Appearance11.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance11.TextHAlignAsString = "Right"
        Me.Grd_Summary.DisplayLayout.Appearance = Appearance11
        Me.Grd_Summary.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridColumn20.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance12.FontData.BoldAsString = "True"
        Appearance12.FontData.Name = "Tahoma"
        Appearance12.FontData.SizeInPoints = 8.0!
        Appearance12.TextHAlignAsString = "Right"
        UltraGridColumn20.CellAppearance = Appearance12
        UltraGridColumn20.Format = ""
        Appearance13.FontData.Name = "Tahoma"
        Appearance13.FontData.SizeInPoints = 8.25!
        Appearance13.TextHAlignAsString = "Right"
        UltraGridColumn20.Header.Appearance = Appearance13
        UltraGridColumn20.Header.Caption = "الكود"
        UltraGridColumn20.Header.VisiblePosition = 7
        UltraGridColumn20.MaskInput = ""
        UltraGridColumn20.MaxWidth = 70
        UltraGridColumn20.MinWidth = 70
        UltraGridColumn20.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        UltraGridColumn20.Width = 70
        UltraGridColumn21.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance14.FontData.BoldAsString = "True"
        Appearance14.TextHAlignAsString = "Right"
        UltraGridColumn21.CellAppearance = Appearance14
        Appearance15.FontData.Name = "Tahoma"
        Appearance15.FontData.SizeInPoints = 8.25!
        Appearance15.TextHAlignAsString = "Right"
        UltraGridColumn21.Header.Appearance = Appearance15
        UltraGridColumn21.Header.Caption = "الوصف"
        UltraGridColumn21.Header.VisiblePosition = 6
        UltraGridColumn21.Hidden = True
        UltraGridColumn21.Width = 241
        UltraGridColumn22.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn22.Header.Caption = "التاريخ"
        UltraGridColumn22.Header.VisiblePosition = 5
        UltraGridColumn22.MaxWidth = 100
        UltraGridColumn22.MinWidth = 100
        UltraGridColumn22.Width = 100
        UltraGridColumn23.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn23.Header.Caption = "الكاشير"
        UltraGridColumn23.Header.VisiblePosition = 4
        UltraGridColumn23.MaxWidth = 120
        UltraGridColumn23.MinWidth = 120
        UltraGridColumn23.Width = 120
        UltraGridColumn24.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn24.Header.Caption = "العميل"
        UltraGridColumn24.Header.VisiblePosition = 3
        UltraGridColumn24.Width = 336
        UltraGridColumn25.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn25.Header.Caption = "المخزن"
        UltraGridColumn25.Header.VisiblePosition = 0
        UltraGridColumn25.Hidden = True
        UltraGridColumn25.MaxWidth = 100
        UltraGridColumn25.MinWidth = 100
        UltraGridColumn25.Width = 100
        UltraGridColumn26.Header.VisiblePosition = 8
        UltraGridColumn26.Hidden = True
        UltraGridColumn26.Width = 42
        UltraGridColumn1.Header.Caption = "أنظمة البيع"
        UltraGridColumn1.Header.VisiblePosition = 1
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.MaxWidth = 140
        UltraGridColumn1.MinWidth = 140
        UltraGridColumn1.Width = 140
        UltraGridColumn2.Header.Caption = "رقم الوردية"
        UltraGridColumn2.Header.VisiblePosition = 2
        UltraGridColumn2.Width = 125
        UltraGridColumn27.Header.VisiblePosition = 9
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn20, UltraGridColumn21, UltraGridColumn22, UltraGridColumn23, UltraGridColumn24, UltraGridColumn25, UltraGridColumn26, UltraGridColumn1, UltraGridColumn2, UltraGridColumn27})
        Appearance16.FontData.Name = "Tahoma"
        Appearance16.FontData.SizeInPoints = 8.0!
        UltraGridBand1.Header.Appearance = Appearance16
        UltraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        UltraGridColumn28.Header.VisiblePosition = 8
        UltraGridColumn28.Hidden = True
        UltraGridColumn28.Width = 83
        UltraGridColumn29.Header.Caption = "كود الصنف"
        UltraGridColumn29.Header.VisiblePosition = 7
        UltraGridColumn29.MaxWidth = 70
        UltraGridColumn29.MinWidth = 70
        UltraGridColumn29.Width = 70
        UltraGridColumn30.Header.Caption = ""
        UltraGridColumn30.Header.VisiblePosition = 6
        UltraGridColumn30.MaxWidth = 50
        UltraGridColumn30.MinWidth = 50
        UltraGridColumn30.Width = 50
        UltraGridColumn31.Header.Caption = "وصف الصنف"
        UltraGridColumn31.Header.VisiblePosition = 5
        UltraGridColumn31.Width = 302
        Appearance17.TextHAlignAsString = "Left"
        UltraGridColumn32.CellAppearance = Appearance17
        UltraGridColumn32.Header.Caption = "الكمية"
        UltraGridColumn32.Header.VisiblePosition = 4
        UltraGridColumn32.MaxWidth = 70
        UltraGridColumn32.MinWidth = 70
        UltraGridColumn32.Width = 70
        UltraGridColumn33.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance18.TextHAlignAsString = "Left"
        UltraGridColumn33.CellAppearance = Appearance18
        UltraGridColumn33.Header.Caption = "السعر"
        UltraGridColumn33.Header.VisiblePosition = 3
        UltraGridColumn33.MaxWidth = 70
        UltraGridColumn33.MinWidth = 70
        UltraGridColumn33.Width = 70
        Appearance19.TextHAlignAsString = "Left"
        UltraGridColumn34.CellAppearance = Appearance19
        UltraGridColumn34.Header.Caption = "أضافة"
        UltraGridColumn34.Header.VisiblePosition = 2
        UltraGridColumn34.Hidden = True
        UltraGridColumn34.MaxWidth = 70
        UltraGridColumn34.MinWidth = 70
        UltraGridColumn34.Width = 70
        Appearance20.TextHAlignAsString = "Left"
        UltraGridColumn35.CellAppearance = Appearance20
        UltraGridColumn35.Header.Caption = "خصم"
        UltraGridColumn35.Header.VisiblePosition = 1
        UltraGridColumn35.MaxWidth = 70
        UltraGridColumn35.MinWidth = 70
        UltraGridColumn35.Width = 70
        Appearance21.TextHAlignAsString = "Left"
        UltraGridColumn36.CellAppearance = Appearance21
        UltraGridColumn36.Header.Caption = "الأجمالي"
        UltraGridColumn36.Header.VisiblePosition = 0
        UltraGridColumn36.MaxWidth = 100
        UltraGridColumn36.MinWidth = 100
        UltraGridColumn36.Width = 100
        UltraGridColumn37.Header.VisiblePosition = 9
        UltraGridColumn37.Hidden = True
        UltraGridColumn37.Width = 25
        UltraGridColumn38.Header.VisiblePosition = 10
        UltraGridColumn38.Hidden = True
        UltraGridColumn38.Width = 84
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn28, UltraGridColumn29, UltraGridColumn30, UltraGridColumn31, UltraGridColumn32, UltraGridColumn33, UltraGridColumn34, UltraGridColumn35, UltraGridColumn36, UltraGridColumn37, UltraGridColumn38})
        Me.Grd_Summary.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.Grd_Summary.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Appearance22.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Summary.DisplayLayout.Override.AddRowAppearance = Appearance22
        Me.Grd_Summary.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Appearance23.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.CellAppearance = Appearance23
        Appearance24.BorderColor = System.Drawing.SystemColors.Control
        Appearance24.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance24.Image = CType(resources.GetObject("Appearance24.Image"), Object)
        Me.Grd_Summary.DisplayLayout.Override.CellButtonAppearance = Appearance24
        Me.Grd_Summary.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.Grd_Summary.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow
        Appearance25.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance25.BackColor2 = System.Drawing.SystemColors.Control
        Appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance25.BorderColor = System.Drawing.SystemColors.Control
        Appearance25.BorderColor3DBase = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.HeaderAppearance = Appearance25
        Appearance26.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.RowAppearance = Appearance26
        Appearance27.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance27.BackColor2 = System.Drawing.SystemColors.Control
        Appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance27.BorderColor = System.Drawing.SystemColors.Control
        Appearance27.BorderColor3DBase = System.Drawing.SystemColors.ControlLightLight
        Appearance27.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorAppearance = Appearance27
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement
        Me.Grd_Summary.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed
        Appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Summary.DisplayLayout.Override.TemplateAddRowAppearance = Appearance28
        Appearance29.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.Override.TemplateAddRowCellAppearance = Appearance29
        Appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance30.BackColor2 = System.Drawing.SystemColors.ControlLight
        Appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance30.BorderColor = System.Drawing.SystemColors.Control
        ScrollBarLook1.Appearance = Appearance30
        Appearance31.BackColor = System.Drawing.SystemColors.Control
        Appearance31.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump
        Appearance31.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance31.ForeColor = System.Drawing.SystemColors.AppWorkspace
        ScrollBarLook1.ButtonAppearance = Appearance31
        Appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.HorizontalBump
        ScrollBarLook1.ThumbAppearance = Appearance32
        Appearance33.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance33.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance33.BorderColor = System.Drawing.SystemColors.Control
        Appearance33.BorderColor3DBase = System.Drawing.SystemColors.Control
        ScrollBarLook1.TrackAppearance = Appearance33
        Me.Grd_Summary.DisplayLayout.ScrollBarLook = ScrollBarLook1
        Me.Grd_Summary.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.Grd_Summary.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Appearance34.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance34.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.SplitterBarHorizontalAppearance = Appearance34
        Appearance35.BackColor = System.Drawing.SystemColors.Control
        Appearance35.BorderColor = System.Drawing.SystemColors.Control
        Appearance35.BorderColor3DBase = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.SplitterBarVerticalAppearance = Appearance35
        Me.Grd_Summary.Location = New System.Drawing.Point(5, 83)
        Me.Grd_Summary.Name = "Grd_Summary"
        Me.Grd_Summary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Grd_Summary.Size = New System.Drawing.Size(791, 320)
        Me.Grd_Summary.TabIndex = 484
        Me.Grd_Summary.UseFlatMode = Infragistics.Win.DefaultableBoolean.[True]
        '
        '_Panel1_Toolbars_Dock_Area_Left
        '
        Me._Panel1_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left
        Me._Panel1_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Left.Location = New System.Drawing.Point(0, 23)
        Me._Panel1_Toolbars_Dock_Area_Left.Name = "_Panel1_Toolbars_Dock_Area_Left"
        Me._Panel1_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 427)
        Me._Panel1_Toolbars_Dock_Area_Left.ToolbarsManager = Me.ToolBar_Main
        '
        'ToolBar_Main
        '
        Me.ToolBar_Main.DesignerFlags = 1
        Me.ToolBar_Main.DockWithinContainer = Me.Panel1
        Me.ToolBar_Main.MultiMonitorDropDownBehavior = Infragistics.Win.UltraWinToolbars.MultiMonitorDropDownBehavior.ShiftToMonitorWithExclusionRect
        Me.ToolBar_Main.RightAlignedMenus = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.RuntimeCustomizationOptions = Infragistics.Win.UltraWinToolbars.RuntimeCustomizationOptions.None
        Me.ToolBar_Main.ShowFullMenusDelay = 500
        Me.ToolBar_Main.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.OfficeXP
        UltraToolbar1.DockedColumn = 0
        UltraToolbar1.DockedRow = 0
        UltraToolbar1.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool1, PopupMenuTool1, LabelTool2})
        Appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance36.BackColor2 = System.Drawing.SystemColors.Control
        Appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        UltraToolbar1.Settings.Appearance = Appearance36
        UltraToolbar1.Settings.CaptionPlacement = Infragistics.Win.TextPlacement.LeftOfImage
        UltraToolbar1.Settings.FillEntireRow = Infragistics.Win.DefaultableBoolean.[True]
        UltraToolbar1.Text = "ToolBar1"
        Me.ToolBar_Main.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1})
        Appearance37.FontData.BoldAsString = "True"
        Appearance37.FontData.Name = "Tahoma"
        Appearance37.TextHAlignAsString = "Right"
        LabelTool3.SharedPropsInternal.AppearancesSmall.Appearance = Appearance37
        LabelTool3.SharedPropsInternal.Caption = "فاتورة شراء"
        LabelTool3.SharedPropsInternal.Visible = False
        LabelTool4.SharedPropsInternal.Spring = True
        LabelTool4.SharedPropsInternal.Visible = False
        Appearance38.FontData.BoldAsString = "True"
        Appearance38.FontData.Name = "Tahoma"
        Appearance38.FontData.SizeInPoints = 12.0!
        Appearance38.Image = CType(resources.GetObject("Appearance38.Image"), Object)
        PopupMenuTool2.SharedPropsInternal.AppearancesSmall.Appearance = Appearance38
        PopupMenuTool2.SharedPropsInternal.Caption = "أدوات"
        PopupMenuTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText
        StateButtonTool1.Checked = True
        StateButtonTool1.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark
        StateButtonTool2.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark
        PopupMenuTool2.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {StateButtonTool1, StateButtonTool2})
        StateButtonTool3.Checked = True
        StateButtonTool3.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark
        StateButtonTool3.SharedPropsInternal.Caption = "تصفية بالتاريخ"
        StateButtonTool4.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark
        StateButtonTool4.SharedPropsInternal.Caption = "تصفية بالفواتير المغلقة"
        Me.ToolBar_Main.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool3, LabelTool4, PopupMenuTool2, StateButtonTool3, StateButtonTool4})
        '
        '_Panel1_Toolbars_Dock_Area_Right
        '
        Me._Panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right
        Me._Panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(800, 23)
        Me._Panel1_Toolbars_Dock_Area_Right.Name = "_Panel1_Toolbars_Dock_Area_Right"
        Me._Panel1_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 427)
        Me._Panel1_Toolbars_Dock_Area_Right.ToolbarsManager = Me.ToolBar_Main
        '
        '_Panel1_Toolbars_Dock_Area_Bottom
        '
        Me._Panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
        Me._Panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 450)
        Me._Panel1_Toolbars_Dock_Area_Bottom.Name = "_Panel1_Toolbars_Dock_Area_Bottom"
        Me._Panel1_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(800, 0)
        Me._Panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.ToolBar_Main
        '
        '_Panel1_Toolbars_Dock_Area_Top
        '
        Me._Panel1_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
        Me._Panel1_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0)
        Me._Panel1_Toolbars_Dock_Area_Top.Name = "_Panel1_Toolbars_Dock_Area_Top"
        Me._Panel1_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(800, 23)
        Me._Panel1_Toolbars_Dock_Area_Top.ToolbarsManager = Me.ToolBar_Main
        '
        'Frm_All_POS_Invoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_All_POS_Invoices"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "فواتير نقاط البيع"
        CType(Me.DTS_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Txt_FndByDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_FndByCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXT_FromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grd_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DTS_Summary As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Grd_Summary As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_ToDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Ok As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Label11 As Label
    Friend WithEvents UltraPictureBox1 As Infragistics.Win.UltraWinEditors.UltraPictureBox
    Friend WithEvents TXT_FromDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Txt_FndByCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Txt_FndByDesc As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label6 As Label
    Friend WithEvents ToolBar_Main As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
End Class
