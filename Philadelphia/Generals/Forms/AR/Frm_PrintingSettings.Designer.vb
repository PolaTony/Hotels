<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PrintingSettings
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_PrintingSettings))
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Txt_Top = New Infragistics.Win.UltraWinEditors.UltraNumericEditor()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_Bottom = New Infragistics.Win.UltraWinEditors.UltraNumericEditor()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Txt_Right = New Infragistics.Win.UltraWinEditors.UltraNumericEditor()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Txt_Left = New Infragistics.Win.UltraWinEditors.UltraNumericEditor()
        Me.UltraRadioButtonGroupManager1 = New Infragistics.Win.UltraWinEditors.UltraRadioButtonGroupManager(Me.components)
        Me.Chk_Portrait = New Infragistics.Win.UltraWinEditors.UltraRadioButton()
        Me.Chk_Landscape = New Infragistics.Win.UltraWinEditors.UltraRadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Btn_Ok = New System.Windows.Forms.Button()
        CType(Me.Txt_Top, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Bottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Right, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Left, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraRadioButtonGroupManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chk_Portrait, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chk_Landscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Txt_Top
        '
        Me.Txt_Top.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance1.TextHAlignAsString = "Right"
        Me.Txt_Top.Appearance = Appearance1
        Me.Txt_Top.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Top.Location = New System.Drawing.Point(44, 19)
        Me.Txt_Top.Name = "Txt_Top"
        Me.Txt_Top.Nullable = True
        Me.Txt_Top.NumericType = Infragistics.Win.UltraWinEditors.NumericType.[Double]
        Me.Txt_Top.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.Txt_Top.Size = New System.Drawing.Size(90, 20)
        Me.Txt_Top.TabIndex = 14
        Me.Txt_Top.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_Top.Value = 10.0R
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label12.Location = New System.Drawing.Point(148, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(27, 13)
        Me.Label12.TabIndex = 539
        Me.Label12.Text = "فوق"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label1.Location = New System.Drawing.Point(148, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 541
        Me.Label1.Text = "تحت"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Bottom
        '
        Me.Txt_Bottom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance2.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance2.TextHAlignAsString = "Right"
        Me.Txt_Bottom.Appearance = Appearance2
        Me.Txt_Bottom.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Bottom.Location = New System.Drawing.Point(44, 45)
        Me.Txt_Bottom.Name = "Txt_Bottom"
        Me.Txt_Bottom.Nullable = True
        Me.Txt_Bottom.NumericType = Infragistics.Win.UltraWinEditors.NumericType.[Double]
        Me.Txt_Bottom.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.Txt_Bottom.Size = New System.Drawing.Size(90, 20)
        Me.Txt_Bottom.TabIndex = 540
        Me.Txt_Bottom.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_Bottom.Value = 10.0R
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label2.Location = New System.Drawing.Point(146, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 545
        Me.Label2.Text = "يمين"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Right
        '
        Me.Txt_Right.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance3.TextHAlignAsString = "Right"
        Me.Txt_Right.Appearance = Appearance3
        Me.Txt_Right.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Right.Location = New System.Drawing.Point(44, 97)
        Me.Txt_Right.Name = "Txt_Right"
        Me.Txt_Right.Nullable = True
        Me.Txt_Right.NumericType = Infragistics.Win.UltraWinEditors.NumericType.[Double]
        Me.Txt_Right.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.Txt_Right.Size = New System.Drawing.Size(90, 20)
        Me.Txt_Right.TabIndex = 544
        Me.Txt_Right.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_Right.Value = 10.0R
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label3.Location = New System.Drawing.Point(140, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 543
        Me.Label3.Text = "شمال"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Left
        '
        Me.Txt_Left.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance4.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance4.TextHAlignAsString = "Right"
        Me.Txt_Left.Appearance = Appearance4
        Me.Txt_Left.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Left.Location = New System.Drawing.Point(44, 71)
        Me.Txt_Left.Name = "Txt_Left"
        Me.Txt_Left.Nullable = True
        Me.Txt_Left.NumericType = Infragistics.Win.UltraWinEditors.NumericType.[Double]
        Me.Txt_Left.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.Txt_Left.Size = New System.Drawing.Size(90, 20)
        Me.Txt_Left.TabIndex = 542
        Me.Txt_Left.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_Left.Value = 10.0R
        '
        'Chk_Portrait
        '
        Appearance5.Image = CType(resources.GetObject("Appearance5.Image"), Object)
        Appearance5.ImageHAlign = Infragistics.Win.HAlign.Center
        Me.Chk_Portrait.Appearance = Appearance5
        Me.Chk_Portrait.GroupManager = Me.UltraRadioButtonGroupManager1
        Me.Chk_Portrait.Location = New System.Drawing.Point(103, 26)
        Me.Chk_Portrait.Name = "Chk_Portrait"
        Me.Chk_Portrait.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Chk_Portrait.Size = New System.Drawing.Size(89, 75)
        Me.Chk_Portrait.TabIndex = 546
        Me.Chk_Portrait.TabStop = False
        '
        'Chk_Landscape
        '
        Appearance6.Image = CType(resources.GetObject("Appearance6.Image"), Object)
        Appearance6.ImageHAlign = Infragistics.Win.HAlign.Center
        Me.Chk_Landscape.Appearance = Appearance6
        Me.Chk_Landscape.Checked = True
        Me.Chk_Landscape.GroupManager = Me.UltraRadioButtonGroupManager1
        Me.Chk_Landscape.Location = New System.Drawing.Point(4, 36)
        Me.Chk_Landscape.Name = "Chk_Landscape"
        Me.Chk_Landscape.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Chk_Landscape.Size = New System.Drawing.Size(106, 58)
        Me.Chk_Landscape.TabIndex = 547
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Chk_Landscape)
        Me.GroupBox1.Controls.Add(Me.Chk_Portrait)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(215, 126)
        Me.GroupBox1.TabIndex = 611
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "اتجاه الطباعة"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Txt_Top)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Txt_Bottom)
        Me.GroupBox2.Controls.Add(Me.Txt_Right)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Txt_Left)
        Me.GroupBox2.Location = New System.Drawing.Point(233, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox2.Size = New System.Drawing.Size(191, 126)
        Me.GroupBox2.TabIndex = 612
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "الهوامش"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label7.Location = New System.Drawing.Point(6, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 549
        Me.Label7.Text = "مللي"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label6.Location = New System.Drawing.Point(6, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 548
        Me.Label6.Text = "مللي"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label5.Location = New System.Drawing.Point(6, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 547
        Me.Label5.Text = "مللي"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label4.Location = New System.Drawing.Point(6, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 546
        Me.Label4.Text = "مللي"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Btn_Ok
        '
        Me.Btn_Ok.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Btn_Ok.Location = New System.Drawing.Point(350, 165)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(75, 40)
        Me.Btn_Ok.TabIndex = 613
        Me.Btn_Ok.Text = "موافق"
        Me.Btn_Ok.UseVisualStyleBackColor = False
        '
        'Frm_PrintingSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 217)
        Me.ControlBox = False
        Me.Controls.Add(Me.Btn_Ok)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Frm_PrintingSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "اعدادات الطباعة"
        CType(Me.Txt_Top, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Bottom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Right, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Left, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraRadioButtonGroupManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chk_Portrait, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chk_Landscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Txt_Top As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Friend WithEvents Label12 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_Bottom As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Friend WithEvents Label2 As Label
    Friend WithEvents Txt_Right As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Friend WithEvents Label3 As Label
    Friend WithEvents Txt_Left As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Private WithEvents UltraRadioButtonGroupManager1 As Infragistics.Win.UltraWinEditors.UltraRadioButtonGroupManager
    Friend WithEvents Chk_Portrait As Infragistics.Win.UltraWinEditors.UltraRadioButton
    Friend WithEvents Chk_Landscape As Infragistics.Win.UltraWinEditors.UltraRadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Btn_Ok As Button
End Class
