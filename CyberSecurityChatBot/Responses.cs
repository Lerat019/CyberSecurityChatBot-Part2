using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{
    public class Responses
    {
        private static Random random = new Random();

        private static Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string>
            {
                "Great question! Use strong passwords with uppercase letters, numbers, and symbols.Never reuse passwords across accounts.",
                "A strong password should be at least 12 characters long. Consider using a password manager to keep track of them safely.",
                "Avoid using personal details like your name or birthday in passwords. Use a mix of characters and change them regularly."
            }
            },
            { "phishing", new List<string>
            {
                "Be careful! Never click suspicious links or download attachments from unknown sources. Always verify the sender's email address.",
                "Phishing emails often create urgency to trick you. If something feels off, do not click anything and report the email.",
                "Legitimate organsisations will never ask for sensitive information like your passwords via email. Always go directly to the official website instead."
            }
            },
            { "privacy", new List<string>
            {
                "Protect your privacy by limiting what you share online. Review app permissions and turn of anything unnecessary.",
                "Use a virtual private network (VPN) on public Wi-Fi to keep your data private. Regularly check your privacy setting on social media.",
                "Be cautious about sharing your ID number, address, or banking details online. Once shared, it is hard to take back."
            }
            },
            { "scam", new List<string>
            {
                "If something sounds too good to be true, it probably is. Never send money or personal information to unverified sources.",
                "Scammers often pretend to be trusted organisations. Contact the organisation directly using official contact details to verify.",
                "Common scams include fake job offers,lottery wins, and urgent bank alerts. Always verify before you act."
            }
            },
            { "malware", new List<string>
            {
                "Never download softare from unknown websites. Keep your antivirus updated and run regular scans.",
                "Malware can steal yiur data or damage your device.Avoid clicking on pop-up ads and only install trusted applications.",
                "Regularly back up your important files in case of a malware attack. Having backups can help you recover your information."
            }
            },
            { "safe browsing", new List<string>
            {
                "Always check that a website uses HTTPS before entering personal information. Avoid suspicious or unfamiliar websites.",
                "Clear your browser cache and cookies regularly. Be cautious when using public Wi-Fi networks.",
                "Use a reputable browser with built-in security features. Avoid downloading files from untusted websites."
            }
            }

    };

        public static string GetRandomResponse(string topic)
        {
            if (topicResponses.ContainsKey(topic))
            {
                List<string> responses = topicResponses[topic];
                return responses[random.Next(responses.Count)];
            }
            return null;
        }

        public static string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared"))
                return "worried";
            if (input.Contains("frustrated") || input.Contains("angry"))
                return "frustrated";
            if (input.Contains("curious") || input.Contains("interested"))
                return "curious";
            if (input.Contains("confused") || input.Contains("don't understand") || input.Contains("unsure"))
                return "confused";
            return "neutral";
        }

        public static string GetSentimentResponse(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    return "I understand your concern, and it is completely valid. Let me help ease your worries.";
                case "frustrated":
                    return "I am sorry you are feeling frustrated. Let me try to help make this clearer.";
                case "curious":
                    return "It's great to see your curiosity! What would you like to know?";
                case "confused":
                    return "No worries at all, let me explain this as clear as possible.";
                default:
                    return "";


            }
        }

        public static string GetResponse(string input, ChatMemory memory)
        {
            string sentiment = DetectSentiment(input);
            string sentimentOpening = GetSentimentResponse(sentiment);

            if (input.Contains("tell me more") || input.Contains("explain more") || input.Contains("more information"))
            {
                if (!string.IsNullOrEmpty(memory.LastTopic))
                    return sentimentOpening + GetRandomResponse(memory.LastTopic);
                else
                    return "Could you let me know which topic you would more information on?";
            }

            if(input.Contains("give me another tip") || input.Contains("another tip") || input.Contains("tell me more"))
            {
                if (!string.IsNullOrEmpty(memory.LastTopic))
                    return sentimentOpening + GetRandomResponse(memory.LastTopic);
                else
                    return "Sure! Which topic would you like another tip on?";
            }

            if (input.Contains("i am interested in") || input.Contains("my favourite topic is") || input.Contains("i like"))
            {
                foreach (string topic in topicResponses.Keys)
                {
                    if (input.Contains(topic))
                    {
                        memory.FavouriteTopic = topic;
                        return "Great! I will remember that you are interested in " + topic + ". " + GetRandomResponse(topic);
                    }
                }
            }

            foreach (string topic in topicResponses.Keys)
            {
                if (input.Contains(topic))
                {
                    memory.LastTopic = topic;
                    string response = sentimentOpening + GetRandomResponse(topic);

                    if (memory.HasFavouriteTopic() && memory.FavouriteTopic == topic)
                        response += " Since " + topic + " is your favourite topic, here is an extra tip: stay updated on the latest threats!";

                    return response;
                }
            }

            if (input.Contains("how are you"))
                return "I am doing great and ready to help you stay safe online!";

            if (input.Contains("purpose") || input.Contains("what do you do"))
                return "I am your Cybersecurity Awareness Assistant. I help you stay safe online by providing tips on topics like passwords, phishing, privacy, and more!";

            if (input.Contains("help") || input.Contains("topics") || input.Contains("what can"))
                return "I can help you with the following topics:\n- Password Safety\n- Phishing Awareness\n- Privacy Tips\n- Scam Awareness\n- Malware Protection\n- Safe Browsing";

            if (input.Contains("hello") || input.Contains("hi"))
            {
                if (memory.HasName())
                    return "Hello again, " + memory.UserName + "! How can I help you stay safe online today?";
                else
                    return "Hello! I am CyberBot. What is your name?";
            }

            if (input.Contains("my name is"))
            {
                string name = input.Replace("my name is", "").Trim();
                memory.UserName = name;
                return "Nice to meet you, " + name + "! How can I help you stay safe online today?";
            }

            return "I am not sure I understand that. Could you try rephrasing? You can also type 'help' to see what topics I can assist with.";
        }
    }
}





