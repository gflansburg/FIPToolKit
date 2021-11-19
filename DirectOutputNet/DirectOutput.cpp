#include "stdafx.h"
#include "DirectOutput.h"
#define STRING(text) gcnew String(TEXT(text))
#define _moduleLoadFunction(name) _##name = (Pfn_DirectOutput_##name)GetProcAddress(_module, "DirectOutput_"#name)
#define switchHr(hr) auto _hr = hr; switch (_hr)
#define throwHr(message) default: throw gcnew System::Runtime::InteropServices::ExternalException(message, _hr);
#define outOfMemory gcnew System::Runtime::InteropServices::ExternalException("Ran out of memory", E_OUTOFMEMORY)
#define handle gcnew System::Runtime::InteropServices::ExternalException("Invalid handle", E_HANDLE)
#define throwIfOutOfMemory(message) case E_OUTOFMEMORY: throw gcnew System::OutOfMemoryException(message, outOfMemory);
#define throwIfHandle(message) case E_HANDLE: throw gcnew System::InvalidOperationException(message, handle);
#define throwIfInvalidArg(message, paramName) case E_INVALIDARG: throw gcnew System::ArgumentException(message, paramName, gcnew System::Runtime::InteropServices::ExternalException("One or more argruments are invalid", E_INVALIDARG));
#define throwIfNotImpl(message) case E_NOTIMPL: throw gcnew System::NotImplementedException(message, gcnew System::Runtime::InteropServices::ExternalException("Not implemented", E_NOTIMPL));
#define deviceThrowIfPageNotActive(message) case E_PAGENOTACTIVE: throw gcnew Saitek::DirectOutput::PageNotActiveException(message);
#define deviceThrowIfOutOfMemory case E_OUTOFMEMORY: throw gcnew System::OutOfMemoryException("Insufficient memory to complete the request.", outOfMemory);
#define deviceThrowIfHandle case E_HANDLE: throw gcnew System::ArgumentException("The device handle specified is invalid.", "device", handle);

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Drawing;
using namespace System::Drawing::Imaging;
using namespace System::Runtime::InteropServices;
using namespace Saitek::DirectOutput;

PageNotActiveException::PageNotActiveException(String^ message) :
	AccessViolationException(message, gcnew ExternalException(message, E_PAGENOTACTIVE)) { }

bool DirectOutputClient::_GetDirectOutputFilename(LPTSTR filename, DWORD length) {
	bool retval(false);
	HKEY hk;
	// Read the Full Path to DirectOutput.dll from the registry
	long lRet = RegOpenKeyEx(HKEY_LOCAL_MACHINE, TEXT("SOFTWARE\\Saitek\\DirectOutput"), 0, KEY_READ, &hk);
	if (ERROR_SUCCESS == lRet)
	{
		DWORD size(length * sizeof(filename[0]));
		// Note: this DirectOutput entry will point to the correct version on x86 or x64 systems
		lRet = RegQueryValueEx(hk, TEXT("DirectOutput"), 0, 0, (LPBYTE)filename, &size);
		if (ERROR_SUCCESS == lRet)
			retval = true;
		RegCloseKey(hk);
	}
	return retval;
}

String^ DirectOutputClient::GetDirectOutputFilename() {
	TCHAR filename[2048] = { 0 };
	auto got = _GetDirectOutputFilename(filename, sizeof(filename) / sizeof(filename[0]));
	return got ? gcnew String(filename) : nullptr;
}

DirectOutputClient::DirectOutputClient() :
	_this(GCHandle::Alloc(this, GCHandleType::WeakTrackResurrection)) {
	TCHAR filename[2048] = { 0 };
	if (!_GetDirectOutputFilename(filename, sizeof(filename) / sizeof(filename[0])))
		throw gcnew InvalidOperationException(STRING("DirectOutput not installed."));
	_module = LoadLibrary(filename);
	if (_module) {
		_moduleLoadFunction(Initialize);
		_moduleLoadFunction(Deinitialize);
		_moduleLoadFunction(RegisterDeviceCallback);
		_moduleLoadFunction(Enumerate);
		_moduleLoadFunction(GetDeviceType);
		_moduleLoadFunction(GetDeviceInstance);
		_moduleLoadFunction(GetSerialNumber);
		_moduleLoadFunction(SetProfile);
		_moduleLoadFunction(RegisterSoftButtonCallback);
		_moduleLoadFunction(RegisterPageCallback);
		_moduleLoadFunction(AddPage);
		_moduleLoadFunction(RemovePage);
		_moduleLoadFunction(SetLed);
		_moduleLoadFunction(SetString);
		_moduleLoadFunction(SetImage);
		_moduleLoadFunction(SetImageFromFile);
		_moduleLoadFunction(StartServer);
		_moduleLoadFunction(CloseServer);
		_moduleLoadFunction(SendServerMsg);
		_moduleLoadFunction(SendServerFile);
		_moduleLoadFunction(SaveFile);
		_moduleLoadFunction(DisplayFile);
		_moduleLoadFunction(DeleteFile);
	}
	else {
		throw gcnew InvalidOperationException(STRING("DirectOutput could not be loaded."));
	}
}

