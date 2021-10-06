using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RlktWarehousePreBuildVerInc
{
    internal class Program
    {
        static int GetVersionFromFile(string filePath)
        {
            int version = 0;

            var text = File.ReadAllText(filePath);

            string pattern = @"[0-9]+";
            Regex rg = new Regex(pattern);
            MatchCollection matchCollection = rg.Matches(text);

            if (matchCollection.Count > 0)
            {
                int.TryParse(matchCollection[0].Value, out version);
            }

            return version;
        }

        static bool SetVersionToFile(string filePath, int nVersion)
        {
            bool bResult = false;

            File.WriteAllText(filePath, String.Format("static const int g_whAppVersion = {0};", nVersion));

            return bResult;
        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: RlktWarehousePreBuildVerInc.exe <version file path>");
                return;
            }

            int iVersion = GetVersionFromFile(args[0]);
            if(iVersion > 0)
            {
                Console.WriteLine("[Warehouse Version] OldVersion:{0}, NewVersion: {1}", iVersion, iVersion + 1);

                iVersion++;
                SetVersionToFile(args[0], iVersion);
            }
            else
            {
                Console.WriteLine("[Version] Could not get version from file {0}.", args[0]);
                return;
            }
        }
    }
}
