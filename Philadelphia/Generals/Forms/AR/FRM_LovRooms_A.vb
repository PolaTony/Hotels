Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid

Public Class FRM_LovRooms_A

    Dim vSqlString As String
    Dim vTitle As String

    Public Sub New(ByVal pSqlString As String, ByVal pTitle As String)
        InitializeComponent()
        vSqlString = pSqlString
        Me.Text = pTitle
        vTitle = pTitle
    End Sub

    Private Sub FRM_LovSpecial_A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRoomData()
    End Sub

    Private Sub LoadRoomData()
        Try
            Using cmd As New SqlCommand(vSqlString, cControls.vSqlConn)
                cControls.vSqlConn.Open()
                DTS_Main.Rows.Clear()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    Dim rowIndex As Integer = 0
                    While reader.Read()
                        DTS_Main.Rows.SetCount(rowIndex + 1)

                        DTS_Main.Rows(rowIndex)("Room_Code") = reader("Room_Code").ToString().Trim()
                        DTS_Main.Rows(rowIndex)("Room_Desc") = reader("Room_Desc").ToString().Trim()
                        DTS_Main.Rows(rowIndex)("Room_Type") = reader("Room_Type").ToString().Trim()
                        DTS_Main.Rows(rowIndex)("Room_Type_Code") = reader("Room_Type_Code").ToString().Trim()

                        rowIndex += 1
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading Rooms: " & ex.Message)
        Finally
            If cControls.vSqlConn.State <> ConnectionState.Closed Then
                cControls.vSqlConn.Close()
            End If
        End Try
    End Sub

    Private Sub Btn_Ok_Click(sender As Object, e As EventArgs) Handles Btn_Ok.Click
        HandleSelection()
    End Sub

    Private Sub Btn_Cancel_Click(sender As Object, e As EventArgs) Handles Btn_Cancel.Click
        vSelectedSortedList_1.Clear()
        Me.Close()
    End Sub

    Private Sub Grd_Main_DoubleClick(sender As Object, e As EventArgs) Handles Grd_Main.DoubleClick
        HandleSelection()
    End Sub

    Private Sub Grd_Main_KeyUp(sender As Object, e As KeyEventArgs) Handles Grd_Main.KeyUp
        If e.KeyCode = Keys.Enter Then
            HandleSelection()
        End If
    End Sub

    Private Sub HandleSelection()
        Try
            vSelectedSortedList_1.Clear()

            If Grd_Main.Selected.Rows.Count = 1 Then
                vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Room_Code").Text
                VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("Room_Desc").Text
                vLovReturn3 = Grd_Main.Selected.Rows(0).Cells("Room_Type").Text
                vLovReturn4 = Grd_Main.Selected.Rows(0).Cells("Room_Type_Code").Text
                Me.Close()

            ElseIf Grd_Main.Selected.Rows.Count > 1 Then
                For Each row As UltraGridRow In Grd_Main.Selected.Rows
                    vSelectedSortedList_1.Add(row.Cells("Room_Code").Text, row.Cells("Room_Desc").Text)
                Next
                Me.Close()
            Else
                MessageBox.Show(" √ﬂœ „‰ «·«Œ Ì«— √Ê·«")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

End Class
