using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour {

    public Text descriptionText;
    public string sceneToLoad = "Ending";

    // Use this for initialization
    void Start () {
        //descriptionText.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void NextPage()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
