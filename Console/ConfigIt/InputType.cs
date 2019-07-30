using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    class InputType
    {
        public bool error { get; set; }
        public string display { get { return ToString(); } }

        /// <summary>
        /// Print to console, unless there is an error
        /// </summary>
        public void Print ()
        {
            if (error) Console.WriteLine(Global.ERROR_PARSE);
            else DoPrint();
        }

        /// <summary>
        /// Print out the display string
        /// </summary>
        protected virtual void DoPrint()
        {
            Console.WriteLine(display);
        }

        public override string ToString ()
        {
            return "";
        }
    }
}
