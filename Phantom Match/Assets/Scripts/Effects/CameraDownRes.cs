using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraDownRes : MonoBehaviour {

    public bool effectsEnabled = false;

    public Material material;

    [SerializeField]
    [Range(0, 15)]
    private int _bitsShifted;

    void Start()
    {
        Debug.Log(1920 >> _bitsShifted);
        Debug.Log(1080 >> _bitsShifted);
    }

    void Update()
    {
        Shader.SetGlobalFloat("_Iters", _bitsShifted);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, material);
    }

    public void UpdateBitsShifted(float newShift)
    {
        _bitsShifted = (int)newShift;
    }

    //void OnRenderImage(RenderTexture src, RenderTexture dst)
    //{

    //    if (effectsEnabled && material != null)
    //    {
    //        int width = src.width >> _blurIterations;
    //        int height = src.height >> _blurIterations;

    //        RenderTexture downSampledRT = RenderTexture.GetTemporary(width, height); //, 0, src.format, RenderTextureReadWrite.Default, 1);
    //        Graphics.Blit(src, downSampledRT);

    //        //for (int i = 0; i < _blurIterations; i++)
    //        //{
    //        //    RenderTexture rt2 = RenderTexture.GetTemporary(width, height); //, 0, src.format, RenderTextureReadWrite.Default, 1);
    //        //    Graphics.Blit(rt, rt2, material);
    //        //    RenderTexture.ReleaseTemporary(rt);
    //        //    rt = rt2;
    //        //}
            
    //        Graphics.Blit(downSampledRT, dst, material);
    //        RenderTexture.ReleaseTemporary(downSampledRT);
    //    }
    //    else
    //    {
    //        Graphics.Blit(src, dst);
    //    }
    //}
}
