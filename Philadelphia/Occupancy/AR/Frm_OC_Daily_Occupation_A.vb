Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_OC_Daily_Occupation_A

#Region " Declaration                                                       "
    Dim vcFrmLevel As New cFrmLevelVariables_A
    Dim vSQlStatment(0) As String
    Dim vMasterBlock As String = "NI"
    Dim vTCode As Int32
#End Region

#Region " Form Level                                               "

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
    Private Sub Frm_OC_Daily_Occupation_A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TXT_FromSummaryDate.Value = Now
        Txt_ToSummaryDate.Value = Now

        sQuerySummary()
        sLoad_PricingSystems()
        sLoad_RoomTypes()
        sLoad_ReservationTypes()

        vMasterBlock = "NI"
    End Sub
    Private Sub Frm_OC_Daily_Occupation_A_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        vcFrmLevel.vParentFrm = Me.ParentForm

        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, False, False, False, False, False, "", True, False, "التفاصيل")
        Else
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
        End If

        sHide_ToolbarMain_Tools()
    End Sub
    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub
    Private Sub Btn_Back_Click(sender As Object, e As EventArgs) Handles Btn_Back.Click
        Tab_Main.Tabs("Tab_Summary").Selected = True
    End Sub

#End Region

#Region " DataBase                                                          "

#Region " Save                                                            "
    Private Function fIsSaveNeeded() As Boolean
        If vMasterBlock = "I" Or vMasterBlock = "U" Then
            Return True
        End If

        Dim vRow As UltraGridRow
        For Each vRow In Grd_RoomsDailyOccupation.Rows
            If vRow.Cells("DML").Text = "I" Or vRow.Cells("DML").Text = "U" Then
                Return True
            End If
        Next

        For Each vRow In Grd_ReservationDailyOccupation.Rows
            If vRow.Cells("DML").Text = "I" Or vRow.Cells("DML").Text = "U" Then
                Return True
            End If
        Next

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

            vMasterBlock = "NI"
            Dim vRow As UltraGridRow
            For Each vRow In Grd_RoomsDailyOccupation.Rows
                If vRow.Cells("DML").Text = "I" Or vRow.Cells("DML").Text = "U" Then
                    vRow.Cells("DML").Value = "NI"
                End If
            Next

            For Each vRow In Grd_ReservationDailyOccupation.Rows
                If vRow.Cells("DML").Text = "I" Or vRow.Cells("DML").Text = "U" Then
                    vRow.Cells("DML").Value = "NI"
                End If
            Next

            sNewRecord()

            Return True
        End If

    End Function
#End Region

