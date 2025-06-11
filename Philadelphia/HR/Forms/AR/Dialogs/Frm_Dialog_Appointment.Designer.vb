<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Dialog_Appointment
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
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Txt_StartDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Txt_EndDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Btn_Save = New Infragistics.Win.Misc.UltraButton()
        Me.Btn_Exit = New Infragistics.Win.Misc.UltraButton()
        CType(Me.Txt_StartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_EndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Txt_StartDate
        '
        Appearance1.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance1.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance1.TextHAlignAsString = "Right"
        Me.Txt_StartDate.Appearance = Appearance1
        Me.Txt_StartDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_StartDate.ButtonAppearance = Appearance2
        Me.Txt_StartDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_StartDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_StartDate.DropDownAppearance = Appearance3
        Me.Txt_StartDate.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_StartDate.DropDownCalendarAlignment = Infragistics.Win.DropDownListAlignment.Left
        Me.Txt_StartDate.Font = New System.Drawing.Font("Droid Arabic Kufi", 10.0!)
        Me.Txt_StartDate.FormatString = "dd-MM-yyyy"
        Me.Txt_StartDate.Location = New System.Drawing.Point(64, 69)
        Me.Txt_StartDate.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_StartDate.MaskInput = "{LOC}dd/mm/yyyy"
        Me.Txt_StartDate.Name = "Txt_StartDate"
        Me.Txt_StartDate.ReadOnly = True
        Me.Txt_StartDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_StartDate.Size = New System.Drawing.Size(163, 33)
        Me.Txt_StartDate.SpinButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_StartDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.Txt_StartDate.TabIndex = 556
        Me.Txt_StartDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.Txt_StartDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_StartDate.Value = Nothing
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label16.Font = New System.Drawing.Font("Droid Arabic Kufi", 10.0!)
        Me.Label16.Location = New System.Drawing.Point(235, 72)
        Me.Label16.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 27)
        Me.Label16.TabIndex = 557
        Me.Label16.Text = "من تاريخ"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_EndDate
        '
        Appearance4.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance4.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance4.TextHAlignAsString = "Right"
        Me.Txt_EndDate.Appearance = Appearance4
        Me.Txt_EndDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_EndDate.ButtonAppearance = Appearance5
        Me.Txt_EndDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_EndDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_EndDate.DropDownAppearance = Appearance6
        Me.Txt_EndDate.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_EndDate.DropDownCalendarAlignment = Infragistics.Win.DropDownListAlignment.Left
        Me.Txt_EndDate.Font = New System.Drawing.Font("Droid Arabic Kufi", 10.0!)
        Me.Txt_EndDate.FormatString = "dd-MM-yyyy"
        Me.Txt_EndDate.Location = New System.Drawing.Point(64, 119)
        Me.Txt_EndDate.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_EndDate.MaskInput = "{LOC}dd/mm/yyyy"
        Me.Txt_EndDate.Name = "Txt_EndDate"
        Me.Txt_EndDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_EndDate.Size = New System.Drawing.Size(163, 33)
        Me.Txt_EndDate.SpinButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_EndDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.Txt_EndDate.TabIndex = 0
        Me.Txt_EndDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.Txt_EndDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_EndDate.Value = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label1.Font = New System.Drawing.Font("Droid Arabic Kufi", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(233, 122)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 27)
        Me.Label1.TabIndex = 559
        Me.Label1.Text = "إلى تاريخ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label2.Font = New System.Drawing.Font("Droid Arabic Kufi", 18.0!)
        Me.Label2.Location = New System.Drawing.Point(111, 9)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(137, 46)
        Me.Label2.TabIndex = 560
        Me.Label2.Text = "مدة الاجازة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Btn_Save
        '
        Appearance7.BackColor = System.Drawing.Color.DarkOliveGreen
        Appearance7.BorderColor = System.Drawing.Color.Transparent
        Appearance7.ForeColor = System.Drawing.Color.White
        Me.Btn_Save.Appearance = Appearance7
        Me.Btn_Save.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat
        Me.Btn_Save.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Save.Font = New System.Drawing.Font("Droid Arabic Kufi", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Save.Location = New System.Drawing.Point(18, 185)
        Me.Btn_Save.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(136, 35)
        Me.Btn_Save.TabIndex = 1
        Me.Btn_Save.Text = "حفظ"
        Me.Btn_Save.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Btn_Exit
        '
        Appearance8.BackColor = System.Drawing.Color.DarkRed
        Appearance8.BackColor2 = System.Drawing.Color.White
        Appearance8.BorderColor = System.Drawing.Color.Transparent
        Appearance8.ForeColor = System.Drawing.Color.White
        Me.Btn_Exit.Appearance = Appearance8
        Me.Btn_Exit.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat
        Me.Btn_Exit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Exit.Font = New System.Drawing.Font("Droid Arabic Kufi", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Exit.Location = New System.Drawing.Point(181, 185)
        Me.Btn_Exit.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(136, 35)
        Me.Btn_Exit.TabIndex = 2
        Me.Btn_Exit.Text = "إلغاء"
        Me.Btn_Exit.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Frm_Dialog_Appointment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(337, 246)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Controls.Add(Me.Btn_Save)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Txt_EndDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txt_StartDate)
        Me.Controls.Add(Me.Label16)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "Frm_Dialog_Appointment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Dialog_Appointment"
        CType(Me.Txt_StartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_EndDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Txt_StartDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label16 As Label
    Friend WithEvents Txt_EndDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Btn_Save As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Exit As Infragistics.Win.Misc.UltraButton
End Class
