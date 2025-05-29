Imports System.IO
Imports Infragistics.Win.UltraWinGrid
Imports ExcelDataReader
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient

Public Class Frm_ImportFromExcel_A

    Dim vTables As DataTableCollection
    Dim vSqlStatment(0) As String
    Dim vSortedList As New SortedList
    Dim dt As New DataTable()
    Dim vDT_Row

    Private Sub Frm_ImportFromExcel_A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sql As String = " SELECT * FROM Attendance_Details "
        Using cmd As New SqlCommand(sql)
            cmd.Connection = cControls.vSqlConn
            Using sda As New SqlDataAdapter(cmd)
                sda.Fill(dt)
            End Using
        End Using
    End Sub
    Private Sub sFillSqlStatmentArray(ByVal pSqlstring As String)
        If vSqlStatment(UBound(vSqlStatment)) = "" Then
            vSqlStatment(UBound(vSqlStatment)) = pSqlstring
        Else
            ReDim Preserve vSqlStatment(UBound(vSqlStatment) + 1)
            vSqlStatment(UBound(vSqlStatment)) = pSqlstring
        End If
    End Sub
    Private Sub sEmptySqlStatmentArray()
        ReDim Preserve vSqlStatment(0)
        vSqlStatment(0) = ""
    End Sub
    Private Sub Btn_Browse_Click(sender As Object, e As EventArgs) Handles Btn_Browse.Click
        Try
            Using ofd As OpenFileDialog = New OpenFileDialog With {.Filter = "Excel 97-2003|*.xls|Excel Workbook|*.xlsx|Macro Work Book|*.xlsm"}
                If ofd.ShowDialog = DialogResult.OK Then
                    Txt_FileName.Text = ofd.FileName
                    Using vStream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read)
                        Using vReader As IExcelDataReader = ExcelReaderFactory.CreateReader(vStream)
                            Dim vResult As DataSet = vReader.AsDataSet(New ExcelDataSetConfiguration() With {
                                                                       .ConfigureDataTable = Function(__) New ExcelDataTableConfiguration() With {
                                                                       .UseHeaderRow = True}})
                            vTables = vResult.Tables
                            Txt_Sheets.Items.Clear()
                            Dim vTable As DataTable
                            For Each vTable In vTables
                                Txt_Sheets.Items.Add(vTable.TableName)
                            Next
                        End Using
                    End Using
                Else
                    Exit Sub
                End If
            End Using

            Dim vDT As DataTable = vTables("Sheet 1")
            Grd_Main.DataSource = vDT
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Txt_Sheets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Txt_Sheets.SelectedIndexChanged
        Dim vDT As DataTable = vTables(Txt_Sheets.SelectedItem.ToString)
        Grd_Main.DataSource = vDT
    End Sub

    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click
        If Grd_Main.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim vRow As UltraGridRow
        Dim vCount As Int32 = 0

        For Each vRow In Grd_Main.Rows
            If fCheck_If_Row_Exist_In_DataBase(vRow) Then
                vRow.Appearance.BackColor = Color.OrangeRed
                vCount += 1
            End If
        Next

        If vCount > 0 Then
            MessageBox.Show("بعض البيانات تم ادخالها من قبل")
            Exit Sub
        End If

        vGrd = Grd_Main

        Me.Close()

        'sSave_All()
    End Sub

    Private Function fCheck_If_Row_Exist_In_DataBase(ByVal pRow As UltraGridRow) As Boolean
        vDT_Row = dt.AsEnumerable().Select(Function(Node) New With {
            .UserCode = Node.Field(Of Int32)("Emp_Code"),
            .TDate = Node.Field(Of Date)("TDate")
            }).Where(Function(Node) Node.UserCode = Trim(pRow.Cells("AC-No.").Text)).Where(Function(Node) Format(Node.TDate, "MM/dd/yyyy") = Trim(pRow.Cells("Date").Text))

        Dim ergerg
        For Each ergerg In vDT_Row
            Return True
        Next

        Return False

    End Function

    Private Sub sSave_All()
        If Grd_Main.Rows.Count = 0 Then
            Exit Sub
        End If

        sEmptySqlStatmentArray()

        Dim vRow As UltraGridRow
        Dim vSqlString As String

        vSortedList.Clear()

        For Each vRow In Grd_Main.Rows

            If vRow.Cells(0).Text = "" Then
                MessageBox.Show("تأكد من اختيار الصنف " & vRow.Cells(0).Text)
                Exit Sub
            End If

            If Not vSortedList.ContainsKey(vRow.Cells(0).Text) Then

                'vCode += 1

                vSortedList.Add(vRow.Cells(0).Text, vRow.Cells(0).Text)
            End If

        Next

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            Me.Close()
        End If
    End Sub

    Private Sub Btn_Cancel_Click(sender As Object, e As EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub

    Private Function fRemoveComma(ByVal pString As String)
        Return Regex.Replace(pString, "[']", "")
    End Function

End Class