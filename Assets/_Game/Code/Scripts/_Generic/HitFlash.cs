using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour {

    [SerializeField] Renderer[] MeshRendererList;
    [SerializeField] Material FlashMaterial;
    [SerializeField] private float _flashDuration = 0.1f;

    private Material[] _previousMaterial;
    private float _flashStartTime;
    private bool _isFlashing = false;

    private void Start() {
        _previousMaterial = new Material[MeshRendererList.Length];
        for (int i = 0; i < MeshRendererList.Length; i++) {
            //MeshRendererList[i].material.EnableKeyword("_EMISSION");
            _previousMaterial[i] = MeshRendererList[i].material;
        }
    }

    public void StartFlash() {
        _isFlashing = true;
        foreach (Renderer item in MeshRendererList) {
            item.material = FlashMaterial;
        }
        _flashStartTime = Time.time;
    }

    private void Update() {
        if (_isFlashing && _flashStartTime + _flashDuration < Time.time) {
            StopFlash();
        }
    }

    public void StopFlash() {
        _isFlashing = false;

        for (int i = 0; i < MeshRendererList.Length; i++) {
            MeshRendererList[i].material = _previousMaterial[i];
        }

    }
}
