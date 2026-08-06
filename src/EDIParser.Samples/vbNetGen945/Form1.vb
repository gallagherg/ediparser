Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnGenerate = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(64, 104)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(144, 48)
        Me.btnGenerate.TabIndex = 0
        Me.btnGenerate.Text = "Generate"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(248, 56)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "This is jus an example program to demonstrate how to generate an EDI X12 945  in " & _
            "VB.NET with the EDI Parser component"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(284, 188)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnGenerate)
        Me.Name = "Form1"
        Me.Text = "Generate an EDI X12 945"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim oX12Parser As New EDIParser.X12Parser()
        Dim nLineItems As Integer = 0
        Dim sEdiFile As String = "945_5010.X12"
        Dim sInterchangeControlNumber As String = "000000031"
        Dim sGroupControlNumber As String = "31"
        Dim sTransactionSetControlNumber As String = "000310001"

        'SET TERMINATORS
        oX12Parser.SegmentSeparator = "~"
        oX12Parser.FieldSeparator = "*"
        oX12Parser.ComponentSeparator = ">"


        'CREATES THE ISA SEGMENT
        oX12Parser.SetValue("ISA.1", "00")     'Authorization Information Qualifier
        oX12Parser.SetValue("ISA.2", " ")     'Authorization Information
        oX12Parser.SetValue("ISA.3", "00")    'Security Information Qualifier
        oX12Parser.SetValue("ISA.4", " ")  'Security Information
        oX12Parser.SetValue("ISA.5", "08")  'Interchange ID Qualifier
        oX12Parser.SetValue("ISA.6", "Sender Id")    'Interchange Sender ID
        oX12Parser.SetValue("ISA.7", "08")    'Interchange ID Qualifier
        oX12Parser.SetValue("ISA.8", "Receiver Id")    'Interchange Receiver ID
        oX12Parser.SetValue("ISA.9", "021104")     'Interchange Date
        oX12Parser.SetValue("ISA.10", "1405")    'Interchange Time
        oX12Parser.SetValue("ISA.11", ":")  'Repetition Separator
        oX12Parser.SetValue("ISA.12", "00501")    'Interchange Control Version Number
        oX12Parser.SetValue("ISA.13", sInterchangeControlNumber)    'Interchange Control Number
        oX12Parser.SetValue("ISA.14", "0")  'Acknowledgment Requested
        oX12Parser.SetValue("ISA.15", "T")  'Usage Indicator
        oX12Parser.SetValue("ISA.16", ">")  'Component Element Separator

        'CREATES THE GS SEGMENT
        oX12Parser.SetValue("GS.1", "SW")  'Functional Identifier Code
        oX12Parser.SetValue("GS.2", "Sender Id")     'Application Sender's Code
        oX12Parser.SetValue("GS.3", "Receiver Id")     'Application Receiver's Code
        oX12Parser.SetValue("GS.4", "20021104")  'Date
        oX12Parser.SetValue("GS.5", "1405")  'Time
        oX12Parser.SetValue("GS.6", sGroupControlNumber) 'Group Control Number
        oX12Parser.SetValue("GS.7", "X") 'Responsible Agency Code
        oX12Parser.SetValue("GS.8", "005010")    'Version / Release / Industry Identifier Code

        'CREATES THE ST SEGMENT
        oX12Parser.SetValue("ST.1", "945") 'Transaction Set Identifier Code
        oX12Parser.SetValue("ST.2", sTransactionSetControlNumber)     'Transaction Set Control Number

        'W06 - WAREHOUSE SHIPMENT IDENTIFICATION
        oX12Parser.SetValue("W06.1", "N")  'Reporting Code
        oX12Parser.SetValue("W06.3", "20020916")     'Date
        oX12Parser.SetValue("W06.4", "2114")    'Shipment Identification Number

        'N1 - NAME
        oX12Parser.SetValue("N1.1", "BT", 1)    'Entity Identifier Code
        oX12Parser.SetValue("N1.2", "Bill-To Company", 1)     'Name
        oX12Parser.SetValue("N1.3", "9", 1)  'Identification Code Qualifier
        oX12Parser.SetValue("N1.4", "BT34589273689", 1)     'Identification Code

        'N1 - NAME
        oX12Parser.SetValue("N1.1", "ST", 2) 'Entity Identifier Code
        oX12Parser.SetValue("N1.2", "Ship-To Company", 2)   'Name
        oX12Parser.SetValue("N1.3", "9", 2)   'Identification Code Qualifier
        oX12Parser.SetValue("N1.4", "ST69802093458", 2)     'Identification Code

        'G62 - DATE/TIME
        oX12Parser.SetValue("G62.1", "11") 'Date Qualifier
        oX12Parser.SetValue("G62.2", "20020916")      'Date

        'W27 - CARRIER DETAIL
        oX12Parser.SetValue("W27.1", "M") 'Transportation Method/Type Code
        oX12Parser.SetValue("W27.2", "NA") 'Standard Carrier Alpha Code

        For nLineItems = 1 To 3
            'LX - ASSIGNED NUMBER
            oX12Parser.SetValue("LX.1", nLineItems.ToString(), nLineItems)        'Assigned Number

            'N9 - REFERENCE IDENTIFICATION
            oX12Parser.SetValue("N9.1", "2I", nLineItems)      'Reference Identification Qualifier
            oX12Parser.SetValue("N9.2", "K018293-00010", nLineItems)     'Reference Identification
            oX12Parser.SetValue("N9.3", "TEST ITEM " & nLineItems.ToString, nLineItems)       'Free-form Description

            'W12 - WAREHOUSE ITEM DETAIL
            oX12Parser.SetValue("W12.1", "CL", nLineItems) 'Shipment/Order Status Code
            oX12Parser.SetValue("W12.2", "120", nLineItems)      'Quantity
            oX12Parser.SetValue("W12.3", "120", nLineItems)      'Number of Units Shipped
            oX12Parser.SetValue("W12.7", "IN", nLineItems)    'Product/Service ID Qualifier
            oX12Parser.SetValue("W12.8", "339408", nLineItems)      'Product/Service ID
            oX12Parser.SetValue("W12.9", "19284", nLineItems)    'Warehouse Lot Number
            oX12Parser.SetValue("W12.10", "6600", nLineItems)   'Weight
            oX12Parser.SetValue("W12.11", "A3", nLineItems) 'Weight Qualifier
            oX12Parser.SetValue("W12.12", "L", nLineItems) 'Weight Unit Code
        Next

        'W03 - TOTAL SHIPMENT INFORMATION
        oX12Parser.SetValue("W03.1", "920")        'Number of Units Shipped
        oX12Parser.SetValue("W03.2", "46600")      'Weight
        oX12Parser.SetValue("W03.3", "01")         'Unit or Basis for Measurement Code

        oX12Parser.SetValue("SE", "") 'Add the SE segment to increment the transaction segment counter
        oX12Parser.SetValue("SE.1", oX12Parser.TransactionSegmentCount.ToString()) 'Total number of segments included in a transaction set including ST and SE segments
        oX12Parser.SetValue("SE.2", sTransactionSetControlNumber)   'Identifying control number 

        oX12Parser.SetValue("GE.1", oX12Parser.TransactionFunctionalGroupCount.ToString()) 'Number of Functional Groups ST/SE Pairs in Interchange  
        oX12Parser.SetValue("GE.2", sGroupControlNumber)          'Group control number

        oX12Parser.SetValue("IEA.1", oX12Parser.TransactionInterchangeCount.ToString())  'Number of Functional Groups GS/GE Pairs in Interchange
        oX12Parser.SetValue("IEA.2", sInterchangeControlNumber) 'Control Number


        Dim sFilePath As String = System.Windows.Forms.Application.StartupPath() & "\\945_5010.X12"
        Dim ostreamwritter As System.IO.StreamWriter

        ostreamwritter = System.IO.File.CreateText(sFilePath)
        ostreamwritter.Write(oX12Parser.Message)
        ostreamwritter.Close()
        Dim sMsg As String = oX12Parser.Message
        System.Windows.Forms.MessageBox.Show("OutPut:" & sFilePath)
        System.Windows.Forms.MessageBox.Show(sMsg)


    End Sub
End Class
