Public Class Frm_PicturePreview

    Public Sub New(ByVal pImage As Bitmap)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        UltraPictureBox1.Image = pImage
    End Sub

    Private Declare Auto Function BitBlt Lib "gdi32.dll" (ByVal _
    hdcDest As IntPtr, ByVal nXDest As Integer, ByVal _
    nYDest As Integer, ByVal nWidth As Integer, ByVal _
    nHeight As Integer, ByVal hdcSrc As IntPtr, ByVal nXSrc _
    As Integer, ByVal nYSrc As Integer, ByVal dwRop As _
    System.Int32) As Boolean
    Private Const SRCCOPY As Integer = &HCC0020

    ' Variables used to print.
    Private m_PrintBitmap As Bitmap
    Private WithEvents m_PrintDocument As Printing.PrintDocument


    Private Function GetFormImage() As Bitmap
        ' Get this form's Graphics object.
        Dim me_gr As Graphics = Me.CreateGraphics

        ' Make a Bitmap to hold the image.
        Dim bm As New Bitmap(Me.ClientSize.Width, _
            Me.ClientSize.Height, me_gr)
        Dim bm_gr As Graphics = me_gr.FromImage(bm)
        Dim bm_hdc As IntPtr = bm_gr.GetHdc

        ' Get the form's hDC. We must do this after 
        ' creating the new Bitmap, which uses me_gr.
        Dim me_hdc As IntPtr = me_gr.GetHdc

        ' BitBlt the form's image onto the Bitmap.
        BitBlt(bm_hdc, 0, 0, Me.ClientSize.Width, _
            Me.ClientSize.Height, _
            me_hdc, 0, 0, SRCCOPY)
        me_gr.ReleaseHdc(me_hdc)
        bm_gr.ReleaseHdc(bm_hdc)

        ' Return the result.
        Return bm
    End Function

    ' Print the form image.
    Private Sub m_PrintDocument_PrintPage(ByVal sender As _
        Object, ByVal e As _
        System.Drawing.Printing.PrintPageEventArgs) Handles _
        m_PrintDocument.PrintPage
        ' Draw the image centered.
        Dim x As Integer = e.MarginBounds.X + _
            (e.MarginBounds.Width - m_PrintBitmap.Width) \ 2
        Dim y As Integer = e.MarginBounds.Y + _
            (e.MarginBounds.Height - m_PrintBitmap.Height) \ 2
        e.Graphics.DrawImage(m_PrintBitmap, x, y)

        ' There's only one page.
        e.HasMorePages = False
    End Sub

    Private Sub ToolBar_Main_ToolClick(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs) Handles ToolBar_Main.ToolClick
        Select Case e.Tool.Key
            Case "Print"
                m_PrintBitmap = GetFormImage()

                ' Make a PrintDocument and print.
                m_PrintDocument = New Printing.PrintDocument
                m_PrintDocument.Print()
        End Select
    End Sub
End Class