#Region " Query                                                             "
    Public Sub sQuery(Optional ByVal pRecPos As Integer = Nothing, Optional ByVal pItemCode As String = Nothing, Optional ByVal pIsGoTo As Boolean = False)
        If fSaveAll(True) = False Then
            Return
        End If

        Dim vFetchRec As Integer
        If pItemCode = Nothing Then
            If pIsGoTo = False Then
                If pRecPos = Nothing Then
                    vFetchRec = 1
                Else
                    vFetchRec = vcFrmLevel.vRecPos + pRecPos
                    If vFetchRec > cControls.fCount_Rec(" From  OC_Rooms_Daily_Occ " &
                                                        " Where Company_Code = " & vCompanyCode) Then

                        vcFrmLevel.vParentFrm.sForwardMessage("33", Me)
                        Return
                    End If
                    If vFetchRec = 0 Then
                        vcFrmLevel.vParentFrm.sForwardMessage("34", Me)
                        vFetchRec = 1
                    End If

                End If
            End If
        End If
        If pRecPos = -2 Then
            vFetchRec = cControls.fCount_Rec(" From  OC_Rooms_Daily_Occ " &
                                             " Where Company_Code = " & vCompanyCode)
        End If

        Dim vFetchCondition As String
        If pItemCode = Nothing Then
            vFetchCondition = " And RecPos = '" & vFetchRec & "'"
        Else
            vFetchCondition = " And MyItems.Code = '" & Trim(pItemCode) & "'"
        End If

        'Here I set vQuery = "Y" to not load auto complete in Desc Field
        'vQuery = "Y"

        Try

            Dim vSQlcommand As New SqlCommand
            vSQlcommand.CommandText =
            " With MyItems as " &
            "( Select Code,                                             " &
            "         TDate,                                            " &
            "         Pricing_System_Code,                              " &
            "         Remarks,                                          " &
            "         ROW_Number() Over (Order By Code) as  RecPos      " &
            "                                                           " &
            " From OC_Rooms_Daily_Occ                                    " &
            "                                                            " &
            " Where Company_Code = " & vCompanyCode & "   )              " &
            " Select * From MyItems " &
            " Where    1 = 1                                             " &
            vFetchCondition

            vSQlcommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vSQlcommand.ExecuteReader
            Do While vSqlReader.Read

                If IsDBNull(vSqlReader("RecPos")) = False Then
                    vcFrmLevel.vRecPos = Trim(vSqlReader("RecPos"))
                End If
                vcFrmLevel.vParentFrm.sPrintRec(vSqlReader("RecPos"))

                'Code
                vTCode = Trim(vSqlReader("Code"))

                'TDate
                If IsDBNull(vSqlReader("Pricing_System_Code")) = False Then
                    Txt_PricingSystems.Value = Trim(vSqlReader("Pricing_System_Code"))
                Else
                    Txt_PricingSystems.Value = Nothing
                End If

                'TDate
                If IsDBNull(vSqlReader("TDate")) = False Then
                    Txt_TDate.Value = Trim(vSqlReader("TDate"))
                Else
                    Txt_TDate.Value = Nothing
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    Txt_Remarks.Text = Trim(vSqlReader("Remarks"))
                Else
                    Txt_Remarks.Text = ""
                End If
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()

            sQueryRooms()
            sQueryReservation()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

        Dim vRow As UltraGridRow
        For Each vRow In Grd_RoomsDailyOccupation.Rows
            If vRow.Cells("DML").Text = "I" Or vRow.Cells("DML").Text = "U" Then
                vRow.Cells("DML").Value = "NI"
            End If
        Next

        For Each vRow In Grd_ReservationDailyOccupation.Rows
            If vRow.Cells("DML").Text = "I" Or vRow.Cells("DML").Text = "U" Then
                vRow.Cells("DML").Value = "NI"
            End If
        Next
        vMasterBlock = "N"
    End Sub
#End Region

#End Region

#Region " Tab Mangment                                                                           "
    Private Sub Tab_Main_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Main.SelectedTabChanging
        If Tab_Main.Tabs("Tab_Details").Selected = True Then
            If fSaveAll(True) = False Then
                e.Cancel = True
            Else
                e.Cancel = False
                'sNewRecord()
            End If
        End If
    End Sub
    Private Sub Tab_Main_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        If Tab_Main.Tabs("Tab_Summary").Selected = True Then
            vcFrmLevel.vParentFrm = Me.ParentForm
            vcFrmLevel.vParentFrm.sEnableTools(True, False, False, True, False, False, False, False, "", False, False, "التفاصيل")
            Btn_Back.Visible = False
            sQuerySummary()
        ElseIf Tab_Main.Tabs("Tab_Details").Selected = True Then
            vcFrmLevel.vParentFrm.sEnableTools(True, True, True, True, True, True, True, True, "", False, False, "بحث")
            Btn_Back.Visible = True
        End If
    End Sub
#End Region

#Region " New Record                                 "
    Public Sub sNewRecord()
        Try
            If fSaveAll(True) = False Then
                Return
            End If

            Tab_Main.Tabs("Tab_Details").Selected = True

            vcFrmLevel.vRecPos = 0
            vcFrmLevel.vParentFrm.sPrintRec("")

            Txt_TDate.Value = Nothing
            Txt_Remarks.Text = ""
            Txt_PricingSystems.SelectedIndex = -1

            Dim vRow As UltraGridRow
            For Each vRow In Grd_RoomsDailyOccupation.Rows
                vRow.Cells("Count").Value = DBNull.Value
                vRow.Cells("Remarks").Value = DBNull.Value
                vRow.Cells("DML").Value = "NI"
            Next

            For Each vRow In Grd_ReservationDailyOccupation.Rows
                vRow.Cells("Count").Value = DBNull.Value
                vRow.Cells("Remarks").Value = DBNull.Value
                vRow.Cells("DML").Value = "NI"
            Next

            vTCode = Nothing
            vMasterBlock = "NI"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

