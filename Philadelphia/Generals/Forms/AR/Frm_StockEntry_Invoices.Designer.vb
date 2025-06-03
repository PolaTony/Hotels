<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_StockEntry_Invoices
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
        Me.components = New System.ComponentModel.Container
        Dim UltraDataBand1 As Infragistics.Win.UltraWinDataSource.UltraDataBand = New Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1")
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Ser")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Item_Code")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Item_Ser")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Item_Desc")
        Dim UltraDataColumn5 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity")
        Dim UltraDataColumn6 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Str_Code")
        Dim UltraDataColumn7 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Str_Desc")
        Dim UltraDataColumn8 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn9 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DescA")
        Dim UltraDataColumn10 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("TDate")
        Dim UltraDataColumn11 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Emp_Desc")
        Dim UltraDataColumn12 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Provider_Desc")
        Dim UltraDataColumn13 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Store_Desc")
        Dim UltraDataColumn14 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Remarks")
        Dim UltraDataColumn15 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DML")
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_StockEntry_Invoices))
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Code")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("DescA")
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TDate")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Emp_Desc")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Provider_Desc")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Store_Desc")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Remarks")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("DML")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Band 1")
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 1", 0)
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Ser")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Item_Code")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Item_Ser")
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Item_Desc")
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity")
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn15 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Str_Code")
        Dim UltraGridColumn16 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Str_Desc")
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim ScrollBarLook1 As Infragistics.Win.UltraWinScrollBar.ScrollBarLook = New Infragistics.Win.UltraWinScrollBar.ScrollBarLook
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraToolbar1 As Infragistics.Win.UltraWinToolbars.UltraToolbar = New Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1")
        Dim LabelTool1 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim PopupMenuTool1 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tools")
        Dim LabelTool2 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Title")
        Dim Appearance59 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim LabelTool3 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("Title")
        Dim Appearance60 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim LabelTool4 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim PopupMenuTool2 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tools")
        Dim Appearance61 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim StateButtonTool1 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("FilterByDate", "")
        Dim StateButtonTool2 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("FilterByProcessed", "")
        Dim StateButtonTool3 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("FilterByDate", "")
        Dim StateButtonTool4 As Infragistics.Win.UltraWinToolbars.StateButtonTool = New Infragistics.Win.UltraWinToolbars.StateButtonTool("FilterByProcessed", "")
        Me.DTS_Summary = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel1_Fill_Panel = New Infragistics.Win.Misc.UltraPanel
        Me.Label17 = New System.Windows.Forms.Label
        Me.Txt_ToSummaryDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
        Me.Label16 = New System.Windows.Forms.Label
        Me.TXT_FromSummaryDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton
        Me.Btn_Ok = New Infragistics.Win.Misc.UltraButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.UltraPictureBox1 = New Infragistics.Win.UltraWinEditors.UltraPictureBox
        Me.TXT_SummaryDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Txt_FndByCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Txt_FndByDesc = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label6 = New System.Windows.Forms.Label
        Me.Grd_Summary = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me._Panel1_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me.ToolBar_Main = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
        Me._Panel1_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Panel1_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Panel1_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        CType(Me.DTS_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel1_Fill_Panel.ClientArea.SuspendLayout()
        Me.Panel1_Fill_Panel.SuspendLayout()
        CType(Me.Txt_ToSummaryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXT_FromSummaryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXT_SummaryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_FndByCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_FndByDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grd_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DTS_Summary
        '
        UltraDataColumn1.DataType = GetType(UInteger)
        UltraDataColumn5.DataType = GetType(Decimal)
        UltraDataBand1.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4, UltraDataColumn5, UltraDataColumn6, UltraDataColumn7})
        Me.DTS_Summary.Band.ChildBands.AddRange(New Object() {UltraDataBand1})
        UltraDataColumn10.DataType = GetType(Date)
        UltraDataColumn15.DefaultValue = "NI"
        Me.DTS_Summary.Band.Columns.AddRange(New Object() {UltraDataColumn8, UltraDataColumn9, UltraDataColumn10, UltraDataColumn11, UltraDataColumn12, UltraDataColumn13, UltraDataColumn14, UltraDataColumn15})
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel1_Fill_Panel)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Left)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Right)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Top)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Bottom)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(834, 457)
        Me.Panel1.TabIndex = 519
        '
        'Panel1_Fill_Panel
        '
        '
        'Panel1_Fill_Panel.ClientArea
        '
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Label17)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Txt_ToSummaryDate)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Label16)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.TXT_FromSummaryDate)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Btn_Cancel)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Btn_Ok)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Label11)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.UltraPictureBox1)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.TXT_SummaryDate)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Label4)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Label7)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Txt_FndByCode)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Txt_FndByDesc)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Label6)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Grd_Summary)
        Me.Panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1_Fill_Panel.Location = New System.Drawing.Point(0, 23)
        Me.Panel1_Fill_Panel.Name = "Panel1_Fill_Panel"
        Me.Panel1_Fill_Panel.Size = New System.Drawing.Size(834, 434)
        Me.Panel1_Fill_Panel.TabIndex = 0
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label17.Location = New System.Drawing.Point(165, 42)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 13)
        Me.Label17.TabIndex = 557
        Me.Label17.Text = "الى تاريخ"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_ToSummaryDate
        '
        Appearance29.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance29.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance29.TextHAlignAsString = "Right"
        Me.Txt_ToSummaryDate.Appearance = Appearance29
        Me.Txt_ToSummaryDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance30.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_ToSummaryDate.ButtonAppearance = Appearance30
        Me.Txt_ToSummaryDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_ToSummaryDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance31.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_ToSummaryDate.DropDownAppearance = Appearance31
        Me.Txt_ToSummaryDate.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_ToSummaryDate.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Never
        Me.Txt_ToSummaryDate.DropDownCalendarAlignment = Infragistics.Win.DropDownListAlignment.Left
        Me.Txt_ToSummaryDate.FormatString = "dd-MM-yyyy"
        Me.Txt_ToSummaryDate.Location = New System.Drawing.Point(12, 38)
        Me.Txt_ToSummaryDate.MaskInput = "{LOC}dd/mm/yyyy"
        Me.Txt_ToSummaryDate.Name = "Txt_ToSummaryDate"
        Me.Txt_ToSummaryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_ToSummaryDate.Size = New System.Drawing.Size(149, 20)
        Me.Txt_ToSummaryDate.SpinButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_ToSummaryDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.Txt_ToSummaryDate.TabIndex = 556
        Me.Txt_ToSummaryDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.Txt_ToSummaryDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_ToSummaryDate.Value = Nothing
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label16.Location = New System.Drawing.Point(167, 15)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(45, 13)
        Me.Label16.TabIndex = 555
        Me.Label16.Text = "من تاريخ"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXT_FromSummaryDate
        '
        Appearance32.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance32.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance32.TextHAlignAsString = "Right"
        Me.TXT_FromSummaryDate.Appearance = Appearance32
        Me.TXT_FromSummaryDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance33.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TXT_FromSummaryDate.ButtonAppearance = Appearance33
        Me.TXT_FromSummaryDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.TXT_FromSummaryDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance34.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TXT_FromSummaryDate.DropDownAppearance = Appearance34
        Me.TXT_FromSummaryDate.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.TXT_FromSummaryDate.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Never
        Me.TXT_FromSummaryDate.DropDownCalendarAlignment = Infragistics.Win.DropDownListAlignment.Left
        Me.TXT_FromSummaryDate.FormatString = "dd-MM-yyyy"
        Me.TXT_FromSummaryDate.Location = New System.Drawing.Point(12, 12)
        Me.TXT_FromSummaryDate.MaskInput = "{LOC}dd/mm/yyyy"
        Me.TXT_FromSummaryDate.Name = "TXT_FromSummaryDate"
        Me.TXT_FromSummaryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TXT_FromSummaryDate.Size = New System.Drawing.Size(149, 20)
        Me.TXT_FromSummaryDate.SpinButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.TXT_FromSummaryDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.TXT_FromSummaryDate.TabIndex = 554
        Me.TXT_FromSummaryDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.TXT_FromSummaryDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.TXT_FromSummaryDate.Value = Nothing
        '
        'Btn_Cancel
        '
        Appearance36.Image = CType(resources.GetObject("Appearance36.Image"), Object)
        Appearance36.TextHAlignAsString = "Right"
        Me.Btn_Cancel.Appearance = Appearance36
        Me.Btn_Cancel.Location = New System.Drawing.Point(579, 399)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(75, 29)
        Me.Btn_Cancel.TabIndex = 519
        Me.Btn_Cancel.Text = "الغاء"
        '
        'Btn_Ok
        '
        Appearance37.Image = CType(resources.GetObject("Appearance37.Image"), Object)
        Appearance37.TextHAlignAsString = "Right"
        Me.Btn_Ok.Appearance = Appearance37
        Me.Btn_Ok.Location = New System.Drawing.Point(675, 399)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(75, 29)
        Me.Btn_Ok.TabIndex = 518
        Me.Btn_Ok.Text = "موافق"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label11.Location = New System.Drawing.Point(736, 236)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 513
        Me.Label11.Text = "التاريخ"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label11.Visible = False
        '
        'UltraPictureBox1
        '
        Me.UltraPictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty
        Me.UltraPictureBox1.Image = CType(resources.GetObject("UltraPictureBox1.Image"), Object)
        Me.UltraPictureBox1.Location = New System.Drawing.Point(754, 12)
        Me.UltraPictureBox1.Name = "UltraPictureBox1"
        Me.UltraPictureBox1.Size = New System.Drawing.Size(32, 28)
        Me.UltraPictureBox1.TabIndex = 508
        '
        'TXT_SummaryDate
        '
        Appearance26.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance26.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance26.TextHAlignAsString = "Right"
        Me.TXT_SummaryDate.Appearance = Appearance26
        Me.TXT_SummaryDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance27.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TXT_SummaryDate.ButtonAppearance = Appearance27
        Me.TXT_SummaryDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.TXT_SummaryDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance28.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TXT_SummaryDate.DropDownAppearance = Appearance28
        Me.TXT_SummaryDate.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.TXT_SummaryDate.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Never
        Me.TXT_SummaryDate.DropDownCalendarAlignment = Infragistics.Win.DropDownListAlignment.Left
        Me.TXT_SummaryDate.FormatString = "yyyy-MMMM"
        Me.TXT_SummaryDate.Location = New System.Drawing.Point(610, 232)
        Me.TXT_SummaryDate.MaskInput = "{LOC}yyyy-mm"
        Me.TXT_SummaryDate.Name = "TXT_SummaryDate"
        Me.TXT_SummaryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TXT_SummaryDate.Size = New System.Drawing.Size(120, 20)
        Me.TXT_SummaryDate.SpinButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.TXT_SummaryDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.TXT_SummaryDate.TabIndex = 512
        Me.TXT_SummaryDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.TXT_SummaryDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.TXT_SummaryDate.Value = Nothing
        Me.TXT_SummaryDate.Visible = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label4.Location = New System.Drawing.Point(792, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 506
        Me.Label4.Text = "بحث"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label7.Location = New System.Drawing.Point(521, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 511
        Me.Label7.Text = "بالوصف"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_FndByCode
        '
        Me.Txt_FndByCode.AlwaysInEditMode = True
        Me.Txt_FndByCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance38.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance38.TextHAlignAsString = "Right"
        Me.Txt_FndByCode.Appearance = Appearance38
        Me.Txt_FndByCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_FndByCode.Location = New System.Drawing.Point(588, 13)
        Me.Txt_FndByCode.Name = "Txt_FndByCode"
        Me.Txt_FndByCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_FndByCode.Size = New System.Drawing.Size(117, 20)
        Me.Txt_FndByCode.TabIndex = 507
        Me.Txt_FndByCode.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Txt_FndByDesc
        '
        Me.Txt_FndByDesc.AlwaysInEditMode = True
        Me.Txt_FndByDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance35.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance35.TextHAlignAsString = "Right"
        Me.Txt_FndByDesc.Appearance = Appearance35
        Me.Txt_FndByDesc.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_FndByDesc.Location = New System.Drawing.Point(244, 12)
        Me.Txt_FndByDesc.Name = "Txt_FndByDesc"
        Me.Txt_FndByDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_FndByDesc.Size = New System.Drawing.Size(271, 20)
        Me.Txt_FndByDesc.TabIndex = 510
        Me.Txt_FndByDesc.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label6.Location = New System.Drawing.Point(711, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 509
        Me.Label6.Text = "بالكود"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Grd_Summary
        '
        Me.Grd_Summary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grd_Summary.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Summary.DataSource = Me.DTS_Summary
        Appearance1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance1.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.None
        Appearance1.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance1.TextHAlignAsString = "Right"
        Me.Grd_Summary.DisplayLayout.Appearance = Appearance1
        Me.Grd_Summary.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance2.FontData.BoldAsString = "True"
        Appearance2.FontData.Name = "Tahoma"
        Appearance2.FontData.SizeInPoints = 8.0!
        Appearance2.TextHAlignAsString = "Right"
        UltraGridColumn1.CellAppearance = Appearance2
        UltraGridColumn1.Format = ""
        Appearance3.FontData.Name = "Tahoma"
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        UltraGridColumn1.Header.Appearance = Appearance3
        UltraGridColumn1.Header.Caption = "الكود"
        UltraGridColumn1.Header.VisiblePosition = 6
        UltraGridColumn1.MaskInput = ""
        UltraGridColumn1.MaxWidth = 70
        UltraGridColumn1.MinWidth = 70
        UltraGridColumn1.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        UltraGridColumn1.Width = 70
        UltraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance4.FontData.BoldAsString = "True"
        Appearance4.TextHAlignAsString = "Right"
        UltraGridColumn2.CellAppearance = Appearance4
        Appearance5.FontData.Name = "Tahoma"
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        UltraGridColumn2.Header.Appearance = Appearance5
        UltraGridColumn2.Header.Caption = "البيان"
        UltraGridColumn2.Header.VisiblePosition = 5
        UltraGridColumn2.Width = 237
        UltraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn3.Header.Caption = "التاريخ"
        UltraGridColumn3.Header.VisiblePosition = 4
        UltraGridColumn3.MaxWidth = 100
        UltraGridColumn3.MinWidth = 100
        UltraGridColumn3.Width = 100
        UltraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn4.Header.Caption = "الموظف"
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.MaxWidth = 120
        UltraGridColumn4.MinWidth = 120
        UltraGridColumn4.Width = 120
        UltraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn5.Header.Caption = "المورد"
        UltraGridColumn5.Header.VisiblePosition = 1
        UltraGridColumn5.Hidden = True
        UltraGridColumn5.MaxWidth = 120
        UltraGridColumn5.MinWidth = 120
        UltraGridColumn5.Width = 120
        UltraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn6.Header.Caption = "المخزن"
        UltraGridColumn6.Header.VisiblePosition = 0
        UltraGridColumn6.Hidden = True
        UltraGridColumn6.MaxWidth = 100
        UltraGridColumn6.MinWidth = 100
        UltraGridColumn6.Width = 100
        UltraGridColumn7.Header.Caption = "ملاحظات"
        UltraGridColumn7.Header.VisiblePosition = 2
        UltraGridColumn7.Width = 246
        UltraGridColumn8.Header.VisiblePosition = 7
        UltraGridColumn8.Hidden = True
        UltraGridColumn8.Width = 42
        UltraGridColumn9.Header.VisiblePosition = 8
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8, UltraGridColumn9})
        Appearance6.FontData.Name = "Tahoma"
        Appearance6.FontData.SizeInPoints = 8.0!
        UltraGridBand1.Header.Appearance = Appearance6
        UltraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        UltraGridColumn10.Header.VisiblePosition = 5
        UltraGridColumn10.Hidden = True
        UltraGridColumn10.Width = 83
        UltraGridColumn11.Header.Caption = "كود الصنف"
        UltraGridColumn11.Header.VisiblePosition = 4
        UltraGridColumn11.MaxWidth = 70
        UltraGridColumn11.MinWidth = 70
        UltraGridColumn11.Width = 70
        UltraGridColumn12.Header.Caption = ""
        UltraGridColumn12.Header.VisiblePosition = 3
        UltraGridColumn12.MaxWidth = 50
        UltraGridColumn12.MinWidth = 50
        UltraGridColumn12.Width = 50
        UltraGridColumn13.Header.Caption = "وصف الصنف"
        UltraGridColumn13.Header.VisiblePosition = 2
        UltraGridColumn13.Width = 417
        Appearance7.TextHAlignAsString = "Left"
        UltraGridColumn14.CellAppearance = Appearance7
        UltraGridColumn14.Header.Caption = "الكمية"
        UltraGridColumn14.Header.VisiblePosition = 1
        UltraGridColumn14.MaxWidth = 70
        UltraGridColumn14.MinWidth = 70
        UltraGridColumn14.Width = 70
        UltraGridColumn15.Header.VisiblePosition = 6
        UltraGridColumn15.Hidden = True
        UltraGridColumn15.Width = 25
        UltraGridColumn16.Header.Caption = "المخزن"
        UltraGridColumn16.Header.VisiblePosition = 0
        UltraGridColumn16.Width = 147
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn10, UltraGridColumn11, UltraGridColumn12, UltraGridColumn13, UltraGridColumn14, UltraGridColumn15, UltraGridColumn16})
        Me.Grd_Summary.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.Grd_Summary.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Summary.DisplayLayout.Override.AddRowAppearance = Appearance12
        Me.Grd_Summary.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Appearance13.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.CellAppearance = Appearance13
        Appearance14.BorderColor = System.Drawing.SystemColors.Control
        Appearance14.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance14.Image = CType(resources.GetObject("Appearance14.Image"), Object)
        Me.Grd_Summary.DisplayLayout.Override.CellButtonAppearance = Appearance14
        Me.Grd_Summary.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Appearance15.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance15.BackColor2 = System.Drawing.SystemColors.Control
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance15.BorderColor = System.Drawing.SystemColors.Control
        Appearance15.BorderColor3DBase = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.HeaderAppearance = Appearance15
        Appearance16.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.RowAppearance = Appearance16
        Appearance17.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance17.BackColor2 = System.Drawing.SystemColors.Control
        Appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance17.BorderColor = System.Drawing.SystemColors.Control
        Appearance17.BorderColor3DBase = System.Drawing.SystemColors.ControlLightLight
        Appearance17.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorAppearance = Appearance17
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement
        Me.Grd_Summary.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed
        Appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Summary.DisplayLayout.Override.TemplateAddRowAppearance = Appearance18
        Appearance19.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.Override.TemplateAddRowCellAppearance = Appearance19
        Appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance20.BackColor2 = System.Drawing.SystemColors.ControlLight
        Appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance20.BorderColor = System.Drawing.SystemColors.Control
        ScrollBarLook1.Appearance = Appearance20
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump
        Appearance21.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance21.ForeColor = System.Drawing.SystemColors.AppWorkspace
        ScrollBarLook1.ButtonAppearance = Appearance21
        Appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.HorizontalBump
        ScrollBarLook1.ThumbAppearance = Appearance22
        Appearance23.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance23.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance23.BorderColor = System.Drawing.SystemColors.Control
        Appearance23.BorderColor3DBase = System.Drawing.SystemColors.Control
        ScrollBarLook1.TrackAppearance = Appearance23
        Me.Grd_Summary.DisplayLayout.ScrollBarLook = ScrollBarLook1
        Me.Grd_Summary.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.Grd_Summary.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance24.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.SplitterBarHorizontalAppearance = Appearance24
        Appearance25.BackColor = System.Drawing.SystemColors.Control
        Appearance25.BorderColor = System.Drawing.SystemColors.Control
        Appearance25.BorderColor3DBase = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.SplitterBarVerticalAppearance = Appearance25
        Me.Grd_Summary.Location = New System.Drawing.Point(12, 64)
        Me.Grd_Summary.Name = "Grd_Summary"
        Me.Grd_Summary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Grd_Summary.Size = New System.Drawing.Size(813, 329)
        Me.Grd_Summary.TabIndex = 520
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
        Me._Panel1_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 434)
        Me._Panel1_Toolbars_Dock_Area_Left.ToolbarsManager = Me.ToolBar_Main
        '
        'ToolBar_Main
        '
        Me.ToolBar_Main.DesignerFlags = 1
        Me.ToolBar_Main.DockWithinContainer = Me.Panel1
        Me.ToolBar_Main.RightAlignedMenus = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.RuntimeCustomizationOptions = Infragistics.Win.UltraWinToolbars.RuntimeCustomizationOptions.None
        Me.ToolBar_Main.ShowFullMenusDelay = 500
        Me.ToolBar_Main.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.OfficeXP
        UltraToolbar1.DockedColumn = 0
        UltraToolbar1.DockedRow = 0
        UltraToolbar1.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool1, PopupMenuTool1, LabelTool2})
        Appearance59.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance59.BackColor2 = System.Drawing.SystemColors.Control
        Appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        UltraToolbar1.Settings.Appearance = Appearance59
        UltraToolbar1.Settings.CaptionPlacement = Infragistics.Win.TextPlacement.LeftOfImage
        UltraToolbar1.Settings.FillEntireRow = Infragistics.Win.DefaultableBoolean.[True]
        UltraToolbar1.Text = "ToolBar1"
        Me.ToolBar_Main.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1})
        Appearance60.FontData.BoldAsString = "True"
        Appearance60.FontData.Name = "Tahoma"
        Appearance60.TextHAlignAsString = "Right"
        LabelTool3.SharedPropsInternal.AppearancesSmall.Appearance = Appearance60
        LabelTool3.SharedPropsInternal.Caption = "فاتورة شراء"
        LabelTool3.SharedPropsInternal.Visible = False
        LabelTool4.SharedPropsInternal.Spring = True
        LabelTool4.SharedPropsInternal.Visible = False
        Appearance61.FontData.BoldAsString = "True"
        Appearance61.FontData.Name = "Tahoma"
        Appearance61.FontData.SizeInPoints = 12.0!
        Appearance61.Image = CType(resources.GetObject("Appearance61.Image"), Object)
        PopupMenuTool2.SharedPropsInternal.AppearancesSmall.Appearance = Appearance61
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
        Me._Panel1_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(834, 23)
        Me._Panel1_Toolbars_Dock_Area_Right.Name = "_Panel1_Toolbars_Dock_Area_Right"
        Me._Panel1_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 434)
        Me._Panel1_Toolbars_Dock_Area_Right.ToolbarsManager = Me.ToolBar_Main
        '
        '_Panel1_Toolbars_Dock_Area_Top
        '
        Me._Panel1_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
        Me._Panel1_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0)
        Me._Panel1_Toolbars_Dock_Area_Top.Name = "_Panel1_Toolbars_Dock_Area_Top"
        Me._Panel1_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(834, 23)
        Me._Panel1_Toolbars_Dock_Area_Top.ToolbarsManager = Me.ToolBar_Main
        '
        '_Panel1_Toolbars_Dock_Area_Bottom
        '
        Me._Panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
        Me._Panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 457)
        Me._Panel1_Toolbars_Dock_Area_Bottom.Name = "_Panel1_Toolbars_Dock_Area_Bottom"
        Me._Panel1_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(834, 0)
        Me._Panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.ToolBar_Main
        '
        'Frm_StockEntry_Invoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 457)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_StockEntry_Invoices"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "أذونات الدخول"
        CType(Me.DTS_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1_Fill_Panel.ClientArea.ResumeLayout(False)
        Me.Panel1_Fill_Panel.ClientArea.PerformLayout()
        Me.Panel1_Fill_Panel.ResumeLayout(False)
        CType(Me.Txt_ToSummaryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXT_FromSummaryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXT_SummaryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_FndByCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_FndByDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grd_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DTS_Summary As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Ok As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents UltraPictureBox1 As Infragistics.Win.UltraWinEditors.UltraPictureBox
    Friend WithEvents TXT_SummaryDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Txt_FndByCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Txt_FndByDesc As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Grd_Summary As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Txt_ToSummaryDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TXT_FromSummaryDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents ToolBar_Main As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    Friend WithEvents Panel1_Fill_Panel As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
End Class
