Imports System.Data.SqlClient
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_LovCustomers_A
    Dim vSqlString As String
    Dim vTableName As String
    Dim vAdditionalString As String
    Dim vTitle As String

    Public Sub New()
        InitializeComponent()
        Dim LabelTool3 As Infragistics.Win.UltraWinToolbars.LabelTool
    End Sub
    Private Sub FRM_LovTreeL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sLoadMainNodes()
    End Sub

    Private Sub sLoadMainNodes(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "", Optional ByVal pMTel As String = "")
        Try
            Dim vCodeFilter, vDescFilter, vMTel As String
            If pCode = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Code Like '%" & pCode & "%'"
            End If

            If pDesc = "" Then
                vDescFilter = ""
            Else
                vDescFilter = " And DescA Like '%" & pDesc & "%'"
            End If

            If pMTel = "" Then
                vMTel = ""
            Else
                vMTel = " And MTel Like '%" & pMTel & "%'"
            End If

            Dim vSqlCommand As New SqlCommand
            vSqlCommand.Connection = cControls.vSqlConn
            vSqlCommand.CommandText =
            " Select Code, DescA, IsNull(MTel, '') From Customers " &
            " Where 1 = 1 " &
            vCodeFilter &
            vDescFilter &
            vMTel

            cControls.vSqlConn.Open()
            DTS_Main.Rows.Clear()
            Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
            Dim vRowCounter As Integer
            While vReader.Read
                DTS_Main.Rows.SetCount(vRowCounter + 1)
                If vReader.IsDBNull(0) = False Then
                    DTS_Main.Rows(vRowCounter)("Code") = Trim(vReader(0))
                    DTS_Main.Rows(vRowCounter)("DescA") = Trim(vReader(1))
                    DTS_Main.Rows(vRowCounter)("MTel") = Trim(vReader(2))
                End If
                vRowCounter += 1
            End While
            cControls.vSqlConn.Close()
            vReader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try

    End Sub

    Private Sub Tre_Adjust_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Main.DoubleClick
        Try
            If Grd_Main.Selected.Rows.Count = 1 Then
                vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
                VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
                vLovReturn3 = Grd_Main.Selected.Rows(0).Cells("MTel").Text
                Me.Close()
            Else
                MessageBox.Show("تأكد من الاختيار أولاً")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Tre_Adjust_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Main.KeyUp
        Try
            If e.KeyData = Keys.Enter Then
                If Grd_Main.Selected.Rows.Count = 1 Then
                    vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
                    VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
                    vLovReturn3 = Grd_Main.Selected.Rows(0).Cells("MTel").Text
                    Me.Close()

                ElseIf Grd_Main.Selected.Rows.Count > 1 Then
                    For Each vRow As UltraGridRow In Grd_Main.Selected.Rows
                        vSelectedSortedList_1.Add(vRow.Cells("Code").Text, vRow.Cells("DescA").Text)
                    Next
                    Me.Close()

                Else
                    MessageBox.Show("تأكد من الاختيار أولاً")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        Try
            vSelectedSortedList_1.Clear()

            If Grd_Main.Selected.Rows.Count = 1 Then
                vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
                VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
                vLovReturn3 = Grd_Main.Selected.Rows(0).Cells("Mtel").Text

                Me.Close()
            ElseIf Grd_Main.Selected.Rows.Count > 1 Then

                For Each vRow As UltraGridRow In Grd_Main.Selected.Rows
                    vSelectedSortedList_1.Add(vRow.Cells("Code").Text, vRow.Cells("DescA").Text)
                Next

                Me.Close()
            Else
                MessageBox.Show("تأكد من الاختيار أولاً")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        vSelectedSortedList_1.Clear()

        Me.Close()
    End Sub

    Private Sub Txt_FndByCode_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged, Txt_FndByMTel.ValueChanged

        sLoadMainNodes(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text), Trim(Txt_FndByMTel.Text))
    End Sub

    Private Sub UltraButton1_Click(sender As Object, e As EventArgs) Handles UltraButton1.Click
        vLovReturn1 = ""
        VLovReturn2 = ""
        vLovReturn3 = ""

        Dim vFrm As New Frm_Add_Customer
        vFrm.ShowDialog()

        If vLovReturn1 <> "" Then
            Me.Close()
        End If
    End Sub
End Class