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
            this.btnGen.Location = new System.Drawing.Point(96, 96);
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
            this.label1.Text = "This program is just an example of how to use the Framework EDIParser.Net compone" +
                "nt in C# so as to generate an EDI file.";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(304, 150);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGen);
            this.Name = "Form1";
            this.Text = "Gen 270X092";
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
			string  sPath;
			
			sPath = AppDomain.CurrentDomain.BaseDirectory;
            EDIParser.X12Parser oX12Parser = new EDIParser.X12Parser();
          
            oX12Parser.SetValue("ISA.1", "00");              //Authorization Information Qualifier
            oX12Parser.SetValue("ISA.2", "          "); 
            oX12Parser.SetValue("ISA.3", "01");            //Security Information Qualifier
            oX12Parser.SetValue("ISA.4", "SECRET    ");
            oX12Parser.SetValue("ISA.5", "ZZ");             //Interchange ID Qualifier
            oX12Parser.SetValue("ISA.6", "9999           ");           //Interchange Sender ID
            oX12Parser.SetValue("ISA.7", "ZZ") ;              //Interchange ID Qualifier
            oX12Parser.SetValue("ISA.8", "7777           "); //Interchange Receiver ID
            oX12Parser.SetValue("ISA.9", "930603");          //Interchange Date
            oX12Parser.SetValue("ISA.10", "1230");           //Interchange Time
            oX12Parser.SetValue("ISA.11", "U");             //Interchange Control Standards Identifier
            oX12Parser.SetValue("ISA.12", "00401");         //Interchange Control Version Number
            oX12Parser.SetValue("ISA.13", "000000905");      //Interchange Control Number
            oX12Parser.SetValue("ISA.14", "1");             //Acknowledgment Requested
            oX12Parser.SetValue("ISA.15", "T");              //Usage Indicator
            oX12Parser.SetValue("ISA.16", ":");               //Component Element Separator
            

			// create the interchange segment
		
			// create the functional group segment

            oX12Parser.SetValue("GS.1", "HS");             //Functional Identifier Code
            oX12Parser.SetValue("GS.2", "SENDER CODE");     //Application Sender's Code
            oX12Parser.SetValue("GS.3", "RECEIVER CODE");   //Application Receiver's Code
            oX12Parser.SetValue("GS.4", "19970101");       //Date
            oX12Parser.SetValue("GS.5", "0802");          //Time
            oX12Parser.SetValue("GS.6", "1")  ;            //Group Control Number
            oX12Parser.SetValue("GS.7", "X") ;             //Responsible Agency Code
            oX12Parser.SetValue("GS.8", "004010X092");     //Version / Release / Industry Identifier Code




			// HEADER
			// TRANSACTION SET HEADER

            oX12Parser.SetValue("ST.1", "270");     //Transaction Set Identifier Code
            oX12Parser.SetValue("ST.2", "1234");  //Transaction Set Control Number
		
            
            //create the BHT segment
            oX12Parser.SetValue("BHT.1", "0022");
            oX12Parser.SetValue("BHT.2", "01");
            oX12Parser.SetValue("BHT.3", "12345");
            oX12Parser.SetValue("BHT.4", "20070329");
            oX12Parser.SetValue("BHT.5", "1225");
            oX12Parser.SetValue("BHT.6", "RT");

            int nNM1Counter = 1;
            int nHLCounter = 0;
			int nHlInfoReceiverParent;
			int nHlSubscriberParent;
			int nHlDependentParent;


			//--------------------------------------------------------------------------------
			// DETAIL INFO SOURCE LEVEL

			int nInfoSources = 1;
			for (int nInfoSourceCtr=1; nInfoSourceCtr <= nInfoSources; nInfoSourceCtr++)
			{
				nHLCounter = nHLCounter + 1;
				nHlInfoReceiverParent = nHLCounter;

                //DETAIL INFO SOURCE LEVEL
                oX12Parser.SetValue("HL.1", nHLCounter.ToString(), nHLCounter);
                oX12Parser.SetValue("HL.3", "20", nHLCounter);
                oX12Parser.SetValue("HL.4", "1", nHLCounter);
               
                //INFORMATION SOURCE NAME
                oX12Parser.SetValue("NM1.1", "PR", nNM1Counter);
                oX12Parser.SetValue("NM1.2", "2", nNM1Counter);
                oX12Parser.SetValue("NM1.3", "WILSON", nNM1Counter);
                oX12Parser.SetValue("NM1.4", "BILL", nNM1Counter);
                oX12Parser.SetValue("NM1.8", "PI", nNM1Counter);
                oX12Parser.SetValue("NM1.9", "87728", nNM1Counter);

                nNM1Counter += 1;
				//----------------------------------------------------------------------------------
				// INFORMATION RECEIVER LEVEL

				int nInfoReceivers = 1;
                int nPERCounter = 1;
                int nREFCounter =1;
                int nN3Counter = 1;
                int nPRVCounter = 1;
                int nTRNCounter = 1;
                int nDTPCounter = 1;
                int nDMGCounter = 1;
                int nEQCounter = 1;
                int nIIICounter = 1;
                int nAMTCounter = 1;
				for (int nInfoReceiverCtr=1; nInfoReceiverCtr <= nInfoReceivers; nInfoReceiverCtr++)
				{
					nHLCounter = nHLCounter + 1;
					nHlSubscriberParent = nHLCounter;

                    //INFORMATION RECEIVER LEVEL
                    oX12Parser.SetValue("HL.1", nHLCounter.ToString(), nHLCounter);
                    oX12Parser.SetValue("HL.2", nHlInfoReceiverParent.ToString(), nHLCounter);
                    oX12Parser.SetValue("HL.3", "21", nHLCounter);
                    oX12Parser.SetValue("HL.4", "1", nHLCounter);

					// INFORMATION RECEIVER NAME


                   oX12Parser.SetValue("NM1.1", "1P", nNM1Counter);
                   oX12Parser.SetValue("NM1.2", "1", nNM1Counter);
                   oX12Parser.SetValue("NM1.3", "PATRICK", nNM1Counter);
                   oX12Parser.SetValue("NM1.4", "FITZGERALD", nNM1Counter);
                   oX12Parser.SetValue("NM1.5", "M", nNM1Counter);
                   oX12Parser.SetValue("NM1.8", "SV", nNM1Counter);
                   oX12Parser.SetValue("NM1.9", "0202034", nNM1Counter);                   
                    nNM1Counter += 1;

     				//INFORMATION RECEVIER ADDITIONAL IDENTIFICATION.

                   oX12Parser.SetValue("REF.1", "N5",nREFCounter);
                   oX12Parser.SetValue("REF.2", "129",nREFCounter);
                   nREFCounter += 1;
                
					// INFORMATION RECEIVER ADDRESS.

                   oX12Parser.SetValue("N3.1", "Cross street Dr." ,nN3Counter);
                   oX12Parser.SetValue("N3.2", "Suite 987", nN3Counter);

					// INFORMATION RECEIVER CITY, STATE ZIP

                   oX12Parser.SetValue("N4.1", "NewYork", nN3Counter);
                   oX12Parser.SetValue("N4.2", "NY", nN3Counter);
                   oX12Parser.SetValue("N4.3", "10023", nN3Counter);
                   oX12Parser.SetValue("N4.4", "USA", nN3Counter);
                   nN3Counter += 1;

					// INFORMATION RECEIVER CONTACT INFORMATION
                   oX12Parser.SetValue("PER.1", "IC", nPERCounter);
                   oX12Parser.SetValue("PER.2", "BILLING DEPT", nPERCounter);
                   oX12Parser.SetValue("PER.3", "TE", nPERCounter);
                   oX12Parser.SetValue("PER.4", "2065551212", nPERCounter);
                   oX12Parser.SetValue("PER.5", "EX", nPERCounter);
                   oX12Parser.SetValue("PER.6", "2104", nPERCounter);
                   oX12Parser.SetValue("PER.7", "FX", nPERCounter);
                   oX12Parser.SetValue("PER.8", "2065551214", nPERCounter);
                   nPERCounter += 1;


					// INFORMATION RECEIVER PROVIDER INFORMATION

                   oX12Parser.SetValue("PRV.1", "PE",nPRVCounter);
                   oX12Parser.SetValue("PRV.2", "ZZ", nPRVCounter);
                   oX12Parser.SetValue("PRV.3", "207K00000X", nPRVCounter);
                   nPRVCounter += 1;
					//------------------------------------------------------------------------------
					// SUBSCRIBER LEVEL

					int nSubscribers = 1;

					for (int nSubscbrCtr=1; nSubscbrCtr <= nSubscribers; nSubscbrCtr++)
					{
						nHLCounter = nHLCounter + 1;
						nHlDependentParent = nHLCounter;


                        oX12Parser.SetValue("HL.1", nHLCounter.ToString(), nHLCounter);
                        oX12Parser.SetValue("HL.2", nHlSubscriberParent.ToString(), nHLCounter);
                        oX12Parser.SetValue("HL.3", "22", nHLCounter);
                        oX12Parser.SetValue("HL.4", "1", nHLCounter);

					
						// SUBSCRIBER TRACE NUMBER

                        oX12Parser.SetValue("TRN.1", "1", nTRNCounter);
                        oX12Parser.SetValue("TRN.2", "98175-02157", nTRNCounter);
                        oX12Parser.SetValue("TRN.3", "9877281234", nTRNCounter);
                        nTRNCounter += 1;
					
						// SUBSCRIBER NAME
                        oX12Parser.SetValue("NM1.1", "IL",nNM1Counter);
                        oX12Parser.SetValue("NM1.2", "1",nNM1Counter);
                        oX12Parser.SetValue("NM1.3", "JONES",nNM1Counter);
                        oX12Parser.SetValue("NM1.4", "EDWARD",nNM1Counter);
                        oX12Parser.SetValue("NM1.5", "S",nNM1Counter);
                        oX12Parser.SetValue("NM1.8", "MI",nNM1Counter);
                        oX12Parser.SetValue("NM1.9", "11122333301",nNM1Counter);
                        nNM1Counter += 1;


					
						// SUBSCRIBER ADDITIONAL IDENTIFICATION
                        oX12Parser.SetValue("REF.1", "1L", nREFCounter);
                        oX12Parser.SetValue("REF.2", "19430519", nREFCounter);
                        nREFCounter += 1;
						

						// SUBSCRIBER ADDRESS INFORMATATION ,SUBSCRIBER CITY STATE ZIP
                        oX12Parser.SetValue("N3.1", "435 th Street", nN3Counter);
                        oX12Parser.SetValue("N3.2", "435 th Street", nN3Counter);


                        oX12Parser.SetValue("N4.1", "NewYork", nN3Counter);
                        oX12Parser.SetValue("N4.2", "NY", nN3Counter);
                        oX12Parser.SetValue("N4.3", "14008", nN3Counter);
                        nN3Counter += 1;
                        
						// SUBSCRIBER DEMOGRAPHIC INFORMATION
                        oX12Parser.SetValue("DMG.1", "D8", nDMGCounter);
                        oX12Parser.SetValue("DMG.2", "19430917", nDMGCounter);
                        oX12Parser.SetValue("DMG.3", "F", nDMGCounter);
                        nDMGCounter += 1;

					
						// SUBSCRIBER DATE
                        oX12Parser.SetValue("DTP.1", "102", nDTPCounter);
                        oX12Parser.SetValue("DTP.2", "D8", nDTPCounter);
                        oX12Parser.SetValue("DTP.3", "19980818", nDTPCounter);
                        nDTPCounter += 1;

						// SUBSCRIBER ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                        oX12Parser.SetValue("EQ.1", "98", nEQCounter);
                        oX12Parser.SetValue("EQ.3", "FAM", nEQCounter);
                        nEQCounter += 1;


						//SUBSCRIBER SPEND DOWN AMOUNT
                        oX12Parser.SetValue("AMT.1", "R", nAMTCounter);
                        oX12Parser.SetValue("AMT.2", "37.5", nAMTCounter);
                        nAMTCounter += 1;



						//SUBSCRIBER ELIGIBILITY OR BENEFIT
                        oX12Parser.SetValue("III.1", "BK", nIIICounter);
                        oX12Parser.SetValue("III.2", "486", nIIICounter);
                        nIIICounter += 1;

						//SUBSCRIBER ADDITIONAL INFORMATION
                        oX12Parser.SetValue("REF.1", "9F", nREFCounter);
                        oX12Parser.SetValue("REF.2", "66045", nREFCounter);
                        nREFCounter += 1;

					

						//SUBSCRIBER ELIGIBILITY/BENEFIT DATE
                        oX12Parser.SetValue("DTP.1", "472", nDTPCounter);
                        oX12Parser.SetValue("DTP.2", "D8", nDTPCounter);
                        oX12Parser.SetValue("DTP.3", "19990707", nDTPCounter);
                        nDTPCounter += 1;

                        nHlSubscriberParent += 1;
                       

						//- ------------------------------------------------------------------------------
						//DEPENDENT LEVEL
						int nDependents = 1;
						for (int nDependentCtr=1; nDependentCtr <= nDependents; nDependentCtr++)
						{
							nHLCounter = nHLCounter + 1;

                            oX12Parser.SetValue("HL.1", nHLCounter.ToString(), nHLCounter);
                            oX12Parser.SetValue("HL.2", nHlSubscriberParent.ToString(), nHLCounter);
                            oX12Parser.SetValue("HL.3", "23", nHLCounter);
                            oX12Parser.SetValue("HL.4", "0", nHLCounter);
							

							//DEPENDENT TRACE NUMBER
                            oX12Parser.SetValue("TRN.1", "1", nTRNCounter);
                            oX12Parser.SetValue("TRN.2", "98175-02157", nTRNCounter);
                            oX12Parser.SetValue("TRN.3", "9877281234", nTRNCounter);
                            oX12Parser.SetValue("TRN.4", "RADIOLOGY", nTRNCounter);
                            nTRNCounter += 1;

							//DEPENDENT NAME
                            oX12Parser.SetValue("NM1.1", "03",nNM1Counter);
                            oX12Parser.SetValue("NM1.2", "1",nNM1Counter);
                            oX12Parser.SetValue("NM1.3", "SMITH",nNM1Counter);
                            oX12Parser.SetValue("NM1.4", "MARY LOU",nNM1Counter);
                            oX12Parser.SetValue("NM1.5", "R",nNM1Counter);
                            nNM1Counter += 1;


							//DEPENDENT ADDITIONAL IDENTIFICATION
                            oX12Parser.SetValue("REF.1", "SY", nREFCounter);
                            oX12Parser.SetValue("REF.2", "003221234", nREFCounter);
                            nREFCounter += 1;

							//DEPENDENT ADDRESS
                            oX12Parser.SetValue("N3.1", "WATERLOO",nN3Counter);
                            oX12Parser.SetValue("N3.2", "32 PARK ST", nN3Counter);

							//DEPENDENT CITY/STATE/ZIP CODE

                            oX12Parser.SetValue("N4.1", "NEWYORK", nN3Counter);
                            oX12Parser.SetValue("N4.2", "NY", nN3Counter);
                            oX12Parser.SetValue("N4.3", "10023", nN3Counter);
                            oX12Parser.SetValue("N4.4", "032", nN3Counter);
                            nN3Counter += 1;
						
							//PROVIDER INFORMATION

                            oX12Parser.SetValue("PRV.1", "PE", nPRVCounter);
                            oX12Parser.SetValue("PRV.2", "ZZ", nPRVCounter);
                            oX12Parser.SetValue("PRV.3", "207K00000X", nPRVCounter);
                            nPRVCounter += 1;
							

							//DEPENDENT DEMOGRAPHIC INFORMATION
                            oX12Parser.SetValue("DMG.1", "D8", nDMGCounter);
                            oX12Parser.SetValue("DMG.2", "19430121", nDMGCounter);
                            oX12Parser.SetValue("DMG.3", "M", nDMGCounter);
                            nDMGCounter += 1;

							//DEPENDENT DATE
                            oX12Parser.SetValue("DTP.1", "102", nDTPCounter);
                            oX12Parser.SetValue("DTP.2", "D8", nDTPCounter);
                            oX12Parser.SetValue("DTP.3", "19500808", nDTPCounter);
                            nDTPCounter += 1;

							//DEPENDENT ELIGIBILITY OR BENEFIT INQUIRY INFORMATION
                            oX12Parser.SetValue("EQ.1", "30", nEQCounter);
                            oX12Parser.SetValue("EQ.3", "FAM", nEQCounter);
                            nEQCounter += 1;

						
							//DEPENDENT ELIGIBILITY OR BENEFIT ADDITIONAL INQUIRY 
                            oX12Parser.SetValue("III.1", "BK", nIIICounter);
                            oX12Parser.SetValue("III.2", "486", nIIICounter);
                            nIIICounter += 1;

							//DEPENDENT ADDITIONAL INFORMATION
                            oX12Parser.SetValue("REF.1", "9F", nREFCounter);
                            oX12Parser.SetValue("REF.2", "660415", nREFCounter);
                            nREFCounter += 1;
						

							//DEPENDENT ELIGIBILITY/BENEFIT DATE
                            oX12Parser.SetValue("DTP.1", "472", nDTPCounter);
                            oX12Parser.SetValue("DTP.2", "D8", nDTPCounter);
                            oX12Parser.SetValue("DTP.3", "19570807", nDTPCounter);
                            nDTPCounter += 1;
						}

					}	
				}	
			}

        oX12Parser.SetValue("SE.1", "38");              //Total number of segments included in a transaction set including ST and SE segments
        oX12Parser.SetValue("SE.2", "1234");             //Identifying control number 

        oX12Parser.SetValue("GE.1", "1") ;               //Total Number of Transaction Sets
        oX12Parser.SetValue("GE.2", "1");

        oX12Parser.SetValue("IEA.1", "1");              //Number of Functional Groups GS/GE Pairs in Interchange
        oX12Parser.SetValue("IEA.2", "000000905");            //Control Number


        string sFilePath = Application.StartupPath + "\\270_X092.txt";
       
        StreamWriter ostreamwritter;
        ostreamwritter = System.IO.File.CreateText(sFilePath);
      
        ostreamwritter.WriteLine(oX12Parser.Message());
        ostreamwritter.Close();

        System.Windows.Forms.MessageBox.Show("OutPut:" + sFilePath);

		}
	}
}
