using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace cSharpGen271X279
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnGenerate;
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(64, 160);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(136, 64);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 72);
            this.label1.TabIndex = 1;
            this.label1.Text = "This is just an example to demonstrate how to generate an EDI 271X279 that has re" +
                "peating elements using Framework EDIParser.Net component in C#";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Name = "Form1";
            this.Text = "Generate 271X279";
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

		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
            string sPath;
            int nHLCounter = 1;
            int nNM1Counter = 1;
            int nN3Counter = 1;
            int nEBCounter = 1;
            int nDMGCounter = 1;
            sPath = AppDomain.CurrentDomain.BaseDirectory;
            EDIParser.X12Parser oX12Parser = new EDIParser.X12Parser();
			//CREATES THE ISA SEGMENT
            oX12Parser.SetValue("ISA.1", "00");              //Authorization Information Qualifier
            oX12Parser.SetValue("ISA.2", "          ");
            oX12Parser.SetValue("ISA.3", "00");            //Security Information Qualifier
            oX12Parser.SetValue("ISA.4", "          ");
            oX12Parser.SetValue("ISA.5", "14");             //Interchange ID Qualifier
            oX12Parser.SetValue("ISA.6", "0073268795005  ");           //Interchange Sender ID
            oX12Parser.SetValue("ISA.7", "ZZ");              //Interchange ID Qualifier
            oX12Parser.SetValue("ISA.8", "RECEIVERISA    "); //Interchange Receiver ID
            oX12Parser.SetValue("ISA.9", "960807");          //Interchange Date
            oX12Parser.SetValue("ISA.10", "1548");           //Interchange Time
            oX12Parser.SetValue("ISA.11", "^");             //Interchange Control Standards Identifier
            oX12Parser.SetValue("ISA.12", "00501");         //Interchange Control Version Number
            oX12Parser.SetValue("ISA.13", "000000020");      //Interchange Control Number
            oX12Parser.SetValue("ISA.14", "0");             //Acknowledgment Requested
            oX12Parser.SetValue("ISA.15", "T");              //Usage Indicator
            oX12Parser.SetValue("ISA.16", ":");               //Component Element Separator

			
			//CREATES THE GS SEGMENT
            oX12Parser.SetValue("GS.1", "HB");             //Functional Identifier Code
            oX12Parser.SetValue("GS.2", "007326879");     //Application Sender's Code
            oX12Parser.SetValue("GS.3", "RECEIVERGS");   //Application Receiver's Code
            oX12Parser.SetValue("GS.4", "19960807");       //Date
            oX12Parser.SetValue("GS.5", "1548");          //Time
            oX12Parser.SetValue("GS.6", "1");            //Group Control Number
            oX12Parser.SetValue("GS.7", "X");             //Responsible Agency Code
            oX12Parser.SetValue("GS.8", "005010X279");     //Version / Release / Industry Identifier Code


			//CREATES THE ST SEGMENT
            oX12Parser.SetValue("ST.1", "271");          //Transaction Set Identifier Code
            oX12Parser.SetValue("ST.2", "4322");        //Transaction Set Control Number
            oX12Parser.SetValue("ST.3", "005010X279");  //Implementation Convention Reference
	
 
			//BHT - BEGINNING OF HIERARCHICAL TRANSACTION
            oX12Parser.SetValue("BHT.1", "0022");    //Hierarchical Structure Code
            oX12Parser.SetValue("BHT.2", "11");      //Transaction Set Purpose Code
            oX12Parser.SetValue("BHT.3", "10001235");    //Reference Identification
            oX12Parser.SetValue("BHT.4", "20060501");   //Reference Identification
            oX12Parser.SetValue("BHT.5", "1319");     //Time
         
 
			//HL - HIERARCHICAL LEVEL
            oX12Parser.SetValue("HL.1", "1" ,nHLCounter);  //Hierarchical ID Number
            oX12Parser.SetValue("HL.3", "20", nHLCounter);       //Hierarchical Level Code
            oX12Parser.SetValue("HL.4", "1", nHLCounter);        //Hierarchical Child Code
            nHLCounter += 1;
 
			//NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
            oX12Parser.SetValue("NM1.1", "PR", nNM1Counter);     //Entity Identifier Code
            oX12Parser.SetValue("NM1.2", "2", nNM1Counter);       //Entity Type Qualifier
            oX12Parser.SetValue("NM1.3", "ABC COMPANY", nNM1Counter);  //Name Last or Organization Name
            oX12Parser.SetValue("NM1.8", "PI", nNM1Counter);   //Identification Code Qualifier
            oX12Parser.SetValue("NM1.9", "842610001", nNM1Counter);    //Identification Code
            nNM1Counter += 1;
	
			//HL - HIERARCHICAL LEVEL
            oX12Parser.SetValue("HL.1", "2", nHLCounter);  //Hierarchical ID Number
            oX12Parser.SetValue("HL.2", "1", nHLCounter);   //Hierarchical Parent ID Number
            oX12Parser.SetValue("HL.3", "21", nHLCounter);       //Hierarchical Level Code
            oX12Parser.SetValue("HL.4", "1", nHLCounter);        //Hierarchical Child Code
          
            nHLCounter += 1;
		    
 
			//NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
            oX12Parser.SetValue("NM1.1", "1P", nNM1Counter);     //Entity Identifier Code
            oX12Parser.SetValue("NM1.2", "2", nNM1Counter);       //Entity Type Qualifier
            oX12Parser.SetValue("NM1.3", "BONE AND JOINT CLINIC", nNM1Counter);  //Name Last or Organization Name
            oX12Parser.SetValue("NM1.8", "SV", nNM1Counter);   //Identification Code Qualifier
            oX12Parser.SetValue("NM1.9", "2000035", nNM1Counter);    //Identification Code

            nNM1Counter += 1;

			//HL - HIERARCHICAL LEVEL
            oX12Parser.SetValue("HL.1", "3", nHLCounter);        //Hierarchical ID Number
            oX12Parser.SetValue("HL.2", "2", nHLCounter);        //Hierarchical Parent ID Number
            oX12Parser.SetValue("HL.3", "22", nHLCounter);       //Hierarchical Level Code
            oX12Parser.SetValue("HL.4", "1", nHLCounter);        //Hierarchical Child Code

            nHLCounter += 1;
 
			//NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
            oX12Parser.SetValue("NM1.1", "IL", nNM1Counter);     //Entity Identifier Code
            oX12Parser.SetValue("NM1.2", "1", nNM1Counter);       //Entity Type Qualifier
            oX12Parser.SetValue("NM1.3", "SMITH", nNM1Counter);  //Name Last or Organization Name
            oX12Parser.SetValue("NM1.4", "JOHN", nNM1Counter);     //Name First
            oX12Parser.SetValue("NM1.8", "MI", nNM1Counter);   //Identification Code Qualifier
            oX12Parser.SetValue("NM1.9", "123456789", nNM1Counter);    //Identification Code

            nNM1Counter += 1;
		
 
			//N3 - ADDRESS INFORMATION
            oX12Parser.SetValue("N3.1", "15197 BROADWAY AVENUE",nN3Counter);    //Address Information
            oX12Parser.SetValue("N3.2", "APT 215", nN3Counter);    //Address Information
           
            
          
 
			//N4 - GEOGRAPHIC LOCATION
            oX12Parser.SetValue("N4.1", "KANSAS CITY", nN3Counter);   //City Name
            oX12Parser.SetValue("N4.2", "MO", nN3Counter);      //State or Province Code
            oX12Parser.SetValue("N4.3", "64108", nN3Counter);   //Postal Code
            nN3Counter += 1;

			//DMG - DEMOGRAPHIC INFORMATION
            oX12Parser.SetValue("DMG.1", "D8", nDMGCounter);   //Date Time Period Format Qualifier
            oX12Parser.SetValue("DMG.2", "19630519", nDMGCounter);  //Date Time Period
            oX12Parser.SetValue("DMG.3", "M", nDMGCounter);    //Gender Code
            nDMGCounter += 1;
			
 
			//HL - HIERARCHICAL LEVEL
            oX12Parser.SetValue("HL.1", "4" ,nHLCounter);        //Hierarchical ID Number
            oX12Parser.SetValue("HL.2", "3" ,nHLCounter);        //Hierarchical Parent ID Number
            oX12Parser.SetValue("HL.3", "23", nHLCounter);       //Hierarchical Level Code
            oX12Parser.SetValue("HL.4", "0", nHLCounter);        //Hierarchical Child Code

            nHLCounter += 1;
			//TRN - TRACE
            oX12Parser.SetValue("TRN.1", "2");      //Trace Type Code
            oX12Parser.SetValue("TRN.2", "93175-012547");    //Reference Identification
            oX12Parser.SetValue("TRN.3", "9877281234");     //Originating Company Identifier
		
 
			//NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
            oX12Parser.SetValue("NM1.1", "03", nNM1Counter);     //Entity Identifier Code
            oX12Parser.SetValue("NM1.2", "1", nNM1Counter);       //Entity Type Qualifier
            oX12Parser.SetValue("NM1.3", "SMITH", nNM1Counter);  //Name Last or Organization Name
            oX12Parser.SetValue("NM1.4", "MARY", nNM1Counter);     //Name First
            nNM1Counter += 1;

			//N3 - ADDRESS INFORMATION
            oX12Parser.SetValue("N3.1", "15197 BROADWAY AVENUE", nN3Counter);    //Address Information
            oX12Parser.SetValue("N3.2", "APT 215", nN3Counter);    //Address Information
			
			
            //N4 - GEOGRAPHIC LOCATION
            oX12Parser.SetValue("N4.1", "KANSAS CITY", nN3Counter);   //City Name
            oX12Parser.SetValue("N4.2", "MO", nN3Counter);      //State or Province Code
            oX12Parser.SetValue("N4.3", "64108", nN3Counter);   //Postal Code
            nN3Counter += 1;
         
              
			//DMG - DEMOGRAPHIC INFORMATION
            oX12Parser.SetValue("DMG.1", "D8", nDMGCounter);   //Date Time Period Format Qualifier
            oX12Parser.SetValue("DMG.2", "19981014", nDMGCounter);  //Date Time Period
            oX12Parser.SetValue("DMG.3", "F", nDMGCounter);    //Gender Code

            nDMGCounter += 1;
            
			//INS - INSURED BENEFIT
            oX12Parser.SetValue("INS.1", "N");    //Yes/No Condition or Response Code
            oX12Parser.SetValue("INS.2", "19");   //Individual Relationship Code
		
 
			//DTP - DATE OR TIME OR PERIOD
            oX12Parser.SetValue("DTP.1", "346");      //Date/Time Qualifier
            oX12Parser.SetValue("DTP.2", "D8");       //Date Time Period Format Qualifier
            oX12Parser.SetValue("DTP.3", "20060101"); //Date Time Period


			//EB - ELIGIBILITY OR BENEFIT INFORMATION
            oX12Parser.SetValue("EB.1", "1",nEBCounter);       //Eligibility or Benefit Information
            oX12Parser.SetValue("EB.3", "30", nEBCounter);       //Service Type Code
            oX12Parser.SetValue("EB.5", "GOLD 123 PLAN", nEBCounter);   //Plan Coverage Description
            nEBCounter += 1;
 
			//EB - ELIGIBILITY OR BENEFIT INFORMATION
            oX12Parser.SetValue("EB.1", "L", nEBCounter);       //Eligibility or Benefit Information
            nEBCounter += 1;
          
			//LS - LOOP HEADER
            oX12Parser.SetValue("LS.1", "2120");    //Loop Identifier Code
			
 
			//NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
            oX12Parser.SetValue("NM1.1", "P3", nNM1Counter);     //Entity Identifier Code
            oX12Parser.SetValue("NM1.2", "1", nNM1Counter);       //Entity Type Qualifier
            oX12Parser.SetValue("NM1.3", "JONES", nNM1Counter);  //Name Last or Organization Name
            oX12Parser.SetValue("NM1.4", "MARCUS", nNM1Counter);     //Name First
            oX12Parser.SetValue("NM1.8", "SV", nNM1Counter);   //Identification Code Qualifier
            oX12Parser.SetValue("NM1.9", "0202034", nNM1Counter);    //Identification Code

            nNM1Counter += 1;
 
			//LE - LOOP TRAILER
            oX12Parser.SetValue("LE.1", "2120");     //Loop Identifier Code
		
 

			//**********************  Generating repeating elements  ***************************************************************
			//EB - ELIGIBILITY OR BENEFIT INFORMATION

            oX12Parser.SetValue("EB.1", "1", nEBCounter);       //Eligibility or Benefit Information
            oX12Parser.SetValue("EB.3", "1^33^35^47^86^88^98^AL^MH^UC", nEBCounter);       //Service Type Code
            nEBCounter += 1;


				//************************************************************************************************************************
 
			//EB - ELIGIBILITY OR BENEFIT INFORMATION
            oX12Parser.SetValue("EB.1", "B", nEBCounter);      //Eligibility or Benefit Information
            oX12Parser.SetValue("EB.3", "1", nEBCounter);      //Service Type Code
            oX12Parser.SetValue("EB.4", "HM", nEBCounter);      //Insurance Type Code
            oX12Parser.SetValue("EB.5", "GOLD 123 PLAN", nEBCounter);     //Plan Coverage Description
            oX12Parser.SetValue("EB.6", "27", nEBCounter);     //Time Period Qualifier
            oX12Parser.SetValue("EB.7", "10", nEBCounter);     //Monetary Amount
            oX12Parser.SetValue("EB.12", "Y", nEBCounter);     //Yes/No Condition or Response Code
            nEBCounter += 1;

			//EB - ELIGIBILITY OR BENEFIT INFORMATION
            oX12Parser.SetValue("EB.1", "B", nEBCounter);      //Eligibility or Benefit Information
            oX12Parser.SetValue("EB.3", "1", nEBCounter);      //Service Type Code
            oX12Parser.SetValue("EB.4", "HM", nEBCounter);      //Insurance Type Code
            oX12Parser.SetValue("EB.5", "GOLD 123 PLAN", nEBCounter);     //Plan Coverage Description
            oX12Parser.SetValue("EB.6", "27", nEBCounter);     //Time Period Qualifier
            oX12Parser.SetValue("EB.7", "30", nEBCounter);     //Monetary Amount
            oX12Parser.SetValue("EB.12", "N", nEBCounter);     //Yes/No Condition or Response Code

            nEBCounter += 1;
			//TRAILING SEGMENTS ARE AUTOMATICALLY CREATED WHEN FREDI COMMITS (SAVES)
			//THE EDIDOC OBJECT INTO AN EDI FILE.

            oX12Parser.SetValue("SE.1", "28");              //Total number of segments included in a transaction set including ST and SE segments
            oX12Parser.SetValue("SE.2", "4322");             //Identifying control number 

            oX12Parser.SetValue("GE.1", "1");               //Total Number of Transaction Sets
            oX12Parser.SetValue("GE.2", "1");

            oX12Parser.SetValue("IEA.1", "1");              //Number of Functional Groups GS/GE Pairs in Interchange
            oX12Parser.SetValue("IEA.2", "000000020~");            //Control Number


            string sFilePath = Application.StartupPath + "\\271_322.txt";

            StreamWriter ostreamwritter;
            ostreamwritter = System.IO.File.CreateText(sFilePath);

            ostreamwritter.WriteLine(oX12Parser.Message());
           
            ostreamwritter.Close();
            MessageBox.Show(oX12Parser.Message());
            System.Windows.Forms.MessageBox.Show("OutPut:" + sFilePath);

		

		}
	}
}
