Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports System.Drawing.Printing

Public Class Frm_RoomCleaning_A

#Region "Declaration"
    Dim vMasterBlock As String = "NI"
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSqlStatment(0) As String
    Dim vFocus As String = "Master"
    Dim vSortedList As New SortedList
    Dim vClear As Boolean
    Dim vGetShiftNumber As String
    Dim vShiftNum_Status As String
    Dim vProviderCode As String
    Dim vLog_Code As Int32
#End Region

#Region "Form Level"

#Region "My Form"
    Public Sub New()

        InitializeComponent()

    End Sub
    Public Sub New(ByVal pProviderCode As String)

        InitializeComponent()

        vProviderCode = pProviderCode

    End Sub
    Private Sub FRM_Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TXT_FromSummaryDate.Value = Now.AddDays(-30)
        Txt_ToSummaryDate.Value = Now

        sLoad_CleaningTypes()
        sLoad_Supervisor()

        vMasterBlock = "NI"

        Txt_EmpDesc.Text = vUsrName
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
    Private Sub FRM_Users_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm

        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "ÇáÊÝÇÕíá")
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "")
        End If

    End Sub
    Private Sub FRM_Users_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
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
        Handles Txt_RoomType.ValueChanged, Txt_CleaningType.ValueChanged, Txt_Supervisor.ValueChanged,
                Txt_CleaningDate.ValueChanged, Txt_Remarks.ValueChanged

        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If
    End Sub
    Private Sub Frm_RoomCleaning_A_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm
        sHide_ToolbarMain_Tools()
    End Sub
    Private Sub sHide_ToolbarMain_Tools()
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_New").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Save").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Delete").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Print").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_NextRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_PreviousRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_LastRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_FirstRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_CloseWindow").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_GotoRecord").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_ChangeUser").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Themes").SharedProps.Visible = True
    End Sub
#End Region

#Region "Tab Management"
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

    Private Sub Tab_Main_SelectedTabChanged(sender As Object, e As UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        If Tab_Main.Tabs("Tab_Details").Selected Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "")
            Btn_Back.Visible = True

        ElseIf Tab_Main.Tabs("Tab_Summary").Selected Then
            vcFrmLevel.vParentFrm = Me.ParentForm
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", False, False, "")
            Btn_Back.Visible = False
            sQuerySummary()
        End If
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Btn_Back_Click(sender As Object, e As EventArgs) Handles Btn_Back.Click
        Tab_Main.Tabs("Tab_Summary").Selected = True
    End Sub

#End Region

#Region "DataBase"

