Imports System.Data.SqlClient
Imports Infragistics.Win

Public Class cControls
    Public Shared vConnectionString As String
    Public vSuccess As Boolean

#Region " Connection Check                                                                               "
    Private Sub sTestConenction()
        Try
            Dim vRegVer As Microsoft.Win32.RegistryKey
            Dim vPath As String
            Dim vDecryptedText As String = ""
            vPath = "SOFTWARE\Dot Net\Hotels"
            vRegVer = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(vPath)
            vDecryptedText = vRegVer.GetValue("XSSES")
            vConnectionString = vDecryptedText

            Dim vSqlConnection As New System.Data.SqlClient.SqlConnection(vConnectionString)
            Dim vConn As New Microsoft.SqlServer.Management.Common.ServerConnection(vSqlConnection)
            vConn.Connect()
            vConn.Disconnect()
            vSuccess = True

            vServerName = vConn.ServerInstance
            vDataBaseName = vConn.DatabaseName

            sFill_ServerName_SortedList()

            'Here I Check if the server Name exist in the list if not stop the program
            If Not vSortedList_ServerName.ContainsValue(vServerName) Then
                MessageBox.Show("ÎØÃ Ýí ÇÏÎÇá ÇáÈíÇäÇÊ")
                vSuccess = False
            End If
        Catch ex As Exception
            'Here I will Check first the alternative Server, If it also no connection then will get the database Management screen

            'sTest_Alternative_Server()

            Dim vFRM_DataBaseMangment As New Frm_DatabaseMangment
            vFRM_DataBaseMangment.ShowDialog()

            If vFRM_DataBaseMangment.vSuccess = True Then
                sTestConenction()
                vSuccess = True
            Else
                vSuccess = False
            End If
        End Try
    End Sub

    Private Sub sTest_Alternative_Server()
        Try
            Dim vRegVer As Microsoft.Win32.RegistryKey
            Dim vPath As String
            Dim vDecryptedText As String = ""
            vPath = "SOFTWARE\Dot Net\ERP"
            vRegVer = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(vPath)
            vDecryptedText = vRegVer.GetValue("Alt_XSSES")
            vConnectionString = vDecryptedText

            Dim vSqlConnection As New System.Data.SqlClient.SqlConnection(vConnectionString)
            Dim vConn As New Microsoft.SqlServer.Management.Common.ServerConnection(vSqlConnection)
            vConn.Connect()
            vConn.Disconnect()
            vSuccess = True

            vServerName = vConn.ServerInstance
            vDataBaseName = vConn.DatabaseName
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region " Initialize  Sql Connection                                                                     "

    Public Shared vSqlConn As SqlConnection
    Sub New()

        sTestConenction()
        If vSuccess = False Then
            Return
        End If
        vSqlConn = New SqlConnection(vConnectionString)

        'Here I check if the check Server in every time is checked...
        If cControls.fReturnValue(" Select IsNull(ChooseServer, 'N') From Controls ", "") = "Y" Then
            Dim vFRM_DataBaseMangment As New Frm_DatabaseMangment
            vFRM_DataBaseMangment.ShowDialog()
            If vFRM_DataBaseMangment.vSuccess = True Then

                sTestConenction()
                vSuccess = True
            Else
                vSuccess = False
            End If
        End If
    End Sub
