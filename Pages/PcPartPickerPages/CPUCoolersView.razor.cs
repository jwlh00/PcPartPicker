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
    public partial class CPUCoolersView : ComponentBase
    {
        [Inject] ICPUCoolerService CPUCoolerService { get; set; }

        //Syncfusion Parts
        public SfGrid<CPUCoolerModel>? PartsGrid { get; set; }
        private List<ItemModel> Tools = new();

        //Models
        public IEnumerable<CPUCoolerModel>? Parts;
        CPUCoolerModel Part = new CPUCoolerModel();

        [Parameter]
        public EventCallback<CPUCoolerModel> OnPartAdd { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parts = await CPUCoolerService.GetCpuCoolersByValue();


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
        }

        public async void RowSelection(RowSelectEventArgs<CPUCoolerModel> args)
        {
            Part.Name = args.Data.Name;
            Part.Brand = args.Data.Brand;
            Part.Type = args.Data.Type;
            Part.Price = args.Data.Price;
        }

        public async void PartAdd()
        {
            await OnPartAdd.InvokeAsync(Part);
        }

    }
}
