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
    public partial class MemoriesView : ComponentBase
    {
        [Inject] IMemoryService MemoryService { get; set; }

        //Syncfusion Parts
        public SfGrid<MemoryModel>? PartsGrid { get; set; }
        private List<ItemModel> Tools = new();

        //Models
        public IEnumerable<MemoryModel>? Parts;
        MemoryModel Part = new MemoryModel();

        [Parameter]
        public EventCallback<MemoryModel> OnPartAdd { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parts = await MemoryService.GetMemoriesByValue();


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
        }

        public async void RowSelection(RowSelectEventArgs<MemoryModel> args)
        {
            Part.Name = args.Data.Name;
            Part.Brand = args.Data.Brand;
            Part.Speed = args.Data.Speed;
            Part.CASLatency = args.Data.CASLatency;
            Part.Price = args.Data.Price;
        }

        public async void PartAdd()
        {
            await OnPartAdd.InvokeAsync(Part);
        }

    }
}
