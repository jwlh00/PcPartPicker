using PcPartPicker.Models;
using PcPartPicker.IService;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using PcPartPicker.Service;
using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.PivotView;
using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Bson;

namespace PcPartPicker.Pages.PcPartPickerPages
{
    public partial class BuilderView : ComponentBase
    {
        [Inject] IBuildService BuildService { get; set; }
        [Inject] IUserService UserService { get; set; }
        [Inject] IImageService ImageService { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }


        //Models

        public IEnumerable<UserModel>? users;
        ImageModel image = new ImageModel();
        private IList<string> imageDataUrls = new List<string>();
        BuildModel Build = new BuildModel();

        CPUModel CPU = new CPUModel();
        CPUCoolerModel CPUCooler = new CPUCoolerModel();
        CaseModel Case = new CaseModel();
        List<GPUModel> GPU = new List<GPUModel>();
        List<MemoryModel> Memory = new List<MemoryModel>();
        MotherboardModel Motherboard = new MotherboardModel();
        PSUModel PSU = new PSUModel();
        List<StorageModel> Storage = new List<StorageModel>();

        protected override async Task OnInitializedAsync()
        {
            users = await UserService.GetActiveUsers();
        }

        public async Task SubmitForm()
        {

            Build.ImgName = image.filename;
            await ImageService.SaveImage(image);

            Build.CPU = CPU;
            Build.CPUCooler = CPUCooler;
            Build.Case = Case;
            Build.GPU = GPU;
            Build.Memory = Memory;
            Build.Motherboard = Motherboard;
            Build.PSU = PSU;
            Build.Storage = Storage;
            Build.IsActive = true;

            await BuildService.CreateBuild(Build);
            NavigationManager.NavigateTo("/builds");
        }

        private async Task SaveImg(InputFileChangeEventArgs e)
        {
            var format = "image/png";
            var imageFile = e.File;
            var resizedFile = await imageFile.RequestImageFileAsync(format, 200, 200);
            var buffer = new byte[resizedFile.Size];
            await resizedFile.OpenReadStream().ReadAsync(buffer);
            var imageDataUrl = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
            imageDataUrls.Add(imageDataUrl);
            image.Content = buffer;
            image.filename = imageFile.Name;
        }

        public void CancelClick()
        {
            Build.Name = "";
            Build.UsernameId= null;
            StateHasChanged();
        }
    }
}
