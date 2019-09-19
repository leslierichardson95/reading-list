# reading-list
A .NET Core 3.0 web app where a user chooses what to read by giving a thumbs up or thumbs down to random books being displayed (think Tinder, but for books)

## Installing .NET Core 3.0 Preview in Visual Studio 2019
To use this app successfully, you'll need to have .NET Core 3.0 or higher installed for your IDE.  If you are using a preview build of .NET Core 3.0 in Visual Studio 2019, complete the following steps:

1. [Download and install .NET Core 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)
2. In Visual Studio 2019, enable preview use by going to **Tools -> Options -> Environment -> Preview Features** and check the **“Use previews of the .NET Core SDK.”** 
3. To double-check that .NET Core 3.0 has been installed, go to **Solution Explorer**, right-click the **ReadingList** project and select **Properties**, and make sure **Target Framework** is set to **.NET Core 3.0** in the **Applications** tab.

## Turn on artificial bugs
This app was originally created to demo debugging tools in Visual Studio 2019, so a few bugs have been intentionally added to this app in order to illustrate them, but many are commented out by default.
To turn on bugs, locate comments containing "DEMO," where the bug will be commented out directly below it.  The demo comment will explain the bug and/or Visual Studio tools/features related to the line or code.

**To quickly locate all demos/bugs via the Task List Window:**

1. Go to **Tools -> Options -> Environment -> Task List**
2. Type **"DEMO"** in the **Name** textbox and select **Add** to add the demo keyword to the Token List.
3. Open Task List window at **View -> Task List** to view, double-click and be redirected to all DEMO locations.
