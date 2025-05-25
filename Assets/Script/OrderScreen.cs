using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScreen : MonoBehaviour
{

    public GameObject[] BaseType;
    public GameObject[] SyrupType;
    public GameObject[] DecorType;

    private int BaseIndexRandom;
    private int SyrupIndexRandom;
    private int DecorIndexRandom;


    public void CreateRandomOrder()
    {
        BaseIndexRandom = Random.Range(0, BaseType.Length);
        SyrupIndexRandom = Random.Range(0, SyrupType.Length);
        DecorIndexRandom = Random.Range(0, DecorType.Length);

        Instantiate(BaseType[BaseIndexRandom], transform.position, Quaternion.identity);
        Instantiate(SyrupType[SyrupIndexRandom], transform.position, Quaternion.identity);
        Instantiate(DecorType[DecorIndexRandom], transform.position, Quaternion.identity);
    }

    public (int baseIndex, int syrupIndex, int decorIndex) CurrentOrderOnScreen()
    {
        int baseIndex = BaseIndexRandom;
        int syrupIndex = SyrupIndexRandom;
        int decorIndex = DecorIndexRandom;

        return (baseIndex, syrupIndex, decorIndex);
    }


}
