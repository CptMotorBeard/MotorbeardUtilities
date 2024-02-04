using System.Collections;
using UnityEngine;

namespace BeardKit
{
    public class CoroutineWithData<T>
    {
        public Coroutine Coroutine { get; private set; }
        public T Result { get; private set; }

        private IEnumerator m_target;

        public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
        {
            m_target = target;
            Coroutine = owner.StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            while (m_target.MoveNext())
            {
                Result = (T)m_target.Current;
                yield return Result;
            }
        }
    }
}

public static class MonoBehaviourCoroutineWithDataExtensions
{
    public static BeardKit.CoroutineWithData<T> StartCoroutineWithData<T>(this MonoBehaviour owner, IEnumerator target)
    {
        return new BeardKit.CoroutineWithData<T>(owner, target);
    }
}