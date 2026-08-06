using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDIParser;
namespace EdiFactSample
{
    class Program
    {
        static void ParseEdiFact()
        {
            EdiFactParser edifact = new EdiFactParser();

            string ediSample = @"UNA:+,? 'UNB+UNOA:1+MMO+D2PU+030924:1650+DIS01430'UNH+1+D2RDSI:1'XRK+USERIDPU+0'XSB+00172+5901234+59201439071234+20030130+20030620+A+FE+N+M+RHO'XKI+024+TH0+A+262025903512345+94712345+PL'XTS+FUN_10+++P2+24+1++20041229+20050129'UNT+6+1'UNZ+1+DIS01430'";
            edifact.ParseMsg(ediSample);

            foreach (Segment s in edifact.Segments)
            {
                Console.WriteLine("Segment:" + s.Value);
                int i = 0;
                foreach (Field f in s.Fields)
                {
                    i++;
                    Console.WriteLine("  Field(" + i.ToString() + "):" + f.Value);
                }

            }
            Console.WriteLine();
            Console.WriteLine("XTS(1):" + edifact.GetValue("XTS.1"));
            Console.WriteLine("XTS(9):" + edifact.GetValue("XTS.9"));

            
        }
        static void Main(string[] args)
        {
            ParseEdiFact();
        }
    }
}
