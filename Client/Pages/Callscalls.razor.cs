using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace EspoNew.Client.Pages
{
    public partial class Callscalls
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public EspoDbNewService EspoDbNewService { get; set; }

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCall> callscalls;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CallsCall> grid0;
        protected int count;

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCallscalls(filter: $"{args.Filter}", expand: "account", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                callscalls = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Callscalls" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCallsCall>("Add CallsCall", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.CallsCall> args)
        {
            await DialogService.OpenAsync<EditCallsCall>("Edit CallsCall", new Dictionary<string, object> { {"call_id", args.Data.call_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CallsCall callsCall)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCallsCall(callId:callsCall.call_id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CallsCall"
                });
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.CallsCall callsCallChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.CallsCall args)
        {
            callsCallChild = args;
        }
    }
}