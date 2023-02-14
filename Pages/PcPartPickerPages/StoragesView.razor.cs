using Microsoft.AspNetCore.Components;
using PcPartPicker.IService;
using PcPartPicker.Models;
using PcPartPicker.Service;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;

namespace PcPartPicker.Pages.PcPartPickerPages
{
    public partial class StoragesView : ComponentBase
    {
        [Inject] IStorageService StorageService { get; set; }

        //Syncfusion Parts
        public SfGrid<StorageModel>? PartsGrid { get; set; }
        private List<ItemModel> Tools = new();

        //Models
        public IEnumerable<StorageModel>? Parts;
        StorageModel Part = new StorageModel();

        [Parameter]
        public EventCallback<StorageModel> OnPartAdd { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parts = await StorageService.GetStoragesByValue();


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
        }

        public async void RowSelection(RowSelectEventArgs<StorageModel> args)
        {
            Part.Name = args.Data.Name;
            Part.Brand = args.Data.Brand;
            Part.Type = args.Data.Type;
            Part.Capacity = args.Data.Capacity;
            Part.Price = args.Data.Price;
        }

        public async void PartAdd()
        {
            await OnPartAdd.InvokeAsync(Part);
        }

    }
}
