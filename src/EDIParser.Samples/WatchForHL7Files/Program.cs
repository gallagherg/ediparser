using System;
using System.Collections.Generic;
using System.Text;
using WatchFile;

namespace WatchForHL7Files
{
    class Program
    {
        public static void Main()
        {
            WatchIncoming incoming = new WatchIncoming();
            //make sure you set the app.config for the file paths you wish to watch.
            /*	<appSettings>
		            <add key = "InFolder" value="C:\HL7\In\"/>
		            <add key = "TmpFolder" value="C:\HL7\Tmp\"/>
		            <add key = "OutFolder" value="C:\HL7\Out\"/>
		            <add key = "ErrFolder" value="C:\HL7\Err\"/>
		            <add key = "ArcFolder" value="C:\HL7\Arc\"/>
		            <add key = "NumOfFileThreads" value="1"/>
		            <add key = "RealtimeWatcher" value="true"/>
		            <add key = "BatchWatcher" value="true"/>
		            <add key = "BatchWatcherWaitTime" value="1000"/>
	            </appSettings>	
             */ 
            
            incoming.IncomingMessage += new IncomingMessageEventHandler(OnIncomingMessage);
            incoming.Start(); 
       
            Console.WriteLine("Press \'q\' to quit.");
            while (Console.Read() != 'q') ;
            incoming.CancelWatch();   
        }
        static private void OnIncomingMessage(object sender, IncomingEventArgs e)
        {           
            if (e.Message != null)
            {
                ProcessMessage p = new ProcessMessage();
                p.Process(e.Message);
            }
        }
    }
}
