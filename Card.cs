using UnityEngine;

public class Card : MonoBehaviour
{

    public int cardValue;
    public int cardNumber;

  public  bool canClick = true;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("Scripts").GetComponent<GameManager>();
    }

    public void OnClick() {

        if (canClick == false) { return; }

        string result = "lose";

        if (cardValue > GameManager.cardBotValue) {

            result = "win";

        } else if (cardValue == GameManager.cardBotValue) {

            result = "draw";

        }

        gameManager.UpdateGameInfo(result, cardNumber);

        canClick = false;
    }

}
