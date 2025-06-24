namespace ChatBotCyber.Models
{
    public class QuizQuestion
    {
        public string Question { get; }
        public string[] Options { get; }
        public int CorrectOptionIndex { get; }
        public string Explanation { get; }

        public QuizQuestion(string question, string[] options, int correctOptionIndex, string explanation)
        {
            Question = question;
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
            Explanation = explanation;
        }

        public bool CheckAnswer(string userAnswer)
        {
            // Check if user selected the correct option index
            if (int.TryParse(userAnswer, out int selectedIndex))
            {
                return selectedIndex == CorrectOptionIndex;
            }

            // Or if they entered the option text
            if (userAnswer.Length == 1)
            {
                char optionChar = char.ToUpper(userAnswer[0]);
                int index = optionChar - 'A';
                return index == CorrectOptionIndex;
            }

            // Or if they entered the full option text
            return Options[CorrectOptionIndex].Equals(userAnswer, StringComparison.OrdinalIgnoreCase);
        }
    }
}