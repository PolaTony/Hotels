Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_CompanyDetailsA_A
#Region " Declaration                                                                       "
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSqlStatment(0) As String
    Dim vIsAdmin As String
    Dim vFocus As String
#End Region
#Region " Form Level                                                                        "
#Region " My Form                                                                           "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sQuery(1)
        'sQueryUser(vUsrCode)
        'vMasterBlock = "NI"

        sLoadStores()
        sLoadBoxes()
        sLoadCostCenters()
        sQueryBranches()
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
    End Sub
    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        'If fSaveAll(True) = False Then
        '    e.Cancel = True
        'Else
        '    e.Cancel = False
        '    vcFrmLevel.vParentFrm.sPrintRec("")
        'End If
    End Sub
    Private Sub FRM_Users_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Me.ParentForm.MdiChildren.Length = 1 Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, False, "", True)
        End If
    End Sub
    Private Sub Txt_All_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
        Handles Txt_Desc.KeyUp, Txt_address.KeyUp, Txt_DealType.KeyUp,
        Txt_FirstTimeDeal.KeyUp, Txt_Remarks.KeyUp, Txt_Email1.KeyUp, Txt_ActivityCode.KeyUp,
        Txt_IssuerId.KeyUp, Txt_FTel1.KeyUp, Txt_MTel.KeyUp,
        Txt_TaxCard.KeyUp, Txt_CommercialNumber.KeyUp

        If e.KeyData = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Desc.ValueChanged, Txt_address.ValueChanged, Txt_DealType.ValueChanged,
    Txt_FirstTimeDeal.ValueChanged, Txt_Remarks.ValueChanged, Txt_Email1.ValueChanged,
    Txt_IssuerId.ValueChanged, Txt_FTel1.ValueChanged, Txt_MTel.ValueChanged, Txt_ActivityCode.ValueChanged,
    Txt_TaxCard.ValueChanged, Txt_CommercialNumber.ValueChanged, Txt_ClientId.ValueChanged, Txt_SecretId.ValueChanged

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

        Return False
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        If fIfsaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        If pAskMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                If fSaveValidation() = True Then
                    sSaveMain()
                Else
                    Return False
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
                'sQueryCompanies()
            ElseIf Tab_Details.Tabs("Tab_Branches").Selected = True Then
                sQueryBranches()
            End If

            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            Return True
        End If
    End Function
#End Region
#Region " Query                                                                             "
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pItemCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)
        'If fSaveAll(True) = False Then
        '    Return
        'End If

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
            vSQlcommand.CommandText =
            " With MyEmployees as ( Select Code, " &
            "                          DescA, " &
            "                          Address, " &
            "                          DealType, " &
            "                          FirstTimeDeal, " &
            "                          Remarks, " &
            "                          Email1, " &
            "                          Activity_Code, " &
            "                          Issuer_Id, " &
            "                          FTel1, " &
            "                          FTel2, " &
            "                          MTel, " &
            "                          TaxCard, " &
            "                          CommercialNumber, " &
            "                          Client_Id,  " &
            "                          Secret_Id,  " &
            "                          Picture, " &
            "                          ROW_Number() Over (Order By Code) RecPos " &
            "                          From Companies       " &
            "                          Where Code = " & vCompanyCode & " )" &
            "                                                       " &
            "                          Select * From MyEmployees    " &
            "                          Where 1 = 1 " &
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

                'Activity Code
                If vSqlReader.IsDBNull(7) = False Then
                    Txt_ActivityCode.Text = Trim(vSqlReader(7))
                Else
                    Txt_ActivityCode.Text = ""
                End If

                'Issuer Id
                If vSqlReader.IsDBNull(8) = False Then
                    Txt_IssuerId.Text = Trim(vSqlReader(8))
                Else
                    Txt_IssuerId.Text = ""
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

                'Client_Id
                If vSqlReader.IsDBNull(14) = False Then
                    Txt_ClientId.Text = Trim(vSqlReader(14))
                Else
                    Txt_ClientId.Text = ""
                End If

                'Secret_Id
                If vSqlReader.IsDBNull(15) = False Then
                    Txt_SecretId.Text = Trim(vSqlReader(15))
                Else
                    Txt_SecretId.Text = ""
                End If

                'Picture
                If vSqlReader.IsDBNull(16) = False Then
                    Dim arrayImage() As Byte = CType(vSqlReader(16), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)
                    UltraPictureBox2.Image = Image.FromStream(ms)
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            'sQueryCompanies()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
    End Sub
    Private Sub sLoadCompanies()
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
    End Sub
    Private Sub sLoadStores()
        Dim vsqlCommand As New SqlClient.SqlCommand
        vsqlCommand.CommandText = " Select Code, DescA From Stores "

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

        Txt_Stores.Items.Clear
        Do While vSqlReader.Read
            Txt_Stores.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub
    Private Sub sLoadBoxes()
        Dim vsqlCommand As New SqlClient.SqlCommand
        vsqlCommand.CommandText = " Select Code, DescA From Boxes "

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

        Txt_Boxes.Items.Clear()

        Do While vSqlReader.Read
            Txt_Boxes.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub
    Private Sub sLoadCostCenters()
        Dim vsqlCommand As New SqlClient.SqlCommand
        vsqlCommand.CommandText = " Select Code, DescA From Cost_Center "

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

        Txt_CostCenters.Items.Clear()

        Do While vSqlReader.Read
            Txt_CostCenters.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub
