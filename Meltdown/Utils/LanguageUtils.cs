﻿using R2API;
using RoR2;
using System.Collections.Generic;

namespace Meltdown.Utils
{
    public static class LanguageUtils
    {
        private static Dictionary<string, string[]> translationFormats = new Dictionary<string, string[]>();

        public static void AddTranslationFormat(string translationKey, string[] formats)
        {
            translationFormats.Add(translationKey, formats);
        }

        public static void Language_onCurrentLanguageChanged()
        {
            foreach (KeyValuePair<string, string[]> translationFormat in translationFormats)
            {
                string formattedTranslation = Language.GetStringFormatted(translationFormat.Key, translationFormat.Value);
                LanguageAPI.AddOverlay(translationFormat.Key, formattedTranslation);
            }
        }
    }
}
