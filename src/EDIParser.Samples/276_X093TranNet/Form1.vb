

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
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.btnStart = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Location = New System.Drawing.Point(24, 72)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(480, 173)
        Me.ListBox1.TabIndex = 0
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(168, 272)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 1
        Me.btnStart.Text = "Start"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(288, 272)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(24, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(480, 40)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "This example program shows you how to use the EDIParser.NET component in a VB. NE" & _
            "T programming language to translate a 276_X093 EDI file."
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(528, 318)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "Form1"
        Me.Text = "Translating a 276_X093 EDI file in VB .NET with EDIParser"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click

        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead("276_X093.txt")
        Dim arMsg() As Byte
        Dim nFileLen As Integer
        Dim sHlLevel As String = String.Empty
        Dim sValue As String
        Dim sEntity As String = String.Empty
        Dim sLXID As String = String.Empty
        Dim sQafr As String = String.Empty
        nFileLen = strEdi.Length
        ReDim arMsg(nFileLen)
        strEdi.Read(arMsg, 0, nFileLen)
        strEdi.Close()
        Dim sMsg As String
        sMsg = System.Text.Encoding.ASCII.GetString(arMsg)

        Dim x12parser As EDIParser.X12Parser = New EDIParser.X12Parser
        x12parser.ParseMsg(sMsg)
        Dim s As EDIParser.Segment
        'LOOP THAT WILL TRAVERSE THRU EDI FILE FROM TOP TO BOTTOM
        For Each s In x12parser.Segments

            If s.Name = "ISA" Then
                sValue = s.Fields(1).Value      'Authorization Information Qualifier
                sValue = s.Fields(2).Value      'Authorization Information
                sValue = s.Fields(3).Value      'Security Information Qualifier
                sValue = s.Fields(4).Value      'Security Information
                sValue = s.Fields(5).Value      'Interchange ID Qualifier
                ListBox1.Items.Add("Intercgange Sender ID = " & s.Fields(6).Value)   'Interchange Sender ID
                sValue = s.Fields(7).Value      'Interchange ID Qualifier
                ListBox1.Items.Add("Interchange Receiver ID = " & s.Fields(8).Value)    'Interchange Receiver ID
                sValue = s.Fields(9).Value      'Interchange Date
                sValue = s.Fields(10).Value     'Interchange Time
                sValue = s.Fields(11).Value     'Interchange Control Standards Identifier
                sValue = s.Fields(12).Value     'Interchange Control Version Number
                ListBox1.Items.Add("Interchange Control Number = " & s.Fields(13).Value)     'Interchange Control Number
                sValue = s.Fields(14).Value     'Acknowledgment Requested
                sValue = s.Fields(15).Value     'Usage Indicator
                sValue = s.Fields(16).Value     'Component Element Separator

            ElseIf s.Name = "GS" Then

                ListBox1.Items.Add("Functional Identifier Code = " & s.Fields(1).Value)       'Functional Identifier Code
                sValue = s.Fields(2).Value      'Application Sender's Code
                sValue = s.Fields(3).Value      'Application Receiver's Code
                sValue = s.Fields(4).Value      'Date
                sValue = s.Fields(5).Value      'Time
                ListBox1.Items.Add("Group Control Number = " & s.Fields(6).Value)       'Group Control Number
                sValue = s.Fields(7).Value      'Responsible Agency Code
                sValue = s.Fields(8).Value      'Version / Release / Industry Identifier Code

            ElseIf s.Name = "ST" Then

                sValue = s.Fields(1).Value     'Transaction Set Identifier Code
                ListBox1.Items.Add("Transaction Set Control Number = " & s.Fields(2).Value)     'Transaction Set Control Number

            ElseIf s.Name = "BHT" Then   'Beginning of Hierarchical Transaction
                sValue = s.Fields(1).Value    'Hierarchical Structure Code
                sValue = s.Fields(2).Value   'Transaction Set Purpose Code
                ListBox1.Items.Add("Reference Identification = " & s.Fields(3).Value)     'Reference Identification
                ListBox1.Items.Add("Date = " & s.Fields(4).Value)     'Date


            ElseIf s.Name = "HL" Then


                sHlLevel = s.Fields(3).Value



                'Information Source Level *********************************************************************
            ElseIf sHlLevel = "20" Then


                If s.Name = "HL" Then
                    sValue = s.Fields(1).Value      'Hierarchical ID Number
                    sValue = s.Fields(2).Value      'Hierarchical Parent ID Number
                    sValue = s.Fields(3).Value     'Hierarchical Level Code
                    sValue = s.Fields(4).Value     'Hierarchical Child Code


                ElseIf s.Name = "NM1" Then


                    sValue = s.Fields(1).Value     'Entity Identifier Code
                    sValue = s.Fields(2).Value      'Entity Type Qualifier
                    ListBox1.Items.Add("Information Source = " & s.Fields(3).Value)     'Name Last or Organization Name
                    sValue = s.Fields(4).Value      'Name First
                    sValue = s.Fields(5).Value     'Name Middle
                    sValue = s.Fields(6).Value      'Name Prefix
                    sValue = s.Fields(7).Value      'Name Suffix
                    sValue = s.Fields(8).Value      'Identification Code Qualifier
                    sValue = s.Fields(9).Value     'Identification Code

                End If


                'Information Receiver Level *******************************************************************
            ElseIf sHlLevel = "19" Then

                If s.Name = "HL" Then
                    sValue = s.Fields(1).Value      'Hierarchical ID Number
                    sValue = s.Fields(2).Value      'Hierarchical Parent ID Number
                    sValue = s.Fields(3).Value     'Hierarchical Level Code
                    sValue = s.Fields(4).Value     'Hierarchical Child Code
                ElseIf s.Name = "NM1" Then


                    sValue = s.Fields(1).Value     'Entity Identifier Code
                    sValue = s.Fields(2).Value      'Entity Type Qualifier
                    ListBox1.Items.Add("Information Receiver = " & s.Fields(3).Value)     'Name Last or Organization Name
                    sValue = s.Fields(4).Value      'Name First
                    sValue = s.Fields(5).Value     'Name Middle
                    sValue = s.Fields(6).Value      'Name Prefix
                    sValue = s.Fields(7).Value      'Name Suffix
                    sValue = s.Fields(8).Value      'Identification Code Qualifier
                    sValue = s.Fields(9).Value     'Identification Code

                End If
                'Service Provider Level *******************************************************************************
            ElseIf sHlLevel = "21" Then
                If s.Name = "HL" Then
                    sValue = s.Fields(1).Value      'Hierarchical ID Number
                    sValue = s.Fields(2).Value      'Hierarchical Parent ID Number
                    sValue = s.Fields(3).Value     'Hierarchical Level Code
                    sValue = s.Fields(4).Value     'Hierarchical Child Code
                ElseIf s.Name = "NM1" Then


                    sValue = s.Fields(1).Value     'Entity Identifier Code
                    sValue = s.Fields(2).Value      'Entity Type Qualifier
                    ListBox1.Items.Add("Service Provider = " & s.Fields(3).Value)     'Name Last or Organization Name
                    sValue = s.Fields(4).Value      'Name First
                    sValue = s.Fields(5).Value     'Name Middle
                    sValue = s.Fields(6).Value      'Name Prefix
                    sValue = s.Fields(7).Value      'Name Suffix
                    sValue = s.Fields(8).Value      'Identification Code Qualifier
                    sValue = s.Fields(9).Value     'Identification Code

                End If

                'Subscriber Level *********************************************************************************
            ElseIf sHlLevel = "22" Then

                If s.Name = "HL" Then
                    sValue = s.Fields(1).Value      'Hierarchical ID Number
                    sValue = s.Fields(2).Value      'Hierarchical Parent ID Number
                    sValue = s.Fields(3).Value     'Hierarchical Level Code
                    sValue = s.Fields(4).Value     'Hierarchical Child Code
                ElseIf s.Name = "NM1" Then


                    sValue = s.Fields(1).Value     'Entity Identifier Code
                    sValue = s.Fields(2).Value      'Entity Type Qualifier
                    ListBox1.Items.Add("Subscriber Lastname = " & s.Fields(3).Value)     'Name Last or Organization Name
                    ListBox1.Items.Add("Subscriber Firstname = " & s.Fields(4).Value)       'Name First
                    sValue = s.Fields(5).Value     'Name Middle
                    sValue = s.Fields(6).Value      'Name Prefix
                    sValue = s.Fields(7).Value      'Name Suffix
                    sValue = s.Fields(8).Value      'Identification Code Qualifier
                    sValue = s.Fields(9).Value     'Identification Code


                ElseIf s.Name = "TRN" Then

                    sValue = s.Fields(1).Value      'Trace Type Code
                    sValue = s.Fields(2).Value      'Reference Identification

                ElseIf s.Name = "REF" Then
                    sValue = s.Fields(1).Value     'Reference Identification Qualifier
                    sValue = s.Fields(2).Value      'Reference Identification

                ElseIf s.Name = "AMT" Then
                    sValue = s.Fields(1).Value     'Amount Qualifier Code
                    ListBox1.Items.Add("Monetary Amount = " & s.Fields(2).Value)     'Monetary Amount

                ElseIf s.Name = "DTP" Then
                    sValue = s.Fields(1).Value     'Date/Time Qualifier
                    sValue = s.Fields(2).Value     'Date Time Period Format Qualifier
                    sValue = s.Fields(3).Value     'Date Time Period


                    'ElseIf s.Name = "SVC" Then

                    '    sValue = s.Fields(1).Value     'TProduct/Service ID Qualifier

                    '    ListBox1.Items.Add("Monetary Amount = " & s.Fields(2).Value)    'Monetary Amount
                    '    'sValue = s.Fields(4).Value    'Product/Service ID
                    '    'sValue = s.Fields(7).Value    'Quantity

                    'ElseIf s.Name = "REF" Then
                    '    sValue = s.Fields(4).Value     'Reference Identification Qualifier
                    '    ListBox1.Items.Add("Reference Identification = " & s.Fields(2).Value)     'Reference Identification

                    'ElseIf s.Name = "DTP" Then
                    '    sValue = s.Fields(1).Value     'Date/Time Qualifier
                    '    sValue = s.Fields(2).Value       'Date Time Period Format Qualifier
                    '    ListBox1.Items.Add("Date Time Period = " & s.Fields(3).Value)
                    '    'Date Time Period
                End If  'sSegmentID


                'Dependent Level *********************************************************************************
            ElseIf sHlLevel = "23" Then

                If s.Name = "HL" Then
                    sValue = s.Fields(1).Value     'Hierarchical ID Number
                    sValue = s.Fields(2).Value     'Hierarchical Parent ID Number
                    sValue = s.Fields(3).Value     'Hierarchical Level Code
                    sValue = s.Fields(4).Value     'Hierarchical Child Code


                ElseIf s.Name = "NM1" Then
                    sValue = s.Fields(1).Value     'Entity Identifier Code
                    sValue = s.Fields(2).Value    'Entity Type Qualifier
                    ListBox1.Items.Add("Dependent Lastname = " & s.Fields(3).Value)     'Name Last or Organization Name
                    ListBox1.Items.Add("Dependent Firstname = " & s.Fields(4).Value)     'Name First
                    sValue = s.Fields(5).Value    'Name Middle
                    sValue = s.Fields(6).Value     'Name Prefix
                    sValue = s.Fields(7).Value     'Name Suffix
                    sValue = s.Fields(8).Value   'Identification Code Qualifier
                    sValue = s.Fields(9).Value    'Identification Code


                ElseIf s.Name = "TRN" Then

                    sValue = s.Fields(1).Value      'Trace Type Code
                    sValue = s.Fields(2).Value     'Reference Identification

                ElseIf s.Name = "REF" Then
                    sValue = s.Fields(1).Value     'Reference Identification Qualifier
                    sValue = s.Fields(2).Value     'Reference Identification

                ElseIf s.Name = "AMT" Then
                    sValue = s.Fields(1).Value     'Amount Qualifier Code
                    ListBox1.Items.Add("Monetary Amount = " & s.Fields(2).Value)     'Monetary Amount

                ElseIf s.Name = "DTP" Then
                    sValue = s.Fields(1).Value     'Date/Time Qualifier
                    sValue = s.Fields(2).Value    'Date Time Period Format Qualifier
                    ListBox1.Items.Add("Date Time Period = " & s.Fields(3).Value)   'Date Time Period

                    sHlLevel = String.Empty
                    'ElseIf s.Name = "SVC" Then

                    '    sValue = s.Fields(1).Value    'TProduct/Service ID Qualifier

                    '    ListBox1.Items.Add("Monetary Amount = " & s.Fields(2).Value)    'Monetary Amount
                    '    ' sValue = s.Fields(4).Value    'Product/Service ID
                    '    ' sValue = s.Fields(7).Value   'Quantity

                    'ElseIf s.Name = "REF" Then
                    '    sValue = s.Fields(1).Value     'Reference Identification Qualifier
                    '    ListBox1.Items.Add("Reference Identification = " & s.Fields(2).Value)     'Reference Identification

                    'ElseIf s.Name = "DTP" Then
                    '    sValue = s.Fields(1).Value      'Date/Time Qualifier
                    '    sValue = s.Fields(2).Value      'Date Time Period Format Qualifier
                    '    ListBox1.Items.Add("Date Time Period = " & s.Fields(3).Value)     'Date Time Period
                End If 'sSegmentID
            End If

        Next

        Me.Cursor = Cursors.Default
        'DESTROY OBJECTS
      

        MessageBox.Show("Done")


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()

    End Sub
End Class
