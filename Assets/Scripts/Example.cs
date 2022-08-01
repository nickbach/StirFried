using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{

    public GameObject prefab;
    public GameController gameController; 
  
    private Transform spawn;
    private Rect rect = new Rect(0, 0, 100, 50);

    public GameObject[] woks;
    
  /* private Dictionary<int, Dictionary<string,int>> allWokIngredients = new Dictionary<int, Dictionary<string, int>>();
 
    */

    void Start()
    {

    }

    void Update()
    {
        if (gameController.paused)
        {
            return; 
        }


        if (Input.GetMouseButton(0) && spawn != null)
        {
            
            var pos = Input.mousePosition;
            pos.z = -Camera.main.transform.position.z;
            spawn.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            var inWok = false;
            var wokNum = -1; 

            if (spawn != null)
            {
                for (int wok = 0; wok < woks.Length; wok++)
                {
                    if (spawn.transform.position.x > woks[wok].transform.position.x - woks[wok].GetComponent<CircleCollider2D>().bounds.size.x / 2 &&
                    spawn.transform.position.x < woks[wok].transform.position.x + woks[wok].GetComponent<CircleCollider2D>().bounds.size.x / 2 &&
                    spawn.transform.position.y > woks[wok].transform.position.y - woks[wok].GetComponent<CircleCollider2D>().bounds.size.y / 2 &&
                    spawn.transform.position.y < woks[wok].transform.position.y + woks[wok].GetComponent<CircleCollider2D>().bounds.size.y / 2)
                    {
                        inWok = true;
                        wokNum = wok;

                        spawn.transform.SetParent(woks[wok].transform); 
                        
                            var myScript = woks[wokNum].GetComponent<WokController>();
                            if (myScript.wokIngredients.ContainsKey(prefab.name)) {
                                myScript.wokIngredients[prefab.name]++; 
                            }
                            else
                            {
                                myScript.wokIngredients.Add(prefab.name, 1); 
                            }

                            string output = "WOK #" + wok + ": ";

                            foreach (KeyValuePair<string, int> pair in myScript.wokIngredients)
                            {
                                output += pair.Key + ": " + pair.Value + " ";
                            }
                           // Debug.Log(output);
             
                }

            }

                
            } 

            if (spawn != null && inWok == false)
            {
                Destroy(spawn.gameObject);
            }
            if (spawn != null && inWok == true)
            {
                spawn = null; 
            }
        }
    }

    void OnGUI()
    {
        if (gameController.paused)
        {
            return;
        }

        Event e = Event.current;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            if (e.type == EventType.MouseDown && hit.collider.gameObject.name == gameObject.name)
            {
                var pos = Input.mousePosition;
                pos.z = -Camera.main.transform.position.z;
                pos = Camera.main.ScreenToWorldPoint(pos);
                
                spawn = Instantiate(prefab.transform, pos, Quaternion.identity) as Transform;
            }

        }

    }
}