#End Region
#Region " Delete                                                                            "
    Public Sub sDelete()
        Dim vSqlString As String

        sEmptySqlStatmentArray()
        If Tab_Details.Tabs("Tab_Companies").Selected Then
            If Not Grd_Companies.ActiveRow Is Nothing Then
                If Grd_Companies.ActiveRow.Cells("DML").Value = "I" Then
                    Grd_Companies.ActiveRow.Delete(False)
                ElseIf Grd_Companies.ActiveRow.Cells("DML").Value = "N" Or Grd_Companies.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString =
                        " Delete From Branches " &
                        " Where  Company_Code   = '" & Grd_Companies.ActiveRow.Cells("Ser").Value & "'"

                        sFillSqlStatmentArray(vSqlString)

                        vSqlString =
                        " Delete From Companies " &
                        " Where Code = " & Grd_Companies.ActiveRow.Cells("Ser").Value

                        sFillSqlStatmentArray(vSqlString)

                        If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then
                            vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_Companies.ActiveRow.Delete(False)
                        End If
                    End If
                End If
            End If
        ElseIf Tab_Details.Tabs("Tab_Branches").Selected Then
            If Not Grd_Branches.ActiveRow Is Nothing Then
                If Grd_Branches.ActiveRow.Cells("DML").Value = "I" Then
                    Grd_Branches.ActiveRow.Delete(False)
                ElseIf Grd_Branches.ActiveRow.Cells("DML").Value = "N" Or Grd_Branches.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString =
                        " Delete From Branches " &
                        " Where  Code       = '" & Grd_Branches.ActiveRow.Cells("Ser").Value & "'" &
                        " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_Branches.ActiveRow.Delete(False)
                        End If
                    End If
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
        Txt_ActivityCode.Clear()
        Txt_IssuerId.Clear()
        Txt_FTel1.Clear()
        Txt_FTel2.Clear()
        Txt_MTel.Clear()
        Txt_TaxCard.Clear()

        Txt_ClientId.Clear()
        Txt_SecretId.Clear()

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

