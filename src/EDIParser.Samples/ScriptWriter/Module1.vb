Module Module1

    Sub Main()
        Dim p As New EDIParser.EdiFactParser()

        p.SetValue("UIB.1.1", "UNOA") 'Syntax identifier
        p.SetValue("UIB.1.2", "0") 'Syntax version number
        p.SetValue("UIB.3.1", "123456") 'Transaction control #
        p.SetValue("UIB.6.1", "DOCOR NCPDP ID") 'Sender identification level one
        p.SetValue("UIB.6.2", "D") 'Sender identification code qualifier
        p.SetValue("UIB.7.1", "PHARMACY NCPDP ID") 'Recipient identification level one
        p.SetValue("UIB.7.2", "P") 'Recipient identification code qualifier
        p.SetValue("UIB.8.1", "20071101") 'Date of the interchange set by sender
        p.SetValue("UIB.8.2", "110000") 'Time of the interchange set by sender

        p.SetValue("UIH.1.1", "SCRIPT") 'Message Type
        p.SetValue("UIH.1.2", "008") 'Message version
        p.SetValue("UIH.1.3", "001") 'Message release
        p.SetValue("UIH.1.4", "NEWRX") 'Message function

        p.SetValue("PVD.1.1", "PC") 'Provider Coded
        p.SetValue("PVD.2.1", "DOCOR NCPDP ID") 'Reference Number
        p.SetValue("PVD.2.2", "D3") 'Reference qualifier
        p.SetValue("PVD.5.1", "Doe") 'Last Name
        p.SetValue("PVD.5.2", "John") 'First Name
        p.SetValue("PVD.5.3", "M") 'Middle Name
        p.SetValue("PVD.5.4", "MD") 'Name Suffix - Jr. III, MD, etc..
        p.SetValue("PVD.5.5", "Dr.") 'Name Prefix - Mr., Ms., Dr., etc.
        p.SetValue("PVD.7.1", "John Doe Clinic") 'Clinic name
        p.SetValue("PVD.8.1", "100 Main St") 'Address Line 1
        p.SetValue("PVD.8.2", "Chicago") 'City
        p.SetValue("PVD.8.3", "IL") 'State
        p.SetValue("PVD.8.4", "60606") 'ZIP Code
        p.SetValue("PVD.9.1", "3121111111") 'Phone Number
        p.SetValue("PVD.9.2", "TE") 'Phone Number Qualifier

        p.SetValue("PVD.1.1", "P2", 2) 'Pharmacy Coded
        p.SetValue("PVD.2.1", "123456", 2) 'Reference Number
        p.SetValue("PVD.2.2", "94", 2) 'Reference qualifier
        p.SetValue("PVD.7.1", "Walgreens", 2) 'Pharmacy name
        p.SetValue("PVD.8.1", "100 Monroe", 2) 'Address Line 1
        p.SetValue("PVD.8.2", "Chicago", 2) 'City   
        p.SetValue("PVD.8.3", "IL", 2) 'State
        p.SetValue("PVD.8.4", "60606", 2) 'ZIP Code
        p.SetValue("PVD.9.1", "3129999999", 2) 'Phone Number
        p.SetValue("PVD.9.2", "TE", 2) 'Phone Number Qualifier

        p.SetValue("PTT.2.1", "19700215") 'Date of Birth
        p.SetValue("PTT.3.1", "Doe") 'Last Name
        p.SetValue("PTT.3.2", "Jane") 'First Name
        p.SetValue("PTT.3.3", "S") 'Middle Name
        p.SetValue("PTT.3.4", "Jr.") 'Name Suffix - Jr. III, MD, etc..
        p.SetValue("PTT.4.1", "F") 'Gender
        p.SetValue("PTT.5.1", "ID-6555") 'Reference Number
        p.SetValue("PTT.5.2", "2U") 'Reference qualifier
        p.SetValue("PTT.6.1", "100 Jackson St") 'Address Line 1
        p.SetValue("PTT.6.2", "Chicago") 'City
        p.SetValue("PTT.6.3", "IL") 'State
        p.SetValue("PTT.6.4", "60606") 'ZIP Code
        p.SetValue("PTT.7.1", "3121234567") 'Phone Number
        p.SetValue("PTT.7.2", "TE") 'Phone Number Qualifier

        p.SetValue("DRU.1.1", "P") 'Item Description Identification
        p.SetValue("DRU.1.2", "Lipitor 10 MG Tablet") 'Medication Description
        p.SetValue("DRU.1.3", "00071015523") 'Product Code
        p.SetValue("DRU.1.4", "ND") 'Product Code Qualifier
        p.SetValue("DRU.1.5", "TAB") 'Dosage Form Code
        p.SetValue("DRU.1.6", "10") 'Drug Strength
        'p.SetValue("DRU.1.7", "P") 'Drug Strength Units Coded
        p.SetValue("DRU.1.8", "47942") 'Drug Database Code
        p.SetValue("DRU.1.9", "MD") 'Drug Database Source
        p.SetValue("DRU.2.1", "EA") 'Quantity Qualifier
        p.SetValue("DRU.2.2", "15") 'Quantity
        p.SetValue("DRU.2.3", "38") 'Original Quantity
        p.SetValue("DRU.3.2", "TAKE 1 TABLET DAILY IN THE MORNING") 'SIG instructions.

        p.SetValue("DRU.4.1", "85") 'Date/Time Period Qualifier (Written Date)
        p.SetValue("DRU.4.2", "20071101") 'Date/Time Period (Written Date)
        p.SetValue("DRU.4.3", "102") 'Date/Time Period Format Qualifier

        p.SetValue("DRU.5.1", "0") 'Product/Service Substitution
        p.SetValue("DRU.6.1", "R") 'Refills
        p.SetValue("DRU.6.2", "2") 'Refill value
        p.SetValue("DRU.7.1", "1") 'Source of diagnosis code
        p.SetValue("DRU.7.2", "100") 'Diagnosis code

        Console.WriteLine(p.Message)
        Console.ReadKey()

    End Sub

End Module
