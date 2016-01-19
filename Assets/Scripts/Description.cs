using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Description : MonoBehaviour {

    public Text[] descriptionText = new Text[5];
    public string sceneToLoad = "Map";

    // Use this for initialization
    void Start () {
        for (int i = 1; i < 5; i++)
            descriptionText[i].enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void NextPage()
    {
        if (descriptionText[0].enabled == true)
        {
            descriptionText[0].enabled = false;
            descriptionText[1].enabled = true;
        }
        else if (descriptionText[1].enabled == true)
        {
            descriptionText[1].enabled = false;
            descriptionText[2].enabled = true;
        }
        else if (descriptionText[2].enabled == true)
        {
            descriptionText[2].enabled = false;
            descriptionText[3].enabled = true;
        }
        else if (descriptionText[3].enabled == true)
        {
            descriptionText[3].enabled = false;
            descriptionText[4].enabled = true;
        }
        else if (descriptionText[4].enabled == true)
        {
            SceneManager.LoadScene(sceneToLoad);
            descriptionText[4].enabled = false;
            descriptionText[0].enabled = false;
        }
    }
}
