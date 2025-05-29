<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Add_Category
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
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Add_Category))
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton
        Me.Btn_Ok = New Infragistics.Win.Misc.UltraButton
        Me.Txt_Desc = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Btn_Cancel
        '
        Appearance2.Image = CType(resources.GetObject("Appearance2.Image"), Object)
        Appearance2.TextHAlignAsString = "Right"
        Me.Btn_Cancel.Appearance = Appearance2
        Me.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Btn_Cancel.ImageSize = New System.Drawing.Size(20, 20)
        Me.Btn_Cancel.Location = New System.Drawing.Point(174, 53)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(85, 32)
        Me.Btn_Cancel.TabIndex = 628
        Me.Btn_Cancel.Text = "الغاء"
        '
        'Btn_Ok
        '
        Appearance3.Image = CType(resources.GetObject("Appearance3.Image"), Object)
        Appearance3.TextHAlignAsString = "Right"
        Me.Btn_Ok.Appearance = Appearance3
        Me.Btn_Ok.ImageSize = New System.Drawing.Size(20, 20)
        Me.Btn_Ok.Location = New System.Drawing.Point(275, 53)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(83, 32)
        Me.Btn_Ok.TabIndex = 627
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
        Me.Txt_Desc.Size = New System.Drawing.Size(376, 20)
        Me.Txt_Desc.TabIndex = 626
        Me.Txt_Desc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frm_Add_Category
        '
        Me.AcceptButton = Me.Btn_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Btn_Cancel
        Me.ClientSize = New System.Drawing.Size(400, 102)
        Me.Controls.Add(Me.Btn_Cancel)
        Me.Controls.Add(Me.Btn_Ok)
        Me.Controls.Add(Me.Txt_Desc)
        Me.Name = "Frm_Add_Category"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "أضافة تصنيف"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Ok As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Txt_Desc As System.Windows.Forms.TextBox
End Class
