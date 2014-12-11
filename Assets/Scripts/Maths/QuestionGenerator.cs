using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace QuestionStuff
{

    public class QuestionGenerator
    {
        Operators op = new Operators();
        Dictionary<string, DifficultyModifier> Difficulties;
        DifficultyModifier currentDiff;
        string question = "";
        string formattedQuestion = "";
        string questionPreFix = "What is ";
        List<string> diffNames = new List<string>();
        public QuestionGenerator(string initalDiff)
        {
            Difficulties = new Dictionary<string, DifficultyModifier>();
            DifficultyModifier VeryEasy = new DifficultyModifier("VeryEasy", 2, 1, false);
            DifficultyModifier Easy = new DifficultyModifier("Easy", 3, 1, false);
            DifficultyModifier Medium = new DifficultyModifier("Medium", 4, 1, true);
            DifficultyModifier Hard = new DifficultyModifier("Hard", 4, 2, false);
            DifficultyModifier VeryHard = new DifficultyModifier("VeryHard", 4, 2, true);
            Difficulties.Add(VeryEasy.Name, VeryEasy);
            Difficulties.Add(Easy.Name, Easy);
            Difficulties.Add(Medium.Name, Medium);
            Difficulties.Add(Hard.Name, Hard);
            Difficulties.Add(VeryHard.Name, VeryHard);
            foreach (string diff in Difficulties.Keys)
            {
                diffNames.Add(diff);
            }
            SetCurrentDifficulty = initalDiff;

        }

        #region Difficulties
        public DifficultyModifier CurrentDifficulty
        {
            get { return currentDiff; }
        }
        public string SetCurrentDifficulty
        {
            set
            {
                if (Difficulties.ContainsKey(value))
                {
                    Debug.Log(string.Format("Difficulty changed from : {0} to {1}", CurrentDifficulty.Name, Difficulties[value].Name));
                    currentDiff = Difficulties[value];
                }
            }
        }
        public void IncreaseDifficulty()
        {
            int idx = diffNames.IndexOf(CurrentDifficulty.Name) + 1;
            SetCurrentDifficulty = diffNames[idx < diffNames.Count ? idx : diffNames.Count - 1];
        }
        public void LowerDifficulty()
        {
            int idx = diffNames.IndexOf(CurrentDifficulty.Name) - 1;
            SetCurrentDifficulty = diffNames[idx >= 0 ? idx : 0];
        }
        #endregion

        public float CorrectAnswer
        {
            get;
            set;
        }
        public string QuestionBuilder()
        {
            
            question = "";
            for (int i = 0; i < CurrentDifficulty.AmountOfOperators; i++)
            {
                question += op.Number;
                string opType = GetOperatorType();
                question += opType;
                if (i == CurrentDifficulty.AmountOfOperators - 1)
                    question += op.Number;
            }
            formattedQuestion = question.Replace('*', 'x');
            formattedQuestion = formattedQuestion.Replace('/', '÷');
            return questionPreFix + formattedQuestion;
        }

        public List<float> AnswerGenerator()
        {
            CorrectAnswer = (float)Evaluate(question);
            List<float> answers = new List<float>();

            //Start at 1 because you already have 1 answer -- The correct one
            for (int i = 1; i < CurrentDifficulty.AmountOfAnswers; i++)
            {
                float a = Random.Range((int)CorrectAnswer - 15, (int)CorrectAnswer + 15);
                answers.Insert(Random.Range(0, answers.Count), a);
            }
            answers.Insert(Random.Range(0, answers.Count+1), CorrectAnswer);

            return answers;

        }
        public static double Evaluate(string expression)
        {
            return (double)new System.Xml.XPath.XPathDocument
            (new System.IO.StringReader("<r/>")).CreateNavigator().Evaluate
            (string.Format("number({0})", new
              System.Text.RegularExpressions.Regex(@"([\+\-\*])").Replace(expression, " ${1} ").Replace("/", " div ").Replace("%", " mod ")));
        }

        public string GetOperatorType()
        {
            switch (Random.Range(0, CurrentDifficulty.IncludeTimesAndDivide ? 4 : 2 ))
            {
                case 0:
                    return op.Add;
                case 1:
                    return op.Subtract;
                case 2:
                    return op.Divide;
                case 3:
                    return op.Multiply;
                default:
                    return op.Add;
            }

        }
        
    }
    public struct DifficultyModifier
    {
        public string Name;
        public int AmountOfAnswers;
        public int AmountOfOperators;
        public bool IncludeTimesAndDivide;

        public DifficultyModifier(string _name, int _amountofanswers, int _amoutofops, bool _includeTimesAndDivide)
        {
            Name = _name;
            AmountOfAnswers = _amountofanswers;
            AmountOfOperators = _amoutofops;
            IncludeTimesAndDivide = _includeTimesAndDivide;
        }
    }
}

