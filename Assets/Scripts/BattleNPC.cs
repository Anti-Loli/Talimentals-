using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleNPC : MonoBehaviour
{
    private enum State { Normal, Battle };
    State currentState;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NormalState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeState(State newState)
    {
        StopAllCoroutines();
        currentState = newState;

        switch (currentState)
        {
            case State.Normal:
                StartCoroutine(NormalState());
                break;
            case State.Battle:
                StartCoroutine(BattleState());
                break;
        } 
    }

    IEnumerator NormalState()
    {
        Debug.Log("i'm very normal right now (in normal state)");
        yield return null;
    }

    IEnumerator BattleState()
    {
        Debug.Log("i must kill (in battle state)");
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("works");
            ChangeState(State.Battle);
            //change state machine code
        }
    }

    //temporary?
    private void OnTriggerExit2D(Collider2D collision)
    {
        ChangeState(State.Normal);
    }

    //state machine code
}
