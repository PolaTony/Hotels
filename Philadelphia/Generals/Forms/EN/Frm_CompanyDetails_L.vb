Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports System.IO
Imports System.Net.Mail

Public Class Frm_CompanyDetails_L

    Private Declare Function ShellEx Lib "shell32.dll" Alias "ShellExecuteA" ( _
        ByVal hWnd As Integer, ByVal lpOperation As String, _
        ByVal lpFile As String, ByVal lpParameters As String, _
        ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
#Region " Declaration                                                                       "
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As New cFrmLevelVariables_L
    Dim vSqlStatment(0) As String
    Dim vIsAdmin As String
    Dim vFocus As String
#End Region
#Region " Form Level                                                                        "
#Region " My Form                                                                           "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            vMasterBlock = "NI"
            sQuery(1)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "FRM_Users_Load")
        End Try
        
        'sQueryUser(vUsrCode)
        'vMasterBlock = "NI"
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
    Private Sub FRM_Users_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            vcFrmLevel.vParentFrm = Me.ParentForm
            vcFrmLevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False, "", False)

            If vcFrmLevel.vRecPos > 0 Then
                vcFrmLevel.vParentFrm.sPrintRec(vcFrmLevel.vRecPos)
            Else
                vcFrmLevel.vParentFrm.sPrintRec("")
            End If

            Dim vTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
            vTool = vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Chk_Help")
            sIsHelpEnabled(vTool.Checked)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "FRM_Users_Activated")
        End Try
        
    End Sub
    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If fSaveAll(True) = False Then
            e.Cancel = True
        Else
            e.Cancel = False
            'vcFrmLevel.vParentFrm.sPrintRec("")
        End If
    End Sub
    Private Sub FRM_Users_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Me.ParentForm.MdiChildren.Length = 1 Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, False, "", True)
        End If
    End Sub
    Private Sub Txt_All_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
        Handles Txt_Desc.KeyUp, Txt_address.KeyUp, Txt_DealType.KeyUp, _
        Txt_FirstTimeDeal.KeyUp, Txt_Remarks.KeyUp, Txt_Email1.KeyUp, Txt_Email2.KeyUp, _
        Txt_Fax.KeyUp, Txt_FTel1.KeyUp, Txt_MTel.KeyUp, Txt_TaxCard.KeyUp, Txt_CommercialNumber.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Desc.ValueChanged, Txt_address.ValueChanged, Txt_DealType.ValueChanged, _
    Txt_FirstTimeDeal.ValueChanged, Txt_Remarks.ValueChanged, Txt_Email1.ValueChanged, _
    Txt_Fax.ValueChanged, Txt_FTel1.ValueChanged, Txt_MTel.ValueChanged, Txt_Email2.ValueChanged, _
    Txt_TaxCard.ValueChanged, Txt_CommercialNumber.ValueChanged

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If
    End Sub
