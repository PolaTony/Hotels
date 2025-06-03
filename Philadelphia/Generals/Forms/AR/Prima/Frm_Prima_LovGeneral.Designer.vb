<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Prima_LovGeneral
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DescA")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Type")
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Prima_LovGeneral))
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Code")
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("DescA")
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Type")
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim ScrollBarLook1 As Infragistics.Win.UltraWinScrollBar.ScrollBarLook = New Infragistics.Win.UltraWinScrollBar.ScrollBarLook
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraToolbar1 As Infragistics.Win.UltraWinToolbars.UltraToolbar = New Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1")
        Dim LabelTool1 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim LabelTool2 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool1")
        Dim Appearance50 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim LabelTool3 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool1")
        Dim Appearance51 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim LabelTool4 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Me.DTS_Main = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton
        Me.Btn_Ok = New Infragistics.Win.Misc.UltraButton
        Me.Txt_FndByDesc = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Txt_FndByCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Grd_Main = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.ToolBar_Main = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        CType(Me.DTS_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Txt_FndByDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_FndByCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grd_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DTS_Main
        '
        Me.DTS_Main.Band.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3})
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Btn_Cancel)
        Me.Panel1.Controls.Add(Me.Btn_Ok)
        Me.Panel1.Controls.Add(Me.Txt_FndByDesc)
        Me.Panel1.Controls.Add(Me.Txt_FndByCode)
        Me.Panel1.Controls.Add(Me.Grd_Main)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(536, 345)
        Me.Panel1.TabIndex = 3
        '
        'Btn_Cancel
        '
        Appearance1.Image = CType(resources.GetObject("Appearance1.Image"), Object)
        Appearance1.TextHAlignAsString = "Right"
        Me.Btn_Cancel.Appearance = Appearance1
        Me.Btn_Cancel.Location = New System.Drawing.Point(304, 308)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(75, 29)
        Me.Btn_Cancel.TabIndex = 492
        Me.Btn_Cancel.Text = "«·€«¡"
        '
        'Btn_Ok
        '
        Appearance2.Image = CType(resources.GetObject("Appearance2.Image"), Object)
        Appearance2.TextHAlignAsString = "Right"
        Me.Btn_Ok.Appearance = Appearance2
        Me.Btn_Ok.Location = New System.Drawing.Point(400, 308)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(75, 29)
        Me.Btn_Ok.TabIndex = 491
        Me.Btn_Ok.Text = "„Ê«›ﬁ"
        '
        'Txt_FndByDesc
        '
        Me.Txt_FndByDesc.AlwaysInEditMode = True
        Me.Txt_FndByDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance3.TextHAlignAsString = "Right"
        Me.Txt_FndByDesc.Appearance = Appearance3
        Me.Txt_FndByDesc.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_FndByDesc.Location = New System.Drawing.Point(16, 6)
        Me.Txt_FndByDesc.Name = "Txt_FndByDesc"
        Me.Txt_FndByDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_FndByDesc.Size = New System.Drawing.Size(338, 20)
        Me.Txt_FndByDesc.TabIndex = 490
        Me.Txt_FndByDesc.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Txt_FndByCode
        '
        Me.Txt_FndByCode.AlwaysInEditMode = True
        Me.Txt_FndByCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance4.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance4.TextHAlignAsString = "Right"
        Me.Txt_FndByCode.Appearance = Appearance4
        Me.Txt_FndByCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_FndByCode.Location = New System.Drawing.Point(360, 6)
        Me.Txt_FndByCode.Name = "Txt_FndByCode"
        Me.Txt_FndByCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_FndByCode.Size = New System.Drawing.Size(164, 20)
        Me.Txt_FndByCode.TabIndex = 489
        Me.Txt_FndByCode.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Grd_Main
        '
        Me.Grd_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grd_Main.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Main.DataSource = Me.DTS_Main
        Appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance5.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.None
        Appearance5.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance5.TextHAlignAsString = "Right"
        Me.Grd_Main.DisplayLayout.Appearance = Appearance5
        Me.Grd_Main.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance25.FontData.BoldAsString = "True"
        Appearance25.FontData.Name = "Tahoma"
        Appearance25.FontData.SizeInPoints = 8.0!
        Appearance25.TextHAlignAsString = "Right"
        UltraGridColumn4.CellAppearance = Appearance25
        UltraGridColumn4.Format = ""
        Appearance26.FontData.Name = "Tahoma"
        Appearance26.FontData.SizeInPoints = 8.25!
        Appearance26.TextHAlignAsString = "Right"
        UltraGridColumn4.Header.Appearance = Appearance26
        UltraGridColumn4.Header.Caption = "«·ﬂÊœ"
        UltraGridColumn4.Header.VisiblePosition = 1
        UltraGridColumn4.MaskInput = ""
        UltraGridColumn4.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        UltraGridColumn4.Width = 168
        UltraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance27.FontData.BoldAsString = "True"
        Appearance27.TextHAlignAsString = "Right"
        UltraGridColumn5.CellAppearance = Appearance27
        Appearance28.FontData.Name = "Tahoma"
        Appearance28.FontData.SizeInPoints = 8.25!
        Appearance28.TextHAlignAsString = "Right"
        UltraGridColumn5.Header.Appearance = Appearance28
        UltraGridColumn5.Header.Caption = "«·Ê’›"
        UltraGridColumn5.Header.VisiblePosition = 0
        UltraGridColumn5.Width = 341
        UltraGridColumn6.Header.VisiblePosition = 2
        UltraGridColumn6.Hidden = True
        UltraGridColumn6.Width = 81
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn4, UltraGridColumn5, UltraGridColumn6})
        Appearance29.FontData.Name = "Tahoma"
        Appearance29.FontData.SizeInPoints = 8.0!
        UltraGridBand1.Header.Appearance = Appearance29
        UltraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        UltraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        UltraGridBand1.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.Grd_Main.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Appearance11.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Main.DisplayLayout.Override.AddRowAppearance = Appearance11
        Me.Grd_Main.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom
        Appearance12.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Main.DisplayLayout.Override.CellAppearance = Appearance12
        Appearance13.BorderColor = System.Drawing.SystemColors.Control
        Appearance13.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance13.Image = CType(resources.GetObject("Appearance13.Image"), Object)
        Me.Grd_Main.DisplayLayout.Override.CellButtonAppearance = Appearance13
        Appearance14.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance14.BackColor2 = System.Drawing.SystemColors.Control
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.BorderColor = System.Drawing.SystemColors.Control
        Appearance14.BorderColor3DBase = System.Drawing.SystemColors.Control
        Me.Grd_Main.DisplayLayout.Override.HeaderAppearance = Appearance14
        Me.Grd_Main.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.Standard
        Appearance15.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Main.DisplayLayout.Override.RowAppearance = Appearance15
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance16.BackColor2 = System.Drawing.SystemColors.Control
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance16.BorderColor = System.Drawing.SystemColors.Control
        Appearance16.BorderColor3DBase = System.Drawing.SystemColors.ControlLightLight
        Appearance16.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Main.DisplayLayout.Override.RowSelectorAppearance = Appearance16
        Me.Grd_Main.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement
        Me.Grd_Main.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed
        Appearance17.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Main.DisplayLayout.Override.TemplateAddRowAppearance = Appearance17
        Appearance18.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Main.DisplayLayout.Override.TemplateAddRowCellAppearance = Appearance18
        Appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance19.BackColor2 = System.Drawing.SystemColors.ControlLight
        Appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance19.BorderColor = System.Drawing.SystemColors.Control
        ScrollBarLook1.Appearance = Appearance19
        Appearance20.BackColor = System.Drawing.SystemColors.Control
        Appearance20.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump
        Appearance20.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance20.ForeColor = System.Drawing.SystemColors.AppWorkspace
        ScrollBarLook1.ButtonAppearance = Appearance20
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.HorizontalBump
        ScrollBarLook1.ThumbAppearance = Appearance21
        Appearance22.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance22.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance22.BorderColor = System.Drawing.SystemColors.Control
        Appearance22.BorderColor3DBase = System.Drawing.SystemColors.Control
        ScrollBarLook1.TrackAppearance = Appearance22
        Me.Grd_Main.DisplayLayout.ScrollBarLook = ScrollBarLook1
        Me.Grd_Main.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.Grd_Main.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Appearance23.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance23.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Main.DisplayLayout.SplitterBarHorizontalAppearance = Appearance23
        Appearance24.BackColor = System.Drawing.SystemColors.Control
        Appearance24.BorderColor = System.Drawing.SystemColors.Control
        Appearance24.BorderColor3DBase = System.Drawing.SystemColors.ControlLight
        Me.Grd_Main.DisplayLayout.SplitterBarVerticalAppearance = Appearance24
        Me.Grd_Main.Location = New System.Drawing.Point(3, 32)
        Me.Grd_Main.Name = "Grd_Main"
        Me.Grd_Main.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Grd_Main.Size = New System.Drawing.Size(530, 270)
        Me.Grd_Main.TabIndex = 484
        Me.Grd_Main.UseFlatMode = Infragistics.Win.DefaultableBoolean.[True]
        '
        'ToolBar_Main
        '
        Me.ToolBar_Main.DesignerFlags = 1
        Me.ToolBar_Main.DockWithinContainer = Me
        Me.ToolBar_Main.DockWithinContainerBaseType = GetType(System.Windows.Forms.Form)
        Me.ToolBar_Main.RightAlignedMenus = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.RuntimeCustomizationOptions = Infragistics.Win.UltraWinToolbars.RuntimeCustomizationOptions.None
        Me.ToolBar_Main.ShowFullMenusDelay = 500
        UltraToolbar1.DockedColumn = 0
        UltraToolbar1.DockedRow = 0
        UltraToolbar1.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool1, LabelTool2})
        Appearance50.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance50.BackColor2 = System.Drawing.SystemColors.Control
        Appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        UltraToolbar1.Settings.Appearance = Appearance50
        UltraToolbar1.Settings.FillEntireRow = Infragistics.Win.DefaultableBoolean.[True]
        UltraToolbar1.Text = "ToolBar1"
        Me.ToolBar_Main.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1})
        Appearance51.FontData.BoldAsString = "True"
        Appearance51.FontData.Name = "Tahoma"
        Appearance51.TextHAlignAsString = "Right"
        LabelTool3.SharedPropsInternal.AppearancesSmall.Appearance = Appearance51
        LabelTool3.SharedPropsInternal.Caption = "«·„ÊŸ›Ì‰"
        LabelTool4.SharedPropsInternal.Spring = True
        Me.ToolBar_Main.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool3, LabelTool4})
        '
        '_Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left
        '
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left.Location = New System.Drawing.Point(0, 23)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left.Name = "_Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left"
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 345)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left.ToolbarsManager = Me.ToolBar_Main
        '
        '_Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right
        '
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(536, 23)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right.Name = "_Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right"
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 345)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right.ToolbarsManager = Me.ToolBar_Main
        '
        '_Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top
        '
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top.Name = "_Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top"
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(536, 23)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top.ToolbarsManager = Me.ToolBar_Main
        '
        '_Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom
        '
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 368)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom.Name = "_Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom"
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(536, 0)
        Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.ToolBar_Main
        '
        'Frm_Prima_LovGeneral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(536, 368)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left)
        Me.Controls.Add(Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right)
        Me.Controls.Add(Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top)
        Me.Controls.Add(Me._Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom)
        Me.Name = "Frm_Prima_LovGeneral"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Prima_LovGeneral"
        CType(Me.DTS_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Txt_FndByDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_FndByCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grd_Main, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DTS_Main As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Ok As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Txt_FndByDesc As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Txt_FndByCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Grd_Main As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents ToolBar_Main As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    Friend WithEvents _Frm_Prima_LovGeneral_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Frm_Prima_LovGeneral_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Frm_Prima_LovGeneral_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Frm_Prima_LovGeneral_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
End Class