#Region " Save                                                                           "
    Private Function fIfsaveNeeded() As Boolean
        If vMasterBlock = "I" Or vMasterBlock = "U" Then
            Return True
        End If

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        If vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Save").SharedProps.Enabled = False Then
            Return True
        End If

        If fIfsaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        vLog_Code = 0

        If pAskMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                If fValidateMain() Then
                    sSaveMain()
                Else
                    Return False
                End If

                If fValidateDetails() Then
                    sSaveDetails()
                Else
                    Return False
                End If
            Else
                vMasterBlock = "NI"
                DTS_Details.Rows.Clear()
                Return True
            End If
        Else
            If fValidateMain() Then
                sSaveMain()
            Else
                Return False
            End If

            If fValidateDetails() Then
                sSaveDetails()
            Else
                Return False
            End If
        End If

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            sSetFlagsUpdate()
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            Return True
        End If
    End Function
    Private Sub sSetFlagsUpdate()

        vMasterBlock = "N"
        sQueryDetails()

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
                    If vFetchRec > cControls.fCount_Rec(" From  HK_Cleaning_Rooms " &
                                                        " Where Company_Code = " & vCompanyCode) Then

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
            vFetchRec = cControls.fCount_Rec(" From HK_Cleaning_Rooms " &
                                             " Where Company_Code = " & vCompanyCode)
        End If

        Dim vFetchCondition As String
        If pItemCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And MyCleaningRooms.Code = '" & Trim(pItemCode) & "'"
        End If

        Try
            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText =
            " With MyCleaningRooms as                                             " &
            "( Select HK_Cleaning_Rooms.Code,                                        " &
            "         HK_Cleaning_Rooms.Room_Code,                                   " &
            "         Rooms.DescA AS Room_Num,                                    " &
            "         HK_Cleaning_Rooms.CleanType_Code,                              " &
            "         HK_Cleaning_Rooms.Supervisor_Code,                             " &
            "         HK_Cleaning_Rooms.Remarks,                                     " &
            "         HK_Cleaning_Rooms.Company_Code,                                " &
            "         Rooms_Types.DescA AS Room_Type,                              " &
            "         Rooms_Types.Code AS Room_Type_Code,                          " &
            "         ROW_Number() Over (Order By HK_Cleaning_Rooms.Code) as RecPos  " &
            "                                                                     " &
            " From HK_Cleaning_Rooms Inner Join Rooms                                " &
            " On   HK_Cleaning_Rooms.Room_Code = Rooms.Code                          " &
            " And  HK_Cleaning_Rooms.Company_Code = Rooms.Company_Code               " &
            "                                                                     " &
            " LEFT JOIN Rooms_Types                                                " &
            " ON Rooms.Room_Type = Rooms_Types.Code                                " &
            " And  Rooms.Company_Code = Rooms_Types.Company_Code )                 " &
            "                                                                     " &
            " Select * From MyCleaningRooms                                       " &
            " Where  1 = 1                                                        " &
            " And    MyCleaningRooms.Company_Code = " & vCompanyCode &
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If IsDBNull(vSqlReader("RecPos")) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader("RecPos"))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader("RecPos"))

                'Code
                Txt_Code.Text = Trim(vSqlReader("Code")).PadLeft(6, "0")

                'Room_Code
                If IsDBNull(vSqlReader("Room_Code")) = False Then
                    Txt_RoomNum.Tag = vSqlReader("Room_Code")
                Else
                    Txt_RoomNum.Tag = ""
                End If

                'Room_Num
                If IsDBNull(vSqlReader("Room_Num")) = False Then
                    Txt_RoomNum.Text = vSqlReader("Room_Num")
                Else
                    Txt_RoomNum.Text = ""
                End If

                'Room_Type
                If IsDBNull(vSqlReader("Room_Type")) = False Then
                    Txt_RoomType.Text = vSqlReader("Room_Type")
                Else
                    Txt_RoomType.Text = ""
                End If

                'Room_Type_Code
                If IsDBNull(vSqlReader("Room_Type_Code")) = False Then
                    Txt_RoomType.Tag = vSqlReader("Room_Type_Code")
                Else
                    Txt_RoomType.Tag = ""
                End If

                'CleanType_Code
                If IsDBNull(vSqlReader("CleanType_Code")) = False Then
                    Txt_CleaningType.Value = vSqlReader("CleanType_Code")
                Else
                    Txt_CleaningType.SelectedIndex = -1
                End If

                'Supervisor_Code
                If IsDBNull(vSqlReader("Supervisor_Code")) = False Then
                    Txt_Supervisor.Value = vSqlReader("Supervisor_Code")
                Else
                    Txt_Supervisor.SelectedIndex = -1
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    Txt_Remarks.Text = vSqlReader("Remarks")
                Else
                    Txt_Remarks.Text = ""
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            sQueryDetails()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try
        vMasterBlock = "N"
    End Sub

#End Region

