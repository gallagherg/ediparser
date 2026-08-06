using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace cSharpNetGen820
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnGenerate;
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
			this.SuspendLayout();
			// 
			// btnGenerate
			// 
			this.btnGenerate.Location = new System.Drawing.Point(88, 144);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(104, 48);
			this.btnGenerate.TabIndex = 0;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Controls.Add(this.btnGenerate);
			this.Name = "Form1";
			this.Text = "Generate EDI X12 820";
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

            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            EDIParser.X12Parser oX12Parser = new EDIParser.X12Parser();

            //SET TERMINATORS
            oX12Parser.SegmentSeparator = "~\r\n";
            oX12Parser.FieldSeparator = "*";
            oX12Parser.ComponentSeparator = ">";


            //CREATES THE ISA SEGMENT
            oX12Parser.SetValue("ISA.1","00");     //Authorization Information Qualifier
            oX12Parser.SetValue("ISA.2", "          ");     //Authorization Information
            oX12Parser.SetValue("ISA.3", "00");     //Security Information Qualifier
            oX12Parser.SetValue("ISA.4", "          ");     //Security Information
            oX12Parser.SetValue("ISA.5", "12");     //Interchange ID Qualifier
            oX12Parser.SetValue("ISA.6", "SENDERID       ");     //Interchange Sender ID
            oX12Parser.SetValue("ISA.7", "14");     //Interchange ID Qualifier
            oX12Parser.SetValue("ISA.8", "Receiver_ID    ");     //Interchange Receiver ID
            oX12Parser.SetValue("ISA.9", "960807");     //Interchange Date
            oX12Parser.SetValue("ISA.10", "1548");     //Interchange Time
            oX12Parser.SetValue("ISA.11", "U");     //Interchange Control Standards Identifier
            oX12Parser.SetValue("ISA.12", "00401");     //Interchange Control Version Number
            oX12Parser.SetValue("ISA.13", "000000020");     //Interchange Control Number
            oX12Parser.SetValue("ISA.14", "0");     //Acknowledgment Requested
            oX12Parser.SetValue("ISA.15", "T");     //Usage Indicator
            oX12Parser.SetValue("ISA.16", ">");     //Component Element Separator
	
			//CREATES THE GS SEGMENT
			oX12Parser.SetValue("GS.1","RA");     //Functional Identifier Code
            oX12Parser.SetValue("GS.2", "SenderCode");     //Application Sender's Code
            oX12Parser.SetValue("GS.3", "ReceiverCode");     //Application Receiver's Code
            oX12Parser.SetValue("GS.4", "20030101");     //Date
            oX12Parser.SetValue("GS.5", "1530");     //Time
            oX12Parser.SetValue("GS.6", "1");     //Group Control Number
            oX12Parser.SetValue("GS.7", "X");     //Responsible Agency Code
            oX12Parser.SetValue("GS.8", "004010");     //Version / Release / Industry Identifier Code
	
			//CREATES THE ST SEGMENT
			oX12Parser.SetValue("ST.1","820");     //Transaction Set Identifier Code
            oX12Parser.SetValue("ST.2", "000000001");     //Transaction Set Control Number
	
			//BPR - BEGINNING SEGMENT FOR PAYMENT ORDER/REMITTANCE ADVICE
			oX12Parser.SetValue("BPR.1","C");     //Transaction Handling Code
            oX12Parser.SetValue("BPR.2", "1000");     //Monetary Amount
            oX12Parser.SetValue("BPR.3", "C");     //Credit/Debit Flag Code
            oX12Parser.SetValue("BPR.4", "ACH");     //Payment Method Code
            oX12Parser.SetValue("BPR.5", "CTX");     //Payment Format Code
            oX12Parser.SetValue("BPR.6", "01");     //(DFI) ID Number Qualifier
            oX12Parser.SetValue("BPR.7", "FINANCIAL049");     //(DFI) Identification Number
            oX12Parser.SetValue("BPR.8", "2");     //Account Number Qualifier
            oX12Parser.SetValue("BPR.9", "PAYOR980434");     //Account Number
            oX12Parser.SetValue("BPR.10", "CUSTID0001");     //Originating Company Identifier
            oX12Parser.SetValue("BPR.11", "ODFI98364");     //Originating Company Supplemental Code
            oX12Parser.SetValue("BPR.12", "01");     //(DFI) ID Number Qualifier
            oX12Parser.SetValue("BPR.13", "DFI_id_numbe");     //(DFI) Identification Number
            oX12Parser.SetValue("BPR.14", "DA");     //Account Number Qualifier
            oX12Parser.SetValue("BPR.15", "PYE34508");     //Account Number
            oX12Parser.SetValue("BPR.16", "20030101");     //Date
	
			//NTE - NOTE/SPECIAL INSTRUCTION
			oX12Parser.SetValue("NTE.1","AAA");     //Note Reference Code
            oX12Parser.SetValue("NTE.2", "COMMENT");     //Description
	
			//REF - REFERENCE IDENTIFICATION
			oX12Parser.SetValue("REF.1","IA");     //Reference Identification Qualifier
            oX12Parser.SetValue("REF.2", "INTVEND0001");     //Reference Identification
	
			//DTM - DATE/TIME REFERENCE
			oX12Parser.SetValue("DTM.1","097");     //Date/Time Qualifier
            oX12Parser.SetValue("DTM.2", "20030101");     //Date
	
			//N1 - NAME
			oX12Parser.SetValue("N1.1","PE");     //Entity Identifier Code
            oX12Parser.SetValue("N1.2", "PAYEE NAME");     //Name
	
			//N3 - ADDRESS INFORMATION
			oX12Parser.SetValue("N3.1","Payee Address");     //Address Information
	
			//N4 - GEOGRAPHIC LOCATION
			oX12Parser.SetValue("N4.1","Payee City");     //City Name
            oX12Parser.SetValue("N4.2", "CA");     //State or Province Code
            oX12Parser.SetValue("N4.3", "Payee Zip");     //Postal Code
	
			//PER - ADMINISTRATIVE COMMUNICATIONS CONTACT
			oX12Parser.SetValue("PER.1","RE");     //Contact Function Code
            oX12Parser.SetValue("PER.2", "Payees Name");     //Name
            oX12Parser.SetValue("PER.3", "EM");     //Communication Number Qualifier
            oX12Parser.SetValue("PER.4", "Payees email");     //Communication Number
	
			//N1 - NAME
			oX12Parser.SetValue("N1.1","PR",2);     //Entity Identifier Code
            oX12Parser.SetValue("N1.2", "PAYER NAME", 2);     //Name
	
			//N3 - ADDRESS INFORMATION
			oX12Parser.SetValue("N3.1","Payer Address", 2);     //Address Information
	
			//N4 - GEOGRAPHIC LOCATION
			oX12Parser.SetValue("N4.1","Payer City",2);     //City Name
            oX12Parser.SetValue("N4.2", "PA", 2);     //State or Province Code
            oX12Parser.SetValue("N4.3", "Payer Zip",2);     //Postal Code
	
			//N1 - NAME
			oX12Parser.SetValue("N1.1","RB", 3);     //Entity Identifier Code
			oX12Parser.SetValue("N1.2","RECV BANK",3);     //Name
	
			//N3 - ADDRESS INFORMATION
			oX12Parser.SetValue("N3.1","Rec Bank Address", 3);     //Address Information
	
			//N4 - GEOGRAPHIC LOCATION
			oX12Parser.SetValue("N4.1","Rec Bank City", 3);     //City Name
            oX12Parser.SetValue("N4.2", "Re", 3);     //State or Province Code
            oX12Parser.SetValue("N4.3", "Rec Bank Zip", 3);     //Postal Code
	
			//ENT - ENTITY
			oX12Parser.SetValue("ENT.1","1");     //Assigned Number
            oX12Parser.SetValue("ENT.2", "ZZ");     //Entity Identifier Code
            oX12Parser.SetValue("ENT.3", "1");     //Identification Code Qualifier
            oX12Parser.SetValue("ENT.4", "DUNS00692");     //Identification Code
	
			//RMR - REMITTANCE ADVICE ACCOUNTS RECEIVABLE OPEN ITEM REFERENCE
			oX12Parser.SetValue("RMR.1","IV");     //Reference Identification Qualifier
            oX12Parser.SetValue("RMR.2", "INV5189807544");     //Reference Identification
            oX12Parser.SetValue("RMR.4", "5000");     //Monetary Amount
	
			//REF - REFERENCE IDENTIFICATION
			oX12Parser.SetValue("REF.1","SM", 2);     //Reference Identification Qualifier
			oX12Parser.SetValue("REF.2","STORE0544", 2);     //Reference Identification
	
			//DTM - DATE/TIME REFERENCE
			oX12Parser.SetValue("DTM.1","097", 2);     //Date/Time Qualifier
			oX12Parser.SetValue("DTM.2","20090503", 2);     //Date
	
			//DTM - DATE/TIME REFERENCE
			oX12Parser.SetValue("DTM.1","003", 3);     //Date/Time Qualifier
			oX12Parser.SetValue("DTM.2","20021102", 3);     //Date
	
			//RMR - REMITTANCE ADVICE ACCOUNTS RECEIVABLE OPEN ITEM REFERENCE
			oX12Parser.SetValue("RMR.1","IV", 2);     //Reference Identification Qualifier
            oX12Parser.SetValue("RMR.2", "INV5189807545", 2);     //Reference Identification
            oX12Parser.SetValue("RMR.4", "2250", 2);     //Monetary Amount
	
			//REF - REFERENCE IDENTIFICATION
			oX12Parser.SetValue("REF.1","SM", 3);     //Reference Identification Qualifier
            oX12Parser.SetValue("REF.2", "STORE0544", 3);     //Reference Identification
	
			//DTM - DATE/TIME REFERENCE
			oX12Parser.SetValue("DTM.1","097", 4);     //Date/Time Qualifier
			oX12Parser.SetValue("DTM.2","20090219", 4);     //Date
	
			//DTM - DATE/TIME REFERENCE
			oX12Parser.SetValue("DTM.1","003", 5);     //Date/Time Qualifier
            oX12Parser.SetValue("DTM.2", "20021201", 5);     //Date
	
			//RMR - REMITTANCE ADVICE ACCOUNTS RECEIVABLE OPEN ITEM REFERENCE
			oX12Parser.SetValue("RMR.1","IV", 3);     //Reference Identification Qualifier
            oX12Parser.SetValue("RMR.2", "INV5189807546", 3);     //Reference Identification
            oX12Parser.SetValue("RMR.4", "1345", 3);     //Monetary Amount
	
			//REF - REFERENCE IDENTIFICATION
			oX12Parser.SetValue("REF.1","SM", 4);     //Reference Identification Qualifier
            oX12Parser.SetValue("REF.2", "STORE0544", 4);     //Reference Identification
	
			//DTM - DATE/TIME REFERENCE
			oX12Parser.SetValue("DTM.1","097", 6);     //Date/Time Qualifier
            oX12Parser.SetValue("DTM.2", "20090728", 6);     //Date
	
			//DTM - DATE/TIME REFERENCE
			oX12Parser.SetValue("DTM.1","003", 7);     //Date/Time Qualifier
			oX12Parser.SetValue("DTM.2","19990503", 7);     //Date
	
			//TXP - TAX PAYMENT
			oX12Parser.SetValue("TXP.1","TAXID34950");     //Tax Identification Number
            oX12Parser.SetValue("TXP.2", "FTACO");     //Tax Payment Type Code
            oX12Parser.SetValue("TXP.3", "20090712");     //Date
            oX12Parser.SetValue("TXP.4", "INFO345345");     //Tax Information Identification Number
            oX12Parser.SetValue("TXP.5", "500000");     //Tax Amount
	
			//DED - DEDUCTIONS
			oX12Parser.SetValue("DED.1","CS");     //Type of Deduction
            oX12Parser.SetValue("DED.2", "CASE845609");     //Reference Identification
            oX12Parser.SetValue("DED.3", "20090712");     //Date
            oX12Parser.SetValue("DED.4", "500000");     //Amount
            oX12Parser.SetValue("DED.5", "SSN438608469");     //Reference Identification
            oX12Parser.SetValue("DED.6", "Y");     //Yes/No Condition or Response Code
            oX12Parser.SetValue("DED.7", "PARENT NAME");     //Name
            oX12Parser.SetValue("DED.8", "FIPS4368T09");     //Reference Identification
            oX12Parser.SetValue("DED.9", "Y");     //Yes/No Condition or Response Code

            //THE TO AN EDI FILE.
            string sFilePath = Application.StartupPath + "\\820_4040.txt";

            StreamWriter ostreamwritter;
            ostreamwritter = System.IO.File.CreateText(sFilePath);

            ostreamwritter.WriteLine(oX12Parser.Message());
            ostreamwritter.Close();

            //Display EDI string
            System.Windows.Forms.MessageBox.Show(oX12Parser.Message(), "EDI 820_4040");
 
		}
	}
}
