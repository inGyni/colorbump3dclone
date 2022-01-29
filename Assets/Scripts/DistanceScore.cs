using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceScore : MonoBehaviour
{
    public Transform player;
    [SerializeField] Slider DistanceSlider;

    float initDistance;

    void Start()
    {
        initDistance = transform.position.z - player.position.z;
    }
    void Update()
    {
        float currentDistance = transform.position.z - player.position.z;
        DistanceSlider.value = (initDistance - currentDistance) / initDistance;
    }
}
