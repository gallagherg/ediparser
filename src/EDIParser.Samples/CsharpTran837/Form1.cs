using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;



namespace CsharpTran837
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtEdifile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdTranslate;
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtEdifile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdTranslate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(16, 64);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(536, 121);
            this.listBox1.TabIndex = 0;
            // 
            // txtEdifile
            // 
            this.txtEdifile.Location = new System.Drawing.Point(144, 208);
            this.txtEdifile.Name = "txtEdifile";
            this.txtEdifile.Size = new System.Drawing.Size(408, 20);
            this.txtEdifile.TabIndex = 1;
            this.txtEdifile.Text = "837_X098.txt";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(528, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "This is an example program to show how one easily use the EDIParser.NET component" +
                " in C# to translate an EDI 837 EDI file. ";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter EDI 837 filename: ";
            // 
            // cmdTranslate
            // 
            this.cmdTranslate.Location = new System.Drawing.Point(184, 256);
            this.cmdTranslate.Name = "cmdTranslate";
            this.cmdTranslate.Size = new System.Drawing.Size(168, 32);
            this.cmdTranslate.TabIndex = 4;
            this.cmdTranslate.Text = "Translate";
            this.cmdTranslate.Click += new System.EventHandler(this.cmdTranslate_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(576, 310);
            this.Controls.Add(this.cmdTranslate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEdifile);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
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
            Application.Run(new Form1());
        }

        private void cmdTranslate_Click(object sender, System.EventArgs e)
        {

            string sNm1Entity = "";
            string sHlEntity = "";
            string sQlfr = "";
            string sValue = "";

            //LOAD'S THE FILE
            System.IO.Stream strEdi = System.IO.File.OpenRead("837_X098.txt");
            //System.IO.Stream strEdi = System.IO.File.OpenRead("837_X098_5010.txt");
            int nFileLen = 0;


            string sEntity = string.Empty;
            string sLXID = string.Empty;
            string sQafr = string.Empty;
            byte[] arMsg = new byte[strEdi.Length];
            nFileLen = Convert.ToInt32(strEdi.Length);

            strEdi.Read(arMsg, 0, nFileLen);
            strEdi.Close();
            string sMsg = null;
            sMsg = System.Text.Encoding.ASCII.GetString(arMsg);

            EDIParser.X12Parser x12parser = new EDIParser.X12Parser();
            x12parser.ParseMsg(sMsg);


            //LOOP THAT WILL TRAVERSE THRU EDI FILE FROM TOP TO BOTTOM
            // This loop iterates though the EDI file a segment at a time
            foreach (EDIParser.Segment s in x12parser.Segments)
            {
                if (s.Name == "NM1")
                {
                    sNm1Entity = ((EDIParser.Field)s.Fields[1]).Value;
                }

                if (s.Name == "ISA")
                {
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Authorization Information Qualifier
                    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Authorization Information
                    sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Security Information Qualifier
                    sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Security Information
                    sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Interchange ID Qualifier
                    sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Interchange Sender ID
                    listBox1.Items.Add("Interchange Sender ID = " + sValue);
                    sValue = ((EDIParser.Field)s.Fields[7]).Value;     //Interchange ID Qualifier
                    sValue = ((EDIParser.Field)s.Fields[8]).Value;    //Interchange Receiver ID
                    listBox1.Items.Add("Interchange Recevier ID = " + sValue);
                    sValue = ((EDIParser.Field)s.Fields[9]).Value;     //Interchange Date
                    sValue = ((EDIParser.Field)s.Fields[10]).Value;     //Interchange Time
                    sValue = ((EDIParser.Field)s.Fields[11]).Value;      //Interchange Control Standards Identifier
                    sValue = ((EDIParser.Field)s.Fields[12]).Value;     //Interchange Control Version Number
                    sValue = ((EDIParser.Field)s.Fields[13]).Value;     //Interchange Control Number
                    listBox1.Items.Add("Interchange Control Number = " + sValue);
                    sValue = ((EDIParser.Field)s.Fields[14]).Value;    //Acknowledgment Requested
                    sValue = ((EDIParser.Field)s.Fields[15]).Value; ;     //Usage Indicator
                    sValue = ((EDIParser.Field)s.Fields[16]).Value;     //Component Element Separator
                }
                else if (s.Name == "GS")
                {
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;      //Functional Identifier Code
                    sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Application Sender's Code
                    sValue = ((EDIParser.Field)s.Fields[3]).Value;      //Application Receiver's Code
                    sValue = ((EDIParser.Field)s.Fields[4]).Value;      //Date
                    sValue = ((EDIParser.Field)s.Fields[5]).Value;      //Time
                    sValue = ((EDIParser.Field)s.Fields[6]).Value;      //Group Control Number
                    listBox1.Items.Add("Group Control Number = " + sValue);
                    sValue = ((EDIParser.Field)s.Fields[7]).Value;      //Responsible Agency Code
                    sValue = ((EDIParser.Field)s.Fields[8]).Value;      //Version / Release / Industry Identifier Code
                }   //sSegmentID


                else if (s.Name == "ST")
                {
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Transaction Set Identifier Code
                    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Transaction Set Control Number
                }
                else if (s.Name == "BHT")
                {
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;      //Hierarchical Structure Code
                    sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Transaction Set Purpose Code
                    sValue = ((EDIParser.Field)s.Fields[3]).Value;      //Reference Identification
                    listBox1.Items.Add("Reference Identification = " + sValue);
                    sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Date
                    listBox1.Items.Add("Date = " + sValue);
                    sValue = ((EDIParser.Field)s.Fields[5]).Value;    //Time
                    sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Transaction Type Code
                }
                else if (s.Name == "REF")
                {
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Reference Identification Qualifier
                    sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Reference Identification
                }   //Segment ID



                else if (sNm1Entity == "41") //SUBMITTER
                {

                    if (s.Name == "NM1")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
                        listBox1.Items.Add("Name Last or Organization Name (Submitter) = " + sValue);
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;    //Name First
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
                        sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
                        sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
                        sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
                        sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code
                    }
                    else if (s.Name == "PER")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Contact Function Code
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Name
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Communication Number Qualifier
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Communication Number
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Communication Number Qualifier
                        sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Communication Number
                    }
                }
                else if (sNm1Entity == "40")	//RECEIVER
                {
                    if (s.Name == "NM1")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
                        listBox1.Items.Add("Name Last or Organization Name (Recevier) = " + sValue);
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;    //Name First
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
                        sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
                        sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
                        sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
                        sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code
                    }
                    sNm1Entity = "";
                }




                else if (s.Name == "HL")
                {
                    sHlEntity = ((EDIParser.Field)s.Fields[3]).Value;
                }



                else if (sHlEntity == "20")	//BILLING PROVIDER
                {


                    if (sNm1Entity == "85") //BILLING PROVIDER NAME
                    {
                        if (s.Name == "NM1")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
                            listBox1.Items.Add("Name Last or Organization Name (Billing Provider) = " + sValue);
                            sValue = ((EDIParser.Field)s.Fields[4]).Value;    //Name First
                            sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
                            sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
                            sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
                            sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
                            sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code


                        }
                        else if (s.Name == "N3")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;    //Address Information
                        }
                        else if (s.Name == "N4")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //City Name
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;    //State or Province Code
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;    //Postal Code
                        }
                    }
                }



                    // **** SUBSCRIBER HIERARCHICAL LEVEL ****
                else if (sHlEntity == "22")	//SUBSCRIBER
                {

                    if (s.Name == "HL")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Hierarchical ID Number
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Hierarchical Parent ID Number
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;    //Hierarchical Level Code
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Hierarchical Child Code
                    }   //Segment ID




                    else if (sNm1Entity == "IL") //SUBSCRIBER NAME
                    {
                        if (s.Name == "NM1")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
                            listBox1.Items.Add("Name Last or Organization Name (Subscriber) = " + sValue);
                            sValue = ((EDIParser.Field)s.Fields[4]).Value;    //Name First
                            sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
                            sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
                            sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
                            sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
                            sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code

                        }
                        else if (s.Name == "N3")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Address Information
                        }
                        else if (s.Name == "N4")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //City Name
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //State or Province Code
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Postal Code
                        }
                        else if (s.Name == "DMG")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date Time Period Format Qualifier
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Date Time Period
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Gender Code
                        }   //Segment ID
                    }
                    else if (sNm1Entity == "PR") //PAYER NAME
                    {
                        if (s.Name == "NM1")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
                            listBox1.Items.Add("Name Last or Organization Name (Payer) = " + sValue);
                            sValue = ((EDIParser.Field)s.Fields[4]).Value;    //Name First
                            sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
                            sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
                            sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
                            sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
                            sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code
                        }
                        else if (s.Name == "N2")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Address Information
                            sNm1Entity = "";
                        }
                    } //sNm1Entity


                    else if (s.Name == "CLM")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Claim Submitter's Identifier
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Monetary Amount
                        listBox1.Items.Add("Name Last or Organization Name (Subscriber Claim) = " + sValue);
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;    //Claim Filing Indicator Code
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Non-Institutional Claim Type Code
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Facility Code Value : Facility Code Qualifier :/Claim Frequency Type Code
                        sValue = ((EDIParser.Field)s.Fields[6]).Value; ;     //Yes/No Condition or Response Code
                        sValue = ((EDIParser.Field)s.Fields[7]).Value; ;     //Provider Accept Assignment Code
                        sValue = ((EDIParser.Field)s.Fields[8]).Value; ;     //Yes/No Condition or Response Code
                        sValue = ((EDIParser.Field)s.Fields[9]).Value; ;     //Release of Information Code
                        sValue = ((EDIParser.Field)s.Fields[10]).Value; ;     //Patient Signature Source Code
                    }
                    else if (s.Name == "DTP")
                    {
                        sQlfr = ((EDIParser.Field)s.Fields[1]).Value;
                        if (sQlfr == "938")		//ORDER
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date/Time Qualifier
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Date Time Period Format Qualifier
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Date Time Period
                        }
                        else if (sQlfr == "454")		//INITIAL TREATMENT
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date/Time Qualifier
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Date Time Period Format Qualifier
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Date Time Period
                        }
                        else if (sQlfr == "431")		//Onset of Current Symptoms or Illness
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date/Time Qualifier
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Date Time Period Format Qualifier
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Date Time Period
                            listBox1.Items.Add("Date Time Period (Subscriber's Onset of Current Symptoms Date) = " + sValue);
                        }
                    }
                    else if (s.Name == "REF")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Reference Identification Qualifier
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Reference Identification
                    }
                    else if (s.Name == "HI")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;

                    }   //Segment ID


                    else if (sNm1Entity == "82") //RENDERING PROVIDER NAME
                    {

                        if (s.Name == "NM1")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;      //Entity Identifier Code
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                            listBox1.Items.Add("Name Last or Organization Name (Subscriber's Rendering Provider) = " + sValue);
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;       //Name Last or Organization Name
                            sValue = ((EDIParser.Field)s.Fields[4]).Value;       //Name First
                            sValue = ((EDIParser.Field)s.Fields[5]).Value;       //Name Middle
                            sValue = ((EDIParser.Field)s.Fields[6]).Value;       //Name Prefix
                            sValue = ((EDIParser.Field)s.Fields[7]).Value;       //Name Suffix
                            sValue = ((EDIParser.Field)s.Fields[8]).Value; ;     //Identification Code Qualifier
                            sValue = ((EDIParser.Field)s.Fields[9]).Value; ;     //Identification Code
                            //   sValue = ((EDIParser.Field)s.Fields[10]).Value;;     //Patient Signature Source Code
                        }
                        else if (s.Name == "PRV")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Provider Code
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Reference Identification Qualifier
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Reference Identification
                        }
                        else if (s.Name == "N3")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Address Information
                        }
                        else if (s.Name == "N4")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //City Name
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //State or Province Code
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Postal Code
                        }   //Segment ID
                    }
                    else if (sNm1Entity == "77") //SERVICE LOCATION
                    {
                        if (s.Name == "NM1")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                            listBox1.Items.Add("Name Last or Organization Name (Subscriber's Service Location) = " + sValue);
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
                            sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Name First
                            sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
                            sValue = ((EDIParser.Field)s.Fields[6]).Value;      //Name Prefix
                            sValue = ((EDIParser.Field)s.Fields[7]).Value;      //Name Suffix
                            sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
                            sValue = ((EDIParser.Field)s.Fields[9]).Value;      //Identification Code
                            // sValue = ((EDIParser.Field)s.Fields[10]).Value;;     //Patient Signature Source Code
                        }
                        else if (s.Name == "N3")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Address Information
                        }
                        else if (s.Name == "N4")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //City Name
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //State or Province Code
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Postal Code
                            sNm1Entity = "";
                        }   //Segment ID
                    }


                    else if (s.Name == "LX")
                    {

                        sValue = ((EDIParser.Field)s.Fields[1]).Value;      //Assigned Number
                    }
                    else if (s.Name == "SV1")
                    {


                        sValue = ((EDIParser.Field)s.Fields[1]).Value;    //Product/Service ID Qualifier    //Product/Service ID

                        string[] ssplit = sValue.Split(':');

                        listBox1.Items.Add("Product/ServiceID(Subscriber's Service Line) = " + ssplit.GetValue(1));


                        listBox1.Items.Add("Description(Subscriber's Service Line) = ");

                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Monetary Amount
                        listBox1.Items.Add("Monetary Amount (Subscriber's Service Line) = " + sValue);

                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Unit or Basis for Measurement Code
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Quantity
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Facility Code Value
                        sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Service Type Code
                        sValue = ((EDIParser.Field)s.Fields[7]).Value;     //Diagnosis Code Pointer
                        sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Monetary Amount
                        sValue = ((EDIParser.Field)s.Fields[9]).Value;     //Yes/No Condition or Response Code
                    }
                    else if (s.Name == "DTP")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date/Time Qualifier
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Date Time Period Format Qualifier
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Date Time Period
                    }   //sSegmentID

                }	//sHlEntity


                    // **** PATIENT HIERARCHICAL LEVEL ****
                else if (sHlEntity == "23")	//DEPENDENT
                {

                    if (s.Name == "HL")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Hierarchical ID Number
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Hierarchical Parent ID Number
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;  //Hierarchical Level Code
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Hierarchical Child Code
                    }
                    else if (s.Name == "PAT")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;
                        sValue = ((EDIParser.Field)s.Fields[6]).Value;
                        sValue = ((EDIParser.Field)s.Fields[7]).Value;
                        sValue = ((EDIParser.Field)s.Fields[8]).Value;
                        sValue = ((EDIParser.Field)s.Fields[9]).Value;
                    }   //Segment ID


                    else if (sNm1Entity == "QC") //PATIENT
                    {
                        if (s.Name == "NM1")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                            listBox1.Items.Add("Name Last or Organization Name (Dependent Patient) = " + sValue);
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;      //Name Last or Organization Name
                            sValue = ((EDIParser.Field)s.Fields[4]).Value;      //Name First
                            sValue = ((EDIParser.Field)s.Fields[5]).Value;      //Name Middle
                            sValue = ((EDIParser.Field)s.Fields[6]).Value;      //Name Prefix
                            sValue = ((EDIParser.Field)s.Fields[7]).Value;      //Name Suffix
                            sValue = ((EDIParser.Field)s.Fields[8]).Value;      //Identification Code Qualifier
                            sValue = ((EDIParser.Field)s.Fields[9]).Value;      //Identification Code
                            sValue = ((EDIParser.Field)s.Fields[10]).Value;
                        }
                        else if (s.Name == "N3")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Address Information
                        }
                        else if (s.Name == "N4")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //City Name
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //State or Province Code
                            sValue = ((EDIParser.Field)s.Fields[3]).Value; ;     //Postal Code
                        }
                        else if (s.Name == "DMG")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date Time Period Format Qualifier
                            sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Date Time Period
                            sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Gender Code
                        }
                        else if (s.Name == "REF")
                        {
                            sValue = ((EDIParser.Field)s.Fields[1]).Value; ;
                            sValue = ((EDIParser.Field)s.Fields[2]).Value; ;
                        }   //Segment ID
                    } //sNm1Entity

                    else if (s.Name == "CLM")
                    {

                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Claim Submitter's Identifier
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Monetary Amount
                        listBox1.Items.Add("Name Last or Organization Name (Dependent Claim) = " + sValue);
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;    //Claim Filing Indicator Code
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Non-Institutional Claim Type Code
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Facility Code Value : Facility Code Qualifier :/Claim Frequency Type Code
                        sValue = ((EDIParser.Field)s.Fields[6]).Value; ;     //Yes/No Condition or Response Code
                        sValue = ((EDIParser.Field)s.Fields[7]).Value; ;     //Provider Accept Assignment Code
                        sValue = ((EDIParser.Field)s.Fields[8]).Value; ;     //Yes/No Condition or Response Code
                        sValue = ((EDIParser.Field)s.Fields[9]).Value; ;     //Release of Information Code
                        sValue = ((EDIParser.Field)s.Fields[10]).Value; ;     //Patient Signature Source Code

                    }
                    else if (s.Name == "DTP")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date/Time Qualifier
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Date Time Period Format Qualifier
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Date Time Period
                    }
                    else if (s.Name == "REF")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Reference Identification Qualifier
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Reference Identification
                    }
                    else if (s.Name == "HI")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;

                    }   //Segment ID
                }


                else if (sNm1Entity == "82") //RENDERING PROVIDER NAME
                {
                    if (s.Name == "NM1")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Name First
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
                        sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
                        sValue = ((EDIParser.Field)s.Fields[7]).Value;     //Name Suffix
                        sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
                        sValue = ((EDIParser.Field)s.Fields[9]).Value;     //Identification Code
                    }
                    else if (s.Name == "PRV")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Provider Code
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Reference Identification Qualifier
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Reference Identification
                    }
                    else if (s.Name == "N3")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Address Information
                    }
                    else if (s.Name == "N4")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //City Name
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //State or Province Code
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Postal Code
                    }   //Segment ID
                }
                else if (sNm1Entity == "77") //SERVICE LOCATION
                {
                    if (s.Name == "NM1")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
                        sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Name First
                        sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
                        sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
                        sValue = ((EDIParser.Field)s.Fields[7]).Value;     //Name Suffix
                        sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
                        sValue = ((EDIParser.Field)s.Fields[9]).Value;     //Identification Code
                    }
                    else if (s.Name == "N3")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;      //Address Information
                    }
                    else if (s.Name == "N4")
                    {
                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //City Name
                        sValue = ((EDIParser.Field)s.Fields[2]).Value;     //State or Province Code
                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Postal Code
                    }   //Segment ID
                }

                if (s.Name == "LX")
                {
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Assigned Number
                }
                else if (s.Name == "SV1")
                {
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Product/Service ID Qualifier
                    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Monetary Amount
                    sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Unit or Basis for Measurement Code
                    sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Quantity
                    sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Facility Code Value
                    sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Service Type Code
                    sValue = ((EDIParser.Field)s.Fields[7]).Value;     //Diagnosis Code Pointer
                    sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Monetary Amount
                    sValue = ((EDIParser.Field)s.Fields[9]).Value;     //Yes/No Condition or Response Code
                }
                else if (s.Name == "DTP")
                {
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date/Time Qualifier
                    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Date Time Period Format Qualifier
                    sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Date Time Period
                }   //sSegmentID
            }

            MessageBox.Show("Done");
        }  

    }			
 
		
			
}
