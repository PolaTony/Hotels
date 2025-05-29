<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_ImportFromExcel_A
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Txt_FileName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Btn_Browse = New System.Windows.Forms.Button()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.Txt_Sheets = New System.Windows.Forms.ComboBox()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.Grd_Main = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton()
        Me.Btn_Save = New Infragistics.Win.Misc.UltraButton()
        CType(Me.Txt_FileName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grd_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Txt_FileName
        '
        Me.Txt_FileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txt_FileName.Location = New System.Drawing.Point(70, 355)
        Me.Txt_FileName.Name = "Txt_FileName"
        Me.Txt_FileName.ReadOnly = True
        Me.Txt_FileName.Size = New System.Drawing.Size(770, 22)
        Me.Txt_FileName.TabIndex = 1
        '
        'Btn_Browse
        '
        Me.Btn_Browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Browse.Location = New System.Drawing.Point(860, 355)
        Me.Btn_Browse.Name = "Btn_Browse"
        Me.Btn_Browse.Size = New System.Drawing.Size(82, 23)
        Me.Btn_Browse.TabIndex = 2
        Me.Btn_Browse.Text = "Browse"
        Me.Btn_Browse.UseVisualStyleBackColor = True
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(12, 359)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(52, 15)
        Me.UltraLabel1.TabIndex = 3
        Me.UltraLabel1.Text = "File Name"
        '
        'Txt_Sheets
        '
        Me.Txt_Sheets.FormattingEnabled = True
        Me.Txt_Sheets.Location = New System.Drawing.Point(70, 416)
        Me.Txt_Sheets.Name = "Txt_Sheets"
        Me.Txt_Sheets.Size = New System.Drawing.Size(180, 21)
        Me.Txt_Sheets.TabIndex = 4
        Me.Txt_Sheets.Visible = False
        '
        'UltraLabel2
        '
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(12, 419)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(36, 15)
        Me.UltraLabel2.TabIndex = 5
        Me.UltraLabel2.Text = "Sheets"
        Me.UltraLabel2.Visible = False
        '
        'Grd_Main
        '
        Me.Grd_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.Grd_Main.DisplayLayout.Appearance = Appearance1
        Me.Grd_Main.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Grd_Main.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.Grd_Main.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Grd_Main.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.Grd_Main.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Grd_Main.DisplayLayout.GroupByBox.Hidden = True
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Grd_Main.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.Grd_Main.DisplayLayout.MaxColScrollRegions = 1
        Me.Grd_Main.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Grd_Main.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.Grd_Main.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.Grd_Main.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.Grd_Main.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.Grd_Main.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.Grd_Main.DisplayLayout.Override.CellAppearance = Appearance8
        Me.Grd_Main.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.Grd_Main.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.Grd_Main.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.Grd_Main.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.Grd_Main.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.Grd_Main.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.Grd_Main.DisplayLayout.Override.RowAppearance = Appearance11
        Me.Grd_Main.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Main.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.Grd_Main.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.Grd_Main.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.Grd_Main.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.Grd_Main.Location = New System.Drawing.Point(12, 12)
        Me.Grd_Main.Name = "Grd_Main"
        Me.Grd_Main.Size = New System.Drawing.Size(930, 337)
        Me.Grd_Main.TabIndex = 6
        Me.Grd_Main.Text = "UltraGrid1"
        '
        'Btn_Cancel
        '
        Me.Btn_Cancel.Location = New System.Drawing.Point(761, 392)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(79, 69)
        Me.Btn_Cancel.TabIndex = 604
        Me.Btn_Cancel.Text = "Cancel"
        '
        'Btn_Save
        '
        Me.Btn_Save.Location = New System.Drawing.Point(860, 392)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(82, 69)
        Me.Btn_Save.TabIndex = 605
        Me.Btn_Save.Text = "Insert"
        '
        'Frm_ImportFromExcel_A
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 480)
        Me.ControlBox = False
        Me.Controls.Add(Me.Btn_Save)
        Me.Controls.Add(Me.Btn_Cancel)
        Me.Controls.Add(Me.Grd_Main)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.Txt_Sheets)
        Me.Controls.Add(Me.UltraLabel1)
        Me.Controls.Add(Me.Btn_Browse)
        Me.Controls.Add(Me.Txt_FileName)
        Me.Name = "Frm_ImportFromExcel_A"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import From Excel"
        CType(Me.Txt_FileName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grd_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Txt_FileName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Btn_Browse As Button
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Txt_Sheets As ComboBox
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Grd_Main As Infragistics.Win.UltraWinGrid.UltraGrid
    'Private WithEvents Btn_Department_Clear As Telerik.WinControls.UI.RadButton
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Save As Infragistics.Win.Misc.UltraButton
End Class
