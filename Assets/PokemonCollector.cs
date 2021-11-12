using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//using CodeMonkey.Utils;


public class PokemonCollector : MonoBehaviour
{
    public delegate void UpdateScore(int score);
    public static event UpdateScore OnUpdate;

    public GameObject[] pokemons;
    int size = 10;
    int index;
    public GameObject flash;
    public GameObject InputWindow;
    public GameObject[] questions;
    string[] correctAnswer = { "T", "100", "0x62fe14", "Karnataka", "LMN", "0 1 2 3", "37651", "6", "2n+8", "21" };
    public string answer;
    public InputField inputField;
    public Text textDisplay;
    int index1;
    public GameObject[] buttons;
    int pokeCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        buttons[1].SetActive(false);
        InputWindow.SetActive(false);
        flash.SetActive(false);
        foreach (var q in questions)
        {
            q.SetActive(false);
        }
        foreach (var p in pokemons)
        {
            p.SetActive(false);
        }
        //PlayerPrefs.SetInt("Number", 0);
        index = PlayerPrefs.GetInt("Number", 0);
        Debug.Log(index);
        pokemons[index].SetActive(true);
        PlayerPrefs.SetInt("Number", index + 1);
        if(index+1 == 10) {
            PlayerPrefs.SetInt("Lives", 0);
            SceneManager.LoadScene("Bye Bye");
        }
    }


    public void onCatch()
    {
        //pop up ques
        InputWindow.SetActive(true);
        buttons[0].SetActive(false);
        //flash
        flash.SetActive(true);
        pokemons[index].SetActive(false);
        //catch button off
        //PlayerPrefs.SetInt("Question", 0);
        index1 = PlayerPrefs.GetInt("Question", 0);
        questions[index1].SetActive(true);
        PlayerPrefs.SetInt("Question", index1 + 1);      
        if(index1+1 == 13)
        {
            PlayerPrefs.SetInt("Question", 0);
        }
    }

    public void onContinue()
    {
        SceneManager.LoadScene("Main");
    }

    [System.Obsolete]
    public void onSubmit()
    {
        answer = inputField.text;
        if (answer == correctAnswer[index1])
        {
            questions[index1].SetActive(false);
            textDisplay.text = "Your answer is correct! You got the pokémon.";
            
            buttons[1].SetActive(true);
            Application.ExternalEval("window.open('https://www.hackerrank.com/')");
            pokeCount++;
            OnUpdate(pokeCount);
            //Redirect to hackerrank

        }
        else
        {
            //Wrong answer message display
            questions[index1].SetActive(false);
            textDisplay.text = "Your answer is incorrect! You couldn't catch the pokémon.";
            buttons[1].SetActive(true);
            //SceneManager.LoadScene("Main");
            //Redirect to Main scene
        }
    }
    

}
