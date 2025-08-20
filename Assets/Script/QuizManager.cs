using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestaoQuiz> todasAsPergunras;
    private List<QuestaoQuiz> perguntasDisponiveis;
    private QuestaoQuiz perguntaAtual;

    private int pontuacao = 0;
    private int numeroDaQuestaoAtual = 0;
    private bool respostaFoiDada = false;

    [Header("Conexões do Painel Principal do Quiz")]
    public GameObject painelDoQuiz;
    public TextMeshProUGUI textoPerguntaUI;
    public Button[] botoesRespostaUI;
    public TextMeshProUGUI textoPontuacaoUI;
    public TextMeshProUGUI textoNumeroQuestaoUI;

    [Header("Conexões do Painel de Fim de Jogo")]
    public GameObject painelFimDeJogo;
    public TextMeshProUGUI textoPontuacaoFinalUI;
    public Button botaoRecompensaUI;

    [Header("Conexões do Painel de Recompensa")]
    public GameObject painelRecompensa;
    public TextMeshProUGUI textoDicaRecompensa;

    void Awake()
    {
        if (painelFimDeJogo != null) painelFimDeJogo.SetActive(false);
        if (painelRecompensa != null) painelRecompensa.SetActive(false);
        if (painelDoQuiz != null) painelDoQuiz.SetActive(true);
    }

    void Start()
    {
        ComecarJogo();
    }

    void ComecarJogo()
    {
        pontuacao = 0;
        numeroDaQuestaoAtual = 0;
        perguntasDisponiveis = new List<QuestaoQuiz>(todasAsPergunras);
        StartCoroutine(CarregarProximaPerguntaComDelay(0f));
    }

    void MostrarPerguntaNaTela()
    {
        respostaFoiDada = false;
        SetBotoesInteragiveis(true);
        textoPerguntaUI.text = perguntaAtual.pergunta;

        for (int i = 0; i < botoesRespostaUI.Length; i++)
        {
            Image[] imagensNoBotao = botoesRespostaUI[i].GetComponentsInChildren<Image>();
            if (imagensNoBotao.Length > 1)
            {
                Image imagemDaSuculenta = imagensNoBotao[1];
                imagemDaSuculenta.sprite = perguntaAtual.respostasImagens[i];
                imagemDaSuculenta.color = Color.white;
            }
        }
        AtualizarTextosUI();
    }

    public void OnRespostaSelecionada(int indiceDoBotao)
    {
        if (respostaFoiDada) return;

        respostaFoiDada = true;
        SetBotoesInteragiveis(false);

        if (indiceDoBotao == perguntaAtual.indiceRespostaCorreta)
        {
            pontuacao += 10;
            botoesRespostaUI[indiceDoBotao].GetComponentsInChildren<Image>()[1].color = Color.green;
            if (AudioManager.instance != null) AudioManager.instance.TocarSomAcerto();
        }
        else
        {
            botoesRespostaUI[indiceDoBotao].GetComponentsInChildren<Image>()[1].color = Color.red;
            botoesRespostaUI[perguntaAtual.indiceRespostaCorreta].GetComponentsInChildren<Image>()[1].color = Color.green;
            if (AudioManager.instance != null) AudioManager.instance.TocarSomErro();
        }

        AtualizarTextosUI();
        StartCoroutine(CarregarProximaPerguntaComDelay(2.0f));
    }

    IEnumerator CarregarProximaPerguntaComDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (perguntasDisponiveis.Count == 0)
        {
            FinalizarQuiz();
            yield break;
        }

        numeroDaQuestaoAtual++;
        int indexAleatorio = Random.Range(0, perguntasDisponiveis.Count);
        perguntaAtual = perguntasDisponiveis[indexAleatorio];
        perguntasDisponiveis.RemoveAt(indexAleatorio);
        MostrarPerguntaNaTela();
    }

    void FinalizarQuiz()
    {
        painelDoQuiz.SetActive(false);
        painelFimDeJogo.SetActive(true);
        textoPontuacaoFinalUI.text = "Sua pontuação final: " + pontuacao;

        if (pontuacao >= 20)
        {
            botaoRecompensaUI.interactable = true;
            if (AudioManager.instance != null) AudioManager.instance.TocarSomVitoria();
        }
        else
        {
            botaoRecompensaUI.interactable = false;
        }
    }

    void SetBotoesInteragiveis(bool estado)
    {
        foreach (Button botao in botoesRespostaUI)
        {
            botao.interactable = estado;
        }
    }

    void AtualizarTextosUI()
    {
        textoPontuacaoUI.text = "Pontos: " + pontuacao;
        textoNumeroQuestaoUI.text = "Questão: " + numeroDaQuestaoAtual + "/" + todasAsPergunras.Count;
    }

    public void JogarNovamente()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MostrarRecompensa()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();

        if (AudioManager.instance != null && textoDicaRecompensa != null)
        {
            string dica = AudioManager.instance.GetDicaAleatoria();
            textoDicaRecompensa.text = "Você Sabia?\n\n" + dica;
        }

        painelFimDeJogo.SetActive(false);
        painelRecompensa.SetActive(true);
    }

    public void FecharRecompensa()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomClique();
        painelRecompensa.SetActive(false);
        painelFimDeJogo.SetActive(true);
    }

    public void VoltarParaMenu()
    {
        if (AudioManager.instance != null) AudioManager.instance.TocarSomTransicao();
        SceneManager.LoadScene("MenuPrincipal");
    }
}