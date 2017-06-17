using System.Collections.Generic;

public class I18N {

    private static readonly Dictionary<string, Dictionary<string, string>> dictionary =
        new Dictionary<string, Dictionary<string, string>> {
            {
                "en", new Dictionary<string, string> {
                    {"ads_screen_description", "Watch ads for {0} hearts?"},
                    {"no_more_ads_screen_description", "We don`t have any more ads to show. Please try again later. :("},
                    {"ok", "ok"},
                    {"watch", "watch"},
                    {"remove_ads", "Remove Ads"}
                }
            },
            {
                "pt", new Dictionary<string, string> {
                    {"ads_screen_description", "Assistir propaganda por {0} corações?"},
                    {"no_more_ads_screen_description", "Desculpe, não temos mais propagandas para mostrar, tente novamente mais tarde. :("},
                    {"ok", "ok"},
                    {"watch", "Assistir"},
                    {"remove_ads", "Remove propagandas"}
                }
            },
        };

    public static string GetText(string key, params object[] args) {
        Dictionary<string, string> lang = dictionary["en"];
        if (!lang.ContainsKey(key)) {
            return null;
        }
        string text = lang[key];
        return string.Format(text, args);
    }

}