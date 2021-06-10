namespace Test.Model
{
    class Answer
    {
        public string Id { get; set; }
        public bool IsCorrect { get;  set; }
        public string Text { get;  set; }

        public Answer(bool _isCorrect, string _text)
        {
            IsCorrect = _isCorrect;
            Text = _text;
            Id = "ans";
        }
    }
}
