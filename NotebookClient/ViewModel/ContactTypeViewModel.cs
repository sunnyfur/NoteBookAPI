using NotebookClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotebookClient.ViewModel
{
   public class ContactTypeViewModel
    {
        public ContactType Model
        {
            get; set;
        }

        public ContactTypeViewModel(ContactType contactType )
        {
            Model = contactType;

        }
    }
}
