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

## Relevant VS 2019 Debugging Features (last update: Version 16.4 Preview 3)
The following list contains debugging tools and corresponding documentation/blogs in Visual Studio that have been demoed using this application.

* [Run to Click](https://devblogs.microsoft.com/devops/run-to-click-debugging-in-visual-studio-2017/)
* [Pinned Datatips](https://docs.microsoft.com/en-us/visualstudio/debugger/view-data-values-in-data-tips-in-the-code-editor?view=vs-2019)
* [Set to Next Statement](https://docs.microsoft.com/en-us/visualstudio/debugger/navigating-through-code-with-the-debugger?view=vs-2019#BKMK_Set_the_next_statement_to_execute)
* [DebuggerDisplay(managed only) & Natvis(C++ only)](https://devblogs.microsoft.com/visualstudio/customize-object-displays-in-the-visual-studio-debugger-your-way/)
* **Pinnable Properties (Doc link TBA)** - Quickly inspect objects by their properties in datatips or Watch windows. Hover over a property in a datatip or Watch window and select the pin icon that appears or right-click the property and select "Pin property as favorite" from the context menu.
* [Text Visualizers](https://docs.microsoft.com/en-us/visualstudio/debugger/string-visualizer-dialog-box?view=vs-2019)
* [Conditional Breakpoints](https://docs.microsoft.com/en-us/visualstudio/debugger/using-breakpoints?view=vs-2019#breakpoint-conditions)
* [Tracepoints](https://docs.microsoft.com/en-us/visualstudio/debugger/using-breakpoints?view=vs-2019#BKMK_Print_to_the_Output_window_with_tracepoints)
* [Search for Autos, Watch, and Locals windows](https://devblogs.microsoft.com/visualstudio/enhanced-in-visual-studio-2019-search-for-objects-and-properties-in-the-watch-autos-and-locals-windows/)
* [Data Breakpoints](https://devblogs.microsoft.com/visualstudio/break-when-value-changes-data-breakpoints-for-net-core-in-visual-studio-2019/)
* **Step Into Specific (Doc link TBA)** - Step into a specific function by right clicking code line, selecting "Step into specific" and choosing a function to step into from the resulting dropdown menu
* [ReturnValue](https://docs.microsoft.com/en-us/visualstudio/debugger/debugger-tips-and-tricks?view=vs-2019#view-return-values-for-functions)
* [Pseudovariables](https://docs.microsoft.com/en-us/visualstudio/debugger/pseudovariables?view=vs-2019)
* [C# Format Specifiers](https://docs.microsoft.com/en-us/visualstudio/debugger/format-specifiers-in-csharp?view=vs-2019)
* [C++ Format Specifiers](https://docs.microsoft.com/en-us/visualstudio/debugger/format-specifiers-in-cpp?view=vs-2019)
* [Edit and Continue](https://docs.microsoft.com/en-us/visualstudio/debugger/edit-and-continue?view=vs-2019)
* [Diagnostics Tools Window](https://docs.microsoft.com/en-us/visualstudio/profiling/running-profiling-tools-with-or-without-the-debugger?view=vs-2019#BKMK_Quick_start__Collect_diagnostic_data)
* [Snapshots on Exceptions](https://devblogs.microsoft.com/visualstudio/snapshots-on-exceptions-while-debugging-with-intellitrace/)
* [First-Chance Exceptions](https://docs.microsoft.com/en-us/visualstudio/debugger/managing-exceptions-with-the-debugger?view=vs-2019#tell-the-debugger-to-break-when-an-exception-is-thrown)
* **Parallel Stacks Window for Tasks (Doc link TBA)** - Visualize Tasks flow in asynchronous applications by selecting **Debug > Windows > Parallel Stacks**
* **Attach to Linux Docker Container (Doc link TBA)** - Attach the debugger to Linux Docker containers by selecting **Debug > Attach to Process..** and select "Docker (Linux Container)" from the "Connection Type" dropdown.
* [Source Link](https://github.com/dotnet/sourcelink/blob/master/README.md)
* [Child Process Debugging Power Tool](https://marketplace.visualstudio.com/items?itemName=vsdbgplat.MicrosoftChildProcessDebuggingPowerTool)
* [Snapshot Debugger](https://devblogs.microsoft.com/visualstudio/snapshot-debugging-with-visual-studio-2017-now-ready-for-production/)
* [Time-Travel Debugging](https://devblogs.microsoft.com/visualstudio/introducing-time-travel-debugging-for-visual-studio-enterprise-2019/)
