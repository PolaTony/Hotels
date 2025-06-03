Imports Infragistics.Win.Misc
Imports System.Data.SqlClient

Public Class Frm_GeneralTiles_L
    Dim vcFrmLevel As New cFrmLevelVariables_L
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
        Me.Text = "Welcome " & vUsrName

        sLoadTiles()
    End Sub

    Private Sub Frm_GeneralTiles_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
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
                    vSqlString = " Insert Into Employees_Large_Tiles (      Emp_Code,            Mod_Code ) " & _
                                 "                            Values ('" & vUsrCode & "', '" & vTile.Name & "' )"

                    sFillSqlStatmentArray(vSqlString)
                End If
            End If
        Next

        Dim vRowCounter As Integer = cControls.fSendData(vSqlStatment, Me.Name)
    End Sub

    Private Sub AllButtons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles Btn_CustomersSettings.Click, ee.Click, UltraButton1.Click, UltraButton6.Click, _
            UltraButton5.Click, UltraButton15.Click, UltraButton7.Click, UltraButton16.Click, _
            UltraButton17.Click, btn_233.Click, UltraButton9.Click, UltraButton11.Click, _
            UltraButton12.Click, UltraButton13.Click, UltraButton14.Click, UltraButton4.Click, _
            UltraButton2.Click, UltraButton3.Click, UltraButton8.Click, Btn_1.Click, UltraButton10.Click, _
            UltraButton19.Click, UltraButton20.Click, UltraButton21.Click, UltraButton22.Click, _
            UltraButton23.Click, UltraButton24.Click, UltraButton25.Click, UltraButton26.Click, _
            UltraButton27.Click, UltraButton28.Click, UltraButton29.Click, UltraButton30.Click, _
            UltraButton31.Click, UltraButton32.Click, UltraButton33.Click, UltraButton18.Click, _
            UltraButton34.Click, UltraButton35.Click


        vcFrmLevel.vParentFrm.sOpenForm(sender.Parent.Name)

    End Sub

    Private Sub sLoadTiles()
        Try

            Dim vTile As UltraTile
            For Each vTile In UltraTilePanel1.Tiles
                vTile.Visible = False
            Next

            Dim vSqlCommand As New SqlCommand
            vSqlCommand.Connection = cControls.vSqlConn

            vSqlCommand.CommandText = " Select Profiles_Systems_Modules.Mod_Code  " & _
                                      " From System_Modules INNER JOIN Profiles_Systems_Modules " & _
                                      " ON   System_Modules.Code = Profiles_Systems_Modules.Mod_Code " & _
                                      " INNER JOIN Profiles " & _
                                      " On Profiles.Code = Profiles_Systems_Modules.Prf_Code " & _
                                      " Inner Join Employees " & _
                                      " On Employees.Profile = Profiles.Code " & _
                                      " Where Employees.Code = " & vUsrCode & _
                                      " And   Enabled  = 'Y' " & _
                                      " And   IsNull(System_Modules.IsActive, 'Y') = 'Y' " & _
                                      " And   IsRibbon = 'Y'"
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader

            Do While vSqlReader.Read
                UltraTilePanel1.Tiles(Trim(vSqlReader(0))).Visible = True
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            sLoadLargeTiles()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sLoadLargeTiles()
        Dim vSQlcommand As New SqlCommand
        vSQlcommand.CommandText = _
        " Select Mod_Code From Employees_Large_Tiles " & _
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
End Class