DirectOutputClient::~DirectOutputClient() {
	if (_registeredDeviceCallback)
		_RegisterDeviceCallback(NULL, NULL);
	if (_this.IsAllocated)
		_this.Free();
	FreeLibrary(_module);
}

void DirectOutputClient::Initialize(String^ pluginName) {
	if (!_Initialize)
		throw gcnew NotImplementedException;
	switchHr(_Initialize(pluginName ? CString(pluginName) : (LPCTSTR)NULL)) {
	case S_OK: break;
		throwIfOutOfMemory("There was insufficient memory to complete this call.")
			throwIfInvalidArg("The argument is invalid.", "pluginName")
			throwIfHandle("The DirectOutputManager process cound not be found.")
			throwHr("Unable to initialize DirectOutput.")
	}
}

void DirectOutputClient::Deinitialize() {
	if (!_Deinitialize)
		throw gcnew NotImplementedException;
	switchHr(_Deinitialize()) {
	case S_OK: break;
		throwIfHandle("DirectOutput was not initialized or was already deinitialized.")
			throwHr("Unable to deinitialize DirectOutput.")
	}
}

void _DeviceChangeCallback(void* hDevice, bool bAdded, void* pvContext) {
	auto _this = (DirectOutputClient^)GCHandle::FromIntPtr((IntPtr)pvContext).Target;
	if (!_this)
		throw gcnew ObjectDisposedException("DirectOutput client disposed.");
	_this->DeviceChanged(_this, gcnew DeviceChangedEventArgs(IntPtr(hDevice), bAdded));
}

void DirectOutputClient::DeviceChanged::add(DeviceChangedEventHandler^ handler) {
	if (!_RegisterDeviceCallback)
		throw gcnew NotImplementedException;
	if (!_registeredDeviceCallback) {
		switchHr(_RegisterDeviceCallback(_DeviceChangeCallback, (void*)(IntPtr)_this)) {
	case S_OK: break;
		throwIfHandle("DirectOutput was not initialized.")
			throwHr("Unable to register the device change callback.")
		}
	}
	++_registeredDeviceCallback;
	_DeviceChanged += handler;
}

void DirectOutputClient::DeviceChanged::remove(DeviceChangedEventHandler^ handler) {
	if (!--_registeredDeviceCallback)
		_RegisterDeviceCallback(NULL, NULL);
	_DeviceChanged -= handler;
}

void DirectOutputClient::DeviceChanged::raise(Object^ sender, DeviceChangedEventArgs^ e) {
	if (_DeviceChanged)
		_DeviceChanged->Invoke(sender, e);
}

void _DeviceEnumerateCallback(void* hDevice, void* pvContext) { (*(List<IntPtr>^*)pvContext)->Add(IntPtr(hDevice)); }

array<IntPtr>^ DirectOutputClient::GetDeviceHandles() {
	if (!_Enumerate)
		throw gcnew NotImplementedException;
	auto devices = gcnew List<IntPtr>;
	switchHr(_Enumerate(_DeviceEnumerateCallback, &devices)) {
	case S_OK: break;
		throwIfHandle("DirectOutput was not initialized.")
			throwHr("Unable to enumerate devices.")
	}
	return devices->ToArray();
}

DeviceClient^ DirectOutputClient::CreateDeviceClient(System::IntPtr device) {
	if (_GetDeviceType) {
		GUID guid;
		if (S_OK == _GetDeviceType((void*)device, &guid)) {
			if (guid == DeviceType_Fip)
				return gcnew FipClient(this, (void*)device);
		}
	}
	return gcnew DeviceClient(this, (void*)device);
}

