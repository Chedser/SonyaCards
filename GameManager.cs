using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int win;
    int lose;
    int draw;
    int total;
    int totalWithDraw;
    float fortune;

  public static  int cardBotValue = 6;
    public int cardNumber = 0;
    public static bool canResetGame = false;

    [SerializeField] Text winValueTxt;
    [SerializeField] Text loseValueTxt;
    [SerializeField] Text drawValueTxt;
    [SerializeField] Text totalValueWithDrawTxt;
    [SerializeField] Text fortuneValueWithDrawTxt;

    [SerializeField] GameObject activeCards;
    [SerializeField] GameObject cardSlot;

    [SerializeField] AudioManager audioManager;

    private void Awake()
    {

        InitGame();
        ShowCards();

    }

    void InitGame() {

        canResetGame = false;
        cardNumber = 0;

        if (!PlayerPrefs.HasKey("win"))
        {

            PlayerPrefs.SetInt("win", 0);

        }

        if (!PlayerPrefs.HasKey("lose"))
        {

            PlayerPrefs.SetInt("lose", 0);

        }

        if (!PlayerPrefs.HasKey("draw"))
        {

            PlayerPrefs.SetInt("draw", 0);

        }

        win = PlayerPrefs.GetInt("win");
        lose = PlayerPrefs.GetInt("lose");
        draw = PlayerPrefs.GetInt("draw");

        total = win + lose;
        totalWithDraw = total + draw;

        if (win == 0 && lose == 0)
        {

            fortune = 100f;

        }
        else
        {

            fortune = ((float)win / total) * 100;

        }

        winValueTxt.text = win.ToString();
        loseValueTxt.text = lose.ToString();
        drawValueTxt.text = draw.ToString();
        totalValueWithDrawTxt.text = totalWithDraw.ToString();
        fortuneValueWithDrawTxt.text = String.Format("{0:F1} %", fortune);

    }

    void OpenCards() {

        Transform currentCard = activeCards.transform.GetChild(1);

        for (int i = 1; i < activeCards.transform.childCount; i++)
        {
            currentCard = activeCards.transform.GetChild(i);

            currentCard.GetChild(1).gameObject.SetActive(true);

            if (cardNumber == i) {

                currentCard.GetChild(1).localScale = new Vector3(1.1f, 1.1f, 0);

            }

            currentCard.GetChild(0).gameObject.SetActive(false);
        }

    }

    void ShowCards() {

        GameObject currentCard = cardSlot.transform.GetChild(UnityEngine.Random.Range(0, cardSlot.transform.childCount - 1)).gameObject;

        currentCard.transform.GetChild(0).gameObject.SetActive(false);
        currentCard.transform.GetChild(0).gameObject.GetComponent<Card>().canClick = false;
        currentCard.transform.GetChild(1).gameObject.SetActive(true);
        currentCard.transform.GetChild(1).localScale = new Vector3(1.1f, 1.1f, 0f);
        currentCard.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f, 0f);
        cardBotValue = currentCard.transform.GetChild(0).gameObject.GetComponent<Card>().cardValue;

        currentCard.transform.SetParent(activeCards.transform);
        currentCard.SetActive(true);

        for (int i = 1; i <= 5; i++) {

            currentCard = cardSlot.transform.GetChild(UnityEngine.Random.Range(0, cardSlot.transform.childCount - 1)).gameObject;

            currentCard.transform.SetParent(activeCards.transform);
            currentCard.SetActive(true);
            currentCard.transform.GetChild(0).GetComponent<Card>().cardNumber = i;
            currentCard.transform.GetChild(1).localScale = new Vector3(1f, 1f, 0f);
            currentCard.transform.GetChild(0).localScale = new Vector3(1f, 1f, 0f);

        }

        canResetGame = false;

    }

    public void ResetCards() {

        Transform currentCardTr = activeCards.transform.GetChild(0);
          

        for (int i = 0; i < 6; i++)
        {
            currentCardTr = activeCards.transform.GetChild(activeCards.transform.childCount - 1);

            currentCardTr.GetChild(1).localScale = new Vector3(1f, 1f, 0);
            currentCardTr.GetChild(1).gameObject.SetActive(false);
            currentCardTr.GetChild(0).gameObject.SetActive(true);
            currentCardTr.GetChild(0).gameObject.GetComponent<Card>().canClick = true;
            currentCardTr.GetChild(0).gameObject.GetComponent<Card>().cardNumber = 0;
            currentCardTr.transform.GetChild(1).localScale = new Vector3(1f, 1f, 0f);
            currentCardTr.transform.GetChild(0).localScale = new Vector3(1f, 1f, 0f);
            currentCardTr.gameObject.SetActive(false);
            currentCardTr.SetParent(cardSlot.transform);
        }

        canResetGame = false;
        cardNumber = 0;

        audioManager.StopPlay();

        ShowCards();

    }

 public  void UpdateGameInfo(string key, int cardNumber) {

        this.cardNumber = cardNumber;

        switch (key) {

            case "win":
                PlayerPrefs.SetInt(key, ++win);
                break;
            case "lose":
                PlayerPrefs.SetInt(key, ++lose);
                break;
            case "draw":PlayerPrefs.SetInt(key, ++draw);
                 break;

        }

        audioManager.PlaySound(key);

        OpenCards();

        total = win + lose;
        totalWithDraw = total + draw;

        fortune = ((float)win / total) * 100;

        winValueTxt.text = win.ToString();
        loseValueTxt.text = lose.ToString();
        drawValueTxt.text = draw.ToString();
        totalValueWithDrawTxt.text = totalWithDraw.ToString();
        fortuneValueWithDrawTxt.text = String.Format("{0:F1} %", fortune);

        canResetGame = true;

    }

}
