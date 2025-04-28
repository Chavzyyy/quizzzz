using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    private Color startColor;
    private Button button;

    private void Start()
    {
        startColor = GetComponent<Image>().color;
        button = GetComponent<Button>();
    }

    public void ResetColor()
    {
        GetComponent<Image>().color = startColor;
        button.interactable = true;
    }

    public void Answer()
    {
        if (!quizManager.canAnswer) return; // Only allow one click

        quizManager.canAnswer = false; // Lock further clicks

        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            Debug.Log("Correct Answer");
            quizManager.StartCoroutine(quizManager.CorrectAnswer());
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            Debug.Log("Wrong Answer");
            quizManager.StartCoroutine(quizManager.WrongAnswer());
        }
    }
}
