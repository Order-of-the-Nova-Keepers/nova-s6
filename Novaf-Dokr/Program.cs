﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.Collections;

using nova_s6.Utils;
using nova_s6.Command;
using System.Runtime.ExceptionServices;
using System.Reflection.Metadata.Ecma335;
using Novaf_Dokr.Customization.configuration;
using Novaf_Dokr.Customization;
using System.Diagnostics.SymbolStore;
using Novaf_Dokr.Customization.lang.xMake;
using Novaf_Dokr.Command.env.u_f_net;
using System.Xml.Linq;
//using nova_s6.Tests;


namespace novaf
{
    public class Program
    {
        public static string __version__ = "4.3.za.24";
        public static string __shell__ = "hqsh";
        static void Main(string[] args)
        {
            //Console.WriteLine("(c) nova Initial Developers | Fri3nds .G");
            DesignFormat.Banner();

            Initnova(false);
        }

        public class Shell
        { 
            public int Num { get; set; }
            public string Name { get; set; }
            public string Desc { get; set; }
            public string Version { get; set; }
        } 
        public static void Initnova(bool isctrlc)
        {
            #region UnitTests
            //UnitTests.Test1();
            //UnitTests.Test2();
            //UnitTests.Test3();
            //UnitTests.Test4();
            //UnitTests.Test_RunCmd();

            //nova_s6.Command.env.Vars.main_test();
            //UnitTests.Test_Exit();
            #endregion

            #region shall tests
            //abc:
            //List<string> parts= new List<string>();
            //parts = ["@bin","ls","--path", "$(BinPath)"];
            //for (int i = 0; i < parts.Count; i++)
            //{
            //    if (parts[i].StartsWith("$("))
            //    {
            //        parts[i] = parts[i].Replace("$(", "");
            //        parts[i] = parts[i].Replace(")", "");

            //        Console.WriteLine($":: {parts[i]}");
            //    }
            //    else
            //    {

            //    }
            //}
            //Console.ReadLine();
            //goto abc;

            //RunOnWindows.RunPythonFile(["C:\\Users\\Hamza\\vin_env\\ibin\\python\\usmnh\\tabl.py"]);
            //Environment.Exit(0);

            //{
            //    List<string> commands = UserInput.Prepare("@sudev do run d:G:\\fri3nds\\v-category-projects\\Developer-Grade-Virtual-OS\\Ju-hind-F\\Ju-Hind-F\\Ju-Hind-F\\Command\\env\\doker_lang\\test.dokr");

            //    IdentifyCommand.Identify(commands);
            //    List<string> parsed_commands = IdentifyCommand.ReturnThemPlease();

            //    PleaseCommandEnv.TheseCommands(parsed_commands);

            //    IdentifyCommand.CacheClean();

            //    Environment.Exit(0);
            //}

            //{
            //    nova_vm.Command.env.doker_lang.Interpreter.Interpret.Helper.ThisCode();


            //    Environment.Exit(0);

            //}

            //string Code = """
            //    rem TEST ;

            //    regs $reg2 125 ;
            //    regl $reg2 ;

            //    dspr $reg2 ;
            //    """;

            //foreach (var item in Novaf_Dokr.posix.emulater.compiler.To_Bytecode(Code))
            //{
            //    Console.WriteLine(item);
            //}

            //Environment.Exit(0);
            #endregion

            //nova_vm.Command.env.doker_lang.Interpreter.Interpret.Helper.ThisCode(File.ReadAllText("G:\\s-cat\\fri3nds\\v-category-projects\\Developer-Grade-Virtual-OS\\Nova-S6\\Novaf-Dokr\\Command\\env\\doker_lang\\test.dokr"));
            //nova_vm.Command.env.doker_lang.Interpreter.Interpret.Helper.Interactive();

            #region Actual Init

            CommandEnv.LoadEnvironmentPointers();
            CommandEnv.LoadEnvironmentVariables();

            master.conf.EnsureEnvironmentSetup();
            nova_s6.shells.hqsh.emul.EnsureMacrosExist();

            //foreach (master.conf.ConfigurationData _cd in master._Conf_Files_List)
            //{
            //    Console.WriteLine(_cd.File.Path.ToString());
            //}

            List<Shell> Shells = new List<Shell>();

            Shells.Add(new Shell
            {
                Num = 0,
                Name = "hqsh",
                Desc = "The default shell emulater, for `nova-s6` systems, not in previous distros.",
                Version = __version__
            });

            Shells.Add(new Shell
            {
                Num = 1,
                Name = "hsh",
                Desc = "The secondary/customized shell for `Novaf-Dokr` and other newer distros.",
                Version = "27/11/2024"
            });

            Shells.Add(new Shell
            {
                Num = 2,
                Name = "ush",
                Desc = "The specilized shell for somewhat m~b and for specilized legal act.",
                Version = $""
            });

            Console.WriteLine($"\n(*) sh: type: `!sh help` for `sh-help`.");

            try
            {
                while (true)
                {
                    _entry_point_main:
                    try
                    {
                        // Handle CTRL+C key press to prevent quitting
                        Console.CancelKeyPress += (sender, e) =>
                        {
                            e.Cancel = false; // Prevent the app from closing
                            Console.WriteLine("\n<CTRL-C>: type `@exit` to exit!");
                            Console.ForegroundColor = XmInterpreter.__CurrentForegroundColor;
                            Console.BackgroundColor = XmInterpreter.__CurrentBackgroundColor;
                            Initnova(true);
                        };

                        if (!isctrlc)
                        {
                            // The rest of your code
                            if (__shell__.ToLower() == "hsh")
                            {
                                DesignFormat.TakeInput([$"\n{CommandEnv.CURRENT_USER_NAME}", "@", "kernal", ":>>", $"{CommandEnv.CurrentDirDest} ", $"{DesignFormat.get_shell_icon(__shell__)} "]);
                            }
                            else if (__shell__.ToLower() == "hqsh")
                            {
                                DesignFormat.TakeInput([$"\n[{CommandEnv.CurrentDirDest}]", $"-[{CommandEnv.CURRENT_USER_NAME}]", $"\n{DesignFormat.get_shell_icon(__shell__)} "]);
                            }
                            else if (__shell__.ToLower() == "ush")
                            {
                                DesignFormat.TakeInput([$"({CommandEnv.CURRENT_USER_NAME})", $" "]);
                            }
                            else
                            {
                                DesignFormat.TakeInput([$"\n{CommandEnv.CurrentDirDest}", $"\n{CommandEnv.CURRENT_USER_NAME}]", $"\n{DesignFormat.get_shell_icon(__shell__)}\n"]);
                            }

                            List<string> commands = UserInput.Prepare(UserInput.Input());
                            IdentifyCommand.Identify(commands);
                            List<string> parsed_commands = IdentifyCommand.ReturnThemPlease();

                            if (parsed_commands.Count <= 0)
                                continue;



                            if (parsed_commands[0].StartsWith("!"))
                            {
                                if (parsed_commands[0] == "!csh")
                                {
                                    if (parsed_commands.Count < 1)
                                        return;

                                    if (parsed_commands[1].ToLower() == "--list")
                                    {

                                        foreach (var shell in Shells)
                                        {
                                            Console.WriteLine($"{shell.Num,-3}: {shell.Name,-6} :{DesignFormat.get_shell_icon(shell.Name),-2} {shell.Desc,-90} {shell.Version,-20} ");
                                        }
                                    }
                                    else if (parsed_commands[1].ToLower() == "+d")
                                    {
                                        if (parsed_commands.Count < 2)
                                            return;

                                        __shell__ = parsed_commands[2];
                                    }
                                    else if (parsed_commands[1].ToLower() == "--help")
                                    {
                                        Console.WriteLine(" !csh --list    :: To list shells. " +
                                                        "\n !csh --help    :: To show this help message." +
                                                        "\n !csh +d $shell :: To deploy `$shell` and logout from the previoes one.");
                                    }
                                    else
                                    {
                                        errs.CacheClean();
                                        errs.New($"sh: csh: type: `!csh --help` for help.");
                                        errs.ListThemAll();
                                        errs.CacheClean();
                                    }
                                }
                                if (parsed_commands[0] == "!cls")
                                {
                                    Console.Clear();
                                }
                            }
                            else
                            {

                                if (__shell__.ToLower() == "hsh")
                                {
                                    PleaseCommandEnv.TheseCommands(parsed_commands);
                                }
                                else if (__shell__.ToLower() == "hqsh")
                                {
                                    nova_s6.shells.hqsh.emul.ate(parsed_commands);
                                }
                                else
                                {
                                    errs.CacheClean();
                                    errs.New($"`{__shell__}`: is not a well-known shell, by me. turning to default `hqsh` shell. Type: `!csh --list` for the list of builtin shells.");
                                    errs.ListThemAll();
                                    errs.CacheClean();

                                    __shell__ = "hqsh";
                                }

                            }

                            IdentifyCommand.CacheClean();
                        }
                        else
                        { 
                            isctrlc = false;
                        }

                        continue;

                    }
                    catch (Exception exp) // Exception handling block
                    {
                        errs.CacheClean();
                        errs.New(exp.ToString());
                        errs.ListThem();
                    }

                    // Handle CTRL+C key press to prevent quitting
                    Console.CancelKeyPress += (sender, e) =>
                    {
                        e.Cancel = false; // Prevent the app from closing
                        //Console.WriteLine("\nTolerating CTRL+C!");
                    };
                }
            }
            catch (Exception exx)
            {
                errs.New(exx.ToString());
                errs.ListThem();
                errs.CacheClean();
            }
        }
        #endregion
    }
}