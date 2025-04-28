using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestions;

    public TMP_Text QuestionTxt;
    public TMP_Text ScoreTxt;

    int totalQuestions = 0;
    public int score;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    public bool canAnswer = true; // To control one click only

    private void Start()
    {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        generateQuestions();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    public IEnumerator CorrectAnswer()
    {
        score += 1;
        yield return new WaitForSeconds(1f); // Wait 1 second
        QnA.RemoveAt(currentQuestions);
        generateQuestions();
    }

    public IEnumerator WrongAnswer()
    {
        // Highlight the correct answer
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].GetComponent<AnswerScript>().isCorrect)
            {
                options[i].GetComponent<Image>().color = Color.green;
            }
        }

        yield return new WaitForSeconds(1f); // Wait 1 second
        QnA.RemoveAt(currentQuestions);
        generateQuestions();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestions].Answers[i];

            if (QnA[currentQuestions].CorrectAnswers == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestions()
    {
        if (QnA.Count > 0)
        {
            ResetOptions(); // Reset colors and buttons

            currentQuestions = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestions].Question;
            SetAnswers();

            canAnswer = true; // Allow clicking again
        }
        else
        {
            Debug.Log("Quiz Finished!");
            GameOver();
        }
    }

    void ResetOptions()
    {
        foreach (GameObject option in options)
        {
            option.GetComponent<AnswerScript>().ResetColor();
        }
    }
}
