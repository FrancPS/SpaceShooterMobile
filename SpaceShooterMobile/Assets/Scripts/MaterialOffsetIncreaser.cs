using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetIncreaser : MonoBehaviour
{
    // Scroll main texture based on time
    public float scrollSpeed = 0.5f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Mathf.Repeat(Time.time * scrollSpeed, 1);
        rend.material.SetTextureOffset("_MainTex", new Vector2(0f, offset));
    }
}
