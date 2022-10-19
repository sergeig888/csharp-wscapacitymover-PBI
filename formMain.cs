using Azure.Core;
using Azure.Identity;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBI_WorkspaceCapacityMover
{
    public partial class formMain : Form
    {
        private static readonly string Username = ConfigurationManager.AppSettings["pbiUsername"];
        private static readonly string ApiUrl = ConfigurationManager.AppSettings["apiUrl"];
        
        PowerBIClient client;
        Capacities capacitiesList;

        public formMain()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            var task = AthenticateToServiceMSAL();
        }

        /// <summary>
        /// Authentication to Power BI API and construction of client object
        /// </summary>
        /// <returns>asyncronous task</returns>
        private async Task AthenticateToServiceMSAL()
        {
            client = await GetPowerBiClient();

            //populating capacities combo box
            getCapacitiesList();
        }

        /// <summary>
        /// Auth routine upgrade to MSAL
        /// </summary>
        /// <returns>PBI Access Token</returns>
        internal async Task<AccessToken> GetAccessToken()
        {
            //NOTE: Default Windows creds; no prompt
            //var credential = new DefaultAzureCredential();
            //NOTE: prompt with pre-populated user name
            InteractiveBrowserCredentialOptions options = new InteractiveBrowserCredentialOptions();
            //NOTE: use of options is optinal to help speed up login process
            options.LoginHint = Username;
            //TODO: experiment with persisting auth options
            //options.TokenCachePersistenceOptions = new TokenCachePersistenceOptions();
            var credential = new InteractiveBrowserCredential(options);

            string[] scopes = { "https://analysis.windows.net/powerbi/api/.default" };

            var token = await credential.GetTokenAsync(new TokenRequestContext(scopes));

            //extracting tenant Id from Jwt token; alternatively can move token Id
            //to config file and set in options.TenantId 
            var jwtToken = new JwtSecurityToken(token.Token);
            string tenantId = jwtToken.Issuer.Split('/')[3];

            tbTenantId.Text = tenantId;

            return token;
        }

        internal async Task<PowerBIClient> GetPowerBiClient()
        {
            var addToken = await GetAccessToken();
            var tokenCredentials = new TokenCredentials(addToken.Token, "Bearer");

            return new PowerBIClient(new Uri(ApiUrl), tokenCredentials);
        }

        private async void getCapacitiesList()
        {
            try
            {
                var capacitiesList = await client.Capacities.GetCapacitiesAsync();

                if (capacitiesList == null || capacitiesList.Value.Count == 0) return;

                foreach (var capacity in capacitiesList.Value)
                {
                    cbCapacities.Items.Add(capacity.Id);
                }

                cbCapacities.SelectedIndex = 0;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Getting Capacities", MessageBoxButtons.OK, MessageBoxIcon.Error);

                updateItemStatus("Get capacities request failed");
            }
        }

        private async void btnGetMyWorkspaces_Click(object sender, EventArgs e)
        {
            //clearing previos result set
            dgViewMyWorkspaces.Rows.Clear();

            //enumerating groups
            //NOTE: need to handle API output in blocks of 5000 calls; tenant is limited to 200 calls per hour
            //test block size needs to be equal to the number of available
            //workspaces to test edge case where block size is equal to the number of returned rows 
            //int blockSize = 611; 
            int blockSize = 5000;  //largest block size per docs; keep at max to avoid being throttled
            int blockOffset = 0;
            int rowCount;

            do
            {
                var groups = await client.Groups.GetGroupsAsAdminAsync(blockSize, skip: blockOffset, expand: "users", filter: "tolower(type) eq 'personalgroup'");

                if (groups == null || groups.Value.Count == 0) return;

                rowCount = groups.Value.Count;

                dgViewMyWorkspaces.Rows.Add(rowCount);

                for (int i = blockOffset; i < dgViewMyWorkspaces.Rows.Count; i++)
                {
                    dgViewMyWorkspaces.Rows[i].Cells[0].Value = groups.Value[i - blockOffset].Id;
                    dgViewMyWorkspaces.Rows[i].Cells[1].Value = groups.Value[i - blockOffset].Name;
                    dgViewMyWorkspaces.Rows[i].Cells[2].Value = groups.Value[i - blockOffset].Users[0].Identifier;
                    dgViewMyWorkspaces.Rows[i].Cells[3].Value = groups.Value[i - blockOffset].State;
                    dgViewMyWorkspaces.Rows[i].Cells[4].Value = groups.Value[i - blockOffset].CapacityId.ToString();
                }

                //intermediate stage update check
                //System.Diagnostics.Debug.WriteLine("Rows:" + dgViewMyWorkspaces.Rows.Count);

                blockOffset += blockSize;

            } while (rowCount == blockSize);

            //final list update check
            //System.Diagnostics.Debug.WriteLine("Rows:" + dgViewMyWorkspaces.Rows.Count);
        }

        private void btnAssignToCapacity_Click(object sender, EventArgs e)
        {
            var selectionCount = dgViewMyWorkspaces.SelectedRows.Count;
            List<string> workspaceGuids = new List<string>();

            if (selectionCount == 0)
            {
                MessageBox.Show("No rows selected!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }           

            for (int i = 0; i < selectionCount; i++)
            {
                workspaceGuids.Add(dgViewMyWorkspaces.SelectedRows[i].Cells[0].Value.ToString());             
            }
                        
            DialogResult userAction = MessageBox.Show("Target capacity: " + cbCapacities.Text +
                "\nTop of " + workspaceGuids.Count +
                " selected workspaces:\n" + string.Join("\n", workspaceGuids.Take(10)),
                "Proceed with assignment?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (userAction != DialogResult.Yes) return;

            //posting action taken notification
            foreach (DataGridViewRow row in dgViewMyWorkspaces.SelectedRows)
            {
                row.Cells[4].Value = "Migration requested";
            }
                        
            //Testing for user selecting "null" capacity to move workspaces to shared
            if (cbCapacities.Text == "00000000-0000-0000-0000-000000000000") //TODO: <- make sure this test holds true
            {
                unAssignWorkspaces(workspaceGuids);
            }
            else
            {
                assignToCapacity(cbCapacities.Text, workspaceGuids);
            }
        }

        /// <summary>
        /// Helper method to unassign workspaces
        /// </summary>
        /// <param name="workspaces"></param>
        private void unAssignWorkspaces(List<string> workspaces)
        {
            try
            {                
                //TODO: add sanity check for the list size; need to determine what is the max for the POST
                UnassignWorkspacesCapacityRequest unAssignRequest = new UnassignWorkspacesCapacityRequest(workspaces);
                client.Capacities.UnassignWorkspacesFromCapacity(unAssignRequest);
            }

            catch (HttpOperationException e)
            {
                MessageBox.Show(e.Message + "\n\nDetails:\n" + e.Response.Content,
                    "Error Sending Unassign Request", MessageBoxButtons.OK, MessageBoxIcon.Error);

                updateItemStatus("Unassign request failed");
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Sending Unassign Request", MessageBoxButtons.OK, MessageBoxIcon.Error);

                updateItemStatus("Unassign request failed");
            }
        }

        /// <summary>
        /// Helper method to assign workspaces
        /// </summary>
        private void assignToCapacity(string targetCapacity, List<string> workspaces)
        {
            try
            {
                //building assign to capacity request object
                CapacityMigrationAssignment migrationAssignment = new CapacityMigrationAssignment(workspaces, targetCapacity);

                List<CapacityMigrationAssignment> migrationAssignmentList = new List<CapacityMigrationAssignment>();

                migrationAssignmentList.Add(migrationAssignment);

                client.Capacities.AssignWorkspacesToCapacity(new AssignWorkspacesToCapacityRequest(migrationAssignmentList));
            }

            catch (HttpOperationException e)
            {
                MessageBox.Show(e.Message + "\n\nDetails:\n" + e.Response.Content,
                    "Error Sending Assign Request", MessageBoxButtons.OK, MessageBoxIcon.Error);

                updateItemStatus("Migration request failed");
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Sending Assign Request", MessageBoxButtons.OK, MessageBoxIcon.Error);

                updateItemStatus("Migration request failed");
            }
        }

        /// <summary>
        /// Helper method to update item request status
        /// </summary>
        /// <param name="requestStatus">status message text</param>
        private void updateItemStatus(string requestStatus)
        {
            //posting action taken notification
            foreach (DataGridViewRow row in dgViewMyWorkspaces.SelectedRows)
            {
                row.Cells[4].Value = requestStatus;
            }
        }


        private async void cbCapacities_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbCapacities.Text.Length == 0) return;

            //special handling shared capacity
            if (cbCapacities.Text == "00000000-0000-0000-0000-000000000000")
            {
                tbCapacityInfo.Text = "Target Capacity: Shared";
                return;
            }

            //building available to tenant Admin capacities collection
            //no need to execute expensive admin call if user only wants to unassign workspaces
            try
            {
                if (capacitiesList is null)
                {
                    capacitiesList = await client.Admin.GetCapacitiesAsAdminAsync("tenantKey");
                }

                var selectedCapacity = capacitiesList.Value.Where(capacity => capacity.Id.ToString() == cbCapacities.Text).FirstOrDefault();

                string capacityInfo = "Target Capacity: " + selectedCapacity.DisplayName +
                                       "\r\nSKU: " + selectedCapacity.Sku +
                                       "  Region: " + selectedCapacity.Region;

                if (selectedCapacity.TenantKey is null)
                {
                    capacityInfo += "\r\nEncryption Key: not set";
                }
                else
                {
                    capacityInfo = capacityInfo + "\r\nEncryption Key: " + selectedCapacity.TenantKey.Name;
                }

                tbCapacityInfo.Text = capacityInfo;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error retrieving and setting capacity information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}