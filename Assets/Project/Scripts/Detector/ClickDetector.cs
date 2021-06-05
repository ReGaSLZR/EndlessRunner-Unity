namespace ReGaSLZR.EndlessRunner.Detector
{

    using System.Collections;
    using UniRx;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ClickDetector : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField]
        private float delayResetClickStatus = 0.5f;

        private ReactiveProperty<bool> isClicked
            = new ReactiveProperty<bool>(false);

        public IReadOnlyReactiveProperty<bool> IsClicked
        {
            get 
            {
                return isClicked;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StopAllCoroutines();
            StartCoroutine(CorResetClickStatus());
        }

        private IEnumerator CorResetClickStatus()
        {
            isClicked.Value = true;
            yield return new WaitForSeconds(delayResetClickStatus);
            isClicked.Value = false;
        }

    }

}