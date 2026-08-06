
Module Module1
    Function BuildLabResult() As String
        Dim hl7 As EDIParser.HL7Parser = New EDIParser.HL7Parser()
        hl7.SegmentSeparator = vbCrLf

        hl7.SetValue("MSH.1.1", "|")
        hl7.SetValue("MSH.2.1", "^~\&")
        hl7.SetValue("MSH.3.1", "SENDAPP")
        hl7.SetValue("MSH.4.1", "SENDAPPFAC")
        hl7.SetValue("MSH.5.1", "RECVAPP")
        hl7.SetValue("MSH.6.1", "RECVAPPFAC")
        hl7.SetValue("MSH.7.1", "20070209140300")
        hl7.SetValue("MSH.8.1", "")
        hl7.SetValue("MSH.9.1", "ORU^M01")
        hl7.SetValue("MSH.10.1", "20070209140349")
        hl7.SetValue("MSH.11.1", "P")
        hl7.SetValue("MSH.12.1", "2.3")

        hl7.SetValue("EVN.1.1", "M01") 'admit/visit
        hl7.SetValue("EVN.2.1", "20070209140300")

        hl7.SetValue("PID.2.1", "954") 'external id
        hl7.SetValue("PID.2.4", "ALT964") 'alternative id
        hl7.SetValue("PID.2.5", "MPI") 'master patient index
        hl7.SetValue("PID.3.1", "111111111") 'internal id
        hl7.SetValue("PID.5.1", "JOHN")
        hl7.SetValue("PID.5.2", "DOE")
        hl7.SetValue("PID.5.3", "E")
        hl7.SetValue("PID.7.1", "20010101000000") 'dob
        hl7.SetValue("PID.8.1", "M") 'gender
        hl7.SetValue("PID.11.1", "100 MAIN STREET", 1, 1) ' demostrates repeating address field
        hl7.SetValue("PID.11.3", "GLENS FALLS", 1, 1)
        hl7.SetValue("PID.11.4", "NY", 1, 1)
        hl7.SetValue("PID.11.5.1", "12801", 1, 1)
        hl7.SetValue("PID.11.5.2", "0003", 1, 1)
        hl7.SetValue("PID.11.6", "US", 1, 1)
        hl7.SetValue("PID.11.1", "101 SUNNY STREET", 1, 2)
        hl7.SetValue("PID.11.3", "NAPLES", 1, 2)
        hl7.SetValue("PID.11.4", "FL", 1, 2)
        hl7.SetValue("PID.11.5", "34101", 1, 2)
        hl7.SetValue("PID.11.6", "US", 1, 2)


        hl7.SetValue("PID.18.1", "123456789") 'patient acount number

        hl7.SetValue("PV.2.1", "I")
        hl7.SetValue("PV.3.1", "E7")
        hl7.SetValue("PV.3.2", "703")
        hl7.SetValue("PV.3.4", "LDS")

        hl7.SetValue("OBR.2.2", "A000520")
        hl7.SetValue("OBR.3.1", "LYTES")
        hl7.SetValue("OBR.3.2", "Serum Electrolytes")

        hl7.SetValue("OBX.1.1", "1", 1)
        hl7.SetValue("OBX.2.1", "NM", 1)
        hl7.SetValue("OBX.3.1", "NAS", 1)
        hl7.SetValue("OBX.3.2", "Serum Sodium", 1)
        hl7.SetValue("OBX.4.1", "1", 1)
        hl7.SetValue("OBX.5.1", "138", 1)
        hl7.SetValue("OBX.6.1", "mmol/L", 1)

        hl7.SetValue("OBX.1.1", "2", 2)
        hl7.SetValue("OBX.2.1", "NM", 2)
        hl7.SetValue("OBX.3.1", "K", 2)
        hl7.SetValue("OBX.3.2", "Serum Potassium", 2)
        hl7.SetValue("OBX.4.1", "1", 2)
        hl7.SetValue("OBX.5.1", "3.2", 2)
        hl7.SetValue("OBX.6.1", "mmol/L", 2)

        hl7.SetValue("OBX.1.1", "3", 3)
        hl7.SetValue("OBX.2.1", "NM", 3)
        hl7.SetValue("OBX.3.1", "CL", 3)
        hl7.SetValue("OBX.3.2", "Serum Chloride", 3)
        hl7.SetValue("OBX.4.1", "1", 3)
        hl7.SetValue("OBX.5.1", "114", 3)
        hl7.SetValue("OBX.6.1", "mmol/L", 3)

        hl7.SetValue("OBX.1.1", "4", 4)
        hl7.SetValue("OBX.2.1", "NM", 4)
        hl7.SetValue("OBX.3.1", "CO2", 4)
        hl7.SetValue("OBX.3.2", "Serum CO2", 4)
        hl7.SetValue("OBX.4.1", "1", 4)
        hl7.SetValue("OBX.5.1", "24", 4)
        hl7.SetValue("OBX.6.1", "mmol/L", 4)

        BuildLabResult = hl7.Message

    End Function
    Function BuildPatientDemographic()
        Dim hl7 As EDIParser.HL7Parser = New EDIParser.HL7Parser()
        hl7.SegmentSeparator = vbCrLf

        hl7.SetValue("MSH.1.1", "|")
        hl7.SetValue("MSH.2.1", "^~\&")
        hl7.SetValue("MSH.3.1", "SENDAPP")
        hl7.SetValue("MSH.4.1", "SENDAPPFAC")
        hl7.SetValue("MSH.5.1", "RECVAPP")
        hl7.SetValue("MSH.6.1", "RECVAPPFAC")
        hl7.SetValue("MSH.7.1", "20070209140300")
        hl7.SetValue("MSH.8.1", "")
        hl7.SetValue("MSH.9.1", "ADT^A01")
        hl7.SetValue("MSH.10.1", "20070209140349")
        hl7.SetValue("MSH.11.1", "P")
        hl7.SetValue("MSH.12.1", "2.3")

        hl7.SetValue("EVN.1.1", "A01") 'admit/visit
        hl7.SetValue("EVN.2.1", "20070209140300")

        hl7.SetValue("PID.2.1", "954") 'external id
        hl7.SetValue("PID.2.4", "ALT964") 'alternative id
        hl7.SetValue("PID.2.5", "MPI") 'master patient index
        hl7.SetValue("PID.3.1", "111111111") 'internal id
        hl7.SetValue("PID.5.1", "JOHN")
        hl7.SetValue("PID.5.2", "DOE")
        hl7.SetValue("PID.5.3", "E")
        hl7.SetValue("PID.7.1", "20010101000000") 'dob
        hl7.SetValue("PID.8.1", "M") 'gender
        hl7.SetValue("PID.11.1", "100 MAIN STREET")
        hl7.SetValue("PID.11.3", "GLENS FALLS")
        hl7.SetValue("PID.11.4", "NY")
        hl7.SetValue("PID.11.5", "12801")
        hl7.SetValue("PID.11.6", "US")
        hl7.SetValue("PID.18.1", "123456789") 'patient acount number

        BuildPatientDemographic = hl7.Message

    End Function
    Sub Main()

        While (1)
            Console.Write("View Patient Demographic-(P) or Lab Result-(L)?")
            Dim k As System.ConsoleKeyInfo = Console.ReadKey()
            If UCase(k.KeyChar) = "P" Then
                Console.WriteLine(BuildPatientDemographic)
            ElseIf UCase(k.KeyChar) = "L" Then
                Console.WriteLine(BuildLabResult)
            Else
                Exit Sub
            End If
        End While
    End Sub
End Module
