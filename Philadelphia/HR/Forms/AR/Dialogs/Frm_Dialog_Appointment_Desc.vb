Public Class Frm_Dialog_Appointment_Desc
    Private Sub Btn_Exit_Click(sender As Object, e As EventArgs) Handles Btn_Exit.Click
        Me.Close()
    End Sub

    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click
        If String.IsNullOrWhiteSpace(Txt_HolidayDesc.Text) Then
            MessageBox.Show("تأكد من ادخال اسم الاجازة",
                "تنبيه",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Exit Sub
        Else
            vHolidayDesc = Txt_HolidayDesc.Text.Trim()
        End If

        If Txt_StartDate.Value Is Nothing Then
            MessageBox.Show("تأكد من ادخال بداية التاريخ",
                "تنبيه",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Exit Sub
        Else
            vStartDate = CDate(Txt_StartDate.Value)
        End If

        If Txt_EndDate.Value Is Nothing Then
            MessageBox.Show("تأكد من ادخال نهاية التاريخ",
                "تنبيه",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Exit Sub
        ElseIf CDate(Txt_EndDate.Value).Date < CDate(Txt_StartDate.Value).date Then
            MessageBox.Show("تاريخ النهاية يجب أن يكون أكبر أو مساوٍ لتاريخ البداية",
                "تنبيه",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Exit Sub
        Else
            vEndDate = CDate(Txt_EndDate.Value)
        End If

        Me.Close()
    End Sub


    Private Sub Frm_Dialog_Appointment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Txt_StartDate.Value = vStartDate

        vStartDate = Nothing
        vEndDate = Nothing
    End Sub
End Class