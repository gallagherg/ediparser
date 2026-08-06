

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
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents txtPayer As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReceiver As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtProvider As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNoDependents As System.Windows.Forms.TextBox
    Friend WithEvents txtSubscriberLast As System.Windows.Forms.TextBox
    Friend WithEvents txtSubscriberFirst As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDependentFirst As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtDependentLast As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnStart = New System.Windows.Forms.Button
        Me.txtPayer = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtReceiver = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtProvider = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtNoDependents = New System.Windows.Forms.TextBox
        Me.txtSubscriberLast = New System.Windows.Forms.TextBox
        Me.txtSubscriberFirst = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDependentFirst = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtDependentLast = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(200, 208)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(104, 32)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start"
        '
        'txtPayer
        '
        Me.txtPayer.Location = New System.Drawing.Point(8, 88)
        Me.txtPayer.Name = "txtPayer"
        Me.txtPayer.Size = New System.Drawing.Size(152, 20)
        Me.txtPayer.TabIndex = 1
        Me.txtPayer.Text = "ABC INSURANCE"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Payer:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(176, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Receiver:"
        '
        'txtReceiver
        '
        Me.txtReceiver.Location = New System.Drawing.Point(168, 88)
        Me.txtReceiver.Name = "txtReceiver"
        Me.txtReceiver.Size = New System.Drawing.Size(152, 20)
        Me.txtReceiver.TabIndex = 4
        Me.txtReceiver.Text = "XYZ SERVICE"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(336, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Provider:"
        '
        'txtProvider
        '
        Me.txtProvider.Location = New System.Drawing.Point(336, 88)
        Me.txtProvider.Name = "txtProvider"
        Me.txtProvider.Size = New System.Drawing.Size(152, 20)
        Me.txtProvider.TabIndex = 6
        Me.txtProvider.Text = "HOME HOSPITAL"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Subscriber Firstname:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(248, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 16)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "No. of Dependents:"
        '
        'txtNoDependents
        '
        Me.txtNoDependents.Location = New System.Drawing.Point(248, 152)
        Me.txtNoDependents.Multiline = True
        Me.txtNoDependents.Name = "txtNoDependents"
        Me.txtNoDependents.Size = New System.Drawing.Size(112, 32)
        Me.txtNoDependents.TabIndex = 16
        Me.txtNoDependents.Text = "0" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1"
        '
        'txtSubscriberLast
        '
        Me.txtSubscriberLast.Location = New System.Drawing.Point(128, 152)
        Me.txtSubscriberLast.Multiline = True
        Me.txtSubscriberLast.Name = "txtSubscriberLast"
        Me.txtSubscriberLast.Size = New System.Drawing.Size(112, 32)
        Me.txtSubscriberLast.TabIndex = 14
        Me.txtSubscriberLast.Text = "SMITH" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "JONES"
        '
        'txtSubscriberFirst
        '
        Me.txtSubscriberFirst.Location = New System.Drawing.Point(8, 152)
        Me.txtSubscriberFirst.Multiline = True
        Me.txtSubscriberFirst.Name = "txtSubscriberFirst"
        Me.txtSubscriberFirst.Size = New System.Drawing.Size(120, 32)
        Me.txtSubscriberFirst.TabIndex = 13
        Me.txtSubscriberFirst.Text = "JOHN" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "PETER"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(128, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 16)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Subscriber Lastname:"
        '
        'txtDependentFirst
        '
        Me.txtDependentFirst.Location = New System.Drawing.Point(368, 152)
        Me.txtDependentFirst.Multiline = True
        Me.txtDependentFirst.Name = "txtDependentFirst"
        Me.txtDependentFirst.Size = New System.Drawing.Size(120, 32)
        Me.txtDependentFirst.TabIndex = 18
        Me.txtDependentFirst.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "JANE"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(368, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 16)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Dependent Firstname:"
        '
        'txtDependentLast
        '
        Me.txtDependentLast.Location = New System.Drawing.Point(488, 152)
        Me.txtDependentLast.Multiline = True
        Me.txtDependentLast.Name = "txtDependentLast"
        Me.txtDependentLast.Size = New System.Drawing.Size(112, 32)
        Me.txtDependentLast.TabIndex = 20
        Me.txtDependentLast.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "JONES"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(488, 136)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(144, 16)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Dependent Lastname:"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(320, 208)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(104, 32)
        Me.btnClose.TabIndex = 22
        Me.btnClose.Text = "Close"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(16, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(600, 40)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "This example program shows you how to use the EDIParser.NET component in a VB. NE" & _
            "T programming language to generate a 276_X093 EDI file."
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(632, 262)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtDependentLast)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtDependentFirst)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtNoDependents)
        Me.Controls.Add(Me.txtSubscriberLast)
        Me.Controls.Add(Me.txtSubscriberFirst)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtProvider)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtReceiver)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPayer)
        Me.Controls.Add(Me.btnStart)
        Me.Name = "Form1"
        Me.Text = "Generating a 276_X093 EDI file in VB .NET with EDIParser"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim oX12Parser As New EDIParser.X12Parser()
        Dim sPath As String

        Me.Cursor = Cursors.WaitCursor
        sPath = AppDomain.CurrentDomain.BaseDirectory

        'CREATES THE ISA SEGMENT
        oX12Parser.SetValue("ISA.1", "00")               'Authorization Information Qualifier
        oX12Parser.SetValue("ISA.2", "          ")           'Authorization Information
        oX12Parser.SetValue("ISA.3", "00")               'Security Information Qualifier
        oX12Parser.SetValue("ISA.4", "          ")           'Security Information
        oX12Parser.SetValue("ISA.5", "12")               'Interchange ID Qualifier
        oX12Parser.SetValue("ISA.6", "Sender         ") 'Interchange Sender ID
        oX12Parser.SetValue("ISA.7", "12")               'Interchange ID Qualifier
        oX12Parser.SetValue("ISA.8", "ReceiverID     ")  'Interchange Receiver ID
        oX12Parser.SetValue("ISA.9", "010821")           'Interchange Date
        oX12Parser.SetValue("ISA.10", "1548")            'Interchange Time
        oX12Parser.SetValue("ISA.11", "U")               'Interchange Control Standards Identifier
        oX12Parser.SetValue("ISA.12", "00401")           'Interchange Control Version Number
        oX12Parser.SetValue("ISA.13", "000000020")       'Interchange Control Number
        oX12Parser.SetValue("ISA.14", "0")               'Acknowledgment Requested
        oX12Parser.SetValue("ISA.15", "T")               'Usage Indicator
        oX12Parser.SetValue("ISA.16", ":")               'Component Element Separator




        'create the GS segment
        oX12Parser.SetValue("GS.1", "HR")             'Functional Identifier Code
        oX12Parser.SetValue("GS.2", "SenderDept")     'Application Sender's Code
        oX12Parser.SetValue("GS.3", "ReceiverDept")   'Application Receiver's Code
        oX12Parser.SetValue("GS.4", "20010821")       'Date
        oX12Parser.SetValue("GS.5", "1548")           'Time
        oX12Parser.SetValue("GS.6", "1")         'Group Control Number
        oX12Parser.SetValue("GS.7", "X")              'Responsible Agency Code
        oX12Parser.SetValue("GS.8", "004010X093") '   'Version / Release / Industry Identifier Code




        'create the ST segment
        oX12Parser.SetValue("ST.1", "276")     'Transaction Set Identifier Code
        oX12Parser.SetValue("ST.2", "00001")   'Transaction Set Control Number


        'BHT - BEGINNING OF HIERARCHICAL TRANSACTION
        'create the BHT segment
        oX12Parser.SetValue("BHT.1", "0010")     'Hierarchical Structure Code
        oX12Parser.SetValue("BHT.2", "13")        'Transaction Set Purpose Code
        oX12Parser.SetValue("BHT.4", "19961115")    'Date




        Dim nInfoSources As Integer = 1
        Dim nInfoSourceCounter As Integer = 1
        Dim nInfoReceivers As Integer = 1
        Dim nInfoReceiverCounter As Integer = 1
        Dim nServiceProviders As Integer = 1
        Dim nServiceProviderCounter As Integer = 1
        Dim nSubscribers As Integer = 2
        Dim nSubscriberCounter As Integer = 1
        Dim nDependents As Integer = 1
        Dim nDependentCounter As Integer = 1

        Dim nHlCounter As Integer = 0
        Dim nNM1Counter As Integer = 1
        Dim nDMGCounter As Integer = 1
        Dim nTRNCounter As Integer = 1
        Dim nREFCounter As Integer = 1
        Dim nDTPCounter As Integer = 1
        Dim nAMTCounter As Integer = 1
        Dim nSVCCounter As Integer = 1
        Dim nHlInfoReceiverParent As Integer
        Dim nHlServiceProviderParent As Integer
        Dim nHlSubscriberParent As Integer
        Dim nHlDependentParent As Integer

        '*************************************************************************************************
        'DETAIL INFORMATION SOURCE LEVEL
        Do While nInfoSourceCounter <= nInfoSources

            nHlCounter = nHlCounter + 1
            nHlInfoReceiverParent = nHlCounter


            'HL - HIERARCHICAL LEVEL
            oX12Parser.SetValue("HL.1", nHlCounter, nHlCounter)  'Hierarchical ID Number
            oX12Parser.SetValue("HL.3", "20", nHlCounter)   'Hierarchical Level Code
            oX12Parser.SetValue("HL.4", "1", nHlCounter)    'Hierarchical Child Code



            'INDIVIDUAL OR ORGANIZATIONAL NAME
            oX12Parser.SetValue("NM1.1", "PR", nNM1Counter) 'Entity Identifier Code - PAYER
            oX12Parser.SetValue("NM1.2", "2", nNM1Counter)   'Entity Type Qualifier
            oX12Parser.SetValue("NM1.3", txtPayer.Text, nNM1Counter)     'Name Last or Organization Name
            oX12Parser.SetValue("NM1.8", "PI", nNM1Counter)   'Identification Code Qualifier
            oX12Parser.SetValue("NM1.9", "12345", nNM1Counter)    'Identification Code
            nNM1Counter += 1

            '*************************************************************************************************
            'DETAIL INFORMATION RECEIVER LEVEL
            Do While nInfoReceiverCounter <= nInfoReceivers

                nHlCounter = nHlCounter + 1
                nHlServiceProviderParent = nHlCounter

                'HL - HIERARCHICAL LEVEL
                oX12Parser.SetValue("HL.1", nHlCounter, nHlCounter)  'Hierarchical ID Number
                oX12Parser.SetValue("HL.2", nHlInfoReceiverParent, nHlCounter)  'Hierarchical Parent ID Number
                oX12Parser.SetValue("HL.3", "21", nHlCounter)   'Hierarchical Level Code
                oX12Parser.SetValue("HL.4", "1", nHlCounter)   'Hierarchical Child Code




                'NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                oX12Parser.SetValue("NM1.1", "41", nNM1Counter) 'Entity Identifier Code - SUBMITTER
                oX12Parser.SetValue("NM1.2", "2", nNM1Counter) 'Entity Type Qualifier
                oX12Parser.SetValue("NM1.3", txtReceiver.Text, nNM1Counter)  'Name Last or Organization Name
                oX12Parser.SetValue("NM1.8", "46", nNM1Counter)  'Identification Code Qualifier
                oX12Parser.SetValue("NM1.9", "X67E", nNM1Counter) 'Identification Code
                nNM1Counter += 1


                '*************************************************************************************************
                'DETAIL SERVICE PROVIDER LEVEL
                Do While nServiceProviderCounter <= nServiceProviders

                    nHlCounter = nHlCounter + 1
                    nHlSubscriberParent = nHlCounter

                    'HL - HIERARCHICAL LEVEL
                    oX12Parser.SetValue("HL.1", nHlCounter, nHlCounter)   'Hierarchical ID Number
                    oX12Parser.SetValue("HL.2", nHlServiceProviderParent, nHlCounter)  'Hierarchical Parent ID Number
                    oX12Parser.SetValue("HL.3", "19", nHlCounter) 'Hierarchical Level Code
                    oX12Parser.SetValue("HL.4", "1", nHlCounter) 'Hierarchical Child Code


                    'NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                    oX12Parser.SetValue("NM1.1", "1P", nNM1Counter)  'Entity Identifier Code - PROVIDER
                    oX12Parser.SetValue("NM1.2", "2", nNM1Counter)    'Entity Type Qualifier
                    oX12Parser.SetValue("NM1.3", "HOME HOSPITAL", nNM1Counter)   'Name Last or Organization Name
                    oX12Parser.SetValue("NM1.8", "SV", nNM1Counter)   'Identification Code Qualifier
                    oX12Parser.SetValue("NM1.9", "987666", nNM1Counter)   'Identification Code
                    nNM1Counter += 1
                    '*************************************************************************************************
                    'DETAIL SUBSCRIBER LEVEL
                    Do While nSubscriberCounter <= nSubscribers

                        nHlCounter = nHlCounter + 1
                        nHlDependentParent = nHlCounter

                        nDependents = Val(txtNoDependents.Lines(nSubscriberCounter - 1))

                        'HL - HIERARCHICAL LEVEL
                        oX12Parser.SetValue("HL.1", nHlCounter, nHlCounter)   'Hierarchical ID Number
                        oX12Parser.SetValue("HL.2", nHlSubscriberParent, nHlCounter)  'Hierarchical Parent ID Number
                        oX12Parser.SetValue("HL.3", "22", nHlCounter) 'Hierarchical Level Code


                        If nDependents = 0 Then
                            oX12Parser.SetValue("HL.4", "0", nHlCounter)      'Hierarchical Child Code
                        Else
                            oX12Parser.SetValue("HL.4", "1", nHlCounter)      'Hierarchical Child Code
                        End If

                        'DMG - DEMOGRAPHIC INFORMATION
                        oX12Parser.SetValue("DMG.1", "D8", nDMGCounter)     'Date Time Period Format Qualifier
                        oX12Parser.SetValue("DMG.2", "19201210", nDMGCounter)  'Date Time Period
                        oX12Parser.SetValue("DMG.3", "M", nDMGCounter)  'Gender Code
                        nDMGCounter += 1


                        'NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME



                        If nDependents = 0 Then
                            oX12Parser.SetValue("NM1.1", "QC", nNM1Counter)

                        Else
                            oX12Parser.SetValue("NM1.1", "IL", nNM1Counter)      'Entity Identifier Code
                        End If
                        oX12Parser.SetValue("NM1.2", "1", nNM1Counter)       'Entity Type Qualifier
                        oX12Parser.SetValue("NM1.3", txtSubscriberLast.Lines(nSubscriberCounter - 1), nNM1Counter)    'Name Last or Organization Name
                        oX12Parser.SetValue("NM1.4", txtSubscriberFirst.Lines(nSubscriberCounter - 1), nNM1Counter)    'Name First
                        oX12Parser.SetValue("NM1.8", "MI", nNM1Counter)   'Identification Code Qualifier
                        oX12Parser.SetValue("NM1.9", "123456789A", nNM1Counter)     'Identification Code
                        nNM1Counter += 1

                        'TRN - TRACE
                        oX12Parser.SetValue("TRN.1", "1", nTRNCounter)           'Trace Type Code
                        oX12Parser.SetValue("TRN.2", "1625032606", nTRNCounter)  'Reference Identification
                        nTRNCounter += 1

                        'REF - REFERENCE IDENTIFICATION
                        oX12Parser.SetValue("REF.1", "BLT", nREFCounter)  'Reference Identification Qualifier
                        oX12Parser.SetValue("REF.2", "111", nREFCounter)  'Reference Identification
                        nREFCounter += 1

                        'AMT - MONETARY AMOUNT

                        oX12Parser.SetValue("AMT.1", "T3", nAMTCounter)   'Amount Qualifier Code
                        oX12Parser.SetValue("AMT.2", "8513.88", nAMTCounter)  'Monetary Amount
                        nAMTCounter += 1

                        'DTP - DATE OR TIME OR PERIOD

                        oX12Parser.SetValue("DTP.1", "232", nDTPCounter) 'Date/Time Qualifier
                        oX12Parser.SetValue("DTP.2", "RD8", nDTPCounter)   'Date Time Period Format Qualifier
                        oX12Parser.SetValue("DTP.3", "19960831-19960906", nDTPCounter) 'Date Time Period
                        nDTPCounter += 1

                        If nDependents = 0 Then
                            'SVC Service Information

                            oX12Parser.SetValue("SVC.1", "AD:CD", nSVCCounter)     'Product/Service ID Qualifier :: 'Product/Service ID
                            oX12Parser.SetValue("SVC.2", "200", nSVCCounter)      'Monetary Amount
                            nSVCCounter += 1

                            'REF - REFERENCE IDENTIFICATION
                            oX12Parser.SetValue("REF.1", "FJ", nREFCounter)   'Reference Identification Qualifier
                            oX12Parser.SetValue("REF.2", "02", nREFCounter)   'Reference Identification
                            nREFCounter += 1
                            'DTP - DATE OR TIME OR PERIOD
                            oX12Parser.SetValue("DTP.1", "472", nDTPCounter) 'Date/Time Qualifier
                            oX12Parser.SetValue("DTP.2", "RD8", nDTPCounter)   'Date Time Period Format Qualifier
                            oX12Parser.SetValue("DTP.3", "19960931-19961030", nDTPCounter) 'Date Time Period
                            nDTPCounter += 1
                        End If
                        '*************************************************************************************************
                        'DETAIL DEPENDENT LEVEL
                        Do While nDependentCounter <= nDependents

                            nHlCounter = nHlCounter + 1

                            'HL - HIERARCHICAL LEVEL
                            oX12Parser.SetValue("HL.1", nHlCounter, nHlCounter)   'Hierarchical ID Number
                            oX12Parser.SetValue("HL.2", nHlDependentParent, nHlCounter)  'Hierarchical Parent ID Number
                            oX12Parser.SetValue("HL.3", "23", nHlCounter) 'Hierarchical Level Code





                            'DMG - DEMOGRAPHIC INFORMATION
                            oX12Parser.SetValue("DMG.1", "D8", nDMGCounter)     'Date Time Period Format Qualifier
                            oX12Parser.SetValue("DMG.2", "19201210", nDTPCounter)  'Date Time Period
                            oX12Parser.SetValue("DMG.3", "M", nDTPCounter)  'Gender Code
                            nDTPCounter += 1


                            'NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME

                            oX12Parser.SetValue("NM1.1", "QC", nNM1Counter)  'Entity Identifier Code - PROVIDER
                            oX12Parser.SetValue("NM1.2", "1", nNM1Counter)    'Entity Type Qualifier
                            oX12Parser.SetValue("NM1.3", txtDependentLast.Lines(nSubscriberCounter - 1), nNM1Counter)   'Name Last or Organization Name
                            oX12Parser.SetValue("NM1.4", txtDependentFirst.Lines(nSubscriberCounter - 1), nNM1Counter)    'Name First
                            oX12Parser.SetValue("NM1.8", "MI", nNM1Counter)   'Identification Code Qualifier
                            oX12Parser.SetValue("NM1.9", "9876453B", nNM1Counter)   'Identification Code
                            nNM1Counter += 1


                            'TRN - TRACE

                            oX12Parser.SetValue("TRN.1", "1", nTRNCounter)           'Trace Type Code
                            oX12Parser.SetValue("TRN.2", "1347897353", nTRNCounter)  'Reference Identification
                            nTRNCounter += 1

                            'REF - REFERENCE IDENTIFICATION
                            oX12Parser.SetValue("REF.1", "BLT", nREFCounter)  'Reference Identification Qualifier
                            oX12Parser.SetValue("REF.2", "111", nREFCounter)  'Reference Identification
                            nREFCounter += 1

                            'AMT - MONETARY AMOUNT
                            oX12Parser.SetValue("AMT.1", "T3", nAMTCounter)   'Amount Qualifier Code
                            oX12Parser.SetValue("AMT.2", "820", nAMTCounter)  'Monetary Amount
                            nAMTCounter += 1


                            'DTP - DATE OR TIME OR PERIOD
                            oX12Parser.SetValue("DTP.1", "232", nDTPCounter) 'Date/Time Qualifier
                            oX12Parser.SetValue("DTP.2", "RD8", nDTPCounter)   'Date Time Period Format Qualifier
                            oX12Parser.SetValue("DTP.3", "19960831-19960906", nDTPCounter) 'Date Time Period
                            nDTPCounter += 1

                            'SVC Service Information
                            oX12Parser.SetValue("SVC.1", "AD:CD", nSVCCounter)     'Product/Service ID Qualifier :: 'Product/Service ID
                            oX12Parser.SetValue("SVC.2", "820", nSVCCounter)      'Monetary Amount
                            nSVCCounter += 1


                            'REF - REFERENCE IDENTIFICATION
                            oX12Parser.SetValue("REF.1", "FJ", nREFCounter)  'Reference Identification Qualifier
                            oX12Parser.SetValue("REF.2", "78", nREFCounter)  'Reference Identification
                            nREFCounter += 1


                            'DTP - DATE OR TIME OR PERIOD
                            oX12Parser.SetValue("DTP.1", "472", nDTPCounter) 'Date/Time Qualifier
                            oX12Parser.SetValue("DTP.2", "RD8", nDTPCounter)   'Date Time Period Format Qualifier
                            oX12Parser.SetValue("DTP.3", "19970219-19971103", nDTPCounter) 'Date Time Period
                            nDTPCounter += 1
                            nDependentCounter = nDependentCounter + 1
                        Loop    'nDependents
                        nSubscriberCounter = nSubscriberCounter + 1
                    Loop    'nSubscribers
                    nServiceProviderCounter = nServiceProviderCounter + 1
                Loop    'nServiceProviders
                nInfoReceiverCounter = nInfoReceiverCounter + 1
            Loop    'nInfoReceivers
            nInfoSourceCounter = nInfoSourceCounter + 1
        Loop    'nInfoSources



        oX12Parser.SetValue("SE.1", "36")               'Total number of segments included in a transaction set including ST and SE segments
        oX12Parser.SetValue("SE.2", "00001")             'Identifying control number 

        oX12Parser.SetValue("GE.1", "1")                'Total Number of Transaction Sets
        oX12Parser.SetValue("GE.2", "1")

        oX12Parser.SetValue("IEA.1", "1")               'Number of Functional Groups GS/GE Pairs in Interchange
        oX12Parser.SetValue("IEA.2", "000000020~")               'Control Number


        Dim sFilePath As String = System.Windows.Forms.Application.StartupPath & "\\276_X093.txt"
        Dim ostreamwritter As System.IO.StreamWriter

        ostreamwritter = System.IO.File.CreateText(sFilePath)
        ostreamwritter.Write(oX12Parser.Message)
        ostreamwritter.Close()

        System.Windows.Forms.MessageBox.Show("OutPut:" & sFilePath)


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()

    End Sub
End Class
