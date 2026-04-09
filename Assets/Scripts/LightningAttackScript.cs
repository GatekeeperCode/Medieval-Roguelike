using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttackScript : MonoBehaviour
{
    public int stacks = 0;
    public GameObject lightning;
    public float multAdjuster = .25f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("lightningStrike", 2, 30 / stacks);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    lightningStrike();
        //}
    }

    private void lightningStrike()
    {
        GameObject target = findTarget();
        Vector3 spawnPoint = new Vector3(((target.transform.position.x + transform.position.x) / 2), ((target.transform.position.y + transform.position.y) / 2), 0);

        Vector2 relativePos = target.transform.position - transform.position;

        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;

        GameObject bolt = Instantiate(lightning, spawnPoint, Quaternion.Euler(0,0, angle));
        
        bolt.transform.localScale = new Vector2(Vector2.Distance(transform.position, target.transform.position), 0.2f);
        bolt.GetComponent<LightningObjectScript>().victim = target;
        bolt.GetComponent<LightningObjectScript>().damage = bolt.GetComponent<LightningObjectScript>().damage * GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>()._magicalStren * multAdjuster;
        bolt.GetComponent<LightningObjectScript>().targets.Add(target);
    }

    private GameObject findTarget()
    {
        List<GameObject> targetList = new List<GameObject>();

        GameObject target = null;

        int loop = 0;
        while (loop < 7)
        {
            loop++;

            switch (loop)
            {
                case 1:
                    foreach (GameObject m in GameObject.FindGameObjectsWithTag("Gobbo"))
                    {
                        targetList.Add(m);
                    }
                    break;
                case 2:
                    foreach (GameObject m in GameObject.FindGameObjectsWithTag("Goo"))
                    {
                        targetList.Add(m);
                    }
                    break;
                case 3:
                    foreach (GameObject m in GameObject.FindGameObjectsWithTag("Minotaur"))
                    {
                        targetList.Add(m);
                    }
                    break;
                case 4:
                    foreach (GameObject m in GameObject.FindGameObjectsWithTag("SpearEnemy"))
                    {
                        targetList.Add(m);
                    }
                    break;
                case 5:
                    foreach (GameObject m in GameObject.FindGameObjectsWithTag("SwordEnemy"))
                    {
                        targetList.Add(m);
                    }
                    break;
                default:
                    foreach (GameObject m in GameObject.FindGameObjectsWithTag("Monster"))
                    {
                        targetList.Add(m);
                    }
                    break;
            }
        }

        foreach (GameObject item in targetList)
        {
            if (target == null)
            {
                target = item;
            }
            else if (Vector3.Distance(transform.position, item.transform.position) < Vector3.Distance(transform.position, target.transform.position))
            {
                target = item;
            }
        }

        return target;
    }
}
