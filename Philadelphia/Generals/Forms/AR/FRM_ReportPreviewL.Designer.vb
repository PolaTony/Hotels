<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRM_ReportPreviewL
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
        Me.components = New System.ComponentModel.Container()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM_ReportPreviewL))
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraToolbar1 As Infragistics.Win.UltraWinToolbars.UltraToolbar = New Infragistics.Win.UltraWinToolbars.UltraToolbar("ToolBar_FRM")
        Dim LabelTool1 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LBL_FormTitle")
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim LabelTool2 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LBL_FormTitle")
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim PopupMenuTool1 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("BTN_Tools")
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ButtonTool1 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tool1")
        Dim ButtonTool2 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tool1")
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Btn_Close = New Infragistics.Win.Misc.UltraButton()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me._Panel1_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me.UltraToolbarsManager1 = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
        Me._Panel1_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._Panel1_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._Panel1_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me.Panel1.SuspendLayout()
        CType(Me.UltraToolbarsManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Btn_Close)
        Me.Panel1.Controls.Add(Me.CrystalReportViewer1)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Left)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Right)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Bottom)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Top)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1194, 586)
        Me.Panel1.TabIndex = 0
        '
        'Btn_Close
        '
        Me.Btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BorderColor = System.Drawing.Color.White
        Appearance1.Image = CType(resources.GetObject("Appearance1.Image"), Object)
        Appearance1.ImageHAlign = Infragistics.Win.HAlign.Center
        Me.Btn_Close.Appearance = Appearance1
        Me.Btn_Close.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button
        Me.Btn_Close.ImageSize = New System.Drawing.Size(50, 50)
        Me.Btn_Close.Location = New System.Drawing.Point(1114, 37)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(71, 73)
        Me.Btn_Close.TabIndex = 557
        Me.Btn_Close.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 116)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1194, 470)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        '_Panel1_Toolbars_Dock_Area_Left
        '
        Me._Panel1_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.Transparent
        Me._Panel1_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left
        Me._Panel1_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Left.Location = New System.Drawing.Point(0, 31)
        Me._Panel1_Toolbars_Dock_Area_Left.Name = "_Panel1_Toolbars_Dock_Area_Left"
        Me._Panel1_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 555)
        Me._Panel1_Toolbars_Dock_Area_Left.ToolbarsManager = Me.UltraToolbarsManager1
        '
        'UltraToolbarsManager1
        '
        Appearance2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance2.BackColor2 = System.Drawing.SystemColors.Control
        Appearance2.FontData.BoldAsString = "True"
        Appearance2.FontData.Name = "Traditional Arabic"
        Appearance2.FontData.SizeInPoints = 12.0!
        Me.UltraToolbarsManager1.Appearance = Appearance2
        Me.UltraToolbarsManager1.DesignerFlags = 1
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Appearance3.BackColor2 = System.Drawing.Color.Transparent
        Appearance3.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.UltraToolbarsManager1.DockAreaAppearance = Appearance3
        Me.UltraToolbarsManager1.DockWithinContainer = Me.Panel1
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.None
        Me.UltraToolbarsManager1.MenuSettings.Appearance = Appearance4
        Appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance5.BackColor2 = System.Drawing.SystemColors.Control
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Me.UltraToolbarsManager1.MenuSettings.IconAreaAppearance = Appearance5
        Appearance6.ImageHAlign = Infragistics.Win.HAlign.Right
        Appearance6.TextHAlignAsString = "Left"
        Me.UltraToolbarsManager1.MenuSettings.ToolAppearance = Appearance6
        Me.UltraToolbarsManager1.ShowFullMenusDelay = 500
        Me.UltraToolbarsManager1.ShowQuickCustomizeButton = False
        Me.UltraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003
        UltraToolbar1.DockedColumn = 0
        UltraToolbar1.DockedRow = 0
        LabelTool1.InstanceProps.IsFirstInGroup = True
        UltraToolbar1.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool1})
        Appearance7.FontData.SizeInPoints = 12.0!
        UltraToolbar1.Settings.Appearance = Appearance7
        UltraToolbar1.Settings.FillEntireRow = Infragistics.Win.DefaultableBoolean.[True]
        UltraToolbar1.Text = "ToolBar_FRM"
        Me.UltraToolbarsManager1.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1})
        Me.UltraToolbarsManager1.ToolbarSettings.AllowCustomize = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraToolbarsManager1.ToolbarSettings.AllowDockBottom = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraToolbarsManager1.ToolbarSettings.AllowDockLeft = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraToolbarsManager1.ToolbarSettings.AllowDockRight = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraToolbarsManager1.ToolbarSettings.AllowDockTop = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraToolbarsManager1.ToolbarSettings.AllowFloating = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraToolbarsManager1.ToolbarSettings.AllowHiding = Infragistics.Win.DefaultableBoolean.[False]
        Appearance8.ImageHAlign = Infragistics.Win.HAlign.Center
        Appearance8.TextHAlignAsString = "Left"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraToolbarsManager1.ToolbarSettings.ToolAppearance = Appearance8
        Me.UltraToolbarsManager1.ToolbarSettings.ToolDisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText
        Appearance9.FontData.BoldAsString = "True"
        Appearance9.FontData.Name = "Traditional Arabic"
        Appearance9.FontData.SizeInPoints = 12.0!
        Appearance9.TextHAlignAsString = "Right"
        LabelTool2.SharedPropsInternal.AppearancesSmall.Appearance = Appearance9
        LabelTool2.SharedPropsInternal.Caption = " ﬁ«—Ì—"
        LabelTool2.SharedPropsInternal.Spring = True
        PopupMenuTool1.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.None
        Appearance10.Image = CType(resources.GetObject("Appearance10.Image"), Object)
        PopupMenuTool1.SharedPropsInternal.AppearancesSmall.Appearance = Appearance10
        PopupMenuTool1.SharedPropsInternal.Caption = "√œÊ« "
        PopupMenuTool1.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool1})
        ButtonTool2.SharedPropsInternal.Caption = "Tool1"
        Me.UltraToolbarsManager1.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool2, PopupMenuTool1, ButtonTool2})
        '
        '_Panel1_Toolbars_Dock_Area_Right
        '
        Me._Panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.Transparent
        Me._Panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right
        Me._Panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(1194, 31)
        Me._Panel1_Toolbars_Dock_Area_Right.Name = "_Panel1_Toolbars_Dock_Area_Right"
        Me._Panel1_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 555)
        Me._Panel1_Toolbars_Dock_Area_Right.ToolbarsManager = Me.UltraToolbarsManager1
        '
        '_Panel1_Toolbars_Dock_Area_Bottom
        '
        Me._Panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.Transparent
        Me._Panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
        Me._Panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 586)
        Me._Panel1_Toolbars_Dock_Area_Bottom.Name = "_Panel1_Toolbars_Dock_Area_Bottom"
        Me._Panel1_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(1194, 0)
        Me._Panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.UltraToolbarsManager1
        '
        '_Panel1_Toolbars_Dock_Area_Top
        '
        Me._Panel1_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.Transparent
        Me._Panel1_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
        Me._Panel1_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0)
        Me._Panel1_Toolbars_Dock_Area_Top.Name = "_Panel1_Toolbars_Dock_Area_Top"
        Me._Panel1_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(1194, 31)
        Me._Panel1_Toolbars_Dock_Area_Top.ToolbarsManager = Me.UltraToolbarsManager1
        '
        'FRM_ReportPreviewL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1194, 586)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FRM_ReportPreviewL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FRM_ReportPreviewL"
        Me.Panel1.ResumeLayout(False)
        CType(Me.UltraToolbarsManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents UltraToolbarsManager1 As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents Btn_Close As Infragistics.Win.Misc.UltraButton
End Class
