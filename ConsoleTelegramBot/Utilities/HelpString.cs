using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Utilities
{
    public static class HelpString
    {
        public static int GetSummary(string Value, char Separator)
        {
            string[] values = Value.Split(Separator);
            int summ = 0;
            int currentValue = 0;
            for (int i = 0; i < values.Length; i++)
            {
                try
                {
                    currentValue = Convert.ToInt32(values[i]);
                }
                catch
                {
                    currentValue = 0;
                } 

                summ += currentValue;
            }    
            return summ;
        }

        //public static int GetLenth(string Value)
        //{
           // return Value.Length;
        //}
        public delegate int GetLenth(string Value);
        public static GetLenth LV = Value => Value.Length;
    }
}
