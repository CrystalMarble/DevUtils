using MIU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DevUtils.Commands
{
    internal class SignalCommands
    {
        [ConsoleCommand("invoke", "Invokes a signal with no arguments", "[string signalType]", false, false)]
        public static string InvokeSignal(params string[] signal)
        {
            if (signal.Length == 0) return "No signal type provided";
            Type SignalType = Utils.GetTypeByName(signal[0]);
            if (SignalType == null) return "No Signal type found with the name " + signal[0];
            
            MethodInfo GenericMethod = typeof(GlobalContext).GetMethod("Invoke");
            if (GenericMethod == null) return "No method Invoke found. Check your game installation";
            MethodInfo genericMethod = GenericMethod.MakeGenericMethod(SignalType);
            genericMethod.Invoke(null, null);
            return $"Invoked {signal[0]} with no arguments";
        }

        [ConsoleCommand("invokeWithString", "Invokes a signal with an argument", "[string signalType] [string argument]", false, false)]
        public static string InvokeSignalWithString(params string[] signal)
        {
            if (signal.Length < 2) return "No signal type and value provided";

            Type SignalType = Utils.GetTypeByName(signal[0]);
            if (SignalType == null) return "No Signal type found with the specified name";

            MethodInfo GenericMethod = typeof(GlobalContext).GetMethod("InvokeWithArray");
            if (GenericMethod == null) return "No method InvokeWithArray found. Check your game installation";

            MethodInfo genericMethod = GenericMethod.MakeGenericMethod(SignalType);

            // Prepare the argument as an object[]
            string[] args = new[] { signal[1] };
            object[] invokeArgs = new object[] { args }; // Wrap args in an object array

            // Invoke the method
            genericMethod.Invoke(null, invokeArgs);

            return $"Invoked {signal[0]} with argument {signal[1]}";
        }
    }
}

