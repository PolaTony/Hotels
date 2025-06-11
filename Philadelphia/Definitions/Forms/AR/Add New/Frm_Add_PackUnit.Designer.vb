<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Add_PackUnit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Add_PackUnit))
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton()
        Me.Btn_Ok = New Infragistics.Win.Misc.UltraButton()
        Me.Txt_Desc = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Btn_Cancel
        '
        Appearance1.Image = CType(resources.GetObject("Appearance1.Image"), Object)
        Appearance1.TextHAlignAsString = "Right"
        Me.Btn_Cancel.Appearance = Appearance1
        Me.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Btn_Cancel.ImageSize = New System.Drawing.Size(20, 20)
        Me.Btn_Cancel.Location = New System.Drawing.Point(203, 65)
        Me.Btn_Cancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(99, 39)
        Me.Btn_Cancel.TabIndex = 628
        Me.Btn_Cancel.Text = "الغاء"
        '
        'Btn_Ok
        '
        Appearance2.Image = CType(resources.GetObject("Appearance2.Image"), Object)
        Appearance2.TextHAlignAsString = "Right"
        Me.Btn_Ok.Appearance = Appearance2
        Me.Btn_Ok.ImageSize = New System.Drawing.Size(20, 20)
        Me.Btn_Ok.Location = New System.Drawing.Point(321, 65)
        Me.Btn_Ok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(97, 39)
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
        Me.Txt_Desc.Location = New System.Drawing.Point(14, 22)
        Me.Txt_Desc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Txt_Desc.Multiline = True
        Me.Txt_Desc.Name = "Txt_Desc"
        Me.Txt_Desc.Size = New System.Drawing.Size(438, 24)
        Me.Txt_Desc.TabIndex = 626
        Me.Txt_Desc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frm_Add_PackUnit
        '
        Me.AcceptButton = Me.Btn_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Btn_Cancel
        Me.ClientSize = New System.Drawing.Size(467, 126)
        Me.Controls.Add(Me.Btn_Cancel)
        Me.Controls.Add(Me.Btn_Ok)
        Me.Controls.Add(Me.Txt_Desc)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Frm_Add_PackUnit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "أضافة وحدة التعبئة"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Ok As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Txt_Desc As System.Windows.Forms.TextBox
End Class
