# CTRInfo (CIAInfo)
Simple application that analyzes one or more CIAs (CTR Importable Archive) files, and displays information about them.

Requirements:

- The latest version of [.NET Core Runtime](https://www.google.com) for Windows (not required for the framework independent INDP binaries)

- The latest [ninfs](https://github.com/ihaveamac/ninfs/releases/tag/v1.6.1) Windows executable and [WinFSP](http://www.secfs.net/winfsp/rel/), if you want to be able to rename the CIAs to the GodMode9-Style naming scheme, and to read the Title Names. This also requires the ARM9 bootrom (boot9.bin) from your 3DS, and a Seed Database file (seeddb.bin) for CIAs that need a seed.

- CIA files

Usage:

`CTRInfo`:

To read single CIAs:
```
CTRInfo cia -p / --path [path] (-g / --gm9-name-format) (-v / --verbose) (-n / --use-ninfs)

```

`-p / --path`: Path to the CIA

`-g / --gm9-name-format`: (Optional) Rename CIA to the GodMode9-style naming scheme. (must be used in combination with -n / --use-ninfs)

`-v / --verbose`: (Optional) Show LOADS of information about every single part of the CIA.

`-n / --use-ninfs`: (Optional) Use ninfs to read Title Names from SMDH.


To read multiple CIAs (in a directory):
```
CTRInfo dir -d / --dir [path] (-g / --gm9-name-format) (-v / --verbose) (-n / --use-ninfs)

```

`-d / --path`: Path to the CIAs

`-g / --gm9-name-format`: (Optional) Rename CIAs to the GodMode9-style naming scheme. (must be used in combination with -n / --use-ninfs)

`-v / --verbose`: (Optional) Show LOADS of information about every single part of the CIAs.

`-n / --use-ninfs`: (Optional) Use ninfs to read Title Names from SMDH.

# Examples

<details>
  <summary>Without using ninfs</summary>

```CTRInfo cia -p example.cia```:

```
CTRInfo 2.0 - Made by TimmSkiller

Long Name: N/A
Short Name: N/A
Publisher N/A
Product Code: CTR-N-SZMP
Title ID: 0004000000165B00
Region: Europe (E)
CIA Type: Original (Old) Nintendo 3DS (CTR) | Game Data
Title Version (Taken from TMD): 0.0.0
Total Size: 13131776 (0xC86000) | 100 blocks
```

`N/A` will be shown for title names when not using ninfs.
</details>

<details>
  <summary>Using ninfs</summary>

```CTRInfo cia -p example.cia -n```:

```
CTRInfo 2.0 - Made by TimmSkiller

Long Name: MARIO KART 7
Short Name: MARIO KART 7
Publisher Nintendo
Product Code: CTR-P-AMKP
Title ID: 0004000000030700
Region: Europe (E)
CIA Type: Original (Old) Nintendo 3DS (CTR) | Game Data
Title Version (Taken from TMD): 0.0.0
Total Size: 671340544 (0x2803D800) | 5121 blocks
```

As seen above, title names will become visible when using ninfs.
</details>

<details>
  <summary>Using ninfs + rename to GodMode9</summary>

```CTRInfo cia -p example.cia -n -g```:

```
CTRInfo 2.0 - Made by TimmSkiller

Long Name: MARIO KART 7
Short Name: MARIO KART 7
Publisher Nintendo
Product Code: CTR-P-AMKP
Title ID: 0004000000030700
Region: Europe (E)
CIA Type: Original (Old) Nintendo 3DS (CTR) | Game Data
Title Version (Taken from TMD): 0.0.0
Total Size: 671340544 (0x2803D800) | 5121 blocks

GodMode9 Naming Scheme: 0004000000030700 MARIO KART 7 (CTR-P-AMKP) (E).cia
```

As seen above, the last line shows the GodMode9-style format. The CIA will be renamed to that name.
</details>

<details>
  <summary>Using verbose mode</summary>

```CTRInfo cia -p example.cia -v```:

```
Signature Name: RSA_2048_SHA256
Signature Size: 60 (0x3c) bytes
Signature Padding: 256 (0x100) bytes
Title ID: 0004000000030700
Save Data Size: 524288 (0x80000) bytes
SRL Save Data Size: 0 (0x0) bytes
Title Version: 0.0.0 (0)
Amount of contents defined in TMD: 3
Content Info Records Hash: BC57FDCC040596FCB50F64E9CC8E4C8F2040A048BD0D17438B054DC18C4C897C
Issuer: Root-CA00000003-CP0000000b
Unused Version: 01
CA CRL Version: 00
Signer CRL Version: 00
Reserved(1): 00
System Version: 0000000000000000
Group ID: 0000
Reserved(2): 00000000
SRL Flag: 00
Reserved(3): 00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
Access Rights: 00000000
Boot Count: 0000
Unused Padding: 0000
--------------------------------
CONTENT CHUNK RECORD INFO FOR INDEX 0000.00000000:

ID: 00000000
Content Index: 0 (0000)

================================
CONTENT TYPE FLAGS:

ENCRYPTED: False
IS DISC: False
CFM: False
OPTIONAL: False
SHARED: False
================================

Content Size: 640881664 (0x26331400) bytes
Hash: D4A8BFA8DE18789D12BF917AA9AC9E5B6BF6DDE546A770D91EC21508102FFFC5
--------------------------------

--------------------------------
CONTENT CHUNK RECORD INFO FOR INDEX 0001.00000001:

ID: 00000001
Content Index: 1 (0001)

================================
CONTENT TYPE FLAGS:

ENCRYPTED: False
IS DISC: False
CFM: False
OPTIONAL: False
SHARED: False
================================

Content Size: 2650624 (0x287200) bytes
Hash: 44BBF08DE2C50B744A18B42A7935875E7647A832A88B5BE7B4C224E6C2775054
--------------------------------

--------------------------------
CONTENT CHUNK RECORD INFO FOR INDEX 0002.00000002:

ID: 00000002
Content Index: 2 (0002)

================================
CONTENT TYPE FLAGS:

ENCRYPTED: False
IS DISC: False
CFM: False
OPTIONAL: False
SHARED: False
================================

Content Size: 27808256 (0x1A85200) bytes
Hash: E87961A29BFCC94A1FFDA784CA799CE56A8E786501578B90A3AB5AE24A207A7A
--------------------------------

CONTENT INFO RECORD:

Index Offset: 0
Command Count: 3
Hash: AEFC0237212F2CF21A4CB2BAB4CB8E2185770DA4B206CA019036915929FDBA9A

Ticket Info:

Ticket Size: 848 (0x350) bytes
Signature Name: RSA_2048_SHA256
Signature Size: 60 (0x3C) bytes
Signature Padding Size: 256 (0x100) bytes

Signature data:
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
000000000000000000000000


Issuer: Root-CA00000003-XS0000000c

ECC Public Key:

00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
000000000000000000000000

Version: 1 (0x1)
CA CRL Version: 0 (0x0)
Signer CRL Version: 0 (0x0)
Title Key: 00000000000000000000000000000000
Ticket ID: 0000000000000000
Console ID: 00000000
Title ID: 0004000000030700
Title Version: 0.0.0 (0)
License Type: 0 (0x0)
Common KeyY Decryption Keyslot Index: 0x0
eShop Account ID: 00000000
Audit: 1 (0x1)

Limits:

00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000

Content Index:

00010014000000AC0000001400010014
00000000000000280000000100000084
000000840003000000000000FFFFFFFF
FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
FFFFFFFFFFFFFFFFFFFFFFFF00000000
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
000000000000000000000000

Content 0000.00000000:


NCCH INFO:

Magic: NCCH
Content Size: 640881664 (0x26331400) bytes - 4889 blocks
Title ID: 0004000000030700
Maker Code: 3031 ("01")
Version: 0.32.0 (512)
Program ID: 0004000000030700
Logo Region Hash: 0000000000000000000000000000000000000000000000000000000000000000

Product Code: CTR-P-AMKP

Product Code Information:

Console: Original (Old) Nintendo 3DS (CTR)
Content Type: Game Data
Region: Europe (E)

Extended Header Hash: 1FE22FE66D0967D26242A24EA3B3ED51E71537B009D71E772429BE22B307B53D
Extended Header Size: 1024 (0x400)

NCCH Flags:

Crypto Method: 0
Content Platform: CTR_Old3DS
Content Unit Size: 1024 (0x400) bytes
Content Type: Data + Excecutable, CTR Executable Image (CXI) NCCH
Encrypted: yes

Plain Region Offset: 2560 (0xA00)
Plain Region Size: 512 (0x200) bytes

Plain Region:

[SDK+NINTENDO:CTR_SDK-2_5_6_200_DspEffectFix]
[SDK+NINTENDO:Firmware-02_31_40]
[SDK+Nintendo:NEX_MM_2_4_3]
[SDK+Nintendo:NEX_2_4_3_S25]
[SDK+Mobiclip:MoflexDemuxer_1_0_3]
[SDK+Mobiclip:MobiclipDec_1_0_1]
[SDK+Mobiclip:Deblocker_1_0_3]
[SDK+Nintendo:NEX_RK_P_2_4_3]
[SDK+Nintendo:NEX_DS_2_4_3]

Logo Region Offset: 0 (0x0)
Logo Region Size: 0 (0x0) bytes

ExeFS Offset: 3072 (0xC00)
ExeFS Size: 3676160 (0x381800) bytes
ExeFS Hash Region Size: 512 (0x200) bytes

RomFS Offset: 3679232 (0x382400)
RomFS Size: 637202432 (0x25FAF000) bytes
RomFS Hash Region Size: 512 (0x200) bytes

ExeFS Superblock Hash: B7F6B2FE7ACC025346E9466E2D4527D432F78069577EF65C7BD5B73B95589ADD
RomFS Superblock Hash: 7157E020E040E6FD6C4040BEFC0AB1EDC36BB4278C70B74D7E2BA5E743ECB755
SMDH Info:

Magic: "N/A"
Version: N/A
Title Structure:

-------------------------------

SMDH Title Name Structure:

Language: N/A

Long Title: N/A

Short Title: N/A

Publisher: N/A


Age Ratings:

N/A: Not enabled

Region Lockout Region: N/A
Online Play Matchmaker ID: N/A
Online Play Matchmaker Bit ID: N/A
EULA (End User License Agreement) Version: N/A
Optional Animation Default Frame: N/A
CEC (StreetPass) ID: N/A

SMDH Application Settings Flags:

Will this title be visible on the HOME Menu? no
Will this title (if gamecart title) be automatically launched on system boot? no
Does this title utilize 3D? no
Do you have to accept the Nintendo 3DS EULA (End User License Agreement) to launch this title? no
Does this title automatically save it's data when exiting from HOME menu? no
Does this title use an extended banner? no
Is game rating required for this title? no
Does this title use Save Data? no
Will data be recorded in the Activity Log (and other places) for this title? no
Are SD Card Save Data backups disabled for this title? no
Is this title New Nintendo 3DS exclusive? no


Content 0001.00000001:


NCCH INFO:

Magic: NCCH
Content Size: 2650624 (0x287200) bytes - 20 blocks
Title ID: 0005000000030700
Maker Code: 3030 ("00")
Version: 0.0.0 (0)
Program ID: 0004000000030700
Logo Region Hash: 0000000000000000000000000000000000000000000000000000000000000000

Product Code: CTR-P-CTAP

Product Code Information:

Console: Original (Old) Nintendo 3DS (CTR)
Content Type: Game Data
Region: Europe (E)

Extended Header Hash: 0000000000000000000000000000000000000000000000000000000000000000
Extended Header Size: 0 (0x0)

NCCH Flags:

Crypto Method: 0
Content Platform: CTR_Old3DS
Content Unit Size: 1024 (0x400) bytes
Content Type: Data, CTR File Archive (CFA) NCCH
Encrypted: yes

Plain Region Offset: 0 (0x0)
Plain Region Size: 0 (0x0) bytes

Plain Region:

(Empty)

Logo Region Offset: 0 (0x0)
Logo Region Size: 0 (0x0) bytes

ExeFS Offset: 0 (0x0)
ExeFS Size: 0 (0x0) bytes
ExeFS Hash Region Size: 0 (0x0) bytes

RomFS Offset: 512 (0x200)
RomFS Size: 2650112 (0x287000) bytes
RomFS Hash Region Size: 512 (0x200) bytes

ExeFS Superblock Hash: 0000000000000000000000000000000000000000000000000000000000000000
RomFS Superblock Hash: 64696C7D052DBC670047FE8C8A15E21A8F550BA3F7B41ECCBAEB28D55A44D214
SMDH Info:

Magic: "N/A"
Version: N/A
Title Structure:

-------------------------------

SMDH Title Name Structure:

Language: N/A

Long Title: N/A

Short Title: N/A

Publisher: N/A


Age Ratings:

N/A: Not enabled

Region Lockout Region: N/A
Online Play Matchmaker ID: N/A
Online Play Matchmaker Bit ID: N/A
EULA (End User License Agreement) Version: N/A
Optional Animation Default Frame: N/A
CEC (StreetPass) ID: N/A

SMDH Application Settings Flags:

Will this title be visible on the HOME Menu? no
Will this title (if gamecart title) be automatically launched on system boot? no
Does this title utilize 3D? no
Do you have to accept the Nintendo 3DS EULA (End User License Agreement) to launch this title? no
Does this title automatically save it's data when exiting from HOME menu? no
Does this title use an extended banner? no
Is game rating required for this title? no
Does this title use Save Data? no
Will data be recorded in the Activity Log (and other places) for this title? no
Are SD Card Save Data backups disabled for this title? no
Is this title New Nintendo 3DS exclusive? no


Content 0002.00000002:


NCCH INFO:

Magic: NCCH
Content Size: 27808256 (0x1A85200) bytes - 212 blocks
Title ID: 0006000000030700
Maker Code: 3030 ("00")
Version: 0.0.0 (0)
Program ID: 0004000000030700
Logo Region Hash: 0000000000000000000000000000000000000000000000000000000000000000

Product Code: CTR-P-CTAP

Product Code Information:

Console: Original (Old) Nintendo 3DS (CTR)
Content Type: Game Data
Region: Europe (E)

Extended Header Hash: 0000000000000000000000000000000000000000000000000000000000000000
Extended Header Size: 0 (0x0)

NCCH Flags:

Crypto Method: 0
Content Platform: CTR_Old3DS
Content Unit Size: 1024 (0x400) bytes
Content Type: Data, CTR File Archive (CFA) NCCH
Encrypted: yes

Plain Region Offset: 0 (0x0)
Plain Region Size: 0 (0x0) bytes

Plain Region:

(Empty)

Logo Region Offset: 0 (0x0)
Logo Region Size: 0 (0x0) bytes

ExeFS Offset: 0 (0x0)
ExeFS Size: 0 (0x0) bytes
ExeFS Hash Region Size: 0 (0x0) bytes

RomFS Offset: 512 (0x200)
RomFS Size: 27807744 (0x1A85000) bytes
RomFS Hash Region Size: 512 (0x200) bytes

ExeFS Superblock Hash: 0000000000000000000000000000000000000000000000000000000000000000
RomFS Superblock Hash: 5F0E7710B2B9D4811971E7BAA11341149F6C7499E2A10498FEA1C46AB116D645
SMDH Info:

Magic: "N/A"
Version: N/A
Title Structure:

-------------------------------

SMDH Title Name Structure:

Language: N/A

Long Title: N/A

Short Title: N/A

Publisher: N/A


Age Ratings:

N/A: Not enabled

Region Lockout Region: N/A
Online Play Matchmaker ID: N/A
Online Play Matchmaker Bit ID: N/A
EULA (End User License Agreement) Version: N/A
Optional Animation Default Frame: N/A
CEC (StreetPass) ID: N/A

SMDH Application Settings Flags:

Will this title be visible on the HOME Menu? no
Will this title (if gamecart title) be automatically launched on system boot? no
Does this title utilize 3D? no
Do you have to accept the Nintendo 3DS EULA (End User License Agreement) to launch this title? no
Does this title automatically save it's data when exiting from HOME menu? no
Does this title use an extended banner? no
Is game rating required for this title? no
Does this title use Save Data? no
Will data be recorded in the Activity Log (and other places) for this title? no
Are SD Card Save Data backups disabled for this title? no
Is this title New Nintendo 3DS exclusive? no
```

Shows tons of info.
</details>

<details>
  <summary>Using verbose mode + ninfs</summary>

```CTRInfo cia -p example.cia -n -v```:

```
CTRInfo 2.0 - Made by TimmSkiller

Signature Name: RSA_2048_SHA256
Signature Size: 60 (0x3c) bytes
Signature Padding: 256 (0x100) bytes
Title ID: 0004000000030700
Save Data Size: 524288 (0x80000) bytes
SRL Save Data Size: 0 (0x0) bytes
Title Version: 0.0.0 (0)
Amount of contents defined in TMD: 3
Content Info Records Hash: BC57FDCC040596FCB50F64E9CC8E4C8F2040A048BD0D17438B054DC18C4C897C
Issuer: Root-CA00000003-CP0000000b
Unused Version: 01
CA CRL Version: 00
Signer CRL Version: 00
Reserved(1): 00
System Version: 0000000000000000
Group ID: 0000
Reserved(2): 00000000
SRL Flag: 00
Reserved(3): 00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
Access Rights: 00000000
Boot Count: 0000
Unused Padding: 0000
--------------------------------
CONTENT CHUNK RECORD INFO FOR INDEX 0000.00000000:

ID: 00000000
Content Index: 0 (0000)

================================
CONTENT TYPE FLAGS:

ENCRYPTED: False
IS DISC: False
CFM: False
OPTIONAL: False
SHARED: False
================================

Content Size: 640881664 (0x26331400) bytes
Hash: D4A8BFA8DE18789D12BF917AA9AC9E5B6BF6DDE546A770D91EC21508102FFFC5
--------------------------------

--------------------------------
CONTENT CHUNK RECORD INFO FOR INDEX 0001.00000001:

ID: 00000001
Content Index: 1 (0001)

================================
CONTENT TYPE FLAGS:

ENCRYPTED: False
IS DISC: False
CFM: False
OPTIONAL: False
SHARED: False
================================

Content Size: 2650624 (0x287200) bytes
Hash: 44BBF08DE2C50B744A18B42A7935875E7647A832A88B5BE7B4C224E6C2775054
--------------------------------

--------------------------------
CONTENT CHUNK RECORD INFO FOR INDEX 0002.00000002:

ID: 00000002
Content Index: 2 (0002)

================================
CONTENT TYPE FLAGS:

ENCRYPTED: False
IS DISC: False
CFM: False
OPTIONAL: False
SHARED: False
================================

Content Size: 27808256 (0x1A85200) bytes
Hash: E87961A29BFCC94A1FFDA784CA799CE56A8E786501578B90A3AB5AE24A207A7A
--------------------------------

CONTENT INFO RECORD:

Index Offset: 0
Command Count: 3
Hash: AEFC0237212F2CF21A4CB2BAB4CB8E2185770DA4B206CA019036915929FDBA9A

Ticket Info:

Ticket Size: 848 (0x350) bytes
Signature Name: RSA_2048_SHA256
Signature Size: 60 (0x3C) bytes
Signature Padding Size: 256 (0x100) bytes

Signature data:
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
000000000000000000000000


Issuer: Root-CA00000003-XS0000000c

ECC Public Key:

00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
000000000000000000000000

Version: 1 (0x1)
CA CRL Version: 0 (0x0)
Signer CRL Version: 0 (0x0)
Title Key: 00000000000000000000000000000000
Ticket ID: 0000000000000000
Console ID: 00000000
Title ID: 0004000000030700
Title Version: 0.0.0 (0)
License Type: 0 (0x0)
Common KeyY Decryption Keyslot Index: 0x0
eShop Account ID: 00000000
Audit: 1 (0x1)

Limits:

00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000

Content Index:

00010014000000AC0000001400010014
00000000000000280000000100000084
000000840003000000000000FFFFFFFF
FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
FFFFFFFFFFFFFFFFFFFFFFFF00000000
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
00000000000000000000000000000000
000000000000000000000000

Content 0000.00000000:


NCCH INFO:

Magic: NCCH
Content Size: 640881664 (0x26331400) bytes - 4889 blocks
Title ID: 0004000000030700
Maker Code: 3031 ("01")
Version: 0.32.0 (512)
Program ID: 0004000000030700
Logo Region Hash: 0000000000000000000000000000000000000000000000000000000000000000

Product Code: CTR-P-AMKP

Product Code Information:

Console: Original (Old) Nintendo 3DS (CTR)
Content Type: Game Data
Region: Europe (E)

Extended Header Hash: 1FE22FE66D0967D26242A24EA3B3ED51E71537B009D71E772429BE22B307B53D
Extended Header Size: 1024 (0x400)

NCCH Flags:

Crypto Method: 0
Content Platform: CTR_Old3DS
Content Unit Size: 1024 (0x400) bytes
Content Type: Data + Excecutable, CTR Executable Image (CXI) NCCH
Encrypted: yes

Plain Region Offset: 2560 (0xA00)
Plain Region Size: 512 (0x200) bytes

Plain Region:

[SDK+NINTENDO:CTR_SDK-2_5_6_200_DspEffectFix]
[SDK+NINTENDO:Firmware-02_31_40]
[SDK+Nintendo:NEX_MM_2_4_3]
[SDK+Nintendo:NEX_2_4_3_S25]
[SDK+Mobiclip:MoflexDemuxer_1_0_3]
[SDK+Mobiclip:MobiclipDec_1_0_1]
[SDK+Mobiclip:Deblocker_1_0_3]
[SDK+Nintendo:NEX_RK_P_2_4_3]
[SDK+Nintendo:NEX_DS_2_4_3]

Logo Region Offset: 0 (0x0)
Logo Region Size: 0 (0x0) bytes

ExeFS Offset: 3072 (0xC00)
ExeFS Size: 3676160 (0x381800) bytes
ExeFS Hash Region Size: 512 (0x200) bytes

RomFS Offset: 3679232 (0x382400)
RomFS Size: 637202432 (0x25FAF000) bytes
RomFS Hash Region Size: 512 (0x200) bytes

ExeFS Superblock Hash: B7F6B2FE7ACC025346E9466E2D4527D432F78069577EF65C7BD5B73B95589ADD
RomFS Superblock Hash: 7157E020E040E6FD6C4040BEFC0AB1EDC36BB4278C70B74D7E2BA5E743ECB755
SMDH Info:

Magic: "SMDH"
Version: 0.0.0
Title Structure:

-------------------------------

SMDH Title Name Structure:

Language: Japanese (JP)

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo

-------------------------------

SMDH Title Name Structure:

Language: English (EN)

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo

-------------------------------

SMDH Title Name Structure:

Language: French (FR)

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo

-------------------------------

SMDH Title Name Structure:

Language: German

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo

-------------------------------

SMDH Title Name Structure:

Language: Italian

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo

-------------------------------

SMDH Title Name Structure:

Language: Spanish

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo

-------------------------------

SMDH Title Name Structure:

Language: Simplified Chinese

Long Title:

Short Title:

Publisher:

-------------------------------

SMDH Title Name Structure:

Language: Korean

Long Title:

Short Title:

Publisher:

-------------------------------

SMDH Title Name Structure:

Language: Dutch

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo

-------------------------------

SMDH Title Name Structure:

Language: Portuguese

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo

-------------------------------

SMDH Title Name Structure:

Language: Russian

Long Title: MARIO KART 7

Short Title: MARIO KART 7

Publisher: Nintendo


Age Ratings:

CERO (Japan): Not enabled

ESRB (USA): Not enabled

USK (Germany): 0 years and up

PEGI GEN (Europe): 3 years and up

PEGI PRT (Portugal): 4 years and up

PEGI BBFC (England): 3 years and up

COB (Australia): 0 years and up

GRB (South Korea): Not enabled

CGSRR (Taiwan): Not enabled

Region Lockout Region: Europe (EU)
Online Play Matchmaker ID: 00060300
Online Play Matchmaker Bit ID: FF0F000000000000
EULA (End User License Agreement) Version: 0.0.1
Optional Animation Default Frame: 00000000
CEC (StreetPass) ID: 198144

SMDH Application Settings Flags:

Will this title be visible on the HOME Menu? yes
Will this title (if gamecart title) be automatically launched on system boot? no
Does this title utilize 3D? yes
Do you have to accept the Nintendo 3DS EULA (End User License Agreement) to launch this title? no
Does this title automatically save it's data when exiting from HOME menu? yes
Does this title use an extended banner? no
Is game rating required for this title? yes
Does this title use Save Data? yes
Will data be recorded in the Activity Log (and other places) for this title? yes
Are SD Card Save Data backups disabled for this title? no
Is this title New Nintendo 3DS exclusive? no


Content 0001.00000001:


NCCH INFO:

Magic: NCCH
Content Size: 2650624 (0x287200) bytes - 20 blocks
Title ID: 0005000000030700
Maker Code: 3030 ("00")
Version: 0.0.0 (0)
Program ID: 0004000000030700
Logo Region Hash: 0000000000000000000000000000000000000000000000000000000000000000

Product Code: CTR-P-CTAP

Product Code Information:

Console: Original (Old) Nintendo 3DS (CTR)
Content Type: Game Data
Region: Europe (E)

Extended Header Hash: 0000000000000000000000000000000000000000000000000000000000000000
Extended Header Size: 0 (0x0)

NCCH Flags:

Crypto Method: 0
Content Platform: CTR_Old3DS
Content Unit Size: 1024 (0x400) bytes
Content Type: Data, CTR File Archive (CFA) NCCH
Encrypted: yes

Plain Region Offset: 0 (0x0)
Plain Region Size: 0 (0x0) bytes

Plain Region:

(Empty)

Logo Region Offset: 0 (0x0)
Logo Region Size: 0 (0x0) bytes

ExeFS Offset: 0 (0x0)
ExeFS Size: 0 (0x0) bytes
ExeFS Hash Region Size: 0 (0x0) bytes

RomFS Offset: 512 (0x200)
RomFS Size: 2650112 (0x287000) bytes
RomFS Hash Region Size: 512 (0x200) bytes

ExeFS Superblock Hash: 0000000000000000000000000000000000000000000000000000000000000000
RomFS Superblock Hash: 64696C7D052DBC670047FE8C8A15E21A8F550BA3F7B41ECCBAEB28D55A44D214
SMDH Info:

Magic: "N/A"
Version: N/A
Title Structure:

-------------------------------

SMDH Title Name Structure:

Language: N/A

Long Title: N/A

Short Title: N/A

Publisher: N/A


Age Ratings:

N/A: Not enabled

Region Lockout Region: N/A
Online Play Matchmaker ID: N/A
Online Play Matchmaker Bit ID: N/A
EULA (End User License Agreement) Version: N/A
Optional Animation Default Frame: N/A
CEC (StreetPass) ID: N/A

SMDH Application Settings Flags:

Will this title be visible on the HOME Menu? no
Will this title (if gamecart title) be automatically launched on system boot? no
Does this title utilize 3D? no
Do you have to accept the Nintendo 3DS EULA (End User License Agreement) to launch this title? no
Does this title automatically save it's data when exiting from HOME menu? no
Does this title use an extended banner? no
Is game rating required for this title? no
Does this title use Save Data? no
Will data be recorded in the Activity Log (and other places) for this title? no
Are SD Card Save Data backups disabled for this title? no
Is this title New Nintendo 3DS exclusive? no


Content 0002.00000002:


NCCH INFO:

Magic: NCCH
Content Size: 27808256 (0x1A85200) bytes - 212 blocks
Title ID: 0006000000030700
Maker Code: 3030 ("00")
Version: 0.0.0 (0)
Program ID: 0004000000030700
Logo Region Hash: 0000000000000000000000000000000000000000000000000000000000000000

Product Code: CTR-P-CTAP

Product Code Information:

Console: Original (Old) Nintendo 3DS (CTR)
Content Type: Game Data
Region: Europe (E)

Extended Header Hash: 0000000000000000000000000000000000000000000000000000000000000000
Extended Header Size: 0 (0x0)

NCCH Flags:

Crypto Method: 0
Content Platform: CTR_Old3DS
Content Unit Size: 1024 (0x400) bytes
Content Type: Data, CTR File Archive (CFA) NCCH
Encrypted: yes

Plain Region Offset: 0 (0x0)
Plain Region Size: 0 (0x0) bytes

Plain Region:

(Empty)

Logo Region Offset: 0 (0x0)
Logo Region Size: 0 (0x0) bytes

ExeFS Offset: 0 (0x0)
ExeFS Size: 0 (0x0) bytes
ExeFS Hash Region Size: 0 (0x0) bytes

RomFS Offset: 512 (0x200)
RomFS Size: 27807744 (0x1A85000) bytes
RomFS Hash Region Size: 512 (0x200) bytes

ExeFS Superblock Hash: 0000000000000000000000000000000000000000000000000000000000000000
RomFS Superblock Hash: 5F0E7710B2B9D4811971E7BAA11341149F6C7499E2A10498FEA1C46AB116D645
SMDH Info:

Magic: "N/A"
Version: N/A
Title Structure:

-------------------------------

SMDH Title Name Structure:

Language: N/A

Long Title: N/A

Short Title: N/A

Publisher: N/A


Age Ratings:

N/A: Not enabled

Region Lockout Region: N/A
Online Play Matchmaker ID: N/A
Online Play Matchmaker Bit ID: N/A
EULA (End User License Agreement) Version: N/A
Optional Animation Default Frame: N/A
CEC (StreetPass) ID: N/A

SMDH Application Settings Flags:

Will this title be visible on the HOME Menu? no
Will this title (if gamecart title) be automatically launched on system boot? no
Does this title utilize 3D? no
Do you have to accept the Nintendo 3DS EULA (End User License Agreement) to launch this title? no
Does this title automatically save it's data when exiting from HOME menu? no
Does this title use an extended banner? no
Is game rating required for this title? no
Does this title use Save Data? no
Will data be recorded in the Activity Log (and other places) for this title? no
Are SD Card Save Data backups disabled for this title? no
Is this title New Nintendo 3DS exclusive? no
```

Will show the same output as verbose mode, but with the title names in all languages that apply, game ratings, and HOME Menu settings.
</details>

Credit goes to [ihaveamac](https://github.com/ihaveamac) for [ninfs](https://github.com/ihaveamac/ninfs).

This Progam uses [CTR.NET](https://github.com/TimmSkiller/CTR.NET).
