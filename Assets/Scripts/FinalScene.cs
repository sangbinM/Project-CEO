using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour {

    public Text[] finalText = new Text[4];
    public string sceneToLoad = "Ending";
    public Image gManager;

    // Use this for initialization
    void Start () {
        gManager = GameObject.FindGameObjectWithTag("G_manager").GetComponent<Image>();
        gManager.enabled = false;
        for (int i = 1; i < 4; i++)
            finalText[i].enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void NextPage()
    {
        if (finalText[0].enabled == true)
        {
            finalText[0].enabled = false;
            finalText[1].enabled = true;
        }
        else if (finalText[1].enabled == true)
        {
            gManager.enabled = true;
            finalText[1].enabled = false;
            finalText[2].enabled = true;
        }
        else if (finalText[2].enabled == true)
        {
            finalText[2].enabled = false;
            finalText[3].enabled = true;
        }
        else if (finalText[3].enabled == true)
        {
            SceneManager.LoadScene(sceneToLoad);
            gManager.enabled = false;
            finalText[3].enabled = false;
            finalText[0].enabled = true;
        }
    }
}
