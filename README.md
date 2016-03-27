# ManagedX.Input
ManagedX.Input is a .NET 4.5 library (DLL) written in C#, providing access to the XInput API on Windows Vista/7/8/8.1/10.
Also supports keyboard and mouse.
For Desktop applications only.


## Features:
- supports both x64 and x86 platforms (AnyCPU)
- CLS-compliant
- automatic selection of the most appropriate version of the XInput API: 1.3 on Windows Vista/7, 1.4 on Windows 8.x
- mouse can use raw input to retrieve high-precision motion
- built taking into account code analysis (all rules enabled)
- fully commented (offline HTML documentation will be provided later)


### Requirements:
- Windows Vista* or greater
- .NET Framework 4.5 : https://www.microsoft.com/en-US/download/details.aspx?id=30653
- ManagedX : https://github.com/YHiniger/ManagedX

*Windows Vista and 7 users also need the DirectX End-User Runtime (June 2010), available here:
https://www.microsoft.com/en-us/download/details.aspx?id=35&44F86079-8679-400C-BFF2-9CA5F2BCBDFC=1