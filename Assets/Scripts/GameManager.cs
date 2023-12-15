using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;
    private Canvas canvas;
    private Button startButton;

    public AudioSource audiosource;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestions = 2f;

    public int answered = 0;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = UnityEngine.Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        factText.text = currentQuestion.fact;
        Instantiate(currentQuestion.gameObject);
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        if(unansweredQuestions.Count == 0)
        {
            Debug.Log("answered: " + answered + "length: " + questions.Length);
            SceneManager.LoadScene("ConfettiAnimation");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            audiosource.clip = Resources.Load<AudioClip>("Audio/hooray");
            audiosource.Play();
            answered++;
            Debug.Log("CORRECT");
            StartCoroutine(TransitionToNextQuestion());
        }
        else
        {
            audiosource.clip = Resources.Load<AudioClip>("Audio/tryagain");
            audiosource.Play();
            Debug.Log("WRONG");
        }
    }

    public void UserSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
            audiosource.clip = Resources.Load<AudioClip>("Audio/hooray");
            audiosource.Play();
            answered++;
            Debug.Log("CORRECT");
            StartCoroutine(TransitionToNextQuestion());
        }
        else
        {
            audiosource.clip = Resources.Load<AudioClip>("Audio/tryagain");
            audiosource.Play();
            Debug.Log("WRONG");
        }
    }
}
