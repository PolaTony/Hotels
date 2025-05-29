<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PicturePreview
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
        Dim UltraToolbar1 As Infragistics.Win.UltraWinToolbars.UltraToolbar = New Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1")
        Dim LabelTool1 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim PopupMenuTool1 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tools")
        Dim LabelTool2 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool1")
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim LabelTool3 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool1")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim LabelTool4 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim PopupMenuTool2 As Infragistics.Win.UltraWinToolbars.PopupMenuTool = New Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tools")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_PicturePreview))
        Dim ButtonTool1 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Print")
        Dim ButtonTool2 As Infragistics.Win.UltraWinToolbars.ButtonTool = New Infragistics.Win.UltraWinToolbars.ButtonTool("Print")
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.UltraPictureBox1 = New Infragistics.Win.UltraWinEditors.UltraPictureBox
        Me.ToolBar_Main = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraPictureBox1
        '
        Me.UltraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty
        Me.UltraPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraPictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPictureBox1.Name = "UltraPictureBox1"
        Me.UltraPictureBox1.Size = New System.Drawing.Size(830, 504)
        Me.UltraPictureBox1.TabIndex = 0
        '
        'ToolBar_Main
        '
        Me.ToolBar_Main.DesignerFlags = 1
        Me.ToolBar_Main.DockWithinContainer = Me
        Me.ToolBar_Main.DockWithinContainerBaseType = GetType(System.Windows.Forms.Form)
        Me.ToolBar_Main.RightAlignedMenus = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.RuntimeCustomizationOptions = Infragistics.Win.UltraWinToolbars.RuntimeCustomizationOptions.None
        Me.ToolBar_Main.ShowFullMenusDelay = 500
        Me.ToolBar_Main.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.OfficeXP
        UltraToolbar1.DockedColumn = 0
        UltraToolbar1.DockedRow = 0
        UltraToolbar1.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool1, PopupMenuTool1, LabelTool2})
        Appearance1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance1.BackColor2 = System.Drawing.SystemColors.Control
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        UltraToolbar1.Settings.Appearance = Appearance1
        UltraToolbar1.Settings.CaptionPlacement = Infragistics.Win.TextPlacement.LeftOfImage
        UltraToolbar1.Settings.FillEntireRow = Infragistics.Win.DefaultableBoolean.[True]
        UltraToolbar1.Text = "ToolBar1"
        Me.ToolBar_Main.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1})
        Appearance2.FontData.BoldAsString = "True"
        Appearance2.FontData.Name = "Tahoma"
        Appearance2.TextHAlignAsString = "Right"
        LabelTool3.SharedPropsInternal.AppearancesSmall.Appearance = Appearance2
        LabelTool3.SharedPropsInternal.Caption = "›« Ê—… ‘—«¡"
        LabelTool3.SharedPropsInternal.Visible = False
        LabelTool4.SharedPropsInternal.Spring = True
        LabelTool4.SharedPropsInternal.Visible = False
        Appearance3.FontData.BoldAsString = "True"
        Appearance3.FontData.Name = "Tahoma"
        Appearance3.FontData.SizeInPoints = 12.0!
        Appearance3.Image = CType(resources.GetObject("Appearance3.Image"), Object)
        PopupMenuTool2.SharedPropsInternal.AppearancesSmall.Appearance = Appearance3
        PopupMenuTool2.SharedPropsInternal.Caption = "√œÊ« "
        PopupMenuTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText
        PopupMenuTool2.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {ButtonTool1})
        Appearance4.FontData.BoldAsString = "True"
        Appearance4.FontData.Name = "Tahoma"
        Appearance4.Image = CType(resources.GetObject("Appearance4.Image"), Object)
        ButtonTool2.SharedPropsInternal.AppearancesSmall.Appearance = Appearance4
        ButtonTool2.SharedPropsInternal.Caption = "ÿ»«⁄…"
        Me.ToolBar_Main.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool3, LabelTool4, PopupMenuTool2, ButtonTool2})
        '
        '_Frm_ImagePreview_Toolbars_Dock_Area_Left
        '
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left.Location = New System.Drawing.Point(0, 23)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left.Name = "_Frm_ImagePreview_Toolbars_Dock_Area_Left"
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 504)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Left.ToolbarsManager = Me.ToolBar_Main
        '
        '_Frm_ImagePreview_Toolbars_Dock_Area_Right
        '
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(830, 23)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right.Name = "_Frm_ImagePreview_Toolbars_Dock_Area_Right"
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 504)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Right.ToolbarsManager = Me.ToolBar_Main
        '
        '_Frm_ImagePreview_Toolbars_Dock_Area_Top
        '
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top.Name = "_Frm_ImagePreview_Toolbars_Dock_Area_Top"
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(830, 23)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Top.ToolbarsManager = Me.ToolBar_Main
        '
        '_Frm_ImagePreview_Toolbars_Dock_Area_Bottom
        '
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 527)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom.Name = "_Frm_ImagePreview_Toolbars_Dock_Area_Bottom"
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(830, 0)
        Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.ToolBar_Main
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UltraPictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(830, 504)
        Me.Panel1.TabIndex = 5
        '
        'Frm_PicturePreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 527)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me._Frm_ImagePreview_Toolbars_Dock_Area_Left)
        Me.Controls.Add(Me._Frm_ImagePreview_Toolbars_Dock_Area_Right)
        Me.Controls.Add(Me._Frm_ImagePreview_Toolbars_Dock_Area_Top)
        Me.Controls.Add(Me._Frm_ImagePreview_Toolbars_Dock_Area_Bottom)
        Me.Name = "Frm_PicturePreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "⁄—÷ «·’Ê—"
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraPictureBox1 As Infragistics.Win.UltraWinEditors.UltraPictureBox
    Friend WithEvents ToolBar_Main As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    Friend WithEvents _Frm_ImagePreview_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Frm_ImagePreview_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Frm_ImagePreview_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Frm_ImagePreview_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
