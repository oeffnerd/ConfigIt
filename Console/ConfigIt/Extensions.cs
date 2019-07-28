using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    static class Extensions
    {
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
