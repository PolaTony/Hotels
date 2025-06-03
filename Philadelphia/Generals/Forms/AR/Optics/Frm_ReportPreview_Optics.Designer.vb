<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ReportPreview_Optics
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
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraToolbar2 As Infragistics.Win.UltraWinToolbars.UltraToolbar = New Infragistics.Win.UltraWinToolbars.UltraToolbar("ToolBar_FRM")
        Dim LabelTool1 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LBL_FormTitle")
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim LabelTool2 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LBL_FormTitle")
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim PopupMenuTool1 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("BTN_Tools")
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_ReportPreview_Optics))
        Dim ButtonTool1 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tool1")
        Dim ButtonTool2 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Tool1")
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me._Panel1_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me.ToolBar_Main = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
        Me._Panel1_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Panel1_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Panel1_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me.Panel1.SuspendLayout()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CrystalReportViewer1)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Left)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Right)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Top)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Bottom)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1194, 746)
        Me.Panel1.TabIndex = 1
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 31)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1194, 715)
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
        Me._Panel1_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 715)
        Me._Panel1_Toolbars_Dock_Area_Left.ToolbarsManager = Me.ToolBar_Main
        '
        'ToolBar_Main
        '
        Appearance11.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance11.BackColor2 = System.Drawing.SystemColors.Control
        Appearance11.FontData.BoldAsString = "True"
        Appearance11.FontData.Name = "Traditional Arabic"
        Appearance11.FontData.SizeInPoints = 12.0!
        Me.ToolBar_Main.Appearance = Appearance11
        Me.ToolBar_Main.DesignerFlags = 1
        Appearance12.BackColor = System.Drawing.Color.Transparent
        Appearance12.BackColor2 = System.Drawing.Color.Transparent
        Appearance12.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.ToolBar_Main.DockAreaAppearance = Appearance12
        Me.ToolBar_Main.DockWithinContainer = Me.Panel1
        Appearance13.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.None
        Me.ToolBar_Main.MenuSettings.Appearance = Appearance13
        Appearance14.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance14.BackColor2 = System.Drawing.SystemColors.Control
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Me.ToolBar_Main.MenuSettings.IconAreaAppearance = Appearance14
        Appearance15.ImageHAlign = Infragistics.Win.HAlign.Right
        Appearance15.TextHAlignAsString = "Left"
        Me.ToolBar_Main.MenuSettings.ToolAppearance = Appearance15
        Me.ToolBar_Main.ShowFullMenusDelay = 500
        Me.ToolBar_Main.ShowQuickCustomizeButton = False
        Me.ToolBar_Main.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003
        UltraToolbar2.DockedColumn = 0
        UltraToolbar2.DockedRow = 0
        LabelTool1.InstanceProps.IsFirstInGroup = True
        UltraToolbar2.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool1})
        Appearance26.FontData.SizeInPoints = 12.0!
        UltraToolbar2.Settings.Appearance = Appearance26
        UltraToolbar2.Settings.FillEntireRow = Infragistics.Win.DefaultableBoolean.[True]
        UltraToolbar2.Text = "ToolBar_FRM"
        Me.ToolBar_Main.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar2})
        Me.ToolBar_Main.ToolbarSettings.AllowCustomize = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.ToolbarSettings.AllowDockBottom = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.ToolbarSettings.AllowDockLeft = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.ToolbarSettings.AllowDockRight = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.ToolbarSettings.AllowDockTop = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.ToolbarSettings.AllowFloating = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.ToolbarSettings.AllowHiding = Infragistics.Win.DefaultableBoolean.[False]
        Appearance27.ImageHAlign = Infragistics.Win.HAlign.Center
        Appearance27.TextHAlignAsString = "Left"
        Appearance27.TextVAlignAsString = "Middle"
        Me.ToolBar_Main.ToolbarSettings.ToolAppearance = Appearance27
        Me.ToolBar_Main.ToolbarSettings.ToolDisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText
        Appearance28.FontData.BoldAsString = "True"
        Appearance28.FontData.Name = "Traditional Arabic"
        Appearance28.FontData.SizeInPoints = 12.0!
        Appearance28.TextHAlignAsString = "Right"
        LabelTool2.SharedPropsInternal.AppearancesSmall.Appearance = Appearance28
        LabelTool2.SharedPropsInternal.Caption = "تقارير"
        LabelTool2.SharedPropsInternal.Spring = True
        PopupMenuTool1.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.None
        Appearance29.Image = CType(resources.GetObject("Appearance29.Image"), Object)
        PopupMenuTool1.SharedPropsInternal.AppearancesSmall.Appearance = Appearance29
        PopupMenuTool1.SharedPropsInternal.Caption = "أدوات"
        PopupMenuTool1.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool1})
        ButtonTool2.SharedPropsInternal.Caption = "Tool1"
        Me.ToolBar_Main.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool2, PopupMenuTool1, ButtonTool2})
        '
        '_Panel1_Toolbars_Dock_Area_Right
        '
        Me._Panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.Transparent
        Me._Panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right
        Me._Panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(1194, 31)
        Me._Panel1_Toolbars_Dock_Area_Right.Name = "_Panel1_Toolbars_Dock_Area_Right"
        Me._Panel1_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 715)
        Me._Panel1_Toolbars_Dock_Area_Right.ToolbarsManager = Me.ToolBar_Main
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
        Me._Panel1_Toolbars_Dock_Area_Top.ToolbarsManager = Me.ToolBar_Main
        '
        '_Panel1_Toolbars_Dock_Area_Bottom
        '
        Me._Panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.Transparent
        Me._Panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
        Me._Panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 746)
        Me._Panel1_Toolbars_Dock_Area_Bottom.Name = "_Panel1_Toolbars_Dock_Area_Bottom"
        Me._Panel1_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(1194, 0)
        Me._Panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.ToolBar_Main
        '
        'Frm_ReportPreview_Optics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1194, 746)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_ReportPreview_Optics"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "التقارير"
        Me.Panel1.ResumeLayout(False)
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents ToolBar_Main As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
End Class
