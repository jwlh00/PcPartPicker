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
    public partial class PSUsView : ComponentBase
    {
        [Inject] IPSUService PSUService { get; set; }

        //Syncfusion Parts
        public SfGrid<PSUModel>? PartsGrid { get; set; }
        private List<ItemModel> Tools = new();

        //Models
        public IEnumerable<PSUModel>? Parts;
        PSUModel Part = new PSUModel();

        [Parameter]
        public EventCallback<PSUModel> OnPartAdd { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parts = await PSUService.GetPsusByValue();


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
        }

        public async void RowSelection(RowSelectEventArgs<PSUModel> args)
        {
            Part.Name = args.Data.Name;
            Part.Brand = args.Data.Brand;
            Part.Modular = args.Data.Modular;
            Part.Rating = args.Data.Rating;
            Part.Wattage = args.Data.Wattage;
            Part.Price = args.Data.Price;
        }

        public async void PartAdd()
        {
            await OnPartAdd.InvokeAsync(Part);
        }

    }
}
