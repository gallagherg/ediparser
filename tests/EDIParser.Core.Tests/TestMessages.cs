namespace EDIParser.Core.Tests;

internal static class TestMessages
{
    internal const string X12 =
        "ISA*00*          *00*          *ZZ*SENDER         *ZZ*RECEIVER       *260731*1512*U*00401*000000001*0*T*:~" +
        "GS*PO*SENDER*RECEIVER*20260731*1512*1*X*004010~" +
        "ST*850*0001~" +
        "BEG*00*SA*12345**20260731~" +
        "SE*3*0001~" +
        "GE*1*1~" +
        "IEA*1*000000001~";

    internal const string X12WithRepeatedN1 =
        "ISA*00*          *00*          *ZZ*SENDER         *ZZ*RECEIVER       *260731*1512*U*00401*000000001*0*T*:~" +
        "GS*PO*SENDER*RECEIVER*20260731*1512*1*X*004010~" +
        "ST*850*0001~" +
        "N1*ST*FIRST LOCATION~" +
        "N1*BT*SECOND LOCATION~" +
        "SE*4*0001~" +
        "GE*1*1~" +
        "IEA*1*000000001~";

    internal const string Hl7 =
        "MSH|^~\\&|SEND|FAC|RECV|FAC|202607311512||ADT^A01|MSG00001|P|2.5\r" +
        "PID|1||12345^^^MRN||DOE^JOHN||19600101|M\r" +
        "PV1|1|I|WARD^101^1\r";

    internal const string Hl7CrLf =
        "MSH|^~\\&|SEND|FAC|RECV|FAC|202607311512||ADT^A01|MSG00001|P|2.5\r\n" +
        "PID|1||12345^^^MRN||DOE^JOHN||19600101|M\r\n" +
        "PV1|1|I|WARD^101^1\r\n";

    internal const string Hl7ContinueFieldLoop =
        "MSH|^~\\&|SEND|FAC|RECV|FAC|202607311512||ADT^A01|1|P|2.5\r" +
        "PID|1||12345^^^MRN||PLAIN~DOE^JOHN\r";

    internal const string HL7WithRepetitions =
        "MSH|^~\\&|SEND|FAC|REC|FAC|202608051200||ADT^A01|MSG00001|P|2.5\r\n" +
        "PID|1||12345^^^MRN~67890^^^ALT||DOE^JOHN||19600101|M\r\n" +
        "PV1|1|I|WARD^101^1\r\n";

    internal const string Edifact =
        "UNA:+.? '" +
        "UNB+UNOC:3+SENDER+RECEIVER+260731:1512+1'" +
        "UNH+1+ORDERS:D:96A:UN'" +
        "BGM+220+PO123+9'" +
        "UNT+3+1'" +
        "UNZ+1+1'";
}
