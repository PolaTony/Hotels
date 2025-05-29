'Imports System.Management
Imports System.Data.SqlClient
Imports System.Drawing.Text
Imports System.IO

Module Mdl_Main
    Sub Main()
        Dim vBase As New cControls
        If vBase.vSuccess = False Then End

        Dim vCheckPassward As New Frm_CheckPassword_L
        vCheckPassward.ShowDialog()

        Dim vSQlcommand As New SqlCommand
        vSQlcommand.CommandText =
        " Select CPU_ID From Security "

        vSQlcommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
        Do While vSqlReader.Read
            If Not IsDBNull(vSqlReader(0)) Then
                If vSqlReader(0) = GetCPUId() Then
                    cControls.vSqlConn.Close()
                    vSqlReader.Close()
                    GoTo ready
                End If
            End If
        Loop

        cControls.vSqlConn.Close()
        vSqlReader.Close()


        'Dim vCounter As Integer = cControls.fReturnValue(" Select IsNull(Counter, 0) From Security ", "Mdl_Main")
        'If vCounter < 40 Then
        '    Dim vFRM_SoftwareScurity As New FRM_SoftwareScurity2
        '    vFRM_SoftwareScurity.ShowDialog()

        '    cControls.fSendData(" Update Security Set Counter = " & vCounter + 1, "Mdl_Main")
        'Else
        '    Dim vFrm_FalseCopy As New Frm_FalseCopy
        '    vFrm_FalseCopy.ShowDialog()
        'End If

        'Microsoft.Win32.Registry.LocalMachine.DeleteSubKey("SOFTWARE\Dot Net\CRM_Security")
        'Microsoft.Win32.Registry.LocalMachine.CreateSubKey(vSecurity_Path).SetValue("XSSES", sEncrypt(vCounter - 1))

ready:  Dim vSplash As New SplashScreen1
        vSplash.ShowDialog()

        If vLang = "A" Then
            Dim vMdiForm As New MDIForm
            Application.EnableVisualStyles()
            Application.Run(vMdiForm)
        Else
            Dim vMdiForm As New MDIForm_L
            Application.EnableVisualStyles()
            Application.Run(vMdiForm)
        End If

    End Sub

    'Public Function fGetCPUId() As String
    'Dim cpuInfo As String = String.Empty
    'Dim temp As String = String.Empty
    'Dim mc As ManagementClass = _
    '    New ManagementClass("Win32_Processor")
    'Dim moc As ManagementObjectCollection = mc.GetInstances()
    'For Each mo As ManagementObject In moc
    '    If cpuInfo = String.Empty Then
    '        cpuInfo = _
    '         mo.Properties("ProcessorId").Value.ToString()
    '    End If
    'Next
    'Return cpuInfo
    'End Function
End Module
