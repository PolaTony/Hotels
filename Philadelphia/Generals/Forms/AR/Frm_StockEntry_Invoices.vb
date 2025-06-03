Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource

Public Class Frm_StockEntry_Invoices
    Dim pTableName, pColumnName As String
    Dim vHO_Type As String
    Dim vTitle As String
    Dim vType As String
    Dim vSqlStatment As String

    Public Sub New(ByVal pSqlStatment As String, Optional ByVal pType As String = "Details")
        'vTitle = pTitle

        'Me.Text = pTitle
        vType = pType
        vSqlStatment = pSqlStatment

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TXT_FromSummaryDate.Value = Now
        Txt_ToSummaryDate.Value = Now
        'sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
    End Sub

    Private Sub sQuerySummaryMain(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            'If vTitle = "فواتير الشراء" Then
            '    pTableName = "Purchase_Invoices"
            'Else
            '    pTableName = "Sales_Invoices"
            'End If

            'If vTitle = "فواتير الشراء" Then
            '    vHO_Type = "Providers"
            'Else
            '    vHO_Type = "Customers"
            'End If

            Dim vCodeFilter, vDescFilter, vDate, vStatus As String

            If pCode = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Stock_Entry.Code Like '%" & pCode & "%'"
            End If

            If pDesc = "" Then
                vDescFilter = ""
            Else
                vDescFilter = " And Stock_Entry.DescA Like '%" & pDesc & "%'"
            End If

            Dim vFromDate, vToDate, vToDate_PlusOneDay As String

            If Not TXT_FromSummaryDate.Value Is Nothing Then
                vFromDate = "'" & Format(TXT_FromSummaryDate.Value, "MM-dd-yyyy") & "'"
            Else
                vFromDate = "NULL"
            End If

            vToDate_PlusOneDay = Txt_ToSummaryDate.DateTime.AddDays(1)
            vToDate = "'" & Format(CDate(vToDate_PlusOneDay), "MM-dd-yyyy") & "'"

            If DTS_Summary.Band.Columns.Count = 0 Then
                Return
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            "  Select Stock_Entry.Code,                                        " & _
            "         Stock_Entry.DescA as SE_DescA,                           " & _
            "         HR1.DescA as Emp_DescA,                                  " & _
            "         Stock_Entry.Remarks,                                     " & _
            "         Stock_Entry.TDate                                        " & _
            " From Stock_Entry Inner Join Employees   HR1                      " & _
            " On Stock_Entry.Emp_Code = HR1.Code                               " & _
            " Where 1 = 1                                                      " & _
            " And Status = 'P'                                                 " & _
            vCodeFilter & vbCrLf & _
            vDescFilter & vbCrLf & _
            vSqlStatment & _
            " And (TDate >= " & vFromDate & " Or " & vFromDate & " Is NULL) " & vbCrLf & _
            " And TDate < " & vToDate

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)
                DTS_Summary.Rows(vRowCounter)("Code") = Trim(vSqlReader(0))

                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Summary.Rows(vRowCounter)("DescA") = Trim(vSqlReader(1))
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = vSqlReader(2)
                Else
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = Nothing
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Summary.Rows(vRowCounter)("Remarks") = Trim(vSqlReader(3))
                Else
                    DTS_Summary.Rows(vRowCounter)("Remarks") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate") = Trim(vSqlReader(4))
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                End If
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()

            'Dim vRow As UltraDataRow
            'Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
            'For Each vRow In DTS_Summary.Rows
            '    'If cBase.fCount_Rec(" From " & pTableName & " Where PI_Code = '" & vRow("Code") & "'") > 0 Then
            '    sQuerySummaryDetails(vRow, vChildBand)
            '    'End If
            'Next
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuerySummaryMain")
        End Try
    End Sub

    Private Sub sQuerySummaryDetails(ByVal pRow As UltraDataRow, ByVal pChildBand As UltraDataBand)
        Try

            Dim vChildRows As UltraDataRowsCollection = pRow.GetChildRows(pChildBand)
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText =
            " Select Stock_Entry_Details.Ser,          " &
            "        Items.UserCode,                    " &
            "        Items.DescA as Item_Desc,              " &
            "        IsNull(Stock_Entry_Details.PU_Ser, 0),  " &
            "        Items_PackUnit.Number,                 " &
            "        Pack_Unit.DescA,                       " &
            "        Stock_Entry_Details.Quantity,     " &
            "        Stock_Entry_Details.Str_Code,     " &
            "        Stores.DescA as Str_Desc,              " &
            "        Stock_Entry_Details.Remarks        " &
            " From   Stock_Entry_Details Inner Join Items " &
            " On     Stock_Entry_Details.Item_Code = Items.Code " &
            "        Inner Join Stores                         " &
            " On     Stock_Entry_Details.Str_Code = Stores.Code " &
            "        Inner Join Pack_Unit                      " &
            " On     Pack_Unit.Code = Items.PU_Code " &
            "        Left Join Items_PackUnit                     " &
            " On     Stock_Entry_Details.Item_Code = Items_PackUnit.Item_Code       " &
            " And    Stock_Entry_Details.PU_Ser    = Items_PackUnit.PU_Code             " &
            " Where  SE_Code = '" & pRow("Code") & "'" &
            " Order  By Ser       "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            'vChildRows.Clear()
            Do While vSqlReader.Read
                vChildRows.SetCount(vRowCounter + 1)
                vChildRows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                vChildRows(vRowCounter)("Item_Code") = Trim(vSqlReader(1))
                vChildRows(vRowCounter)("Item_Desc") = Trim(vSqlReader(2))
                vChildRows(vRowCounter)("Quantity") = Trim(vSqlReader(6))

                If vSqlReader.IsDBNull(7) = False Then
                    vChildRows(vRowCounter)("Str_Code") = Trim(vSqlReader(7))
                Else
                    vChildRows(vRowCounter)("Str_Code") = Nothing
                End If

                If vSqlReader.IsDBNull(8) = False Then
                    vChildRows(vRowCounter)("Str_Desc") = Trim(vSqlReader(8))
                Else
                    vChildRows(vRowCounter)("Str_Desc") = Nothing
                End If
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuerySummaryDetails")
        End Try
    End Sub

    Private Sub Grd_Summary_BeforeRowExpanded(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CancelableRowEventArgs) Handles Grd_Summary.BeforeRowExpanded
        Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
        Dim vRow As UltraDataRow = DTS_Summary.Rows(e.Row.Index)
        sQuerySummaryDetails(vRow, vChildBand)
    End Sub
    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged, _
            TXT_FromSummaryDate.ValueChanged, Txt_ToSummaryDate.ValueChanged

        sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
    End Sub

    Private Sub ToolBar_Main_ToolClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case e.Tool.Key
            Case "FilterByDate"
                sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
            Case "FilterByProcessed"
                sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
        End Select
    End Sub

    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        'vDTS_AllInvoices.Rows.Clear()
        vDTS_AllInvoices.Band.Columns.Clear()
        vDTS_AllInvoices.Band.Columns.Add("Invoice_Code", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Invoice_Desc", GetType(String))
        'vDTS_AllInvoices.Band.Columns.Add("Customer_Code", GetType(String))
        'vDTS_AllInvoices.Band.Columns.Add("Customer_Desc", GetType(String))
        'vDTS_AllInvoices.Band.Columns.Add("SalesMan_Code", GetType(String))
        'vDTS_AllInvoices.Band.Columns.Add("SalesMan_Desc", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Str_Code", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Str_Desc", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Item_Code", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Item_Ser", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Item_Desc", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Quantity", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Price", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Addition", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("Deduction", GetType(String))
        vDTS_AllInvoices.Band.Columns.Add("LCost", GetType(String))

        If Grd_Summary.Selected.Rows.Count > 0 Then
            Dim vRow As UltraGridRow
            Dim vRowCounter As Integer
            For Each vRow In Grd_Summary.Selected.Rows
                If Not vRow.ParentRow Is Nothing Then
                    vDTS_AllInvoices.Rows.SetCount(vRowCounter + 1)
                    vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Code") = vRow.ParentRow.Cells("Code").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Desc") = vRow.ParentRow.Cells("DescA").Value

                    vDTS_AllInvoices.Rows(vRowCounter)("Item_Code") = vRow.Cells("Item_Code").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Item_Ser") = vRow.Cells("Item_Ser").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Item_Desc") = vRow.Cells("Item_Desc").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Quantity") = vRow.Cells("Quantity").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Price") = vRow.Cells("Price").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Addition") = vRow.Cells("Addition").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Deduction") = vRow.Cells("Deduction").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("LCost") = vRow.Cells("LCost").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Str_Code") = vRow.Cells("Str_Code").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Str_Desc") = vRow.Cells("Str_Desc").Value
                Else
                    vDTS_AllInvoices.Rows.SetCount(vRowCounter + 1)
                    'vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Code") = vRow.Cells("Code").Value
                    'vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Desc") = vRow.Cells("DescA").Value
                    vLovReturn1 = vRow.Cells("Code").Value
                    VLovReturn2 = vRow.Cells("DescA").Value
                End If
                vRowCounter += 1
            Next
        End If
        Me.Close()
    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        vDTS_AllInvoices.Rows.Clear()
        Me.Close()
    End Sub

End Class