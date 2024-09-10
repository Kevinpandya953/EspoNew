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
    public partial class Meetingsmeetings
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> meetingsmeetings;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> grid0;
        protected int count;

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetMeetingsmeetings(filter: $"{args.Filter}", expand: "account", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                meetingsmeetings = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Meetingsmeetings" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddMeetingsMeeting>("Add MeetingsMeeting", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> args)
        {
            await DialogService.OpenAsync<EditMeetingsMeeting>("Edit MeetingsMeeting", new Dictionary<string, object> { {"meeting_id", args.Data.meeting_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsMeeting)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteMeetingsMeeting(meetingId:meetingsMeeting.meeting_id);

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
                    Detail = $"Unable to delete MeetingsMeeting"
                });
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsMeetingChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting args)
        {
            meetingsMeetingChild = args;
        }
    }
}