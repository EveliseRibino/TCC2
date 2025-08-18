using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    // Tempo em segundos que a splash screen vai durar
    public float tempoDeDuracao = 20f;

    // O nome da cena para carregar
    public string nomeDaCena = "MenuPrincipal";

    void Start()
    {
        // A função Invoke é perfeita para isso.
        // Ela vai chamar a função "CarregarProximaCena" DEPOIS que o tempoDeDuracao passar.
        Invoke("CarregarProximaCena", tempoDeDuracao);
    }

    void CarregarProximaCena()
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}