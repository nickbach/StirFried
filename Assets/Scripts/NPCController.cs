using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class NPCController : MonoBehaviour
{
    public Dictionary<string, int> npcRequest = new Dictionary<string, int>();
    public int ordersize = 3;

    public GameController gameController; 

    public GameObject[] orderLocations;
    private bool[] orderInputs; 
    private GameObject[] orderImgs;

    public GameObject order1;
    public GameObject order2;
    public GameObject order3;
   
    public GameObject chickenprefab; 
    public GameObject broccoliprefab; 
    public GameObject mushroomprefab; 
    public GameObject onionprefab;
    public GameObject steakprefab; //4
    public GameObject shrimpprefab;  //5

    public GameObject steakTray;
    public GameObject shrimpTray; 

    private bool firstPlay = true;
    public Image emotionBar;

    public float playerHealth = 100;

    public bool takeoutboxready;
  

    //function that randomly generates an order request
    Dictionary<string,int> npcOrder()
    {
        
        int[] numIng = randarraygenerator();
        if (numIng[0] > 0)
        {
            npcRequest.Add("chicken", numIng[0]);
        }
        if (numIng[1] > 0)
        {
            npcRequest.Add("broccoli", numIng[1]);
        }
        if (numIng[2] > 0)
        {
            npcRequest.Add("mushroom", numIng[2]);
        }
        if (numIng[3] > 0)
        {
            npcRequest.Add("onion", numIng[3]);
        }
        if (steakTray.activeSelf == true)
        {
            if (numIng[4] > 0)
            {
                npcRequest.Add("steak", numIng[4]);
            }
        }
        if (shrimpTray.activeSelf == true)
        {
            if (numIng[5] > 0)
            {
                npcRequest.Add("shrimp", numIng[5]);
            }
        } 
        return npcRequest;
    }



    int[] randarraygenerator()
    {
        if (steakTray.activeSelf == false && shrimpTray.activeSelf == false)
        {
            int[] numIng = { 0, 0, 0, 0 };
            int count = 0;
            while (count < ordersize)
            {
                int index = Random.Range(0, 4);
                ++numIng[index];
                ++count;
            }
            return numIng;
        }
        else if (steakTray.activeSelf == true && shrimpTray.activeSelf == false)
        {
            int[] numIng = { 0, 0, 0, 0, 0 };
            int count = 0;
            while (count < ordersize)
            {
                int index = Random.Range(0, 5);
                ++numIng[index];
                ++count;
            }
            return numIng;
        }
        else
        {
            int[] numIng = { 0, 0, 0, 0, 0, 0 };
            int count = 0;
            while (count < ordersize)
            {
                int index = Random.Range(0, 6);
                ++numIng[index];
                ++count;
            }
            return numIng;
        }

        
        
    }


    // Start is called before the first frame update
    void Start()
    {
      
       
    }

    // Update is called once per frame
    void Update()
    {
       

        if (gameController.paused)
        {
            return; 
        }

        int levelDifficulty = gameController.day;

        playerHealth -= Time.deltaTime * (2+(levelDifficulty/2));
   
        emotionBar.fillAmount = playerHealth / 100;

        if (this.emotionBar.fillAmount < .7 && this.emotionBar.fillAmount > .3)
        {
            emotionBar.color = Color.yellow; 
        } 
        else if (this.emotionBar.fillAmount < .30)
        {
            emotionBar.color = Color.red;
        }
        else
        {
            this.emotionBar.color = Color.green; 
        }

        if (this.emotionBar.fillAmount == 0)
        {
            this.enabled = false;
            this.playerHealth = 100; 
            this.enabled = true;
            gameController.lostMoney(10);

        }


    }

    private void OnEnable()
    {

        int spriteNumber = Random.Range(1, 69);
        var renderer = GetComponent<SpriteRenderer>();
      //  Debug.Log("Character/" + spriteNumber); 
        var newSprite = Resources.Load<Sprite>("Characters/" + spriteNumber);
        renderer.sprite = newSprite;
        this.playerHealth = 100;
        emotionBar.color = Color.green; 
       this.emotionBar.fillAmount = playerHealth; 

        if (firstPlay)
        {
            orderInputs = new bool[ordersize];
            orderImgs = new GameObject[ordersize]; 
            for (int i = 0; i < orderInputs.Length; ++i)
            {
                orderInputs[i] = false;
            }
            firstPlay = false; 
        } 
        for (int i = 0; i < orderInputs.Length; ++i)
        {
            if (orderInputs[i])
            {
                Destroy(orderImgs[i]);
                orderInputs[i] = false; 
            }
        }
       

        npcRequest.Clear(); 
        takeoutboxready = false;
        npcOrder();


        string output = "NPC Order: ";

        List<string> orderImages = new List<string>();



        foreach (KeyValuePair<string, int> pair in npcRequest)
        {
            output += pair.Key + ": " + pair.Value + " ";
            int tick = pair.Value;
            while (tick > 0)
            {
                orderImages.Add(pair.Key);
                tick--;
            }

        }

        

        foreach (string s in orderImages)
        {
            if (s == "steak")
            {

                for(int i = 0; i < orderInputs.Length; ++i )
                {
                    if (!orderInputs[i])
                    {
                        orderImgs[i] = Instantiate(steakprefab);
                        orderImgs[i].transform.SetParent(orderLocations[i].transform);
                        orderImgs[i].transform.position = orderLocations[i].transform.position;
                        orderInputs[i] = true;
                        break; 
                    }
                }

              
            }
            if (s == "shrimp")
            {

                for (int i = 0; i < orderInputs.Length; ++i)
                {
                    if (!orderInputs[i])
                    {
                        orderImgs[i] = Instantiate(shrimpprefab);
                        orderImgs[i].transform.SetParent(orderLocations[i].transform);
                        orderImgs[i].transform.position = orderLocations[i].transform.position;
                        orderInputs[i] = true;
                        break;
                    }
                }


            }
            if (s == "chicken")
            {
                for (int i = 0; i < orderInputs.Length; ++i)
                {
                    if (!orderInputs[i])
                    {
                        orderImgs[i] = Instantiate(chickenprefab);
                        orderImgs[i].transform.SetParent(orderLocations[i].transform);
                        orderImgs[i].transform.position = orderLocations[i].transform.position;
                        orderInputs[i] = true;
                        break; 
                    }
                }
            
            }
            if (s == "mushroom")
            {
                for (int i = 0; i < orderInputs.Length; ++i)
                {
                    if (!orderInputs[i])
                    {
                        orderImgs[i] = Instantiate(mushroomprefab);
                        orderImgs[i].transform.SetParent(orderLocations[i].transform); 
                        orderImgs[i].transform.position = orderLocations[i].transform.position;
                        orderInputs[i] = true;
                        break; 
                    }
                }
             
            }
            if (s == "broccoli")
            {

                for (int i = 0; i < orderInputs.Length; ++i)
                {
                    if (!orderInputs[i])
                    {
                        orderImgs[i] = Instantiate(broccoliprefab);
                        orderImgs[i].transform.SetParent(orderLocations[i].transform);
                        orderImgs[i].transform.position = orderLocations[i].transform.position;
                        orderInputs[i] = true;
                        break; 
                    }
                }
               
            }
            if (s == "onion")
            {

                for (int i = 0; i < orderInputs.Length; ++i)
                {
                    if (!orderInputs[i])
                    {
                        orderImgs[i] = Instantiate(onionprefab);
                        orderImgs[i].transform.SetParent(orderLocations[i].transform);
                        orderImgs[i].transform.position = orderLocations[i].transform.position;
                        orderInputs[i] = true;
                        break; 
                    }
                }
             
            }
        }

        Debug.Log(output);



    }
}
