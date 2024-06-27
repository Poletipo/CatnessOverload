using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    private float _trauma = 0;
    public float Trauma {
        get { return _trauma; }
        set {
            value = Mathf.Clamp01(value);
            _trauma = value;
        }
    }
    public float MaxShakeIntensity = 1f;
    public int TraumaPower = 2;

    private Vector3 _shakeOffset;
    private Vector3 _rotationOffset;
    private float _shakeIntensity = 0;


    CinemachineBrain _cinemachineBrain;
    CinemachineVirtualCamera _cineCam;

    // Start is called before the first frame update
    void Start()
    {
        _shakeOffset = Vector3.zero;
        _cinemachineBrain = GetComponent<CinemachineBrain>();
        SetupCamNoise();
    }

    // Update is called once per frame
    void Update()
    {
        if (_cineCam == null) {
            SetupCamNoise();
            return;
        }

        if (Trauma <= 0) {
            return;
        }

        if (Trauma > 0) {
            Trauma -= Time.deltaTime;
        }

        _shakeIntensity = Mathf.Pow(Trauma, TraumaPower);

        _cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeIntensity * MaxShakeIntensity;
    }

    void SetupCamNoise()
    {
        _cineCam = (CinemachineVirtualCamera)_cinemachineBrain.ActiveVirtualCamera;
        if (_cineCam == null) {
            return;
        }
        _cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }


    //public Vector3 GetPositionOffset() {
    //    float timer = Time.time * ShakeFrequency;


    //    _shakeIntensity = Mathf.Pow(Trauma, TraumaPower);

    //    _shakeOffset.x = GetPerlinNoise(151, timer) * _shakeIntensity * MaxShakeIntensity * PositionMultiplier.x;
    //    _shakeOffset.y = GetPerlinNoise(954, timer) * _shakeIntensity * MaxShakeIntensity * PositionMultiplier.y;
    //    _shakeOffset.z = GetPerlinNoise(598, timer) * _shakeIntensity * MaxShakeIntensity * PositionMultiplier.z;

    //    return _shakeOffset;
    //}

    //public Vector3 GetRotationOffset() {
    //    float timer = Time.time * ShakeFrequency;
    //    _rotationOffset.x = GetPerlinNoise(151, timer) * _shakeIntensity * MaxShakeAngle;
    //    _rotationOffset.y = GetPerlinNoise(954, timer) * _shakeIntensity * MaxShakeAngle;
    //    _rotationOffset.z = GetPerlinNoise(598, timer) * _shakeIntensity * MaxShakeAngle;

    //    return _rotationOffset;
    //}

    //private float GetPerlinNoise(int seed, float timer) {
    //    return (-0.5f + Mathf.PerlinNoise(timer + seed, timer + seed));
    //}



    public void AddTrauma(float traumaValue)
    {
        Trauma += traumaValue;
    }

    public void SetTrauma(float traumaValue)
    {
        Trauma = traumaValue;
    }

    public void SetHigherTrauma(float traumaValue)
    {
        if (Trauma < traumaValue) {
            Trauma = traumaValue;
        }
    }

}
