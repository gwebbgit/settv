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
        Crestron.Logos.SplusObjects.StringOutput HISTORY__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogOutput SELECT_ITEM__POUND__;
        Crestron.Logos.SplusObjects.AnalogOutput DESELECT_ITEM__POUND__;
        private void REMAKE_HISTORY_STRING (  SplusExecutionContext __context__ ) 
            { 
            ushort I = 0;
            
            CrestronString S;
            S  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 255, this );
            
            
            __context__.SourceCodeLine = 87;
            S  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 88;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)10; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 89;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ I ] == ""))  ) ) 
                    {
                    __context__.SourceCodeLine = 89;
                    break ; 
                    }
                
                __context__.SourceCodeLine = 90;
                S  .UpdateValue ( S + _SplusNVRAM.S_HISTORY [ I ] + "\r\n"  ) ; 
                __context__.SourceCodeLine = 88;
                } 
            
            __context__.SourceCodeLine = 94;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( S ) > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 95;
                S  .UpdateValue ( Functions.Left ( S ,  (int) ( (Functions.Length( S ) - 1) ) )  ) ; 
                } 
            
            __context__.SourceCodeLine = 99;
            HISTORY__DOLLAR__  .UpdateValue ( S  ) ; 
            
            }
            
        private void ADD_HISTORY (  SplusExecutionContext __context__, CrestronString S ) 
            { 
            ushort I = 0;
            ushort EOL = 0;
            
            
            __context__.SourceCodeLine = 108;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ 10 ] != ""))  ) ) 
                { 
                __context__.SourceCodeLine = 110;
                EOL = (ushort) ( (10 + 1) ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 112;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)10; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 113;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ I ] == ""))  ) ) 
                        { 
                        __context__.SourceCodeLine = 115;
                        EOL = (ushort) ( I ) ; 
                        __context__.SourceCodeLine = 116;
                        break ; 
                        } 
                    
                    __context__.SourceCodeLine = 112;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 122;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( EOL > 10 ))  ) ) 
                { 
                __context__.SourceCodeLine = 123;
                ushort __FN_FORSTART_VAL__2 = (ushort) ( 2 ) ;
                ushort __FN_FOREND_VAL__2 = (ushort)10; 
                int __FN_FORSTEP_VAL__2 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__2; (__FN_FORSTEP_VAL__2 > 0)  ? ( (I  >= __FN_FORSTART_VAL__2) && (I  <= __FN_FOREND_VAL__2) ) : ( (I  <= __FN_FORSTART_VAL__2) && (I  >= __FN_FOREND_VAL__2) ) ; I  += (ushort)__FN_FORSTEP_VAL__2) 
                    { 
                    __context__.SourceCodeLine = 124;
                    _SplusNVRAM.S_HISTORY [ (I - 1) ]  .UpdateValue ( _SplusNVRAM.S_HISTORY [ I ]  ) ; 
                    __context__.SourceCodeLine = 123;
                    } 
                
                __context__.SourceCodeLine = 126;
                _SplusNVRAM.S_HISTORY [ 10 ]  .UpdateValue ( ""  ) ; 
                __context__.SourceCodeLine = 127;
                EOL = (ushort) ( 10 ) ; 
                } 
            
            __context__.SourceCodeLine = 131;
            _SplusNVRAM.S_HISTORY [ EOL ]  .UpdateValue ( S  ) ; 
            __context__.SourceCodeLine = 134;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void SET_START_NOW (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 138;
            _SplusNVRAM.I_START_H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 139;
            _SplusNVRAM.I_START_M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 140;
            _SplusNVRAM.I_START_S = (ushort) ( Functions.GetSecondsNum() ) ; 
            __context__.SourceCodeLine = 141;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
            __context__.SourceCodeLine = 142;
            DURATION_TITLE__DOLLAR__  .UpdateValue ( "DURATION"  ) ; 
            __context__.SourceCodeLine = 143;
            DURATION_H__DOLLAR__  .UpdateValue ( "0"  ) ; 
            __context__.SourceCodeLine = 144;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            
            }
            
        private void UPDATE_TIMES (  SplusExecutionContext __context__ ) 
            { 
            ushort H = 0;
            ushort M = 0;
            ushort S = 0;
            ushort DEC = 0;
            
            short DUR_H = 0;
            
            
            __context__.SourceCodeLine = 150;
            H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 151;
            M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 152;
            S = (ushort) ( Functions.GetSecondsNum() ) ; 
            __context__.SourceCodeLine = 153;
            Trace( "update_times()...\r\n") ; 
            __context__.SourceCodeLine = 156;
            MakeString ( TOD__DOLLAR__ , "{0:d}:{1:d2}", (ushort)H, (ushort)M) ; 
            __context__.SourceCodeLine = 157;
            MakeString ( DATE__DOLLAR__ , "{0} {1} {2:d}, {3:d}", Functions.Left ( Functions.Day ( ) ,  (int) ( 3 ) ) , Functions.Left ( Functions.Month ( ) ,  (int) ( 3 ) ) , (ushort)Functions.GetDateNum(), (ushort)Functions.GetYearNum()) ; 
            __context__.SourceCodeLine = 160;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 3) ) || Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 0) )) ))  ) ) 
                {
                __context__.SourceCodeLine = 160;
                return ; 
                }
            
            __context__.SourceCodeLine = 163;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( S < _SplusNVRAM.I_START_S ))  ) ) 
                { 
                __context__.SourceCodeLine = 164;
                S = (ushort) ( (S + 60) ) ; 
                __context__.SourceCodeLine = 165;
                M = (ushort) ( (M - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 167;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < _SplusNVRAM.I_START_M ))  ) ) 
                { 
                __context__.SourceCodeLine = 168;
                M = (ushort) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 169;
                H = (ushort) ( (H - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 171;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( H < _SplusNVRAM.I_START_H ))  ) ) 
                { 
                __context__.SourceCodeLine = 172;
                H = (ushort) ( (H + 24) ) ; 
                } 
            
            __context__.SourceCodeLine = 174;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < _SplusNVRAM.I_START_M ))  ) ) 
                { 
                __context__.SourceCodeLine = 176;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (_SplusNVRAM.I_START_M - M) < 12 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 178;
                    DUR_H = (short) ( 0 ) ; 
                    __context__.SourceCodeLine = 179;
                    DEC = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 180;
                    Trace( ">>>dur_h within 12 hours earlier... FIXME\r\n") ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 187;
                    Trace( ">>>dur_h else within 12 hours later, must be after midnight; add 24 to h... FIXME\r\n") ; 
                    } 
                
                } 
            
            __context__.SourceCodeLine = 192;
            DUR_H = (short) ( (H - _SplusNVRAM.I_START_H) ) ; 
            __context__.SourceCodeLine = 193;
            DEC = (ushort) ( ((M - _SplusNVRAM.I_START_M) / 6) ) ; 
            __context__.SourceCodeLine = 194;
            Trace( "\tm={0:d}:{1:d}, i_start_m={2:d}  dur_h={3:d}:{4:d}  dec={5:d}\r\n", (ushort)M, (short)M, (ushort)_SplusNVRAM.I_START_M, (ushort)DUR_H, (short)DUR_H, (ushort)DEC) ; 
            __context__.SourceCodeLine = 195;
            MakeString ( DURATION_H__DOLLAR__ , "{0:d}.{1:d}", (short)DUR_H, (ushort)DEC) ; 
            __context__.SourceCodeLine = 196;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            
            }
            
        private void SELECT_ITEM (  SplusExecutionContext __context__, ushort SEL ) 
            { 
            
            __context__.SourceCodeLine = 200;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 1))  ) ) 
                {
                __context__.SourceCodeLine = 200;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 201;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 2))  ) ) 
                {
                __context__.SourceCodeLine = 201;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 2 ) ; 
                }
            
            __context__.SourceCodeLine = 202;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 3))  ) ) 
                {
                __context__.SourceCodeLine = 202;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 3 ) ; 
                }
            
            __context__.SourceCodeLine = 203;
            SELECT_ITEM__POUND__  .Value = (ushort) ( SEL ) ; 
            __context__.SourceCodeLine = 204;
            _SplusNVRAM.SAVE_SELECT = (ushort) ( SEL ) ; 
            
            }
            
        private void INC_TIME (  SplusExecutionContext __context__, short INC ) 
            { 
            short M = 0;
            
            
            __context__.SourceCodeLine = 208;
            M = (short) ( (_SplusNVRAM.I_START_M + INC) ) ; 
            __context__.SourceCodeLine = 209;
            Trace( "inc_time({0:d}) m={1:d} i_start_h={2:d}, i_start_m={3:d},  m S/ 60={4:d}  m%60={5:d} m%60={6:d}\r\n", (short)INC, (short)M, (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M, (ushort)Divide( M , 60 ), (ushort)Mod( M , 60 ), (short)Mod( M , 60 )) ; 
            __context__.SourceCodeLine = 212;
            _SplusNVRAM.I_START_H = (ushort) ( (_SplusNVRAM.I_START_H + Divide( M , 60 )) ) ; 
            __context__.SourceCodeLine = 213;
            _SplusNVRAM.I_START_H = (ushort) ( Mod( _SplusNVRAM.I_START_H , 24 ) ) ; 
            __context__.SourceCodeLine = 214;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 215;
                Trace( "\t\tm=m+60 (m={0:d}:{1:d})\r\n", (ushort)M, (short)M) ; 
                __context__.SourceCodeLine = 216;
                M = (short) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 217;
                _SplusNVRAM.I_START_H = (ushort) ( (_SplusNVRAM.I_START_H - 1) ) ; 
                __context__.SourceCodeLine = 218;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.I_START_H < 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 218;
                    _SplusNVRAM.I_START_H = (ushort) ( 23 ) ; 
                    }
                
                __context__.SourceCodeLine = 219;
                Trace( "\t\t\ti_start_h <- {0:d}:{1:d}\r\n", (ushort)_SplusNVRAM.I_START_H, (short)_SplusNVRAM.I_START_H) ; 
                __context__.SourceCodeLine = 214;
                } 
            
            __context__.SourceCodeLine = 221;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 222;
            Trace( "\t\tm %60 = ({0:d}:{1:d})\r\n", (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 223;
            _SplusNVRAM.I_START_M = (ushort) ( M ) ; 
            __context__.SourceCodeLine = 224;
            Trace( "\tnewh={0:d} newm={1:d}, m={2:d} m={3:d}\r\n", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M, (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 225;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 226;
            Trace( "\tm={0:d} m={1:d}\r\n", (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 229;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
            __context__.SourceCodeLine = 230;
            UPDATE_TIMES (  __context__  ) ; 
            
            }
            
        object SLEEP_PR_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                CrestronString S;
                S  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 13, this );
                
                
                __context__.SourceCodeLine = 235;
                SELECT_ITEM (  __context__ , (ushort)( 1 )) ; 
                __context__.SourceCodeLine = 236;
                SINCE_TITLE__DOLLAR__  .UpdateValue ( "SLEEPING SINCE"  ) ; 
                __context__.SourceCodeLine = 237;
                SET_START_NOW (  __context__  ) ; 
                __context__.SourceCodeLine = 238;
                MakeString ( S , "SLEEP: {0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
                __context__.SourceCodeLine = 239;
                ADD_HISTORY (  __context__ , S) ; 
                
                
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
            CrestronString S;
            S  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 12, this );
            
            
            __context__.SourceCodeLine = 243;
            SELECT_ITEM (  __context__ , (ushort)( 2 )) ; 
            __context__.SourceCodeLine = 244;
            SINCE_TITLE__DOLLAR__  .UpdateValue ( "AWAKE SINCE"  ) ; 
            __context__.SourceCodeLine = 245;
            SET_START_NOW (  __context__  ) ; 
            __context__.SourceCodeLine = 246;
            MakeString ( S , "WAKE: {0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
            __context__.SourceCodeLine = 247;
            ADD_HISTORY (  __context__ , S) ; 
            
            
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
        CrestronString S;
        S  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
        
        
        __context__.SourceCodeLine = 251;
        SELECT_ITEM (  __context__ , (ushort)( 3 )) ; 
        __context__.SourceCodeLine = 252;
        SINCE_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 253;
        SINCE_TIME__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 254;
        DURATION_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 255;
        DURATION_H__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 256;
        DURATION_UNIT__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 257;
        MakeString ( S , "OFF: {0:d}:{1:d2}", (ushort)Functions.GetHourNum(), (ushort)Functions.GetMinutesNum()) ; 
        __context__.SourceCodeLine = 258;
        ADD_HISTORY (  __context__ , S) ; 
        __context__.SourceCodeLine = 259;
        Functions.Delay (  (int) ( 100 ) ) ; 
        __context__.SourceCodeLine = 260;
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
        
        __context__.SourceCodeLine = 263;
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
        
        __context__.SourceCodeLine = 267;
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
        
        __context__.SourceCodeLine = 271;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 272;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.SAVE_SELECT > 0 ))  ) ) 
            {
            __context__.SourceCodeLine = 272;
            SELECT_ITEM (  __context__ , (ushort)( _SplusNVRAM.SAVE_SELECT )) ; 
            }
        
        __context__.SourceCodeLine = 273;
        while ( Functions.TestForTrue  ( ( 1)  ) ) 
            { 
            __context__.SourceCodeLine = 274;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 275;
            Functions.Delay (  (int) ( 200 ) ) ; 
            __context__.SourceCodeLine = 273;
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
        
        
        __context__.SourceCodeLine = 282;
        R1 = (short) ( Mod( ARG1  .ShortValue , ARG2  .ShortValue ) ) ; 
        __context__.SourceCodeLine = 283;
        R2 = (ushort) ( Mod( ARG1  .UshortValue , ARG2  .UshortValue ) ) ; 
        __context__.SourceCodeLine = 284;
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
    _SplusNVRAM.S_HISTORY  = new CrestronString[ 11 ];
    for( uint i = 0; i < 11; i++ )
        _SplusNVRAM.S_HISTORY [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 20, this );
    
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
    
    HISTORY__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( HISTORY__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( HISTORY__DOLLAR____AnalogSerialOutput__, HISTORY__DOLLAR__ );
    
    
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
const uint HISTORY__DOLLAR____AnalogSerialOutput__ = 7;
const uint SELECT_ITEM__POUND____AnalogSerialOutput__ = 8;
const uint DESELECT_ITEM__POUND____AnalogSerialOutput__ = 9;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    [SplusStructAttribute(0, false, true)]
            public ushort I_START_H = 0;
            [SplusStructAttribute(1, false, true)]
            public ushort I_START_M = 0;
            [SplusStructAttribute(2, false, true)]
            public ushort I_START_S = 0;
            [SplusStructAttribute(3, false, true)]
            public ushort SAVE_SELECT = 0;
            [SplusStructAttribute(4, false, true)]
            public CrestronString [] S_HISTORY;
            
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
