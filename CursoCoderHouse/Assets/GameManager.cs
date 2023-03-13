using UnityEngine;

public enum GameLoop
{
    MainMenu,
    Gameplay
}

public enum MainMenu
{
    PressStart,
    Menu,
    NewGame,
    Credits
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameLoop gameState;
    private MainMenu mainMenuState;
    private UIViewController mainMenuViewController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameState = GameLoop.MainMenu;
        mainMenuState = MainMenu.PressStart;
        mainMenuViewController = FindObjectOfType<UIViewController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenuState == MainMenu.PressStart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                HidePressStart();
            }
        }
        else if (mainMenuState == MainMenu.Menu)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mainMenuState = MainMenu.Menu;
            }
            else if (mainMenuState == MainMenu.NewGame)
            {

            }
        }
    public void HidePressStart()
    {
        mainMenuViewController.HidePressStart();
        mainMenuState = MainMenu.Menu;
    }
}
