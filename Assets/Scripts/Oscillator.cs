using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 StartingPosition ;
    [SerializeField]Vector3 MovementVector;
    [SerializeField] [Range(0 ,1)]float MovementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
        Debug.Log(StartingPosition);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(period == 0f) {return;}
        float cycle = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycle * tau);

        MovementFactor = (rawSinWave + 1f) / 2f ;


        Vector3 Offset = MovementVector * MovementFactor;
        transform.position = StartingPosition + Offset;
    }
}
