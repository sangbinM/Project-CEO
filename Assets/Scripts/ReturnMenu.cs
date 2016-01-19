using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour {


    public string sceneToLoad;
    
    public void goToMenu() {


        SceneManager.LoadScene(sceneToLoad);
        
    }

}
