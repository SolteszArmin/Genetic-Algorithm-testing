using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GENETIC_ALGORITHM
{
    class RandomLetter
    {
        Random r = new Random();
        private readonly string legalCharacters = " öüóqwertzuiopőúasdfghjkléáűíyxcvbnm";
        public char GetLetters()
        {
            char let = legalCharacters[r.Next(legalCharacters.Length)];

            if (r.Next(0, 101) > 50)
            {
                let = char.ToUpper(let);
            }
            

            return let;
        }
    }
}
