using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft;
using RestSharp;
using System.Windows;

namespace zipcode_to_address
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //ボタン1を押したら、郵便番号を読み込み、返答をテキストボックスに表示
        private void button1_Click(object sender, EventArgs e)
        {
            string zpcd = textBox1.Text;

            var client = new RestClient();
            var request = new RestRequest();
            client.BaseUrl = new Uri("http://zipcloud.ibsnet.co.jp/api/search");

            request.Method = Method.GET;
            request.AddParameter("zipcode", zpcd, ParameterType.GetOrPost);

            var response = client.Execute(request);
            string responsejson = response.Content.ToString();

            //デシリアライズ
            var adrs = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(responsejson);

            //表示と例外処理
            if (adrs.message != null)
            {
                MessageBox.Show("郵便番号が正しくありません");
            }
            else
            {
                textBox2.Text = adrs.results[0].zipcode;
                textBox3.Text = adrs.results[0].address1 + adrs.results[0].address2 + adrs.results[0].address3;
            }

        }

        //ボタン2を押したら、住所をクリップボードにコピー
        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox3.Text;
            if (text != "")
            {
                Clipboard.SetData(DataFormats.Text, text);
                MessageBox.Show("住所をクリップボードにコピーしました");
            }
        }

               
        //API返答のクラスを準備
        public class Rootobject
        {
            public object message { get; set; }
            public Result[] results { get; set; }
            public int status { get; set; }
        }

        public class Result
        {
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string kana1 { get; set; }
            public string kana2 { get; set; }
            public string kana3 { get; set; }
            public string prefcode { get; set; }
            public string zipcode { get; set; }
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
