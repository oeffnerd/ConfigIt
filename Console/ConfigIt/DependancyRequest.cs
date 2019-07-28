using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConfigIt
{
    class DownloadRequest : InputType
    {
        int downloads = 0;
        PackageInfo[] packages;
        int dependancies = 0;
        PackageDependancy[] packageDependancies= new PackageDependancy[0];

        public DownloadRequest(string[] lines)
        {
            Load(lines);
        }

        private void Load(string[] lines)
        {
            int index = 0;

            error = InputUtility.ParseInt(lines[index], out downloads);
            if(error) return;
            //Console.WriteLine("Number of requested downloads: " + downloads);

            packages = new PackageInfo[downloads];
            for (index = 1; index < 1+downloads; index++)
            {
                PackageInfo package = new PackageInfo(lines[index]);
                packages[index - 1] = package;
                //package.Print();
            }

            // if current index == lines.length there are no dependancies
            if (lines.Length == index) return;

            error = InputUtility.ParseInt(lines[index], out dependancies);
            if (error) return;
            //Console.WriteLine("Number of package dependancies: " + dependancies);

            packageDependancies = new PackageDependancy[dependancies];
            int deltaIndex = index + 1;
            for (index = deltaIndex; index < deltaIndex + dependancies; index++)
            {
                PackageDependancy pd = new PackageDependancy(lines[index]);

                packageDependancies[index- deltaIndex] = pd;
                //pd.Print();
            }
        }

        public bool IsValid()
        {
            mRequired = new List<PackageInfo>();// packages.ToList<PackageInfo>();

            //return Original();
            return ValidateDownloads();
        }

        private bool ValidateDownloads()
        {
            bool valid = true;
            foreach(PackageInfo pi in packages)
            {
                valid = AddPackage(pi);
                if (!valid) return valid;
            }
            return valid;
        }

        private List<PackageInfo> mRequired;
        private bool AddPackage(PackageInfo pi)
        {
            if(!mRequired.Contains(pi))
            {
                // try to add the dependancy
                PackageInfo conflict = null;
                bool added = mRequired.AddPackage(pi, out conflict);

                if (conflict != null)
                {
                    // Display some information as to the conflict
                    if (Global.DEBUG) Console.WriteLine("Conflict Adding Package: " + pi.display);
                    return false;
                }

                if (added)
                {
                    if (Global.DEBUG) Console.WriteLine("Added Package: " + pi.display);
                    return CheckDependancies(pi);
                }
            }
            return true;
        }

        private bool CheckDependancies(PackageInfo pi)
        {
            //mRequired = packages.ToList<PackageInfo>();
            bool result = true;
            foreach (PackageDependancy pd in packageDependancies)
            {
                if (pd.package.Equals(pi))
                {
                    result = AddDependancy(pd);
                }
            }
            return result;
        }

        private bool AddDependancy(PackageDependancy pd)
        {
            if (pd != null)
            {
                if (Global.DEBUG) Console.WriteLine("Adding Dependancies: " + pd.display);
                //PackageInfo newPkg = pd.dependancy;
                foreach (PackageInfo newPkg in pd.dependancies)
                {
                    if (!AddPackage(newPkg))
                        return false;
                }
            }
            return true;
        }

        private bool Original()
        {
            mRequired = packages.ToList<PackageInfo>();
            foreach (PackageDependancy pd in packageDependancies)
            {
                //PackageInfo newPkg = pd.dependancy;
                foreach (PackageInfo newPkg in pd.dependancies)
                {
                    // As we loop through the dependancies we check to see if that package is included
                    if (mRequired.Contains(pd.package))
                    {
                        // try to add the dependancy
                        PackageInfo conflict = null;
                        bool added = mRequired.AddPackage(newPkg, out conflict);
                        if (conflict != null)
                        {
                            // Display some information as to the conflict
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