#End Region
#Region " DataBase                                                                          "
#Region " Save                                                                              "
    Private Function fIfsaveNeeded() As Boolean
        Try
            If vMasterBlock = "I" Or vMasterBlock = "U" Then
                Return True
            End If

            Dim vRow As UltraGridRow
            If Tab_Details.Tabs("Tab_Companies").Selected = True Then
                For Each vRow In Grd_Companies.Rows
                    If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                        Return True
                    End If
                Next
            ElseIf Tab_Details.Tabs("Tab_Branches").Selected = True Then
                For Each vRow In Grd_Branches.Rows
                    If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                        Return True
                    End If
                Next
            End If

            For Each vRow In Grd_Details.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next

            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "fIfsaveNeeded")
        End Try
        
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        If fIfsaveNeeded() = False Then
            Return True
        End If

        Try
            sEmptySqlStatmentArray()
            If pAskMe Then
                If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                    If fSaveValidation() = True Then
                        sSaveMain()
                    Else
                        Return False
                    End If

                    If fValidateSave_Details() Then
                        If sSaveDetails() Then
                            sQueryDetails()
                        End If
                    End If

                    If Tab_Details.Tabs("Tab_Companies").Selected = True Then
                        If fValidateCompanies() Then
                            sSaveCompanies()
                        Else
                            Return False
                        End If
                    ElseIf Tab_Details.Tabs("Tab_Branches").Selected = True Then
                        If fValidateBranches() Then
                            sSaveBranches()
                        Else
                            Return False
                        End If
                    End If

                Else
                    Return True
                End If
            Else
                If fSaveValidation() = True Then
                    sSaveMain()
                Else
                    Return False
                End If

                If fValidateSave_Details() Then
                    If sSaveDetails() Then
                        sQueryDetails()
                    End If
                End If

                If Tab_Details.Tabs("Tab_Companies").Selected = True Then
                    If fValidateCompanies() Then
                        sSaveCompanies()
                    Else
                        Return False
                    End If
                ElseIf Tab_Details.Tabs("Tab_Branches").Selected = True Then
                    If fValidateBranches() Then
                        sSaveBranches()
                    Else
                        Return False
                    End If
                End If
            End If

            Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
            If vRowCounter > 0 Then
                vMasterBlock = "N"
                If Tab_Details.Tabs("Tab_Companies").Selected = True Then
                    sQueryCompanies()
                ElseIf Tab_Details.Tabs("Tab_Branches").Selected = True Then
                    sQueryBranches()
                End If
                sSetFlagsUpdate()

                vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
                Return True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "fSaveAll")
        End Try
        
    End Function

    Private Sub sSetFlagsUpdate()
        Try
            vMasterBlock = "N"
            sQueryDetails()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "sSetFlagsUpdate")
        End Try
        
        'sQueryAdjustments()

        'vMasterBlock = "NI"
        'DTS_Details.Rows.Clear()
        'sNewRecord()
    End Sub
