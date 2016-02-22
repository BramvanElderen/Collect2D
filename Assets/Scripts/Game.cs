using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    static Game Instance;
    
    public Text countText;
    public Text winText;
    public GameObject PlayerObject;
    public PlayerController PlayerScript;
    public int count;

    void Start()
    {
        if (Instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        winText.text = "";
        UpdateCounter();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            SceneManager.LoadScene("main");
        }
        if (Input.GetKeyUp(KeyCode.F2))
        {
            SceneManager.LoadScene("Level2");
        }
        if (Input.GetKeyUp(KeyCode.F3))
        {
            SceneManager.LoadScene("Level3");
        }

        UpdateCounter();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("main");
    }

    void UpdateCounter()
    {
        if (PlayerObject == null)
        {
            PlayerObject = GameObject.Find("UFO");
            PlayerScript = (PlayerController)PlayerObject.GetComponent(typeof(PlayerController));
            winText = (Text)GameObject.Find("WinText").GetComponent(typeof(Text));
            countText = (Text)GameObject.Find("CountText").GetComponent(typeof(Text));
        }

        if (!PlayerObject.activeSelf)
        {
            winText.text = "Lose";
        } else
        {
            count = PlayerScript.getCount();

            countText.text = "Count: " + count.ToString();
            if (count >= 12)
            {
                winText.text = "Win";
            }
        }        
    }
}
