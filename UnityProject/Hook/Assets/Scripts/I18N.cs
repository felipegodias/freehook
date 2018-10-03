using System.Collections.Generic;

using UnityEngine;

public class I18N
{

    private static readonly Dictionary<SystemLanguage, Dictionary<string, string>> dictionary =
        new Dictionary<SystemLanguage, Dictionary<string, string>>
        {
            {
                SystemLanguage.English, new Dictionary<string, string>
                {
                    {
                        "ads_screen_description", "Would you like to watch an advertisement to win {0} hearts?"
                    },
                    {
                        "no_more_ads_screen_description",
                        "Sorry, we no longer have ads to show, please try again later. :("
                    },
                    {
                        "ok", "Ok"
                    },
                    {
                        "watch", "Watch"
                    },
                    {
                        "remove_ads", "Remove Ads"
                    },
                    {
                        "tutorial_1", "Click on the circles to pull the lines."
                    },
                    {
                        "tutorial_3", "Be careful with hooks! Don't let the lines touch each other."
                    },
                    {
                        "tutorial_17", "Put the switches in the correct position before click on the circle."
                    },
                    {
                        "privacy_policy","To continue, you must agree to our <u>privacy policy<u>."
                    },
                    { 
                        "agree","I agree."
                    }
                }
            },
            {
                SystemLanguage.Portuguese, new Dictionary<string, string>
                {
                    {
                        "ads_screen_description", "Gostaria de assistir uma propaganda para ganhar {0} corações?"
                    },
                    {
                        "no_more_ads_screen_description",
                        "Desculpe, não temos mais propagandas para mostrar, tente novamente mais tarde. :("
                    },
                    {
                        "ok", "Ok"
                    },
                    {
                        "watch", "Assistir"
                    },
                    {
                        "remove_ads", "Remover propagandas"
                    },
                    {
                        "tutorial_1", "Clique nos círculos para puxar as linhas."
                    },
                    {
                        "tutorial_3", "Cuidado com os gachos! Não deixe as linhas se tocarem."
                    },
                    {
                        "tutorial_17", "Coloque os interruptores na posição correta antes de clicar nos circulos."
                    }
                }
            },
            {
                SystemLanguage.Spanish, new Dictionary<string, string>
                {
                    {
                        "ads_screen_description", "¿Quieres ver una propaganda para ganar {0} corazones?"
                    },
                    {
                        "no_more_ads_screen_description",
                        "Lo sentimos, ya no tenemos anuncios para mostrar, vuelve a intentarlo más tarde. :("
                    },
                    {
                        "ok", "Ok"
                    },
                    {
                        "watch", "Ver el anuncio"
                    },
                    {
                        "remove_ads", "Eliminar anuncios"
                    },
                    {
                        "tutorial_1", "Haga clic en los círculos para tirar de las líneas."
                    },
                    {
                        "tutorial_3", "¡Ten cuidado con los ganchos! No dejes que las líneas se toquen entre sí."
                    },
                    {
                        "tutorial_17", "Coloque los interruptores en la posición correcta antes de hacer clic en el círculo."
                    }
                }
            },
            {
                SystemLanguage.Italian, new Dictionary<string, string>
                {
                    {
                        "ads_screen_description", "Vorrei vedere una pubblicità per i cuori {0} vincente?"
                    },
                    {
                        "no_more_ads_screen_description",
                        "Siamo spiacenti, non abbiamo annunci da mostrare, riprovare più tardi. :("
                    },
                    {
                        "ok", "Ok"
                    },
                    {
                        "watch", "Assistere"
                    },
                    {
                        "remove_ads", "Rimuovere annunci"
                    },
                    {
                        "tutorial_1", "Clicca sui cerchi per tirare le linee."
                    },
                    {
                        "tutorial_3", "Stai attento con i ganci! Non lasciare che le linee si tocchino."
                    },
                    {
                        "tutorial_17", "Mettere gli interruttori nella posizione corretta prima di fare clic sul cerchio."
                    }
                }
            },
            {
                SystemLanguage.French, new Dictionary<string, string>
                {
                    {
                        "ads_screen_description", "Je voudrais regarder une publicité pour gagner {0} coeurs?"
                    },
                    {
                        "no_more_ads_screen_description",
                        "Désolé, nous avons aucune publicité à afficher, réessayer plus tard. :("
                    },
                    {
                        "ok", "Ok"
                    },
                    {
                        "watch", "Assister à"
                    },
                    {
                        "remove_ads", "Retirer annonces"
                    },
                    {
                        "tutorial_1", "Cliquez sur les cercles pour tirer les lignes."
                    },
                    {
                        "tutorial_3", "Soyez prudent avec les crochets! Ne laissez pas les lignes se toucher."
                    },
                    {
                        "tutorial_17", "Mettez les commutateurs dans la bonne position avant de cliquer sur le cercle."
                    }
                }
            }
        };

    private static IDictionary<string, string> GetLanguageDictionary()
    {
        SystemLanguage systemLanguage = Application.systemLanguage;
        if (dictionary.ContainsKey(systemLanguage))
        {
            return dictionary[systemLanguage];
        }

        return dictionary[SystemLanguage.English];
    }

    public static string GetText(string key, params object[] args)
    {
        IDictionary<string, string> lang = GetLanguageDictionary();
        if (!lang.ContainsKey(key))
        {
            return null;
        }

        string text = lang[key];
        return string.Format(text, args);
    }

}