using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public OrderScreen orderScreen;
    public Transform plateSpawnSpot;
    public Transform plateInitialPoint;
    public GameObject platePrefab;

    public ConveyorBelt conveyorBelt;

    //public ChefMovement chefAnimation;
    public DialogueTrigger chefGrayDialogue;

    //to track each round to stop game later on GameOver/Win state
    private bool isGameComplete = false;
    public bool hasGameWin = false; //only true when game -> Win && complete

    //End of the Game UI Elements
    public CanvasGroup endGameCanvas;
    public TextMeshProUGUI gameResultText;
    public float fadeDuration = 1f;


    public static GameManager Instance; // Singleton
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }


    private void Start()
    {
        // When game starts -> need order and new plate ready 
        orderScreen.CreateRandomOrder();
        SpawnNewPlate();

        chefGrayDialogue.TriggerDialogue(DialogueTrigger.DialogueType.Intro);

        isGameComplete = false;
        hasGameWin = false;

        endGameCanvas.alpha = 0f;
        endGameCanvas.interactable = false;
        endGameCanvas.blocksRaycasts = false;
        gameResultText.text = "";
    }

    public void SpawnNewPlate()
    {
        if (!isGameComplete)
        {
            GameObject newPlate = Instantiate(platePrefab, plateSpawnSpot.position, Quaternion.identity);
            conveyorBelt.SetCurrentPlate(newPlate.transform); //pass latest plate to conveyor belt to move it
            StartCoroutine(PlateInitialMovement(newPlate, plateInitialPoint.position, 1f));
        }
        else
        {
            Debug.Log("Game is complete -> no more plate is spawned !");
        }
        
    }

    public bool MatchOrder(PlateContainer plate)
    {
        if (plate != null && orderScreen != null)
        {
            // Get the order from plate
            var (plateBase, plateSyrup, plateDecor) = plate.CurrentOrderOnPlate();

            //Get the order from screen
            var (screenBase, screenSyrup, screenDecor) = orderScreen.CurrentOrderOnScreen();

            // Compare them, if even one item is false -> all Fail
            return plateBase == screenBase &&
                   plateSyrup == screenSyrup &&
                   plateDecor == screenDecor;

        }

        Debug.LogWarning("Plate or OrderScreen reference missing.");
        return false;
    }

    private IEnumerator PlateInitialMovement(GameObject plate, Vector3 targetPos, float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = plate.transform.position;

        while (elapsed < duration)
        {
            if (plate == null) yield break;
            plate.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        plate.transform.position = targetPos;
        plate.transform.position += new Vector3(1.5f, 0f, 0f);
    }

    //you win the game when you complete all 5orders with 4/5 accuracy 
    public void WinGame()
    {
        isGameComplete = true;
        hasGameWin = true;

        //start chefGray Transition and Win dialogue
        chefGrayDialogue.TriggerDialogue(DialogueTrigger.DialogueType.Win);

    }

    //Game is over when you get 2 orders wrong 
    public void GameOver()
    {
        isGameComplete = true;

        //start chefGray Transition and Lose dialogue 
        chefGrayDialogue.TriggerDialogue(DialogueTrigger.DialogueType.Lose);

    }

    public void EndOfGameUI()
    {
        if (!isGameComplete)
        {
            return;
        }

        StartFadeInEndOfLevelUI();

        if (hasGameWin)
        {
            gameResultText.text = "VICTORY";

        }else if (!hasGameWin)
        {
            gameResultText.text = "GAME OVER";
        }
        else
        {
            Debug.LogWarning("End of Game UI Text Missing or unassigned");
        }

    }

    public void RetryGame()
    {
        // Reload the current active scene (since the scope of game isn't large yet)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartFadeInEndOfLevelUI()
    {
        StartCoroutine(FadeInEndOfLevelUI());
    }

    private IEnumerator FadeInEndOfLevelUI()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            endGameCanvas.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        endGameCanvas.interactable = true;
        endGameCanvas.blocksRaycasts = true;
    }

}
