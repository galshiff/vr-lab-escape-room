using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMonsterScript : MonoBehaviour
    { 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateVictoryWin(float seconds)
    {
        gameObject.SetActive(true);
        StartCoroutine(activateVictoryWinForTime(seconds));
    }

    IEnumerator activateVictoryWinForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
        // PartyMonsterEndLevelWin.gameObject.GetComponent<Animator>().enabled = false;
    }
}
