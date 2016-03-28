# ManagedX.Input
ManagedX.Input is a .NET 4.6 library (DLL), which provides access to the RawInput and XInput APIs on Windows Vista/7/8/8.1/10.

For Desktop applications only.


## Features:
- supports both x64 and x86 platforms (AnyCPU)
- CLS-compliant
- automatic selection of the most appropriate version of the XInput API: 1.3(*) on Windows Vista/7, 1.4 on Windows 8.x/10
- mouse can use raw input to retrieve high-precision motion
- built taking into account code analysis (all rules enabled)
- fully commented (offline HTML documentation will be provided later)


### Requirements:
- Windows Vista (SP2) or newer
- .NET Framework 4.6 : https://www.microsoft.com/en-us/download/details.aspx?id=48130
- ManagedX : https://github.com/YHiniger/ManagedX

(*) Windows Vista and 7 users also need the DirectX End-User Runtime (June 2010), available here:
https://www.microsoft.com/en-us/download/details.aspx?id=35&44F86079-8679-400C-BFF2-9CA5F2BCBDFC=1