using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithConnectAnimation
{
    public class Model : INotifyPropertyChanged
    {


        private string title;
        private string content;
        public string Title
        {
            get { return title; }

            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        public string Content
        {
            get { return content; }

            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            
        }
    }
}
