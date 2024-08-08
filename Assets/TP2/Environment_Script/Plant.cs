using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Material material;
    public Vector2 textureOffsetSpeed;
    public float textureOffsetThreshold;

    private void Update()
    {
        if (material != null)
        {
            Vector2 offset = material.GetTextureOffset("_BaseMap");
            offset += textureOffsetSpeed * Time.deltaTime * 0.5f;

            if (offset.x >= textureOffsetThreshold)
            {
                textureOffsetSpeed.x *= -1;
                offset.x = textureOffsetThreshold;
            }
            else if (offset.x <= -textureOffsetThreshold)
            {
                textureOffsetSpeed.x *= -1;
                offset.x = -textureOffsetThreshold;
            }

            material.SetTextureOffset("_BaseMap", offset);
        }
    }
}