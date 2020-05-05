using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTakTokGame
{
    public partial class Form1 : Form
    {
        #region Constructor
        public Form1()
        {
            InitializeComponent();
            FillAllButtons();
            Buttons.Reverse();
            Btns = new Dictionary<int, string>();
            Buttons.ForEach(btn => { Btns.Add(Buttons.IndexOf(btn) + 1, btn.Name); });

            Reset();
        }
        #endregion

        #region Properties
        public bool Oplayer { get; set; }
        public int XPlayerCounter { get; set; }
        public int OPlayerCounter { get; set; }
        private int BtnCounter { get; set; }
        public bool IsCPU { get; set; }
        public bool CPUTrun { get; set; }
        public Dictionary<int, string> Btns { get; set; }
        private int Postion { get; set; }

        private List<Button> Buttons { get; set; }
        #endregion

        #region Events
        private void x1_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void x2_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void x3_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void y1_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void y2_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void y3_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void z1_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void z2_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void z3_Click(object sender, EventArgs e)
        {
            ChoosePostion(HandleClick(sender));
        }
        private void resetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void twoPlayer_Click(object sender, EventArgs e)
        {
            Reset();
            IsCPU = false;
            PlayerMode();
        }
        private void cpuPlayer_Click(object sender, EventArgs e)
        {
            Reset();
            IsCPU = true;
            PlayerMode();
        }
        private TicBtn HandleClick(object sender)
        {
            TicBtn ticBtn = new TicBtn();
            ticBtn.button = sender as Button;
            return ticBtn;
        }
        #endregion

        #region Methods
        private void ChoosePostion(TicBtn ticBtn)
        {
            BtnCounter++;
            var _Winner = string.Empty;
            if (IsCPU && CPUTrun)
            {
                Postion = 0;
                var button = CPUDecesion();
                button.Text = Oplayer ? "O" : "X";
                button.BackColor = Oplayer ? Color.Blue : Color.Green;
                button.Enabled = false;
            }
            else
            {
                ticBtn.button.Text = Oplayer ? "O" : "X";
                ticBtn.button.BackColor = Oplayer ? Color.Blue : Color.Green;
                ticBtn.button.Enabled = false;
            }
            if (IsCPU) CPUTrun = !CPUTrun;
            Oplayer = !Oplayer;
            lblTurnPlayer.Text = Oplayer ? "O Player" : "X Player";
            _Winner = EvaluateWinner();
            if (!string.IsNullOrWhiteSpace(_Winner))
            {
                if (_Winner == "O")
                {
                    OPlayerCounter++;
                    lblO.Text = WinCounter("O");
                }
                else
                {
                    XPlayerCounter++;
                    lblX.Text = WinCounter("X");
                }
                MessageBox.Show($"Player {_Winner} Wins!");
                ContinueGame();
            }

            if (BtnCounter == 8)
            {
                MessageBox.Show("Draw !");
                ContinueGame();
            }
            if (IsCPU && CPUTrun) ChoosePostion(new TicBtn());
        }
        private void ContinueGame()
        {
            BtnCounter = 0;
            Oplayer = OPlayerCounter > XPlayerCounter ? true : false;
            CPUTrun = !IsCPU ? false : !Oplayer;
            lblTurnPlayer.Text = Oplayer ? "O Player" : "X Player";
            x1.Enabled = x2.Enabled = x3.Enabled = y1.Enabled = y2.Enabled = y3.Enabled = z1.Enabled = z2.Enabled = z3.Enabled = true;
            x1.Text = x2.Text = x3.Text = y1.Text = y2.Text = y3.Text = z1.Text = z2.Text = z3.Text = "";
            x1.BackColor = x2.BackColor = x3.BackColor = y1.BackColor = y2.BackColor = y3.BackColor = z1.BackColor = z2.BackColor = z3.BackColor = Color.White;
            if (CPUTrun) ChoosePostion(new TicBtn());
        }
        private void Reset()
        {
            CPUTrun = false;
            IsCPU = false;
            BtnCounter = 0;
            Oplayer = true;
            lblO.Text = "0";
            lblX.Text = "0";
            lblTurnPlayer.Text = Oplayer ? "O Player" : "X Player";
            x1.Enabled = x2.Enabled = x3.Enabled = y1.Enabled = y2.Enabled = y3.Enabled = z1.Enabled = z2.Enabled = z3.Enabled = true;
            x1.Text = x2.Text = x3.Text = y1.Text = y2.Text = y3.Text = z1.Text = z2.Text = z3.Text = "";
            x1.BackColor = x2.BackColor = x3.BackColor = y1.BackColor = y2.BackColor = y3.BackColor = z1.BackColor = z2.BackColor = z3.BackColor = Color.White;
            PlayerMode();
        }
        private string WinCounter(string player)
        {
            string _counter = string.Empty;
            switch (player)
            {
                case "X":
                    _counter = XPlayerCounter + " Wins";
                    break;
                case "O":
                    _counter = OPlayerCounter + " Wins";
                    break;
            }
            return _counter;
        }
        private string EvaluateWinner()
        {
            string _winner = string.Empty;

            if (x1.Text == x2.Text && x1.Text == x3.Text)
            {
                _winner = x1.Text;
            }
            else if (y1.Text == y2.Text && y1.Text == y3.Text)
            {
                _winner = y1.Text;
            }
            else if (z1.Text == z2.Text && z1.Text == z3.Text)
            {
                _winner = z1.Text;
            }
            else if (x1.Text == y1.Text && y1.Text == z1.Text)
            {
                _winner = x1.Text;
            }
            else if (x2.Text == y2.Text && y2.Text == z2.Text)
            {
                _winner = x2.Text;
            }
            else if (x3.Text == y3.Text && y3.Text == z3.Text)
            {
                _winner = x3.Text;
            }
            else if (x1.Text == y2.Text && y2.Text == z3.Text)
            {
                _winner = x1.Text;
            }
            else if (x3.Text == y2.Text && y2.Text == z1.Text)
            {
                _winner = x3.Text;
            }

            return _winner;
        }
        private Control CPUDecesion()
        {
            var _ctrlId = string.Empty;
            var _pred = WinMoveForCPU();
            if (_pred > 0)
            {
                _ctrlId = Btns.Where(b => b.Key == _pred).Select(v => v.Value).FirstOrDefault();
                if (CheckValidPostion(_ctrlId))
                {
                    return Buttons.Where(b => b.Name == _ctrlId).FirstOrDefault();
                }
                else
                {
                    return PickPostion();
                }
            }
            else
            {
                return PickPostion();
            }
        }
        private Control PickPostion()
        {
            var _postion = ++Postion;
            var _ctrlId = Btns.Where(b => b.Key == _postion).Select(v => v.Value).FirstOrDefault();
            if (CheckValidPostion(_ctrlId))
            {
                return Buttons.Where(b => b.Name == _ctrlId).FirstOrDefault();
            }
            else
            {
                return PickPostion();
            }
        }
        private bool CheckValidPostion(string postion)
        {
            return Buttons.Any(b => b.Name == postion && b.Enabled && string.IsNullOrEmpty(b.Text));
        }
        private void PlayerMode()
        {
            if (IsCPU)
            {
                cpuPlayer.BackColor = Color.Green;
                twoPlayer.BackColor = Color.White;
            }
            else
            {
                cpuPlayer.BackColor = Color.White;
                twoPlayer.BackColor = Color.Green;
            }
        }
        private int WinMoveForCPU()
        {
            var Xs = new List<string>();
            Buttons.ForEach(btn =>
            {
                if (btn.Text == "X") Xs.Add(btn.Name);
            });

            #region Raws
            //X Raw
            if (Xs.Contains("x3") && Xs.Contains("x2") && string.IsNullOrEmpty(x1.Text))
            {
                return 1;
            }
            else if (Xs.Contains("x3") && Xs.Contains("x1") && string.IsNullOrEmpty(x2.Text))
            {
                return 2;
            }
            else if (Xs.Contains("x1") && Xs.Contains("x2") && string.IsNullOrEmpty(x3.Text))
            {
                return 3;
            }
            //Y raw
            else if (Xs.Contains("y3") && Xs.Contains("y2") && string.IsNullOrEmpty(y1.Text))
            {
                return 4;
            }
            else if (Xs.Contains("y3") && Xs.Contains("y1") && string.IsNullOrEmpty(y2.Text))
            {
                return 5;
            }
            else if (Xs.Contains("y2") && Xs.Contains("x1") && string.IsNullOrEmpty(y3.Text))
            {
                return 6;
            }
            //Z raw
            else if (Xs.Contains("z3") && Xs.Contains("z2") && string.IsNullOrEmpty(z1.Text))
            {
                return 7;
            }
            else if (Xs.Contains("z3") && Xs.Contains("z1") && string.IsNullOrEmpty(z2.Text))
            {
                return 8;
            }
            else if (Xs.Contains("z2") && Xs.Contains("z1") && string.IsNullOrEmpty(z3.Text))
            {
                return 9;
            }
            #endregion
            #region Cols
            //Col 1
            else if (Xs.Contains("x1") && Xs.Contains("y1") && string.IsNullOrEmpty(z1.Text))
            {
                return 7;
            }
            else if (Xs.Contains("z1") && Xs.Contains("x1") && string.IsNullOrEmpty(y1.Text))
            {
                return 4;
            }
            else if (Xs.Contains("z1") && Xs.Contains("y1") && string.IsNullOrEmpty(x1.Text))
            {
                return 1;
            }
            //col 2
            else if (Xs.Contains("x2") && Xs.Contains("z2") && string.IsNullOrEmpty(y2.Text))
            {
                return 5;
            }
            else if (Xs.Contains("x2") && Xs.Contains("y2") && string.IsNullOrEmpty(z2.Text))
            {
                return 8;
            }
            else if (Xs.Contains("z2") && Xs.Contains("y2") && string.IsNullOrEmpty(x2.Text))
            {
                return 2;
            }
            //col 3
            else if (Xs.Contains("z3") && Xs.Contains("x3") && string.IsNullOrEmpty(y3.Text))
            {
                return 6;
            }
            else if (Xs.Contains("z3") && Xs.Contains("y3") && string.IsNullOrEmpty(x3.Text))
            {
                return 3;
            }
            else if (Xs.Contains("x3") && Xs.Contains("y3") && string.IsNullOrEmpty(z3.Text))
            {
                return 9;
            }
            #endregion
            #region Diagonal
            //Diagonal One
            else if (Xs.Contains("z3") && Xs.Contains("y2") && string.IsNullOrEmpty(x1.Text))
            {
                return 1;
            }
            else if (Xs.Contains("z3") && Xs.Contains("x1") && string.IsNullOrEmpty(y2.Text))
            {
                return 5;
            }
            else if (Xs.Contains("x1") && Xs.Contains("y2") && string.IsNullOrEmpty(z3.Text))
            {
                return 9;
            }
            //Diagonal Two
            else if (Xs.Contains("x3") && Xs.Contains("y2") && string.IsNullOrEmpty(z1.Text))
            {
                return 7;
            }
            else if (Xs.Contains("z1") && Xs.Contains("x3") && string.IsNullOrEmpty(y2.Text))
            {
                return 5;
            }
            else if (Xs.Contains("z1") && Xs.Contains("y2") && string.IsNullOrEmpty(x3.Text))
            {
                return 3;
            }
            #endregion

            return -1;
        }

        private void FillAllButtons()
        {
            Buttons = new List<Button>();
            // Add Global property for all buttons control
            foreach (var control in this.FindForm().Controls)
            {
                var _btn = control as Control;
                if (_btn is Button)
                {
                    if ((_btn.Name.StartsWith("x") || _btn.Name.StartsWith("y") || _btn.Name.StartsWith("z")) && _btn.Name.Length == 2) Buttons.Add(_btn as Button);
                }
            }
        }
        #endregion
    }

    public class TicBtn
    {
        public System.Windows.Forms.Button button { get; set; }
        public string Postion { get; set; }
        public string Player { get; set; }
    }
}
