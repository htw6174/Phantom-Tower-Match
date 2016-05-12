using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFX : MonoBehaviour {

    public bool effectsEnabled = false;

    public Material material;

    public CameraFX secondaryFX;

    //public bool waveEffect = false;
    //public bool vaporEffect = false;

    //public Material waveMaterial;
    //public Material vaporMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (effectsEnabled && material != null)
        {
            Graphics.Blit(src, dst, material);
        }
        else
        {
            Graphics.Blit(src, dst);
        }

        //if (waveEffect && vaporEffect)
        //{
        //    RenderTexture temp = new RenderTexture(src.width, src.height, src.depth, src.format);
        //    Graphics.Blit(src, temp, waveMaterial);
        //    Graphics.Blit(temp, dst, vaporMaterial);
        //}
        //else if (waveEffect)
        //{
        //    Graphics.Blit(src, dst, waveMaterial);
        //}
        //else if (vaporEffect)
        //{
        //    Graphics.Blit(src, dst, vaporMaterial);
        //}
        //else
        //{
        //    Graphics.Blit(src, dst);
        //}
    }

    public void ToggleEnabled()
    {
        effectsEnabled = !effectsEnabled;
        secondaryFX.effectsEnabled = effectsEnabled;
    }
}
