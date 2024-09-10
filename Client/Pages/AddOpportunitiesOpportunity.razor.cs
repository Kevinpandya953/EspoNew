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
    public partial class AddOpportunitiesOpportunity
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

        protected override async Task OnInitializedAsync()
        {
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunity;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> campaigncampaignsForcampaignId;


        protected int accountsaccountsForaccountIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdValue;
        protected async Task accountsaccountsForaccountIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(account_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountId = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCount = result.Count;

                if (!object.Equals(opportunitiesOpportunity.account_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAccountsaccounts(filter: $"account_id eq '{opportunitiesOpportunity.account_id}'");
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

        protected int contactscontactsForcontactIdCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdValue;
        protected async Task contactscontactsForcontactIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(contact_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactId = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCount = result.Count;

                if (!object.Equals(opportunitiesOpportunity.contact_id, null))
                {
                    var valueResult = await EspoDbNewService.GetContactscontacts(filter: $"contact_id eq '{opportunitiesOpportunity.contact_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        contactscontactsForcontactIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int campaigncampaignsForcampaignIdCount;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaignsForcampaignIdValue;
        protected async Task campaigncampaignsForcampaignIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCampaigncampaigns(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(campaign_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                campaigncampaignsForcampaignId = result.Value.AsODataEnumerable();
                campaigncampaignsForcampaignIdCount = result.Count;

                if (!object.Equals(opportunitiesOpportunity.campaign_id, null))
                {
                    var valueResult = await EspoDbNewService.GetCampaigncampaigns(filter: $"campaign_id eq '{opportunitiesOpportunity.campaign_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        campaigncampaignsForcampaignIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load campaign" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.CreateOpportunitiesOpportunity(opportunitiesOpportunity);
                DialogService.Close(opportunitiesOpportunity);
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

        bool hascampaign_idValue;

        [Parameter]
        public string campaign_id { get; set; }

        bool hascontact_idValue;

        [Parameter]
        public string contact_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            opportunitiesOpportunity = new EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity();

            hasaccount_idValue = parameters.TryGetValue<string>("account_id", out var hasaccount_idResult);

            if (hasaccount_idValue)
            {
                opportunitiesOpportunity.account_id = hasaccount_idResult;
            }

            hascampaign_idValue = parameters.TryGetValue<string>("campaign_id", out var hascampaign_idResult);

            if (hascampaign_idValue)
            {
                opportunitiesOpportunity.campaign_id = hascampaign_idResult;
            }

            hascontact_idValue = parameters.TryGetValue<string>("contact_id", out var hascontact_idResult);

            if (hascontact_idValue)
            {
                opportunitiesOpportunity.contact_id = hascontact_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}