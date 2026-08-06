Public Class FrmGenerate

    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click

        'This is just an example program to demonstrate how to generate an 270 EDI file
        'in VB .NET using the Framework EDIParser .NET hybrid component
        Dim oX12Parser As New EDIParser.X12Parser()
        ' oX12Parser.SegmentSeparator = "~" & vbCrLf

        'create the interchange (ISA segment)
        oX12Parser.SetValue("ISA.1", "00")               'Authorization Information Qualifier
        oX12Parser.SetValue("ISA.2", "          ")
        oX12Parser.SetValue("ISA.3", "00")               'Security Information Qualifier
        oX12Parser.SetValue("ISA.4", "          ")
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
        oX12Parser.SetValue("GS.1", "HS")             'Functional Identifier Code
        oX12Parser.SetValue("GS.2", "SenderDept")     'Application Sender's Code
        oX12Parser.SetValue("GS.3", "ReceiverDept")   'Application Receiver's Code
        oX12Parser.SetValue("GS.4", "20010821")       'Date
        oX12Parser.SetValue("GS.5", "1548")           'Time
        oX12Parser.SetValue("GS.6", "1")         'Group Control Number
        oX12Parser.SetValue("GS.7", "X")              'Responsible Agency Code
        oX12Parser.SetValue("GS.8", "004010X092") '   'Version / Release / Industry Identifier Code

        'create the ST segment
        oX12Parser.SetValue("ST.1", "270")     'Transaction Set Identifier Code
        oX12Parser.SetValue("ST.2", "1234")   'Transaction Set Control Number

        'create the BHT segment
        oX12Parser.SetValue("BHT.1", "0022")
        oX12Parser.SetValue("BHT.2", "13")
        oX12Parser.SetValue("BHT.3", "10001234")
        oX12Parser.SetValue("BHT.4", "19990501")
        oX12Parser.SetValue("BHT.5", "1319")
       

        Dim nInfoSources As Integer = 1
        Dim nInfoSourceCounter As Integer = 1
        Dim nInfoReceivers As Integer = 1
        Dim nInfoReceiverCounter As Integer = 1
        Dim nSubscribers As Integer = 2
        Dim nSubscriberCounter As Integer = 1

        Dim nHlCounter As Integer = 0
        Dim nHlInfoReceiverParent As Integer
        Dim nHlSubscriberParent As Integer
        Dim nCounter As Integer = 1
        Dim nNM1Counter As Integer = 1
        Dim nN3Counter As Integer = 1
        Dim nTRNCounter As Integer = 1
        Dim nREFCounter As Integer = 1
        Dim nDMGCounter As Integer = 1
        Dim nDTPCounter As Integer = 1
        Dim nEQCounter As Integer = 1



        '*************************************************************************************************
        'DETAIL INFORMATION SOURCE LEVEL
        Do While nInfoSourceCounter <= nInfoSources


            nHlCounter = nHlCounter + 1
            nHlInfoReceiverParent = nHlCounter

            'DETAIL INFO SOURCE LEVEL
            oX12Parser.SetValue("HL.1", nHlCounter, nHlCounter)
            oX12Parser.SetValue("HL.3", "20", nHlCounter)
            oX12Parser.SetValue("HL.4", "1", nHlCounter)


            'INFORMATION SOURCE NAME
            oX12Parser.SetValue("NM1.1", "PR", nNM1Counter)
            oX12Parser.SetValue("NM1.2", "2", nNM1Counter)
            oX12Parser.SetValue("NM1.3", "ABC COMPANY", nNM1Counter)
            oX12Parser.SetValue("NM1.8", "PI", nNM1Counter)
            oX12Parser.SetValue("NM1.9", "842610001", nNM1Counter)

            nNM1Counter += 1
            '*************************************************************************************************
            'DETAIL INFORMATION RECEIVER LEVEL
            Do While nInfoReceiverCounter <= nInfoReceivers

                nHlCounter = nHlCounter + 1
                nHlSubscriberParent = nHlCounter

                'INFORMATION RECEIVER LEVEL
                oX12Parser.SetValue("HL.1", nHlCounter, nHlCounter)
                oX12Parser.SetValue("HL.2", nHlInfoReceiverParent, nHlCounter)
                oX12Parser.SetValue("HL.3", "21", nHlCounter)
                oX12Parser.SetValue("HL.4", "1", nHlCounter)

                'INFORMATION RECEIVER NAME
                oX12Parser.SetValue("NM1.1", "1P", nCounter)
                oX12Parser.SetValue("NM1.2", "1", nCounter)
                oX12Parser.SetValue("NM1.3", "JONES", nCounter)
                oX12Parser.SetValue("NM1.4", "MARCUS", nCounter)
                oX12Parser.SetValue("NM1.8", "SV", nCounter)
                oX12Parser.SetValue("NM1.9", "0202034", nCounter)
                nCounter += 1

                '*************************************************************************************************
                'DETAIL SUBSCRIBER LEVEL
                Do While nSubscriberCounter <= nSubscribers

                    nHlCounter = nHlCounter + 1

                    'SUBSCRIBER LEVEL
                    oX12Parser.SetValue("HL.1", nHlCounter, nHlCounter)
                    oX12Parser.SetValue("HL.2", nHlSubscriberParent, nHlCounter)
                    oX12Parser.SetValue("HL.3", "22", nHlCounter)
                    oX12Parser.SetValue("HL.4", "0", nHlCounter)


                    'SUBSCRIBER TRACE NUMBER
                    oX12Parser.SetValue("TRN.1", "1", nTRNCounter)
                    oX12Parser.SetValue("TRN.2", "93175-012547", nTRNCounter)
                    oX12Parser.SetValue("TRN.3", "9877281234", nTRNCounter)
                    nTRNCounter += 1

                    'SUBSCRIBER NAME
                    oX12Parser.SetValue("NM1.1", "IL", nNM1Counter)
                    oX12Parser.SetValue("NM1.2", "1", nNM1Counter)
                    oX12Parser.SetValue("NM1.3", "SMITH", nNM1Counter)
                    oX12Parser.SetValue("NM1.4", "ROBERT", nNM1Counter)
                    oX12Parser.SetValue("NM1.5", "B", nNM1Counter)
                    oX12Parser.SetValue("NM1.8", "MI", nNM1Counter)
                    oX12Parser.SetValue("NM1.9", "11122333301", nNM1Counter)

                    nNM1Counter += 1


                    'SUBSCRIBER ADDITIONAL IDENTIFICATION
                    oX12Parser.SetValue("REF.1", "1L", nREFCounter)
                    oX12Parser.SetValue("REF.2", "599119", nREFCounter)
                    nREFCounter += 1

                    oX12Parser.SetValue("N3.1", "12345 HIGHWAY ST", nN3Counter)

                    oX12Parser.SetValue("N4.1", "BURBANK", nN3Counter)
                    oX12Parser.SetValue("N4.2", "CA", nN3Counter)
                    oX12Parser.SetValue("N4.3", "12345", nN3Counter)
                    nN3Counter += 1





                    'SUBSCRIBER DEMOGRAPHIC INFORMATION
                    oX12Parser.SetValue("DMG.1", "D8", nDMGCounter)
                    oX12Parser.SetValue("DMG.2", "19430519", nDMGCounter)
                    oX12Parser.SetValue("DMG.3", "M", nDMGCounter)
                    nDMGCounter += 1


                    'SUBSCRIBER DATE

                    oX12Parser.SetValue("DTP.1", "472", nDTPCounter)
                    oX12Parser.SetValue("DTP.2", "D8", nDTPCounter)
                    oX12Parser.SetValue("DTP.3", "19990501", nDTPCounter)
                    nDTPCounter += 1


                    'SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                    oX12Parser.SetValue("EQ.1", "98", nEQCounter)
                    oX12Parser.SetValue("EQ.3", "FAM", nEQCounter)
                    nEQCounter += 1

                    nSubscriberCounter = nSubscriberCounter + 1
                Loop    'nSubscribers

                nInfoReceiverCounter = nInfoReceiverCounter + 1
            Loop    'nInfoReceivers

            nInfoSourceCounter = nInfoSourceCounter + 1
        Loop    'nInfoSources

        oX12Parser.SetValue("SE.1", "25")               'Total number of segments included in a transaction set including ST and SE segments
        oX12Parser.SetValue("SE.2", "1234")             'Identifying control number 

        oX12Parser.SetValue("GE.1", "1")                'Total Number of Transaction Sets
        oX12Parser.SetValue("GE.2", "1")

        oX12Parser.SetValue("IEA.1", "1")               'Number of Functional Groups GS/GE Pairs in Interchange
        oX12Parser.SetValue("IEA.2", "000000020~")               'Control Number


        Dim sFilePath As String = System.Windows.Forms.Application.StartupPath() & "\\270_X092.txt"
        Dim ostreamwritter As System.IO.StreamWriter

        ostreamwritter = System.IO.File.CreateText(sFilePath)
        ostreamwritter.Write(oX12Parser.Message)
        ostreamwritter.Close()
        txtEdiFile.Text = oX12Parser.Message
        System.Windows.Forms.MessageBox.Show("OutPut:" & sFilePath)

    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
End Class