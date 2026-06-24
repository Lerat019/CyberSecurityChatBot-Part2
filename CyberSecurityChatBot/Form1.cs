using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CyberSecurityChatBot
{
    public partial class Form1 : Form
    {
        // Stores user details like name and favourite topic across the conversation
        private ChatMemory memory = new ChatMemory();
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public Form1()
        {
            InitializeComponent();
            // Sets up all UI controls and layout for the chat window
            SetupUI();
        }

        // Builds and configures all visual controls on the form
        private void SetupUI()
        {
            this.BackColor = Color.FromArgb(30, 30, 46);
            this.ForeColor = Color.White;
            this.Font = new Font("Consolas", 10);
            this.Size = new Size(920, 650);
            this.Text = "CyberBot - Cybersecurity Awareness Assistant";

            // Tab control to hold all features
            TabControl tabControl = new TabControl();
            tabControl.Name = "tabControl";
            tabControl.Location = new Point(10, 10);
            tabControl.Size = new Size(880, 590);
            tabControl.BackColor = Color.FromArgb(30, 30, 46);
            this.Controls.Add(tabControl);

            // Tab 1 - Chat
            TabPage chatTab = new TabPage("Chat");
            chatTab.BackColor = Color.FromArgb(30, 30, 46);
            tabControl.TabPages.Add(chatTab);
            SetupChatTab(chatTab);

            // Tab 2 - Task Assistant
            TabPage taskTab = new TabPage("Tasks");
            taskTab.BackColor = Color.FromArgb(30, 30, 46);
            tabControl.TabPages.Add(taskTab);
            SetupTaskTab(taskTab);
        }

        private void SetupChatTab(TabPage tab)
        {
            // ASCII art logo
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
            lblLogo.Location = new Point(10, 5);
            lblLogo.Size = new Size(840, 160);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            tab.Controls.Add(lblLogo);

            // Chat display
            RichTextBox chatBox = new RichTextBox();
            chatBox.Name = "chatBox";
            chatBox.Location = new Point(10, 170);
            chatBox.Size = new Size(840, 270);
            chatBox.BackColor = Color.FromArgb(20, 20, 35);
            chatBox.ForeColor = Color.LightGreen;
            chatBox.Font = new Font("Consolas", 10);
            chatBox.ReadOnly = true;
            chatBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            tab.Controls.Add(chatBox);

            // Input box
            TextBox inputBox = new TextBox();
            inputBox.Name = "inputBox";
            inputBox.Location = new Point(10, 450);
            inputBox.Size = new Size(700, 30);
            inputBox.BackColor = Color.FromArgb(20, 20, 35);
            inputBox.ForeColor = Color.White;
            inputBox.Font = new Font("Consolas", 10);
            tab.Controls.Add(inputBox);

            // Send button
            Button sendBtn = new Button();
            sendBtn.Name = "sendBtn";
            sendBtn.Text = "Send";
            sendBtn.Location = new Point(720, 448);
            sendBtn.Size = new Size(80, 30);
            sendBtn.BackColor = Color.FromArgb(100, 200, 100);
            sendBtn.ForeColor = Color.Black;
            sendBtn.Font = new Font("Consolas", 10, FontStyle.Bold);
            sendBtn.Click += SendBtn_Click;
            tab.Controls.Add(sendBtn);

            // Clear button
            Button clearBtn = new Button();
            clearBtn.Name = "clearBtn";
            clearBtn.Text = "Clear";
            clearBtn.Location = new Point(810, 448);
            clearBtn.Size = new Size(50, 30);
            clearBtn.BackColor = Color.FromArgb(200, 80, 80);
            clearBtn.ForeColor = Color.White;
            clearBtn.Font = new Font("Consolas", 9, FontStyle.Bold);
            clearBtn.Click += (s, e) =>
            {
                RichTextBox cb = chatBox;
                cb.Clear();
                AppendMessage("CyberBot", "Chat cleared! How can I help you?", Color.Cyan);
            };
            tab.Controls.Add(clearBtn);

            // Topics label
            Label lblTopics = new Label();
            lblTopics.Text = "Topics: password | phishing | privacy | scam | malware | safe browsing";
            lblTopics.ForeColor = Color.Yellow;
            lblTopics.Font = new Font("Consolas", 8);
            lblTopics.Location = new Point(10, 485);
            lblTopics.Size = new Size(700, 20);
            tab.Controls.Add(lblTopics);

            // Enter key support
            inputBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SendBtn_Click(s, e);
            };
        }

        private void SetupTaskTab(TabPage tab)
        {
            // Title label
            Label lblTitle = new Label();
            lblTitle.Text = "TASK ASSISTANT - Manage Your Cybersecurity Tasks";
            lblTitle.ForeColor = Color.Cyan;
            lblTitle.Font = new Font("Consolas", 11, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.Size = new Size(840, 25);
            tab.Controls.Add(lblTitle);

            // Task title input
            Label lblTaskTitle = new Label();
            lblTaskTitle.Text = "Task Title:";
            lblTaskTitle.ForeColor = Color.White;
            lblTaskTitle.Location = new Point(10, 50);
            lblTaskTitle.Size = new Size(80, 20);
            tab.Controls.Add(lblTaskTitle);

            TextBox txtTaskTitle = new TextBox();
            txtTaskTitle.Name = "txtTaskTitle";
            txtTaskTitle.Location = new Point(100, 48);
            txtTaskTitle.Size = new Size(350, 25);
            txtTaskTitle.BackColor = Color.FromArgb(20, 20, 35);
            txtTaskTitle.ForeColor = Color.White;
            tab.Controls.Add(txtTaskTitle);

            // Task description input
            Label lblDesc = new Label();
            lblDesc.Text = "Description:";
            lblDesc.ForeColor = Color.White;
            lblDesc.Location = new Point(10, 85);
            lblDesc.Size = new Size(85, 20);
            tab.Controls.Add(lblDesc);

            TextBox txtDesc = new TextBox();
            txtDesc.Name = "txtDesc";
            txtDesc.Location = new Point(100, 83);
            txtDesc.Size = new Size(350, 25);
            txtDesc.BackColor = Color.FromArgb(20, 20, 35);
            txtDesc.ForeColor = Color.White;
            tab.Controls.Add(txtDesc);

            // Reminder date input
            Label lblReminder = new Label();
            lblReminder.Text = "Reminder:";
            lblReminder.ForeColor = Color.White;
            lblReminder.Location = new Point(10, 120);
            lblReminder.Size = new Size(85, 20);
            tab.Controls.Add(lblReminder);

            TextBox txtReminder = new TextBox();
            txtReminder.Name = "txtReminder";
            txtReminder.Location = new Point(100, 118);
            txtReminder.Size = new Size(200, 25);
            txtReminder.BackColor = Color.FromArgb(20, 20, 35);
            txtReminder.ForeColor = Color.White;
            txtReminder.Text = "e.g. 2026-07-01";
            tab.Controls.Add(txtReminder);

            // Add task button
            Button btnAddTask = new Button();
            btnAddTask.Text = "Add Task";
            btnAddTask.Location = new Point(100, 155);
            btnAddTask.Size = new Size(100, 30);
            btnAddTask.BackColor = Color.FromArgb(100, 200, 100);
            btnAddTask.ForeColor = Color.Black;
            btnAddTask.Font = new Font("Consolas", 9, FontStyle.Bold);
            tab.Controls.Add(btnAddTask);

            // Tasks display
            ListBox lstTasks = new ListBox();
            lstTasks.Name = "lstTasks";
            lstTasks.Location = new Point(10, 200);
            lstTasks.Size = new Size(840, 200);
            lstTasks.BackColor = Color.FromArgb(20, 20, 35);
            lstTasks.ForeColor = Color.LightGreen;
            lstTasks.Font = new Font("Consolas", 9);
            tab.Controls.Add(lstTasks);

            // Complete and Delete buttons
            Button btnComplete = new Button();
            btnComplete.Text = "Mark Complete";
            btnComplete.Location = new Point(10, 415);
            btnComplete.Size = new Size(130, 30);
            btnComplete.BackColor = Color.FromArgb(50, 150, 250);
            btnComplete.ForeColor = Color.White;
            btnComplete.Font = new Font("Consolas", 9, FontStyle.Bold);
            tab.Controls.Add(btnComplete);

            Button btnDelete = new Button();
            btnDelete.Text = "Delete Task";
            btnDelete.Location = new Point(150, 415);
            btnDelete.Size = new Size(110, 30);
            btnDelete.BackColor = Color.FromArgb(200, 80, 80);
            btnDelete.ForeColor = Color.White;
            btnDelete.Font = new Font("Consolas", 9, FontStyle.Bold);
            tab.Controls.Add(btnDelete);

            // Load tasks into listbox
            void RefreshTasks()
            {
                lstTasks.Items.Clear();
                foreach (string task in dbHelper.GetAllTasks())
                    lstTasks.Items.Add(task);
            }

            // Add task button click
            btnAddTask.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTaskTitle.Text)) return;
                dbHelper.AddTask(txtTaskTitle.Text, txtDesc.Text, txtReminder.Text);
                RefreshTasks();
                txtTaskTitle.Clear();
                txtDesc.Clear();
                txtReminder.Text = "e.g. 2026-07-01";
                MessageBox.Show("Task added successfully!", "CyberBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Complete button click
            btnComplete.Click += (s, e) =>
            {
                if (lstTasks.SelectedItem == null) return;
                string selected = lstTasks.SelectedItem.ToString();
                int id = int.Parse(selected.Split('.')[0]);
                dbHelper.CompleteTask(id);
                RefreshTasks();
            };

            // Delete button click
            btnDelete.Click += (s, e) =>
            {
                if (lstTasks.SelectedItem == null) return;
                string selected = lstTasks.SelectedItem.ToString();
                int id = int.Parse(selected.Split('.')[0]);
                dbHelper.DeleteTask(id);
                RefreshTasks();
            };

            // Load tasks on startup
            RefreshTasks();
        }

        // Runs when the form loads - plays voice greeting and shows welcome message
        private void Form1_Load(object sender, EventArgs e)
        {
            // Play voice greeting on a background thread to avoid freezing the UI
            Thread audioThread = new Thread(() => AudioHelper.PlayVoiceGreeting());
            audioThread.IsBackground = true;
            audioThread.Start();

            // Display the initial welcome message in the chat
            AppendMessage("CyberBot", "Welcome! I am your Cybersecurity Awareness Assistant. What is your name?", Color.Cyan);
        }

        // Handles sending user messages and displaying bot responses
        private void SendBtn_Click(object sender, EventArgs e)
        {
            TextBox inputBox = this.Controls["inputBox"] as TextBox;
            RichTextBox chatBox = this.Controls["chatBox"] as RichTextBox;

            string userInput = inputBox.Text.Trim().ToLower();

            // Ignore empty input
            if (string.IsNullOrWhiteSpace(userInput))
                return;

            // Display the users message in white
            AppendMessage("You", inputBox.Text.Trim(), Color.White);
            inputBox.Clear();

            // Get the bots response and display it in green
            string response = Responses.GetResponse(userInput, memory);
            AppendMessage("CyberBot", response, Color.LightGreen);

            chatBox.ScrollToCaret();
        }

        // Appends a coloured message to the chat display area
        private void AppendMessage(string sender, string message, Color color)
        {
            RichTextBox chatBox = null;

            // Search inside the tab control for the chatBox
            TabControl tabControl = this.Controls["tabControl"] as TabControl;
            if (tabControl != null)
            {
                foreach (TabPage tab in tabControl.TabPages)
                {
                    chatBox = tab.Controls["chatBox"] as RichTextBox;
                    if (chatBox != null) break;
                }
            }

            if (chatBox == null) return;

            chatBox.SelectionStart = chatBox.TextLength;
            chatBox.SelectionLength = 0;
            chatBox.SelectionColor = color;
            chatBox.AppendText(sender + ": " + message + "\n\n");
            chatBox.SelectionColor = chatBox.ForeColor;
        }
    }
}