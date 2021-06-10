using System;
using System.Linq;

namespace Test.Model
{
    class Question
    {
        public Answer[] Answers = new Answer[4];
        public string Text { get; set; }

        public Question(Answer[] _answers, string _text)
        {
            if (!CheckAnswers(_answers))
            {
                throw new Exception("Złe odpowiedzi.");
            }

            Answers = _answers;
            Text = _text;
        }

        private static Random random = new Random();
        public static string RandomId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool CheckAnswers(Answer[] answers)
        {
            if (answers.Length > 4)
            {
                return false;
            }

            string id = RandomId();
            int correctAnswers = 0;
            foreach (var answer in answers)
            {
                if (answer.IsCorrect)
                {
                    correctAnswers++;
                }
                answer.Id = id;
            }

            if (correctAnswers != 1)
            {
                return false;
            }

            return true;
        }
    }
}
