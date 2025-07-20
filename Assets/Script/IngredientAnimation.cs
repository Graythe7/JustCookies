using UnityEngine;
using System.Collections;

public class IngredientAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] ingredientType;
    public Animator animator;


    public void ActivateAnimation(int ingredientIndex, bool isActive)
    {
        spriteRenderer.sprite = ingredientType[ingredientIndex];

        animator.SetBool("isActive", true);

        if (isActive)
        {
            StartCoroutine(DeactivateAfterDelay("isActive", false));
        }
    }

    private IEnumerator DeactivateAfterDelay(string parametersName, bool defaultState)
    {
        yield return new WaitForSeconds(0.8f); // adjust to animation length
        animator.SetBool(parametersName, defaultState);
    }

}
