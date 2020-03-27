namespace Crestron.CRPC.CIPDirectTransport;
        // class declarations
         class CIPServerTransport;
         class BEBinaryWriter;
         class BEBinaryReader;
         class CIPTransportBase;
         class CIPClientTransport;
         class CIPCommon;
         class CIPHeartbeatRequest;
         class CIPHeartbeatResponse;
         class CIPConnectRequest;
         class CIPConnectResponse;
         class CIPData;
     class CIPServerTransport 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION AddFilter ( STRING name );
        FUNCTION RemoveFilter ( STRING name );
        FUNCTION Stop ( SIGNED_LONG_INTEGER msToWait );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Type[];
    };

     class BEBinaryWriter 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Write ( SIGNED_INTEGER value );
        FUNCTION Close ();
        FUNCTION Flush ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class CIPTransportBase 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION AddFilter ( STRING name );
        FUNCTION RemoveFilter ( STRING name );
        FUNCTION Start ();
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Type[];
    };

     class CIPClientTransport 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        FUNCTION AddFilter ( STRING name );
        FUNCTION RemoveFilter ( STRING name );
        FUNCTION Start ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Type[];
    };

     class CIPCommon 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER PacketLength;

        // class properties
    };

     class CIPHeartbeatRequest 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        SIGNED_INTEGER Handler;
        INTEGER PacketLength;

        // class properties
    };

     class CIPHeartbeatResponse 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        SIGNED_INTEGER Handler;
        INTEGER PacketLength;

        // class properties
    };

     class CIPConnectRequest 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER DestinationCID;
        INTEGER PortNumber;
        INTEGER Timeout;
        INTEGER Type;
        STRING Name[];
        INTEGER PacketLength;

        // class properties
    };

     class CIPConnectResponse 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        SIGNED_INTEGER Handler;
        INTEGER PacketLength;

        // class properties
    };

     class CIPData 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        static SIGNED_LONG_INTEGER PAYLOAD_LENGTH;
        static INTEGER _maxPacket;
        INTEGER PacketLength;

        // class properties
    };

