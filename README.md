# CIAInfo
Simple application that implements ninfs to mount one or more CIAs (CTR Importable Archive) files, read differnet parts of it, and displays the information.

Requirements:

- The latest version of [.NET Core Runtime](https://www.google.com) for Windows (not required for the framework independent binaries)

- The latest [ninfs](https://github.com/ihaveamac/ninfs/releases/tag/v1.6.1) Windows executable and [WinFSP](http://www.secfs.net/winfsp/rel/)

- The ARM9 bootrom (boot9.bin) from your 3DS (ninfs requirement), and a Seed Database file (seeddb.bin) for CIAs that need a seed.

- CIA files

How to use:

First, make sure you have all the requirements

1. Download the latest release of CIAInfo
2. Put the ninfs executable into the ninfs folder
3. Put your CIAs in to the "Put CIAs here" folder
4. Create a new Folder called "3ds" in AppData\Roaming (can be accessed by opening %appdata% in windows explorer), and put both boot9.bin and seeddb.bin inside it
5. Open either a Command Prompt or Windows PowerShell and navigate to the folder where CIAInfo.exe is located
6. Execute CIAInfo.exe

NOTE: CIAInfo does not support parameters. Whatever you pass in will not be used.

CIAInfo will now start reading each CIA one by one and display the read information from it.

Example output:

```
CIAInfo 1.0 - Made by TimmSkiller, credit goes to ihaveamac for ninfs
Checking existence of needed folders...
Mounting example.cia...

File Name: 000400000017C200 YO-KAI WATCH (CTR-P-AYWZ) (E).cia

Long Name: YO-KAI WATCH                                                                                                 
Short Name: YO-KAI WATCH
Publisher : Nintendo
Product Code: CTR-P-AYWZ
Title ID: 000400000017C200
Region: Australia (Z)
CIA Type: Original 3DS | Game data
Version (WIP): 0000
Size: 1887436 KB | 14745 Blocks (estimated)
```

This app might be badly written, as it's my first ever app that turned into something useful.
