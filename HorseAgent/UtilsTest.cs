using Horse.TransportModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.Agent
{
    class Utils
    {
        public string LocateEXE(String filename)
        {
            String path = Environment.GetEnvironmentVariable("path");
            String[] folders = path.Split(';');
            foreach (String folder in folders)
            {
                if (File.Exists(folder + filename))
                {
                    return folder + filename;
                }
                else if (File.Exists(folder + "\\" + filename))
                {
                    return folder + "\\" + filename;
                }
            }

            return String.Empty;
        }

        public static void zipTest()
        {

            string startPath = @"d:\test";
            string zipPath = @"d:\test.zip";
            string extractPath = @"e:\example\extract";

            ZipFile.CreateFromDirectory(startPath, zipPath);

            ZipFile.ExtractToDirectory(zipPath, extractPath);

        }

        public static List<AppInfo> GetSoftwares()
        {

            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            List<AppInfo> appinfoList = new List<AppInfo>();

            var props = typeof(AppInfo).GetProperties();

            //DataTable dt = new DataTable();
            //props.ToList().ForEach(p => { dt.Columns.Add(p.Name); });

            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                string[] keys = key.GetSubKeyNames();

                foreach (string subkey_name in keys)
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        AppInfo item = new AppInfo();
                        foreach (var p in props)
                        {
                            object val = subkey.GetValue(p.Name);
                            if (val != null)
                            {
                                p.SetValue(item, subkey.GetValue(p.Name).ToString());

                            }
                        }
                        appinfoList.Add(item);
                        //if (!string.IsNullOrEmpty(item.InstallLocation))
                        //{
                           
                        //}
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter("softwares.csv",false,Encoding.UTF8))
            {
                string line = "";
                foreach(var prop in props)
                {
                    line += prop.Name + ",";
                }
                line = line.Substring(0, line.Length - 1);
                sw.WriteLine(line);

                foreach (var item in appinfoList)
                {
                    line = "";
                    foreach(var prop in props)
                    {
                        var val = prop.GetValue(item);
                        string str = val == null ? "" : val.ToString();
                        line += str + ",";
                    }
                    if(line!="")
                    {
                        line = line.Substring(0, line.Length - 1);
                        sw.WriteLine(line);
                    }
                    
                }
            }
                

            return appinfoList;
        }
    }
}
