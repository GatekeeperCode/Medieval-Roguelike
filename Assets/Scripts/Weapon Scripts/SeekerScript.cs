using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerScript : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] targetList = GameObject.FindGameObjectsWithTag("Gobbo");

        int loop = 0;
        while (targetList.Length == 0)
        {
            loop++;

            switch (loop)
            {
                case 1:
                    targetList = GameObject.FindGameObjectsWithTag("Gobbo");
                    break;
                case 2:
                    targetList = GameObject.FindGameObjectsWithTag("Goo");
                    break;
                case 3:
                    targetList = GameObject.FindGameObjectsWithTag("Minotaur");
                    break;
                case 4:
                    targetList = GameObject.FindGameObjectsWithTag("SpearEnemy");
                    break;
                case 5:
                    targetList = GameObject.FindGameObjectsWithTag("SwordEnemy");
                    break;
                default:
                    targetList = GameObject.FindGameObjectsWithTag("Monster");
                    break;
            }
        }

        foreach (GameObject item in targetList)
        {
            if(target==null)
            {
                target = item;
            }
            else if (Vector3.Distance(transform.position, item.transform.position) > Vector3.Distance(transform.position, target.transform.position))
            {
                target = item;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
