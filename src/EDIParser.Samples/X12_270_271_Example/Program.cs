using System;
using System.Collections.Generic;
using System.Text;

namespace X12_270_271_Example
{
    class Program
    {
        private static string buildX12_270()
        {

            EDIParser.X12Parser x12_270 = new EDIParser.X12Parser();

            x12_270.SetValue("ISA.1", "00");  //No Authorization Information Present
            x12_270.SetValue("ISA.3", "00");  //No Security Information Present 
            x12_270.SetValue("ISA.5", "ZZ");  //Mutually Defined
            x12_270.SetValue("ISA.6", "SENDER"); //EDI assigned Submitter ID at the time of enrollment.
            x12_270.SetValue("ISA.7", "ZZ"); //ZZ - Mutually Defined
            x12_270.SetValue("ISA.8", "RECEIVER"); //Receiver ID of the state receiving the 270 inquiry
            x12_270.SetValue("ISA.9", "090731"); //Valid date in YYMMDD format
            x12_270.SetValue("ISA.10", "1301"); //Valid time in HHMM format
            x12_270.SetValue("ISA.11", "U"); //U - U.S. EDI Community of ASC X12
            x12_270.SetValue("ISA.12", "00401"); //Draft Standards
            x12_270.SetValue("ISA.13", "000000001"); //Unique control number greater than zero 
            x12_270.SetValue("ISA.14", "1"); //Acknowledgment Requested
            x12_270.SetValue("ISA.15", "P"); //P - Production Data

            x12_270.SetValue("GS.1", "HS"); //Eligibility
            x12_270.SetValue("GS.6", "1");  //Message/control number assigned by sender
            x12_270.SetValue("GS.7", "X"); //Accredited Standards Committee
            x12_270.SetValue("GS.8", "004010X092A1");

            x12_270.SetValue("ST.1", "270"); //270 - Eligibility
            x12_270.SetValue("ST.2", "0001"); //Transaction Set - Control Number, Unique value

            x12_270.SetValue("BHT.1", "0022"); //0022 - Information Source, Information Receiver, Subscriber, Dependent
            x12_270.SetValue("BHT.2", "13"); //13 - Request

            x12_270.SetValue("HL.1", "1", 1); //Hierarchical ID Number
            x12_270.SetValue("HL.3", "20", 1); //Information Source Identifies the payer
            x12_270.SetValue("HL.4", "1", 1); //Additional Subordinate HL Data Segment in this Hierarchical Structure

            x12_270.SetValue("NM1.1", "2B", 1); //Third Party Administrator
            x12_270.SetValue("NM1.2", "2", 1); //2 - Nonperson Entity
            x12_270.SetValue("NM1.3", "TRADINGPARTNER", 1); //Corresponds to Receiver/Sender Name
            x12_270.SetValue("NM1.8", "DP", 1); //Code designating the system/method of code structure used for Identification Code (Data Provider)
            x12_270.SetValue("NM1.9", "TRADREQ", 1); //Information Source Identifier (Receiver ID)

            x12_270.SetValue("HL.1", "2", 2); //Hierarchical ID Number
            x12_270.SetValue("HL.2", "1", 2); //Indicating that this HL loop is subordinate to the first HL loop
            x12_270.SetValue("HL.3", "21", 2); //Information receiver 
            x12_270.SetValue("HL.4", "1", 2); //Additional Subordinate HL Data Segment in This Hierarchical Structure

            x12_270.SetValue("NM1.1", "GP", 2); //Information Source Name, GP - Gateway Provider
            x12_270.SetValue("NM1.2", "1", 2);   //Person
            x12_270.SetValue("NM1.3", "Doe", 2); //Lastname
            x12_270.SetValue("NM1.4", "Dean", 2); //Firstname
            x12_270.SetValue("NM1.8", "XX", 2);  //Code designating the system/method of code structure used fo
            x12_270.SetValue("NM1.9", "XXXXXXXX", 2); //Code identifying a party or other code

            x12_270.SetValue("REF.1", "EO"); //Submitter Identification Number
            x12_270.SetValue("REF.2", "4689517444002"); //Reference information as defined for a particular Transaction Set
            x12_270.SetValue("REF.3", "MyCompany"); // my company name

            x12_270.SetValue("N3.1", "123 Main Street", 1); //company street address 1

            x12_270.SetValue("N4.1", "Chicago", 1); //company city
            x12_270.SetValue("N4.2", "IL", 1); //company state
            x12_270.SetValue("N4.3", "60606", 1); //company zip

            x12_270.SetValue("HL.1", "3", 3); //Hierarchical ID Number
            x12_270.SetValue("HL.2", "2", 3); //2 - Indicating that this HL loop is subordinate to the second HL loop
            x12_270.SetValue("HL.3", "22", 3); //22- Subscriber identifies the employee or group member who is covered for the insurance
            x12_270.SetValue("HL.4", "0", 3); //0 - No Subordinate HL Segment in This Hierarchical Structure

            x12_270.SetValue("NM1.1", "IL", 3); //Insured
            x12_270.SetValue("NM1.2", "1", 3); //1 - Person
            x12_270.SetValue("NM1.3", "Doe", 3); //Last Name
            x12_270.SetValue("NM1.4", "Douglas", 3);//First Name
            x12_270.SetValue("NM1.8", "ZZ", 3); //ZZ- Mutually defined
            x12_270.SetValue("NM1.9", "813", 3); //Subscriber Primary Identifier

            x12_270.SetValue("N3.1", "123 Trumble Dr", 2); //Person street address 1
            x12_270.SetValue("N3.2", "Apt. 101", 2); //Person street address 2

            x12_270.SetValue("N4.1", "Chicago", 2); //Person city
            x12_270.SetValue("N4.2", "IL", 2); //Person state
            x12_270.SetValue("N4.3", "01040", 2); //Person zipcode

            x12_270.SetValue("DMG.1", "D8"); //D8 - Date Expressed in Format CCYYMMDD
            x12_270.SetValue("DMG.2", "19680901"); //Person birth date
            x12_270.SetValue("DMG.3", "M"); //Person gender

            x12_270.SetValue("DTP.1", "307"); //Eligibility
            x12_270.SetValue("DTP.2", "D8");// D8 - Date Expressed in Format CCYYMMDD
            x12_270.SetValue("DTP.3", "20090731"); //Expression of a date

            x12_270.SetValue("EQ.1", "88"); //88 = Pharmacy

            x12_270.SetValue("SE.1", "17"); //Total number of segments included in a transaction set including ST and SE segments
            x12_270.SetValue("SE.2", "0001"); //Identifying control number 

            x12_270.SetValue("GE.1", "1"); //Total Number of Transaction Sets

            x12_270.SetValue("IEA.1", "1"); //Number of Functional Groups GS/GE Pairs in Interchange
            x12_270.SetValue("IEA.2", "000497182"); //Control Number
            return x12_270.Message(); 
        }
        private static string buildX12_271()
        {
            EDIParser.X12Parser x12_271 = new EDIParser.X12Parser();


            x12_271.SetValue("ISA.1", "00"); //No Authorization Information Present
            x12_271.SetValue("ISA.5", "DP"); //Data Provider (Trading Partner Response ID
            x12_271.SetValue("ISA.6", "TRADRES"); //EDI assigned Submitter ID at the time of enrollment.
            x12_271.SetValue("ISA.7", "ZZ"); //ZZ - Mutually Defined
            x12_271.SetValue("ISA.8", "RECEIVER"); //Receiver ID of the state receiving the 271 inquiry
            x12_271.SetValue("ISA.9", "20061003"); //Valid date in YYMMDD format
            x12_271.SetValue("ISA.10", "1558"); //Valid time in HHMM format
            x12_271.SetValue("ISA.11", "U");  //U - U.S. EDI Community of ASC X12
            x12_271.SetValue("ISA.12", "00401"); //Draft Standards
            x12_271.SetValue("ISA.13", "000000001"); //Unique control number greater than zero 
            x12_271.SetValue("ISA.14", "0"); //No Acknowledgment Requested
            x12_271.SetValue("ISA.15", "P"); //P - Production Data

            x12_271.SetValue("GS.1", "HB"); //Eligibility, Coverage or Benefit Information (271)
            x12_271.SetValue("GS.6", "83762"); //Message/control number assigned by sender
            x12_271.SetValue("GS.7", "X");  //Accredited Standards Committee
            x12_271.SetValue("GS.8", "004010X092A1");

            x12_271.SetValue("ST.1", "271"); //271 – Eligibility, Coverage or Benefit Response
            x12_271.SetValue("ST.2", "0001"); //Transaction Set - Control Number, Unique value

            x12_271.SetValue("BHT.1", "0022"); //0022 - Information Source, Information Receiver, Subscriber, Dependent
            x12_271.SetValue("BHT.2", "11"); //11 -  Response ( for 271)
            x12_271.SetValue("BHT.3", "1"); //Reference information as defined for a particular Transaction Set


            x12_271.SetValue("AAA.1", "Y"); //Code indicating a Yes or No condition or response

            x12_271.SetValue("HL.1", "1", 1); //Hierarchical ID Number
            x12_271.SetValue("HL.3", "20", 1); //Information Source Identifies the payer
            x12_271.SetValue("HL.4", "1", 1); //Additional Subordinate HL Data Segment in this Hierarchical Structure

            x12_271.SetValue("NM1.1", "2B", 1);  //2B- Third-Party Administrator
            x12_271.SetValue("NM1.2", "2", 1); //2 - Nonperson Entity
            x12_271.SetValue("NM1.3", "TRANDINGPARTNER", 1); //Corresponds to Receiver/Sender Name
            x12_271.SetValue("NM1.8", "DP", 1); // Data Provider 
            x12_271.SetValue("NM1.9", "TRADREQ", 1); //Code identifying a party or other code

            x12_271.SetValue("HL.1", "2", 2); //Hierarchical ID Number
            x12_271.SetValue("HL.2", "1", 2); //Indicating that this HL loop is subordinate to the first HL loop
            x12_271.SetValue("HL.3", "21", 2); //Information receiver 
            x12_271.SetValue("HL.4", "1", 2); //Additional Subordinate HL Data Segment in This Hierarchical Structure

            x12_271.SetValue("NM1.1", "GP", 2); //Information Source Name, GP - Gateway Provider
            x12_271.SetValue("NM1.2", "2", 2); //Person
            x12_271.SetValue("NM1.8", "XX", 2); //Code designating the system/method of code structure used fo
            x12_271.SetValue("NM1.9", "XXXXXXXX", 2); //Code identifying a party or other code

            x12_271.SetValue("HL.1", "3", 3); //Hierarchical ID Number
            x12_271.SetValue("HL.2", "2", 3); //2 - Indicating that this HL loop is subordinate to the second HL loop
            x12_271.SetValue("HL.3", "22", 3); //22- Subscriber identifies the employee or group member who is covered for the insurance
            x12_271.SetValue("HL.4", "0", 3); //0 - No Subordinate HL Segment in This Hierarchical Structure

            x12_271.SetValue("NM1.1", "IL", 3); //Insured 
            x12_271.SetValue("NM1.2", "1", 3);  //1 - Person
            x12_271.SetValue("NM1.3", "Doe", 3); //Last Name
            x12_271.SetValue("NM1.4", "Douglas", 3); //First Name
            x12_271.SetValue("NM1.8", "ZZ", 3); //ZZ- Mutually defined
            x12_271.SetValue("NM1.9", "813", 3); //Subscriber Primary Identifier

            x12_271.SetValue("REF.1", "SY", 1); //SY Social Security Number
            x12_271.SetValue("REF.2", "111111111", 1); 

            x12_271.SetValue("REF.1", "1W", 3); //= Member ID Number
            x12_271.SetValue("REF.2", "222222222", 3);

            x12_271.SetValue("N3.1", "123 Trumble Dr"); //Address 1
            x12_271.SetValue("N3.2", "Apt 101"); //Address 2
            
            x12_271.SetValue("N4.1", "Chicago"); //City
            x12_271.SetValue("N4.2", "IL"); //State
            x12_271.SetValue("N4.3", "01040"); //Zip

            x12_271.SetValue("DMG.1", "D8"); //D8 - Date Expressed in Format CCYYMMDD
            x12_271.SetValue("DMG.2", "19680901"); //Birth date
            x12_271.SetValue("DMG.3", "M"); // Gender

            x12_271.SetValue("DTP.1", "307"); //307 -Eligibility
            x12_271.SetValue("DTP.2", "D8"); //D8 - Date Expressed in Format CCYYMMDD
            x12_271.SetValue("DTP.3", "20090731"); //Eligibility Date

            x12_271.SetValue("EB.1", "1"); //1 Active Coverage
            x12_271.SetValue("EB.3", "88"); //88 Pharmacy

            x12_271.SetValue("SE.1", "17"); //Total number of segments included in a transaction set including ST and SE segments
            x12_271.SetValue("SE.2", "0001"); //Identifying control number 

            x12_271.SetValue("GE.1", "1"); //Total Number of Transaction Sets

            x12_271.SetValue("IEA.1", "1"); //Number of Functional Groups GS/GE Pairs in Interchange
            x12_271.SetValue("IEA.2", "1"); //Control Number
            
            return x12_271.Message();

        }

        static void Main(string[] args)
        {
            EDIParser.X12Parser x12_270 = new EDIParser.X12Parser();
            x12_270.CheckISASeparator = false;  // For this test we don't want to check the ISA segment, we will use the parsers defaults.
            EDIParser.X12Parser x12_271 = new EDIParser.X12Parser();
            x12_271.CheckISASeparator = false; // For this test we don't want to check the ISA segment, we will use the parsers defaults.

            x12_270.ParseMsg(buildX12_270());
            if (x12_270.GetValue("NM1.9",3) == "813") //Member is eligible 
              Console.WriteLine(buildX12_271());
            Console.ReadKey();
        }
    }
}
