using System;
using System.Collections.Generic;
using System.Text;

namespace WatchForHL7Files
{
    class ProcessMessage
    {
        private EDIParser.HL7Parser _parser;
         
        private bool ParseMessage(string message)
        {
            bool ret = true;
            _parser = new EDIParser.HL7Parser();
            try
            {
                _parser.ParseMsg(message);

            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
        public bool Process(string message)
        {
            bool ret = true;
            if (!ParseMessage(message))
                throw new Exception("Unable to parse HL7 message");
            try
            {            
                foreach (EDIParser.Segment s in _parser.Segments)
                {
                    Console.WriteLine("{0}", s.Name);
                    foreach (EDIParser.Field f in s.Fields)
                    {
                        if (f.Components.Count == 0)
                            Console.WriteLine("{0}", string.Empty.PadLeft(4) + s.Name + "." + f.Name + " - " + f.Value);
                        else
                            Console.WriteLine("{0}", string.Empty.PadLeft(4) + s.Name + "." + f.Name);
                        foreach (EDIParser.Component c in f.Components)
                        {
                            if (c.Repetitions.Count == 0)
                                Console.WriteLine("{0}", string.Empty.PadLeft(8) + s.Name + "." + f.Name + "." + c.Name + " - " + c.Value);
                        }
                    }
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

    }
}
