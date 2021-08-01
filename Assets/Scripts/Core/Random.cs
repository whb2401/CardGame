using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLH.Core
{
    // https://stackoverflow.com/questions/8090518/inconsistent-pseudo-random-between-c-and-c-sharp
    // int get_pseudo_rand()
    // {
    //     return (((_last_rand = (_last_rand * 214013 + 2531011) >> 16) & 0x7fff));
    // }

    public class Random
    {
        private int _nSeed = 0;
        private const int DEF_CONST_M = 0x7fff;
        private const int DEF_CONST_A = 214013;
        private const int DEF_CONST_B = 2531011;
        private const int DEF_INIT_SEED = 0;
        private int _nTimes = 0;

        public Random()
        {
            _nSeed = DEF_INIT_SEED;
        }

        public Random(int seed)
        {
            _nSeed = seed & 0x7FFF;
        }

        public void SetSeed(string hexString)
        {
            int seed = 0;
            hexString = hexString.Replace("-", "");
            for (var i = 0; i < 4; i++)
            {
                seed += Int32.Parse(hexString.Substring(i * 8, 8), System.Globalization.NumberStyles.HexNumber);
            }

            SetSeed(seed);
        }

        public void SetSeed(int nSeed)
        {
            _nSeed = nSeed * DEF_CONST_A + DEF_CONST_B;
            _nTimes = 0;
        }

        public int GetRand()
        {
            int nResult = _nSeed;
            _nSeed = _nSeed * DEF_CONST_A + DEF_CONST_B;
            _nTimes += 1;

            return ((nResult >> 16) & DEF_CONST_M);
        }
    }
}
