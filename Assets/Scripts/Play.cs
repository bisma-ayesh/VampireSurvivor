using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour

{
    private GameStateManager gameStateManager;
    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }

}
