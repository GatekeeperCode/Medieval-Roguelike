using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCircleScript : MonoBehaviour
{
    public float damage;
    SphereWizardScript sws;

    private void Start()
    {
        sws = transform.parent.transform.parent.GetComponent<SphereWizardScript>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sws.elements.Remove(gameObject);
        Destroy(gameObject);
    }
}
