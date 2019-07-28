using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    class PackageDependancy : InputType
    {
        public PackageInfo package;
        public PackageInfo dependancy;

        public PackageDependancy(string value)
        {
            string[] parts = value.Split(Library.COMMA);
            if (parts.Length != 4)
            {
                error = true;
                return;
            }

            package = new PackageInfo(parts[0], parts[1]);
            dependancy = new PackageInfo(parts[2], parts[3]);
        }

        public override string Display()
        {
            return package.display + " Requires " + dependancy.display ;
        }
    }
}
