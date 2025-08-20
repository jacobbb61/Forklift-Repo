using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Vector2 scrollSpeed;

    void Update()
    {
        meshRenderer.material.mainTextureOffset += scrollSpeed * Time.deltaTime;
    }
}
