using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ViewModel
{
    using BaseClass;
    using System.Collections.ObjectModel;

    class QuestionViewModal : ViewModel
    {
        private Model.Question questionInstance;
        public ObservableCollection<Model.Answer> Answers { get; set; }

        public QuestionViewModal(Model.Question question)
        {
            questionInstance = question;
            InitAns();
            QuestionName = question.Text;
        }

        public void InitAns()
        {
            Answers = new ObservableCollection<Model.Answer>();
            foreach (var ans in questionInstance.Answers)
            {
                Answers.Add(ans);
            }
            onPropertyChange(nameof(Answers));
        }

        private string questionName;
        public string QuestionName
        {
            get { return questionName; }
            set
            {
                questionName = value;
                questionInstance.Text = value;
                onPropertyChange(nameof(QuestionName));
            }
        }
    }
}
