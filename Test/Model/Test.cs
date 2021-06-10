using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    class Test
    {
        public string Name { get; private set; }
        public List<Question> Questions { get; private set; }

        public Test(string _name)
        {
            Name = _name;
            Questions = new List<Question>();
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public static Test LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Brak testu pod wskazanym adresem.");
            }

            string[] rawTest = File.ReadAllLines(path);

            if (rawTest.Length < 1 || (rawTest.Length - 1) % 6 != 0)
            {
                throw new Exception("Zły format testu");
            }

            var TestInstance = new Test(rawTest[0]);

            if (rawTest.Length < 7)
            {
                return TestInstance;
            }

            var questions = new List<Question>();
            for (ushort i = 1; i < rawTest.Length - 1; i += 6)
            {
                var answers = new Answer[4];

                for (ushort j = 0; j < 4; j++)
                {
                    var answerLine = rawTest[i + j + 1];

                    if (answerLine[1] != '|')
                    {
                        throw new Exception("Zły format testu");
                    }

                    var isCorrect = answerLine[0] == '1';
                    var name = answerLine.Substring(2);

                    answers[j] = new Answer(isCorrect, name);
                }

                var question = new Question(answers, rawTest[i]);
                TestInstance.AddQuestion(question);
            }

            return TestInstance;
        }

        public void SaveToFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create("test.txt");
            }

            

            string[] lines = new string[1 + Questions.Count * 6];

            lines[0] = Name;

            int idx = 1;
            foreach(var question in Questions)
            {
                lines[idx] = question.Text;
                idx++;
                foreach (var ans in question.Answers)
                {
                    int isCorrect = ans.IsCorrect ? 1 : 0;
                    lines[idx] = $"{isCorrect}|{ans.Text}";
                    idx++;
                }
                lines[idx] = "**********";
                idx++;
            }

            File.WriteAllLines(path, lines);
        }
    }
}
