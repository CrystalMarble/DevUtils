using MIU;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DevUtils.Commands
{
    internal class DevCommands
    {
        [ConsoleCommand("types", "Gets all types and saves them to Types.txt", null, false, false)]

        public static string GetAllTypes(params string[] args)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Types.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Listing all available types:");

                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        // Write the assembly name
                        writer.WriteLine($"Assembly: {assembly.FullName}");

                        // Write all types in the assembly
                        foreach (Type type in assembly.GetTypes())
                        {
                            writer.WriteLine($"  Type: {type.FullName}");
                        }
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        writer.WriteLine($"  [Error] Unable to load some types in {assembly.FullName}");
                        foreach (var loaderException in ex.LoaderExceptions)
                        {
                            writer.WriteLine($"    {loaderException.Message}");
                        }
                    }
                }
            }

            return $"All types saved to {filePath}";
        }
    }

}