#End Region

#Region " Master                                                     "

#Region " DataBase                                                   "

#Region " Save                                                       "
    Public Function fValidate_Save() As Boolean

        If Txt_TDate.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("53", Me)
            Txt_TDate.Select()
            Return False
        End If

        If Txt_PricingSystems.Text = "" Then
            vcFrmLevel.vParentFrm.sForwardMessage("196", Me)
            Txt_PricingSystems.Select()
            Return False
        End If

        Dim vRow As UltraGridRow
        Dim vCount As Int16
        For Each vRow In Grd_RoomsDailyOccupation.Rows
            If vRow.Cells("Count").Text <> "" Then
                vCount += 1
            End If
        Next

        If vCount = 0 Then
            vcFrmLevel.vParentFrm.sForwardMessage("52", Me)
            Return False
        End If

        Return True
    End Function
    Private Sub sSave()
        Try

            sEmptySqlStatmentArray()

            Dim vSqlString As String

            Dim vRow As Infragistics.Win.UltraWinGrid.UltraGridRow

            Dim vDate As String = " '" & Strings.Format(Txt_TDate.Value, "MM-dd-yyyy") & "' "

            Grd_RoomsDailyOccupation.UpdateData()
            Grd_ReservationDailyOccupation.UpdateData()

            If vMasterBlock = "I" Then

                vSqlString = " Select IsNull(Max(Convert(Int, Code)), 0) + 1 From OC_Rooms_Daily_Occ " &
                        " Where  Company_Code = " & vCompanyCode

                vTCode = cControls.fReturnValue(vSqlString, Me.Name)

                vSqlString = " Insert Into OC_Rooms_Daily_Occ (  Code,    Company_Code,  User_Code,   TDate,            Remarks,              Pricing_System_Code          ) " &
                            $"                         Values ( {vTCode}, {vCompanyCode}, {vUsrCode}, {vDate}, '{Trim(Txt_Remarks.Text)}', {Txt_PricingSystems.Value} ) "

                sFillSqlStatmentArray(vSqlString)

            ElseIf vMasterBlock = "U" Then

                vSqlString = " Update OC_Rooms_Daily_Occ 
                                 Set TDate = " & vDate & ", " &
                                  " Remarks = '" & Trim(Txt_Remarks.Text) & "', " &
                                  " Pricing_System_Code = " & Txt_PricingSystems.Value &
                            " Where Code = " & vTCode &
                            " And Company_Code = " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If

            For Each vRow In Grd_RoomsDailyOccupation.Rows

                If vRow.Cells("DML").Text = "I" Then

                    vSqlString = " Insert Into OC_Rooms_Daily_Occ_Details (  RM_Code,   Company_Code,          Room_Type_Code,                       Count,                             Remarks,                                               Price                                                                                                   ) " &
                                $"                                 Values (  {vTCode},  {vCompanyCode}, {vRow.Cells("Room_Code").Value}, {vRow.Cells("Count").Value}, '{Trim(vRow.Cells("Remarks").Text)}',      dbo.fn_Get_Room_Price({vRow.Cells("Room_Code").Value}, {Txt_PricingSystems.Value}, {vCompanyCode}) * {fIsNull(vRow.Cells("Count").Value, 0)} ) "

                    sFillSqlStatmentArray(vSqlString)

                ElseIf vRow.Cells("DML").Text = "U" Then

                    vSqlString = "
                        Update OC_Rooms_Daily_Occ_Details
                        Set Count = " & fIsNull(vRow.Cells("Count").Value, "NULL") & ", " &
                          " Remarks = '" & vRow.Cells("Remarks").Text & "', " &
                         $" Price = dbo.fn_Get_Room_Price({vRow.Cells("Room_Code").Value}, {Txt_PricingSystems.Value}, {vCompanyCode}) * {fIsNull(vRow.Cells("Count").Value, 0)}" &
                         "                         " &
                         $" Where RM_Code = {vTCode}" &
                         $" And Company_Code = {vCompanyCode}" &
                         $" And Room_Type_Code = {vRow.Cells("Room_Code").Value}"

                    sFillSqlStatmentArray(vSqlString)

                End If
            Next

            For Each vRow In Grd_ReservationDailyOccupation.Rows
                If vRow.Cells("DML").Text = "I" Then
                    vSqlString = " Insert Into OC_Rooms_Daily_Reservation_Details (  RM_Code,   Company_Code,               Reservation_Type_Code,                  Count,                             Remarks            ) " &
                                $"                                   Values (  {vTCode},  {vCompanyCode}, {vRow.Cells("Reservation_Code").Value}, {vRow.Cells("Count").Value}, '{fIsNull(vRow.Cells("Remarks").Value, "")}' ) "

                    sFillSqlStatmentArray(vSqlString)
                ElseIf vRow.Cells("DML").Text = "U" Then

                    vSqlString = "
                        Update OC_Rooms_Daily_Reservation_Details
                        Set Count = " & fIsNull(vRow.Cells("Count").Value, "NULL") & ", " &
                          " Remarks = '" & vRow.Cells("Remarks").Text & "' " &
                         $" Where RM_Code = {vTCode }" &
                         $" And Company_Code = {vCompanyCode}" &
                         $" And Reservation_Type_Code = {vRow.Cells("Reservation_Code").Value}"

                    sFillSqlStatmentArray(vSqlString)

                End If
            Next


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#Region " Delete                                                                         "
    Public Sub sDelete()

        If vMasterBlock = "I" Or vMasterBlock = "NI" Then
            sNewRecord()
            Exit Sub
        ElseIf vMasterBlock = "N" Or vMasterBlock = "U" Then
            If vcFrmLevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                sEmptySqlStatmentArray()

                Dim vSqlstring As String

                'Here I Insert Into Delete Log Table..

                vSqlstring =
                " Delete From OC_Rooms_Daily_Reservation_Details " &
                " Where  RM_Code = " & vTCode &
                " And    Company_Code = " & vCompanyCode &
                "                                           " &
                " Delete From OC_Rooms_Daily_Occ_Details " &
                " Where  RM_Code = " & vTCode &
                " And    Company_Code = " & vCompanyCode &
                "                                           " &
                " Delete From OC_Rooms_Daily_Occ " &
                " Where  Code = " & vTCode &
                " And    Company_Code = " & vCompanyCode

                sFillSqlStatmentArray(vSqlstring)

                'vSqlstring = " Insert Into Sales_Invoices  (        Code,                  Invoice_Code,                    DescA,                 TDate,                   Emp_Code,                          Provider_Code,              SalesMan_Code,                 Cur_Code,                 Box_Code,       CostCenter_Code,    SalesType_Code,               Remarks,            Status,    Company_Code,             PayType,                   Payed)" & _
                '             "                     Values  ('" & vLog_Code & "', '" & Trim(Txt_Code.Text) & "', '" & Trim(Txt_Desc.Text) & "', " & vDate & ", '" & Trim(Txt_EmpCode.Text) & "', '" & Trim(Txt_CustomerCode.Text) & "', " & vSalesMan & ", '" & Trim(Txt_CurrencyCode.Text) & "', " & vBox & ", " & vCostCenter & ", " & vSalesType & ", '" & Trim(Txt_Remarks.Text) & "',   'O', " & vCompanyCode & ", '" & Txt_PayType.Value & "', " & vPayed & ")"

                If cControls.fSendData(vSQlStatment, Me.Name) > 0 Then
                    'Here I Save the Log Data
                    sEmptySqlStatmentArray()

                    sNewRecord()
                    vcFrmLevel.vParentFrm.sForwardMessage("38", Me)
                End If
            End If
        End If
    End Sub
