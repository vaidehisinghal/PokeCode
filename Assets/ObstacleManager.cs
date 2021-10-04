using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] pokeball;
    public GameObject[] bomb;
    int pbSize;
    int bombs;
    bool isActiveP = false;
    bool isActiveB = false;

    private void Start()
    {
        pbSize = 10;
        bombs = 5;
        foreach( var p in pokeball)
        {
            p.SetActive(false);
        }
        foreach( var b in bomb)
        {
            b.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //pokeball
        if (!isActiveP)
        {
            int n = Random.Range(0, pbSize);
            pokeball[n].SetActive(true);
            isActiveP = true;
            StartCoroutine(destroyPokeball(pokeball[n]));
            //destroy pokeball
        }


        if(!isActiveB)
        {
            //bomb
            int n = Random.Range(0, bombs);
            bomb[n].SetActive(true);
            isActiveB = true;
            StartCoroutine(destroyBomb(bomb[n]));
        }
        
        
    }

    IEnumerator destroyPokeball(GameObject p)
    {
        yield return new WaitForSeconds(10);
        p.SetActive(false);
        isActiveP = false;
    }

    IEnumerator destroyBomb(GameObject b)
    {
        yield return new WaitForSeconds(5);
        b.SetActive(false);
        isActiveB = false;
    }
}
