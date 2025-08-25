using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Conexões do Menu Lateral")]
    public GameObject painelMenuLateral;

    [Header("Conexões da Tela Principal")]
    public Image imagemPlantaDiaUI; // Arraste aqui a Imagem da Planta do Dia
    public TextMeshProUGUI textoAbaixoDaPlantaUI; // Arraste o texto "Toque para ver a dica"
    public Button botaoFotoPlantaDia; // Arraste a própria Imagem da Planta (que tem o componente Button)

    [Header("Conexões do Pop-up da Dica")]
    public GameObject painelPopupDica; // Arraste o PainelPopupDica
    public TextMeshProUGUI textoDicaDoDiaUI; // Arraste o texto que está DENTRO do pop-up

    private SuculentaData plantaSorteada; // Para guardar a planta que foi sorteada

    void Start()
    {
        // Garante que o pop-up comece sempre fechado
        if (painelPopupDica != null)
        {
            painelPopupDica.SetActive(false);
        }

        if (painelMenuLateral != null)
        {
            painelMenuLateral.SetActive(false);
        }

        SortearPlantaDoDia();
    }

    // --- FUNÇÃO PARA O BOTÃO DO MENU SANDUÍCHE ---
    public void ToggleMenuLateral()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();

        if (painelMenuLateral != null)
        {
            // Pega o estado atual do painel (está ativo ou inativo?)
            bool estaAtivo = painelMenuLateral.activeSelf;

            // Inverte o estado: se estava ativo, desativa. Se estava inativo, ativa.
            painelMenuLateral.SetActive(!estaAtivo);
        }

        if (painelPopupDica != null) painelPopupDica.SetActive(false);
    }
    void SortearPlantaDoDia()
    {
        if (AudioManager.instance == null || AudioManager.instance.todasAsSuculentas.Count == 0)
        {
            // Se não houver plantas, desativa o botão e mostra uma mensagem padrão
            textoAbaixoDaPlantaUI.text = "Bem-vindo ao FLOReve!";
            if (botaoFotoPlantaDia != null) botaoFotoPlantaDia.interactable = false;
            return;
        }

        // Pega a lista e sorteia uma planta
        var todasAsSuculentas = AudioManager.instance.todasAsSuculentas;
        int indexAleatorio = Random.Range(0, todasAsSuculentas.Count);
        plantaSorteada = todasAsSuculentas[indexAleatorio];

        // Atualiza a imagem principal e o texto abaixo dela
        if (imagemPlantaDiaUI != null) imagemPlantaDiaUI.sprite = plantaSorteada.foto;
        if (textoAbaixoDaPlantaUI != null) textoAbaixoDaPlantaUI.text = "Planta do Dia: " + plantaSorteada.nome + "\n(Toque para ver a dica)";
    }

    // --- FUNÇÕES DOS BOTÕES ---

    public void AbrirPopupDica()
    {
        if (plantaSorteada == null) return;

        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();

        if (painelMenuLateral != null) painelMenuLateral.SetActive(false);

        // Preenche o texto do pop-up com a dica da planta que foi sorteada
        if (textoDicaDoDiaUI != null)
        {
            textoDicaDoDiaUI.text = "Você Sabia?\n\n" + plantaSorteada.dicaCuriosa;
        }

        // Mostra o painel pop-up
        if (painelPopupDica != null)
        {
            painelPopupDica.SetActive(true);
        }
    }

    public void FecharPopupDica()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();
        if (painelPopupDica != null)
        {
            painelPopupDica.SetActive(false);
        }
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
        SceneManager.LoadScene("Quiz"); // Por enquanto vai direto para o Quiz
    }

    public void IrParaSobre()
    {
        Debug.Log("Botão 'Sobre' clicado! Criar a cena 'Sobre' depois.");
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();
    }
}