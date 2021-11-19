#pragma once
#include "DirectOutput_Native.h"
#define FromGUID(guid) System::Guid(guid.Data1, guid.Data2, guid.Data3, guid.Data4[0], guid.Data4[1], guid.Data4[2], guid.Data4[3], guid.Data4[4], guid.Data4[5], guid.Data4[6], guid.Data4[7])

namespace Saitek {
	namespace DirectOutput {
		public ref class PageNotActiveException : System::AccessViolationException {
		internal:
			PageNotActiveException(System::String^ message);
		};

		public ref class DeviceChangedEventArgs : System::EventArgs
		{
			System::IntPtr _Device;
			bool _Added;

		public:
			DeviceChangedEventArgs(System::IntPtr device, bool added) :
				_Device(device), _Added(added) { }

			property System::IntPtr Device {
				inline System::IntPtr get() { return _Device; }
			}

			property bool Added {
				inline bool get() { return _Added; }
			}
		};

		public delegate void DeviceChangedEventHandler(System::Object^ sender, DeviceChangedEventArgs^ e);

		ref class DeviceClient;

		public ref class DirectOutputClient
		{
			static bool _GetDirectOutputFilename(LPTSTR filename, DWORD length);
			System::Runtime::InteropServices::GCHandle _this;

			HMODULE _module = NULL;

			Pfn_DirectOutput_Initialize _Initialize = NULL;
			Pfn_DirectOutput_Deinitialize _Deinitialize = NULL;
			Pfn_DirectOutput_RegisterDeviceCallback _RegisterDeviceCallback = NULL;
			Pfn_DirectOutput_Enumerate _Enumerate = NULL;

			int _registeredDeviceCallback = 0;
			DeviceChangedEventHandler^ _DeviceChanged;

		internal:
			Pfn_DirectOutput_GetDeviceType _GetDeviceType = NULL;
			Pfn_DirectOutput_GetDeviceInstance _GetDeviceInstance = NULL;
			Pfn_DirectOutput_GetSerialNumber _GetSerialNumber = NULL;
			Pfn_DirectOutput_SetProfile _SetProfile = NULL;
			Pfn_DirectOutput_RegisterSoftButtonCallback _RegisterSoftButtonCallback = NULL;
			Pfn_DirectOutput_RegisterPageCallback _RegisterPageCallback = NULL;
			Pfn_DirectOutput_AddPage _AddPage = NULL;
			Pfn_DirectOutput_RemovePage _RemovePage = NULL;
			Pfn_DirectOutput_SetLed _SetLed = NULL;
			Pfn_DirectOutput_SetString _SetString = NULL;
			Pfn_DirectOutput_SetImage _SetImage = NULL;
			Pfn_DirectOutput_SetImageFromFile _SetImageFromFile = NULL;
			Pfn_DirectOutput_StartServer _StartServer = NULL;
			Pfn_DirectOutput_CloseServer _CloseServer = NULL;
			Pfn_DirectOutput_SendServerMsg _SendServerMsg = NULL;
			Pfn_DirectOutput_SendServerFile _SendServerFile = NULL;
			Pfn_DirectOutput_SaveFile _SaveFile = NULL;
			Pfn_DirectOutput_DisplayFile _DisplayFile = NULL;
			Pfn_DirectOutput_DeleteFile _DeleteFile = NULL;

		public:
			static System::String^ GetDirectOutputFilename();

			DirectOutputClient();
			~DirectOutputClient();

			inline void Initialize() { Initialize(nullptr); }
			void Initialize(System::String^ pluginName);

			void Deinitialize();

			event DeviceChangedEventHandler^ DeviceChanged {
				void add(DeviceChangedEventHandler^ handler);
				void remove(DeviceChangedEventHandler^ handler);
		internal:
			void raise(System::Object^ sender, DeviceChangedEventArgs^ e);
			}

			cli::array<System::IntPtr>^ GetDeviceHandles();
			DeviceClient^ CreateDeviceClient(System::IntPtr device);
		};

		[System::Flags]
		public enum class SoftButtons : DWORD {
			None = NULL,
			Select = SoftButton_Select,
			Up = SoftButton_Up,
			Down = SoftButton_Down,
			Left = SoftButton_Right,
			Right = SoftButton_Left,
			Button1 = SoftButton_1,
			Button2 = SoftButton_2,
			Button3 = SoftButton_3,
			Button4 = SoftButton_4,
			Button5 = SoftButton_5,
			Button6 = SoftButton_6
		};

		public ref class SoftButtonsEventArgs : System::EventArgs {
			SoftButtons _Buttons;

		public:
			SoftButtonsEventArgs(SoftButtons buttons) :
				_Buttons(buttons) { }

			property SoftButtons Buttons {
				inline SoftButtons get() { return _Buttons; }
			}
		};

		public delegate void SoftButtonsEventHandler(System::Object^ sender, SoftButtonsEventArgs^ e);

		public ref class PageEventArgs : System::EventArgs {
			DWORD _page;
			bool _activated;

		public:
			PageEventArgs(DWORD page, bool activated) :
				_page(page), _activated(activated) { }

			property DWORD Page {
				inline DWORD get() { return _page; }
			}

			property bool Activated {
				inline bool get() { return _activated; }
			}
		};

