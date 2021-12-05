using UnityEngine;

public class NewGame : MonoBehaviour
{

   [SerializeField] GameManager gameManager;
    [SerializeField] AudioManager audioManager;

    public void OnClick() {

        audioManager.PlaySound("draw");

        if (GameManager.canResetGame == false) { return; }

        gameManager.ResetCards();

    }


}
