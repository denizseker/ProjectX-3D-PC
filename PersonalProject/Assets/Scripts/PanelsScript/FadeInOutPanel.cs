using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutPanel : MonoBehaviour
{
    //private Camera mainCamera;            // Kameran�n referans�
    //private CanvasGroup panelCanvasGroup; // Panelin CanvasGroup bile�eni
    //public float targetDistance = 200f;   // Hedef mesafe
    //public float transitionTime = 2f;     // Ge�i� s�resi
    //private float currentAlpha = 1f;      // �u anki alfa de�eri
    //private float targetAlpha = 1f;       // Hedef alfa de�eri

    //private void Start()
    //{
    //    mainCamera = Camera.main;  // Ana kameray� al
    //    panelCanvasGroup = GetComponent<CanvasGroup>(); // Panelin CanvasGroup bile�enini al
    //}

    //private void Update()
    //{

    //    float currentDistance = Vector3.Distance(mainCamera.transform.position, transform.position);

    //    if (currentDistance >= targetDistance)
    //    {
    //        targetAlpha = 0f; // Paneli kapatmak i�in hedef alfa de�erini 0 yap
    //    }
    //    else
    //    {
    //        targetAlpha = 1f; // Paneli a�mak i�in hedef alfa de�erini 1 yap
    //    }

    //    // Belirli bir ad�m b�y�kl���yle alfa de�erini hedefe yakla�t�r
    //    float step = Time.deltaTime / transitionTime; // Ge�i� ad�m b�y�kl���
    //    currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, step);
    //    panelCanvasGroup.alpha = currentAlpha;
    //}

    private CanvasGroup panelCanvasGroup;
    public float fadeDuration = 0.5f;


    private void Awake()
    {
        panelCanvasGroup = GetComponentInParent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StartFadeIn();
    }

    // Bu fonksiyon, paneli belirli bir s�re i�inde fade-out yapar.
    public void StartFadeOut()
    {
        StartCoroutine(FadeTo(0,1, fadeDuration));
    }

    // Bu fonksiyon, paneli belirli bir s�re i�inde fade-in yapar.
    public void StartFadeIn()
    {
        StartCoroutine(FadeTo(1,0, fadeDuration));
    }

    private IEnumerator FadeTo(float targetAlpha,float startAlpha, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        // Alpha de�erini kesinlikle hedefe ayarlay�n.
        panelCanvasGroup.alpha = targetAlpha;
    }

}
