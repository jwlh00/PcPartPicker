using PcPartPicker.Models;
using PcPartPicker.IService;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;

namespace PcPartPicker.Pages.PcPartPickerPages
{
    public partial class Users : ComponentBase
    {
        [Inject] IBuildService BuildService { get; set; }
        [Inject] IUserService UserService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        //Syncfusion Parts
        public SfGrid<UserModel>? UsersGrid { get; set; }
        private List<ItemModel> Tools = new();
        public SfDialog DeleteDiag = null!;
        public SfDialog MsgDiag = null!;

        //Models
        public IEnumerable<UserModel>? users;

        //Variables
        public string? userID = null;
        public string msg;
        private bool? isChecked = null;

        protected override async Task OnInitializedAsync()
        {
            users = await UserService.GetActiveUsers();

            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
            Tools.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit", PrefixIcon = "e-edit" });
            Tools.Add(new ItemModel() { Text = "Delete", TooltipText = "Delete", PrefixIcon = "e-delete" });
        }

        public void ToolbarClickHandler(ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                NavigationManager.NavigateTo("useraddedit/add");
            }
            if (args.Item.Text == "Edit")
            {
                if (userID == null)
                {
                    msg = "You need to select a user first before trying to edit it";
                    MsgDiag.ShowAsync();
                }
                else
                {
                    NavigationManager.NavigateTo("useraddedit/" + userID);
                }
            }
            if (args.Item.Text == "Delete")
            {
                if (userID == null)
                {
                    msg = "You need to select a user first before trying to delete it";
                    MsgDiag.ShowAsync();
                }
                else
                {
                    DeleteDiag.ShowAsync();
                }
            }
        }

        public void RowSelectHandler(RowSelectEventArgs<UserModel> args)
        {
            userID = args.Data.Id;
        }

        public async Task DeleteClick()
        {
            await UserService.DeleteUser(userID);
            await DeleteDiag.HideAsync();

            users = await UserService.GetActiveUsers();
            StateHasChanged();
        }

        public void CancelDiagClick()
        {
            MsgDiag.HideAsync();
            DeleteDiag.HideAsync();
        }

        public async void OnBulkAdd()
        {
            UserService.BulkAdd();
            if (isChecked == true)
            {
                users = await UserService.GetUsers();
            }
            else
            {
                users = await UserService.GetActiveUsers();
            }
        }

        public async void OnBulkDelete()
        {
            UserService.BulkDeleteNotActive();
            if (isChecked == true)
            {
                users = await UserService.GetUsers();
            }
            else
            {
                users = await UserService.GetActiveUsers();
            }
        }

        public async void OnActive()
        {
            UserService.BulkChangeActivity(true);
            if (isChecked == true)
            {
                users = await UserService.GetUsers();
            }
            else
            {
                users = await UserService.GetActiveUsers();
            }
        }

        public async void OnInactive()
        {
            UserService.BulkChangeActivity(false);
            if (isChecked == true)
            {
                users = await UserService.GetUsers();
            }
            else
            {
                users = await UserService.GetActiveUsers();
            }
        }

        private async void Change(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool?> args)
        {
            if(isChecked == true)
            {
                users = await UserService.GetUsers();
            }
            else
            {
                users = await UserService.GetActiveUsers();
            }
        }
    }
}
