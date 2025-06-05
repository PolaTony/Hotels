Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinToolbars
Imports Infragistics.Win.AppStyling
Imports System.Deployment.Application
Imports DevComponents.DotNetBar.Controls
Imports System.Drawing.Printing
Imports Infragistics.Win.UltraWinExplorerBar
Imports System.Drawing.Text
Imports System.IO
Imports System.Text

Public Class MDIForm

#Region " Variables Declaration                                                  "
    Dim vCounter As Integer = 0
    Dim vSettings As Settings
    Dim vChangeUser As String = "N"
    Dim vMousePosition As Integer
    Dim vPreviousPosition As Integer = 0
    Dim vNotActiveUser As Integer = 0
    Dim vUserStatus As String
#End Region

#Region " Message Area Manager                                                   "
    Public Function sForwardMessage(ByVal pMsgNo As String, Optional ByVal vObj As Object = "") As MsgBoxResult
        'If pTimer = True Then

        Dim clr As eDesktopAlertColor
        Dim vType As String = cControls.fReturnValue(" Select Mode From Messages Where Code = '" & pMsgNo & "'", Me.Name)
        Dim vMessage As String = cControls.fReturnValue(" Select Desc" & vLang & " From Messages Where Code = '" & pMsgNo & "'", Me.Name)
        If Trim(vType) = "WRN" Or Trim(vType) = "CRT" Then
            StsBar_Main.Panels("Msg_Text").Appearance.ForeColor = Color.Red
            Txt_Message.Appearance.ForeColor = Color.Red

            clr = eDesktopAlertColor.Red

            'UltraDesktopAlert1.TextAppearance.FontData.SizeInPoints = 13
            'UltraDesktopAlert1.TextAppearance.ForeColor = Color.Red
        ElseIf Trim(vType) = "DON" Then
            StsBar_Main.Panels("Msg_Text").Appearance.ForeColor = Color.Blue
            Txt_Message.Appearance.ForeColor = Color.Blue

            clr = eDesktopAlertColor.[Default]

            'UltraDesktopAlert1.TextAppearance.FontData.SizeInPoints = 13
            'UltraDesktopAlert1.TextAppearance.ForeColor = Color.Blue
        ElseIf Trim(vType) = "MsgOkErr" Then
            MessageBox.Show(vMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        ElseIf Trim(vType) = "MsgYesNoQus" Then
            UltraMessageBoxManager1.ContentAreaAppearance.BackColor2 = Color.Yellow

            If UltraMessageBoxManager1.Show(vMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Return MsgBoxResult.Yes
            Else
                Return MsgBoxResult.No
            End If
            'If MessageBox.Show(vMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '    Return MsgBoxResult.Yes
            'Else
            '    Return MsgBoxResult.No
            'End If
        End If

        'Txt_Message.Text = vMessage

        'Txt_Message.Visible = True

        'vCounter = 20

        'Txt_Message.Appearance.AlphaLevel = 250

        'Notification_Timer.Enabled = True

        Dim MsgIcon As String = ""
        Dim Duration_Sec As Int16 = 4

        DesktopAlert.Show(vMessage, MsgIcon, 0, Color.Empty, clr, eAlertPosition.BottomLeft, Duration_Sec, 0, Nothing)

        'End If
    End Function

    Public Enum enumMessageType
        Critical = 1
        Warning = 2
        Done = 3
        Saved = 4
        Assistant = 5
        Empty = 6
    End Enum

    Public Sub sSendMsgToStatusBar(ByVal pMsg As String, Optional ByVal pTimer As Boolean = True, Optional ByVal pType As enumMessageType = enumMessageType.Warning)
        If pTimer = True Then
            If pType = enumMessageType.Warning Or pType = enumMessageType.Critical Then
                StsBar_Main.Panels("Msg_Text").Appearance.ForeColor = Color.Red
            ElseIf pType = enumMessageType.Saved Or pType = enumMessageType.Done Then
                StsBar_Main.Panels("Msg_Text").Appearance.ForeColor = Color.Blue
            End If
            StsBar_Main.Panels("Msg_Text").Text = pMsg
            Msg_Timer.Enabled = True
        End If
    End Sub

    Private Sub Msg_Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Msg_Timer.Tick
        vCounter += 1
        CheckTimer()
    End Sub

    Private Sub CheckTimer()
        If vCounter > 6 Then
            Msg_Timer.Enabled = False
            StsBar_Main.Panels("Msg_Text").Text = ""
            vCounter = 0
        End If
    End Sub

#End Region
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub MDIForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Dim vEmpDesc As String = cControls.fReturnValue(" Select DescA From Employees " &
                                                            " Where  Code = '" & vUsrCode & "' " &
                                                            " And    Company_Code = " & vCompanyCode, Me.Name)

            Dim vBranchDesc As String = cControls.fReturnValue(" Select DescA From Branches " &
                                                               " Where  Code = '" & vCompanyCode & "'" &
                                                               " And    Company_Code = " & vCompanyCode, Me.Name)


            If vChangeUser = "Y" Then
                Dim vNewFrm As System.Windows.Forms.Form
                For Each vNewFrm In Me.MdiChildren
                    vNewFrm.Close()
                Next

                'Here Is I Call the Tiles Screen in the load of the system.. 

                'vNewFrm = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".Frm_GeneralTiles_A", True)
                'vNewFrm.MdiParent = Me
                'vNewFrm.Show()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub MDIForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'sLoadSettings()
        Try

            Dim vEmpDesc As String = vUsrName

            Dim vPublishVersion As String

            If My.Application.IsNetworkDeployed Then
                vPublishVersion = My.Application.Deployment.CurrentVersion.ToString
            Else
                vPublishVersion = ""
            End If

            Dim vCompanyDesc As String = cControls.fReturnValue(" Select DescA From Companies " &
                                                                " Where  Code = '" & vCompanyCode & "'", Me.Name)

            StsBar_Main.Panels("ServerName").Text = "Server Name: " & vServerName & " - UserName: " & vEmpDesc & " - Company: " & vCompanyDesc &
                                                    " - Publish version: " & vPublishVersion


            'Here I Load the AppStyle Selected before by the user..
            Dim vAppStyle As String = cControls.fReturnValue(" Select AppStyle From Employees " &
                                                             " Where  Code = '" & vUsrCode & "' " &
                                                             " And    Company_Code = " & vCompanyCode, Me.Name)

            If vAppStyle <> "" Then
                StyleManager.Load(Application.StartupPath + "\Styles\" + vAppStyle)
                Dim vCombo As ComboBoxTool = ToolBar_Main.Tools("Themes")
                vCombo.Value = vAppStyle
            End If

            Exp_Main.Groups.Clear()
            Exp_Main.Groups.Add("Main")

            'Run Sql Scripts
            sRunSqlScripts()

            'Exp_Main.Font = New Font(fonts.Families(0), 12)

            'Here Is I Call the Tiles Screen in the load of the system.. 
            Dim vNewFrm As System.Windows.Forms.Form

            vNewFrm = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".Frm_GeneralTiles_A", True)
            vNewFrm.MdiParent = Me
            vNewFrm.Show()

            'If vUsrCode = "000038" Then
            '    vNewFrm = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".Frm_UsersFollowUp_A", True)
            '    vNewFrm.MdiParent = Me
            '    vNewFrm.Show()
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sRunSqlScripts()
        If Not cControls.fIsExist(" From Reports Where Code = 'Ded.sql' ", Me.Name) Then
            Dim cmd As New SqlClient.SqlCommand
            Dim objReader As System.IO.StreamReader

            cControls.vSqlConn.Open()
            cmd.CommandType = CommandType.Text
            cmd.Connection = cControls.vSqlConn

            Dim vEncode As Text.Encoding = Encoding.Default
            objReader = New System.IO.StreamReader(Application.StartupPath & "\Ded.sql", vEncode)
            cmd.CommandText = objReader.ReadToEnd
            cmd.ExecuteNonQuery()

            cControls.vSqlConn.Close()

            cControls.fSendData(" Insert Into Reports (Sys_Code,      Code ) " &
                                "             Values  (  'SP',     'Ded.sql') ", Me.Name)

        End If

    End Sub

    Private Sub MDIForm_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        'If MessageBox.Show("Â· „ √ﬂœ „‰ «€·«ﬁ «·»—‰«„Ã", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
        '    e.Cancel = True
        '    Exit Sub
        'End If

        'sSaveSettings()

        cControls.fSendData(" Update Employees Set Active_Status = 'OF' " &
                            " Where  Code = '" & vUsrCode & "' " &
                            " And    Company_Code = " & vCompanyCode, Me.Name)

        'Here I Create the Log Data
        Dim vSqlString As String
        Dim vLog_Code As Int32

        vSqlString = " Select IsNull(Max(Code), 0) + 1 From  Employees_AllActions_Log "
        vLog_Code = cControls.fReturnValue(vSqlString, Me.Name)

        vSqlString = " Insert Into Employees_AllActions_Log (      Code,             Emp_Code,          TDate,           Action_Desc,    Action_Type,      Invoice_Type,          Invoice_Code,         Main_Object_Code,        Main_Object_Desc,      CostCenter_Code,     Remarks,               ComputerName )" &
                     "                             Values   (" & vLog_Code & ", '" & vUsrCode & "',   GetDate(),        ' ”ÃÌ· «·Œ—ÊÃ',   'O',                  '',                     '',                     '',                     '',                 '',                 '',         '" & My.Computer.Name & "' ) "

        cControls.fSendData(vSqlString, Me.Name)

        For O As Single = 1 To 0 Step -0.1
            Me.Opacity = O
            Me.Refresh()
            System.Threading.Thread.Sleep(100)
        Next
        Me.Opacity = 0
    End Sub

    Private Sub Exp_Main_Click(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinExplorerBar.ItemEventArgs) Handles Exp_Main.ItemClick
        sOpenForm(e.Item.Key)
    End Sub

    Public Sub sOpenForm(ByVal pFormName As String)
        Try
            Dim vNewFrm As System.Windows.Forms.Form

            If Trim(pFormName) <> "Frm_Mafroshat_SalesInvoice" And Trim(pFormName) <> "Frm_POS" Then
                For Each vNewFrm In Me.MdiChildren
                    If vNewFrm.Name = pFormName & "_" & vLang Then
                        vNewFrm.Activate()
                        Return
                    End If
                Next
            End If

            'cControls.fSendData(" Insert Into Employees_Open_Form_Log  (     Company_Code,          Emp_Code,          Mod_Code,       TDate ) " &
            '                    "                               Values (" & vCompanyCode & ", '" & vUsrCode & "', '" & pFormName & "', GetDate() ) ", Me.Name)

            vNewFrm = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & "." & Trim(pFormName) & "_" & vLang, True)
            vNewFrm.MdiParent = Me
            vNewFrm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Print_OpenCashDrawer(ByVal sender As Object, ByVal ppea As PrintPageEventArgs)
        'LD 23-1-2009
        'Header & footer Variables
        '  Try

        Dim f_English As New Font("Tahoma", 9, FontStyle.Regular, GraphicsUnit.Point)

        Dim grfx As System.Drawing.Graphics = ppea.Graphics
        grfx.DrawString("Open Cash Drawer", f_English, System.Drawing.Brushes.Black, 5, 3)

    End Sub

    Private Sub ToolBar_Main_ToolClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Try
            Dim vForm As Object
            Select Case e.Tool.Key
                Case "Btn_CloseWindow"
                    If Me.MdiChildren.Length > 0 Then
                        Me.ActiveMdiChild.Close()
                    End If
                    Return
                Case "Btn_CloseApplication"
                    Application.Exit()
                Case "Btn_Save"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.fSaveAll(False)
                    End If
                    Return
                Case "Btn_New"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sNewRecord()
                    End If
                    Return
                Case "Btn_Delete"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sDelete()
                    End If
                    Return
                Case "Btn_PreviousRecord"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sQuery(1)
                    End If
                    Return
                Case "Btn_NextRecord"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sQuery(-1)
                    End If
                    Return
                Case "Btn_LastRecord"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sQuery()
                    End If
                    Return
                Case "Btn_FirstRecord"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sQuery(-2)
                    End If
                    Return
                Case "Btn_Find"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sFind()
                    End If
                    Return
                Case "Btn_Query"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sQuery()
                    End If
                    Return
                Case "Btn_Print"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sPrint()
                    End If
                    Return
                Case "Btn_ScreenState"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        'vForm.sChangeTab()
                    End If
                    Return
                Case "Btn_ChangeUser"
                    vChangeUser = "Y"

                    'First I Close the welcome Screen..
                    For Each vMdiForm As Form In Me.MdiChildren
                        If vMdiForm.Name = "Frm_GeneralTiles" Then
                            vMdiForm.Close()
                        End If
                    Next

                    Dim vCheckChange As String = vUsrCode
                    Dim vCheckPassward As New Frm_CheckPassword_L
                    vCheckPassward.ShowDialog()

                    If vUsrCode <> vCheckChange Then

                        'Here I Load the AppStyle Selected before by the user..
                        Dim vAppStyle As String = cControls.fReturnValue(" Select AppStyle From Employees " &
                                                                         " Where  Code = '" & vUsrCode & "' " &
                                                                         " And    Company_Code = " & vCompanyCode, Me.Name)

                        If vAppStyle <> "" Then
                            StyleManager.Load(Application.StartupPath + "\Styles\" + vAppStyle)
                            Dim vCombo As ComboBoxTool = ToolBar_Main.Tools("Themes")
                            vCombo.Value = vAppStyle
                        Else
                            StyleManager.Reset()
                            Dim vCombo As ComboBoxTool = ToolBar_Main.Tools("Themes")
                            vCombo.SelectedIndex = -1
                        End If

                        'Here I Create the Log Data
                        'First I Sign Out the Previous User and Sign In the new one
                        Dim vSqlString As String
                        Dim vLog_Code As Int32

                        vSqlString = " Select IsNull(Max(Code), 0) + 1 From  Employees_AllActions_Log "
                        vLog_Code = cControls.fReturnValue(vSqlString, Me.Name)

                        vSqlString = " Insert Into Employees_AllActions_Log (      Code,               Emp_Code,          TDate,           Action_Desc,    Action_Type,      Invoice_Type,          Invoice_Code,         Main_Object_Code,        Main_Object_Desc,      CostCenter_Code,     Remarks,               ComputerName )" &
                                     "                             Values   (" & vLog_Code & ", '" & vCheckChange & "',   GetDate(),        ' ”ÃÌ· «·Œ—ÊÃ',   'O',                  '',                     '',                     '',                     '',                 '',                 '',         '" & My.Computer.Name & "' ) "

                        cControls.fSendData(vSqlString, Me.Name)

                        cControls.fSendData(" Update Employees Set Active_Status = 'OF' " &
                                            " Where  Code = '" & vCheckChange & "' " &
                                            " And    Company_Code = " & vCompanyCode, Me.Name)

                        vSqlString = " Select IsNull(Max(Code), 0) + 1 From  Employees_AllActions_Log "
                        vLog_Code = cControls.fReturnValue(vSqlString, Me.Name)

                        vSqlString = " Insert Into Employees_AllActions_Log (      Code,             Emp_Code,          TDate,           Action_Desc,    Action_Type,      Invoice_Type,          Invoice_Code,         Main_Object_Code,        Main_Object_Desc,      CostCenter_Code,     Remarks,               ComputerName )" &
                                     "                             Values   (" & vLog_Code & ", '" & vUsrCode & "',   GetDate(),        ' ”ÃÌ· «·œŒÊ·',   'O',                  '',                     '',                     '',                     '',                 '',                 '',         '" & My.Computer.Name & "' ) "

                        cControls.fSendData(vSqlString, Me.Name)

                        cControls.fSendData(" Update Employees Set Active_Status = 'ON' " &
                                            " Where  Code = '" & vUsrCode & "' " &
                                            " And    Company_Code = " & vCompanyCode, Me.Name)

                        'For Each vMdiForm As Form In Me.MdiChildren
                        '    If cControls.fReturnValue(" Select Enabled From Profiles_Systems_Modules " & _
                        '                           " Inner Join Employees " & _
                        '                           " On Employees.Code = Profiles_Systems_Modules.Emp_Code " & _
                        '                           " Inner Join Employees " & _
                        '                           " On Employees.Code = Employees.Profile " & _
                        '                           " Where Mod_Code = '" & vMdiForm.Name & "'" & _
                        '                           " And   Employees.Code = '" & vUsrCode & "'", Me.Name) = "N" Then
                        '        vMdiForm.Close()
                        '    End If
                        'Next
                    End If

                    'Here I Check if the Help Button is Enabled or No...
                    Dim vTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
                    'vTool = ToolBar_Main.Tools("Chk_Help")
                    'If cControls.fReturnValue("Select IsEnabled From IsHelpEnabled Where Emp_Code = '" & vUsrCode & "'", Me.Name) = "Y" Then
                    '    vTool.Checked = True
                    'Else
                    '    vTool.Checked = False
                    'End If

                    vChangeUser = "N"

                    Return
                Case "Chk_Help"

                    Dim vTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
                    vTool = ToolBar_Main.Tools("Chk_Help")
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sIsHelpEnabled(vTool.Checked)
                    End If
                    cControls.fSendData(" Delete From IsHelpEnabled " &
                                        " Where  Emp_Code = '" & vUsrCode & "'", Me.Name)

                    If vTool.Checked Then
                        cControls.fSendData(" Insert Into IsHelpEnabled (Emp_Code, IsEnabled) Values ('" & vUsrCode & "', 'Y')", Me.Name)
                    Else
                        cControls.fSendData(" Insert Into IsHelpEnabled (Emp_Code, IsEnabled) Values ('" & vUsrCode & "', 'N')", Me.Name)
                    End If

                    Return
            End Select

            Dim vNewForm As Form
            For Each vNewForm In Me.MdiChildren
                If vNewForm.Name = e.Tool.Key Then
                    vNewForm.Activate()
                    Return
                End If
            Next

            vNewForm = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & "." & Trim(e.Tool.Key), True)
            vNewForm.MdiParent = Me
            vNewForm.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub sEnableTools(ByVal pNew As Boolean, ByVal pQuery As Boolean, ByVal pDelete As Boolean, ByVal pSave As Boolean, ByVal pLastRecord As Boolean, ByVal pNextRecord As Boolean, ByVal pPrevious As Boolean, ByVal pFirstRecord As Boolean, ByVal pGoto As String, ByVal pPrint As Boolean, Optional ByVal pFind As Boolean = False, Optional ByVal pScreenStateText As String = "", Optional ByVal pScreenStateEnabled As Boolean = True, Optional ByVal pCloseWindow As Boolean = True)
        ToolBar_Main.Tools("Btn_New").SharedProps.Enabled = pNew
        ToolBar_Main.Tools("Btn_Query").SharedProps.Enabled = pQuery
        ToolBar_Main.Tools("Btn_Delete").SharedProps.Enabled = pDelete
        ToolBar_Main.Tools("Btn_Save").SharedProps.Enabled = pSave
        ToolBar_Main.Tools("Btn_LastRecord").SharedProps.Enabled = pLastRecord
        ToolBar_Main.Tools("Btn_NextRecord").SharedProps.Enabled = pNextRecord
        ToolBar_Main.Tools("Btn_PreviousRecord").SharedProps.Enabled = pPrevious
        ToolBar_Main.Tools("Btn_FirstRecord").SharedProps.Enabled = pFirstRecord
        ToolBar_Main.Tools("Btn_Print").SharedProps.Enabled = pPrint
        ToolBar_Main.Tools("Btn_ScreenState").SharedProps.Caption = pScreenStateText
        ToolBar_Main.Tools("Btn_ScreenState").SharedProps.Enabled = pScreenStateEnabled
        ToolBar_Main.Tools("Btn_CloseWindow").SharedProps.Enabled = pCloseWindow
        'ToolBar_Main.Tools("Btn_Find").SharedProps.Enabled = pFind
    End Sub

    Public Sub sLoadExplorer(ByVal pSection_Code As String)
        Try
            Dim vGroup As RibbonGroup
            Dim vTool As ToolBase

            'For Each vGroup In ToolBar_Main.Ribbon.Tabs(0).Groups
            '    vGroup.Visible = False

            '    For Each vTool In vGroup.Tools
            '        vTool.SharedProps.Visible = False
            '    Next
            'Next

            'First I Check if this user Is Admin
            Dim vSqlString As String

            If vIsAdmin = "Y" Then
                vSqlString = " Select Systems.Code, " &
                             "        IsNull(Systems.Desc" & vLang & ", 'UnKnown'),       " &
                             "        IsNull(Systems.IsRibbon, 'N')      " &
                             " From   Systems                            " &
                             " Where  1 = 1                              " &
                             " And    Sec_Code = '" & pSection_Code & "' " &
                             " And    IsNull(IsActive, 'Y') = 'Y'        " &
                             " Order By Systems.Load_Order "
            Else
                vSqlString = " Select Profiles_Systems.Sys_Code, " &
                             "        IsNull(Systems.Desc" & vLang & ", 'UnKnown'),       " &
                             "        IsNull(Systems.IsRibbon, 'N')      " &
                             " From Systems Inner Join Profiles_Systems " &
                             " On Systems.Code = Profiles_Systems.Sys_Code " &
                             "                                          " &
                             " Inner Join Employees " &
                             " On  Employees.Code = Profiles_Systems.Emp_Code " &
                             " And Employees.Company_Code = Profiles_Systems.Company_Code " & vbCrLf &
                             "                                             " & vbCrLf &
                             " Where Employees.Code = " & vUsrCode &
                             " And   Enabled = 'Y' " & vbCrLf &
                             " And   IsNULL(Systems.IsActive, 'Y') = 'Y' " &
                             " And   Employees.Company_Code = " & vCompanyCode & vbCrLf &
                             " Order By Systems.Load_Order "
            End If

            Dim vSqlCommand As New SqlCommand
            vSqlCommand.Connection = cControls.vSqlConn

            vSqlCommand.CommandText = vSqlString
            cControls.vSqlConn.Open()
            Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
            Exp_Main.Groups.Clear()
            While vReader.Read
                If IsDBNull(vReader(0)) = False Then
                    Exp_Main.Groups.Add(Trim(vReader(0)), Trim(vReader(1)))
                    'If vReader(2) = "Y" Then
                    '    ToolBar_Main.Ribbon.Tabs(0).Groups(Trim(vReader(0))).Visible = True
                    'End If
                End If
            End While
            cControls.vSqlConn.Close()
            vReader.Close()
            vSqlCommand.Connection = cControls.vSqlConn

            If vIsAdmin = "Y" Then
                vSqlString = " Select System_Modules.Sys_Code, " &
                             "        System_Modules.Code, " &
                             "        IsNull(System_Modules.Desc" & vLang & ", 'UnKnown'), " &
                             "        IsNull(System_Modules.IsRibbon, 'N')  " &
                             " From System_Modules                          " &
                             "                                              " &
                             " Where 1 = 1 " &
                             " And   System_Modules.Sec_Code = '" & pSection_Code & "' " &
                             " And   IsNULL(System_Modules.IsActive, 'Y') = 'Y' " &
                             "                                           " &
                             " Order BY System_Modules.Load_Order "
            Else
                vSqlString = " Select Profiles_Systems_Modules.Sys_Code, " &
                             "        Profiles_Systems_Modules.Mod_Code, " &
                             "        IsNull(System_Modules.Desc" & vLang & ", 'UnKnown'), " &
                             "        IsNull(System_Modules.IsRibbon, 'N')  " &
                             " From System_Modules Inner Join Profiles_Systems_Modules " &
                             " On   System_Modules.Sys_Code = Profiles_Systems_Modules.Sys_Code " &
                             " And  System_Modules.Code     = Profiles_Systems_Modules.Mod_Code " &
                             "                                                                  " &
                             " Inner Join Employees " &
                             " On Employees.Code = Profiles_Systems_Modules.Emp_Code " &
                             " And Employees.Company_Code = Profiles_Systems_Modules.Company_Code " & vbCrLf &
                             "                                                      " &
                             " Where Employees.Code = " & vUsrCode &
                             " And   Employees.Company_Code = " & vCompanyCode & vbCrLf &
                             " And   Enabled = 'Y' " &
                             " And   IsNULL(System_Modules.IsActive, 'Y') = 'Y' " &
                             "                                                  " &
                             " Order BY Load_Order "

            End If

            'vSqlCommand.CommandText = " Select Sys_Code, Code, DescA From System_Modules "
            vSqlCommand.CommandText = vSqlString
            cControls.vSqlConn.Open()
            vReader = vSqlCommand.ExecuteReader
            While vReader.Read
                Dim vint As Integer = vReader.RecordsAffected
                If IsDBNull(vReader(0)) = False Then

                    Exp_Main.Groups(Trim(vReader(0))).Items.Add(Trim(vReader(1)), Trim(vReader(2)))

                    If vReader(3) = "Y" Then
                        'ToolBar_Main.Ribbon.Tabs(0).Groups(Trim(vReader(0))).Tools(Trim(vReader(1))).SharedProps.Visible = True
                    End If
                End If
            End While

            ' Styling Items In Sidebar Groups
            For Each group As UltraExplorerBarGroup In Exp_Main.Groups
                For Each item As UltraExplorerBarItem In group.Items
                    item.Settings.AppearancesSmall.Appearance.BackColor = Color.White
                    item.Settings.AppearancesSmall.Appearance.BackColor2 = Color.FromArgb(242, 200, 160)
                    item.Settings.AppearancesSmall.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
                    item.Settings.AppearancesSmall.Appearance.Cursor = Cursors.Hand
                Next
            Next

            cControls.vSqlConn.Close()
            vReader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub sPrintRec(ByVal pRecPos As String)
        Dim vTextBoxTool As Infragistics.Win.UltraWinToolbars.TextBoxTool
        vTextBoxTool = ToolBar_Main.Tools("Btn_GotoRecord")
        vTextBoxTool.Text = pRecPos
    End Sub

    Private Sub UltraCheckEditor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_ScreenState.CheckedChanged
        If sender.Name = "UltraCheckEditor1" Then
            Chk_ScreenState.Checked = False
        End If
    End Sub

    Private Sub sSaveSettings()
        Try
            vSettings.vMinimizeRibbon = ToolBar_Main.Ribbon.IsMinimized
            vSettings.vExpMain_Width = Exp_Main.Width

            Settings.Persist(vSettings, "C:\DotNet_Settings.txt")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sLoadSettings()
        Try
            vSettings = Settings.Load("C:\DotNet_Settings.txt")

            ToolBar_Main.Ribbon.IsMinimized = vSettings.vMinimizeRibbon
            Exp_Main.Width = vSettings.vExpMain_Width
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Notification_Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Notification_Timer.Tick

        If vCounter > 0 Then
            vCounter -= 1
        End If

        If vCounter = 0 Then
            Txt_Message.Appearance.AlphaLevel -= 4

        End If

        If Txt_Message.Appearance.AlphaLevel < 3 Then
            Txt_Message.Visible = False
            Notification_Timer.Enabled = False
            Exit Sub
        End If
    End Sub

    'Private Sub StsBar_Main_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StsBar_Main.DoubleClick
    '    Try
    '        Dim vChangeBranch As New Frm_ChangeBranch
    '        vChangeBranch.ShowDialog()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub StsBar_Main_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StsBar_Main.DoubleClick
        'Call Main()

        Dim vFrm_DataBase As New Frm_DatabaseMangment
        vFrm_DataBase.ShowDialog()
    End Sub

    Private Sub ToolBar_Main_ToolValueChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolEventArgs) Handles ToolBar_Main.ToolValueChanged
        If Not TypeOf e.Tool Is ComboBoxTool Then
            Exit Sub
        End If

        Dim vTool As ComboBoxTool = e.Tool

        If vTool.SelectedIndex = -1 Then
            Exit Sub
        End If

        If vTool.Value = "None" Then
            StyleManager.Reset()
            cControls.fSendData(" Update Employees Set AppStyle = NULL  " & _
                                " Where  Code = '" & vUsrCode & "' " & _
                                " And    Company_Code = " & vCompanyCode, Me.Name)
        Else
            StyleManager.Load(Application.StartupPath + "\Styles\" + vTool.Value)
            cControls.fSendData(" Update Employees Set AppStyle = '" & vTool.Value & "' " & _
                                " Where  Code = '" & vUsrCode & "' " & _
                                " And    Company_Code = " & vCompanyCode, Me.Name)

        End If
    End Sub

    'This is how I can detect the not active user by Timer
    Private Sub ActiveDetect_Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActiveDetect_Timer.Tick
        vMousePosition = MousePosition.X

        If vPreviousPosition = 0 Then
            vPreviousPosition = vMousePosition
        Else
            If vPreviousPosition = vMousePosition Then
                If vUserStatus <> "Away" Then
                    vNotActiveUser += 1

                    If vNotActiveUser = 23 Then
                        vUserStatus = "Away"
                        cControls.fReturnValue(" Update Employees Set Active_Status = 'AW' " & _
                                               " Where  Code = '" & vUsrCode & "' " & _
                                               " And    Company_Code = " & vCompanyCode, Me.Name)
                    End If
                End If
            Else
                vNotActiveUser = 1
                vPreviousPosition = vMousePosition
                If vUserStatus <> "Online" Then
                    vUserStatus = "Online"
                    cControls.fReturnValue(" Update Employees Set Active_Status = 'ON' " & _
                                           " Where  Code = '" & vUsrCode & "' " & _
                                           " And    Company_Code = " & vCompanyCode, Me.Name)
                End If
            End If
        End If
    End Sub

    'Private Sub MDIForm_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress

    'If Me.ActiveMdiChild.Name = "Frm_ElGohary_POS3_A" Then
    '    Dim vNewFrm As New Frm_ElGohary_POS3_A
    '    vNewFrm.sListenToKeys(e.KeyChar)

    'End If
    'End Sub

    Private Sub Update_Timer_Tick(sender As Object, e As EventArgs) Handles Update_Timer.Tick
        'This Timer to check for new Update versions 
        InstallUpdateSyncWithInfo()
    End Sub
    Private Sub InstallUpdateSyncWithInfo()
        Dim info As UpdateCheckInfo = Nothing

        If (ApplicationDeployment.IsNetworkDeployed) Then
            Dim AD As ApplicationDeployment = ApplicationDeployment.CurrentDeployment

            Try
                info = AD.CheckForDetailedUpdate()
            Catch dde As DeploymentDownloadException
                MessageBox.Show("The new version of the application cannot be downloaded at this time. " + ControlChars.Lf & ControlChars.Lf & "Please check your network connection, or try again later. Error: " + dde.Message)
                Return
            Catch ioe As InvalidOperationException
                MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " & ioe.Message)
                Return
            End Try

            If (info.UpdateAvailable) Then
                Dim doUpdate As Boolean = True

                If (Not info.IsUpdateRequired) Then

                    Update_Timer.Enabled = False

                    Dim dr As DialogResult = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel)

                    If (Not System.Windows.Forms.DialogResult.OK = dr) Then
                        doUpdate = False
                    End If
                Else
                    ' Display a message that the app MUST reboot. Display the minimum required version.
                    MessageBox.Show("This application has detected a mandatory update from your current " &
                        "version to version " & info.MinimumRequiredVersion.ToString() &
                        ". The application will now install the update and restart.",
                        "Update Available", MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                End If

                If (doUpdate) Then
                    Try
                        AD.Update()
                        MessageBox.Show("The application has been upgraded, and will now restart.")
                        Application.Restart()
                    Catch dde As DeploymentDownloadException
                        MessageBox.Show("Cannot install the latest version of the application. " & ControlChars.Lf & ControlChars.Lf & "Please check your network connection, or try again later.")
                        Return
                    End Try
                End If
            End If
        End If
    End Sub

End Class