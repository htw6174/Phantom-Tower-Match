using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFX : MonoBehaviour {

    public bool displayCameraEffects = false;

    public Material effectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (displayCameraEffects)
        {
            Graphics.Blit(src, dst, effectMaterial);
        }
    }
}
