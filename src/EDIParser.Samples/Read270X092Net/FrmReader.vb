Public Class FrmReader

    Private Sub btnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead.Click

        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead("270_X092.txt")
        Dim arMsg() As Byte
        Dim nFileLen As Integer

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

            ElseIf s.Name = "BHT" Then   'Beginning of Hierarchical Transaction

                lstresults.Items.Add("Reference ID: " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                lstresults.Items.Add("Date: " & CType(s.Fields.Item(4), EDIParser.Field).Value)

            ElseIf s.Name = "HL" Then
                sEntity = CType(s.Fields.Item(3), EDIParser.Field).Value

            ElseIf sEntity = "20" Then
                If s.Name = "NM1" Then  'Information Source Name
                    lstresults.Items.Add("Payer Name: " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                    lstresults.Items.Add("Payer ID: " & CType(s.Fields.Item(9), EDIParser.Field).Value)
                End If

            ElseIf sEntity = "21" Then
                If s.Name = "NM1" Then  'Information Source Name
                    lstresults.Items.Add("Provider Lastname: " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                    lstresults.Items.Add("Provider Firstname: " & CType(s.Fields.Item(4), EDIParser.Field).Value)
                    lstresults.Items.Add("Service Provider No: " & CType(s.Fields.Item(9), EDIParser.Field).Value)
                End If


            ElseIf sEntity = "22" Then  'Subscriber Level
                If s.Name = "TRN" Then  'Subscriber Trace Number
                    lstresults.Items.Add("ReferenceID: " & CType(s.Fields.Item(2), EDIParser.Field).Value)
                    lstresults.Items.Add("Company ID: " & CType(s.Fields.Item(3), EDIParser.Field).Value)



                ElseIf s.Name = "NM1" Then
                    sQafr = CType(s.Fields.Item(1), EDIParser.Field).Value
                End If
                If sQafr = "IL" Then
                    If s.Name = "NM1" Then  'Subscriber Name
                        lstresults.Items.Add("Insured Lastname: " & CType(s.Fields.Item(3), EDIParser.Field).Value)
                        lstresults.Items.Add("Insured Firstname: " & CType(s.Fields.Item(4), EDIParser.Field).Value)
                        lstresults.Items.Add("Insured Middle Initial: " & CType(s.Fields.Item(5), EDIParser.Field).Value)

                    ElseIf s.Name = "REF" Then  'Subscriber Additional Identification
                        lstresults.Items.Add("Policy No: " & CType(s.Fields.Item(2), EDIParser.Field).Value)

                    ElseIf s.Name = "N3" Then
                        lstresults.Items.Add("Address: " & CType(s.Fields.Item(1), EDIParser.Field).Value)

                    ElseIf s.Name = "N4" Then
                        lstresults.Items.Add("City:" & CType(s.Fields.Item(1), EDIParser.Field).Value)
                        lstresults.Items.Add("State:" & CType(s.Fields.Item(2), EDIParser.Field).Value)
                        lstresults.Items.Add("Zip:" & CType(s.Fields.Item(3), EDIParser.Field).Value)


                    ElseIf s.Name = "DMG" Then  'Subscriber Demographic Information
                        lstresults.Items.Add("Birthday: " & CType(s.Fields.Item(2), EDIParser.Field).Value)
                        lstresults.Items.Add("Gender: " & CType(s.Fields.Item(3), EDIParser.Field).Value)

                    ElseIf s.Name = "DTP" Then  'Subscriber Date
                        lstresults.Items.Add("Service Date: " & CType(s.Fields.Item(3), EDIParser.Field).Value)

                    ElseIf s.Name = "EQ" Then   'Subscriber Eligibility or Benefit Inquiry Information
                        lstresults.Items.Add("Service Type Code: " & CType(s.Fields.Item(1), EDIParser.Field).Value)
                        lstresults.Items.Add("Coverage Level Code: " & CType(s.Fields.Item(3), EDIParser.Field).Value)


                    End If


                End If


            End If


        Next

        System.Windows.Forms.MessageBox.Show("Finished")


    End Sub
End Class