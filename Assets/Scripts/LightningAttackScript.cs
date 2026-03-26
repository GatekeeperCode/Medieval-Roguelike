using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttackScript : MonoBehaviour
{
    public int stacks;
    public GameObject lightning;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("lightningStrike", 2, 25 / stacks);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            lightningStrike();
        }
    }

    private void lightningStrike()
    {
        GameObject target = findTarget();
        Vector3 spawnPoint = new Vector3(((target.transform.position.x + transform.position.x) / 2), ((target.transform.position.y + transform.position.y) / 2), 0);

        Vector2 relativePos = target.transform.position - transform.position;

        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;

        GameObject bolt = Instantiate(lightning, spawnPoint, Quaternion.Euler(0,0, angle));
        
        bolt.transform.localScale = new Vector2(Vector2.Distance(transform.position, target.transform.position), 0.2f);
    }

    private GameObject findTarget()
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

        GameObject target = null;

        foreach (GameObject item in targetList)
        {
            if (target == null)
            {
                target = item;
            }
            else if (Vector3.Distance(transform.position, item.transform.position) > Vector3.Distance(transform.position, target.transform.position))
            {
                target = item;
            }

        }

        return target;
    }
}