#End Region
#Region " Connecting with DataBase                                                                       "
    Public Shared Function fSendData(ByVal pSqlStatment As String, ByVal pFormName As String) As Long
        Dim vTrans As SqlTransaction
        Try
            Dim vSqlCommand As New SqlCommand(pSqlStatment, vSqlConn)
            Dim vNoOfRowsAffected As Long

            vSqlConn.Open()
            vTrans = vSqlConn.BeginTransaction(IsolationLevel.ReadCommitted)
            vSqlCommand.Transaction = vTrans
            vNoOfRowsAffected = vSqlCommand.ExecuteNonQuery()
            vTrans.Commit()
            vSqlConn.Close()

            Return vNoOfRowsAffected
        Catch vex As SqlException
            vTrans.Rollback()
            If vex.Number = 547 Then
                vSqlConn.Close()
                MDIForm.sForwardMessage("2", Nothing)
            Else
                MessageBox.Show(vex.Message)
            End If
            Return -1
        Catch ex As Exception
            vTrans.Rollback()
            vSqlConn.Close()
            MessageBox.Show(ex.Message)
            Return -1
        Finally
            If vSqlConn.State = ConnectionState.Open Or vSqlConn.State = ConnectionState.Broken Then
                vSqlConn.Close()
            End If
        End Try
    End Function
    Public Shared Function fSendData(ByVal pSqlStatment() As String, ByVal pFormName As String) As Long

        Dim vNoOfRowsAffected As Long
        Dim vCounter As Long
        Dim vSqlCommand As SqlCommand
        Dim vTrans As SqlTransaction
        Try
            If pSqlStatment.Length = 1 And pSqlStatment(0) = "" Then
                Exit Function
            End If

            vSqlConn.Open()
            vTrans = vSqlConn.BeginTransaction(IsolationLevel.ReadCommitted)
            For vCounter = 0 To pSqlStatment.Length - 1
                vSqlCommand = New SqlCommand(pSqlStatment(vCounter), vSqlConn)
                vSqlCommand.CommandTimeout = 30000
                vSqlCommand.Transaction = vTrans
                vNoOfRowsAffected = vNoOfRowsAffected + vSqlCommand.ExecuteNonQuery()
            Next
            vTrans.Commit()
            vSqlConn.Close()
            Return vNoOfRowsAffected
        Catch vex As SqlException
            If vex.Number = 547 Then
                vTrans.Rollback()
                vSqlConn.Close()
                MDIForm.sForwardMessage("47", Nothing)
            ElseIf vex.Number = 2627 Then
                vTrans.Rollback()
                vSqlConn.Close()
                MDIForm.sForwardMessage("48", Nothing)
            Else
                vTrans.Rollback()
                vSqlConn.Close()
                MessageBox.Show(vex.Message)
                'cException.sHandleException(vex.Message, pFormName, "cbase.fSendData")
            End If
            Return -1
        Catch ex As Exception
            vTrans.Rollback()
            vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, pFormName, "cbase.fSendData")
            Return -1
        Finally
            If vSqlConn.State = ConnectionState.Open Or vSqlConn.State = ConnectionState.Broken Then
                vSqlConn.Close()
            End If
        End Try
    End Function
    Public Shared Function fReturnValue(ByVal pSqlStatment As String, ByVal pFormName As String) As String

        Try
            Dim vSqlCommand As New SqlCommand(pSqlStatment, vSqlConn)
            vSqlCommand.CommandTimeout = 3600
            Dim vReturn As String
            vSqlConn.Open()
            If IsDBNull(vSqlCommand.ExecuteScalar) Then
                vReturn = ""
            Else
                vReturn = Trim(vSqlCommand.ExecuteScalar)
            End If
            vSqlConn.Close()
            Return vReturn
        Catch vex As SqlException
            If vex.Number = 547 Then
                vSqlConn.Close()
                MDIForm.sForwardMessage("66", Nothing)
            Else
                MessageBox.Show(vex.Message)
            End If
            Return -1
        Catch ex As Exception
            If vSqlConn IsNot Nothing Then
                vSqlConn.Close()
            End If

            MessageBox.Show(ex.Message)
            Return 0
        Finally
            If vSqlConn IsNot Nothing Then
                If vSqlConn.State = ConnectionState.Open Or vSqlConn.State = ConnectionState.Broken Then
                    vSqlConn.Close()
                End If
            End If
        End Try
    End Function
    Public Shared Function fCount_Rec(ByVal pSqlStatment) As Integer
        Try
            Dim vCount_Rec As Integer = 0
            Dim vSql_Command As New Data.SqlClient.SqlCommand
            vSql_Command.Connection = vSqlConn
            vSql_Command.CommandText = _
            " Select Count(*) " & _
            pSqlStatment
            Dim Sql_Reader As Data.SqlClient.SqlDataReader
            vSqlConn.Open()
            Sql_Reader = vSql_Command.ExecuteReader
            Do While Sql_Reader.Read()
                If Sql_Reader.IsDBNull(0) = False Then
                    vCount_Rec = Sql_Reader.GetInt32(0)
                End If
                Exit Do
            Loop
            Sql_Reader.Close()
            vSqlConn.Close()
            Return vCount_Rec
        Catch ex As Exception
            vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, "cBase", "fCount_Rec")
            Return 0
        Finally
            If vSqlConn.State = ConnectionState.Open Or vSqlConn.State = ConnectionState.Broken Then
                vSqlConn.Close()
            End If
        End Try
    End Function
    Public Shared Function fIsExist(ByVal pSqlStatment As String, ByVal pFormName As String) As Boolean
        Try
            Dim vSqlCommand As New SqlCommand("IF Exists ( Select * " & pSqlStatment & ") Begin Select 'True' End Else Begin Select 'False' End", vSqlConn)
            Dim vReturn As String
            vSqlConn.Open()
            If IsDBNull(vSqlCommand.ExecuteScalar) Then
                vReturn = ""
            Else
                vReturn = Trim(vSqlCommand.ExecuteScalar)
            End If

            Return vReturn

            vSqlConn.Close()
            Return vReturn
        Catch vex As SqlException
            If vex.Number = 547 Then
                vSqlConn.Close()
                'MDIForm.sForwardMessage("66", Nothing)
            Else
                'MessageBox.Show(vex.Message)
            End If
            Return -1
        Catch ex As Exception
            vSqlConn.Close()
            'MessageBox.Show(ex.Message)
            Return 0
        Finally
            If vSqlConn.State = ConnectionState.Open Or vSqlConn.State = ConnectionState.Broken Then
                vSqlConn.Close()
            End If
        End Try
    End Function

    Public Shared Sub sSaveImage(ByVal pImage As Image, ByVal pTableName As String)
        Try
            Dim ms As New System.IO.MemoryStream
            Dim arrPicture() As Byte
            Dim vMyCommand As New SqlCommand("Update  " & pTableName & " Set Picture = (@image)", vSqlConn)
            If Not IsNothing(pImage) Then
                pImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                'If ms.Length > MAX_IMAGE_SIZE Then
                '    MsgBox("image trop grosse")
                'End If
                arrPicture = ms.GetBuffer()
                ms.Flush()
                vMyCommand.Parameters.Add("@image", SqlDbType.Image).Value = arrPicture
            Else
                vMyCommand.Parameters.Add("@image", SqlDbType.Image).Value = DBNull.Value
            End If
            vSqlConn.Open()
            vMyCommand.ExecuteNonQuery()
            vSqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            vSqlConn.Close()
        End Try
    End Sub
    Public Shared Function fFixQuote(ByVal strIn As String) As String
        Dim y As Integer
        If InStr(strIn, "'") <> 0 Then
            For y = 1 To Len(strIn)
                If Mid(strIn, y, 1) = "'" Then
                    fFixQuote = fFixQuote + "''"
                Else
                    fFixQuote = fFixQuote + Mid(strIn, y, 1)
                End If
            Next
        Else
            fFixQuote = strIn
        End If
    End Function

    Public Shared Sub sLoadSettings(ByVal pFormName As String, ByVal pGrd As Object)

        Try
            Dim LayoutArray() As Byte
            Dim vSqlString As String = " Select LayOut_Data          " &
                                       " From  Employees_Layout_Settings     " &
                                       " Where Emp_Code = '" & vUsrCode & "' " &
                                       " And   Mod_Code = '" & pFormName & "' " &
                                       " And   Grd_Name = '" & pGrd.Name & "' "

            Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)

            cControls.vSqlConn.Open()
            LayoutArray = vMyCommand.ExecuteScalar
            cControls.vSqlConn.Close()

            If LayoutArray Is Nothing Then
                Exit Sub
            End If

            Dim MS As New IO.MemoryStream(LayoutArray)
            MS.Seek(0, IO.SeekOrigin.Begin)

            If TypeOf pGrd Is UltraWinGrid.UltraGrid Then
                pGrd.DisplayLayout.Load(MS, Infragistics.Win.UltraWinGrid.PropertyCategories.All)
            ElseIf TypeOf pGrd Is DevExpress.XtraPivotGrid.PivotGridControl Then
                pGrd.RestoreLayoutFromStream(MS)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Public Shared Sub sSaveSettings(ByVal pFormName As String, ByVal pGrd As Object)
        Try
            Dim MS As New IO.MemoryStream()

            If TypeOf pGrd Is UltraWinGrid.UltraGrid Then
                pGrd.DisplayLayout.Save(MS, Infragistics.Win.UltraWinGrid.PropertyCategories.All)
            ElseIf TypeOf pGrd Is DevExpress.XtraPivotGrid.PivotGridControl Then
                pGrd.SaveLayoutToStream(MS)
            ElseIf TypeOf pGrd Is DevExpress.XtraBars.Ribbon.RibbonControl Then
                Dim vXML As String
                pGrd.saveLayouttoXml(vXML)
            End If

            Dim LayoutArray() As Byte
            LayoutArray = MS.ToArray()

            cControls.fSendData(" Delete From Employees_Layout_Settings        " &
                                " Where  Emp_Code = '" & vUsrCode & "' " &
                                " And    Mod_Code = '" & pFormName & "' " &
                                " And    Grd_Name = '" & pGrd.Name & "' ", pFormName)

            Dim vSqlString As String = " Insert Into Employees_Layout_Settings (     Emp_Code,           Mod_Code,        LayOut_Data,       Grd_Name   ) " &
                                       "                                Values ('" & vUsrCode & "', '" & pFormName & "',  (@FileData), '" & pGrd.Name & "'  ) "

            Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)
            vMyCommand.Parameters.AddWithValue("@FileData ", LayoutArray)

            cControls.vSqlConn.Open()
            vMyCommand.ExecuteNonQuery()
            cControls.vSqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
#End Region
End Class
