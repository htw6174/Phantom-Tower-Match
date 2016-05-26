using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFX : MonoBehaviour {

    public bool effectsEnabled = false;

    public Material material;

    [SerializeField]
    [Range(0f, 0.1f)]
    private float _blurMagnitude;

    void Update()
    {
        Shader.SetGlobalFloat("_GlobalMagnitude", _blurMagnitude);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (effectsEnabled && material != null && _blurMagnitude != 0)
        {
            Graphics.Blit(src, dst, material);
        }
        else
        {
            Graphics.Blit(src, dst);
        }
    }

    public void UpdateBlurMagnitude(float newMagnitude)
    {
        _blurMagnitude = newMagnitude;
    }

    public void ToggleEnabled()
    {
        effectsEnabled = !effectsEnabled;
    }
}
