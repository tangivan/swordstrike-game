using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCounter : MonoBehaviour
{
    [SerializeField] private Weapon weapon1;
    private CanvasRenderer canvasRenderer;
    private TextMeshProUGUI textMeshPro;
    private int hitCount;
    private Vector3 baseLocalPosition;
    private float shakeIntensity;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        canvasRenderer = GetComponent<CanvasRenderer>();
        baseLocalPosition = transform.localPosition;
        HideHitCounter();
    }

    private void Start()
    {
        hitCount = 0;
    }

    private void Update()
    {
        if (shakeIntensity > 0)
        {
            float intensityDropAmount = .5f;
            shakeIntensity -= intensityDropAmount * Time.deltaTime;
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            transform.localPosition = baseLocalPosition + randomDirection * shakeIntensity;
        }
    }

    public void onKill()
    {
        hitCount++;
        SetHitCounter(hitCount);

        float baseIntensity = .2f;
        float perHitIntensity = 0.02f;
        shakeIntensity = baseIntensity + perHitIntensity * hitCount;
    }

    public void SetHitCounter(int hitCount)
    {
        textMeshPro.SetText(hitCount.ToString());
        Color32 color = new Color32(255, 255, 255, 255);
        canvasRenderer.SetAlpha(1f);

        if (hitCount >= 10) color = new Color32(0, 160, 255, 255);
        if (hitCount >= 20) color = new Color32(36, 225, 0, 255);
        if (hitCount >= 30) color = new Color32(255, 227, 0, 255);
        if (hitCount >= 40) color = new Color32(255, 127, 28, 255);
        if (hitCount >= 50) color = new Color32(255, 58, 242, 255);

        textMeshPro.color = color;

        float startingFontSize = 100f;
        float perHitFontSize = 1f;
        textMeshPro.fontSize = Mathf.Clamp(startingFontSize + perHitFontSize * hitCount, startingFontSize, startingFontSize * 1.5f);


    }

    public void onMiss()
    {
        hitCount = 0;
        SetHitCounter(hitCount);
        HideHitCounter();
    }

    private void HideHitCounter()
    {
        canvasRenderer.SetAlpha(0f);
    }
}
