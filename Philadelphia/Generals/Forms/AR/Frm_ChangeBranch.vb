Imports System.Data.SqlClient

Public Class Frm_ChangeBranch

    Private Sub Frm_ChangeBranch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sLoadBranches()
    End Sub

    Private Sub sLoadBranches()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Code, DescA  From Companies "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Branches.Items.Clear()
            Do While vSqlReader.Read
                Txt_Branches.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Txt_Branches.Value = vCompanyCode

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        If Txt_Branches.SelectedIndex = -1 Then
            MessageBox.Show("تأكد من اختيار الفرع", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Txt_Branches.Select()
            Return
        End If

        vCompanyCode = Txt_Branches.Value
        Me.Close()
    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub
End Class