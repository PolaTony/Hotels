Imports System.Data.SqlClient
Imports Infragistics.Win.UltraWinTree
Imports Infragistics.Win.UltraWinEditors
Imports Infragistics.Win.UltraWinGrid

Public Class FRM_HouseKeepingSettings_A

#Region " Variables Declaration                                                         "
    Dim vBankMaster As String = "NI"
    Dim vSqlStatment(0) As String
    Dim vFocus As String
    Dim vSortedType As New SortedList
    Dim vSortedEffect As New SortedList
    Dim vSelected As Boolean = False
    Dim vCheckNodesChange As Boolean
    Dim MyStack As New Stack
    Dim vcFrmlevel As New cFrmLevelVariables_A
    Dim vWeekly_TimeOff As String = "N"
#End Region

#Region "My Form"

#Region "Form Level"

    Private Sub FRM_FinancialSetup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try

            If Not vcFrmlevel Is Nothing Then

                vcFrmlevel.vParentFrm = Me.ParentForm

                If Tab_Main.Tabs("Tab_RoomTypes").Selected Then
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                End If

            End If

        Catch ex As Exception

            'MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub FRM_FinancialSetupL_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
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
    Private Sub FRM_HouseKeepingSettings_A_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sQueryRoomTypes()
    End Sub

#End Region

#Region " DataBase                                                                      "

#Region " Save                                                                          "
    Private Function fIsSaveNeeded() As Boolean

        Dim vRow As UltraGridRow

        If Tab_Main.Tabs("Tab_RoomTypes").Selected Then
            For Each vRow In Grd_RoomTypes.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_Rooms").Selected Then
            For Each vRow In Grd_Rooms.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_CleaningTypes").Selected Then
            For Each vRow In Grd_CleaningTypes.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        ElseIf Tab_Main.Tabs("Tab_CleaningPoints").Selected Then
            For Each vRow In Grd_CleaningPoints.Rows
                If vRow.Cells("DML").Value = "I" Or vRow.Cells("DML").Value = "U" Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function
    Private Function fCheckChildNodesChange(ByVal pTreeNode As TreeNode) As Boolean
        Dim vTreeNode As TreeNode
        For Each vTreeNode In pTreeNode.Nodes
            If vTreeNode.Tag = "I" Or vTreeNode.Tag = "U" Then
                vCheckNodesChange = True
                Return True
            End If

            If vTreeNode.Nodes.Count > 0 Then
                fCheckChildNodesChange(vTreeNode)
            End If
        Next
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
                    Case "Tab_RoomTypes"
                        If fValidateRoomTypes() Then
                            sSaveRoomTypes()
                        Else
                            Return False
                        End If
                    Case "Tab_Rooms"
                        If fValidateRooms() Then
                            sSaveRooms()
                        Else
                            Return False
                        End If
                    Case "Tab_CleaningTypes"
                        If fValidateCleaningTypes() Then
                            sSaveCleaningTypes()
                        Else
                            Return False
                        End If
                    Case "Tab_CleaningPoints"
                        If fValidateCleaningPoints() Then
                            sSaveCleaningPoints()
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
                Case "Tab_RoomTypes"
                    If fValidateRoomTypes() Then
                        sSaveRoomTypes()
                    Else
                        Return False
                    End If
                Case "Tab_Rooms"
                    If fValidateRooms() Then
                        sSaveRooms()
                    Else
                        Return False
                    End If
                Case "Tab_CleaningTypes"
                    If fValidateCleaningTypes() Then
                        sSaveCleaningTypes()
                    Else
                        Return False
                    End If
                Case "Tab_CleaningPoints"
                    If fValidateCleaningPoints() Then
                        sSaveCleaningPoints()
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
        If Tab_Main.Tabs("Tab_RoomTypes").Selected Then
            sQueryRoomTypes()
        ElseIf Tab_Main.Tabs("Tab_Rooms").Selected Then
            sQueryRooms()
        ElseIf Tab_Main.Tabs("Tab_CleaningTypes").Selected Then
            sQueryCleaningTypes()
        ElseIf Tab_Main.Tabs("Tab_CleaningPoints").Selected Then
            sQueryCleaningPoints()
        End If
    End Sub
    Private Sub sUpdateChildNodes(ByVal pTreeNode As TreeNode)
        Dim vTreeNode As TreeNode
        For Each vTreeNode In pTreeNode.Nodes
            If vTreeNode.Tag = "I" Or vTreeNode.Tag = "U" Then
                vTreeNode.Tag = "N"
            End If
            If vTreeNode.Nodes.Count > 0 Then
                sUpdateChildNodes(vTreeNode)
            End If
        Next
    End Sub
#End Region

