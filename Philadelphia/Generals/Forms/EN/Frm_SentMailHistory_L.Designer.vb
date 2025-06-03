<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SentMailHistory_L
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
        Me.components = New System.ComponentModel.Container
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Code")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("From")
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("To")
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TDate")
        Dim UltraGridColumn15 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("CC")
        Dim UltraGridColumn16 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Bcc")
        Dim UltraGridColumn17 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject")
        Dim UltraGridColumn18 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Message")
        Dim UltraGridColumn19 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("CompleteFileName")
        Dim UltraGridColumn20 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AttachedFile")
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_SentMailHistory_L))
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim ScrollBarLook1 As Infragistics.Win.UltraWinScrollBar.ScrollBarLook = New Infragistics.Win.UltraWinScrollBar.ScrollBarLook
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Code")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("From")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("To")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("TDate")
        Dim UltraDataColumn5 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("CC")
        Dim UltraDataColumn6 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Bcc")
        Dim UltraDataColumn7 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Subject")
        Dim UltraDataColumn8 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Message")
        Dim UltraDataColumn9 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("CompleteFileName")
        Dim UltraDataColumn10 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AttachedFile")
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl
        Me.UltraTextEditor1 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Txt_ToSummaryDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
        Me.Label16 = New System.Windows.Forms.Label
        Me.TXT_FromSummaryDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
        Me.Label7 = New System.Windows.Forms.Label
        Me.Txt_FndByDesc = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Grd_Summary = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.DTS_Summary = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.Txt_FndByCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Tab_Main = New Infragistics.Win.UltraWinTabControl.UltraTabControl
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl
        Me.Tab_Details = New Infragistics.Win.UltraWinTabControl.UltraTabControl
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl
        Me.Grd_AddDed = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.Txt_Beneficiary = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Txt_AddDed = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Txt_ItemProperties = New Infragistics.Win.UltraWinEditors.UltraComboEditor
        Me.Label2 = New System.Windows.Forms.Label
        Me.Txt_Total_After_AD = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl
        Me.Grd_Items = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.Txt_Items = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Txt_StrCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Txt_AdditionalInformation1 = New Infragistics.Win.UltraWinEditors.UltraComboEditor
        Me.UltraPictureBox1 = New Infragistics.Win.UltraWinEditors.UltraPictureBox
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.UltraTextEditor1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_ToSummaryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TXT_FromSummaryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_FndByDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grd_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTS_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_FndByCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.Tab_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tab_Main.SuspendLayout()
        CType(Me.Tab_Details, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tab_Details.SuspendLayout()
        CType(Me.Grd_AddDed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Beneficiary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_AddDed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_ItemProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Total_After_AD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Grd_Items, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Items, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_StrCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_AdditionalInformation1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraTextEditor1)
        Me.UltraTabPageControl1.Controls.Add(Me.Label18)
        Me.UltraTabPageControl1.Controls.Add(Me.Label17)
        Me.UltraTabPageControl1.Controls.Add(Me.Txt_ToSummaryDate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label16)
        Me.UltraTabPageControl1.Controls.Add(Me.TXT_FromSummaryDate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label7)
        Me.UltraTabPageControl1.Controls.Add(Me.Txt_FndByDesc)
        Me.UltraTabPageControl1.Controls.Add(Me.Label6)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox2)
        Me.UltraTabPageControl1.Controls.Add(Me.Grd_Summary)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraPictureBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.Txt_FndByCode)
        Me.UltraTabPageControl1.Controls.Add(Me.Label4)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(998, 395)
        '
        'UltraTextEditor1
        '
        Me.UltraTextEditor1.AlwaysInEditMode = True
        Me.UltraTextEditor1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance18.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance18.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance18.ForeColor = System.Drawing.Color.Blue
        Appearance18.TextHAlignAsString = "Center"
        Me.UltraTextEditor1.Appearance = Appearance18
        Me.UltraTextEditor1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraTextEditor1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!)
        Me.UltraTextEditor1.Location = New System.Drawing.Point(627, 306)
        Me.UltraTextEditor1.Name = "UltraTextEditor1"
        Me.UltraTextEditor1.ReadOnly = True
        Me.UltraTextEditor1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.UltraTextEditor1.Size = New System.Drawing.Size(217, 39)
        Me.UltraTextEditor1.TabIndex = 556
        Me.UltraTextEditor1.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraTextEditor1.Visible = False
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Label18.Location = New System.Drawing.Point(575, 322)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(38, 17)
        Me.Label18.TabIndex = 557
        Me.Label18.Text = "Total"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label18.Visible = False
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label17.Location = New System.Drawing.Point(773, 46)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(45, 13)
        Me.Label17.TabIndex = 553
        Me.Label17.Text = "To Date"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_ToSummaryDate
        '
        Me.Txt_ToSummaryDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance19.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance19.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance19.TextHAlignAsString = "Left"
        Me.Txt_ToSummaryDate.Appearance = Appearance19
        Me.Txt_ToSummaryDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance20.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_ToSummaryDate.ButtonAppearance = Appearance20
        Me.Txt_ToSummaryDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_ToSummaryDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance21.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_ToSummaryDate.DropDownAppearance = Appearance21
        Me.Txt_ToSummaryDate.FormatString = "dd-MM-yyyy"
        Me.Txt_ToSummaryDate.Location = New System.Drawing.Point(840, 43)
        Me.Txt_ToSummaryDate.MaskInput = "{LOC}dd/mm/yyyy"
        Me.Txt_ToSummaryDate.Name = "Txt_ToSummaryDate"
        Me.Txt_ToSummaryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_ToSummaryDate.Size = New System.Drawing.Size(149, 20)
        Me.Txt_ToSummaryDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.Txt_ToSummaryDate.TabIndex = 552
        Me.Txt_ToSummaryDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.Txt_ToSummaryDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_ToSummaryDate.Value = Nothing
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label16.Location = New System.Drawing.Point(773, 20)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(57, 13)
        Me.Label16.TabIndex = 551
        Me.Label16.Text = "From Date"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TXT_FromSummaryDate
        '
        Me.TXT_FromSummaryDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance22.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance22.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance22.TextHAlignAsString = "Left"
        Me.TXT_FromSummaryDate.Appearance = Appearance22
        Me.TXT_FromSummaryDate.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance23.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TXT_FromSummaryDate.ButtonAppearance = Appearance23
        Me.TXT_FromSummaryDate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.TXT_FromSummaryDate.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance24.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TXT_FromSummaryDate.DropDownAppearance = Appearance24
        Me.TXT_FromSummaryDate.FormatString = "dd-MM-yyyy"
        Me.TXT_FromSummaryDate.Location = New System.Drawing.Point(840, 17)
        Me.TXT_FromSummaryDate.MaskInput = "{LOC}dd/mm/yyyy"
        Me.TXT_FromSummaryDate.Name = "TXT_FromSummaryDate"
        Me.TXT_FromSummaryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TXT_FromSummaryDate.Size = New System.Drawing.Size(149, 20)
        Me.TXT_FromSummaryDate.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.TXT_FromSummaryDate.TabIndex = 550
        Me.TXT_FromSummaryDate.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.TXT_FromSummaryDate.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.TXT_FromSummaryDate.Value = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label7.Location = New System.Drawing.Point(263, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 487
        Me.Label7.Text = "By Descriptions"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label7.Visible = False
        '
        'Txt_FndByDesc
        '
        Me.Txt_FndByDesc.AlwaysInEditMode = True
        Me.Txt_FndByDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance25.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance25.TextHAlignAsString = "Left"
        Me.Txt_FndByDesc.Appearance = Appearance25
        Me.Txt_FndByDesc.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_FndByDesc.Location = New System.Drawing.Point(349, 20)
        Me.Txt_FndByDesc.Name = "Txt_FndByDesc"
        Me.Txt_FndByDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_FndByDesc.Size = New System.Drawing.Size(402, 20)
        Me.Txt_FndByDesc.TabIndex = 486
        Me.Txt_FndByDesc.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_FndByDesc.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label6.Location = New System.Drawing.Point(87, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 485
        Me.Label6.Text = "By Code"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label6.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 69)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(982, 2)
        Me.GroupBox2.TabIndex = 484
        Me.GroupBox2.TabStop = False
        '
        'Grd_Summary
        '
        Me.Grd_Summary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grd_Summary.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Summary.DataSource = Me.DTS_Summary
        Appearance1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance1.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.None
        Appearance1.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance1.FontData.SizeInPoints = 10.0!
        Appearance1.TextHAlignAsString = "Left"
        Me.Grd_Summary.DisplayLayout.Appearance = Appearance1
        Me.Grd_Summary.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridColumn11.Header.VisiblePosition = 0
        UltraGridColumn11.Hidden = True
        UltraGridColumn12.Header.VisiblePosition = 1
        UltraGridColumn12.RowLayoutColumnInfo.OriginX = 1
        UltraGridColumn12.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn12.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn12.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn13.Header.VisiblePosition = 3
        UltraGridColumn13.RowLayoutColumnInfo.OriginX = 3
        UltraGridColumn13.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn13.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn13.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn14.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        UltraGridColumn14.Format = "dd-MM-yyyy"
        UltraGridColumn14.Header.Caption = "Date"
        UltraGridColumn14.Header.VisiblePosition = 2
        UltraGridColumn14.RowLayoutColumnInfo.OriginX = 0
        UltraGridColumn14.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn14.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(121, 29)
        UltraGridColumn14.RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 43)
        UltraGridColumn14.RowLayoutColumnInfo.SpanX = 1
        UltraGridColumn14.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn14.Width = 50
        UltraGridColumn15.Header.VisiblePosition = 4
        UltraGridColumn15.RowLayoutColumnInfo.OriginX = 5
        UltraGridColumn15.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn15.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn15.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn16.Header.VisiblePosition = 5
        UltraGridColumn16.RowLayoutColumnInfo.OriginX = 7
        UltraGridColumn16.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn16.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn16.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn17.Header.VisiblePosition = 6
        UltraGridColumn17.RowLayoutColumnInfo.OriginX = 9
        UltraGridColumn17.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn17.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn17.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn18.Header.VisiblePosition = 7
        UltraGridColumn18.RowLayoutColumnInfo.OriginX = 11
        UltraGridColumn18.RowLayoutColumnInfo.OriginY = 0
        UltraGridColumn18.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(107, 0)
        UltraGridColumn18.RowLayoutColumnInfo.SpanX = 2
        UltraGridColumn18.RowLayoutColumnInfo.SpanY = 2
        UltraGridColumn19.Header.VisiblePosition = 9
        UltraGridColumn19.Hidden = True
        UltraGridColumn20.Header.VisiblePosition = 8
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn11, UltraGridColumn12, UltraGridColumn13, UltraGridColumn14, UltraGridColumn15, UltraGridColumn16, UltraGridColumn17, UltraGridColumn18, UltraGridColumn19, UltraGridColumn20})
        Appearance36.FontData.Name = "Tahoma"
        Appearance36.FontData.SizeInPoints = 8.0!
        UltraGridBand1.Header.Appearance = Appearance36
        UltraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        UltraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        UltraGridBand1.Override.AllowRowLayoutColMoving = Infragistics.Win.Layout.GridBagLayoutAllowMoving.AllowAll
        Appearance37.BackColor = System.Drawing.Color.White
        Appearance37.BackColor2 = System.Drawing.Color.GhostWhite
        Appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance37.ForeColor = System.Drawing.Color.Blue
        UltraGridBand1.Override.CellAppearance = Appearance37
        UltraGridBand1.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        UltraGridBand1.RowLayoutStyle = Infragistics.Win.UltraWinGrid.RowLayoutStyle.GroupLayout
        Me.Grd_Summary.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Summary.DisplayLayout.Override.AddRowAppearance = Appearance4
        Me.Grd_Summary.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom
        Me.Grd_Summary.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand
        Me.Grd_Summary.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free
        Me.Grd_Summary.DisplayLayout.Override.AllowGroupMoving = Infragistics.Win.UltraWinGrid.AllowGroupMoving.WithinBand
        Me.Grd_Summary.DisplayLayout.Override.AllowRowLayoutCellSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.None
        Me.Grd_Summary.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.BasedOnDataType
        Appearance5.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.CellAppearance = Appearance5
        Appearance6.BorderColor = System.Drawing.SystemColors.Control
        Appearance6.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance6.Image = CType(resources.GetObject("Appearance6.Image"), Object)
        Me.Grd_Summary.DisplayLayout.Override.CellButtonAppearance = Appearance6
        Me.Grd_Summary.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains
        Me.Grd_Summary.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow
        Me.Grd_Summary.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.Button
        Me.Grd_Summary.DisplayLayout.Override.GroupBySummaryDisplayStyle = Infragistics.Win.UltraWinGrid.GroupBySummaryDisplayStyle.SummaryCellsAlwaysBelowDescription
        Appearance7.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance7.BackColor2 = System.Drawing.SystemColors.Control
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance7.BorderColor = System.Drawing.SystemColors.Control
        Appearance7.BorderColor3DBase = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.HeaderAppearance = Appearance7
        Me.Grd_Summary.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle
        Me.Grd_Summary.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance8.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.RowAppearance = Appearance8
        Appearance9.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance9.BackColor2 = System.Drawing.SystemColors.Control
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Control
        Appearance9.BorderColor3DBase = System.Drawing.SystemColors.ControlLightLight
        Appearance9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorAppearance = Appearance9
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorWidth = 70
        Me.Grd_Summary.DisplayLayout.Override.RowSizingArea = Infragistics.Win.UltraWinGrid.RowSizingArea.RowSelectorsOnly
        Me.Grd_Summary.DisplayLayout.Override.SummaryDisplayArea = CType((Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed Or Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.GroupByRowsFooter), Infragistics.Win.UltraWinGrid.SummaryDisplayAreas)
        Appearance10.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Summary.DisplayLayout.Override.TemplateAddRowAppearance = Appearance10
        Appearance11.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.Override.TemplateAddRowCellAppearance = Appearance11
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance12.BackColor2 = System.Drawing.SystemColors.ControlLight
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance12.BorderColor = System.Drawing.SystemColors.Control
        ScrollBarLook1.Appearance = Appearance12
        Appearance13.BackColor = System.Drawing.SystemColors.Control
        Appearance13.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump
        Appearance13.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance13.ForeColor = System.Drawing.SystemColors.AppWorkspace
        ScrollBarLook1.ButtonAppearance = Appearance13
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.HorizontalBump
        ScrollBarLook1.ThumbAppearance = Appearance14
        Appearance15.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance15.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance15.BorderColor = System.Drawing.SystemColors.Control
        Appearance15.BorderColor3DBase = System.Drawing.SystemColors.Control
        ScrollBarLook1.TrackAppearance = Appearance15
        Me.Grd_Summary.DisplayLayout.ScrollBarLook = ScrollBarLook1
        Me.Grd_Summary.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.Grd_Summary.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance16.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.SplitterBarHorizontalAppearance = Appearance16
        Appearance17.BackColor = System.Drawing.SystemColors.Control
        Appearance17.BorderColor = System.Drawing.SystemColors.Control
        Appearance17.BorderColor3DBase = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.SplitterBarVerticalAppearance = Appearance17
        Me.Grd_Summary.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.Grd_Summary.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Grd_Summary.Location = New System.Drawing.Point(11, 77)
        Me.Grd_Summary.Name = "Grd_Summary"
        Me.Grd_Summary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Grd_Summary.Size = New System.Drawing.Size(984, 315)
        Me.Grd_Summary.TabIndex = 483
        Me.Grd_Summary.UseFlatMode = Infragistics.Win.DefaultableBoolean.[True]
        '
        'DTS_Summary
        '
        UltraDataColumn4.DataType = GetType(Date)
        Me.DTS_Summary.Band.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4, UltraDataColumn5, UltraDataColumn6, UltraDataColumn7, UltraDataColumn8, UltraDataColumn9, UltraDataColumn10})
        '
        'Txt_FndByCode
        '
        Me.Txt_FndByCode.AlwaysInEditMode = True
        Appearance26.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance26.TextHAlignAsString = "Left"
        Me.Txt_FndByCode.Appearance = Appearance26
        Me.Txt_FndByCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_FndByCode.Location = New System.Drawing.Point(140, 20)
        Me.Txt_FndByCode.Name = "Txt_FndByCode"
        Me.Txt_FndByCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_FndByCode.Size = New System.Drawing.Size(117, 20)
        Me.Txt_FndByCode.TabIndex = 481
        Me.Txt_FndByCode.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_FndByCode.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label4.Location = New System.Drawing.Point(44, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 480
        Me.Label4.Text = "Search"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label4.Visible = False
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Tab_Main)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1002, 421)
        Me.Panel7.TabIndex = 495
        '
        'Tab_Main
        '
        Me.Tab_Main.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.Tab_Main.Controls.Add(Me.UltraTabPageControl1)
        Me.Tab_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tab_Main.Location = New System.Drawing.Point(0, 0)
        Me.Tab_Main.Name = "Tab_Main"
        Me.Tab_Main.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.Tab_Main.Size = New System.Drawing.Size(1002, 421)
        Me.Tab_Main.TabIndex = 498
        Me.Tab_Main.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.Key = "Tab_Summary"
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Summary"
        Me.Tab_Main.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1})
        Me.Tab_Main.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(998, 395)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(998, 395)
        '
        'Tab_Details
        '
        Me.Tab_Details.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tab_Details.Controls.Add(Me.UltraTabPageControl5)
        Me.Tab_Details.Controls.Add(Me.UltraTabPageControl6)
        Me.Tab_Details.Location = New System.Drawing.Point(11, 162)
        Me.Tab_Details.Name = "Tab_Details"
        Me.Tab_Details.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.Tab_Details.Size = New System.Drawing.Size(978, 230)
        Me.Tab_Details.TabIndex = 9
        Me.Tab_Details.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.TopLeft
        UltraTab2.Key = "Tab_Items"
        UltraTab2.TabPage = Me.UltraTabPageControl5
        UltraTab2.Text = "Items"
        UltraTab3.Key = "Tab_AD"
        UltraTab3.TabPage = Me.UltraTabPageControl6
        UltraTab3.Text = "Addition and Deduction"
        Me.Tab_Details.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab2, UltraTab3})
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(974, 204)
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Padding = New System.Windows.Forms.Padding(3)
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(974, 173)
        '
        'Grd_AddDed
        '
        Me.Grd_AddDed.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grd_AddDed.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_AddDed.Location = New System.Drawing.Point(3, 3)
        Me.Grd_AddDed.Name = "Grd_AddDed"
        Me.Grd_AddDed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Grd_AddDed.Size = New System.Drawing.Size(971, 130)
        Me.Grd_AddDed.TabIndex = 538
        Me.Grd_AddDed.UseFlatMode = Infragistics.Win.DefaultableBoolean.[True]
        '
        'Txt_Beneficiary
        '
        Me.Txt_Beneficiary.AcceptsReturn = True
        Me.Txt_Beneficiary.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance27.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance27.FontData.Name = "Tahoma"
        Appearance27.FontData.SizeInPoints = 8.0!
        Me.Txt_Beneficiary.Appearance = Appearance27
        Me.Txt_Beneficiary.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Beneficiary.Location = New System.Drawing.Point(584, 105)
        Me.Txt_Beneficiary.Name = "Txt_Beneficiary"
        Me.Txt_Beneficiary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Beneficiary.Size = New System.Drawing.Size(92, 20)
        Me.Txt_Beneficiary.TabIndex = 529
        Me.Txt_Beneficiary.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_Beneficiary.Visible = False
        '
        'Txt_AddDed
        '
        Me.Txt_AddDed.AcceptsReturn = True
        Me.Txt_AddDed.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance28.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance28.FontData.Name = "Tahoma"
        Appearance28.FontData.SizeInPoints = 8.0!
        Appearance28.TextHAlignAsString = "Left"
        Me.Txt_AddDed.Appearance = Appearance28
        Me.Txt_AddDed.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_AddDed.Location = New System.Drawing.Point(416, 105)
        Me.Txt_AddDed.Name = "Txt_AddDed"
        Me.Txt_AddDed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_AddDed.Size = New System.Drawing.Size(92, 20)
        Me.Txt_AddDed.TabIndex = 530
        Me.Txt_AddDed.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_AddDed.Visible = False
        '
        'Txt_ItemProperties
        '
        Me.Txt_ItemProperties.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance29.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance29.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance29.TextHAlignAsString = "Left"
        Me.Txt_ItemProperties.Appearance = Appearance29
        Me.Txt_ItemProperties.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance30.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_ItemProperties.ButtonAppearance = Appearance30
        Me.Txt_ItemProperties.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_ItemProperties.DropDownListAlignment = Infragistics.Win.DropDownListAlignment.Right
        Me.Txt_ItemProperties.DropDownListWidth = -1
        Me.Txt_ItemProperties.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList
        Me.Txt_ItemProperties.Location = New System.Drawing.Point(274, 105)
        Me.Txt_ItemProperties.Name = "Txt_ItemProperties"
        Me.Txt_ItemProperties.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_ItemProperties.Size = New System.Drawing.Size(101, 20)
        Me.Txt_ItemProperties.TabIndex = 537
        Me.Txt_ItemProperties.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_ItemProperties.Visible = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.Label2.Location = New System.Drawing.Point(486, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(267, 19)
        Me.Label2.TabIndex = 581
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Total_After_AD
        '
        Me.Txt_Total_After_AD.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance31.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance31.ForeColor = System.Drawing.Color.Blue
        Appearance31.TextHAlignAsString = "Center"
        Me.Txt_Total_After_AD.Appearance = Appearance31
        Me.Txt_Total_After_AD.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Total_After_AD.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Txt_Total_After_AD.Location = New System.Drawing.Point(759, 142)
        Me.Txt_Total_After_AD.Multiline = True
        Me.Txt_Total_After_AD.Name = "Txt_Total_After_AD"
        Me.Txt_Total_After_AD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Total_After_AD.Scrollbars = System.Windows.Forms.ScrollBars.Vertical
        Me.Txt_Total_After_AD.Size = New System.Drawing.Size(215, 22)
        Me.Txt_Total_After_AD.TabIndex = 581
        Me.Txt_Total_After_AD.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Padding = New System.Windows.Forms.Padding(3)
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(974, 204)
        '
        'Grd_Items
        '
        Me.Grd_Items.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Items.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grd_Items.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Grd_Items.Location = New System.Drawing.Point(3, 3)
        Me.Grd_Items.Name = "Grd_Items"
        Me.Grd_Items.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Grd_Items.Size = New System.Drawing.Size(968, 198)
        Me.Grd_Items.TabIndex = 0
        Me.Grd_Items.UseFlatMode = Infragistics.Win.DefaultableBoolean.[True]
        '
        'Txt_Items
        '
        Me.Txt_Items.AcceptsReturn = True
        Me.Txt_Items.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance32.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance32.FontData.Name = "Tahoma"
        Appearance32.FontData.SizeInPoints = 8.0!
        Me.Txt_Items.Appearance = Appearance32
        Me.Txt_Items.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Items.Location = New System.Drawing.Point(833, 48)
        Me.Txt_Items.Name = "Txt_Items"
        Me.Txt_Items.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Items.Size = New System.Drawing.Size(92, 20)
        Me.Txt_Items.TabIndex = 534
        Me.Txt_Items.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_Items.Visible = False
        '
        'Txt_StrCode
        '
        Me.Txt_StrCode.AcceptsReturn = True
        Me.Txt_StrCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance33.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance33.FontData.Name = "Tahoma"
        Appearance33.FontData.SizeInPoints = 8.0!
        Me.Txt_StrCode.Appearance = Appearance33
        Me.Txt_StrCode.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_StrCode.Location = New System.Drawing.Point(565, 48)
        Me.Txt_StrCode.Name = "Txt_StrCode"
        Me.Txt_StrCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_StrCode.Size = New System.Drawing.Size(92, 20)
        Me.Txt_StrCode.TabIndex = 536
        Me.Txt_StrCode.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_StrCode.Visible = False
        '
        'Txt_AdditionalInformation1
        '
        Me.Txt_AdditionalInformation1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance34.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance34.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance34.TextHAlignAsString = "Left"
        Me.Txt_AdditionalInformation1.Appearance = Appearance34
        Me.Txt_AdditionalInformation1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance35.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_AdditionalInformation1.ButtonAppearance = Appearance35
        Me.Txt_AdditionalInformation1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_AdditionalInformation1.DropDownListAlignment = Infragistics.Win.DropDownListAlignment.Right
        Me.Txt_AdditionalInformation1.DropDownListWidth = -1
        Me.Txt_AdditionalInformation1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList
        Me.Txt_AdditionalInformation1.Location = New System.Drawing.Point(127, 48)
        Me.Txt_AdditionalInformation1.Name = "Txt_AdditionalInformation1"
        Me.Txt_AdditionalInformation1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_AdditionalInformation1.Size = New System.Drawing.Size(133, 20)
        Me.Txt_AdditionalInformation1.TabIndex = 580
        Me.Txt_AdditionalInformation1.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_AdditionalInformation1.Visible = False
        '
        'UltraPictureBox1
        '
        Me.UltraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty
        Me.UltraPictureBox1.Image = CType(resources.GetObject("UltraPictureBox1.Image"), Object)
        Me.UltraPictureBox1.Location = New System.Drawing.Point(11, 16)
        Me.UltraPictureBox1.Name = "UltraPictureBox1"
        Me.UltraPictureBox1.Size = New System.Drawing.Size(32, 28)
        Me.UltraPictureBox1.TabIndex = 482
        Me.UltraPictureBox1.Visible = False
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(974, 204)
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6"
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(974, 204)
        '
        'Frm_SentMailHistory_L
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1002, 421)
        Me.Controls.Add(Me.Panel7)
        Me.Name = "Frm_SentMailHistory_L"
        Me.Text = "Sent Mail History"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        CType(Me.UltraTextEditor1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_ToSummaryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TXT_FromSummaryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_FndByDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grd_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTS_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_FndByCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        CType(Me.Tab_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tab_Main.ResumeLayout(False)
        CType(Me.Tab_Details, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tab_Details.ResumeLayout(False)
        CType(Me.Grd_AddDed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Beneficiary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_AddDed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_ItemProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Total_After_AD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Grd_Items, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Items, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_StrCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_AdditionalInformation1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DTS_Summary As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Tab_Main As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTextEditor1 As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Txt_ToSummaryDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TXT_FromSummaryDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Txt_FndByDesc As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Grd_Summary As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPictureBox1 As Infragistics.Win.UltraWinEditors.UltraPictureBox
    Friend WithEvents Txt_FndByCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Tab_Details As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Grd_AddDed As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Txt_Beneficiary As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Txt_AddDed As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Txt_ItemProperties As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Txt_Total_After_AD As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Grd_Items As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Txt_Items As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Txt_StrCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Txt_AdditionalInformation1 As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
End Class
