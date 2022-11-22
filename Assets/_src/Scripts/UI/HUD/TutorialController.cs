using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PedroAurelio.HermitCrab
{
    public class TutorialController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private List<GameObject> hudElements;
        [SerializeField] private CanvasGroup panels;

        [Header("Settings")]
        [SerializeField] private int blinkCount = 3;
        [SerializeField] private float blinkDuration = 0.75f;

        private WaitForSeconds _waitForBlink;

        private void Awake()
        {
            _waitForBlink = new WaitForSeconds(blinkDuration);

            SetHudElements(false);
            StartCoroutine(Blink());
        }

        private void SetHudElements(bool value)
        {
            foreach (GameObject element in hudElements)
                element.SetActive(value);
        }

        private IEnumerator Blink()
        {
            for (int i = 0; i < blinkCount; i++)
            {
                panels.DOFade(0f, blinkDuration);
                yield return _waitForBlink;
                panels.DOFade(1f, blinkDuration);
                yield return _waitForBlink;
            }

            SetHudElements(true);
            gameObject.SetActive(false);
        }
    }
}
