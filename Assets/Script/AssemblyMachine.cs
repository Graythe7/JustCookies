using UnityEngine;
using System.Collections;


public class AssemblyMachine : MonoBehaviour
{
    public Animator assemblyMachine;


    public void ActivateAnimation(bool isActivated)
    {
        assemblyMachine.SetBool("isActivated", isActivated);

        if (isActivated)
        {
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // adjust to animation length
        assemblyMachine.SetBool("isActivated", false);
    }

}
