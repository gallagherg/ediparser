using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;


namespace Gen810
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmGen810Sample : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnGenerate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtInvoiceNo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listBoxErrors;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;

		public frmGen810Sample()
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
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxErrors = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(280, 72);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(104, 32);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(536, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "This is just an example program to demonstrate how to generate and validate an 81" +
                "0 EDI file in C#  using the EDI Parser.NET component";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(24, 80);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(100, 20);
            this.txtInvoiceNo.TabIndex = 2;
            this.txtInvoiceNo.Text = "123456";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Invoice No:";
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Location = new System.Drawing.Point(16, 144);
            this.listBoxErrors.Name = "listBoxErrors";
            this.listBoxErrors.Size = new System.Drawing.Size(544, 108);
            this.listBoxErrors.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Errors:";
            // 
            // frmGen810Sample
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(576, 270);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxErrors);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInvoiceNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Name = "frmGen810Sample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDI Generate 810 Sample with C#.NET";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmGen810Sample());
		}


		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
			//This is just an example program to demonstrate how to generate and validate an 810 EDI file 
			//in C# using the EDIParser.NET component

            int nmSegCnt = 1;
            int it1SegCnt = 1;
            int pidSegCnt = 1;
            string sEdiFile;

            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            EDIParser.X12Parser oX12Parser = new EDIParser.X12Parser();

            //SET TERMINATORS
            oX12Parser.SegmentSeparator = "~\r\n";
            oX12Parser.FieldSeparator = "*";
            oX12Parser.ComponentSeparator = ">";

			sEdiFile = "810OUTPUT.X12";
 
			//CREATES THE ISA SEGMENT
			oX12Parser.SetValue("ISA.1","00");     //Authorization Information Qualifier
			oX12Parser.SetValue("ISA.2","          ");     //Authorization Information
			oX12Parser.SetValue("ISA.3","00");     //Security Information Qualifier
			oX12Parser.SetValue("ISA.4","          ");     //Security Information
			oX12Parser.SetValue("ISA.5","ZZ");     //Interchange ID Qualifier
			oX12Parser.SetValue("ISA.6","SENDERISA      ");     //Interchange Sender ID
			oX12Parser.SetValue("ISA.7","ZZ");     //Interchange ID Qualifier
			oX12Parser.SetValue("ISA.8","RECEIVERISA    ");     //Interchange Receiver ID
			oX12Parser.SetValue("ISA.9","960807");     //Interchange Date
			oX12Parser.SetValue("ISA.10","1548");     //Interchange Time
			oX12Parser.SetValue("ISA.11","U");     //Interchange Control Standards Identifier
			oX12Parser.SetValue("ISA.12","00401");     //Interchange Control Version Number
			oX12Parser.SetValue("ISA.13","000000020");     //Interchange Control Number
			oX12Parser.SetValue("ISA.14","0");     //Acknowledgment Requested
			oX12Parser.SetValue("ISA.15","T");     //Usage Indicator
			oX12Parser.SetValue("ISA.16",">");     //Component Element Separator
 
			//CREATES THE GS SEGMENT
			oX12Parser.SetValue("GS.1","IN");     //Functional Identifier Code
			oX12Parser.SetValue("GS.2","SENDERDEPT");     //Application Sender's Code
			oX12Parser.SetValue("GS.3","007326879");     //Application Receiver's Code
			oX12Parser.SetValue("GS.4","19960807");     //Date
			oX12Parser.SetValue("GS.5","1548");     //Time
			oX12Parser.SetValue("GS.6","1");     //Group Control Number
			oX12Parser.SetValue("GS.7","X");     //Responsible Agency Code
			oX12Parser.SetValue("GS.8","004010");     //Version / Release / Industry Identifier Code
 
			//CREATES THE ST SEGMENT
			oX12Parser.SetValue("ST.1","810");     //Transaction Set Identifier Code
			oX12Parser.SetValue("ST.2","000000001");     //Transaction Set Control Number
 
			//BIG - BEGINNING SEGMENT FOR INVOICE
			//oX12Parser.SetValue(1","19971211");     //Date		//this is a mandatory element, which will generata an error if omitted
			oX12Parser.SetValue("BIG.2",txtInvoiceNo.Text );     //Invoice Number
			oX12Parser.SetValue("BIG.4","A99999-01");     //Purchase Order Number
 
			//SHIP-TO INFORMATION LOOP
			//N1 - NAME
			oX12Parser.SetValue("N1.1","ST",nmSegCnt);     //Entity Identifier Code
			oX12Parser.SetValue("N1.2","BUYSNACKS PORT",nmSegCnt);     //Name
			oX12Parser.SetValue("N1.3","9",nmSegCnt);     //Identification Code Qualifier
			oX12Parser.SetValue("N1.4","1223334445",nmSegCnt);     //Identification Code
 
			//N3 - ADDRESS INFORMATION
			oX12Parser.SetValue("N3.1","1000 N. SAMPLE HIGHWAY",nmSegCnt);     //Address Information
 
			//N4 - GEOGRAPHIC LOCATION
			oX12Parser.SetValue("N4.1","ATHENS",nmSegCnt);     //City Name
			oX12Parser.SetValue("N4.2","GA",nmSegCnt);     //State or Province Code
			oX12Parser.SetValue("N4.3","30603",nmSegCnt);     //Postal Code
 
			//BILL-TO INFORMATION LOOP
			//N1 - NAME
            nmSegCnt +=1;
			oX12Parser.SetValue("N1.1","BT",nmSegCnt);     //Entity Identifier Code
			oX12Parser.SetValue("N1.2","BUYSNACKS",nmSegCnt);     //Name
			oX12Parser.SetValue("N1.3","9",nmSegCnt);     //Identification Code Qualifier
			oX12Parser.SetValue("N1.4","1223334444",nmSegCnt);     //Identification Code
 
			//N3 - ADDRESS INFORMATION
			oX12Parser.SetValue("N3.1","P.O. BOX 0000",nmSegCnt);     //Address Information
 
			//N4 - GEOGRAPHIC LOCATION
			oX12Parser.SetValue("N4.1","TEMPLE",nmSegCnt);     //City Name
			oX12Parser.SetValue("N4.2","TX",nmSegCnt);     //State or Province Code
			oX12Parser.SetValue("N4.3","76503",nmSegCnt);     //Postal Code
 
			//REMIT-TO INFORMATION
			//N1 - NAME
            nmSegCnt +=1;
			oX12Parser.SetValue("N1.1","RE",nmSegCnt);     //Entity Identifier Code
			oX12Parser.SetValue("N1.2","FOODSELLER",nmSegCnt);     //Name
			oX12Parser.SetValue("N1.3","9",nmSegCnt);     //Identification Code Qualifier
			oX12Parser.SetValue("N1.4","12345QQQQ",nmSegCnt);     //Identification Code
 
			//N3 - ADDRESS INFORMATION
			oX12Parser.SetValue("N3.1","P.O. BOX 222222",nmSegCnt);     //Address Information
 
			//N4 - GEOGRAPHIC LOCATION
			oX12Parser.SetValue("N4.1","DALLAS",nmSegCnt);     //City Name
			oX12Parser.SetValue("N4.2","TX",nmSegCnt);     //State or Province Code
			oX12Parser.SetValue("N4.3","723224444",nmSegCnt);     //Postal Code
 
			//ITD - TERMS OF SALE/DEFERRED TERMS OF SALE
			oX12Parser.SetValue("ITD.1","01");     //Terms Type Code
			oX12Parser.SetValue("ITD.2","3");     //Terms Basis Date Code
			oX12Parser.SetValue("ITD.3","1.000");     //Terms Discount Percent
			oX12Parser.SetValue("ITD.5","15");     //Terms Discount Days Due
			oX12Parser.SetValue("ITD.7","16");     //Terms Net Days
			oX12Parser.SetValue("ITD.12","1/15 NET 30");     //Description
 
			//FOB - F.O.B. RELATED INSTRUCTIONS
			oX12Parser.SetValue("FOB.1","PP");     //Shipment Method of Payment
 
			//IT1 - BASELINE ITEM DATA (INVOICE)
			oX12Parser.SetValue("IT1.2","16", it1SegCnt );     //Quantity Invoiced
			oX12Parser.SetValue("IT1.3","CA", it1SegCnt);     //Unit or Basis for Measurement Code
			oX12Parser.SetValue("IT1.4","12.34", it1SegCnt);     //Unit Price
			oX12Parser.SetValue("IT1.6","UA", it1SegCnt);     //Product/Service ID Qualifier
			oX12Parser.SetValue("IT1.7","002840022222", it1SegCnt);     //Product/Service ID
 
			//PID - PRODUCT/ITEM DESCRIPTION
			oX12Parser.SetValue("PID.1","F", pidSegCnt );     //Item Description Type
			oX12Parser.SetValue("PID.5","CRUNCHY CHIPS LSS", pidSegCnt);     //Description
 
			//IT1 - BASELINE ITEM DATA (INVOICE)
            it1SegCnt +=1;
			oX12Parser.SetValue("IT1.2","13", it1SegCnt);     //Quantity Invoiced
			oX12Parser.SetValue("IT1.3","CA", it1SegCnt);     //Unit or Basis for Measurement Code
			oX12Parser.SetValue("IT1.4","12.34", it1SegCnt);     //Unit Price
			oX12Parser.SetValue("IT1.6","UA", it1SegCnt);     //Product/Service ID Qualifier
			oX12Parser.SetValue("IT1.7","002840033333", it1SegCnt);     //Product/Service ID
 
			//PID - PRODUCT/ITEM DESCRIPTION
            pidSegCnt +=1;
			oX12Parser.SetValue("PID.1","F", pidSegCnt);     //Item Description Type
			oX12Parser.SetValue("PID.5","NACHO CHIPS LSS", pidSegCnt);     //Description
 
			//IT1 - BASELINE ITEM DATA (INVOICE)
            it1SegCnt +=1;
			oX12Parser.SetValue("IT1.2","32", it1SegCnt);     //Quantity Invoiced
			oX12Parser.SetValue("IT1.3","CA", it1SegCnt);     //Unit or Basis for Measurement Code
			oX12Parser.SetValue("IT1.4","12.34", it1SegCnt);     //Unit Price
			oX12Parser.SetValue("IT1.6","UA", it1SegCnt);     //Product/Service ID Qualifier
			oX12Parser.SetValue("IT1.7","002840044444", it1SegCnt);     //Product/Service ID
 
			//PID - PRODUCT/ITEM DESCRIPTION
            pidSegCnt +=1;
			oX12Parser.SetValue("PID.1","F",pidSegCnt);     //Item Description Type
			oX12Parser.SetValue("PID.5","POTATO CHIPS", pidSegCnt);     //Description
 
			//IT1 - BASELINE ITEM DATA (INVOICE)
            it1SegCnt +=1;
			oX12Parser.SetValue("IT1.2","51", it1SegCnt);     //Quantity Invoiced
			oX12Parser.SetValue("IT1.3","CA", it1SegCnt);     //Unit or Basis for Measurement Code
			oX12Parser.SetValue("IT1.4","12.34", it1SegCnt);     //Unit Price
			oX12Parser.SetValue("IT1.6","UA", it1SegCnt);     //Product/Service ID Qualifier
			oX12Parser.SetValue("IT1.7","002840055555", it1SegCnt);     //Product/Service ID
 
			//PID - PRODUCT/ITEM DESCRIPTION
            pidSegCnt +=1;
			oX12Parser.SetValue("PID.1","F", pidSegCnt);     //Item Description Type
			oX12Parser.SetValue("PID.5","CORN CHIPS", pidSegCnt);     //Description
 
			//IT1 - BASELINE ITEM DATA (INVOICE)
            it1SegCnt +=1;
			oX12Parser.SetValue("IT1.2","9", it1SegCnt);     //Quantity Invoiced
			oX12Parser.SetValue("IT1.3","CA", it1SegCnt);     //Unit or Basis for Measurement Code
			oX12Parser.SetValue("IT1.4","12.34", it1SegCnt);     //Unit Price
			oX12Parser.SetValue("IT1.6","UA", it1SegCnt);     //Product/Service ID Qualifier
			oX12Parser.SetValue("IT1.7","002840066666", it1SegCnt);     //Product/Service ID
 
			//PID - PRODUCT/ITEM DESCRIPTION
            pidSegCnt +=1;
			oX12Parser.SetValue("PID.1","F", pidSegCnt );     //Item Description Type
			oX12Parser.SetValue("PID.5","BBQ CHIPS", pidSegCnt );     //Description
 
			//IT1 - BASELINE ITEM DATA (INVOICE)
            it1SegCnt +=1;
			oX12Parser.SetValue("IT1.2","85", it1SegCnt );     //Quantity Invoiced
			oX12Parser.SetValue("IT1.3","CA", it1SegCnt );     //Unit or Basis for Measurement Code
			oX12Parser.SetValue("IT1.4","12.34", it1SegCnt );     //Unit Price
			oX12Parser.SetValue("IT1.6","UA", it1SegCnt );     //Product/Service ID Qualifier
			oX12Parser.SetValue("IT1.7","002840077777", it1SegCnt );     //Product/Service ID
 
			//PID - PRODUCT/ITEM DESCRIPTION
            pidSegCnt +=1;
			oX12Parser.SetValue("PID.1","F", pidSegCnt );     //Item Description Type
			oX12Parser.SetValue("PID.5","GREAT BIG CHIPS LSS", pidSegCnt );     //Description
 
			//IT1 - BASELINE ITEM DATA (INVOICE)
            it1SegCnt +=1;
			oX12Parser.SetValue("IT1.2","1", it1SegCnt );     //Quantity Invoiced
			oX12Parser.SetValue("IT1.3","CA", it1SegCnt );     //Unit or Basis for Measurement Code
			oX12Parser.SetValue("IT1.4","12.34", it1SegCnt );     //Unit Price
			oX12Parser.SetValue("IT1.6","UA", it1SegCnt );     //Product/Service ID Qualifier
			oX12Parser.SetValue("IT1.7","002840088888", it1SegCnt );     //Product/Service ID
 
			//PID - PRODUCT/ITEM DESCRIPTION
            pidSegCnt +=1;
			oX12Parser.SetValue("PID.1","F", pidSegCnt );     //Item Description Type
			oX12Parser.SetValue("PID.5","MINI CHIPS LSS", pidSegCnt );     //Description
 
			//TDS - TOTAL MONETARY VALUE SUMMARY
			oX12Parser.SetValue("TDS.1","255438");     //Amount
 
			//CAD - CARRIER DETAIL
			oX12Parser.SetValue("CAD.5","FREEFORM");     //Routing
 
			//ISS - INVOICE SHIPMENT SUMMARY
			oX12Parser.SetValue("ISS.1","207");     //Number of Units Shipped
			oX12Parser.SetValue("ISS.2","CA");     //Unit or Basis for Measurement Code
 
			//CTT - TRANSACTION TOTALS
			oX12Parser.SetValue("CIT.1","7");     //Number of Line Items


            //THE TO AN EDI FILE.
            string sFilePath = Application.StartupPath + "\\810_4040.txt";

            StreamWriter ostreamwritter;
            ostreamwritter = System.IO.File.CreateText(sFilePath);

            ostreamwritter.WriteLine(oX12Parser.Message());
            ostreamwritter.Close();

            //Display EDI string
            System.Windows.Forms.MessageBox.Show(oX12Parser.Message(), "EDI 810_4040");
 
			MessageBox.Show("Done. Output = " + sPath + sEdiFile);		}

	}
}
