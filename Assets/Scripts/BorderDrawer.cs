using UnityEngine;

public class BorderDrawer : MonoBehaviour
{
    [Range(0, 500)]
    public float borderWidth = 100;
    private Material material;

    private Color OutsideColor = new Color(0, 0, 0, 1);
    private Color InsideColor = new Color(0, 0, 0, 0);

    private void Start()
    {
        // Creat a Material using the FadeBorder shader
        material = new Material(Shader.Find("Unlit/VertexColor"));
    }

    void OnPostRender()
    {
        // HoloLens resolution 1280 x 720 
        // HoloLens screen/safe area 1268 x 720

        float left = (float)borderWidth / (float)Screen.width;
        float bottom = (float)borderWidth / (float)Screen.height;
        float right = 1 - left;
        float top = 1 - bottom;

        GL.PushMatrix();
        material.SetPass(0);

        GL.LoadOrtho();
        GL.Begin(GL.TRIANGLE_STRIP);

        GL.Color(OutsideColor);
        GL.Vertex3(0.0F, 0.0F, 0);
        GL.Color(InsideColor);
        GL.Vertex3(left, bottom, 0);

        GL.Color(OutsideColor);
        GL.Vertex3(1F, 0F, 0);
        GL.Color(InsideColor);
        GL.Vertex3(right, bottom, 0);

        GL.Color(OutsideColor);
        GL.Vertex3(1F, 1F, 0);
        GL.Color(InsideColor);
        GL.Vertex3(right, top, 0);

        GL.Color(OutsideColor);
        GL.Vertex3(0F, 1F, 0);
        GL.Color(InsideColor);
        GL.Vertex3(left, top, 0);

        GL.Color(OutsideColor);
        GL.Vertex3(0F, 0F, 0);
        GL.Color(InsideColor);
        GL.Vertex3(left, bottom, 0);

        GL.End();
        GL.PopMatrix();
    }
}