#End Region
#Region " Help                                                                              "
    Public Sub sIsHelpEnabled(ByVal pEnabled As Boolean)

        UltraToolTipManager1.Enabled = pEnabled

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
        If fSaveValidation() = False Then
            Return
        End If

        Dim vFirstTimeDeal As String
        Dim vSqlCommand As String = ""
        If Not Txt_FirstTimeDeal.Value = Nothing Then
            vFirstTimeDeal = "'" & Format(Txt_FirstTimeDeal.Value, "MM-dd-yyyy") & "'"
        Else
            vFirstTimeDeal = "NULL"
        End If

        If vMasterBlock = "I" Then
            vSqlCommand = " Insert Into Companies     ( Code,           DescA,                             Address,                          DealType,                  FirstTimeDeal,                    Remarks,                          Email1,                          Activity_Code,                                Issuer_Id,                  FTel1,                          FTel2,                          MTel,                            TaxNumber,                       CommercialNumber,                         Client_Id,                         Secret_Id   ) " &
                                  " Values            ('001', '" & Trim(Txt_Desc.Text) & "', '" & Trim(Txt_address.Text) & "', '" & Trim(Txt_DealType.Value) & "', " & vFirstTimeDeal & " , '" & Trim(Txt_Remarks.Text) & "', '" & Trim(Txt_Email1.Text) & "', '" & Trim(Txt_ActivityCode.Text) & "', '" & Trim(Txt_IssuerId.Text) & "', '" & Trim(Txt_FTel1.Text) & "', '" & Trim(Txt_FTel2.Text) & "', '" & Trim(Txt_MTel.Text) & "', '" & Trim(Txt_TaxCard.Text) & "', '" & Trim(Txt_CommercialNumber.Text) & "', '" & Trim(Txt_ClientId.Text) & "', '" & Trim(Txt_SecretId.Text) & "') "
            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Companies " &
                          " Set   DescA                 = '" & Trim(Txt_Desc.Text) & "'," &
                          "       Address               = '" & Trim(Txt_address.Text) & "'," &
                          "       DealType              = '" & Txt_DealType.Value & "',    " &
                          "       FirstTimeDeal         =  " & vFirstTimeDeal & ", " &
                          "       Remarks               = '" & Trim(Txt_Remarks.Text) & "', " &
                          "       Email1                = '" & Trim(Txt_Email1.Text) & "', " &
                          "       Activity_Code         = '" & Trim(Txt_ActivityCode.Text) & "', " &
                          "       Issuer_Id             = '" & Trim(Txt_IssuerId.Text) & "', " &
                          "       FTel1                 = '" & Trim(Txt_FTel1.Text) & "', " &
                          "       FTel2                 = '" & Trim(Txt_FTel2.Text) & "', " &
                          "       MTel                  = '" & Trim(Txt_MTel.Text) & "', " &
                          "       TaxCard               = '" & Trim(Txt_TaxCard.Text) & "', " &
                          "       CommercialNumber      = '" & Trim(Txt_CommercialNumber.Text) & "', " &
                          "       Client_Id             = '" & Trim(Txt_ClientId.Text) & "', " &
                          "       Secret_Id             = '" & Trim(Txt_SecretId.Text) & "' " &
                          "                                                             " &
                          " Where Code = " & vCompanyCode

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
        'If Txt_Company.SelectedIndex = -1 Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("97", Me)
        '    Txt_Company.Select()
        '    Return False
        'End If

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Branches.Rows
            If IsDBNull(vRow.Cells("DescA").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("Store").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("50", Me)
                vRow.Cells("Store").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("Box").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("64", Me)
                vRow.Cells("Box").Selected = True
                Return False
            End If

            'If IsDBNull(vRow.Cells("CostCenter").Value) Then
            '    vcFrmLevel.vParentFrm.sForwardMessage("29", Me)
            '    vRow.Cells("CostCenter").Selected = True
            '    Return False
            'End If
        Next
        Return True
    End Function
    Private Sub sSaveBranches()

        Dim vRow As UltraGridRow
        Grd_Companies.UpdateData()
        'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
        Dim vCounter As Integer = 0
        For Each vRow In Grd_Branches.Rows
            Dim vSqlString, vGetSerial, vStore, vBox, vCostCenter As String

            If Not IsDBNull(vRow.Cells("Store").Value) Then
                vStore = " '" & vRow.Cells("Store").Value & "' "
            Else
                vStore = "NULL"
            End If

            If Not IsDBNull(vRow.Cells("Box").Value) Then
                vBox = " '" & vRow.Cells("Box").Value & "' "
            Else
                vBox = "NULL"
            End If

            If Not IsDBNull(vRow.Cells("CostCenter").Value) Then
                vCostCenter = vRow.Cells("CostCenter").Value
            Else
                vCostCenter = "NULL"
            End If

            If vRow.Cells("DML").Value = "I" Then

                'Here I will Check if the version is POS I will prevent enter more than one Branch
                If cControls.fReturnValue(" Select VersionType From Controls ", Me.Name) = "POS" Then
                    If cControls.fIsExist(" From Branches ", Me.Name) Then
                        vcFrmLevel.vParentFrm.sForwardMessage("155", Me)
                        Exit Sub
                    End If
                End If

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Branches Where Company_Code = " & vCompanyCode

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Branches  (       Code,                              DescA,                                    DescL,                                    Address,                                    Tel,                                    Remarks,                 Store_Code,     Box_Code,        CostCenter_Code,      Company_Code )" &
                             "             Values    (" & vGetSerial & ", '" & Trim(vRow.Cells("DescA").Text) & "', '" & Trim(vRow.Cells("DescL").Text) & "', '" & Trim(vRow.Cells("Address").Text) & "', '" & Trim(vRow.Cells("Tel").Text) & "', '" & Trim(vRow.Cells("Remarks").Text) & "', " & vStore & ", " & vBox & ", " & vCostCenter & ", " & vCompanyCode & ")"

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Branches " &
                              " Set     DescA         = '" & Trim(vRow.Cells("DescA").Text) & "', " &
                              "         DescL         = '" & Trim(vRow.Cells("DescL").Text) & "', " &
                              "         Address       = '" & Trim(vRow.Cells("Address").Text) & "', " &
                              "         Tel           = '" & Trim(vRow.Cells("Tel").Text) & "', " &
                              "         Store_Code    =  " & vStore & ", " &
                              "         Box_Code      =  " & vBox & ", " &
                              "         CostCenter_Code = " & vCostCenter & ", " &
                              "         Remarks       = '" & Trim(vRow.Cells("Remarks").Text) & "' " &
                              " Where   Code          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                             "
    Private Sub sQueryBranches()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText =
            " Select Code,                  " &
            "        DescA,                 " &
            "        DescL,                 " &
            "        Address,               " &
            "        Tel,                   " &
            "        Remarks,               " &
            "        Store_Code,            " &
            "        Box_Code,              " &
            "        CostCenter_Code        " &
            "                               " &
            " From   Branches               " &
            " Where  Company_Code = " & vCompanyCode &
            " Order  By Code                 "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Branches.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Branches.Rows.SetCount(vRowCounter + 1)
                DTS_Branches.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))

                If vSqlReader.IsDBNull(1) = False Then
                    DTS_Branches.Rows(vRowCounter)("DescA") = Trim(vSqlReader(1))
                Else
                    DTS_Branches.Rows(vRowCounter)("DescA") = ""
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Branches.Rows(vRowCounter)("DescL") = Trim(vSqlReader(2))
                Else
                    DTS_Branches.Rows(vRowCounter)("DescL") = ""
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Branches.Rows(vRowCounter)("Address") = Trim(vSqlReader(3))
                Else
                    DTS_Branches.Rows(vRowCounter)("Address") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Branches.Rows(vRowCounter)("Tel") = Trim(vSqlReader(4))
                Else
                    DTS_Branches.Rows(vRowCounter)("Tel") = ""
                End If

                If vSqlReader.IsDBNull(5) = False Then
                    DTS_Branches.Rows(vRowCounter)("Remarks") = Trim(vSqlReader(5))
                Else
                    DTS_Branches.Rows(vRowCounter)("Remarks") = ""
                End If

                If vSqlReader.IsDBNull(6) = False Then
                    DTS_Branches.Rows(vRowCounter)("Store") = Trim(vSqlReader(6))
                Else
                    DTS_Branches.Rows(vRowCounter)("Store") = Nothing
                End If

                If vSqlReader.IsDBNull(7) = False Then
                    DTS_Branches.Rows(vRowCounter)("Box") = Trim(vSqlReader(7))
                Else
                    DTS_Branches.Rows(vRowCounter)("Box") = Nothing
                End If

                If vSqlReader.IsDBNull(8) = False Then
                    DTS_Branches.Rows(vRowCounter)("CostCenter") = vSqlReader(8)
                Else
                    DTS_Branches.Rows(vRowCounter)("CostCenter") = Nothing
                End If

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
            'sQueryCompanies()
        ElseIf Tab_Details.Tabs("Tab_Branches").Selected = True Then
            DTS_Branches.Rows.Clear()
            'sLoadCompanies()
        End If
    End Sub
    Private Sub Tab_Details_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Details.SelectedTabChanging
        'If fSaveAll(True) = False Then
        '    e.Cancel = True
        'Else
        '    e.Cancel = False
        'End If
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

End Class