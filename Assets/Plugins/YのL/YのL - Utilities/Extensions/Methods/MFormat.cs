using System.Globalization;
using System;

namespace YNL.Extension.Method
{
    public static class MFormat
    {
        #region 💱 Unit Format
        public static string DecimalFormat(this int number, int digit) => number.ToString($"D{digit}");
        public static string FloatFormat(this float number, int digit) => number.ToString($"F{digit}");
        public static string HexadecimalFormat(this float number, int digit) => number.ToString($"X{digit}");
        public static string CommaSeparated(this int number) => number.ToString("#,#", CultureInfo.CurrentCulture);
        #endregion
        #region 🕓 Time Format
        /// <summary>
        /// Format the interger time into time format. <br></br><br></br>
        /// 
        /// <i>"00:00" | "colon"</i>: Time'll be like 59:01, 01:20:59, ect <br></br>
        /// <i>"hhmmss" | "letter"</i>: Time'll be like 59m01s, 01h20m59s, ect <br></br>
        /// </summary>
        public static string TimeFormat(this int time, string type)
        {
            switch (type)
            {
                case "00:00":
                case "00:00:00":
                case "00:00:00:00":
                case "Colon":
                case "colon":
                    return TimeFormatColon(time);
                case "hhmmss":
                case "ddhhmmss":
                case "Letter":
                case "letter":
                    return TimeFormatLetter(time);
            }
            return "";
        }
        public static string TimeFormatColon(int time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            if (time < 3600) return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            else if (time < 86400) return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            else return $"{timeSpan.Days:D2}:{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
        public static string TimeFormatLetter(this float time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            if (time < 60) return $"{timeSpan.Seconds:D2}s";
            else if (time < 3600) return $"{timeSpan.Minutes:D2}m{timeSpan.Seconds:D2}s";
            else if (time < 86400) return $"{timeSpan.Hours:D2}h{timeSpan.Minutes:D2}m{timeSpan.Seconds:D2}s";
            else return $"{timeSpan.Days:D2}d{timeSpan.Hours:D2}h{timeSpan.Minutes:D2}m{timeSpan.Seconds:D2}s";
        }

        #endregion
    }
}