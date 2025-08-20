using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Conexões dos Painéis")]
    public GameObject painelDetalhes;
    public GameObject painelListaSuculentas;

    [Header("Conexões da UI de Detalhes")]
    public Image fotoSuculenta;
    public TextMeshProUGUI nomeSuculenta;
    public TextMeshProUGUI descricaoSuculenta;
    public TextMeshProUGUI luzSuculenta;
    public TextMeshProUGUI regaSuculenta;
    public TextMeshProUGUI soloSuculenta;
    public ScrollRect scrollRectDetalhes;

    void Start()
    {
        // Garante que o painel de detalhes comece escondido
        if (painelDetalhes != null)
        {
            painelDetalhes.SetActive(false);
        }
    }

    public void MostrarDetalhes(SuculentaData suculenta)
    {
        if (suculenta == null) return;

        fotoSuculenta.sprite = suculenta.foto;
        nomeSuculenta.text = suculenta.nome;
        descricaoSuculenta.text = suculenta.descricao;
        luzSuculenta.text = "Iluminação:\n" + suculenta.iluminacao;
        regaSuculenta.text = "Rega:\n" + suculenta.rega;
        soloSuculenta.text = "Substrato:\n" + suculenta.substrato;

        painelDetalhes.SetActive(true);
        painelListaSuculentas.SetActive(false);

        if (scrollRectDetalhes != null)
        {
            scrollRectDetalhes.verticalNormalizedPosition = 1f;
        }
    }

    public void FecharDetalhes()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();
        painelDetalhes.SetActive(false);
        painelListaSuculentas.SetActive(true);
    }

    public void VoltarParaMenu()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomTransicao();
        SceneManager.LoadScene("MenuPrincipal");
    }

    // --- NOVA FUNÇÃO ---
    public void IrParaQuiz()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomTransicao();
        SceneManager.LoadScene("Quiz");
    }

    // --- FUNÇÃO PARA OS BOTÕES DA LISTA (SOM DE CLIQUE) ---
    public void TocarSomDeClique()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.TocarSomClique();
        }
    }
}