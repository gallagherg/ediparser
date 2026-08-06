Module Module1

    Sub Main()
        Dim strEdi As System.IO.Stream = System.IO.File.OpenRead("x12_837.txt")
        Dim arMsg() As Byte
        Dim nFileLen As Integer

        nFileLen = strEdi.Length
        ReDim arMsg(nFileLen)
        strEdi.Read(arMsg, 0, nFileLen)
        strEdi.Close()
        Dim sMsg As String
        sMsg = System.Text.Encoding.ASCII.GetString(arMsg)

        Dim x12parser As EDIParser.X12Parser = New EDIParser.X12Parser
        x12parser.CheckISASeparator = True 'we want to check the ISA segement because the delimiters may be differnet than the default X12Parser delimiters
        x12parser.ParseMsg(sMsg)
        Dim s As EDIParser.Segment
        For Each s In x12parser.Segments
            If s.Name = "NM1" Then
                If CType(s.Fields(1), EDIParser.Field).Value = "41" Then
                    Console.WriteLine("Billing Provider Name:" & CType(s.Fields.Item(3), EDIParser.Field).Value)
                    Exit For
                End If
            End If
        Next
        Console.ReadKey()

    End Sub

End Module
