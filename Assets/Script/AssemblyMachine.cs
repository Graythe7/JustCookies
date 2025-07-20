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
            StartCoroutine(DeactivateAfterDelay("isActivated",false));
        }
    }

    public void CantSpawnAnimation(bool wrongAction)
    {
        assemblyMachine.SetBool("wrongAction", wrongAction); 

        if (wrongAction) 
        {
            StartCoroutine(DeactivateAfterDelay("wrongAction",false));
        }
    }

    private IEnumerator DeactivateAfterDelay(string parametersName,bool defaultState)
    {
        yield return new WaitForSeconds(0.1f); // adjust to animation length
        assemblyMachine.SetBool(parametersName, defaultState);
    }

}
