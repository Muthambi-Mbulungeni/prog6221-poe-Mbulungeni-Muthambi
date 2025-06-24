using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotCyber.Services
{
    public class TopicSearch
    {
        private readonly Dictionary<string, List<string>> topicResponses = new()
        {
            { "phishing", new List<string> {
                "Be cautious of emails asking for personal information.",
                "Avoid clicking on suspicious links in emails.",
                "Scammers often disguise themselves as trusted organisations.",
                "Look for spelling mistakes in email addresses and domains.",
                "Always verify unexpected messages from banks or authorities.",
                "Phishing is a social engineering attack where attackers trick individuals into providing sensitive information."
            }},
            { "password", new List<string> {
                "Use a mix of letters, numbers, and symbols in your passwords.",
                "Avoid using personal info like birthdates in passwords.",
                "Use a password manager to store your credentials securely.",
                "Change your passwords regularly and never reuse them."
            }},
            { "malware", new List<string> {
                "Malware is software intentionally designed to cause damage to computers.",
                "Install antivirus and keep it updated.",
                "Avoid downloading files from untrusted sources.",
                "Keep your operating system and software patched."
            }},
            { "ransomware", new List<string> {
                "Backup your files regularly to external devices or cloud storage.",
                "Don't open suspicious attachments or click unknown links.",
                "Never pay the ransom - it doesn't guarantee access and encourages attacks."
            }},
            { "firewall", new List<string> {
                "A firewall monitors and controls incoming and outgoing network traffic.",
                "Use both hardware and software firewalls for better protection."
            }},
            { "vpn", new List<string> {
                "A VPN extends a private network across a public network for secure data transmission.",
                "Use VPNs especially when traveling or using public Wi-Fi."
            }},
            { "cybersecurity", new List<string> {
                "Cybersecurity is the practice of protecting systems from digital attacks.",
                "Always keep your software updated to protect against vulnerabilities."
            }}
        };

        private readonly string[] sentimentWords = { "worried", "confused", "frustrated", "anxious", "nervous" };

        public List<string> GetAllTopics() => topicResponses.Keys.ToList();

        public string SearchTopic(string input, string userName, string userInterest)
        {
            if (string.IsNullOrEmpty(input))
                return "I'm not sure I understand. Can you try rephrasing or asking about another topic?";

            string lowered = input.ToLower();

            // Check for sentiment
            foreach (var sentiment in sentimentWords)
            {
                if (lowered.Contains(sentiment))
                {
                    return "It's okay to feel that way — cybersecurity can be overwhelming. Let's break it down together.";
                }
            }

            // Search for topic
            foreach (var topic in topicResponses)
            {
                if (lowered.Contains(topic.Key))
                {
                    var random = new Random();
                    string response = topic.Value[random.Next(topic.Value.Count)];

                    // Add personalization if it's the user's favorite topic
                    if (topic.Key.Equals(userInterest, StringComparison.OrdinalIgnoreCase))
                    {
                        string[] personalIntroVariants = {
                            "Here's a tip that might interest you:",
                            "You might find this useful:",
                            "Since you're into this topic, try this one:",
                            "A quick suggestion for you:",
                            "Check this out:"
                        };
                        response = personalIntroVariants[random.Next(personalIntroVariants.Length)] + " " + response;
                    }

                    return response;
                }
            }

            return "I'm not sure I understand. Can you try rephrasing or asking about another topic?";
        }
    }
}