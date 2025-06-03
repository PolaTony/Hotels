<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ConfigureMail
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
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Txt_Port = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label2 = New System.Windows.Forms.Label
        Me.Txt_Password = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label1 = New System.Windows.Forms.Label
        Me.Txt_SMTP = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label16 = New System.Windows.Forms.Label
        Me.Txt_Email = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Btn_Save = New Infragistics.Win.Misc.UltraButton
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox1.SuspendLayout()
        CType(Me.Txt_Port, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Password, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_SMTP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Email, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraGroupBox1
        '
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.UltraGroupBox1.Appearance = Appearance1
        Me.UltraGroupBox1.Controls.Add(Me.Btn_Cancel)
        Me.UltraGroupBox1.Controls.Add(Me.Label3)
        Me.UltraGroupBox1.Controls.Add(Me.Txt_Port)
        Me.UltraGroupBox1.Controls.Add(Me.Label2)
        Me.UltraGroupBox1.Controls.Add(Me.Txt_Password)
        Me.UltraGroupBox1.Controls.Add(Me.Label1)
        Me.UltraGroupBox1.Controls.Add(Me.Txt_SMTP)
        Me.UltraGroupBox1.Controls.Add(Me.Label16)
        Me.UltraGroupBox1.Controls.Add(Me.Txt_Email)
        Me.UltraGroupBox1.Controls.Add(Me.Btn_Save)
        Me.UltraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGroupBox1.Name = "UltraGroupBox1"
        Me.UltraGroupBox1.Size = New System.Drawing.Size(539, 186)
        Me.UltraGroupBox1.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(9, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Port"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Port
        '
        Me.Txt_Port.AlwaysInEditMode = True
        Me.Txt_Port.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance5.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_Port.Appearance = Appearance5
        Me.Txt_Port.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Port.Location = New System.Drawing.Point(88, 101)
        Me.Txt_Port.Name = "Txt_Port"
        Me.Txt_Port.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Port.Size = New System.Drawing.Size(426, 20)
        Me.Txt_Port.TabIndex = 36
        Me.Txt_Port.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(9, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Password"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Password
        '
        Me.Txt_Password.AlwaysInEditMode = True
        Me.Txt_Password.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance4.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_Password.Appearance = Appearance4
        Me.Txt_Password.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Password.Location = New System.Drawing.Point(88, 75)
        Me.Txt_Password.Name = "Txt_Password"
        Me.Txt_Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Txt_Password.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Password.Size = New System.Drawing.Size(426, 20)
        Me.Txt_Password.TabIndex = 34
        Me.Txt_Password.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(9, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "SMTP"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_SMTP
        '
        Me.Txt_SMTP.AlwaysInEditMode = True
        Me.Txt_SMTP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_SMTP.Appearance = Appearance3
        Me.Txt_SMTP.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_SMTP.Location = New System.Drawing.Point(88, 23)
        Me.Txt_SMTP.Name = "Txt_SMTP"
        Me.Txt_SMTP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_SMTP.Size = New System.Drawing.Size(426, 20)
        Me.Txt_SMTP.TabIndex = 32
        Me.Txt_SMTP.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(9, 52)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(31, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Email"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Email
        '
        Me.Txt_Email.AlwaysInEditMode = True
        Me.Txt_Email.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance2.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.Txt_Email.Appearance = Appearance2
        Me.Txt_Email.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Email.Location = New System.Drawing.Point(88, 49)
        Me.Txt_Email.Name = "Txt_Email"
        Me.Txt_Email.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Email.Size = New System.Drawing.Size(426, 20)
        Me.Txt_Email.TabIndex = 1
        Me.Txt_Email.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Btn_Save
        '
        Me.Btn_Save.Location = New System.Drawing.Point(426, 144)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(88, 31)
        Me.Btn_Save.TabIndex = 0
        Me.Btn_Save.Text = "Save"
        '
        'Btn_Cancel
        '
        Me.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Btn_Cancel.Location = New System.Drawing.Point(332, 144)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(88, 31)
        Me.Btn_Cancel.TabIndex = 38
        Me.Btn_Cancel.Text = "Cancel"
        '
        'Frm_ConfigureMail
        '
        Me.AcceptButton = Me.Btn_Save
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Btn_Cancel
        Me.ClientSize = New System.Drawing.Size(539, 186)
        Me.ControlBox = False
        Me.Controls.Add(Me.UltraGroupBox1)
        Me.Name = "Frm_ConfigureMail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configure Mail"
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox1.ResumeLayout(False)
        Me.UltraGroupBox1.PerformLayout()
        CType(Me.Txt_Port, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Password, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_SMTP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Email, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Txt_Port As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Txt_Password As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txt_SMTP As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Txt_Email As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Btn_Save As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
End Class
