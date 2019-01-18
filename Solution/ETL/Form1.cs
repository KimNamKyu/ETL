using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETL
{
    public partial class Form1 : Form
    {
        private string no;
        private MySqlConnection conn;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Click += btn_event;
            button2.Click += btn_event;
            button3.Click += btn_event;
            lv.DoubleClick += lv_event;
        }

        private void lv_event(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            ListView.SelectedListViewItemCollection itemGroup = lv.SelectedItems;
                    
            for(int i = 0; i< itemGroup.Count; i++)
            {
                ListViewItem item = itemGroup[i];
                no = item.SubItems[0].Text;
                MessageBox.Show(no);
            }
            Getlist();
        }

        private void btn_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "button1":
                    Development();

                    break;
                case "button2":
                    operation();
                    break;
                case "button3":
                    이행();
                    break;
                default:
                    break;
            }
        }

        private bool 이행()
        {
            MessageBox.Show("이행()");
            return true;
        }

        private bool Development()
        {
            try
            {
                MessageBox.Show("Development()");
                string strConnetion = string.Format("server={0}; user={1}; password={2}; database={3}", "192.168.3.115", "root", "1234", "test");

                conn = GetConnetion(strConnetion);
                if(conn == null)
                {
                    MessageBox.Show("접속 오류");
                    return false;
                }
                else
                {
                    lv.Items.Clear();
                    conn.Open();
                    MessageBox.Show("접속 성공");
                   
                    string sql = "show tables;";
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    MySqlDataReader sdr = comm.ExecuteReader();
                  
                    while (sdr.Read())
                    {
                        string[] arr = new string[sdr.FieldCount];
                        for (int i = 0; i < sdr.FieldCount; i++)
                        {
                            arr[i] = sdr.GetValue(i).ToString();
                        }
                        lv.Items.Add(new ListViewItem(arr));
                    }
                    sdr.Close();
                    conn.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool operation()
        {
            try
            {
                MessageBox.Show("operation()");
                string strConnetion = string.Format("server={0}; user={1}; password={2}; database={3}", "192.168.3.146", "root", "1234", "test");

                conn = GetConnetion(strConnetion);
                if (conn == null)
                {
                    MessageBox.Show("접속 오류");
                    return false;
                }
                MessageBox.Show("접속 성공");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private MySqlConnection GetConnetion(string strConnection)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = strConnection;
                return conn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void Getlist()
        {
            lv.Items.Clear();
            conn.Open();
            MessageBox.Show("접속 성공");

            string sql = string.Format("select * from {0};", no);
            MySqlCommand comm = new MySqlCommand(sql, conn);
            MySqlDataReader sdr = comm.ExecuteReader();

            while (sdr.Read())
            {
                string[] arr = new string[sdr.FieldCount];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    arr[i] = sdr.GetValue(i).ToString();
                }
                lv.Items.Add(new ListViewItem(arr));
            }
            sdr.Close();
            conn.Close();
        }
    }
}
