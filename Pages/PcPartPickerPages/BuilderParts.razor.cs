using PcPartPicker.Models;
using PcPartPicker.IService;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;

namespace PcPartPicker.Pages.PcPartPickerPages
{
    public partial class BuilderParts: ComponentBase
    {
        [Inject] IBuildService BuildService { get; set; }

        //Parameters
        [Parameter] public string buildID { get; set; }

        //Syncfusion Parts
        public SfGrid<BuildModel>? BuilderGrid { get; set; }
        private List<ItemModel> Tools = new();
        public SfDialog PartViewerDiag;

        //Models

        public IEnumerable<BuildModel>? builder;
        BuildUserModel buildUser = new BuildUserModel();
        CPUModel CPU = new CPUModel();
        CPUCoolerModel CPUCooler = new CPUCoolerModel();
        CaseModel Case = new CaseModel();
        List<GPUModel> GPU = new List<GPUModel>();
        List<MemoryModel> Memory = new List<MemoryModel>();
        MotherboardModel Motherboard = new MotherboardModel();
        PSUModel PSU = new PSUModel();
        List<StorageModel> Storage = new List<StorageModel>();

        //Variables
        int cellIndex;
        string? part;
        string buildName;
        string userName;
        string userGender;

        protected override async Task OnInitializedAsync()
        {
            builder = await BuildService.GetBuildAsEnum(buildID);
            buildUser = await BuildService.GetBuildUser(buildID);

            foreach (var item in builder)
            {
                buildName = item.Name;
            }
            userName = buildUser.User.UserName;
            userGender = buildUser.User.Gender;


            Tools.Add(new ItemModel() { Text = "Add", TooltipText = "Add", PrefixIcon = "e-add" });
            Tools.Add(new ItemModel() { Text = "Remove", TooltipText = "Remove", PrefixIcon = "e-delete" });
        }

        public async Task ToolbarClickHandler(ClickEventArgs args)
        {
            //Handler of toolbar items
            if (args.Item.Text == "Add")
            {
                if (cellIndex == 0)
                {
                    //Selected CPU
                    part = "CPU";
                }
                if (cellIndex == 1)
                {
                    //Selected CPUCooler
                    part = "CPUCooler";
                }
                if (cellIndex == 2)
                {
                    //Selected Motherboard
                    part = "Motherboard";
                }
                if (cellIndex == 3)
                {
                    //Selected Memory
                    part = "Memory";
                }
                if (cellIndex == 4)
                {
                    //Selected Storage
                    part = "Storage";
                }
                if (cellIndex == 5)
                {
                    //Selected GPU
                    part = "GPU";
                }
                if (cellIndex == 6)
                {
                    //Selected Case
                    part = "Case";
                }
                if (cellIndex == 7)
                {
                    //Selected PSU
                    part = "PSU";
                }
                await PartViewerDiag.ShowAsync();
            }
            else if (args.Item.Text == "Remove")
            {
                if (cellIndex == 0)
                {
                    //Selected CPU
                    foreach(var part in builder)
                    {
                        part.CPU = CPU;
                        BuildService.UpdateBuild(part);
                    }
                }
                if (cellIndex == 1)
                {
                    //Selected CPUCooler
                    foreach (var part in builder)
                    {
                        part.CPUCooler = CPUCooler;
                        BuildService.UpdateBuild(part);
                    }
                }
                if (cellIndex == 2)
                {
                    //Selected Motherboard
                    foreach (var part in builder)
                    {
                        part.Motherboard = Motherboard;
                        BuildService.UpdateBuild(part);
                    }
                }
                if (cellIndex == 3)
                {
                    //Selected Memory
                    foreach (var part in builder)
                    {
                        part.Memory = Memory;
                        BuildService.UpdateBuild(part);
                    }
                }
                if (cellIndex == 4)
                {
                    //Selected Storage
                    foreach (var part in builder)
                    {
                        part.Storage = Storage;
                        BuildService.UpdateBuild(part);
                    }
                }
                if (cellIndex == 5)
                {
                    //Selected GPU
                    foreach (var part in builder)
                    {
                        part.GPU = GPU;
                        BuildService.UpdateBuild(part);
                    }
                }
                if (cellIndex == 6)
                {
                    //Selected Case
                    foreach (var part in builder)
                    {
                        part.Case = Case;
                        BuildService.UpdateBuild(part);
                    }
                }
                if (cellIndex == 7)
                {
                    //Selected PSU
                    foreach (var part in builder)
                    {
                        part.PSU = PSU;
                        BuildService.UpdateBuild(part);
                    }
                }
            }
        }

        public void CellSelectedHandler(CellSelectEventArgs<BuildModel> args)
        {
            cellIndex = (int)args.CellIndex;
        }

        #region ChosenMethods
        protected async void CPUChosen(CPUModel cpu)
        {
            foreach (var part in builder)
            {
                part.CPU.Name = cpu.Name;
                part.CPU.Brand = cpu.Brand;
                part.CPU.CoreCount = cpu.CoreCount;
                part.CPU.PerformanceCoreClock = cpu.PerformanceCoreClock;
                part.CPU.TDP = cpu.TDP;
                part.CPU.Price = cpu.Price;

                BuildService.UpdateBuild(part);
            }
            StateHasChanged();
        }

        protected async void CPUCoolerChosen(CPUCoolerModel cpuCooler)
        {
            foreach (var part in builder)
            {
                part.CPUCooler.Name = cpuCooler.Name;
                part.CPUCooler.Brand = cpuCooler.Brand;
                part.CPUCooler.Type = cpuCooler.Type;
                part.CPUCooler.Price = cpuCooler.Price;
                

                BuildService.UpdateBuild(part);
            }
            StateHasChanged();
        }

        protected async void MotherboardChosen(MotherboardModel motherboard)
        {
            foreach (var part in builder)
            {
                part.Motherboard.Name = motherboard.Name;
                part.Motherboard.Brand = motherboard.Brand;
                part.Motherboard.Socket = motherboard.Socket;
                part.Motherboard.Form = motherboard.Form;
                part.Motherboard.Price = motherboard.Price;


                BuildService.UpdateBuild(part);
            }
            StateHasChanged();
        }

        protected async void MemoryChosen(MemoryModel memory)
        {
            foreach (var part in builder)
            {
                part.Memory.Add(memory);

                BuildService.UpdateBuild(part);
            }
            StateHasChanged();
        }

        protected async void StorageChosen(StorageModel storage)
        {
            foreach (var part in builder)
            {
                part.Storage.Add(storage);

                BuildService.UpdateBuild(part);
            }
            StateHasChanged();
        }

        protected async void GPUChosen(GPUModel gpu)
        {
            foreach (var part in builder)
            {
                part.GPU.Add(gpu);

                BuildService.UpdateBuild(part);
            }
            StateHasChanged();
        }

        protected async void CaseChosen(CaseModel cases)
        {
            foreach (var part in builder)
            {
                part.Case.Name = cases.Name;
                part.Case.Brand = cases.Brand;
                part.Case.Type = cases.Type;
                part.Case.Price = cases.Price;


                BuildService.UpdateBuild(part);
            }
            StateHasChanged();
        }

        protected async void PSUChosen(PSUModel psu)
        {
            foreach (var part in builder)
            {
                part.PSU.Name = psu.Name;
                part.PSU.Brand = psu.Brand;
                part.PSU.Modular = psu.Modular;
                part.PSU.Rating = psu.Rating;
                part.PSU.Wattage = psu.Wattage;
                part.PSU.Price = psu.Price;


                BuildService.UpdateBuild(part);
            }
            StateHasChanged();
        }

        #endregion
    }
}
