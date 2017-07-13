using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibData.Bussiness;
using TestWSVietinbank.ServiceVietinbank;
using System.Web;

namespace TestWSVietinbank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                txtResponse.Text = new EncryptAndDecrypt().Encrypt(txtResponse.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDencrypt_Click(object sender, EventArgs e)
        {
            try
            {
                txtResponse.Text = new EncryptAndDecrypt().Decrypt(txtResponse.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCallService_Click(object sender, EventArgs e)
        {
            try
            {
                VB2B_SMSSoapClient client = new VB2B_SMSSoapClient();
                client.Open();
                string xmlreq = txtRequest.Text;
                string res =  client.ReceiveMTRes(xmlreq);
                MessageBox.Show(res);
                txtResponse.Text = res;
                client.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEncodeHTML_Click(object sender, EventArgs e)
        {
            txtRequest.Text = HttpUtility.HtmlEncode(txtRequest.Text);
        }
        private void btnDecodeHTML_Click(object sender, EventArgs e)
        {
            txtRequest.Text = HttpUtility.HtmlDecode(txtRequest.Text);
        }
    }
}