DeviceClient::DeviceClient(DirectOutputClient^ client, void* device) :
	_client(client), _device(device), _this(GCHandle::Alloc(this, GCHandleType::WeakTrackResurrection)) { }

DeviceClient::~DeviceClient() {
	if (_registeredPageCallback)
		_client->_RegisterPageCallback(_device, NULL, NULL);
	if (_registeredSoftButtonCallback)
		_client->_RegisterSoftButtonCallback(_device, NULL, NULL);
	for each(auto page in pages)
		this->RemovePage(page);
	if (_this.IsAllocated)
		_this.Free();
}

Guid DeviceClient::GetDeviceType() {
	if (!_client->_GetDeviceType)
		throw gcnew NotImplementedException;
	GUID guid;
	switchHr(_client->_GetDeviceType(_device, &guid)) {
	case S_OK: break;
		throwIfInvalidArg("An argument in invalid.", "device")
			throwIfHandle("DirectOutput was not initialized.")
			throwHr("Unable to get the device type.")
	}
	return FromGUID(guid);
}

Guid DeviceClient::GetDeviceInstance() {
	if (!_client->_GetDeviceInstance)
		throw gcnew NotImplementedException;
	GUID guid;
	switchHr(_client->_GetDeviceInstance(_device, &guid)) {
	case S_OK: break;
		throwIfNotImpl("This device does not support DirectInput.")
			throwIfInvalidArg("An argument in invalid.", "device")
			throwIfHandle("DirectOutput was not initialized.")
			throwHr("Unable to get the device instance.")
	}
	return FromGUID(guid);
}

String^ DeviceClient::GetSerialNumber() {
	if (!_client->_GetSerialNumber)
		throw gcnew NotImplementedException;
	wchar_t s[16] = { 0 };
	switchHr(_client->_GetSerialNumber(_device, s, 16)) {
	case S_OK: break;
		throwHr("Unable to get serial number.")
	}
	return gcnew String(s);
}

void DeviceClient::SetProfile(String^ filename) {
	if (!_client->_SetProfile)
		throw gcnew NotImplementedException;
	if (String::IsNullOrEmpty(filename))
		throw gcnew ArgumentException("Null or empty filename.", "filename");
	switchHr(_client->_SetProfile(_device, filename->Length, (CString)filename)) {
	case S_OK: break;
		throwIfNotImpl("The device does not support profiling.")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to set the profile.")
	}
}

void _SoftButtonCallback(void* hDevice, DWORD dwButtons, void* pvContext) {
	auto _this = (DeviceClient^)GCHandle::FromIntPtr((IntPtr)pvContext).Target;
	if (!_this)
		throw gcnew ObjectDisposedException("Device client disposed.");
	if (hDevice != _this->_device)
		throw gcnew ExternalException("Soft buttons callback to different device client.");
	_this->SoftButtons(_this, gcnew SoftButtonsEventArgs((SoftButtons)dwButtons));
}

void DeviceClient::SoftButtons::add(SoftButtonsEventHandler^ handler) {
	if (!_client->_RegisterSoftButtonCallback)
		throw gcnew NotImplementedException;
	if (!_registeredSoftButtonCallback) {
		switchHr(_client->_RegisterSoftButtonCallback(_device, _SoftButtonCallback, (void*)(IntPtr)_this)) {
	case S_OK: break;
		deviceThrowIfHandle
			throwHr("Unable to register the soft buttons callback.")
		}
	}
	++_registeredSoftButtonCallback;
	_SoftButtons += handler;
}

void DeviceClient::SoftButtons::remove(SoftButtonsEventHandler^ handler) {
	if (!--_registeredSoftButtonCallback)
		_client->_RegisterSoftButtonCallback(_device, NULL, NULL);
	_SoftButtons -= handler;
}

void DeviceClient::SoftButtons::raise(Object^ sender, SoftButtonsEventArgs^ e) {
	if (_SoftButtons)
		_SoftButtons->Invoke(sender, e);
}

void _PageCallback(void* hDevice, DWORD dwPage, bool bActivated, void* pvContext) {
	auto _this = (DeviceClient^)GCHandle::FromIntPtr((IntPtr)pvContext).Target;
	if (!_this)
		throw gcnew ObjectDisposedException("Device client disposed.");
	if (hDevice != _this->_device)
		throw gcnew ExternalException("Page callback to different device client.");
	_this->Page(_this, gcnew PageEventArgs(dwPage, bActivated));
}