#End Region
#Region " Query                                                                             "
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pItemCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)
        'If fSaveAll(True) = False Then
        '    Return
        'End If
        Try
            Dim vFetchRec As Integer
            If pItemCode = Nothing Then
                If pIsGoTo = False Then
                    If pRecPos = Nothing Then
                        vFetchRec = 1
                    Else
                        vFetchRec = vcFrmLevel.vRecPos + pRecPos
                        If vFetchRec > cControls.fCount_Rec(" From Employees ") Then
                            vcFrmLevel.vParentFrm.sForwardMessage("33", Me)
                            Return
                        End If
                        If vFetchRec = 0 Then
                            vcFrmLevel.vParentFrm.sForwardMessage("34", Me)
                            vFetchRec = 1
                        End If

                    End If
                End If
            End If

            If pRecPos = -2 Then
                vFetchRec = cControls.fCount_Rec(" From Employees ")
            End If

            Dim vFetchCondition As String
            If pItemCode = Nothing Then
                vFetchCondition = " And RecPos = '" & vFetchRec & "'"
            Else
                vFetchCondition = " And Code = '" & Trim(pItemCode) & "'"
            End If
            Try

                Dim vSQlcommand As New SqlCommand
                vSQlcommand.CommandText = _
                " With MyEmployees as ( Select Code, " & _
                "                          DescA, " & _
                "                          Address, " & _
                "                          DealType, " & _
                "                          FirstTimeDeal, " & _
                "                          Remarks, " & _
                "                          Email1, " & _
                "                          Email2, " & _
                "                          Fax, " & _
                "                          FTel1, " & _
                "                          FTel2, " & _
                "                          MTel, " & _
                "                          TaxCard, " & _
                "                          CommercialNumber, " & _
                "                          Picture, " & _
                "                          ROW_Number() Over (Order By Code) RecPos " & _
                "                          From Company)     " & _
                "                          Select * From MyEmployees  " & _
                "                          Where 1 = 1 " & _
                                          vFetchCondition

                vSQlcommand.Connection = cControls.vSqlConn
                cControls.vSqlConn.Open()
                Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
                Do While vSqlReader.Read

                    'If vSqlReader.IsDBNull(16) = False Then
                    '    vcFrmLevel.vRecPos = Trim(vSqlReader(16))
                    'End If
                    'vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(16))

                    'Name
                    Txt_Desc.Text = Trim(vSqlReader(1))

                    'Address
                    If vSqlReader.IsDBNull(2) = False Then
                        Txt_address.Text = Trim(vSqlReader(2))
                    Else
                        Txt_address.Text = ""
                    End If

                    'DealType
                    If vSqlReader.IsDBNull(3) = False Then
                        Dim vValueList As Infragistics.Win.ValueListItem
                        For Each vValueList In Txt_DealType.Items
                            If vValueList.DataValue = Trim(vSqlReader(3)) Then
                                Txt_DealType.SelectedItem = vValueList
                            End If
                        Next
                    End If

                    'FirstTimeDeal
                    If vSqlReader.IsDBNull(4) = False Then
                        Txt_FirstTimeDeal.Text = Trim(vSqlReader(4))
                    Else
                        Txt_FirstTimeDeal.Text = Nothing
                    End If

                    'Remarks
                    If vSqlReader.IsDBNull(5) = False Then
                        Txt_Remarks.Text = Trim(vSqlReader(5))
                    Else
                        Txt_Remarks.Text = ""
                    End If

                    'Email1
                    If vSqlReader.IsDBNull(6) = False Then
                        Txt_Email1.Text = Trim(vSqlReader(6))
                    Else
                        Txt_Email1.Text = ""
                    End If

                    'Email2
                    If vSqlReader.IsDBNull(7) = False Then
                        Txt_Email2.Text = Trim(vSqlReader(7))
                    Else
                        Txt_Email2.Text = ""
                    End If

                    'Fax
                    If vSqlReader.IsDBNull(8) = False Then
                        Txt_Fax.Text = Trim(vSqlReader(8))
                    Else
                        Txt_Fax.Text = ""
                    End If

                    'FTel1
                    If vSqlReader.IsDBNull(9) = False Then
                        Txt_FTel1.Text = Trim(vSqlReader(9))
                    Else
                        Txt_FTel1.Text = ""
                    End If

                    'FTel2
                    If vSqlReader.IsDBNull(10) = False Then
                        Txt_FTel2.Text = Trim(vSqlReader(10))
                    Else
                        Txt_FTel2.Text = ""
                    End If

                    'MTel
                    If vSqlReader.IsDBNull(11) = False Then
                        Txt_MTel.Text = Trim(vSqlReader(11))
                    Else
                        Txt_MTel.Text = ""
                    End If

                    'TaxCard
                    If vSqlReader.IsDBNull(12) = False Then
                        Txt_TaxCard.Text = Trim(vSqlReader(12))
                    Else
                        Txt_TaxCard.Text = ""
                    End If

                    'CommercialNumber
                    If vSqlReader.IsDBNull(13) = False Then
                        Txt_CommercialNumber.Text = Trim(vSqlReader(13))
                    Else
                        Txt_CommercialNumber.Text = ""
                    End If

                    'Picture
                    If vSqlReader.IsDBNull(14) = False Then
                        Dim arrayImage() As Byte = CType(vSqlReader(14), Byte())
                        Dim ms As New IO.MemoryStream(arrayImage)
                        UltraPictureBox2.Image = Image.FromStream(ms)
                    End If

                Loop

                cControls.vSqlConn.Close()
                vSqlReader.Close()

                'sQueryCompanies()
                sQueryDetails()

            Catch ex As Exception
                cControls.vSqlConn.Close()
                MessageBox.Show(ex.Message)
                'cException.sHandleException(ex.Message, Me.Name, "sQuery")
            End Try
            vMasterBlock = "N"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Query")
        End Try
        
    End Sub
    Private Sub sLoadCompanies()
        Try
            Txt_Company.Items.Clear()
            Dim vsqlCommand As New SqlClient.SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA From Companies Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

            Do While vSqlReader.Read
                Txt_Company.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "sLoadCompanies")
        End Try
        
    End Sub
