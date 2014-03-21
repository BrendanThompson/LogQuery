using System;
using System.IO;
using System.Net;
using NAppUpdate.Framework;
using NAppUpdate.Framework.Sources;

namespace LogQuery.Lib
{
    class AutoUpdate
    {
        public static void ResolveUpdateDependencies()
        {
            var loc = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var assem = String.Format(@"{0}\NAppUpdate.Framework.dll", loc);

            if (!File.Exists(assem))
            {
                using (var wc = new WebClient())
                {
                    wc.DownloadFile("http://updates.btsdevelopment.com.au/NAU/Latest/NAppUpdate.Framework.dll", assem);
                }
                ResolveUpdateDependencies();
            }
            else
            {
                CheckForUpdatesAndApply();
            }
        }

        public static void CleanupDependencies()
        {
            var loc = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var assem = String.Format(@"{0}\NAppUpdate.Framework.dll", loc);

            if (File.Exists(assem))
                File.Delete(assem);
        }

        public static void CheckForUpdatesAndApply()
        {
            var updateManager = UpdateManager.Instance;
            IUpdateSource src = new SimpleWebSource(@"http://updates.btsdevelopment.com.au/LogQuery/Latest/LogQuery.xml");
            updateManager.UpdateSource = src;
            updateManager.ReinstateIfRestarted();
            updateManager.CheckForUpdates();
            updateManager.PrepareUpdates();
            updateManager.ApplyUpdates(false, true, false);
        }
    }
}