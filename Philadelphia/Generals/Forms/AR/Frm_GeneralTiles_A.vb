Imports Infragistics.Win.Misc
Imports System.Data.SqlClient
Imports System.Drawing.Text
Imports System.IO

Public Class Frm_GeneralTiles_A
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSqlStatment(0) As String

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
    Private Sub Frm_GeneralTiles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        vcFrmLevel.vParentFrm = Me.ParentForm
        Me.Text = vUsrName & "  مرحبا "

        'UltraTilePanel1.Font = New Font(fonts.Families(0), 12)

        sLoadTiles()
        'sLoadMostUsed()
    End Sub

    Private Sub Frm_GeneralTiles_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm
        sHide_ToolbarMain_Tools()

        'Me.Text = vUsrName & "  مرحبا "

        'sLoadTiles()
    End Sub

    Private Sub GeneralTiles_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        sEmptySqlStatmentArray()

        Dim vTile As UltraTile
        Dim vSqlString As String

        vSqlString = "Delete From Employees_Large_Tiles Where Emp_Code = '" & vUsrCode & "' "

        sFillSqlStatmentArray(vSqlString)

        For Each vTile In UltraTilePanel1.Tiles
            If vTile.Visible Then
                If vTile.State = TileState.Large Then
                    vSqlString = " Insert Into Employees_Large_Tiles (      Emp_Code,            Mod_Code ) " &
                                 "                            Values ('" & vUsrCode & "', '" & vTile.Name & "' )"

                    sFillSqlStatmentArray(vSqlString)
                End If
            End If
        Next

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
    End Sub

    Private Sub AllButtons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Button.Click, UltraButton10.Click, UltraButton25.Click, UltraButton34.Click,
            UltraButton35.Click, UltraButton3.Click

        vcFrmLevel.vParentFrm.sLoadExplorer(sender.Parent.Name)

    End Sub

    Private Sub sLoadTiles()
        Try

            Dim vTile As UltraTile
            For Each vTile In UltraTilePanel1.Tiles
                vTile.Visible = False
            Next

            Dim vSqlCommand As New SqlCommand
            vSqlCommand.Connection = cControls.vSqlConn

            If vIsAdmin = "Y" Then
                vSqlCommand.CommandText = " Select Sections.Code, " &
                                          "        IsNull(Sections.Desc" & vLang & ", 'UnKnown') " &
                                          "                                              " &
                                          " From Sections       " &
                                          "                                              " &
                                          " Where 1 = 1 " &
                                          " And   IsNULL(Sections.IsActive, 'Y') = 'Y' "
            Else
                vSqlCommand.CommandText = " Select Profiles_Systems.Sys_Code  " & vbCrLf &
                                      " From Systems INNER JOIN Profiles_Systems " & vbCrLf &
                                      " ON   Systems.Code = Profiles_Systems.Sys_Code " & vbCrLf &
                                      "                      " & vbCrLf &
                                      " INNER JOIN Employees " & vbCrLf &
                                      " On Employees.Code = Profiles_Systems.Emp_Code " & vbCrLf &
                                      "                        " & vbCrLf &
                                      " Where Employees.Code = " & vUsrCode & vbCrLf &
                                      " And   Enabled = 'Y' " & vbCrLf &
                                      " And   IsNull(Systems.IsActive, 'Y') = 'Y' "
            End If

            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader

            Do While vSqlReader.Read
                UltraTilePanel1.Tiles(Trim(vSqlReader(0))).Visible = True
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            'sLoadLargeTiles()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sLoadLargeTiles()
        Dim vSQlcommand As New SqlCommand
        vSQlcommand.CommandText =
        " Select Mod_Code From Employees_Large_Tiles " &
        " Where Emp_Code = '" & vUsrCode & "' "

        vSQlcommand.Connection = cControls.vSqlConn
        cControls.vSqlConn.Open()
        Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
        Do While vSqlReader.Read
            UltraTilePanel1.Tiles(Trim(vSqlReader(0))).State = TileState.Large
        Loop

        cControls.vSqlConn.Close()
        vSqlReader.Close()
    End Sub

    Private Sub sLoadMostUsed()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
              " Select Top 5 [Employees_Open_Form_Log].Mod_Code,            " & vbCrLf &
              " System_Modules.DescA as Mod_Desc                            " & vbCrLf &
              " From [dbo].[Employees_Open_Form_Log] INNER JOIN Employees   " & vbCrLf &
              " ON   Employees.Code =  [Employees_Open_Form_Log].Emp_Code   " & vbCrLf &
              "                                                             " & vbCrLf &
              " INNER JOIN Profiles_Systems_Modules                         " & vbCrLf &
              " ON Employees.Code = Profiles_Systems_Modules.Emp_Code        " & vbCrLf &
              " And [Employees_Open_Form_Log].Mod_Code = Profiles_Systems_Modules.Mod_Code " & vbCrLf &
              "                                                             " & vbCrLf &
              " INNER JOIN System_Modules                                   " & vbCrLf &
              " ON System_Modules.Code = Profiles_Systems_Modules.Mod_Code  " & vbCrLf &
              " Where [Employees_Open_Form_Log].Emp_Code = '" & vUsrCode & "' " & vbCrLf &
              " And   Profiles_Systems_Modules.Enabled = 'Y'                " & vbCrLf &
              " Group By [Employees_Open_Form_Log].Mod_Code, System_Modules.DescA   " & vbCrLf &
              " Order By Count(*) Desc                                      "


            vsqlCommand.Connection = cControls.vSqlConn

            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Exp_MostUsed.Groups(0).Items.Clear()

            Do While vSqlReader.Read
                Exp_MostUsed.Groups(0).Items.Add(vSqlReader(0), vSqlReader(1))
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Exp_MostUsed_ItemClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinExplorerBar.ItemEventArgs) Handles Exp_MostUsed.ItemClick
        vcFrmLevel.vParentFrm.sOpenForm(e.Item.Key)
    End Sub

    Private Sub TileControl1_ItemClick(sender As Object, e As DevExpress.XtraEditors.TileItemEventArgs)
        If e.Item.Name = "TileItem1" Then
            Exit Sub
        End If

        'I Check First if the user has the permission to open the Screen
        Dim vEnabled As String = cControls.fReturnValue(" Select Enabled From Profiles_Systems_Modules " &
                                                                " INNER  JOIN Employees                        " &
                                                                " ON     Employees.Profile = Profiles_Systems_Modules.Prf_Code " &
                                                                " And    Employees.Company_Code = Profiles_Systems_Modules.Company_Code " &
                                                                "                                                 " &
                                                                " Where  Mod_Code = '" & e.Item.Name & "'       " &
                                                                " And    Employees.Company_Code = " & vCompanyCode &
                                                                " And    Employees.Code = '" & vUsrCode & "'   ", Me.Name)

        If vEnabled <> "Y" Then
            vcFrmLevel.vParentFrm.sForwardMessage("131", Me)
            Exit Sub
        End If

        vcFrmLevel.vParentFrm.sOpenForm(e.Item.Name)
    End Sub
    Private Sub sHide_ToolbarMain_Tools()
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_New").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Save").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Delete").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Print").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_NextRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_PreviousRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_LastRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_FirstRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_CloseWindow").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_GotoRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_ChangeUser").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Themes").SharedProps.Visible = False
    End Sub
End Class