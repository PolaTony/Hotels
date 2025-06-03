Imports System.Data.SqlClient
Imports System.IO
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_NewMessage_A
    Private Declare Function ShellEx Lib "shell32.dll" Alias "ShellExecuteA" ( _
    ByVal hWnd As Integer, ByVal lpOperation As String, _
    ByVal lpFile As String, ByVal lpParameters As String, _
    ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

#Region " Declaration                                                               "
    Dim vSqlStatment(0) As String
    Dim vCode As String
    Dim vType As String
#End Region
#Region " Form Level                                                                "
    Public Sub New(ByVal pType As String)
        vType = pType

        InitializeComponent()

    End Sub
    Private Sub Frm_NewMessage_A_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sLoadUsers()
        sLoadOthers()

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
#End Region

#Region " Message Editor                                                            "

    Private Sub Txt_FontName_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FontName.ValueChanged
        Txt_Message.Appearance.FontData.Name = Txt_FontName.Text
    End Sub
    Private Sub Txt_FontSize_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FontSize.ValueChanged
        Txt_Message.Appearance.FontData.SizeInPoints = Txt_FontSize.Value
    End Sub
    Private Sub UltraColorPicker1_ColorChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraColorPicker1.ColorChanged
        Txt_Message.Appearance.ForeColor = UltraColorPicker1.Color
    End Sub
    Private Sub Chk_Bold_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_Bold.CheckedChanged
        Try
            If Chk_Bold.Checked Then
                Txt_Message.Appearance.FontData.Bold = DefaultableBoolean.True
            Else
                Txt_Message.Appearance.FontData.Bold = DefaultableBoolean.False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Chk_Italic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_Italic.CheckedChanged
        Try
            If Chk_Italic.Checked Then
                Txt_Message.Appearance.FontData.Italic = DefaultableBoolean.True
            Else
                Txt_Message.Appearance.FontData.Italic = DefaultableBoolean.False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub
    Private Sub Chk_UnderLine_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_UnderLine.CheckedChanged
        Try
            If Chk_UnderLine.Checked Then
                Txt_Message.Appearance.FontData.Underline = DefaultableBoolean.True
            Else
                Txt_Message.Appearance.FontData.Underline = DefaultableBoolean.False
            End If

        Catch ex As Exception

        End Try

    End Sub
#End Region

    Private Sub sLoadUsers()
        'TXT_User.Items.Clear()
        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText = " Select Code, DescA From Employees " & _
                                      " Where EmployeeType In ('E', 'C') "

            vSqlCommand.Connection = cControls.vSqlConn

            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSqlCommand.ExecuteReader

            Txt_Sendto.Items.Clear()
            Do While vSqlReader.Read
                Me.Txt_Sendto.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub sLoadOthers()
        'TXT_User.Items.Clear()
        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText = " Select Code, DescA From Employees " & _
                                      " Where EmployeeType In ('E', 'C') "

            vSqlCommand.Connection = cControls.vSqlConn

            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSqlCommand.ExecuteReader

            Txt_Others.Items.Clear()
            Do While vSqlReader.Read
                Me.Txt_Others.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Btn_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Send.Click
        If Txt_Message.Text = "" Then
            MessageBox.Show("تأكد من ادخال الرسالة")
            Txt_Message.Select()
            Exit Sub
        End If

        If Txt_Sendto.SelectedIndex = -1 Then
            MessageBox.Show("تأكد من اختيار المرسل اليه")
            Txt_Message.Select()
            Exit Sub
        End If

        Dim vListItem As ValueListItem
        'Dim vCounter As Int16 = 0
        'For Each vListItem In Txt_Others.Items
        '    If vListItem.CheckState = CheckState.Checked Then
        '        vCounter += 1
        '    End If
        'Next

        'If vCounter = 0 Then
        '    MessageBox.Show("تأكد من اختيار شخص على الأقل")
        '    Txt_Others.Select()
        '    Exit Sub
        'End If

        Dim vSqlString As String
        Dim vHaveAttachments As String

        If Grd_Details.Rows.Count > 0 Then
            vHaveAttachments = "'Y'"
        Else
            vHaveAttachments = "'N'"
        End If

        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From  Inbox "
        vCode = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(6, "0")

        sEmptySqlStatmentArray()



        vSqlString = " Insert Into Inbox  (     Code,           From_Emp_Code,             To_Emp_Code,                    Message,                    TDate,       IsDeliverd,        IsRead,        HaveAttachments ) " & _
                     "             Values ('" & vCode & "', '" & vUsrCode & "', '" & Trim(Txt_Sendto.Value) & "', '" & Txt_Message.Value & "',        GetDate(),        'N',              'N',    " & vHaveAttachments & ") "

        sFillSqlStatmentArray(vSqlString)

        For Each vListItem In Txt_Others.Items
            If vListItem.CheckState = CheckState.Checked Then
                If Trim(vListItem.DataValue) <> Trim(Txt_Sendto.Value) Then
                    vSqlString = " Insert Into Inbox_To (   IN_Code,                To_Emp_Code ) " & _
                             "                  Values  ('" & vCode & "', '" & vListItem.DataValue & "' )"

                    sFillSqlStatmentArray(vSqlString)
                End If
            End If
        Next

        If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then
            'If Message Saved Successfully then I start to save the Attachments
            If fValidateSave() Then
                sSaveDetails()
            End If

            Me.Close()
        End If

    End Sub

#Region " Details                                                                        "
#Region " DataBase                                                                       "
#Region " Query                                                                          "
    Private Sub sQueryDetails()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Ser,                   " & _
            "        FileData,              " & _
            "        DescA,                 " & _
            "        CompleteFileName,      " & _
            "        FileName,              " & _
            "        FileType               " & _
            " From   Inbox_Attachments        " & _
            " Where  IN_Code  =       '" & vCode & "'"

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vRow As UltraGridRow
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Details.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Details.Rows.SetCount(vRowCounter + 1)
                DTS_Details.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))

                'Picture
                If vSqlReader.IsDBNull(1) = False Then
                    'Dim vVar As Object = DTS_Details.Rows(vRowCounter)("Picture").GetType
                    Dim arrayImage() As Byte = CType(vSqlReader(1), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)

                    If vSqlReader.IsDBNull(4) = False Then
                        If Trim(vSqlReader(4)) = "Image" Then
                            DTS_Details.Rows(vRowCounter)("Picture") = Image.FromStream(ms)
                        End If
                    End If

                Else
                    DTS_Details.Rows(vRowCounter)("Picture") = Nothing
                End If

                'DescA
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Details.Rows(vRowCounter)("DescL") = Trim(vSqlReader(2))
                Else
                    DTS_Details.Rows(vRowCounter)("DescL") = ""
                End If

                'File Name
                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Details.Rows(vRowCounter)("CompleteFileName") = Trim(vSqlReader(3))
                Else
                    DTS_Details.Rows(vRowCounter)("CompleteFileName") = ""
                End If

                'File Name
                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Details.Rows(vRowCounter)("FileName") = Trim(vSqlReader(4))
                Else
                    DTS_Details.Rows(vRowCounter)("FileName") = ""
                End If

                'File Type
                If vSqlReader.IsDBNull(5) = False Then
                    If Trim(LCase(vSqlReader(5))) = ".bmp" Or _
                        Trim(LCase(vSqlReader(5))) = ".jpg" Or _
                        Trim(LCase(vSqlReader(5))) = ".jpeg" Or _
                        Trim(LCase(vSqlReader(5))) = ".png" Or _
                        Trim(LCase(vSqlReader(5))) = ".gif" Or _
                        Trim(LCase(vSqlReader(5))) = "image" Then

                        DTS_Details.Rows(vRowCounter)("FileType") = "Image"
                    ElseIf Trim(LCase(vSqlReader(5))) = ".docx" Then
                        DTS_Details.Rows(vRowCounter)("FileType") = ".docx"
                    ElseIf Trim(LCase(vSqlReader(5))) = ".xls" Then
                        DTS_Details.Rows(vRowCounter)("FileType") = ".xls"
                    ElseIf Trim(LCase(vSqlReader(5))) = ".pdf" Then
                        DTS_Details.Rows(vRowCounter)("FileType") = ".pdf"
                    End If
                Else
                    DTS_Details.Rows(vRowCounter)("FileType") = ""
                End If

                DTS_Details.Rows(vRowCounter)("SerNum") = DTS_Details.Rows(vRowCounter).Index + 1
                DTS_Details.Rows(vRowCounter)("DML") = "N"

                'Grd_Details.Rows(vRowCounter).Height = 200

                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Grd_Details.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQueryDetails")
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#Region " Save                                                                           "

    Private Function fValidateSave() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            If vRow.Cells("FileName").Text = "" Then
                vRow.Cells("FileName").Selected = True
                MessageBox.Show("Select the file first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        Return True
    End Function
    Private Function sSaveDetails() As Boolean
        Dim vSqlString As String
        Dim vGetSerial As String
        Dim vCounter As Integer = 0

        Try
            For Each vRow As UltraGridRow In Grd_Details.Rows
                'Call Upload Images Or File

                Dim Extension As String = System.IO.Path.GetExtension(vRow.Cells("FileName").Text)
                Dim vFileType As String

                Dim imageData As Byte()
                Dim sFileName As String

                Dim FileData() As Byte

                If vRow.Cells("CompleteFileName").Text <> "" Then
                    FileData = ReadFileData(vRow.Cells("CompleteFileName").Text)
                End If

                If vRow.Cells("DML").Value = "I" Then

                    vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Inbox_Attachments " & _
                                 " Where IN_Code = '" & vCode & "'"

                    vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                    vSqlString = " Insert Into Inbox_Attachments (    IN_Code,           Ser,           FileData,                   DescA,                               FileName,                   FileType ) " & _
                                 "             Values           ('" & vCode & "', " & vGetSerial & ", (@FileData), '" & vRow.Cells("DescL").Text & "', '" & vRow.Cells("FileName").Text & "', '" & vFileType & "')"

                    Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)
                    vMyCommand.Parameters.AddWithValue("@FileData ", FileData)

                    cControls.vSqlConn.Open()
                    vMyCommand.ExecuteNonQuery()
                    cControls.vSqlConn.Close()

                    vCounter += 1
                ElseIf vRow.Cells("DML").Value = "U" Then
                    Dim ms As New System.IO.MemoryStream
                    Dim arrPicture() As Byte

                    vSqlString = " Update Inbox_Attachments " & _
                                 " Set    DescA = '" & vRow.Cells("DescL").Text & "',  " & _
                                 "        FileData = (@FileData),                     " & _
                                 "        FileName = '" & vRow.Cells("FileName").Text & "', " & _
                                 "        FileType = '" & vFileType & "'" & _
                                 " Where  IN_Code  = '" & vCode & "'" & _
                                 " And    Ser      = '" & vRow.Cells("Ser").Text & "'"

                    Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)
                    vMyCommand.Parameters.AddWithValue("@FileData ", FileData)

                    cControls.vSqlConn.Open()
                    vMyCommand.ExecuteNonQuery()
                    cControls.vSqlConn.Close()

                End If
            Next

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()

            Return False
        End Try

    End Function

    Private Function ReadFileData(ByVal filename As String) As Byte()
        Dim fs As New System.IO.FileStream(filename, IO.FileMode.Open)
        Dim br As New System.IO.BinaryReader(fs)

        Dim data() As Byte = br.ReadBytes(fs.Length)

        br.Close()
        fs.Close()

        Return (data)
    End Function

    Private Sub WriteFileData(ByVal pCompleteFileName As String, ByVal pFilename As String, ByVal pData As Byte())

        'Dim vFileName As String
        'For Each vFileName In Directory.GetFiles("C:\DotNet_eg_Files")
        '    If vFileName = pFilename Then
        '        GoTo Done
        '    End If
        'Next

        Dim fs As New System.IO.FileStream("C:\DotNet_eg_Files\" & pFilename, IO.FileMode.Create)
        Dim bw As New System.IO.BinaryWriter(fs)

        bw.Write(pData)

        bw.Close()
        fs.Close()

        Dim vProcess As New Process
        vProcess.StartInfo.FileName = "C:\DotNet_eg_Files\" & pFilename
        vProcess.StartInfo.CreateNoWindow = False
        vProcess.Start()


    End Sub
#End Region
#End Region
#Region " Grd                                                                            "
    Private Sub Grd_Details_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles Grd_Details.AfterRowInsert
        e.Row.Cells("SerNum").Value = e.Row.Index + 1
    End Sub

    Private Sub Grd_Details_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Details.CellChange
        If Grd_Details.ActiveRow.Cells("DML").Value = "NI" Then
            Grd_Details.ActiveRow.Cells("DML").Value = "I"
        ElseIf Grd_Details.ActiveRow.Cells("DML").Value = "N" Then
            Grd_Details.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub

    Private Sub Grd_Details_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Details.ClickCellButton
        Try
            'Grd_Details.ActiveRow.Cells("Picture").Value = Image.FromFile(OpenFileDialog1.FileName)
            'Grd_Details.ActiveRow.Height = 200

            'PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
            'Dim x As Integer = cControls.sSaveImage(Image.FromFile(OpenFileDialog1.FileName), "Company")
            Dim vFileName As String
            Dim vRowCounter, vIndex As Integer

            If Grd_Details.ActiveRow.Cells("AddNew").Activated Then
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    If OpenFileDialog1.FileNames.Length > 1 Then
                        vRowCounter = DTS_Details.Rows.Count
                        vIndex = 0
                        For Each vFileName In OpenFileDialog1.FileNames
                            DTS_Details.Rows.SetCount(vRowCounter + 1)
                            DTS_Details.Rows(vRowCounter)("FileName") = OpenFileDialog1.SafeFileNames(vIndex)
                            DTS_Details.Rows(vRowCounter)("CompleteFileName") = vFileName
                            DTS_Details.Rows(vRowCounter)("DescL") = OpenFileDialog1.SafeFileNames(vIndex)
                            DTS_Details.Rows(vRowCounter)("SerNum") = vRowCounter
                            DTS_Details.Rows(vRowCounter)("DML") = "I"
                            vRowCounter += 1
                            vIndex += 1
                        Next
                    Else
                        Grd_Details.ActiveRow.Cells("FileName").Value = OpenFileDialog1.SafeFileName
                        Grd_Details.ActiveRow.Cells("CompleteFileName").Value = OpenFileDialog1.FileName
                        Grd_Details.ActiveRow.Cells("DescL").Value = OpenFileDialog1.SafeFileName

                        If Grd_Details.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_Details.ActiveRow.Cells("DML").Value = "I"
                        ElseIf Grd_Details.ActiveRow.Cells("DML").Value = "N" Then
                            Grd_Details.ActiveRow.Cells("DML").Value = "U"
                        End If
                    End If
                    'If OpenFileDialog1.FileName <> "" Then

                    'End If
                End If

            ElseIf Grd_Details.ActiveRow.Cells("View").Activated Then

                'Temporary I Check if the File path is exist and open it Through Process Function..
                If Grd_Details.ActiveRow.Cells("FileName").Text <> "" Then
                    If Grd_Details.ActiveRow.Cells("DML").Text = "I" Or Grd_Details.ActiveRow.Cells("DML").Text = "NI" Then
                        sOpenUnSavedFiles(Grd_Details.ActiveRow.Cells("CompleteFileName").Text)
                        Exit Sub
                    ElseIf Grd_Details.ActiveRow.Cells("DML").Text = "N" Or Grd_Details.ActiveRow.Cells("DML").Text = "U" Then
                        If Not Directory.Exists("C:\DotNet_eg_Files") Then
                            Directory.CreateDirectory("C:\DotNet_eg_Files")
                        End If

                        cControls.vSqlConn.Close()

                        Dim cmd As New SqlCommand("", cControls.vSqlConn)

                        cmd.CommandText = " Select FileData FROM Inbox_Attachments " & _
                                          " WHERE IN_Code = '" & vCode & "'" & _
                                          " AND   Ser      = " & Grd_Details.ActiveRow.Cells("Ser").Value

                        cControls.vSqlConn.Open()

                        Dim rdr As SqlDataReader = cmd.ExecuteReader

                        If rdr.Read Then
                            WriteFileData(Grd_Details.ActiveRow.Cells("CompleteFileName").Text, Grd_Details.ActiveRow.Cells("FileName").Text, rdr("FileData"))
                        Else
                            MsgBox(Grd_Details.ActiveRow.Cells("FileName").Text & " not found")
                        End If

                        rdr.Close()
                        cControls.vSqlConn.Close()
                    End If
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub downLoadFile(ByVal iFileId As Long, ByVal sFileName As String, ByVal sFileExtension As String)
        Dim strSql As String
        'For Document
        Try
            'Get image data from gridview column. 
            strSql = "Select FileData from Customers_Deals_Details_Attachments " & _
                     " WHERE IN_Code = '" & vCode & "'" & _
                     " And   Ser = " & Grd_Details.ActiveRow.Cells("Ser").Value

            Dim sqlCmd As New SqlCommand(strSql, cControls.vSqlConn)

            'Get image data from DB
            cControls.vSqlConn.Open()
            Dim fileData As Byte() = DirectCast(sqlCmd.ExecuteScalar(), Byte())
            cControls.vSqlConn.Close()

            Dim sTempFileName As String = Application.StartupPath & "\" & sFileName

            If Not fileData Is Nothing Then
                'Read image data into a file stream 
                Using fs As New FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Write(fileData, 0, fileData.Length)
                    'Set image variable value using memory stream. 
                    fs.Flush()
                    fs.Close()
                End Using

                'Open File
                ' 10 = SW_SHOWDEFAULT
                ShellEx(Me.Handle, "Open", sFileName, "", "", 10)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function ReadFile(ByVal sPath As String) As Byte()
        'Initialize byte array with a null value initially. 
        Dim data As Byte() = Nothing

        'Use FileInfo object to get file size. 
        Dim fInfo As New FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length

        'Open FileStream to read file 
        Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)

        'Use BinaryReader to read file stream into byte array. 
        Dim br As New BinaryReader(fStream)

        'When you use BinaryReader, you need to supply number of bytes to read from file. 
        'In this case we want to read entire file. So supplying total number of bytes. 
        data = br.ReadBytes(CInt(numBytes))

        Return data
    End Function

    Private Sub sOpenUnSavedFiles(ByVal pFileName As String)

        Dim vProcess As New Process
        vProcess.StartInfo.FileName = pFileName
        vProcess.StartInfo.CreateNoWindow = False
        vProcess.Start()
    End Sub
#End Region
#End Region

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub
End Class