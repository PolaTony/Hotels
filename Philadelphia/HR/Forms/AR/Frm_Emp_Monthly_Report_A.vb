Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_Emp_Monthly_Report_A

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
                " SELECT                                            " &
                "       Code,                                       " &
                "       Emp_Desc,                                   " &
                "       Salary,                                     " &
                "       Dep_Desc,                                   " &
                "       Daily_Hours,                                " &
                "       TimeOff_Days,                               " &
                "       OverTime_Ratio,                             " &
                "       IsNull(Hour_Value, 0) as Hour_Value,        " &
                "       IsNull(Real_Hours, 0) as Real_Hours,        " &
                "       IsNull(OverTime, 0) as OverTime,            " &
                "       Additions,                                  " &
                "       Deductions,                                 " &
                "       Unpaid_TimeOff_Days,                             " &
                "       IsNull(Required_Hours, 0) as Required_Hours " &
                "                                     " &
                $" FROM  dbo.fn_Get_Main_Emp_Salary_Report('{vMonth_StartDay}', {vCompanyCode})   "

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read
                DTS_Summary.Rows.SetCount(vRowCounter + 1)

                'Code
                DTS_Summary.Rows(vRowCounter)("Code") = vSqlReader("Code")

                'Emp_Desc
                If IsDBNull(vSqlReader("Emp_Desc")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = vSqlReader("Emp_Desc")
                Else
                    DTS_Summary.Rows(vRowCounter)("Emp_Desc") = Nothing
                End If

                'Salary
                If IsDBNull(vSqlReader("Salary")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Salary") = vSqlReader("Salary")
                Else
                    DTS_Summary.Rows(vRowCounter)("Salary") = Nothing
                End If

                'Department_Desc
                If IsDBNull(vSqlReader("Dep_Desc")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Dep_Desc") = vSqlReader("Dep_Desc")
                Else
                    DTS_Summary.Rows(vRowCounter)("Dep_Desc") = Nothing
                End If

                'OverTime
                If IsDBNull(vSqlReader("OverTime")) = False Then
                    DTS_Summary.Rows(vRowCounter)("OverTime") = vSqlReader("OverTime")
                Else
                    DTS_Summary.Rows(vRowCounter)("OverTime") = Nothing
                End If

                'Additions
                If IsDBNull(vSqlReader("Additions")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Additions") = vSqlReader("Additions")
                Else
                    DTS_Summary.Rows(vRowCounter)("Additions") = Nothing
                End If

                'Ded_Time
                DTS_Summary.Rows(vRowCounter)("Ded_Time") = Math.Round((vSqlReader("Required_Hours") - vSqlReader("Real_Hours")) * vSqlReader("Hour_Value"), 2)

                'Deductions
                If IsDBNull(vSqlReader("Deductions")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Deductions") = vSqlReader("Deductions")
                Else
                    DTS_Summary.Rows(vRowCounter)("Deductions") = Nothing
                End If

                'Unpaid_TimeOff
                DTS_Summary.Rows(vRowCounter)("Unpaid_TimeOff") = Math.Round(vSqlReader("Unpaid_TimeOff_Days") * vSqlReader("Daily_Hours") * vSqlReader("Hour_Value"), 2)

                'Net
                DTS_Summary.Rows(vRowCounter)("Net") = Math.Round(vSqlReader("Salary") + vSqlReader("Additions") + vSqlReader("OverTime") - DTS_Summary.Rows(vRowCounter)("Ded_Time") - vSqlReader("Deductions") - DTS_Summary.Rows(vRowCounter)("Unpaid_TimeOff"), 2)

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