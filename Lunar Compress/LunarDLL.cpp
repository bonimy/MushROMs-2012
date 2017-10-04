/**************************************************************************

   Lunar Compress C Dynamic DLL Access Program
   Created by FuSoYa,  Defender of Relm
   http://fusoya.eludevisibility.org (also http://fusoya.cjb.net)

   Check "LunarDLL.h" for documentation on using this file.

**************************************************************************/

//#include "stdafx.h"  //use this if you get errors compiling under VC
#include <windows.h>
#include "LunarDLL.h"

LCPROC1 LunarVersion;
LCPROC2 LunarCloseFile;
LCPROC3 LunarOpenFile;
LCPROC4 LunarGetFileSize;
LCPROC5 LunarReadFile;
LCPROC6 LunarWriteFile;
LCPROC7 LunarSNEStoPC;
LCPROC8 LunarPCtoSNES;
LCPROC9 LunarDecompress;
LCPROC10 LunarRecompress;
LCPROC11 LunarEraseArea;
LCPROC12 LunarExpandROM;
LCPROC13 LunarVerifyFreeSpace;
LCPROC14 LunarCreatePixelMap;
LCPROC15 LunarCreateBppMap;
LCPROC16 LunarSNEStoPCRGB;
LCPROC17 LunarPCtoSNESRGB;
LCPROC18 LunarRender8x8;
LCPROC19 LunarWriteRatArea;
LCPROC20 LunarEraseRatArea;
LCPROC21 LunarGetRatAreaSize;
LCPROC22 LunarOpenRAMFile;
LCPROC23 LunarSaveRAMFile;
LCPROC24 LunarIPSCreate;
LCPROC25 LunarIPSApply;
LCPROC26 LunarSetFreeBytes;

HINSTANCE LunarLibInst = NULL;


bool LunarUnloadDLL()
{
	if (!LunarLibInst)
		return true;	//the library is already unloaded
	FreeLibrary(LunarLibInst);
	LunarLibInst = NULL;
	return true;   //library unloaded
}

bool LunarLoadDLL()  
{
	unsigned int i = 0;

	if (LunarLibInst)
		return true;	//the library is already loaded
	LunarLibInst = LoadLibrary(L"Lunar Compress");
	if (!LunarLibInst) //hmm,  try the other file name?
		LunarLibInst = LoadLibrary(L"Lunar_Compress");
	if (!LunarLibInst)
		return false;	//Couldn't load DLL...

	// If the handle is valid,  try to get the function addresses.
	i += (LunarVersion = (LCPROC1)GetProcAddress(LunarLibInst, "LunarVersion")) != 0;
	i += (LunarCloseFile = (LCPROC2)GetProcAddress(LunarLibInst, "LunarCloseFile")) != 0;
	i += (LunarOpenFile = (LCPROC3)GetProcAddress(LunarLibInst, "LunarOpenFile")) != 0;
	i += (LunarGetFileSize = (LCPROC4)GetProcAddress(LunarLibInst, "LunarGetFileSize")) != 0;
	i += (LunarReadFile = (LCPROC5)GetProcAddress(LunarLibInst, "LunarReadFile")) != 0;
	i += (LunarWriteFile = (LCPROC6)GetProcAddress(LunarLibInst, "LunarWriteFile")) != 0;
	i += (LunarSNEStoPC = (LCPROC7)GetProcAddress(LunarLibInst, "LunarSNEStoPC")) != 0;
	i += (LunarPCtoSNES = (LCPROC8)GetProcAddress(LunarLibInst, "LunarPCtoSNES")) != 0;
	i += (LunarDecompress = (LCPROC9)GetProcAddress(LunarLibInst, "LunarDecompress")) != 0;
	i += (LunarRecompress = (LCPROC10)GetProcAddress(LunarLibInst, "LunarRecompress")) != 0;
	i += (LunarEraseArea = (LCPROC11)GetProcAddress(LunarLibInst, "LunarEraseArea")) != 0;
	i += (LunarExpandROM = (LCPROC12)GetProcAddress(LunarLibInst, "LunarExpandROM")) != 0;
	i += (LunarVerifyFreeSpace = (LCPROC13)GetProcAddress(LunarLibInst, "LunarVerifyFreeSpace")) != 0;
	i += (LunarCreatePixelMap = (LCPROC14)GetProcAddress(LunarLibInst, "LunarCreatePixelMap")) != 0;
	i += (LunarCreateBppMap = (LCPROC15)GetProcAddress(LunarLibInst, "LunarCreateBppMap")) != 0;
	i += (LunarSNEStoPCRGB = (LCPROC16)GetProcAddress(LunarLibInst, "LunarSNEStoPCRGB")) != 0;
	i += (LunarPCtoSNESRGB = (LCPROC17)GetProcAddress(LunarLibInst, "LunarPCtoSNESRGB")) != 0;
	i += (LunarRender8x8 = (LCPROC18)GetProcAddress(LunarLibInst, "LunarRender8x8")) != 0;
	i += (LunarWriteRatArea = (LCPROC19)GetProcAddress(LunarLibInst, "LunarWriteRatArea")) != 0;
	i += (LunarEraseRatArea = (LCPROC20)GetProcAddress(LunarLibInst, "LunarEraseRatArea")) != 0;
	i += (LunarGetRatAreaSize = (LCPROC21)GetProcAddress(LunarLibInst, "LunarGetRatAreaSize")) != 0;
	i += (LunarOpenRAMFile = (LCPROC22)GetProcAddress(LunarLibInst, "LunarOpenRAMFile")) != 0;
	i += (LunarSaveRAMFile = (LCPROC23)GetProcAddress(LunarLibInst, "LunarSaveRAMFile")) != 0;
	i += (LunarIPSCreate = (LCPROC24)GetProcAddress(LunarLibInst, "LunarIPSCreate")) != 0;
	i += (LunarIPSApply = (LCPROC25)GetProcAddress(LunarLibInst, "LunarIPSApply")) != 0;
	i += (LunarSetFreeBytes = (LCPROC26)GetProcAddress(LunarLibInst, "LunarSetFreeBytes")) != 0;

	if (i >= 26)
		return true;	//load successful!

	LunarUnloadDLL();
	return false;	//we couldn't find all the functions...
}