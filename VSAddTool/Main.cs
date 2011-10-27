using System;
using Microsoft.Win32;

namespace VSAddTool
{
    class MainClass
    {
        public static int Main (string[] args)
        {
            string command = "";
            string parameters = "";
            string name = "";
            string dir = "";
            int options = 18;
			
            Options opts = new Options
			{
                new Option(new string[]{}, x=>name=x),
				new Option(new string[]{}, x=>command=x),
				new Option(new []{"p","params"}, x=>parameters=x),
                new Option(new []{"d","startdir"}, x=>dir=x),
			};
            opts.Parse (args);
			
            if (command == "" || name == "")
            {
                Console.WriteLine ("Usage: VSAddTool.exe <Tool Name> <Command> [-p <command parameters>][-d <start in directory>]");
                return 1;	
            }


            using (var exToolKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\VisualStudio\10.0\External Tools",true))
            {
                int numOfTools = (int)exToolKey.GetValue ("ToolNumKeys");

                string pathVal = "ToolCmd" + numOfTools;
                string argsVal = "ToolArg" + numOfTools;
                string dirVal = "ToolDir" + numOfTools;
                string nameVal = "ToolTitle" + numOfTools;
                string optsVal = "ToolOpt" + numOfTools;



                exToolKey.SetValue (pathVal, command, RegistryValueKind.String);
                exToolKey.SetValue (argsVal, parameters, RegistryValueKind.String);
                exToolKey.SetValue (nameVal, name, RegistryValueKind.String);
                exToolKey.SetValue (dirVal, dir, RegistryValueKind.String);
                exToolKey.SetValue (optsVal, options, RegistryValueKind.DWord);
                exToolKey.SetValue ("ToolNumKeys", numOfTools + 1, RegistryValueKind.DWord);
            }



            return 0;
        }
    }
}
