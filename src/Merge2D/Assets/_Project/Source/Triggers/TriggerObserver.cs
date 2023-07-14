using System;
using UnityEngine;

public class TriggerObserver : MonoBehaviour
{
    [SerializeField] private LayerMask layer;

    public event Action<GameObject> TriggerEnter;
    public event Action<GameObject> TriggerExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isNotTriggerLayer = (1 << other.gameObject.layer & layer) == 0;
        if (isNotTriggerLayer)
            return;

        TriggerEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bool isNotTriggerLayer = (1 << other.gameObject.layer & layer) == 0;
        if (isNotTriggerLayer)
            return;

        TriggerExit?.Invoke(other.gameObject);
    }
}
