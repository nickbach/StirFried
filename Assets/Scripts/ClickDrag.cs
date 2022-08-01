using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDrag : MonoBehaviour
{
    [SerializeField]
    //private Transform bearPlace;

    private Vector2 initialPosition;

    private Vector2 mousePosition;

    private float deltaX, deltaY;

    public static bool locked;

    public BoxCollider2D objectArea;

    public GameObject[] woks;

    




    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        initialPosition = transform.position;


        
    }

    private void OnMouseDown()
    {
        if (!locked)
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }

    private void OnMouseDrag()
    {
        if(!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);

            //Debug.Log("x : " + transform.position.y);

            float objectSizeX = objectArea.size.x * transform.localScale.x;
            float objectSizeY = objectArea.size.y * transform.localScale.y;

            Camera camera = Camera.main;
            float screenY = camera.orthographicSize;
            float screenX = camera.aspect * screenY;

            var posx = Mathf.Clamp(transform.position.x, -screenX+objectSizeX/2, screenX-objectSizeX/2);
            var posy = Mathf.Clamp(transform.position.y, -screenY+objectSizeY/2, screenY-objectSizeY/2);

            transform.position = new Vector2(posx, posy); 

            /*
            if (transform.position.x > screenX - objectSizeX / 2 && transform.position.y > screenY - objectSizeY / 2)
            {
                transform.position = new Vector2(posx, posy);
            }
            if (transform.position.x > screenX - objectSizeX / 2 && transform.position.y < -screenY + objectSizeY / 2)
            {
                transform.position = new Vector2(screenX - objectSizeX / 2, -screenY + objectSizeY / 2);
            }
            if (transform.position.x < -screenX + objectSizeX / 2 && transform.position.y > screenY - objectSizeY / 2)
            {
                transform.position = new Vector2(-screenX + objectSizeX / 2, screenY - objectSizeY / 2);
            }
            if (transform.position.x < -screenX + objectSizeX / 2 && transform.position.y < -screenY + objectSizeY / 2)
            {
                
                transform.position = new Vector2(-screenX + objectSizeX/2 , -screenY + objectSizeY/2);
            }
            

            if (transform.position.y > screenY - objectSizeY / 2)
            {
                    transform.position = new Vector2(posx, posy);
            }

            if (transform.position.y < -screenY + objectSizeY / 2)
            {
                transform.position = new Vector2(mousePosition.x - deltaX, -screenY + objectSizeY / 2);
            }

            if (transform.position.x > screenX - objectSizeX / 2)
            {
                transform.position = new Vector2(screenX - objectSizeX / 2, mousePosition.y - deltaY);
            }

            if (transform.position.x < -screenX + objectSizeX / 2)
            {
                transform.position = new Vector2(-screenX + objectSizeX / 2, mousePosition.y - deltaY);
            }

        */

          


            
/*
            var pos = transform.position;
            *//*pos.x = Mathf.Clamp(pos.x, objectArea.size.x / Screen.width, (1-objectArea.size.x) / Screen.width);
            pos.y = Mathf.Clamp(pos.y, objectArea.size.y / Screen.height, (1 - objectArea.size.y) / Screen.height);*//*

            pos.x = Mathf.Clamp(objectArea.bounds.min.x, -Screen.width / 2, Screen.width / 2);
            pos.y = Mathf.Clamp(objectArea.bounds.min.y, -Screen.height / 2, Screen.height / 2);

            //pos.x = Mathf.Clamp(pos.x, pos.x, 1-pos.x);
            //pos.y = Mathf.Clamp(pos.y, pos.y, 1 - pos.y);
            
            *//*pos.x = Mathf.Clamp(pos.x, 0, 10);
            pos.y = Mathf.Clamp(pos.y, 0, 10);*//*
            transform.position = pos;
            Debug.Log("Current BoxCollider Size : " + objectArea.bounds.max);*/


        }
    }
    
    private void OnMouseUp()
    {

        for (int wok = 0; wok < woks.Length; wok++)
        {
            if (transform.position.x > woks[wok].transform.position.x - woks[wok].GetComponent<Collider2D>().bounds.size.x / 2 &&
            transform.position.x < woks[wok].transform.position.x + woks[wok].GetComponent<Collider2D>().bounds.size.x / 2 &&
            transform.position.y > woks[wok].transform.position.y - woks[wok].GetComponent<Collider2D>().bounds.size.y / 2 &&
            transform.position.y < woks[wok].transform.position.y + woks[wok].GetComponent<Collider2D>().bounds.size.y / 2)
            {
                Debug.Log("test" + woks[wok]);
            }
        }

        

        /*
        if (Mathf.Abs(transform.position.x - bearPlace.position.x) <= 0.5f &&
            Mathf.Abs(transform.position.y - bearPlace.position.y) <= 0.5f)
        {
            transform.position = new Vector2(bearPlace.position.x, bearPlace.position.y);
            locked = true;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
        */


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        /*Vector3 clampedPosition = transform.position;
        // limit the x and y positions to be between the area's min and max x and y.
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, area.bounds.min.x, area.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, area.bounds.min.y, area.bounds.max.y);
        // z remains unchanged
        // apply the clamped position
        transform.position = clampedPosition;*/
    }
}
