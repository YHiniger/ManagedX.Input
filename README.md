# ManagedX.Input
ManagedX.Input is a .NET library (DLL), which provides access to the XInput and RawInput APIs on Windows 7(*)/8.x/10.

For Desktop applications only.


## Features:
- supports both x64 and x86 platforms (AnyCPU)
- CLS-compliant
- automatic selection of the most appropriate version of the XInput API: 1.3 on Windows 7(*), 1.4 on Windows 8.x/10
- high-precision mouse motion and multi-mouse support through RawInput
- fully commented (in english)
- built with respect to Visual Studio 2017 code analysis (all rules enabled)


### Requirements:
- Windows 7(*) or newer
- .NET Framework 4.6 : https://www.microsoft.com/en-us/download/details.aspx?id=48130
- ManagedX : https://github.com/YHiniger/ManagedX

(*) Windows 7 users need the DirectX End-User Runtime (June 2010): https://www.microsoft.com/en-us/download/details.aspx?id=35&44F86079-8679-400C-BFF2-9CA5F2BCBDFC=1