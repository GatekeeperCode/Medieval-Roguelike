using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningObjectScript : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject lightning;
    public float multAdjuster = .25f;
    public float damage;
    public GameObject victim;

    // Start is called before the first frame update
    void Start()
    {
        if(targets.Count <= 3)
        {
            lightningStrike();
        }

        StartCoroutine("LightningFlash");
    }

    private void lightningStrike()
    {
        GameObject target = findTarget();

        print(target);

        Vector3 spawnPoint = new Vector3(((target.transform.position.x + victim.transform.position.x) / 2), ((target.transform.position.y + victim.transform.position.y) / 2), 0);

        Vector2 relativePos = target.transform.position - victim.transform.position;

        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;

        GameObject bolt = Instantiate(lightning, spawnPoint, Quaternion.Euler(0, 0, angle));

        bolt.transform.localScale = new Vector2(Vector2.Distance(victim.transform.position, target.transform.position), 0.2f);
        bolt.GetComponent<LightningObjectScript>().victim = target;
        bolt.GetComponent<LightningObjectScript>().damage = damage;
        
        foreach(GameObject item in targets)
        {
            bolt.GetComponent<LightningObjectScript>().targets.Add(item);
        }

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
            if (targets.Contains(item))
            { }
            else if (target == null)
            {
                target = item;
            }
            else if (Vector3.Distance(victim.transform.position, item.transform.position) < Vector3.Distance(victim.transform.position, target.transform.position))
            {
                target = item;
            }
        }

        return target;
    }

    private IEnumerator LightningFlash()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
