using nova_s6.Command;
using nova_s6.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace nova_s6.nova_s6.shells.ush
{
    public class emul
    {
        private static Dictionary<string, string> macros = new Dictionary<string, string>();
        private static string okayMacrosPath = Path.Combine(CommandEnv.UserHomeDir, "vin_env\\third_party\\nova\\hqsh\\macros\\okay_macros.json"); //Path.Combine(AppContext.BaseDirectory, "okay_macros.json");
        private static string notOkayMacrosPath = Path.Combine(CommandEnv.UserHomeDir, "vin_env\\third_party\\nova\\hqsh\\macros\\not_okay_macros.json");  //Path.Combine(AppContext.BaseDirectory, "not_okay_macros.json");

        public static void EnsureMacrosExist()
        {
            try
            {
                // Ensure the directory for macros exists
                string macrosDirectory = Path.GetDirectoryName(okayMacrosPath);
                if (!Directory.Exists(macrosDirectory))
                {
                    Directory.CreateDirectory(macrosDirectory);
                    Console.WriteLine($"Directory created: {macrosDirectory}");
                }

                // Check and create the "okay_macros.json" file if not present
                if (!File.Exists(okayMacrosPath))
                {
                    File.WriteAllText(okayMacrosPath, "{}"); // Empty JSON object
                    Console.WriteLine($"File created: {okayMacrosPath}");
                }

                // Check and create the "not_okay_macros.json" file if not present
                if (!File.Exists(notOkayMacrosPath))
                {
                    File.WriteAllText(notOkayMacrosPath, "{}"); // Empty JSON object
                    Console.WriteLine($"File created: {notOkayMacrosPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ensuring macro files exist: {ex.Message}");
            }
        }

        public static int ate(List<string> pcS)
        {
            for (int i = 0; i < pcS.Count; i++)
            {
                if (macros.ContainsKey(pcS[i]))
                {
                    // Split the macro's value into a list and replace the current input with it
                    var expandedMacro = macros[pcS[i]].Split(" ").ToList();

                    // Remove the current macro key from pcS
                    pcS.RemoveAt(i);

                    // Insert the expanded macro at the same index
                    pcS.InsertRange(i, expandedMacro);

                    // Adjust the loop index to skip over the inserted elements
                    i += expandedMacro.Count - 1;
                }
            }

            #region pre-processing
            if (pcS == null)
                return 0;

            if (pcS.Count <= 0)
                return 0;

            // the main stuff

            CommandEnv.CommandHistory.Add(pcS.ToString());
            var parts = pcS;

            // Check if it is a get env arg value ` $(var_name) `
            //// List<string> parts = new List<string>();
            //// parts = ["@bin", "ls", "--path", "$(BinPath)"];
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i] = CommandEnv.ReplaceEnvironmentVariables(parts[i]);
            }

            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].StartsWith("\\"))
                {
                    parts[i] = parts[i].Replace("\\n", "\n");
                    parts[i] = parts[i].Replace("\\t", "\t");
                }
            }

            pcS = parts;
            #endregion


            if (pcS[0].ToLower() == "echo")
            {
                for (int i = 1; i < pcS.Count; i++)
                {
                    Console.Write($"{pcS[i]} ");
                }
            }
            else if (pcS[0].ToLower() == "echoln")
            {
                for (int i = 1; i < pcS.Count; i++)
                {
                    Console.WriteLine(pcS[i]);
                }
            }

            else if (pcS[0].ToLower() == "help")
            {
                List<string> help_lines = [
                    "Builtin:",
                    " echo     ==> echo stuff ==> to echo stuff.",
                    " echoln   ==> to echo something on seperate lines.",
                    " mind     ==> to store something in MindStorage[int][string].",
                    "  mind -add item1 item2 item3    // Adds items to storage",
                    "  mind -get 1                    // Gets item at index 1",
                    "  mind -list                     // Shows all stored items",
                    " reg      ==> to store something in RegisterStorage[string][string].",
                    "  reg>>set>>key>>value     // Sets a key-value pair",
                    "  reg>>get>>key            // Gets value for a key",
                    "  reg>>list                // Lists all registered key-value pairs",
                    " cd",
                    "  cd directory_path  // Changes the current-dir",
                    " exit",
                    "  exit // Exit the application",
                    " macro",
                    "  macro add alias command     // To add a new macro",
                    "  macro remove alias command  // To remove a macro",
                    "  macro update alias command  // To update an existing macro",
                    "  macro list                  // To list all macros",
                    "  macro restore               // To restore the deleted macros",
                  "\n$$:",
                    " $$env::variableName           // Gets value of specific variable",
                    " $$env>>variableName>>value    // Sets environment variable",
                    " $$env>>$all                   // Lists all environment variables",
                    " $$env>>$rem>>variableName     // Removes environment variable",
                  "\nUtils:",
                    " File:",
                    "  file create filename content  // Creates new file with content",
                    "  file read filename            // Displays file content",
                    "  file delete filename          // Deletes specified file",
                    "  file append filename content  // Appends content to file",
                    "  file rename oldname newname   // Renames file",
                    "  file copy source destination  // Copies file",
                    "  file info filename            // Shows file information",
                    "  file exists filename          // Checks if file exists",
                    "  file lines-count filename     // Counts lines in file",
                    "  file search filename text     // Searches for text in file",
                    "  file encrypt filename         // Encrypts file",
                    "  file decrypt filename         // Decrypts file",
                    "  file compress filename        // Compresses file",
                    "  file decompress filename      // Decompresses file",
                    "  file hash filename            // Generates SHA-256 hash",
                    "  file watch filename           // Monitors file for changes",
                    "  file temp [content]           // Creates temporary file",
                    " Directory:",
                    "  dir create dirname            // Creates new directory",
                    "  dir list dirname              // Lists directory contents",
                    "  dir delete dirname            // Deletes directory",
                    "  dir rename oldname newname    // Renames directory",
                    "  dir exists dirname            // Checks if directory exists",
                    "  dir info dirname              // Shows directory information",
                    "  dir size dirname              // Calculates directory size",
                    "  dir count-files dirname       // Counts files in directory",
                    "  dir count-dirs dirname        // Counts subdirectories",
                    "  dir backup dirname            // Creates directory backup",
                    "  dir clean dirname             // Removes all contents",
                    "  dir find dirname pattern      // Finds files matching pattern",
                    "  dir monitor dirname           // Monitors directory changes",
                    " Network:",
                    "  net tcp-connect hostname port [message]      // Establishes TCP connection to a host and port",
                    "  net tcp-server port                          // Creates a TCP server on the specified port",
                    "  net smtp-send server port username password to subject body // Sends email using SMTP",
                    "  net rdp-check hostname                       // Checks if RDP port is accessible",
                    "  net ping hostname                            // Tests connectivity to a host",
                    "  net gethost hostname                         // Gets host information",
                    "  net getip hostname                           // Resolves domain name to IP address",
                    "  net scan-ports hostname startPort endPort    // Scans port range on a host",
                    "  net traceroute hostname                      // Traces route to a destination",
                    "  net whois domain                             // Performs WHOIS lookup",
                    "  net listen port                              // Creates a network listener",
                    "  net netstat                                  // Shows active connections",
                    "  net check-connection                         // Verifies network connectivity",
                    "  net bandwidth-test                           // Tests download speed",
                    "  net download url localfile                   // Downloads file from a URL",
                    "  net http-get url                             // Performs HTTP GET request",
                    "  net mac                                      // Displays MAC addresses",
                    "  net route                                    // Shows routing information",
                    "  net send-packet host port message            // Sends a UDP packet",

                    ];

                foreach (var line in help_lines)
                {
                    Console.WriteLine(line);
                }
            }
            else if (pcS[0].ToLower() == "exit")
            {
                Environment.Exit(0);
            }
            else
            {
                errs.CacheClean();
                errs.New($"hqsh: `{pcS[0]}`: something went wrong!, type `help` for help!");
                errs.ListThemAll();
                errs.CacheClean();
                return 0;
            }

            // return the result code

            return 1;
        }
    }
}
