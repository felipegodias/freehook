using System.Collections.Generic;
using UnityEngine;

public class I18N {

    private static readonly Dictionary<SystemLanguage, Dictionary<string, string>> dictionary =
        new Dictionary<SystemLanguage, Dictionary<string, string>> {
            {
                SystemLanguage.English, new Dictionary<string, string> {
                    {"ads_screen_description", "Would you like to watch an advertisement to win {0} hearts?"},
                    {"no_more_ads_screen_description", "Sorry, we no longer have ads to show, please try again later. :("},
                    {"ok", "Ok"},
                    {"watch", "Watch"},
                    {"remove_ads", "Remove Ads"}
                }
            },{
                SystemLanguage.Portuguese, new Dictionary<string, string> {
                    {"ads_screen_description", "Gostaria de assistir uma propaganda para ganhar {0} corações?"},
                    {"no_more_ads_screen_description", "Desculpe, não temos mais propagandas para mostrar, tente novamente mais tarde. :("},
                    {"ok", "Ok"},
                    {"watch", "Assistir"},
                    {"remove_ads", "Remover propagandas"}
                }
            },{
                SystemLanguage.Spanish, new Dictionary<string, string> {
                    {"ads_screen_description", "¿Quieres ver una propaganda para ganar {0} corazones?"},
                    {"no_more_ads_screen_description", "Lo sentimos, ya no tenemos anuncios para mostrar, vuelve a intentarlo más tarde. :("},
                    {"ok", "Ok"},
                    {"watch", "Ver el anuncio"},
                    {"remove_ads", "Eliminar anuncios"}
                }
            },{
                SystemLanguage.Italian, new Dictionary<string, string> {
                    {"ads_screen_description", "Vorrei vedere una pubblicità per i cuori {0} vincente?"},
                    {"no_more_ads_screen_description", "Siamo spiacenti, non abbiamo annunci da mostrare, riprovare più tardi. :("},
                    {"ok", "Ok"},
                    {"watch", "Assistere"},
                    {"remove_ads", "Rimuovere annunci"}
                }
            },{
                SystemLanguage.French, new Dictionary<string, string> {
                    {"ads_screen_description", "Je voudrais regarder une publicité pour gagner {0} coeurs?"},
                    {"no_more_ads_screen_description", "Désolé, nous avons aucune publicité à afficher, réessayer plus tard. :("},
                    {"ok", "Ok"},
                    {"watch", "Assister à"},
                    {"remove_ads", "Retirer annonces"}
                }
            },
        };

    private static IDictionary<string, string> GetLanguageDictionary() {
        SystemLanguage systemLanguage = Application.systemLanguage;
        if (dictionary.ContainsKey(systemLanguage)) {
            return dictionary[systemLanguage];
        }
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