using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour

{
    public static GameController instance;
    public GameObject gameovermenu;
    public GameObject daymenu;
    public GameObject startmenu; 
    public Text moneyText;
    public Text dayText;
    public Text dayMenuText;
    public Text HighScoreText;
    public Text newrecordText;
    public Button reset; 
    
    public Text ScoreText;
    public Image timeImage;
    public GameObject sunImage;
    public float time;
    public const float fullTime = 100;
    public int money;
    public int highscore; 
    public int day;
    public bool paused = false;

    public GameObject wok2;
    public GameObject wok3;
    public Button addWok;

    public GameObject customer1;
    public GameObject customer1emotion; 
    public GameObject customer2;
    public GameObject customer2emotion; 
    public GameObject customer3;
    public GameObject customer3emotion;

    public GameObject steakTray;
    public GameObject shrimpTray;
    public Button addIng;
    public Button customertreat;
    public int custTreatCost = 10; 

    private void Awake()
    {
        startmenu.SetActive(false);
        daymenu.SetActive(false); 
        gameovermenu.SetActive(false); // hide game over menu
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        reset.interactable = true;

        time = fullTime;
        money = PlayerPrefs.GetInt("Money"); 
        highscore = PlayerPrefs.GetInt("HighScore"); 
        day = PlayerPrefs.GetInt("Day");
        moneyText.text = "$" + money.ToString();
        dayText.text = "Day " + day.ToString();
        addWok.interactable = false;
        addIng.interactable = false;
        customertreat.interactable = false; 


        if (!PlayerPrefs.HasKey("isFirstTime") || PlayerPrefs.GetInt("isFirstTime") != 1)
        {
            startmenu.SetActive(true);
            
            paused = true; 
         }

        if (PlayerPrefs.GetInt("NumWoks") <= 2)
        {
            wok2.SetActive(false);
            wok3.SetActive(false);
        }

        if(PlayerPrefs.GetInt("NumWoks") == 3 )
        {
            wok2.SetActive(true);
            wok3.SetActive(false);

        }

        if (PlayerPrefs.GetInt("NumWoks") == 4)
        {
            wok2.SetActive(true);
            wok3.SetActive(true);
            addWok.interactable = false; 
        }





        if (PlayerPrefs.GetInt("NumIngs") <= 4)
        {
            steakTray.SetActive(false);
            shrimpTray.SetActive(false);
        }

        if (PlayerPrefs.GetInt("NumIngs") == 5)
        {
            steakTray.SetActive(true);
            shrimpTray.SetActive(false);

        }

        if (PlayerPrefs.GetInt("NumIngs") == 6)
        {
            steakTray.SetActive(true);
            shrimpTray.SetActive(true);
            addIng.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            reset.interactable = false; 
            return; 
        }

        sunImage.SetActive(true); 
        time -= Time.deltaTime * (2); // day timer couter
        Vector2 timepos = new Vector2(sunImage.transform.position.x, sunImage.transform.position.y);
        timepos.x = -((18 * time) / fullTime - 8);
        timepos.y = Mathf.Sqrt((1 - ((timepos.x + 1) * (timepos.x + 1)) / 81) * 25);
        //timepos.y = 1;
        sunImage.transform.position = timepos;
        timeImage.fillAmount = time / fullTime; // day timer filler 

        customer1.SetActive(true);
        customer1emotion.SetActive(true); 

        if (time < 90)
        {
            customer2.SetActive(true);
            customer2emotion.SetActive(true); 
        }
        if (time < 83)
        {
            customer3.SetActive(true);
            customer3emotion.SetActive(true); 
        }

        if (this.timeImage.fillAmount < 0.001) // if the day is over 
        {
            if (money < 0) // if we're out of money 
            {
                 // end game 
                gameovermenu.SetActive(true);
                ScoreText.text = "Score: " + day.ToString() + " days";
                if(day >= highscore)
                {
                    PlayerPrefs.SetInt("HighScore", day);
                    HighScoreText.text = "High Score: " + day.ToString() + " days";
                    newrecordText.text = "New Record!"; 
                }
                else
                {
                    HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString() + " days";
                    newrecordText.text = ""; 
                }


                paused = true; 
            }
            else
            {
                // if the day's over and we're not out of money, start new day 
                paused = true;
                dayMenuText.text = "You've made it through day " + day.ToString() + "!";
                daymenu.SetActive(true);

            }
        }

        updateDisplay();

        if(money >= 10 && PlayerPrefs.GetInt("NumWoks") < 4 )
        {
           
                addWok.interactable = true;
           
        }
        else
        {
            addWok.interactable = false; 
        }

        if (money >= 10 && PlayerPrefs.GetInt("NumIngs") < 6)
        {

            addIng.interactable = true;

        }
        else
        {
            addIng.interactable = false;
        }

        if (money >= custTreatCost)
        {
            customertreat.interactable = true; 
        }
        else
        {
            customertreat.interactable = false; 
        }
    }

    public void earnedMoney() 
    {
        money += 10;
    }

    public void lostMoney(int x)
    {
        money -= x; 
    }


    public void StartNewDay() // starts new day 
    {
        paused = false; 
        daymenu.SetActive(false);
        time = 100;
        day++;
        dayText.text = "Day " + day.ToString();
        PlayerPrefs.SetInt("Day", day);
        SceneManager.LoadScene("SampleScene");

    }

    void updateDisplay() // updates money 
    {
        PlayerPrefs.SetInt("Money", money);
        moneyText.text = "$" + money.ToString();

    }

    public void LoadNewGame()

    {
        day = 1;
        money = 0;
        PlayerPrefs.SetInt("Day", day);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.DeleteKey("isFirstTime");
        PlayerPrefs.DeleteKey("NumWoks");
        PlayerPrefs.DeleteKey("NumIngs"); 
        PlayerPrefs.Save();
        //day = 1; 
        SceneManager.LoadScene("SampleScene"); 
    }

    public void Begin()

    {
        day = 1;
        money = 0;
        PlayerPrefs.SetInt("Day", day);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("isFirstTime", 1);
        PlayerPrefs.DeleteKey("NumWoks");
        PlayerPrefs.DeleteKey("NumIngs"); 
        PlayerPrefs.Save();
        //day = 1; 
        SceneManager.LoadScene("SampleScene");
    }

    public void addNewWok()
    {

            if (PlayerPrefs.GetInt("NumWoks") <= 2)
            {
                int numwoks = 3; 
                wok2.SetActive(true);
                PlayerPrefs.SetInt("NumWoks", numwoks);
                money -= 10; 
                PlayerPrefs.Save(); 
            }
            else if (PlayerPrefs.GetInt("NumWoks") == 3){
                int numwoks = 4;
                wok3.SetActive(true);
                PlayerPrefs.SetInt("NumWoks", numwoks);
                money -= 10; 
                PlayerPrefs.Save();

            }
        
    }

    public void addNewIngredient()
    {
          if (PlayerPrefs.GetInt("NumIngs") <= 4)
            {
                int numIngs = 5;
                 steakTray.SetActive(true);
                PlayerPrefs.SetInt("NumIngs", numIngs);
                money -= 10; 
                PlayerPrefs.Save(); 
            }
            else if (PlayerPrefs.GetInt("NumIngs") == 5){
                int numIngs = 6;
                 shrimpTray.SetActive(true);
                PlayerPrefs.SetInt("NumIngs", numIngs);
                money -= 10; 
                PlayerPrefs.Save();

            }
    }

    public void customerTreat()
    {
        if (money > custTreatCost)
        {
            customer1.GetComponent<NPCController>().playerHealth = 100;
            customer2.GetComponent<NPCController>().playerHealth = 100;
            customer3.GetComponent<NPCController>().playerHealth = 100;
            money -= custTreatCost; 
        }
    }

}
