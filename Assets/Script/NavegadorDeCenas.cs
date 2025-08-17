using UnityEngine;
using UnityEngine.SceneManagement;

public class NavegadorDeCenas : MonoBehaviour
{
    // Start is called before the first frame update
    public void CarregarCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
