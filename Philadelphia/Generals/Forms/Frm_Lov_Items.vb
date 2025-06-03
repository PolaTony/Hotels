Imports System.Data.Sqlclient

Public Class Frm_LovItems

    Private Sub Frm_LovItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sLoadMainCategories()
    End Sub

    Private Sub sLoadMainCategories()
        Dim vTreeNode As TreeNode
        Dim vSqlCommand As New SqlCommand
        vSqlCommand.Connection = cBase.vSqlConn
        vSqlCommand.CommandText = _
        " Select Code, DescA, Remarks From Categories" & _
        " Where Parent_Code Is Null "
        cBase.vSqlConn.Open()
        Tre_Category.Nodes.Clear()
        vTreeNode = Tre_Category.Nodes.Add("All", "«·ﬂ·")
        vTreeNode.ForeColor = Color.OrangeRed
        vTreeNode.BackColor = Color.Silver
        Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
        Dim vCount As Integer = vReader.RecordsAffected
        While vReader.Read
            If vReader.IsDBNull(0) = False Then
                vTreeNode = Tre_Category.Nodes.Add(Trim(vReader(0)), Trim(vReader(1)))
                vTreeNode.ForeColor = Color.Blue
                vTreeNode.Tag = "N"
            End If
        End While
        cBase.vSqlConn.Close()
        vReader.Close()

        For Each vTreeNode In Tre_Category.Nodes
            If cBase.fCount_Rec(" From Categories " & _
               " Where Parent_Code = '" & vTreeNode.Name & "'") > 0 Then

                vTreeNode = sLoadChildCategories(vTreeNode)
            End If
        Next
        'If vExpandedType = True Then
        '    Tre_Category.ExpandAll()
        'Else
        '    Tre_Category.CollapseAll()
        'End If
        'Tre_Category.ForeColor = vTreeColor
    End Sub
    Private Function sLoadChildCategories(ByVal pTreeNode As TreeNode) As TreeNode
        Dim vTreeNode As TreeNode
        Dim vNewTreeNode As TreeNode
        Dim vSqlCommand As New SqlCommand
        vSqlCommand.Connection = cBase.vSqlConn
        vSqlCommand.CommandText = _
        " Select Code, DescA, Remarks From Categories" & _
        " Where Parent_Code = '" & pTreeNode.Name & "'"

        cBase.vSqlConn.Open()
        Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
        Dim vCount As Integer = vReader.RecordsAffected
        While vReader.Read
            If vReader.IsDBNull(0) = False Then
                vNewTreeNode = pTreeNode.Nodes.Add(Trim(vReader(0)), Trim(vReader(1)))
                vNewTreeNode.ForeColor = Color.Blue
                vNewTreeNode.Tag = "N"
            End If
        End While
        cBase.vSqlConn.Close()
        vReader.Close()

        For Each vTreeNode In pTreeNode.Nodes
            If cBase.fCount_Rec(" From Categories " & _
               " Where Parent_Code = '" & vTreeNode.Name & "'") > 0 Then

                vTreeNode = sLoadChildCategories(vTreeNode)
            End If
        Next
        Return vTreeNode
    End Function

    Private Sub sQueryMainItems(Optional ByVal pCode As String = "", Optional ByVal pDesc As String = "")

        Dim vTreeNode As TreeNode
        Dim vSqlCommand As New SqlCommand
        Dim vCategoryFilter, vCodeFilter, vDescFilter As String
        If Tre_Category.SelectedNode.Name = "All" Then
            vCategoryFilter = ""
        Else
            vCategoryFilter = " And Cat_Code = '" & Tre_Category.SelectedNode.Name & "'"
        End If

        If pCode = "" Then
            vCodeFilter = ""
        Else
            vCodeFilter = " And Items.Code Like '%" & pCode & "%'"
        End If

        If pDesc = "" Then
            vDescFilter = ""
        Else
            vDescFilter = " And Items.DescA Like '%" & pDesc & "%'"
        End If
        vSqlCommand.Connection = cBase.vSqlConn
        vSqlCommand.CommandText = _
        " Select Code, DescA From Items " & _
        " Where 1 = 1 " & _
        vCategoryFilter & _
        vCodeFilter & _
        vDescFilter & _
        " Order By Code "

        cBase.vSqlConn.Open()
        DTS_Main.Rows.Clear()
        Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
        Dim vCount As Integer
        While vReader.Read
            If vReader.IsDBNull(0) = False Then
                DTS_Main.Rows.SetCount(vCount + 1)
                DTS_Main.Rows(vCount)("Code") = vReader(0)
                DTS_Main.Rows(vCount)("DescA") = vReader(1)
                'vTreeNode.ForeColor = Color.Blue
            End If
            vCount += 1
        End While
        cBase.vSqlConn.Close()
        vReader.Close()

        'For Each vTreeNode In Tre_Items.Nodes
        '    If cBase.fCount_Rec(" From Items " & _
        '       " Where Code = '" & vTreeNode.Name & "' And Ser <> '00' ") > 0 Then

        '        vTreeNode.ForeColor = Color.Blue
        '        vTreeNode = sQueryChildItems(vTreeNode)
        '    Else
        '        vTreeNode.ForeColor = Color.Black
        '    End If
        'Next
    End Sub

    Private Function sQueryChildItems(ByVal pTreeNode As TreeNode) As TreeNode
        Dim vTreeNode As TreeNode
        Dim vNewTreeNode As TreeNode
        Dim vSqlCommand As New SqlCommand
        vSqlCommand.Connection = cBase.vSqlConn
        vSqlCommand.CommandText = _
        " Select Ser, DescA, Price From Items" & _
        " Where Code = '" & pTreeNode.Name & "'" & _
        " Order By Code "

        cBase.vSqlConn.Open()
        Dim vReader As SqlDataReader = vSqlCommand.ExecuteReader
        Dim vCount As Integer = vReader.RecordsAffected
        While vReader.Read
            If vReader.IsDBNull(0) = False Then
                vNewTreeNode = pTreeNode.Nodes.Add(Trim(vReader(0)), Trim(vReader(1)))
                vNewTreeNode.ForeColor = Color.Black
                If vReader.IsDBNull(2) = False Then
                    vNewTreeNode.ToolTipText = " «·”⁄— " & Trim(vReader(2))
                End If
            End If
        End While
        cBase.vSqlConn.Close()
        vReader.Close()

        'For Each vTreeNode In pTreeNode.Nodes
        '    If cBase.fCount_Rec(" From Items " & _
        '       " Where Code = '" & vTreeNode.Name & "'") > 0 Then

        '        vTreeNode = sQueryChildItems(vTreeNode)
        '    End If
        'Next
        Return vTreeNode
    End Function

    Private Sub Txt_FndByAll_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_FndByCode.ValueChanged, Txt_FndByDesc.ValueChanged
        sQueryMainItems(Txt_FndByCode.Text, Txt_FndByDesc.Text)
    End Sub

    Private Sub Tre_Category_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Tre_Category.AfterSelect
        sQueryMainItems(Txt_FndByCode.Text, Txt_FndByDesc.Text)
    End Sub

    Private Sub Tre_Items_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If Grd_Main.Selected.Rows.Count = 1 Then
            vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
            VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
            Me.Close()
        Else
            MessageBox.Show(" √ﬂœ „‰ «Œ Ì«— ’‰› √Ê·«")
        End If
    End Sub

    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        If Grd_Main.Selected.Rows.Count = 1 Then
            vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
            VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
            Me.Close()
        Else
            MessageBox.Show(" √ﬂœ „‰ «Œ Ì«— ’‰› √Ê·«")
        End If
    End Sub

    Private Sub Grd_Main_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Main.KeyUp
        If e.KeyData = Keys.Enter Then
            If Grd_Main.Selected.Rows.Count = 1 Then
                vLovReturn1 = Grd_Main.Selected.Rows(0).Cells("Code").Text
                VLovReturn2 = Grd_Main.Selected.Rows(0).Cells("DescA").Text
                Me.Close()
            Else
                MessageBox.Show(" √ﬂœ „‰ «Œ Ì«— ’‰› √Ê·«")
            End If
        End If
    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub
End Class