using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using EDIParser;

namespace CsharpTran130
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        static string sN1LoopQlfr = "";
        static int nArea = 0;

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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(24, 16);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(264, 199);
            this.listBox1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(400, 246);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Translate EDI X12 130 - Async";
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

			string sEdiFile = "130.x12";
			string sPath = AppDomain.CurrentDomain.BaseDirectory;
            listBox1.Items.Clear();
 
			Cursor = Cursors.WaitCursor;

            EDIParser.X12Parser x12parser = null;
            // Initialize the parser.
            x12parser = new EDIParser.X12Parser();
            // setup the parser for asynchronise processing using the ParsedSegment event. 
            x12parser.ParsedSegment += x12parser_ParsedSegment;
            // conserve memory when processing large messages.  
            // This option is only available when used in conjustion with
            // the ParsedSegment event.
            x12parser.ConserveMemory = true;
            x12parser.SegmentParsingOption = EDIParser.Parser.SegmentParsingOptions.InMemory;  //parse segments in memory only

            // Initialize static variables used in x12parser_ParsedSegment event.
            sN1LoopQlfr = "";
            nArea = 0;

            //Load the EDI File into an IO stream.
            System.IO.Stream strEdi = System.IO.File.OpenRead(sPath + sEdiFile);
            byte[] arMsg = new byte[strEdi.Length];
            int nFileLen = Convert.ToInt32(strEdi.Length);

            strEdi.Read(arMsg, 0, nFileLen);
            strEdi.Close();
            string sMsg = null;
            sMsg = System.Text.Encoding.ASCII.GetString(arMsg);
            //parse the message. Note: Actual processing occurs in the 
            //x12parser_ParsedSegment event.  ConserveMemory is turned so
            //the parsed message structure is no longer in memory.
            x12parser.ParseMsg(sMsg);
            // This loop iterates though the EDI file a segment at a time
			Cursor = Cursors.Default;

			MessageBox.Show("Done.");
		
		}
        //x12parser_ParsedSegment is called for each segment that is parsed in the message. ConserveMemory is turned
        //on, which means the segment will only persist within x12parser_ParsedSegment call back function.
        private void x12parser_ParsedSegment(object sender, int SegmentNbr, ref Segment objSegment, ref bool Cancel)
        {
            //This is just an example program to show how to translate an EDI X12 130 Student Educational Record (Transcript)
            //in C# with Framework EDI component
            string sSegmentID;
            string sValue;

            EDIParser.Parser parser = (EDIParser.Parser)sender;
            EDIParser.Segment s = objSegment;
            {
                //DATA SEGMENTS WILL BE IDENTIFIED BY THEIR ID, THE LOOP SECTION AND AREA
                //(OR TABLE) NUMBER THAT THEY ARE IN.
                sSegmentID = s.Name;

                if (sSegmentID == "ST") { nArea = 1; } //header
                if (sSegmentID == "LX") { nArea = 2; } //detail

                if (nArea == 0)
                {
                    if (sSegmentID == "ISA")
                    {
                        sValue = s.Fields[1].Value;     //Authorization Information Qualifier
                        sValue = s.Fields[2].Value;     //Authorization Information
                        sValue = s.Fields[3].Value;     //Security Information Qualifier
                        sValue = s.Fields[4].Value;     //Security Information
                        sValue = s.Fields[5].Value;     //Interchange ID Qualifier
                        sValue = s.Fields[6].Value;     //Interchange Sender ID
                        sValue = s.Fields[7].Value;     //Interchange ID Qualifier
                        sValue = s.Fields[8].Value;     //Interchange Receiver ID
                        sValue = s.Fields[9].Value;     //Interchange Date
                        sValue = s.Fields[10].Value;     //Interchange Time
                        sValue = s.Fields[11].Value;     //Interchange Control Standards Identifier
                        sValue = s.Fields[12].Value;     //Interchange Control Version Number
                        listBox1.Items.Add("Interchange Control Number = " + s.Fields[13].Value);     //Interchange Control Number
                        sValue = s.Fields[14].Value;     //Acknowledgment Requested
                        sValue = s.Fields[15].Value;     //Usage Indicator
                        sValue = s.Fields[16].Value;     //Component Element Separator
                    }
                    else if (sSegmentID == "GS")
                    {
                        sValue = s.Fields[1].Value;     //Functional Identifier Code
                        sValue = s.Fields[2].Value;     //Application Sender's Code
                        sValue = s.Fields[3].Value;     //Application Receiver's Code
                        sValue = s.Fields[4].Value;     //Date
                        sValue = s.Fields[5].Value;     //Time
                        listBox1.Items.Add("Group Control Number = " + s.Fields[6].Value);     //Group Control Number
                        sValue = s.Fields[7].Value;     //Responsible Agency Code
                        sValue = s.Fields[8].Value;     //Version / Release / Industry Identifier Code
                    }   //sSegmentID
                }
                else if (nArea == 1)
                {
                    if (sSegmentID == "ST")
                    {
                        sValue = s.Fields[1].Value;     //Transaction Set Identifier Code
                        listBox1.Items.Add("Transaction Set Control Number = " + s.Fields[2].Value);     //Transaction Set Control Number
                    }
                    else if (sSegmentID == "BGN")
                    {
                        sValue = s.Fields[1].Value;     //Transaction Set Purpose Code
                        listBox1.Items.Add("Reference Identification = " + s.Fields[2].Value);     //Reference Identification
                        sValue = s.Fields[3].Value;     //Date
                        sValue = s.Fields[4].Value;     //Time
                        sValue = s.Fields[5].Value;     //Time Code
                    }
                    else if (sSegmentID == "ERP")
                    {
                        sValue = s.Fields[1].Value;     //Transaction Type Code
                        sValue = s.Fields[2].Value;     //Status Reason Code
                    }
                    else if (sSegmentID == "REF")
                    {
                        sValue = s.Fields[1].Value;     //Reference Identification Qualifier
                        listBox1.Items.Add("Reference Identification = " + s.Fields[2].Value);     //Reference Identification
                    }   //Segment ID
                    //if loop has more that one instance, then you should check for the qualifier that differentiates the loop instances here e.g.
                    if (sSegmentID == "N1")
                    {
                        sN1LoopQlfr = s.Fields[1].Value;   //In most cases the loop qualifier is the first element of the first segment in the loop, but not necessarily
                    }
                    if (sN1LoopQlfr == "AS") //Post Secondary Education Sender
                    {
                        if (sSegmentID == "N1")
                        {
                            sValue = s.Fields[1].Value;     //Entity Identifier Code
                            listBox1.Items.Add("Name Sender = " + s.Fields[2].Value);     //Name
                        }
                        else if (sSegmentID == "N3")
                        {
                            sValue = s.Fields[1].Value;     //Address Information
                        }
                        else if (sSegmentID == "N4")
                        {
                            listBox1.Items.Add("City Sender = " + s.Fields[2].Value);     //City Name
                        }   //Segment ID
                    }
                    else if (sN1LoopQlfr == "AT")	//'Post Secondary Education Receiver
                    {
                        if (sSegmentID == "N1")
                        {
                            sValue = s.Fields[1].Value;     //Entity Identifier Code
                            listBox1.Items.Add("Name Receiver = " + s.Fields[2].Value);     //Name
                        }
                        else if (sSegmentID == "N3")
                        {
                            sValue = s.Fields[1].Value;     //Address Information
                        }
                        else if (sSegmentID == "N4")
                        {
                            listBox1.Items.Add("City Receiver = " + s.Fields[2].Value);     //City Name
                        }   //Segment ID
                    }
                    if (sSegmentID == "IN1")
                    {
                        sValue = s.Fields[1].Value;     //Entity Type Qualifier
                        sValue = s.Fields[2].Value;     //Name Type Code
                    }
                    else if (sSegmentID == "IN2")
                    {
                        sValue = s.Fields[1].Value;     //Name Component Qualifier
                        listBox1.Items.Add("Insurance Name  = " + s.Fields[2].Value);     //Name
                    }   //Segment ID
                    if (sSegmentID == "SST")
                    {
                        sValue = s.Fields[1].Value;     //Status Reason Code
                        sValue = s.Fields[2].Value;     //Date Time Period Format Qualifier
                        listBox1.Items.Add("Date/Time = " + s.Fields[3].Value);     //Date Time Period
                    }
                    else if (sSegmentID == "N1")
                    {
                        sValue = s.Fields[1].Value;     //Entity Identifier Code
                        listBox1.Items.Add("Name = " + s.Fields[2].Value);     //Name
                    }
                    else if (sSegmentID == "N4")
                    {
                        listBox1.Items.Add("City = " + s.Fields[1].Value);     //City Name
                        sValue = s.Fields[2].Value;     //State or Province Code
                    }   //Segment ID
                    if (sSegmentID == "ATV")
                    {
                        sValue = s.Fields[1].Value;     //Code List Qualifier Code
                        sValue = s.Fields[2].Value;     //Industry Code
                        listBox1.Items.Add("Title = " + s.Fields[3].Value);     //Entity Title
                    }
                    else if (sSegmentID == "DTP")
                    {
                        sValue = s.Fields[1].Value;     //Date/Time Qualifier
                        sValue = s.Fields[2].Value;     //Date Time Period Format Qualifier
                        listBox1.Items.Add("Date/Time = " + s.Fields[3].Value);     //Date Time Period
                    }   //Segment ID
                    if (sSegmentID == "TST")
                    {
                        sValue = s.Fields[1].Value;     //Educational Test or Requirement Code
                        listBox1.Items.Add("Test Name = " + s.Fields[2].Value);     //Name
                        sValue = s.Fields[3].Value;     //Date Time Period Format Qualifier
                        sValue = s.Fields[4].Value;     //Date Time Period
                        sValue = s.Fields[5].Value;     //Reference Identification
                        sValue = s.Fields[6].Value;     //Reference Identification
                        sValue = s.Fields[7].Value;     //Level of Individual, Test, or Course Code
                    }   //Segment ID
                    if (sSegmentID == "SBT")
                    {
                        listBox1.Items.Add("Sub Test Code = " + s.Fields[1].Value);     //Subtest Code
                    }
                    else if (sSegmentID == "SRE")
                    {
                        sValue = s.Fields[1].Value;     //Test Score Qualifier Code
                        listBox1.Items.Add("Test Score Qualifier Code = " + s.Fields[2].Value);     //Description
                    }   //sSegmentID
                }
                else if (nArea == 2)
                {

                    if (sSegmentID == "LX")
                    {
                        sValue = s.Fields[1].Value;     //Assigned Number
                    }
                    else if (sSegmentID == "HS")
                    {
                        sValue = s.Fields[1].Value;     //Health Screening Type Code
                        sValue = s.Fields[2].Value;     //Date Time Period Format Qualifier
                        sValue = s.Fields[3].Value;     //Date Time Period
                        sValue = s.Fields[4].Value;     //Status Reason Code
                    }
                    else if (sSegmentID == "IMM")
                    {
                        sValue = s.Fields[1].Value;     //Immunization Type Code
                        sValue = s.Fields[2].Value;     //Date Time Period Format Qualifier
                        sValue = s.Fields[3].Value;     //Date Time Period
                        sValue = s.Fields[4].Value;     //Immunization Status Code
                    }   //Segment ID
                    if (sSegmentID == "SES")
                    {
                        sValue = s.Fields[1].Value;     //Date Time Period
                        sValue = s.Fields[2].Value;     //Count
                        sValue = s.Fields[3].Value;     //Date Time Period
                        listBox1.Items.Add("Session Code = " + s.Fields[4].Value);     //Session Code
                        sValue = s.Fields[5].Value;     //Name
                        sValue = s.Fields[6].Value;     //Date Time Period Format Qualifier
                        sValue = s.Fields[7].Value;     //Date Time Period
                        sValue = s.Fields[8].Value;     //Date Time Period Format Qualifier
                        sValue = s.Fields[9].Value;     //Date Time Period
                        sValue = s.Fields[10].Value;     //Level of Individual, Test, or Course Code
                        sValue = s.Fields[11].Value;     //Identification Code Qualifier
                        sValue = s.Fields[12].Value;     //Identification Code
                        listBox1.Items.Add(" Session Name = " + s.Fields[13].Value);     //Name
                        sValue = s.Fields[14].Value;     //Status Reason Code
                    }   //Segment ID
                    if (sSegmentID == "CRS")
                    {
                        sValue = s.Fields[1].Value;     //Basis for Academic Credit Code
                        sValue = s.Fields[2].Value;     //Academic Credit Type Code
                        sValue = s.Fields[3].Value;     //Quantity
                        sValue = s.Fields[4].Value;     //Quantity
                        sValue = s.Fields[5].Value;     //Academic Grade Qualifier
                        sValue = s.Fields[6].Value;     //Academic Grade
                        sValue = s.Fields[7].Value;     //Yes/No Condition or Response Code
                        sValue = s.Fields[8].Value;     //Academic Grade or Course Level Code
                        sValue = s.Fields[9].Value;     //Course Repeat or No Count Indicator Code
                        sValue = s.Fields[10].Value;     //Identification Code Qualifier
                        sValue = s.Fields[11].Value;     //Identification Code
                        sValue = s.Fields[12].Value;     //Quantity
                        sValue = s.Fields[13].Value;     //Level of Individual, Test, or Course Code
                        listBox1.Items.Add("Credit Name = " + s.Fields[14].Value);     //Name
                        sValue = s.Fields[15].Value;     //Reference Identification
                        sValue = s.Fields[16].Value;     //Name
                    }   //sSegmentID
                }   //nArea
            }   //end 
            s = null;
            parser = null;
        }
	}
}
