

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
        Me.cmdTranslate.Location = New System.Drawing.Point(96, 136)
        Me.cmdTranslate.Name = "cmdTranslate"
        Me.cmdTranslate.Size = New System.Drawing.Size(128, 32)
        Me.cmdTranslate.TabIndex = 0
        Me.cmdTranslate.Text = "Translate"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(288, 80)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(328, 198)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdTranslate)
        Me.Name = "Form1"
        Me.Text = "Translating an EDI 837 - Inbound"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdTranslate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTranslate.Click
    
        Dim oConn As ADODB.Connection

        'This sample program uses ADODB (not ADO.NET) to access the database 
        Dim oRsInterchange As ADODB.Recordset
        Dim oRsFuncGroup As ADODB.Recordset
        Dim oRsX098Header As ADODB.Recordset
        Dim oRsX098ProviderInfo As ADODB.Recordset
        Dim oRsX098SubscriberInfo As ADODB.Recordset
        Dim oRsX098DependentInfo As ADODB.Recordset
        Dim oRsX098Claims As ADODB.Recordset
        Dim oRsX098OtherSubscriberInfo As ADODB.Recordset
        Dim oRsX098ServiceInfo As ADODB.Recordset

        Dim i As Integer

        Dim sPath As String
        Dim sEntity As String
        Dim sQlfr As String
        Dim nLineCount As Integer
       


        Me.Cursor = Cursors.WaitCursor

        'sPath = AppDomain.CurrentDomain.BaseDirectory
        sPath = "../App_Data/"
        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead("834_X098.txt")
        Dim arMsg() As Byte
        Dim nFileLen As Integer

        Dim s As EDIParser.Segment

      
        nFileLen = strEdi.Length
        ReDim arMsg(nFileLen)
        strEdi.Read(arMsg, 0, nFileLen)
        strEdi.Close()
        Dim sMsg As String
        sMsg = System.Text.Encoding.ASCII.GetString(arMsg)

        Dim x12parser As EDIParser.X12Parser = New EDIParser.X12Parser
        x12parser.ParseMsg(sMsg)


        'Connection string to access an MS Acess database
        Dim sConnection As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & sPath & "db1.mdb"
        'OPENS DATABASE CONNECTION
        oConn = New ADODB.Connection
        oConn.Open(sConnection)

        'OPENS INTERCHANGE TABLE
        oRsInterchange = New ADODB.Recordset
        oRsInterchange.Open("Interchange", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        'OPENS FUNCTIONAL GROUP TABLE
        oRsFuncGroup = New ADODB.Recordset
        oRsFuncGroup.Open("FuncGroup", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        'OPENS X098Header TABLE
        oRsX098Header = New ADODB.Recordset
        oRsX098Header.Open("X098Header", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        'OPENS X098ProviderInfo TABLE
        oRsX098ProviderInfo = New ADODB.Recordset
        oRsX098ProviderInfo.Open("X098ProviderInfo", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        'OPENS X098SubscriberInfo TABLE
        oRsX098SubscriberInfo = New ADODB.Recordset
        oRsX098SubscriberInfo.Open("X098SubscriberInfo", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        'OPENS X098DependentInfo TABLE
        oRsX098DependentInfo = New ADODB.Recordset
        oRsX098DependentInfo.Open("X098DependentInfo", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        'OPENS X098Claims TABLE
        oRsX098Claims = New ADODB.Recordset
        oRsX098Claims.Open("X098Claims", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        'OPENS X098ServiceInfo TABLE
        oRsX098ServiceInfo = New ADODB.Recordset
        oRsX098ServiceInfo.Open("X098ServiceInfo", oConn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)


        'Iterate through all segments in the EDI file
        'Get the firs data segment in the EDI file
   

        For Each s In x12parser.Segments
            If s.Name = "NM1" Then

                sQlfr = CType(s.Fields.Item(1), EDIParser.Field).Value

            ElseIf s.Name = "HL" Then

                sEntity = CType(s.Fields.Item(3), EDIParser.Field).Value

            End If

            If s.Name = "ISA" Then
                'add record in the interchange table to save the interchange information
                oRsInterchange.AddNew()
                oRsInterchange("SenderQlfr").Value = CType(s.Fields.Item(5), EDIParser.Field).Value       'Interchange ID Qualifier
                oRsInterchange("SenderID").Value = CType(s.Fields.Item(6), EDIParser.Field).Value      'Interchange Sender ID
                oRsInterchange("ReceiverQlfr").Value = CType(s.Fields.Item(7), EDIParser.Field).Value      'Interchange ID Qualifier
                oRsInterchange("ReceiverID").Value = CType(s.Fields.Item(8), EDIParser.Field).Value      'Interchange Receiver ID
                oRsInterchange("InterDate").Value = CType(s.Fields.Item(9), EDIParser.Field).Value      'Interchange Date

            ElseIf s.Name = "GS" Then
                'add record in the funcGroup table to save the functional group information
                oRsFuncGroup.AddNew()
                oRsFuncGroup("FuncID").Value = CType(s.Fields.Item(1), EDIParser.Field).Value      'Functional Identifier Code
                oRsFuncGroup("ControlNo").Value = CType(s.Fields.Item(6), EDIParser.Field).Value      'Group Control Number
                'save the interchange primary key to FuncGroup table as foreign key
                oRsFuncGroup("Interkey").Value = oRsInterchange("InterKey").Value

            ElseIf s.Name = "GE" Then
                oRsFuncGroup.Update()

            ElseIf s.Name = "IEA" Then
                oRsInterchange.Update()



            ElseIf s.Name = "ST" Then
                'add record in the X098Header table to save the transaction set information
                oRsX098Header.AddNew()

                oRsX098Header("MessageNo").Value = CType(s.Fields.Item(1), EDIParser.Field).Value      'Transaction Set Identifier Code
                oRsX098Header("ControlNo").Value = CType(s.Fields.Item(2), EDIParser.Field).Value      'Transaction Set Control Number

                'save the FuncGroup primary key to X098Header table as foreign key
                oRsX098Header("Groupkey").Value = oRsFuncGroup("Groupkey").Value
            ElseIf s.Name = "BHT" Then
                oRsX098Header("ReferenceID").Value = CType(s.Fields.Item(3), EDIParser.Field).Value      'Reference Identification
                oRsX098Header("ReferenceDate").Value = CType(s.Fields.Item(4), EDIParser.Field).Value      'Date




            ElseIf sQlfr = "41" Then    'SUBMITTER
                If s.Name = "NM1" Then
                    oRsX098Header("SubmitterCompanyName").Value = CType(s.Fields.Item(3), EDIParser.Field).Value      'Name Last or Organization Name
                    oRsX098Header("SubmitterCode").Value = CType(s.Fields.Item(9), EDIParser.Field).Value      'Identification Code

                ElseIf s.Name = "PER" Then  'SUBMITTER EDI CONTACT INFORMATION
                    'txtSenderContactName = oSegment.DataElementValue(2)
                    'txtSenderContactPhone.Text = oSegment.DataElementValue(4)
                    'txtSenderContactExt.Text = oSegment.DataElementValue(6)
                End If

            ElseIf sQlfr = "40" Then    'RECEIVER
                If s.Name = "NM1" Then
                    oRsX098Header("ReceiverCompanyName").Value = CType(s.Fields.Item(3), EDIParser.Field).Value      'Name Last or Organization Name
                    oRsX098Header("ReceiverCode").Value = CType(s.Fields.Item(9), EDIParser.Field).Value      'Identification Code
                    sQlfr = ""
                End If



            ElseIf s.Name = "SE" Then
                oRsX098Header.Update()

         


                '**************************************************************
                '*  BILLING PROVIDER ******************************************
                '**************************************************************

            ElseIf sEntity = "20" Then  'BILLING PROVIDER
                If s.Name = "HL" Then
                    If Not oRsX098ProviderInfo.BOF Then
                        oRsX098ProviderInfo.Update()
                    End If
                    oRsX098ProviderInfo.AddNew()
                    oRsX098ProviderInfo("Headerkey").Value = oRsX098Header("Headerkey").Value


           
                ElseIf sQlfr = "85" Then    'BILLING PROVIDER NAME
                    If s.Name = "NM1" Then

                        oRsX098ProviderInfo("CompanyName").Value = CType(s.Fields.Item(3), EDIParser.Field).Value      'Name Last or Organization Name
                        oRsX098ProviderInfo("NationalID").Value = CType(s.Fields.Item(9), EDIParser.Field).Value     'Identification Code

                    ElseIf s.Name = "N3" Then
                        oRsX098ProviderInfo("Address1").Value = CType(s.Fields.Item(1), EDIParser.Field).Value      'Address Information

                    ElseIf s.Name = "N4" Then
                        oRsX098ProviderInfo("City").Value = CType(s.Fields.Item(1), EDIParser.Field).Value     'City Name
                        oRsX098ProviderInfo("State").Value = CType(s.Fields.Item(2), EDIParser.Field).Value      'State or Province Code
                        oRsX098ProviderInfo("Zip").Value = CType(s.Fields.Item(3), EDIParser.Field).Value     'Postal Code
                    End If

                End If  'sQlfr



                '**************************************************************
                '*  SUBSCRIBER ************************************************
                '**************************************************************
            ElseIf sEntity = "22" Then  'SUBSCRIBER

                If s.Name = "HL" Then

                    If Not oRsX098SubscriberInfo.BOF Then
                        oRsX098SubscriberInfo.Update()
                    End If

                    oRsX098SubscriberInfo.AddNew()
                    oRsX098SubscriberInfo("Providerkey").Value = oRsX098ProviderInfo("Providerkey").Value

                ElseIf s.Name = "SBR" Then    'SUBSCRIBER INFORMATION
                    'txtPatientGroupNo.Text = oSegment.DataElementValue(3)



                ElseIf sQlfr = "IL" Then    'SUBSCRIBER NAME
                    If s.Name = "NM1" Then
                        oRsX098SubscriberInfo("SubscriberLastOrgName").Value = CType(s.Fields.Item(3), EDIParser.Field).Value      'Name Last or Organization Name
                        oRsX098SubscriberInfo("SubscriberFirstName").Value = CType(s.Fields.Item(4), EDIParser.Field).Value     'Name First
                        oRsX098SubscriberInfo("SubscriberMemberID").Value = CType(s.Fields.Item(9), EDIParser.Field).Value      'Identification Code

                    ElseIf s.Name = "N3" Then
                        oRsX098SubscriberInfo("SubscriberAddress").Value = CType(s.Fields.Item(1), EDIParser.Field).Value      'Address Information

                    ElseIf s.Name = "N4" Then
                        oRsX098SubscriberInfo("SubscriberCity").Value = CType(s.Fields.Item(1), EDIParser.Field).Value     'City Name
                        oRsX098SubscriberInfo("SubscriberZip").Value = CType(s.Fields.Item(3), EDIParser.Field).Value      'Postal Code

                    ElseIf s.Name = "DMG" Then  'SUBSCRIBER DEMOGRAPHIC INFORMATION
                        oRsX098SubscriberInfo("SubscriberDOB").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                        oRsX098SubscriberInfo("SubscriberGender").Value = CType(s.Fields.Item(3), EDIParser.Field).Value

                    ElseIf s.Name = "REF" Then
                    End If

                ElseIf sQlfr = "PR" Then    ' PAYER NAME
                    If s.Name = "NM1" Then
                        oRsX098SubscriberInfo("PayerLastOrgName").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                        oRsX098SubscriberInfo("PayerID").Value = CType(s.Fields.Item(9), EDIParser.Field).Value

                    ElseIf s.Name = "N2" Then
                        sQlfr = ""
                    End If


                ElseIf s.Name = "CLM" Then     'claim informaton of the subscriber patient

                    If Not oRsX098Claims.BOF Then
                        oRsX098Claims.Update()
                    End If
                    oRsX098Claims.AddNew()
                    oRsX098Claims("SubscriberKey").Value = oRsX098SubscriberInfo("SubscriberKey").Value

                    oRsX098Claims("PatientAccountNo").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                    oRsX098Claims("ClaimAmount").Value = CType(s.Fields.Item(2), EDIParser.Field).Value

                ElseIf s.Name = "DTP" Then
                    oRsX098Claims("ClaimDate").Value = CType(s.Fields.Item(3), EDIParser.Field).Value

                ElseIf s.Name = "REF" Then
                ElseIf s.Name = "HI" Then



                ElseIf sQlfr = "82" Then    'RENDERING PROVIDER NAME
                    If s.Name = "NM1" Then
                        oRsX098Claims("RenderingLastname").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                        oRsX098Claims("RenderingFirstname").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                        oRsX098Claims("RenderingID").Value = CType(s.Fields.Item(9), EDIParser.Field).Value

                    ElseIf s.Name = "PRV" Then
                    End If

                ElseIf sQlfr = "77" Then
                    If s.Name = "NM1" Then
                        oRsX098Claims("FacilityName").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                        oRsX098Claims("FacilityID").Value = CType(s.Fields.Item(9), EDIParser.Field).Value

                    ElseIf s.Name = "N3" Then
                        oRsX098Claims("FacilityAddr").Value = CType(s.Fields.Item(1), EDIParser.Field).Value

                    ElseIf s.Name = "N4" Then
                        oRsX098Claims("FacilityCity").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                        oRsX098Claims("FacilityState").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                        oRsX098Claims("FacilityZip").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                        sQlfr = ""
                    End If


                ElseIf s.Name = "LX" Then  'service line detail of the claim

                    If Not oRsX098ServiceInfo.BOF Then
                        oRsX098ServiceInfo.Update()
                    End If
                    oRsX098ServiceInfo.AddNew()

                    oRsX098ServiceInfo("ServiceLine").Value = nLineCount + 1

                ElseIf s.Name = "SV1" Then
                    oRsX098ServiceInfo("ServiceID").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                    oRsX098ServiceInfo("ServiceAmount").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                    oRsX098ServiceInfo("Diagnosis").Value = CType(s.Fields.Item(7), EDIParser.Field).Value

                ElseIf s.Name = "DTP" Then
                    oRsX098ServiceInfo("ServiceDate").Value = CType(s.Fields.Item(3), EDIParser.Field).Value

                End If



                '**************************************************************
                '*  DEPENDENT ************************************************
                '**************************************************************
            ElseIf sEntity = "23" Then  'DEPENDENT

                If s.Name = "HL" Then
                    If Not oRsX098DependentInfo.BOF Then
                        oRsX098DependentInfo.Update()
                    End If
                    oRsX098DependentInfo.AddNew()
                    oRsX098DependentInfo("SubscriberKey").Value = oRsX098SubscriberInfo("SubscriberKey").Value
                ElseIf s.Name = "PAT" Then
                    If CType(s.Fields.Item(1), EDIParser.Field).Value = "01" Then
                        oRsX098DependentInfo("Relationship").Value = "SPOUSE"
                    ElseIf CType(s.Fields.Item(1), EDIParser.Field).Value = "19" Then
                        oRsX098DependentInfo("Relationship").Value = "CHILD"
                    Else
                        oRsX098DependentInfo("Relationship").Value = "OTHER"
                    End If




                ElseIf sQlfr = "QC" Then
                    If s.Name = "NM1" Then
                        oRsX098DependentInfo("Lastname").Value = CType(s.Fields.Item(3), EDIParser.Field).Value      'Name Last or Organization Name
                        oRsX098DependentInfo("Firstname").Value = CType(s.Fields.Item(4), EDIParser.Field).Value    'Name First

                    ElseIf s.Name = "N3" Then
                        oRsX098DependentInfo("Address").Value = CType(s.Fields.Item(1), EDIParser.Field).Value      'Address Information

                    ElseIf s.Name = "N4" Then
                        oRsX098DependentInfo("City").Value = CType(s.Fields.Item(1), EDIParser.Field).Value     'City Name
                        oRsX098DependentInfo("State").Value = CType(s.Fields.Item(2), EDIParser.Field).Value      'State or Province Code
                        oRsX098DependentInfo("Zip").Value = CType(s.Fields.Item(3), EDIParser.Field).Value     'Postal Code

                    ElseIf s.Name = "DMG" Then
                        '                        sValue = oSegment.DataElementValue(1)     'Date Time Period Format Qualifier
                        oRsX098DependentInfo("DOB").Value = CType(s.Fields.Item(2), EDIParser.Field).Value     'Date Time Period
                        oRsX098DependentInfo("Gender").Value = CType(s.Fields.Item(3), EDIParser.Field).Value     'Gender Code
                        sQlfr = ""
                    End If   'Segment ID



                ElseIf s.Name = "CLM" Then     'claim informaton of the dependent patient

                    If Not oRsX098Claims.BOF Then
                        oRsX098Claims.Update()
                    End If
                    oRsX098Claims.AddNew()
                    oRsX098Claims("SubscriberKey").Value = oRsX098DependentInfo("SubscriberKey").Value
                    oRsX098Claims("DependentKey").Value = oRsX098DependentInfo("DependentKey").Value

                    oRsX098Claims("PatientAccountNo").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                    oRsX098Claims("ClaimAmount").Value = CType(s.Fields.Item(2), EDIParser.Field).Value

                ElseIf s.Name = "DTP" Then
                    oRsX098Claims("ClaimDate").Value = CType(s.Fields.Item(3), EDIParser.Field).Value

                ElseIf s.Name = "REF" Then
                ElseIf s.Name = "HI" Then



                ElseIf sQlfr = "82" Then    'RENDERING PROVIDER NAME
                    If s.Name = "NM1" Then
                        oRsX098Claims("RenderingLastname").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                        oRsX098Claims("RenderingFirstname").Value = CType(s.Fields.Item(4), EDIParser.Field).Value
                        oRsX098Claims("RenderingID").Value = CType(s.Fields.Item(9), EDIParser.Field).Value

                    ElseIf s.Name = "PRV" Then
                    End If

                ElseIf sQlfr = "77" Then
                    If s.Name = "NM1" Then
                        oRsX098Claims("FacilityName").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                        oRsX098Claims("FacilityID").Value = CType(s.Fields.Item(9), EDIParser.Field).Value

                    ElseIf s.Name = "N3" Then
                        oRsX098Claims("FacilityAddr").Value = CType(s.Fields.Item(1), EDIParser.Field).Value

                    ElseIf s.Name = "N4" Then
                        oRsX098Claims("FacilityCity").Value = CType(s.Fields.Item(1), EDIParser.Field).Value
                        oRsX098Claims("FacilityState").Value = CType(s.Fields.Item(2), EDIParser.Field).Value
                        oRsX098Claims("FacilityZip").Value = CType(s.Fields.Item(3), EDIParser.Field).Value
                    End If

                End If




            End If  'sEntity



        Next
        oRsX098ServiceInfo.Update()
        oRsX098Claims.Update()
        oRsX098DependentInfo.Update()
        oRsX098ProviderInfo.Update()
        oRsX098SubscriberInfo.Update()

        Me.Cursor = Cursors.Default
        MsgBox("Done")

    End Sub
End Class