void DeviceClient::Page::add(PageEventHandler^ handler) {
	if (!_client->_RegisterPageCallback)
		throw gcnew NotImplementedException;
	if (!_registeredPageCallback) {
		switchHr(_client->_RegisterPageCallback(_device, _PageCallback, (void*)(IntPtr)_this)) {
	case S_OK: break;
		deviceThrowIfHandle
			throwHr("Unable to register the page callback.")
		}
	}
	++_registeredPageCallback;
	_Page += handler;
}

void DeviceClient::Page::remove(PageEventHandler^ handler) {
	if (!--_registeredPageCallback)
		_client->_RegisterPageCallback(_device, NULL, NULL);
	_Page -= handler;
}

void DeviceClient::Page::raise(Object^ sender, PageEventArgs^ e) {
	if (_Page)
		_Page->Invoke(sender, e);
}

void DeviceClient::AddPage(DWORD page, PageFlags flags) {
	if (!_client->_AddPage)
		throw gcnew NotImplementedException;
	switchHr(_client->_AddPage(_device, page, (DWORD)flags)) {
	case S_OK: pages->Add(page); break;
		deviceThrowIfOutOfMemory
			throwIfInvalidArg("The page already exists.", "page")
			deviceThrowIfHandle
			throwHr("Unable to add the page.")
	}
}

void DeviceClient::RemovePage(DWORD page) {
	if (!_client->_RemovePage)
		throw gcnew NotImplementedException;
	switchHr(_client->_RemovePage(_device, page)) {
	case S_OK: pages->Remove(page); break;
		throwIfInvalidArg("The page does not reference a valid page id.", "page")
			deviceThrowIfHandle
			throwHr("Unable to remove the page.")
	}
}

void DeviceClient::SetLed(DWORD page, DWORD index, bool value) {
	if (!_client->_SetLed)
		throw gcnew NotImplementedException;
	switchHr(_client->_SetLed(_device, page, index, value)) {
	case S_OK: break;
		deviceThrowIfPageNotActive("The specified page is not active. Displaying information is not permitted when the page is not active.")
			throwIfInvalidArg("The page does not reference a valid page id, or the index does not specifiy a valid LED id.", "page|id")
			deviceThrowIfHandle
			throwHr("Unable to set LED.")
	}
}

void DeviceClient::SetString(DWORD page, DWORD index, String^ value) {
	if (!_client->_SetString)
		throw gcnew NotImplementedException;
	switchHr(_client->_SetString(_device, page, index, value ? value->Length : 0, value ? CString(value) : (LPCTSTR)NULL)) {
	case S_OK: break;
		deviceThrowIfPageNotActive("The specified page is not active. Displaying information is not permitted when the page is not active.")
			throwIfInvalidArg("The page does not reference a valid page id, or the index does not reference a valid string id.", "page|id")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to set string.")
	}
}

void DeviceClient::SetImage(DWORD page, DWORD index, array<Byte>^ value) {
	if (!_client->_SetImage)
		throw gcnew NotImplementedException;
	pin_ptr<Byte> _value = &value[0];
	switchHr(_client->_SetImage(_device, page, index, value->Length, _value)) {
	case S_OK: break;
		deviceThrowIfPageNotActive("The specified page is not active. Displaying information is not permitted when the page is not active.")
			throwIfInvalidArg("The page argument does not reference a valid page id, or the index does not reference a valid image id.", "page|index")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to set image.")
	}
}

void DeviceClient::SetImageFromFile(DWORD page, DWORD index, String^ filename) {
	if (!_client->_SetImageFromFile)
		throw gcnew NotImplementedException;
	switchHr(_client->_SetImageFromFile(_device, page, index, filename->Length, (CString)filename)) {
	case S_OK: break;
		deviceThrowIfPageNotActive("The specified page is not active. Displaying information is not permitted when the page is not active.")
			throwIfInvalidArg("The page does not refereence a valid page id, or the index does not reference a valid image id.", "page|index")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to set image from file.")
	}
}

void _ServerRequestStatus(ServerRequestStatus% managed, SRequestStatus &native) {
	managed.HeaderError = native.dwHeaderError;
	managed.HeaderInfo = native.dwHeaderInfo;
	managed.RequestError = native.dwRequestError;
	managed.RequestInfo = native.dwRequestInfo;
}

