Imports System.Data.sqlclient

Public Class MDIForm

#Region " Variables Declaration                                                  "
    Dim vCounter As Integer = 0
#End Region

#Region " Message Area Manager                                                   "
    'Public Enum enumMessageType
    '    Critical = 1
    '    Warning = 2
    '    Done = 3
    '    Saved = 4
    '    Assistant = 5
    '    Empty = 6
    'End Enum

    'Public Sub sSendMsgToStatusBar(ByVal pMsg As String, Optional ByVal pTimer As Boolean = True, Optional ByVal pType As enumMessageType = enumMessageType.Warning)
    '    If pTimer = True Then
    '        If pType = enumMessageType.Warning Or pType = enumMessageType.Critical Then
    '            StsBar_Main.Panels("Msg_Text").Appearance.ForeColor = Color.Red
    '        ElseIf pType = enumMessageType.Saved Or pType = enumMessageType.Done Then
    '            StsBar_Main.Panels("Msg_Text").Appearance.ForeColor = Color.Blue
    '        End If
    '        StsBar_Main.Panels("Msg_Text").Text = pMsg
    '        Msg_Timer.Enabled = True
    '    End If
    'End Sub

    'Private Sub Msg_Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Msg_Timer.Tick
    '    vCounter += 1
    '    CheckTimer()
    'End Sub

    'Private Sub CheckTimer()
    '    If vCounter > 6 Then
    '        Msg_Timer.Enabled = False
    '        StsBar_Main.Panels("Msg_Text").Text = ""
    '        vCounter = 0
    '    End If
    'End Sub

