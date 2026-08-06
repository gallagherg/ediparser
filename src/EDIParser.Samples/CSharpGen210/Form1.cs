using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;


namespace CSharpGen210
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
            this.btnGenerate.Location = new System.Drawing.Point(72, 120);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(136, 40);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "This is just an example program to demonstrate how to generate an EDI X12 210 fil" +
                "e using the EDIParser.NET component in C#";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(284, 212);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Name = "Form1";
            this.Text = "Generate EDI X12 210 Example";
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
            //This is just an example program to demonstrate how to generate and validate an 810 EDI file 
            //in C# using the EDIParser.NET component

            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            EDIParser.X12Parser oX12Parser = new EDIParser.X12Parser();

            //SET TERMINATORS
            oX12Parser.SegmentSeparator = "~\r\n";
            oX12Parser.FieldSeparator = "*";
            oX12Parser.ComponentSeparator = ">";


			//CREATES THE ISA SEGMENT
			oX12Parser.SetValue("ISA.1","00");     //Authorization Information Qualifier
			oX12Parser.SetValue("ISA.2","          ");     //Authorization Information
			oX12Parser.SetValue("ISA.3","00");     //Security Information Qualifier
			oX12Parser.SetValue("ISA.4","          ");     //Security Information
			oX12Parser.SetValue("ISA.5","ZZ");     //Interchange ID Qualifier
			oX12Parser.SetValue("ISA.6","AABB           ");     //Interchange Sender ID
			oX12Parser.SetValue("ISA.7","01");     //Interchange ID Qualifier
			oX12Parser.SetValue("ISA.8","112233445      ");     //Interchange Receiver ID
			oX12Parser.SetValue("ISA.9","080304");     //Interchange Date
			oX12Parser.SetValue("ISA.10","1116");     //Interchange Time
			oX12Parser.SetValue("ISA.11","U");     //Interchange Control Standards Identifier
			oX12Parser.SetValue("ISA.12","00307");     //Interchange Control Version Number
			oX12Parser.SetValue("ISA.13","000017859");     //Interchange Control Number
			oX12Parser.SetValue("ISA.14","0");     //Acknowledgment Requested
			oX12Parser.SetValue("ISA.15","T");     //Usage Indicator
			oX12Parser.SetValue("ISA.16",">");     //Component Element Separator
 
			//CREATES THE GS SEGMENT
			oX12Parser.SetValue("GS.1","IM");     //Functional Identifier Code
			oX12Parser.SetValue("GS.2","AABB");     //Application Sender's Code
			oX12Parser.SetValue("GS.3","112233445");     //Application Receiver's Code
			oX12Parser.SetValue("GS.4","080304");     //Date
			oX12Parser.SetValue("GS.5","1116");     //Time
			oX12Parser.SetValue("GS.6","1");     //Group Control Number
			oX12Parser.SetValue("GS.7","X");     //Responsible Agency Code
			oX12Parser.SetValue("GS.8","003070");     //Version / Release / Industry Identifier Code
 
			//CREATES THE ST SEGMENT
			oX12Parser.SetValue("ST.1","210");     //Transaction Set Identifier Code
			oX12Parser.SetValue("ST.2","0001");     //Transaction Set Control Number
 
			//B3 - BEGINNING SEGMENT FOR CARRIER'S INVOICE
			oX12Parser.SetValue("B3.2","1538260");     //Invoice Number
			oX12Parser.SetValue("B3.4","DE");     //Shipment Method of Payment
			oX12Parser.SetValue("B3.6","080304");     //Date
			oX12Parser.SetValue("B3.7","190520");     //Net Amount Due
			oX12Parser.SetValue("B3.11","AABB");     //Standard Carrier Alpha Code
			oX12Parser.SetValue("B3.12","200803");     //Date
 
			//B2A - SET PURPOSE
			oX12Parser.SetValue("B2A.1","00");     //Transaction Set Purpose Code
 
			//N9 - REFERENCE IDENTIFICATION
			oX12Parser.SetValue("N9.1","CN");     //Reference Identification Qualifier
			oX12Parser.SetValue("N9.2","338131");     //Reference Identification
 
			//G62 - DATE/TIME
			oX12Parser.SetValue("G62.1","03");     //Date Qualifier
			oX12Parser.SetValue("G62.2","080304");     //Date
			oX12Parser.SetValue("G62.3","0");     //Time Qualifier
			oX12Parser.SetValue("G62.4","1116");     //Time
			oX12Parser.SetValue("G62.5","LT");     //Time Code
 
			// SHIPPER INFORMATION LOOP
			//N1 - NAME
			oX12Parser.SetValue("N1.1","SH");     //Entity Identifier Code
			oX12Parser.SetValue("N1.2","HARDWARE BIZ");     //Name
			oX12Parser.SetValue("N1.3","1");     //Identification Code Qualifier
			oX12Parser.SetValue("N1.4","006932917");     //Identification Code
 
			//N2 - ADDITIONAL NAME INFORMATION
			oX12Parser.SetValue("N2.1","99448855");     //Name
 
			//N3 - ADDRESS INFORMATION
			oX12Parser.SetValue("N3.1","200 HIGHWAY ST");     //Address Information
 
			//N4 - GEOGRAPHIC LOCATION
			oX12Parser.SetValue("N4.1","LONGBEACH");     //City Name
			oX12Parser.SetValue("N4.2","CA");     //State or Province Code
			oX12Parser.SetValue("N4.3","37027");     //Postal Code
 
			//BILL-TO INFORMATION LOOP
			//N1 - NAME
			oX12Parser.SetValue("N1.1","BT", 2);     //Entity Identifier Code
            oX12Parser.SetValue("N1.2", "OFFICE HQ", 2);     //Name
            oX12Parser.SetValue("N1.3", "1", 2);     //Identification Code Qualifier
            oX12Parser.SetValue("N1.4", "PP22BBCC66", 2);     //Identification Code
 
			//N2 - ADDITIONAL NAME INFORMATION
            oX12Parser.SetValue("N2.1", "HQ99999999", 2);     //Name
 
			//N3 - ADDRESS INFORMATION
            oX12Parser.SetValue("N3.1", "123 CENTER DR", 2);     //Address Information
 
			//N4 - GEOGRAPHIC LOCATION
            oX12Parser.SetValue("N4.1", "LOS ANGELES", 2);     //City Name
            oX12Parser.SetValue("N4.2", "CA", 2);     //State or Province Code
            oX12Parser.SetValue("N4.3", "46064", 2);     //Postal Code
 
			//N7 - EQUIPMENT DETAILS
			oX12Parser.SetValue("N7.1","EIS");     //Equipment Initial
			oX12Parser.SetValue("N7.2","803126");     //Equipment Number
			oX12Parser.SetValue("N7.18","6");     //Equipment Number Check Digit
 
			//LX - ASSIGNED NUMBER
			oX12Parser.SetValue("LX.1","1");     //Assigned Number
 
			//N9 - REFERENCE IDENTIFICATION
			oX12Parser.SetValue("N9.1","F9");     //Reference Identification Qualifier
			oX12Parser.SetValue("N9.2","1006494351  CommodityCode");     //Reference Identification
 
			//L1 - RATE AND CHARGES
			oX12Parser.SetValue("L1.2","1430.00");     //Freight Rate
			oX12Parser.SetValue("L1.3","FR");     //Rate/Value Qualifier
			oX12Parser.SetValue("L1.4","143000");     //Charge
			oX12Parser.SetValue("L1.12","HANDLING");     //Special Charge Description
 
			//LX - ASSIGNED NUMBER
            oX12Parser.SetValue("LX.1", "2", 2);     //Assigned Number
 
			//L1 - RATE AND CHARGES
            oX12Parser.SetValue("L1.2", "72.00", 2);     //Freight Rate
            oX12Parser.SetValue("L1.3", "FR", 2);     //Rate/Value Qualifier
            oX12Parser.SetValue("L1.4", "7200", 2);     //Charge
            oX12Parser.SetValue("L1.12", "EARLY", 2);     //Special Charge Description
 
			//LX - ASSIGNED NUMBER
			oX12Parser.SetValue("LX.1","3", 3);     //Assigned Number
 
			//L1 - RATE AND CHARGES
            oX12Parser.SetValue("L1.2", "403.20", 3);     //Freight Rate
            oX12Parser.SetValue("L1.3", "FR", 3);     //Rate/Value Qualifier
            oX12Parser.SetValue("L1.4", "40320", 3);     //Charge
            oX12Parser.SetValue("L1.12", "LATE", 3);     //Special Charge Description
 
            //THE TO AN EDI FILE.

            string sFilePath = Application.StartupPath + "\\210OUTPUT.X12";

            StreamWriter ostreamwritter;
            ostreamwritter = System.IO.File.CreateText(sFilePath);

            ostreamwritter.WriteLine(oX12Parser.Message());
            ostreamwritter.Close();

            //Display EDI string
            System.Windows.Forms.MessageBox.Show(oX12Parser.Message(), "EDI 210_4040");

            MessageBox.Show("Done. Output = " + sFilePath);		

		}
	}
}
