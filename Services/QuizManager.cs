using ChatBotCyber.Models;
using System.Collections.Generic;

namespace ChatBotCyber.Services
{
    public class QuizManager
    {
        public List<QuizQuestion> Questions { get; } = new List<QuizQuestion>
        {
            new QuizQuestion(
                "What should you do if you receive an email asking for your password?",
                new[]
                {
                    "Reply with your password",
                    "Delete the email",
                    "Report the email as phishing",
                    "Ignore it"
                },
                2,
                "Correct! Reporting phishing emails helps prevent scams."
            ),
            new QuizQuestion(
                "What's the most secure way to store your passwords?",
                new[]
                {
                    "Write them in a notebook",
                    "Use the same password everywhere",
                    "Use a password manager",
                    "Save them in your browser"
                },
                2,
                "Yes! Password managers securely store and generate strong passwords."
            ),
            new QuizQuestion(
                "What does HTTPS indicate in a website URL?",
                new[]
                {
                    "The site is popular",
                    "The site has a secure connection",
                    "The site is government-owned",
                    "The site is free to use"
                },
                1,
                "Correct! HTTPS indicates an encrypted, secure connection."
            ),
            new QuizQuestion(
                "What is two-factor authentication (2FA)?",
                new[]
                {
                    "Using two passwords",
                    "Verifying identity with two methods",
                    "Logging in from two devices",
                    "Having two security questions"
                },
                1,
                "Exactly! 2FA adds an extra layer of security beyond just a password."
            ),
            new QuizQuestion(
                "What should you do before connecting to public Wi-Fi?",
                new[]
                {
                    "Disable your firewall",
                    "Enable file sharing",
                    "Use a VPN",
                    "Share your login credentials"
                },
                2,
                "Correct! A VPN encrypts your connection on public networks."
            ),
            new QuizQuestion(
                "How often should you update your software?",
                new[]
                {
                    "Never",
                    "Only when it stops working",
                    "Once a year",
                    "As soon as updates are available"
                },
                3,
                "Yes! Regular updates patch security vulnerabilities."
            ),
            new QuizQuestion(
                "What is phishing?",
                new[]
                {
                    "A fishing technique",
                    "A type of malware",
                    "A social engineering attack",
                    "A password cracking method"
                },
                2,
                "Correct! Phishing tricks users into revealing sensitive information."
            ),
            new QuizQuestion(
                "Why should you avoid using public computers for sensitive activities?",
                new[]
                {
                    "They're too slow",
                    "They might have keyloggers",
                    "They lack internet access",
                    "They're too expensive"
                },
                1,
                "Exactly! Keyloggers can record your keystrokes on public computers."
            ),
            new QuizQuestion(
                "What should you do if you suspect a data breach?",
                new[]
                {
                    "Ignore it",
                    "Change your passwords immediately",
                    "Share the news on social media",
                    "Continue using the same accounts"
                },
                1,
                "Correct! Changing passwords quickly limits potential damage."
            ),
            new QuizQuestion(
                "What's the best way to create a strong password?",
                new[]
                {
                    "Use your pet's name",
                    "Use a long, random mix of characters",
                    "Use your birthdate",
                    "Use 'password123'"
                },
                1,
                "Yes! Long, random passwords are hardest to crack."
            )
        };
    }
}