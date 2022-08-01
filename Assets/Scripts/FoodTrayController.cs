using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrayController : MonoBehaviour
{
    //Outlet
    public GameObject foodPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);

        //Generate Food
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newFood = Instantiate(foodPrefab);
            newFood.transform.position = mousePosition;
            
        }
    }


}