#Region " Delete                                                                        "
    Public Sub sDelete()

        Dim vSqlString As String
        If Tab_Main.Tabs("Tab_RoomTypes").Selected Then
            If Not Grd_RoomTypes.ActiveRow Is Nothing Then
                If Grd_RoomTypes.ActiveRow.Cells("DML").Value = "N" Or Grd_RoomTypes.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From Room_Types " &
                                     " Where Code = " & Grd_RoomTypes.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_RoomTypes.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_RoomTypes.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_Rooms").Selected Then
            If Not Grd_Rooms.ActiveRow Is Nothing Then
                If Grd_Rooms.ActiveRow.Cells("DML").Value = "N" Or Grd_Rooms.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From Rooms " &
                                     " Where Code = " & Grd_Rooms.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_Rooms.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_Rooms.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_CleaningTypes").Selected Then
            If Not Grd_CleaningTypes.ActiveRow Is Nothing Then
                If Grd_CleaningTypes.ActiveRow.Cells("DML").Value = "N" Or Grd_CleaningTypes.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From Cleaning_Types " &
                                     " Where Code = " & Grd_CleaningTypes.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_CleaningTypes.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_CleaningTypes.ActiveRow.Delete(False)
                End If
            End If
        ElseIf Tab_Main.Tabs("Tab_CleaningPoints").Selected Then
            If Not Grd_CleaningPoints.ActiveRow Is Nothing Then
                If Grd_CleaningPoints.ActiveRow.Cells("DML").Value = "N" Or Grd_CleaningPoints.ActiveRow.Cells("DML").Value = "U" Then
                    If vcFrmlevel.vParentFrm.sForwardMessage("39", Me) = MsgBoxResult.Yes Then
                        vSqlString = " Delete From Cleaning_Points " &
                                     " Where Code = " & Grd_CleaningPoints.ActiveRow.Cells("Code").Value &
                                     " And    Company_Code = " & vCompanyCode

                        If cControls.fSendData(vSqlString, Me.Name) > 0 Then
                            vcFrmlevel.vParentFrm.sForwardMessage("38", Me)
                            Grd_CleaningPoints.ActiveRow.Delete(False)
                        End If
                    End If
                Else
                    Grd_CleaningPoints.ActiveRow.Delete(False)
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
                If Tab_Main.Tabs("Tab_RoomTypes").Selected Then
                    sQueryRoomTypes()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_Rooms").Selected Then
                    sLoad_RoomTypes()
                    sQueryRooms()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_CleaningTypes").Selected Then
                    sQueryCleaningTypes()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                ElseIf Tab_Main.Tabs("Tab_CleaningPoints").Selected Then
                    sLoad_RoomTypes()
                    sLoad_CleaningTypes()
                    sQueryCleaningPoints()
                    vcFrmlevel.vParentFrm.sEnableTools(False, False, True, True, False, False, False, False, "", False, False)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#End Region

#Region "Room Types"

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryRoomTypes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA, Remarks From Room_Types Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_RoomTypes.Rows.Clear()
            Do While vSqlReader.Read
                DTS_RoomTypes.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_RoomTypes.Rows(vRowCounter)("Code") = vSqlReader("Code")
                End If

                'DescA
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_RoomTypes.Rows(vRowCounter)("Item") = vSqlReader("DescA")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_RoomTypes.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                DTS_RoomTypes.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidateRoomTypes() As Boolean
        Dim vRow As UltraGridRow
        For Each vRow In Grd_RoomTypes.Rows
            If vRow.Cells("Item").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("180", Me)
                vRow.Cells("Item").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveRoomTypes()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_RoomTypes.UpdateData()

        For Each vRow In Grd_RoomTypes.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Room_Types " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Room_Types (       Code,           Company_Code,                   DescA,                                Remarks             )" &
                             " Values                 ( " & vGetCode & ", " & vCompanyCode & ", '" & vRow.Cells("Item").Text & "', '" & vRow.Cells("Remarks").Text & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update Room_Types      " &
                             "                        " &
                             " Set    DescA        = '" & vRow.Cells("Item").Text & "', " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "' " &
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
    Private Sub GRD_RoomTypes_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_RoomTypes.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_RoomTypes_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_RoomTypes.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_RoomTypes_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_RoomTypes.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub

#End Region

#End Region

#Region "Rooms"

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryRooms()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA, Remarks, Room_Type From Rooms Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_Rooms.Rows.Clear()
            Do While vSqlReader.Read
                DTS_Rooms.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_Rooms.Rows(vRowCounter)("Code") = vSqlReader("Code")
                End If

                'DescA
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_Rooms.Rows(vRowCounter)("DescA") = vSqlReader("DescA")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_Rooms.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                'Room_Type
                If IsDBNull(vSqlReader("Room_Type")) = False Then
                    DTS_Rooms.Rows(vRowCounter)("Room_Type") = vSqlReader("Room_Type")
                End If

                DTS_Rooms.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub sLoad_RoomTypes()
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, Desc" & vLang & " From Room_Types " &
            " Where 1 = 1 And Company_Code = " & vCompanyCode

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

