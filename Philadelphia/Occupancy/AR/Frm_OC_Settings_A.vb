Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinGrid

Public Class Frm_OC_Settings_A

#Region " Variables Declaration                                                         "
    Dim vSqlStatment(0) As String
    Dim vcFrmlevel As New cFrmLevelVariables_A
#End Region

#Region " My Form                                                                     "

#Region "Form Level"

    Private Sub Frm_OC_Settings_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try

            If Not vcFrmlevel Is Nothing Then

                vcFrmlevel.vParentFrm = Me.ParentForm

                vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub Frm_OC_Settings_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        Try
            If fSaveAll(True) = False Then
                e.Cancel = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub sFillSqlStatmentArray(ByVal vSqlstring As String)
        If vSqlStatment(UBound(vSqlStatment)) = "" Then
            vSqlStatment(UBound(vSqlStatment)) = vSqlstring
        Else
            ReDim Preserve vSqlStatment(UBound(vSqlStatment) + 1)
            vSqlStatment(UBound(vSqlStatment)) = vSqlstring
        End If
    End Sub
    Private Sub sEmptySqlStatmentArray()
        ReDim vSqlStatment(0)
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Frm_OC_Settings_A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sQueryReservationTypes()
    End Sub

#End Region

#Region " DataBase                                                                      "

#Region " Save                                                                          "
    Private Function fIsSaveNeeded() As Boolean

        Dim vRow As UltraGridRow

        If Tab_Main.Tabs("Tab_ReservationTypes").Selected Then
            For Each vRow In Grd_ReservationTypes.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_PricingSystems").Selected Then
            For Each vRow In Grd_RoomCoding.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_RoomPrices").Selected Then
            For Each vRow In Grd_RoomPrices.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function
    Public Function fSaveAll(ByVal pAskMe As Boolean) As Boolean
        If fIsSaveNeeded() = False Then
            Return True
        End If

        sEmptySqlStatmentArray()
        Dim vRowCounter As Integer
        If pAskMe = True Then
            If vcFrmlevel.vParentFrm.sForwardMessage("6", Me) = MsgBoxResult.Yes Then
                Select Case Tab_Main.SelectedTab.Key
                    Case "Tab_ReservationTypes"
                        If fValidateReservationTypes() Then
                            sSaveReservationTypes()
                        Else
                            Return False
                        End If
                    Case "Tab_PricingSystems"
                        If fValidateRoomCoding() Then
                            sSaveRoomCoding()
                        Else
                            Return False
                        End If
                    Case "Tab_RoomPrices"
                        If fValidateRoomPrices() Then
                            sSaveRoomPrices()
                        Else
                            Return False
                        End If
                End Select
            Else
                sSetFlagsUpdate()
                Return True
            End If
        Else
            Select Case Tab_Main.SelectedTab.Key
                Case "Tab_ReservationTypes"
                    If fValidateReservationTypes() Then
                        sSaveReservationTypes()
                    Else
                        Return False
                    End If
                Case "Tab_PricingSystems"
                    If fValidateRoomCoding() Then
                        sSaveRoomCoding()
                    Else
                        Return False
                    End If
                Case "Tab_RoomPrices"
                    If fValidateRoomPrices() Then
                        sSaveRoomPrices()
                    Else
                        Return False
                    End If
            End Select
        End If

        vRowCounter = cControls.fSendData(vSqlStatment, Me.Name)
        If vRowCounter > 0 Then
            sEmptySqlStatmentArray()

            If Not vcFrmlevel Is Nothing Then
                vcFrmlevel.vParentFrm.sForwardMessage("7", Me)
            End If

            sSetFlagsUpdate()
            Return True
        End If
    End Function
    Private Sub sSetFlagsUpdate()
        If Tab_Main.Tabs("Tab_ReservationTypes").Selected Then
            sQueryReservationTypes()
        ElseIf Tab_Main.Tabs("Tab_PricingSystems").Selected Then
            sQueryRoomCoding()
        ElseIf Tab_Main.Tabs("Tab_RoomPrices").Selected Then
            sQueryRoomPrices()
        End If
    End Sub

#End Region

#Region " Delete                                                                        "
    Public Sub sDelete()

        Dim vSqlString As String
        If Tab_Main.Tabs("Tab_ReservationTypes").Selected Then
            If Not Grd_ReservationTypes.ActiveRow Is Nothing Then
                If Grd_ReservationTypes.ActiveRow.Cells("DML").Value = "N" Or Grd_ReservationTypes.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From OC_Reservation_Types " &
                                     " Where  Code = " & Grd_ReservationTypes.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_ReservationTypes.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_ReservationTypes.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_PricingSystems").Selected Then
            If Not Grd_RoomCoding.ActiveRow Is Nothing Then
                If Grd_RoomCoding.ActiveRow.Cells("DML").Value = "N" Or Grd_RoomCoding.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From OC_Pricing_Systems " &
                                     " Where  Code         =      " & Grd_RoomCoding.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code =      " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_RoomCoding.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_RoomCoding.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_RoomPrices").Selected Then
            If Not Grd_RoomPrices.ActiveRow Is Nothing Then
                If Grd_RoomPrices.ActiveRow.Cells("DML").Value = "N" Or Grd_RoomPrices.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From OC_Room_Prices " &
                                     " Where  Code         =      " & Grd_RoomPrices.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code =      " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_RoomPrices.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_RoomPrices.ActiveRow.Delete(False)
                End If
            End If
        End If

    End Sub
