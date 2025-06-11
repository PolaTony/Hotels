Public Class Frm_Add_Category
    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        If Txt_Desc.Text = "" Then
            MessageBox.Show("تأكد من ادخال الوصف")
            Txt_Desc.Select()
            Exit Sub
        End If

        If cControls.fIsExist(" From Categories Where DescA = '" & Trim(Txt_Desc.Text) & "'", Me.Name) Then
            MessageBox.Show("هذا التصنيف موجود بالفعل")
            Txt_Desc.Select()
            Exit Sub
        End If

        Dim vSqlString, vGetCode, vGetSer As String
        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From  Categories " &
                     " Where  Company_Code = " & vCompanyCode

        vGetCode = cControls.fReturnValue(vSqlString, Me.Name)

        vSqlString = " Select IsNull(Max(Ser), 0) + 1 From  Categories "

        vGetSer = cControls.fReturnValue(vSqlString, Me.Name)

        vSqlString = " Insert Into Categories    (      Ser,              Code,                     DescA,        Node_Level,     Node_Index,       Company_Code ) " &
                     "                   Values  (" & vGetSer & ", '" & vGetCode & "', '" & Trim(Txt_Desc.Text) & "',  1,      " & vGetCode & ", " & vCompanyCode & " )"

        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
            vLovReturn1 = cControls.fReturnValue(" Select Ser From Categories Where Code = '" & vGetCode & "' ", Me.Name)
            VLovReturn2 = Txt_Desc.Text

            Me.Close()
        End If

    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub
End Class