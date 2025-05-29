Module Mdl_Permissions
    'This is the select Stores with Permissions to all Screens..
    Public Function vSelectStoresWithPermissions() As String
        'Here I Check first if this user is admin

        Dim vStores As String

        If vIsAdmin <> "Y" Then
            vStores = " Select Stores.Code, Stores.DescA From Stores " &
                    " INNER JOIN Profiles_Stores " &
                    " ON Stores.Code = Profiles_Stores.Store_Code " &
                    " And Stores.Company_Code = Profiles_Stores.Company_Code " &
                    "                                             " &
                    " INNER JOIN Employees " &
                    " ON Employees.Profile = Profiles_Stores.Prf_Code " &
                    " And Employees.Company_Code = Profiles_Stores.Company_Code " &
                    "                                             " &
                    " Where 1 = 1 " &
                    " And Employees.Code = '" & vUsrCode & "' " &
                    " And Employees.Company_Code  = " & vCompanyCode &
                    " And Enabled = 'Y' "
        Else
            vStores = " Select Code, DescA From Stores " &
                      " Where 1 = 1 " &
                      " And   Company_Code = " & vCompanyCode
        End If

        Return vStores
    End Function
    Public Function vSelectBoxesWithPermissions() As String
        Dim vBoxes As String

        If vIsAdmin <> "Y" Then
            vBoxes = " Select Boxes.Code, Boxes.DescA From Boxes " &
                    " INNER JOIN Profiles_Boxes " &
                    " ON Boxes.Code = Profiles_Boxes.Box_Code " &
                    " And Boxes.Company_Code = Profiles_Boxes.Company_Code " &
                    "                                         " &
                    " INNER JOIN Employees " &
                    " ON Employees.Profile = Profiles_Boxes.Prf_Code " &
                    " And Employees.Company_Code = Profiles_Boxes.Company_Code " &
                    "                                                " &
                    " Where 1 = 1 " &
                    " And Employees.Code = '" & vUsrCode & "' " &
                    " And Employees.Company_Code  = " & vCompanyCode &
                    " And Enabled = 'Y'"
        Else
            vBoxes = " Select Code, DescA From Boxes " &
                     " Where 1 = 1 " &
                     " And   Company_Code = " & vCompanyCode
        End If


        Return vBoxes
    End Function

    Public Function vSelectCostCentersWithPermissions() As String
        Dim vIsAdmin As String = cControls.fReturnValue(" Select IsNull(IsAdmin, 'N') From Employees Where Code = '" & vUsrCode & "' ", "")
        Dim vEnabled As String

        If vIsAdmin <> "Y" Then
            vEnabled = " And Enabled = 'Y'"
        Else
            vEnabled = ""
        End If

        Dim vCostCenters As String = " Select Cost_Center.Code, Cost_Center.DescA " &
                                                    " From Cost_Center INNER JOIN Profiles_Departments " &
                                                    " ON   Cost_Center.Code         = Profiles_Departments.Department_Code " &
                                                    " And  Cost_Center.Company_Code = Profiles_Departments.Company_Code " &
                                                    "                                         " &
                                                    " INNER JOIN Employees                    " &
                                                    " ON  Employees.Profile      = Profiles_Departments.Prf_Code " &
                                                    " And Employees.Company_Code = Profiles_Departments.Company_Code " &
                                                    "                                                " &
                                                    " Where 1 = 1 " &
                                                    " And Employees.Code = '" & vUsrCode & "' " &
                                                    " And Employees.Company_Code  = " & vCompanyCode &
                                                    vEnabled

        Return vCostCenters
    End Function

    Public Function fCheckStore_Permission(ByVal pStore_Code As String) As Boolean

        If vIsAdmin = "Y" Then
            Return True
        Else
            If cControls.fReturnValue(" Select IsNull(Enabled, 'N') " &
                                  " From Profiles_Stores INNER JOIN Employees " &
                                  " ON Employees.Profile = Profiles_Stores.Prf_Code " &
                                  " And Employees.Company_Code = Profiles_Stores.Company_Code " &
                                  "                                           " &
                                  " Where Profiles_Stores.Store_Code = '" & pStore_Code & "' " &
                                  " And Employees.Code = '" & vUsrCode & "' " &
                                  " And Employees.Company_Code  = " & vCompanyCode, "") = "Y" Then
                Return True
            Else
                Return False
            End If
        End If

    End Function

    Public Function fcheckBox_Permission(ByVal pBox_Code As String)
        Dim vIsAdmin As String = cControls.fReturnValue(" Select IsNull(IsAdmin, 'N') From Employees Where Code = '" & vUsrCode & "' ", "")

        If vIsAdmin = "Y" Then
            Return True
        Else
            If cControls.fReturnValue(" Select IsNull(Enabled, 'N') " &
                                  " From Profiles_Boxes INNER JOIN Employees " &
                                  " ON Employees.Profile = Profiles_Boxes.Prf_Code " &
                                  " And Employees.Company_Code = Profiles_Boxes.Company_Code " &
                                  "                                          " &
                                  " Where Profiles_Boxes.Box_Code = '" & pBox_Code & "' " &
                                  " And Employees.Code = '" & vUsrCode & "' " &
                                  " And Employees.Company_Code  = " & vCompanyCode, "") = "Y" Then
                Return True
            Else
                Return False

            End If
        End If
    End Function

    Public Function fCheckCostCenter_Permission(ByVal pStore_Code As String) As Boolean

        Dim vIsAdmin As String = cControls.fReturnValue(" Select IsNull(IsAdmin, 'N') From Employees Where Code = '" & vUsrCode & "' ", "")

        If vIsAdmin = "Y" Then
            Return True
        Else
            If cControls.fReturnValue(" Select IsNull(Enabled, 'N') " &
                                  " From Profiles_Departments INNER JOIN Employees " &
                                  " ON Employees.Profile = Profiles_Departments.Prf_Code " &
                                  " And Employees.Company_Code = Profiles_Departments.Company_Code " &
                                  "                                                " &
                                  " Where Profiles_Departments.Department_Code = '" & pStore_Code & "' " &
                                  " And Employees.Code = '" & vUsrCode & "' " &
                                  " And Employees.Company_Code  = " & vCompanyCode, "") = "Y" Then
                Return True
            Else
                Return False

            End If
        End If
    End Function

End Module
