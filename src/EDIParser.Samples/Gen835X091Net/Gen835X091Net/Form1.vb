Public Class Form1

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        'This is just an example program to demonstrate how to generate an 835 EDI file
        'in VB .NET using the Framework EDIParser .NET hybrid component
        Dim oX12Parser As New EDIParser.X12Parser()
        ' oX12Parser.SegmentSeparator = "~" & vbCrLf

        'create the interchange (ISA segment)
        oX12Parser.SetValue("ISA.1", "00")               'Authorization Information Qualifier
        oX12Parser.SetValue("ISA.2", "          ")       'Authorization Information
        oX12Parser.SetValue("ISA.3", "00")               'Security Information Qualifier
        oX12Parser.SetValue("ISA.4", "          ")       'Security Information
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
        oX12Parser.SetValue("GS.1", "HP")             'Functional Identifier Code
        oX12Parser.SetValue("GS.2", "SenderDept")     'Application Sender's Code
        oX12Parser.SetValue("GS.3", "ReceiverDept")   'Application Receiver's Code
        oX12Parser.SetValue("GS.4", "20010821")       'Date
        oX12Parser.SetValue("GS.5", "1548")           'Time
        oX12Parser.SetValue("GS.6", "000001")         'Group Control Number
        oX12Parser.SetValue("GS.7", "X")              'Responsible Agency Code
        oX12Parser.SetValue("GS.8", "004010X091") '   'Version / Release / Industry Identifier Code

        'create the ST segment
        oX12Parser.SetValue("ST.1", "835")     'Transaction Set Identifier Code
        oX12Parser.SetValue("ST.2", "00001")   'Transaction Set Control Number

        'create the BPR segment
        oX12Parser.SetValue("BPR.1", "C")          'Transaction Handling Code
        oX12Parser.SetValue("BPR.2", "150000")     'Monetary Amount
        oX12Parser.SetValue("BPR.3", "C")          'Credit/Debit Flag Code
        oX12Parser.SetValue("BPR.4", "ACH")        'Payment Method Code
        oX12Parser.SetValue("BPR.5", "CTX")        'Payment Format Code
        oX12Parser.SetValue("BPR.6", "01")         '(DFI) ID Number Qualifier
        oX12Parser.SetValue("BPR.7", "999999992")  '(DFI) Identification Number
        oX12Parser.SetValue("BPR.8", "DA")         'Account Number Qualifier
        oX12Parser.SetValue("BPR.9", "123456")     'Account Number
        oX12Parser.SetValue("BPR.10", "512345678 ") 'Originating Company Identifier
        oX12Parser.SetValue("BPR.12", "01")        '(DFI) ID Number Qualifier
        oX12Parser.SetValue("BPR.13", "999988880") '(DFI) Identification Number
        oX12Parser.SetValue("BPR.14", "DA")        'Account Number Qualifier
        oX12Parser.SetValue("BPR.15", "98765")     'Account Number
        oX12Parser.SetValue("BPR.16", "19960913")  'Date

        'create the TRN segment
        oX12Parser.SetValue("TRN.1", "1")          'Trace Type Code
        oX12Parser.SetValue("TRN.2", "98765")      'Reference Identification
        oX12Parser.SetValue("TRN.3", "512345678 ")  'Originating Company Identifier

        'PRODUCTION DATE
        oX12Parser.SetValue("DTM.1", "405", 1)     'Date/Time Qualifier
        oX12Parser.SetValue("DTM.2", "19960913", 1) 'Date

        'PAYER ID INFO
        oX12Parser.SetValue("N1.1", "PR", 1)          'Entity Identifier Code
        oX12Parser.SetValue("N1.2", "Insurance Company of Timbucktu", 1)  'Name

        'create the N3 segment
        oX12Parser.SetValue("N3.1", "1 Main Street")         'Address Information

        'create the N4 segment
        oX12Parser.SetValue("N4.1", "Timbucktu")   'City Name
        oX12Parser.SetValue("N4.2", "AK")          'State or Province Code
        oX12Parser.SetValue("N4.3", "89111")       'Postal Code


        'create the REF segment
        oX12Parser.SetValue("REF.1", "2U")   'Reference Identification Qualifier
        oX12Parser.SetValue("REF.2", "999")  'Reference Identification

        'PAYEE ID INFO
        'create the N1 segment in the second instance of the N1
        oX12Parser.SetValue("N1.1", "PE", 2)         'Entity Identifier Code
        oX12Parser.SetValue("N1.2", "Cybil Mental Hospital", 2) 'Name
        oX12Parser.SetValue("N1.3", "XX", 2)         'Identification Code Qualifier
        oX12Parser.SetValue("N1.4", "6543210903", 2) 'Identification Code

        'INPATIENT PROVIDER SUMMARY INFO
        'create the LX segment
        oX12Parser.SetValue("LX.1", "961221", 1) 'Assigned Number

        'INPATIENT PROVIDER SUMMARY INFO
        'create the TS3 segment
        oX12Parser.SetValue("TS3.1", "6543210903", 1)  'Reference Identification
        oX12Parser.SetValue("TS3.2", "11", 1)          'Facility Code Value
        oX12Parser.SetValue("TS3.3", "19961231", 1)    'Date
        oX12Parser.SetValue("TS3.4", "1", 1)           'Quantity
        oX12Parser.SetValue("TS3.5", "211366.97", 1) 'Monetary Amount
        oX12Parser.SetValue("TS3.6", "138018.40", 1) 'Monetary Amount
        oX12Parser.SetValue("TS3.9", "138018.40", 1) 'Monetary Amount
        oX12Parser.SetValue("TS3.11", "73348.57", 1) 'Monetary Amount

        'PROVIDER SUPPLEMENTAL SUMMARY INFO
        'create the TS2 segment
        oX12Parser.SetValue("TS2.1", "2178.45") 'Monetary Amount
        oX12Parser.SetValue("TS2.2", "1919.71") 'Monetary Amount
        oX12Parser.SetValue("TS2.4", "56.82")   'Monetary Amount
        oX12Parser.SetValue("TS2.5", "197.69")  'Monetary Amount
        oX12Parser.SetValue("TS2.6", "4.23")    'Monetary Amount

        'INPATIENT CLAIM PAYMENT INFO
        'create the CLP segment
        oX12Parser.SetValue("CLP.1", "666123", 1)        'Claim Submitter's Identifier
        oX12Parser.SetValue("CLP.2", "1", 1)             'Claim Status Code
        oX12Parser.SetValue("CLP.3", "211366.97", 1)     'Monetary Amount
        oX12Parser.SetValue("CLP.4", "138018.40", 1)     'Monetary Amount
        oX12Parser.SetValue("CLP.6", "MA", 1)            'Claim Filing Indicator Code
        oX12Parser.SetValue("CLP.7", "1999999444444", 1) 'Reference Identification
        oX12Parser.SetValue("CLP.8", "11", 1)            'Facility Code Value
        oX12Parser.SetValue("CLP.9", "1", 1)             'Claim Frequency Type Code

        'INPATIENT CLAIM ADJUSTMENT
        'create the CAS segment
        oX12Parser.SetValue("CAS.1", "CO", 1)         'Claim Adjustment Group Code
        oX12Parser.SetValue("CAS.2", "A2", 1)         'Claim Adjustment Reason Code
        oX12Parser.SetValue("CAS.3", "73348.57", 1)   'Monetary Amount

        'INPATIENT CLAIM ADJUSTMENT
        'create the NM1 segment
        oX12Parser.SetValue("NM1.1", "QC", 1)           'Entity Identifier Code
        oX12Parser.SetValue("NM1.2", "1", 1)            'Entity Type Qualifier
        oX12Parser.SetValue("NM1.3", "Shepard", 1)      'Name Last or Organization Name
        oX12Parser.SetValue("NM1.4", "Sam", 1)          'Name First
        oX12Parser.SetValue("NM1.5", "O", 1)            'Name Middle
        oX12Parser.SetValue("NM1.8", "HN", 1)           'Identification Code Qualifier
        oX12Parser.SetValue("NM1.9", "666-66-6666A", 1) 'Identification Code


        'INPATIENT ADJUDICATION
        'create the MIA segment
        oX12Parser.SetValue("MIA.1", "0")               'Quantity
        oX12Parser.SetValue("MIA.4", "138018.40")       'Monetary Amount

        'INPATIENT CLAIM DATE
        'create the first instance of a DTP segment
        oX12Parser.SetValue("DTM.1", "232", 2)          'Date/Time Qualifier
        oX12Parser.SetValue("DTM.2", "19960816", 2)     'Date

        'create the second instance of a DTP segment
        oX12Parser.SetValue("DTM.1", "233", 3)          'Date/Time Qualifier
        oX12Parser.SetValue("DTM.2", "19960824", 3)     'Date

        'CLAIM SUPPLEMENTAL INFO QUANTITY
        'create the QTY segment
        oX12Parser.SetValue("QTY.1", "CA")              'Quantity Qualifier
        oX12Parser.SetValue("QTY.2", "8")               'Quantity

        'OUTPATIENT PROVIDER INFO
        'create the LX segment
        oX12Parser.SetValue("LX.1", "961213", 2)        'Assigned Number

        'OUTPATIENT PROVIDER INFO
        'create the TS3 segment
        oX12Parser.SetValue("TS3.1", "6543210903", 2)   'Reference Identification
        oX12Parser.SetValue("TS3.2", "13", 2)           'Facility Code Value
        oX12Parser.SetValue("TS3.3", "19961231", 2)     'Date
        oX12Parser.SetValue("TS3.4", "15000", 2)        'Quantity
        oX12Parser.SetValue("TS3.5", "15000", 2)        'Monetary Amount
        oX12Parser.SetValue("TS3.6", "11980.33", 2)     'Monetary Amount
        oX12Parser.SetValue("TS3.9", "138018.40", 2)    'Monetary Amount
        oX12Parser.SetValue("TS3.11", "3019.67", 2)     'Monetary Amount

        'OUTPATIENT CLAIM PAYMENT INFO
        'create the CLP segment
        oX12Parser.SetValue("CLP.1", "777777", 2)       'Claim Submitter's Identifier
        oX12Parser.SetValue("CLP.2", "1", 2)            'Claim Status Code
        oX12Parser.SetValue("CLP.3", "15000", 2)        'Monetary Amount
        oX12Parser.SetValue("CLP.4", "11980.33", 2)     'Monetary Amount
        oX12Parser.SetValue("CLP.6", "MB", 2)           'Claim Filing Indicator Code
        oX12Parser.SetValue("CLP.7", "1999999444445", 2) 'Reference Identification
        oX12Parser.SetValue("CLP.8", "13", 2)           'Facility Code Value
        oX12Parser.SetValue("CLP.9", "1", 2)            'Claim Frequency Type Code


        'OUTPATIENT CLAIM ADJUSTMENT
        'create the CAS segment
        oX12Parser.SetValue("CAS.1", "CO", 2)           'Claim Adjustment Group Code
        oX12Parser.SetValue("CAS.2", "A2", 2)           'Claim Adjustment Reason Code
        oX12Parser.SetValue("CAS.3", "3019.67", 2)      'Monetary Amount

        'OUTPATIENT NAME
        'create the NM1 segment
        oX12Parser.SetValue("NM1.1", "QC", 2)           'Entity Identifier Code
        oX12Parser.SetValue("NM1.2", "1", 2)            'Entity Type Qualifier
        oX12Parser.SetValue("NM1.3", "Borden", 2)       'Name Last or Organization Name
        oX12Parser.SetValue("NM1.4", "Liz", 2)          'Name First
        oX12Parser.SetValue("NM1.5", "E", 2)            'Name Middle
        oX12Parser.SetValue("NM1.8", "HN", 2)           'Identification Code Qualifier
        oX12Parser.SetValue("NM1.9", Replace("996-66-9999B", "-", ""), 2) 'Identification Code


        'OUTPATIENT ADJUDICATION INFO
        'create the MOA segment
        oX12Parser.SetValue("MOA.3", "MA02")            'Reference Identification

        'OUTPATIENT CLAIM DATE
        'create the DTM segment 
        oX12Parser.SetValue("DTM.1", "232", 4)          'Date/Time Qualifier
        oX12Parser.SetValue("DTM.2", "19960512", 4)     'Date

        'PROVIDER ADJUSTMENT
        'create the PLB segment
        oX12Parser.SetValue("PLB.1", "6543210903")      'Reference Identification
        oX12Parser.SetValue("PLB.2", "19961231")        'Date
        oX12Parser.SetValue("PLB.3", "CV:CP")           'Adjustment Reason Code:Reference Identification
        oX12Parser.SetValue("PLB.4", "1.27")            'Monetary Amount

        oX12Parser.SetValue("SE.1", "28")               'Total number of segments included in a transaction set including ST and SE segments
        oX12Parser.SetValue("SE.2", "00001")             'Identifying control number 

        oX12Parser.SetValue("GE.1", "1")
        oX12Parser.SetValue("GE.2", "000001")                'Total Number of Transaction Sets

        oX12Parser.SetValue("IEA.1", "1")               'Number of Functional Groups GS/GE Pairs in Interchange
        oX12Parser.SetValue("IEA.2", "000000020~")               'Control Number


        Dim sFilePath As String = Application.StartupPath() & "\\835_091.txt"
        Dim ostreamwritter As System.IO.StreamWriter

        ostreamwritter = System.IO.File.CreateText(sFilePath)
        ostreamwritter.Write(oX12Parser.Message)
        ostreamwritter.Close()
        txtEdiString.Text = oX12Parser.Message
        MessageBox.Show("OutPut:" & sFilePath)

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
End Class
