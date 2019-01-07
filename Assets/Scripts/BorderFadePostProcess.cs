using UnityEngine;

public class BorderFadePostProcess : MonoBehaviour
{
    [Range(0, 500)]
    public float borderWidth = 100;
    private Material material;

    void Awake()
    {
        // Creat a Material using the FadeBorder shader
        material = new Material(Shader.Find("Hidden/FadeBorder"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_borderWidth", borderWidth);
        
        // Rerender the scene on a screen aligned quad with the given material/shader
        Graphics.Blit(source, destination, material);
    }
}
