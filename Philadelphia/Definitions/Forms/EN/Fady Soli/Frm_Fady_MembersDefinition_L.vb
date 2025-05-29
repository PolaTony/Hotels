Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinEditors
Imports Infragistics.Win.AppStyling

Public Class Frm_Fady_MembersDefinition_L
#Region " Declaration                                                                    "
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As New cFrmLevelVariables_L
    Dim vSqlStatment(0) As String
    Dim vFocus As String = "Master"
    Dim vSortedList As New SortedList
    Dim vClear As Boolean = True
#End Region
#Region " Form Level                                                                     "
#Region " My Form                                                                        "
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TXT_SummaryDate.Value = cControls.fReturnValue(" Select GetDate() ", Me.Name)
        Txt_EmpCode.Text = vUsrCode
        Txt_EmpDesc.Text = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & vUsrCode & "'", Me.Name)
        sNewCode()
        'sQueryUser(vUsrCode)
        vMasterBlock = "NI"
        sQuerySummaryMain()

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

        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "«· ›«’Ì·")
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "»ÕÀ")
        End If
        sLoadCountries()

        If vcFrmLevel.vRecPos > 0 Then
            vcFrmLevel.vParentFrm.sPrintRec(vcFrmLevel.vRecPos)
        Else
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If
    End Sub
    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If fSaveAll(True) = False Then
            e.Cancel = True
        Else
            e.Cancel = False
            vcFrmLevel.vParentFrm.sPrintRec("")
        End If
    End Sub
    Private Sub FRM_Users_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Me.ParentForm.MdiChildren.Length = 1 Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, False, "", True)
        End If
    End Sub
    Private Sub Txt_All_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_Name.ValueChanged, Txt_Remarks.ValueChanged, Txt_EmpDesc.ValueChanged, _
             Txt_TDate.ValueChanged, Txt_BirthDate.ValueChanged, _
             Txt_Job.ValueChanged, Txt_WifeName.ValueChanged, Txt_WifeBirthDate.ValueChanged, _
             Txt_ID.ValueChanged, Txt_Countries.ValueChanged, _
             Txt_Tel.ValueChanged, Txt_Location.ValueChanged

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If
    End Sub

    Private Sub sLoadCountries()
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = _
            " Select Code, DescA From Countries " & _
            " Order By Code "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Countries.Items.Clear()

            Do While vSqlReader.Read
                Txt_Countries.Items.Add(vSqlReader(0), vSqlReader(1))
            Loop
            cControls.vSqlConn.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#Region " Tab Management                                                                 "
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
    Private Sub Tab_Main_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm = Me.ParentForm
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "«· ›«’Ì·")
            sQuerySummaryMain()
            ToolBar_Main.Tools("Submit").SharedProps.Visible = False
            ToolBar_Main.Tools("FilterByDate").SharedProps.Visible = True
            ToolBar_Main.Tools("FilterByProcessed").SharedProps.Visible = True
            ToolBar_Main.Tools("PurchasePrices").SharedProps.Visible = False
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "»ÕÀ")
            If Grd_Summary.Selected.Rows.Count > 0 Then
                If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                    sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
                Else
                    sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
                End If
            Else
                sNewRecord()
            End If
            ToolBar_Main.Tools("Submit").SharedProps.Visible = True
            ToolBar_Main.Tools("FilterByDate").SharedProps.Visible = False
            ToolBar_Main.Tools("FilterByProcessed").SharedProps.Visible = False
            ToolBar_Main.Tools("PurchasePrices").SharedProps.Visible = True
        End If
    End Sub
    Public Sub sChangeTab()
        If Tab_Main.Tabs("Tab_Summary").Selected Then
            Tab_Main.Tabs("Tab_Details").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", True, False, "»ÕÀ")
        Else
            Tab_Main.Tabs("Tab_Summary").Selected = True
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "«· ›«’Ì·")
        End If
    End Sub
#End Region
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fIfsaveNeeded() As Boolean
        If vMasterBlock = "I" Or vMasterBlock = "U" Then
            Return True
        End If

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Childrens.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next

        For Each vRow In Grd_Wives.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next

        Return False
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        'If Txt_Status.Text = "„ﬁ›·" Then
        '    Return True
        'End If

        If fIfsaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        If pAskMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                If fValidateMain() Then
                    sSaveMain()
                    sSave_MainPicture()
                Else
                    Return False
                End If

                If fValidateChildrens() Then
                    sSaveChildrens()
                Else
                    Return False
                End If

                If fValidateWives() Then
                    sSaveWives()
                Else
                    Return False
                End If
            Else
                vMasterBlock = "NI"
                DTS_Childrens.Rows.Clear()
                'DTS_AddDed.Rows.Clear()
                Return True
            End If
        Else
            If fValidateMain() Then
                sSaveMain()
                sSave_MainPicture()
            Else
                Return False
            End If

            If fValidateChildrens() Then
                sSaveChildrens()
            Else
                Return False
            End If

            If fValidateWives() Then
                sSaveWives()
            Else
                Return False
            End If
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            If vClear = True Then
                sSetFlagsUpdate()
            End If
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            Return True
        End If
    End Function
    Private Sub sSetFlagsUpdate()
        vMasterBlock = "N"
        sQueryChildrens()
        sQueryWives()

        'vMasterBlock = "NI"
        'DTS_Details.Rows.Clear()
        'sNewRecord()
    End Sub
