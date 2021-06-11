using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class EstatesPageViewModel : ItemsListViewModel<Estate>, ICrudViewModel
    {
        public EstatesPageViewModel() : base(typeof(Owner))
        {

        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }
    }
}
