using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    class PackageDependancy : InputType
    {
        public PackageInfo package;
        public PackageInfo[] dependancies;

        public PackageDependancy(string value)
        {
            string[] parts = value.Split(Library.COMMA);
            if (parts.Length < 4 || (parts.Length % 2 != 0))
            {
                error = true;
                return;
            }

            package = new PackageInfo(parts[0], parts[1]);

            int pairs = parts.Length / 2;
            dependancies = new PackageInfo[pairs - 1];
            for(int i=1; i<pairs; i++)
            {
                int start = i * 2;
                dependancies[i-1] = new PackageInfo(parts[start], parts[start+1]);
            }
        }

        public override string ToString()
        {
            string display = package.display + " Requires ";
            for(int i=0; i<dependancies.Length;i++)
            {
                if (i != 0) display += ", ";
                display += dependancies[i].display;
            }
            return display;
        }
    }
}
