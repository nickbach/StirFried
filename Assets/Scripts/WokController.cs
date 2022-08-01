using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WokController : MonoBehaviour
{

    public Dictionary<string, int> wokIngredients = new Dictionary<string, int>();
    public GameObject[] npcs;
    private GameObject takeout;
    private GameObject trash; 
    private GameObject fire;
    private GameObject timer;
    public GameObject takeoutprefab;
    public GameObject trashprefab;
    public GameObject fireprefab;
    public GameObject woktimerprefab;
    public float timerDelay = 5.5f;
    bool wokTimerDone = false;
    public GameController gamecontroller;
 

    void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {

        
        if (gamecontroller.paused)
        {
           return; 
        }

        if (fire != null)
        {
            if (wokTimerDone)
            {
                var renderer = fire.GetComponent<SpriteRenderer>();
                var steam = Resources.Load<Sprite>("Prefabs/Steam");
                renderer.sprite = steam;
            }
        }

        if (fire == null)
        {

           

            var numWokIngredients = 0;

            foreach (KeyValuePair<string, int> pair in wokIngredients)
            {
                numWokIngredients += pair.Value;
                //Debug.Log("NumWokIngredients:" + numWokIngredients);
            }

            if (numWokIngredients == 3)
            {

                fire = Instantiate(fireprefab);
                Vector2 firepos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                firepos.y = firepos.y + .45f;
                firepos.x = firepos.x + .3f; 
                fire.transform.position = firepos;
                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject);

                }

                timer = Instantiate(woktimerprefab);
                Vector2 timerpos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                timerpos.y = timerpos.y + .22f;
                timerpos.x = timerpos.x + .27f;
                timer.transform.position = timerpos;


                StartCoroutine("CookingTimer");
                
            }
        } 

        if (Input.GetMouseButton(0) && takeout != null)
        {

            var pos = Input.mousePosition;
            pos.z = -Camera.main.transform.position.z;
            takeout.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
       else if(Input.GetMouseButton(0) && trash != null)
        {

            var pos = Input.mousePosition;
            pos.z = -Camera.main.transform.position.z;
            trash.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            var toCharacter = false;
            var playerNum = -1;
            //bool rightPlayer = false;

         
            if (takeout != null)
            {
                for (int player = 0; player < npcs.Length; player++)
                {
                    if (takeout.transform.position.x > npcs[player].transform.position.x - npcs[player].GetComponent<CapsuleCollider2D>().bounds.size.x / 2 &&
                    takeout.transform.position.x < npcs[player].transform.position.x + npcs[player].GetComponent<CapsuleCollider2D>().bounds.size.x / 2 &&
                    takeout.transform.position.y > npcs[player].transform.position.y - npcs[player].GetComponent<CapsuleCollider2D>().bounds.size.y / 2 &&
                    takeout.transform.position.y < npcs[player].transform.position.y + npcs[player].GetComponent<CapsuleCollider2D>().bounds.size.y / 2)
                    {
                        toCharacter = true;
                        if (npcs[player].GetComponent<NPCController>().takeoutboxready)
                        {
                            // WINSSS
                            npcs[player].GetComponent<NPCController>().enabled = false;
                            gamecontroller.earnedMoney();
                            Destroy(takeout.gameObject);
                            npcs[player].GetComponent<NPCController>().enabled = true;

                        }
                       else if (!npcs[player].GetComponent<NPCController>().takeoutboxready)
                        {
                            // LOSSS
                            gamecontroller.lostMoney(5); 
                            Destroy(takeout.gameObject); 
                        }

                        playerNum = player;
                    }
                }

            }

            if (trash != null)
            {
                //LOSS
                Destroy(trash.gameObject);
                gamecontroller.lostMoney(5);

            }


            if (takeout != null && toCharacter == false)
            {
                //LOSS
                Destroy(takeout.gameObject);
                gamecontroller.lostMoney(5);

            }
            if (takeout != null && toCharacter == true)
            {
                takeout = null;
            }

        }

    }

    void OnMouseDown()
    {
        bool matchesOrder = false; 
        Debug.Log("click");

        for(int i = 0; i < npcs.Length; ++i)
        {
            int checkcount = 0;
            foreach (KeyValuePair<string, int> reqIng in npcs[i].GetComponent<NPCController>().npcRequest)
            {
                if(wokIngredients.ContainsKey(reqIng.Key))
                {
                    if(wokIngredients[reqIng.Key]==reqIng.Value)
                    {
                        checkcount = checkcount + reqIng.Value;
                    }
                }
            }

            Debug.Log(checkcount);
            if(checkcount == npcs[i].GetComponent<NPCController>().ordersize && wokTimerDone)
            {
                npcs[i].GetComponent<NPCController>().takeoutboxready = true; 
                takeout = Instantiate(takeoutprefab);
                takeout.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

                Debug.Log("success");

                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject);

                }
                matchesOrder = true; 
              // break;
            }
           
        }
        if (matchesOrder)
        {
            wokIngredients.Clear();
            wokTimerDone = false;
            Destroy(fire);

        }
        if (!matchesOrder)
        {
            trash = Instantiate(trashprefab);
            trash.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("fail");

            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);

            }
            // Destroy(npcs[i]);
            Destroy(fire); 
            wokIngredients.Clear();
            if (timer!= null)
            {
                Destroy(timer.gameObject);
            }
            wokTimerDone = false;
        }

    }

    IEnumerator CookingTimer()
    {
        yield return new WaitForSeconds(timerDelay);

        wokTimerDone = true;
        Destroy(timer.gameObject);
     
    }

    
}
