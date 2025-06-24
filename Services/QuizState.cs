using ChatBotCyber.Models;

namespace ChatBotCyber.Services
{
    public class QuizState
    {
        private int _currentQuestionIndex = 0;
        private int _score = 0;
        private readonly QuizManager _manager;

        public bool IsActive => _currentQuestionIndex < _manager.Questions.Count;
        public bool IsComplete => !IsActive;
        public int Score => _score;

        public QuizState(QuizManager manager)
        {
            _manager = manager;
        }

        public string GetNextQuestion()
        {
            if (!IsActive) return "Quiz completed!";

            var question = _manager.Questions[_currentQuestionIndex];
            string options = "\n" + string.Join("\n",
                question.Options.Select((opt, idx) => $"{(char)('A' + idx)}) {opt}"));

            return $"Question {_currentQuestionIndex + 1}/{_manager.Questions.Count}:\n" +
                   $"{question.Question}{options}";
        }

        public string AnswerQuestion(string answer)
        {
            if (!IsActive) return "Quiz has already ended.";

            var question = _manager.Questions[_currentQuestionIndex];
            bool isCorrect = question.CheckAnswer(answer);

            _currentQuestionIndex++;
            if (isCorrect) _score++;

            string result = $"{(isCorrect ? "✓ Correct" : "✗ Incorrect")}: {question.Explanation}";

            if (IsActive)
            {
                return $"{result}\n\n{GetNextQuestion()}";
            }

            return $"{result}\n\nQuiz completed! Final score: {_score}/{_manager.Questions.Count}";
        }
    }
}