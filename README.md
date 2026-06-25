\# CyberBot - Cybersecurity Awareness Chatbot

\### PROG6221 Portfolio of Evidence - Part 3

\*\*Student:\*\* \[Lerato Mabusela]  

\*\*Student Number:\*\* \[ST10494431]



\## Overview



CyberBot is a Windows Forms chatbot application built in C# that educates users on 

cybersecurity. The project was developed across three parts, each building on the last.



Part 1 was a console application with ASCII art, typing effects, console colors, 

WAV audio playback, and basic keyword responses across four classes.



Part 2 upgraded the project to a full Windows Forms GUI with a dark themed chat 

interface, colored messages, sentiment detection, conversation memory that remembers 

the users name and favorite topic, multiple random responses per topic, and a WAV 

voice greeting that plays on startup.



Part 3 is the final version. It adds four major features on top of everything from 

Parts 1 and 2 - a task assistant backed by a real MySQL database, a cybersecurity 

mini quiz, smarter NLP-style chat responses, and an activity log with timestamps.





\## Features



\### Chat Tab

The original chatbot from Parts 1 and 2. Responds to cybersecurity keywords like 

password, phishing, privacy, scam, malware, and safe browsing. Detects the users 

sentiment and adjusts its opening response accordingly. Remembers the users name 

and favorite topic throughout the session. Gives varied responses by randomly 

selecting from a list of answers per topic.



\### Task Assistant Tab

Users can add cybersecurity tasks like "Enable two-factor authentication" or 

"Review privacy settings". Each task has a title, description, and an optional 

reminder date. Tasks are saved to a MySQL database so they are not lost when the 

app closes. Users can mark tasks as complete or delete them and those changes 

reflect in the database immediately.



\### Quiz Tab

A cybersecurity quiz with 11 questions covering phishing, password safety, malware, 

safe browsing, two-factor authentication, and social engineering. Questions are 

shuffled randomly each time so the order is different every run. The quiz mixes 

multiple choice and true or false formats. After each answer the user gets instant 

feedback explaining why it was correct or wrong. A final score and message is shown 

at the end based on how well the user did.



\### Activity Log Tab

Records every significant action the chatbot takes with a timestamp. This includes 

chat responses, NLP-detected commands, quiz interactions, and when the user views 

the log. Shows the last 10 actions and can be refreshed or cleared at any time.



\### NLP Simulation

The chatbot recognises user intent even when commands are worded differently. For 

example "quiz me", "test me", and "play quiz" all trigger the quiz. "Add task", 

"new task", and "remind me to" all direct the user to the task tab. "Show activity 

log" and "what have you done for me" both display the recent log. This is done using 

keyword detection with string.Contains() checks on the lowercased input.





\## Project Structure



File - Purpose

Form1.cs - Main form that sets up all tabs and handles UI events |

Responses.cs - Keyword responses, sentiment detection and NLP handling |

ChatMemory.cs - Stores the users name and favorite topic for the session |

DatabaseHelper.cs - All MySQL database operations for the task assistant |

QuizManager.cs - Quiz questions, answer checking, scoring and feedback |

AudioHelper.cs - Plays the WAV voice greeting when the app starts |





\## How to Set Up



\### Requirements

\- Visual Studio 2022

\- .NET Framework 4.7.2 or higher

\- MySQL Server 8.0

\- MySql.Data NuGet package



\### Database Setup

Open MySQL Workbench, connect to your local instance, and run this:



```sql

CREATE DATABASE IF NOT EXISTS cyberbot\_db;



USE cyberbot\_db;



CREATE TABLE IF NOT EXISTS tasks (

&#x20;   Id INT AUTO\_INCREMENT PRIMARY KEY,

&#x20;   Title VARCHAR(255) NOT NULL,

&#x20;   Description TEXT,

&#x20;   ReminderDate VARCHAR(100),

&#x20;   IsCompleted BOOLEAN DEFAULT FALSE

);



Running the Project

Clone the repository from GitHub

Open CyberSecurityChatBot.sln in Visual Studio

Open DatabaseHelper.cs and update the password in the connection string

to match your MySQL root password

Build the solution with Ctrl+Shift+B

Run the project with the Start button





GitHub and YouTube Video

GitHub Repository: \[GitHub Link]

YouTube Presentation: \[YouTube Link]

