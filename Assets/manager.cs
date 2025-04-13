using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace DyslexiaGames
{
    public class ConsonantIslandScene : MonoBehaviour
    {
        [Header("UI Elements")]
        public Text consonantText;
        public Button[] choiceButtons;
        public Text scoreText;
        public GameObject feedbackPanel;
        public Text feedbackText;
        public Button nextRoundButton;

        [Header("Game Settings")]
        public List<string> consonants = new List<string>() { "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z" };
        public int numberOfChoices = 3;
        public int roundsToPlay = 10;

        private string currentConsonant;
        private int currentRound = 0;
        private int score = 0;
        private List<string> currentChoices = new List<string>();


        void Start()
        {
            //Initialization
            feedbackPanel.SetActive(false);
            nextRoundButton.onClick.AddListener(StartNewRound); // Make sure the Button has a listener attached
            StartNewRound();
            UpdateScoreUI();
        }


        void StartNewRound()
        {
            feedbackPanel.SetActive(false); //Hide Feedback Panel

            if (currentRound >= roundsToPlay)
            {
                GameOver();
                return;
            }

            currentRound++;
            GenerateQuestion();
        }

        void GenerateQuestion()
        {
            //Pick a random consonant
            currentConsonant = consonants[Random.Range(0, consonants.Count)];
            consonantText.text = currentConsonant;

            //Generate choices, including the correct answer
            currentChoices.Clear();
            currentChoices.Add(currentConsonant);

            while (currentChoices.Count < numberOfChoices)
            {
                string randomConsonant = consonants[Random.Range(0, consonants.Count)];
                if (!currentChoices.Contains(randomConsonant))
                {
                    currentChoices.Add(randomConsonant);
                }
            }

            //Shuffle the choices
            Shuffle(currentChoices);

            //Assign choices to buttons
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (i < currentChoices.Count)
                {
                    choiceButtons[i].gameObject.SetActive(true);
                    string choice = currentChoices[i];
                    choiceButtons[i].GetComponentInChildren<Text>().text = choice;  // Ensure the button text is updated
                    choiceButtons[i].onClick.RemoveAllListeners(); // Important: Remove existing listeners before adding a new one.
                    choiceButtons[i].onClick.AddListener(() => CheckAnswer(choice));
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(false); //Deactivate Buttons when not in use
                }
            }
        }

        void CheckAnswer(string selectedConsonant)
        {
            if (selectedConsonant == currentConsonant)
            {
                score++;
                ShowFeedback(true);
            }
            else
            {
                ShowFeedback(false);
            }
            UpdateScoreUI();


        }


        void ShowFeedback(bool correct)
        {
            feedbackPanel.SetActive(true);

            if (correct)
            {
                feedbackText.text = "Correct!";
                feedbackText.color = Color.green;
            }
            else
            {
                feedbackText.text = "Incorrect! The correct answer was " + currentConsonant;
                feedbackText.color = Color.red;
            }

            //After showing feedback, start the next round. Can be implemented by using a button or a coroutine with a delay.
            //For now, let's use a button to give the player control on starting the next round.

        }

        void UpdateScoreUI()
        {
            scoreText.text = "Score: " + score;
        }

        void GameOver()
        {
            Debug.Log("Game Over! Score: " + score);

            // Display game over screen or transition to another scene
            // For example, you could load a "GameOverScene" with the final score
            // SceneManager.LoadScene("GameOverScene");

            //For testing:
            feedbackPanel.SetActive(true);
            feedbackText.text = "Game Over! Score: " + score;
            feedbackText.color = Color.blue;

            // Optionally, disable the buttons
            foreach (var button in choiceButtons)
            {
                button.interactable = false;
            }
            nextRoundButton.gameObject.SetActive(false);


        }

        // Fisher-Yates shuffle algorithm
        void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}