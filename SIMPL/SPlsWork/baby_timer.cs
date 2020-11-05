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
        short DUR_H = 0;
        ushort DUR_DEC = 0;
        private void REMAKE_HISTORY_STRING (  SplusExecutionContext __context__ ) 
            { 
            ushort I = 0;
            ushort APPEND_DURATION = 0;
            
            CrestronString S;
            S  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 255, this );
            
            
            __context__.SourceCodeLine = 96;
            S  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 97;
            APPEND_DURATION = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 99;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)10; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 100;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ I ] == ""))  ) ) 
                    {
                    __context__.SourceCodeLine = 100;
                    break ; 
                    }
                
                __context__.SourceCodeLine = 104;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (I == 10))  ) ) 
                    {
                    __context__.SourceCodeLine = 104;
                    APPEND_DURATION = (ushort) ( 1 ) ; 
                    }
                
                else 
                    {
                    __context__.SourceCodeLine = 105;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ (I + 1) ] == ""))  ) ) 
                        {
                        __context__.SourceCodeLine = 105;
                        APPEND_DURATION = (ushort) ( 1 ) ; 
                        }
                    
                    }
                
                __context__.SourceCodeLine = 106;
                if ( Functions.TestForTrue  ( ( APPEND_DURATION)  ) ) 
                    { 
                    __context__.SourceCodeLine = 107;
                    MakeString ( S , "{0}{1} ({2:d}.{3:d}...\r", S , _SplusNVRAM.S_HISTORY [ I ] , (short)DUR_H, (ushort)DUR_DEC) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 109;
                    S  .UpdateValue ( S + _SplusNVRAM.S_HISTORY [ I ] + "\r"  ) ; 
                    } 
                
                __context__.SourceCodeLine = 99;
                } 
            
            __context__.SourceCodeLine = 114;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( S ) > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 115;
                S  .UpdateValue ( Functions.Left ( S ,  (int) ( (Functions.Length( S ) - 1) ) )  ) ; 
                } 
            
            __context__.SourceCodeLine = 119;
            HISTORY__DOLLAR__  .UpdateValue ( S  ) ; 
            
            }
            
        private void APPEND_DURATION (  SplusExecutionContext __context__, ushort POS ) 
            { 
            
            __context__.SourceCodeLine = 129;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( POS < 1 ) ) || Functions.TestForTrue ( Functions.BoolToInt ( POS > 10 ) )) ))  ) ) 
                {
                __context__.SourceCodeLine = 129;
                return ; 
                }
            
            __context__.SourceCodeLine = 132;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (DUR_DEC == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 133;
                MakeString ( _SplusNVRAM.S_HISTORY [ POS ] , "{0} ({1:d})", _SplusNVRAM.S_HISTORY [ POS ] , (short)DUR_H) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 135;
                MakeString ( _SplusNVRAM.S_HISTORY [ POS ] , "{0} ({1:d}.{2:d})", _SplusNVRAM.S_HISTORY [ POS ] , (short)DUR_H, (ushort)DUR_DEC) ; 
                } 
            
            __context__.SourceCodeLine = 137;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void CHANGE_2NDLAST_HISTORY_DURATION (  SplusExecutionContext __context__, CrestronString NEWDUR ) 
            { 
            ushort I = 0;
            ushort POS = 0;
            ushort POS2 = 0;
            
            
            __context__.SourceCodeLine = 149;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ 2 ] == ""))  ) ) 
                {
                __context__.SourceCodeLine = 149;
                return ; 
                }
            
            __context__.SourceCodeLine = 153;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ 10 ] != ""))  ) ) 
                { 
                __context__.SourceCodeLine = 155;
                POS = (ushort) ( (10 - 1) ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 159;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 3 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)10; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 160;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ I ] == ""))  ) ) 
                        { 
                        __context__.SourceCodeLine = 162;
                        POS = (ushort) ( (I - 2) ) ; 
                        __context__.SourceCodeLine = 163;
                        break ; 
                        } 
                    
                    __context__.SourceCodeLine = 159;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 169;
            POS2 = (ushort) ( Functions.Find( "(" , _SplusNVRAM.S_HISTORY[ POS ] ) ) ; 
            __context__.SourceCodeLine = 170;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (POS2 == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 172;
                _SplusNVRAM.S_HISTORY [ POS ]  .UpdateValue ( _SplusNVRAM.S_HISTORY [ POS ] + " (" + NEWDUR + ")"  ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 175;
                _SplusNVRAM.S_HISTORY [ POS ]  .UpdateValue ( Functions.Left ( _SplusNVRAM.S_HISTORY [ POS ] ,  (int) ( POS2 ) ) + NEWDUR + ")"  ) ; 
                } 
            
            __context__.SourceCodeLine = 177;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void ADD_HISTORY (  SplusExecutionContext __context__, CrestronString S ) 
            { 
            ushort I = 0;
            ushort EOL = 0;
            
            
            __context__.SourceCodeLine = 189;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ 10 ] != ""))  ) ) 
                { 
                __context__.SourceCodeLine = 191;
                EOL = (ushort) ( (10 + 1) ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 193;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)10; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 194;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.S_HISTORY[ I ] == ""))  ) ) 
                        { 
                        __context__.SourceCodeLine = 196;
                        EOL = (ushort) ( I ) ; 
                        __context__.SourceCodeLine = 197;
                        break ; 
                        } 
                    
                    __context__.SourceCodeLine = 193;
                    } 
                
                } 
            
            __context__.SourceCodeLine = 203;
            APPEND_DURATION (  __context__ , (ushort)( (EOL - 1) )) ; 
            __context__.SourceCodeLine = 206;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( EOL > 10 ))  ) ) 
                { 
                __context__.SourceCodeLine = 207;
                ushort __FN_FORSTART_VAL__2 = (ushort) ( 2 ) ;
                ushort __FN_FOREND_VAL__2 = (ushort)10; 
                int __FN_FORSTEP_VAL__2 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__2; (__FN_FORSTEP_VAL__2 > 0)  ? ( (I  >= __FN_FORSTART_VAL__2) && (I  <= __FN_FOREND_VAL__2) ) : ( (I  <= __FN_FORSTART_VAL__2) && (I  >= __FN_FOREND_VAL__2) ) ; I  += (ushort)__FN_FORSTEP_VAL__2) 
                    { 
                    __context__.SourceCodeLine = 208;
                    _SplusNVRAM.S_HISTORY [ (I - 1) ]  .UpdateValue ( _SplusNVRAM.S_HISTORY [ I ]  ) ; 
                    __context__.SourceCodeLine = 207;
                    } 
                
                __context__.SourceCodeLine = 210;
                _SplusNVRAM.S_HISTORY [ 10 ]  .UpdateValue ( ""  ) ; 
                __context__.SourceCodeLine = 211;
                EOL = (ushort) ( 10 ) ; 
                } 
            
            __context__.SourceCodeLine = 215;
            _SplusNVRAM.S_HISTORY [ EOL ]  .UpdateValue ( S  ) ; 
            __context__.SourceCodeLine = 218;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void SET_START_NOW (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 222;
            _SplusNVRAM.I_START_H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 223;
            _SplusNVRAM.I_START_M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 224;
            _SplusNVRAM.I_START_S = (ushort) ( Functions.GetSecondsNum() ) ; 
            __context__.SourceCodeLine = 225;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
            __context__.SourceCodeLine = 226;
            DURATION_TITLE__DOLLAR__  .UpdateValue ( "DURATION"  ) ; 
            __context__.SourceCodeLine = 227;
            DURATION_H__DOLLAR__  .UpdateValue ( "0"  ) ; 
            __context__.SourceCodeLine = 228;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            
            }
            
        private void UPDATE_TIMES (  SplusExecutionContext __context__ ) 
            { 
            ushort H = 0;
            ushort M = 0;
            ushort S = 0;
            
            
            __context__.SourceCodeLine = 237;
            H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 238;
            M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 239;
            S = (ushort) ( Functions.GetSecondsNum() ) ; 
            __context__.SourceCodeLine = 240;
            Trace( "update_times()...\r\n") ; 
            __context__.SourceCodeLine = 243;
            MakeString ( TOD__DOLLAR__ , "{0:d}:{1:d2}", (ushort)H, (ushort)M) ; 
            __context__.SourceCodeLine = 244;
            MakeString ( DATE__DOLLAR__ , "{0} {1} {2:d}, {3:d}", Functions.Left ( Functions.Day ( ) ,  (int) ( 3 ) ) , Functions.Left ( Functions.Month ( ) ,  (int) ( 3 ) ) , (ushort)Functions.GetDateNum(), (ushort)Functions.GetYearNum()) ; 
            __context__.SourceCodeLine = 247;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( S < _SplusNVRAM.I_START_S ))  ) ) 
                { 
                __context__.SourceCodeLine = 248;
                S = (ushort) ( (S + 60) ) ; 
                __context__.SourceCodeLine = 249;
                M = (ushort) ( (M - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 251;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < _SplusNVRAM.I_START_M ))  ) ) 
                { 
                __context__.SourceCodeLine = 252;
                M = (ushort) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 253;
                H = (ushort) ( (H - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 255;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( H < _SplusNVRAM.I_START_H ))  ) ) 
                { 
                __context__.SourceCodeLine = 256;
                H = (ushort) ( (H + 24) ) ; 
                } 
            
            __context__.SourceCodeLine = 258;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < _SplusNVRAM.I_START_M ))  ) ) 
                { 
                __context__.SourceCodeLine = 260;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (_SplusNVRAM.I_START_M - M) < 12 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 262;
                    DUR_H = (short) ( 0 ) ; 
                    __context__.SourceCodeLine = 263;
                    DUR_DEC = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 264;
                    Trace( ">>>Dur_h within 12 hours earlier... FIXME\r\n") ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 271;
                    Trace( ">>>Dur_h else within 12 hours later, must be after midnight; add 24 to h... FIXME\r\n") ; 
                    } 
                
                } 
            
            __context__.SourceCodeLine = 276;
            DUR_H = (short) ( (H - _SplusNVRAM.I_START_H) ) ; 
            __context__.SourceCodeLine = 277;
            DUR_DEC = (ushort) ( ((M - _SplusNVRAM.I_START_M) / 6) ) ; 
            __context__.SourceCodeLine = 278;
            Trace( "\tm={0:d}:{1:d}, i_start_m={2:d}  Dur_h={3:d}:{4:d}  Dur_dec={5:d}\r\n", (ushort)M, (short)M, (ushort)_SplusNVRAM.I_START_M, (ushort)DUR_H, (short)DUR_H, (ushort)DUR_DEC) ; 
            __context__.SourceCodeLine = 281;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 1) ) || Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 2) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 282;
                MakeString ( DURATION_H__DOLLAR__ , "{0:d}.{1:d}", (short)DUR_H, (ushort)DUR_DEC) ; 
                __context__.SourceCodeLine = 283;
                DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
                } 
            
            __context__.SourceCodeLine = 287;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void SELECT_ITEM (  SplusExecutionContext __context__, ushort SEL ) 
            { 
            
            __context__.SourceCodeLine = 291;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 1))  ) ) 
                {
                __context__.SourceCodeLine = 291;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 292;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 2))  ) ) 
                {
                __context__.SourceCodeLine = 292;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 2 ) ; 
                }
            
            __context__.SourceCodeLine = 293;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 3))  ) ) 
                {
                __context__.SourceCodeLine = 293;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 3 ) ; 
                }
            
            __context__.SourceCodeLine = 294;
            SELECT_ITEM__POUND__  .Value = (ushort) ( SEL ) ; 
            __context__.SourceCodeLine = 295;
            _SplusNVRAM.SAVE_SELECT = (ushort) ( SEL ) ; 
            
            }
            
        private void INC_TIME (  SplusExecutionContext __context__, short INC ) 
            { 
            short M = 0;
            
            
            __context__.SourceCodeLine = 304;
            M = (short) ( (_SplusNVRAM.I_START_M + INC) ) ; 
            __context__.SourceCodeLine = 305;
            Trace( "inc_time({0:d}) m={1:d} i_start_h={2:d}, i_start_m={3:d},  m S/ 60={4:d}  m%60={5:d} m%60={6:d}\r\n", (short)INC, (short)M, (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M, (ushort)Divide( M , 60 ), (ushort)Mod( M , 60 ), (short)Mod( M , 60 )) ; 
            __context__.SourceCodeLine = 308;
            _SplusNVRAM.I_START_H = (ushort) ( (_SplusNVRAM.I_START_H + Divide( M , 60 )) ) ; 
            __context__.SourceCodeLine = 309;
            _SplusNVRAM.I_START_H = (ushort) ( Mod( _SplusNVRAM.I_START_H , 24 ) ) ; 
            __context__.SourceCodeLine = 310;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 311;
                Trace( "\t\tm=m+60 (m={0:d}:{1:d})\r\n", (ushort)M, (short)M) ; 
                __context__.SourceCodeLine = 312;
                M = (short) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 313;
                _SplusNVRAM.I_START_H = (ushort) ( (_SplusNVRAM.I_START_H - 1) ) ; 
                __context__.SourceCodeLine = 314;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.I_START_H < 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 314;
                    _SplusNVRAM.I_START_H = (ushort) ( 23 ) ; 
                    }
                
                __context__.SourceCodeLine = 315;
                Trace( "\t\t\ti_start_h <- {0:d}:{1:d}\r\n", (ushort)_SplusNVRAM.I_START_H, (short)_SplusNVRAM.I_START_H) ; 
                __context__.SourceCodeLine = 310;
                } 
            
            __context__.SourceCodeLine = 317;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 318;
            Trace( "\t\tm %60 = ({0:d}:{1:d})\r\n", (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 319;
            _SplusNVRAM.I_START_M = (ushort) ( M ) ; 
            __context__.SourceCodeLine = 320;
            Trace( "\tnewh={0:d} newm={1:d}, m={2:d} m={3:d}\r\n", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M, (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 321;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 322;
            Trace( "\tm={0:d} m={1:d}\r\n", (ushort)M, (short)M) ; 
            __context__.SourceCodeLine = 325;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
            __context__.SourceCodeLine = 326;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 329;
            CHANGE_2NDLAST_HISTORY_DURATION (  __context__ , "???") ; 
            
            }
            
        object SLEEP_PR_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                CrestronString S;
                S  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 13, this );
                
                
                __context__.SourceCodeLine = 335;
                SELECT_ITEM (  __context__ , (ushort)( 1 )) ; 
                __context__.SourceCodeLine = 336;
                SINCE_TITLE__DOLLAR__  .UpdateValue ( "SLEEPING SINCE"  ) ; 
                __context__.SourceCodeLine = 337;
                SET_START_NOW (  __context__  ) ; 
                __context__.SourceCodeLine = 338;
                MakeString ( S , "SLEEP: {0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
                __context__.SourceCodeLine = 339;
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
            
            
            __context__.SourceCodeLine = 343;
            SELECT_ITEM (  __context__ , (ushort)( 2 )) ; 
            __context__.SourceCodeLine = 344;
            SINCE_TITLE__DOLLAR__  .UpdateValue ( "AWAKE SINCE"  ) ; 
            __context__.SourceCodeLine = 345;
            SET_START_NOW (  __context__  ) ; 
            __context__.SourceCodeLine = 346;
            MakeString ( S , "WAKE: {0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
            __context__.SourceCodeLine = 347;
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
        
        
        __context__.SourceCodeLine = 351;
        SELECT_ITEM (  __context__ , (ushort)( 3 )) ; 
        __context__.SourceCodeLine = 352;
        SINCE_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 353;
        SINCE_TIME__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 354;
        DURATION_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 355;
        DURATION_H__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 356;
        DURATION_UNIT__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 357;
        MakeString ( S , "OFF: {0:d}:{1:d2}", (ushort)Functions.GetHourNum(), (ushort)Functions.GetMinutesNum()) ; 
        __context__.SourceCodeLine = 358;
        ADD_HISTORY (  __context__ , S) ; 
        __context__.SourceCodeLine = 359;
        Functions.Delay (  (int) ( 100 ) ) ; 
        __context__.SourceCodeLine = 360;
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
        
        __context__.SourceCodeLine = 363;
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
        
        __context__.SourceCodeLine = 367;
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
        
        __context__.SourceCodeLine = 373;
        DUR_H = (short) ( 0 ) ; 
        __context__.SourceCodeLine = 374;
        DUR_DEC = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 377;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 378;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.SAVE_SELECT > 0 ))  ) ) 
            { 
            __context__.SourceCodeLine = 379;
            SELECT_ITEM (  __context__ , (ushort)( _SplusNVRAM.SAVE_SELECT )) ; 
            __context__.SourceCodeLine = 380;
            switch ((int)_SplusNVRAM.SAVE_SELECT)
            
                { 
                case 1 : 
                
                    { 
                    __context__.SourceCodeLine = 382;
                    SINCE_TITLE__DOLLAR__  .UpdateValue ( "*SLEEPING SINCE"  ) ; 
                    __context__.SourceCodeLine = 383;
                    MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
                    __context__.SourceCodeLine = 384;
                    DURATION_TITLE__DOLLAR__  .UpdateValue ( "*DURATION"  ) ; 
                    __context__.SourceCodeLine = 385;
                    break ; 
                    } 
                
                goto case 2 ;
                case 2 : 
                
                    { 
                    __context__.SourceCodeLine = 388;
                    SINCE_TITLE__DOLLAR__  .UpdateValue ( "*AWAKE SINCE"  ) ; 
                    __context__.SourceCodeLine = 389;
                    MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.I_START_H, (ushort)_SplusNVRAM.I_START_M) ; 
                    __context__.SourceCodeLine = 390;
                    DURATION_TITLE__DOLLAR__  .UpdateValue ( "*DURATION"  ) ; 
                    __context__.SourceCodeLine = 391;
                    break ; 
                    } 
                
                break;
                } 
                
            
            } 
        
        __context__.SourceCodeLine = 397;
        while ( Functions.TestForTrue  ( ( 1)  ) ) 
            { 
            __context__.SourceCodeLine = 398;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 399;
            Functions.Delay (  (int) ( 200 ) ) ; 
            __context__.SourceCodeLine = 397;
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
        
        
        __context__.SourceCodeLine = 406;
        R1 = (short) ( Mod( ARG1  .ShortValue , ARG2  .ShortValue ) ) ; 
        __context__.SourceCodeLine = 407;
        R2 = (ushort) ( Mod( ARG1  .UshortValue , ARG2  .UshortValue ) ) ; 
        __context__.SourceCodeLine = 408;
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
        _SplusNVRAM.S_HISTORY [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 25, this );
    
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
