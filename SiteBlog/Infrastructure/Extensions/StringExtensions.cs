﻿using System.Globalization;
using System.Text;

namespace SiteBlog.Infrastructure.Extensions;
public static class StringExtensions
{
    // https://stackoverflow.com/questions/5459641/replacing-characters-in-c-sharp-ascii/13154805
    public static string RemoveDiacritics(this string stIn)
    {
        string stFormD = stIn.Normalize(NormalizationForm.FormD);

        StringBuilder sb = new StringBuilder();

        for (int ich = 0; ich < stFormD.Length; ich++)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);

            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(stFormD[ich]);
            }
        }

        return (sb.ToString().Normalize(NormalizationForm.FormC));
    }
}