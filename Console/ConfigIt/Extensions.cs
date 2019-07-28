using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    static class Extensions
    {

        public static PackageInfo AddPackage(this List<PackageInfo> packages, PackageInfo package)
        {
            bool contains = false;
            for (int i = 0; i < packages.Count; i++)
            {
                PackageInfo p = packages[i];
                if (p.CheckConflict(package))
                {
                    // We have a version mismatch
                    return p;
                }
                else if(p.Equals(package))
                {
                    contains = true;
                    break;
                }
            }

            if(!contains) packages.Add(package);
            return null;
        }
    }
}
