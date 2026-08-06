

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
    Friend WithEvents cmdTranslate As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.cmdTranslate = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdTranslate
        '
        Me.cmdTranslate.Location = New System.Drawing.Point(120, 136)
        Me.cmdTranslate.Name = "cmdTranslate"
        Me.cmdTranslate.Size = New System.Drawing.Size(128, 32)
        Me.cmdTranslate.TabIndex = 0
        Me.cmdTranslate.Text = "Translate 834_X095"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(336, 80)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(368, 198)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdTranslate)
        Me.Name = "Form1"
        Me.Text = "Translating an EDI 835 - Inbound"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdTranslate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTranslate.Click
        'This is just an example program to show how to translate an EDI 834 X095 EDI file into a database in VB.NET
        'with EDIParser.Net component

        Dim oConn As ADODB.Connection

        'This sample program uses ADODB (not ADO.NET) to access the database 
        Dim oRsInterchange As ADODB.Recordset
        Dim oRsFuncGroup As ADODB.Recordset
        Dim oRsX095Header As ADODB.Recordset
        Dim oRsX095MemberDetail As ADODB.Recordset
        Dim oRsX095HealthCoverage As ADODB.Recordset
        Dim sQlfr As String
        Dim sPath As String
        Me.Cursor = Cursors.WaitCursor
        '  sPath = AppDomain.CurrentDomain.BaseDirectory
        sPath = "../App_Data/"
        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead("834_X095.txt")
        Dim arMsg() As Byte
        Dim nFileLen As Integer


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


        'Connection string to access an MS Acess database
        Dim sConnection As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & sPath & "db1.mdb"
        'OPENS DATABASE CONNECTION
        oConn = New ADODB.Connection
        oConn.Open(sConnection)

        oRsInterchange = New ADODB.Recordset
        oRsInterchange.Open("Interchange", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        oRsFuncGroup = New ADODB.Recordset
        oRsFuncGroup.Open("FuncGroup", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        oRsX095Header = New ADODB.Recordset
        oRsX095Header.Open("X095Header", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        oRsX095MemberDetail = New ADODB.Recordset
        oRsX095MemberDetail.Open("X095MemberDetail", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        oRsX095HealthCoverage = New ADODB.Recordset
        oRsX095HealthCoverage.Open("X095HealthCoverage", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        
        For Each s In x12parser.Segments

            If s.Name = "ISA" Then
                oRsInterchange.AddNew()
                oRsInterchange("SenderQlfr").Value = CType(s.Fields.Item(5), EDIParser.Field).Value      'Interchange ID Qualifier
                oRsInterchange("SenderID").Value = CType(s.Fields.Item(6), EDIParser.Field).Value     'Interchange Sender ID
                oRsInterchange("ReceiverQlfr").Value = CType(s.Fields.Item(7), EDIParser.Field).Value      'Interchange ID Qualifier
                oRsInterchange("ReceiverID").Value = CType(s.Fields.Item(8), EDIParser.Field).Value     'Interchange Receiver ID
                oRsInterchange("InterDate").Value = CType(s.Fields.Item(9), EDIParser.Field).Value     'Interchange Date
                oRsInterchange("ControlNo").Value = CType(s.Fields.Item(13), EDIParser.Field).Value      'Interchange Control Number

            ElseIf s.Name = "GS" Then
                oRsFuncGroup.AddNew()
                oRsFuncGroup("InterchangeKey").Value = oRsInterchange("InterchangeKey").Value
                oRsFuncGroup("FuncID").Value = CType(s.Fields.Item(1), EDIParser.Field).Value     'Functional Identifier Code
                oRsFuncGroup("ControlNo").Value = CType(s.Fields.Item(6), EDIParser.Field).Value     'Group Control Number

            ElseIf s.Name = "IEA" Then
                'update interchange record
                oRsInterchange.Update()

            ElseIf s.Name = "GE" Then
                'update functional group record
                oRsFuncGroup.Update()



            ElseIf s.Name = "ST" Then
                oRsX095Header.AddNew()
                oRsX095Header("GroupKey").Value = oRsFuncGroup("GroupKey").Value
                oRsX095Header("MessageId").Value = CType(s.Fields.Item(1), EDIParser.Field).Value      'Transaction Set Identifier Code
                oRsX095Header("ControlNo").Value = CType(s.Fields.Item(2), EDIParser.Field).Value     'Transaction Set Control Number

            ElseIf s.Name = "BGN" Then
                oRsX095Header("PurposeCode").Value = CType(s.Fields.Item(1), EDIParser.Field).Value   ' Transaction Set Purpose Code (353) 
                oRsX095Header("TransactionId").Value = CType(s.Fields.Item(2), EDIParser.Field).Value ' Reference Identification (127) 
                oRsX095Header("TransactionDate").Value = CType(s.Fields.Item(3), EDIParser.Field).Value   ' Date (373)
                oRsX095Header("TransactionTime").Value = CType(s.Fields.Item(4), EDIParser.Field).Value   ' Time (337) 
                oRsX095Header("ActionCode").Value = CType(s.Fields.Item(8), EDIParser.Field).Value        ' Action Code (306) 


            ElseIf s.Name = "N1" Then

                sEntity = CType(s.Fields.Item(1), EDIParser.Field).Value  'identify loop instance by their entity identifier value
                sQlfr = CType(s.Fields.Item(3), EDIParser.Field).Value
                If sEntity = "P5" And sQlfr = "FI" Then 'Sponser infomation
                    oRsX095Header("SponserName").Value = CType(s.Fields.Item(2), EDIParser.Field).Value   ' Name (93)
                    oRsX095Header("SponserTaxId").Value = CType(s.Fields.Item(4), EDIParser.Field).Value  ' Identification Code (67)

                ElseIf sEntity = "IN" And sQlfr = "FI" Then 'Insurer information
                    oRsX095Header("InsurerName").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                    oRsX095Header("InsurerTaxId").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                End If
              
            ElseIf s.Name = "SE" Then
                'The SE segment is the end of the transaction set so is a good place to make sure all records are updated
                oRsX095Header.Update()
                oRsX095MemberDetail.Update()
             
            ElseIf s.Name = "INS" Then
                If Not oRsX095MemberDetail.BOF Then
                    'update any previous record before creating a new record
                    oRsX095MemberDetail.Update()
                End If
                oRsX095MemberDetail.AddNew()
                oRsX095MemberDetail("TSetKey").Value = oRsX095Header("TSetKey").Value
                oRsX095MemberDetail("Subscriber").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                oRsX095MemberDetail("Relationship").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                oRsX095MemberDetail("BenefitStatusCode").Value = CType(s.Fields.Item(5), EDIParser.Field).Value

            ElseIf s.Name = "REF" Then
                sQlfr = CType(s.Fields.Item(1), EDIParser.Field).Value    'check qualifier to identify the many instances of the same REF segment
                If sQlfr = "0F" Then    'subscriber name
                    oRsX095MemberDetail("SubscriberNo").Value = CType(s.Fields.Item(2), EDIParser.Field).Value

                ElseIf sQlfr = "1L" Then    'group or policy number
                    oRsX095MemberDetail("GroupPolicyNo").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                End If

            ElseIf s.Name = "DTP" Then
                If CType(s.Fields.Item(1), EDIParser.Field).Value = "356" Then    'Eligibility Begin
                    oRsX095MemberDetail("EligibilityStartDate").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                End If



            ElseIf s.Name = "NM1" Then
                sEntity = CType(s.Fields.Item(1), EDIParser.Field).Value  'Get entity qualifer to determine loop instances
                oRsX095MemberDetail("Firstname").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                oRsX095MemberDetail("Lastname").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                oRsX095MemberDetail("SSN").Value = CType(s.Fields.Item(9), EDIParser.Field).Value

                'LOOP 2100A
            ElseIf sEntity = "74" Or sEntity = "IL" Then    'Insured

                If s.Name = "PER" Then
                    oRsX095MemberDetail("HomePhone").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                    oRsX095MemberDetail("WorkPhone").Value = CType(s.Fields.Item(6), EDIParser.Field).Value

                ElseIf s.Name = "N3" Then
                    oRsX095MemberDetail("Address").Value = CType(s.Fields.Item(1), EDIParser.Field).Value

                ElseIf s.Name = "N4" Then
                    oRsX095MemberDetail("City").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                    oRsX095MemberDetail("State").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                    oRsX095MemberDetail("Zip").Value = CType(s.Fields.Item(3), EDIParser.Field).Value

                ElseIf s.Name = "DMG" Then
                    oRsX095MemberDetail("BirthDate").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                    oRsX095MemberDetail("GenderCode").Value = CType(s.Fields.Item(3), EDIParser.Field).Value

                    'Removing sEntity value so that it can not reenter
                    sEntity = String.Empty
                End If

            ElseIf sEntity = "70" Then  'LOOP 2100B - Incorrect insured
                If s.Name = "NM1" Then
                ElseIf s.Name = "DMG" Then
                    'Removing sEntity value so that it can not reenter
                    sEntity = String.Empty
                End If

            ElseIf sEntity = "31" Then  'LOOP 2100C - Post mailing address
                'If s.Name = "NM1" Then
                '    oRsX095MemberDetail("MailToFirstname").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                '    oRsX095MemberDetail("MailToLastname").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                '    oRsX095MemberDetail("MailToSSN").Value = CType(s.Fields.Item(9), EDIParser.Field).Value

                'ElseIf s.Name = "PER" Then
                '    oRsX095MemberDetail("MailToHomePhone").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                '    oRsX095MemberDetail("MailToWorkPhone").Value = CType(s.Fields.Item(6), EDIParser.Field).Value

                'ElseIf s.Name = "N3" Then
                '    oRsX095MemberDetail("MailToAddress").Value = CType(s.Fields.Item(1), EDIParser.Field).Value

                'ElseIf s.Name = "N4" Then
                '    oRsX095MemberDetail("MailToCity").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                '    oRsX095MemberDetail("MailToState").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                '    oRsX095MemberDetail("MailToZip").Value = CType(s.Fields.Item(3), EDIParser.Field).Value

                'Removing sEntity value so that it can not reenter
                'sEntity = String.Empty
                'End If

            ElseIf sEntity = "ES" Then  'LOOP 2100D - Employer Name
                'If s.Name = "NM1" Then
                '    oRsX095MemberDetail("EmployerFirstname").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                '    oRsX095MemberDetail("EmployerLastname").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                '    oRsX095MemberDetail("EmployerSSN").Value = CType(s.Fields.Item(9), EDIParser.Field).Value

                'ElseIf s.Name = "PER" Then
                '    oRsX095MemberDetail("EmployerHomePhone").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                '    oRsX095MemberDetail("EmployerWorkPhone").Value = CType(s.Fields.Item(6), EDIParser.Field).Value

                'ElseIf s.Name = "N3" Then
                '    oRsX095MemberDetail("EmployerAddress").Value = CType(s.Fields.Item(1), EDIParser.Field).Value

                'ElseIf s.Name = "N4" Then
                '    oRsX095MemberDetail("EmployerCity").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                '    oRsX095MemberDetail("EmployerState").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                '    oRsX095MemberDetail("EmployerZip").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                'Removing sEntity value so that it can not reenter
                'sEntity = String.Empty
                'End If

            ElseIf sEntity = "M8" Then  'LOOP 2100E - Educational Institution
                'If sSegmentID = "NM1" Then
                'ElseIf sSegmentID = "PER" Then
                'ElseIf sSegmentID = "N3" Then
                'ElseIf sSegmentID = "N4" Then
                'Removing sEntity value so that it can not reenter
                'sEntity = String.Empty
                'End If

            ElseIf sEntity = "S3" Then  'LOOP 2100F - Custodial Parent
                'If sSegmentID = "NM1" Then
                'ElseIf sSegmentID = "PER" Then
                'ElseIf sSegmentID = "N3" Then
                'ElseIf sSegmentID = "N4" Then
                'Removing sEntity value so that it can not reenter
                'sEntity = String.Empty
                'End If

            ElseIf sEntity = "E1" Or sEntity = "EI" Or sEntity = "GD" Or sEntity = "J6" Then   'LOOP 2100G - Guradian
                'If sSegmentID = "NM1" Then
                'ElseIf sSegmentID = "PER" Then
                'ElseIf sSegmentID = "N3" Then
                'ElseIf sSegmentID = "N4" Then
                'Removing sEntity value so that it can not reenter
                'sEntity = String.Empty
                'End If



            ElseIf s.Name = "HD" Then
                If Not oRsX095HealthCoverage.BOF Then
                    oRsX095HealthCoverage.Update()
                End If
                oRsX095HealthCoverage.AddNew()
                oRsX095HealthCoverage("MemberKey").Value = oRsX095MemberDetail("MemberKey").Value

                sEntity = CType(s.Fields.Item(3), EDIParser.Field).Value
                If sEntity = "HLT" Then
                    oRsX095HealthCoverage("InsuranceCode").Value = "Health"
                ElseIf sEntity = "DEN" Then
                    oRsX095HealthCoverage("InsuranceCode").Value = "Dental"
                ElseIf sEntity = "VIS" Then
                    oRsX095HealthCoverage("InsuranceCode").Value = "Vision"
                End If

            ElseIf s.Name = "DTP" Then
                oRsX095HealthCoverage("BenefitBeginDate").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
            End If

        Next
        oRsX095HealthCoverage.Update()
        oRsX095MemberDetail.Update()
        oRsX095Header.Update()
        oRsFuncGroup.Update()
        oRsInterchange.Update()


        Me.Cursor = Cursors.Default
        MessageBox.Show("Finished")

    End Sub
End Class
