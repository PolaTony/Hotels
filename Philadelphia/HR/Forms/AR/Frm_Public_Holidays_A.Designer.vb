<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Public_Holidays_A
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Public_Holidays_A))
        Dim UltraToolbar1 As Infragistics.Win.UltraWinToolbars.UltraToolbar = New Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1")
        Dim LabelTool1 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim LabelTool2 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool1")
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim LabelTool3 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool1")
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim LabelTool4 As Infragistics.Win.UltraWinToolbars.LabelTool = New Infragistics.Win.UltraWinToolbars.LabelTool("LabelTool2")
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraCalendarLook1 = New Infragistics.Win.UltraWinSchedule.UltraCalendarLook(Me.components)
        Me.UltraMonthViewSingle1 = New Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle()
        Me.UltraCalendarInfo1 = New Infragistics.Win.UltraWinSchedule.UltraCalendarInfo(Me.components)
        Me.debounceTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolBar_Main = New Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1_Fill_Panel = New Infragistics.Win.Misc.UltraPanel()
        Me.Btn_Close = New Infragistics.Win.Misc.UltraButton()
        Me.Btn_Add_Holiday = New Infragistics.Win.Misc.UltraButton()
        Me._Panel1_Toolbars_Dock_Area_Left = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._Panel1_Toolbars_Dock_Area_Right = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._Panel1_Toolbars_Dock_Area_Bottom = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        Me._Panel1_Toolbars_Dock_Area_Top = New Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea()
        CType(Me.UltraMonthViewSingle1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel1_Fill_Panel.ClientArea.SuspendLayout()
        Me.Panel1_Fill_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraCalendarLook1
        '
        Appearance1.FontData.Name = "Droid Arabic Kufi"
        Appearance1.FontData.SizeInPoints = 20.0!
        Me.UltraCalendarLook1.AppointmentAppearance = Appearance1
        Appearance2.FontData.SizeInPoints = 20.0!
        Me.UltraCalendarLook1.HolidayAppearance = Appearance2
        Appearance3.FontData.SizeInPoints = 20.0!
        Me.UltraCalendarLook1.MonthAppearance = Appearance3
        Appearance4.FontData.SizeInPoints = 20.0!
        Me.UltraCalendarLook1.NoteAppearance = Appearance4
        Me.UltraCalendarLook1.SaveSettings = True
        Appearance5.FontData.SizeInPoints = 20.0!
        Me.UltraCalendarLook1.SelectedAppointmentAppearance = Appearance5
        Me.UltraCalendarLook1.SettingsKey = "FRM_Exceptions.UltraCalendarLook1"
        Me.UltraCalendarLook1.ViewStyle = Infragistics.Win.UltraWinSchedule.ViewStyle.Office2007
        '
        'UltraMonthViewSingle1
        '
        Me.UltraMonthViewSingle1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance6.FontData.SizeInPoints = 11.0!
        Me.UltraMonthViewSingle1.Appearance = Appearance6
        Me.UltraMonthViewSingle1.CalendarInfo = Me.UltraCalendarInfo1
        Me.UltraMonthViewSingle1.CalendarLook = Me.UltraCalendarLook1
        Me.UltraMonthViewSingle1.Location = New System.Drawing.Point(0, 123)
        Me.UltraMonthViewSingle1.MaximumOwnersInView = 5
        Me.UltraMonthViewSingle1.Name = "UltraMonthViewSingle1"
        Me.UltraMonthViewSingle1.Size = New System.Drawing.Size(1111, 512)
        Me.UltraMonthViewSingle1.TabIndex = 1
        Me.UltraMonthViewSingle1.VisibleWeeks = 3
        Me.UltraMonthViewSingle1.WeekHeaderDisplayStyle = Infragistics.Win.UltraWinSchedule.WeekHeaderDisplayStyle.None
        Me.UltraMonthViewSingle1.YearDisplayStyle = Infragistics.Win.UltraWinSchedule.YearDisplayStyleEnum.FirstDayOfMonth
        '
        'UltraCalendarInfo1
        '
        Me.UltraCalendarInfo1.AllowRecurringAppointments = True
        Me.UltraCalendarInfo1.ReminderImage = CType(resources.GetObject("UltraCalendarInfo1.ReminderImage"), System.Drawing.Image)
        Me.UltraCalendarInfo1.SaveSettings = True
        Me.UltraCalendarInfo1.SaveSettingsCategories = CType((((((((((((Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.General Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.AppearancesCollection) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.Appointments) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.Holidays) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.Notes) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.DaysOfWeek) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.DaysOfMonth) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.DaysOfYear) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.MonthsOfYear) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.WeeksOfYear) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.Years) _
            Or Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.Owners), Infragistics.Win.UltraWinSchedule.CalendarInfoCategories)
        Me.UltraCalendarInfo1.SettingsKey = "FRM_Exceptions.UltraCalendarInfo1"
        Me.UltraCalendarInfo1.WeekRule = System.Globalization.CalendarWeekRule.FirstFourDayWeek
        '
        'debounceTimer
        '
        Me.debounceTimer.Interval = 1000
        '
        'ToolBar_Main
        '
        Me.ToolBar_Main.DesignerFlags = 1
        Me.ToolBar_Main.DockWithinContainer = Me.Panel1
        Me.ToolBar_Main.MultiMonitorDropDownBehavior = Infragistics.Win.UltraWinToolbars.MultiMonitorDropDownBehavior.ShiftToMonitorWithExclusionRect
        Me.ToolBar_Main.RightAlignedMenus = Infragistics.Win.DefaultableBoolean.[False]
        Me.ToolBar_Main.RuntimeCustomizationOptions = Infragistics.Win.UltraWinToolbars.RuntimeCustomizationOptions.None
        Me.ToolBar_Main.ShowFullMenusDelay = 500
        Me.ToolBar_Main.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.OfficeXP
        UltraToolbar1.DockedColumn = 0
        UltraToolbar1.DockedRow = 0
        UltraToolbar1.NonInheritedTools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool1, LabelTool2})
        Appearance9.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance9.BackColor2 = System.Drawing.SystemColors.Control
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        UltraToolbar1.Settings.Appearance = Appearance9
        UltraToolbar1.Settings.FillEntireRow = Infragistics.Win.DefaultableBoolean.[True]
        UltraToolbar1.Text = "ToolBar1"
        Me.ToolBar_Main.Toolbars.AddRange(New Infragistics.Win.UltraWinToolbars.UltraToolbar() {UltraToolbar1})
        Appearance10.FontData.BoldAsString = "True"
        Appearance10.FontData.Name = "Tahoma"
        Appearance10.TextHAlignAsString = "Right"
        LabelTool3.SharedPropsInternal.AppearancesSmall.Appearance = Appearance10
        LabelTool3.SharedPropsInternal.Caption = "الاجازات العامة"
        LabelTool4.SharedPropsInternal.Spring = True
        Me.ToolBar_Main.Tools.AddRange(New Infragistics.Win.UltraWinToolbars.ToolBase() {LabelTool3, LabelTool4})
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel1_Fill_Panel)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Left)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Right)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Bottom)
        Me.Panel1.Controls.Add(Me._Panel1_Toolbars_Dock_Area_Top)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1123, 638)
        Me.Panel1.TabIndex = 103
        '
        'Panel1_Fill_Panel
        '
        '
        'Panel1_Fill_Panel.ClientArea
        '
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Btn_Close)
        Me.Panel1_Fill_Panel.ClientArea.Controls.Add(Me.Btn_Add_Holiday)
        Me.Panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1_Fill_Panel.Location = New System.Drawing.Point(0, 23)
        Me.Panel1_Fill_Panel.Name = "Panel1_Fill_Panel"
        Me.Panel1_Fill_Panel.Size = New System.Drawing.Size(1123, 615)
        Me.Panel1_Fill_Panel.TabIndex = 0
        '
        'Btn_Close
        '
        Me.Btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance7.BackColor = System.Drawing.Color.White
        Appearance7.Image = CType(resources.GetObject("Appearance7.Image"), Object)
        Appearance7.ImageHAlign = Infragistics.Win.HAlign.Center
        Me.Btn_Close.Appearance = Appearance7
        Me.Btn_Close.ImageSize = New System.Drawing.Size(50, 50)
        Me.Btn_Close.Location = New System.Drawing.Point(1040, 14)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(71, 71)
        Me.Btn_Close.TabIndex = 563
        Me.Btn_Close.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Btn_Add_Holiday
        '
        Appearance8.BackColor = System.Drawing.Color.SteelBlue
        Appearance8.ForeColor = System.Drawing.Color.White
        Appearance8.Image = CType(resources.GetObject("Appearance8.Image"), Object)
        Appearance8.TextHAlignAsString = "Right"
        Me.Btn_Add_Holiday.Appearance = Appearance8
        Me.Btn_Add_Holiday.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat
        Me.Btn_Add_Holiday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Add_Holiday.Font = New System.Drawing.Font("Droid Arabic Kufi", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Add_Holiday.Location = New System.Drawing.Point(24, 24)
        Me.Btn_Add_Holiday.Name = "Btn_Add_Holiday"
        Me.Btn_Add_Holiday.Padding = New System.Drawing.Size(15, 0)
        Me.Btn_Add_Holiday.Size = New System.Drawing.Size(213, 48)
        Me.Btn_Add_Holiday.TabIndex = 562
        Me.Btn_Add_Holiday.Text = "اضافة اجازة"
        Me.Btn_Add_Holiday.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        '_Panel1_Toolbars_Dock_Area_Left
        '
        Me._Panel1_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left
        Me._Panel1_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Left.Location = New System.Drawing.Point(0, 23)
        Me._Panel1_Toolbars_Dock_Area_Left.Name = "_Panel1_Toolbars_Dock_Area_Left"
        Me._Panel1_Toolbars_Dock_Area_Left.Size = New System.Drawing.Size(0, 615)
        Me._Panel1_Toolbars_Dock_Area_Left.ToolbarsManager = Me.ToolBar_Main
        '
        '_Panel1_Toolbars_Dock_Area_Right
        '
        Me._Panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right
        Me._Panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Right.Location = New System.Drawing.Point(1123, 23)
        Me._Panel1_Toolbars_Dock_Area_Right.Name = "_Panel1_Toolbars_Dock_Area_Right"
        Me._Panel1_Toolbars_Dock_Area_Right.Size = New System.Drawing.Size(0, 615)
        Me._Panel1_Toolbars_Dock_Area_Right.ToolbarsManager = Me.ToolBar_Main
        '
        '_Panel1_Toolbars_Dock_Area_Bottom
        '
        Me._Panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom
        Me._Panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Bottom.Location = New System.Drawing.Point(0, 638)
        Me._Panel1_Toolbars_Dock_Area_Bottom.Name = "_Panel1_Toolbars_Dock_Area_Bottom"
        Me._Panel1_Toolbars_Dock_Area_Bottom.Size = New System.Drawing.Size(1123, 0)
        Me._Panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = Me.ToolBar_Main
        '
        '_Panel1_Toolbars_Dock_Area_Top
        '
        Me._Panel1_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me._Panel1_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control
        Me._Panel1_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top
        Me._Panel1_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Panel1_Toolbars_Dock_Area_Top.Location = New System.Drawing.Point(0, 0)
        Me._Panel1_Toolbars_Dock_Area_Top.Name = "_Panel1_Toolbars_Dock_Area_Top"
        Me._Panel1_Toolbars_Dock_Area_Top.Size = New System.Drawing.Size(1123, 23)
        Me._Panel1_Toolbars_Dock_Area_Top.ToolbarsManager = Me.ToolBar_Main
        '
        'Frm_Public_Holidays_A
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1123, 638)
        Me.Controls.Add(Me.UltraMonthViewSingle1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Public_Holidays_A"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الاجازات العامة"
        CType(Me.UltraCalendarLook1, System.Configuration.IPersistComponentSettings).LoadComponentSettings()
        CType(Me.UltraMonthViewSingle1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraCalendarInfo1, System.Configuration.IPersistComponentSettings).LoadComponentSettings()
        CType(Me.ToolBar_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1_Fill_Panel.ClientArea.ResumeLayout(False)
        Me.Panel1_Fill_Panel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraCalendarLook1 As Infragistics.Win.UltraWinSchedule.UltraCalendarLook
    Friend WithEvents UltraCalendarInfo1 As Infragistics.Win.UltraWinSchedule.UltraCalendarInfo
    Friend WithEvents UltraMonthViewSingle1 As Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle
    Friend WithEvents debounceTimer As Timer
    Friend WithEvents ToolBar_Main As Infragistics.Win.UltraWinToolbars.UltraToolbarsManager
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel1_Fill_Panel As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Left As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Right As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Bottom As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents _Panel1_Toolbars_Dock_Area_Top As Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea
    Friend WithEvents Btn_Add_Holiday As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Close As Infragistics.Win.Misc.UltraButton
End Class
