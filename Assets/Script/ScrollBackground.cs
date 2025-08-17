using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    public float velocidadeX = 0.05f;
    public float velocidadeY = 0.05f;

    private RawImage imagemFundo;

    void Start()
    {
        imagemFundo = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect uvRect = imagemFundo.uvRect;
        uvRect.x += velocidadeX * Time.deltaTime;
        uvRect.y += velocidadeY * Time.deltaTime;
        imagemFundo.uvRect = uvRect;

    }
}
