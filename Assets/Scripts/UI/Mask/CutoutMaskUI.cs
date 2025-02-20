using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections;
public class CutoutMaskUI : Image
{
    int StencilComp = Shader.PropertyToID("_StencilComp");

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Fix());
    }

    /// Fix for async loading scenes
    private IEnumerator Fix()
    {
        yield return null;
        maskable = false;
        maskable = true;
    }

    public override Material materialForRendering
    {
        get
        {
            Material material = new Material(base.materialForRendering);
            material.SetInt(StencilComp, (int)CompareFunction.NotEqual);
            return material;
        }
    }
}