#Region " Delete                                                                         "
    Public Sub sDelete()

        If vMasterBlock = "I" Or vMasterBlock = "NI" Then
            Dim vRow As UltraGridRow

            sNewRecord()
            Exit Sub
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                sEmptySqlStatmentArray()

                Dim vSqlstring As String

                'Here I Insert Into Delete Log Table..

                vSqlstring =
                " Delete From HK_Cleaning_Rooms_Details " &
                " Where  CL_Code = '" & Txt_Code.Text & "' " &
                " And    Company_Code = " & vCompanyCode &
                "                                           " &
                " Delete From HK_Cleaning_Rooms " &
                " Where  Code = '" & Txt_Code.Text & "'" &
                " And    Company_Code = " & vCompanyCode

                sFillSqlStatmentArray(vSqlstring)

                'vSqlstring = " Insert Into Sales_Invoices  (        Code,                  Invoice_Code,                    DescA,                 TDate,                   Emp_Code,                          Provider_Code,              SalesMan_Code,                 Cur_Code,                 Box_Code,       CostCenter_Code,    SalesType_Code,               Remarks,            Status,    Company_Code,             PayType,                   Payed)" & _
                '             "                     Values  ('" & vLog_Code & "', '" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', " & vDate & ", '" & Trim(Txt_EmpCode.Text) & "', '" & Trim(Txt_CustomerCode.Text) & "', " & vSalesMan & ", '" & Trim(Txt_CurrencyCode.Text) & "', " & vBox & ", " & vCostCenter & ", " & vSalesType & ", '" & Trim(Txt_Remarks.Text) & "',   'O', " & vCompanyCode & ", '" & Txt_PayType.Value & "', " & vPayed & ")"

                If cControls.fSendData(vSqlStatment, Me.Name) > 0 Then
                    'Here I Save the Log Data
                    'sEmptySqlStatmentArray()

                    'vSqlstring = " Select IsNull(Max(Code), 0) + 1 From  Employees_AllActions_Log " &
                    '             " Where  Company_Code = " & vCompanyCode

                    'vLog_Code = cControls.fReturnValue(vSqlstring, Me.Name)

                    'vSqlstring = " Insert Into Employees_AllActions_Log (      Code,             Emp_Code,          TDate,          Action_Desc,    Action_Type,      Invoice_Type,          Invoice_Code,                     Main_Object_Code,                    Main_Object_Desc,                       CostCenter_Code,                          Remarks,                    ComputerName,                     TValue,                  Company_Code )" &
                    '             "                             Values   (" & vLog_Code & ", '" & vUsrCode & "', GetDate(),      'ÇáÛÇÁ ÝÇÊæÑÉ ÈíÚ',   'D',             'SI',     '" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_CustomerCode.Text) & "', '" & Trim(Txt_CustomerDesc.Text) & "', '" & Trim(Txt_CostCenter_Code.Text) & "', '" & Trim(Txt_Remarks.Text) & "', '" & My.Computer.Name & "', " & Txt_NetAfter_AddDed.Value & ", " & vCompanyCode & " ) "

                    'sFillSqlStatmentArray(vSqlstring)

                    'Dim vRow As UltraGridRow
                    'For Each vRow In Grd_Items.Rows
                    '    vSqlstring = " Insert Into Employees_AllActions_Log_Details (     Log_Code,         Company_Code,                     Ser,                                  Item_Code,                                     Str_Code,                              Quantity,                                  Price,            Action_Type )  " &
                    '                 "                                      Values  (" & vLog_Code & ", " & vCompanyCode & ", " & vRow.Cells("Ser").Text & ", '" & Trim(vRow.Cells("Fixed_Code").Text) & "', '" & Trim(vRow.Cells("Str_Code").Text) & "', " & vRow.Cells("Quantity").Value & ", " & Trim(vRow.Cells("Price").Value) & ", 'ÇáÛÇÁ' )"

                    '    sFillSqlStatmentArray(vSqlstring)
                    'Next

                    'cControls.fSendData(vSqlStatment, Me.Name)

                    'Here we set Master Block = N to not let ask for save
                    vMasterBlock = "N"

                    sNewRecord()
                    vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                End If
            End If
        End If
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

        Txt_RoomNum.Clear()
        Txt_RoomNum.Tag = ""

        Txt_RoomType.Clear()
        Txt_RoomType.Tag = ""

        Txt_CleaningType.SelectedIndex = -1

        Txt_Supervisor.SelectedIndex = -1

        Txt_CleaningDate.Value = Now

        Txt_Remarks.Clear()

        Txt_EmpDesc.Text = vUsrName

        vMasterBlock = "NI"
        vFocus = "Master"
        vcFrmLevel.vRecPos = 0
        vcFrmLevel.vParentFrm.sPrintRec("")

        Txt_RoomNum.Select()

        DTS_Details.Rows.Clear()
    End Sub
    Private Sub sNewCode()
        Dim vSqlString As String
        vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From HK_Cleaning_Rooms " &
                     " Where  Company_Code = " & vCompanyCode

        Txt_Code.Text = cControls.fReturnValue(vSqlString, Me.Name).PadLeft(6, "0")
    End Sub

#End Region

#Region " sOpenLov                                                                       "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String)
        vLovReturn1 = ""
        VLovReturn2 = ""
        vLovReturn3 = ""

        If pTitle = "Rooms" Then
            Dim Frm_Lov As New FRM_LovRooms_A(pSqlStatment, "ÇáÛÑÝ")
            Frm_Lov.ShowDialog()
        ElseIf pTitle = "Emp" Then
            Dim Frm_Lov As New FRM_LovGeneral_A(pSqlStatment, "ÇáãæÙÝíä")
            Frm_Lov.ShowDialog()
        End If

        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            If pTitle = "Rooms" Then

                Txt_RoomNum.Tag = vLovReturn1
                Txt_RoomNum.Text = VLovReturn2
                Txt_RoomType.Text = vLovReturn3
                Txt_RoomType.Tag = vLovReturn4

            ElseIf pTitle = "Emp" Then
                If Grd_Details.ActiveRow.Cells("DML").Value = "NI" Then
                    Grd_Details.ActiveRow.Cells("DML").Value = "I"
                ElseIf Grd_Details.ActiveRow.Cells("DML").Value = "N" Then
                    Grd_Details.ActiveRow.Cells("DML").Value = "U"
                End If

                Grd_Details.ActiveRow.Cells("Emp_Code").Value = vLovReturn1
                Grd_Details.ActiveRow.Cells("Emp_Desc").Value = VLovReturn2

            End If
        End If
    End Sub

