using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestGiver : MonoBehaviour
{
    public static string PlayerString = "Player";

    public bool inTrigger;

    public Quest unitQuest;



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {            
            if (inTrigger)
            {
                // give player 
            }
        }
    }


}

public class Quest
{
    public string questName;
}
