using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 5, 0); // How far up to move
    public float moveDuration = 2f;               // Time to move up/down
    public float pauseDuration = 2f;              // Time to pause at top/bottom
    public float minStartDelay = 0f;              // Minimum delay before starting
    public float maxStartDelay = 3f;              // Maximum delay before starting

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + offset;
        float startDelay = Random.Range(minStartDelay, maxStartDelay);
        StartCoroutine(DelayedStart(startDelay));
    }

    IEnumerator DelayedStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(MoveCycle());
    }

    IEnumerator MoveCycle()
    {
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, startPos, targetPos, moveDuration));
            yield return new WaitForSeconds(pauseDuration);
            yield return StartCoroutine(MoveObject(transform, targetPos, startPos, moveDuration));
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    IEnumerator MoveObject(Transform obj, Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            obj.position = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.position = to;
    }
}


