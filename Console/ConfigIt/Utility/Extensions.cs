using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    static class Extensions
    {
        /// <summary>
        /// Kinda done this way just to show extensions, probably should be located 
        /// closer to the core "package logic" rather then some random script
        /// Also shows out and this parameters
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="package"></param>
        /// <param name="conflict"></param>
        /// <returns></returns>
        public static bool AddPackage(this List<PackageInfo> packages, PackageInfo package, out PackageInfo conflict)
        {
            conflict = null;
            bool add = true;
            for (int i = 0; i < packages.Count; i++)
            {
                PackageInfo p = packages[i];
                if (p.CheckConflict(package))
                {
                    // We have a version mismatch
                    if(Global.DEBUG) Console.WriteLine("Version Conflict: " + p.display + " : " + package.display);
                    conflict = p;
                    break;
                }
                else if(p.Equals(package))
                {
                    add = false;
                    break;
                }
            }

            if (add) packages.Add(package);

            return add;
        }
    }
}
