using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTexture : MonoBehaviour {
    
    public Texture[] textures;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;

        int index = Random.Range(0, textures.Length);

        mat.SetTexture("_BaseMap", textures[index]);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
