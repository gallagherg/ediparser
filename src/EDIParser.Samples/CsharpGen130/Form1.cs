using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.IO;

namespace CsharpGen130
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(128, 128);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 40);
			this.button1.TabIndex = 0;
			this.button1.Text = "Generate";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(344, 214);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Generate EDI X12 130";
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			//This is just an example program to show how to generate an EDI X12 130 Student Educational Record (Transcript)
			//in C# with the EDI Parser for .NET

            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            EDIParser.X12Parser oX12Parser = new EDIParser.X12Parser();

            //SET TERMINATORS
            oX12Parser.SegmentSeparator = "~\r\n";
            oX12Parser.FieldSeparator = "*";
            oX12Parser.ComponentSeparator = ">";
			
			string sEdiFile = "130Outbound.x12";
 
			Cursor = Cursors.WaitCursor;

			//CREATES THE ISA SEGMENT
			 oX12Parser.SetValue("ISA.1", "00");     //Authorization Information Qualifier
			 oX12Parser.SetValue("ISA.2", "          ");     //Authorization Information
			 oX12Parser.SetValue("ISA.3", "00");     //Security Information Qualifier
			 oX12Parser.SetValue("ISA.4", "          ");     //Security Information
			 oX12Parser.SetValue("ISA.5", "ZZ");     //Interchange ID Qualifier
			 oX12Parser.SetValue("ISA.6", "SENDER ID      ");     //Interchange Sender ID
			 oX12Parser.SetValue("ISA.7", "ZZ");     //Interchange ID Qualifier
			 oX12Parser.SetValue("ISA.8", "RECEIVER ID    ");     //Interchange Receiver ID
			 oX12Parser.SetValue("ISA.9", "061206");     //Interchange Date
			 oX12Parser.SetValue("ISA.10", "0101");     //Interchange Time
			 oX12Parser.SetValue("ISA.11", "U");     //Interchange Control Standards Identifier
			 oX12Parser.SetValue("ISA.12", "00401");     //Interchange Control Version Number
			 oX12Parser.SetValue("ISA.13", "000000001");     //Interchange Control Number
			 oX12Parser.SetValue("ISA.14", "0");     //Acknowledgment Requested
			 oX12Parser.SetValue("ISA.15", "T");     //Usage Indicator
			 oX12Parser.SetValue("ISA.16", "!");     //Component Element Separator
 
			//CREATES THE GS SEGMENT
			oX12Parser.SetValue("GS.1", "ED");     //Functional Identifier Code
			oX12Parser.SetValue("GS.2", "APP SENDER");     //Application Sender's Code
			oX12Parser.SetValue("GS.3", "APP RECEIVER");     //Application Receiver's Code
			oX12Parser.SetValue("GS.4", "01010101");     //Date
			oX12Parser.SetValue("GS.5", "01010101");     //Time
			oX12Parser.SetValue("GS.6", "1");     //Group Control Number
			oX12Parser.SetValue("GS.7", "X");     //Responsible Agency Code
			oX12Parser.SetValue("GS.8", "004010");     //Version / Release / Industry Identifier Code
 
			//CREATES THE ST SEGMENT
			oX12Parser.SetValue("ST.1","130");     //Transaction Set Identifier Code
			oX12Parser.SetValue("ST.2","0001");     //Transaction Set Control Number
 
			//BGN - BEGINNING SEGMENT
			oX12Parser.SetValue("BGN.1","00");     //Transaction Set Purpose Code
			oX12Parser.SetValue("BGN.2","1234567");     //Reference Identification
			oX12Parser.SetValue("BGN.3","20050503");     //Date
			oX12Parser.SetValue("BGN.4","103020");     //Time
			oX12Parser.SetValue("BGN.5","ET");     //Time Code
 
			//ERP - EDUCATIONAL RECORD PURPOSE
			oX12Parser.SetValue("ERP.1","PS");     //Transaction Type Code
			oX12Parser.SetValue("ERP.2","INF");     //Status Reason Code
 
			//REF - REFERENCE IDENTIFICATION
			oX12Parser.SetValue("REF.1","SY");     //Reference Identification Qualifier
			oX12Parser.SetValue("REF.2","12345679");     //Reference Identification
 
			//N1 - NAME
			//create N1 segment in N1 loop
			oX12Parser.SetValue("N1.1","AS");     //Postsecondary Education Sender Entity Identifier Code
			oX12Parser.SetValue("N1.2","UNIVERSITY SENDER");     //Name
 
			//N3 - ADDRESS INFORMATION
			//create N3 segment in N1 loop
			oX12Parser.SetValue("N3.1","123 SENDER AVENUE");     //Address Information
 
			//N4 - GEOGRAPHIC LOCATION
			//create N4 segment in N1 loop
			oX12Parser.SetValue("N4.1","LOS ANGELES");     //City Name
 
			//N1 - NAME
			//create N1 segment in the second instance of the N1 loop
			oX12Parser.SetValue("N1.1","AT",2);     //Postsecondary Education Receiver Entity Identifier Code
            oX12Parser.SetValue("N1.2", "COLLEGE RECEIVER", 2);     //Name
 
			//N3 - ADDRESS INFORMATION
			//create N3 segment in the second instance of the N1 loop
            oX12Parser.SetValue("N3.1", "456 RECEIVER ST", 2);     //Address Information
 
			//N4 - GEOGRAPHIC LOCATION
			//create N4 segment in the second instance of the N1 loop
            oX12Parser.SetValue("N4.1", "NEW YORK", 2);     //City Name
 
			//IN1 - INDIVIDUAL IDENTIFICATION
			oX12Parser.SetValue("IN1.1","1");     //Entity Type Qualifier
			oX12Parser.SetValue("IN1.2","04");     //Name Type Code
 
			//IN2 - INDIVIDUAL NAME STRUCTURE COMPONENTS
			oX12Parser.SetValue("IN2.1","05");     //Name Component Qualifier
			oX12Parser.SetValue("IN2.2","DOE");     //Name
 
			//IN2 - INDIVIDUAL NAME STRUCTURE COMPONENTS
			oX12Parser.SetValue("IN1.1","02",2);     //Name Component Qualifier
			oX12Parser.SetValue("IN1.2","MARY",2);     //Name
 
			//IN2 - INDIVIDUAL NAME STRUCTURE COMPONENTS
			oX12Parser.SetValue("IN2.1","15",2);     //Name Component Qualifier
			oX12Parser.SetValue("IN2.2","J",2);     //Name
 
			//SST - STUDENT ACADEMIC STATUS
			oX12Parser.SetValue("SST.1","B18");     //Status Reason Code
			oX12Parser.SetValue("SST.2","D8");     //Date Time Period Format Qualifier
			oX12Parser.SetValue("SST.3","20131215");     //Date Time Period
 
			//N1 - NAME
			oX12Parser.SetValue("N1.1","HS",3);     //Entity Identifier Code
			oX12Parser.SetValue("N1.2","ST MARY'S HIGH SCHOOL",3);     //Name
 
			//N4 - GEOGRAPHIC LOCATION
			oX12Parser.SetValue("N4.1","CARSON",3);     //City Name
			oX12Parser.SetValue("N4.2","CA",3);     //State or Province Code
 
			for (int nAtvLoop = 1; nAtvLoop <= 2; nAtvLoop++)	//number of activities
			{
				//ATV - STUDENT ACTIVITIES AND AWARDS
                oX12Parser.SetValue("ATV.3", "ATHLETE OF THE YEAR 2013", nAtvLoop);     //Entity Title
 
				//DTP - DATE OR TIME OR PERIOD
                oX12Parser.SetValue("DTP.1", "103", nAtvLoop);     //Date/Time Qualifier
                oX12Parser.SetValue("DTP.2", "D8", nAtvLoop);     //Date Time Period Format Qualifier
                oX12Parser.SetValue("DTP.3", "19871130", nAtvLoop);     //Date Time Period
			}
  
			for (int nTstLoop = 1; nTstLoop <=3; nTstLoop++)	//number of tests
			{
				//TST - TEST SCORE RECORD
                oX12Parser.SetValue("TST.1", "CPE", nTstLoop);     //Educational Test or Requirement Code
                oX12Parser.SetValue("TST.2", "CERTIFIED PRIMARY EDU", nTstLoop);     //Name
                oX12Parser.SetValue("TST.3", "D8", nTstLoop);     //Date Time Period Format Qualifier
                oX12Parser.SetValue("TST.4", "19991128", nTstLoop);     //Date Time Period
                oX12Parser.SetValue("TST.7", "07", nTstLoop);     //Level of Individual, Test, or Course Code
 
				//SBT - SUBTEST
                oX12Parser.SetValue("SBT.1", "TOTAL", nTstLoop);     //Subtest Code
 
				//SRE - TEST SCORES
                oX12Parser.SetValue("SRE.1", "3", nTstLoop);     //Test Score Qualifier Code
                oX12Parser.SetValue("SRE.2", "ABA", nTstLoop);     //Description
			}

			//LX - ASSIGNED NUMBER
			oX12Parser.SetValue("LX.1","123456");     //Assigned Number
 
			//HS - HEALTH SCREENING
			oX12Parser.SetValue("HS.1","IDIDID");     //Health Screening Type Code
			oX12Parser.SetValue("HS.2","CC");     //Date Time Period Format Qualifier
			oX12Parser.SetValue("HS.3","A1B2C3D4E5");     //Date Time Period
			oX12Parser.SetValue("HS.4","001");     //Status Reason Code
 
			//IMM - IMMUNIZATION STATUS CODE
			//create first instance of IMM segment in LX loop
			oX12Parser.SetValue("IMM.1","FLU");     //Immunization Type Code
			oX12Parser.SetValue("IMM.2","D8");     //Date Time Period Format Qualifier
			oX12Parser.SetValue("IMM.3","19881128");     //Date Time Period
			oX12Parser.SetValue("IMM.4","1");     //Immunization Status Code
 
			//IMM - IMMUNIZATION STATUS CODE
			//create second instance of IMM segment in LX loop
			oX12Parser.SetValue("IMM.1","TETANU",2);     //Immunization Type Code
			oX12Parser.SetValue("IMM.2","D8",2);     //Date Time Period Format Qualifier
			oX12Parser.SetValue("IMM.3","19900219",2);     //Date Time Period
			oX12Parser.SetValue("IMM.4","1",2);     //Immunization Status Code
 
			//IMM - IMMUNIZATION STATUS CODE
			//create third instance of IMM segment in LX loop
			oX12Parser.SetValue("IMM.1","MUMPS",3);     //Immunization Type Code
			oX12Parser.SetValue("IMM.2","D8",3);     //Date Time Period Format Qualifier
			oX12Parser.SetValue("IMM.3","19950504",3);     //Date Time Period
			oX12Parser.SetValue("IMM.4","1",3);     //Immunization Status Code
 
			for (int nSesLoop = 1; nSesLoop >= 2; nSesLoop++)	//number of sessions
			{
				//SES - ACADEMIC SESSION HEADER
				//create SES segment in SES loop nested in LX loop
                oX12Parser.SetValue("SES.1", "20010407", nSesLoop);     //Date Time Period
                oX12Parser.SetValue("SES.4", "4", nSesLoop);     //Session Code
                oX12Parser.SetValue("SES.5", "SPRING QUARTER 2001", nSesLoop);     //Name
                oX12Parser.SetValue("SES.6", "D8", nSesLoop);     //Date Time Period Format Qualifier
                oX12Parser.SetValue("SES.7", "20130407", nSesLoop);     //Date Time Period
                oX12Parser.SetValue("SES.8", "D8", nSesLoop);     //Date Time Period Format Qualifier
                oX12Parser.SetValue("SES.9", "20130630", nSesLoop);     //Date Time Period
                oX12Parser.SetValue("SES.10", "21", nSesLoop);     //Level of Individual, Test, or Course Code
                oX12Parser.SetValue("SES.14", "B35", nSesLoop);     //Status Reason Code

				//SSE - ENTRY AND EXIT INFORMATION
				//create SSE segment in SES loop nested in LX loop
                oX12Parser.SetValue("SSE.14", "20051231", nSesLoop);      //Date

				for (int nCrsLoop = 1; nCrsLoop >= 2; nCrsLoop++)	//number of courses in a session
				{
					//CRS - COURSE RECORD
					//create CRS segment in CRS loop nested in SES loop nested in LX loop
                    oX12Parser.SetValue("CRS.1", "R", nCrsLoop);     //Basis for Academic Credit Code
                    oX12Parser.SetValue("CRS.2", "U", nCrsLoop);     //Academic Credit Type Code
                    oX12Parser.SetValue("CRS.5", "GRD", nCrsLoop);     //Academic Grade Qualifier
                    oX12Parser.SetValue("CRS.6", "AB", nCrsLoop);     //Academic Grade
                    oX12Parser.SetValue("CRS.8", "U", nCrsLoop);     //Academic Grade or Course Level Code
                    oX12Parser.SetValue("CRS.12", "12", nCrsLoop);     //Quantity
                    oX12Parser.SetValue("CRS.14", "BEGIN MATH", nCrsLoop);     //Name
                    oX12Parser.SetValue("CRS.15", "MAT101", nCrsLoop);     //Reference Identification
                    oX12Parser.SetValue("CRS.16", "MATH", nCrsLoop);     //Name

					//create NTE segment in CRS loop nested in SES loop nested in LX loop
                    oX12Parser.SetValue("NTE.2", "INSTRUCTOR      JOE", nCrsLoop);     //Basis for Academic Credit Code

				}
			}
 

			Cursor = Cursors.Default;
            //THE TO AN EDI FILE.
            string sFilePath = Application.StartupPath + sEdiFile;

            StreamWriter ostreamwritter;
            ostreamwritter = System.IO.File.CreateText(sFilePath);

            ostreamwritter.WriteLine(oX12Parser.Message());
            ostreamwritter.Close();

            //Display EDI string
            System.Windows.Forms.MessageBox.Show(oX12Parser.Message(), "EDI 130 4010");
		
		}
	}
}
