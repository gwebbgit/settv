#include "TypeDefs.h"
#include "Globals.h"
#include "FnctList.h"
#include "Library.h"
#include "SimplSig.h"
#include "S2_hh_ui_symnet.h"

FUNCTION_MAIN( S2_hh_ui_symnet );
EVENT_HANDLER( S2_hh_ui_symnet );
DEFINE_ENTRYPOINT( S2_hh_ui_symnet );

static void S2_hh_ui_symnet__FCN_REPUSH_UI ( ) 
    { 
    /* Begin local function variable declarations */
    
    CREATE_STRING_STRUCT( S2_hh_ui_symnet, __SPLS_TMPVAR__LOCALSTR_0__, sizeof( "-" ) );
    DECLARE_LOCAL_STRING_STRUCT( S2_hh_ui_symnet, __SPLS_TMPVAR__LOCALSTR_0__ );
    
    CheckStack( INSTANCE_PTR( S2_hh_ui_symnet ) );
    
    ALLOCATE_LOCAL_STRING_STRUCT( S2_hh_ui_symnet, __SPLS_TMPVAR__LOCALSTR_0__ );
    SET_LOCAL_STRING_STRUCT( __SPLS_TMPVAR__LOCALSTR_0__, "-" );
    
    
    /* End local function variable declarations */
    
    
    UpdateSourceCodeLine( INSTANCE_PTR( S2_hh_ui_symnet ), 74 );
    if ( ((GetAnalogOutput( INSTANCE_PTR( S2_hh_ui_symnet ), __S2_hh_ui_symnet_BROWSE_CH_ANALOG_OUTPUT ) < 1) || (GetAnalogOutput( INSTANCE_PTR( S2_hh_ui_symnet ), __S2_hh_ui_symnet_BROWSE_CH_ANALOG_OUTPUT ) > 4))) 
        { 
        UpdateSourceCodeLine( INSTANCE_PTR( S2_hh_ui_symnet ), 76 );
        if( ObtainStringOutputSemaphore( INSTANCE_PTR( S2_hh_ui_symnet ) ) == 0 ) {
        FormatString ( INSTANCE_PTR( S2_hh_ui_symnet ) , GENERIC_STRING_OUTPUT( S2_hh_ui_symnet )  ,2 , "%s"  ,  LOCAL_STRING_STRUCT( __SPLS_TMPVAR__LOCALSTR_0__ )    )  ; 
        SetSerial( INSTANCE_PTR( S2_hh_ui_symnet ), __S2_hh_ui_symnet_UI_BROWSE_TEXT_STRING_OUTPUT, GENERIC_STRING_OUTPUT( S2_hh_ui_symnet ) );
        ReleaseStringOutputSemaphore( INSTANCE_PTR( S2_hh_ui_symnet ) ); }
        
        ; 
        } 
    
    else 
        { 
        UpdateSourceCodeLine( INSTANCE_PTR( S2_hh_ui_symnet ), 79 );
        if( ObtainStringOutputSemaphore( INSTANCE_PTR( S2_hh_ui_symnet ) ) == 0 ) {
        FormatString ( INSTANCE_PTR( S2_hh_ui_symnet ) , GENERIC_STRING_OUTPUT( S2_hh_ui_symnet )  ,2 , "%s"  , GetStringArrayElement ( INSTANCE_PTR( S2_hh_ui_symnet ),  GLOBAL_STRING_ARRAY( S2_hh_ui_symnet, __SOURCE_NAME  )    ,  GetAnalogOutput( INSTANCE_PTR( S2_hh_ui_symnet ), __S2_hh_ui_symnet_BROWSE_CH_ANALOG_OUTPUT )  )  )  ; 
        SetSerial( INSTANCE_PTR( S2_hh_ui_symnet ), __S2_hh_ui_symnet_UI_BROWSE_TEXT_STRING_OUTPUT, GENERIC_STRING_OUTPUT( S2_hh_ui_symnet ) );
        ReleaseStringOutputSemaphore( INSTANCE_PTR( S2_hh_ui_symnet ) ); }
        
        ; 
        } 
    
    
    S2_hh_ui_symnet_Exit__FCN_REPUSH_UI:
/* Begin Free local function variables */
    FREE_LOCAL_STRING_STRUCT( __SPLS_TMPVAR__LOCALSTR_0__ );
    /* End Free local function variables */
    
    }
    
DEFINE_INDEPENDENT_EVENTHANDLER( S2_hh_ui_symnet, 00000 /*reupdate_pr*/ )

    {
    /* Begin local function variable declarations */
    
    SAVE_GLOBAL_POINTERS ;
    CheckStack( INSTANCE_PTR( S2_hh_ui_symnet ) );
    
    
    /* End local function variable declarations */
    
    
    UpdateSourceCodeLine( INSTANCE_PTR( S2_hh_ui_symnet ), 88 );
    S2_hh_ui_symnet__FCN_REPUSH_UI ( ) ; 
    
    S2_hh_ui_symnet_Exit__Event_0:
    /* Begin Free local function variables */
/* End Free local function variables */
RESTORE_GLOBAL_POINTERS ;

}


/********************************************************************************
  Constructor
********************************************************************************/

/********************************************************************************
  Destructor
********************************************************************************/

