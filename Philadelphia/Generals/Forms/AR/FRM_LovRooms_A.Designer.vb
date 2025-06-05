<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FRM_LovRooms_A
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
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
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Room_Code")
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Room_Desc")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Room_Type")
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Room_Type_Code")
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM_LovRooms_A))
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ScrollBarLook1 As Infragistics.Win.UltraWinScrollBar.ScrollBarLook = New Infragistics.Win.UltraWinScrollBar.ScrollBarLook()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Room_Code")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Room_Desc")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Room_Type")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Room_Type_Code")
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Btn_Cancel = New Infragistics.Win.Misc.UltraButton()
        Me.Btn_Ok = New Infragistics.Win.Misc.UltraButton()
        Me.Grd_Main = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.DTS_Main = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.Grd_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTS_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Btn_Cancel)
        Me.Panel1.Controls.Add(Me.Btn_Ok)
        Me.Panel1.Controls.Add(Me.Grd_Main)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(625, 453)
        Me.Panel1.TabIndex = 2
        '
        'Btn_Cancel
        '
        Me.Btn_Cancel.Location = New System.Drawing.Point(213, 407)
        Me.Btn_Cancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(87, 36)
        Me.Btn_Cancel.TabIndex = 492
        Me.Btn_Cancel.Text = "«·€«¡"
        '
        'Btn_Ok
        '
        Me.Btn_Ok.Location = New System.Drawing.Point(325, 407)
        Me.Btn_Ok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(87, 36)
        Me.Btn_Ok.TabIndex = 491
        Me.Btn_Ok.Text = "„Ê«›ﬁ"
        '
        'Grd_Main
        '
        Me.Grd_Main.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Main.DataSource = Me.DTS_Main
        Appearance1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance1.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.None
        Appearance1.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance1.TextHAlignAsString = "Right"
        Me.Grd_Main.DisplayLayout.Appearance = Appearance1
        Me.Grd_Main.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn3.Hidden = True
        UltraGridColumn3.Width = 81
        UltraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance2.FontData.BoldAsString = "True"
        Appearance2.FontData.Name = "Tahoma"
        Appearance2.FontData.SizeInPoints = 8.0!
        Appearance2.TextHAlignAsString = "Right"
        UltraGridColumn1.CellAppearance = Appearance2
        UltraGridColumn1.Format = ""
        Appearance3.FontData.Name = "Tahoma"
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        UltraGridColumn1.Header.Appearance = Appearance3
        UltraGridColumn1.Header.Caption = "«·€—›…"
        UltraGridColumn1.Header.VisiblePosition = 1
        UltraGridColumn1.MaskInput = ""
        UltraGridColumn1.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        UltraGridColumn1.Width = 200
        UltraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
        Appearance4.FontData.BoldAsString = "True"
        Appearance4.TextHAlignAsString = "Right"
        UltraGridColumn2.CellAppearance = Appearance4
        Appearance5.FontData.Name = "Tahoma"
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        UltraGridColumn2.Header.Appearance = Appearance5
        UltraGridColumn2.Header.Caption = "‰Ê⁄ «·€—›…"
        UltraGridColumn2.Header.VisiblePosition = 0
        UltraGridColumn2.Width = 397
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.Hidden = True
        UltraGridColumn4.Width = 100
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn3, UltraGridColumn1, UltraGridColumn2, UltraGridColumn4})
        Appearance6.FontData.Name = "Tahoma"
        Appearance6.FontData.SizeInPoints = 8.0!
        UltraGridBand1.Header.Appearance = Appearance6
        UltraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        UltraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        UltraGridBand1.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.Grd_Main.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Appearance7.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Main.DisplayLayout.Override.AddRowAppearance = Appearance7
        Me.Grd_Main.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom
        Appearance8.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Main.DisplayLayout.Override.CellAppearance = Appearance8
        Appearance9.BorderColor = System.Drawing.SystemColors.Control
        Appearance9.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance9.Image = CType(resources.GetObject("Appearance9.Image"), Object)
        Me.Grd_Main.DisplayLayout.Override.CellButtonAppearance = Appearance9
        Appearance10.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance10.BackColor2 = System.Drawing.SystemColors.Control
        Appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance10.BorderColor = System.Drawing.SystemColors.Control
        Appearance10.BorderColor3DBase = System.Drawing.SystemColors.Control
        Me.Grd_Main.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.Grd_Main.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.Standard
        Appearance11.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Main.DisplayLayout.Override.RowAppearance = Appearance11
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance12.BackColor2 = System.Drawing.SystemColors.Control
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance12.BorderColor = System.Drawing.SystemColors.Control
        Appearance12.BorderColor3DBase = System.Drawing.SystemColors.ControlLightLight
        Appearance12.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Main.DisplayLayout.Override.RowSelectorAppearance = Appearance12
        Me.Grd_Main.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement
        Me.Grd_Main.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed
        Appearance13.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Main.DisplayLayout.Override.TemplateAddRowAppearance = Appearance13
        Appearance14.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Main.DisplayLayout.Override.TemplateAddRowCellAppearance = Appearance14
        Appearance15.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance15.BackColor2 = System.Drawing.SystemColors.ControlLight
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance15.BorderColor = System.Drawing.SystemColors.Control
        ScrollBarLook1.Appearance = Appearance15
        Appearance16.BackColor = System.Drawing.SystemColors.Control
        Appearance16.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump
        Appearance16.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance16.ForeColor = System.Drawing.SystemColors.AppWorkspace
        ScrollBarLook1.ButtonAppearance = Appearance16
        Appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.HorizontalBump
        ScrollBarLook1.ThumbAppearance = Appearance17
        Appearance18.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance18.BackColor2 = System.Drawing.SystemColors.ControlLightLight
        Appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance18.BorderColor = System.Drawing.SystemColors.Control
        Appearance18.BorderColor3DBase = System.Drawing.SystemColors.Control
        ScrollBarLook1.TrackAppearance = Appearance18
        Me.Grd_Main.DisplayLayout.ScrollBarLook = ScrollBarLook1
        Me.Grd_Main.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.Grd_Main.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Appearance19.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance19.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Main.DisplayLayout.SplitterBarHorizontalAppearance = Appearance19
        Appearance20.BackColor = System.Drawing.SystemColors.Control
        Appearance20.BorderColor = System.Drawing.SystemColors.Control
        Appearance20.BorderColor3DBase = System.Drawing.SystemColors.ControlLight
        Me.Grd_Main.DisplayLayout.SplitterBarVerticalAppearance = Appearance20
        Me.Grd_Main.Location = New System.Drawing.Point(3, 21)
        Me.Grd_Main.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Grd_Main.Name = "Grd_Main"
        Me.Grd_Main.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Grd_Main.Size = New System.Drawing.Size(618, 378)
        Me.Grd_Main.TabIndex = 484
        Me.Grd_Main.UseFlatMode = Infragistics.Win.DefaultableBoolean.[True]
        '
        'DTS_Main
        '
        Me.DTS_Main.Band.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4})
        '
        'FRM_LovRooms_A
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(625, 453)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FRM_LovRooms_A"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "«·€—›"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Grd_Main, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTS_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DTS_Main As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents Btn_Cancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Btn_Ok As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Grd_Main As Infragistics.Win.UltraWinGrid.UltraGrid
End Class