#End Region

#End Region

#End Region

#Region " Details                                                              "

#Region " DataBase                                                             "

#Region " Save                                                                 "


#End Region
#Region " Query                                                                "

    Private Sub sQueryReservation()
        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText =
              " SELECT 

	                OC_Reservation_Types.Code AS Reservation_Code,
	                OC_Reservation_Types.DescA AS Reservation_Type,
	                OC_Rooms_Daily_Reservation_Details.Count,
	                OC_Rooms_Daily_Reservation_Details.Remarks,
                    CASE 
		                WHEN OC_Reservation_Types.Code = OC_Rooms_Daily_Reservation_Details.Reservation_Type_Code 
		                THEN 'y'
		                ELSE 'n'
	                END AS Is_Inserted

                FROM OC_Reservation_Types

                LEFT JOIN OC_Rooms_Daily_Reservation_Details
                ON OC_Reservation_Types.Code = OC_Rooms_Daily_Reservation_Details.Reservation_Type_Code
                AND OC_Reservation_Types.Company_Code = OC_Rooms_Daily_Reservation_Details.Company_Code
                AND OC_Rooms_Daily_Reservation_Details.RM_Code = " & vTCode & "

                WHERE OC_Reservation_Types.Company_Code = " & vCompanyCode

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_ReservationDailyOccupation.Rows.Clear()

            Do While vSqlReader.Read

                DTS_ReservationDailyOccupation.Rows.SetCount(vRowCounter + 1)

                'Reservation_Code
                DTS_ReservationDailyOccupation.Rows(vRowCounter)("Reservation_Code") = vSqlReader("Reservation_Code")

                'Reservation_Type
                If IsDBNull(vSqlReader("Reservation_Type")) = False Then
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Reservation_Type") = vSqlReader("Reservation_Type")
                Else
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Reservation_Type") = Nothing
                End If

                'Count
                If IsDBNull(vSqlReader("Count")) = False Then
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Count") = vSqlReader("Count")
                Else
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Count") = Nothing
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                Else
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Remarks") = Nothing
                End If

                'Is_Inserted
                If vSqlReader("Is_Inserted") = "y" Then
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("DML") = "N"
                Else
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("DML") = "NI"
                End If

                vRowCounter += 1
            Loop


            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_ReservationDailyOccupation.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sQueryRooms()
        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText =
              " SELECT 

	                Rooms_Types.Code AS Room_Code,
	                Rooms_Types.DescA AS Room_Type,
	                OC_Rooms_Daily_Occ_Details.Count,
	                OC_Rooms_Daily_Occ_Details.Remarks,
                    CASE 
                        WHEN Rooms_Types.Code = OC_Rooms_Daily_Occ_Details.Room_Type_Code 
                        THEN 'y'
                        ELSE 'n'
                    END AS Is_Inserted

                FROM Rooms_Types

                LEFT JOIN OC_Rooms_Daily_Occ_Details
                ON Rooms_Types.Code = OC_Rooms_Daily_Occ_Details.Room_Type_Code
                AND Rooms_Types.Company_Code = OC_Rooms_Daily_Occ_Details.Company_Code
                AND OC_Rooms_Daily_Occ_Details.RM_Code = " & vTCode & "

                WHERE Rooms_Types.Company_Code = " & vCompanyCode

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_RoomsDailyOccupation.Rows.Clear()

            Do While vSqlReader.Read

                DTS_RoomsDailyOccupation.Rows.SetCount(vRowCounter + 1)

                'Room_Code
                DTS_RoomsDailyOccupation.Rows(vRowCounter)("Room_Code") = vSqlReader("Room_Code")

                'Room_Type
                If IsDBNull(vSqlReader("Room_Type")) = False Then
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Room_Type") = vSqlReader("Room_Type")
                Else
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Room_Type") = Nothing
                End If

                'Count
                If IsDBNull(vSqlReader("Count")) = False Then
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Count") = vSqlReader("Count")
                Else
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Count") = Nothing
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                Else
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Remarks") = Nothing
                End If

                'Is_Inserted
                If vSqlReader("Is_Inserted") = "y" Then
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("DML") = "N"
                Else
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("DML") = "NI"
                End If

                vRowCounter += 1
            Loop


            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_RoomsDailyOccupation.UpdateData()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#End Region

