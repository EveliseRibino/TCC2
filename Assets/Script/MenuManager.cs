using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Conexões do Menu Lateral")]
    public GameObject painelMenuLateral;

    [Header("Conexões da Tela Principal")]
    public Image imagemPlantaDiaUI;
    public TextMeshProUGUI textoAbaixoDaPlantaUI;
    public Button botaoFotoPlantaDia;

    [Header("Conexões do Pop-up da Dica")]
    public GameObject painelPopupDica;
    public TextMeshProUGUI textoDicaDoDiaUI;

    private SuculentaData plantaSorteada;
    private bool menuAberto = false;

    void Start()
    {
        if (painelPopupDica != null) painelPopupDica.SetActive(false);
        if (painelMenuLateral != null) painelMenuLateral.SetActive(false);

        SortearPlantaDoDia();
    }

    // --- FUNÇÃO PARA O BOTÃO DO MENU SANDUÍCHE COM ANIMAÇÃO ---
    public void ToggleMenuLateral()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();

        RectTransform rt = painelMenuLateral.GetComponent<RectTransform>();

        if (!menuAberto)
        {
            painelMenuLateral.SetActive(true);
            rt.anchoredPosition = new Vector2(-600, 0);
            LeanTween.moveX(rt, 0, 0.8f).setEaseOutExpo();
            menuAberto = true;
        }
        else
        {
            LeanTween.moveX(rt, -600, 0.8f).setEaseInExpo().setOnComplete(() =>
            {
                painelMenuLateral.SetActive(false);
            });
            menuAberto = false;
        }

        if (painelPopupDica != null) painelPopupDica.SetActive(false);
    }

    void SortearPlantaDoDia()
    {
        if (AudioManager.instance == null || AudioManager.instance.todasAsSuculentas.Count == 0)
        {
            textoAbaixoDaPlantaUI.text = "Bem-vindo ao FLOReve!";
            if (botaoFotoPlantaDia != null) botaoFotoPlantaDia.interactable = false;
            return;
        }

        var todasAsSuculentas = AudioManager.instance.todasAsSuculentas;
        int indexAleatorio = Random.Range(0, todasAsSuculentas.Count);
        plantaSorteada = todasAsSuculentas[indexAleatorio];

        if (imagemPlantaDiaUI != null) imagemPlantaDiaUI.sprite = plantaSorteada.foto;
        if (textoAbaixoDaPlantaUI != null)
            textoAbaixoDaPlantaUI.text = "Planta do Dia: " + plantaSorteada.nome + "\n(Toque para ver a dica)";
    }

    // --- FUNÇÕES DOS BOTÕES ---

    public void AbrirPopupDica()
    {
        if (plantaSorteada == null) return;

        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();
        if (painelMenuLateral != null) painelMenuLateral.SetActive(false);

        if (textoDicaDoDiaUI != null)
            textoDicaDoDiaUI.text = "Você Sabia?\n\n" + plantaSorteada.dicaCuriosa;

        if (painelPopupDica != null) painelPopupDica.SetActive(true);
    }

    public void FecharPopupDica()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();
        if (painelPopupDica != null) painelPopupDica.SetActive(false);
    }

    // --- FUNÇÕES DE NAVEGAÇÃO DA BARRA DE RODAPÉ ---

    public void IrParaGuiaDeCuidados()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomTransicao();
        SceneManager.LoadScene("Enciclopedia");
    }

    public void IrParaMenuDeJogos()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomTransicao();
        SceneManager.LoadScene("Quiz");
    }

    public void IrParaSobre()
    {
        Debug.Log("Botão 'Sobre' clicado! Criar a cena 'Sobre' depois.");
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();
    }
}
