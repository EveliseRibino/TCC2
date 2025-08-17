using UnityEngine;

[CreateAssetMenu(fileName = "Nova Pergunta Quiz", menuName = "Quiz/Criar Pergunta com Imagem")]

public class QuestaoQuiz : ScriptableObject
{
    [TextArea(2, 5)]
    public string pergunta;
    public Sprite[] respostasImagens = new Sprite[4];
    public int indiceRespostaCorreta;
}
