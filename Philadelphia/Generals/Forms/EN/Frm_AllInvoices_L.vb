Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource

Public Class Frm_AllInvoices_L

    Dim pTableName, pColumnName As String
    Dim vHO_Type As String
    Dim vTitle As String
    Dim vType As String
    Dim vSqlStatment As String

    Public Sub New(ByVal pTitle As String, ByVal pSqlStatment As String, Optional ByVal pType As String = "Details")
        vTitle = pTitle

        Me.Text = pTitle
        vType = pType
        vSqlStatment = pSqlStatment

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TXT_SummaryDate.Value = Now
        'sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
    End Sub

    Private Sub sQuerySummaryMain(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            If vTitle = "ÝæÇÊíÑ ÇáÔÑÇÁ" Then
                pTableName = "Purchase_Invoices"
            Else
                pTableName = "Sales_Invoices"
            End If
            If vTitle = "ÝæÇÊíÑ ÇáÔÑÇÁ" Then
                vHO_Type = "Providers"
            Else
                vHO_Type = "Customers"
            End If
            Dim vCodeFilter1, vCodeFilter2, vDescFilter1, vDescFilter2, vDate, vStatus As String
            If pCode = "" Then
                vCodeFilter1 = ""
                vCodeFilter2 = ""
            Else
                vCodeFilter1 = " And " & pTableName & ".Code Like '%" & pCode & "%'"
                'vCodeFilter2 = " And " & pTableName & "_WithTaxes.Code Like '%" & pCode & "%'"
            End If
            If pDesc = "" Then
                vDescFilter1 = ""
                vDescFilter2 = ""
            Else
                vDescFilter1 = " And " & pTableName & ".DescA Like '%" & pDesc & "%'"
                'vDescFilter2 = " And " & pTableName & "_WithTaxes.DescA Like '%" & pDesc & "%'"
            End If

            Dim x As Integer = ToolBar_Main.Tools.Count
            If x > 0 Then
                Dim vStateButtonTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
                vStateButtonTool = ToolBar_Main.Tools("FilterByDate")
                If vStateButtonTool.Checked Then
                    vDate = " And Month(TDate) = '" & TXT_SummaryDate.DateTime.Month & "'" & _
                            " And Year(TDate) = '" & TXT_SummaryDate.DateTime.Year & "'"
                Else
                    vDate = ""
                End If

                vStateButtonTool = ToolBar_Main.Tools("FilterByProcessed")
                If vStateButtonTool.Checked Then
                    vStatus = " And Status = 'P' "
                Else
                    vStatus = ""
                End If
            Else
                Return
            End If

            If DTS_Summary.Band.Columns.Count = 0 Then
                Return
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select " & pTableName & ".Code,                                " & vbCrLf & _
            "        " & pTableName & ".DescA as PI_DescA,                   " & vbCrLf & _
            "         Employees.DescA as Emp_Desc,                           " & vbCrLf & _
            "       " & vHO_Type & ".DescA as Provider_Desc,                 " & vbCrLf & _
            "         Stores.DescA as Store_Desc,                            " & vbCrLf & _
            "         TDate                                                  " & vbCrLf & _
            " From " & pTableName & " Inner Join Employees                   " & vbCrLf & _
            " On " & pTableName & ".Emp_Code = Employees.Code                " & vbCrLf & _
            " Inner Join " & vHO_Type & vbCrLf & _
            " On " & pTableName & ".Provider_Code = " & vHO_Type & ".Code    " & vbCrLf & _
            " Left Join Stores                                               " & vbCrLf & _
            " On " & pTableName & ".Str_Code = Stores.Code                   " & vbCrLf & _
            " Where 1 = 1                                                    " & vbCrLf & _
            vCodeFilter1 & vbCrLf & _
            vDescFilter1 & vbCrLf & _
            vDate & vbCrLf & _
            vStatus & vbCrLf & _
            vSqlStatment

            '" Union " & _
            '" Select " & pTableName & "_WithTaxes.Code,                          " & _
            '"        " & pTableName & "_WithTaxes.DescA as PI_DescA,             " & _
            '"         Employees.DescA as Emp_Desc,                           " & _
            '"       " & vHO_Type & ".DescA as Provider_Desc,                      " & _
            '"         Stores.DescA as Store_Desc,                      " & _
            '"         TDate                                            " & _
            '" From " & pTableName & "_WithTaxes Inner Join Employees      " & _
            '" On " & pTableName & "_WithTaxes.Emp_Code = Employees.Code                " & _
            '" Inner Join " & vHO_Type & _
            '" On " & pTableName & "_WithTaxes.Provider_Code = " & vHO_Type & ".Code    " & _
            '" Left Join Stores                                        " & _
            '" On " & pTableName & "_WithTaxes.Str_Code = Stores.Code             " & _
            '" Where 1 = 1                                              " & _
            'vCodeFilter2 & _
            'vDescFilter2 & _
            'vDate & _
            'vStatus
            'vSqlStatment

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
                    DTS_Summary.Rows(vRowCounter)("Provider_Desc") = Trim(vSqlReader(3))
                Else
                    DTS_Summary.Rows(vRowCounter)("Provider_Desc") = ""
                End If
                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Summary.Rows(vRowCounter)("Store_Desc") = Trim(vSqlReader(4))
                Else
                    DTS_Summary.Rows(vRowCounter)("Store_Desc") = ""
                End If
                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate") = Trim(vSqlReader(5))
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
            If vTitle = "ÝæÇÊíÑ ÇáÔÑÇÁ" Then
                pTableName = "Purchase_Invoice_Details"
                pColumnName = "PI_Code"
            Else
                pTableName = "Sales_Invoice_Details"
                pColumnName = "SI_Code"
            End If

            Dim vChildRows As UltraDataRowsCollection = pRow.GetChildRows(pChildBand)
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select " & pTableName & ".Ser,                                 " & _
            "        " & pTableName & ".Item_Code,                           " & _
            "        " & pTableName & ".Item_Ser,                            " & _
            "        Items.DescA as Item_Desc,                               " & _
            "        " & pTableName & ".Quantity,                            " & _
            "        " & pTableName & ".Price,                               " & _
            "        " & pTableName & ".Addition,                            " & _
            "        " & pTableName & ".Deduction,                           " & _
            "        " & pTableName & ".LCost,                               " & _
            "        " & pTableName & ".Str_Code,                            " & _
            "        Stores.DescA                                            " & _
            "        From  " & pTableName & " Inner Join  Items              " & _
            "        On    " & pTableName & ".Item_Code = Items.Code         " & _
            "        Left  Join Stores                                       " & _
            "        On Stores.Code = " & pTableName & ".Str_Code            " & _
            " Where " & pColumnName & " = '" & pRow("Code") & "'" & _
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
                vChildRows(vRowCounter)("Item_Ser") = Trim(vSqlReader(2))
                vChildRows(vRowCounter)("Item_Desc") = Trim(vSqlReader(3))
                vChildRows(vRowCounter)("Quantity") = Trim(vSqlReader(4))
                vChildRows(vRowCounter)("Price") = Trim(vSqlReader(5))
                If vSqlReader.IsDBNull(6) = False Then
                    vChildRows(vRowCounter)("Addition") = Trim(vSqlReader(6))
                Else
                    vChildRows(vRowCounter)("Addition") = Nothing
                End If
                If vSqlReader.IsDBNull(7) = False Then
                    vChildRows(vRowCounter)("Deduction") = Trim(vSqlReader(7))
                Else
                    vChildRows(vRowCounter)("Deduction") = Nothing
                End If
                If vSqlReader.IsDBNull(8) = False Then
                    vChildRows(vRowCounter)("LCost") = Trim(vSqlReader(8))
                Else
                    vChildRows(vRowCounter)("LCost") = Nothing
                End If

                If vSqlReader.IsDBNull(9) = False Then
                    vChildRows(vRowCounter)("Str_Code") = Trim(vSqlReader(9))
                Else
                    vChildRows(vRowCounter)("Str_Code") = Nothing
                End If
                If vSqlReader.IsDBNull(10) = False Then
                    vChildRows(vRowCounter)("Str_Desc") = Trim(vSqlReader(10))
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
    Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged, TXT_SummaryDate.ValueChanged
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
                    vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Code") = vRow.Cells("Code").Value
                    vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Desc") = vRow.Cells("DescA").Value
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