#End Region

#Region " Print                                                                          "

#End Region

#End Region

#Region " Master                                                                         "

#Region " DataBase                                                                       "

#Region " Save                                                                           "
    Private Function fValidateMain() As Boolean

        If Txt_RoomNum.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("183", Me)
            Txt_RoomNum.Select()
            Return False
        End If

        If Txt_CleaningType.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("184", Me)
            Txt_CleaningType.Select()
            Return False
        End If

        If Txt_Supervisor.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("186", Me)
            Txt_Supervisor.Select()
            Return False
        End If

        If Txt_CleaningDate.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("187", Me)
            Txt_CleaningDate.Select()
            Return False
        End If

        If Grd_Details.Rows.Count = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("52", Me)
            Return False
        End If

        Return True
    End Function
    Private Sub sSaveMain()

        Dim vSqlCommand As String = ""
        Dim vDate As String
        If Not Txt_CleaningDate.Value = Nothing Then
            vDate = "'" & Format(Txt_CleaningDate.Value, "MM-dd-yyyy HH:mm") & "'"
        Else
            vDate = "NULL"
        End If

        If vMasterBlock = "I" Then

            sNewCode()

            vSqlCommand = " Insert Into HK_Cleaning_Rooms (  Company_Code,      Code,         TDate,   User_Code,            Remarks,                CleanType_Code,            Supervisor_Code,        Room_Code,                                      Points  ) " &
                         $"                     Values ( {vCompanyCode}, {Txt_Code.Text}, {vDate}, {vUsrCode}, '{Trim(Txt_Remarks.Text)}',   {Txt_CleaningType.Value},  {Txt_Supervisor.Value}, {Txt_RoomNum.Tag},   dbo.fn_Calc_Clean_Room_Points({Txt_RoomType.Tag}, {Txt_CleaningType.Value}, {vCompanyCode}) ) "

            sFillSqlStatmentArray(vSqlCommand)

        ElseIf vMasterBlock = "U" Then

            vSqlCommand = " Update HK_Cleaning_Rooms                               " &
                         $" Set   Room_Code       = '{Txt_RoomNum.Tag}',        " &
                         $"       CleanType_Code  = '{Txt_CleaningType.Value}', " &
                         $"       Supervisor_Code = '{Txt_Supervisor.Value}',   " &
                         $"       User_Code       = '{vUsrCode}',               " &
                         $"       Remarks         = '{Trim(Txt_Remarks.Text)}', " &
                         $"       Points          = dbo.fn_Calc_Clean_Room_Points({Txt_RoomType.Tag}, {Txt_CleaningType.Value}, {vCompanyCode}) " &
                         $" Where Code            = '{Txt_Code.Text}'           " &
                         $" And   Company_Code    = '{vCompanyCode}'            "

            sFillSqlStatmentArray(vSqlCommand)
        End If
    End Sub

#End Region

#End Region