void _StartServer(DirectOutputClient^ client, void* device, String^ filename, DWORD& serverId, SRequestStatus* status) {
	if (!client->_StartServer)
		throw gcnew NotImplementedException;
	switchHr(client->_StartServer(device, filename->Length, (CString)filename, &serverId, status)) {
	case S_OK: break;
		throwIfNotImpl("The device does not support server applications.")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to start server.")
	}
}

void DeviceClient::StartServer(String^ filename, DWORD% serverId) {
	DWORD _serverId;
	try { _StartServer(_client, _device, filename, _serverId, NULL); }
	finally { serverId = _serverId; }
}

void DeviceClient::StartServer(String^ filename, DWORD% serverId, ServerRequestStatus% status) {
	DWORD _serverId;
	SRequestStatus _status;
	try { _StartServer(_client, _device, filename, _serverId, &_status); }
	finally {
		serverId = _serverId;
		_ServerRequestStatus(status, _status);
	}
}

void _CloseServer(DirectOutputClient^ client, void* device, DWORD serverId, SRequestStatus* status) {
	if (!client->_CloseServer)
		throw gcnew NotImplementedException;
	switchHr(client->_CloseServer(device, serverId, status)) {
	case S_OK: break;
		throwIfNotImpl("The device does not support server applications.")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("The device handle specified is invalid.");
	}
}

inline void DeviceClient::CloseServer(DWORD serverId) { _CloseServer(_client, _device, serverId, NULL); }

void DeviceClient::CloseServer(DWORD serverId, ServerRequestStatus% status) {
	SRequestStatus _status;
	try { _CloseServer(_client, _device, serverId, &_status); }
	finally { _ServerRequestStatus(status, _status); }
}

void _SendServerMessage(DirectOutputClient^ client, void* device, DWORD serverId, DWORD request, DWORD page, array<Byte>^ in, array<Byte>^ out, SRequestStatus* status) {
	if (!client->_SendServerMsg)
		throw gcnew NotImplementedException;
	pin_ptr<Byte> _in = &in[0], _out = &out[0];
	switchHr(client->_SendServerMsg(device, serverId, request, page, in->Length, _in, out->Length, _out, status)) {
	case S_OK: break;
		throwIfNotImpl("The device does not support server applications.")
			deviceThrowIfPageNotActive("The specified page is not active and the server application attempted to access the display.")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to send server message.");
	}
}

void DeviceClient::SendServerMessage(DWORD serverId, DWORD request, DWORD page, array<Byte>^ in, array<Byte>^ out) { _SendServerMessage(_client, _device, serverId, request, page, in, out, NULL); }

void DeviceClient::SendServerMessage(DWORD serverId, DWORD request, DWORD page, array<Byte>^ in, array<Byte>^ out, ServerRequestStatus% status) {
	SRequestStatus _status;
	try { _SendServerMessage(_client, _device, serverId, request, page, in, out, &_status); }
	finally { _ServerRequestStatus(status, _status); }
}

void _SendServerFile(DirectOutputClient^ client, void* device, DWORD serverId, DWORD request, DWORD page, array<Byte>^ inHeader, String^ filename, array<Byte>^ out, SRequestStatus* status) {
	if (!client->_SendServerFile)
		throw gcnew NotImplementedException;
	pin_ptr<Byte> _inHeader = &inHeader[0], _out = &out[0];
	switchHr(client->_SendServerFile(device, serverId, request, page, inHeader->Length, _inHeader, filename->Length, (CString)filename, out->Length, _out, status)) {
	case S_OK: break;
		throwIfNotImpl("The device does not support server applications.")
			deviceThrowIfPageNotActive("The specified page is not active and the server application attempted to access the display.")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to send server file.");
	}
}

void DeviceClient::SendServerFile(DWORD serverId, DWORD request, DWORD page, array<Byte>^ inHeader, String^ filename, array<Byte>^ out) { _SendServerFile(_client, _device, serverId, request, page, inHeader, filename, out, NULL); }

void DeviceClient::SendServerFile(DWORD serverId, DWORD request, DWORD page, array<Byte>^ inHeader, String^ filename, array<Byte>^ out, ServerRequestStatus % status) {
	SRequestStatus _status;
	try { _SendServerFile(_client, _device, serverId, request, page, inHeader, filename, out, NULL); }
	finally { _ServerRequestStatus(status, _status); }
}

