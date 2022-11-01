using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;//npc scriptable object

    bool isTalking = false;
    bool inRange = false;

    float distance;
    float curResponseTracker = 0;

    public GameObject player;
    public GameObject dialogueUI;

    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;


    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
    }

    private void Update()
    {
        if (inRange == true)
        {
            //makes sure curResponseTracker doesn't go out of bounds
            if (Input.GetKeyDown(KeyCode.J))
            {
                curResponseTracker++;
                if (curResponseTracker >= npc.playerDialogue.Length - 1)
                {
                    curResponseTracker = npc.playerDialogue.Length - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                curResponseTracker--;
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }

            //triggers dialogue with the NPC
            if (Input.GetKeyDown(KeyCode.E) && isTalking == false && inRange)
            {
                StartConversation();
            }
            else if (Input.GetKeyDown(KeyCode.E)&& isTalking == true)
            {
                EndDialogue();
            }

            for(int i = 0; i < npc.dialogue.Length; i++)
            {
                if (curResponseTracker == i && npc.playerDialogue.Length >= i)
                {
                    playerResponse.text = npc.playerDialogue[i];
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        npcDialogueBox.text = npc.dialogue[i + 1];
                    }
                }
            }
            /*
            if (curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
            {
                playerResponse.text = npc.playerDialogue[0];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[1];
                }
            }
            else if (curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
            {
                playerResponse.text = npc.playerDialogue[1];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[2];
                }
            }
            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
            {
                playerResponse.text = npc.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[3];
                }
            }
            */
        }

        if(inRange == false)
        {
            EndDialogue();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }
}
