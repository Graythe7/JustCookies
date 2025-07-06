using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefMovement : MonoBehaviour
{
    public SpriteRenderer chefGraySprite;
    public Transform chefPos;
    private Vector3 startPos;
    private Vector3 endPos;
    private float duration = 0.5f;

    private void Start()
    {
        startPos = chefPos.transform.position;
        endPos = startPos + new Vector3(-4.5f, 0, 0);
    }

    public void startTransition(bool isGameComplete)
    {
        if (isGameComplete)
        {
            StartCoroutine(TransitionToEndPos());
        }
        else
        {
            Debug.Log("Can't transition Chef Gray since the game is not complete");
        }
    }

    private IEnumerator TransitionToEndPos()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            chefPos.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        chefPos.position = endPos;
    }
}
