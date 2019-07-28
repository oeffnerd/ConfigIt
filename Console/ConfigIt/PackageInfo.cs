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
            string[] pkgInfo = value.Split(Library.COMMA);

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
        
        private void SetInfo(string sName, string sVersion)
        {
            name = sName;
            version = sVersion;
        }

        public bool CheckConflict (PackageInfo pi)
        {
            bool nameMatch = name.Equals(pi.name);
            bool versionMatch = version.Equals(pi.version);

            // If name !match no conflict, if it does and version doesnt there is
            return (nameMatch && !versionMatch);
        }

        public override string ToString()
        {
            return name + " v." + version; //"<{name}> v.<{version}>"
        }

        public bool Equals (PackageInfo pi)
        {
            return (name.Equals(pi.name) && version.Equals(pi.version));
        }
    }
}