/********************************************************************************
  static void ProcessDigitalEvent( struct Signal_s *Signal )
********************************************************************************/
static void ProcessDigitalEvent( struct Signal_s *Signal )
{
    switch ( Signal->SignalNumber )
    {
        case __S2_hh_ui_symnet_REUPDATE_PR_DIG_INPUT :
            if ( Signal->Value /*Push*/ )
            {
                SAFESPAWN_EVENTHANDLER( S2_hh_ui_symnet, 00000 /*reupdate_pr*/, 0 );
                
            }
            else /*Release*/
            {
                SetSymbolEventStart ( INSTANCE_PTR( S2_hh_ui_symnet ) ); 
                
            }
            break;
            
        default :
            SetSymbolEventStart ( INSTANCE_PTR( S2_hh_ui_symnet ) ); 
            break ;
        
    }
}

/********************************************************************************
  static void ProcessAnalogEvent( struct Signal_s *Signal )
********************************************************************************/
static void ProcessAnalogEvent( struct Signal_s *Signal )
{
    switch ( Signal->SignalNumber )
    {
        default :
            SetSymbolEventStart ( INSTANCE_PTR( S2_hh_ui_symnet ) ); 
            break ;
        
    }
}

/********************************************************************************
  static void ProcessStringEvent( struct Signal_s *Signal )
********************************************************************************/
static void ProcessStringEvent( struct Signal_s *Signal )
{
    if ( UPDATE_INPUT_STRING( S2_hh_ui_symnet ) == 1 ) return ;
    
    switch ( Signal->SignalNumber )
    {
        default :
            SetSymbolEventStart ( INSTANCE_PTR( S2_hh_ui_symnet ) ); 
            break ;
        
    }
}

/********************************************************************************
  static void ProcessSocketConnectEvent( struct Signal_s *Signal )
********************************************************************************/
static void ProcessSocketConnectEvent( struct Signal_s *Signal )
{
    switch ( Signal->SignalNumber )
    {
        default :
            SetSymbolEventStart ( INSTANCE_PTR( S2_hh_ui_symnet ) ); 
            break ;
        
    }
}

/********************************************************************************
  static void ProcessSocketDisconnectEvent( struct Signal_s *Signal )
********************************************************************************/
static void ProcessSocketDisconnectEvent( struct Signal_s *Signal )
{
    switch ( Signal->SignalNumber )
    {
        default :
            SetSymbolEventStart ( INSTANCE_PTR( S2_hh_ui_symnet ) ); 
            break ;
        
    }
}

/********************************************************************************
  static void ProcessSocketReceiveEvent( struct Signal_s *Signal )
********************************************************************************/
static void ProcessSocketReceiveEvent( struct Signal_s *Signal )
{
    if ( UPDATE_INPUT_STRING( S2_hh_ui_symnet ) == 1 ) return ;
    
    switch ( Signal->SignalNumber )
    {
        default :
            SetSymbolEventStart ( INSTANCE_PTR( S2_hh_ui_symnet ) ); 
            break ;
        
    }
}

/********************************************************************************
  static void ProcessSocketStatusEvent( struct Signal_s *Signal )
********************************************************************************/
static void ProcessSocketStatusEvent( struct Signal_s *Signal )
{
    switch ( Signal->SignalNumber )
    {
        default :
            SetSymbolEventStart ( INSTANCE_PTR( S2_hh_ui_symnet ) ); 
            break ;
        
    }
}

/********************************************************************************
  EVENT_HANDLER( S2_hh_ui_symnet )
********************************************************************************/
EVENT_HANDLER( S2_hh_ui_symnet )
    {
    SAVE_GLOBAL_POINTERS ;
    switch ( Signal->Type )
        {
        case e_SIGNAL_TYPE_DIGITAL :
            ProcessDigitalEvent( Signal );
            break ;
        case e_SIGNAL_TYPE_ANALOG :
            ProcessAnalogEvent( Signal );
            break ;
        case e_SIGNAL_TYPE_STRING :
            ProcessStringEvent( Signal );
            break ;
        case e_SIGNAL_TYPE_CONNECT :
            ProcessSocketConnectEvent( Signal );
            break ;
        case e_SIGNAL_TYPE_DISCONNECT :
            ProcessSocketDisconnectEvent( Signal );
            break ;
        case e_SIGNAL_TYPE_RECEIVE :
            ProcessSocketReceiveEvent( Signal );
            break ;
        case e_SIGNAL_TYPE_STATUS :
            ProcessSocketStatusEvent( Signal );
            break ;
        }
        
    RESTORE_GLOBAL_POINTERS ;
    
    }
    
/********************************************************************************
  FUNCTION_MAIN( S2_hh_ui_symnet )
********************************************************************************/
FUNCTION_MAIN( S2_hh_ui_symnet )
{
    SAVE_GLOBAL_POINTERS ;
    
    SET_INSTANCE_POINTER ( S2_hh_ui_symnet );
    
    INITIALIZE_GLOBAL_STRING_PARAMETER_ARRAY( S2_hh_ui_symnet, __SOURCE_NAME, e_INPUT_TYPE_STRING_PARAMETER, __S2_hh_ui_symnet_SOURCE_NAME_ARRAY_NUM_ELEMS, __S2_hh_ui_symnet_SOURCE_NAME_ARRAY_NUM_CHARS, __S2_hh_ui_symnet_SOURCE_NAME_STRING_PARAMETER );
    
    INITIALIZE_GLOBAL_STRING_STRUCT ( S2_hh_ui_symnet, sGenericOutStr, e_INPUT_TYPE_NONE, GENERIC_STRING_OUTPUT_LEN );
    
    
    
    S2_hh_ui_symnet_Exit__MAIN:
    RESTORE_GLOBAL_POINTERS ;
    return 0 ;
    }