#Region " Form Level                                                                     "
    Private Sub Txt_SearchEmployee_EditorButtonClick(sender As Object, e As UltraWinEditors.EditorButtonEventArgs) Handles _
            Txt_LovEmployee.EditorButtonClick, Txt_RoomNum.EditorButtonClick

        If sender.name = "Txt_LovEmployee" Then
            Dim vSqlString As String = " SELECT                                                " &
                                   "     Employees.Code,                                   " &
                                   "     Employees.DescA                                   " &
                                   " FROM Employees                                        " &
                                   " LEFT JOIN Departments                                 " &
                                   " ON Employees.Department_Code = Departments.Code       " &
                                   " AND Employees.Company_Code = Departments.Company_Code " &
                                   " WHERE Departments.Sign = 'HK'                         " &
                                   " AND Employees.Is_Supervisor = 0                       " &
                                  $" And Employees.Company_Code = {vCompanyCode}           "

            sOpenLov(vSqlString, "Emp")

        ElseIf sender.name = "Txt_RoomNum" Then
            Dim vSqlString As String = " SELECT                                       " &
                                   "     Rooms.Code AS Room_Code,                     " &
                                   "     Rooms.DescA AS Room_Desc,                    " &
                                   "     Rooms_Types.DescA AS Room_Type,               " &
                                   "     Rooms_Types.Code AS Room_Type_Code            " &
                                   " FROM Rooms                                       " &
                                   " LEFT JOIN Rooms_Types                             " &
                                   " ON Rooms.Room_Type = Rooms_Types.Code             " &
                                   " AND Rooms.Company_Code = Rooms_Types.Company_Code "

            sOpenLov(vSqlString, "Rooms")
        End If



    End Sub
    Private Sub Txt_All_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Txt_RoomNum.Enter, Txt_Remarks.Enter, Txt_EmpDesc.Enter, Txt_Code.Enter, Txt_CleaningDate.Enter

        vFocus = "Master"
    End Sub

    Private Sub Txt_RoomNum_EditorButtonClick(sender As Object, e As UltraWinEditors.EditorButtonEventArgs) Handles Txt_RoomNum.EditorButtonClick



    End Sub

    Private Sub sLoad_CleaningTypes()
        Try
            Dim vsqlCommand As New SqlCommand

            vsqlCommand.CommandText =
            " Select Code, Desc" & vLang & " From HK_Cleaning_Types " &
            " Where Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_CleaningType.Items.Clear()

            Do While vSqlReader.Read
                Txt_CleaningType.Items.Add(vSqlReader("Code"), vSqlReader("Desc" & vLang))
            Loop
            cControls.vSqlConn.Close()
            Txt_CleaningType.SelectedIndex = -1

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sLoad_Supervisor()
        Try
            Dim vsqlCommand As New SqlCommand

            vsqlCommand.CommandText = " SELECT                                          " &
                                       "    Employees.Code As Code,                      " &
                                       "    Employees.DescA As DescA                      " &
                                       " FROM Employees                                  " &
                                       " LEFT JOIN Departments                           " &
                                       " ON Employees.Department_Code = Departments.Code " &
                                      $" WHERE Employees.Company_Code = {vCompanyCode}   " &
                                       " AND Departments.Sign = 'HK'                     " &
                                       " AND Employees.Is_Supervisor = 1 "


            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_Supervisor.Items.Clear()

            Do While vSqlReader.Read
                Txt_Supervisor.Items.Add(vSqlReader("Code"), vSqlReader("Desc" & vLang))
            Loop
            cControls.vSqlConn.Close()
            Txt_Supervisor.SelectedIndex = -1

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Txt_RoomNum_KeyUp(sender As Object, e As KeyEventArgs) Handles Txt_RoomNum.KeyUp
        Try

            If e.KeyData = Keys.Enter Then

                If Not Trim(Txt_RoomNum.Text) = "" Then

                    Dim vSqlString As String =
                        " FROM  Rooms                              " &
                       $" WHERE Company_Code = {vCompanyCode}      " &
                       $" And   DescA = '{Trim(Txt_RoomNum.Text)}' "

                    If cControls.fCount_Rec(vSqlString) = 0 Then

                        vcFrmLevel.vParentFrm.sForwardMessage("8", Me)

                        Txt_RoomNum.Text = ""
                        Txt_RoomType.Text = ""
                        Txt_RoomType.Tag = ""

                    Else

                        sLoad_RoomInfo()

                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoad_RoomInfo()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand

            vsqlCommand.CommandText = " SELECT                                             " &
                                      "       Rooms.Code AS Room_Code,                     " &
                                      "       Rooms.DescA AS Room_Num,                     " &
                                      "       Rooms_Types.DescA AS Room_Type,               " &
                                      "       Rooms_Types.Code AS Room_Type_Code            " &
                                      " FROM  Rooms                                        " &
                                      " LEFT  JOIN Rooms_Types                              " &
                                      " ON    Rooms.Room_Type = Rooms_Types.Code            " &
                                      " AND   Rooms.Company_Code = Rooms_Types.Company_Code " &
                                     $" WHERE Rooms.DescA = {Trim(Txt_RoomNum.Text)}       "

            vsqlCommand.Connection = cControls.vSqlConn

            cControls.vSqlConn.Open()

            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

            Do While vSqlReader.Read
                Txt_RoomNum.Tag = vSqlReader("Room_Code")
                Txt_RoomNum.Text = vSqlReader("Room_Num")
                Txt_RoomType.Text = vSqlReader("Room_Type")
                Txt_RoomType.Tag = vSqlReader("Room_Type_Code")
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#End Region