#End Region
#Region " Delete                                                                            "
    Public Sub sDelete()
        Dim vSqlString As String

        sEmptySqlStatmentArray()
        If vMasterBlock = "I" Then
            Dim vRow As UltraGridRow

            If Grd_Details.Focused Then
                'If Not Grd_Details.ActiveRow Is Nothing Then
                Grd_Details.ActiveRow.Delete(False)
                Exit Sub
                'End If
            End If
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then

                If Grd_Details.Focused Then
                    If Not Grd_Details.ActiveRow Is Nothing Then
                        If Grd_Details.ActiveRow.Cells("DML").Value = "I" Or Grd_Details.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_Details.ActiveRow.Delete(False)

                            Exit Sub
                        ElseIf Grd_Details.ActiveRow.Cells("DML").Value = "N" Or Grd_Details.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlString = _
                            " Delete From Company_Attachments " & _
                            " Where  Ser  = '" & Grd_Details.ActiveRow.Cells("Ser").Value & "'"
                        End If
                    End If
                End If

                If cControls.fSendData(vSqlstring, Me.Name) > 0 Then
                    Dim vRow As UltraGridRow

                    If Grd_Details.Focused Then
                        Grd_Details.ActiveRow.Delete(False)

                    End If
                    vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                End If
            End If
        End If

    End Sub
#End Region
#End Region
#Region " New Record                                                                        "
    Public Sub sNewRecord()
        Tab_Main.Tabs("Tab_Details").Selected = True
        Txt_Desc.Clear()
        Txt_address.Clear()
        Txt_DealType.Clear()
        Txt_FirstTimeDeal.Value = Nothing
        Txt_Remarks.Clear()
        Txt_Email1.Clear()
        Txt_Email2.Clear()
        Txt_Fax.Clear()
        Txt_FTel1.Clear()
        Txt_FTel2.Clear()
        Txt_MTel.Clear()
        Txt_TaxCard.Clear()
        Txt_CommercialNumber.Clear()
        vMasterBlock = "NI"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")
    End Sub
#End Region
#Region " sOpenLov                                                                          "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String)
        vLovReturn1 = ""
        VLovReturn2 = ""
        'Dim Frm_Lov As New FRM_LovTreeL(pSqlStatment, pTitle)
        'Frm_Lov.ShowDialog()
        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            sQuery(pItemCode:=vLovReturn1)
        End If
    End Sub
#End Region
#Region " Tab Mangment                                                                      "
    Private Sub Tab_Main_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Main.SelectedTabChanging
        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            If fSaveAll(True) = False Then
                e.Cancel = True
            Else
                e.Cancel = False
                sNewRecord()
            End If
        End If
    End Sub
#End Region
#Region " Help                                                                              "
    Public Sub sIsHelpEnabled(ByVal pEnabled As Boolean)

        'UltraToolTipManager1.Enabled = pEnabled

    End Sub
#End Region
#End Region

