using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPlus
{
    public static class ChinesePunctuationConverter
    {
        private static readonly Dictionary<char, char> FullToHalf = new Dictionary<char, char>()
        {
            ['，'] = ',',
            ['。'] = '.',
            ['！'] = '!',
            ['？'] = '?',
            ['：'] = ':',
            ['；'] = ';',
            ['（'] = '(',
            ['）'] = ')',
            ['〈'] = '<',
            ['〉'] = '>',
            //['“'] = '"',
            //['”'] = '"',
            //['‘'] = '\'',
            //['’'] = '\'',
            ['～'] = '~',
            ['｜'] = '|',
            ['￥'] = '\\',
            ['　'] = ' '
        };

        private static readonly Dictionary<char, char> HalfToFull =
            FullToHalf.ToDictionary(x => x.Value, x => x.Key);

        public static char ConvertSymbolToFullWidth(char c)
        {
            return HalfToFull.TryGetValue(c, out char result)
                ? result
                : c;
        }

        public static char ConvertSymbolToHalfWidth(char c)
        {
            return FullToHalf.TryGetValue(c, out char result)
                ? result
                : c;
        }
    }
}
