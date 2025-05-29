Public Class Frm_Add_Customer
    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        If Txt_Desc.Text = "" Then
            MessageBox.Show("تأكد من ادخال اسم العميل")
            Txt_Desc.Select()
            Exit Sub
        End If

        If Txt_MTel.Text = "" Then
            MessageBox.Show("تأكد من ادخال رقم التليفون")
            Txt_MTel.Select()
            Exit Sub
        End If

        If cControls.fIsExist(" From Customers Where DescA = '" & Trim(Txt_Desc.Text) & "'", Me.Name) Then
            MessageBox.Show("هذا القسم موجود بالفعل")
            Txt_Desc.Select()
            Exit Sub
        End If

        Dim vSqlString, vGetCode As String
        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From Customers "

        vGetCode = cControls.fReturnValue(vSqlString, Me.Name)

        vSqlString = " Insert Into Customers  (     Code,             Company_Code,                  DescA,                         MTel ) " &
                     "                Values  (" & vGetCode & ", " & vCompanyCode & ", '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_MTel.Text) & "' ) "

        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
            vLovReturn1 = vGetCode
            VLovReturn2 = Trim(Txt_Desc.Text)
            vLovReturn3 = Trim(Txt_MTel.Text)

            Me.Close()
        End If

    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub
End Class