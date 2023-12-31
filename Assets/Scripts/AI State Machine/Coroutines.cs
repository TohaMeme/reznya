using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Coroutines 
{
    class CoroutinesRunner:MonoBehaviour
    {

    }

    static CoroutinesRunner coroutinesRunner;
    public static void StartCoroutine(IEnumerator coroutine)
    {
        if(coroutinesRunner == null)
        {
            coroutinesRunner = new GameObject("coroutines_runner").AddComponent<CoroutinesRunner>();
        }
        coroutinesRunner.StartCoroutine(coroutine);
    }

    public static void StopCoroutine(IEnumerator coroutine)
    {
        if (coroutinesRunner == null)
            return;

        coroutinesRunner.StopCoroutine(coroutine);
    }
}
