Module mdl_AllProject
    Public Function fIsNull(ByVal pValue As Object, ByVal pReplacedValue As String) As String
        Dim vReturnValue As String
        If IsDBNull(pValue) Then
            vReturnValue = pReplacedValue
        ElseIf pValue = Nothing Then
            vReturnValue = pReplacedValue
        Else
            vReturnValue = pValue
        End If
        Return vReturnValue
    End Function

    Public Function fCheck_QuotationMark(ByVal pString As String) As String
        If pString.Contains("'") Then
            Dim vPosition As Int16
            Dim vModifiedString As String
            vPosition = pString.IndexOf("'")
            vModifiedString = pString.Insert(vPosition, "'")

            Return vModifiedString
        Else
            Return pString
        End If
    End Function
End Module