#End Region

#Region " Save                                                                          "
    Private Function fValidateRooms() As Boolean
        Grd_Rooms.UpdateData()

        Dim vRow As UltraGridRow
        For Each vRow In Grd_Rooms.Rows
            If vRow.Cells("DescA").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("181", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If

            If vRow.Cells("Room_Type").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("180", Me)
                vRow.Cells("Room_Type").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveRooms()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_Rooms.UpdateData()

        For Each vRow In Grd_Rooms.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Rooms " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Rooms (       Code,           Company_Code,                      DescA,                              Remarks,                             Room_Type             )" &
                             " Values            ( " & vGetCode & ", " & vCompanyCode & ", '" & vRow.Cells("DescA").Text & "', '" & vRow.Cells("Remarks").Text & "', " & vRow.Cells("Room_Type").Value & " )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update Rooms           " &
                             "                        " &
                             " Set    DescA        = '" & vRow.Cells("DescA").Text & "', " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "', " &
                             "        Room_Type    =  " & vRow.Cells("Room_Type").Value &
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
    Private Sub GRD_Rooms_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Rooms.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_Rooms_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_Rooms.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_Rooms_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_Rooms.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub

#End Region

#End Region

#Region "Cleaning Types"

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryCleaningTypes()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, DescA, Remarks From Cleaning_Types Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_CleaningTypes.Rows.Clear()
            Do While vSqlReader.Read
                DTS_CleaningTypes.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_CleaningTypes.Rows(vRowCounter)("Code") = vSqlReader("Code")
                End If

                'DescA
                If IsDBNull(vSqlReader("DescA")) = False Then
                    DTS_CleaningTypes.Rows(vRowCounter)("DescA") = vSqlReader("DescA")
                End If

                'Remarks
                If IsDBNull(vSqlReader("Remarks")) = False Then
                    DTS_CleaningTypes.Rows(vRowCounter)("Remarks") = vSqlReader("Remarks")
                End If

                DTS_CleaningTypes.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidateCleaningTypes() As Boolean
        Grd_CleaningTypes.UpdateData()

        Dim vRow As UltraGridRow
        For Each vRow In Grd_CleaningTypes.Rows
            If vRow.Cells("DescA").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("182", Me)
                vRow.Cells("DescA").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveCleaningTypes()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_CleaningTypes.UpdateData()

        For Each vRow In Grd_CleaningTypes.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Cleaning_Types " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Cleaning_Types (       Code,           Company_Code,                      DescA,                              Remarks )" &
                             " Values                     ( " & vGetCode & ", " & vCompanyCode & ", '" & vRow.Cells("DescA").Text & "', '" & vRow.Cells("Remarks").Text & "' )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update Cleaning_Types  " &
                             "                        " &
                             " Set    DescA        = '" & vRow.Cells("DescA").Text & "', " &
                             "        Remarks      = '" & vRow.Cells("Remarks").Text & "' " &
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
    Private Sub GRD_CleaningTypes_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_CleaningTypes.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_CleaningTypes_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_CleaningTypes.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_CleaningTypes_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_CleaningTypes.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub

#End Region

#End Region

#Region "Cleaning Points"

#Region " DataBase                                                                      "

#Region " Query                                                                         "
    Private Sub sQueryCleaningPoints()
        Try
            Dim vRowCounter As Integer
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText = " Select Code, Room_Type, Cleaning_Type, Points From Cleaning_Points Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            vRowCounter = 0
            DTS_CleaningPoints.Rows.Clear()
            Do While vSqlReader.Read
                DTS_CleaningPoints.Rows.SetCount(vRowCounter + 1)

                'Code
                If IsDBNull(vSqlReader("Code")) = False Then
                    DTS_CleaningPoints.Rows(vRowCounter)("Code") = vSqlReader("Code")
                End If

                'Room_Type
                If IsDBNull(vSqlReader("Room_Type")) = False Then
                    DTS_CleaningPoints.Rows(vRowCounter)("Room_Type") = vSqlReader("Room_Type")
                End If

                'Cleaning_Type
                If IsDBNull(vSqlReader("Cleaning_Type")) = False Then
                    DTS_CleaningPoints.Rows(vRowCounter)("Cleaning_Type") = vSqlReader("Cleaning_Type")
                End If

                'Points
                If IsDBNull(vSqlReader("Points")) = False Then
                    DTS_CleaningPoints.Rows(vRowCounter)("Points") = vSqlReader("Points")
                End If

                DTS_CleaningPoints.Rows(vRowCounter)("DML") = "N"
                vRowCounter += 1
            Loop
            cControls.vSqlConn.Close()
            vSqlReader.Close()
        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sLoad_CleaningTypes()
        Try
            Dim vsqlCommand As New SqlCommand
            vsqlCommand.CommandText =
            " Select Code, Desc" & vLang & " From Cleaning_Types " &
            " Where 1 = 1 And Company_Code = " & vCompanyCode

            vsqlCommand.Connection = cControls.vSqlConn
            cControls.vSqlConn.Open()
            Dim vSqlReader As SqlDataReader = vsqlCommand.ExecuteReader
            Txt_CleaningTypes.Items.Clear()

            Do While vSqlReader.Read
                Txt_CleaningTypes.Items.Add(vSqlReader("Code"), vSqlReader("DescA"))
            Loop
            cControls.vSqlConn.Close()
            Txt_CleaningTypes.SelectedIndex = -1

        Catch ex As Exception
            cControls.vSqlConn.Close()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region " Save                                                                          "
    Private Function fValidateCleaningPoints() As Boolean
        Grd_CleaningPoints.UpdateData()

        Dim vRow As UltraGridRow
        For Each vRow In Grd_CleaningPoints.Rows
            If vRow.Cells("Room_Type").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("183", Me)
                vRow.Cells("Room_Type").Selected = True
                Return False
            End If
            If vRow.Cells("Cleaning_Type").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("184", Me)
                vRow.Cells("Cleaning_Type").Selected = True
                Return False
            End If
            If vRow.Cells("Points").Text = "" Then
                vcFrmlevel.vParentFrm.sForwardMessage("185", Me)
                vRow.Cells("Points").Selected = True
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub sSaveCleaningPoints()

        Dim vSqlString As String
        Dim vRow As UltraGridRow
        Dim vCounter As Int16
        Dim vGetCode As Integer

        Grd_CleaningPoints.UpdateData()

        For Each vRow In Grd_CleaningPoints.Rows

            If vRow.Cells("DML").Value = "I" Then

                vSqlString = " Select IsNull(Max(Code), 0) + 1 From Cleaning_Points " &
                             " Where  Company_Code  = " & vCompanyCode

                vGetCode = cControls.fReturnValue(vSqlString, Me.Name) + vCounter

                vSqlString = " Insert Into Cleaning_Points (       Code,           Company_Code,                     Room_Type,                             Cleaning_Type,                             Points            )" &
                             " Values                      ( " & vGetCode & ", " & vCompanyCode & ", " & vRow.Cells("Room_Type").Value & ", " & vRow.Cells("Cleaning_Type").Value & ", " & vRow.Cells("Points").Text & " )"

                sFillSqlStatmentArray(vSqlString)

                vCounter += 1

            ElseIf vRow.Cells("DML").Value = "U" Then

                vSqlString = " Update Cleaning_Points  " &
                             "                         " &
                             " Set    Room_Type     =  " & vRow.Cells("Room_Type").Value & ", " &
                             "        Cleaning_Type =  " & vRow.Cells("Cleaning_Type").Value & ", " &
                             "        Points =         " & vRow.Cells("Points").Text & " " &
                             "                         " &
                             " Where  Code         =   " & vRow.Cells("Code").Value &
                             " And    Company_Code =   " & vCompanyCode

                sFillSqlStatmentArray(vSqlString)

            End If

        Next

    End Sub

#End Region

#End Region

#Region " Navigation                                                                    "
    Private Sub GRD_CleaningPoints_CellChange(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_CleaningPoints.CellChange
        If sender.ActiveRow.Cells("DML").Value = "NI" Then
            sender.ActiveRow.Cells("DML").Value = "I"
        ElseIf sender.ActiveRow.Cells("DML").Value = "N" Then
            sender.ActiveRow.Cells("DML").Value = "U"
        End If
    End Sub
    Private Sub GRD_CleaningPoints_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grd_CleaningPoints.KeyUp
        If e.KeyData = Keys.Delete And e.Control Then
            sDelete()
        End If
    End Sub
    Private Sub GRD_CleaningPoints_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles Grd_CleaningPoints.ClickCellButton
        If sender.ActiveRow.Cells("Delete").Activated Then
            sDelete()
        End If
    End Sub
    Private Sub Grd_CleaningPoints_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Grd_CleaningPoints.KeyPress
        If Grd_CleaningPoints.ActiveCell IsNot Nothing AndAlso Grd_CleaningPoints.ActiveCell.Column.Key = "Points" Then
            ' Allow digits, control characters (like Backspace)
            If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True ' Cancel the keypress
            End If
        End If
    End Sub

#End Region

#End Region

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Me.Close()
    End Sub

End Class