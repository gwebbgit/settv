namespace Crestron.TV_Presets.Model;
        // class declarations
         class ProfileExternalProviders;
         class AppException;
         class ChangeEvent;
         class SortOptions;
         class ExternalProviderPresets;
         class ExternalProvider;
         class ProviderManager;
         class Provider;
         class Profile;
         class RcCode;
         class Token;
         class TvPreset;
    static class ChangeEvent // enum
    {
        static SIGNED_LONG_INTEGER Add;
        static SIGNED_LONG_INTEGER Edit;
        static SIGNED_LONG_INTEGER Move;
        static SIGNED_LONG_INTEGER Delete;
    };

    static class SortOptions // enum
    {
        static SIGNED_LONG_INTEGER Unsorted;
        static SIGNED_LONG_INTEGER ChannelNumber;
        static SIGNED_LONG_INTEGER ChannelName;
    };

     class ExternalProviderPresets 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        Profile Profile;
        ExternalProvider ExternalProvider;
        STRING ExternalProviderId[];
        STRING LastUpdated[];
        STRING ProfileId[];
        TvPreset Presets[];
        SIGNED_LONG_INTEGER Count;
    };

     class ExternalProvider 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Id[];
        STRING Name[];
        STRING ETag[];
        STRING ChannelsLastUpdated[];
        STRING ChannelFile[];
        STRING ImageFile[];
        STRING FileName[];
    };

     class ProviderManager 
    {
        // class delegates

        // class events
        static EventHandler ChannelLineupUpdated ( Provider obj );

        // class functions
        FUNCTION AddPresets ( STRING profileId , STRING externalId , STRING presets );
        FUNCTION EditPresets ( STRING profileId , STRING externalId , STRING presets );
        FUNCTION MovePreset ( STRING presetId , SIGNED_LONG_INTEGER dstIndex , STRING profileId , STRING externalId );
        FUNCTION DeletePresets ( STRING profileId , STRING externalId , STRING presetIds );
        FUNCTION ClearPresets ( STRING profileId , STRING externalId );
        FUNCTION SetProvider ( SIGNED_LONG_INTEGER sourceId , STRING providerId );
        FUNCTION DeleteProvider ( SIGNED_LONG_INTEGER sourceId );
        SIGNED_LONG_INTEGER_FUNCTION TryGetToken ( STRING tokenName );
        SIGNED_LONG_INTEGER_FUNCTION RefreshToken ( STRING tokenName , SIGNED_LONG_INTEGER token );
        SIGNED_LONG_INTEGER_FUNCTION ReleaseToken ( STRING tokenName , SIGNED_LONG_INTEGER token );
        FUNCTION RefreshProvider ( STRING exProviderId );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        ProviderManager Instance;
        Provider Providers[];
    };

     class Provider 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER Id;
        STRING ExternalId[];
        ExternalProvider ExternalProvider;
    };

     class Profile 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Id[];
        STRING Name[];
        STRING Pin[];
        STRING Language[];
        STRING TranslationBaseUri[];
        ProfileExternalProviders ProfileExternalProviders;
    };

    static class RcCode // enum
    {
        static SIGNED_LONG_INTEGER SUCCESS;
        static SIGNED_LONG_INTEGER FAILED;
        static SIGNED_LONG_INTEGER EXCEPTION;
        static SIGNED_LONG_INTEGER PROFILE_DOES_NOT_EXIST;
        static SIGNED_LONG_INTEGER PROFILE_NAME_ALREADY_EXIST;
        static SIGNED_LONG_INTEGER PROVIDER_DOES_NOT_EXIST;
        static SIGNED_LONG_INTEGER PRESET_DOES_NOT_EXIST;
        static SIGNED_LONG_INTEGER INVALID_PIN;
        static SIGNED_LONG_INTEGER PRESET_INVALID;
        static SIGNED_LONG_INTEGER PROFILE_DELETE_LAST;
    };

     class TvPreset 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Edit ( TvPreset tvPreset );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Id[];
        STRING Name[];
        STRING Image[];
        STRING ChannelId[];
        STRING ChannelNumber[];
    };

