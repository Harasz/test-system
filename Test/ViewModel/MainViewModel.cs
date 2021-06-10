using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Test.ViewModel
{
    using BaseClass;
    using System.Diagnostics;
    using System.Windows;
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows.Forms;

    class MainViewModel : ViewModel
    {
        private Test TestInstance;
        public ObservableCollection<QuestionViewModal> Questions { get; set; }

        public MainViewModel()
        {
            Questions = new ObservableCollection<QuestionViewModal>();
            InitTest();
            ShowStart();
        }

        private Visibility startVisibility;
        public Visibility StartVisibility
        {
            get { return startVisibility; }
            private set
            {
                startVisibility = value;
                onPropertyChange(nameof(StartVisibility));
            }
        }

        private Visibility controlTestVisibility;
        public Visibility ControlTestVisibility
        {
            get { return controlTestVisibility; }
            private set
            {
                controlTestVisibility = value;
                onPropertyChange(nameof(ControlTestVisibility));
            }
        }

        private string testName;
        public string TestName
        {
            get { return testName; }
            set
            {
                testName = value;
                TestInstance.SetName(value);
                onPropertyChange(nameof(TestName));
            }
        }

        private ICommand openNew;
        public ICommand OpenNew
        {
            get
            {
                return openNew ?? (openNew = new RelayCommand(OnOpenNew, _ => true));
            }
        }

        private ICommand openFile;
        public ICommand OpenFile
        {
            get
            {
                return openFile ?? (openFile = new RelayCommand(OnOpenFile, _ => true));
            }
        }

        private ICommand addQuestion;
        public ICommand AddQuestion
        {
            get
            {
                return addQuestion ?? (addQuestion = new RelayCommand(AddNewQuestion, _ => true));
            }
        }
        

        private ICommand saveToFile;
        public ICommand SaveToFile
        {
            get
            {
                return saveToFile ?? (saveToFile = new RelayCommand(SaveToFileTest, _ => true));
            }
        }
        

        private void AddNewQuestion(object _)
        {
            var answers = new Answer[] { new Answer(false, "Zła odpowiedź"), new Answer(false, "Zła odpowiedź"), new Answer(false, "Zła odpowiedź"), new Answer(true, "Dobra odpowiedź") };
            var question = new Question(answers, "Treść pytania");
            var questionVM = new QuestionViewModal(question);
            TestInstance.AddQuestion(question);
            Questions.Add(questionVM);
        }

        private void SaveToFileTest(object _)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Text file (*.txt)|*.txt"
            };
            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                TestInstance.SaveToFile(saveFileDialog.FileName);
            }
        }

        private void InitTest()
        {
            var answers = new Answer[] { new Answer(false, "Zła odpowiedź"), new Answer(false, "Zła odpowiedź"), new Answer(false, "Zła odpowiedź"), new Answer(true, "Dobra odpowiedź") };
            TestInstance = new Test("Nazwa testu");
            var question = new Question(answers, "Treść pytania");
            var questionVM = new QuestionViewModal(question);
            Questions.Add(questionVM);
            TestInstance.AddQuestion(question);
            TestName = TestInstance.Name;
        }

        private void OnOpenNew(object _)
        {
            ShowControlTest();
        }

        private void OnOpenFile(object _)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "Test",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                TestInstance = Test.LoadFromFile(filename);
                TestName = TestInstance.Name;

                Questions.Remove(Questions[0]);

                foreach (var quest in TestInstance.Questions)
                {
                    Questions.Add(new QuestionViewModal(quest));
                }
                
            } else
            {
                return;
            }

            ShowControlTest();
        }

        private void ShowStart()
        {
            StartVisibility = Visibility.Visible;
            ControlTestVisibility = Visibility.Hidden;
        }

        private void ShowControlTest()
        {
            StartVisibility = Visibility.Hidden;
            ControlTestVisibility = Visibility.Visible;
        }
    }
}
