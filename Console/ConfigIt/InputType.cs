using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    class InputType
    {
        public bool error { get; set; }
        public string display { get { return ToString(); } }

        public void Print ()
        {
            if (error) Console.WriteLine("Error Reading Value");
            else DoPrint();
        }

        protected virtual void DoPrint()
        {
            Console.WriteLine(display);
        }

        public virtual string ToString ()
        {
            return "";
        }
    }
}
