Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_Cleaning_Room_Monthly_Report_A

#Region "Declaration"

    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSQlStatment(0) As String
    Dim vHours_In_Decimals As Decimal
    Dim vTime_Split As String()
    Dim vMinutes

#End Region
    Private Sub sQuerySummary()
        If DTS_Summary.Band.Columns.Count = 0 Then
            Exit Sub
        End If

        Dim vMonth_StartDay As String

        vMonth_StartDay = CDate(Txt_Month.Value).Month & "-1-" & CDate(Txt_Month.Value).Year

        Try
            Dim vSqlCommand As New SqlClient.SqlCommand

            vSqlCommand.CommandText =
                " SELECT " &
                "     HK_Cleaning_Rooms_Details.Emp_Code AS Emp_Code, " &
                "     Employees.DescA AS Emp_Desc, " &
                "     Rooms.DescA As Clean_Place, " &
                "     Rooms_Types.DescA AS Room_Type, " &
                "     HK_Cleaning_Types.DescA AS Clean_Type, " &
                "     Supervisor_Employees.DescA AS Supervisor, " &
                "     CL.TDate AS TDate, " &
                "     CL.Points / ( Select Count(*) From HK_Cleaning_Rooms_Details  " &
                " 	              Where Cl_Code = CL.Code) as Points  " &
                "  " &
                " FROM HK_Cleaning_Rooms_Details " &
                "  " &
                " LEFT JOIN Employees " &
                " ON HK_Cleaning_Rooms_Details.Emp_Code = Employees.Code " &
                " AND HK_Cleaning_Rooms_Details.Company_Code = Employees.Company_Code " &
                "  " &
                " LEFT JOIN HK_Cleaning_Rooms as CL " &
                " ON HK_Cleaning_Rooms_Details.Cl_Code = CL.Code " &
                " AND HK_Cleaning_Rooms_Details.Company_Code = CL.Company_Code " &
                "  " &
                " LEFT JOIN Rooms " &
                " ON CL.Room_Code = Rooms.Code " &
                " AND CL.Company_Code = Rooms.Company_Code " &
                "  " &
                " LEFT JOIN Rooms_Types " &
                " ON Rooms.Room_Type = Rooms_Types.Code " &
                " AND Rooms.Company_Code = Rooms_Types.Company_Code " &
                "  " &
                " LEFT JOIN HK_Cleaning_Types " &
                " ON CL.CleanType_Code = HK_Cleaning_Types.Code " &
                " AND CL.Company_Code = HK_Cleaning_Types.Company_Code " &
                "  " &
                " INNER JOIN Employees AS Supervisor_Employees " &
                " ON CL.Supervisor_Code = Supervisor_Employees.Code " &
                " AND CL.Company_Code = Supervisor_Employees.Company_Code " &
                "  " &
                $" WHERE Employees.Company_Code = {vCompanyCode} " &
                $" AND CL.TDate BETWEEN '{vMonth_StartDay}' AND EOMONTH('{vMonth_StartDay}') "

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)

                'Emp_Code
                If IsDBNull(vSqlReader("Emp_Code")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Emp_Code") = vSqlReader("Emp_Code")
                Else
                    DTS_Summary.Rows(vRowCounter)("Emp_Code") = Nothing
                End If

                'Emp_Desc
                If IsDBNull(vSqlReader("Emp_Desc")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = vSqlReader("Emp_Desc")
                Else
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = Nothing
                End If

                'Clean_Place
                If IsDBNull(vSqlReader("Clean_Place")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Clean_Place") = vSqlReader("Clean_Place")
                Else
                    DTS_Summary.Rows(vRowCounter)("Clean_Place") = Nothing
                End If

                'Room_Type
                If IsDBNull(vSqlReader("Room_Type")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Room_Type") = vSqlReader("Room_Type")
                Else
                    DTS_Summary.Rows(vRowCounter)("Room_Type") = Nothing
                End If

                'Clean_Type
                If IsDBNull(vSqlReader("Clean_Type")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Clean_Type") = vSqlReader("Clean_Type")
                Else
                    DTS_Summary.Rows(vRowCounter)("Clean_Type") = Nothing
                End If

                'Supervisor
                If IsDBNull(vSqlReader("Supervisor")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Supervisor") = vSqlReader("Supervisor")
                Else
                    DTS_Summary.Rows(vRowCounter)("Supervisor") = Nothing
                End If

                'TDate
                If IsDBNull(vSqlReader("TDate")) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate") = vSqlReader("TDate")
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                End If

                'Points
                If IsDBNull(vSqlReader("Points")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Points") = vSqlReader("Points")
                Else
                    DTS_Summary.Rows(vRowCounter)("Points") = Nothing
                End If

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_Summary.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#Region " Form Level                                                                            "
#Region " My Form                                                                               "
    Private Sub FRM_AddDed_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm
        vcFrmLevel.vParentFrm.sEnableTools(False, False, False, False, False, False, False, False, "", True, False, "التفاصيل")

        sHide_ToolbarMain_Tools()
    End Sub

    Private Sub Frm_AddTrans_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Txt_Month.Value = Now
    End Sub

#End Region

#Region " sOpenLov                                                                              "
    Private Sub sOpenLov(ByVal pSqlStatment As String, ByVal pTitle As String)
        vLovReturn1 = ""
        VLovReturn2 = ""

        Dim Frm_Lov As New FRM_LovGeneral_A(pSqlStatment, pTitle)
        Frm_Lov.ShowDialog()

        If vLovReturn1.Length > 0 And VLovReturn2.Length > 0 Then
            If pTitle = "Emp" Then
                'GRD_Additions.ActiveRow.Cells("EMP_Code").Value = vLovReturn1
                'GRD_Additions.ActiveRow.Cells("EMP_Desc").Value = VLovReturn2

                'sLoad_EmployeeInfo(GRD_Additions.ActiveRow)
            End If
        End If
    End Sub

#End Region

#End Region

    Private Sub sHide_ToolbarMain_Tools()
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_New").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Save").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Delete").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_Print").SharedProps.Visible = True
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_NextRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_PreviousRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_LastRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_FirstRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_CloseWindow").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_GotoRecord").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Btn_ChangeUser").SharedProps.Visible = False
        vcFrmLevel.vParentFrm.ToolBar_Main.Tools("Themes").SharedProps.Visible = False
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Btn_Search_Click(sender As Object, e As EventArgs) Handles Btn_Search.Click

        If Txt_Month.Value Is Nothing Then
            vcFrmLevel.vParentFrm.sForwardMessage("33", Me)
            Exit Sub
        End If

        sQuerySummary()

    End Sub

End Class