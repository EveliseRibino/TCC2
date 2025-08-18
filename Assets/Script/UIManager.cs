using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject painelDetalhes;
    public GameObject painelListaSuculentas;
    public Image fotoSuculenta;
    public TextMeshProUGUI nomeSuculenta;
    public TextMeshProUGUI descricaoSuculenta;
    public TextMeshProUGUI luzSuculenta;
    public TextMeshProUGUI regaSuculenta;
    public TextMeshProUGUI soloSuculenta;

    void Start()
    {
        painelDetalhes.SetActive(false);
    }

    public void MostrarDetalhes(SuculentaData suculenta)
    {
        fotoSuculenta.sprite = suculenta.foto;
        nomeSuculenta.text = suculenta.nome;
        descricaoSuculenta.text = suculenta.descricao;
        luzSuculenta.text = "Luz: " + suculenta.iluminacao;
        regaSuculenta.text = "Rega: " + suculenta.rega;
        soloSuculenta.text = "Solo: " + suculenta.substrato;

        painelDetalhes.SetActive(true);
        painelListaSuculentas.SetActive(false);
    }

    public void FecharDetalhes()
    {
        painelDetalhes.SetActive(false);
        painelListaSuculentas.SetActive(true);
    }

    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void TocarSomDeClique()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.TocarSomClique();
        }
    }
}
