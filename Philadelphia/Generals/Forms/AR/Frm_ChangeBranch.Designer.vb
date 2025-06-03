<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ChangeBranch
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
        Dim Appearance49 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance50 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance51 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim ValueListItem3 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_ChangeBranch))
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.Label6 = New System.Windows.Forms.Label
        Me.Txt_Branches = New Infragistics.Win.UltraWinEditors.UltraComboEditor
        Me.Btn_Ok = New Infragistics.Win.Misc.UltraButton
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        CType(Me.Txt_Branches, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(265, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 261
        Me.Label6.Text = "الفرع"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Txt_Branches
        '
        Me.Txt_Branches.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance49.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance49.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance49.TextHAlignAsString = "Right"
        Me.Txt_Branches.Appearance = Appearance49
        Me.Txt_Branches.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance50.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_Branches.ButtonAppearance = Appearance50
        Me.Txt_Branches.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_Branches.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_Branches.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList
        Appearance51.TextHAlignAsString = "Right"
        Me.Txt_Branches.ItemAppearance = Appearance51
        ValueListItem1.DataValue = "C"
        ValueListItem1.DisplayText = "نقدي"
        ValueListItem2.DataValue = "D"
        ValueListItem2.DisplayText = "آجل"
        ValueListItem3.DataValue = "V"
        ValueListItem3.DisplayText = "فيزا"
        Me.Txt_Branches.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3})
        Me.Txt_Branches.Location = New System.Drawing.Point(12, 25)
        Me.Txt_Branches.Name = "Txt_Branches"
        Me.Txt_Branches.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Branches.Size = New System.Drawing.Size(247, 20)
        Me.Txt_Branches.TabIndex = 262
        Me.Txt_Branches.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Btn_Ok
        '
        Me.Btn_Ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance6.FontData.BoldAsString = "False"
        Appearance6.FontData.SizeInPoints = 10.0!
        Appearance6.Image = CType(resources.GetObject("Appearance6.Image"), Object)
        Appearance6.ImageHAlign = Infragistics.Win.HAlign.Left
        Appearance6.TextHAlignAsString = "Right"
        Me.Btn_Ok.Appearance = Appearance6
        Me.Btn_Ok.Location = New System.Drawing.Point(221, 71)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(75, 30)
        Me.Btn_Ok.TabIndex = 263
        Me.Btn_Ok.Text = "موافق"
        '
        'Btn_Cancel
        '
        Me.Btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance7.FontData.SizeInPoints = 10.0!
        Appearance7.Image = CType(resources.GetObject("Appearance7.Image"), Object)
        Appearance7.TextHAlignAsString = "Right"
        Me.Btn_Cancel.Appearance = Appearance7
        Me.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Btn_Cancel.Location = New System.Drawing.Point(140, 71)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(75, 30)
        Me.Btn_Cancel.TabIndex = 264
        Me.Btn_Cancel.Text = "الغاء"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 55)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(310, 2)
        Me.GroupBox2.TabIndex = 485
        Me.GroupBox2.TabStop = False
        '
        'Frm_ChangeBranch
        '
        Me.AcceptButton = Me.Btn_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Btn_Cancel
        Me.ClientSize = New System.Drawing.Size(318, 113)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Btn_Ok)
        Me.Controls.Add(Me.Btn_Cancel)
        Me.Controls.Add(Me.Txt_Branches)
        Me.Controls.Add(Me.Label6)
        Me.Name = "Frm_ChangeBranch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "اختار الفرع"
        CType(Me.Txt_Branches, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Txt_Branches As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents Btn_Ok As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
