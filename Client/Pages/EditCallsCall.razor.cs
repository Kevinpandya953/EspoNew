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
    public partial class EditCallsCall
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

        [Parameter]
        public string call_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            callsCall = await EspoDbNewService.GetCallsCallByCallId(callId:call_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.CallsCall callsCall;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountId;


        protected int accountsaccountsForaccountIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdValue;
        protected async Task accountsaccountsForaccountIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(account_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountId = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCount = result.Count;

                if (!object.Equals(callsCall.account_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAccountsaccounts(filter: $"account_id eq '{callsCall.account_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        accountsaccountsForaccountIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateCallsCall(callId:call_id, callsCall);
                DialogService.Close(callsCall);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }





        bool hasaccount_idValue;

        [Parameter]
        public string account_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            callsCall = new EspoNew.Server.Models.EspoDbNew.CallsCall();

            hasaccount_idValue = parameters.TryGetValue<string>("account_id", out var hasaccount_idResult);

            if (hasaccount_idValue)
            {
                callsCall.account_id = hasaccount_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}