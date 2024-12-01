using UnityEngine;

public static class MenuManager
{
    public static bool IsInitialised { get; private set; }
    public static GameObject mainMenu, creditsMenu, pauseMenu;

    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        creditsMenu = canvas.transform.Find("CreditsMenu").gameObject;
        pauseMenu = canvas.transform.Find("PauseMenu").gameObject;

        IsInitialised = true;
    }

    public static void ChangeMenuState(Menu menu, MenuState newState, GameObject callingMenu = null)
    {
        if (!IsInitialised)
            Init();

        switch (menu)
        {
            case Menu.MAIN_MENU:
                mainMenu.SetActive(newState == MenuState.OPEN ? true : false);
                break;
            case Menu.CREDITS_MENU:
                creditsMenu.SetActive(newState == MenuState.OPEN ? true : false);
                break;
            case Menu.PAUSE_MENU:
                pauseMenu.SetActive(newState == MenuState.OPEN ? true : false);
                break;
        }

        if (newState == MenuState.CLOSE && callingMenu)
            throw new UnityException(
                "The value of the variable 'callingMenu' must be NULL because the value of the variable 'newState' is MenuState.CLOSE");

        if (callingMenu)
            callingMenu.SetActive(false);
    }
}

public enum MenuState
{
    OPEN,
    CLOSE
}