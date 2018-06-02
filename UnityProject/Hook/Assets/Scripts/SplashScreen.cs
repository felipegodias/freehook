using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    private void Start()
    {
        SceneManager.LoadScene("Game");
    }

}