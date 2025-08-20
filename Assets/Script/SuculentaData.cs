using UnityEngine;

[CreateAssetMenu(fileName = "Nova Suculenta", menuName = "Suculenta/Criar Nova Suculenta")]
public class SuculentaData : ScriptableObject
{
    public string nome;
    public Sprite foto;

    [TextArea(3, 10)]
    public string descricao;

    [TextArea(3, 10)]
    public string iluminacao;

    [TextArea(3, 10)]
    public string rega;

    [TextArea(3, 10)]
    public string substrato;

    [TextArea(2, 5)]
    public string dicaCuriosa; // <-- ESTA É A LINHA IMPORTANTE
}