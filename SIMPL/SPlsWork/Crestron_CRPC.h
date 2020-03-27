namespace Crestron.CRPC;
        // class declarations
         class CrpcConstants;
         class CrpcServiceConstants;
         class JsonRpc2Errors;
         class JSONRPC;
         class CrpcRegisterEvent;
         class CrpcDeregisterEvent;
         class CrpcEvent;
         class Register;
         class Method;
         class Property;
         class GetObjects;
         class GetMembers;
         class JSONRPCResponseMembersMethodArray;
         class JSONRPCResponseMembersPropertyArray;
         class JSONRPCResponseMembersEventArray;
         class CrpcClientService;
         class ClientTransportLayer;
         class XLockObject;
         class CrpcEventInfo;
         class CrpcDirectory;
         class CrpcDescriptor;
         class ServerTransportLayer;
         class CrpcObject;
         class JSONRPCResponseObjectArray;
         class CrpcServiceObject;
         class ConnectionInfo;
         class XLock;
         class CrpcService;
         class CrpcUtility;
         class JSONRPCMethod;
         class JSONRPCEventParams;
         class CrpcJsonException;
         class CrpcEvents;
    static class CrpcConstants 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

    static class CrpcServiceConstants 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static SIGNED_LONG_INTEGER MAX_PACKET_SIZE;
        static STRING CRPC_VERSION[];
        static STRING CRPC_RESERVED_OBJECT_NAME[];
        static STRING CRPC_FORMAT_JSON[];
        static STRING CRPC_FORMAT_BSON[];
        static STRING CRPC_EVENT_OBJECTDIRCHANGED[];
        static STRING CRPC_EVENT_OBJECTS_ADDED[];
        static STRING CRPC_EVENT_OBJECTS_REMOVED[];
        static STRING CRPC_SERVICE_OBJECT_NAME[];

        // class properties
    };

    static class JsonRpc2Errors 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static SIGNED_LONG_INTEGER PARSE_ERROR;
        static SIGNED_LONG_INTEGER INVALID_REQUEST;
        static SIGNED_LONG_INTEGER METHOD_NOT_FOUND;
        static SIGNED_LONG_INTEGER INVALID_PARAMS;
        static SIGNED_LONG_INTEGER INTERNAL_ERROR;
        static SIGNED_LONG_INTEGER IMPL_SERVER_ERROR_START;
        static SIGNED_LONG_INTEGER IMPL_SERVER_PROPERTY_NOT_AVAILABLE;
        static SIGNED_LONG_INTEGER IMPL_SERVER_OBJECT_NOT_FOUND;
        static SIGNED_LONG_INTEGER IMPL_SERVER_ERROR_END;

        // class properties
    };

    static class JSONRPC 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_VERSION[];
        static STRING JSONRPC_ID[];
        static STRING JSONRPC_RESULT[];
        static STRING JSONRPC_ERROR[];
        static STRING JSONRPC_PARAMS[];
        static STRING JSONRPC_PARAMETERS[];

        // class properties
    };

    static class CrpcRegisterEvent 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_REGISTEREVENT[];
        static STRING JSONRPC_REGISTEREVENT_PARAM_EVENT[];
        static STRING JSONRPC_REGISTEREVENT_PARAM_HANDLE[];
        static STRING JSONRPC_EVENT_PARAMS[];

        // class properties
    };

    static class CrpcDeregisterEvent 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_DEREGISTEREVENT[];
        static STRING JSONRPC_DEREGISTEREVENT_PARAM_EVENT[];
        static STRING JSONRPC_DEREGISTEREVENT_PARAM_HANDLE[];
        static STRING JSONRPC_EVENT_PARAMS[];

        // class properties
    };

    static class CrpcEvent 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_EVENT[];
        static STRING JSONRPC_EVENT_PARAM_EVENT[];
        static STRING JSONRPC_EVENT_PARAM_HANDLE[];
        static STRING JSONRPC_EVENT_PARAMS[];
        static STRING JSONRPC_EVENT_PARAMETERS[];

        // class properties
    };

    static class Register 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_REGISTER[];
        static STRING JSONRPC_REGISTER_VER[];
        static STRING JSONRPC_REGISTER_NAME[];
        static STRING JSONRPC_REGISTER_UUID[];
        static STRING JSONRPC_REGISTER_CONNECTIONS[];
        static STRING JSONRPC_REGISTER_MAXPACKETSIZE[];
        static STRING JSONRPC_REGISTER_ENCODING[];
        static STRING JSONRPC_REGISTER_FORMAT[];

        // class properties
    };

    static class Method 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_METHOD[];

        // class properties
    };

    static class Property 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_PROPERTY_GET[];
        static STRING JSONRPC_PROPERTY_SET[];
        static STRING JSONRPC_PROPERTY_NAME[];
        static STRING JSONRPC_PROPERTY_VALUE[];

        // class properties
    };

    static class GetObjects 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_GETOBJECTS[];
        static STRING JSONRPC_GETOBJECTS_CATEGORY[];
        static STRING JSONRPC_GETOBJECTS_INSTANCENAME[];
        static STRING JSONRPC_GETOBJECTS_INTERFACES[];
        static STRING JSONRPC_GETOBJECTS_UUID[];
        static STRING JSONRPC_GETOBJECTS_NAME[];

        // class properties
    };

    static class GetMembers 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static STRING JSONRPC_GETMEMBERS[];
        static STRING JSONRPC_GETMEMBERS_OBJECT[];
        static STRING JSONRPC_GETMEMBERS_ACCESS[];

        // class properties
    };

     class CrpcClientService 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Unregister ( STRING name );
        FUNCTION Dispose ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Version[];
        STRING Name[];
        SIGNED_LONG_INTEGER MaxPacketSize;
        SIGNED_LONG_INTEGER ObjectCount;
    };

     class XLockObject 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Lock ();
        FUNCTION Unlock ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class CrpcEventInfo 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING EventName[];
        STRING Handle[];
    };

     class CrpcDirectory 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Unregister ( STRING name );
        FUNCTION Dispose ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Version[];
        STRING Name[];
        SIGNED_LONG_INTEGER MaxPacketSize;
        SIGNED_LONG_INTEGER ObjectCount;
    };

     class ServerTransportLayer 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION AddFilter ( STRING name );
        FUNCTION RemoveFilter ( STRING name );
        FUNCTION Initialize ( STRING Type );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Type[];
        STRING Name[];
    };

     class CrpcObject 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Name[];
        STRING Version[];
    };

     class CrpcServiceObject 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class ConnectionInfo 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Type[];
    };

     class CrpcService 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Initialize ( STRING name );
        FUNCTION Unregister ( STRING name );
        FUNCTION Dispose ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        CrpcService Instance;
        STRING Version[];
        STRING Name[];
        SIGNED_LONG_INTEGER MaxPacketSize;
        SIGNED_LONG_INTEGER ObjectCount;
    };

    static class CrpcUtility 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class CrpcEvents 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

