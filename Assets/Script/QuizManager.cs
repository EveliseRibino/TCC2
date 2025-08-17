using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    // DADOS E REFERÊNCIAS ---
    public List<QuestaoQuiz> todasAsPergunras;
    private List<QuestaoQuiz> perguntasDisponiveis;
    private QuestaoQuiz perguntaAtual;

    // VARIÁVEIS DE ESTADO DO JOGO ---
    private int pontuacao = 0;
    private int numeroDaQuestaoAtual = 0;
    private bool respostaFoiDada = false;

    // CONEXÕES COM A UI ---
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

    // MÉTODOS DO JOGO ---
    void Awake()
    {
        Debug.Log("--- AWAKE EXECUTADO --- Escondendo painel final, mostrando painel do quiz.");
        if (painelFimDeJogo != null) painelFimDeJogo.SetActive(false);
        if (painelRecompensa != null) painelRecompensa.SetActive(false);
        if (painelDoQuiz != null) painelDoQuiz.SetActive(true);
    }
    void Start()
    {
        Debug.Log("--- START EXECUTADO --- Chamando ComecarJogo().");
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
            Image imagemDaSuculenta = imagensNoBotao[1];
            imagemDaSuculenta.sprite = perguntaAtual.respostasImagens[i];
            imagemDaSuculenta.color = Color.white;
        }

        AtualizarTextosUI();
    }

    public void OnRespostaSelecionada(int indiceDoBotao)
    {
        if (respostaFoiDada)
        {
            return;
        }

        respostaFoiDada = true;
        SetBotoesInteragiveis(false);

        if (indiceDoBotao == perguntaAtual.indiceRespostaCorreta)
        {
            Debug.Log("RESPOSTA CORRETA!");
            pontuacao += 10;
            botoesRespostaUI[indiceDoBotao].GetComponentsInChildren<Image>()[1].color = Color.green;
        }
        else
        {
            Debug.Log("Resposta Errada.");
            botoesRespostaUI[indiceDoBotao].GetComponentsInChildren<Image>()[1].color = Color.red;
            botoesRespostaUI[perguntaAtual.indiceRespostaCorreta].GetComponentsInChildren<Image>()[1].color = Color.green;
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
        Debug.Log("--- FINALIZAR QUIZ EXECUTADO --- Mostrando painel final.");
        painelDoQuiz.SetActive(false);
        painelFimDeJogo.SetActive(true);

        textoPontuacaoFinalUI.text = "Sua pontuação final: " + pontuacao;

        if (pontuacao >= 20)
        {
            botaoRecompensaUI.interactable = true;
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

    // FUNÇÕES PARA OS BOTÕES DOS NOVOS PAINÉIS ---

    public void JogarNovamente()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MostrarRecompensa()
    {
        painelFimDeJogo.SetActive(false);
        painelRecompensa.SetActive(true);
    }

    public void FecharRecompensa()
    {
        painelRecompensa.SetActive(false);
        painelFimDeJogo.SetActive(true);
    }

    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}