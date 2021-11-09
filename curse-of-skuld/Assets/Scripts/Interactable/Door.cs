using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    // Start is called before the first frame update
    private bool _canOpen = false;
    [SerializeField]
    private float openingSpeed;
    [SerializeField]
    public GameObject boss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        
        _canOpen = true;
    }

    public void Interact()
    {
        if (_canOpen)
        {
            StartCoroutine(Opening());
            // this.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    IEnumerator Opening()
    {
        
        this.transform.position =
            new Vector3(transform.position.x, transform.position.y, transform.position.z + openingSpeed);
        yield return new WaitForSeconds(0.2f);
        if (this.gameObject.transform.position.z < 54)
            boss.SetActive(true);
        if (this.gameObject.transform.position.z < 65)
            StartCoroutine(Opening());

    }
}
