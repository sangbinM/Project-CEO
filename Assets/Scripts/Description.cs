using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Description : MonoBehaviour {

    public Text[] descriptionText = new Text[3];
    public string sceneToLoad = "Map";

    // Use this for initialization
    void Start () {
        descriptionText[1].enabled = false;
        descriptionText[2].enabled = false;
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
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