#End Region
#Region " Query                                                                          "
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pItemCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)
        If fSaveAll(True) = False Then
            Return
        End If

        Dim vFetchRec As Integer
        If pItemCode = Nothing Then
            If pIsGoTo = False Then
                If pRecPos = Nothing Then
                    vFetchRec = 1
                Else
                    vFetchRec = vcFrmLevel.vRecPos + pRecPos
                    If vFetchRec > cControls.fCount_Rec(" From Members ") Then
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
            vFetchRec = cControls.fCount_Rec(" From Members ")
        End If

        Dim vFetchCondition As String
        If pItemCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And MyMembers.Code = '" & Trim(pItemCode) & "'"
        End If

        Try
            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText = _
            " With MyMembers as                                            " & _
            "( Select Members.Code,                                        " & _
            "         Members.Name,                                        " & _
            "         Members.Remarks,                                     " & _
            "         Emp_Code,                                            " & _
            "         HR1.DescA as Emp_Desc,                                " & _
            "         Members.TDate,                                        " & _
            "         Members.BirthDate,                                    " & _
            "         Tel,                                                  " & _
            "         Job,                                                  " & _
            "         Country_Code,                                         " & _
            "         Location,                                             " & _
            "         ID,                                                   " & _
            "         Members.Picture,                                      " & _
            "         ROW_Number() Over (Order By Members.Code) as  RecPos " & _
            " From Members Inner Join Employees HR1                        " & _
            " On Members.Emp_Code = HR1.Code         )                      " & _
            " Select * From MyMembers                                      " & _
            " Where 1 = 1                                                         " & _
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If vSqlReader.IsDBNull(13) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader(13))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader(13))

                'Code
                Txt_Code.Text = Trim(vSqlReader(0))

                'Name
                Txt_Name.Text = Trim(vSqlReader(1))

                'Remarks
                If vSqlReader.IsDBNull(2) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader(2))
                Else
                    Txt_Remarks.Text = ""
                End If

                'Emp_Code
                If vSqlReader.IsDBNull(3) = False Then
                    Txt_EmpCode.Text = Trim(vSqlReader(3))
                Else
                    Txt_EmpCode.Text = ""
                End If

                'Emp_Desc
                If vSqlReader.IsDBNull(4) = False Then
                    Txt_EmpDesc.Text = Trim(vSqlReader(4))
                Else
                    Txt_EmpDesc.Text = ""
                End If

                'TDate
                If vSqlReader.IsDBNull(5) = False Then
                    Txt_TDate.Value = Trim(vSqlReader(5))
                Else
                    Txt_TDate.Value = Nothing
                End If

                'TDate
                If vSqlReader.IsDBNull(6) = False Then
                    Txt_BirthDate.Value = Trim(vSqlReader(6))
                Else
                    Txt_BirthDate.Value = Nothing
                End If

                'Age
                If vSqlReader.IsDBNull(6) = False Then
                    Txt_Age.Text = Year(Now) - Year(vSqlReader(6))
                Else
                    Txt_Age.Text = ""
                End If

                'Tel
                If vSqlReader.IsDBNull(7) = False Then
                    Txt_Tel.Text = Trim(vSqlReader(7))
                Else
                    Txt_Tel.Text = ""
                End If

                'Job
                If vSqlReader.IsDBNull(8) = False Then
                    Txt_Job.Text = Trim(vSqlReader(8))
                Else
                    Txt_Job.Text = ""
                End If

                'Country
                If vSqlReader.IsDBNull(9) = False Then
                    Txt_Countries.Value = Trim(vSqlReader(9))
                Else
                    Txt_Countries.SelectedIndex = -1
                End If

                'Location
                If vSqlReader.IsDBNull(10) = False Then
                    Txt_Location.Value = Trim(vSqlReader(10))
                Else
                    Txt_Location.SelectedIndex = -1
                End If

                'ID
                If vSqlReader.IsDBNull(11) = False Then
                    Txt_ID.Text = Trim(vSqlReader(11))
                Else
                    Txt_ID.Text = ""
                End If

                'Picture
                If vSqlReader.IsDBNull(12) = False Then
                    Dim arrayImage() As Byte = CType(vSqlReader(12), Byte())
                    Dim ms As New IO.MemoryStream(arrayImage)
                    PictureBox1.Image = Image.FromStream(ms)
                    PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

                Else
                    PictureBox1.Image = PictureBox1.InitialImage
                    PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            sQueryChildrens()
            sQueryWives()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
            'cException.sHandleException(ex.Message, Me.Name, "sQuery")
        End Try
        vMasterBlock = "N"
    End Sub
    Private Sub sLoad_CarsCompanies()
        Txt_Countries.Items.Clear()
        Dim vsqlCommand As New SqlClient.SqlCommand
        vsqlCommand.CommandText = " Select Code, DescA From Countries Order By Code "

        vsqlCommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
        Do While vSqlReader.Read
            Txt_Countries.Items.Add(Trim(vSqlReader(0)), Trim(vSqlReader(1)))
        Loop
        cControls.vSqlConn.Close()
        vSqlReader.Close()

    End Sub
#End Region
#Region " Delete                                                                         "
    Public Sub sDelete()
        Dim vSqlstring As String

        If vMasterBlock = "I" Then
            If Grd_Childrens.Focused Then
                Grd_Childrens.ActiveRow.Delete(False)
                Exit Sub
            ElseIf Grd_Wives.Focused Then
                Grd_Wives.ActiveRow.Delete(False)
                Exit Sub
            ElseIf vFocus = "Master" Then
                sNewRecord()
                Exit Sub
            End If
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then

                If Grd_Childrens.Focused Then
                    If Not Grd_Childrens.ActiveRow Is Nothing Then
                        If Grd_Childrens.ActiveRow.Cells("DML").Value = "I" Or Grd_Childrens.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_Childrens.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_Childrens.ActiveRow.Cells("DML").Value = "N" Or Grd_Childrens.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring = _
                            " Delete From Members_Childrens " & _
                            " Where  PE_Code   = '" & Txt_Code.Text & "'" & _
                            " And    Ser       = '" & Grd_Childrens.ActiveRow.Cells("Ser").Value & "'"
                        End If
                    End If
                ElseIf Grd_Wives.Focused Then
                    If Not Grd_Wives.ActiveRow Is Nothing Then
                        If Grd_Wives.ActiveRow.Cells("DML").Value = "I" Or Grd_Wives.ActiveRow.Cells("DML").Value = "NI" Then
                            Grd_Wives.ActiveRow.Delete(False)
                            Exit Sub
                        ElseIf Grd_Wives.ActiveRow.Cells("DML").Value = "N" Or Grd_Wives.ActiveRow.Cells("DML").Value = "U" Then
                            vSqlstring = _
                            " Delete From Members_Wives " & _
                            " Where  PE_Code   = '" & Txt_Code.Text & "'" & _
                            " And    Ser       = '" & Grd_Wives.ActiveRow.Cells("Ser").Value & "'"
                        End If
                    End If
                ElseIf vFocus = "Master" Then
                    vSqlstring = _
                    " Delete From Members_Childrens Where PE_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Members_Wives Where PE_Code = '" & Txt_Code.Text & "'" & _
                    " Delete From Members Where Code = '" & Txt_Code.Text & "'"
                End If
            End If

            If cControls.fSendData(vSqlstring, Me.Name) > 0 Then

                If Grd_Childrens.Focused Then
                    Grd_Childrens.ActiveRow.Delete(False)
                ElseIf Grd_Wives.Focused Then
                    Grd_Wives.ActiveRow.Delete(False)
                End If
                vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
            End If
        End If
    End Sub