#Region " Master                                                                            "
#Region " DataBase                                                                          "
#Region " Save                                                                              "
    Private Function fSaveValidation() As Boolean
        If Txt_Desc.Text.Length = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("13", Me)
            Txt_Desc.Select()
            Return False
        End If
        Return True
    End Function
    Private Sub sSaveMain()
        'If fSaveValidation() = False Then
        '    Return
        'End If
        Dim vFirstTimeDeal, vFirstBalance, vDeduction As String
        Dim vSqlCommand As String = ""
        If Not Txt_FirstTimeDeal.Value = Nothing Then
            vFirstTimeDeal = "'" & Format(Txt_FirstTimeDeal.Value, "MM-dd-yyyy") & "'"
        Else
            vFirstTimeDeal = "NULL"
        End If

        If vMasterBlock = "I" Then
            vSqlCommand = " Insert Into Company     ( Code,           DescA,                      Address,                                 DealType,                  FirstTimeDeal,                    Remarks,                          Email1,                          Email2,                          Fax,                          FTel1,                          FTel2,                          MTel,                            TaxNumber,                       CommercialNumber   ) " & _
                                  " Values            ('001', '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_address.Text) & "', '" & Trim(Txt_DealType.Value) & "', " & vFirstTimeDeal & " , '" & Trim(Txt_Remarks.Text) & "', '" & Trim(Txt_Email1.Text) & "', '" & Trim(Txt_Email2.Text) & "', '" & Trim(Txt_Fax.Text) & "', '" & Trim(Txt_FTel1.Text) & "', '" & Trim(Txt_FTel2.Text) & "', '" & Trim(Txt_MTel.Text) & "', '" & Trim(Txt_TaxCard.Text) & "', '" & Trim(Txt_CommercialNumber.Text) & "')"
            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Company " & _
                          " Set   DescA                 = '" & Trim(Txt_Desc.Text) & "'," & _
                          "       Address               = '" & Trim(Txt_address.Text) & "'," & _
                          "       DealType              = '" & Txt_DealType.Value & "',    " & _
                          "       FirstTimeDeal         =  " & vFirstTimeDeal & ", " & _
                          "       Remarks               = '" & Trim(Txt_Remarks.Text) & "', " & _
                          "       Email1                = '" & Trim(Txt_Email1.Text) & "', " & _
                          "       Email2                = '" & Trim(Txt_Email2.Text) & "', " & _
                          "       Fax                   = '" & Trim(Txt_Fax.Text) & "', " & _
                          "       FTel1                  = '" & Trim(Txt_FTel1.Text) & "', " & _
                          "       FTel2                  = '" & Trim(Txt_FTel2.Text) & "', " & _
                          "       MTel                  = '" & Trim(Txt_MTel.Text) & "', " & _
                          "       TaxCard               = '" & Trim(Txt_TaxCard.Text) & "', " & _
                          "       CommercialNumber      = '" & Trim(Txt_CommercialNumber.Text) & "'"

            sFillSqlStatmentArray(vSqlCommand)
        End If
    End Sub
#End Region
#End Region
#End Region

