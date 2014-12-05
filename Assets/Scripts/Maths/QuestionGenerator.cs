using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace QuestionStuff
{

    public class QuestionGenerator
    {
        Operators op = new Operators();
        string question = "";
        string formattedQuestion = "";
        string questionPreFix = "What is ";

        public float CorrectAnswer
        {
            get;
            set;
        }
        public string QuestionBuilder(int amountOfOperators)
        {
            question = "";
            for (int i = 0; i < amountOfOperators; i++)
            {
                string opType = GetOperatorType();
                question += op.Number;
                if (i != amountOfOperators - 1)
                    question += opType;
            }
            formattedQuestion = question.Replace('*', 'x');
            formattedQuestion = formattedQuestion.Replace('/', '÷');
            return questionPreFix + formattedQuestion;
        }

        public List<float> AnswerGenerator(int amountOfAnswers)
        {
            CorrectAnswer = (float)Evaluate(question);
            List<float> answers = new List<float>();

            for (int i = 0; i < amountOfAnswers; i++)
            {
                float a = Random.Range((int)CorrectAnswer - 15, (int)CorrectAnswer + 15);
                answers.Insert(Random.Range(0, answers.Count), a);
            }
            answers.Insert(Random.Range(0, answers.Count), CorrectAnswer);

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
            switch (Random.Range(0, 4))
            {
                case 0:
                    return op.Add;
                case 1:
                    return op.Multiply;
                case 2:
                    return op.Divide;
                case 3:
                    return op.Subtract;
                default:
                    return op.Add;
            }

        }
    }
}

