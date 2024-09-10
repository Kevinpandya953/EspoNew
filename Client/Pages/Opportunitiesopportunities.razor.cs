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
    public partial class Opportunitiesopportunities
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> opportunitiesopportunities;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> grid0;
        protected int count;

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetOpportunitiesopportunities(filter: $"{args.Filter}", expand: "account,contact,campaign", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                opportunitiesopportunities = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Opportunitiesopportunities" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddOpportunitiesOpportunity>("Add OpportunitiesOpportunity", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> args)
        {
            await DialogService.OpenAsync<EditOpportunitiesOpportunity>("Edit OpportunitiesOpportunity", new Dictionary<string, object> { {"opportunity_id", args.Data.opportunity_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunity)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteOpportunitiesOpportunity(opportunityId:opportunitiesOpportunity.opportunity_id);

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
                    Detail = $"Unable to delete OpportunitiesOpportunity"
                });
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunityChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity args)
        {
            opportunitiesOpportunityChild = args;
        }
    }
}