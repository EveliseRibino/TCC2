using UnityEngine;
using UnityEngine.SceneManagement;

public class NavegadorDeCenas : MonoBehaviour
{
    public void CarregarCena(string nomeDaCena)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.TocarSomTransicao();
        }
        SceneManager.LoadScene(nomeDaCena);
    }

    public void TocarSomDeClique()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.TocarSomClique();
        }
    }
}