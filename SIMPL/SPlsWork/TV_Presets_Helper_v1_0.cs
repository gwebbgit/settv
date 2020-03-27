using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace CrestronModule_TV_PRESETS_HELPER_V1_0
{
    public class CrestronModuleClass_TV_PRESETS_HELPER_V1_0 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput SEQUENCE_BUSY;
        Crestron.Logos.SplusObjects.DigitalInput NEED_ENTER;
        Crestron.Logos.SplusObjects.AnalogInput MIN_CHANNEL_LEN;
        Crestron.Logos.SplusObjects.StringInput CHANNEL_IN__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput CHANNEL_OUT__DOLLAR__;
        UShortParameter DELAY_BETWEEN_SEQUENCES;
        CrestronString _SQUEDCHANNEL;
        private CrestronString PADCHANNEL (  SplusExecutionContext __context__, CrestronString SCHANNEL ) 
            { 
            CrestronString SADJUSTED;
            SADJUSTED  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            
            ushort I = 0;
            ushort IPADQTY = 0;
            
            
            __context__.SourceCodeLine = 113;
            IPADQTY = (ushort) ( (MIN_CHANNEL_LEN  .UshortValue - Functions.Length( SCHANNEL )) ) ; 
            __context__.SourceCodeLine = 115;
            SADJUSTED  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 116;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)IPADQTY; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 118;
                SADJUSTED  .UpdateValue ( SADJUSTED + "0"  ) ; 
                __context__.SourceCodeLine = 116;
                } 
            
            __context__.SourceCodeLine = 121;
            SADJUSTED  .UpdateValue ( SADJUSTED + SCHANNEL  ) ; 
            __context__.SourceCodeLine = 123;
            return ( SADJUSTED ) ; 
            
            }
            
        private void SENDCHANNEL (  SplusExecutionContext __context__, CrestronString SCHANNEL ) 
            { 
            CrestronString SADJUSTED;
            SADJUSTED  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            
            
            __context__.SourceCodeLine = 130;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( SCHANNEL ) < MIN_CHANNEL_LEN  .UshortValue ))  ) ) 
                { 
                __context__.SourceCodeLine = 132;
                SADJUSTED  .UpdateValue ( PADCHANNEL (  __context__ , SCHANNEL)  ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 136;
                SADJUSTED  .UpdateValue ( SCHANNEL  ) ; 
                } 
            
            __context__.SourceCodeLine = 139;
            if ( Functions.TestForTrue  ( ( NEED_ENTER  .Value)  ) ) 
                {
                __context__.SourceCodeLine = 140;
                CHANNEL_OUT__DOLLAR__  .UpdateValue ( SADJUSTED + "!"  ) ; 
                }
            
            else 
                {
                __context__.SourceCodeLine = 142;
                CHANNEL_OUT__DOLLAR__  .UpdateValue ( SADJUSTED  ) ; 
                }
            
            
            }
            
        object SEQUENCE_BUSY_OnRelease_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 157;
                if ( Functions.TestForTrue  ( ( Functions.Length( _SQUEDCHANNEL ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 159;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( DELAY_BETWEEN_SEQUENCES  .Value > 0 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 161;
                        Functions.Delay (  (int) ( DELAY_BETWEEN_SEQUENCES  .Value ) ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 164;
                    SENDCHANNEL (  __context__ , _SQUEDCHANNEL) ; 
                    __context__.SourceCodeLine = 165;
                    _SQUEDCHANNEL  .UpdateValue ( ""  ) ; 
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object CHANNEL_IN__DOLLAR___OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 171;
            if ( Functions.TestForTrue  ( ( SEQUENCE_BUSY  .Value)  ) ) 
                { 
                __context__.SourceCodeLine = 173;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( CHANNEL_IN__DOLLAR__ ) > 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 174;
                    _SQUEDCHANNEL  .UpdateValue ( CHANNEL_IN__DOLLAR__  ) ; 
                    }
                
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 178;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( CHANNEL_IN__DOLLAR__ ) > 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 179;
                    SENDCHANNEL (  __context__ , CHANNEL_IN__DOLLAR__) ; 
                    }
                
                } 
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 196;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 198;
        _SQUEDCHANNEL  .UpdateValue ( ""  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    _SQUEDCHANNEL  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
    
    SEQUENCE_BUSY = new Crestron.Logos.SplusObjects.DigitalInput( SEQUENCE_BUSY__DigitalInput__, this );
    m_DigitalInputList.Add( SEQUENCE_BUSY__DigitalInput__, SEQUENCE_BUSY );
    
    NEED_ENTER = new Crestron.Logos.SplusObjects.DigitalInput( NEED_ENTER__DigitalInput__, this );
    m_DigitalInputList.Add( NEED_ENTER__DigitalInput__, NEED_ENTER );
    
    MIN_CHANNEL_LEN = new Crestron.Logos.SplusObjects.AnalogInput( MIN_CHANNEL_LEN__AnalogSerialInput__, this );
    m_AnalogInputList.Add( MIN_CHANNEL_LEN__AnalogSerialInput__, MIN_CHANNEL_LEN );
    
    CHANNEL_IN__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( CHANNEL_IN__DOLLAR____AnalogSerialInput__, 10, this );
    m_StringInputList.Add( CHANNEL_IN__DOLLAR____AnalogSerialInput__, CHANNEL_IN__DOLLAR__ );
    
    CHANNEL_OUT__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( CHANNEL_OUT__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( CHANNEL_OUT__DOLLAR____AnalogSerialOutput__, CHANNEL_OUT__DOLLAR__ );
    
    DELAY_BETWEEN_SEQUENCES = new UShortParameter( DELAY_BETWEEN_SEQUENCES__Parameter__, this );
    m_ParameterList.Add( DELAY_BETWEEN_SEQUENCES__Parameter__, DELAY_BETWEEN_SEQUENCES );
    
    
    SEQUENCE_BUSY.OnDigitalRelease.Add( new InputChangeHandlerWrapper( SEQUENCE_BUSY_OnRelease_0, false ) );
    CHANNEL_IN__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( CHANNEL_IN__DOLLAR___OnChange_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public CrestronModuleClass_TV_PRESETS_HELPER_V1_0 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint SEQUENCE_BUSY__DigitalInput__ = 0;
const uint NEED_ENTER__DigitalInput__ = 1;
const uint MIN_CHANNEL_LEN__AnalogSerialInput__ = 0;
const uint CHANNEL_IN__DOLLAR____AnalogSerialInput__ = 1;
const uint CHANNEL_OUT__DOLLAR____AnalogSerialOutput__ = 0;
const uint DELAY_BETWEEN_SEQUENCES__Parameter__ = 10;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
