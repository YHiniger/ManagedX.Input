# ManagedX.Input
ManagedX.Input is a .NET 4.5 library (DLL) written in C#, providing access to the XInput API on Windows Vista/7/8/8.1 (10 untested).
Also supports keyboard and mouse.


### Features:
- supports both x64 and x86 platforms (AnyCPU)
- CLS-compliant
- automatic selection of the most appropriate version of the XInput API: 1.3 on Windows Vista/7, 1.4 on Windows 8.x, 1.5 on Windows 10
- fully commented (offline documentation will be provided later)
- built taking into account code analysis (all rules enabled)


### Requirements:
- ManagedX
- Windows Vista and 7 users also need the DirectX End-User Runtime (June 2010).