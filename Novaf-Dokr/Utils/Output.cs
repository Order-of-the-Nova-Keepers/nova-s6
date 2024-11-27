﻿using Novaf_Dokr.Customization.lang.xMake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nova.Utils
{
    internal class novaOutput
    {
        public static void starerror(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("* ");
            Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
            Console.Write(msg);
        }
        public static void starinfo(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("* ");
            Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
            Console.Write(msg);
        }
        public class erroroutputs
        {
            public static void errinfo(string err)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(" e  ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(err + "\n");

                Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
            }
            public static void err(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" +---");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(err + "\n");

                Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
            }
            public static void errlast(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" +---");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(err + "\n");

                Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
            }
        }
        public class warningoutputs
        {
            public static void warninfo(string err)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" w  ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(err + "\n");

                Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
            }
            public static void warn(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" +---");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(err + "\n");

                Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
            }
            public static void warnlast(string err, string at)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" +---");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"> ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(err + "\n");

                Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
            }
        }
        public static void stdout(string warning)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(warning + "\n");

            Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
        }
        public static void stdoutfailed(string warning)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(warning + "\n");

            Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
        }
        public static void result(string warning)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("[result] ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(warning + "\n");

            Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
        }
    }
}