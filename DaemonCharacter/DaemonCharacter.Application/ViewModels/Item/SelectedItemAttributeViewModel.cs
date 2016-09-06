using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.ViewModels.Item
{
    public class SelectedItemAttributeViewModel
    {
        public Attributes Attribute { get; set; }

        public bool Selected { get; set; }

        public SelectedItemAttributeViewModel()
        {
            Selected = false;
        }
    }
}
