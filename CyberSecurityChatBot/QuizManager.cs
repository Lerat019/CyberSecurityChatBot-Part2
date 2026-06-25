using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; }
        public bool IsTrueFalse { get; set; }
    }

    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentIndex = 0;
        public int Score { get; private set; } = 0;
        public int TotalAnswered { get; private set; } = 0;

        public QuizManager()
        {
            questions = new List<QuizQuestion>()
            {
                new QuizQuestion {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report it as phishing", "Ignore it" },
                    CorrectIndex = 2,
                    Explanation = "Reporting phishing emails helps prevent scams and protects others.",
                    IsTrueFalse = false
                },
                new QuizQuestion {
                    Question = "A strong password should be at least 12 characters long.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 0,
                    Explanation = "Longer passwords are harder to crack. 12+ characters is recommended.",
                    IsTrueFalse = true
                },
                new QuizQuestion {
                    Question = "Which of these is the safest password?",
                    Options = new List<string> { "password123", "John1990", "Tr0ub4dor&3", "abc123" },
                    CorrectIndex = 2,
                    Explanation = "A mix of uppercase, lowercase, numbers and symbols makes a strong password.",
                    IsTrueFalse = false
                },
                new QuizQuestion {
                    Question = "Public Wi-Fi is always safe to use for banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 1,
                    Explanation = "Public Wi-Fi is unsecured. Always use a VPN when doing sensitive activities.",
                    IsTrueFalse = true
                },
                new QuizQuestion {
                    Question = "What does HTTPS mean in a website address?",
                    Options = new List<string> { "The site is fast", "The site is secure", "The site is free", "The site is popular" },
                    CorrectIndex = 1,
                    Explanation = "HTTPS means the connection is encrypted and more secure than HTTP.",
                    IsTrueFalse = false
                },
                new QuizQuestion {
                    Question = "Two-factor authentication adds an extra layer of security.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 0,
                    Explanation = "2FA requires a second verification step, making accounts much harder to hack.",
                    IsTrueFalse = true
                },
                new QuizQuestion {
                    Question = "Which of these is a sign of a phishing email?",
                    Options = new List<string> { "Sender is your known contact", "Email asks for urgent action", "Email has no attachments", "Email comes from a known domain" },
                    CorrectIndex = 1,
                    Explanation = "Phishing emails often create urgency to trick you into acting without thinking.",
                    IsTrueFalse = false
                },
                new QuizQuestion {
                    Question = "You should use the same password for all your accounts.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 1,
                    Explanation = "If one account is hacked, all your accounts become vulnerable.",
                    IsTrueFalse = true
                },
                new QuizQuestion {
                    Question = "What is malware?",
                    Options = new List<string> { "A type of hardware", "Software designed to harm your device", "A secure browser", "A type of firewall" },
                    CorrectIndex = 1,
                    Explanation = "Malware includes viruses, ransomware, and spyware that can damage or steal data.",
                    IsTrueFalse = false
                },
                new QuizQuestion {
                    Question = "Keeping your software updated helps protect against security vulnerabilities.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 0,
                    Explanation = "Updates often include security patches that fix known vulnerabilities.",
                    IsTrueFalse = true
                },
                new QuizQuestion {
                    Question = "What is social engineering in cybersecurity?",
                    Options = new List<string> { "Building secure networks", "Manipulating people to reveal information", "Writing security software", "Encrypting data" },
                    CorrectIndex = 1,
                    Explanation = "Social engineering tricks people rather than hacking systems directly.",
                    IsTrueFalse = false
                }
            };

            // Shuffle questions
            Random rng = new Random();
            for (int i = questions.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                var temp = questions[i];
                questions[i] = questions[j];
                questions[j] = temp;
            }
        }

        public QuizQuestion GetCurrentQuestion()
        {
            if (currentIndex < questions.Count)
                return questions[currentIndex];
            return null;
        }

        public bool SubmitAnswer(int selectedIndex)
        {
            QuizQuestion q = GetCurrentQuestion();
            if (q == null) return false;
            TotalAnswered++;
            bool correct = selectedIndex == q.CorrectIndex;
            if (correct) Score++;
            currentIndex++;
            return correct;
        }

        public bool IsFinished()
        {
            return currentIndex >= questions.Count;
        }

        public void Reset()
        {
            currentIndex = 0;
            Score = 0;
            TotalAnswered = 0;
        }

        public string GetFinalFeedback()
        {
            double percentage = (double)Score / questions.Count * 100;
            if (percentage >= 80)
                return "Great job! You are a cybersecurity pro!";
            else if (percentage >= 50)
                return "Good effort! Keep learning to stay safe online.";
            else
                return "Keep learning to stay safe online! Review the topics and try again.";
        }
    }
}