#Region " Details                                                                        "

#Region " Items                                                                          "

#Region " DataBase                                                                       "

#Region " Save                                                                           "
    Private Function fValidateDetails() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_Details.Rows
            If IsDBNull(vRow.Cells("Emp_Code").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("3", Me)
                vRow.Cells("Emp_Code").Selected = True
                Return False
            End If

            If IsDBNull(vRow.Cells("Emp_Desc").Value) Then
                vcFrmLevel.vParentFrm.sForwardMessage("11", Me)
                vRow.Cells("Emp_Desc").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveDetails()

        Dim vRow As UltraGridRow
        Dim vCounter As Integer = 0

        Grd_Details.UpdateData()

        For Each vRow In Grd_Details.Rows
            Dim vSqlString, vGetSerial As String

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Ser), 0) + 1 From HK_Cleaning_Rooms_Details " &
                             " Where  Cl_Code = " & Txt_Code.Text &
                             " And    Company_Code = " & vCompanyCode

                vGetSerial = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into HK_Cleaning_Rooms_Details (     Ser,       Company_Code,      Cl_Code,                  Emp_Code,                       Remarks          ) " &
                            $"                             Values ( {vGetSerial}, {vCompanyCode}, {Txt_Code.Text}, {vRow.Cells("Emp_Code").Text}, '{vRow.Cells("Remarks").Text}' ) "

                sFillSqlStatmentArray(vSqlString)
                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then
                vSqlString = " Update HK_Cleaning_Rooms_Details                                  " &
                            $" Set    Emp_Code               = {vRow.Cells("Emp_Code").Text}, " &
                            $"        Remarks                = '{vRow.Cells("Remarks").Text}'   " &
                            $" Where  Ser                    = {vRow.Cells("Ser").Text}       " &
                            $" And    Company_Code           = {vCompanyCode}                 " &
                            $" And    Cl_Code                = {Txt_Code.Text}                "
                sFillSqlStatmentArray(vSqlString)
            End If
        Next

    End Sub

#End Region

#Region " Query                                                                          "
    Private Sub sQueryDetails()
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand
            Dim vRowCounter As Integer
            vsqlCommand.CommandText =
            " Select HK_Cleaning_Rooms_Details.Ser,                          " &
            "        HK_Cleaning_Rooms_Details.Remarks,                      " &
            "        HK_Cleaning_Rooms_Details.Emp_Code,                     " &
            "        Employees.DescA                                      " &
            " From   HK_Cleaning_Rooms_Details Inner Join Employees          " &
            " On     HK_Cleaning_Rooms_Details.Emp_Code = Employees.Code     " &
            " Where  HK_Cleaning_Rooms_Details.Cl_Code =     " & Txt_Code.Text &
            " And    HK_Cleaning_Rooms_Details.Company_Code = " & vCompanyCode &
            " Order  By Ser "

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Details.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Details.Rows.SetCount(vRowCounter + 1)
                DTS_Details.Rows(vRowCounter)("Ser") = Trim(vSqlReader("Ser"))
                DTS_Details.Rows(vRowCounter)("Emp_Code") = Trim(vSqlReader("Emp_Code"))
                DTS_Details.Rows(vRowCounter)("Emp_Desc") = Trim(vSqlReader("DescA"))
                DTS_Details.Rows(vRowCounter)("Remarks") = Trim(vSqlReader("Remarks"))
                DTS_Details.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Details.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#End Region

