using PcPartPicker.Models;
using PcPartPicker.IService;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.PivotView;

namespace PcPartPicker.Pages.PcPartPickerPages
{
    public partial class UserAddEdit : ComponentBase
    {
        [Inject] IBuildService BuildService { get; set; }
        [Inject] IUserService UserService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        //Parameters
        [Parameter] public string userID { get; set; }

        //Syncfusion Parts
        public SfDialog AddEditDiag;

        //Models
        public UserModel user = new UserModel();

        //Variables
        public string title;

        protected override async Task OnInitializedAsync()
        {

            if (userID == "add")
            {
                title = "Add Production";
                
            }
            else
            {
                title = "Edit Production";
                user = await UserService.GetUser(userID);
            }
        }

        public async Task SubmitForm()
        {
            if (userID == "add")
            {
                user.IsUserActive = true;
                await UserService.CreateUser(user);
                NavigationManager.NavigateTo("users");
            }
            else
            {
                await UserService.UpdateUser(user);
                NavigationManager.NavigateTo("users");
            }
        }

        public void CancelClick()
        {
            NavigationManager.NavigateTo("users");
        }

        public void CloseDiag()
        {
            NavigationManager.NavigateTo("users");
        }
    }
}
