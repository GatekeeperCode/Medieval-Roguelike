using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWizardScript : EnemyBase
{
    public float damage;
    public GameObject elementPrefab;
    public GameObject elementRotation;
    bool hitStun;
    /*
     * Higher Scaling factor means slowing scaling in game.
     */
    public int scalingFactor;
    float lastPSCheck;
    public List<GameObject> elements = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _c = GetComponent<SpriteRenderer>().color;
        StartCoroutine("elementSpawn");
        hitStun = false;
        lastPSCheck = 0;
    }

    void scaleStats(float playerScore)
    {
        if (playerScore > 0)
        {
            lastPSCheck += playerScore;

            float scaleFun = Mathf.Pow(2, playerScore) / scalingFactor;

            if (scaleFun > scalingFactor) { scaleFun = 1.5f; }

            health = scaleFun * health;
            damage *= scaleFun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleStats(player.GetComponent<PlayerMovement>().score - lastPSCheck);

        elementRotation.transform.Rotate(0, 0, 50 * Time.deltaTime);

        if (!roomVars.playerPresent)
        {
            transform.position = resetPoint.transform.position;
        }
        else if(!hitStun)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (health <= 0)
        {
            player.GetComponent<PlayerMovement>()._defense += .5f;
            Destroy(gameObject);
        }
    }

    private IEnumerator elementSpawn()
    {
        yield return new WaitForSeconds(3f);
        int numElements = elements.Count;

        if(numElements<3)
        {
            GameObject newElement = Instantiate(elementPrefab);
            newElement.GetComponent<ElementCircleScript>().damage = damage;
            newElement.transform.parent = elementRotation.transform;
            elements.Add(newElement);


            for (int i = 0; i < numElements+1; ++i)
            {
                float theta = (2 * Mathf.PI / (numElements+1)) * i;
                float x = elementRotation.transform.position.x + Mathf.Cos(theta)* 1.75f;
                float y = elementRotation.transform.position.y + Mathf.Sin(theta) * 1.75f;
                elements[i].transform.position = new Vector2(x, y);
            }
        }

        StartCoroutine("elementSpawn");
    }

    public override IEnumerator hitReg()
    {
        hitStun = true;
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().color = _c;
        hitStun = false;
    }
}
