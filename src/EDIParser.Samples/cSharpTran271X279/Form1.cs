using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace cSharpTran271X279
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnTranslate;
		private System.Windows.Forms.ListBox listBox1;
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
            this.btnTranslate = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(456, 72);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(104, 56);
            this.btnTranslate.TabIndex = 0;
            this.btnTranslate.Text = "Translate";
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(16, 64);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(408, 277);
            this.listBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(536, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "This is just an example program to show how to translate an EDI 271X279 that has " +
                "repeating elements using the EDIParser.Net component in C#";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(584, 372);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnTranslate);
            this.Name = "Form1";
            this.Text = "Translate EDI 271X279";
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
        string[] strStorage;
		private void btnTranslate_Click(object sender, System.EventArgs e)
		{


         
            string sValue = "";
            string sLoopHLQlfr ="";
            System.IO.Stream strEdi = System.IO.File.OpenRead("271_322.txt");

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
                if (s.Name == "ISA")
			
					{
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Authorization Information Qualifier
				    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Authorization Information
				    sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Security Information Qualifier
				    sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Security Information
				    sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Interchange ID Qualifier
				    sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Interchange Sender ID
				    sValue = ((EDIParser.Field)s.Fields[7]).Value;     //Interchange ID Qualifier
				    sValue = ((EDIParser.Field)s.Fields[8]).Value;    //Interchange Receiver ID
				    sValue = ((EDIParser.Field)s.Fields[9]).Value;     //Interchange Date
				    sValue = ((EDIParser.Field)s.Fields[10]).Value;     //Interchange Time
				    sValue = ((EDIParser.Field)s.Fields[11]).Value;      //Interchange Control Standards Identifier
				    sValue = ((EDIParser.Field)s.Fields[12]).Value;     //Interchange Control Version Number
				    sValue = ((EDIParser.Field)s.Fields[13]).Value;     //Interchange Control Number
				    listBox1.Items.Add("Interchange Control Number = " + sValue);
				    sValue = ((EDIParser.Field)s.Fields[14]).Value;    //Acknowledgment Requested
				    sValue = ((EDIParser.Field)s.Fields[15]).Value;;     //Usage Indicator
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
				}   
				
			
                else if (s.Name == "ST")
				{
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Transaction Set Identifier Code
				    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Transaction Set Control Number
					listBox1.Items.Add("Transaction Set Control Number = " + sValue);     //Transaction Set Control Number
					sValue = ((EDIParser.Field)s.Fields[2]).Value;   //Implementation Convention Reference
                   
				}
				else if (s.Name == "BHT")
				{
                    sValue = ((EDIParser.Field)s.Fields[1]).Value;      //Hierarchical Structure Code
				    sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Transaction Set Purpose Code
				    sValue = ((EDIParser.Field)s.Fields[3]).Value;      //Reference Identification
				    listBox1.Items.Add("Response Reference Identification = " + sValue);
				    sValue = ((EDIParser.Field)s.Fields[4]).Value;     //Date
				    listBox1.Items.Add("Date = " + sValue);
				    sValue = ((EDIParser.Field)s.Fields[5]).Value;    //Time
				}
			
			
                else if (s.Name == "HL")
				{
					sLoopHLQlfr = ((EDIParser.Field)s.Fields[3]).Value;    //In most cases the loop qualifier is the first element of the first segment in the loop, but not necessarily
					sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Hierarchical ID Number
				    sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Hierarchical Parent ID Number
				    sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Hierarchical Level Code
				    sValue = ((EDIParser.Field)s.Fields[4]).Value;      //Hierarchical Child Code
                
                }
					
				else if (sLoopHLQlfr == "20")	//Information Source
					{
						
						
							if (s.Name == "NM1")
							{

                                sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
							    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
							    sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
							    listBox1.Items.Add("Payer Name = " + sValue);
							    sValue = ((EDIParser.Field)s.Fields[4]).Value;    //Name First
							    sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
							    sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
							    sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
							    sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
							    sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code
                                listBox1.Items.Add("Payer ID = " + sValue);

							}
						
					}	//sLoopHLQlfr == "20"

			    else if (sLoopHLQlfr == "21")	//Information Receiver
					{
					
							if (s.Name == "HL")
							{
										sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Hierarchical ID Number
				                        sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Hierarchical Parent ID Number
				                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Hierarchical Level Code
				                        sValue = ((EDIParser.Field)s.Fields[4]).Value;      //Hierarchical Child Code
                
							}   
						
						
							else if (s.Name == "NM1")
							{
                                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
					                    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
					                    sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
					                    listBox1.Items.Add("Provider Name  = " + sValue);
					                    sValue = ((EDIParser.Field)s.Fields[4]).Value;    //Name First
					                    sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
					                    sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
					                    sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
					                    sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
					                    sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code
                                        listBox1.Items.Add("Service Provider Identification = " + sValue);
	
							}
					
					}	//sLoopHLQlfr == "21"

					else if (sLoopHLQlfr == "22")	//Subscriber
					{
						   if (s.Name == "HL")
							{
										sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Hierarchical ID Number
				                        sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Hierarchical Parent ID Number
				                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Hierarchical Level Code
				                        sValue = ((EDIParser.Field)s.Fields[4]).Value;      //Hierarchical Child Code
                
							}   
						
						
							else if (s.Name == "NM1")
							{



                                        sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Entity Identifier Code
					                    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Entity Type Qualifier
					                    sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Name Last or Organization Name
					                    listBox1.Items.Add("Subscriber Name  = " + sValue);
					                    sValue = ((EDIParser.Field)s.Fields[4]).Value;    //Name First
					                    sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
					                    sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
					                    sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
					                    sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
					                    sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code
                                        listBox1.Items.Add("Subscriber Member Identification = " + sValue);
	
								   
							}
							else if (s.Name == "N3")
							{
								listBox1.Items.Add("Subscriber Address = " + ((EDIParser.Field)s.Fields[1]).Value);     //Address Information
								sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Address Information
							}
							else if (s.Name == "N4")
							{
								listBox1.Items.Add("Subscriber City = " + ((EDIParser.Field)s.Fields[1]).Value);     //City Name
								sValue = ((EDIParser.Field)s.Fields[2]).Value;     //State or Province Code
								sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Postal Code
							}
							else if (s.Name  == "DMG")
							{
								sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date Time Period Format Qualifier
								sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Date Time Period
								sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Gender Code
							}
					
					}	//sLoopHLQlfr == "22"

					else if (sLoopHLQlfr == "23")	//Dependent
					{
						   if (s.Name == "HL")
							{
										sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Hierarchical ID Number
				                        sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Hierarchical Parent ID Number
				                        sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Hierarchical Level Code
				                        sValue = ((EDIParser.Field)s.Fields[4]).Value;      //Hierarchical Child Code
                
							}   
						
						
							else if (s.Name == "NM1")
							{
                        		sValue = ((EDIParser.Field)s.Fields[1]).Value;      //Entity Identifier Code
								sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Entity Type Qualifier
								listBox1.Items.Add("Dependent Lastname = " + ((EDIParser.Field)s.Fields[3]).Value);     //Name Last or Organization Name
								listBox1.Items.Add("Dependent Firstname = " + ((EDIParser.Field)s.Fields[4]).Value);     //Name First
							//	sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
							//	sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
							//	sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
							//	sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
							//	sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code
							}
							else if (s.Name == "N3")
							{
								sValue = ((EDIParser.Field)s.Fields[1]).Value;    //Address Information
								sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Address Information
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
								sValue = ((EDIParser.Field)s.Fields[2]).Value;    //Date Time Period
								sValue = ((EDIParser.Field)s.Fields[3]).Value;    //Gender Code
							}
							else if (s.Name == "INS")
							{
								sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Yes/No Condition or Response Code
								listBox1.Items.Add("Dependent Relationship = " + ((EDIParser.Field)s.Fields[2]).Value);     //Individual Relationship Code
							}
							else if (s.Name == "DTP")
							{
								sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Date/Time Qualifier
								sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Date Time Period Format Qualifier
								sValue = ((EDIParser.Field)s.Fields[3]).Value;     //Date Time Period

                                sLoopHLQlfr = "";
                            }   
						}	
					
                else if (s.Name == "EB")
				    	{
								listBox1.Items.Add("Eligibility or Benefit Information = " + ((EDIParser.Field)s.Fields[1]).Value);     //Eligibility or Benefit Information

                                if (s.Fields.Count > 1)
                                {
                                    sValue = ((EDIParser.Field)s.Fields[2]).Value;     //Coverage Level Code

                                    //************************ repeating element *******************************************************************************************
                                    sValue = ((EDIParser.Field)s.Fields[3]).Value;
                                    string[] str = sValue.Split(':');
                                    
                                    if ( strStorage == null)
                                    {
                                        strStorage = str;

                                      
                                    }
                                    else if (strStorage.Length <= 1)
                                    {
                                        strStorage = str;
                                    }
                                    if (strStorage.GetValue(0).ToString() != "")
                                    {
                                        for (int i = 0; i <= strStorage.Length - 1; i++)
                                        {
                                            listBox1.Items.Add("Service Type Code " + Convert.ToString(i+1) + " = " + strStorage.GetValue(i).ToString());     //Service Type Code
                                        }
                                    }

                                    //********************************************************************************************************************************
                                  
                                }
							}
			    else if (s.Name  == "LS")
			    {
				                sValue = ((EDIParser.Field)s.Fields[1]).Value;     //Loop Identifier Code
			    }   
					
						
				else 	if (s.Name == "NM1")
				{

                    	        sValue = ((EDIParser.Field)s.Fields[1]).Value;      //Entity Identifier Code
								sValue = ((EDIParser.Field)s.Fields[2]).Value;      //Entity Type Qualifier
								listBox1.Items.Add("Primary Care Lastname = " + ((EDIParser.Field)s.Fields[3]).Value);     //Name Last or Organization Name
								listBox1.Items.Add("Primary Care Firstname = " + ((EDIParser.Field)s.Fields[4]).Value);     //Name First
								sValue = ((EDIParser.Field)s.Fields[5]).Value;     //Name Middle
								sValue = ((EDIParser.Field)s.Fields[6]).Value;     //Name Prefix
								sValue = ((EDIParser.Field)s.Fields[7]).Value;    //Name Suffix
								sValue = ((EDIParser.Field)s.Fields[8]).Value;     //Identification Code Qualifier
								sValue = ((EDIParser.Field)s.Fields[9]).Value;    //Identification Code
					
				}   
					
			}  
 
			Cursor = Cursors.Default;

			MessageBox.Show("Done.");

 
		}

        private void label1_Click(object sender, EventArgs e)
        {

        }

			
		}
	}

