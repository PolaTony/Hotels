Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Management
Imports System.Drawing.Printing
Imports System.Drawing.Text

Module mdlGlobals
#Region " G E N E R A L  G L O B A L S                                                          "
    Dim vItemPrice As String
    Public vLovReturn1 As String                    'Global Variable for LOV Return 
    Public VLovReturn2 As String                    'Global Variable for LOV Return
    Public vLovReturn3 As String                    'Global Variable for LOV Return
    Public vLovReturn4 As String                    'Global Variable for LOV Return
    Public vZeroBasedLevel As Integer = 3           'To detect the zero based level in financial tree

    Public vDTS_AllInvoices As New UltraDataSource
    Public vDS_TreeView As New DTS_BarCode

    Public vUsrCode As String
    Public vUsrName As String
    Public vIsAdmin As String
    Public vCloseApplication As Boolean = True
    Public vSelectedSortedList_1 As New SortedList
    Public vSelectedSortedList_2 As New SortedList
    Public vSelectedSortedList_3 As New SortedList
    Public vSelectedSortedList_4 As New SortedList

    Public vSelectedSortedList As New SortedList
    Public vTreeNodeCode As String = ""
    Public vExpandedType As Boolean
    Public vTreeColor As Color = Color.Blue
    Public vLang As String = "L"
    Public vCompanyCode As String
    Public vBranchCode As String
    Public vServerName As String
    Public vDataBaseName As String
    Public vToolTipType As String
    Public vDefaultCheck As Boolean = False
    Public vUnClosedForm As System.Windows.Forms.Form 'To Detect the UnClosed Form Due to Save Validation.
    Public vSortedList_SN As New SortedList
    Public vSortedList_ServerName As New SortedList
    Public vIsNewItemFound As Boolean
    Public vOriginalOrCopy As String
    Public vCustomer_Code As String
    Public vDueDate As String

    Public vGrd As UltraGrid
    Public vPayed As Decimal
    Public vCurrentBarCode As Int16
    Public fonts As New PrivateFontCollection

    Public vStartDate As String
    Public vEndDate As String
    Public vHolidayDesc As String

    Public Function fCalculateItemBalance(ByVal pRow As UltraGridRow, ByVal pStr_Code As String)
        Dim vNumber As Decimal

        Try
            'Dim vReturnValue As String = cControls.fReturnValue(" Select Sum(QtyIn * IsNULL(dbo.[Get_PackUnit_Number](Mov_Item_Stores.Item_Code, Mov_Item_Stores.PU_Ser), 1) - QtyOut * ISNULL(dbo.[Get_PackUnit_Number](Mov_Item_Stores.Item_Code, Mov_Item_Stores.PU_Ser), 1)) " &
            '                             " From   Mov_Item_Stores " &
            '                             " Where  Item_Code = '" & Trim(pRow.Cells("Fixed_Code").Text) & "' " &
            '                             " And    Str_Code  = '" & Trim(pStr_Code) & "' " &
            '                             " And    Company_Code  = " & vCompanyCode, "")

            Dim vReturnValue As String = cControls.fReturnValue(" Select Balance From Items_Stores_Balance " &
                                  " Where  Item_Code = '" & Trim(pRow.Cells("Fixed_Code").Text) & "' " &
                                  " And    Str_Code =  '" & Trim(pStr_Code) & "' " &
                                  " And    Company_Code  = " & vCompanyCode, "")

            If vReturnValue = "" Then
                fCalculateItemBalance = 0
            Else
                fCalculateItemBalance = vReturnValue
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'If IsDBNull(vRow.Cells("PU_Number").Value) Then
        '    vNumber = 1
        'Else
        '    vNumber = vRow.Cells("PU_Number").Value
        'End If

    End Function

    Public Sub sFill_SerialNumbers_SortedList()
        vSortedList_SN.Add("0011-234-567", "14657832")
        vSortedList_SN.Add("0011-234-568", "14568731")
        vSortedList_SN.Add("0011-234-569", "14357321")
        vSortedList_SN.Add("0011-234-570", "14533712")
        vSortedList_SN.Add("0011-234-571", "14720048")
        vSortedList_SN.Add("0011-234-572", "14270084")
        vSortedList_SN.Add("0011-234-573", "15702050")
        vSortedList_SN.Add("0011-234-574", "15070205")
        vSortedList_SN.Add("0011-234-575", "15302090")
        vSortedList_SN.Add("0011-234-576", "15030209")
        vSortedList_SN.Add("0011-234-577", "15203040")
        vSortedList_SN.Add("0011-234-578", "15020304")
        vSortedList_SN.Add("0011-234-579", "15607080")
        vSortedList_SN.Add("0011-234-580", "15060708")
        vSortedList_SN.Add("0011-234-581", "15304050")
        vSortedList_SN.Add("0011-234-582", "15030405")
        vSortedList_SN.Add("0011-234-583", "15306080")
        vSortedList_SN.Add("0011-234-584", "15030608")
        vSortedList_SN.Add("0011-234-585", "15050207")
        vSortedList_SN.Add("0011-234-586", "15090203")
        vSortedList_SN.Add("0011-234-587", "15040302")
        vSortedList_SN.Add("0011-234-588", "15080706")
        vSortedList_SN.Add("0011-234-589", "15050403")
        vSortedList_SN.Add("0011-234-590", "15080603")
        vSortedList_SN.Add("0011-234-591", "15702080")
        vSortedList_SN.Add("0011-234-592", "15070208")
        vSortedList_SN.Add("0011-234-593", "15080207")
        vSortedList_SN.Add("0011-234-594", "15702040")
        vSortedList_SN.Add("0011-234-595", "15070204")
        vSortedList_SN.Add("0011-234-596", "15070402")
        vSortedList_SN.Add("0011-234-597", "15040207")
        vSortedList_SN.Add("0011-234-598", "15040702")
        vSortedList_SN.Add("0011-234-599", "15020704")
        vSortedList_SN.Add("0011-234-600", "15020407")
        vSortedList_SN.Add("0011-234-601", "15702010")
        vSortedList_SN.Add("0011-234-602", "15070201")
        vSortedList_SN.Add("0011-234-603", "15070102")
        vSortedList_SN.Add("0011-234-604", "15020701")
        vSortedList_SN.Add("0011-234-605", "15020107")
        vSortedList_SN.Add("0011-234-606", "15010702")
        vSortedList_SN.Add("0011-234-607", "15010207")
        vSortedList_SN.Add("0011-234-608", "15702030")
        vSortedList_SN.Add("0011-234-609", "15070203")
        vSortedList_SN.Add("0011-234-610", "15070302")
        vSortedList_SN.Add("0011-234-611", "15020307")
        vSortedList_SN.Add("0011-234-612", "15020703")
        vSortedList_SN.Add("0011-234-613", "15030702")
        vSortedList_SN.Add("0011-234-614", "15030207")
        vSortedList_SN.Add("0011-234-615", "15702060")
        vSortedList_SN.Add("0011-234-616", "15070206")
        vSortedList_SN.Add("0011-234-617", "15070602")
        vSortedList_SN.Add("0011-234-618", "15060702")
        vSortedList_SN.Add("0011-234-619", "15060207")
        vSortedList_SN.Add("0011-234-620", "15020607")
        vSortedList_SN.Add("0011-234-621", "15020706")
        vSortedList_SN.Add("0011-234-622", "15702090")
        vSortedList_SN.Add("0011-234-623", "15070209")
        vSortedList_SN.Add("0011-234-624", "15070209")
        vSortedList_SN.Add("0011-234-625", "15020709")
        vSortedList_SN.Add("0011-234-626", "15020907")
        vSortedList_SN.Add("0011-234-627", "15090207")
        vSortedList_SN.Add("0011-234-628", "15090702")
        vSortedList_SN.Add("0011-234-629", "15703010")
        vSortedList_SN.Add("0011-234-630", "15070301")
        vSortedList_SN.Add("0011-234-631", "15070103")
        vSortedList_SN.Add("0011-234-632", "15010703")
        vSortedList_SN.Add("0011-234-633", "15010307")
        vSortedList_SN.Add("0011-234-634", "15030107")
        vSortedList_SN.Add("0011-234-635", "15030701")
        vSortedList_SN.Add("0011-234-636", "15703020")
        vSortedList_SN.Add("0011-234-637", "15070302")
        vSortedList_SN.Add("0011-234-638", "15070203")
        vSortedList_SN.Add("0011-234-639", "15030702")
        vSortedList_SN.Add("0011-234-640", "15030207")
        vSortedList_SN.Add("0011-234-641", "15020307")
        vSortedList_SN.Add("0011-234-642", "15020703")
        vSortedList_SN.Add("0011-234-643", "15703040")
        vSortedList_SN.Add("0011-234-644", "15070304")
        vSortedList_SN.Add("0011-234-645", "15070403")
        vSortedList_SN.Add("0011-234-646", "15040307")
        vSortedList_SN.Add("0011-234-647", "15040703")
        vSortedList_SN.Add("0011-234-648", "15030407")
        vSortedList_SN.Add("0011-234-649", "15030704")
        vSortedList_SN.Add("0011-234-650", "15703050")
        vSortedList_SN.Add("0011-234-651", "15070305")
        vSortedList_SN.Add("0011-234-652", "15070503")
        vSortedList_SN.Add("0011-234-653", "15050703")
        vSortedList_SN.Add("0011-234-654", "15050307")
        vSortedList_SN.Add("0011-234-655", "15030705")
        vSortedList_SN.Add("0011-234-656", "15030507")
        vSortedList_SN.Add("0011-234-657", "15703060")
        vSortedList_SN.Add("0011-234-658", "15070306")
        vSortedList_SN.Add("0011-234-659", "15070603")
        vSortedList_SN.Add("0011-234-660", "15030706")
        vSortedList_SN.Add("0011-234-661", "15030607")
        vSortedList_SN.Add("0011-234-662", "15060307")
        vSortedList_SN.Add("0011-234-663", "15060703")
        vSortedList_SN.Add("0011-234-664", "15703080")
        vSortedList_SN.Add("0011-234-665", "15070308")
        vSortedList_SN.Add("0011-234-666", "15070803")
        vSortedList_SN.Add("0011-234-667", "15080703")
        vSortedList_SN.Add("0011-234-668", "15080307")
        vSortedList_SN.Add("0011-234-669", "15030708")
        vSortedList_SN.Add("0011-234-670", "15030807")
        vSortedList_SN.Add("0011-234-671", "15703090")
        vSortedList_SN.Add("0011-234-672", "15070309")
        vSortedList_SN.Add("0011-234-673", "15070903")
        vSortedList_SN.Add("0011-234-674", "15030709")
        vSortedList_SN.Add("0011-234-675", "15030907")
        vSortedList_SN.Add("0011-234-676", "15090307")
        vSortedList_SN.Add("0011-234-677", "15090703")
        vSortedList_SN.Add("0011-234-678", "15704010")
        vSortedList_SN.Add("0011-234-679", "15070401")
        vSortedList_SN.Add("0011-234-680", "15070104")
        vSortedList_SN.Add("0011-234-681", "15040701")
        vSortedList_SN.Add("0011-234-682", "15040107")
        vSortedList_SN.Add("0011-234-683", "15010407")
        vSortedList_SN.Add("0011-234-684", "15010704")
        vSortedList_SN.Add("0011-234-685", "15704020")
        vSortedList_SN.Add("0011-234-686", "15070402")
        vSortedList_SN.Add("0011-234-687", "15070204")
        vSortedList_SN.Add("0011-234-689", "15040702")
        vSortedList_SN.Add("0011-234-690", "15040207")
        vSortedList_SN.Add("0011-234-691", "15020704")
        vSortedList_SN.Add("0011-234-692", "15020407")
        vSortedList_SN.Add("0011-234-693", "15704030")
        vSortedList_SN.Add("0011-234-694", "15070403")
        vSortedList_SN.Add("0011-234-695", "15070304")
        vSortedList_SN.Add("0011-234-696", "15030704")
        vSortedList_SN.Add("0011-234-697", "15030407")
        vSortedList_SN.Add("0011-234-698", "15040703")
        vSortedList_SN.Add("0011-234-699", "15040307")
        vSortedList_SN.Add("0011-234-700", "15704050")
        vSortedList_SN.Add("0011-234-701", "15070405")
        vSortedList_SN.Add("0011-234-702", "15070504")
        vSortedList_SN.Add("0011-234-703", "15040705")
        vSortedList_SN.Add("0011-234-704", "15040507")
        vSortedList_SN.Add("0011-234-705", "15050407")
        vSortedList_SN.Add("0011-234-706", "15050704")
        vSortedList_SN.Add("0011-234-707", "15704060")


    End Sub
    Public Sub sFill_ServerName_SortedList()
        vSortedList_ServerName.Clear()
        vSortedList_ServerName.Add("1", "DESKTOP-9CF0VAB\DOTNET_2019")
        vSortedList_ServerName.Add("2", "HAI\DOTNET")
        vSortedList_ServerName.Add("3", "SERVER\DOTNET")
        vSortedList_ServerName.Add("4", "PC\DOTNET")
    End Sub

    Public Function GetCPUId() As String
        Dim cpuInfo As String = String.Empty
        Dim temp As String = String.Empty
        Dim mc As ManagementClass = _
            New ManagementClass("Win32_Processor")
        Dim moc As ManagementObjectCollection = mc.GetInstances()
        For Each mo As ManagementObject In moc
            If cpuInfo = String.Empty Then
                cpuInfo = _
                 mo.Properties("ProcessorId").Value.ToString()
            End If
        Next
        Return cpuInfo
    End Function
    Public Function fCheck_PrintPermissions(ByVal pMod_Code As String, ByVal pType As String, ByVal pCtrl_Code As String)
        'Here I Get if the user have the privilage to print or No

        Dim vIsAdmin As String = cControls.fReturnValue(" Select IsNull(IsAdmin, 'N') From Employees Where Code = '" & vUsrCode & "' ", "")

        If vIsAdmin = "Y" Then
            Return "Y"
        Else
            Dim vSqlString As String
            vSqlString = " Select Enabled " &
                         " From   Profiles_Controls INNER JOIN Employees " &
                         " ON     Employees.Profile = Profiles_Controls.Prf_Code " &
                         " And    Employees.Company_Code = Profiles_Controls.Company_Code " &
                         "                                               " &
                         " Where  Employees.Code =  '" & vUsrCode & "'            " &
                         " And    Employees.Company_Code = " & vCompanyCode &
                         " And    Mod_Code       = '" & pMod_Code & "' " &
                         " And    Type           = '" & pType & "'      " &
                         " And    Ctrl_Code      = '" & pCtrl_Code & "' "

            Return cControls.fReturnValue(vSqlString, "")
        End If
    End Function

    Public Function sGetColor(ByVal pColorValue As Object, ByVal pRow As UltraGridRow)
        If Not IsDBNull(pColorValue) Then
            Dim vFullRGB As String = Trim(pColorValue)
            Dim vRGB() As String

            vFullRGB = vFullRGB.Replace("Color", "")
            vFullRGB = vFullRGB.Replace("[", "")
            vFullRGB = vFullRGB.Replace("]", "")
            vFullRGB = vFullRGB.Replace(" ", "")
            vFullRGB = vFullRGB.Replace("=", "")

            If vFullRGB.Contains(",") Then
                vFullRGB = vFullRGB.Replace("A", "")
                vFullRGB = vFullRGB.Replace("R", "")
                vFullRGB = vFullRGB.Replace("G", "")
                vFullRGB = vFullRGB.Replace("B", "")
                vRGB = vFullRGB.Split(",")

                If vRGB.Length = 3 Then
                    pRow.Cells("Colors").Appearance.BackColor = Color.FromArgb(vRGB(0), vRGB(1), vRGB(2))
                Else
                    pRow.Cells("Colors").Appearance.BackColor = Color.FromArgb(vRGB(0), vRGB(1), vRGB(2), vRGB(3))
                End If
            Else
                pRow.Cells("Colors").Appearance.BackColor = Color.FromName(vFullRGB)
            End If
        End If
    End Function
    Public Sub sGet_First_AllowEdit_Cell(ByVal pGrd As UltraGrid)
        Do
            pGrd.PerformAction(UltraGridAction.NextCell)
        Loop Until pGrd.DisplayLayout.Bands(0).Columns(pGrd.ActiveCell.Column.Index).CellActivation = Activation.AllowEdit

        pGrd.PerformAction(UltraGridAction.EnterEditMode)
    End Sub
#End Region
End Module
