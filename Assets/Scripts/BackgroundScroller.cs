using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // config variables
    [SerializeField] float backgroundScrollSpeed = 0.5f;

    // utility variables
    Vector2 offset;

    // cached references
    Material backgroundMaterial;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
