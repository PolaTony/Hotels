Imports Microsoft.SqlServer.Management.Smo

Public Class Frm_DatabaseMangment
    Public vSuccess As Boolean
    Public vConnection As String
    Dim vChooseServer As String
    Dim vAlt_Server_Connection_Succedded As Boolean = False

    Private Sub Frm_DatabaseMangment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sFillListAvailableSQLServers()

        'vChooseServer = cControls.fReturnValue(" Select IsNull(ChooseServer, 'N') From Controls ", "")
        'If vChooseServer = "Y" Then
        '    Chk_ChooseServer.Checked = True
        'Else
        '    Chk_ChooseServer.Checked = False
        'End If

    End Sub

    Function fTestConnection(Optional ByVal pReturnMsg As Boolean = True) As Boolean
        Try
            Dim vConn As New Microsoft.SqlServer.Management.Common.ServerConnection(TXT_ServerName.Text, Txt_LogIn.Text, TXT_Password.Text)
            Dim vServer As New Microsoft.SqlServer.Management.Smo.Server(vConn)
            vConn.Connect()

            If pReturnMsg Then
                MsgBox("Test connection succeeded", MsgBoxStyle.Information, "Test")
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Function fAlt_TestConnection(Optional ByVal pReturnMsg As Boolean = True) As Boolean
        Try
            Dim vConn As New Microsoft.SqlServer.Management.Common.ServerConnection(Txt_Alt_ServerName.Text, Txt_Alt_Login.Text, Txt_Alt_Password.Text)
            Dim vServer As New Microsoft.SqlServer.Management.Smo.Server(vConn)
            vConn.Connect()

            If pReturnMsg Then
                vAlt_Server_Connection_Succedded = True
                MsgBox("Alternative Server Test connection succeeded", MsgBoxStyle.Information, "Test")
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Sub sFillListAvailableSQLServers()
        Dim a As DataTable
        Dim vRow As DataRow
        Dim oSQLApp As SmoApplication
        oSQLApp = New SmoApplication
        a = oSQLApp.EnumAvailableSqlServers

        For Each vRow In a.Rows
            Txt_ServerName.Items.Add(vRow(0))
            Txt_Alt_ServerName.Items.Add(vRow(0))
        Next
    End Sub
    Private Sub TXT_Database_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXT_Database.Enter
        Try
            Dim vDB As Microsoft.SqlServer.Management.Smo.Database
            Dim vConn As New Microsoft.SqlServer.Management.Common.ServerConnection(TXT_ServerName.Text, Txt_LogIn.Text, TXT_Password.Text)
            Dim vServer As New Microsoft.SqlServer.Management.Smo.Server(vConn)
            vConn.Connect()
            For Each vDB In vServer.Databases
                TXT_Database.Items.Add(vDB.Name)
            Next
        Catch vEx As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Alt_DataBase_Enter(sender As Object, e As EventArgs) Handles Txt_Alt_DataBase.Enter
        Try
            Dim vDB As Microsoft.SqlServer.Management.Smo.Database
            Dim vConn As New Microsoft.SqlServer.Management.Common.ServerConnection(Txt_Alt_ServerName.Text, Txt_Alt_Login.Text, Txt_Alt_Password.Text)
            Dim vServer As New Microsoft.SqlServer.Management.Smo.Server(vConn)
            vConn.Connect()
            For Each vDB In vServer.Databases
                Txt_Alt_DataBase.Items.Add(vDB.Name)
            Next
        Catch vEx As Exception
            Exit Sub
        End Try
    End Sub

    Sub sSaveConnection()
        Dim vRegVer As Microsoft.Win32.RegistryKey
        Dim vPath As String
        vPath = "SOFTWARE\Dot Net\Hotels"
        vRegVer = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(vPath)
        If fTestConnection(False) = False Then
            Exit Sub
        Else
            'Here I Check if Alternative Server and DataBase is Entered
            If Txt_Alt_DataBase.SelectedIndex <> -1 Then
                If fAlt_TestConnection(False) = False Then
                    Exit Sub
                End If
            End If
            vSuccess = True
        End If

        vConnection = "data source=" & Txt_ServerName.Value & ";Failover Partner=DELL-PC\SQL_2014;initial catalog=" & TXT_Database.Value & ";user id=" & Txt_LogIn.Text & " ;password=" & TXT_Password.Text & ";Connect Timeout=" & 15

        cControls.vConnectionString = vConnection
        Dim vEncryptedConnection As String = vConnection
        Microsoft.Win32.Registry.LocalMachine.CreateSubKey(vPath).SetValue("XSSES", vEncryptedConnection)
        MsgBox("Configuration is saved successfully", MsgBoxStyle.Information, "Cofiguration")

        'Here I will Save the Alternative Server Connection String if it succedded
        If vAlt_Server_Connection_Succedded Then
            vConnection = "data source=" & Txt_Alt_ServerName.Value & ";initial catalog=" & Txt_Alt_DataBase.Value & ";user id=" & Txt_Alt_Login.Text & " ;password=" & Txt_Alt_Password.Text & ";Connect Timeout=" & 15

            cControls.vConnectionString = vConnection
            Dim vAlt_EncryptedConnection As String = vConnection
            Microsoft.Win32.Registry.LocalMachine.CreateSubKey(vPath).SetValue("Alt_XSSES", vAlt_EncryptedConnection)
            'MsgBox("Configuration For Alternative Server is saved successfully", MsgBoxStyle.Information, "Cofiguration")
        End If

        'If Chk_ChooseServer.Checked Then
        '    vChooseServer = "Y"
        'Else
        '    vChooseServer = "N"
        'End If

        'cControls.fSendData(" Update Controls Set ChooseServer = '" & vChooseServer & "'", "")

    End Sub

    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        If TXT_Database.SelectedIndex <> -1 Then
            If fTestConnection() = False Then
                Return
            End If

            If Txt_Alt_DataBase.SelectedIndex <> -1 Then
                If fAlt_TestConnection() = False Then
                    Return
                End If
            End If

            sSaveConnection()
            Me.Close()
        Else
            MessageBox.Show("Select DataBase First")
        End If
    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub

End Class