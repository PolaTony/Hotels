Public Class Frm_ReportPreview_Optics

    Public Sub New(ByVal pTitle As String, ByVal pSqlStatment As String, ByVal pDataSet As DataSet, ByVal pReport As Object)
        Try
            InitializeComponent()

            Me.Text = pTitle
            Dim vSqlAdapter As New SqlClient.SqlDataAdapter(pSqlStatment, cControls.vSqlConn)
            pDataSet.Clear()
            vSqlAdapter.SelectCommand.CommandTimeout = 3600
            vSqlAdapter.Fill(pDataSet.Tables("Rep_FinancialElements"))
            pReport.SetDataSource(pDataSet)
            CrystalReportViewer1.ReportSource = pReport
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub New(ByVal pTitle As String, ByVal pSortedList As SortedList, ByVal pDataSet As DataSet, ByVal pReport As Object)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Try
            Me.Text = pTitle
            pDataSet.Clear()
            For Each vKey As Object In pSortedList.Keys
                Dim vSqlAdapter As New SqlClient.SqlDataAdapter(pSortedList.Item(vKey), cControls.vSqlConn)
                vSqlAdapter.SelectCommand.CommandTimeout = 3600
                vSqlAdapter.Fill(pDataSet.Tables(vKey))
            Next
            pReport.SetDataSource(pDataSet)
            CrystalReportViewer1.ReportSource = pReport
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub New(ByVal pTitle As String, ByVal pDataSet As DataSet, ByVal pReport As Object)
        InitializeComponent()

        Me.Text = pTitle
        pReport.SetDataSource(pDataSet)
        CrystalReportViewer1.ReportSource = pReport
    End Sub

    Public Sub New(ByVal pTitle As String, ByVal pSqlStatment As String, ByVal pDataSet As DataSet, ByVal pTableName As String, ByVal pReport As Object)
        Try
            InitializeComponent()

            Me.Text = pTitle
            Dim vSqlAdapter As New SqlClient.SqlDataAdapter(pSqlStatment, cControls.vSqlConn)
            vSqlAdapter.SelectCommand.CommandTimeout = 3600
            pDataSet.Clear()
            vSqlAdapter.Fill(pDataSet.Tables(pTableName))
            pReport.SetDataSource(pDataSet)
            CrystalReportViewer1.ReportSource = pReport
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub FRM_ReportPreviewL_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            'vcFrmLevel.vParentFrm = Me.ParentForm
            'vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", False)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class