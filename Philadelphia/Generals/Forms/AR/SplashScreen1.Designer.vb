<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreen1))
        Me.UltraPictureBox1 = New Infragistics.Win.UltraWinEditors.UltraPictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.UltraPictureBox2 = New Infragistics.Win.UltraWinEditors.UltraPictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'UltraPictureBox1
        '
        Me.UltraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty
        Me.UltraPictureBox1.Image = CType(resources.GetObject("UltraPictureBox1.Image"), Object)
        Me.UltraPictureBox1.Location = New System.Drawing.Point(275, 281)
        Me.UltraPictureBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.UltraPictureBox1.Name = "UltraPictureBox1"
        Me.UltraPictureBox1.ScaleImage = Infragistics.Win.ScaleImage.Always
        Me.UltraPictureBox1.Size = New System.Drawing.Size(98, 76)
        Me.UltraPictureBox1.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 3000
        '
        'UltraPictureBox2
        '
        Me.UltraPictureBox2.BorderShadowColor = System.Drawing.Color.Empty
        Me.UltraPictureBox2.Image = CType(resources.GetObject("UltraPictureBox2.Image"), Object)
        Me.UltraPictureBox2.Location = New System.Drawing.Point(18, 14)
        Me.UltraPictureBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.UltraPictureBox2.Name = "UltraPictureBox2"
        Me.UltraPictureBox2.ScaleImage = Infragistics.Win.ScaleImage.Always
        Me.UltraPictureBox2.Size = New System.Drawing.Size(351, 261)
        Me.UltraPictureBox2.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Droid Arabic Kufi", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 304)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(268, 31)
        Me.Label6.TabIndex = 261
        Me.Label6.Text = "DotNet Software Solutions"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SplashScreen1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(386, 370)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.UltraPictureBox2)
        Me.Controls.Add(Me.UltraPictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SplashScreen1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UltraPictureBox1 As Infragistics.Win.UltraWinEditors.UltraPictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents UltraPictureBox2 As Infragistics.Win.UltraWinEditors.UltraPictureBox
    Friend WithEvents Label6 As Label
End Class