#End Region
#Region " Find                                                                           "
    Public Sub sFind()
        sOpenLov("Select Code, Name From Users", "«·„ÊŸ›Ì‰")
    End Sub
#End Region
#End Region
#Region " New Record                                                                     "
    Public Sub sNewRecord()
        If fSaveAll(True) = False Then
            Return
        End If

        Tab_Main.Tabs("Tab_Details").Selected = True
        sNewCode()
        Txt_Name.Clear()
        Txt_Remarks.Clear()
        Txt_TDate.Value = Now
        Txt_EmpCode.Text = ""
        Txt_EmpDesc.Text = ""
        Txt_BirthDate.Value = Nothing
        Txt_Age.Text = ""
        Txt_Tel.Text = ""
        Txt_Job.Text = ""
        Txt_WifeName.Text = ""
        Txt_WifeBirthDate.Value = Nothing
        Txt_WifeAge.Text = ""
        Txt_ID.Text = ""
        Txt_Location.SelectedIndex = -1
        Txt_Countries.SelectedIndex = -1

        PictureBox1.Image = PictureBox1.InitialImage

        Txt_EmpCode.Text = vUsrCode
        Txt_EmpDesc.Text = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & vUsrCode & "'", Me.Name)
        vMasterBlock = "NI"
        vFocus = "Master"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")

        DTS_Childrens.Rows.Clear()
        DTS_Wives.Rows.Clear()
    End Sub
    Private Sub sNewCode()
        Dim vSqlString As String
        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From  Members "
        Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(6, "0")
    End Sub

#End Region
#Region " sOpenLov                                                                       "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String, Optional ByVal pTableName As String = "", Optional ByVal pAdditionalString As String = "")
        vLovReturn1 = ""
        VLovReturn2 = ""
        vLovReturn3 = ""
        Dim vPrice, vDeduction, vPU As String
        If pTitle = "«·√’‰«›" Then
            Dim Frm_Items As New Frm_ItemsLov
            Frm_Items.ShowDialog()
            If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
                Grd_Childrens.ActiveRow.Cells("Item_Code").Value = Trim(vLovReturn1)
                Grd_Childrens.ActiveRow.Cells("Item_Desc").Value = Trim(VLovReturn2)
                If Txt_Countries.SelectedIndex <> -1 Then
                    vPrice = cControls.fReturnValue(" Select Price From Items_SalesTypes " & _
                                                 " Where Item_Code      = '" & Trim(vLovReturn1) & "'" & _
                                                 " And   St_Code        = '" & Trim(Txt_Countries.Value) & "'", Me.Name)

                    If vPrice <> "" Then
                        Grd_Childrens.ActiveRow.Cells("Price").Value = vPrice
                    Else
                        Grd_Childrens.ActiveRow.Cells("Price").Value = DBNull.Value
                    End If
                Else
                    vPrice = cControls.fReturnValue(" Select SPrice From Items " & _
                                                 " Where  Code      = '" & Trim(vLovReturn1) & "'", Me.Name)

                    If vPrice <> "" Then
                        Grd_Childrens.ActiveRow.Cells("Price").Value = vPrice
                    Else
                        Grd_Childrens.ActiveRow.Cells("Price").Value = DBNull.Value
                    End If

                    vDeduction = cControls.fReturnValue(" Select Deduction From Items " & _
                                                 " Where  Code      = '" & Trim(vLovReturn1) & "'", Me.Name)

                    If vDeduction <> "" Then
                        Grd_Childrens.ActiveRow.Cells("Deduction").Value = vPrice
                    Else
                        Grd_Childrens.ActiveRow.Cells("Deduction").Value = DBNull.Value
                    End If
                End If

                vPU = cControls.fReturnValue(" Select Pack_Unit.DescA From Items Left Join Pack_Unit " & _
                                             " On     Pack_Unit.Code = Items.PU_Code                 " & _
                                             " Where  Items.Code      = '" & Trim(vLovReturn1) & "'", Me.Name)

                If vPU <> "" Then
                    Grd_Childrens.ActiveRow.Cells("PU_Desc").Value = vPU
                Else
                    Grd_Childrens.ActiveRow.Cells("PU_Ser").Value = DBNull.Value
                    Grd_Childrens.ActiveRow.Cells("PU_Desc").Value = DBNull.Value
                End If

                Grd_Childrens.ActiveRow.Cells("Quantity").Value = DBNull.Value
                Grd_Childrens.ActiveRow.Cells("Addition").Value = DBNull.Value
                Grd_Childrens.ActiveRow.Cells("Deduction").Value = DBNull.Value

            End If
        Else
            'Dim Frm_Lov As New FRM_LovTreeL(pSqlStatment, pTitle, pTableName, pAdditionalString)
            'Frm_Lov.ShowDialog()
            'If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            '    If pTitle = "«·„ÊŸ›Ì‰" Then
            '        Txt_EmpCode.Text = vLovReturn1
            '        Txt_EmpDesc.Text = VLovReturn2
            '    End If
            'End If
        End If

    End Sub
