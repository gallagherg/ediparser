
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
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btnTranslate As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.btnTranslate = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Location = New System.Drawing.Point(24, 56)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(328, 264)
        Me.ListBox1.TabIndex = 0
        '
        'btnTranslate
        '
        Me.btnTranslate.Location = New System.Drawing.Point(368, 88)
        Me.btnTranslate.Name = "btnTranslate"
        Me.btnTranslate.Size = New System.Drawing.Size(136, 48)
        Me.btnTranslate.TabIndex = 1
        Me.btnTranslate.Text = "Translate"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(496, 40)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "This is just an example program to demonstrate how to translate an EDI X12 945 fi" & _
            "le in VB .NET using the EDIParser.NET component"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(520, 340)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnTranslate)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "Form1"
        Me.Text = "Translates an EDI X12 945"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnTranslate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTranslate.Click
        Dim sLoopSection As String
        Dim sValue As String
        Dim sN1Entity As String
        Dim arMsg() As Byte
        Dim nFileLen As Integer

        'LOADS THE EDI FILE
        Dim sPath As String = AppDomain.CurrentDomain.BaseDirectory
        Dim sEdiFile As String = "945_5010.X12"
        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead(sPath & sEdiFile)
        nFileLen = strEdi.Length
        ReDim arMsg(nFileLen)
        strEdi.Read(arMsg, 0, nFileLen)
        strEdi.Close()
        Dim sMsg As String
        sMsg = System.Text.Encoding.ASCII.GetString(arMsg)

        'CREATE EDIPARSER object and parse message
        Dim x12parser As EDIParser.X12Parser = New EDIParser.X12Parser
        x12parser.ParseMsg(sMsg)
        Dim s As EDIParser.Segment
        'LOOP THAT WILL TRAVERSE THRU EDI FILE FROM TOP TO BOTTOM
        For Each s In x12parser.Segments
            If s.Name = "ISA" Then
                sValue = s.Fields(1).Value     'Authorization Information Qualifier
                sValue = s.Fields(2).Value     'Authorization Information
                sValue = s.Fields(3).Value     'Security Information Qualifier
                sValue = s.Fields(4).Value     'Security Information
                sValue = s.Fields(5).Value     'Interchange ID Qualifier
                sValue = s.Fields(6).Value     'Interchange Sender ID
                sValue = s.Fields(7).Value     'Interchange ID Qualifier
                sValue = s.Fields(8).Value     'Interchange Receiver ID
                sValue = s.Fields(9).Value     'Interchange Date
                sValue = s.Fields(10).Value     'Interchange Time
                sValue = s.Fields(11).Value     'Repetition Separator
                sValue = s.Fields(12).Value     'Interchange Control Version Number
                ListBox1.Items.Add("Interchage Control No.: " & s.Fields(13).Value)     'Interchange Control Number
                sValue = s.Fields(14).Value     'Acknowledgment Requested
                sValue = s.Fields(15).Value     'Usage Indicator
                sValue = s.Fields(16).Value     'Component Element Separator

            ElseIf s.Name = "GS" Then
                sValue = s.Fields(1).Value     'Functional Identifier Code
                sValue = s.Fields(2).Value     'Application Sender's Code
                sValue = s.Fields(3).Value     'Application Receiver's Code
                sValue = s.Fields(4).Value     'Date
                sValue = s.Fields(5).Value     'Time
                ListBox1.Items.Add("Group Control No.: " & s.Fields(6).Value)     'Group Control Number
                sValue = s.Fields(7).Value     'Responsible Agency Code
                sValue = s.Fields(8).Value     'Version / Release / Industry Identifier Code

            ElseIf s.Name = "ST" Then
                sValue = s.Fields(1).Value     'Transaction Set Identifier Code
                ListBox1.Items.Add("Transactionset Control No.: " & s.Fields(2).Value)     'Transaction Set Control Number

            ElseIf s.Name = "W06" Then
                sValue = s.Fields(1).Value     'Reporting Code
                sValue = s.Fields(2).Value     'Depositor Order Number
                sValue = s.Fields(3).Value     'Date
                ListBox1.Items.Add("Bill of Lading No.: " & s.Fields(4).Value)     'Shipment Identification Number

            ElseIf s.Name = "G62" Then
                sValue = s.Fields(1).Value
                ListBox1.Items.Add("Shipped date: " & s.Fields(2).Value)

            ElseIf s.Name = "W27" Then
                'If loop has more that one instance, then you should check for the qualifier that differentiates the loop instances 
            ElseIf s.Name = "N1" Then
                sN1Entity = s.Fields(1).Value
            End If

            If sN1Entity = "BT" Then
                If s.Name = "N1" Then
                    sValue = s.Fields(1).Value     'Entity Identifier Code
                    ListBox1.Items.Add("Bill-To Name: " & s.Fields(2).Value)     'Name
                    sValue = s.Fields(3).Value     'Identification Code Qualifier
                    sValue = s.Fields(4).Value     'Identification Code

                ElseIf s.Name = "N3" Then
                    sValue = s.Fields(1).Value

                ElseIf s.Name = "N4" Then
                    sValue = s.Fields(1).Value
                    sValue = s.Fields(2).Value
                    sValue = s.Fields(3).Value
                End If   'sSegmentID

            ElseIf sN1Entity = "ST" Then
                If s.Name = "N1" Then
                    sValue = s.Fields(1).Value     'Entity Identifier Code
                    ListBox1.Items.Add("Ship-To Name: " & s.Fields(2).Value)     'Name
                    sValue = s.Fields(3).Value     'Identification Code Qualifier
                    sValue = s.Fields(4).Value     'Identification Code

                ElseIf s.Name = "N3" Then
                    sValue = s.Fields(1).Value

                ElseIf s.Name = "N4" Then
                    sValue = s.Fields(1).Value
                    sValue = s.Fields(2).Value
                    sValue = s.Fields(3).Value
                End If   'sSegmentID
            End If

            If s.Name = "LX" Then
                ListBox1.Items.Add("Line No.: " & s.Fields(1).Value)     'Assigned Number
                sLoopSection = "LX"
            ElseIf s.Name = "N9" Then
                sValue = s.Fields(1).Value     'Reference Identification Qualifier
                ListBox1.Items.Add("Tracking No.: " & s.Fields(2).Value)     'Reference Identification
                ListBox1.Items.Add("Description: " & s.Fields(3).Value)     'Free-form Description
            End If   'Segment ID

            If sLoopSection = "LX" Then
                If s.Name = "W12" Then

                    'CType(s.Fields.Item(5), EDIParser.Field).Value 
                    sValue = s.Fields(1).Value     'Shipment/Order Status Code
                    sValue = s.Fields(2).Value     'Quantity
                    ListBox1.Items.Add("Number of Units Shipped: " & s.Fields(3).Value)     'Number of Units Shipped
                    sValue = s.Fields(4).Value     'Quantity Difference
                    sValue = s.Fields(5).Value     'Unit or Basis for Measurement Code
                    sValue = s.Fields(6).Value     'U.P.C. Case Code
                    sValue = s.Fields(7).Value     'Product/Service ID Qualifier
                    ListBox1.Items.Add("Buyer's Item No: " & s.Fields(8).Value)     'Product/Service ID
                    sValue = s.Fields(9).Value     'Warehouse Lot Number
                    sValue = s.Fields(10).Value     'Weight
                    sValue = s.Fields(11).Value     'Weight Qualifier
                    sValue = s.Fields(12).Value     'Weight Unit Code
                    sLoopSection = ""
                End If   'sSegmentID
            End If   'sLoopSection

            If s.Name = "W03" Then
                ListBox1.Items.Add("Total Number of Units Shipped: " & s.Fields(1).Value)     'Number of Units Shipped
                sValue = s.Fields(2).Value     'Weight
                sValue = s.Fields(3).Value     'Unit or Basis for Measurement Code
            End If  'sSegmentID


        Next

        MessageBox.Show("Done")

    End Sub

End Class