#End Region

#End Region

#Region " Tab Managment                                                                 "
    Private Sub Tab_Main_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles Tab_Main.SelectedTabChanging
        Try
            If fSaveAll(True) = False Then
                e.Cancel = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Tab_Main_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles Tab_Main.SelectedTabChanged
        Try
            If Not vcFrmlevel.vParentFrm Is Nothing Then
                If Tab_Main.Tabs("Tab_ReservationTypes").Selected Then
                    sQueryReservationTypes()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_PricingSystems").Selected Then
                    sQueryRoomCoding()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_RoomPrices").Selected Then
                    sLoad_RoomTypes()
                    sLoad_PricingSystems()
                    sQueryRoomPrices()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#End Region

#Region " Reservation Types             "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryReservationTypes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA, Remarks From OC_Reservation_Types Where Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_ReservationTypes.Rows.Clear()
            Do While vSqlReader.Read
                DTS_ReservationTypes.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_ReservationTypes.Rows(vRowCounter)("Code") = vSqlReader("Code")
                End If

                'DescA
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_ReservationTypes.Rows(vRowCounter)("DescA") = vSqlReader("DescA")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_ReservationTypes.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                DTS_ReservationTypes.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Grd_ReservationTypes.UpdateData()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidateReservationTypes() As Boolean
        Grd_ReservationTypes.UpdateData()

        Dim vRow As UltraGridRow
        For Each vRow In Grd_ReservationTypes.Rows
            If vRow.Cells("DescA").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("193", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveReservationTypes()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_ReservationTypes.UpdateData()

        For Each vRow In Grd_ReservationTypes.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From OC_Reservation_Types " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into OC_Reservation_Types (       Code,           Company_Code,                      DescA,                               Remarks              )" &
                             " Values                           ( " & vGetCode & ", " & vCompanyCode & ", '" & vRow.Cells("DescA").Value & "', '" & vRow.Cells("Remarks").Value & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update OC_Reservation_Types  " &
                             "                              " &
                             " Set    DescA        =       '" & vRow.Cells("DescA").Value & "', " &
                             "        Remarks      =       '" & vRow.Cells("Remarks").Value & "' " &
                             "                              " &
                             " Where  Code         =        " & vRow.Cells("Code").Value &
                             " And    Company_Code =        " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If

        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub Grd_ReservationTypes_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_ReservationTypes.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub Grd_ReservationTypes_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_ReservationTypes.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub Grd_ReservationTypes_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_ReservationTypes.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub

#End Region

#End Region

#Region " Room Coding                                                                   "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryRoomCoding()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA, Remarks From OC_Pricing_Systems Where Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_RoomCoding.Rows.Clear()
            Do While vSqlReader.Read
                DTS_RoomCoding.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_RoomCoding.Rows(vRowCounter)("Code") = vSqlReader("Code")
                End If

                'DescA
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_RoomCoding.Rows(vRowCounter)("DescA") = vSqlReader("DescA")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_RoomCoding.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                DTS_RoomCoding.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Grd_RoomCoding.UpdateData()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidateRoomCoding() As Boolean
        Grd_RoomCoding.UpdateData()

        Dim vRow As UltraGridRow
        For Each vRow In Grd_RoomCoding.Rows
            If vRow.Cells("DescA").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("194", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveRoomCoding()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_RoomCoding.UpdateData()

        For Each vRow In Grd_RoomCoding.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From OC_Pricing_Systems " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into OC_Pricing_Systems (       Code,           Company_Code,                      DescA,                               Remarks              )" &
                             " Values                     ( " & vGetCode & ", " & vCompanyCode & ", '" & vRow.Cells("DescA").Value & "', '" & vRow.Cells("Remarks").Value & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update OC_Pricing_Systems  " &
                             "                        " &
                             " Set    DescA        = '" & vRow.Cells("DescA").Value & "', " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Value & "' " &
                             "                        " &
                             " Where  Code         =  " & vRow.Cells("Code").Value &
                             " And    Company_Code =  " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If

        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub Grd_Room_Coding_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_RoomCoding.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub Grd_Room_Coding_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_RoomCoding.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub Grd_Room_Coding_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_RoomCoding.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub

#End Region

#End Region

#Region " Room Prices                                                                   "

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryRoomPrices()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
              " SELECT 
	                OC_Room_Prices.Code,
	                OC_Room_Prices.Price,
	                OC_Room_Prices.Remarks,
	                Rooms_Types.Code AS Room_Type,
	                OC_Pricing_Systems.Code AS Room_Coding
                FROM OC_Room_Prices

                LEFT JOIN Rooms_Types
                ON OC_Room_Prices.Room_Type = Rooms_Types.Code
                AND OC_Room_Prices.Company_Code = Rooms_Types.Company_Code

                LEFT JOIN OC_Pricing_Systems
                ON OC_Room_Prices.Room_Coding = OC_Pricing_Systems.Code
                AND OC_Room_Prices.Company_Code = OC_Pricing_Systems.Company_Code

                WHERE OC_Room_Prices.Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_RoomPrices.Rows.Clear()
            Do While vSqlReader.Read
                DTS_RoomPrices.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_RoomPrices.Rows(vRowCounter)("Code") = vSqlReader("Code")
                End If

                'Price
                If IsDBNull(vSqlReader("Price")) = False Then
                    DTS_RoomPrices.Rows(vRowCounter)("Price") = vSqlReader("Price")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_RoomPrices.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                'Room_Type
                If IsDBNull(vSqlReader("Room_Type")) = False Then
                    DTS_RoomPrices.Rows(vRowCounter)("Room_Type") = vSqlReader("Room_Type")
                End If

                'Room_Coding
                If IsDBNull(vSqlReader("Room_Coding")) = False Then
                    DTS_RoomPrices.Rows(vRowCounter)("Room_Coding") = vSqlReader("Room_Coding")
                End If

                DTS_RoomPrices.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()

            Grd_RoomPrices.UpdateData()

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sLoad_RoomTypes()
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, Desc" & vLang & " From Rooms_Types " &
            " Where Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_RoomTypes.Items.Clear()

            Do While vSqlReader.Read
                Txt_RoomTypes.Items.Add(vSqlReader("Code"), vSqlReader("DescA"))
            Loop
            cControls.vSqlConn.Close()
            Txt_RoomTypes.SelectedIndex = -1

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
            Txt_RoomCoding.Items.Clear()

            Do While vSqlReader.Read
                Txt_RoomCoding.Items.Add(vSqlReader("Code"), vSqlReader("DescA"))
            Loop
            cControls.vSqlConn.Close()
            Txt_RoomCoding.SelectedIndex = -1

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidateRoomPrices() As Boolean
        Grd_RoomPrices.UpdateData()

        Dim vRow As UltraGridRow
        For Each vRow In Grd_RoomPrices.Rows
            If vRow.Cells("Room_Type").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("180", Me)
                vRow.Cells("Room_Type").Selected = True
                Return False
            End If
            If vRow.Cells("Room_Coding").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("195", Me)
                vRow.Cells("Room_Coding").Selected = True
                Return False
            End If
            If vRow.Cells("Price").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("22", Me)
                vRow.Cells("Price").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveRoomPrices()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_RoomPrices.UpdateData()

        For Each vRow In Grd_RoomPrices.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From OC_Room_Prices " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into OC_Room_Prices (       Code,           Company_Code,                     Room_Type,                             Room_Coding,                             Price,                              Remarks              )" &
                             " Values                     ( " & vGetCode & ", " & vCompanyCode & ", " & vRow.Cells("Room_Type").Value & ", " & vRow.Cells("Room_Coding").Value & ", " & vRow.Cells("Price").Value & ", '" & vRow.Cells("Remarks").Value & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update OC_Room_Prices  " &
                             "                        " &
                             " Set    Room_Type    =  " & vRow.Cells("Room_Type").Value & ", " &
                             "        Room_Coding  =  " & vRow.Cells("Room_Coding").Value & ", " &
                             "        Price        =  " & vRow.Cells("Price").Value & ", " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Value & "' " &
                             "                        " &
                             " Where  Code         =  " & vRow.Cells("Code").Value &
                             " And    Company_Code =  " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If

        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub Grd_Room_Prices_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_RoomPrices.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub Grd_Room_Prices_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_RoomPrices.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub Grd_Room_Prices_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_RoomPrices.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub
    Private Sub Grd_Room_Prices_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Grd_RoomPrices.KeyPress
        If Grd_RoomPrices.ActiveCell IsNot Nothing AndAlso Grd_RoomPrices.ActiveCell.Column.Key = "Price" Then
            ' Allow digits, control characters (like Backspace), and one dot
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
                e.Handled = True ' Cancel the keypress
            End If

            ' Prevent more than one dot
            If e.KeyChar = "." AndAlso Grd_RoomPrices.ActiveCell.Text.Contains(".") Then
                e.Handled = True
            End If
        End If
    End Sub

#End Region

#End Region

End Class