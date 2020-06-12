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
        Crestron.Logos.SplusObjects.DigitalInput TIME_INC_PR;
        Crestron.Logos.SplusObjects.DigitalInput TIME_DEC_PR;
        Crestron.Logos.SplusObjects.AnalogInput ARG1;
        Crestron.Logos.SplusObjects.AnalogInput ARG2;
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
            
            __context__.SourceCodeLine = 80;
            I_START_H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 81;
            I_START_M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 82;
            I_START_S = (ushort) ( Functions.GetSecondsNum() ) ; 
            __context__.SourceCodeLine = 83;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)I_START_H, (ushort)I_START_M) ; 
            __context__.SourceCodeLine = 84;
            DURATION_TITLE__DOLLAR__  .UpdateValue ( "DURATION"  ) ; 
            __context__.SourceCodeLine = 85;
            DURATION_H__DOLLAR__  .UpdateValue ( "0"  ) ; 
            __context__.SourceCodeLine = 86;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            
            }
            
        private void UPDATE_TIMES (  SplusExecutionContext __context__ ) 
            { 
            ushort H = 0;
            ushort M = 0;
            ushort S = 0;
            ushort DEC = 0;
            
            short DUR_H = 0;
            
            
            __context__.SourceCodeLine = 92;
            H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 93;
            M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 94;
            S = (ushort) ( Functions.GetSecondsNum() ) ; 
            __context__.SourceCodeLine = 95;
            Trace( "update_times()...\r\n") ; 
            __context__.SourceCodeLine = 98;
            MakeString ( TOD__DOLLAR__ , "{0:d}:{1:d2}", (ushort)H, (ushort)M) ; 
            __context__.SourceCodeLine = 99;
            MakeString ( DATE__DOLLAR__ , "{0} {1} {2:d}, {3:d}", Functions.Left ( Functions.Day ( ) ,  (int) ( 3 ) ) , Functions.Left ( Functions.Month ( ) ,  (int) ( 3 ) ) , (ushort)Functions.GetDateNum(), (ushort)Functions.GetYearNum()) ; 
            __context__.SourceCodeLine = 102;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 3) ) || Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 0) )) ))  ) ) 
                {
                __context__.SourceCodeLine = 102;
                return ; 
                }
            
            __context__.SourceCodeLine = 105;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( S < I_START_S ))  ) ) 
                { 
                __context__.SourceCodeLine = 106;
                S = (ushort) ( (S + 60) ) ; 
                __context__.SourceCodeLine = 107;
                M = (ushort) ( (M - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 109;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < I_START_M ))  ) ) 
                { 
                __context__.SourceCodeLine = 110;
                M = (ushort) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 111;
                H = (ushort) ( (H - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 113;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( H < I_START_H ))  ) ) 
                { 
                __context__.SourceCodeLine = 114;
                H = (ushort) ( (H + 24) ) ; 
                } 
            
            __context__.SourceCodeLine = 116;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < I_START_M ))  ) ) 
                { 
                __context__.SourceCodeLine = 118;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (I_START_M - M) < 12 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 120;
                    DUR_H = (short) ( 0 ) ; 
                    __context__.SourceCodeLine = 121;
                    DEC = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 122;
                    Trace( ">>>dur_h within 12 hours earlier... FIXME\r\n") ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 129;
                    Trace( ">>>dur_h else within 12 hours later, must be after midnight; add 24 to h... FIXME\r\n") ; 
                    } 
                
                } 
            
            __context__.SourceCodeLine = 134;
            DUR_H = (short) ( (H - I_START_H) ) ; 
            __context__.SourceCodeLine = 135;
            DEC = (ushort) ( ((M - I_START_M) / 6) ) ; 
            __context__.SourceCodeLine = 136;
            Trace( "\tm={0:d}:{1:d}, i_start_m={2:d}  dur_h={3:d}:{4:d}  dec={5:d}\r\n", (ushort)M, (short)M, (ushort)I_START_M, (ushort)DUR_H, (short)DUR_H, (ushort)DEC) ; 
            __context__.SourceCodeLine = 137;
            MakeString ( DURATION_H__DOLLAR__ , "{0:d}.{1:d}", (short)DUR_H, (ushort)DEC) ; 
            __context__.SourceCodeLine = 138;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            
            }
            
        private void SELECT_ITEM (  SplusExecutionContext __context__, ushort SEL ) 
            { 
            
            __context__.SourceCodeLine = 142;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 1))  ) ) 
                {
                __context__.SourceCodeLine = 142;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 143;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 2))  ) ) 
                {
                __context__.SourceCodeLine = 143;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 2 ) ; 
                }
            
            __context__.SourceCodeLine = 144;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 3))  ) ) 
                {
                __context__.SourceCodeLine = 144;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 3 ) ; 
                }
            
            __context__.SourceCodeLine = 145;
            SELECT_ITEM__POUND__  .Value = (ushort) ( SEL ) ; 
            
            }
            
        private void INC_TIME (  SplusExecutionContext __context__, short INC ) 
            { 
            short M = 0;
            
            
            __context__.SourceCodeLine = 149;
            M = (short) ( (I_START_M + INC) ) ; 
            __context__.SourceCodeLine = 150;
            Trace( "inc_time({0:d}) m={1:d} i_start_h={2:d}, i_start_m={3:d},  m S/ 60={4:d}  m%60={5:d} m%60={6:d}\r\n", (short)INC, (short)M, (ushort)I_START_H, (ushort)I_START_M, (ushort)Divide( M , 60 ), (ushort)Mod( M , 60 ), (short)Mod( M , 60 )) ; 
            __context__.SourceCodeLine = 153;
            I_START_H = (ushort) ( (I_START_H + Divide( M , 60 )) ) ; 
            __context__.SourceCodeLine = 154;
            I_START_H = (ushort) ( Mod( I_START_H , 24 ) ) ; 
            __context__.SourceCodeLine = 155;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 156;
                Trace( "\t\tm=m+60 (m={0:d}:{1:d})\r\n", (ushort)M, (short)M) ; 
                __context__.SourceCodeLine = 157;
                M = (short) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 158;
                I_START_H = (ushort) ( (I_START_H - 1) ) ; 
                __context__.SourceCodeLine = 159;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I_START_H < 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 159;
                    I_START_H = (ushort) ( 23 ) ; 
                    }
                
                __context__.SourceCodeLine = 160;
                Trace( "\t\t\ti_start_h <- {0:d}:{1:d}\r\n", (ushort)I_START_H, (short)I_START_H) ; 
                __context__.SourceCodeLine = 155;
                } 
            
            __context__.SourceCodeLine = 162;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 163;
            Trace( "\t\tm %60 = ({0:d}:{1:d})\r\n", (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 164;
            I_START_M = (ushort) ( M ) ; 
            __context__.SourceCodeLine = 165;
            Trace( "\tnewh={0:d} newm={1:d}, m={2:d} m={3:d}\r\n", (ushort)I_START_H, (ushort)I_START_M, (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 166;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 167;
            Trace( "\tm={0:d} m={1:d}\r\n", (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 170;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)I_START_H, (ushort)I_START_M) ; 
            __context__.SourceCodeLine = 171;
            UPDATE_TIMES (  __context__  ) ; 
            
            }
            
        object SLEEP_PR_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 175;
                SELECT_ITEM (  __context__ , (ushort)( 1 )) ; 
                __context__.SourceCodeLine = 176;
                SINCE_TITLE__DOLLAR__  .UpdateValue ( "SLEEPING SINCE"  ) ; 
                __context__.SourceCodeLine = 177;
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
            
            __context__.SourceCodeLine = 180;
            SELECT_ITEM (  __context__ , (ushort)( 2 )) ; 
            __context__.SourceCodeLine = 181;
            SINCE_TITLE__DOLLAR__  .UpdateValue ( "AWAKE SINCE"  ) ; 
            __context__.SourceCodeLine = 182;
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
        
        __context__.SourceCodeLine = 185;
        SELECT_ITEM (  __context__ , (ushort)( 3 )) ; 
        __context__.SourceCodeLine = 186;
        SINCE_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 187;
        SINCE_TIME__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 188;
        DURATION_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 189;
        DURATION_H__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 190;
        DURATION_UNIT__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 191;
        Functions.Delay (  (int) ( 100 ) ) ; 
        __context__.SourceCodeLine = 192;
        DESELECT_ITEM__POUND__  .Value = (ushort) ( 3 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object TIME_INC_PR_OnPush_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 195;
        INC_TIME (  __context__ , (short)( 10 )) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object TIME_DEC_PR_OnPush_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 199;
        INC_TIME (  __context__ , (short)( Functions.ToSignedInteger( -( 10 ) ) )) ; 
        
        
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
        
        __context__.SourceCodeLine = 203;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 204;
        while ( Functions.TestForTrue  ( ( 1)  ) ) 
            { 
            __context__.SourceCodeLine = 205;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 206;
            Functions.Delay (  (int) ( 200 ) ) ; 
            __context__.SourceCodeLine = 204;
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    
object ARG1_OnChange_5 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        short R1 = 0;
        
        ushort R2 = 0;
        
        
        __context__.SourceCodeLine = 213;
        R1 = (short) ( Mod( ARG1  .ShortValue , ARG2  .ShortValue ) ) ; 
        __context__.SourceCodeLine = 214;
        R2 = (ushort) ( Mod( ARG1  .UshortValue , ARG2  .UshortValue ) ) ; 
        __context__.SourceCodeLine = 215;
        Trace( "({0:d}:{1:d}) %({2:d}:{3:d}) = ({4:d}:{5:d})({6:d}:{7:d})\r\n", (ushort)ARG1  .UshortValue, (short)ARG1  .UshortValue, (ushort)ARG2  .UshortValue, (short)ARG2  .UshortValue, (ushort)R1, (short)R1, (ushort)R2, (short)R2) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
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
    
    TIME_INC_PR = new Crestron.Logos.SplusObjects.DigitalInput( TIME_INC_PR__DigitalInput__, this );
    m_DigitalInputList.Add( TIME_INC_PR__DigitalInput__, TIME_INC_PR );
    
    TIME_DEC_PR = new Crestron.Logos.SplusObjects.DigitalInput( TIME_DEC_PR__DigitalInput__, this );
    m_DigitalInputList.Add( TIME_DEC_PR__DigitalInput__, TIME_DEC_PR );
    
    ARG1 = new Crestron.Logos.SplusObjects.AnalogInput( ARG1__AnalogSerialInput__, this );
    m_AnalogInputList.Add( ARG1__AnalogSerialInput__, ARG1 );
    
    ARG2 = new Crestron.Logos.SplusObjects.AnalogInput( ARG2__AnalogSerialInput__, this );
    m_AnalogInputList.Add( ARG2__AnalogSerialInput__, ARG2 );
    
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
    TIME_INC_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( TIME_INC_PR_OnPush_3, false ) );
    TIME_DEC_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( TIME_DEC_PR_OnPush_4, false ) );
    ARG1.OnAnalogChange.Add( new InputChangeHandlerWrapper( ARG1_OnChange_5, false ) );
    ARG2.OnAnalogChange.Add( new InputChangeHandlerWrapper( ARG1_OnChange_5, false ) );
    
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
const uint TIME_INC_PR__DigitalInput__ = 3;
const uint TIME_DEC_PR__DigitalInput__ = 4;
const uint ARG1__AnalogSerialInput__ = 0;
const uint ARG2__AnalogSerialInput__ = 1;
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
