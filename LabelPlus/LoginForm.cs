using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LabelPlus.APIManager;

namespace LabelPlus
{
    public partial class LoginForm : Form
    {
        public string ApiUrl
        {
            get { return apiTextBox.Text.Trim(); }
            set {  apiTextBox.Text = value; }
        }
        public string UserName
        {
            get { return userNameText.Text.Trim(); }
            set { userNameText.Text = value; }
        }
        public string Password
        {
            get { return passwordText.Text.Trim(); }
            set { passwordText.Text = value; }
        }

        private APIManager _apiManager;
        private string _captchaId = string.Empty;
        public delegate void LoginSuccessCallback();
        public event LoginSuccessCallback OnLoginSuccess;

        public LoginForm(APIManager manager)
        {
            InitializeComponent();
            Language.InitFormLanguage(this, StringResources.GetValue("lang"));

            _apiManager = manager;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(_captchaId))
            {
                MessageBox.Show("Please get captcha image first.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(string.IsNullOrEmpty(userNameText.Text.Trim()))
            {
                MessageBox.Show("Please enter your User Name.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(string.IsNullOrEmpty(passwordText.Text.Trim()))
            {
                MessageBox.Show("Please enter your Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(string.IsNullOrEmpty(captchaText.Text.Trim()))
            {
                MessageBox.Show("Please enter captcha.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var token = await _apiManager.PostAsync<TokenResponse>("/user/token", new TokenRequest
                {
                    email = userNameText.Text.Trim(),
                    password = passwordText.Text.Trim(),
                    captcha = captchaText.Text.Trim(),
                    captcha_info = _captchaId
                });
                if (token != null)
                {
                    _apiManager.UserToken = token.token;
                    var userInfo = await _apiManager.GetAsync<UserInfoResponse>("/user/info");
                    if (userInfo != null) 
                    {
                        _apiManager.UserName = userInfo.name;
                        MessageBox.Show(string.Format("Welcome back, {0}!", _apiManager.UserName), "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    OnLoginSuccess?.Invoke();
                    Close();
                }
                else
                {
                    MessageBox.Show("Can not get token.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadCaptchaImage();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(apiTextBox.Text))
            {
                MessageBox.Show("Please enter API URL first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _apiManager.APIBaseURL = apiTextBox.Text.Trim();

            LoadCaptchaImage();
        }

        public async void LoadCaptchaImage()
        {
            userNameText.Enabled = false;
            passwordText.Enabled = false;
            captchaText.Enabled = false;
            connectAPIButton.Enabled = false;
            loginButton.Enabled = false;

            try
            {
                var captcha = await _apiManager.PostAsync<CaptchaResponse>("/captchas");
                _captchaId = captcha.info;
                captchaPic.SizeMode = PictureBoxSizeMode.AutoSize;
                if (captchaPic.Image != null)
                {
                    captchaPic.Image.Dispose();
                }
                captchaPic.Image = LoadBase64Image(captcha.image);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load captcha failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                userNameText.Enabled = true;
                passwordText.Enabled = true;
                captchaText.Enabled = true;
                connectAPIButton.Enabled = true;
                loginButton.Enabled = true;
            }
        }


        public static Image LoadBase64Image(string dataUri)
        {
            int commaIndex = dataUri.IndexOf(',');
            string base64 = commaIndex >= 0
                ? dataUri.Substring(commaIndex + 1)
                : dataUri;

            byte[] imageBytes = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
