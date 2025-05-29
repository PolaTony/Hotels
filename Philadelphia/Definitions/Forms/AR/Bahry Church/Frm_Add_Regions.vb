Public Class Frm_Add_Regions

    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        If Txt_Desc.Text = "" Then
            MessageBox.Show("تأكد من ادخال المنطقة")
            Txt_Desc.Select()
            Exit Sub
        End If

        If cControls.fCount_Rec(" From Regions Where DescA = '" & Trim(Txt_Desc.Text) & "'") > 0 Then
            MessageBox.Show("هذه المنطقة موجود بالفعل")
            Txt_Desc.Select()
            Exit Sub
        End If

        Dim vSqlString As String
        'vSqlString = " Select IsNull(Max(Code), 0) + 1 From  Car_Colors "
        'vGetCode = cControls.fReturnValue(vSqlString, Me.Name)

        vSqlString = " Insert Into Regions (              DescA ) " & _
                     "             Values  ('" & Trim(Txt_Desc.Text) & "' )"

        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
            Me.Close()
        End If

    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub
End Class