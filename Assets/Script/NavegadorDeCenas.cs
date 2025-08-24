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

    public void IrParaMenuDeJogos()
    {
        // Por enquanto, vamos fazer ir direto para o Quiz
        // No futuro, podemos criar uma cena "MenuJogos"
        CarregarCena("Quiz");
    }

    public void IrParaSobre()
    {
        // Ainda não temos a cena "Sobre", então vamos só registrar o clique
        Debug.Log("Botão 'Sobre' clicado!");
        TocarSomDeClique();
    }
}