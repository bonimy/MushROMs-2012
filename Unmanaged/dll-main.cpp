#include <stdlib.h>
#include <string.h>

//Defines a keyword for exporting dll functions
#define EXPORT extern "C" __declspec(dllexport)

EXPORT void* CreatePointer(const int size)
{
	return malloc(size);
}

EXPORT void* CreateEmptyPointer(const int size)
{
	return calloc(size, 1);
}

EXPORT void* ResizePointer(void* ptr, const int size)
{
	return realloc(ptr, size);
}

EXPORT void FreePointer(void* ptr)
{
	free(ptr);
}

EXPORT void* SetMemory(void* ptr, const int value, const int size)
{
	return memset(ptr, value, size);
}

EXPORT void* MoveMemory(void* dest, const void* src, const int size)
{
	return memmove(dest, src, size);
}

EXPORT void* CopyMemory(void* dest, const void* src, const int size)
{
	return memcpy(dest, src, size);
}

EXPORT int CompareMemory(const void* ptr1, const void* ptr2, const int size)
{
	return memcmp(ptr1, ptr2, size);
}