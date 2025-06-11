<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_HK_Cleaning_Points_Report_A
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
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Emp_Code")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Emp_Desc")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Clean_Place")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Room_Type")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Supervisor")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("TDate")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Points")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Clean_Type")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim SummarySettings1 As Infragistics.Win.UltraWinGrid.SummarySettings = New Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, Nothing, "Points", 6, True, "Band 0", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "Points", 6, True)
        Dim ColScrollRegion1 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(915)
        Dim ColScrollRegion2 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1533)
        Dim ColScrollRegion3 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1533)
        Dim ColScrollRegion4 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1533)
        Dim ColScrollRegion5 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1533)
        Dim ColScrollRegion6 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion7 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion8 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion9 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion10 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion11 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion12 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion13 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion14 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1252)
        Dim ColScrollRegion15 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1239)
        Dim ColScrollRegion16 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1239)
        Dim ColScrollRegion17 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1227)
        Dim ColScrollRegion18 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(892)
        Dim ColScrollRegion19 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(892)
        Dim ColScrollRegion20 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(892)
        Dim ColScrollRegion21 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(892)
        Dim ColScrollRegion22 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(892)
        Dim ColScrollRegion23 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(898)
        Dim ColScrollRegion24 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion25 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion26 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion27 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion28 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion29 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion30 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion31 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion32 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion33 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion34 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion35 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1236)
        Dim ColScrollRegion36 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1248)
        Dim ColScrollRegion37 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1248)
        Dim ColScrollRegion38 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1248)
        Dim ColScrollRegion39 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1248)
        Dim ColScrollRegion40 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1248)
        Dim ColScrollRegion41 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1248)
        Dim ColScrollRegion42 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1248)
        Dim ColScrollRegion43 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1248)
        Dim ColScrollRegion44 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1061)
        Dim ColScrollRegion45 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1061)
        Dim ColScrollRegion46 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1061)
        Dim ColScrollRegion47 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion48 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion49 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion50 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion51 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion52 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion53 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion54 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion55 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion56 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion57 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion58 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion59 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion60 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion61 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion62 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion63 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion64 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion65 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion66 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1002)
        Dim ColScrollRegion67 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(997)
        Dim ColScrollRegion68 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(869)
        Dim ColScrollRegion69 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1351)
        Dim ColScrollRegion70 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1351)
        Dim ColScrollRegion71 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1361)
        Dim ColScrollRegion72 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1361)
        Dim ColScrollRegion73 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1361)
        Dim ColScrollRegion74 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1361)
        Dim ColScrollRegion75 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1361)
        Dim ColScrollRegion76 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion77 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion78 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion79 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion80 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion81 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion82 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion83 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion84 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion85 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion86 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion87 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion88 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion89 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion90 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion91 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1234)
        Dim ColScrollRegion92 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1189)
        Dim ColScrollRegion93 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1197)
        Dim ColScrollRegion94 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1197)
        Dim ColScrollRegion95 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1197)
        Dim ColScrollRegion96 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1197)
        Dim ColScrollRegion97 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1197)
        Dim ColScrollRegion98 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1197)
        Dim ColScrollRegion99 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1197)
        Dim ColScrollRegion100 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1188)
        Dim ColScrollRegion101 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1188)
        Dim ColScrollRegion102 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1188)
        Dim ColScrollRegion103 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(1188)
        Dim ColScrollRegion104 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion105 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion106 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion107 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion108 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion109 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion110 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion111 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion112 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion113 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion114 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(973)
        Dim ColScrollRegion115 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(992)
        Dim ColScrollRegion116 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(992)
        Dim ColScrollRegion117 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(992)
        Dim ColScrollRegion118 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(959)
        Dim ColScrollRegion119 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(959)
        Dim ColScrollRegion120 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(959)
        Dim ColScrollRegion121 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(959)
        Dim ColScrollRegion122 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(959)
        Dim ColScrollRegion123 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(926)
        Dim ColScrollRegion124 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(926)
        Dim ColScrollRegion125 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(820)
        Dim ColScrollRegion126 As Infragistics.Win.UltraWinGrid.ColScrollRegion = New Infragistics.Win.UltraWinGrid.ColScrollRegion(730)
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_HK_Cleaning_Points_Report_A))
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ScrollBarLook1 As Infragistics.Win.UltraWinScrollBar.ScrollBarLook = New Infragistics.Win.UltraWinScrollBar.ScrollBarLook()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Emp_Code")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Emp_Desc")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Clean_Place")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Room_Type")
        Dim UltraDataColumn5 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Supervisor")
        Dim UltraDataColumn6 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("TDate")
        Dim UltraDataColumn7 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Points")
        Dim UltraDataColumn8 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("Clean_Type")
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Grd_Summary = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.DTS_Summary = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.Txt_Month = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Txt_Code = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Btn_Search = New Infragistics.Win.Misc.UltraButton()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_Close = New Infragistics.Win.Misc.UltraButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.Grd_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DTS_Summary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Month, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
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
        Appearance1.FontData.Name = "Droid Arabic Kufi"
        Appearance1.FontData.SizeInPoints = 12.5!
        Appearance1.TextHAlignAsString = "Center"
        Me.Grd_Summary.DisplayLayout.Appearance = Appearance1
        Me.Grd_Summary.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        UltraGridBand1.ColHeaderLines = 2
        UltraGridColumn1.Header.Caption = "رقم الموظف"
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.MaxWidth = 120
        UltraGridColumn1.MinWidth = 120
        UltraGridColumn1.Width = 120
        UltraGridColumn2.Header.Caption = "اسم الموظف"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.MinWidth = 250
        UltraGridColumn2.Width = 250
        UltraGridColumn3.Header.Caption = "المكان / الغرفة"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn3.MinWidth = 200
        UltraGridColumn3.Width = 200
        UltraGridColumn4.Header.Caption = "نوع الغرفة"
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.MaxWidth = 150
        UltraGridColumn4.MinWidth = 150
        UltraGridColumn4.Width = 150
        UltraGridColumn5.Header.Caption = "المشرف"
        UltraGridColumn5.Header.VisiblePosition = 5
        UltraGridColumn5.MinWidth = 200
        UltraGridColumn5.Width = 200
        UltraGridColumn6.Format = "dd-MM-yyyy"
        UltraGridColumn6.Header.Caption = "تاريخ التنظيف"
        UltraGridColumn6.Header.VisiblePosition = 6
        UltraGridColumn6.MaxWidth = 200
        UltraGridColumn6.MinWidth = 200
        UltraGridColumn6.Width = 200
        UltraGridColumn7.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.[True]
        UltraGridColumn7.Header.Caption = "النقاط"
        UltraGridColumn7.Header.VisiblePosition = 7
        UltraGridColumn7.MaxWidth = 150
        UltraGridColumn7.MinWidth = 150
        UltraGridColumn7.Width = 150
        UltraGridColumn8.Header.Caption = "نوع التنظيف"
        UltraGridColumn8.Header.VisiblePosition = 4
        UltraGridColumn8.MaxWidth = 200
        UltraGridColumn8.MinWidth = 200
        UltraGridColumn8.Width = 200
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Appearance2.FontData.Name = "Tahoma"
        Appearance2.FontData.SizeInPoints = 8.0!
        UltraGridBand1.Header.Appearance = Appearance2
        UltraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Appearance3.BackColor = System.Drawing.Color.Lavender
        UltraGridBand1.Override.RowAlternateAppearance = Appearance3
        SummarySettings1.DisplayFormat = "المجموع = {0}"
        SummarySettings1.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.InGroupByRows
        UltraGridBand1.Summaries.AddRange(New Infragistics.Win.UltraWinGrid.SummarySettings() {SummarySettings1})
        UltraGridBand1.SummaryFooterCaption = ""
        Me.Grd_Summary.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion1)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion2)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion3)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion4)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion5)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion6)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion7)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion8)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion9)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion10)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion11)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion12)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion13)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion14)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion15)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion16)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion17)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion18)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion19)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion20)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion21)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion22)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion23)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion24)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion25)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion26)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion27)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion28)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion29)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion30)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion31)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion32)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion33)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion34)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion35)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion36)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion37)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion38)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion39)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion40)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion41)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion42)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion43)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion44)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion45)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion46)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion47)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion48)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion49)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion50)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion51)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion52)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion53)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion54)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion55)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion56)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion57)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion58)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion59)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion60)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion61)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion62)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion63)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion64)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion65)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion66)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion67)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion68)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion69)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion70)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion71)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion72)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion73)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion74)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion75)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion76)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion77)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion78)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion79)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion80)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion81)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion82)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion83)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion84)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion85)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion86)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion87)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion88)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion89)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion90)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion91)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion92)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion93)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion94)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion95)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion96)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion97)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion98)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion99)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion100)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion101)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion102)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion103)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion104)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion105)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion106)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion107)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion108)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion109)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion110)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion111)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion112)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion113)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion114)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion115)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion116)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion117)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion118)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion119)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion120)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion121)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion122)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion123)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion124)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion125)
        Me.Grd_Summary.DisplayLayout.ColScrollRegions.Add(ColScrollRegion126)
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Grd_Summary.DisplayLayout.Override.AddRowAppearance = Appearance4
        Appearance5.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.CellAppearance = Appearance5
        Appearance6.BorderColor = System.Drawing.SystemColors.Control
        Appearance6.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance6.Image = CType(resources.GetObject("Appearance6.Image"), Object)
        Me.Grd_Summary.DisplayLayout.Override.CellButtonAppearance = Appearance6
        Me.Grd_Summary.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.Grd_Summary.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains
        Me.Grd_Summary.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow
        Appearance7.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance7.BackColor2 = System.Drawing.SystemColors.Control
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance7.BorderColor = System.Drawing.SystemColors.Control
        Appearance7.BorderColor3DBase = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.HeaderAppearance = Appearance7
        Me.Grd_Summary.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.Standard
        Appearance8.BorderColor = System.Drawing.SystemColors.Control
        Me.Grd_Summary.DisplayLayout.Override.RowAppearance = Appearance8
        Appearance9.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance9.BackColor2 = System.Drawing.SystemColors.Control
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Control
        Appearance9.BorderColor3DBase = System.Drawing.SystemColors.ControlLightLight
        Appearance9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorAppearance = Appearance9
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement
        Me.Grd_Summary.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.RowIndex
        Me.Grd_Summary.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed
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
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLight
        Appearance16.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.SplitterBarHorizontalAppearance = Appearance16
        Appearance17.BackColor = System.Drawing.SystemColors.Control
        Appearance17.BorderColor = System.Drawing.SystemColors.Control
        Appearance17.BorderColor3DBase = System.Drawing.SystemColors.ControlLight
        Me.Grd_Summary.DisplayLayout.SplitterBarVerticalAppearance = Appearance17
        Me.Grd_Summary.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.Grd_Summary.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Grd_Summary.Location = New System.Drawing.Point(0, 133)
        Me.Grd_Summary.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Grd_Summary.Name = "Grd_Summary"
        Me.Grd_Summary.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Grd_Summary.Size = New System.Drawing.Size(917, 442)
        Me.Grd_Summary.TabIndex = 558
        Me.Grd_Summary.UseFlatMode = Infragistics.Win.DefaultableBoolean.[True]
        '
        'DTS_Summary
        '
        UltraDataColumn1.DataType = GetType(Short)
        UltraDataColumn6.DataType = GetType(Date)
        UltraDataColumn7.DataType = GetType(Short)
        Me.DTS_Summary.Band.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4, UltraDataColumn5, UltraDataColumn6, UltraDataColumn7, UltraDataColumn8})
        '
        'Txt_Month
        '
        Appearance18.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance18.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance18.TextHAlignAsString = "Right"
        Me.Txt_Month.Appearance = Appearance18
        Me.Txt_Month.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance19.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_Month.ButtonAppearance = Appearance19
        Me.Txt_Month.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton
        Me.Txt_Month.DateTime = New Date(1753, 1, 1, 0, 0, 0, 0)
        Appearance20.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Txt_Month.DropDownAppearance = Appearance20
        Me.Txt_Month.DropDownButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_Month.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Never
        Me.Txt_Month.DropDownCalendarAlignment = Infragistics.Win.DropDownListAlignment.Left
        Me.Txt_Month.Font = New System.Drawing.Font("Droid Arabic Kufi", 10.0!)
        Me.Txt_Month.FormatString = "MM-yyyy"
        Me.Txt_Month.Location = New System.Drawing.Point(93, 20)
        Me.Txt_Month.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Month.MaskInput = "mm-yy"
        Me.Txt_Month.Name = "Txt_Month"
        Me.Txt_Month.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Month.Size = New System.Drawing.Size(94, 33)
        Me.Txt_Month.SpinButtonAlignment = Infragistics.Win.ButtonAlignment.Left
        Me.Txt_Month.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always
        Me.Txt_Month.TabIndex = 554
        Me.Txt_Month.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextSection
        Me.Txt_Month.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_Month.Value = Nothing
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label16.Font = New System.Drawing.Font("Droid Arabic Kufi", 10.0!)
        Me.Label16.Location = New System.Drawing.Point(191, 23)
        Me.Label16.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(49, 27)
        Me.Label16.TabIndex = 555
        Me.Label16.Text = "الشهر"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Txt_Code
        '
        Me.Txt_Code.AlwaysInEditMode = True
        Me.Txt_Code.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlLight
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.ControlDark
        Appearance21.TextHAlignAsString = "Right"
        Me.Txt_Code.Appearance = Appearance21
        Me.Txt_Code.BackColor = System.Drawing.SystemColors.Control
        Me.Txt_Code.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.Txt_Code.Location = New System.Drawing.Point(501, 198)
        Me.Txt_Code.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Txt_Code.Name = "Txt_Code"
        Me.Txt_Code.ReadOnly = True
        Me.Txt_Code.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Txt_Code.Size = New System.Drawing.Size(46, 20)
        Me.Txt_Code.TabIndex = 270
        Me.Txt_Code.TabStop = False
        Me.Txt_Code.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        Me.Txt_Code.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel1.Controls.Add(Me.Grd_Summary)
        Me.Panel1.Controls.Add(Me.Txt_Code)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(917, 576)
        Me.Panel1.TabIndex = 274
        '
        'Btn_Search
        '
        Appearance23.BackColor = System.Drawing.Color.SlateGray
        Appearance23.BorderColor = System.Drawing.Color.Transparent
        Appearance23.ForeColor = System.Drawing.Color.White
        Appearance23.Image = CType(resources.GetObject("Appearance23.Image"), Object)
        Appearance23.TextHAlignAsString = "Right"
        Appearance23.TextVAlignAsString = "Middle"
        Me.Btn_Search.Appearance = Appearance23
        Me.Btn_Search.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat
        Me.Btn_Search.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Search.Font = New System.Drawing.Font("Droid Arabic Kufi", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_Search.Location = New System.Drawing.Point(5, 20)
        Me.Btn_Search.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Btn_Search.Name = "Btn_Search"
        Me.Btn_Search.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Btn_Search.Size = New System.Drawing.Size(77, 33)
        Me.Btn_Search.TabIndex = 566
        Me.Btn_Search.Text = "بحث"
        Me.Btn_Search.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 416.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Btn_Close, 2, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(917, 96)
        Me.TableLayoutPanel2.TabIndex = 623
        '
        'Btn_Close
        '
        Appearance22.BackColor = System.Drawing.Color.Transparent
        Appearance22.BorderColor = System.Drawing.Color.Transparent
        Appearance22.Cursor = System.Windows.Forms.Cursors.Hand
        Appearance22.Image = CType(resources.GetObject("Appearance22.Image"), Object)
        Appearance22.ImageHAlign = Infragistics.Win.HAlign.Center
        Me.Btn_Close.Appearance = Appearance22
        Me.Btn_Close.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat
        Me.Btn_Close.Dock = System.Windows.Forms.DockStyle.Right
        Me.Btn_Close.ImageSize = New System.Drawing.Size(70, 70)
        Me.Btn_Close.Location = New System.Drawing.Point(823, 17)
        Me.Btn_Close.Margin = New System.Windows.Forms.Padding(2, 2, 10, 2)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.ShowFocusRect = False
        Me.Btn_Close.ShowOutline = False
        Me.Btn_Close.Size = New System.Drawing.Size(84, 77)
        Me.Btn_Close.TabIndex = 602
        Me.Btn_Close.UseOsThemes = Infragistics.Win.DefaultableBoolean.[False]
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SlateGray
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Droid Arabic Kufi", 17.0!)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(252, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(412, 81)
        Me.Label1.TabIndex = 560
        Me.Label1.Text = "تقرير نقاط النظافة"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Btn_Search)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Txt_Month)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 18)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(244, 75)
        Me.Panel2.TabIndex = 603
        '
        'Frm_HK_Cleaning_Points_Report_A
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 576)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "Frm_HK_Cleaning_Points_Report_A"
        Me.Text = "تقرير نقاط النظافة"
        CType(Me.Grd_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DTS_Summary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Month, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt_Code, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Txt_Code As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents DTS_Summary As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents Grd_Summary As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Txt_Month As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label16 As Label
    Friend WithEvents Btn_Search As Infragistics.Win.Misc.UltraButton
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Btn_Close As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
End Class
