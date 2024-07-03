using System;
using System.Collections;
using UnityEngine;

namespace Helpers.Tasks.Impl
{
    public class CoroutineDispatcher : MonoBehaviour, ICoroutineDispatcher
    {
        public void RunCoroutine(IEnumerator enumerator, Action onComplete)
        {
            StartCoroutine(CreateCoroutine(enumerator, onComplete));
        }

        public void RunCoroutine(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }

        public void Delay(float time, Action onComplete)
        {
            var delay = Delay(time);

            StartCoroutine(CreateCoroutine(delay, onComplete));
        }

        IEnumerator CreateCoroutine(IEnumerator coroutine, Action action)
        {
            yield return coroutine;

            action();
        }

        private IEnumerator Delay(float time)
        {
            var wait = time;
            
            while (wait > 0f)
            {
                wait -= Time.deltaTime;

                yield return null;
            }
        }
    }
}