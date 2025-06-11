<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Public_Holiday_A
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Public_Holiday_A))
        Me.debounceTimer = New System.Windows.Forms.Timer(Me.components)
        Me.UltraCalendarInfo1 = New Infragistics.Win.UltraWinSchedule.UltraCalendarInfo(Me.components)
        Me.UltraCalendarLook1 = New Infragistics.Win.UltraWinSchedule.UltraCalendarLook(Me.components)
        Me.UltraMonthViewSingle1 = New Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle()
        Me.Btn_Add_Holiday = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_Close = New Infragistics.Win.Misc.UltraButton()
        Me.Label44 = New System.Windows.Forms.Label()
        CType(Me.UltraMonthViewSingle1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'debounceTimer
        '
        Me.debounceTimer.Interval = 1000
        '
        'UltraCalendarInfo1
        '
        Me.UltraCalendarInfo1.DataBindingsForAppointments.BindingContextControl = Me
        Me.UltraCalendarInfo1.DataBindingsForOwners.BindingContextControl = Me
        '
        'UltraCalendarLook1
        '
        Me.UltraCalendarLook1.ViewStyle = Infragistics.Win.UltraWinSchedule.ViewStyle.Office2007
        '
        'UltraMonthViewSingle1
        '
        Appearance1.FontData.SizeInPoints = 11.0!
        Me.UltraMonthViewSingle1.Appearance = Appearance1
        Me.UltraMonthViewSingle1.CalendarInfo = Me.UltraCalendarInfo1
        Me.UltraMonthViewSingle1.CalendarLook = Me.UltraCalendarLook1
        Me.UltraMonthViewSingle1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraMonthViewSingle1.Location = New System.Drawing.Point(0, 151)
        Me.UltraMonthViewSingle1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.UltraMonthViewSingle1.MaximumOwnersInView = 5
        Me.UltraMonthViewSingle1.Name = "UltraMonthViewSingle1"
        Me.UltraMonthViewSingle1.Size = New System.Drawing.Size(917, 484)
        Me.UltraMonthViewSingle1.TabIndex = 2
        Me.UltraMonthViewSingle1.VisibleWeeks = 3
        Me.UltraMonthViewSingle1.WeekHeaderDisplayStyle = Infragistics.Win.UltraWinSchedule.WeekHeaderDisplayStyle.None
        Me.UltraMonthViewSingle1.YearDisplayStyle = Infragistics.Win.UltraWinSchedule.YearDisplayStyleEnum.FirstDayOfMonth
        '
        'Btn_Add_Holiday
        '
        Me.Btn_Add_Holiday.Anchor = System.Windows.Forms.AnchorStyles.None
        Appearance3.BackColor = System.Drawing.Color.DarkOliveGreen
        Appearance3.BorderColor = System.Drawing.Color.Transparent
        Appearance3.ForeColor = System.Drawing.Color.White
        Appearance3.Image = CType(resources.GetObject("Appearance3.Image"), Object)
        Appearance3.TextHAlignAsString = "Right"
        Me.Btn_Add_Holiday.Appearance = Appearance3
        Me.Btn_Add_Holiday.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat
        Me.Btn_Add_Holiday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Add_Holiday.Font = New System.Drawing.Font("Droid Arabic Kufi", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Add_Holiday.ImageSize = New System.Drawing.Size(32, 32)
        Me.Btn_Add_Holiday.Location = New System.Drawing.Point(36, 31)
        Me.Btn_Add_Holiday.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Btn_Add_Holiday.Name = "Btn_Add_Holiday"
        Me.Btn_Add_Holiday.Padding = New System.Drawing.Size(15, 0)
        Me.Btn_Add_Holiday.Size = New System.Drawing.Size(177, 48)
        Me.Btn_Add_Holiday.TabIndex = 563
        Me.Btn_Add_Holiday.Text = "اضافة اجازة"
        Me.Btn_Add_Holiday.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(917, 635)
        Me.Panel1.TabIndex = 609
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 416.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Close, 2, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label44, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Add_Holiday, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(917, 96)
        Me.TableLayoutPanel2.TabIndex = 627
        '
        'Btn_Close
        '
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Appearance2.BorderColor = System.Drawing.Color.Transparent
        Appearance2.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance2.Image = CType(resources.GetObject("Appearance2.Image"), Object)
        Appearance2.ImageHAlign = Infragistics.Win.HAlign.Center
        Me.Btn_Close.Appearance = Appearance2
        Me.Btn_Close.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat
        Me.Btn_Close.Dock = System.Windows.Forms.DockStyle.Right
        Me.Btn_Close.ImageSize = New System.Drawing.Size(70, 70)
        Me.Btn_Close.Location = New System.Drawing.Point(819, 17)
        Me.Btn_Close.Margin = New System.Windows.Forms.Padding(2, 2, 10, 2)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.ShowFocusRect = False
        Me.Btn_Close.ShowOutline = False
        Me.Btn_Close.Size = New System.Drawing.Size(88, 77)
        Me.Btn_Close.TabIndex = 602
        Me.Btn_Close.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label44.Font = New System.Drawing.Font("Droid Arabic Kufi", 17.0!)
        Me.Label44.ForeColor = System.Drawing.Color.White
        Me.Label44.Location = New System.Drawing.Point(252, 15)
        Me.Label44.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(412, 81)
        Me.Label44.TabIndex = 560
        Me.Label44.Text = "الاجازات العامة"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_Public_Holiday_A
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 635)
        Me.Controls.Add(Me.UltraMonthViewSingle1)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Frm_Public_Holiday_A"
        Me.Text = "الاجازات العامة"
        CType(Me.UltraMonthViewSingle1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents debounceTimer As Timer
    Friend WithEvents UltraCalendarInfo1 As Infragistics.Win.UltraWinSchedule.UltraCalendarInfo
    Friend WithEvents UltraCalendarLook1 As Infragistics.Win.UltraWinSchedule.UltraCalendarLook
    Friend WithEvents UltraMonthViewSingle1 As Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle
    Friend WithEvents Btn_Add_Holiday As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_Close As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Label44 As Label
End Class
