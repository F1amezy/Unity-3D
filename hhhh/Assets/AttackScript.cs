using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript: MonoBehaviour
{

    public GameObject Sword;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        Sword.GetComponent<Animator>().Play("Swing");
        yield return new WaitForSeconds(1.0f);
        Sword.GetComponent<Animator>().Play("New State");
    }
}