#Region " Form Level                                                           "
    Private Sub Grd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Grd_RoomsDailyOccupation.KeyPress, Grd_ReservationDailyOccupation.KeyPress

        If Grd_RoomsDailyOccupation.ActiveCell IsNot Nothing Then

            If Grd_RoomsDailyOccupation.ActiveCell.Column.Key = "Count" Then

                If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                    e.Handled = True ' Cancel the keypress
                End If

            End If


        End If

        If Grd_ReservationDailyOccupation.ActiveCell IsNot Nothing Then

            If Grd_ReservationDailyOccupation.ActiveCell.Column.Key = "Count" Then

                If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                    e.Handled = True ' Cancel the keypress
                End If

            End If


        End If

    End Sub

    Private Sub Grd_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) _
        Handles Grd_ReservationDailyOccupation.CellChange, Grd_RoomsDailyOccupation.CellChange
        If sender.ActiveRow.Cells("DML").Text = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Text = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
#End Region

#End Region

#Region " Data Access                                      "
    Private Sub sLoad_RoomTypes()
        If DTS_RoomsDailyOccupation.Band.Columns.Count = 0 Then
            Exit Sub
        End If

        Try
            Dim vSqlCommand As New SqlClient.SqlCommand

            vSqlCommand.CommandText =
               " SELECT                               
                        Code AS Room_Code,                         
                        DescA AS Room_Type                         
                                                      
                 From    Rooms_Types                         
                 Where   Company_Code = " & vCompanyCode

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_RoomsDailyOccupation.Rows.Clear()

            Do While vSqlReader.Read
                DTS_RoomsDailyOccupation.Rows.SetCount(vRowCounter + 1)

                'Room_Code
                If IsDBNull(vSqlReader("Room_Code")) = False Then
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Room_Code") = vSqlReader("Room_Code")
                Else
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Room_Code") = Nothing
                End If

                'Room_Type
                If IsDBNull(vSqlReader("Room_Type")) = False Then
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Room_Type") = vSqlReader("Room_Type")
                Else
                    DTS_RoomsDailyOccupation.Rows(vRowCounter)("Room_Type") = Nothing
                End If

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_RoomsDailyOccupation.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoad_ReservationTypes()
        If DTS_ReservationDailyOccupation.Band.Columns.Count = 0 Then
            Exit Sub
        End If

        Try
            Dim vSqlCommand As New SqlClient.SqlCommand

            vSqlCommand.CommandText =
               " SELECT                               
                        Code AS Reservation_Code,                         
                        DescA AS Reservation_Type                         
                                                      
                 From    OC_Reservation_Types                         
                 Where   Company_Code = " & vCompanyCode

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader
            DTS_ReservationDailyOccupation.Rows.Clear()

            Do While vSqlReader.Read
                DTS_ReservationDailyOccupation.Rows.SetCount(vRowCounter + 1)

                'Reservation_Code
                If IsDBNull(vSqlReader("Reservation_Code")) = False Then
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Reservation_Code") = vSqlReader("Reservation_Code")
                Else
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Reservation_Code") = Nothing
                End If

                'Reservation_Type
                If IsDBNull(vSqlReader("Reservation_Type")) = False Then
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Reservation_Type") = vSqlReader("Reservation_Type")
                Else
                    DTS_ReservationDailyOccupation.Rows(vRowCounter)("Reservation_Type") = Nothing
                End If

                vRowCounter += 1
            Loop

            cControls.vSqlConn.Close()
            vSqlReader.Close()
            Grd_RoomsDailyOccupation.UpdateData()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sLoad_PricingSystems()
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, Desc" & vLang & " From OC_Pricing_Systems " &
            " Where Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_PricingSystems.Items.Clear()

            Do While vSqlReader.Read
                Txt_PricingSystems.Items.Add(vSqlReader("Code"), vSqlReader("DescA"))
            Loop
            cControls.vSqlConn.Close()
            Txt_PricingSystems.SelectedIndex = -1

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region " Summary                  "

    Private Sub sQuerySummary()
        If DTS_Summary.Band.Columns.Count = 0 Then
            Exit Sub
        End If

        Dim vFromDate, vToDate_PlusOneDay As String

        If Not TXT_FromSummaryDate.Value Is Nothing Then
            vFromDate = "'" & Format(TXT_FromSummaryDate.Value, "MM-dd-yyyy") & "'"
        Else
            vFromDate = "NULL"
        End If

        vToDate_PlusOneDay = " '" & Format(Txt_ToSummaryDate.Value, "MM-dd-yyyy") & "' "

        Try
            Dim vSqlCommand As New SqlClient.SqlCommand
            vSqlCommand.CommandText =
                " SELECT 
	                OC_Rooms_Daily_Occ.Code, 
	                OC_Rooms_Daily_Occ.TDate,
	                OC_Rooms_Daily_Occ.Remarks,
	                Users.DescA AS User_DescA,
                    OC_Pricing_Systems.DescA AS Pricing_System

                FROM OC_Rooms_Daily_Occ

                LEFT JOIN Users
                ON OC_Rooms_Daily_Occ.User_Code = Users.Code
                AND OC_Rooms_Daily_Occ.Company_Code = Users.Company_Code

                LEFT JOIN OC_Pricing_Systems
                ON OC_Rooms_Daily_Occ.Pricing_System_Code = OC_Pricing_Systems.Code
                AND OC_Rooms_Daily_Occ.Company_Code = OC_Pricing_Systems.Company_Code

                WHERE OC_Rooms_Daily_Occ.Company_Code = " & vCompanyCode &
              " AND OC_Rooms_Daily_Occ.TDate BETWEEN " & vFromDate & " AND " & vToDate_PlusOneDay

            Dim vRowCounter As Integer = 0

            vSqlCommand.Connection = cControls.vSqlConn

            cControls.vSqlConn.Open()

            Dim vSqlReader As SqlClient.SqlDataReader = vSqlCommand.ExecuteReader

            DTS_Summary.Rows.Clear()

            Do While vSqlReader.Read

                DTS_Summary.Rows.SetCount(vRowCounter + 1)

                'Code
                DTS_Summary.Rows(vRowCounter)("Code") = CStr(vSqlReader("Code"))

                'TDate
                If IsDBNull(vSqlReader("TDate")) = False Then
                    DTS_Summary.Rows(vRowCounter)("TDate") = vSqlReader("TDate")
                Else
                    DTS_Summary.Rows(vRowCounter)("TDate") = Nothing
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                Else
                    DTS_Summary.Rows(vRowCounter)("Remarks") = Nothing
                End If

                'User_DescA
                If IsDBNull(vSqlReader("User_DescA")) = False Then
                    DTS_Summary.Rows(vRowCounter)("User_DescA") = vSqlReader("User_DescA")
                Else
                    DTS_Summary.Rows(vRowCounter)("User_DescA") = Nothing
                End If

                'Pricing_System
                If IsDBNull(vSqlReader("Pricing_System")) = False Then
                    DTS_Summary.Rows(vRowCounter)("Pricing_System") = vSqlReader("Pricing_System")
                Else
                    DTS_Summary.Rows(vRowCounter)("Pricing_System") = Nothing
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

    Private Sub Grd_Summary_DoubleClick(sender As Object, e As EventArgs) Handles Grd_Summary.DoubleClick
        If Grd_Summary.Selected.Rows.Count > 0 Then
            sQuery(pItemCode:=Grd_Summary.ActiveRow.Cells("Code").Value)
        Else
            sNewRecord()
        End If

        Tab_Main.Tabs("Tab_Details").Selected = True
    End Sub

    Private Sub Txt_MasterDetails_ValueChanged(sender As Object, e As EventArgs) Handles Txt_PricingSystems.ValueChanged, Txt_Remarks.ValueChanged, Txt_TDate.ValueChanged
        If vMasterBlock = "NI" Then
            vMasterBlock = "I"
        ElseIf vMasterBlock = "N" Then
            vMasterBlock = "U"
        End If
    End Sub

    Private Sub Btn_Search_Click(sender As Object, e As EventArgs) Handles Btn_Search.Click
        sQuerySummary()
    End Sub

#End Region

End Class