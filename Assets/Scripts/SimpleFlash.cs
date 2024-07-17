using System.Collections;

using UnityEngine;

namespace BarthaSzabolcs.Tutorial_SpriteFlash
{
    public class SimpleFlash : MonoBehaviour
    {

        [SerializeField] private Material flashMaterial;
        [SerializeField] private float duration;

        private SpriteRenderer spriteRenderer;
        private Material originalMaterial;
        private Coroutine flashRoutine;


        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalMaterial = spriteRenderer.material;
        }


        public void Flash()
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }

            flashRoutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            spriteRenderer.material = flashMaterial;

            yield return new WaitForSeconds(duration);

            spriteRenderer.material = originalMaterial;

            flashRoutine = null;
        }

    }
}