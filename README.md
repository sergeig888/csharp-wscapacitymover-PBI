# csharp-wscapacitymover-PBI

Power BI workspace to capacity mover WinForms tool sample code.

This sample code is provided "as is".  Users are expected to modify it to fit their needs.

**Purpose**: provide convenient easy to use UI experience to Power BI tenant Admins for MyWorkspace capacity assignment management at scale leveraging Power BI REST API.

* **Scenario 1:** compliance or internal policies require that MyWorkspace for a group of users needs to be placed on a multi-geo and/or protected by customer owned encryption key (BYOK) capacity.
* **Scenario 2:** tenant admin wants to discourage the use of MyWorkspace on the tenant by placing it on a starved for resources capacity or on a paused embedded capacity.  Note that in order to assign a workspace to an embedded (A SKU) capacity that capacity needs to be active and in running state.  However, once the assignment request is successfully completed, capacity can be paused.  Pausing the capacity will prevent user content from rendering. It will also make it impossible to publish new content to MyWorkspace assigned to paused capacity.  In contrast P or EM SKUs can't be paused and therefore users will be able to continue to publish content to MyWorkspace.

IMPORTANT: because the owner of MyWorkspace has workspace admin privileges that can't be revoked or restricted, the user will always retain the ability to move the workspace back to any capacity where the user has assignment rights including shared capacity. Tenant admin can monitor and detect undesirable user activity leveraging activity or audit log events and take corrective actions.

The sample targets .Net Framework 4.8.

After installing all NuGet packages first time project user should perform only one modification in the app config file: change value of user@domain.com to a valid Power BI tenant admin identity:

```xml
<!-- NOTE: providing active windows  session user name speeds up login-->
<add key="pbiUsername" value="user@domain.com" />
 ```

IMPORTANT: Power BI or O365 Global tenant admin level of access is required to execute Power BI Admin API calls.

This sample uses well-known built-in Power BI app id (it is used by Power BI PowerShell modules).  Users of this sample app may choose to use their own app.  Authentication with AAD and API access token acquisition code uses older ADAL libraries that at the time of this writing are expected to be supported through June 2022.  It is recommended to consider a switch to MSAL after June 2022.