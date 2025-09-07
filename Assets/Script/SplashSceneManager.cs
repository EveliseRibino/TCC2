using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SplashSceneManager : MonoBehaviour
{
    // Define as configurações da transição de cena, ajustáveis no Inspector
    [Header("Configurações de Transição")]
    public float tempoDeDuracao = 20f;
    public string nomeDaCena = "MenuPrincipal";
    private bool transicaoIniciada = false;

    // Define as configurações da animação do título, ajustáveis no Inspector
    [Header("Configurações do Título Animado")]
    public GameObject tituloContainer;
    public float tempoDeAnimacaoLetras = 10.0f;
    public float delayEntreLetras = 2.0f;
    public float distanciaInicialLetras = 2500f;

    // Função Start é chamada uma vez quando a cena carrega
    void Start()
    {
        // Agenda a transição automática de cena usando um temporizador
        Invoke(nameof(IniciarTransicao), tempoDeDuracao);

        // Inicia a animação das letras se o container estiver conectado
        if (tituloContainer != null)
        {
            StartCoroutine(AnimarLetras());
        }
    }

    // Função Update é chamada a cada frame
    void Update()
    {
        // Ouve por um clique do mouse ou toque na tela para pular a introdução
        if (Input.GetMouseButtonDown(0))
        {
            IniciarTransicao();
        }
    }

    // Controla a lógica de transição para a próxima cena
    public void IniciarTransicao()
    {
        // Trava para garantir que a transição ocorra apenas uma vez
        if (transicaoIniciada) return;
        transicaoIniciada = true;

        // Cancela o temporizador agendado, caso o usuário tenha pulado
        CancelInvoke(nameof(IniciarTransicao));

        // Toca um som de transição através do AudioManager
        if (AudioManager.instance != null)
        {
            AudioManager.instance.TocarSomTransicao();
        }

        // Carrega a próxima cena
        SceneManager.LoadScene(nomeDaCena);
    }

    // Corrotina que controla a animação das letras
    private IEnumerator AnimarLetras()
    {
        if (tituloContainer == null) yield break;

        GridLayoutGroup layoutGroup = tituloContainer.GetComponent<GridLayoutGroup>();
        List<RectTransform> letras = new List<RectTransform>();
        List<Vector3> posicoesFinais = new List<Vector3>();

        // Força o cálculo do layout para "anotar" as posições corretas
        if (layoutGroup != null)
        {
            layoutGroup.enabled = true;
            Canvas.ForceUpdateCanvases();
        }
        yield return null;

        // Armazena a referência de cada letra e sua posição final
        foreach (Transform child in tituloContainer.transform)
        {
            RectTransform letraRect = child.GetComponent<RectTransform>();
            if (letraRect != null)
            {
                letras.Add(letraRect);
                posicoesFinais.Add(letraRect.localPosition);
            }
        }

        // Desliga o layout automático para permitir a animação manual
        if (layoutGroup != null)
        {
            layoutGroup.enabled = false;
        }

        // Move cada letra para sua posição inicial (fora da tela)
        for (int i = 0; i < letras.Count; i++)
        {
            float posXFinal = posicoesFinais[i].x;
            float posYInicial = -distanciaInicialLetras;
            float variacaoXAleatoria = Random.Range(-50f, 50f);
            letras[i].localPosition = new Vector3(posXFinal + variacaoXAleatoria, posYInicial, 0);
        }

        // Anima cada letra de volta para sua posição final com um atraso
        for (int i = 0; i < letras.Count; i++)
        {
            LeanTween.moveLocal(letras[i].gameObject, posicoesFinais[i], tempoDeAnimacaoLetras)
                     .setEase(LeanTweenType.easeOutQuint);
            yield return new WaitForSeconds(delayEntreLetras);
        }
    }
}