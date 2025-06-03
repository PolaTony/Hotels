Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource

Public Class Frm_All_POS_Invoices
    Dim pTableName, pColumnName, vCustomerTableName, pTotal As String
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
        TXT_FromDate.Value = Now.AddDays(-30)
        Txt_ToDate.Value = Now

        sQuerySummaryMain()
    End Sub
    Private Sub Frm_AllInvoices_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub sQuerySummaryMain(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try

            If TXT_FromDate.Value Is Nothing Then
                Exit Sub
            End If

            If Txt_ToDate.Value Is Nothing Then
                Exit Sub
            End If

            If Not TXT_FromDate.Value Is Nothing Then
                If TXT_FromDate.DateTime.Year < 2000 Then
                    Exit Sub
                End If
            End If

            If Not Txt_ToDate.Value Is Nothing Then
                If Txt_ToDate.DateTime.Year < 2000 Then
                    Exit Sub
                End If
            End If

            Dim vCodeFilter1, vCodeFilter2 As String
            If pCode = "" Then
                vCodeFilter1 = ""
                vCodeFilter2 = ""
            Else
                vCodeFilter1 = " And Sales_Invoices.Code Like '%" & pCode & "%'"
                'vCodeFilter2 = " And " & pTableName & "_WithTaxes.Code Like '%" & pCode & "%'"
            End If

            Dim vFromDate, vToDate, vToDate_PlusOneDay As String

            If Not TXT_FromDate.Value Is Nothing Then
                vFromDate = "'" & Format(TXT_FromDate.Value, "MM-dd-yyyy") & "'"
            Else
                vFromDate = "NULL"
            End If

            vToDate_PlusOneDay = Txt_ToDate.DateTime.AddDays(1)
            vToDate = "'" & Format(CDate(vToDate_PlusOneDay), "MM-dd-yyyy") & "'"

            If DTS_Summary.Band.Columns.Count = 0 Then
                Return
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText =
            " Select  POS.Code,                                " & vbCrLf &
            "         POS.DescA as PI_DescA,                   " & vbCrLf &
            "         Employees.DescA as Emp_Desc,             " & vbCrLf &
            "         Customers.DescA as Provider_Desc,        " & vbCrLf &
            "         POS.TDate,                               " & vbCrLf &
            "         POS.ShiftNum_Code                        " & vbCrLf &
            "                                                  " & vbCrLf &
            " From POS Inner Join Employees                    " & vbCrLf &
            " On   POS.Emp_Code = Employees.Code               " & vbCrLf &
            "                                                  " & vbCrLf &
            " LEFT Join Customers                              " & vbCrLf &
            " On POS.Customer_Code = Customers.Code            " & vbCrLf &
            "                                                  " & vbCrLf &
            " Where 1 = 1                                      " & vbCrLf &
            vCodeFilter1 & vbCrLf &
            " And (TDate >= " & vFromDate & " Or " & vFromDate & " Is NULL) " & vbCrLf &
            " And TDate < " & vToDate &
            " And Status = 'P' " &
            vSqlStatment

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
                    DTS_Summary.Rows(vRowCounter)("TDate") = Trim(vSqlReader(4))
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Summary.Rows(vRowCounter)("ShiftNum") = Trim(vSqlReader(5))
                Else
                    DTS_Summary.Rows(vRowCounter)("ShiftNum") = Nothing
                End If
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()

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
            " Select POS_Details.Ser,                              " & vbCrLf &
            "        Items.UserCode,                               " & vbCrLf &
            "        Items.DescA as Item_Desc,                     " & vbCrLf &
            "        POS_Details.Quantity,                         " & vbCrLf &
            "        POS_Details.Price,                            " & vbCrLf &
            "        POS_Details.Deduction,                        " & vbCrLf &
            "        POS_Details.Total                             " & vbCrLf &
            "                                                      " & vbCrLf &
            "        From  POS_Details Inner Join  Items           " & vbCrLf &
            "        On    POS_Details.Item_Code = Items.Code      " & vbCrLf &
            "                                                      " & vbCrLf &
            "        Left  Join Stores                             " & vbCrLf &
            "        On Stores.Code = POS_Details.Str_Code         " & vbCrLf &
            " Where POS_Details.POS_Code = '" & pRow("Code") & "' " & vbCrLf &
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
                vChildRows(vRowCounter)("Quantity") = Trim(vSqlReader(3))
                vChildRows(vRowCounter)("Price") = Trim(vSqlReader(4))

                If vSqlReader.IsDBNull(5) = False Then
                    vChildRows(vRowCounter)("Deduction") = Trim(vSqlReader(5))
                Else
                    vChildRows(vRowCounter)("Deduction") = Nothing
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    vChildRows(vRowCounter)("LCost") = Trim(vSqlReader(6))
                Else
                    vChildRows(vRowCounter)("LCost") = Nothing
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
    Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged, TXT_FromDate.ValueChanged

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
                    vLovReturn1 = vRow.ParentRow.Cells("Code").Value

                    If vRow.ParentRow.Cells("DescA").Text = "" Then
                        VLovReturn2 = vRow.ParentRow.Cells("Code").Text
                    Else
                        VLovReturn2 = vRow.ParentRow.Cells("DescA").Text
                    End If

                    vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Code") = vRow.ParentRow.Cells("Code").Value

                    'Here I Check if DescA is Empty then I Assign the Code to the Desc
                    If vRow.ParentRow.Cells("DescA").Text = "" Then
                        vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Desc") = vRow.ParentRow.Cells("Code").Value
                    Else
                        vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Desc") = vRow.ParentRow.Cells("DescA").Value
                    End If

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

                    vLovReturn1 = vRow.Cells("Code").Value
                    If vRow.Cells("DescA").Text = "" Then
                        VLovReturn2 = vRow.Cells("Code").Text
                    Else
                        VLovReturn2 = vRow.Cells("DescA").Text
                    End If

                    vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Code") = vRow.Cells("Code").Value

                    'Here I Check if DescA is Empty then I Assign the Code to the Desc
                    If vRow.Cells("DescA").Text = "" Then
                        vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Desc") = vRow.Cells("Code").Value
                    Else
                        vDTS_AllInvoices.Rows(vRowCounter)("Invoice_Desc") = vRow.Cells("DescA").Value
                    End If
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