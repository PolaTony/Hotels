Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_HK_Count_A

#Region " Declaration                                                       "
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSQlStatment(0) As String
    Dim vMasterBlock As String = "NI"
#End Region
#Region " Form Level                                                        "
#Region " My Form                                                           "
    Private Sub sFillSqlStatmentArray(ByVal pSqlstring As String)
        If vSQlStatment(UBound(vSQlStatment)) = "" Then
            vSQlStatment(UBound(vSQlStatment)) = pSqlstring
        Else
            ReDim Preserve vSQlStatment(UBound(vSQlStatment) + 1)
            vSQlStatment(UBound(vSQlStatment)) = pSqlstring
        End If
    End Sub
    Private Sub sEmptySqlStatmentArray()
        ReDim Preserve vSQlStatment(0)
        vSQlStatment(0) = ""
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
    Private Sub Frm_Count_A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sLoad_Items()
    End Sub
    Private Sub Frm_Count_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm
        vcFrmLevel.vParentFrm.sEnableTools(False, False, False, True, False, False, False, False, "", True, False, "التفاصيل")

        sHide_ToolbarMain_Tools()
    End Sub
    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

#End Region
#End Region

#Region " Master                                                               "

#End Region
#Region " Details                                                              "
#Region " DataBase                                                             "
#Region " Save                                                                 "
    Public Function fValidate_Save() As Boolean

        Grd_Items.UpdateData()

        If Txt_TDate.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("53", Me)
            Txt_TDate.Select()
            Return False
        End If

        Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow

        For Each vRow In Grd_Items.Rows

            'If IsDBNull(vRow.Cells("Current_Balance").Value) Then
            '    vcFrmLevel.vParentFrm.sForwardMessage("189", Me)
            '    vRow.Cells("Current_Balance").Selected = True
            '    Return False
            'End If

            If vRow.Cells("Current_Balance").Text <> "" Then
                If vRow.Cells("Current_Balance").Value < vRow.Cells("Balance").Value Then 'I will check for the lost and consumed only if the current balance is less than the balance
                    If CDec(fIsNull(vRow.Cells("Balance").Value, 0)) <> CDec(fIsNull(vRow.Cells("Current_Balance").Value, 0)) + CDec(fIsNull(vRow.Cells("Consumed").Value, 0)) + CDec(fIsNull(vRow.Cells("Lost").Value, 0)) Then
                        vcFrmLevel.vParentFrm.sForwardMessage("192", Me)
                        vRow.Cells("Current_Balance").Selected = True
                        Return False
                    End If
                End If
            End If
        Next

        Return True
    End Function
#End Region
#End Region
#Region " Form Level                                                           "
    Private Sub Grd_Items_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Grd_Items.KeyPress
        If Grd_Items.ActiveCell IsNot Nothing Then

            If Grd_Items.ActiveCell.Column.Key = "Balance" OrElse Grd_Items.ActiveCell.Column.Key = "Consumed" OrElse Grd_Items.ActiveCell.Column.Key = "Lost" Then

                ' Allow digits, control characters (like Backspace), and one dot
                If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
                    e.Handled = True ' Cancel the keypress
                End If

                ' Prevent more than one dot
                If e.KeyChar = "." AndAlso Grd_Items.ActiveCell.Text.Contains(".") Then
                    e.Handled = True
                End If

            End If


        End If
    End Sub
    Private Sub Grd_Items_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Items.CellChange
        vMasterBlock = "I"
    End Sub
#End Region
#End Region

#Region "Data Access"
    Private Sub sLoad_Items()
        If DTS_Summary.Band.Columns.Count = 0 Then
            Exit Sub
        End If

        Try
            Dim vSqlCommand As New SqlClient.SqlCommand

            vSqlCommand.CommandText =
                " SELECT                               
                        Code,                         
                        DescA,                        
                        IsNull(Balance, 0) as Balance 
                                                      
                 From   HK_Items                         
                 Where   Company_Code = " & vCompanyCode

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Code") = vSqlReader("Code")
                Else
                    DTS_Summary.Rows(vRowCounter)("Code") = Nothing
                End If

                'DescA
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_Summary.Rows(vRowCounter)("DescA") = vSqlReader("DescA")
                Else
                    DTS_Summary.Rows(vRowCounter)("DescA") = Nothing
                End If

                'Balance
                If IsDBNull(vSqlReader("Balance")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Balance") = vSqlReader("Balance")
                Else
                    DTS_Summary.Rows(vRowCounter)("Balance") = Nothing
                End If

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Items.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sSave()
        Try
            Dim vSqlString As String
            Dim vCode As Int16

            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow

            Dim vDate As String = " '" & Strings.Format(Txt_TDate.Value, "MM-dd-yyyy") & "' "

            Grd_Items.UpdateData()

            vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From HK_Count " &
                         " Where  Company_Code = " & vCompanyCode

            vCode = cControls.fReturnValue(vSqlString, Me.Name)

            vSqlString = " Insert Into HK_Count (  Code,    Company_Code,  User_Code,   TDate,            Remarks          ) " &
                        $"               Values ( {vCode}, {vCompanyCode}, {vUsrCode}, {vDate}, '{Trim(Txt_Remarks.Text)}' ) "

            sFillSqlStatmentArray(vSqlString)

            For Each vRow In Grd_Items.Rows

                If vRow.Cells("Current_Balance").Text <> "" Then
                    vSqlString = " Insert Into HK_Count_Details (  Count_Code,   Company_Code,         Item_Code,                        Current_Balance,                       Balance,                               Consumed,                                        Lost,                              Remarks         ) " &
                                $"                       Values (    {vCode},   {vCompanyCode}, {vRow.Cells("Code").Value}, {vRow.Cells("Current_Balance").Value}, {vRow.Cells("Balance").Value}, {fIsNull(vRow.Cells("Consumed").Value, "NULL")}, {fIsNull(vRow.Cells("Lost").Value, "NULL")}, '{Trim(Txt_Remarks.Text)}' ) "

                    sFillSqlStatmentArray(vSqlString)

                    vSqlString = " Update HK_Items          " &
                             " Set    Balance      = " & vRow.Cells("Current_Balance").Value &
                             " Where  Code         = " & vRow.Cells("Code").Value &
                             " AND    Company_Code = " & vCompanyCode

                    sFillSqlStatmentArray(vSqlString)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function fIsSaveNeeded() As Boolean
        If vMasterBlock = "I" Then
            Return True
        End If

        Return False
    End Function
    Public Function fSaveAll(ByVal pASkMe As Boolean) As Boolean
        If fIsSaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()

        If pASkMe Then
            If vcFrmLevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                If fValidate_Save() Then
                    sSave()
                Else
                    Return False
                End If
            Else
                DTS_Summary.Rows.Clear()
                Return True
            End If
        Else
            If fValidate_Save() Then
                sSave()
            Else
                Return False
            End If
        End If

        If cControls.fSendData(vSQlStatment, Me.Name) > 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("7", Me)
            sLoad_Items()
            vMasterBlock = "NI"
            Return True
        End If

    End Function

#End Region

End Class