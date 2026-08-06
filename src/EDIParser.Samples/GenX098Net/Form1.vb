

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
    Friend WithEvents cmdGenerate As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.cmdGenerate = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdGenerate
        '
        Me.cmdGenerate.Location = New System.Drawing.Point(112, 136)
        Me.cmdGenerate.Name = "cmdGenerate"
        Me.cmdGenerate.Size = New System.Drawing.Size(136, 32)
        Me.cmdGenerate.TabIndex = 0
        Me.cmdGenerate.Text = "Generate"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(336, 72)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(376, 198)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdGenerate)
        Me.Name = "Form1"
        Me.Text = "Generating an 837 X098 Outbound"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click


        Dim iItemCount As Integer
        Dim sInstance As String
        Dim sSql As String
        Dim nHlCount As Integer
        Dim nHlProvParent As Integer
        Dim nHlSubscriberParent As Integer

        Dim oClaimsDs As DataSet
        Dim oClaimsRow As DataRow
        Dim oServiceDs As DataSet
        Dim oServiceRow As DataRow


        ' Dim sPath As String = AppDomain.CurrentDomain.BaseDirectory
        Dim sPath As String = "App_Data/db1.mdb"
        'connection string to an access database
        Dim sConnection As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & sPath

        'create connection to database
        Dim oConnection As New OleDb.OleDbConnection(sConnection)
        oConnection.Open()

        'Prepare a dataset from the Interchange table
        sSql = "select * from Interchange"
        Dim oAdapter As New OleDb.OleDbDataAdapter(sSql, oConnection)
        Dim oInterchangeDS As New DataSet("dsInterchange")
        Dim oInterchangeRow As DataRow
        oAdapter.Fill(oInterchangeDS, "dsInterchange")
        Dim oX12Parser As New EDIParser.X12Parser()
        ' oX12Parser.SegmentSeparator = "~" + vbCrLf
        'create interchange loop
        For Each oInterchangeRow In oInterchangeDS.Tables("dsInterchange").Rows

         
            'CREATE INTERCHANGE
            oX12Parser.SetValue("ISA.1", "00")
            oX12Parser.SetValue("ISA.2", "          ")
            oX12Parser.SetValue("ISA.3", "00")               'Security Information Qualifier
            oX12Parser.SetValue("ISA.4", "          ")
            oX12Parser.SetValue("ISA.5", "12") 'Interchange Sender ID
            oX12Parser.SetValue("ISA.6", oInterchangeRow("SenderID") & "     ")               'Interchange ID Qualifier
            oX12Parser.SetValue("ISA.7", "12")  'Interchange Receiver ID
            oX12Parser.SetValue("ISA.8", oInterchangeRow("ReceiverID") & "   ")           'Interchange Date
            oX12Parser.SetValue("ISA.9", "010821")
            oX12Parser.SetValue("ISA.10", "1548")            'Interchange Time
            oX12Parser.SetValue("ISA.11", "U")               'Interchange Control Standards Identifier
            oX12Parser.SetValue("ISA.12", "00401")           'Interchange Control Version Number
            oX12Parser.SetValue("ISA.13", oInterchangeRow("ControlNo"))       'Interchange Control Number
            oX12Parser.SetValue("ISA.14", "0")               'Acknowledgment Requested
            oX12Parser.SetValue("ISA.15", "T")               'Usage Indicator
            oX12Parser.SetValue("ISA.16", ":")               'Component Element Separato
         
            'prepare dataset from the FuncGroup table
            sSql = "select * from FuncGroup where Interkey = " & Trim(Str(oInterchangeRow("Interkey")))
            oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
            Dim oGroupDs As New DataSet("dsGroup")
            Dim oGroupRow As DataRow
            oAdapter.Fill(oGroupDs, "dsGroup")

            'create the functional group loop
            For Each oGroupRow In oGroupDs.Tables("dsGroup").Rows

                'CREATE FUNCTIONAL GROUP

                oX12Parser.SetValue("GS.1", oGroupRow("FuncID"))             'Functional Identifier Code
                oX12Parser.SetValue("GS.2", "SenderDept")     'Application Sender's Code
                oX12Parser.SetValue("GS.3", "ReceiverDept")  'Application Receiver's Code
                oX12Parser.SetValue("GS.4", "20010821")       'Date
                oX12Parser.SetValue("GS.5", "1548")           'Time
                oX12Parser.SetValue("GS.6", oGroupRow("ControlNo"))         'Group Control Number
                oX12Parser.SetValue("GS.7", "X")              'Responsible Agency Code
                oX12Parser.SetValue("GS.8", "004010X098") '   'Version / Release / Industry Identifier Code

                'prepare dataset from X098Header table 
                sSql = "select * from X098Header where Groupkey = " & Trim(Str(oGroupRow("Groupkey")))
                oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                Dim oSetDs As New DataSet("dsSet")
                Dim oSetRow As DataRow
                oAdapter.Fill(oSetDs, "dsSet")
                Dim nNM1Counter As Integer = 1
                Dim nN3Counter As Integer = 1
                Dim nN2Counter As Integer = 1
                Dim nREFCounter As Integer = 1
                Dim nBHTCounter As Integer = 1
                Dim nDMGCounter As Integer = 1
                Dim nSBRCounter As Integer = 1
                Dim nCLMCounter As Integer = 1
                Dim nDTPCounter As Integer = 1
                Dim nHICounter As Integer = 1
                Dim nLXCounter As Integer = 1
                Dim nSVCCounter As Integer = 1
                Dim nPERCounter As Integer = 1
                Dim nPATCounter As Integer = 1
                Dim nPRVCounter As Integer = 1
                'create the transaction set loop
                Dim nSTCounter As Integer = 1
                For Each oSetRow In oSetDs.Tables("dsSet").Rows

                    nHlCount = 0

                    'HEADER
                    'ST TRANSACTION SET HEADER
                    oX12Parser.SetValue("ST.1", "837", nSTCounter)     'Transaction Set Identifier Code
                    oX12Parser.SetValue("ST.2", oSetRow("ControlNo"), nSTCounter)   'Transaction Set Control Number


                    'BHT BEGINNING OF HIERARCHICAL TRANSACTION
                    oX12Parser.SetValue("BHT.1", "0019", nBHTCounter)
                    oX12Parser.SetValue("BHT.2", "00", nBHTCounter)
                    oX12Parser.SetValue("BHT.3", "0123", nBHTCounter)
                    oX12Parser.SetValue("BHT.4", "19981015", nBHTCounter)
                    oX12Parser.SetValue("BHT.5", "1230", nBHTCounter)
                    oX12Parser.SetValue("BHT.6", "RP", nBHTCounter)

                    'REF TRANSMISSION TYPE IDENTIFICATION
                    oX12Parser.SetValue("REF.1", "87", nREFCounter)
                    oX12Parser.SetValue("REF.2", oSetRow("ReferenceID"), nREFCounter)
                    nREFCounter += 1

                    '1000A SUBMITTER
                    'NM1 SUBMITTER
                    oX12Parser.SetValue("NM1.1", "41", nNM1Counter)
                    oX12Parser.SetValue("NM1.2", "2", nNM1Counter)
                    oX12Parser.SetValue("NM1.3", oSetRow("SubmitterCompanyName"), nNM1Counter)
                    oX12Parser.SetValue("NM1.8", "46", nNM1Counter)
                    oX12Parser.SetValue("NM1.9", oSetRow("SubmitterCode"), nNM1Counter)
                    nNM1Counter += 1

                    'PER SUBMITTER EDI CONTACT INFORMATION

                    oX12Parser.SetValue("PER.1", "IC", nPERCounter)
                    oX12Parser.SetValue("PER.2", oSetRow("SubmitterContactName"), nPERCounter)
                    oX12Parser.SetValue("PER.3", "TE", nPERCounter)
                    oX12Parser.SetValue("PER.4", oSetRow("SubmitterPhone"), nPERCounter)
                    oX12Parser.SetValue("PER.5", "EX", nPERCounter)
                    oX12Parser.SetValue("PER.6", oSetRow("SubmitterExt"), nPERCounter)
                    nPERCounter += 1

                    '1000B RECEIVER
                    'NM1 RECEIVER NAME
                    oX12Parser.SetValue("NM1.1", "40", nNM1Counter)
                    oX12Parser.SetValue("NM1.2", "2", nNM1Counter)
                    oX12Parser.SetValue("NM1.3", oSetRow("ReceiverCompanyName"), nNM1Counter)
                    oX12Parser.SetValue("NM1.8", "46", nNM1Counter)
                    oX12Parser.SetValue("NM1.9", oSetRow("ReceiverCode"), nNM1Counter)
                    nNM1Counter += 1


                    '**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************
                    sSql = "select * from X098ProviderInfo where Headerkey = " & Trim(Str(oSetRow("Headerkey")))
                    oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                    Dim oProviderDs As New DataSet("dsProvider")
                    Dim oProviderRow As DataRow
                    oAdapter.Fill(oProviderDs, "dsProvider")
                    For Each oProviderRow In oProviderDs.Tables("dsProvider").Rows

                        nHlCount = nHlCount + 1
                        nHlProvParent = nHlCount

                        '2000A BILLING/PAY-TO PROVIDER HL LOOP
                        'HL-BILLING PROVIDER
                        oX12Parser.SetValue("HL.1", nHlCount, nHlCount)
                        oX12Parser.SetValue("HL.3", "20", nHlCount)
                        oX12Parser.SetValue("HL.4", "1", nHlCount)
                        '2010AA BILLING PROVIDER
                        'NM1 BILLING PROVIDER NAME
                        oX12Parser.SetValue("NM1.1", "85", nNM1Counter)
                        oX12Parser.SetValue("NM1.2", "2", nNM1Counter)
                        oX12Parser.SetValue("NM1.3", oProviderRow("CompanyName"), nNM1Counter)
                        oX12Parser.SetValue("NM1.8", "XX", nNM1Counter)
                        oX12Parser.SetValue("NM1.9", oProviderRow("BillingID"), nNM1Counter)
                        nNM1Counter += 1


                        'N3 BILLING PROVIDER ADDRESS
                        oX12Parser.SetValue("N3.1", oProviderRow("Address1"), nN3Counter)

                        'N4 BILLING PROVIDER LOCATION
                        oX12Parser.SetValue("N4.1", oProviderRow("City"), nN3Counter)
                        oX12Parser.SetValue("N4.2", oProviderRow("State"), nN3Counter)
                        oX12Parser.SetValue("N4.3", oProviderRow("Zip"), nN3Counter)
                        nN3Counter += 1

                        '******************************************************************************************************
                        '******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                        '******************************************************************************************************

                        sSql = "select * from X098SubscriberInfo where Providerkey = " & Trim(Str(oProviderRow("Providerkey")))
                        oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                        Dim oSubscriberDs As New DataSet("dsSubscriber")
                        Dim oSubscriberRow As DataRow
                        oAdapter.Fill(oSubscriberDs, "dsSubscriber")
                        For Each oSubscriberRow In oSubscriberDs.Tables("dsSubscriber").Rows
                            nHlCount = nHlCount + 1
                            nHlSubscriberParent = nHlCount

                            '2000B SUBSCRIBER HL LOOP
                            'HL-SUBSCRIBER
                            oX12Parser.SetValue("HL.1", nHlCount, nHlCount)
                            oX12Parser.SetValue("HL.2", nHlProvParent, nHlCount)
                            oX12Parser.SetValue("HL.3", "22", nHlCount)


                            Dim sIndvRel As String = IIf(IsDBNull(oSubscriberRow("IndividualRelation")), "", oSubscriberRow("IndividualRelation"))
                            If sIndvRel = "18" Then
                                oX12Parser.SetValue("HL.4", "0", nHlCount)
                            Else
                                oX12Parser.SetValue("HL.4", "1", nHlCount)
                            End If

                            'SBR SUBSCRIBER INFORMATION
                            oX12Parser.SetValue("SBR.1", "P", nSBRCounter)
                            oX12Parser.SetValue("SBR.2", sIndvRel, nSBRCounter)
                            oX12Parser.SetValue("SBR.3", oSubscriberRow("PolicyNo"), nSBRCounter)
                            oX12Parser.SetValue("SBR.9", "HM", nSBRCounter)
                            nSBRCounter += 1

                            '2010BA SUBSCRIBER
                            'NM1 SUBSCRIBER NAME
                            oX12Parser.SetValue("NM1.1", "IL", nNM1Counter)
                            oX12Parser.SetValue("NM1.2", "1", nNM1Counter)
                            oX12Parser.SetValue("NM1.3", oSubscriberRow("SubscriberLastOrgName"), nNM1Counter)
                            oX12Parser.SetValue("NM1.4", oSubscriberRow("SubscriberFirstname"), nNM1Counter)
                            oX12Parser.SetValue("NM1.8", "MI", nNM1Counter)
                            oX12Parser.SetValue("NM1.9", oSubscriberRow("SubscriberMemberID"), nNM1Counter)
                            nNM1Counter += 1
                            'N3 SUBSCRIBER ADDRESS
                            oX12Parser.SetValue("N3.1", oSubscriberRow("SubscriberAddress"), nN3Counter)


                            'N4 SUBSCRIBER CITY
                            oX12Parser.SetValue("N4.1", oSubscriberRow("SubscriberCity"), nN3Counter)
                            oX12Parser.SetValue("N4.2", oSubscriberRow("SubscrberState"), nN3Counter)
                            oX12Parser.SetValue("N4.3", oSubscriberRow("SubscriberZip"), nN3Counter)
                            nN3Counter += 1

                            'DMG SUBSCRIBER DEMOGRAPHIC INFORMATION
                            If Not IsDBNull(oSubscriberRow("SubscriberDOB")) Then
                                oX12Parser.SetValue("DMG.1", "D8", nDMGCounter)
                                oX12Parser.SetValue("DMG.2", oSubscriberRow("SubscriberDOB"), nDMGCounter)
                                oX12Parser.SetValue("DMG.3", oSubscriberRow("SubscriberGender"), nDMGCounter)
                                nDMGCounter += 1
                            End If


                            '2010BB SUBSCRIBER/PAYER
                            'NM1 PAYER NAME

                            oX12Parser.SetValue("NM1.1", "PR", nNM1Counter)
                            oX12Parser.SetValue("NM1.2", "2", nNM1Counter)
                            If Not IsDBNull(oSubscriberRow("PayerLastOrgName")) Then
                                oX12Parser.SetValue("NM1.3", oSubscriberRow("PayerLastOrgName"), nNM1Counter)
                            End If
                            If Not IsDBNull(oSubscriberRow("PayerID")) Then
                                oX12Parser.SetValue("NM1.8", "PI", nNM1Counter)
                                oX12Parser.SetValue("NM1.9", oSubscriberRow("PayerID"), nNM1Counter)
                            End If
                            nNM1Counter += 1
                            'N2 PAYER ADDITIONAL NAME INFORMATION
                            oX12Parser.SetValue("N2.1", "COMPANY", nN2Counter)
                            nN2Counter += 1




                            '******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                            sSql = "select * from X098Claims where Subscriberkey = " & Trim(Str(oSubscriberRow("Subscriberkey")))
                            oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                            oClaimsDs = New DataSet("dsClaims")
                            oAdapter.Fill(oClaimsDs, "dsClaims")
                            For Each oClaimsRow In oClaimsDs.Tables("dsClaims").Rows
                                '2300 CLAIM
                                'CLM CLAIM LEVEL INFORMATION

                                oX12Parser.SetValue("CLM.1", oClaimsRow("PatientAccountNo"), nCLMCounter)
                                oX12Parser.SetValue("CLM.2", oClaimsRow("ClaimAmount"), nCLMCounter)
                                oX12Parser.SetValue("CLM.5", "11::1", nCLMCounter)
                                oX12Parser.SetValue("CLM.6", "Y", nCLMCounter)
                                oX12Parser.SetValue("CLM.7", "A", nCLMCounter)
                                oX12Parser.SetValue("CLM.8", "Y", nCLMCounter)
                                oX12Parser.SetValue("CLM.9", "Y", nCLMCounter)
                                oX12Parser.SetValue("CLM.10", "C", nCLMCounter)
                                nCLMCounter += 1
                                'DTP DATE OF ONSET
                                oX12Parser.SetValue("DTP.1", "431", nDTPCounter)
                                oX12Parser.SetValue("DTP.2", "D8", nDTPCounter)
                                oX12Parser.SetValue("DTP.3", oClaimsRow("ClaimDate"), nDTPCounter)
                                nDTPCounter += 1
                                'REF CLEARING HOUSE CLAIM NUMBER

                                oX12Parser.SetValue("REF.1", "D9", nREFCounter)
                                oX12Parser.SetValue("REF.2", "17312345600006351", nREFCounter)
                                nREFCounter += 1
                                'HI HEALTH CARE DIAGNOSIS CODES

                                oX12Parser.SetValue("HI.1", "BK:0340", nHICounter)
                                oX12Parser.SetValue("HI.2", "BF:V7389", nHICounter)
                                nHICounter += 1


                                '2310B RENDERING PROVIDER
                                'NM1 RENDERING PROVIDER NAME

                                oX12Parser.SetValue("NM1.1", "82", nNM1Counter)
                                oX12Parser.SetValue("NM1.2", "1", nNM1Counter)
                                oX12Parser.SetValue("NM1.3", oClaimsRow("RenderingLastname"), nNM1Counter)
                                oX12Parser.SetValue("NM1.4", oClaimsRow("RenderingFirstname"), nNM1Counter)
                                oX12Parser.SetValue("NM1.8", "34", nNM1Counter)
                                oX12Parser.SetValue("NM1.9", oClaimsRow("RenderingID"), nNM1Counter)
                                nNM1Counter += 1
                                'PRV RENDERING PROVIDER INFORMATION

                                oX12Parser.SetValue("PRV.1", "PE", nHICounter)
                                oX12Parser.SetValue("PRV.2", "ZZ", nHICounter)
                                oX12Parser.SetValue("PRV.3", "203BF0100Y", nHICounter)
                                nHICounter += 1


                                '2310D SERVICE LOCATION
                                'NM1 SERVICE FACILITY LOCATION
                                oX12Parser.SetValue("NM1.1", "77", nNM1Counter)
                                oX12Parser.SetValue("NM1.2", "2", nNM1Counter)
                                oX12Parser.SetValue("NM1.3", oClaimsRow("FacilityName"), nNM1Counter)
                                oX12Parser.SetValue("NM1.8", "24", nNM1Counter)
                                oX12Parser.SetValue("NM1.9", oClaimsRow("FacilityID"), nNM1Counter)
                                nNM1Counter += 1

                                'N3 SERVICE FACILITY ADDRESS
                                oX12Parser.SetValue("N3.1", oClaimsRow("FacilityAddr"), nN3Counter)


                                'N4 SERVICE FACILITY CITY/STATE/ZIP
                                oX12Parser.SetValue("N4.1", oClaimsRow("FacilityCity"), nN3Counter)
                                oX12Parser.SetValue("N4.2", oClaimsRow("FacilityState"), nN3Counter)
                                oX12Parser.SetValue("N4.3", oClaimsRow("FacilityZip"), nN3Counter)
                                nN3Counter += 1

                                '******* SUBSCRIBER SERVICE LINE *************************************************************
                                sSql = "select * from X098ServiceInfo where Claimskey = " & Trim(Str(oClaimsRow("Claimskey")))
                                oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                                oServiceDs = New DataSet("dsService")
                                oAdapter.Fill(oServiceDs, "dsService")
                                '2400 SERVICE LINE
                                iItemCount = 0
                                For Each oServiceRow In oServiceDs.Tables("dsService").Rows
                                    iItemCount = iItemCount + 1
                                    sInstance = Trim(Str(iItemCount))

                                    'LX SERVICE LINE COUNTER

                                    oX12Parser.SetValue("LX.1", iItemCount, nSVCCounter)

                                    'SV1 PROFESSIONAL SERVICE
                                    oX12Parser.SetValue("SV1.1", "HC:" & oServiceRow("ServiceID") & "", nSVCCounter)
                                    oX12Parser.SetValue("SV1.2", oServiceRow("ServiceAmount"), nSVCCounter)
                                    oX12Parser.SetValue("SV1.3", "UN", nSVCCounter)
                                    oX12Parser.SetValue("SV1.4", "1", nSVCCounter)
                                    oX12Parser.SetValue("SV1.7", oServiceRow("Diagnosis"), nSVCCounter)
                                    oX12Parser.SetValue("SV1.9", "N", nSVCCounter)
                                    nSVCCounter += 1

                                    'DTP DATE - SERVICE DATE(S)
                                    oX12Parser.SetValue("DTP.1", "472", nDTPCounter)
                                    oX12Parser.SetValue("DTP.2", "D8", nDTPCounter)
                                    oX12Parser.SetValue("DTP.3", oServiceRow("ServiceDate"), nDTPCounter)
                                    nDTPCounter += 1

                                Next    'Service
                            Next    'Claims




                            '*****************************************************************************************************
                            '******* DEPENDENT HIERARCHICAL LEVEL ****************************************************************
                            '*****************************************************************************************************
                            sSql = "select * from X098DependentInfo where SubscriberKey = " & Trim(Str(oSubscriberRow("SubscriberKey")))
                            oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                            Dim oDependentDs As New DataSet("dsDependent")
                            Dim oDependentRow As DataRow
                            oAdapter.Fill(oDependentDs, "dsDependent")
                            For Each oDependentRow In oDependentDs.Tables("dsDependent").Rows
                                nHlCount = nHlCount + 1

                                '2000B DEPENDENT HL LOOP
                                'HL-DEPENDENT
                                oX12Parser.SetValue("HL.1", nHlCount, nHlCount)
                                oX12Parser.SetValue("HL.2", nHlSubscriberParent, nHlCount)
                                oX12Parser.SetValue("HL.3", "23", nHlCount)
                                oX12Parser.SetValue("HL.4", "0", nHlCount)

                                'PAT - PATIENT dependent INFORMATION
                                oX12Parser.SetValue("PAT.1", oDependentRow("RelationshipCode"), nPATCounter)
                                nPATCounter += 1

                                'NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                oX12Parser.SetValue("NM1.1", "QC", nNM1Counter)
                                oX12Parser.SetValue("NM1.2", "1", nNM1Counter)
                                oX12Parser.SetValue("NM1.3", oDependentRow("Lastname"), nNM1Counter)
                                oX12Parser.SetValue("NM1.4", oDependentRow("Firstname"), nNM1Counter)
                                nNM1Counter += 1
                                'N3 - ADDRESS INFORMATION
                                oX12Parser.SetValue("N3.1", oDependentRow("Address"), nN3Counter)

                                'N4 - GEOGRAPHIC LOCATION

                                If Not IsDBNull(oDependentRow("City")) Then
                                    oX12Parser.SetValue("N4.1", oDependentRow("City"), nN3Counter)  'City Name
                                End If

                                If Not IsDBNull(oDependentRow("State")) Then
                                    oX12Parser.SetValue("N4.2", oDependentRow("State"), nN3Counter)  'State
                                End If

                                If Not IsDBNull(oDependentRow("Zip")) Then 'Zip code
                                    oX12Parser.SetValue("N4.3", oDependentRow("Zip"), nN3Counter)   'Zip
                                End If
                                nN3Counter += 1
                                'DMG - DEMOGRAPHIC INFORMATION
                                If Not IsDBNull(oDependentRow("DOB")) Then


                                    oX12Parser.SetValue("DMG.1", "D8", nDMGCounter) 'Date Time Period Format Qualifier
                                    oX12Parser.SetValue("DMG.2", oDependentRow("DOB"), nDMGCounter) 'Date
                                    oX12Parser.SetValue("DMG.3", oDependentRow("Gender"), nDMGCounter) 'State Name
                                    nDMGCounter += 1
                                End If

                                '******* DEPENDENT CLAIM INFORMATION *************************************************************
                                sSql = "select * from X098Claims where Dependentkey = " & Trim(Str(oDependentRow("Dependentkey"))) & " and Subscriberkey = " & Trim(Str(oSubscriberRow("Subscriberkey")))
                                oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                                oClaimsDs = New DataSet("dsClaims")
                                oAdapter.Fill(oClaimsDs, "dsClaims")
                                For Each oClaimsRow In oClaimsDs.Tables("dsClaims").Rows
                                    '2300 CLAIM
                                    'CLM CLAIM LEVEL INFORMATION


                                    oX12Parser.SetValue("CLM.1", oClaimsRow("PatientAccountNo"), nCLMCounter)
                                    oX12Parser.SetValue("CLM.2", oClaimsRow("ClaimAmount"), nCLMCounter)
                                    oX12Parser.SetValue("CLM.5", "11::1", nCLMCounter)
                                    oX12Parser.SetValue("CLM.6", "Y", nCLMCounter)
                                    oX12Parser.SetValue("CLM.7", "A", nCLMCounter)
                                    oX12Parser.SetValue("CLM.8", "Y", nCLMCounter)
                                    oX12Parser.SetValue("CLM.9", "Y", nCLMCounter)
                                    oX12Parser.SetValue("CLM.10", "C", nCLMCounter)
                                    nCLMCounter += 1
                                    'DTP DATE OF ONSET
                                    oX12Parser.SetValue("DTP.1", "431", nDTPCounter)
                                    oX12Parser.SetValue("DTP.2", "D8", nDTPCounter)
                                    oX12Parser.SetValue("DTP.3", oClaimsRow("ClaimDate"), nDTPCounter)
                                    nDTPCounter += 1

                                    'REF CLEARING HOUSE CLAIM NUMBER
                                    oX12Parser.SetValue("REF.1", "D9", nREFCounter)
                                    oX12Parser.SetValue("REF.2", "17312345600006351", nREFCounter)
                                    nREFCounter += 1

                                    'HI HEALTH CARE DIAGNOSIS CODES
                                    oX12Parser.SetValue("HI.1", "BK:0340", nHICounter)
                                    oX12Parser.SetValue("HI.2", "BF:V7389", nHICounter)
                                    nHICounter += 1

                                    '2310B RENDERING PROVIDER
                                    'NM1 RENDERING PROVIDER NAME
                                    oX12Parser.SetValue("NM1.1", "82", nNM1Counter)
                                    oX12Parser.SetValue("NM1.2", "1", nNM1Counter)
                                    oX12Parser.SetValue("NM1.3", oClaimsRow("RenderingLastname"), nNM1Counter)
                                    oX12Parser.SetValue("NM1.4", oClaimsRow("RenderingFirstname"), nNM1Counter)
                                    oX12Parser.SetValue("NM1.8", "34", nNM1Counter)
                                    oX12Parser.SetValue("NM1.9", oClaimsRow("RenderingID"), nNM1Counter)
                                    nNM1Counter += 1

                                    'PRV RENDERING PROVIDER INFORMATION
                                    oX12Parser.SetValue("PRV.1", "PE", nPRVCounter)
                                    oX12Parser.SetValue("PRV.2", "ZZ", nPRVCounter)
                                    oX12Parser.SetValue("PRV.3", "203BF0100Y", nPRVCounter)
                                    nPRVCounter += 1

                                    '2310D SERVICE LOCATION
                                    'NM1 SERVICE FACILITY LOCATION
                                    oX12Parser.SetValue("NM1.1", "77", nNM1Counter)
                                    oX12Parser.SetValue("NM1.2", "2", nNM1Counter)
                                    oX12Parser.SetValue("NM1.3", oClaimsRow("FacilityName"), nNM1Counter)
                                    oX12Parser.SetValue("NM1.8", "24", nNM1Counter)
                                    oX12Parser.SetValue("NM1.9", oClaimsRow("FacilityID"), nNM1Counter)
                                    nNM1Counter += 1

                                    'N3 SERVICE FACILITY ADDRESS
                                    oX12Parser.SetValue("N3.1", oClaimsRow("FacilityAddr"), nN3Counter)


                                    'N4 SERVICE FACILITY CITY/STATE/ZIP
                                    oX12Parser.SetValue("N4.1", oClaimsRow("FacilityCity"), nN3Counter)
                                    oX12Parser.SetValue("N4.2", oClaimsRow("FacilityState"), nN3Counter)
                                    oX12Parser.SetValue("N4.3", oClaimsRow("FacilityZip"), nN3Counter)
                                    nN3Counter += 1

                                    '******* DEPENDENT SERVICE LINE **************************************************************
                                    sSql = "select * from X098ServiceInfo where Claimskey = " & Trim(Str(oClaimsRow("Claimskey")))
                                    oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                                    oServiceDs = New DataSet("dsService")
                                    oAdapter.Fill(oServiceDs, "dsService")
                                    '2400 SERVICE LINE
                                    iItemCount = 0
                                    For Each oServiceRow In oServiceDs.Tables("dsService").Rows
                                        iItemCount = iItemCount + 1
                                        sInstance = Trim(Str(iItemCount))
                                        oX12Parser.SetValue("LX.1", iItemCount, nLXCounter)
                                        nLXCounter += 1

                                        'SV1 PROFESSIONAL SERVICE
                                        oX12Parser.SetValue("SV1.1", "HC:" & oServiceRow("ServiceID") & "", nSVCCounter)
                                        oX12Parser.SetValue("SV1.2", oServiceRow("ServiceAmount"), nSVCCounter)
                                        oX12Parser.SetValue("SV1.3", "UN", nSVCCounter)
                                        oX12Parser.SetValue("SV1.4", "1", nSVCCounter)
                                        oX12Parser.SetValue("SV1.7", oServiceRow("Diagnosis"), nSVCCounter)
                                        oX12Parser.SetValue("SV1.9", "N", nSVCCounter)
                                        nSVCCounter += 1

                                        'DTP DATE - SERVICE DATE(S)
                                        oX12Parser.SetValue("DTP.1", "472", nDTPCounter)
                                        oX12Parser.SetValue("DTP.2", "D8", nDTPCounter)
                                        oX12Parser.SetValue("DTP.3", oServiceRow("ServiceDate"), nDTPCounter)

                                        nDTPCounter += 1

                                    Next    'Service
                                Next    'Claims
                            Next    'Dependent

                        Next    'Subscriber
                    Next    'Provider Info
                    If nSTCounter = 2 Then


                        oX12Parser.SetValue("SE.1", "86", nSTCounter)               'Total number of segments included in a transaction set including ST and SE segments
                        oX12Parser.SetValue("SE.2", oSetRow("ControlNo"), nSTCounter)
                        nSTCounter += 1 'Identifying control number 


                    Else

                        oX12Parser.SetValue("SE.1", "54", nSTCounter)               'Total number of segments included in a transaction set including ST and SE segments
                        oX12Parser.SetValue("SE.2", oSetRow("ControlNo"), nSTCounter)
                        nSTCounter += 1 'Identifying control number 
                    End If
                Next    'SetRow
               

            Next    'GroupRow
        Next    'oInterchangeRow
        oX12Parser.SetValue("GE.1", "2")                'Total Number of Transaction Sets
        oX12Parser.SetValue("GE.2", "121")

        oX12Parser.SetValue("IEA.1", "1")               'Number of Functional Groups GS/GE Pairs in Interchange
        oX12Parser.SetValue("IEA.2", "000000020~")               'Control Number
        Dim sFilePath As String = System.Windows.Forms.Application.StartupPath & "\\834_X098.txt"
        Dim ostreamwritter As System.IO.StreamWriter

        ostreamwritter = System.IO.File.CreateText(sFilePath)
        ostreamwritter.Write(oX12Parser.Message)
        MessageBox.Show(oX12Parser.Message)
        ostreamwritter.Close()

        System.Windows.Forms.MessageBox.Show("OutPut:" & sFilePath)



    End Sub
End Class
