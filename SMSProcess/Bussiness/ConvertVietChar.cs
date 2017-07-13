using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace SMSProcess.Bussiness
{
    public class ConvertVietChar
    {
            public static string convertToUnSign2(string s)
            {
                string stFormD = s.Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder();
                for (int ich = 0; ich < stFormD.Length; ich++)
                {
                    System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                    if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(stFormD[ich]);
                    }
                }
                sb = sb.Replace('Đ', 'D');
                sb = sb.Replace('đ', 'd');
                return (sb.ToString().Normalize(NormalizationForm.FormD));
            }
            public static bool ContainsUnicodeCharacter(string input)
            {
                const int MaxAnsiCode = 255;

                return input.Any(c => c > MaxAnsiCode);
            }
    }
}