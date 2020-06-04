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

namespace UserModule_BABY_TIMER
{
    public class UserModuleClass_BABY_TIMER : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        Crestron.Logos.SplusObjects.DigitalInput SLEEP_PR;
        Crestron.Logos.SplusObjects.DigitalInput WAKE_PR;
        Crestron.Logos.SplusObjects.DigitalInput OFF_PR;
        Crestron.Logos.SplusObjects.StringOutput TOD__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput SINCE_TITLE__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput SINCE_TIME__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DURATION_TITLE__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DURATION_H__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DURATION_UNIT__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DATE__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogOutput SELECT_ITEM__POUND__;
        Crestron.Logos.SplusObjects.AnalogOutput DESELECT_ITEM__POUND__;
        ushort I_START_H = 0;
        ushort I_START_M = 0;
        ushort I_START_S = 0;
        private void SET_START_NOW (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 66;
            I_START_H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 67;
            I_START_M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 68;
            I_START_S = (ushort) ( Functions.GetSecondsNum() ) ; 
            __context__.SourceCodeLine = 69;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)I_START_H, (ushort)I_START_M) ; 
            __context__.SourceCodeLine = 70;
            DURATION_TITLE__DOLLAR__  .UpdateValue ( "DURATION"  ) ; 
            __context__.SourceCodeLine = 71;
            DURATION_H__DOLLAR__  .UpdateValue ( "0"  ) ; 
            __context__.SourceCodeLine = 72;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            
            }
            
        private void UPDATE_TIMES (  SplusExecutionContext __context__ ) 
            { 
            ushort H = 0;
            ushort M = 0;
            ushort S = 0;
            
            
            __context__.SourceCodeLine = 77;
            H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 78;
            M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 79;
            S = (ushort) ( Functions.GetSecondsNum() ) ; 
            __context__.SourceCodeLine = 82;
            MakeString ( TOD__DOLLAR__ , "{0:d}:{1:d2}", (ushort)H, (ushort)M) ; 
            __context__.SourceCodeLine = 83;
            MakeString ( DATE__DOLLAR__ , "{0} {1} {2:d}, {3:d}", Functions.Left ( Functions.Day ( ) ,  (int) ( 3 ) ) , Functions.Left ( Functions.Month ( ) ,  (int) ( 3 ) ) , (ushort)Functions.GetDateNum(), (ushort)Functions.GetYearNum()) ; 
            __context__.SourceCodeLine = 86;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 3) ) || Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 0) )) ))  ) ) 
                {
                __context__.SourceCodeLine = 86;
                return ; 
                }
            
            __context__.SourceCodeLine = 89;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( S < I_START_S ))  ) ) 
                { 
                __context__.SourceCodeLine = 90;
                S = (ushort) ( (S + 60) ) ; 
                __context__.SourceCodeLine = 91;
                M = (ushort) ( (M - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 93;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < I_START_M ))  ) ) 
                { 
                __context__.SourceCodeLine = 94;
                M = (ushort) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 95;
                H = (ushort) ( (H - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 97;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( H < I_START_H ))  ) ) 
                { 
                __context__.SourceCodeLine = 98;
                H = (ushort) ( (H + 24) ) ; 
                } 
            
            __context__.SourceCodeLine = 100;
            DURATION_H__DOLLAR__  .UpdateValue ( Functions.ItoA (  (int) ( (H - I_START_H) ) )  ) ; 
            __context__.SourceCodeLine = 101;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            
            }
            
        private void SELECT_ITEM (  SplusExecutionContext __context__, ushort SEL ) 
            { 
            
            __context__.SourceCodeLine = 105;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 1))  ) ) 
                {
                __context__.SourceCodeLine = 105;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 106;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 2))  ) ) 
                {
                __context__.SourceCodeLine = 106;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 2 ) ; 
                }
            
            __context__.SourceCodeLine = 107;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 3))  ) ) 
                {
                __context__.SourceCodeLine = 107;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 3 ) ; 
                }
            
            __context__.SourceCodeLine = 108;
            SELECT_ITEM__POUND__  .Value = (ushort) ( SEL ) ; 
            
            }
            
        object SLEEP_PR_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 112;
                SELECT_ITEM (  __context__ , (ushort)( 1 )) ; 
                __context__.SourceCodeLine = 113;
                SINCE_TITLE__DOLLAR__  .UpdateValue ( "SLEEPING SINCE"  ) ; 
                __context__.SourceCodeLine = 114;
                SET_START_NOW (  __context__  ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object WAKE_PR_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 117;
            SELECT_ITEM (  __context__ , (ushort)( 2 )) ; 
            __context__.SourceCodeLine = 118;
            SINCE_TITLE__DOLLAR__  .UpdateValue ( "AWAKE SINCE"  ) ; 
            __context__.SourceCodeLine = 119;
            SET_START_NOW (  __context__  ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object OFF_PR_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 122;
        SELECT_ITEM (  __context__ , (ushort)( 3 )) ; 
        __context__.SourceCodeLine = 123;
        SINCE_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 124;
        SINCE_TIME__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 125;
        DURATION_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 126;
        DURATION_H__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 127;
        DURATION_UNIT__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 128;
        Functions.Delay (  (int) ( 100 ) ) ; 
        __context__.SourceCodeLine = 129;
        DESELECT_ITEM__POUND__  .Value = (ushort) ( 3 ) ; 
        
        
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
        
        __context__.SourceCodeLine = 132;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 133;
        while ( Functions.TestForTrue  ( ( 1)  ) ) 
            { 
            __context__.SourceCodeLine = 134;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 135;
            Functions.Delay (  (int) ( 200 ) ) ; 
            __context__.SourceCodeLine = 133;
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    
    SLEEP_PR = new Crestron.Logos.SplusObjects.DigitalInput( SLEEP_PR__DigitalInput__, this );
    m_DigitalInputList.Add( SLEEP_PR__DigitalInput__, SLEEP_PR );
    
    WAKE_PR = new Crestron.Logos.SplusObjects.DigitalInput( WAKE_PR__DigitalInput__, this );
    m_DigitalInputList.Add( WAKE_PR__DigitalInput__, WAKE_PR );
    
    OFF_PR = new Crestron.Logos.SplusObjects.DigitalInput( OFF_PR__DigitalInput__, this );
    m_DigitalInputList.Add( OFF_PR__DigitalInput__, OFF_PR );
    
    SELECT_ITEM__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( SELECT_ITEM__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( SELECT_ITEM__POUND____AnalogSerialOutput__, SELECT_ITEM__POUND__ );
    
    DESELECT_ITEM__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( DESELECT_ITEM__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( DESELECT_ITEM__POUND____AnalogSerialOutput__, DESELECT_ITEM__POUND__ );
    
    TOD__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( TOD__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( TOD__DOLLAR____AnalogSerialOutput__, TOD__DOLLAR__ );
    
    SINCE_TITLE__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( SINCE_TITLE__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( SINCE_TITLE__DOLLAR____AnalogSerialOutput__, SINCE_TITLE__DOLLAR__ );
    
    SINCE_TIME__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( SINCE_TIME__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( SINCE_TIME__DOLLAR____AnalogSerialOutput__, SINCE_TIME__DOLLAR__ );
    
    DURATION_TITLE__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( DURATION_TITLE__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( DURATION_TITLE__DOLLAR____AnalogSerialOutput__, DURATION_TITLE__DOLLAR__ );
    
    DURATION_H__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( DURATION_H__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( DURATION_H__DOLLAR____AnalogSerialOutput__, DURATION_H__DOLLAR__ );
    
    DURATION_UNIT__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( DURATION_UNIT__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( DURATION_UNIT__DOLLAR____AnalogSerialOutput__, DURATION_UNIT__DOLLAR__ );
    
    DATE__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( DATE__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( DATE__DOLLAR____AnalogSerialOutput__, DATE__DOLLAR__ );
    
    
    SLEEP_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( SLEEP_PR_OnPush_0, false ) );
    WAKE_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( WAKE_PR_OnPush_1, false ) );
    OFF_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( OFF_PR_OnPush_2, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_BABY_TIMER ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint SLEEP_PR__DigitalInput__ = 0;
const uint WAKE_PR__DigitalInput__ = 1;
const uint OFF_PR__DigitalInput__ = 2;
const uint TOD__DOLLAR____AnalogSerialOutput__ = 0;
const uint SINCE_TITLE__DOLLAR____AnalogSerialOutput__ = 1;
const uint SINCE_TIME__DOLLAR____AnalogSerialOutput__ = 2;
const uint DURATION_TITLE__DOLLAR____AnalogSerialOutput__ = 3;
const uint DURATION_H__DOLLAR____AnalogSerialOutput__ = 4;
const uint DURATION_UNIT__DOLLAR____AnalogSerialOutput__ = 5;
const uint DATE__DOLLAR____AnalogSerialOutput__ = 6;
const uint SELECT_ITEM__POUND____AnalogSerialOutput__ = 7;
const uint DESELECT_ITEM__POUND____AnalogSerialOutput__ = 8;

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