		public delegate void PageEventHandler(System::Object^ sender, PageEventArgs^ e);

		[System::Flags]
		public enum class PageFlags : DWORD {
			None = NULL,
			SetAsActive = FLAG_SET_AS_ACTIVE
		};

		public value struct ServerRequestStatus {
			property DWORD HeaderError;
			property DWORD HeaderInfo;
			property DWORD RequestError;
			property DWORD RequestInfo;
		};

		public ref class DeviceClient {
			System::Runtime::InteropServices::GCHandle _this;
			SoftButtonsEventHandler^ _SoftButtons;
			PageEventHandler^ _Page;
			int _registeredSoftButtonCallback = 0, _registeredPageCallback = 0;

		protected:
			DirectOutputClient^ _client;

		internal:
			void* _device;
			DeviceClient(DirectOutputClient^ client, void* device);
			~DeviceClient();
			System::Collections::Generic::SortedSet<DWORD>^ pages = gcnew System::Collections::Generic::SortedSet<DWORD>;

		public:
			System::Guid GetDeviceType();

			System::Guid GetDeviceInstance();

			System::String^ GetSerialNumber();

			void SetProfile(System::String^ filename);

			event SoftButtonsEventHandler^ SoftButtons {
				void add(SoftButtonsEventHandler^ handler);
				void remove(SoftButtonsEventHandler^ handler);
		internal:
			void raise(System::Object^ sender, SoftButtonsEventArgs^ e);
			}

			event PageEventHandler^ Page {
				void add(PageEventHandler^ handler);
				void remove(PageEventHandler^ handler);
		internal:
			void raise(System::Object^ sender, PageEventArgs^ e);
			}

			void AddPage(DWORD page, [System::Runtime::InteropServices::Optional][System::Runtime::InteropServices::DefaultParameterValue(PageFlags::None)]PageFlags flags);

			void RemovePage(DWORD page);

			void SetLed(DWORD page, DWORD index, bool value);

			void SetString(DWORD page, DWORD index, System::String^ value);

			void SetImage(DWORD page, DWORD index, array<System::Byte>^ value);

			void SetImageFromFile(DWORD page, DWORD index, System::String^ filename);

			void StartServer(System::String^ filename, [System::Runtime::InteropServices::Out]DWORD% serverId);
			void StartServer(System::String^ filename, [System::Runtime::InteropServices::Out]DWORD% serverId, [System::Runtime::InteropServices::Out]ServerRequestStatus% status);

			void CloseServer(DWORD serverId);
			void CloseServer(DWORD serverId, [System::Runtime::InteropServices::Out]ServerRequestStatus% status);

			void SendServerMessage(DWORD serverId, DWORD request, DWORD page, array<System::Byte>^ in, array<System::Byte>^ out);
			void SendServerMessage(DWORD serverId, DWORD request, DWORD page, array<System::Byte>^ in, array<System::Byte>^ out, [System::Runtime::InteropServices::Out]ServerRequestStatus% status);

			void SendServerFile(DWORD serverId, DWORD request, DWORD page, array<System::Byte>^ inHeader, System::String^ filename, array<System::Byte>^ out);
			void SendServerFile(DWORD serverId, DWORD request, DWORD page, array<System::Byte>^ inHeader, System::String^ filename, array<System::Byte>^ out, [System::Runtime::InteropServices::Out]ServerRequestStatus% status);

			void SaveFile(DWORD page, DWORD file, System::String^ filename);
			void SaveFile(DWORD page, DWORD file, System::String^ filename, [System::Runtime::InteropServices::Out]ServerRequestStatus% status);

			void DisplayFile(DWORD page, DWORD index, DWORD file);
			void DisplayFile(DWORD page, DWORD index, DWORD file, [System::Runtime::InteropServices::Out]ServerRequestStatus% status);

			void DeleteFile(DWORD page, DWORD file);
			void DeleteFile(DWORD page, DWORD file, [System::Runtime::InteropServices::Out]ServerRequestStatus% status);
		};

		public value struct DeviceTypes {
			static const System::Guid
				Fip = FromGUID(DeviceType_Fip),
				X52Pro = FromGUID(DeviceType_X52Pro),
				X56RhinoStick = FromGUID(DeviceType_X56_Stick),
				X56RhinoThrottle = FromGUID(DeviceType_X56_Throttle);

			inline static System::String^ GetName(System::Guid guid) { return guid == Fip ? "Saitek Pro Flight Instrument Panel" : guid == X52Pro ? "Saitek X52Pro Flight Controller" : guid == X56RhinoStick ? "Saitek X56 Rhino Stick" : guid == X56RhinoThrottle ? "Saitek X56 Rhino Throttle" : "Unknown"; }
		};

		public ref class FipClient : DeviceClient {
		internal:
			inline FipClient(DirectOutputClient^ client, void* device) :
				DeviceClient(client, device) { }

		public:
			void SetImage(DWORD page, System::Drawing::Image^ image);
		};
	}
}