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
    public partial class CasesView : ComponentBase
    {
        [Inject] ICaseService CaseService { get; set; }

        //Syncfusion Parts
        public SfGrid<CaseModel>? PartsGrid { get; set; }
        private List<ItemModel> Tools = new();

        //Models
        public IEnumerable<CaseModel>? Parts;
        CaseModel Part = new CaseModel();

        [Parameter]
        public EventCallback<CaseModel> OnPartAdd { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Parts = await CaseService.GetCasesByValue();


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
        }

        public async void RowSelection(RowSelectEventArgs<CaseModel> args)
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
