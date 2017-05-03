using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public Text numberWavesSurvivedText;

    private void OnEnable()
    {
        //numberWavesSurvived.text = _insertWaveNumberHere_
        numberWavesSurvivedText.text = NexusStats.nexusHealth.ToString(); //Example, except we should change the variable to the wave number
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

}
