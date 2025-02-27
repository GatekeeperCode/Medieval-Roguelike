using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterScript : MonoBehaviour
{
    public GameObject cluster;
    public int powerScale = 0;
    private Animation animator;

    // Start is called before the first frame update
    void Start()
    {
        cluster.SetActive(true);
        animator = cluster.GetComponent<Animation>();
        animator.Play("ClusterCore");
    }
}
