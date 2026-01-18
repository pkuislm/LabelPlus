using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LabelPlus
{
    public class APIManager
    {
        private string _user = "LabelPlusUser";
        private string _password = "";
        private string _token = string.Empty;
        private string _api = "https://api.moetran.com/v1/";
        private string _website = "https://www.moetran.com";
        private readonly HttpClient _client = new HttpClient();
        private readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();
        public string UserName
        {
            get { return _user; }
            set { _user = value; }
        }

        public string APIBaseURL
        {
            get { return _api; }
            set { _api = value; }
        }

        public string UserToken
        {
            get { return _token; }
            set
            {
                _token = value;
                if (!string.IsNullOrEmpty(_token))
                {
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                }
                else
                {
                    _client.DefaultRequestHeaders.Authorization = null;
                }
            }
        }

        public APIManager()
        {
        }

        public void Init()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //setup token if exists
            if(File.Exists("moetran_token.bin"))
            {
                try
                {
                    var tokenFile = File.ReadAllBytes("moetran_token.bin");
                    for(int i=0; i < tokenFile.Length; i++)
                    {
                        tokenFile[i] ^= 0x5A;
                    }
                    using(var ms = new MemoryStream(tokenFile))
                    using (var br = new BinaryReader(ms))
                    {
                        APIBaseURL = br.ReadString();
                        UserName = br.ReadString();
                        UserToken = br.ReadString();
                    }
                }
                catch
                {
                    //ignore
                }
            }
            _client.DefaultRequestHeaders.Referrer = new Uri(_website);//CROS
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/143.0.0.0 Safari/537.36");
        }

        //Though Httpclient provides BaseAddress field, but it seems not support path in it (like "example.com/v1/")
        private string HandlePath(string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }
            return APIBaseURL + url;
        }

        public void Save()
        {
            try
            {
                using (var ms = new MemoryStream())
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(APIBaseURL);
                    bw.Write(UserName);
                    bw.Write(UserToken);
                    bw.Flush();
                    var tokenFile = ms.ToArray();
                    for (int i = 0; i < tokenFile.Length; i++)
                    {
                        tokenFile[i] ^= 0x5A;
                    }
                    File.WriteAllBytes("moetran_token.bin", tokenFile);
                }
            }
            catch
            {
                //ignore
            }
        }

        public void Login(Action loginSuccessAction)
        {
            _client.DefaultRequestHeaders.Clear();
            var login = new LoginForm(this);
            login.OnLoginSuccess += () =>
            {
                Save();
                loginSuccessAction?.Invoke();
            };
            login.ApiUrl = APIBaseURL;
            login.UserName = _user;
            login.Password = _password;
            login.Show();
        }

        public void Logout()
        {
            UserName = "LabelPlusUser";
            UserToken = "";
            Save();
        }

        public Task<byte[]> GetByteArrayAsync(string url) => _client.GetByteArrayAsync(HandlePath(url));
        public Task<HttpResponseMessage> DeleteAsync(string url) => _client.DeleteAsync(HandlePath(url));
        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content) => _client.PutAsync(HandlePath(url), content);

        public async Task<T> PostAsync<T>(string path, object arg = null)
        {
            var content = arg != null
                ? arg is HttpContent ? arg as HttpContent : new StringContent(SerializeJson(arg), Encoding.UTF8, "application/json")
                : null;

            HttpResponseMessage response;
            try
            {
                response = await _client.PostAsync(HandlePath(path), content);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("HTTP request failed", ex);
            }

            var text = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return DeserializeJson<T>(text);
            }

            var error = DeserializeJson<ErrorResponse>(text);
            var message = error?.message?.FirstOrDefault().Value?[0] ?? "Unknown error";

            throw new ApiException(message, error, response.StatusCode);
        }

        public async Task<T> GetAsync<T>(string path)
        {
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(HandlePath(path));
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("HTTP request failed", ex);
            }
            var text = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return DeserializeJson<T>(text);
            }
            var error = DeserializeJson<ErrorResponse>(text);
            var message = error?.message?.FirstOrDefault().Value?[0] ?? "Unknown error";
            throw new ApiException(message, error, response.StatusCode);
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(UserToken);
        }

        public class CaptchaResponse
        {
            public string image { get; set; }
            public string info { get; set; }
        }

        public class TokenRequest
        {
            public string email { get; set; }
            public string password { get; set; }
            public string captcha { get; set; }
            public string captcha_info { get; set; }
        }

        public class TokenResponse
        {
            public string token { get; set; }
        }

        public class UserInfoResponse
        {
            public bool admin { get; set; }
            public bool has_avatar { get; set; }
            public string avatar { get; set; }
            public string id { get; set; }
            public string signature { get; set; }
            public string name { get; set; }
        }

        public class Team
        {
            public string id { get; set; }
            public string name { get; set; }
            public string avatar { get; set; }
            public int user_count { get; set; }
        }

        public class ProjectSet
        {
            public string id { get; set; }
            public string name { get; set; }
            public string team_id { get; set; }
            public int? project_count { get; set; }
        }

        public class Project
        {
            public string id { get; set; }
            public string name { get; set; }
            public int source_count { get; set; }
            public int translated_source_count { get; set; }
            public int checked_source_count { get; set; }
        }

        public class ProjectFile
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string url { get; set; }
            public string cover_url { get; set; }
            public int source_count { get; set; }
            public int translated_source_count { get; set; }
            public int checked_source_count { get; set; }
        }

        public class ProjectTarget
        {
            public string id { get; set; }
            public ProjectLanguage language { get; set; }
            public int checked_source_count{ get; set; }
            public int translated_source_count { get; set; }
        }

        public class ProjectLanguage
        {
            public string id { get; set; }
            public string en_name { get; set; }
            public string i18n_name { get; set; }
            public string lo_name { get; set; }

            public override string ToString()
            {
                return en_name;
            }
        }

        public class TranslationSource
        {
            public string id{ get; set; }
            public int position_type { get; set; }
            public List<TranslationSourceItem> translations { get; set; }
            public double x { get; set; }
            public double y { get; set; }
        }

        public class TranslationSourceRequest
        {
            public int position_type { get; set; }
            public double x { get; set; }
            public double y { get; set; }
        }

        public class TranslationSourceResponse
        {
            public string id{ get; set; }
            public int position_type { get; set; }
            public double x { get; set; }
            public double y { get; set; }
        }

        public class TranslationSourceItem
        {
            public string id { get; set; }
            public string content { get; set; }
            public string proofread_content { get; set; }
        }

        public class TranslationSourceItemRequest
        {
            public string target_id { get; set; }
            public string content { get; set; }
        }

        public class NewProjectRequest
        {
            public int allow_apply_type { get; set; } = 3;
            public int application_check_type { get; set; } = 1;
            public string default_role { get; set; } = "63d87c24b8bebd75ff934269";
            public string source_language { get; set; } = "ja";
            public List<string> target_languages { get; set; } = new List<string>() { "zh-CN" };
            public string intro { get; set; }
            public string name { get; set; }
            public string project_set { get; set; }
        }

        public class NewProjectResponse
        {
            public string message { get; set; }
            public Project project { get; set; }
        }

        public class ErrorResponse
        {
            public int code { get; set; }
            public string error { get; set; }
            public Dictionary<string, string[]> message { get; set; }
        }

        public T DeserializeJson<T>(string jsonString)
        {
            return _serializer.Deserialize<T>(jsonString);
        }

        public string SerializeJson(object obj)
        {
            return _serializer.Serialize(obj);
        }

        public class ApiException : Exception
        {
            public HttpStatusCode StatusCode { get; }
            public ErrorResponse Response { get; }

            public ApiException(string message, ErrorResponse resp, HttpStatusCode statusCode)
                : base(message)
            {
                StatusCode = statusCode;
                Response = resp;
            }
        }
    }
}