void _SaveFile(DirectOutputClient^ client, void* device, DWORD page, DWORD file, String^ filename, SRequestStatus* status) {
	if (!client->_SaveFile)
		throw gcnew NotImplementedException;
	switchHr(client->_SaveFile(device, page, file, filename->Length, (CString)filename, status)) {
	case S_OK: break;
		throwIfNotImpl("The device does not support saving files.")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to save file.");
	}
}

void DeviceClient::SaveFile(DWORD page, DWORD file, String^ filename) { _SaveFile(_client, _device, page, file, filename, NULL); }

void DeviceClient::SaveFile(DWORD page, DWORD file, String^ filename, ServerRequestStatus% status) {
	SRequestStatus _status;
	try { _SaveFile(_client, _device, page, file, filename, &_status); }
	finally { _ServerRequestStatus(status, _status); }
}

void _DisplayFile(DirectOutputClient^ client, void* device, DWORD page, DWORD index, DWORD file, SRequestStatus* status) {
	if (!client->_DisplayFile)
		throw gcnew NotImplementedException;
	switchHr(client->_DisplayFile(device, page, file, index, status)) {
	case S_OK: break;
		throwIfNotImpl("The device does not support displaying files.")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Undable to display file.")
	}
}

inline void DeviceClient::DisplayFile(DWORD page, DWORD index, DWORD file) { _DisplayFile(_client, _device, page, index, file, NULL); }

void DeviceClient::DisplayFile(DWORD page, DWORD index, DWORD file, ServerRequestStatus% status) {
	SRequestStatus _status;
	try { _DisplayFile(_client, _device, page, index, file, &_status); }
	finally { _ServerRequestStatus(status, _status); }
}

void _DeleteFile(DirectOutputClient^ client, void* device, DWORD page, DWORD file, SRequestStatus* status) {
	if (!client->_DeleteFile)
		throw gcnew NotImplementedException;
	switchHr(client->_DeleteFile(device, page, file, status)) {
	case S_OK: break;
		throwIfNotImpl("The device does not support deleting files.")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to delete file.")
	}
}

inline void DeviceClient::DeleteFile(DWORD page, DWORD file) { _DeleteFile(_client, _device, page, file, NULL); }

void DeviceClient::DeleteFile(DWORD page, DWORD file, ServerRequestStatus% status) {
	SRequestStatus _status;
	try { _DeleteFile(_client, _device, page, file, NULL); }
	finally { _ServerRequestStatus(status, _status); }
}

void FipClient::SetImage(DWORD page, Image^ image)
{
	byte data[230400];
	auto dispose = false;
	Bitmap^ bitmap = dynamic_cast<Bitmap^>(image);
	try {
		if (bitmap == nullptr || bitmap->PixelFormat != PixelFormat::Format24bppRgb || bitmap->Width != 320 || bitmap->Height != 240) {
			dispose = true;
			bitmap = gcnew Bitmap(320, 240, PixelFormat::Format24bppRgb);
			auto g = Graphics::FromImage(bitmap);
			try { g->DrawImage(image, 0, 0, 320, 240); }
			finally { delete g; }
		}
		BitmapData^ b;
		try {
			b = bitmap->LockBits(System::Drawing::Rectangle(0, 0, 320, 240), ImageLockMode::ReadOnly, PixelFormat::Format24bppRgb);
			for (auto i = 0; i < 240; ++i) {
				switchHr(memcpy_s(data + b->Stride*(239 - i), 960, (void*)(b->Scan0 + b->Stride * i), 960)) {
	case S_OK: break;
	case EINVAL: throw gcnew ArgumentException("Invalid argument.", "dest|destSize|src|count");
		throwHr("Error copying bytes between buffers.");
				}
			}
		}
		finally{
			bitmap->UnlockBits(b);
		}
	}
	finally {
		if (dispose)
			delete bitmap;
	}
	switchHr(_client->_SetImage(_device, page, 0, 230400, &data)) {
	case S_OK: break;
		deviceThrowIfPageNotActive("The specified page is not active. Displaying information is not permitted when the page is not active.")
			throwIfInvalidArg("The page argument does not reference a valid page id.", "page")
			deviceThrowIfOutOfMemory
			deviceThrowIfHandle
			throwHr("Unable to set image.")
	}
}