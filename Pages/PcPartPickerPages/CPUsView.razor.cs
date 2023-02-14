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
    public partial class CPUsView : ComponentBase
    {
        [Inject] ICPUService CPUService { get; set; }

        //Syncfusion Parts
        public SfGrid<CPUModel>? PartsGrid { get; set; }
        private List<ItemModel> Tools = new();

        //Models
        public IEnumerable<CPUModel>? Parts;
        CPUModel Part = new CPUModel();

        [Parameter]
        public EventCallback<CPUModel> OnPartAdd { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parts = await CPUService.GetCpusByValue();


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
        }

        public async void RowSelection(RowSelectEventArgs<CPUModel> args)
        {
            Part.Name = args.Data.Name;
            Part.Brand = args.Data.Brand;
            Part.CoreCount = args.Data.CoreCount;
            Part.PerformanceCoreClock = args.Data.PerformanceCoreClock;
            Part.TDP = args.Data.TDP;
            Part.Price = args.Data.Price;
        }

        public async void PartAdd()
        {
            await OnPartAdd.InvokeAsync(Part);
        }

    }
}
