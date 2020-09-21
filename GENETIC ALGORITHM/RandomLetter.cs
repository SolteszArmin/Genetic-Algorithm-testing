using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GENETIC_ALGORITHM
{
    class RandomLetter
    {

        public char GetLetter()
        {
             Random r = new Random();

            int num = r.Next(0, 26); 
            char let = (char)('a' + num);
            if (r.Next(0, 101) > 50)
            {
                let=char.ToUpper(let);
            }

            return let;
        }
    }
}
