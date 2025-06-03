<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_NewMail
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance102 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox
        Me.Lbl_Attachments_Desc = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.UltraGroupBox2 = New Infragistics.Win.Misc.UltraGroupBox
        Me.Txt_Body = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label3 = New System.Windows.Forms.Label
        Me.Txt_Subject = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label2 = New System.Windows.Forms.Label
        Me.Txt_Bcc = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label1 = New System.Windows.Forms.Label
        Me.Txt_CC = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label16 = New System.Windows.Forms.Label
        Me.Txt_To = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.UltraButton1 = New Infragistics.Win.Misc.UltraButton
        Me.UltraButton2 = New Infragistics.Win.Misc.UltraButton
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox1.SuspendLayout()
        CType(Me.UltraGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox2.SuspendLayout()
        CType(Me.Txt_Body, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Subject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Bcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_CC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_To, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraGroupBox1
        '
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.UltraGroupBox1.Appearance = Appearance1
        Me.UltraGroupBox1.Controls.Add(Me.UltraButton2)
        Me.UltraGroupBox1.Controls.Add(Me.Lbl_Attachments_Desc)
        Me.UltraGroupBox1.Controls.Add(Me.Label4)
        Me.UltraGroupBox1.Controls.Add(Me.UltraGroupBox2)
        Me.UltraGroupBox1.Controls.Add(Me.Label3)
        Me.UltraGroupBox1.Controls.Add(Me.Txt_Subject)
        Me.UltraGroupBox1.Controls.Add(Me.Label2)
        Me.UltraGroupBox1.Controls.Add(Me.Txt_Bcc)
        Me.UltraGroupBox1.Controls.Add(Me.Label1)
        Me.UltraGroupBox1.Controls.Add(Me.Txt_CC)
        Me.UltraGroupBox1.Controls.Add(Me.Label16)
        Me.UltraGroupBox1.Controls.Add(Me.Txt_To)
        Me.UltraGroupBox1.Controls.Add(Me.UltraButton1)
        Me.UltraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGroupBox1.Name = "UltraGroupBox1"
        Me.UltraGroupBox1.Size = New System.Drawing.Size(539, 319)
        Me.UltraGroupBox1.TabIndex = 2
        '
        'Lbl_Attachments_Desc
        '
        Me.Lbl_Attachments_Desc.AutoSize = True
        Me.Lbl_Attachments_Desc.BackColor = System.Drawing.Color.Transparent
        Me.Lbl_Attachments_Desc.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Lbl_Attachments_Desc.Location = New System.Drawing.Point(90, 32)
        Me.Lbl_Attachments_Desc.Name = "Lbl_Attachments_Desc"
        Me.Lbl_Attachments_Desc.Size = New System.Drawing.Size(0, 13)
        Me.Lbl_Attachments_Desc.TabIndex = 40
        Me.Lbl_Attachments_Desc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(14, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Attachments"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UltraGroupBox2
        '
        Me.UltraGroupBox2.Controls.Add(Me.Txt_Body)
        Me.UltraGroupBox2.Location = New System.Drawing.Point(93, 167)
        Me.UltraGroupBox2.Name = "UltraGroupBox2"
        Me.UltraGroupBox2.Size = New System.Drawing.Size(429, 107)
        Me.UltraGroupBox2.TabIndex = 38
        '
        'Txt_Body
        '
        Me.Txt_Body.AlwaysInEditMode = True
        Appearance102.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_Body.Appearance = Appearance102
        Me.Txt_Body.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Txt_Body.Location = New System.Drawing.Point(3, 0)
        Me.Txt_Body.Multiline = True
        Me.Txt_Body.Name = "Txt_Body"
        Me.Txt_Body.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Body.Size = New System.Drawing.Size(423, 104)
        Me.Txt_Body.TabIndex = 0
        Me.Txt_Body.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(14, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Subject"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Subject
        '
        Me.Txt_Subject.AlwaysInEditMode = True
        Me.Txt_Subject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance5.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_Subject.Appearance = Appearance5
        Me.Txt_Subject.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Subject.Location = New System.Drawing.Point(93, 141)
        Me.Txt_Subject.Name = "Txt_Subject"
        Me.Txt_Subject.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Subject.Size = New System.Drawing.Size(426, 20)
        Me.Txt_Subject.TabIndex = 3
        Me.Txt_Subject.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(14, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Bcc"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Bcc
        '
        Me.Txt_Bcc.AlwaysInEditMode = True
        Me.Txt_Bcc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance4.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_Bcc.Appearance = Appearance4
        Me.Txt_Bcc.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Bcc.Location = New System.Drawing.Point(93, 115)
        Me.Txt_Bcc.Name = "Txt_Bcc"
        Me.Txt_Bcc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Bcc.Size = New System.Drawing.Size(426, 20)
        Me.Txt_Bcc.TabIndex = 2
        Me.Txt_Bcc.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(14, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "CC"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_CC
        '
        Me.Txt_CC.AlwaysInEditMode = True
        Me.Txt_CC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_CC.Appearance = Appearance3
        Me.Txt_CC.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_CC.Location = New System.Drawing.Point(93, 89)
        Me.Txt_CC.Name = "Txt_CC"
        Me.Txt_CC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_CC.Size = New System.Drawing.Size(426, 20)
        Me.Txt_CC.TabIndex = 1
        Me.Txt_CC.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(14, 66)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(19, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "To"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_To
        '
        Me.Txt_To.AlwaysInEditMode = True
        Me.Txt_To.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance2.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_To.Appearance = Appearance2
        Me.Txt_To.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_To.Location = New System.Drawing.Point(93, 63)
        Me.Txt_To.Name = "Txt_To"
        Me.Txt_To.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_To.Size = New System.Drawing.Size(426, 20)
        Me.Txt_To.TabIndex = 0
        Me.Txt_To.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'UltraButton1
        '
        Me.UltraButton1.Location = New System.Drawing.Point(426, 280)
        Me.UltraButton1.Name = "UltraButton1"
        Me.UltraButton1.Size = New System.Drawing.Size(88, 31)
        Me.UltraButton1.TabIndex = 0
        Me.UltraButton1.Text = "Send"
        '
        'UltraButton2
        '
        Me.UltraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.UltraButton2.Location = New System.Drawing.Point(323, 280)
        Me.UltraButton2.Name = "UltraButton2"
        Me.UltraButton2.Size = New System.Drawing.Size(88, 31)
        Me.UltraButton2.TabIndex = 41
        Me.UltraButton2.Text = "Cancel"
        '
        'Frm_NewMail_L
        '
        Me.AcceptButton = Me.UltraButton1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.UltraButton2
        Me.ClientSize = New System.Drawing.Size(539, 319)
        Me.ControlBox = False
        Me.Controls.Add(Me.UltraGroupBox1)
        Me.Name = "Frm_NewMail_L"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Mail"
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox1.ResumeLayout(False)
        Me.UltraGroupBox1.PerformLayout()
        CType(Me.UltraGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox2.ResumeLayout(False)
        Me.UltraGroupBox2.PerformLayout()
        CType(Me.Txt_Body, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Subject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Bcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_CC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_To, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents UltraGroupBox2 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents Txt_Body As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Txt_Subject As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Txt_Bcc As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txt_CC As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Txt_To As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraButton1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Lbl_Attachments_Desc As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents UltraButton2 As Infragistics.Win.Misc.UltraButton
End Class
