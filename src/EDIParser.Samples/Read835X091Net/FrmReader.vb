Public Class FrmReader

    Private Sub BtnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRead.Click
        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead("835_091.txt")
        Dim arMsg() As Byte
        Dim nFileLen As Integer

        Dim sValue As String
        Dim sEntity As String = String.Empty
        Dim sLXID As String = String.Empty
        nFileLen = strEdi.Length
        ReDim arMsg(nFileLen)
        strEdi.Read(arMsg, 0, nFileLen)
        strEdi.Close()
        Dim sMsg As String
        sMsg = System.Text.Encoding.ASCII.GetString(arMsg)

        Dim x12parser As EDIParser.X12Parser = New EDIParser.X12Parser
        x12parser.ParseMsg(sMsg)
        Dim s As EDIParser.Segment
        For Each s In x12parser.Segments

            If s.Name = "ISA" Then

                sValue = CType(s.Fields.Item(1), EDIParser.Field).Value      'Authorization Information Qualifier
                sValue = CType(s.Fields.Item(2), EDIParser.Field).Value      'Authorization Information
                sValue = CType(s.Fields.Item(3), EDIParser.Field).Value      'Security Information Qualifier
                sValue = CType(s.Fields.Item(4), EDIParser.Field).Value      'Security Information
                sValue = CType(s.Fields.Item(5), EDIParser.Field).Value      'Interchange ID Qualifier
                sValue = CType(s.Fields.Item(6), EDIParser.Field).Value      'Interchange Sender ID
                sValue = CType(s.Fields.Item(7), EDIParser.Field).Value      'Interchange ID Qualifier
                sValue = CType(s.Fields.Item(8), EDIParser.Field).Value      'Interchange Receiver ID
                sValue = CType(s.Fields.Item(9), EDIParser.Field).Value      'Interchange Date
                sValue = CType(s.Fields.Item(10), EDIParser.Field).Value     'Interchange Time
                sValue = CType(s.Fields.Item(11), EDIParser.Field).Value     'Interchange Control Standards Identifier
                sValue = CType(s.Fields.Item(12), EDIParser.Field).Value     'Interchange Control Version Number
                sValue = CType(s.Fields.Item(13), EDIParser.Field).Value     'Interchange Control Number
                sValue = CType(s.Fields.Item(14), EDIParser.Field).Value     'Acknowledgment Requested
                sValue = CType(s.Fields.Item(15), EDIParser.Field).Value     'Usage Indicator
                sValue = CType(s.Fields.Item(16), EDIParser.Field).Value     'Component Element Separator

            ElseIf s.Name = "GS" Then

                sValue = CType(s.Fields.Item(1), EDIParser.Field).Value       'Functional Identifier Code
                sValue = CType(s.Fields.Item(2), EDIParser.Field).Value      'Application Sender's Code
                sValue = CType(s.Fields.Item(3), EDIParser.Field).Value      'Application Receiver's Code
                sValue = CType(s.Fields.Item(4), EDIParser.Field).Value      'Date
                sValue = CType(s.Fields.Item(5), EDIParser.Field).Value      'Time
                sValue = CType(s.Fields.Item(6), EDIParser.Field).Value       'Group Control Number
                sValue = CType(s.Fields.Item(7), EDIParser.Field).Value      'Responsible Agency Code
                sValue = CType(s.Fields.Item(8), EDIParser.Field).Value      'Version / Release / Industry Identifier Code

            ElseIf s.Name = "ST" Then

                sValue = CType(s.Fields.Item(1), EDIParser.Field).Value    'Transaction Set Identifier Code
                sValue = CType(s.Fields.Item(2), EDIParser.Field).Value    'Transaction Set Control Number

            ElseIf s.Name = "BPR" Then

                If CType(s.Fields.Item(1), EDIParser.Field).Value = "C" Then

                    sValue = CType(s.Fields.Item(2), EDIParser.Field).Value    'Monetary Amount
                    sValue = CType(s.Fields.Item(3), EDIParser.Field).Value     'Credit/Debit Flag Code
                    sValue = CType(s.Fields.Item(4), EDIParser.Field).Value     'Payment Method Code
                    sValue = CType(s.Fields.Item(5), EDIParser.Field).Value     'Payment Format Code
                    sValue = CType(s.Fields.Item(6), EDIParser.Field).Value    '(DFI) ID Number Qualifier
                    lstresults.Items.Add("BankFromABA:  " & CType(s.Fields.Item(7), EDIParser.Field).Value)     '(DFI) Identification Number
                    sValue = CType(s.Fields.Item(8), EDIParser.Field).Value     'Account Number Qualifier
                    lstresults.Items.Add("BankFromAccountNo:  " & CType(s.Fields.Item(9), EDIParser.Field).Value)     'Account Number
                    lstresults.Items.Add("InsFedTaxID:  " & CType(s.Fields.Item(10), EDIParser.Field).Value)     'Originating Company Identifier
                    sValue = CType(s.Fields.Item(11), EDIParser.Field).Value     'Originating Company Supplemental Code
                    sValue = CType(s.Fields.Item(12), EDIParser.Field).Value     '(DFI) ID Number Qualifier
                    lstresults.Items.Add("BankToABA:  " & CType(s.Fields.Item(13), EDIParser.Field).Value)     '(DFI) Identification Number
                    sValue = CType(s.Fields.Item(14), EDIParser.Field).Value     'Account Number Qualifier
                    lstresults.Items.Add("BankToAccountNo:  " & CType(s.Fields.Item(15), EDIParser.Field).Value)     'Account Number
                    lstresults.Items.Add("TransferDate:  " & CType(s.Fields.Item(16), EDIParser.Field).Value)     'Date

                End If

            ElseIf s.Name = "TRN" Then

                If CType(s.Fields.Item(1), EDIParser.Field).Value = "1" Then
                    lstresults.Items.Add("BankFromAccountNo:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)
                    lstresults.Items.Add("InsFedTaxID:  " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                End If


            ElseIf s.Name = "DTM" Then
                If CType(s.Fields.Item(1), EDIParser.Field).Value = "405" Then  'Date/Time Qualifier
                    lstresults.Items.Add("TransferDate:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)
             
                End If

                If sLXID = "961221" Then

                    If CType(s.Fields.Item(1), EDIParser.Field).Value = "232" Then
                        lstresults.Items.Add("InHospFrom:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)

                    ElseIf CType(s.Fields.Item(1), EDIParser.Field).Value = "233" Then
                        lstresults.Items.Add("InHospTo:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)

                        sLXID = String.Empty
                    End If

                ElseIf sLXID = "961213" Then
                    sLXID = String.Empty
                    lstresults.Items.Add("OutServiceDate:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)

                End If
            ElseIf s.Name = "N1" Then

                sEntity = CType(s.Fields.Item(1), EDIParser.Field).Value 'get loop entity qualifier to identity each N1 loop instances

                If sEntity = "PR" Then
                    sValue = CType(s.Fields.Item(1), EDIParser.Field).Value  ' Entity Identifier Code (98) 
                    lstresults.Items.Add("InsName:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)   ' Name (93) 
                ElseIf sEntity = "PE" Then
                    sValue = CType(s.Fields.Item(1), EDIParser.Field).Value   ' Entity Identifier Code (98) 
                    lstresults.Items.Add("HospName:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)   ' Name (93) 
                    sValue = CType(s.Fields.Item(3), EDIParser.Field).Value   ' Identification Code Qualifier (66) 
                    lstresults.Items.Add("HospProviderNo:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)   ' Identification Code (67)
                End If

            ElseIf sEntity = "PR" Then

                If s.Name = "N3" Then
                    lstresults.Items.Add("InsAddr:  " & CType(s.Fields.Item(1), EDIParser.Field).Value)   ' Address Information (166) 
                ElseIf s.Name = "N4" Then
                    lstresults.Items.Add("InsCity:  " & CType(s.Fields.Item(1), EDIParser.Field).Value)  ' City Name (19) 
                    lstresults.Items.Add("InsState:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)   ' State or Province Code (156) 
                    lstresults.Items.Add("InsZip:  " & CType(s.Fields.Item(3), EDIParser.Field).Value)  ' Postal Code (116) 
                ElseIf s.Name = "REF" Then

                    If CType(s.Fields.Item(1), EDIParser.Field).Value = "2U" Then
                        lstresults.Items.Add("InsMedIntID:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)  ' Reference Identification (127) 
                    End If

                End If
            ElseIf s.Name = "LX" Then

                sLXID = CType(s.Fields.Item(1), EDIParser.Field).Value

            ElseIf sLXID = "961221" Then

                If s.Name = "TS3" Then
                    lstresults.Items.Add("HospProviderNo (LX):  " & CType(s.Fields.Item(1), EDIParser.Field).Value)
                    lstresults.Items.Add("InFacilityType:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)
                    lstresults.Items.Add("InpatientClaim:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)
                    lstresults.Items.Add("InTotalCharges:  " & CType(s.Fields.Item(5), EDIParser.Field).Value)
                    lstresults.Items.Add("InPaidAmount:  " & CType(s.Fields.Item(9), EDIParser.Field).Value)
                    lstresults.Items.Add("InAdjustment:  " & CType(s.Fields.Item(11), EDIParser.Field).Value)
                ElseIf s.Name = "TS2" Then
                    lstresults.Items.Add("DiagRelatedGroupAmnt:  " & CType(s.Fields.Item(1), EDIParser.Field).Value)
                    lstresults.Items.Add("FedSpecAmnt:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)
                    lstresults.Items.Add("DisproportionShareAmnt:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)
                    lstresults.Items.Add("CapitalAmnt:  " & CType(s.Fields.Item(5), EDIParser.Field).Value)
                    lstresults.Items.Add("IndirectMedEduAmnt:  " & CType(s.Fields.Item(6), EDIParser.Field).Value)
                ElseIf s.Name = "CLP" Then
                    lstresults.Items.Add("InSubmitter:  " & CType(s.Fields.Item(1), EDIParser.Field).Value)   ' Claim Submitter's Identifier (1028) 
                    sValue = CType(s.Fields.Item(2), EDIParser.Field).Value   ' Claim Status Code (1029) 
                    lstresults.Items.Add("InTotalCharges:  " & CType(s.Fields.Item(3), EDIParser.Field).Value)   ' Monetary Amount (782) 
                    lstresults.Items.Add("InPaidAmount:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)   ' Monetary Amount (782) 
                    sValue = CType(s.Fields.Item(5), EDIParser.Field).Value   ' Monetary Amount (782) 
                    sValue = CType(s.Fields.Item(6), EDIParser.Field).Value   ' Claim Filing Indicator Code (1032) 
                    lstresults.Items.Add("PayerClaimControlNo:  " & CType(s.Fields.Item(7), EDIParser.Field).Value)   ' Reference Identification (127) 
                    lstresults.Items.Add("InFacilityType:  " & CType(s.Fields.Item(8), EDIParser.Field).Value)   ' Facility Code Value (1331) 
                    lstresults.Items.Add("InpatientClaim:  " & CType(s.Fields.Item(9), EDIParser.Field).Value)   ' Claim Frequency Type Code (1325) 
                    'sValue = CType(s.Fields.Item(10), EDIParser.Field).Value   ' Patient Status Code (1352) 
                    'sValue = CType(s.Fields.Item(11), EDIParser.Field).Value   ' Diagnosis Related Group (DRG) Code (1354) 
                    'sValue = CType(s.Fields.Item(12), EDIParser.Field).Value   ' Quantity (380) 
                    'sValue = CType(s.Fields.Item(13), EDIParser.Field).Value   ' Percent (954) 
                ElseIf s.Name = "CAS" Then

                    If CType(s.Fields.Item(1), EDIParser.Field).Value = "CO" Then
                        sValue = CType(s.Fields.Item(2), EDIParser.Field).Value   ' Claim Adjustment Reason Code (1034) 
                        lstresults.Items.Add("InAdjustment:  " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                        'sValue = CType(s.Fields.Item(4), EDIParser.Field).Value   ' Quantity (380) 
                    End If

                ElseIf s.Name = "NM1" Then
                    sValue = CType(s.Fields.Item(1), EDIParser.Field).Value   ' Entity Identifier Code (98) 
                    sValue = CType(s.Fields.Item(2), EDIParser.Field).Value  ' Entity Type Qualifier (1065) 
                    lstresults.Items.Add("InLastname:  " & CType(s.Fields.Item(3), EDIParser.Field).Value)   ' Name Last or Organization Name (1035) 
                    lstresults.Items.Add("InFirstname:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)   ' Name First (1036) 
                    lstresults.Items.Add("InMiddlename:  " & CType(s.Fields.Item(5), EDIParser.Field).Value)   ' Name Middle (1037) 
                    sValue = CType(s.Fields.Item(6), EDIParser.Field).Value   ' Name Prefix (1038) 
                    sValue = CType(s.Fields.Item(7), EDIParser.Field).Value  ' Name Suffix (1039) 
                    sValue = CType(s.Fields.Item(8), EDIParser.Field).Value  ' Identification Code Qualifier (66) 
                    lstresults.Items.Add("InHIC:  " & CType(s.Fields.Item(9), EDIParser.Field).Value)   ' Identification Code (67) 
                    ' sValue = CType(s.Fields.Item(10), EDIParser.Field).Value   ' Entity Relationship Code (706) 
                    ' sValue = CType(s.Fields.Item(11), EDIParser.Field).Value   ' Entity Identifier Code (98) 
                ElseIf s.Name = "MIA" Then

                    If Val(CType(s.Fields.Item(1), EDIParser.Field).Value) = 0 Then
                        lstresults.Items.Add("InPaidAmount:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)
                    End If

                End If

            ElseIf sLXID = "961213" Then

                If s.Name = "TS3" Then
                    lstresults.Items.Add("HospProviderNo (TS3):  " & CType(s.Fields.Item(1), EDIParser.Field).Value)
                    lstresults.Items.Add("OutFacilityType:  " & CType(s.Fields.Item(2), EDIParser.Field).Value)
                    lstresults.Items.Add("OutTotalCharges:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)
                    lstresults.Items.Add("OutTotalCharges:  " & CType(s.Fields.Item(5), EDIParser.Field).Value)
                    lstresults.Items.Add("OutPaidAmount:  " & CType(s.Fields.Item(6), EDIParser.Field).Value)
                    lstresults.Items.Add("InPaidAmount:  " & CType(s.Fields.Item(9), EDIParser.Field).Value)
                    lstresults.Items.Add("OutAdjustment:  " & CType(s.Fields.Item(11), EDIParser.Field).Value)
                ElseIf s.Name = "CLP" Then
                    lstresults.Items.Add("OutSubmitter:  " & CType(s.Fields.Item(1), EDIParser.Field).Value)
                    lstresults.Items.Add("OutTotalCharges:  " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                    lstresults.Items.Add("OutPaidAmount:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)
                    lstresults.Items.Add("OutFacilityType:  " & CType(s.Fields.Item(8), EDIParser.Field).Value)
                    lstresults.Items.Add("OutpatientClaim:  " & CType(s.Fields.Item(9), EDIParser.Field).Value)
                ElseIf s.Name = "CAS" Then

                    If CType(s.Fields.Item(1), EDIParser.Field).Value = "CO" Then
                        lstresults.Items.Add("OutAdjustment:  " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                    End If

                ElseIf s.Name = "NM1" Then
                    lstresults.Items.Add("OutLastname:  " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                    lstresults.Items.Add("OutFirstname:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)
                    lstresults.Items.Add("OutMiddlename:  " & CType(s.Fields.Item(5), EDIParser.Field).Value)

                    If CType(s.Fields.Item(8), EDIParser.Field).Value = "HN" Then
                        lstresults.Items.Add("OutHIC:  " & CType(s.Fields.Item(9), EDIParser.Field).Value)
                    End If

                ElseIf s.Name = "MIA" Then

                    If Val(CType(s.Fields.Item(1), EDIParser.Field).Value) = 0 Then
                        lstresults.Items.Add("InPaidAmount:  " & CType(s.Fields.Item(7), EDIParser.Field).Value)
                    End If

                End If

            ElseIf s.Name = "PLB" Then
                sValue = CType(s.Fields.Item(1), EDIParser.Field).Value  ' Reference Identification (127) 
                lstresults.Items.Add("HospProviderNo (PLB):  " & CType(s.Fields.Item(2), EDIParser.Field).Value)   ' Date (373) 
                sValue = CType(s.Fields.Item(3), EDIParser.Field).Value   ' Adjustment Reason Code (426) 
                sValue = CType(s.Fields.Item(3), EDIParser.Field).Value   ' Reference Identification (127) 
                lstresults.Items.Add("CapitalPassThru:  " & CType(s.Fields.Item(4), EDIParser.Field).Value)   ' Monetary Amount (782) 
              
            End If

        Next

        System.Windows.Forms.MessageBox.Show("Finished")

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class