using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using QuestionStuff;

public class QuestionRenderer : MonoBehaviour {
    QuestionGenerator q = new QuestionGenerator("Easy");
    string questionString;
    List<float> answers = new List<float>();
    public GameObject SpawnObject;
    public float percentAccrossScreen;
    public TeamController myTeam;

    void Start()
    {
        myTeam = this.GetComponent<TeamController>();
        GenerateNewQuestion();
    }
    public int EnemiesKilled
    {
        get;
        set;
    }
    public void GenerateNewQuestion()
    {

        questionString = q.QuestionBuilder();
        q.LowerDifficulty();
        answers = q.AnswerGenerator();
    }
    void OnGUI()
    {
        if (answers.Count > 0)
        {
            float width = Screen.width * 0.4f;
            GUI.BeginGroup(new Rect(0 + (Screen.width * percentAccrossScreen), 0, width, Screen.height * 0.7f));

            GUI.Box(new Rect(0, 0, width, Screen.height * 0.4f), questionString);
            GUI.Label(new Rect(30, 25, width, Screen.height * 0.4f), "Current Gold : " + myTeam.Gold);
            for (int i = 0; i < answers.Count; i++)
            {
                if (GUI.Button(new Rect((int)(width * 0.1f) + i * ((width * 0.8f) / answers.Count), Screen.height * 0.1f, (width * 0.8f) / answers.Count, Screen.height * 0.15f), answers[i].ToString()))
                {

                    if (answers[i] == q.CorrectAnswer)
                    {
                        CorrectAnswer();
                    }
                    else
                    {
                        MinionController[] minions = GameObject.FindObjectsOfType<MinionController>();
                        foreach (MinionController m in minions)
                        {
                            if (m.tag == this.tag)
                            {
                                Destroy(m.gameObject);
                                break;
                            }
                        }
                    }
                }
            }
            if (myTeam.Gold < 10)
                GUI.enabled = false;
            //PowerUps
            if (GUI.Button(new Rect((int)(width * 0.1f) + 0 * ((width * .8f)), Screen.height * 0.25f, (width * 0.4f), Screen.height * 0.1f), "Spawn 5 Allies"))
            {
                    StartCoroutine(SpawnGuys(5));
                    myTeam.Gold -= 10;
            }
            GUI.enabled = true;
            GUI.EndGroup();
        }
    }

    public IEnumerator SpawnGuys(int amountOfGuys)
    {
        for (int i = 0; i < amountOfGuys; i++)
        {
            yield return new WaitForSeconds(.25f);
            Instantiate(SpawnObject);
        }
    }
    private void CorrectAnswer()
    {
        myTeam.Gold++;
        answers.Clear();
        GenerateNewQuestion();
        Instantiate(SpawnObject);
    }
}