#Region " Details                                                                           "
#Region " Companies                                                                         "
#Region " DataBase                                                                          "
#Region " Save                                                                              "
    Private Function fValidateCompanies() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Companies.Rows
            If IsDBNull(vRow.Cells("DescA").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveCompanies()
        'If fValidateCompanies() = False Then
        '    Return
        'End If
        Dim vRow As UltraGridRow
        Grd_Companies.UpdateData()
        'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
        Dim vCounter As Integer = 0
        For Each vRow In Grd_Companies.Rows
            Dim vDate, vSqlString, vGetSerial As String

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Companies "

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Companies        (       Code,                             DescA,                                  Address,                                   Remarks)" & _
                             "                    Values    (" & vGetSerial & ",'" & Trim(vRow.Cells("DescA").Text) & "', '" & Trim(vRow.Cells("Address").Text) & "', '" & Trim(vRow.Cells("Remarks").Text) & "')"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Companies " & _
                              " Set     DescA         = '" & Trim(vRow.Cells("DescA").Text) & "'," & _
                              "         Address       = '" & Trim(vRow.Cells("Address").Text) & "'," & _
                              "         Remarks       = '" & Trim(vRow.Cells("Remarks").Text) & "' " & _
                              " Where   Code          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryCompanies()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Code,                   " & _
            "        DescA,                 " & _
            "        Address,               " & _
            "        Remarks                " & _
            " From   Companies               " & _
            " Order  By Code                 "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Companies.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Companies.Rows.SetCount(vRowCounter + 1)
                DTS_Companies.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                DTS_Companies.Rows(vRowCounter)("DescA") = Trim(vSqlReader(1))
                DTS_Companies.Rows(vRowCounter)("Address") = Trim(vSqlReader(2))
                DTS_Companies.Rows(vRowCounter)("Remarks") = Trim(vSqlReader(3))
                DTS_Companies.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Companies.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQueryDetails")
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub Grd_Companies_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Companies.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If

        Grd_Companies.UpdateData()
    End Sub
    Private Sub Grd_Companies_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Companies.Enter
        vFocus = "Details"
    End Sub
#End Region
#End Region
#Region " Branches                                                                          "
#Region " DataBase                                                                          "
#Region " Save                                                                              "
    Private Function fValidateBranches() As Boolean
        If Txt_Company.SelectedIndex = -1 Then
            vcFrmLevel.vParentFrm.sForwardMessage("97", Me)
            Txt_Company.Select()
            Return False
        End If

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Branches.Rows
            If IsDBNull(vRow.Cells("DescA").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveBranches()
        'If fValidateDetails() = False Then
        '    Return
        'End If
        Dim vRow As UltraGridRow
        Grd_Companies.UpdateData()
        'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
        Dim vCounter As Integer = 0
        For Each vRow In Grd_Branches.Rows
            Dim vDate, vSqlString, vGetSerial As String

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Branches Where Company_Code = " & Txt_Company.Value

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Branches  (       Code,                             DescA,                                  Address,                                   Remarks,                          Company_Code)" & _
                             "             Values    (" & vGetSerial & ",'" & Trim(vRow.Cells("DescA").Text) & "', '" & Trim(vRow.Cells("Address").Text) & "', '" & Trim(vRow.Cells("Remarks").Text) & "', " & Txt_Company.Value & ")"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Branches " & _
                              " Set     DescA         = '" & Trim(vRow.Cells("DescA").Text) & "'," & _
                              "         Address       = '" & Trim(vRow.Cells("Address").Text) & "'," & _
                              "         Remarks       = '" & Trim(vRow.Cells("Remarks").Text) & "' " & _
                              " Where   Code          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryBranches()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Code,                   " & _
            "        DescA,                 " & _
            "        Address,               " & _
            "        Remarks                " & _
            " From   Branches               " & _
            " Where  Company_Code = " & Txt_Company.Value & _
            " Order  By Code                 "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Branches.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Branches.Rows.SetCount(vRowCounter + 1)
                DTS_Branches.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                DTS_Branches.Rows(vRowCounter)("DescA") = Trim(vSqlReader(1))
                DTS_Branches.Rows(vRowCounter)("Address") = Trim(vSqlReader(2))
                DTS_Branches.Rows(vRowCounter)("Remarks") = Trim(vSqlReader(3))
                DTS_Branches.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Companies.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQueryDetails")
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub Grd_Branches_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Branches.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If

        Grd_Branches.UpdateData()
    End Sub
    Private Sub Grd_Details_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Branches.Enter
        vFocus = "Details"
    End Sub
#End Region
#End Region
#Region " Attachments                                                                       "
#Region " DataBase                                                                          "
#Region " Query                                                                             "
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
            "        FileType,               " & _
            "        TDate                  " & _
            " From   Company_Attachments         "


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

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Details.Rows(vRowCounter)("Date") = Trim(vSqlReader(6))
                Else
                    DTS_Details.Rows(vRowCounter)("Date") = Nothing
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
#Region " Save                                                                              "

    Private Function fValidateSave_Details() As Boolean
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

        Grd_Details.UpdateData()

        Try
            For Each vRow As UltraGridRow In Grd_Details.Rows
                'Call Upload Images Or File

                Dim vDate As String
                Dim Extension As String = System.IO.Path.GetExtension(vRow.Cells("FileName").Text)
                Dim vFileType As String

                Dim imageData As Byte()
                Dim sFileName As String

                Dim FileData() As Byte

                If vRow.Cells("CompleteFileName").Text <> "" Then
                    FileData = ReadFileData(vRow.Cells("CompleteFileName").Text)
                End If

                If IsDBNull(vRow.Cells("Date").Value) Then
                    vDate = "NULL"
                Else
                    vDate = "'" & Format(vRow.Cells("Date").Value, "MM-dd-yyyy") & "'"
                End If

                If vRow.Cells("DML").Value = "I" Then

                    vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Company_Attachments "


                    vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                    vSqlString = " Insert Into Company_Attachments (        Ser,           FileData,                   DescA,                             FileName,                   FileType,                            CompleteFileName,                  TDate ) " & _
                                 "                      Values     (" & vGetSerial & ", (@FileData), '" & vRow.Cells("DescL").Text & "', '" & vRow.Cells("FileName").Text & "', '" & vFileType & "', '" & Trim(vRow.Cells("CompleteFileName").Text) & "', " & vDate & ")"

                    Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)
                    vMyCommand.Parameters.AddWithValue("@FileData ", FileData)

                    cControls.vSqlConn.Open()
                    vMyCommand.ExecuteNonQuery()
                    cControls.vSqlConn.Close()

                    vcFrmLevel.vParentFrm.sForwardMessage("7", Me)

                    vCounter += 1
                ElseIf vRow.Cells("DML").Value = "U" Then
                    Dim ms As New System.IO.MemoryStream
                    Dim arrPicture() As Byte

                    vSqlString = " Update Company_Attachments " & _
                                 " Set    DescA = '" & vRow.Cells("DescL").Text & "',  " & _
                                 "        FileData = (@FileData),                     " & _
                                 "        FileName = '" & vRow.Cells("FileName").Text & "', " & _
                                 "        FileType = '" & vFileType & "', " & _
                                 "        TDate    =  " & vDate & _
                                 " Where  Ser      =  " & vRow.Cells("Ser").Text

                    Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)
                    vMyCommand.Parameters.AddWithValue("@FileData ", FileData)

                    cControls.vSqlConn.Open()
                    vMyCommand.ExecuteNonQuery()
                    cControls.vSqlConn.Close()

                    vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
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
#Region " Grd                                                                               "
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

                        cmd.CommandText = " Select FileData FROM Company_Attachments " & _
                                          " Where   Ser   = " & Grd_Details.ActiveRow.Cells("Ser").Value

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
            ElseIf Grd_Details.ActiveRow.Cells("SendMail").Activated Then
                Dim vCompleteFileName As String = Trim(Grd_Details.ActiveRow.Cells("CompleteFileName").Text)
                Dim vFile_Name As String = Trim(Grd_Details.ActiveRow.Cells("FileName").Text)

                fSaveAll(False)

                If vCompleteFileName <> "" Then
                    Dim vSendMail As New Frm_NewMail(vCompleteFileName, vFile_Name)
                    vSendMail.ShowDialog()
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
            strSql = " Select FileData from Customers_Deals_Details_Attachments " & _
                     " Where   Ser = " & Grd_Details.ActiveRow.Cells("Ser").Value

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
#End Region
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            OpenFileDialog1.Filter = "JPEGS|*.jpg|GIFS|*.gif|Bitmaps|*.bmp|all files|*.*"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.ShowDialog()
            If Not OpenFileDialog1.FileName = "" Then
                cControls.sSaveImage(Image.FromFile(OpenFileDialog1.FileName), "Company")
                'UltraPictureBox2.SizeMode = PictureBoxSizeMode.CenterImage
                UltraPictureBox2.Image = Image.FromFile(OpenFileDialog1.FileName)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Details.SelectedTabChanged
        If Tab_Details.Tabs("Tab_Companies").Selected Then
            sQueryCompanies()
        ElseIf Tab_Details.Tabs("Tab_Branches").Selected = True Then
            DTS_Branches.Rows.Clear()
            sLoadCompanies()
        End If
    End Sub
    Private Sub Tab_Details_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Details.SelectedTabChanging
        If fSaveAll(True) = False Then
            e.Cancel = True
        Else
            e.Cancel = False
        End If
    End Sub
    Private Sub Txt_Company_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Company.ValueChanged
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Code, DescA, Address, Remarks From Branches " & _
            " Where Company_Code = " & Txt_Company.Value & _
            " Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Branches.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Branches.Rows.SetCount(vRowCounter + 1)
                'Code
                If vSqlReader.IsDBNull(0) = False Then
                    DTS_Branches.Rows(vRowCounter)("Ser") = vSqlReader(0)
                End If

                'DescA
                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Branches.Rows(vRowCounter)("DescA") = vSqlReader(1)
                End If

                'Address
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Branches.Rows(vRowCounter)("Address") = vSqlReader(2)
                Else
                    DTS_Branches.Rows(vRowCounter)("Address") = ""
                End If

                'Remarks
                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Branches.Rows(vRowCounter)("Remarks") = vSqlReader(3)
                Else
                    DTS_Branches.Rows(vRowCounter)("Remarks") = ""
                End If

                DTS_Branches.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Btn_ConfigureMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ConfigureMail.Click
        Dim vConfigureMail As New Frm_ConfigureMail
        vConfigureMail.ShowDialog()
    End Sub
End Class