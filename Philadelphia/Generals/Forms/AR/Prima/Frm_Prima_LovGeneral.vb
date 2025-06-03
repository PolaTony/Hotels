Imports System.Data.SqlClient
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_Prima_LovGeneral
    Dim vSqlString As String
    Dim vTableName As String
    Dim vAdditionalString As String

    Public Sub New(ByVal pSqlString As String, ByVal pTitle As String, Optional ByVal pTableName As String = "", Optional ByVal pAdditionalString As String = "")
        InitializeComponent()
        vSqlString = pSqlString
        Me.Text = pTitle
        Dim LabelTool3 As Infragistics.Win.UltraWinToolbars.LabelTool
        LabelTool3 = ToolBar_Main.Tools("LabelTool1")
        LabelTool3.SharedProps.Caption = pTitle
        vTableName = pTableName
        vAdditionalString = pAdditionalString
    End Sub
    Private Sub FRM_LovTreeL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ToolBar_Main.Tools("LabelTool1").SharedProps.Caption = "«·√÷«›«  Ê«·Œ’Ê„« " Then
            sLoadAddDed()
        Else
            If ToolBar_Main.Tools("LabelTool1").SharedProps.Caption <> "«·⁄„·«¡" Then
                sLoadMainNodes()
            End If
        End If

    End Sub

    Private Sub sLoadMainNodes(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            If ToolBar_Main.Tools("LabelTool1").SharedProps.Caption = "«·⁄„·«¡" Then
                If pCode = "" And pDesc = "" Then
                    DTS_Main.Rows.Clear()
                    Return
                End If

                If pDesc.Length < 2 And pCode = "" Then
                    Return
                End If
            End If

            Dim vResult As String
            If ToolBar_Main.Tools("LabelTool1").SharedProps.Caption = "«·Œ“‰" Then
                'Here I Check if all branches is Enabled or no...

                vResult = cControls.fReturnValue(" Select Enabled " & _
                                                   " From   Controls_Profiles INNER JOIN Employees         " & _
                                                   " ON     Employees.Profile = Controls_Profiles.Prf_Code " & _
                                                   " Where  Employees.Code = '" & vUsrCode & "'            " & _
                                                   " And    Mod_Code       = 'GN'                          " & _
                                                   " And    Type           = 'Ctrl'                        " & _
                                                   " And    Ctrl_Code      = 'Ctrl_AllBranches'            ", Me.Name)

                If vResult = "Y" Then
                    vSqlString = " Select Code, DescA           " & _
                                 " From Boxes                   " & _
                                 " Where 1 = 1                  "

                Else
                    vSqlString = " Select Box_Code, Boxes.DescA " & _
                             " FROM Employees Inner Join Boxes  " & _
                             " ON   Boxes.Code = Employees.Box_Code " & _
                             " Where 1 = 1                  " & _
                             " And  Employees.Code = '" & vUsrCode & "'"
                End If
            ElseIf ToolBar_Main.Tools("LabelTool1").SharedProps.Caption = "«·›—Ê⁄" Then
                'Here I Check if all branches is Enabled or no...

                'vResult = cControls.fReturnValue(" Select Enabled " & _
                '" From   Controls_Profiles INNER JOIN Employees         " & _
                '" ON     Employees.Profile = Controls_Profiles.Prf_Code " & _
                '" Where  Employees.Code = '" & vUsrCode & "'            " & _
                '" And    Mod_Code       = 'GN'                          " & _
                '" And    Type           = 'Ctrl'                        " & _
                '" And    Ctrl_Code      = 'Ctrl_AllBranches'            ", Me.Name)

                'If vResult = "Y" Then
                vSqlString = " Select Code, DescA           " & _
                             " From Branches                   " & _
                             " Where 1 = 1                  "

                'Else
                'vSqlString = " Select Branches.Code, Branches.DescA " & _
                '         " FROM Employees Inner Join Branches  " & _
                '         " ON   Branches.Code = Employees.Branch " & _
                '         " Where 1 = 1                  " & _
                '         " And  Employees.Code = '" & vUsrCode & "'"
                'End If
            ElseIf ToolBar_Main.Tools("LabelTool1").SharedProps.Caption = "«·„Œ«“‰" Then
                'Here I Check if all branches is Enabled or no...

                vResult = cControls.fReturnValue(" Select Enabled " & _
                                                   " From   Controls_Profiles INNER JOIN Employees         " & _
                                                   " ON     Employees.Profile = Controls_Profiles.Prf_Code " & _
                                                   " Where  Employees.Code = '" & vUsrCode & "'            " & _
                                                   " And    Mod_Code       = 'GN'                          " & _
                                                   " And    Type           = 'Ctrl'                        " & _
                                                   " And    Ctrl_Code      = 'Ctrl_AllBranches'            ", Me.Name)

                If vResult = "Y" Then
                    vSqlString = " Select Code, DescA           " & _
                                 " From Stores                   " & _
                                 " Where 1 = 1                  "

                Else
                    vSqlString = " Select Stores.Code, Stores.DescA " & _
                             " FROM Branches INNER JOIN Stores                     " & _
                             " ON   Stores.Company_Code = Branches.Code  " & _
                             " Where 1 = 1                           " & _
                             " And  Branches.Code = '" & vCompanyCode & "'"
                End If
            ElseIf ToolBar_Main.Tools("LabelTool1").SharedProps.Caption = "«·Êﬂ·«¡" Then
                'Here I Check if all branches is Enabled or no...

                vResult = cControls.fReturnValue(" Select Enabled " & _
                                                   " From   Controls_Profiles INNER JOIN Employees         " & _
                                                   " ON     Employees.Profile = Controls_Profiles.Prf_Code " & _
                                                   " Where  Employees.Code = '" & vUsrCode & "'            " & _
                                                   " And    Mod_Code       = 'GN'                          " & _
                                                   " And    Type           = 'Ctrl'                        " & _
                                                   " And    Ctrl_Code      = 'Ctrl_AllBranches'            ", Me.Name)

                If vResult = "Y" Then
                    vSqlString = " Select Code, DescA From Employees Where IsSalesman = 'Y' "


                Else
                    vSqlString = " Select Employees.Code, Employees.DescA  " & _
                             " FROM Employees INNER JOIN Branches    " & _
                             " ON   Branches.Code = Employees.Branch " & _
                             " Where 1 = 1                           " & _
                             " And IsSalesman = 'Y'                  " & _
                             " And  Branches.Code = '" & vCompanyCode & "'"
                End If
            End If

            Dim vCodeFilter, vDescFilter As String
            If pCode = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Code Like '%" & pCode & "%'"
            End If
            If pDesc = "" Then
                vDescFilter = ""
            Else
                vDescFilter = " And DescA Like '%" & pDesc & "%'"
            End If

            Dim vSqlCommand As New SqlCommand
            vSqlCommand.Connection = cControls.vSqlConn
            vSqlCommand.CommandText = _
            vSqlString & _
            vCodeFilter & _
            vDescFilter

            cControls.vSqlConn.Open()
            DTS_Main.Rows.Clear()
            Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
            Dim vRowCounter As Integer
            While vReader.Read
                DTS_Main.Rows.SetCount(vRowCounter + 1)
                If vReader.IsDBNull(0) = False Then
                    DTS_Main.Rows(vRowCounter)("Code") = Trim(vReader(0))
                    DTS_Main.Rows(vRowCounter)("DescA") = Trim(vReader(1))
                End If
                vRowCounter += 1
            End While
            cControls.vSqlConn.Close()
            vReader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try


        'If vAdditionalString <> "" Then
        '    For Each vTreeNode In Tre_Adjust.Nodes
        '        If cBase.fCount_Rec(" From " & vTableName & _
        '           " Where Parent_Code = '" & vTreeNode.Name & "'") > 0 Then

        '            vTreeNode = sLoadChildNodes(vTreeNode)
        '        End If
        '    Next
        'End If

    End Sub

    Private Sub sLoadAddDed(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")
        Try
            Dim vCodeFilter, vDescFilter As String
            If pCode = "" Then
                vCodeFilter = ""
            Else
                vCodeFilter = " And Code Like '%" & pCode & "%'"
            End If
            If pDesc = "" Then
                vDescFilter = ""
            Else
                vDescFilter = " And DescA Like '%" & pDesc & "%'"
            End If

            Dim vSqlCommand As New SqlCommand
            vSqlCommand.Connection = cControls.vSqlConn
            vSqlCommand.CommandText = _
            vSqlString & _
            vCodeFilter & _
            vDescFilter

            cControls.vSqlConn.Open()
            DTS_Main.Rows.Clear()
            Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
            Dim vRowCounter As Integer
            While vReader.Read
                DTS_Main.Rows.SetCount(vRowCounter + 1)
                If vReader.IsDBNull(0) = False Then
                    DTS_Main.Rows(vRowCounter)("Code") = Trim(vReader(0))
                    DTS_Main.Rows(vRowCounter)("DescA") = Trim(vReader(1))
                    DTS_Main.Rows(vRowCounter)("Type") = Trim(vReader(2))
                End If
                vRowCounter += 1
            End While
            cControls.vSqlConn.Close()
            vReader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try

    End Sub
    Private Function sLoadChildNodes(ByVal pTreeNode As TreeNode) As TreeNode
        Try
            Dim vTreeNode As TreeNode
            Dim vNewTreeNode As TreeNode
            Dim vSqlCommand As New SqlCommand
            vSqlCommand.Connection = cControls.vSqlConn
            vSqlCommand.CommandText = vAdditionalString & pTreeNode.Name & "'"

            cControls.vSqlConn.Open()
            Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
            Dim vCount As Integer = vReader.RecordsAffected
            While vReader.Read
                If vReader.IsDBNull(0) = False Then
                    vNewTreeNode = pTreeNode.Nodes.Add(Trim(vReader(0)), Trim(vReader(1)))
                    'vNewTreeNode.ForeColor = Color.Blue
                    vNewTreeNode.Tag = "N"
                End If
            End While
            cControls.vSqlConn.Close()
            vReader.Close()

            For Each vTreeNode In pTreeNode.Nodes
                If cControls.fCount_Rec(" From " & vTableName & _
                     " Where Parent_Code = '" & vTreeNode.Name & "'") > 0 Then

                    vTreeNode = sLoadChildNodes(vTreeNode)
                End If
            Next
            Return vTreeNode
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            cControls.vSqlConn.Close()
        End Try

    End Function

    Private Sub Tre_Adjust_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd_Main.DoubleClick
        Try
            If Grd_Main.Selected.Rows.Count = 1 Then
                vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
                VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
                vLovReturn3 = Grd_Main.Selected.Rows(0).Cells("Type").Text
                Me.Close()
            Else
                MessageBox.Show(" √ﬂœ „‰ «·«Œ Ì«— √Ê·«")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Tre_Adjust_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Main.KeyUp
        Try
            If e.KeyData = Keys.Enter Then
                If Grd_Main.Selected.Rows.Count = 1 Then
                    vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
                    VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
                    vLovReturn3 = Grd_Main.Selected.Rows(0).Cells("Type").Text
                    Me.Close()

                ElseIf Grd_Main.Selected.Rows.Count > 1 Then
                    For Each vRow As UltraGridRow In Grd_Main.Selected.Rows
                        vSelectedSortedList_1.Add(vRow.Cells("Code").Text, vRow.Cells("DescA").Text)
                    Next
                    Me.Close()

                Else
                    MessageBox.Show(" √ﬂœ „‰ «·«Œ Ì«— √Ê·«")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        Try
            vSelectedSortedList_1.Clear()

            If Grd_Main.Selected.Rows.Count = 1 Then
                vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
                VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
                vLovReturn3 = Grd_Main.Selected.Rows(0).Cells("Type").Text

                Me.Close()
            ElseIf Grd_Main.Selected.Rows.Count > 1 Then

                For Each vRow As UltraGridRow In Grd_Main.Selected.Rows
                    vSelectedSortedList_1.Add(vRow.Cells("Code").Text, vRow.Cells("DescA").Text)
                Next

                Me.Close()
            Else
                MessageBox.Show(" √ﬂœ „‰ «·«Œ Ì«— √Ê·«")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        vSelectedSortedList_1.Clear()

        Me.Close()
    End Sub

    Private Sub Txt_FndByCode_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged
        If ToolBar_Main.Tools("LabelTool1").SharedProps.Caption = "«·√÷«›«  Ê«·Œ’Ê„« " Then
            sLoadAddDed(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
        Else
            sLoadMainNodes(Trim(Txt_FndByCode.Text), Trim(Txt_FndByDesc.Text))
        End If
    End Sub
End Class