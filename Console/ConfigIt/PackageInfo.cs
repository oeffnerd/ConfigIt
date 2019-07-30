using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    class PackageInfo : InputType, IEquatable<PackageInfo>
    {
        public string name;
        public string version;

        public PackageInfo (string value)
        {
            // Formatted "A,1"
            string[] pkgInfo = value.Split(Global.COMMA);

            if (pkgInfo.Length != 2)
            {
                error = true;
                return;
            }

            SetInfo(pkgInfo[0], pkgInfo[1]);
        }

        public PackageInfo(string sName, string sVersion)
        {
            SetInfo(sName, sVersion);
        }
        
        /// <summary>
        /// Sets name and version
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sVersion"></param>
        private void SetInfo(string sName, string sVersion)
        {
            name = sName;
            version = sVersion;
        }

        /// <summary>
        /// Check for matching name but conflicting versions
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool CheckConflict (PackageInfo pi)
        {
            bool nameMatch = name.Equals(pi.name);
            bool versionMatch = version.Equals(pi.version);

            // If name !match no conflict, if it does and version doesnt there is
            return (nameMatch && !versionMatch);
        }

        /// <summary>
        /// Display String for output purposes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name + " v." + version; 
        }

        /// <summary>
        /// Do both packages match in name and version
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool Equals (PackageInfo pi)
        {
            return (name.Equals(pi.name) && version.Equals(pi.version));
        }
    }
}
