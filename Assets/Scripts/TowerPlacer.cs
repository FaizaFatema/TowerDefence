using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if(transform.childCount == 0)
        {
            Instantiate(towerPrefab,transform.position, Quaternion.identity,transform);
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