#Region " Form Level                                                                     "
    Private Sub Grd_Details_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Details.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If

        Grd_Details.UpdateData()
    End Sub
    Private Sub Grd_Details_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Details.Enter
        vFocus = "Details"
    End Sub

    Private Sub Grd_Details_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Grd_Details.KeyPress
        If Grd_Details.ActiveCell IsNot Nothing AndAlso Grd_Details.ActiveCell.Column.Key = "Emp_Code" Then
            ' Allow digits, control characters (like Backspace), and one dot
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True ' Cancel the keypress
            End If
        End If
    End Sub

    Private Sub Grd_Details_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Details.KeyUp
        Try
            Grd_Details.UpdateData()

            If e.KeyData = Keys.Enter Then
                If Grd_Details.ActiveRow.Cells("Emp_Code").Activated = True Then
                    If IsDBNull(Grd_Details.ActiveRow.Cells("Emp_Code").Value) = False Then
                        If Grd_Details.ActiveRow.Cells("Emp_Code").Text <> "" Then

                            Dim vSqlString As String =
                                " FROM Employees                                                       " &
                                " LEFT JOIN Departments                                                " &
                                " ON Employees.Department_Code = Departments.Code                      " &
                                " And Employees.Company_Code = Departments.Company_Code                " &
                                " WHERE Departments.Sign = 'HK'                                        " &
                                " AND Employees.Is_Supervisor = 0                                      " &
                               $" And Employees.Company_Code = {vCompanyCode}                          " &
                               $" And Employees.Code = {Grd_Details.ActiveRow.Cells("Emp_Code").Value} "

                            If cControls.fCount_Rec(vSqlString) = 0 Then

                                vcFrmLevel.vParentFrm.sForwardMessage("8", Me)
                                Grd_Details.ActiveRow.Cells("Emp_Desc").Value = DBNull.Value

                            Else

                                sLoad_EmployeeInfo(Grd_Details.ActiveRow)
                                Do
                                    Grd_Details.PerformAction(UltraGridAction.PrevCell)
                                Loop Until Grd_Details.DisplayLayout.Bands(0).Columns(Grd_Details.ActiveCell.Column.Index).CellActivation = Activation.AllowEdit

                                Grd_Details.PerformAction(UltraGridAction.EnterEditMode)
                            End If
                        End If
                    End If
                ElseIf Grd_Details.ActiveRow.Cells("Remarks").Activated = True Then
                    Grd_Details.PerformAction(UltraGridAction.NextRow)
                    Grd_Details.ActiveRow.Cells("Emp_Code").Selected = True
                    Grd_Details.ActiveRow.Cells("Emp_Code").Activate()
                    Grd_Details.PerformAction(UltraGridAction.EnterEditMode)
                Else
                    sGet_First_AllowEdit_Cell(Grd_Details)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sLoad_EmployeeInfo(ByVal pRow As UltraGridRow)
        Try
            Dim vsqlCommand As New SqlClient.SqlCommand

            vsqlCommand.CommandText = " SELECT                                                " &
                                      "     Employees.Code,                                   " &
                                      "     Employees.DescA                                   " &
                                      " FROM Employees                                        " &
                                      " LEFT JOIN Departments                                 " &
                                      " ON Employees.Department_Code = Departments.Code       " &
                                      " And Employees.Company_Code = Departments.Company_Code " &
                                      " WHERE Departments.Sign = 'HK'                         " &
                                      " AND Employees.Is_Supervisor = 0                       " &
                                     $" And Employees.Company_Code = {vCompanyCode}           " &
                                     $" And Employees.Code = {Grd_Details.ActiveRow.Cells("Emp_Code").Value} "


            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vsqlCommand.ExecuteReader

            Do While vSqlReader.Read
                pRow.Cells("Emp_Code").Value = vSqlReader("Code")
                pRow.Cells("Emp_Desc").Value = vSqlReader("DescA")
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Grd_Details_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) _
        Handles Grd_Details.ClickCellButton

        If sender.ActiveRow.Cells("Delete").Activated Then

            If sender.ActiveRow.Cells("DML").Value = "I" Or sender.ActiveRow.Cells("DML").Value = "NI" Then

                sender.ActiveRow.Delete(False)

            ElseIf sender.ActiveRow.Cells("DML").Value = "N" Or sender.ActiveRow.Cells("DML").Value = "U" Then

                If vcFrmLevel.vParentFrm.sForwardMessage("133", Me) = MsgBoxResult.Yes Then

                    Dim vSqlstring As String =
                    " Delete From HK_Cleaning_Rooms_Details " &
                    " Where  1 = 1 " &
                    " And    Company_Code = " & vCompanyCode &
                    " And    Ser        = " & sender.ActiveRow.Cells("Ser").Value

                    If cControls.fSendData(vSqlstring, Me.Name) > 0 Then

                        sender.ActiveRow.Delete(False)

                    End If

                End If
            End If
        End If
    End Sub

#End Region

#End Region

#End Region

#Region " Summary                                                                               "

#Region " DataBase                                                                              "

