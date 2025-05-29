Module Module1
    Public Sub AutoComplete(ByVal cbo As ComboBox, _
        ByVal e As System.Windows.Forms.KeyEventArgs)
        ' Call this from your form passing in the name 
        ' of your combobox and the event arg:
        ' AutoComplete(cboState, e)
        Dim iIndex As Integer
        Dim sActual As String
        Dim sFound As String
        Dim bMatchFound As Boolean

        'check if the text is blank or not, if not then only proceed
        If Not cbo.Text = "" Then 'if the text is not blank then only proceed

            ' If backspace then remove the last character 
            ' that was typed in and try to find 
            ' a match. note that the selected text from the 
            ' last character typed in to the 
            ' end of the combo text field will also be deleted.
            If e.KeyCode = Keys.Back Then
                cbo.Text = Mid(cbo.Text, 1, Len(cbo.Text) - 1)
            End If

            ' Do nothing for some keys such as navigation keys...
            If ((e.KeyCode = Keys.Left) Or _
             (e.KeyCode = Keys.Right) Or _
             (e.KeyCode = Keys.Up) Or _
             (e.KeyCode = Keys.Down) Or _
             (e.KeyCode = Keys.PageUp) Or _
             (e.KeyCode = Keys.PageDown) Or _
             (e.KeyCode = Keys.Home) Or _
             (e.KeyCode = Keys.End)) Then
                Return
            End If


            Do
                ' Store the actual text that has been typed.
                sActual = cbo.Text
                ' Find the first match for the typed value.
                iIndex = cbo.FindString(sActual)
                ' Get the text of the first match.
                ' if index > -1 then a match was found.

                If (iIndex > -1) Then '** FOUND SECTION **
                    sFound = cbo.Items(iIndex).ToString()
                    ' Select this item from the list.
                    cbo.SelectedIndex = iIndex
                    ' Select the portion of the text that was automatically
                    ' added so that additional typing will replace it.
                    cbo.SelectionStart = sActual.Length
                    cbo.SelectionLength = sFound.Length
                    bMatchFound = True
                Else '** NOT FOUND SECTION **

                    'if there isn't a match and the text typed in is only 1 character 
                    'or nothing then just select the first entry in the combo box.

                    If sActual.Length = 1 Or sActual.Length = 0 Then
                        cbo.SelectedIndex = 0
                        cbo.SelectionStart = 0
                        cbo.SelectionLength = Len(cbo.Text)
                        bMatchFound = True

                    Else

                        'if there isn't a match for the text typed in then 
                        'remove the last character of the text typed in
                        'and try to find a match.

                        '************************** NOTE **************************
                        'COMMENT THE FOLLOWING THREE LINES AND UNCOMMENT 
                        'THE (bMatchFound = True) LINE 
                        'INCASE YOU WANT TO ALLOW TEXTS TO BE TYPED IN
                        ' WHICH ARE NOT IN THE LIST. ELSE IF 
                        'YOU WANT TO RESTRICT THE USER TO TYPE TEXTS WHICH ARE 
                        'NOT IN THE LIST THEN LEAVE THE FOLLOWING THREE LINES AS IT IS
                        'AND COMMENT THE (bMatchFound = True) LINE.
                        '***********************************************************
                        '/////// THREE LINES SECTION STARTS HERE ///////////
                        cbo.SelectionStart = sActual.Length - 1
                        cbo.SelectionLength = sActual.Length - 1
                        cbo.Text = Mid(cbo.Text, 1, Len(cbo.Text) - 1)
                        '/////// THREE LINES SECTION ENDS HERE /////////////
                        'bMatchFound = True

                    End If

                End If

            Loop Until bMatchFound

        End If

    End Sub

End Module
