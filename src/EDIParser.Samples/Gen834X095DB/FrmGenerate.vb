Public Class FrmGenerate

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click

        'This is just an example program to show how to generate an EDI 834 X095 file from a database in VB.NET
        'with Framework EDIParser.Net component

     
   
        Dim sSql As String
        Dim nHlCount As Integer
        Dim sControlNbr As String = ""

        Dim sPath As String = "App_Data/db1.mdb"



        'connection string to an access database
        Dim sConnection As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & sPath

        'create connection to database
        Dim oConnection As New OleDb.OleDbConnection(sConnection)
        oConnection.Open()

        ''Prepare a dataset from the Interchange table
        sSql = "select * from Interchange"
        Dim oAdapter As New OleDb.OleDbDataAdapter(sSql, oConnection)
        Dim oInterchangeDS As New DataSet("dsInterchange")
        Dim oInterchangeRow As DataRow
        oAdapter.Fill(oInterchangeDS, "dsInterchange")

        ''create interchange loop
        Dim oX12Parser As New EDIParser.X12Parser()
        For Each oInterchangeRow In oInterchangeDS.Tables("dsInterchange").Rows

            'CREATE INTERCHANGE

            oX12Parser.SetValue("ISA.1", "00")
            oX12Parser.SetValue("ISA.2", "          ")
            oX12Parser.SetValue("ISA.3", "00")               'Security Information Qualifier
            oX12Parser.SetValue("ISA.4", "          ")
            oX12Parser.SetValue("ISA.5", oInterchangeRow("SenderQlfr")) 'Interchange Sender ID
            oX12Parser.SetValue("ISA.6", oInterchangeRow("SenderID"))               'Interchange ID Qualifier
            oX12Parser.SetValue("ISA.7", oInterchangeRow("ReceiverQlfr"))  'Interchange Receiver ID
            oX12Parser.SetValue("ISA.8", oInterchangeRow("ReceiverID"))           'Interchange Date
            oX12Parser.SetValue("ISA.9", "010821")
            oX12Parser.SetValue("ISA.10", "1548")            'Interchange Time
            oX12Parser.SetValue("ISA.11", "U")               'Interchange Control Standards Identifier
            oX12Parser.SetValue("ISA.12", "00401")           'Interchange Control Version Number
            sControlNbr = oInterchangeRow("ControlNo")
            oX12Parser.SetValue("ISA.13", sControlNbr)       'Interchange Control Number
            oX12Parser.SetValue("ISA.14", "0")               'Acknowledgment Requested
            oX12Parser.SetValue("ISA.15", "T")               'Usage Indicator
            oX12Parser.SetValue("ISA.16", ":")               'Component Element Separato

            'prepare dataset from the FuncGroup table
            sSql = "select * from FuncGroup where InterchangeKey = " & Trim(Str(oInterchangeRow("InterchangeKey")))
            oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
            Dim oGroupDs As New DataSet("dsGroup")
            Dim oGroupRow As DataRow
            oAdapter.Fill(oGroupDs, "dsGroup")

            'create the functional group loop
            Dim nGSCounter As Integer = 1
            Dim nSTCounter As Integer = 1
            Dim nBGNCounter As Integer = 1
            Dim nN1Counter As Integer = 1
            Dim nINSCounter As Integer = 1
            Dim nREFCounter As Integer = 1
            Dim nDTPCounter As Integer = 1
            Dim nNM1Counter As Integer = 1
            Dim nN3Counter As Integer = 1
            Dim nPERCounter As Integer = 1
            Dim nDMGCounter As Integer = 1
            Dim nHGCounter As Integer = 1

            For Each oGroupRow In oGroupDs.Tables("dsGroup").Rows

                'CREATE FUNCTIONAL GROUP

                oX12Parser.SetValue("GS.1", oGroupRow("FuncID"))             'Functional Identifier Code
                oX12Parser.SetValue("GS.2", oInterchangeRow("SenderID").ToString().Trim(), nGSCounter)     'Application Sender's Code
                oX12Parser.SetValue("GS.3", oInterchangeRow("ReceiverID").ToString().Trim(), nGSCounter)  'Application Receiver's Code
                oX12Parser.SetValue("GS.4", "20010821", nGSCounter)       'Date
                oX12Parser.SetValue("GS.5", "1548", nGSCounter)           'Time
                oX12Parser.SetValue("GS.6", oGroupRow("ControlNo"), nGSCounter)         'Group Control Number
                oX12Parser.SetValue("GS.7", "X", nGSCounter)              'Responsible Agency Code
                oX12Parser.SetValue("GS.8", "004010X095", nGSCounter) '   'Version / Release / Industry Identifier Code
                nGSCounter += 1


                'prepare dataset from X098Header table 
                sSql = "select * from X095Header where Groupkey = " & Trim(Str(oGroupRow("Groupkey")))
                oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                Dim oSetDs As New DataSet("dsSet")
                Dim oSetRow As DataRow
                oAdapter.Fill(oSetDs, "dsSet")

                'create the transaction set loop
                For Each oSetRow In oSetDs.Tables("dsSet").Rows

                    'HEADER
                    'ST TRANSACTION SET HEADER
                    oX12Parser.SetValue("ST.1", oSetRow("MessageId"), nSTCounter)     'Transaction Set Identifier Code
                    oX12Parser.SetValue("ST.2", oSetRow("ControlNo"), nSTCounter)   'Transaction Set Control Number
                    nSTCounter += 1

                    'Beginning Segment
                    'create BGN segment
                    oX12Parser.SetValue("BGN.1", oSetRow("PurposeCode"), nBGNCounter)
                    oX12Parser.SetValue("BGN.2", oSetRow("TransactionId"), nBGNCounter)
                    oX12Parser.SetValue("BGN.3", oSetRow("TransactionDate"), nBGNCounter)
                    oX12Parser.SetValue("BGN.4", oSetRow("TransactionTime"), nBGNCounter)
                    oX12Parser.SetValue("BGN.8", oSetRow("ActionCode"), nBGNCounter)
                    nBGNCounter += 1

                    'Plan Sponsor
                    'create N1 segment 
                    oX12Parser.SetValue("N1.1", "P5", nN1Counter)
                    oX12Parser.SetValue("N1.2", oSetRow("SponserName"), nN1Counter)
                    oX12Parser.SetValue("N1.3", "FI", nN1Counter)
                    oX12Parser.SetValue("N1.4", oSetRow("SponserTaxId"), nN1Counter)
                    nN1Counter += 1

                    'Payer
                    'create N1 segment 
                    oX12Parser.SetValue("N1.1", "IN", nN1Counter)
                    oX12Parser.SetValue("N1.2", oSetRow("InsurerName"), nN1Counter)
                    oX12Parser.SetValue("N1.3", "FI", nN1Counter)
                    oX12Parser.SetValue("N1.4", oSetRow("InsurerTaxId"), nN1Counter)
                    nN1Counter += 1


                    'Member Detail
                    sSql = "select * from X095MemberDetail where TSetKey = " & Trim(Str(oSetRow("TSetKey")))
                    oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                    Dim oMemberDetailDs As New DataSet("dsMemberDetail")
                    Dim oMemberDetailRow As DataRow
                    oAdapter.Fill(oMemberDetailDs, "dsMemberDetail")
                    Dim nCounter As Integer = 1
                    Dim nCounterDate As Integer = 1
                    For Each oMemberDetailRow In oMemberDetailDs.Tables("dsMemberDetail").Rows
                        'Member Level Detail
                        'create INS segment 
                        oX12Parser.SetValue("INS.1", oMemberDetailRow("Subscriber"), nINSCounter)
                        oX12Parser.SetValue("INS.2", oMemberDetailRow("Relationship"), nINSCounter)
                        oX12Parser.SetValue("INS.3", "021", nINSCounter)
                        oX12Parser.SetValue("INS.4", "20", nINSCounter)
                        oX12Parser.SetValue("INS.5", oMemberDetailRow("BenefitStatusCode"), nINSCounter)
                        oX12Parser.SetValue("INS.8", "FT", nINSCounter)
                        nINSCounter += 1
                        'Subscriber Number
                        'create REF segment 
                        oX12Parser.SetValue("REF.1", "0F", nREFCounter)
                        oX12Parser.SetValue("REF.2", oMemberDetailRow("SubscriberNo"), nREFCounter)
                        nREFCounter += 1

                        'Member Policy Number
                        'create second instance of REF 
                        oX12Parser.SetValue("REF.1", "1L", nREFCounter)
                        oX12Parser.SetValue("REF.2", oMemberDetailRow("GroupPolicyNo"), nREFCounter)
                        nREFCounter += 1

                        'Member Level Dates
                        'create DTP segment 
                        oX12Parser.SetValue("DTP.1", "356", nDTPCounter)
                        oX12Parser.SetValue("DTP.2", "D8", nDTPCounter)
                        oX12Parser.SetValue("DTP.3", oMemberDetailRow("EligibilityStartDate"), nDTPCounter)
                        nDTPCounter += 1

                        'Member Name
                        'create NM1 segment 
                        oX12Parser.SetValue("NM1.1", "IL", nNM1Counter)
                        oX12Parser.SetValue("NM1.2", "1", nNM1Counter)
                        oX12Parser.SetValue("NM1.3", oMemberDetailRow("Lastname"), nNM1Counter)
                        oX12Parser.SetValue("NM1.4", oMemberDetailRow("Firstname"), nNM1Counter)
                        oX12Parser.SetValue("NM1.8", "34", nNM1Counter)
                        oX12Parser.SetValue("NM1.9", oMemberDetailRow("SSN"), nNM1Counter)
                        nNM1Counter += 1

                        'Member Communications Numbers
                        'create PER segment
                        oX12Parser.SetValue("PER.1", "IP", nPERCounter)
                        oX12Parser.SetValue("PER.3", "HP", nPERCounter)
                        oX12Parser.SetValue("PER.4", oMemberDetailRow("HomePhone"), nPERCounter)
                        oX12Parser.SetValue("PER.5", "WP", nPERCounter)
                        oX12Parser.SetValue("PER.6", oMemberDetailRow("WorkPhone"), nPERCounter)
                        nPERCounter += 1


                        'Member Residence Street Address
                        'create N3 segment 
                        oX12Parser.SetValue("N3.1", oMemberDetailRow("Address"), nN3Counter)


                        'Member Residence City, State, ZIP Code
                        'create N4 segment
                        oX12Parser.SetValue("N4.1", oMemberDetailRow("City"), nPERCounter)
                        oX12Parser.SetValue("N4.2", oMemberDetailRow("State"), nPERCounter)
                        oX12Parser.SetValue("N4.3", oMemberDetailRow("Zip"), nPERCounter)
                        nPERCounter += 1

                        'Member Demographics
                        'create DMG segment 

                        oX12Parser.SetValue("DMG.1", "D8", nDMGCounter)
                        oX12Parser.SetValue("DMG.2", oMemberDetailRow("BirthDate"), nDMGCounter)
                        oX12Parser.SetValue("DMG.3", oMemberDetailRow("GenderCode"), nDMGCounter)
                        nDMGCounter += 1


                        'Member Detail
                        sSql = "select * from X095HealthCoverage where MemberKey = " & Trim(Str(oMemberDetailRow("MemberKey")))
                        oAdapter = New OleDb.OleDbDataAdapter(sSql, oConnection)
                        Dim oHealthCoverageDs As New DataSet("dsHealthCoverage")
                        Dim oHealthCoverageRow As DataRow
                        oAdapter.Fill(oHealthCoverageDs, "dsHealthCoverage")
                        For Each oHealthCoverageRow In oHealthCoverageDs.Tables("dsHealthCoverage").Rows
                            'Health Coverage - Health
                            'create HD segment 
                            oX12Parser.SetValue("HD.1", "021", nHGCounter)
                            oX12Parser.SetValue("HD.3", oHealthCoverageRow("InsuranceCode"), nHGCounter)
                            nHGCounter += 1

                            'Health Coverage Dates
                            'create DTP segment
                            oX12Parser.SetValue("DTP.1", "348", nDTPCounter)
                            oX12Parser.SetValue("DTP.2", "D8", nDTPCounter)
                            oX12Parser.SetValue("DTP.3", oHealthCoverageRow("BenefitBeginDate"), nDTPCounter)
                            nDTPCounter += 1
                        Next

                    Next
                Next
            Next
        Next
        oX12Parser.SetValue("SE.1", "20")               'Total number of segments included in a transaction set including ST and SE segments
        oX12Parser.SetValue("SE.2", "000011234")             'Identifying control number 

        oX12Parser.SetValue("GE.1", "1")                'Total Number of Transaction Sets
        oX12Parser.SetValue("GE.2", "121")

        oX12Parser.SetValue("IEA.1", "1")               'Number of Functional Groups GS/GE Pairs in Interchange
        oX12Parser.SetValue("IEA.2", sControlNbr)               'Control Number


        Dim sFilePath As String = System.Windows.Forms.Application.StartupPath() & "\\834_X095.txt"

        Dim ostreamwritter As System.IO.StreamWriter

        ostreamwritter = System.IO.File.CreateText(sFilePath)
        ostreamwritter.Write(oX12Parser.Message)

        ostreamwritter.Close()

        System.Windows.Forms.MessageBox.Show("OutPut:" & sFilePath)

    End Sub
End Class