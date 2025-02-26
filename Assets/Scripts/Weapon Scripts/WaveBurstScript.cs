using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBurstScript : MonoBehaviour
{
    public GameObject wave;
    public int powerScale = 0;
    private Animation animator;


    // Start is called before the first frame update
    void Start()
    {
        wave.SetActive(true);
        animator = wave.GetComponent<Animation>();
        animator.Play("WaveBurstGrow");
    }
}
