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
    public partial class MotherboardsView : ComponentBase
    {
        [Inject] IMotherboardService MotherboardService { get; set; }

        //Syncfusion Parts
        public SfGrid<MotherboardModel>? PartsGrid { get; set; }
        private List<ItemModel> Tools = new();

        //Models
        public IEnumerable<MotherboardModel>? Parts;
        MotherboardModel Part = new MotherboardModel();

        [Parameter]
        public EventCallback<MotherboardModel> OnPartAdd { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parts = await MotherboardService.GetMotherboardsByValue();


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
        }

        public async void RowSelection(RowSelectEventArgs<MotherboardModel> args)
        {
            Part.Name = args.Data.Name;
            Part.Brand = args.Data.Brand;
            Part.Socket = args.Data.Socket;
            Part.Form = args.Data.Form;
            Part.Price = args.Data.Price;
        }

        public async void PartAdd()
        {
            await OnPartAdd.InvokeAsync(Part);
        }

    }
}
