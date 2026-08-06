using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;


namespace CSharpGen270
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnGen;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnGen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(96, 104);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(104, 32);
            this.btnGen.TabIndex = 0;
            this.btnGen.Text = "Generate";
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 56);
            this.label1.TabIndex = 1;
            this.label1.Text = "This program is just an example on how to use the Framework EDIParser.Net compone" +
                "nt in C# to generate an EDI file.";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(328, 166);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGen);
            this.Name = "Form1";
            this.Text = "Gen 837 X098";
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnGen_Click(object sender, System.EventArgs e)
		{
            string sPath;
           
            sPath = AppDomain.CurrentDomain.BaseDirectory;
            EDIParser.X12Parser oX12Parser = new EDIParser.X12Parser();
            oX12Parser.SetValue("ISA.1", "00");              //Authorization Information Qualifier
            oX12Parser.SetValue("ISA.2", "          ");
            oX12Parser.SetValue("ISA.3", "00");            //Security Information Qualifier
            oX12Parser.SetValue("ISA.4", "          ");
            oX12Parser.SetValue("ISA.5", "12");             //Interchange ID Qualifier
            oX12Parser.SetValue("ISA.6", "Sender         ");           //Interchange Sender ID
            oX12Parser.SetValue("ISA.7", "12");              //Interchange ID Qualifier
            oX12Parser.SetValue("ISA.8", "ReceiverID     "); //Interchange Receiver ID
            oX12Parser.SetValue("ISA.9", "010821");          //Interchange Date
            oX12Parser.SetValue("ISA.10", "1548");           //Interchange Time
            oX12Parser.SetValue("ISA.11", "U");             //Interchange Control Standards Identifier
            oX12Parser.SetValue("ISA.12", "00401");         //Interchange Control Version Number
            oX12Parser.SetValue("ISA.13", "000000020");      //Interchange Control Number
            oX12Parser.SetValue("ISA.14", "0");             //Acknowledgment Requested
            oX12Parser.SetValue("ISA.15", "T");              //Usage Indicator
            oX12Parser.SetValue("ISA.16", ":");               //Component Element Separator

            int nREFCounter = 1;
            int nNM1Counter = 1;
             

	
			//CREATES THE GS SEGMENT
            oX12Parser.SetValue("GS.1", "HC");             //Functional Identifier Code
            oX12Parser.SetValue("GS.2", "SenderDept");     //Application Sender's Code
            oX12Parser.SetValue("GS.3", "ReceiverDept");   //Application Receiver's Code
            oX12Parser.SetValue("GS.4", "20010821");       //Date
            oX12Parser.SetValue("GS.5", "1548");          //Time
            oX12Parser.SetValue("GS.6", "1");            //Group Control Number
            oX12Parser.SetValue("GS.7", "X");             //Responsible Agency Code
            oX12Parser.SetValue("GS.8", "004010X098");     //Version / Release / Industry Identifier Code



 
			//CREATES THE ST SEGMENT
            oX12Parser.SetValue("ST.1", "837");     //Transaction Set Identifier Code
            oX12Parser.SetValue("ST.2", "0021");  //Transaction Set Control Number
		


			//BHT - BEGINNING OF HIERARCHICAL TRANSACTION
            oX12Parser.SetValue("BHT.1", "0019");    //Hierarchical Structure Code
            oX12Parser.SetValue("BHT.2", "00");      //Transaction Set Purpose Code
            oX12Parser.SetValue("BHT.3", "0123");    //Reference Identification
            oX12Parser.SetValue("BHT.4", "19981015");   //Reference Identification
            oX12Parser.SetValue("BHT.5", "1230");     //Time
            oX12Parser.SetValue("BHT.6", "RP");    //Transaction Type Code

		

			//REF - REFERENCE IDENTIFICATION
            oX12Parser.SetValue("REF.1", "87", nREFCounter);    //Reference Identification Qualifier
            oX12Parser.SetValue("REF.2", "004010X098", nREFCounter);    //Reference Identification
	
 
			//NM1 
            oX12Parser.SetValue("NM1.1", "41" , nNM1Counter);     //Entity Identifier Code
            oX12Parser.SetValue("NM1.2", "2", nNM1Counter);       //Entity Type Qualifier
            oX12Parser.SetValue("NM1.3", "Premier Billing Service", nNM1Counter);  //Name Last or Organization Name
            oX12Parser.SetValue("NM1.8", "46", nNM1Counter);   //Identification Code Qualifier
            oX12Parser.SetValue("NM1.9", "TGJ23", nNM1Counter);    //Identification Code

            nNM1Counter += 1;
		
 
			//PER - ADMINISTRATIVE COMMUNICATIONS CONTACT
            oX12Parser.SetValue("PER.1", "IC");   //Contact Function Code
            oX12Parser.SetValue("PER.2", "JERRY");    //Name
            oX12Parser.SetValue("PER.3", "TE");    //Communication Number Qualifier
            oX12Parser.SetValue("PER.4", "3055552222");  //Communication Number
            oX12Parser.SetValue("PER.5", "EX");       //Communication Number Qualifier
            oX12Parser.SetValue("PER.6", "231");        //Communication Number
         

 
			//NM1 - LOOP ID - 1000B RECEIVER NAME
            oX12Parser.SetValue("NM1.1", "40", nNM1Counter);     //Entity Identifier Code
            oX12Parser.SetValue("NM1.2", "2", nNM1Counter);       //Entity Type Qualifier
            oX12Parser.SetValue("NM1.3", "XYZ REPRICER", nNM1Counter);  //Name Last or Organization Name
            oX12Parser.SetValue("NM1.8", "46", nNM1Counter);   //Identification Code Qualifier
            oX12Parser.SetValue("NM1.9", "66783JJT", nNM1Counter);    //Identification Code

            nREFCounter += 1;
            nNM1Counter += 1;
            int nN3Counter = 1;
            int nHLCounter = 1;
            int nHICounter = 1;
            int nDMGCounter = 1;
            int nCLMCounter = 1;
            int nDTPCounter = 1;            
            int nPRVCounter = 1;
			int nHlSubscriberParent;
			int nHlDependentParent;

			//--------------------------------------------------------------------------------------------------------
			// 2000A BILLING/PAY-TO PROVIDER HL LOOP

			int nProviders = 1;	

			for (int nPrvdrCtr=1; nPrvdrCtr <= nProviders; nPrvdrCtr++)
			{
				nHlSubscriberParent = nHLCounter;

				//HL - HIERARCHICAL LEVEL

                oX12Parser.SetValue("HL.1", nHLCounter.ToString(), nHLCounter);  //Hierarchical ID Number
                oX12Parser.SetValue("HL.3", "20", nHLCounter);       //Hierarchical Level Code
                oX12Parser.SetValue("HL.4", "1", nHLCounter);        //Hierarchical Child Code
                 
		
				//NM1 - 2010AA BILLING PROVIDER
                oX12Parser.SetValue("NM1.1", "85", nNM1Counter);     //Entity Identifier Code
                oX12Parser.SetValue("NM1.2", "2", nNM1Counter);       //Entity Type Qualifier
                oX12Parser.SetValue("NM1.3", "Premier Billing Service", nNM1Counter);  //Name Last or Organization Name
                oX12Parser.SetValue("NM1.8", "24", nNM1Counter);   //Identification Code Qualifier
                oX12Parser.SetValue("NM1.9", "587654321", nNM1Counter);    //Identification Code

                nNM1Counter += 1;
				//N3 - ADDRESS INFORMATION
                oX12Parser.SetValue("N3.1", "234 Seaway St.", nN3Counter);    //Address Information
 
				//N4 - GEOGRAPHIC LOCATION
                oX12Parser.SetValue("N4.1", "Miami", nN3Counter);   //City Name
                oX12Parser.SetValue("N4.2", "FL", nN3Counter);      //State or Province Code
                oX12Parser.SetValue("N4.3", "33111", nN3Counter);   //Postal Code


                nN3Counter += 1;
				//NM1 - 2010AB PAY-TO PROVIDER
                oX12Parser.SetValue("NM1.1", "87", nNM1Counter);     //Entity Identifier Code
                oX12Parser.SetValue("NM1.2", "2", nNM1Counter);       //Entity Type Qualifier
                oX12Parser.SetValue("NM1.3", "Kildare Associates", nNM1Counter);  //Name Last or Organization Name
                oX12Parser.SetValue("NM1.8", "24", nNM1Counter);   //Identification Code Qualifier
                oX12Parser.SetValue("NM1.9", "99878-ABA", nNM1Counter);    //Identification Code
                nNM1Counter += 1;

                
				//N3 - ADDRESS INFORMATION
                oX12Parser.SetValue("N3.1", "2345 Ocean Blvd.", nN3Counter);    //Address Information
			
				//N4 - GEOGRAPHIC LOCATION
                oX12Parser.SetValue("N4.1", "Miami", nN3Counter);   //City Name
                oX12Parser.SetValue("N4.2", "FL", nN3Counter);      //State or Province Code
                oX12Parser.SetValue("N4.3", "33111", nN3Counter);   //Postal Code


                nN3Counter += 1;                
				//-------------------------------------------------------------------------------------------------------------
				// 2000B SUBSCRIBER HL LOOP

				int nSubscriberCount = 1;

				for (int nSubscbrCtr=1; nSubscbrCtr <= nSubscriberCount; nSubscbrCtr++)
				{
					nHLCounter = nHLCounter + 1;
					nHlDependentParent = nHLCounter;

					//HL - HIERARCHICAL LEVEL

                    oX12Parser.SetValue("HL.1", nHLCounter.ToString(), nHLCounter);  //Hierarchical ID Number
                    oX12Parser.SetValue("HL.2", nHlSubscriberParent.ToString(), nHLCounter);     //Hierarchical Parent ID Number
                    oX12Parser.SetValue("HL.3", "22", nHLCounter);       //Hierarchical Level Code
                    oX12Parser.SetValue("HL.4", "0", nHLCounter);        //Hierarchical Child Code
                    nHLCounter += 1;
 
					//SBR - SUBSCRIBER INFORMATION
                    oX12Parser.SetValue("SBR.1", "P");    //Payer Responsibility Sequence Number Code
                    oX12Parser.SetValue("SBR.2", "18");      //Individual Relationship Code
                    oX12Parser.SetValue("SBR.3", "12312-A");  //Reference Identification
                    oX12Parser.SetValue("SBR.9", "HM");     //Claim Filing Indicator Code


					//NM1 - 2010BA SUBSCRIBER
                    oX12Parser.SetValue("NM1.1", "IL", nNM1Counter);     //Entity Identifier Code
                    oX12Parser.SetValue("NM1.2", "1", nNM1Counter);       //Entity Type Qualifier
                    oX12Parser.SetValue("NM1.3", "Smith", nNM1Counter);  //Name Last or Organization Name
                    oX12Parser.SetValue("NM1.4", "Ted", nNM1Counter);  //Name First
                    oX12Parser.SetValue("NM1.8", "MI", nNM1Counter);   //Identification Code Qualifier
                    oX12Parser.SetValue("NM1.9", "000221111", nNM1Counter);    //Identification Code

                    nNM1Counter += 1;
 
					//N3 - ADDRESS INFORMATION
                    oX12Parser.SetValue("N3.1", "236 N. Main St.", nN3Counter);    //Address Information
	
 
					//N4 - GEOGRAPHIC LOCATION
                    oX12Parser.SetValue("N4.1", "Maimi", nN3Counter);   //City Name
                    oX12Parser.SetValue("N4.2", "Fl", nN3Counter);      //State or Province Code
                    oX12Parser.SetValue("N4.3", "33413", nN3Counter);   //Postal Code

                    nN3Counter += 1;
				
 
					//DMG - DEMOGRAPHIC INFORMATION
                    oX12Parser.SetValue("DMG.1", "D8" ,nDMGCounter);   //Date Time Period Format Qualifier
                    oX12Parser.SetValue("DMG.2", "19430501", nDMGCounter);  //Date Time Period
                    oX12Parser.SetValue("DMG.3", "M", nDMGCounter);    //Gender Code

                    nDMGCounter += 1;
					//NM1 - 2010BB SUBSCRIBER/PAYER
                    oX12Parser.SetValue("NM1.1", "PR", nNM1Counter);     //Entity Identifier Code
                    oX12Parser.SetValue("NM1.2", "2", nNM1Counter);       //Entity Type Qualifier
                    oX12Parser.SetValue("NM1.3", "Alliance Health and Life Insurance", nNM1Counter);  //Name Last or Organization Name
                    oX12Parser.SetValue("NM1.8", "PI", nNM1Counter);  //Name First
                    oX12Parser.SetValue("NM1.9", "741234", nNM1Counter);    //Identification Code

                    nNM1Counter += 1;
					//N2 - ADDITIONAL NAME INFORMATION
                    oX12Parser.SetValue("N2.1", "COMPANY");    //Name

			
					//----------------------------------------------------------------------------------------------------
					// CLM - HEALTH CLAIM -- CLAIM LOOP

					int nClaimsCount = 1;

					for (int nClmCtr=1; nClmCtr <= nClaimsCount; nClmCtr++)
					{
						//CLM - HEALTH CLAIM

                        oX12Parser.SetValue("CLM.1", "26462967",nCLMCounter);    //Claim Submitter's Identifier
                        oX12Parser.SetValue("CLM.2", "100.00", nCLMCounter);      //Monetary Amount
                        oX12Parser.SetValue("CLM.5", "11::1", nCLMCounter);        //Claim Submitter's Identifier :  Claim Filing Indicator Code
                        oX12Parser.SetValue("CLM.6", "Y", nCLMCounter);           //Yes/No Condition or Response Code
                        oX12Parser.SetValue("CLM.7", "A", nCLMCounter);           //Provider Accept Assignment Code
                        oX12Parser.SetValue("CLM.8", "Y", nCLMCounter);           //Yes/No Condition or Response Code
                        oX12Parser.SetValue("CLM.9", "Y", nCLMCounter);           //Release of Information Code
                        oX12Parser.SetValue("CLM.10", "C", nCLMCounter);          //Patient Signature Source Code

                        
						//DTP - DATE OR TIME OR PERIOD
                        oX12Parser.SetValue("DTP.1", "431",nDTPCounter);      //Date/Time Qualifier
                        oX12Parser.SetValue("DTP.2", "D8", nDTPCounter);       //Date Time Period Format Qualifier
                        oX12Parser.SetValue("DTP.3", "19981003", nDTPCounter); //Date Time Period

 
						//REF - REFERENCE IDENTIFICATION
                        oX12Parser.SetValue("REF.1", "D9", nREFCounter);      //Reference Identification Qualifier
                        oX12Parser.SetValue("REF.2", "17312345600006351", nREFCounter);   //Reference Identification
 
						//HI - HEALTH CARE INFORMATION CODES
                        oX12Parser.SetValue("HI.1", "BK:0340", nHICounter);      //Health Care Code Information  :  Health Care Code Information
                        oX12Parser.SetValue("HI.2", "BF:V7389", nHICounter);       //Date Time Period Format Qualifier :  Health Care Code Information

                        nHICounter += 1;

 
						//NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                        oX12Parser.SetValue("NM1.1", "82", nNM1Counter);     //Entity Identifier Code
                        oX12Parser.SetValue("NM1.2", "1", nNM1Counter);       //Entity Type Qualifier
                        oX12Parser.SetValue("NM1.3", "Kildare", nNM1Counter);  //Name Last or Organization Name
                        oX12Parser.SetValue("NM1.4", "Ben", nNM1Counter);  //Name First
                        oX12Parser.SetValue("NM1.8", "34", nNM1Counter);  //Identification Code Qualifier
                        oX12Parser.SetValue("NM1.9", "112233334", nNM1Counter);    //Identification Code

                        nNM1Counter += 1;
 
						//PRV - PROVIDER INFORMATION
                        oX12Parser.SetValue("PRV.1", "PE", nPRVCounter);     //Provider Code
                        oX12Parser.SetValue("PRV.2", "ZZ", nPRVCounter);     //Reference Identification Qualifier
                        oX12Parser.SetValue("PRV.3", "203BF0100Y", nPRVCounter);  //Reference Identification
                        nPRVCounter += 1;

						//NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                        oX12Parser.SetValue("NM1.1", "77", nNM1Counter);     //Entity Identifier Code
                        oX12Parser.SetValue("NM1.2", "2", nNM1Counter);       //Entity Type Qualifier
                        oX12Parser.SetValue("NM1.3", "Kildare Associates", nNM1Counter);  //Name Last or Organization Name
                        oX12Parser.SetValue("NM1.8", "24", nNM1Counter);  //Name First
                        oX12Parser.SetValue("NM1.9", "581234567", nNM1Counter);    //Identification Code

                        nNM1Counter += 1;
						//N3 - ADDRESS INFORMATION
                        oX12Parser.SetValue("N3.1", "2345 Ocean Blvd.", nN3Counter);    //Address Information
					
 
						//N4 - GEOGRAPHIC LOCATION
                        oX12Parser.SetValue("N4.1", "Miami", nN3Counter);   //City Name
                        oX12Parser.SetValue("N4.2", "FL", nN3Counter);      //State or Province Code
                        oX12Parser.SetValue("N4.3", "33111", nN3Counter);   //Postal Code

                        nN3Counter += 1;
                        nCLMCounter += 1;
                        nDTPCounter += 1;
						//----------------------------------------------------------------------------------------------------
						// LX - 2400 SERVICE LINE LOOP

						int nServiceLineCount = 4;
                        nREFCounter += 1;
                        int nLXCounter = 1;
						for (int nSrvLineCtr=1; nSrvLineCtr <= nServiceLineCount; nSrvLineCtr++)
						{
							//LX - ASSIGNED NUMBER 
                            oX12Parser.SetValue("LX.1", nSrvLineCtr.ToString(), nLXCounter);    //Assigned Number

 
							//SV1 - PROFESSIONAL SERVICE
                            oX12Parser.SetValue("SV1.1", "HC:99213", nLXCounter);      //Composite Medical Procedure Identifier : Monetary Amount
                            oX12Parser.SetValue("SV1.2", "40.00", nLXCounter);         //Monetary Amount
                            oX12Parser.SetValue("SV1.3", "UN", nLXCounter);            //Unit or Basis for Measurement Code
                            oX12Parser.SetValue("SV1.4", "1", nLXCounter);             //Quantity
                            oX12Parser.SetValue("SV1.7", "1", nLXCounter);             //Composite Medical Procedure Identifier
                            oX12Parser.SetValue("SV1.9", "N", nLXCounter);             //Yes/No Condition or Response Code

							//DTP - DATE OR TIME OR PERIOD
                            oX12Parser.SetValue("DTP.1", "472", nDTPCounter);      //Date/Time Qualifier
                            oX12Parser.SetValue("DTP.2", "D8", nDTPCounter);        //Date Time Period Format Qualifier
                            oX12Parser.SetValue("DTP.3", "19981003", nDTPCounter);  //Date Time Period
                            nDTPCounter += 1;
                            nLXCounter += 1;
						
						}
					}              
				}
			}
            oX12Parser.SetValue("SE.1", "43");              //Total number of segments included in a transaction set including ST and SE segments
            oX12Parser.SetValue("SE.2", "0021");             //Identifying control number 

            oX12Parser.SetValue("GE.1", "1");               //Total Number of Transaction Sets
            oX12Parser.SetValue("GE.2", "1");

            oX12Parser.SetValue("IEA.1", "1");              //Number of Functional Groups GS/GE Pairs in Interchange
            oX12Parser.SetValue("IEA.2", "000000020~");            //Control Number


            string sFilePath = Application.StartupPath + "\\837_X098.txt";

            StreamWriter ostreamwritter;
            ostreamwritter = System.IO.File.CreateText(sFilePath);

            ostreamwritter.WriteLine(oX12Parser.Message());
            ostreamwritter.Close();

            System.Windows.Forms.MessageBox.Show("OutPut:" + sFilePath);

		}

	}
}
