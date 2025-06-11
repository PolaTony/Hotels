Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_OC_Reservation_Report_A

#Region " Declaration                                                       "
    Dim vcFrmLevel As New cFrmLevelVariables_A
#End Region

#Region " Form Level                                               "

#Region " My Form                                                           "
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
    Private Sub Frm_OC_Reservation_Report_A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Txt_FromDate.Value = Now
        Txt_ToDate.Value = Now
        sQueryReservationReport()
    End Sub
    Private Sub Frm_OC_Reservation_Report_A_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm
        vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", True, False, "")
        sHide_ToolbarMain_Tools()
    End Sub
    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

#End Region

#End Region

#Region " Summary                  "

    Private Sub sQueryReservationReport()
        If DTS_ReservationReport.Band.Columns.Count = 0 Then
            Exit Sub
        End If

        Dim vFromDate, vToDate As String

        If Not Txt_FromDate.Value Is Nothing Then
            vFromDate = "'" & Format(Txt_FromDate.Value, "MM-dd-yyyy") & "'"
        Else
            vFromDate = "NULL"
        End If

        If Not Txt_ToDate.Value Is Nothing Then
            vToDate = "'" & Format(Txt_ToDate.Value, "MM-dd-yyyy") & "'"
        Else
            vToDate = "NULL"
        End If

        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText =
              " SELECT 

	                OC_Rooms_Daily_Occ.TDate AS TDate,
	                OC_Reservation_Types.DescA AS [Type],
	                OC_Reservation_Daily_Details.Reservation_Count AS [Count],
	                OC_Reservation_Daily_Details.Remarks AS Remarks

                FROM OC_Reservation_Daily_Details

                LEFT JOIN OC_Reservation_Types
                ON OC_Reservation_Daily_Details.Reservation_Type_Code = OC_Reservation_Types.Code
                AND OC_Reservation_Daily_Details.Company_Code = OC_Reservation_Types.Company_Code

                LEFT JOIN OC_Rooms_Daily_Occ
                ON OC_Reservation_Daily_Details.RM_Code = OC_Rooms_Daily_Occ.Code
                AND OC_Reservation_Daily_Details.Company_Code = OC_Rooms_Daily_Occ.Company_Code

                WHERE OC_Reservation_Daily_Details.Company_Code = " & vCompanyCode &
              " AND OC_Rooms_Daily_Occ.TDate BETWEEN " & vFromDate & " AND " & vToDate

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn

            cControls.vSqlConn.Open()

            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader

            DTS_ReservationReport.Rows.Clear()

            Do While vSqlReader.Read

                DTS_ReservationReport.Rows.SetCount(vRowCounter + 1)

                'TDate
                If IsDBNull(vSqlReader("TDate")) = False Then
                    DTS_ReservationReport.Rows(vRowCounter)("TDate") = vSqlReader("TDate")
                Else
                    DTS_ReservationReport.Rows(vRowCounter)("TDate") = Nothing
                End If

                'Type
                If IsDBNull(vSqlReader("Type")) = False Then
                    DTS_ReservationReport.Rows(vRowCounter)("Type") = vSqlReader("Type")
                Else
                    DTS_ReservationReport.Rows(vRowCounter)("Type") = Nothing
                End If

                'Count
                If IsDBNull(vSqlReader("Count")) = False Then
                    DTS_ReservationReport.Rows(vRowCounter)("Count") = vSqlReader("Count")
                Else
                    DTS_ReservationReport.Rows(vRowCounter)("Count") = Nothing
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_ReservationReport.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                Else
                    DTS_ReservationReport.Rows(vRowCounter)("Remarks") = Nothing
                End If

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_ReservationReport.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Btn_Search_Click(sender As Object, e As EventArgs) Handles Btn_Search.Click
        sQueryReservationReport()
    End Sub

#End Region

End Class