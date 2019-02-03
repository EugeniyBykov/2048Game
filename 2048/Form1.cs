using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        Random _rnd;
        Label[,] _arr;
        int _score;

        List<string> _playerName;
        List<string> _bestScore;
        string NewName;
        Form f2;


        public Form1()
        {
            InitializeComponent();
            _playerName = new List<string>();
            _bestScore = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                _playerName.Add("Uknown");
                _bestScore.Add(0.ToString());
            }
            LoadData();

            _arr = new Label[4, 4]
            {
                {l1, l2, l3, l4 },
                {l5, l6, l7, l8 },
                {l9, l10, l11, l12 },
                {l13, l14, l15, l16 },
            };
            _rnd = new Random();
            _score = 0;
            AddTwoNumbers();


        }

        public string NewPlayerName { set { NewName = value; } }
        public List<string> PlayerNamearr { get { return _playerName; } }
        public List<string> PlayerScorearr { get { return _bestScore; } }
        public int Score { get { return _score; } }
        public void LoadData()
        {
            string tmp = Directory.GetCurrentDirectory();
            string path = tmp + @"\data.dat";
            int k = 0;
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        string score = reader.ReadString();
                        _playerName[k] = name;
                        _bestScore[k] = score;
                        k++;
                    }

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            label8.Text = _bestScore[0];
        }

        private void AddTwoNumbers()
        {
            int q, w, r;
            int count = FreeNumbers();

            if (count >= 2)
            {
                for (int i = 0; i < 2;)
                {
                    q = _rnd.Next(0, 4);
                    w = _rnd.Next(0, 4);
                    r = _rnd.Next(0, 11);
                    if (_arr[q, w].Text == "")
                    {
                        if (r > 7)
                            _arr[q, w].Text = 4.ToString();
                        else
                            _arr[q, w].Text = 2.ToString();
                        i++;
                    }

                }
            }

            if (count == 1)
            {
                for (int i = 0; i < 1;)
                {
                    q = _rnd.Next(0, 4);
                    w = _rnd.Next(0, 4);
                    r = _rnd.Next(0, 11);
                    if (_arr[q, w].Text == "")
                    {
                        if (r > 7)
                            _arr[q, w].Text = 4.ToString();
                        else
                            _arr[q, w].Text = 2.ToString();
                        i++;
                    }
                }
            }

            isLose();
            ChangeColor(); 

        }

        private int FreeNumbers()
        {
            int count = 0;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (_arr[i, j].Text == "")
                        count++;


            return count;
        }

        private void isLose()
        {

            if (FreeNumbers() == 0)
            {
                bool lose = true;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_arr[i, j].Text == _arr[i, j + 1].Text || _arr[i, j].Text == _arr[i + 1, j].Text)
                        {
                            lose = false;
                            break;
                        }
                    }
                    if (lose == false)
                        break;
                }

                int l = 3;
                int k = 0;
                while (k < 3)
                {
                    if (_arr[l, k].Text == _arr[l, k + 1].Text)
                    {
                        lose = false;
                        break;
                    }
                    k++;
                }

                k = 0;
                while (k < 3)
                {
                    if (_arr[k, l].Text == _arr[k + 1, l].Text)
                    {
                        lose = false;
                        break;
                    }
                    k++;
                }

                if (lose == true)
                {
                    MessageBox.Show("Game Over");
                    for (int i = 0; i < _playerName.Count; i++)
                    {
                        if (_score >= (Convert.ToInt32(_bestScore[i])))
                        {
                            f2 = new Form2(this);
                            f2.Show();
                            break;
                        }
                    }
                }


            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Right")
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 3; j >= 0; j--)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (_arr[i, k].Text != "")
                            {
                                if (_arr[i, j].Text == "")
                                {
                                    _arr[i, j].Text = _arr[i, k].Text;
                                    _arr[i, k].Text = "";
                                }
                                else
                                {
                                    if (_arr[i, j].Text == _arr[i, k].Text)
                                    {
                                        _arr[i, j].Text = (Int32.Parse(_arr[i, k].Text) * 2).ToString();
                                        _arr[i, k].Text = "";
                                        _score += Int32.Parse(_arr[i, j].Text) * 2;
                                        label7.Text = _score.ToString();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                AddTwoNumbers();

            }

            if (e.KeyCode.ToString() == "Left")
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (_arr[i, k].Text != "")
                            {
                                if (_arr[i, j].Text == "")
                                {
                                    _arr[i, j].Text = _arr[i, k].Text;
                                    _arr[i, k].Text = "";
                                }
                                else
                                {
                                    if (_arr[i, j].Text == _arr[i, k].Text)
                                    {
                                        _arr[i, j].Text = (Int32.Parse(_arr[i, k].Text) * 2).ToString();
                                        _arr[i, k].Text = "";
                                        _score += Int32.Parse(_arr[i, j].Text) * 2;
                                        label7.Text = _score.ToString();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                AddTwoNumbers();
            }

            if (e.KeyCode.ToString() == "Up")
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = i + 1; k < 4; k++)
                        {
                            if (_arr[k, j].Text != "")
                            {
                                if (_arr[i, j].Text == "")
                                {
                                    _arr[i, j].Text = _arr[k, j].Text;
                                    _arr[k, j].Text = "";
                                }
                                else
                                {
                                    if (_arr[i, j].Text == _arr[k, j].Text)
                                    {
                                        _arr[i, j].Text = (Int32.Parse(_arr[k, j].Text) * 2).ToString();
                                        _arr[k, j].Text = "";
                                        _score += Int32.Parse(_arr[i, j].Text) * 2;
                                        label7.Text = _score.ToString();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                AddTwoNumbers();
            }

            if (e.KeyCode.ToString() == "Down")
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 3; i >= 0; i--)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (_arr[k, j].Text != "")
                            {
                                if (_arr[i, j].Text == "")
                                {
                                    _arr[i, j].Text = _arr[k, j].Text;
                                    _arr[k, j].Text = "";
                                }
                                else
                                {
                                    if (_arr[i, j].Text == _arr[k, j].Text)
                                    {
                                        _arr[i, j].Text = (Int32.Parse(_arr[k, j].Text) * 2).ToString();
                                        _arr[k, j].Text = "";
                                        _score += Int32.Parse(_arr[i, j].Text) * 2;
                                        label7.Text = _score.ToString();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                AddTwoNumbers();

            }
            ChangeColor(); // доделать смену цвета 
        }

        private void ChangeColor()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (_arr[i, j].Text == "")
                    {
                        _arr[i, j].BackColor = Color.Silver;
                    }
                 
                    else
                    {
                        if (Convert.ToUInt32(_arr[i, j].Text) >= 8 && Convert.ToUInt32(_arr[i, j].Text) <= 128)
                            _arr[i, j].BackColor = Color.Pink;
                        if (Convert.ToUInt32(_arr[i, j].Text) >= 256 && Convert.ToUInt32(_arr[i, j].Text) <= 512)
                            _arr[i, j].BackColor = Color.Green;
                        if (Convert.ToUInt32(_arr[i, j].Text) >= 1024)
                            _arr[i, j].BackColor = Color.Red;
                        if (Convert.ToUInt32(_arr[i, j].Text) < 8)
                            _arr[i, j].BackColor = Color.YellowGreen;
                    }
                }

        }
    }
    }


