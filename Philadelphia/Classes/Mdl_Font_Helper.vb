Imports System.Drawing.Text
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing

Module Mdl_Font_Helper

    Private PrivateFonts As New PrivateFontCollection()

    ''' <summary>
    ''' Changes the font family of the given form and its controls using a custom font file in the project folder,
    ''' but preserves original font size and style.
    ''' </summary>
    ''' <param name="targetForm">The form to update</param>
    ''' <param name="fontFileName">The font file name (e.g., "MyFont.ttf") located in "Fonts" subfolder</param>
    Public Sub ApplyCustomFontFamilyToForm(ByVal targetForm As Form, ByVal fontFileName As String)
        Try
            Dim fontPath As String = Path.Combine(Application.StartupPath, "Fonts", fontFileName)

            If Not File.Exists(fontPath) Then
                MessageBox.Show($"Font file not found: {fontPath}", "Font Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Load the font file
            PrivateFonts.AddFontFile(fontPath)
            Dim newFontFamily As FontFamily = PrivateFonts.Families(0)

            ' Apply new font family to all controls (keeping size and style)
            SetFontFamilyRecursive(targetForm, newFontFamily)

            MessageBox.Show("Font family applied: " & newFontFamily.Name)

        Catch ex As Exception
            MessageBox.Show("Error applying custom font family: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetFontFamilyRecursive(ByVal ctrl As Control, ByVal newFamily As FontFamily)
        ' Replace only the font family, preserve size and style
        ctrl.Font = New Font(newFamily, ctrl.Font.Size, ctrl.Font.Style)

        ' Do the same for all children
        For Each child As Control In ctrl.Controls
            SetFontFamilyRecursive(child, newFamily)
        Next
    End Sub

End Module
