Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_OC_Occupation_Report_A

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
    Private Sub Frm_OC_Occupation_Report_A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Txt_FromDate.Value = Now
        Txt_ToDate.Value = Now
        sQueryOccReport()
    End Sub
    Private Sub Frm_OC_Occupation_Report_A_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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

    Private Sub sQueryOccReport()
        If DTS_OccReport.Band.Columns.Count = 0 Then
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
	                OC_Pricing_Systems.DescA AS Pricing_Sys,
	                Rooms_Types.DescA AS [Type],
	                OC_Rooms_Daily_Occ_Details.Count AS [Count],
	                OC_Rooms_Daily_Occ_Details.Price AS Price,
	                OC_Rooms_Daily_Occ_Details.Remarks AS Remarks

                FROM OC_Rooms_Daily_Occ_Details

                LEFT JOIN Rooms_Types
                ON OC_Rooms_Daily_Occ_Details.Room_Type_Code = Rooms_Types.Code
                AND OC_Rooms_Daily_Occ_Details.Company_Code = Rooms_Types.Company_Code

                LEFT JOIN OC_Rooms_Daily_Occ
                ON OC_Rooms_Daily_Occ_Details.RM_Code = OC_Rooms_Daily_Occ.Code
                AND OC_Rooms_Daily_Occ_Details.Company_Code = OC_Rooms_Daily_Occ.Company_Code

                LEFT JOIN OC_Pricing_Systems
                ON OC_Rooms_Daily_Occ.Pricing_System_Code = OC_Pricing_Systems.Code
                AND OC_Rooms_Daily_Occ.Company_Code = OC_Pricing_Systems.Company_Code

                WHERE OC_Rooms_Daily_Occ_Details.Company_Code = " & vCompanyCode &
              " AND OC_Rooms_Daily_Occ.TDate BETWEEN " & vFromDate & " AND " & vToDate

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn

            cControls.vSqlConn.Open()

            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader

            DTS_OccReport.Rows.Clear()

            Do While vSqlReader.Read

                DTS_OccReport.Rows.SetCount(vRowCounter + 1)

                'TDate
                If IsDBNull(vSqlReader("TDate")) = False Then
                    DTS_OccReport.Rows(vRowCounter)("TDate") = vSqlReader("TDate")
                Else
                    DTS_OccReport.Rows(vRowCounter)("TDate") = Nothing
                End If

                'Pricing_Sys
                If IsDBNull(vSqlReader("Pricing_Sys")) = False Then
                    DTS_OccReport.Rows(vRowCounter)("Pricing_Sys") = vSqlReader("Pricing_Sys")
                Else
                    DTS_OccReport.Rows(vRowCounter)("Pricing_Sys") = Nothing
                End If

                'Type
                If IsDBNull(vSqlReader("Type")) = False Then
                    DTS_OccReport.Rows(vRowCounter)("Type") = vSqlReader("Type")
                Else
                    DTS_OccReport.Rows(vRowCounter)("Type") = Nothing
                End If

                'Count
                If IsDBNull(vSqlReader("Count")) = False Then
                    DTS_OccReport.Rows(vRowCounter)("Count") = vSqlReader("Count")
                Else
                    DTS_OccReport.Rows(vRowCounter)("Count") = Nothing
                End If

                'Price
                If IsDBNull(vSqlReader("Price")) = False Then
                    DTS_OccReport.Rows(vRowCounter)("Price") = vSqlReader("Price")
                Else
                    DTS_OccReport.Rows(vRowCounter)("Price") = Nothing
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_OccReport.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                Else
                    DTS_OccReport.Rows(vRowCounter)("Remarks") = Nothing
                End If

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_OccReport.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Btn_Search_Click(sender As Object, e As EventArgs) Handles Btn_Search.Click
        sQueryOccReport()
    End Sub

#End Region

End Class