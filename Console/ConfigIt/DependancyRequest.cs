using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConfigIt
{
    /// <summary>
    /// Acts as the main object for each file that loads and validates the request
    /// </summary>
    class DownloadRequest : InputType
    {
        private int mDownloads = 0;
        private PackageInfo[] mPackages;
        private int mDependancies = 0;
        private PackageDependancy[] mPackageDependancies = new PackageDependancy[0];
        private List<PackageInfo> mRequired;

        public bool LOADED { get; set; }

        /// <summary>
        /// Constructor consisting of the lines of the file
        /// </summary>
        /// <param name="lines"></param>
        public DownloadRequest(string[] lines)
        {
            Load(lines);
        }

        /// <summary>
        /// Split up for now because down the road you may want 
        /// to load it outside of the constructor
        /// </summary>
        /// <param name="lines"></param>
        private void Load(string[] lines)
        {
            int index = 0;

            //Parse the first line looking for a number of downloads requested
            //returns if its not of the proper format
            error = InputUtility.ParseInt(lines[index], out mDownloads);
            if (error) return;

            // Now cast all of the packages
            mPackages = new PackageInfo[mDownloads];
            for (index = 1; index < 1 + mDownloads; index++)
            {
                PackageInfo package = new PackageInfo(lines[index]);
                mPackages[index - 1] = package;

                error = package.error;
                //returns if its not of the proper format
                if (error)
                    return;
            }

            LOADED = true;

            // if current index == lines.length there are no dependancies
            if (lines.Length == index) return;

            //Parse the first line looking for a number of dependancies
            //returns if its not of the proper format
            error = InputUtility.ParseInt(lines[index], out mDependancies);
            if (error) return;

            mPackageDependancies = new PackageDependancy[mDependancies];
            int deltaIndex = index + 1;
            for (index = deltaIndex; index < deltaIndex + mDependancies; index++)
            {
                PackageDependancy pd = new PackageDependancy(lines[index]);

                mPackageDependancies[index - deltaIndex] = pd;

                error = pd.error;
                //returns if its not of the proper format
                if (error)
                    return;
            }
        }

        /// <summary>
        /// Is it a valid request
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            mRequired = new List<PackageInfo>();// packages.ToList<PackageInfo>();

            //return Original();
            return ValidateDownloads();
        }

        private bool ValidateDownloads()
        {
            bool valid = true;
            foreach (PackageInfo pi in mPackages)
            {
                valid = AddPackage(pi);
                if (!valid) return valid;
            }
            return valid;
        }

        /// <summary>
        /// Adds a package to the required list and check for conflicts
        /// if there is a conflict then pass it back and validate will fail
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        private bool AddPackage(PackageInfo pi)
        {
            if (!mRequired.Contains(pi))
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

        /// <summary>
        /// Checks all package dependancies for one matching "pi"
        /// If it matches then those dependancies need to be added
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        private bool CheckDependancies(PackageInfo pi)
        {
            //mRequired = packages.ToList<PackageInfo>();
            bool result = true;
            foreach (PackageDependancy pd in mPackageDependancies)
            {
                if (pd.package.Equals(pi))
                {
                    result = AddDependancy(pd);
                }
            }
            return result;
        }

        /// <summary>
        /// Loops through a package dependancy and adds all required packages
        /// Part
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
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

        // Works in farely low iterations but fails when adding a package whos dependancy has already been checked.
        private bool Original()
        {
            mRequired = mPackages.ToList<PackageInfo>();
            foreach (PackageDependancy pd in mPackageDependancies)
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