#End Region
#Region " Print                                                                          "
    Public Sub sPrint()
        'If vMasterBlock = "NI" Then
        '    Return
        'End If

        'vClear = False
        'If fSaveAll(True) = False Then
        '    Return
        'End If
        'vClear = True

        vSortedList.Clear()
        Dim vCompany As String = cControls.fReturnValue(" Select DescA From Company ", Me.Name)
        Dim vSqlString As String
        vSqlString = _
        " Select Members.Code as Member_Code,       " & _
        "        Members.Name as Member_Name,       " & _
        "        Members.Picture                    " & _
        " From   Members                            " & _
        " Where  1 = 1                              " & _
        sFndByMembers("Members")

        vSortedList.Add("DT_MembersCard", vSqlString)

        Dim vRep_Preview As New FRM_ReportPreviewL("Members Card", vSortedList, New DS_MembersCard, New Rpt_MembersCard)
        vRep_Preview.MdiParent = Me.MdiParent
        vRep_Preview.Show()

        'vSqlString = _
        '" Select Purchase_Invoice_Details.Ser,       " & _
        '"        Purchase_Invoice_Details.Item_Code, " & _
        '"        Purchase_Invoice_Details.Item_Ser,  " & _
        '"        Items.DescA as Item_Desc,  " & _
        '"        Purchase_Invoice_Details.Quantity,    " & _
        '"        Purchase_Invoice_Details.Price,       " & _
        '"        Purchase_Invoice_Details.Addition,    " & _
        '"        Purchase_Invoice_Details.Deduction,    " & _
        '"        Purchase_Invoice_Details.LCost        " & _
        '" From   Purchase_Invoice_Details Inner Join Items " & _
        '" On     Purchase_Invoice_Details.Item_Code = Items.Code " & _
        '" And    Purchase_Invoice_Details.Item_Ser  = Items.Ser  " & _
        '" Where  PI_Code = '" & Trim(Txt_Code.Text) & "'" & _
        '" Order  By Ser       "

        'vSortedList.Add("DT_PurchaseInvoiceDetails", vSqlString)


    End Sub
    Private Function sFndByMembers(ByVal pTableName As String) As String

        Dim vItemValues As String = ""
        Dim vRow As UltraGridRow

        If Grd_Summary.Selected.Rows.Count > 0 Then
            For Each vRow In Grd_Summary.Selected.Rows
                If Trim(vItemValues.Length) > 0 Then
                    vItemValues += ", "
                End If
                vItemValues += "'" & vRow.Cells("Code").Text & "'"
            Next

            sFndByMembers = " And " & pTableName & ".Code  In  (" & vItemValues & ")"
        Else
            sFndByMembers = ""
        End If
    End Function
#End Region
#End Region

#Region " Master                                                                         "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidateMain() As Boolean
        If Txt_Name.Text.Length = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("4", Me)
            Txt_Name.Select()
            Return False
        End If

        If Txt_EmpDesc.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("42", Me)
            Txt_EmpDesc.Select()
            Return False
        End If

        If Txt_Location.SelectedIndex = -1 Then
            MessageBox.Show("Select the location")
            Txt_Location.Select()
            Return False
        End If

        'If Grd_Details.Rows.Count = 0 Then
        '    vcFrmLevel.vParentFrm.sForwardMessage("52", Me)
        '    Return False
        'End If
        Return True
    End Function
    Private Sub sSaveMain()
        'If fValidateMain() = False Then
        '    Return
        'End If
        Dim vSqlCommand As String = ""
        Dim vDate, vCountry_Code, vBirthDate, vWife_BirthDate As String

        If Not IsDBNull(Txt_TDate.Value) Then
            If Not Txt_TDate.Value = Nothing Then
                vDate = "'" & Format(Txt_TDate.Value, "MM-dd-yyyy HH:mm") & "'"
            Else
                vDate = "NULL"
            End If
        Else
            vDate = "NULL"
        End If

        If Txt_Countries.SelectedIndex = -1 Then
            vCountry_Code = "NULL"
        Else
            vCountry_Code = Txt_Countries.Value
        End If

        If Not IsDBNull(Txt_BirthDate.Value) Then
            If Not Txt_BirthDate.Value = Nothing Then
                vBirthDate = "'" & Format(Txt_BirthDate.Value, "MM-dd-yyyy HH:mm") & "'"
            Else
                vBirthDate = "NULL"
            End If
        Else
            vBirthDate = "NULL"
        End If

        If Not IsDBNull(Txt_WifeBirthDate.Value) Then
            If Not Txt_WifeBirthDate.Value = Nothing Then
                vWife_BirthDate = "'" & Format(Txt_WifeBirthDate.Value, "MM-dd-yyyy HH:mm") & "'"
            Else
                vWife_BirthDate = "NULL"
            End If
        Else
            vWife_BirthDate = "NULL"
        End If

        If vMasterBlock = "I" Then
            vSqlCommand = " Insert Into Members  (              Code,                          Name,                 TDate,                   Emp_Code,                        Remarks,                BirthDate,                   Tel,                          Job,                 Country_Code,                    ID,                     Location )" & _
                                      " Values   ('" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Name.Text) & "', " & vDate & ", '" & Trim(Txt_EmpCode.Text) & "', '" & Trim(Txt_Remarks.Text) & "', " & vBirthDate & ", '" & Trim(Txt_Tel.Text) & "', '" & Trim(Txt_Job.Text) & "', " & vCountry_Code & ", '" & Trim(Txt_ID.Text) & "', '" & Txt_Location.Value & "' )"

            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then
            vSqlCommand = " Update   Members " & _
                          " Set   Name         = '" & Trim(Txt_Name.Text) & "'," & _
                          "       TDate         =  " & vDate & ", " & _
                          "       Emp_Code      = '" & Trim(Txt_EmpCode.Text) & "'," & _
                          "       Remarks       = '" & Trim(Txt_Remarks.Text) & "', " & _
                          "       BirthDate     =  " & vBirthDate & ", " & _
                          "       Tel           = '" & Trim(Txt_Tel.Text) & "', " & _
                          "       Job           = '" & Trim(Txt_Job.Text) & "', " & _
                          "       Country_Code  =  " & vCountry_Code & ", " & _
                          "       ID            = '" & Trim(Txt_ID.Text) & "', " & _
                          "       Location      = '" & Txt_Location.Value & "' " & _
                          " Where Code          = '" & Txt_Code.Text & "'"

            sFillSqlStatmentArray(vSqlCommand)
        End If
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "
    Private Sub TXT_All_EditorButtonClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinEditors.EditorButtonEventArgs) Handles _
    Txt_EmpCode.EditorButtonClick

        If sender.name = "Txt_EmpCode" Then
            'sOpenLov(" Select Code, DescA From Employees Where 1 = 1 ", "«·„ÊŸ›Ì‰")
        ElseIf sender.name = "Txt_ProviderCode" Then
            sOpenLov(" Select Code, DescA From Branches Where 1 = 1  ", "«·›—Ê⁄")
        ElseIf sender.name = "Txt_StoreCode" Then
            sOpenLov(" Select Code, DescA From Stores Where 1 = 1 ", "«·„Œ«“‰")
        ElseIf sender.name = "Txt_Items" Then
            sOpenLov("", "«·√’‰«›")
        ElseIf sender.name = "Txt_Beneficiary" Then
            sOpenLov(" Select Code, DescA From Providers Where 1 = 1 ", "«·„” ›Ìœ")
        ElseIf sender.name = "Txt_AddDed" Then
            sOpenLov(" Select Code, DescA, Type From Addition_Deduction Where 1 = 1 ", "«·√÷«›«  Ê«·Œ’Ê„« ")
        ElseIf sender.name = "Txt_SalesManCode" Then
            sOpenLov(" Select Code, DescA From Employees Where IsSalesman = 'Y' ", "«·„‰œÊ»Ì‰")
        ElseIf sender.name = "Txt_CurrencyCode" Then
            sOpenLov(" Select Code, DescA from Currencies Where 1 = 1 ", "«·⁄„·« ")
        ElseIf sender.name = "Txt_PackUnit" Then
            sOpenLov(" Select Code, DescA from Pack_Unit Where 1 = 1 ", "«·ÊÕœ« ")
        End If
    End Sub
    Private Sub Txt_EmpCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_EmpCode.Validating
        If Txt_EmpCode.Text <> "" Then
            If cControls.fCount_Rec(" From Employees Where Code = '" & Trim(Txt_EmpCode.Text) & "'") = 0 Then
                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                Txt_EmpCode.Select()
                e.Cancel = True
            Else
                Txt_EmpDesc.Text = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & Trim(Txt_EmpCode.Text) & "'", Me.Name)
                e.Cancel = False
            End If
        Else
            Txt_EmpDesc.Text = ""
        End If
    End Sub
    Private Sub Txt_All_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Txt_Code.Enter, Txt_Name.Enter, Txt_Remarks.Enter, Txt_TDate.Enter, _
    Txt_EmpCode.Enter, Txt_EmpDesc.Enter

        vFocus = "Master"
    End Sub
