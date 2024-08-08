using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Water : MonoBehaviour
{
    Material material;
    Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();

        material = renderer.material;
    }

    void Update()
    {
        float currentOffset = material.GetTextureOffset("_BaseMap").y;
        currentOffset += 0.05f * Time.deltaTime;
        material.SetTextureOffset("_BaseMap", new Vector2(0, currentOffset));
    }
}
