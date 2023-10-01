using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfExplode : MonoBehaviour
{
    [SerializeField] float destroyDelay = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
