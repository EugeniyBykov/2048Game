using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form3 : Form
    {
        List<string> _playerName;
        List<string> _bestScore;
        List<Label> _lList;
        List<Label> _lList1;
     

        public Form3(List<string> a , List<string> b)
        {
            InitializeComponent();
            
            _lList = new List<Label>();
            _lList1 = new List<Label>();
            _playerName = a;
            _bestScore = b;
            AddLables();
         
            for (int i = 0; i < 5; i++)
            {      
                    _lList[i].Text = _playerName[i];
                    _lList1[i].Text = _bestScore[i];
            }

            Save(); 
            

        }

        public void AddLables()
        {
            _lList.Add(l1);
            _lList.Add(l2);
            _lList.Add(l3);
            _lList.Add(l4);
            _lList.Add(l5);
            _lList1.Add(l6);
            _lList1.Add(l7);
            _lList1.Add(l8);
            _lList1.Add(l9);
            _lList1.Add(l10);
        }

        private void Save()
        {
            string tmp = Directory.GetCurrentDirectory();
            string path = tmp + @"\data.dat";
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {

                    for (int i = 0; i < 5; i++)
                    {
                        if (_bestScore[i] != 0.ToString())
                        {
                            writer.Write(_playerName[i]);
                            writer.Write(_bestScore[i]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
