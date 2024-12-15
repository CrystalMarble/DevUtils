using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DevUtils
{
    class Utils
    {
        public static Type GetTypeByName(string name)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    // Write all types in the assembly
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.FullName == name) return type;
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Debug.Log("error while getting types: " + ex.Message);
                }
            }
            return null;
        }
    }
}
