using System;
using Microsoft.Win32;
using System.Linq;
using System.Collections;

namespace VSAddTool
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			string command = "";
			string parameters = "";
			
			Options opts = new Options
			{
				new Option(new string[]{}, (x)=>command=x),
				new Option(new string[]{}, (x)=>parameters=x)
			};
			
			if(command =="")
			{
				return 1;	
			}
			
			int currentTool= 0;
			
			
			var exToolKey = Registry.CurrentUser.OpenSubKey("foo\\doo\\n");
			string[] values = exToolKey.GetValueNames();
			
			string prefix = "FoooKey";
			values.Where(x=>x.StartsWith(prefix)).Select(x=>x.Substring(prefix.Length)).Where(x=>CanParseToInt(x)).Select(x=>int.Parse(x));
			
			return 0;
		}
		
		private static bool CanParseToInt(string s)
		{
				int x;
			return int.TryParse(s,out x);
		}
	}
}
