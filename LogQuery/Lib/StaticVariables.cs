using System;
using System.Reflection;
using System.Text;

namespace LogQuery.Lib
{
    class StaticVariables
    {
        public static string Logo(bool outputToConsole)
        {
            string logo = String.Format(@"
BT Systems LogQuery Version {0}
Copyright (c) Brendan Thompson, BT Systems 2013. All rights reserved.
LogQuery command line event log query utility
-------------------------------------------------------------------------------
", Assembly.GetExecutingAssembly().GetName().Version);

            if (outputToConsole.Equals(true))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(logo);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            return logo;
        }

        public static void License()
        {
            const string license = @"
LogQuery License
----------------------------------------------------------------------------

Copyright (c) 2013 Brendan Thompson

----------------------------------------------------------------------------

PowerArgs License
----------------------------------------------------------------------------

Copyright (c) 2013 Adam Abdelhamed

Permission is hereby granted, free of charge, to any person obtaining a
copy of this software and associated documentation files (the ""Software""),
to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be 
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

----------------------------------------------------------------------------
";
            Console.WriteLine(license);
            Environment.Exit(2);

        }

        public static void Help()
        {
            const string usage = @"
Usage:
-------

LogQuery [options] -log [options]

Options:

    Commands                Description                            Req
  --------------------------------------------------------------------
    -h                      This help                              [ ]
    -el  [string]           The Event Log in which you want to     [X]
                            query
                            i.e. Application, System, Security
    -id  [int]              The Event ID you are looking for       [ ]
    -qry [""string""]         The string query you are looking for   [ ]
                            in the Event Entry Message
    -src [string]           Part or All of the Event Source        [ ]
    -cat [string]           Filter for specific Category           [ ]
    -lvl [string]           What Event type; Error, Information    [ ]
    -dr  (switch)           Date range switch                      [ ]
    -log (switch)           Enables outputting to a file           [ ]
    -dst [string]           Output directory                       [ ]
    -lic (switch)           Shows the applications license         [ ]

######################################################################

Date Range Options:

    Options                 Description                            Req
  --------------------------------------------------------------------
    -sd                     Start date &/or time of the range      [ ]
    -ed                     Start date &/or time of the range      [ ]

         --------------------------------------------------------
         | THE FOLLOWING ARE VALID START DATE OR END DATE VALUES|
         |         ALL ""hh"" MUST BE IN 24-hour FORMAT.          |
         |                 ------------------                   |
         |                                                      |
         |       dd/MM/yy, dd/MM/yyyy, d/MM/yy, d/MM/yyyy       |
         |       ""dd/MM/yy hh:mm"" ""dd/MM/yy hh:mm:ss""           |
         |       ""dd/MM/yyyy hh:mm"" ""dd/MM/yyyy hh:mm:ss""       |
         |       ""d/MM/yy hh:mm"" ""d/MM/yy hh:mm:ss""             |
         |       ""d/MM/yyyy hh:mm"" ""d/MM/yyyy hh:mm:ss""         |
         --------------------------------------------------------

######################################################################

Log Options:

    Options                 Description                            Req
  --------------------------------------------------------------------
    -text                   Logs to a text file                    [ ]
    -xml                    Logs to an xml file                    [ ]
    -html                   Logs to a html file                    [ ]
    -json                   Logs to a json file                    [ ]
";

            Console.WriteLine(usage);
            Environment.Exit(2);
        }

        public static string CurrentQuery(bool outputToConsole)
        {
            string tmp;
            var sb = new StringBuilder();

                foreach (var cArg in Environment.GetCommandLineArgs())
                {
                    sb.Append(cArg + " ");
                }
            if (sb.ToString().Contains("-"))
            {
                tmp = sb.ToString().Remove(0, sb.ToString().IndexOf('-'));
                tmp = String.Format("LogQuery {0}", tmp);
                if (outputToConsole.Equals(true))
                {
                    Console.Write("Current Query: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(tmp);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                }
                return tmp;
            }
            return null;
        }
    }
}
