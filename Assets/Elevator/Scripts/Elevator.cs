using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Elevator : MonoBehaviour
{
    private Animator anim;
    public Transform spawner;
    public GameObject deliveryBox;
    public int strayFactor = 30;

    private string deliveryID, deliveryRecipient;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void NewDelivery(string guid, string recipient)
    {
        deliveryID = guid;
        deliveryRecipient = recipient;
        anim.SetTrigger("Open");
    }

    public void SpawnDelivery()
    {
        GameObject delivery = Instantiate(deliveryBox, spawner.position, spawner.rotation);
        var randomNumberX = Random.Range(-strayFactor, strayFactor);
        var randomNumberY = Random.Range(-strayFactor, strayFactor);
        var randomNumberZ = Random.Range(-strayFactor, strayFactor); ;
        delivery.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        delivery.GetComponent<Rigidbody>().AddForce(spawner.forward * (Random.Range(50, 450)));
        
        DeliveryBox box = delivery.GetComponent<DeliveryBox>(); 
        box.data.itemId = deliveryID;
        box.data.itemName = "Delivery For " + deliveryRecipient;
        box.objectName = "Delivery";
        box.objectDescription = "For " + deliveryRecipient;
    }
}