#End Region

    Private Sub MDIForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sLoadExplorer()
    End Sub

    Private Sub MDIForm_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'If vIsDemoVersion Then
        '    Dim vSQlString As String
        '    vSQlString = " Update Software_Security " & _
        '                 " Set TCount = (Select Max(IsNull(TCount, 0)) + 1 From  Software_Security) "
        '    cBase.fDMLData(vSQlString, Me.Name)
        'End If
    End Sub

    Private Sub Exp_Main_Click(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinExplorerBar.ItemEventArgs) Handles Exp_Main.ItemClick
        sOpenForm(e.Item.Key)
    End Sub

    Private Sub sOpenForm(ByVal pFormName As String)
        Dim vNewFrm As System.Windows.Forms.Form

        For Each vNewFrm In Me.MdiChildren
            If vNewFrm.Name = pFormName Then
                vNewFrm.Activate()
                Return
            End If
        Next
        vNewFrm = System.Reflection.Assembly.GetExecutingAssembly.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & "." & Trim(pFormName), True)
        vNewFrm.MdiParent = Me
        vNewFrm.show()
    End Sub

    Private Sub ToolBar_Main_ToolClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Dim vForm As Object
        Select Case e.Tool.Key
            Case "Btn_CloseWindow"
                If Me.MdiChildren.Length > 0 Then
                    Me.ActiveMdiChild.Close()
                End If
            Case "Btn_CloseApplication"
                Application.Exit()
            Case "Btn_Save"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.fSaveAll(False)
                End If
            Case "Btn_New"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sNewRecord()
                End If
            Case "Btn_Delete"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sDelete()
                End If
            Case "Btn_PreviousRecord"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sQuery(-1)
                End If
            Case "Btn_NextRecord"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sQuery(1)
                End If
            Case "Btn_LastRecord"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sQuery(-2)
                End If
            Case "Btn_FirstRecord"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sQuery()
                End If
            Case "Btn_Find"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sFind()
                End If
            Case "Btn_Query"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sQuery()
                End If
            Case "Btn_Print"
                If Me.MdiChildren.Length > 0 Then
                    vForm = Me.ActiveMdiChild
                    vForm.sPrint()
                End If
        End Select
    End Sub

    Public Sub sEnableTools(ByVal pNew As Boolean, ByVal pQuery As Boolean, ByVal pDelete As Boolean, ByVal pSave As Boolean, ByVal pLastRecord As Boolean, ByVal pNextRecord As Boolean, ByVal pPrevious As Boolean, ByVal pFirstRecord As Boolean, ByVal pGoto As String, ByVal pPrint As Boolean, Optional ByVal pFind As Boolean = False)
        ToolBar_Main.Tools("Btn_New").SharedProps.Enabled = pNew
        ToolBar_Main.Tools("Btn_Query").SharedProps.Enabled = pQuery
        ToolBar_Main.Tools("Btn_Delete").SharedProps.Enabled = pDelete
        ToolBar_Main.Tools("Btn_Save").SharedProps.Enabled = pSave
        ToolBar_Main.Tools("Btn_LastRecord").SharedProps.Enabled = pLastRecord
        ToolBar_Main.Tools("Btn_NextRecord").SharedProps.Enabled = pNextRecord
        ToolBar_Main.Tools("Btn_PreviousRecord").SharedProps.Enabled = pPrevious
        ToolBar_Main.Tools("Btn_FirstRecord").SharedProps.Enabled = pFirstRecord
        ToolBar_Main.Tools("Btn_Print").SharedProps.Enabled = pPrint
        ToolBar_Main.Tools("Btn_Find").SharedProps.Enabled = pFind
    End Sub

    Private Sub sLoadExplorer()
        'If Trim(cBase.fReturnScalar("Select IsAdmin From Users Where Code = '" & vUsrCode & "'", Me.Name)) = "Y" Then
        Dim vSqlCommand As New SqlCommand
        vSqlCommand.Connection = cBase.vSqlConn
        vSqlCommand.CommandText = " Select Sys_Code, Code, DescA From System_Modules "
        cBase.vSqlConn.Open()
        Dim vCString = cBase.vSqlConn.ConnectionString
        Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
        While vReader.Read
            Dim vint As Integer = vReader.RecordsAffected
            If IsDBNull(vReader(0)) = False Then
                If Trim(vReader(0)) = "RT" Then
                    If vReader.IsDBNull(1) = False And vReader.IsDBNull(2) = False Then
                        Exp_Main.Groups("Grp_Reports").Items.Add(Trim(vReader(1)), Trim(vReader(2)))
                    End If
                ElseIf Trim(vReader(0)) = "HR" Then
                    If vReader.IsDBNull(1) = False And vReader.IsDBNull(2) = False Then
                        Exp_Main.Groups("Grp_HR").Items.Add(Trim(vReader(1)), Trim(vReader(2)))
                    End If
                ElseIf Trim(vReader(0)) = "GN" Then
                    If vReader.IsDBNull(1) = False And vReader.IsDBNull(2) = False Then
                        Exp_Main.Groups("Grp_Settings").Items.Add(Trim(vReader(1)), Trim(vReader(2)))
                    End If
                ElseIf Trim(vReader(0)) = "PR" Then
                    If vReader.IsDBNull(1) = False And vReader.IsDBNull(2) = False Then
                        Exp_Main.Groups("Grp_Purchasing").Items.Add(Trim(vReader(1)), Trim(vReader(2)))
                    End If
                ElseIf Trim(vReader(0)) = "SL" Then
                    If vReader.IsDBNull(1) = False And vReader.IsDBNull(2) = False Then
                        Exp_Main.Groups("Grp_Sales").Items.Add(Trim(vReader(1)), Trim(vReader(2)))
                    End If
                ElseIf Trim(vReader(0)) = "IN" Then
                    If vReader.IsDBNull(1) = False And vReader.IsDBNull(2) = False Then
                        Exp_Main.Groups("Grp_Inventory").Items.Add(Trim(vReader(1)), Trim(vReader(2)))
                    End If
                ElseIf Trim(vReader(0)) = "FN" Then
                    If vReader.IsDBNull(1) = False And vReader.IsDBNull(2) = False Then
                        Exp_Main.Groups("Grp_Financial").Items.Add(Trim(vReader(1)), Trim(vReader(2)))
                    End If
                End If
            End If
        End While
        cBase.vSqlConn.Close()
        Dim vItem As Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem
        'Load Images
        For Each vItem In Exp_Main.Groups("Grp_HR").Items
            If vItem.Key = "FRM_Users" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(56)
            End If
            If vItem.Key = "FRM_AddDed" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(44)
            End If
            If vItem.Key = "FRM_Leaves" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(16)
            End If
        Next
        'For Each vItem In Exp_Main.Groups("Grp_Reports").Items
        '    If vItem.Key = "FRM_AttendanceReport" Then
        '        vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(55)
        '        'ElseIf vItem.Key = "FRM_ItemSetup" Then
        '        '    vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(28)
        '    End If
        'Next
        For Each vItem In Exp_Main.Groups("Grp_Settings").Items
            If vItem.Key = "FRM_GeneralSettings" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(27)
            End If
            If vItem.Key = "FRM_FinancialSetup" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(44)
            End If
            If vItem.Key = "FRM_HR_Definition" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(26)
            End If
            If vItem.Key = "FRM_ItemSetup" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(27)
            End If
        Next
        For Each vItem In Exp_Main.Groups("Grp_Purchasing").Items
            If vItem.Key = "FRM_PurchaseInvoice" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(56)
            End If
            If vItem.Key = "Frm_PurchaseReturn" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(55)
            End If
        Next
        For Each vItem In Exp_Main.Groups("Grp_Sales").Items
            If vItem.Key = "Frm_SalesInvoice" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(56)
            End If
            If vItem.Key = "Frm_SalesReturn" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(55)
            End If
        Next
        For Each vItem In Exp_Main.Groups("Grp_Inventory").Items
            If vItem.Key = "Frm_Count" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(57)
            End If
        Next
        For Each vItem In Exp_Main.Groups("Grp_Financial").Items
            If vItem.Key = "Frm_FinancialAdjustment" Then
                vItem.Settings.AppearancesSmall.Appearance.Image = ImgLst.Images(44)
            End If
        Next
    End Sub

    Public Sub sPrintRec(ByVal pRecPos As String)
        Dim vTextBoxTool As Infragistics.Win.UltraWinToolbars.TextBoxTool
        vTextBoxTool = ToolBar_Main.Tools("Btn_GotoRecord")
        vTextBoxTool.Text = pRecPos
    End Sub

End Class