
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickSpawnScript : MonoBehaviour
{

    //public GameObject spawnObject; //Assign this in inspector or in code for whatever prefab you want instantiated.
    public GameObject spawnedPrefab;


    public GameObject newobject;

    private Vector2 mousePosition;
    private float deltaX, deltaY;
    public static bool locked;
    private void Update()
    {


        if (Input.GetMouseButtonDown(0)) //If we click left mouse button
        {
      

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);


                if (hit.collider.gameObject.name == gameObject.name)
                {
                    Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
                    newobject = Instantiate(spawnedPrefab, rayCast.GetPoint(10), Quaternion.identity) as GameObject;
                }
            }

         
        }

    }

    private void OnMouseDown()
    {
        if (!locked)
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - newobject.transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - newobject.transform.position.y;
        } 
    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newobject.transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);

            //Debug.Log("x : " + transform.position.y);

            //float objectSizeX = objectArea.size.x * transform.localScale.x;
            //float objectSizeY = objectArea.size.y * transform.localScale.y;

            Camera camera = Camera.main;
            float screenY = camera.orthographicSize;
            float screenX = camera.aspect * screenY;

            //var posx = Mathf.Clamp(transform.position.x, -screenX + objectSizeX / 2, screenX - objectSizeX / 2);
            //var posy = Mathf.Clamp(transform.position.y, -screenY + objectSizeY / 2, screenY - objectSizeY / 2);

            newobject.transform.position = new Vector2(transform.position.x, transform.position.y);
        }

    }




    //https://kylewbanks.com/blog/unity-2d-detecting-gameobject-clicks-using-raycasts
    //https://stackoverflow.com/questions/43424118/instantiate-object-at-mouse-position






}