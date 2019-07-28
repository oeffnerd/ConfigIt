using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    class DownloadRequest : InputType
    {
        int downloads = 0;
        PackageInfo[] packages;
        int dependancies = 0;
        PackageDependancy[] packageDependancies;

        public DownloadRequest(string[] lines)
        {
            Load(lines);
        }

        private void Load(string[] lines)
        {
            //foreach (string line in lines)
                //Console.WriteLine(line);

            int index = 0;

            error = InputUtility.ParseInt(lines[index], out downloads);
            if(error)
            {
                Console.WriteLine("Error Parsing Int: " + lines[index]);
                return;
            }
            Console.WriteLine("Number of requested downloads: " + downloads);

            packages = new PackageInfo[downloads];
            for (index = 1; index < 1+downloads; index++)
            {
                PackageInfo package = new PackageInfo(lines[index]);
                packages[index - 1] = package;
                package.Print();
            }

            // if current index == lines.length there are no dependancies
            if (lines.Length == index) return;

            error = InputUtility.ParseInt(lines[index], out dependancies);

            if (error)
                return;
            Console.WriteLine("Number of package dependancies: " + dependancies + " : " + index);

            packageDependancies = new PackageDependancy[dependancies];
            int deltaIndex = index + 1;
            for (index = deltaIndex; index < deltaIndex + dependancies; index++)
            {
                PackageDependancy pd = new PackageDependancy(lines[index]);

                packageDependancies[index- deltaIndex] = pd;
                pd.Print();
            }
        }

        private bool IsValid()
        {
            /*List<PackageInfo> required = new List<PackageInfo>();
            foreach(PackageInfo pi in packages)
            {
                required.Add(pi);
                foreach ()
            }*/
            return false;
        }
    }
}