#Region " Query                                                                                 "
    Private Sub sQuerySummary()
        If DTS_Summary.Band.Columns.Count = 0 Then
            Exit Sub
        End If

        Dim vFromDate, vToDate_PlusOneDay As String

        If Not TXT_FromSummaryDate.Value Is Nothing Then
            vFromDate = "'" & Format(TXT_FromSummaryDate.Value, "MM-dd-yyyy") & "'"
        Else
            vFromDate = "NULL"
        End If

        vToDate_PlusOneDay = " '" & Format(Txt_ToSummaryDate.DateTime.AddDays(1), "MM-dd-yyyy") & "' "

        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText =
                                    " SELECT                                                                  " &
                                    "       HK_Cleaning_Rooms.Code AS Code,                                      " &
                                    "       Rooms.DescA AS Room_Num,                                          " &
                                    "       HK_Cleaning_Types.DescA AS Cleaning_Type,                            " &
                                    "       Employees.DescA AS Supervisor,                                    " &
                                    "       HK_Cleaning_Rooms.TDate AS Cleaning_Date,                            " &
                                    "       HK_Cleaning_Rooms.Remarks AS Room_Remarks                            " &
                                    "                                                                         " &
                                    " FROM  HK_Cleaning_Rooms                                                    " &
                                    "                                                                         " &
                                    " LEFT  JOIN Rooms                                                        " &
                                    " ON    HK_Cleaning_Rooms.Room_Code = Rooms.Code                             " &
                                    " AND   HK_Cleaning_Rooms.Company_Code = Rooms.Company_Code                  " &
                                    "                                                                         " &
                                    " LEFT  JOIN HK_Cleaning_Types                                               " &
                                    " ON    HK_Cleaning_Rooms.CleanType_Code = HK_Cleaning_Types.Code               " &
                                    " AND   HK_Cleaning_Rooms.Company_Code = HK_Cleaning_Types.Company_Code         " &
                                    "                                                                         " &
                                    " LEFT  JOIN Employees                                                    " &
                                    " ON    HK_Cleaning_Rooms.Supervisor_Code = Employees.Code                   " &
                                    " AND   HK_Cleaning_Rooms.Company_Code = Employees.Company_Code              " &
                                    "                                                                         " &
                                   $" WHERE HK_Cleaning_Rooms.Company_Code = {vCompanyCode}                      " &
                                   $" AND   HK_Cleaning_Rooms.TDate BETWEEN {vFromDate} AND {vToDate_PlusOneDay} "


            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn

            cControls.vSqlConn.Open()

            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader

            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read

                DTS_Summary.Rows.SetCount(vRowCounter + 1)

                'Code
                DTS_Summary.Rows(vRowCounter)("Code") = CStr(vSqlReader("Code")).PadLeft(6, "0")

                'Room_Num
                If IsDBNull(vSqlReader("Room_Num")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Room_Num") = vSqlReader("Room_Num")
                Else
                    DTS_Summary.Rows(vRowCounter)("Room_Num") = Nothing
                End If

                'Cleaning_Type
                If IsDBNull(vSqlReader("Cleaning_Type")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Cleaning_Type") = vSqlReader("Cleaning_Type")
                Else
                    DTS_Summary.Rows(vRowCounter)("Cleaning_Type") = Nothing
                End If

                'Supervisor
                If IsDBNull(vSqlReader("Supervisor")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Supervisor") = vSqlReader("Supervisor")
                Else
                    DTS_Summary.Rows(vRowCounter)("Supervisor") = Nothing
                End If

                'Cleaning_Date
                If IsDBNull(vSqlReader("Cleaning_Date")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Cleaning_Date") = vSqlReader("Cleaning_Date")
                Else
                    DTS_Summary.Rows(vRowCounter)("Cleaning_Date") = Nothing
                End If

                'Room_Remarks
                If IsDBNull(vSqlReader("Room_Remarks")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Room_Remarks") = vSqlReader("Room_Remarks")
                Else
                    DTS_Summary.Rows(vRowCounter)("Room_Remarks") = Nothing
                End If

                'DML
                DTS_Summary.Rows(vRowCounter)("DML") = "N"

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#End Region

#Region " Form Level                                                                            "

    Private Sub TXT_FromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles TXT_FromSummaryDate.ValueChanged, Txt_ToSummaryDate.ValueChanged

        sQuerySummary()
    End Sub
    Private Sub Grd_Summary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Summary.DoubleClick
        If Grd_Summary.ActiveRow.IsGroupByRow Then
            Exit Sub
        End If

        If Grd_Summary.ActiveRow.Band.Index <> 0 Then
            Exit Sub
        End If

        If Grd_Summary.Selected.Rows.Count > 0 Then
            sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
        Else
            sNewRecord()
        End If

        Tab_Main.Tabs("Tab_Details").Selected = True
    End Sub
#End Region

#End Region

End Class