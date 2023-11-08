using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpider : ObstacleBase
{
    [SerializeField] public Transform target;
    [SerializeField] public float step = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.Normalize((target.position - transform.position) * step) * Time.deltaTime) ;
    }
}
