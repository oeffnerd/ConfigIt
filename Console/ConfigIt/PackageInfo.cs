using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    class PackageInfo : InputType
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

        public override string Display()
        {
            return name + " v." + version;
        }
    }
}
