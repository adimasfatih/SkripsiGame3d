using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    private CinemachineCamera cineCam;
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    private Coroutine shakeRoutine;
    private float currentShakeIntensity = 0f;

    public static CameraShakeController Instance { get; private set; }


    void Awake()
    {
        Instance = this;
        cineCam = GetComponent<CinemachineCamera>();

        if (cineCam != null)
        {
            perlinNoise = cineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        ResetIntensity();
    }

    public void ShakeCamera(float intensity, float shakeTime)
    {
        if (perlinNoise == null) return;

        if (intensity > currentShakeIntensity)
        {
            if (shakeRoutine != null)
                StopCoroutine(shakeRoutine);

            currentShakeIntensity = intensity;
            perlinNoise.AmplitudeGain = intensity;
            shakeRoutine = StartCoroutine(WaitAndReset(shakeTime));
        }
    }

    private IEnumerator WaitAndReset(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();
        shakeRoutine = null;
    }

    public void ResetIntensity()
    {
        if (perlinNoise != null)
        {
            currentShakeIntensity = 0f;
            perlinNoise.AmplitudeGain = 0f;
        }
    }
}