#End Region
#End Region
#Region " Details                                                                        "
#Region " Childrens                                                                      "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidateChildrens() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Childrens.Rows
            If IsDBNull(vRow.Cells("Name").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("13", Me)
                'Tab_Details.Tabs("Tab_Items").Selected = True
                vRow.Cells("Name").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveChildrens()
        'If fValidateChildrens() = False Then
        '    Return
        'End If
        Dim vRow As UltraGridRow
        Grd_Childrens.UpdateData()

        'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
        Dim vCounter As Integer = 0
        Dim vBirthDate As String

        Dim vSqlString, vGetSerial As String

        For Each vRow In Grd_Childrens.Rows

            If IsDBNull(vRow.Cells("BirthDate").Value) Then
                vBirthDate = "NULL"
            Else
                vBirthDate = "'" & Format(vRow.Cells("BirthDate").Value, "MM-dd-yyyy HH:mm") & "'"
            End If

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Members_Childrens " & _
                             " Where  PE_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Members_Childrens  (          PE_Code,                    Ser,                               Name,                   BirthDate,                          Grade,                                    Health_Status )" & _
                             "                        Values  ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("Name").Text) & "', " & vBirthDate & ", '" & Trim(vRow.Cells("Grade").Text) & "', '" & Trim(vRow.Cells("Healthy_Status").Text) & "') "

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Members_Childrens " & _
                              " Set     Name        = '" & Trim(vRow.Cells("Name").Text) & "', " & _
                              "         BirthDate     =  " & vBirthDate & "," & _
                              "         Grade         = '" & Trim(vRow.Cells("Grade").Text) & "', " & _
                              "         Health_Status = '" & Trim(vRow.Cells("Healthy_Status").Text) & "' " & _
                              " Where   PE_Code      = '" & Txt_Code.Text & "'" & _
                              " And     Ser          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryChildrens()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Members_Childrens.Ser,         " & _
            "        Members_Childrens.Name,        " & _
            "        Members_Childrens.BirthDate,   " & _
            "        Grade,                         " & _
            "        Health_Status                  " & _
            " From   Members_Childrens " & _
            " Where  PE_Code = '" & Trim(Txt_Code.Text) & "'" & _
            " Order  By Ser       "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vRow As UltraGridRow
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Childrens.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Childrens.Rows.SetCount(vRowCounter + 1)
                DTS_Childrens.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                DTS_Childrens.Rows(vRowCounter)("Name") = Trim(vSqlReader(1))

                'If vSqlReader.IsDBNull(2) = False Then
                '    DTS_Details.Rows(vRowCounter)("Name") = Trim(vSqlReader(2))
                'Else
                '    DTS_Details.Rows(vRowCounter)("Name") = ""
                'End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Childrens.Rows(vRowCounter)("BirthDate") = Trim(vSqlReader(2))
                Else
                    DTS_Childrens.Rows(vRowCounter)("BirthDate") = Nothing
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Childrens.Rows(vRowCounter)("Age") = Year(Now.Date) - Year(vSqlReader(2))
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Childrens.Rows(vRowCounter)("Grade") = Trim(vSqlReader(3))
                Else
                    DTS_Childrens.Rows(vRowCounter)("Grade") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Childrens.Rows(vRowCounter)("Healthy_Status") = Trim(vSqlReader(4))
                Else
                    DTS_Childrens.Rows(vRowCounter)("Healthy_Status") = ""
                End If

                vRow = Grd_Childrens.Rows(vRowCounter)

                DTS_Childrens.Rows(vRowCounter)("SerNum") = DTS_Childrens.Rows(vRowCounter).Index + 1
                DTS_Childrens.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Grd_Childrens.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQueryDetails")
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "

    Private Sub Grd_Details_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles Grd_Childrens.AfterRowInsert
        Try
            e.Row.Cells("SerNum").Value = e.Row.Index + 1

            'vComboEditor.Name = vCounter
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_Details_BeforeCellDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Grd_Childrens.BeforeCellDeactivate
        Try
            Dim vRow As UltraGridRow = Grd_Childrens.ActiveRow
            If vRow.Cells("BirthDate").Activated Then
                If Not IsDBNull(vRow.Cells("BirthDate").Value) Then
                    vRow.Cells("Age").Value = Year(Now.Date) - Year(vRow.Cells("BirthDate").Value)
                Else
                    vRow.Cells("Age").Value = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_Details_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Childrens.CellChange
        Try
            If sender.ActiveRow.Cells("DML").Value = "NI" Then
                sender.ActiveRow.Cells("DML").Value = "I"
            ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
                sender.ActiveRow.Cells("DML").Value = "U"
            End If

            Grd_Childrens.UpdateData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Grd_Details_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Childrens.Enter
        Try
            vFocus = "Details"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_Details_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Childrens.KeyUp
        Try
            Grd_Childrens.UpdateData()

            If e.KeyData = Keys.Enter Then
                If Grd_Childrens.ActiveRow.Cells("Item_Code").Activated = True Then
                    If IsDBNull(Grd_Childrens.ActiveRow.Cells("Item_Code").Value) = False Then
                        If Grd_Childrens.ActiveRow.Cells("Item_Code").Value <> "" Then
                            If cControls.fCount_Rec("From Items " & _
                                " Where Code = '" & Grd_Childrens.ActiveRow.Cells("Item_Code").Value & "'") = 0 Then
                                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                                Grd_Childrens.ActiveRow.Cells("Item_Code").SelectAll()
                                Grd_Childrens.ActiveRow.Cells("Item_Desc").Value = ""
                            Else
                                Dim vSqlString As String = " Select DescA From Items " & _
                                "Where Code = '" & Grd_Childrens.ActiveRow.Cells("Item_Code").Value & "'"
                                Grd_Childrens.ActiveRow.Cells("Item_Desc").Value = cControls.fReturnValue(vSqlString, Me.Name)
                                Grd_Childrens.PerformAction(UltraGridAction.PrevCell)
                                Grd_Childrens.PerformAction(UltraGridAction.PrevCell)
                                Grd_Childrens.PerformAction(UltraGridAction.EnterEditMode)
                            End If
                        End If
                    End If
                ElseIf Grd_Childrens.ActiveRow.Cells("Deduction").Activated = True Then
                    Grd_Childrens.PerformAction(UltraGridAction.NextRow)
                    Grd_Childrens.ActiveRow.Cells("Item_Code").Selected = True
                    Grd_Childrens.ActiveRow.Cells("Item_Code").Activate()
                    Grd_Childrens.PerformAction(UltraGridAction.EnterEditMode)
                Else
                    Grd_Childrens.PerformAction(UltraGridAction.PrevCell)
                    Grd_Childrens.PerformAction(UltraGridAction.EnterEditMode)
                    'SendKeys.Send("{Tab}")
                End If
            ElseIf e.KeyData = Keys.F12 Then
                If Grd_Childrens.ActiveRow.Cells("Item_Code").Activated = True Then
                    sOpenLov("", "«·√’‰«›")
                ElseIf Grd_Childrens.ActiveRow.Cells("PU_Code").Activated = True Then
                    sOpenLov(" Select Code, DescA from Pack_Unit Where 1 = 1 ", "«·ÊÕœ« ")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GRD_Details_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Childrens.DoubleClick

    End Sub

#End Region
#End Region
#Region " Wives                                                                          "
#Region " DataBase                                                                       "
#Region " Save                                                                           "
    Private Function fValidateWives() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Wives.Rows
            If IsDBNull(vRow.Cells("Name").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("13", Me)
                'Tab_Details.Tabs("Tab_Items").Selected = True
                vRow.Cells("Name").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveWives()
        'If fValidateWives() = False Then
        '    Return
        'End If
        Dim vRow As UltraGridRow
        Grd_Wives.UpdateData()

        'Grd_Details.PerformAction(UltraGridAction.ExitEditMode)
        Dim vCounter As Integer = 0
        Dim vBirthDate As String

        Dim vSqlString, vGetSerial As String

        For Each vRow In Grd_Wives.Rows

            If IsDBNull(vRow.Cells("BirthDate").Value) Then
                vBirthDate = "NULL"
            Else
                vBirthDate = "'" & Format(vRow.Cells("BirthDate").Value, "MM-dd-yyyy HH:mm") & "'"
            End If

            If vRow.Cells("DML").Value = "I" Then
                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From Members_Wives " & _
                             " Where  PE_Code = '" & Txt_Code.Text & "'"
                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(3, "0") + vCounter

                vSqlString = " Insert Into Members_Wives  (          PE_Code,                    Ser,                               Name,                   BirthDate,                          Grade,                                    Health_Status )" & _
                             "                        Values  ('" & Trim(Txt_Code.Text) & "', " & vGetSerial & ", '" & Trim(vRow.Cells("Name").Text) & "', " & vBirthDate & ", '" & Trim(vRow.Cells("Grade").Text) & "', '" & Trim(vRow.Cells("Healthy_Status").Text) & "') "

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1
            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update   Members_Wives " & _
                              " Set     Name        = '" & Trim(vRow.Cells("Name").Text) & "', " & _
                              "         BirthDate     =  " & vBirthDate & "," & _
                              "         Grade         = '" & Trim(vRow.Cells("Grade").Text) & "', " & _
                              "         Health_Status = '" & Trim(vRow.Cells("Healthy_Status").Text) & "' " & _
                              " Where   PE_Code      = '" & Txt_Code.Text & "'" & _
                              " And     Ser          = '" & vRow.Cells("Ser").Text & "'"
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub
#End Region
#Region " Query                                                                          "
    Private Sub sQueryWives()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            " Select Members_Wives.Ser,         " & _
            "        Members_Wives.Name,        " & _
            "        Members_Wives.BirthDate,   " & _
            "        Grade,                         " & _
            "        Health_Status                  " & _
            " From   Members_Wives " & _
            " Where  PE_Code = '" & Trim(Txt_Code.Text) & "'" & _
            " Order  By Ser       "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vRow As UltraGridRow
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Wives.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Wives.Rows.SetCount(vRowCounter + 1)
                DTS_Wives.Rows(vRowCounter)("Ser") = Trim(vSqlReader(0))
                DTS_Wives.Rows(vRowCounter)("Name") = Trim(vSqlReader(1))

                'If vSqlReader.IsDBNull(2) = False Then
                '    DTS_Details.Rows(vRowCounter)("Name") = Trim(vSqlReader(2))
                'Else
                '    DTS_Details.Rows(vRowCounter)("Name") = ""
                'End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Wives.Rows(vRowCounter)("BirthDate") = Trim(vSqlReader(2))
                Else
                    DTS_Wives.Rows(vRowCounter)("BirthDate") = Nothing
                End If

                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Wives.Rows(vRowCounter)("Age") = Year(Now.Date) - Year(vSqlReader(2))
                End If

                If vSqlReader.IsDBNull(3) = False Then
                    DTS_Wives.Rows(vRowCounter)("Grade") = Trim(vSqlReader(3))
                Else
                    DTS_Wives.Rows(vRowCounter)("Grade") = ""
                End If

                If vSqlReader.IsDBNull(4) = False Then
                    DTS_Wives.Rows(vRowCounter)("Healthy_Status") = Trim(vSqlReader(4))
                Else
                    DTS_Wives.Rows(vRowCounter)("Healthy_Status") = ""
                End If

                vRow = Grd_Wives.Rows(vRowCounter)

                DTS_Wives.Rows(vRowCounter)("SerNum") = DTS_Wives.Rows(vRowCounter).Index + 1
                DTS_Wives.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Grd_Wives.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            'cException.sHandleException(ex.Message, Me.Name, "sQueryDetails")
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region
#Region " Form Level                                                                     "

    Private Sub Grd_Wives_AfterRowInsert(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles Grd_Wives.AfterRowInsert
        Try
            e.Row.Cells("SerNum").Value = e.Row.Index + 1

            'vComboEditor.Name = vCounter
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_Wives_BeforeCellDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Grd_Wives.BeforeCellDeactivate
        Try
            Dim vRow As UltraGridRow = Grd_Wives.ActiveRow
            If vRow.Cells("BirthDate").Activated Then
                If Not IsDBNull(vRow.Cells("BirthDate").Value) Then
                    vRow.Cells("Age").Value = Year(Now.Date) - Year(vRow.Cells("BirthDate").Value)
                Else
                    vRow.Cells("Age").Value = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_Wives_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Wives.CellChange
        Try
            If sender.ActiveRow.Cells("DML").Value = "NI" Then
                sender.ActiveRow.Cells("DML").Value = "I"
            ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
                sender.ActiveRow.Cells("DML").Value = "U"
            End If

            Grd_Wives.UpdateData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Grd_Wives_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Wives.Enter
        Try
            vFocus = "Details"
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Grd_Wives_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Wives.KeyUp
        Try
            Grd_Wives.UpdateData()

            If e.KeyData = Keys.Enter Then
                If Grd_Wives.ActiveRow.Cells("Item_Code").Activated = True Then
                    If IsDBNull(Grd_Wives.ActiveRow.Cells("Item_Code").Value) = False Then
                        If Grd_Wives.ActiveRow.Cells("Item_Code").Value <> "" Then
                            If cControls.fCount_Rec("From Items " & _
                                " Where Code = '" & Grd_Wives.ActiveRow.Cells("Item_Code").Value & "'") = 0 Then
                                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                                Grd_Wives.ActiveRow.Cells("Item_Code").SelectAll()
                                Grd_Wives.ActiveRow.Cells("Item_Desc").Value = ""
                            Else
                                Dim vSqlString As String = " Select DescA From Items " & _
                                "Where Code = '" & Grd_Wives.ActiveRow.Cells("Item_Code").Value & "'"
                                Grd_Wives.ActiveRow.Cells("Item_Desc").Value = cControls.fReturnValue(vSqlString, Me.Name)
                                Grd_Wives.PerformAction(UltraGridAction.PrevCell)
                                Grd_Wives.PerformAction(UltraGridAction.PrevCell)
                                Grd_Wives.PerformAction(UltraGridAction.EnterEditMode)
                            End If
                        End If
                    End If
                ElseIf Grd_Wives.ActiveRow.Cells("Deduction").Activated = True Then
                    Grd_Wives.PerformAction(UltraGridAction.NextRow)
                    Grd_Wives.ActiveRow.Cells("Item_Code").Selected = True
                    Grd_Wives.ActiveRow.Cells("Item_Code").Activate()
                    Grd_Wives.PerformAction(UltraGridAction.EnterEditMode)
                Else
                    Grd_Wives.PerformAction(UltraGridAction.PrevCell)
                    Grd_Wives.PerformAction(UltraGridAction.EnterEditMode)
                    'SendKeys.Send("{Tab}")
                End If
            ElseIf e.KeyData = Keys.F12 Then
                If Grd_Wives.ActiveRow.Cells("Item_Code").Activated = True Then
                    sOpenLov("", "«·√’‰«›")
                ElseIf Grd_Wives.ActiveRow.Cells("PU_Code").Activated = True Then
                    sOpenLov(" Select Code, DescA from Pack_Unit Where 1 = 1 ", "«·ÊÕœ« ")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GRD_Wives_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Wives.DoubleClick

    End Sub

#End Region
#End Region
#End Region

#Region " Summary                                                                        "
    Private Sub sQuerySummaryMain(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            Dim vCodeFilter, vDescFilter, vDate, vStatus As String
            If pCode = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Members.Code Like '%" & pCode & "%'"
            End If

            If pDesc = "" Then
                vDescFilter = ""
            Else
                vDescFilter = " And Members.DescA Like '%" & pDesc & "%'"
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
            End If

            If DTS_Summary.Band.Columns.Count = 0 Then
                Return
            End If

            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText = _
            "  Select Members.Code,                                         " & _
            "         Members.Name,                                         " & _
            "         dbo.Get_All_Wives_And_Childrens_Sum(Members.Code) as Family_Members ,  " & _
            "         ROW_Number() Over (Order By Members.Code) as  RecPos  " & _
            " From Members Inner Join Employees HR1                         " & _
            " On Members.Emp_Code = HR1.Code                                " & _
            " LEFT JOIN Members_Childrens                                   " & _
            " ON Members.Code = Members_Childrens.PE_Code                   " & _
            " LEFT JOIN Members_Wives                                       " & _
            " ON Members.Code = Members_Wives.PE_Code                       " & _
            " Group BY Members.Code, Members.Name                           " & _
            vCodeFilter & _
            vDescFilter

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

                'TDate
                'If vSqlReader.IsDBNull(3) = False Then
                '    DTS_Summary.Rows(vRowCounter)("TDate") = Trim(vSqlReader(3))
                'Else
                '    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                'End If

                'Family_Member
                If vSqlReader.IsDBNull(2) = False Then
                    DTS_Summary.Rows(vRowCounter)("Age") = vSqlReader(2)
                Else
                    DTS_Summary.Rows(vRowCounter)("Age") = Nothing
                End If

                'Age
                'If vSqlReader.IsDBNull(2) = False Then
                '    DTS_Summary.Rows(vRowCounter)("Age") = Year(Now) - Year(vSqlReader(2))
                'Else
                '    DTS_Summary.Rows(vRowCounter)("Age") = Nothing
                'End If

                'If vSqlReader.IsDBNull(8) = False Then
                '    If vSqlReader(8) = "P" Then
                Grd_Summary.Rows(vRowCounter).Appearance.BackColor = Color.Wheat
                '    Else
                'Grd_Summary.Rows(vRowCounter).Appearance.BackColor = Color.Cornsilk
                '    End If
                'End If
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()

            'Dim vRow As UltraDataRow
            'Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
            'For Each vRow In DTS_Summary.Rows
            '    If cBase.fCount_Rec(" From Sales_Invoice_Details Where SI_Code = '" & vRow("Code") & "'") > 0 Then
            '        sQuerySummaryDetails(vRow, vChildBand)
            '    End If
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
            vsqlCommand.CommandText = _
            " Select Sales_Invoice_Details.Ser,       " & _
            "        Sales_Invoice_Details.Item_Code, " & _
            "        Sales_Invoice_Details.Item_Ser,  " & _
            "        Items.DescA as Item_Desc,           " & _
            "        Pack_Unit.DescA,                    " & _
            "        Sales_Invoice_Details.Quantity,    " & _
            "        Sales_Invoice_Details.Price,       " & _
            "        Sales_Invoice_Details.Addition,    " & _
            "        Sales_Invoice_Details.Deduction,    " & _
            "        Sales_Invoice_Details.LCost        " & _
            "        From   Sales_Invoice_Details Inner Join  Items  " & _
            "        On     Sales_Invoice_Details.Item_Code = Items.Code  " & _
            "        Inner Join Pack_Unit                      " & _
            "        On     Pack_Unit.Code = Sales_Invoice_Details.PU_Code " & _
            " Where SI_Code = '" & pRow("Code") & "'" & _
            " Order By Ser       "

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
                If vSqlReader.IsDBNull(4) = False Then
                    vChildRows(vRowCounter)("PU_Desc") = Trim(vSqlReader(4))
                Else
                    vChildRows(vRowCounter)("PU_Desc") = Nothing
                End If
                vChildRows(vRowCounter)("Quantity") = Trim(vSqlReader(5))
                vChildRows(vRowCounter)("Price") = Trim(vSqlReader(6))
                If vSqlReader.IsDBNull(7) = False Then
                    vChildRows(vRowCounter)("Addition") = Trim(vSqlReader(7))
                Else
                    vChildRows(vRowCounter)("Addition") = Nothing
                End If
                If vSqlReader.IsDBNull(8) = False Then
                    vChildRows(vRowCounter)("Deduction") = Trim(vSqlReader(8))
                Else
                    vChildRows(vRowCounter)("Deduction") = Nothing
                End If
                If vSqlReader.IsDBNull(9) = False Then
                    vChildRows(vRowCounter)("LCost") = Trim(vSqlReader(9))
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
            'cException.sHandleException(ex.Message, Me.Name, "sQuerySummaryDetails")
        End Try
    End Sub
    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick
        If Grd_Summary.Selected.Rows.Count > 0 Then
            If Not Grd_Summary.ActiveRow.ParentRow Is Nothing Then
                sQuery(pItemCode:=Grd_Summary.ActiveRow.ParentRow.Cells("Code").Value)
            Else
                sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
            End If
        Else
            sNewRecord()
        End If
        Tab_Main.Tabs("Tab_Details").Selected = True
    End Sub
    Private Sub Txt_AllFilters_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged, TXT_SummaryDate.ValueChanged
        sQuerySummaryMain(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
    End Sub
    Private Sub Grd_Summary_BeforeRowExpanded(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CancelableRowEventArgs) Handles Grd_Summary.BeforeRowExpanded
        Dim vChildBand As UltraDataBand = DTS_Summary.Band.ChildBands(0)
        Dim vRow As UltraDataRow = DTS_Summary.Rows(e.Row.Index)
        sQuerySummaryDetails(vRow, vChildBand)
    End Sub
#End Region

    Private Sub Btn_Add_CarCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Add_CarCompany.Click
        'Dim vNewItem As New Frm_AddNewCountry
        'vNewItem.ShowDialog()

        'sLoad_CarsCompanies()
    End Sub

    Private Sub Txt_BirthDate_BeforeExitEditMode(ByVal sender As System.Object, ByVal e As Infragistics.Win.BeforeExitEditModeEventArgs) Handles Txt_BirthDate.BeforeExitEditMode
        If Not Txt_BirthDate.Value = Nothing Then
            Txt_Age.Text = Year(Now) - Year(Txt_BirthDate.Value)
        Else
            Txt_Age.Text = ""
        End If
    End Sub

    Private Sub Txt_WifeBirthDate_BeforeExitEditMode(ByVal sender As System.Object, ByVal e As Infragistics.Win.BeforeExitEditModeEventArgs) Handles Txt_WifeBirthDate.BeforeExitEditMode
        If Not Txt_WifeBirthDate.Value = Nothing Then
            Txt_WifeAge.Text = Year(Now) - Year(Txt_WifeBirthDate.Value)
        Else
            Txt_WifeAge.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If vMasterBlock = "NI" Then
                Return
            End If

            If vMasterBlock = "I" Then
                If fSaveAll(False) = False Then
                    Return
                End If
            End If

            OpenFileDialog1.Filter = "JPEGS|*.jpg|GIFS|*.gif|Bitmaps|*.bmp|all files|*.*"
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.ShowDialog()
            If Not OpenFileDialog1.FileName = "" Then
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                'cControls.sSaveImage(Image.FromFile(OpenFileDialog1.FileName), "Company")
                sSave_MainPicture()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function sSave_MainPicture()
        Try
            If Not IsDBNull(UltraPictureBox1.Image) Then
                Dim vSqlString As String
                Dim ms As New System.IO.MemoryStream
                Dim arrPicture() As Byte

                vSqlString = " Update Members Set Picture = (@image) Where Code = '" & Trim(Txt_Code.Text) & "'"

                Dim vMyCommand As New SqlCommand(vSqlString, cControls.vSqlConn)

                PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                'If ms.Length > MAX_IMAGE_SIZE Then
                '    MsgBox("image trop grosse")
                'End If
                arrPicture = ms.GetBuffer()
                ms.Flush()
                vMyCommand.Parameters.Add("@image", SqlDbType.Image).Value = arrPicture

                cControls.vSqlConn.Open()
                vMyCommand.ExecuteNonQuery()
                cControls.vSqlConn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try
    End Function
End Class