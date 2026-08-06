
Module Module1

    Sub Main()
        While (1)
            Console.Write("View Patient Demographic-(P) or Lab Result-(L)?")
            Dim k As System.ConsoleKeyInfo = Console.ReadKey()
            Console.WriteLine("")

            If UCase(k.KeyChar) = "P" Then
                ReadPatientName()
            ElseIf UCase(k.KeyChar) = "L" Then
                ReadLabOrder()
            Else
                Exit Sub
            End If
        End While

    End Sub

    Private Sub ReadPatientName()
        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead("PatientVisit.txt")
        Dim arMsg() As Byte
        Dim nFileLen As Integer

        nFileLen = strEdi.Length
        ReDim arMsg(nFileLen)
        strEdi.Read(arMsg, 0, nFileLen)
        strEdi.Close()
        Dim sMsg As String
        sMsg = System.Text.Encoding.ASCII.GetString(arMsg)

        Dim hl7parser As EDIParser.HL7Parser = New EDIParser.HL7Parser

        If sMsg.IndexOf(vbCrLf) <> -1 Then
            hl7parser.SegmentSeparator = vbCrLf
        ElseIf sMsg.IndexOf(vbCr) <> -1 Then
            hl7parser.SegmentSeparator = vbCr
        Else
            MsgBox("Unknown Segment separator, customary separator are <cr> or <crlf>.")
            Exit Sub
        End If
        hl7parser.ParseMsg(sMsg)
        Console.WriteLine("Patient Name:" & hl7parser.GetValue("PID.5.1") & ", " & hl7parser.GetValue("PID.5.2"))
    End Sub


    Private Sub ReadLabOrder()
        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead("LabResult.txt")
        Dim arMsg() As Byte
        Dim nFileLen As Integer

        nFileLen = strEdi.Length
        ReDim arMsg(nFileLen)
        strEdi.Read(arMsg, 0, nFileLen)
        strEdi.Close()
        Dim sMsg As String
        sMsg = System.Text.Encoding.ASCII.GetString(arMsg)

        Dim hl7parser As EDIParser.HL7Parser = New EDIParser.HL7Parser
        If sMsg.IndexOf(vbCrLf) <> -1 Then
            hl7parser.SegmentSeparator = vbCrLf
        ElseIf sMsg.IndexOf(vbCr) <> -1 Then
            hl7parser.SegmentSeparator = vbCr
        Else
            MsgBox("Unknown Segment separator, customary separator are <cr> or <crlf>.")
            Exit Sub
        End If
        hl7parser.ParseMsg(sMsg)
        Dim s As EDIParser.Segment
        For Each s In hl7parser.Segments
            Select Case (s.Name)
                Case "PID"
                    Console.WriteLine("Patient Name:" & s.ValueIndexer("PID.5.1") & ", " & s.ValueIndexer("PID.5.2"))
                Case "OBR"
                    Console.WriteLine("Ordering Provider:" & s.ValueIndexer("OBR.16.2") & ", " & s.ValueIndexer("OBR.16.3"))
                Case "OBX"
                    If s.ValueIndexer("OBX.1.1") = "ST" Then
                        Console.WriteLine("Observation Value:" & s.ValueIndexer("OBX.5.1") & ", Units:" & s.ValueIndexer("OBX.6.1"))
                    Else
                        Console.WriteLine("Observation Value:" & s.ValueIndexer("OBX.5.2") & ", Units:" & s.ValueIndexer("OBX.6.1"))
                    End If
            End Select
        Next
    End Sub

End Module
