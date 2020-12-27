using System.Collections;
using UnityEngine;
using System.Linq;

public class Hacker : MonoBehaviour
{
    string[] level1Passwords = { "book", "aisle", "shelf", "password", "borrow", "font" };
    string[] level2Passwords = { "prisoner", "arrest", "uniform", "handcuffs", "holster", "jailer" };
    string mainMenu = "Enter menu for main menu";
    bool waitingToHack = false;
    int level;
    enum Screen { MainMenu,Password,Win};
    Screen currentScreen = Screen.MainMenu;
    string password;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for police station");
        Terminal.WriteLine("Enter Selection : ");
    }

    void OnUserInput(string input)
    {
        if (!waitingToHack)
        {
            if (input == "menu")
            {
                ShowMainMenu();
            }
            else if (currentScreen == Screen.MainMenu)
            {
                RunMainMenu(input);
            }
            else if (currentScreen == Screen.Password)
            {
                CheckPassword(input);
            }
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Sorry, wrong password TRY AGAIN!!");
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch(level)
        {
            case 1:
                Terminal.WriteLine("Library hacked!! Have a book now....");
                Terminal.WriteLine(@"
    _______
   /      /,
  /      // Book(really??)
 /______//
(______(/
");
                if (!gameManager.PlacesHacked.Contains("library"))
                {
                    gameManager.PlacesHacked[0] =  "library";
                }
                else
                {
                    Terminal.WriteLine("You've already hacked Library");
                }
                break;
            case 2:
                Terminal.WriteLine("Police station hacked!!\n Do you have a friend there");
                Terminal.WriteLine(@"
    .--.
  /.-. '----------.\
  \'-' .--''--''''-/ 
   '--'
You got the key??
(⊙＿⊙')
");
                if (!gameManager.PlacesHacked.Contains("station"))
                {
                    gameManager.PlacesHacked[1] = "station";
                }
                else
                {
                    Terminal.WriteLine("You've already hacked Police Station");
                }
                break;
        }
        Terminal.WriteLine(mainMenu);
        print(gameManager.PlacesHacked[0]);
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            StartGame();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Choose a level Mr.Bond");
        }
        else
        {
            Terminal.WriteLine("Please choose a level");
            Terminal.WriteLine(mainMenu);
        }
    }

    void StartGame()
    {
        int index;
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        switch(level)
        {
            case 1:
                index = Random.Range(0, level1Passwords.Length);
                password = level1Passwords[index];
                break;
            case 2:
                index = Random.Range(0, level2Passwords.Length);
                password = level2Passwords[index];
                break;
            default:
                password = null;
                break;
        }
        Terminal.WriteLine("Trying to hack into systems....");
        StartCoroutine(WaitForHacking());
    }

    IEnumerator WaitForHacking()
    {
        waitingToHack = true;
        yield return new WaitForSeconds(2f);
        Terminal.WriteLine("We got this anagram\n" + password.Anagram());
        Terminal.WriteLine("Enter correct password : ");
        waitingToHack = false;
    } 
}
