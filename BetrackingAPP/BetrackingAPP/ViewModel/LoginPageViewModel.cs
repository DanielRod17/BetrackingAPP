using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BetrackingAPP.Models;
using BetrackingAPP.Views;
using Xamarin.Forms;
using PCLCrypto;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.AppCenter;

namespace BetrackingAPP.ViewModel
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        //private bool _happened;
        public string sn { get; set; }
        public string hash { get; set; }

        private bool _happened;
        public bool HasPropertyValueChanged
        {
            get => _happened;
            set
            {
                _happened = value;
                OnPropertyChanged();
            }
        }

        private INavigation Navigation;
        public Command MainPageCommand { get; set; }

        private User _dataServicio;
        public User usuarioBT
        {
            get { return _dataServicio; }
            set
            {
                _dataServicio = value;
                OnPropertyChanged();
            }
        }

        public LoginPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            LoginCommand = new Command(async () => await NavigateToMainPage());
            MainPageCommand = new Command(async () => await NavigateToMainPage());
        }

        public Command LoginCommand { get; set; }

        public async Task NavigateToMainPage()
        {

            HasPropertyValueChanged = true;
            var contra = CalculateSha1Hash(hash);
            try
            {
                var client = new HttpClient();
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("sn", sn),
                    new KeyValuePair<string, string>("con", contra),
                });

                var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/login.php", formContent);
                if (result.IsSuccessStatusCode)
                {
                    var responseData = await result.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    var elemeneto =     JToken.Parse(responseData);
                    if (elemeneto[0].ToString() == "Correct")
                    {
                        var mensageJson = JsonConvert.DeserializeObject<User>(elemeneto[1].ToString(), settings);
                        System.Diagnostics.Debug.WriteLine(mensageJson);
                        usuarioBT = mensageJson;
                        var _payroll = "US";
                        if (usuarioBT.Payroll == "142")
                        {
                            _payroll = "MX";
                        }
                        CustomProperties properties = new CustomProperties();
                        properties.Set("Payroll", _payroll);
                        AppCenter.SetUserId(usuarioBT.Id.ToString());
                        AppCenter.SetCustomProperties(properties);
                        await Navigation.PushAsync(new MainPage(usuarioBT));
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Oops", elemeneto[1].ToString(), "OK");
                        HasPropertyValueChanged = false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong", "OK");
                    HasPropertyValueChanged = false;
                }
            }
            catch (HttpRequestException e)
            {
                await Application.Current.MainPage.DisplayAlert("Oops", e.Message, "OK");
            }
        }

        private static string CalculateSha1Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = hasher.HashData(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X"));
            }
            return sb.ToString();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}