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
    public partial class GPUsView : ComponentBase
    {
        [Inject] IGPUService GPUService { get; set; }

        //Syncfusion Parts
        public SfGrid<GPUModel>? PartsGrid { get; set; }
        private List<ItemModel> Tools = new();

        //Models
        public IEnumerable<GPUModel>? Parts;
        GPUModel Part = new GPUModel();

        [Parameter]
        public EventCallback<GPUModel> OnPartAdd { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parts = await GPUService.GetGpusByValue();


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
        }

        public async void RowSelection(RowSelectEventArgs<GPUModel> args)
        {
            Part.Name = args.Data.Name;
            Part.Brand = args.Data.Brand;
            Part.Chipset = args.Data.Chipset;
            Part.Memory = args.Data.Memory;
            Part.Price = args.Data.Price;
        }

        public async void PartAdd()
        {
            await OnPartAdd.InvokeAsync(Part);
        }
    }
}