namespace Crestron.TV_Presets.User_Interfaces;
        // class declarations
         class SimplPlusAttribute;
         class ProfileManagerSimpl;
         class PresetBaseEventArgs;
         class PresetListUpdatedEventArgs;
         class PresetIdListUpdatedEventArgs;
         class ProviderListUpdatedEventArgs;
         class ChannelLineupUpdatedEventArgs;
         class StateChangedEventArgs;
         class StatusMsgEventArgs;
         class PresetManagerCrpc;
         class CrpcInitialization;
         class ProfileEventArgs;
         class ProfileUpdatedEventArgs;
         class ProfileListEventArgs;
         class ProfileManagerCrpc;
     class SimplPlusAttribute 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class ProfileManagerSimpl 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION Initialize ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        INTEGER DataFileLocation;
        STRING CustomDataPath[];
    };

     class PresetManagerCrpc 
    {
        // class delegates

        // class events
        EventHandler stateChanged ( PresetManagerCrpc sender, StateChangedEventArgs args );
        EventHandler presetListUpdated ( PresetManagerCrpc sender, PresetListUpdatedEventArgs args );
        EventHandler presetIdListUpdated ( PresetManagerCrpc sender, PresetIdListUpdatedEventArgs args );
        EventHandler providerListUpdated ( PresetManagerCrpc sender, ProviderListUpdatedEventArgs args );
        EventHandler statusMsg ( PresetManagerCrpc sender, StatusMsgEventArgs args );

        // class functions
        FUNCTION FireStatusMsg ( STRING message );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER Version;
        STRING Name[];
        SIGNED_LONG_INTEGER Status;
    };

     class CrpcInitialization 
    {
        // class delegates
        delegate FUNCTION DelegateFnString ( SIMPLSHARPSTRING myString );

        // class events

        // class functions
        FUNCTION InitializeDataPath ( STRING path );
        FUNCTION InitializeCrpc ( INTEGER port , INTEGER adapterID );
        FUNCTION JoinTransportSendback ( STRING stream );
        FUNCTION MessageIn ( STRING pkt );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        DelegateProperty DelegateFnString MessageOut;
    };

     class ProfileManagerCrpc 
    {
        // class delegates

        // class events
        EventHandler profileUpdated ( ProfileManagerCrpc sender, ProfileUpdatedEventArgs args );
        EventHandler profileListChanged ( ProfileManagerCrpc sender, ProfileListEventArgs args );

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Name[];
        SIGNED_LONG_INTEGER Count;
    };

namespace Crestron.TV_Presets.CrpcModel;
        // class declarations
         class CrpcProvider;
         class CrpcProfileList;
         class CrpcToken;
         class CrpcProfile;
         class CrpcResult;
         class CrpcTimeOffset;
         class CrpcPreset;
         class CrpcExternalProvider;
     class CrpcResult 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static Crestron.TV_Presets.CrpcModel.CrpcResult OK;

        // class properties
        SIGNED_LONG_INTEGER ResultCode;
        STRING Message[];
    };

     class CrpcTimeOffset 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING PayloadType[];
        SIGNED_LONG_INTEGER Offset;
    };

namespace Crestron.TV_Presets.CrpcMessages;
        // class declarations
         class CrpcPresetListArray;
         class CrpcPresetList;
         class CrpcExternalProviderList;
         class CrpcProviderList;
         class CrpcPayloadString;
         class CrpcPresetLastUpdated;
     class CrpcPresetListArray 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION set_Item ( SIGNED_LONG_INTEGER index , CrpcPreset value );
        FUNCTION Add ( CrpcPreset item );
        SIGNED_LONG_INTEGER_FUNCTION BinarySearch ( CrpcPreset item );
        FUNCTION Clear ();
        FUNCTION CopyTo ( CrpcPreset array[] );
        SIGNED_LONG_INTEGER_FUNCTION IndexOf ( CrpcPreset item );
        FUNCTION Insert ( SIGNED_LONG_INTEGER index , CrpcPreset item );
        SIGNED_LONG_INTEGER_FUNCTION LastIndexOf ( CrpcPreset item );
        FUNCTION RemoveAt ( SIGNED_LONG_INTEGER index );
        FUNCTION RemoveRange ( SIGNED_LONG_INTEGER index , SIGNED_LONG_INTEGER count );
        FUNCTION Reverse ();
        FUNCTION Sort ();
        FUNCTION TrimExcess ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER Capacity;
        SIGNED_LONG_INTEGER Count;
    };

     class CrpcPresetList 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Add ( CrpcPreset preset );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING PayloadType[];
        CrpcPresetListArray list;
        SIGNED_LONG_INTEGER Start;
        SIGNED_LONG_INTEGER PresetCount;
        STRING LastUpdated[];
        STRING profileId[];
        STRING externalId[];
    };

namespace Crestron.TV_Presets;
        // class declarations
         class ProfileManager;
     class ProfileManager 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        ProfileManager Instance;
        SIGNED_LONG_INTEGER Count;
    };

namespace Crestron.TV_Presets.Managers;
        // class declarations
         class Logger;
         class FileManager;
     class Logger 
    {
        // class delegates

        // class events

        // class functions
        static FUNCTION Error ( STRING message );
        static FUNCTION Warn ( STRING message );
        static FUNCTION Info ( STRING message );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

    static class FileManager 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING DataPath[];
    };

namespace Crestron.TV_Presets.File_Manager;
        // class declarations

