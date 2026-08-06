using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;



namespace CsharpTran270
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox txtReferenceID;
        private System.Windows.Forms.TextBox txtTransactionDate;
        private System.Windows.Forms.TextBox txtPayerId;
        private System.Windows.Forms.TextBox txtProviderLastname;
        private System.Windows.Forms.TextBox txtProviderFirstname;
        private System.Windows.Forms.TextBox txtProviderNo;
        private System.Windows.Forms.TextBox txtSubscriberId;
        private System.Windows.Forms.TextBox txtSubscriberCompanyId;
        private System.Windows.Forms.TextBox txtInsuredLastname;
        private System.Windows.Forms.TextBox txtInsuredFirstname;
        private System.Windows.Forms.TextBox txtInsuredMidInitial;
        private System.Windows.Forms.TextBox txtPolicyNo;
        private System.Windows.Forms.TextBox txtInsuredCity;
        private System.Windows.Forms.TextBox txtInsuredState;
        private System.Windows.Forms.TextBox txtInsuredZip;
        private System.Windows.Forms.TextBox txtInsuredAddress;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPayerLastname;
        private System.Windows.Forms.TextBox txtPayerFirstname;
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
            this.btnTranslate = new System.Windows.Forms.Button();
            this.txtReferenceID = new System.Windows.Forms.TextBox();
            this.txtTransactionDate = new System.Windows.Forms.TextBox();
            this.txtPayerLastname = new System.Windows.Forms.TextBox();
            this.txtPayerId = new System.Windows.Forms.TextBox();
            this.txtProviderLastname = new System.Windows.Forms.TextBox();
            this.txtProviderFirstname = new System.Windows.Forms.TextBox();
            this.txtProviderNo = new System.Windows.Forms.TextBox();
            this.txtSubscriberId = new System.Windows.Forms.TextBox();
            this.txtSubscriberCompanyId = new System.Windows.Forms.TextBox();
            this.txtInsuredLastname = new System.Windows.Forms.TextBox();
            this.txtInsuredFirstname = new System.Windows.Forms.TextBox();
            this.txtInsuredMidInitial = new System.Windows.Forms.TextBox();
            this.txtPolicyNo = new System.Windows.Forms.TextBox();
            this.txtInsuredCity = new System.Windows.Forms.TextBox();
            this.txtInsuredState = new System.Windows.Forms.TextBox();
            this.txtInsuredZip = new System.Windows.Forms.TextBox();
            this.txtInsuredAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPayerFirstname = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(136, 344);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(104, 32);
            this.btnTranslate.TabIndex = 0;
            this.btnTranslate.Text = "Translate";
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // txtReferenceID
            // 
            this.txtReferenceID.Location = new System.Drawing.Point(16, 72);
            this.txtReferenceID.Name = "txtReferenceID";
            this.txtReferenceID.Size = new System.Drawing.Size(136, 20);
            this.txtReferenceID.TabIndex = 1;
            this.txtReferenceID.Text = "TransactionReference";
            // 
            // txtTransactionDate
            // 
            this.txtTransactionDate.Location = new System.Drawing.Point(176, 72);
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.Size = new System.Drawing.Size(100, 20);
            this.txtTransactionDate.TabIndex = 2;
            this.txtTransactionDate.Text = "TransactionDate";
            // 
            // txtPayerLastname
            // 
            this.txtPayerLastname.Location = new System.Drawing.Point(16, 112);
            this.txtPayerLastname.Name = "txtPayerLastname";
            this.txtPayerLastname.Size = new System.Drawing.Size(100, 20);
            this.txtPayerLastname.TabIndex = 3;
            this.txtPayerLastname.Text = "PayerLastname";
            // 
            // txtPayerId
            // 
            this.txtPayerId.Location = new System.Drawing.Point(256, 112);
            this.txtPayerId.Name = "txtPayerId";
            this.txtPayerId.Size = new System.Drawing.Size(100, 20);
            this.txtPayerId.TabIndex = 4;
            this.txtPayerId.Text = "PayerId";
            // 
            // txtProviderLastname
            // 
            this.txtProviderLastname.Location = new System.Drawing.Point(16, 152);
            this.txtProviderLastname.Name = "txtProviderLastname";
            this.txtProviderLastname.Size = new System.Drawing.Size(100, 20);
            this.txtProviderLastname.TabIndex = 5;
            this.txtProviderLastname.Text = "ProviderLastname";
            // 
            // txtProviderFirstname
            // 
            this.txtProviderFirstname.Location = new System.Drawing.Point(136, 152);
            this.txtProviderFirstname.Name = "txtProviderFirstname";
            this.txtProviderFirstname.Size = new System.Drawing.Size(100, 20);
            this.txtProviderFirstname.TabIndex = 6;
            this.txtProviderFirstname.Text = "ProviderFirstname";
            // 
            // txtProviderNo
            // 
            this.txtProviderNo.Location = new System.Drawing.Point(256, 152);
            this.txtProviderNo.Name = "txtProviderNo";
            this.txtProviderNo.Size = new System.Drawing.Size(100, 20);
            this.txtProviderNo.TabIndex = 7;
            this.txtProviderNo.Text = "ProviderNo";
            // 
            // txtSubscriberId
            // 
            this.txtSubscriberId.Location = new System.Drawing.Point(16, 200);
            this.txtSubscriberId.Name = "txtSubscriberId";
            this.txtSubscriberId.Size = new System.Drawing.Size(100, 20);
            this.txtSubscriberId.TabIndex = 8;
            this.txtSubscriberId.Text = "SubscriberId";
            // 
            // txtSubscriberCompanyId
            // 
            this.txtSubscriberCompanyId.Location = new System.Drawing.Point(128, 200);
            this.txtSubscriberCompanyId.Name = "txtSubscriberCompanyId";
            this.txtSubscriberCompanyId.Size = new System.Drawing.Size(120, 20);
            this.txtSubscriberCompanyId.TabIndex = 9;
            this.txtSubscriberCompanyId.Text = "SubscriberCompanyId";
            // 
            // txtInsuredLastname
            // 
            this.txtInsuredLastname.Location = new System.Drawing.Point(16, 232);
            this.txtInsuredLastname.Name = "txtInsuredLastname";
            this.txtInsuredLastname.Size = new System.Drawing.Size(100, 20);
            this.txtInsuredLastname.TabIndex = 10;
            this.txtInsuredLastname.Text = "InsuredLastname";
            // 
            // txtInsuredFirstname
            // 
            this.txtInsuredFirstname.Location = new System.Drawing.Point(128, 232);
            this.txtInsuredFirstname.Name = "txtInsuredFirstname";
            this.txtInsuredFirstname.Size = new System.Drawing.Size(100, 20);
            this.txtInsuredFirstname.TabIndex = 11;
            this.txtInsuredFirstname.Text = "InsuredFirstname";
            // 
            // txtInsuredMidInitial
            // 
            this.txtInsuredMidInitial.Location = new System.Drawing.Point(240, 232);
            this.txtInsuredMidInitial.Name = "txtInsuredMidInitial";
            this.txtInsuredMidInitial.Size = new System.Drawing.Size(100, 20);
            this.txtInsuredMidInitial.TabIndex = 12;
            this.txtInsuredMidInitial.Text = "InsuredMidInitial";
            // 
            // txtPolicyNo
            // 
            this.txtPolicyNo.Location = new System.Drawing.Point(264, 200);
            this.txtPolicyNo.Name = "txtPolicyNo";
            this.txtPolicyNo.Size = new System.Drawing.Size(100, 20);
            this.txtPolicyNo.TabIndex = 13;
            this.txtPolicyNo.Text = "PolicyNo";
            // 
            // txtInsuredCity
            // 
            this.txtInsuredCity.Location = new System.Drawing.Point(16, 296);
            this.txtInsuredCity.Name = "txtInsuredCity";
            this.txtInsuredCity.Size = new System.Drawing.Size(100, 20);
            this.txtInsuredCity.TabIndex = 14;
            this.txtInsuredCity.Text = "InsuredCity";
            // 
            // txtInsuredState
            // 
            this.txtInsuredState.Location = new System.Drawing.Point(128, 296);
            this.txtInsuredState.Name = "txtInsuredState";
            this.txtInsuredState.Size = new System.Drawing.Size(100, 20);
            this.txtInsuredState.TabIndex = 15;
            this.txtInsuredState.Text = "InsuredState";
            // 
            // txtInsuredZip
            // 
            this.txtInsuredZip.Location = new System.Drawing.Point(240, 296);
            this.txtInsuredZip.Name = "txtInsuredZip";
            this.txtInsuredZip.Size = new System.Drawing.Size(100, 20);
            this.txtInsuredZip.TabIndex = 16;
            this.txtInsuredZip.Text = "InsuredZip";
            // 
            // txtInsuredAddress
            // 
            this.txtInsuredAddress.Location = new System.Drawing.Point(16, 264);
            this.txtInsuredAddress.Name = "txtInsuredAddress";
            this.txtInsuredAddress.Size = new System.Drawing.Size(264, 20);
            this.txtInsuredAddress.TabIndex = 17;
            this.txtInsuredAddress.Text = "InsuredAddress";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 40);
            this.label1.TabIndex = 18;
            this.label1.Text = "This program is just an example of how to use the EDIParser.Net component in C# t" +
                "o acknowledge and translate an EDI file..";
            // 
            // txtPayerFirstname
            // 
            this.txtPayerFirstname.Location = new System.Drawing.Point(136, 112);
            this.txtPayerFirstname.Name = "txtPayerFirstname";
            this.txtPayerFirstname.Size = new System.Drawing.Size(100, 20);
            this.txtPayerFirstname.TabIndex = 19;
            this.txtPayerFirstname.Text = "PayerFirstname";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(384, 390);
            this.Controls.Add(this.txtPayerFirstname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInsuredAddress);
            this.Controls.Add(this.txtInsuredZip);
            this.Controls.Add(this.txtInsuredState);
            this.Controls.Add(this.txtInsuredCity);
            this.Controls.Add(this.txtPolicyNo);
            this.Controls.Add(this.txtInsuredMidInitial);
            this.Controls.Add(this.txtInsuredFirstname);
            this.Controls.Add(this.txtInsuredLastname);
            this.Controls.Add(this.txtSubscriberCompanyId);
            this.Controls.Add(this.txtSubscriberId);
            this.Controls.Add(this.txtProviderNo);
            this.Controls.Add(this.txtProviderFirstname);
            this.Controls.Add(this.txtProviderLastname);
            this.Controls.Add(this.txtPayerId);
            this.Controls.Add(this.txtPayerLastname);
            this.Controls.Add(this.txtTransactionDate);
            this.Controls.Add(this.txtReferenceID);
            this.Controls.Add(this.btnTranslate);
            this.Name = "Form1";
            this.Text = "TranAck 270X092";
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

        private void btnTranslate_Click(object sender, System.EventArgs e)
        {

            string sHLcode = "";


            btnTranslate.Enabled = false;


            System.IO.Stream strEdi = System.IO.File.OpenRead("270_X092.txt");

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
            // This loop iterates though the EDI file a segment at a time
            foreach (EDIParser.Segment s in x12parser.Segments)
            {
                // A segment is identified by its Area number, Loop section and segment id.

                if (s.Name == "ISA")
                {
                    // map data elements of ISA segment in here
                }
                else if (s.Name == "GS")
                {
                    // map data elements of GS segment in here
                }


                else if (s.Name == "ST")
                {
                    // map data element of ST segment in here

                }
                else if (s.Name == "BHT")
                {
                    // mapping the data elements of BIG segment to textbox fields
                    
                    txtReferenceID.Text = ((EDIParser.Field)s.Fields[3]).Value;
                    txtTransactionDate.Text = ((EDIParser.Field)s.Fields[4]).Value; ;
                }

                else if (s.Name == "HL")
                {

                    sHLcode = ((EDIParser.Field)s.Fields[3]).Value;

                }

                else if (sHLcode == "20")
                //Information Source
                {

                    if (s.Name == "NM1")
                    {
                        txtPayerLastname.Text = ((EDIParser.Field)s.Fields[3]).Value;
                        txtPayerFirstname.Text = ((EDIParser.Field)s.Fields[4]).Value;
                        txtPayerId.Text = ((EDIParser.Field)s.Fields[9]).Value;
                    }	//sSegmentID

                    // sHLcode == "20"
                }
                else if (sHLcode == "21")
                {		//Information Receiver


                    if (s.Name == "NM1")
                    {            
                       
                        txtProviderLastname.Text = ((EDIParser.Field)s.Fields[3]).Value;
                        txtProviderFirstname.Text = ((EDIParser.Field)s.Fields[4]).Value;
                        txtProviderNo.Text = ((EDIParser.Field)s.Fields[9]).Value;
                    }
                    else if (s.Name == "REF")
                    {
                        // txtProviderNetId.Text = ((EDIParser.Field)s.Fields[2]).Value;
                    }
                    else if (s.Name == "N3")
                    {
                        // txtProviderAddress.Text  = ((EDIParser.Field)s.Fields[1]).Value;	//Ship-To Address
                    }
                    else if (s.Name == "N4")
                    {
                        // txtProviderCity.Text = ((EDIParser.Field)s.Fields[1]).Value;	//Ship-To City
                        // txtProviderState.Text  = ((EDIParser.Field)s.Fields[2]).Value;	// Ship-To State
                        // txtProviderZip.Text = ((EDIParser.Field)s.Fields[3]).Value;
                    }	//sSegmentID
                    else if (s.Name == "PER")
                    {
                    }
                    else if (s.Name == "PER")
                    {
                    }	//sSegmentID

                }	// sHLcode == "21"

                else if (sHLcode == "22")
                {		//Subscriber
                    if (s.Name == "TRN")
                    {
                        txtSubscriberId.Text = ((EDIParser.Field)s.Fields[2]).Value;
                        txtSubscriberCompanyId.Text = ((EDIParser.Field)s.Fields[3]).Value;
                    }

                    else if (s.Name == "NM1")
                    {
                        txtInsuredLastname.Text = ((EDIParser.Field)s.Fields[3]).Value;
                        txtInsuredFirstname.Text = ((EDIParser.Field)s.Fields[4]).Value;
                        txtInsuredMidInitial.Text = ((EDIParser.Field)s.Fields[5]).Value;
                        
                    }
                    else if (s.Name == "REF")
                    {
                        if (((EDIParser.Field)s.Fields[1]).Value == "1L")
                        {
                            txtPolicyNo.Text = ((EDIParser.Field)s.Fields[2]).Value;
                        }
                    }
                    else if (s.Name == "N3")
                    {
                        txtInsuredAddress.Text = ((EDIParser.Field)s.Fields[1]).Value;	//Ship-To Address
                    }
                    else if (s.Name == "N4")
                    {
                        txtInsuredCity.Text = ((EDIParser.Field)s.Fields[1]).Value;	//Ship-To City
                        txtInsuredState.Text = ((EDIParser.Field)s.Fields[2]).Value;	// Ship-To State
                        txtInsuredZip.Text = ((EDIParser.Field)s.Fields[3]).Value;
                    }


                    else if (s.Name == "EQ")
                    {
                    }
                    else if (s.Name == "AMT")
                    {
                    }
                    else if (s.Name == "III")
                    {
                    }
                    else if (s.Name == "REF")
                    {
                    }
                    else if (s.Name == "DTP")
                    {
                    }

                }	//sHLcode == "22"

                else if (sHLcode == "23")
                {		//Dependent

                    if (s.Name == "HL")
                    {
                    }
                    else if (s.Name == "TRN")
                    {
                    }


                    else if (s.Name == "NM1")
                    {
                        // txtDependentLastname.Text = ((EDIParser.Field)s.Fields[3]).Value;
                        // txtDependentFirstname.Text = ((EDIParser.Field)s.Fields[4]).Value;
                        // txtDependentMidInitial.Text = ((EDIParser.Field)s.Fields[5]).Value;
                    }
                    else if (s.Name == "REF")
                    {
                    }
                    else if (s.Name == "N3")
                    {
                        // txtDependentAddress.Text  = ((EDIParser.Field)s.Fields[1]).Value;	//Ship-To Address
                    }
                    else if (s.Name == "N4")
                    {
                        // txtDependentCity.Text = ((EDIParser.Field)s.Fields[1]).Value;	//Ship-To City
                        // txtDependentState.Text  = ((EDIParser.Field)s.Fields[2]).Value;	// Ship-To State
                        // txtDependentZip.Text = ((EDIParser.Field)s.Fields[3]).Value;
                    }	//sSegmentID
                }

                else if (s.Name == "EQ")
                {
                }
                else if (s.Name == "AMT")
                {
                }
                else if (s.Name == "III")
                {
                }
                else if (s.Name == "REF")
                {
                }
                else if (s.Name == "DTP")
                {
                }
            }
            //sHLcode == "23"

            // Checks the 997 acknowledgment file just created.
            // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
            // to translating any other EDI file.

            // Gets the first segment of the 997 acknowledgment file



            foreach (EDIParser.Segment s in x12parser.Segments)
            {
                if (s.Name == "AK9")
                {
                    if (((EDIParser.Field)s.Fields[1]).Value == "R")
                    {
                        // reject EDI file
                    }
                }
            }
        }



    }
}


		