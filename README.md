# LoB solution with ASE

**Table of content**

[Create App Registrations in AAD](#create-app-registrations-in-aad)

* [Get Tenant Id](#get-tenant-id)
* [Create App Registration for the Admin Web App](#create-app-registration-for-the-admin-web-app)

[Deploy Azure Components with ARM Template](#deploy-azure-components-with-arm-template)

* [GitHub Authorize](#github-authorize)
* [Deploy Azure Components](#deploy-azure-components)
* [Deployment output](#deployment-output)


* [Add Reply URL to the Admin Web App](#add-reply-url-to-the-admin-web-app)

[Access Deployed Resources](#access-deployed-resources)

## Architecture

![](images/architecture.png)

## Create App Registrations in Azure Active Directory (AAD) 

### Get Tenant Id

Open the AAD in the Azure portal, and copy get the **Directory ID**.

![](images/aad-tenant-id.png)

The **Directory ID** is used to set the value of the **Azure Ad Tenant Id** parameter in the ARM Template.

### Create App Registration for the Admin Web App

Create a new App Registration in AAD:

* Name: LoB with ASE - Admin

* Application Type: Web app/API

* Sign-on URL: https://lob-with-ase/admin

* Permissions:

   | API                            | Permission Type | Permissions                   |
   | ------------------------------ | --------------- | ----------------------------- |
   | Windows Azure Active Directory | Delegated       | Sign in and read user profile |

Copy the **Application Id** and save it in a text file. You will need it later to specify the values for the **Azure Ad Client Id** parameter in the ARM Template.

## Deploy Azure Components with the ARM Template

### GitHub Authorize

1. Generate Token

   - Open [https://github.com/settings/tokens](https://github.com/settings/tokens) in your web browser.

   - Sign into your GitHub account where you forked this repository.

   - Click **Generate Token**.

   - Enter a value in the **Token description** text box.

   - Select the followings (your selections should match the screenshot below):

     - repo (all) -> repo:status, repo_deployment, public_repo
     - admin:repo_hook -> read:repo_hook

     ![](images/github-new-personal-access-token.png)

   - Click **Generate token**.

   - Copy the token and save it in a text file.

2. Add the GitHub Token to Azure in the Azure Resource Explorer

   - Open [https://resources.azure.com/providers/Microsoft.Web/sourcecontrols/GitHub](https://resources.azure.com/providers/Microsoft.Web/sourcecontrols/GitHub) in your web browser.

   - Log in with your Azure account.

   - Select the correct Azure subscription.

   - Select **Read/Write** mode.

   - Click **Edit**.

   - Paste the token into the **token parameter**.

     ![](images/update-github-token-in-azure-resource-explorer.png)

   - Click **PUT**.

### Deploy Azure Components

1. Fork this repository to your GitHub account.

2. Click the Deploy to Azure Button:

   [![Deploy to Azure](https://camo.githubusercontent.com/9285dd3998997a0835869065bb15e5d500475034/687474703a2f2f617a7572656465706c6f792e6e65742f6465706c6f79627574746f6e2e706e67)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FTylerLu%2FMS-Azure-LOB-ASE%2Fmaster%2Fazuredeploy.json)

3. Fill in the values on the deployment page:

   **Basics**:

   * **Subscription**: choose one of your subscriptions
   * **Resource group**: create a new resource group
   * **Location**:  select a location

   **Settings**:

   * **Ase Name**: the name of the App Service Environment. 

     >**Note:** This value is also used to name other Azure resources created by the ARM template. To avoid naming conflicts, please choose a unique name, for example: **tyler-eshop-180308**.

   * **Ase Location**: choose the same location as the location of the resource group.

     >**Note:** This parameter was added because of this [issue](https://github.com/Azure/azure-quickstart-templates/issues/2828).

   * **Sql Server Administrator Login**:  please keep the default value.

     >**Note:** If you want to change it, please make sure your login name meets the following requirements - It's a SQL Identifier, and not a typical system name (like admin, administrator, sa, root, dbmanager, loginmanager, etc.), or a built-in database user or role (like dbo, guest, public, etc.). Make sure your name doesn't contain whitespaces, unicode characters, or nonalphabetic characters, and that it doesn't begin with numbers or symbols.

   * **Sql Server Administrator Login Password**: please do use a strong password.

     >**Note:**
     >
     > - Your password must be at least 8 characters in length.
     > - Your password must contain characters from three of the following categories – English uppercase letters, English lowercase letters, numbers (0-9), and non-alphanumeric characters (!, $, #, %, etc.)

   * **Azure Ad Tenant Id**/**Client Id**: use the values you got from the previous chapter.

     > Note: These values are used to authenticate the Admin Web App.

   * **Source Code Repository URL**: use the URL of the repository you just created -`https://github.com/<YourAccount>/MS-Azure-LOB-ASE`

   * **Source Code Branch**: master

   * **Source code Manual Integration**: false

   **Terms and conditions**:

   * Check **I agree to the terms and conditions stated above**.

4. Click **Purchase**.

### Deployment output

After the deployment finishes, you will see the URLs of the Web App and Admin App in the outputs tab.

![](images/azure-deployment-outputs.png)

> Note: the deployment window shown above can be re-opened by clicking the **Deployments** tab in the resource group.
>
> ![](images/azure-resource-group-deployments.png)

### Add Reply URL to the Admin Web App

1. Copy the URL of the Admin Web app, and change the protocol schema from http to http**s**. This creates the base URL.

    ![](images/admin-web-app.png)

    For example: `https://huddle-bot.azurewebsites.net`

2. Append `/signin-oidc` to the base URL, to create the replay URL. 

   For example: `https://dev-eshop-admin.dev-eshop.p.azurewebsites.net/signin-oidc`

3. Add the replay URL to the Bot App Registration and save it.

   ![](images/app-registration-reply-urls.png)


## Access Deployed Resources

### Web App

Open the Web App URL you got from the output section in a browser.

![](images/web-app.png)

### Admin App

Open the Admin App URL you got from the output section in a browser. Then sign in with a work account in the AAD where you registered the AAD application.

 ![](images/admin-app.png)


**Copyright (c) 2018 Microsoft. All rights reserved.**
