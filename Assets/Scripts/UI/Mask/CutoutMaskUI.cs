using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class CutoutMaskUI : Image
{
    int StencilComp = Shader.PropertyToID("_StencilComp");
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
