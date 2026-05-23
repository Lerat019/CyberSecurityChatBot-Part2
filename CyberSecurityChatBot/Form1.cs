using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CyberSecurityChatBot
{
    public partial class Form1 : Form
    {
        private ChatMemory memory = new ChatMemory();
        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.BackColor = Color.FromArgb(30, 30, 46);
            this.ForeColor = Color.White;
            this.Font = new Font("Consolas", 10);

            Label lblLogo = new Label();
            lblLogo.Text =

                "  * * * * * * *\n" +
                "*               *\n" +
                "*    \\\\          *\n" +
                "*     \\\\         *\n" +
                "*      \\\\____    *\n" +
                "*           \\\\   *\n" +
                "*            \\\\  *\n" +
                "  * * * * * * *\n" +
                "   >> CYBER BOT <<\n" +
                " Cybersecurity Awareness Assistant";

            lblLogo.ForeColor = Color.Magenta;
            lblLogo.Font = new Font("Consolas", 9, FontStyle.Bold);
            lblLogo.Location = new Point(10, 10);
            lblLogo.Size = new Size(860, 180);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblLogo);

            RichTextBox chatBox = new RichTextBox();
            chatBox.Name = "chatBox";
            chatBox.Location = new Point(10, 200);
            chatBox.Size = new Size(860, 300);
            chatBox.BackColor = Color.FromArgb(20, 20, 35);
            chatBox.ForeColor = Color.LightGreen;
            chatBox.Font = new Font("Consolas", 10);
            chatBox.ReadOnly = true;
            chatBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.Controls.Add(chatBox);

            TextBox inputBox = new TextBox();
            inputBox.Name = "inputBox";
            inputBox.Location = new Point(10, 510);
            inputBox.Size = new Size(730, 30);
            inputBox.BackColor = Color.FromArgb(20, 20, 35);
            inputBox.ForeColor = Color.White;
            inputBox.Font = new Font("Consolas", 10);
            this.Controls.Add(inputBox);

            Button sendBtn = new Button();
            sendBtn.Name = "sendBtn";
            sendBtn.Text = "Send";
            sendBtn.Location = new Point(755, 508);
            sendBtn.Size = new Size(110, 32);
            sendBtn.BackColor = Color.FromArgb(100, 200, 100);
            sendBtn.ForeColor = Color.Black;
            sendBtn.Font = new Font("Consolas", 10, FontStyle.Bold);
            sendBtn.Click += SendBtn_Click;
            this.Controls.Add(sendBtn);
            
            Button clearBtn = new Button();
            clearBtn.Name = "clearBtn";
            clearBtn.Text = "Clear";
            clearBtn.Location = new Point(755, 545);
            clearBtn.Size = new Size(110, 32);
            clearBtn.BackColor = Color.FromArgb(200, 80, 80);
            clearBtn.ForeColor = Color.White;
            clearBtn.Font = new Font("Consolas", 10, FontStyle.Bold);
            clearBtn.Click += (s, e) =>
            {
                RichTextBox clearChatBox = this.Controls["chatBox"] as RichTextBox;
                clearChatBox.Clear();
                AppendMessage("CyberBot", "Chat cleared! How can I help you?", Color.Cyan);
            };
            this.Controls.Add(clearBtn);

            inputBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SendBtn_Click(s, e);
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread audioThread = new Thread(() => AudioHelper.PlayVoiceGreeting());
            audioThread.IsBackground = true;
            audioThread.Start();

            AppendMessage("CyberBot", "Welcome! I am your Cybersecurity Awareness Assistant. What is your name?", Color.Cyan);
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            TextBox inputBox = this.Controls["inputBox"] as TextBox;
            RichTextBox chatBox = this.Controls["chatBox"] as RichTextBox;

            string userInput = inputBox.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(userInput))
                return;

            AppendMessage("You", inputBox.Text.Trim(), Color.White);
            inputBox.Clear();

            string response = Responses.GetResponse(userInput, memory);
            AppendMessage("CyberBot", response, Color.LightGreen);

            chatBox.ScrollToCaret();
        }

        private void AppendMessage(string sender, string message, Color color)
        {
            RichTextBox chatBox = this.Controls["chatBox"] as RichTextBox;
            chatBox.SelectionStart = chatBox.TextLength;
            chatBox.SelectionLength = 0;
            chatBox.SelectionColor = color;
            chatBox.AppendText(sender + ": " + message + "\n\n");
            chatBox.SelectionColor = chatBox.ForeColor;
        }
    }
}