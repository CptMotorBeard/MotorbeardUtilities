using System.Collections;
using BeardKit;
using UnityEngine;

namespace BeardKit
{
    public class CoroutineWithData<T>
    {
        public Coroutine Coroutine { get; private set; }
        public T Result { get; private set; }

        private readonly IEnumerator m_target;

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
    public static CoroutineWithData<T> StartCoroutineWithData<T>(this MonoBehaviour owner, IEnumerator target)
    {
        return new CoroutineWithData<T>(owner, target);
    }
}