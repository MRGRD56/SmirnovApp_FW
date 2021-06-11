using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmirnovApp.Common;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.ViewModels.PagesViewModels
{
    public class EstatesPageViewModel : ItemsListViewModel<Estate>, ICrudViewModel
    {
        public EstatesPageViewModel() : base(typeof(Owner))
        {

        }

        public ICommand AddCommand => new Command(parameter =>
        {

        });

        public ICommand EditCommand => new Command(parameter =>
        {

        }, parameter => SelectedItem != null);

        public ICommand RemoveCommand => new Command(parameter =>
        {

        }, parameter => SelectedItem != null);
    }
}
