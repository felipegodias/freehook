using System.Collections.Generic;

public class I18N {

    private static readonly Dictionary<string, string> dictionary = new Dictionary<string, string> {
        {"ads_screen_description", "Watch ads for {0} hearts?"}
    };

    public static string GetText(string key, params object[] args) {
        if (!dictionary.ContainsKey(key)) {
            return null;
        }
        string text = dictionary[key];
        return string.Format(text, args);
    }

}