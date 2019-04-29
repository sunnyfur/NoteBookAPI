using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotebookClient.Model

{
    public class ContactType :BaseCl
    {
        public int Id { get; set; }
        string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public override string ToString()
        {
            return Title;
        }
    }
}