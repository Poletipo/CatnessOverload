using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RandomTextureDecal : MonoBehaviour {
    
    public Material[] textures;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<DecalProjector>().material;


        int index = Random.Range(0, textures.Length);
        Debug.Log(index);

        mat = textures[index];

    }

    // Update is called once per frame
    void Update()
    {

    }
}
