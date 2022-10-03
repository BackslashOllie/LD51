using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public string eventName;
    public EventType type;

    public float startTime;
    [Header("Duration of Event")]
    public float durationMin,durationMax;
    [Space]
    [Header("Cool Down ")]
    public float coolDownMin, coolDownMax;
    [Space]
    float duration , coolDown;
    float count;

    public int eventValue = 10;
    
    public string objectIDRequired;

    public Notification notification;

    public bool isActive, inCoolDown;

    public bool inTrigger;

    public MeshRenderer sphereObj;
    // fail state

    private void Start()
    {
        sphereObj.enabled = false;
    }


    public virtual void CompleteEvent()
    {
        if (inCoolDown)
        {
            //TODO - remove items
            
            return;
        }

        if (objectIDRequired != "") Inventory.Instance.RemoveItem(objectIDRequired);

        LogManager.IncreaseCompletedTasks(eventValue * (duration - count));
        sphereObj.material.color = Color.blue;
        EndEvent(true);
    }

    public virtual void FailEvent()
    {
        
    }

    public void EndEvent(bool success)
    {

        if (!success) FailEvent();
        
        if (notification != null && notification.shown) notification.EndNotification(true);

        GameEventManager.RemoveEvent(this);


        isActive = false;
        switch (type)
        {
            case EventType.coffee:
                LogManager.log.coffeesDelivered++;
                break;
            case EventType.coffeeFilter:
                LogManager.log.filtersChanged++;
                break;
            case EventType.delivery:
                LogManager.log.boxesDelivered++;
                break;
            case EventType.bin:
                LogManager.log.firesExtinguished++;
                break;
            case EventType.toilet:
                LogManager.log.toiletsFixed++;
                break;
            case EventType.printer:
                LogManager.log.printersUnjammed++;
                break;
            case EventType.server:
                LogManager.log.serversReset++;
                break;
            case EventType.lightBulb:
                LogManager.log.lightBulbsChanged++;
                break;
            case EventType.BSOD:
                LogManager.log.BSODsFixed++;
                break;


            default:
                break;
        }

        StartCoroutine("DoCoolDown");
    }

    public void TriggerEnter()
    {
        // sorry is ugly
        StarterAssets.StarterAssetsInputs.Instance.SubscribeToInteractInput(OnInteract);
        inTrigger = true;
    }

    public void TriggerExit()
    {
        StarterAssets.StarterAssetsInputs.Instance.UnsubscribeToInteractInput(OnInteract);
        inTrigger = false;
    }

    public virtual void StartEvent(float time)
    {
        startTime = time;
        duration = Random.Range(durationMin, durationMax);
        coolDown = Random.Range(coolDownMin, coolDownMax);
        count = 0;
        isActive = true;

        sphereObj.material.color = Color.green;
        sphereObj.enabled = true;

        notification = NotificationManager.Instance.ShowNotification(eventName, duration, objectIDRequired);
    }

    public void OnInteract()
    {
        if(isActive && inTrigger)
        {
            bool canComplete = Inventory.Instance.HasItem(objectIDRequired) || objectIDRequired == "";

            if(canComplete) CompleteEvent();
        }
    }

    public void UpdateEvent(float timeDelta)
    {
        count += timeDelta;

        if (count > duration)
        {
            //Debug.Log("failed");
            sphereObj.material.color = Color.red;
            notification = null;// if (notification.shown) notification.EndNotification(false);
            LogManager.IncreaseFailed();

            switch (type)
            {
                case EventType.coffee:
                    LogManager.log.thirstyPeople++;
                    break;
                case EventType.coffeeFilter:
                    LogManager.log.filtersUnchanged++;
                    break;
                case EventType.delivery:
                    LogManager.log.boxesUndelivered++;
                    break;
                case EventType.bin:
                    LogManager.log.firesStillBurning++;
                    break;
                case EventType.toilet:
                    LogManager.log.toiletsStillBroken++;
                    break;
                case EventType.printer:
                    LogManager.log.printersJammed++;
                    break;
                case EventType.server:
                    LogManager.log.serversDown++;
                    break;
                case EventType.lightBulb:
                    LogManager.log.lightsStillFlickering++;
                    break;
                case EventType.BSOD:
                    LogManager.log.BSODsUnresolved++;
                    break;

                default:
                    break;
            }


            EndEvent(false);
        }
    }

    private IEnumerator DoCoolDown()
    {
        float time = 0;
        inCoolDown = true;
        while (time <= coolDown)
        {
            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        sphereObj.enabled = false;
        inCoolDown = false;
    }

}

public enum EventType
{    
    coffee,
    coffeeFilter,
    delivery,
    bin,
    toilet,
    printer,
    server,
    lightBulb,
    BSOD
}

