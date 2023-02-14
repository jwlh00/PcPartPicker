using PcPartPicker.Models;
using PcPartPicker.IService;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;

namespace PcPartPicker.Pages.PcPartPickerPages
{
    public partial class Builds : ComponentBase
    {
        [Inject] IBuildService BuildService { get; set; }
        [Inject] IImageService ImageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        //Syncfusion Parts
        public SfGrid<BuildImageModel>? BuildsGrid { get; set; }
        private List<ItemModel> Tools = new();
        public SfDialog DeleteDiag = null!;
        public SfDialog MsgDiag = null!;

        //Models

        public IEnumerable<BuildModel>? builds;
        public IEnumerable<ImageModel>? images;
        public IEnumerable<BuildImageModel>? buildImg;
        BuildModel Build = new BuildModel();

        //Variables
        public string buildID;
        public int count;

        protected override async Task OnInitializedAsync()
        {
            //builds = await BuildService.GetActiveBuilds();

            buildImg = await BuildService.GetBuildImage();
            images = await ImageService.GetAllImages();
            foreach(var build in buildImg)
            {
                foreach(var image in images)
                {
                    if (build.ImgName == image.filename)
                    {
                        build.Image = image;
                    }
                }
            }


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Save the build", PrefixIcon = "e-save" });
            Tools.Add(new ItemModel() { Text = "Delete", TooltipText = "Save the build", PrefixIcon = "e-save" });
        }

        public async Task ToolbarClickHandler(ClickEventArgs args)
        {
            //Handler of toolbar items
            if (args.Item.Text == "Add")
            {
                NavigationManager.NavigateTo("builder");
            }
            if (args.Item.Text == "Delete")
            {
                if (buildID == null)
                {
                    MsgDiag.ShowAsync();
                }
                else
                {
                    DeleteDiag.ShowAsync();
                }
            }
        }

        public void OnRecordDoubleClickHandler(RecordDoubleClickEventArgs<BuildImageModel> args)
        {
            NavigationManager.NavigateTo("builderParts/" + buildID);
        }

        public void RowSelectHandler(RowSelectEventArgs<BuildImageModel> args)
        {
            buildID = args.Data.Id;
        }

        public async Task DeleteClick()
        {
            await BuildService.DeleteBuild(buildID);
            await DeleteDiag.HideAsync();

            builds = await BuildService.GetBuilds();
            StateHasChanged();
        }

        public void CancelDiagClick()
        {
            MsgDiag.HideAsync();
            DeleteDiag.HideAsync();
        }
    }
}
