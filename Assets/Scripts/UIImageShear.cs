using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIImageShear : Image {

    float a = 1;
    float b = 2;
    float c = 1;
    float d = 2;

    protected override void OnPopulateMesh(Mesh m)
    {
        List<UIVertex> verts = new List<UIVertex>();

        float wHalf = rectTransform.rect.width / 2;
        float hHalf = rectTransform.rect.height / 2;
        a = Mathf.Min(1, Mathf.Max(0, a));
        b = Mathf.Min(1, Mathf.Max(0, b));
        c = Mathf.Min(1, Mathf.Max(0, c));
        d = Mathf.Min(1, Mathf.Max(0, d));

        Color32 color32 = color;
        using (var vh = new VertexHelper())
        {
            vh.AddVert(new Vector3(-wHalf * a, 0), color32, new Vector2(0f, 0f));
            vh.AddVert(new Vector3(0, wHalf * b), color32, new Vector2(0f, 1f));
            vh.AddVert(new Vector3(wHalf * c, 0), color32, new Vector2(1f, 1f));
            vh.AddVert(new Vector3(0, -wHalf * d), color32, new Vector2(1f, 0f));

            vh.AddTriangle(0, 1, 2);
            //vh.AddTriangle(2, 3, 0);
            vh.FillMesh(m);
        }
    }
}
