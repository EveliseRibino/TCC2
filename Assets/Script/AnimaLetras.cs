using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Precisamos de Listas
using UnityEngine.UI; // Precisamos disso para acessar o Layout Group

public class AnimaLetras : MonoBehaviour
{
    public float tempoDeAnimacao = 1.5f;
    public float delayEntreLetras = 0.1f;
    public float distanciaInicial = 500f;

    void Start()
    {
        StartCoroutine(AnimarComLayout());
    }

    IEnumerator AnimarComLayout()
    {
        // Pega o componente Grid Layout Group do objeto atual
        GridLayoutGroup layoutGroup = GetComponent<GridLayoutGroup>();

        // Uma lista para guardar apenas as letras (excluindo o pai)
        List<RectTransform> letras = new List<RectTransform>();

        // Uma lista para guardar as posições finais
        List<Vector3> posicoesFinais = new List<Vector3>();

        // Força o Layout Group a calcular tudo imediatamente
        if (layoutGroup != null)
        {
            layoutGroup.enabled = true; // Garante que está ligado
            Canvas.ForceUpdateCanvases(); // Força a UI a se redesenhar
        }

        // Espera um frame para garantir que o cálculo foi feito
        yield return null;

        // Passo 1: "Anota" as posições finais de cada letra
        foreach (Transform child in transform)
        {
            RectTransform letraRect = child.GetComponent<RectTransform>();
            if (letraRect != null)
            {
                letras.Add(letraRect);
                posicoesFinais.Add(letraRect.localPosition);
            }
        }

        // Passo 2: Desliga o Layout Group para termos controle total
        if (layoutGroup != null)
        {
            layoutGroup.enabled = false;
        }

        // Passo 3: Joga as letras para posições ABAIXO da tela
        for (int i = 0; i < letras.Count; i++)
        {
            // A posição X final da letra (para não ficarem todas empilhadas)
            float posXFinal = posicoesFinais[i].x;

            // A posição Y inicial, bem abaixo da tela
            float posYInicial = -distanciaInicial; // Usamos um valor negativo fixo

            // Adiciona uma pequena variação aleatória para não subirem em linha reta perfeita
            float variacaoXAleatoria = Random.Range(-50f, 50f);

            letras[i].localPosition = new Vector3(posXFinal + variacaoXAleatoria, posYInicial, 0);
        }

        for (int i = 0; i < letras.Count; i++)
        {
            LeanTween.moveLocal(letras[i].gameObject, posicoesFinais[i], tempoDeAnimacao)
                     .setEase(LeanTweenType.easeOutQuint);

            // Espera um pouco antes de animar a próxima letra
            yield return new WaitForSeconds(delayEntreLetras);
        }
    }
}