using Horse.TransportModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.Agent
{
    public static class InstalledPrograms
    {
        const string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";


        public static List<AppSimpleInfo> GetInstalledPrograms()
        {
            var result = new List<AppSimpleInfo>();
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32));
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry64));
            //GetIcons(result);
            var props = typeof(AppSimpleInfo).GetProperties();
            using (StreamWriter sw = new StreamWriter("softwares.csv", false, Encoding.UTF8))
            {
                string line = "";
                foreach (var prop in props)
                {
                    line += prop.Name + "|";
                }
                line = line.Substring(0, line.Length - 1);
                sw.WriteLine(line);

                foreach (var item in result)
                {
                    line = "";
                    foreach (var prop in props)
                    {
                        var val = prop.GetValue(item);
                        string str = val == null ? "" : val.ToString();
                        line += str + "|";
                    }
                    if (line != "")
                    {
                        line = line.Substring(0, line.Length - 1);
                        sw.WriteLine(line);
                    }

                }
            }

            return result;
        }

        //public static void GetIcons(List<AppSimpleInfo> apps)
        //{
        //    var dic = apps.ToDictionary(s => s.DisplayName);
        //    string key = @"Installer\Products";
        //    using (RegistryKey pKey = Registry.ClassesRoot.OpenSubKey(key))
        //    {
        //        string[] keys = pKey.GetSubKeyNames();
        //        foreach(var subkey_name in keys)
        //        {
        //            using (RegistryKey subkey = pKey.OpenSubKey(subkey_name))
        //            {
        //                var icon = (string)subkey.GetValue("ProductIcon");
        //                var name = (string)subkey.GetValue("ProductName");
        //                if(!string.IsNullOrEmpty(icon) && !string.IsNullOrEmpty(name))
        //                {
        //                    if (dic.ContainsKey(name))
        //                        dic[name].ProductIcon = icon;
        //                }
        //            }
        //        }
        //    }

        //}



        private static IEnumerable<AppSimpleInfo> GetInstalledProgramsFromRegistry(RegistryView registryView)
        {
            var result = new List<AppSimpleInfo>();

            using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView).OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if (IsProgramVisible(subkey))
                        {
                            var app = new AppSimpleInfo();
                            app.KeyName = subkey_name;
                            app.InstallLocation = (string)subkey.GetValue("InstallLocation");
                            app.HelpLink = (string)subkey.GetValue("HelpLink");
                            app.Publisher = (string)subkey.GetValue("Publisher");
                            app.DisplayName = (string)subkey.GetValue("DisplayName");
                            app.DisplayVersion = (string)subkey.GetValue("DisplayVersion");
                            app.DisplayIcon = (string)subkey.GetValue("DisplayIcon");
                            app.UninstallString = (string)subkey.GetValue("UninstallString");
                            result.Add(app);
                        }
                    }
                }
            }

            return result;
        }

        private static bool IsProgramVisible(RegistryKey subkey)
        {
            var name = (string)subkey.GetValue("DisplayName");
            var releaseType = (string)subkey.GetValue("ReleaseType");
            var unistallString = (string)subkey.GetValue("UninstallString");
            var systemComponent = subkey.GetValue("SystemComponent");
            var parentName = (string)subkey.GetValue("ParentDisplayName");
            var installLocation = (string)subkey.GetValue("InstallLocation");
            return
                !string.IsNullOrEmpty(name)
                && !string.IsNullOrEmpty(unistallString)
                //&& !string.IsNullOrEmpty(installLocation)
                && string.IsNullOrEmpty(releaseType)
                && string.IsNullOrEmpty(parentName)
                && (systemComponent == null);
        }
    }
}
