using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSlipperCollectable : MonoBehaviour
{
    ItemStorage itemStorage;
    void Start()
    {
        itemStorage = FindObjectOfType<ItemStorage>();
    }

    void OnTriggerEnter(Collider other)
    {
        itemStorage.glassSlipperAmount++;
        Destroy(gameObject);
    }
}
