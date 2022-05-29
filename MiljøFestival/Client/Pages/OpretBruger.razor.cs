using Microsoft.AspNetCore.Components.Forms;
using MiljøFestival.Shared.Models;

namespace MiljøFestival.Client.Pages
{
    public partial class OpretBruger
    {
        private Bruger BrugerModel = new Bruger();
        private EditContext EditContext;

        protected override void OnInitialized()
        {
            EditContext = new EditContext(BrugerModel);
        }
    }
}
