<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Add_Customer
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Add_Customer))
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton()
        Me.Btn_Ok = New Infragistics.Win.Misc.UltraButton()
        Me.Txt_Desc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txt_MTel = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Btn_Cancel
        '
        Appearance1.Image = CType(resources.GetObject("Appearance1.Image"), Object)
        Appearance1.TextHAlignAsString = "Right"
        Me.Btn_Cancel.Appearance = Appearance1
        Me.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Btn_Cancel.ImageSize = New System.Drawing.Size(20, 20)
        Me.Btn_Cancel.Location = New System.Drawing.Point(201, 88)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(85, 32)
        Me.Btn_Cancel.TabIndex = 631
        Me.Btn_Cancel.Text = "الغاء"
        '
        'Btn_Ok
        '
        Appearance2.Image = CType(resources.GetObject("Appearance2.Image"), Object)
        Appearance2.TextHAlignAsString = "Right"
        Me.Btn_Ok.Appearance = Appearance2
        Me.Btn_Ok.ImageSize = New System.Drawing.Size(20, 20)
        Me.Btn_Ok.Location = New System.Drawing.Point(302, 88)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(83, 32)
        Me.Btn_Ok.TabIndex = 630
        Me.Btn_Ok.Text = "موافق"
        '
        'Txt_Desc
        '
        Me.Txt_Desc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txt_Desc.AutoCompleteCustomSource.AddRange(New String() {"القاهرة", "اسكندرية", "بني سويف"})
        Me.Txt_Desc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Txt_Desc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.Txt_Desc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_Desc.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Txt_Desc.Location = New System.Drawing.Point(12, 18)
        Me.Txt_Desc.Multiline = True
        Me.Txt_Desc.Name = "Txt_Desc"
        Me.Txt_Desc.Size = New System.Drawing.Size(304, 20)
        Me.Txt_Desc.TabIndex = 629
        Me.Txt_Desc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label7.Location = New System.Drawing.Point(349, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 632
        Me.Label7.Text = "الاسم"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label1.Location = New System.Drawing.Point(322, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 634
        Me.Label1.Text = "رقم التليفون"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_MTel
        '
        Me.Txt_MTel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Txt_MTel.AutoCompleteCustomSource.AddRange(New String() {"القاهرة", "اسكندرية", "بني سويف"})
        Me.Txt_MTel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Txt_MTel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.Txt_MTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_MTel.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Txt_MTel.Location = New System.Drawing.Point(12, 49)
        Me.Txt_MTel.Multiline = True
        Me.Txt_MTel.Name = "Txt_MTel"
        Me.Txt_MTel.Size = New System.Drawing.Size(304, 20)
        Me.Txt_MTel.TabIndex = 633
        Me.Txt_MTel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frm_Add_Customer
        '
        Me.AcceptButton = Me.Btn_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 132)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_MTel)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Btn_Cancel)
        Me.Controls.Add(Me.Btn_Ok)
        Me.Controls.Add(Me.Txt_Desc)
        Me.Name = "Frm_Add_Customer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "عميل جديد"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Ok As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Txt_Desc As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Txt_MTel As TextBox
End Class
