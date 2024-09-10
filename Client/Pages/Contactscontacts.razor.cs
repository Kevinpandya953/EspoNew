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
    public partial class Contactscontacts
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontacts;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.ContactsContact> grid0;
        protected int count;

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(filter: $"{args.Filter}", expand: "account,campaign", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                contactscontacts = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Contactscontacts" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddContactsContact>("Add ContactsContact", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.ContactsContact> args)
        {
            await DialogService.OpenAsync<EditContactsContact>("Edit ContactsContact", new Dictionary<string, object> { {"contact_id", args.Data.contact_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContact)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteContactsContact(contactId:contactsContact.contact_id);

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
                    Detail = $"Unable to delete ContactsContact"
                });
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContactChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.ContactsContact args)
        {
            contactsContactChild = args;
            var Accountsaccount_contactsResult = await EspoDbNewService.GetAccountsaccount_contacts();
            if (Accountsaccount_contactsResult != null)
            {
                args.Accountsaccount_contacts = Accountsaccount_contactsResult.Value.ToList();
            }
            var Cases_casesResult = await EspoDbNewService.GetCases_cases();
            if (Cases_casesResult != null)
            {
                args.Cases_cases = Cases_casesResult.Value.ToList();
            }
            var OpportunitiesopportunitiesResult = await EspoDbNewService.GetOpportunitiesopportunities();
            if (OpportunitiesopportunitiesResult != null)
            {
                args.Opportunitiesopportunities = OpportunitiesopportunitiesResult.Value.ToList();
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccountContact accountsAccountContactAccountsaccount_contacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdAccountsaccount_contacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdAccountsaccount_contacts;

        protected int accountsaccountsForaccountIdAccountsaccount_contactsCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdAccountsaccount_contactsValue;
        protected async Task accountsaccountsForaccountIdAccountsaccount_contactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(account_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdAccountsaccount_contacts = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdAccountsaccount_contactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int contactscontactsForcontactIdAccountsaccount_contactsCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdAccountsaccount_contactsValue;
        protected async Task contactscontactsForcontactIdAccountsaccount_contactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(contact_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdAccountsaccount_contacts = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdAccountsaccount_contactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> Accountsaccount_contactsDataGrid;

        protected async Task Accountsaccount_contactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddAccountsAccountContact>("Add Accountsaccount_contacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Accountsaccount_contactsDataGrid.Reload();

        }

        protected async Task Accountsaccount_contactsRowSelect(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditAccountsAccountContact>("Edit Accountsaccount_contacts", new Dictionary<string, object> { {"account_contact_id", args.account_contact_id} });
            await GetChildData(data);
            await Accountsaccount_contactsDataGrid.Reload();
        }

        protected async Task Accountsaccount_contactsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccountContact accountsAccountContact)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAccountsAccountContact(accountContactId:accountsAccountContact.account_contact_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await Accountsaccount_contactsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete AccountsAccountContact"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.CasesCase casesCaseCases_cases;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdCases_cases;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleadsForleadIdCases_cases;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdCases_cases;

        protected int accountsaccountsForaccountIdCases_casesCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdCases_casesValue;
        protected async Task accountsaccountsForaccountIdCases_casesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(account_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdCases_cases = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCases_casesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int leadsleadsForleadIdCases_casesCount;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsleadsForleadIdCases_casesValue;
        protected async Task leadsleadsForleadIdCases_casesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetLeadsleads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(lead_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                leadsleadsForleadIdCases_cases = result.Value.AsODataEnumerable();
                leadsleadsForleadIdCases_casesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load lead" });
            }
        }

        protected int contactscontactsForcontactIdCases_casesCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdCases_casesValue;
        protected async Task contactscontactsForcontactIdCases_casesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(contact_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdCases_cases = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCases_casesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CasesCase> Cases_casesDataGrid;

        protected async Task Cases_casesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCasesCase>("Add Cases_cases", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Cases_casesDataGrid.Reload();

        }

        protected async Task Cases_casesRowSelect(EspoNew.Server.Models.EspoDbNew.CasesCase args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditCasesCase>("Edit Cases_cases", new Dictionary<string, object> { {"case_id", args.case_id} });
            await GetChildData(data);
            await Cases_casesDataGrid.Reload();
        }

        protected async Task Cases_casesDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CasesCase casesCase)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCasesCase(caseId:casesCase.case_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await Cases_casesDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CasesCase"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunityOpportunitiesopportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdOpportunitiesopportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdOpportunitiesopportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> campaigncampaignsForcampaignIdOpportunitiesopportunities;

        protected int accountsaccountsForaccountIdOpportunitiesopportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdOpportunitiesopportunitiesValue;
        protected async Task accountsaccountsForaccountIdOpportunitiesopportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(account_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdOpportunitiesopportunities = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdOpportunitiesopportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int contactscontactsForcontactIdOpportunitiesopportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdOpportunitiesopportunitiesValue;
        protected async Task contactscontactsForcontactIdOpportunitiesopportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(contact_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdOpportunitiesopportunities = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdOpportunitiesopportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int campaigncampaignsForcampaignIdOpportunitiesopportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaignsForcampaignIdOpportunitiesopportunitiesValue;
        protected async Task campaigncampaignsForcampaignIdOpportunitiesopportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCampaigncampaigns(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(campaign_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                campaigncampaignsForcampaignIdOpportunitiesopportunities = result.Value.AsODataEnumerable();
                campaigncampaignsForcampaignIdOpportunitiesopportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load campaign" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> OpportunitiesopportunitiesDataGrid;

        protected async Task OpportunitiesopportunitiesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddOpportunitiesOpportunity>("Add Opportunitiesopportunities", new Dictionary<string, object> {  });
            await GetChildData(data);
            await OpportunitiesopportunitiesDataGrid.Reload();

        }

        protected async Task OpportunitiesopportunitiesRowSelect(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditOpportunitiesOpportunity>("Edit Opportunitiesopportunities", new Dictionary<string, object> { {"opportunity_id", args.opportunity_id} });
            await GetChildData(data);
            await OpportunitiesopportunitiesDataGrid.Reload();
        }

        protected async Task OpportunitiesopportunitiesDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunity)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteOpportunitiesOpportunity(opportunityId:opportunitiesOpportunity.opportunity_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await OpportunitiesopportunitiesDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete OpportunitiesOpportunity"
                });
            }
        }
    }
}