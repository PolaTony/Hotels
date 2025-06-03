Imports System.Data.sqlclient
Imports Infragistics.Win.UltraWinToolbars
Imports Infragistics.Win.AppStyling

Public Class MDIForm_L

#Region " Variables Declaration                                                  "
    Dim vCounter As Integer = 0
    Dim vSettings As Settings
#End Region

#Region " Message Area Manager                                                   "
    Public Function sForwardMessage(ByVal pMsgNo As String, Optional ByVal vObj As Object = "") As MsgBoxResult
        'If pTimer = True Then
        Dim vType As String = cControls.fReturnValue(" Select Mode From Messages Where Code = '" & pMsgNo & "'", Me.Name)
        Dim vMessage As String = cControls.fReturnValue(" Select Desc" & vLang & " From Messages Where Code = '" & pMsgNo & "'", Me.Name)
        If Trim(vType) = "WRN" Or Trim(vType) = "CRT" Then
            StsBar_Main.Panels("Msg_Text").Appearance.ForeColor = Color.Red
            Txt_Message.Appearance.ForeColor = Color.Red
        ElseIf Trim(vType) = "DON" Then
            StsBar_Main.Panels("Msg_Text").Appearance.ForeColor = Color.Blue
            Txt_Message.Appearance.ForeColor = Color.Blue
        ElseIf Trim(vType) = "MsgOkErr" Then
            MessageBox.Show(vMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        ElseIf Trim(vType) = "MsgYesNoQus" Then
            If MessageBox.Show(vMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Return MsgBoxResult.Yes
            Else
                Return MsgBoxResult.No
            End If
        End If
        'StsBar_Main.Panels("Msg_Text").Text = vMessage

        'Msg_Timer.Enabled = True

        Txt_Message.Text = vMessage

        Txt_Message.Visible = True

        vCounter = 20

        Txt_Message.Appearance.AlphaLevel = 250

        Notification_Timer.Enabled = True

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

    Private Sub MDIForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Dim vEmpDesc As String = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & vUsrCode & "' ", Me.Name)

            Dim vBranchDesc As String = cControls.fReturnValue(" Select DescA From Branches Where Code = '" & vCompanyCode & "'", Me.Name)
            StsBar_Main.Panels("ServerName").Text = "Server Name: " & vServerName & " - UserName: " & vEmpDesc & " - Branch: " & vBranchDesc
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub MDIForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'sLoadSettings()

        sLoadExplorer()
        Dim vEmpDesc As String = cControls.fReturnValue(" Select DescA From Employees Where Code = '" & vUsrCode & "' ", Me.Name)

        Dim vCompanyDesc As String = cControls.fReturnValue(" Select DescA From Companies Where Code = '" & vCompanyCode & "'", Me.Name)
        StsBar_Main.Panels("ServerName").Text = "Server Name: " & vServerName & " - UserName: " & vEmpDesc & " - Company: " & vCompanyDesc

        'Here I check if the Help Button is Enabled or No..
        Dim vTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
        vTool = ToolBar_Main.Tools("Chk_Help")

        'Here I Load the AppStyle Selected before by the user..
        Dim vAppStyle As String = cControls.fReturnValue(" Select AppStyle From Employees Where Code = '" & vUsrCode & "' ", Me.Name)

        If vAppStyle <> "" Then
            StyleManager.Load(Application.StartupPath + "\Styles\" + vAppStyle)
            Dim vCombo As ComboBoxTool = ToolBar_Main.Tools("Themes")
            vCombo.Value = vAppStyle
        End If

        If cControls.fReturnValue("Select IsEnabled From IsHelpEnabled Where Emp_Code = '" & vUsrCode & "'", Me.Name) = "Y" Then
            vTool.Checked = True
        Else
            vTool.Checked = False
        End If

        'Here Is I Call the Tiles Screen in the load of the system.. 
        Dim vNewFrm As System.Windows.Forms.Form

        vNewFrm = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & ".Frm_GeneralTiles_L", True)
        vNewFrm.MdiParent = Me
        vNewFrm.Show()
    End Sub

    Private Sub MDIForm_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'If vIsDemoVersion Then
        '    Dim vSQlString As String
        '    vSQlString = " Update Software_Security " & _
        '                 " Set TCount = (Select Max(IsNull(TCount, 0)) + 1 From  Software_Security) "
        '    cBase.fSendData(vSQlString, Me.Name)
        'End If

        'sSaveSettings()

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

            If pFormName <> "Frm_ElGohary_POS3" Then
                For Each vNewFrm In Me.MdiChildren
                    If vNewFrm.Name = pFormName & "_" & vLang Then
                        vNewFrm.Activate()
                        Return
                    End If
                Next
            End If

            vNewFrm = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & "." & Trim(pFormName) & "_" & vLang, True)
            vNewFrm.MdiParent = Me
            vNewFrm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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
                        vForm.sQuery(-1)
                    End If
                    Return
                Case "Btn_NextRecord"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sQuery(1)
                    End If
                    Return
                Case "Btn_LastRecord"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sQuery(-2)
                    End If
                    Return
                Case "Btn_FirstRecord"
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sQuery()
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
                    Dim vCheckChange As String = vUsrCode
                    Dim vCheckPassward As New Frm_CheckPassword_L
                    vCheckPassward.ShowDialog()

                    If vUsrCode <> vCheckChange Then
                        sLoadExplorer()

                        'Here I Load the AppStyle Selected before by the user..
                        Dim vAppStyle As String = cControls.fReturnValue(" Select AppStyle From Employees Where Code = '" & vUsrCode & "' ", Me.Name)

                        If vAppStyle <> "" Then
                            StyleManager.Load(Application.StartupPath + "\Styles\" + vAppStyle)
                            Dim vCombo As ComboBoxTool = ToolBar_Main.Tools("Themes")
                            vCombo.Value = vAppStyle
                        Else
                            StyleManager.Reset()
                            Dim vCombo As ComboBoxTool = ToolBar_Main.Tools("Themes")
                            vCombo.SelectedIndex = -1
                        End If

                        For Each vMdiForm As Form In Me.MdiChildren
                            If cControls.fReturnValue(" Select Enabled From Profiles_Systems_Modules " & _
                                                   " Inner Join Profiles " & _
                                                   " On Profiles.Code = Profiles_Systems_Modules.Prf_Code " & _
                                                   " Inner Join Employees " & _
                                                   " On Profiles.Code = Employees.Profile " & _
                                                   " Where Mod_Code = '" & vMdiForm.Name & "'" & _
                                                   " And   Employees.Code = '" & vUsrCode & "'", Me.Name) = "N" Then
                                vMdiForm.Close()
                            End If
                        Next
                    End If

                    'Here I Check if the Help Button is Enabled or No...
                    Dim vTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
                    vTool = ToolBar_Main.Tools("Chk_Help")
                    If cControls.fReturnValue("Select IsEnabled From IsHelpEnabled Where Emp_Code = '" & vUsrCode & "'", Me.Name) = "Y" Then
                        vTool.Checked = True
                    Else
                        vTool.Checked = False
                    End If

                    Return
                Case "Chk_Help"

                    Dim vTool As Infragistics.Win.UltraWinToolbars.StateButtonTool
                    vTool = ToolBar_Main.Tools("Chk_Help")
                    If Me.MdiChildren.Length > 0 Then
                        vForm = Me.ActiveMdiChild
                        vForm.sIsHelpEnabled(vTool.Checked)
                    End If
                    cControls.fSendData(" Delete From IsHelpEnabled Where Emp_Code = '" & vUsrCode & "'", Me.Name)

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

    Public Sub sEnableTools(ByVal pNew As Boolean, ByVal pQuery As Boolean, ByVal pDelete As Boolean, ByVal pSave As Boolean, ByVal pLastRecord As Boolean, ByVal pNextRecord As Boolean, ByVal pPrevious As Boolean, ByVal pFirstRecord As Boolean, ByVal pGoto As String, ByVal pPrint As Boolean, Optional ByVal pFind As Boolean = False, Optional ByVal pScreenStateText As String = "", Optional ByVal pScreenStateEnabled As Boolean = True)
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
        'ToolBar_Main.Tools("Btn_Find").SharedProps.Enabled = pFind
    End Sub

    Private Sub sLoadExplorer()
        Try
            Dim vGroup As RibbonGroup
            Dim vTool As ToolBase
            For Each vGroup In ToolBar_Main.Ribbon.Tabs(0).Groups
                vGroup.Visible = False

                For Each vTool In vGroup.Tools
                    vTool.SharedProps.Visible = False
                Next
            Next

            Dim vSqlCommand As New SqlCommand
            vSqlCommand.Connection = cControls.vSqlConn
            'vSqlCommand.CommandText = " Select Sys_Code, Code, DescA From System_Modules "
            vSqlCommand.CommandText = " Select Profiles_Systems.Sys_Code, " & _
                                      "        IsNull(Systems.Desc" & vLang & ", 'UnKnown'),       " & _
                                      "        IsNull(Systems.IsRibbon, 'N')      " & _
                                      " From Systems Inner Join Profiles_Systems " & _
                                      " On Systems.Code = Profiles_Systems.Sys_Code " & _
                                      " Inner Join Profiles " & _
                                      " On Profiles.Code = Profiles_Systems.Prf_Code " & _
                                      " Inner Join Employees " & _
                                      " On Employees.Profile = Profiles.Code " & _
                                      " Where Employees.Code = " & vUsrCode & _
                                      " And   Enabled = 'Y' " & vbCrLf & _
                                      " Order By Systems.Load_Order "
            cControls.vSqlConn.Open()
            Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
            Exp_Main.Groups.Clear()
            While vReader.Read
                If IsDBNull(vReader(0)) = False Then
                    Exp_Main.Groups.Add(Trim(vReader(0)), Trim(vReader(1)))
                    If vReader(2) = "Y" Then
                        'ToolBar_Main.Ribbon.Tabs(0).Groups(Trim(vReader(0))).Visible = True
                    End If
                End If
            End While
            cControls.vSqlConn.Close()
            vReader.Close()
            'If Trim(cBase.fReturnValue("Select IsAdmin From Users Where Code = '" & vUsrCode & "'", Me.Name)) = "Y" Then
            vSqlCommand.Connection = cControls.vSqlConn
            'vSqlCommand.CommandText = " Select Sys_Code, Code, DescA From System_Modules "
            vSqlCommand.CommandText = " Select Profiles_Systems_Modules.Sys_Code, " & _
                                      "        Profiles_Systems_Modules.Mod_Code, " & _
                                      "        IsNull(System_Modules.Desc" & vLang & ", 'UnKnown'), " & _
                                      "        IsNull(System_Modules.IsRibbon, 'N')  " & _
                                      " From System_Modules Inner Join Profiles_Systems_Modules " & _
                                      " On   System_Modules.Sys_Code = Profiles_Systems_Modules.Sys_Code " & _
                                      " And  System_Modules.Code     = Profiles_Systems_Modules.Mod_Code " & _
                                      " Inner Join Profiles " & _
                                      " On Profiles.Code = Profiles_Systems_Modules.Prf_Code " & _
                                      " Inner Join Employees " & _
                                      " On Employees.Profile = Profiles.Code " & _
                                      " Where Employees.Code = " & vUsrCode & _
                                      " And   Enabled = 'Y' "
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
            cControls.vSqlConn.Close()
            vReader.Close()
            Dim vItem As Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'For Each vItem In Exp_Main.Groups("Grp_Reports").Items
        '    If vItem.Key = "FRM_AttendanceReport" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(55)
        '        'ElseIf vItem.Key = "FRM_ItemSetup" Then
        '        '    vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(28)
        '    End If
        'Next
        'For Each vItem In Exp_Main.Groups("GN").Items
        '    If vItem.Key = "FRM_GeneralSettings" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(27)
        '    End If
        '    If vItem.Key = "FRM_FinancialSetup" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(44)
        '    End If
        '    If vItem.Key = "FRM_HR_Definition" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(26)
        '    End If
        '    If vItem.Key = "FRM_ItemSetup" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(27)
        '    End If
        'Next
        'For Each vItem In Exp_Main.Groups("Grp_Definitions").Items
        '    If vItem.Key = "Frm_ProviderDefinition" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(18)
        '    End If
        '    If vItem.Key = "Frm_CustomerDefinition" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(18)
        '    End If
        '    If vItem.Key = "FRM_ItemDefinition" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(45)
        '    End If
        '    If vItem.Key = "Frm_EmployeeDeifinition" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(18)
        '    End If
        'Next
        'For Each vItem In Exp_Main.Groups("Grp_Purchasing").Items
        '    If vItem.Key = "FRM_PurchaseInvoice" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(53)
        '    End If
        '    If vItem.Key = "Frm_PurchaseReturn" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(20)
        '    End If
        'Next
        'For Each vItem In Exp_Main.Groups("Grp_Sales").Items
        '    If vItem.Key = "Frm_SalesInvoice" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(53)
        '    End If
        '    If vItem.Key = "Frm_SalesReturn" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(20)
        '    End If
        'Next
        'For Each vItem In Exp_Main.Groups("Grp_Inventory").Items
        '    If vItem.Key = "Frm_Count" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(34)
        '    End If
        'Next
        'For Each vItem In Exp_Main.Groups("Grp_Financial").Items
        '    If vItem.Key = "Frm_Customer_FinancialAdjustment" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(50)
        '    ElseIf vItem.Key = "Frm_Provider_FinancialAdjustment" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(50)
        '    ElseIf vItem.Key = "Frm_CashReceipt" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(48)
        '    ElseIf vItem.Key = "Frm_CashPayment" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(49)
        '    End If
        'Next
        'For Each vItem In Exp_Main.Groups("Grp_Reports").Items
        '    If vItem.Key = "Frm_PurchaseReports" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(55)
        '    End If
        '    If vItem.Key = "Frm_SalesReports" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(55)
        '    End If
        '    If vItem.Key = "Frm_InvReports" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(55)
        '    End If
        'Next

        'For x As Int16 = Exp_Main.Groups.Count - 1 To 0 Step -1
        '    If Exp_Main.Groups(x).Items.Count = 0 Then
        '        Exp_Main.Groups(x).Visible = False
        '    End If
        'Next

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
            cControls.fSendData(" Update Employees Set AppStyle = NULL  Where Code = '" & vUsrCode & "' ", Me.Name)
        Else
            StyleManager.Load(Application.StartupPath + "\Styles\" + vTool.Value)
            cControls.fSendData(" Update Employees Set AppStyle = '" & vTool.Value & "' Where Code = '" & vUsrCode & "' ", Me.Name)

        End If
    End Sub
End Class