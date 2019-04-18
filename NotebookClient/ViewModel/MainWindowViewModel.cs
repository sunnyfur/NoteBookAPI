
using NotebookClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotebookClient.ViewModel
{
    public class MainWindowViewModel:BaseCl
    {
        const string _baseAddress = "http://localhost:54619/";
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        RelayCommand updateCommand;

        ObservableCollection<Person> people;
        public ObservableCollection<Person> People
        {
            get { return people; }
            set
            {
                people = value;
                OnPropertyChanged("People");
            }
        }


        public MainWindowViewModel()
        {
           
        }
            
         public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                (updateCommand = new RelayCommand((o) =>
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_baseAddress);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response;

                        response = client.GetAsync("api/People").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var res = response.Content.ReadAsAsync<ObservableCollection<Person>>().Result;

                            People = res;
                        }
                    }

                }));
            }
        }






      

    }
}
