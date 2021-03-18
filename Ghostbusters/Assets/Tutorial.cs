using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    //introduce the peekaboo ghost
    //pause and button press
    //introduce how to scare it
    //spawn in ghost
    //pause and press
    //teach about the medium and big ghosts
    //once they split

    //list of strings
    //some ui element

    PeekabooGhost peekaboo;
    int inputIndexChecker = 0;

    private void Start()
    {
        peekaboo = FindObjectOfType<PeekabooGhost>();
        peekaboo.gameObject.SetActive(false);
        StartCoroutine(SpawnPeekabooDelay());
    }

    private void Update()
    {
        if (Gamepad.current.buttonSouth.isPressed)
        {
            inputIndexChecker++;

        }
    }

    public IEnumerator SpawnPeekabooDelay()
    {
        yield return new WaitForSeconds(3);
        peekaboo.gameObject.SetActive(true);
        print("test");
        //show text talking about the peekaboo ghost
        
        while(inputIndexChecker != 1)
        {
            Time.timeScale = 0;
        }

        //yield return StartCoroutine(//new delay)
    }

}
