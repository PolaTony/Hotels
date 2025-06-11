Public Class Frm_Add_PackUnit
    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        If Txt_Desc.Text = "" Then
            MessageBox.Show("تأكد من ادخال الوصف")
            Txt_Desc.Select()
            Exit Sub
        End If

        If cControls.fIsExist(" From Pack_Unit Where DescA = '" & Trim(Txt_Desc.Text) & "'", Me.Name) Then
            MessageBox.Show("هذه الوحدة موجوده بالفعل")
            Txt_Desc.Select()
            Exit Sub
        End If

        Dim vSqlString, vGetCode As String
        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From  Pack_Unit " &
                     " Where  Company_Code = " & vCompanyCode

        vGetCode = cControls.fReturnValue(vSqlString, Me.Name)

        vSqlString = " Insert Into Pack_Unit  (      Code,                     DescA,                Company_Code ) " &
                     "                Values  (" & vGetCode & ", '" & Trim(Txt_Desc.Text) & "', " & vCompanyCode & " )"

        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
            vLovReturn1 = vGetCode
            VLovReturn2 = Txt_Desc.Text

            Me.Close()
        End If

    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub
End Class