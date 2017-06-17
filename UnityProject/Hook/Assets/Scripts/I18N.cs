using System.Collections.Generic;
using UnityEngine;

public class I18N {

    private static readonly Dictionary<SystemLanguage, Dictionary<string, string>> dictionary =
        new Dictionary<SystemLanguage, Dictionary<string, string>> {
            {
                SystemLanguage.English, new Dictionary<string, string> {
                    {"ads_screen_description", "Watch ads for {0} hearts?"},
                    {"no_more_ads_screen_description", "We don`t have any more ads to show. Please try again later. :("},
                    {"ok", "ok"},
                    {"watch", "watch"},
                    {"remove_ads", "Remove Ads"}
                }
            },
            {
                SystemLanguage.Portuguese, new Dictionary<string, string> {
                    {"ads_screen_description", "Assistir propaganda para ganhar {0} corações?"},
                    {"no_more_ads_screen_description", "Desculpe, não temos mais propagandas para mostrar, tente novamente mais tarde. :("},
                    {"ok", "ok"},
                    {"watch", "Assistir"},
                    {"remove_ads", "Remove propagandas"}
                }
            },
        };

    private static IDictionary<string, string> GetLanguageDictionary() {
        /*SystemLanguage systemLanguage = Application.systemLanguage;
        if (dictionary.ContainsKey(systemLanguage)) {
            return dictionary[systemLanguage];
        }*/
        return dictionary[SystemLanguage.English];
    }

    public static string GetText(string key, params object[] args) {
        IDictionary<string, string> lang = GetLanguageDictionary();
        if (!lang.ContainsKey(key)) {
            return null;
        }
        string text = lang[key];
        return string.Format(text, args);
    }

}