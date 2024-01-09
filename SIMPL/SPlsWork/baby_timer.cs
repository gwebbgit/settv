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
        Crestron.Logos.SplusObjects.AnalogInput FONTSCALE__POUND__;
        Crestron.Logos.SplusObjects.StringInput UCMD__DOLLAR__;
        Crestron.Logos.SplusObjects.DigitalOutput TOD_PM;
        Crestron.Logos.SplusObjects.StringOutput SINCE_TITLE__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput SINCE_TIME__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DURATION_TITLE__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DURATION_H__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DURATION_UNIT__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DATE__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput TOD_12H__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput TOD_24H__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogOutput SELECT_ITEM__POUND__;
        Crestron.Logos.SplusObjects.AnalogOutput DESELECT_ITEM__POUND__;
        Crestron.Logos.SplusObjects.AnalogOutput FONTSCALE_OUT__POUND__;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> HISTORY__DOLLAR__;
        CrestronString CURR_DUR;
        TIME_ENTRY NOW;
        private void UPDATE_NOW (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 120;
            NOW . H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 121;
            NOW . M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 122;
            NOW . S = (ushort) ( Functions.GetSecondsNum() ) ; 
            
            }
            
        private void REMAKE_HISTORY_STRING (  SplusExecutionContext __context__ ) 
            { 
            ushort I = 0;
            
            CrestronString COLOR;
            COLOR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 6, this );
            
            
            __context__.SourceCodeLine = 132;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)_SplusNVRAM.I_CURR; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 135;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.TE[ I ].TYPE == "WAKE"))  ) ) 
                    { 
                    __context__.SourceCodeLine = 137;
                    COLOR  .UpdateValue ( "a0ffa0"  ) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 139;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.TE[ I ].TYPE == "SLEEP"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 141;
                        COLOR  .UpdateValue ( "7f7fff"  ) ; 
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 143;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.TE[ I ].TYPE == "OFF"))  ) ) 
                            { 
                            __context__.SourceCodeLine = 145;
                            COLOR  .UpdateValue ( "7f7f7f"  ) ; 
                            } 
                        
                        else 
                            { 
                            __context__.SourceCodeLine = 148;
                            COLOR  .UpdateValue ( "ffffff"  ) ; 
                            } 
                        
                        }
                    
                    }
                
                __context__.SourceCodeLine = 154;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (I == _SplusNVRAM.I_CURR) ) || Functions.TestForTrue ( Functions.BoolToInt (I == 10) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 156;
                    MakeString ( HISTORY__DOLLAR__ [ I] , "<font color=\"#{0}\">{1}: {2:d}:{3:d2} ({4}...</font>", COLOR , _SplusNVRAM.TE [ I] . TYPE , (ushort)_SplusNVRAM.TE[ I ].H, (ushort)_SplusNVRAM.TE[ I ].M, CURR_DUR ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 159;
                    MakeString ( HISTORY__DOLLAR__ [ I] , "<font color=\"#{0}\">{1}: {2:d}:{3:d2} ({4})</font>", COLOR , _SplusNVRAM.TE [ I] . TYPE , (ushort)_SplusNVRAM.TE[ I ].H, (ushort)_SplusNVRAM.TE[ I ].M, _SplusNVRAM.TE [ I] . DUR ) ; 
                    } 
                
                __context__.SourceCodeLine = 132;
                } 
            
            
            }
            
        private CrestronString CALC_DURATION (  SplusExecutionContext __context__, TIME_ENTRY TE1 , TIME_ENTRY TE2 ) 
            { 
            ushort DUR_DEC = 0;
            
            short DUR_H = 0;
            
            CrestronString RET;
            RET  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
            
            TIME_ENTRY LT2;
            LT2  = new TIME_ENTRY( this, true );
            LT2 .PopulateCustomAttributeList( false );
            
            
            __context__.SourceCodeLine = 177;
            /* Trace( "calc_duration()...\r\n") */ ; 
            __context__.SourceCodeLine = 180;
            LT2 . H = (ushort) ( TE2.H ) ; 
            __context__.SourceCodeLine = 181;
            LT2 . M = (ushort) ( TE2.M ) ; 
            __context__.SourceCodeLine = 182;
            LT2 . S = (ushort) ( TE2.S ) ; 
            __context__.SourceCodeLine = 183;
            LT2 . DUR  .UpdateValue ( TE2 . DUR  ) ; 
            __context__.SourceCodeLine = 184;
            LT2 . TYPE  .UpdateValue ( TE2 . TYPE  ) ; 
            __context__.SourceCodeLine = 187;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LT2.S < TE1.S ))  ) ) 
                { 
                __context__.SourceCodeLine = 188;
                LT2 . S = (ushort) ( (LT2.S + 60) ) ; 
                __context__.SourceCodeLine = 189;
                LT2 . M = (ushort) ( (LT2.M - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 191;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LT2.M < TE1.M ))  ) ) 
                { 
                __context__.SourceCodeLine = 192;
                LT2 . M = (ushort) ( (LT2.M + 60) ) ; 
                __context__.SourceCodeLine = 193;
                LT2 . H = (ushort) ( (LT2.H - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 195;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LT2.H < TE1.H ))  ) ) 
                { 
                __context__.SourceCodeLine = 196;
                LT2 . H = (ushort) ( (LT2.H + 24) ) ; 
                } 
            
            __context__.SourceCodeLine = 198;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LT2.M < TE1.M ))  ) ) 
                { 
                __context__.SourceCodeLine = 200;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (TE1.M - LT2.M) < 12 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 202;
                    DUR_H = (short) ( 0 ) ; 
                    __context__.SourceCodeLine = 203;
                    DUR_DEC = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 204;
                    /* Trace( ">>>dur_h within 12 hours earlier... FIXME\r\n") */ ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 210;
                    /* Trace( ">>>dur_h else within 12 hours later, must be after midnight; add 24 to te2.h... FIXME\r\n") */ ; 
                    } 
                
                } 
            
            __context__.SourceCodeLine = 215;
            DUR_H = (short) ( (LT2.H - TE1.H) ) ; 
            __context__.SourceCodeLine = 216;
            DUR_DEC = (ushort) ( ((LT2.M - TE1.M) / 6) ) ; 
            __context__.SourceCodeLine = 217;
            /* Trace( "\tlt2.m={0:d}:{1:d}, te1.m={2:d}:{3:d}  dur_h={4:d}:{5:d}  dur_dec={6:d}\r\n", (ushort)TE2.M, (short)TE2.M, (ushort)TE1.M, (short)TE1.M, (ushort)DUR_H, (short)DUR_H, (ushort)DUR_DEC) */ ; 
            __context__.SourceCodeLine = 219;
            MakeString ( RET , "{0:d}.{1:d}", (short)DUR_H, (ushort)DUR_DEC) ; 
            __context__.SourceCodeLine = 220;
            return ( RET ) ; 
            
            }
            
        private void ADD_START_HISTORY (  SplusExecutionContext __context__, CrestronString NEWTYPE ) 
            { 
            ushort I = 0;
            
            CrestronString PREV_DUR;
            PREV_DUR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
            
            
            __context__.SourceCodeLine = 233;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.I_CURR >= 10 ))  ) ) 
                { 
                __context__.SourceCodeLine = 235;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 2 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)10; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 236;
                    _SplusNVRAM.TE [ (I - 1)] . H = (ushort) ( _SplusNVRAM.TE[ I ].H ) ; 
                    __context__.SourceCodeLine = 237;
                    _SplusNVRAM.TE [ (I - 1)] . M = (ushort) ( _SplusNVRAM.TE[ I ].M ) ; 
                    __context__.SourceCodeLine = 238;
                    _SplusNVRAM.TE [ (I - 1)] . S = (ushort) ( _SplusNVRAM.TE[ I ].S ) ; 
                    __context__.SourceCodeLine = 239;
                    _SplusNVRAM.TE [ (I - 1)] . DUR  .UpdateValue ( _SplusNVRAM.TE [ I] . DUR  ) ; 
                    __context__.SourceCodeLine = 240;
                    _SplusNVRAM.TE [ (I - 1)] . TYPE  .UpdateValue ( _SplusNVRAM.TE [ I] . TYPE  ) ; 
                    __context__.SourceCodeLine = 235;
                    } 
                
                __context__.SourceCodeLine = 242;
                _SplusNVRAM.TE [ 10] . TYPE  .UpdateValue ( ""  ) ; 
                __context__.SourceCodeLine = 243;
                _SplusNVRAM.I_CURR = (ushort) ( 10 ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 246;
                _SplusNVRAM.I_CURR = (ushort) ( (_SplusNVRAM.I_CURR + 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 249;
            UPDATE_NOW (  __context__  ) ; 
            __context__.SourceCodeLine = 252;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . H = (ushort) ( NOW.H ) ; 
            __context__.SourceCodeLine = 253;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . M = (ushort) ( NOW.M ) ; 
            __context__.SourceCodeLine = 254;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . S = (ushort) ( NOW.S ) ; 
            __context__.SourceCodeLine = 255;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . TYPE  .UpdateValue ( NEWTYPE  ) ; 
            __context__.SourceCodeLine = 256;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . DUR  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 257;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].H, (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M) ; 
            __context__.SourceCodeLine = 258;
            DURATION_TITLE__DOLLAR__  .UpdateValue ( "DURATION"  ) ; 
            __context__.SourceCodeLine = 259;
            DURATION_H__DOLLAR__  .UpdateValue ( "0"  ) ; 
            __context__.SourceCodeLine = 260;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            __context__.SourceCodeLine = 263;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.I_CURR > 1 ))  ) ) 
                { 
                __context__.SourceCodeLine = 264;
                PREV_DUR  .UpdateValue ( CALC_DURATION (  __context__ , _SplusNVRAM.TE[ (_SplusNVRAM.I_CURR - 1) ], _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ])  ) ; 
                __context__.SourceCodeLine = 265;
                _SplusNVRAM.TE [ (_SplusNVRAM.I_CURR - 1)] . DUR  .UpdateValue ( PREV_DUR  ) ; 
                } 
            
            __context__.SourceCodeLine = 269;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void UPDATE_TIMES (  SplusExecutionContext __context__ ) 
            { 
            uint SIZE1 = 0;
            uint SIZE2 = 0;
            
            
            __context__.SourceCodeLine = 278;
            UPDATE_NOW (  __context__  ) ; 
            __context__.SourceCodeLine = 279;
            /* Trace( "update_times()...\r\n\ti_curr={0:d}\r\n", (ushort)_SplusNVRAM.I_CURR) */ ; 
            __context__.SourceCodeLine = 282;
            SIZE1 = (uint) ( ((320 * FONTSCALE__POUND__  .UintValue) / 65535) ) ; 
            __context__.SourceCodeLine = 283;
            SIZE2 = (uint) ( ((160 * FONTSCALE__POUND__  .UintValue) / 65535) ) ; 
            __context__.SourceCodeLine = 286;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (NOW.H == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 289;
                MakeString ( TOD_12H__DOLLAR__ , "<font size=\"{0:d}\">{1:d}:{2:d2}</font><font size=\"{3:d}\">AM</font>", (uint)SIZE1, (ushort)12, (ushort)NOW.M, (uint)SIZE2) ; 
                __context__.SourceCodeLine = 290;
                TOD_PM  .Value = (ushort) ( 0 ) ; 
                } 
            
            else 
                {
                __context__.SourceCodeLine = 291;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NOW.H < 12 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 294;
                    MakeString ( TOD_12H__DOLLAR__ , "<font size=\"{0:d}\">{1:d}:{2:d2}</font><font size=\"{3:d}\">AM</font>", (uint)SIZE1, (ushort)NOW.H, (ushort)NOW.M, (uint)SIZE2) ; 
                    __context__.SourceCodeLine = 295;
                    TOD_PM  .Value = (ushort) ( 0 ) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 296;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (NOW.H == 12))  ) ) 
                        { 
                        __context__.SourceCodeLine = 299;
                        MakeString ( TOD_12H__DOLLAR__ , "<font size=\"{0:d}\">{1:d}:{2:d2}</font><font size=\"{3:d}\">PM</font>", (uint)SIZE1, (ushort)12, (ushort)NOW.M, (uint)SIZE2) ; 
                        __context__.SourceCodeLine = 300;
                        TOD_PM  .Value = (ushort) ( 1 ) ; 
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 301;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NOW.H > 12 ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 304;
                            MakeString ( TOD_12H__DOLLAR__ , "<font size=\"{0:d}\">{1:d}:{2:d2}</font><font size=\"{3:d}\">PM</font>", (uint)SIZE1, (ushort)NOW.H, (ushort)NOW.M, (uint)SIZE2) ; 
                            __context__.SourceCodeLine = 305;
                            TOD_PM  .Value = (ushort) ( 1 ) ; 
                            } 
                        
                        }
                    
                    }
                
                }
            
            __context__.SourceCodeLine = 307;
            MakeString ( TOD_24H__DOLLAR__ , "{0:d}:{1:d2}", (ushort)NOW.H, (ushort)NOW.M) ; 
            __context__.SourceCodeLine = 308;
            MakeString ( DATE__DOLLAR__ , "{0} {1} {2:d}, {3:d}", Functions.Left ( Functions.Day ( ) ,  (int) ( 3 ) ) , Functions.Left ( Functions.Month ( ) ,  (int) ( 3 ) ) , (ushort)Functions.GetDateNum(), (ushort)Functions.GetYearNum()) ; 
            __context__.SourceCodeLine = 312;
            CURR_DUR  .UpdateValue ( CALC_DURATION (  __context__ , _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ], NOW)  ) ; 
            __context__.SourceCodeLine = 313;
            /* Trace( "\tCurr_Dur= {{{0}}}\r\n", CURR_DUR ) */ ; 
            __context__.SourceCodeLine = 316;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 1) ) || Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 2) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 318;
                DURATION_H__DOLLAR__  .UpdateValue ( CURR_DUR  ) ; 
                __context__.SourceCodeLine = 319;
                DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
                } 
            
            __context__.SourceCodeLine = 323;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void SELECT_ITEM (  SplusExecutionContext __context__, ushort SEL ) 
            { 
            
            __context__.SourceCodeLine = 327;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 1))  ) ) 
                {
                __context__.SourceCodeLine = 327;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 328;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 2))  ) ) 
                {
                __context__.SourceCodeLine = 328;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 2 ) ; 
                }
            
            __context__.SourceCodeLine = 329;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 3))  ) ) 
                {
                __context__.SourceCodeLine = 329;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 3 ) ; 
                }
            
            __context__.SourceCodeLine = 330;
            SELECT_ITEM__POUND__  .Value = (ushort) ( SEL ) ; 
            __context__.SourceCodeLine = 331;
            _SplusNVRAM.SAVE_SELECT = (ushort) ( SEL ) ; 
            
            }
            
        private void INC_TIME (  SplusExecutionContext __context__, short INC ) 
            { 
            ushort SH = 0;
            ushort SM = 0;
            ushort SS = 0;
            
            short M = 0;
            
            CrestronString PREV_DUR;
            PREV_DUR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
            
            
            __context__.SourceCodeLine = 346;
            SH = (ushort) ( _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].H ) ; 
            __context__.SourceCodeLine = 347;
            SM = (ushort) ( _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M ) ; 
            __context__.SourceCodeLine = 348;
            SS = (ushort) ( _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].S ) ; 
            __context__.SourceCodeLine = 351;
            M = (short) ( (_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M + INC) ) ; 
            __context__.SourceCodeLine = 352;
            /* Trace( "inc_time({0:d}) m={1:d} sh={2:d}, sm={3:d},  m S/ 60={4:d}  m%60={5:d} m%60={6:d}\r\n", (short)INC, (short)M, (ushort)SH, (ushort)SM, (ushort)Divide( M , 60 ), (ushort)Mod( M , 60 ), (short)Mod( M , 60 )) */ ; 
            __context__.SourceCodeLine = 355;
            SH = (ushort) ( (SH + Divide( M , 60 )) ) ; 
            __context__.SourceCodeLine = 356;
            SH = (ushort) ( Mod( SH , 24 ) ) ; 
            __context__.SourceCodeLine = 357;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 358;
                /* Trace( "\t\tm=m+60 (m={0:d}:{1:d})\r\n", (ushort)M, (short)M) */ ; 
                __context__.SourceCodeLine = 359;
                M = (short) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 360;
                SH = (ushort) ( (SH - 1) ) ; 
                __context__.SourceCodeLine = 361;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( SH < 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 361;
                    SH = (ushort) ( 23 ) ; 
                    }
                
                __context__.SourceCodeLine = 362;
                /* Trace( "\t\t\tsh <- {0:d}:{1:d}\r\n", (ushort)SH, (short)SH) */ ; 
                __context__.SourceCodeLine = 357;
                } 
            
            __context__.SourceCodeLine = 364;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 365;
            /* Trace( "\t\tm %60 = ({0:d}:{1:d})\r\n", (ushort)M, (short)M) */ ; 
            __context__.SourceCodeLine = 366;
            SM = (ushort) ( M ) ; 
            __context__.SourceCodeLine = 367;
            /* Trace( "\tnewh={0:d} newm={1:d}, m={2:d} m={3:d}\r\n", (ushort)SH, (ushort)SM, (ushort)M, (short)M) */ ; 
            __context__.SourceCodeLine = 368;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 369;
            /* Trace( "\tm={0:d} m={1:d}\r\n", (ushort)M, (short)M) */ ; 
            __context__.SourceCodeLine = 372;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . H = (ushort) ( SH ) ; 
            __context__.SourceCodeLine = 373;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . M = (ushort) ( SM ) ; 
            __context__.SourceCodeLine = 374;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . S = (ushort) ( SS ) ; 
            __context__.SourceCodeLine = 377;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.I_CURR > 1 ))  ) ) 
                { 
                __context__.SourceCodeLine = 378;
                PREV_DUR  .UpdateValue ( CALC_DURATION (  __context__ , _SplusNVRAM.TE[ (_SplusNVRAM.I_CURR - 1) ], _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ])  ) ; 
                __context__.SourceCodeLine = 379;
                _SplusNVRAM.TE [ (_SplusNVRAM.I_CURR - 1)] . DUR  .UpdateValue ( PREV_DUR  ) ; 
                __context__.SourceCodeLine = 380;
                /* Trace( "\tupdating TE[i_curr-1 ({0:d})].dur = {{{1}}}\r\n", (ushort)(_SplusNVRAM.I_CURR - 1), PREV_DUR ) */ ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 382;
                /* Trace( "\tNOT updating TE[i_curr-1 ({0:d})].dur\r\n", (ushort)(_SplusNVRAM.I_CURR - 1)) */ ; 
                } 
            
            __context__.SourceCodeLine = 386;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)SH, (ushort)SM) ; 
            __context__.SourceCodeLine = 387;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 390;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        object SLEEP_PR_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 396;
                SELECT_ITEM (  __context__ , (ushort)( 1 )) ; 
                __context__.SourceCodeLine = 397;
                SINCE_TITLE__DOLLAR__  .UpdateValue ( "SLEEPING SINCE"  ) ; 
                __context__.SourceCodeLine = 398;
                ADD_START_HISTORY (  __context__ , "SLEEP") ; 
                
                
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
            
            __context__.SourceCodeLine = 402;
            SELECT_ITEM (  __context__ , (ushort)( 2 )) ; 
            __context__.SourceCodeLine = 403;
            SINCE_TITLE__DOLLAR__  .UpdateValue ( "AWAKE SINCE"  ) ; 
            __context__.SourceCodeLine = 404;
            ADD_START_HISTORY (  __context__ , "WAKE") ; 
            
            
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
        
        __context__.SourceCodeLine = 408;
        SELECT_ITEM (  __context__ , (ushort)( 3 )) ; 
        __context__.SourceCodeLine = 409;
        SINCE_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 410;
        SINCE_TIME__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 411;
        DURATION_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 412;
        DURATION_H__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 413;
        DURATION_UNIT__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 414;
        ADD_START_HISTORY (  __context__ , "OFF") ; 
        __context__.SourceCodeLine = 415;
        Functions.Delay (  (int) ( 100 ) ) ; 
        __context__.SourceCodeLine = 416;
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
        
        __context__.SourceCodeLine = 419;
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
        
        __context__.SourceCodeLine = 423;
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
        
        __context__.SourceCodeLine = 429;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 430;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.SAVE_SELECT > 0 ))  ) ) 
            { 
            __context__.SourceCodeLine = 431;
            SELECT_ITEM (  __context__ , (ushort)( _SplusNVRAM.SAVE_SELECT )) ; 
            __context__.SourceCodeLine = 432;
            switch ((int)_SplusNVRAM.SAVE_SELECT)
            
                { 
                case 1 : 
                
                    { 
                    __context__.SourceCodeLine = 434;
                    SINCE_TITLE__DOLLAR__  .UpdateValue ( "*SLEEPING SINCE"  ) ; 
                    __context__.SourceCodeLine = 435;
                    MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].H, (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M) ; 
                    __context__.SourceCodeLine = 436;
                    DURATION_TITLE__DOLLAR__  .UpdateValue ( "*DURATION"  ) ; 
                    __context__.SourceCodeLine = 437;
                    break ; 
                    } 
                
                goto case 2 ;
                case 2 : 
                
                    { 
                    __context__.SourceCodeLine = 440;
                    SINCE_TITLE__DOLLAR__  .UpdateValue ( "*AWAKE SINCE"  ) ; 
                    __context__.SourceCodeLine = 441;
                    MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].H, (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M) ; 
                    __context__.SourceCodeLine = 442;
                    DURATION_TITLE__DOLLAR__  .UpdateValue ( "*DURATION"  ) ; 
                    __context__.SourceCodeLine = 443;
                    break ; 
                    } 
                
                break;
                } 
                
            
            } 
        
        __context__.SourceCodeLine = 447;
        FONTSCALE_OUT__POUND__  .Value = (ushort) ( _SplusNVRAM.SAVE_FONTSCALE ) ; 
        __context__.SourceCodeLine = 450;
        while ( Functions.TestForTrue  ( ( 1)  ) ) 
            { 
            __context__.SourceCodeLine = 451;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 452;
            Functions.Delay (  (int) ( 200 ) ) ; 
            __context__.SourceCodeLine = 450;
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
        
        
        __context__.SourceCodeLine = 459;
        R1 = (short) ( Mod( ARG1  .ShortValue , ARG2  .ShortValue ) ) ; 
        __context__.SourceCodeLine = 460;
        R2 = (ushort) ( Mod( ARG1  .UshortValue , ARG2  .UshortValue ) ) ; 
        __context__.SourceCodeLine = 461;
        /* Trace( "({0:d}:{1:d}) %({2:d}:{3:d}) = ({4:d}:{5:d})({6:d}:{7:d})\r\n", (ushort)ARG1  .UshortValue, (short)ARG1  .UshortValue, (ushort)ARG2  .UshortValue, (short)ARG2  .UshortValue, (ushort)R1, (short)R1, (ushort)R2, (short)R2) */ ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object UCMD__DOLLAR___OnChange_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        uint N = 0;
        
        CrestronString S;
        S  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
        
        
        __context__.SourceCodeLine = 468;
        UCMD__DOLLAR__  .UpdateValue ( Functions.Upper ( UCMD__DOLLAR__ )  ) ; 
        __context__.SourceCodeLine = 469;
        S  .UpdateValue ( Functions.Remove ( "FONTSCALE=" , UCMD__DOLLAR__ )  ) ; 
        __context__.SourceCodeLine = 470;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (S == ""))  ) ) 
            {
            __context__.SourceCodeLine = 470;
            Functions.TerminateEvent (); 
            }
        
        __context__.SourceCodeLine = 471;
        N = (uint) ( Functions.Atoi( UCMD__DOLLAR__ ) ) ; 
        __context__.SourceCodeLine = 472;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( N > 100 ))  ) ) 
            {
            __context__.SourceCodeLine = 472;
            N = (uint) ( 100 ) ; 
            }
        
        __context__.SourceCodeLine = 473;
        N = (uint) ( ((N * 65535) / 100) ) ; 
        __context__.SourceCodeLine = 474;
        FONTSCALE_OUT__POUND__  .Value = (ushort) ( Functions.LowWord( (uint)( N ) ) ) ; 
        __context__.SourceCodeLine = 475;
        _SplusNVRAM.SAVE_FONTSCALE = (ushort) ( Functions.LowWord( (uint)( N ) ) ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FONTSCALE__POUND___OnChange_7 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 480;
        Print( "BT Fontscale is now {0:d}\r\n", (ushort)FONTSCALE__POUND__  .UshortValue) ; 
        
        
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
    CURR_DUR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
    _SplusNVRAM.S_HISTORY  = new CrestronString[ 11 ];
    for( uint i = 0; i < 11; i++ )
        _SplusNVRAM.S_HISTORY [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 25, this );
    NOW  = new TIME_ENTRY( this, true );
    NOW .PopulateCustomAttributeList( false );
    _SplusNVRAM.TE  = new TIME_ENTRY[ 11 ];
    for( uint i = 0; i < 11; i++ )
    {
        _SplusNVRAM.TE [i] = new TIME_ENTRY( this, false );
        
    }
    
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
    
    TOD_PM = new Crestron.Logos.SplusObjects.DigitalOutput( TOD_PM__DigitalOutput__, this );
    m_DigitalOutputList.Add( TOD_PM__DigitalOutput__, TOD_PM );
    
    ARG1 = new Crestron.Logos.SplusObjects.AnalogInput( ARG1__AnalogSerialInput__, this );
    m_AnalogInputList.Add( ARG1__AnalogSerialInput__, ARG1 );
    
    ARG2 = new Crestron.Logos.SplusObjects.AnalogInput( ARG2__AnalogSerialInput__, this );
    m_AnalogInputList.Add( ARG2__AnalogSerialInput__, ARG2 );
    
    FONTSCALE__POUND__ = new Crestron.Logos.SplusObjects.AnalogInput( FONTSCALE__POUND____AnalogSerialInput__, this );
    m_AnalogInputList.Add( FONTSCALE__POUND____AnalogSerialInput__, FONTSCALE__POUND__ );
    
    SELECT_ITEM__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( SELECT_ITEM__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( SELECT_ITEM__POUND____AnalogSerialOutput__, SELECT_ITEM__POUND__ );
    
    DESELECT_ITEM__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( DESELECT_ITEM__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( DESELECT_ITEM__POUND____AnalogSerialOutput__, DESELECT_ITEM__POUND__ );
    
    FONTSCALE_OUT__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( FONTSCALE_OUT__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( FONTSCALE_OUT__POUND____AnalogSerialOutput__, FONTSCALE_OUT__POUND__ );
    
    UCMD__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( UCMD__DOLLAR____AnalogSerialInput__, 255, this );
    m_StringInputList.Add( UCMD__DOLLAR____AnalogSerialInput__, UCMD__DOLLAR__ );
    
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
    
    TOD_12H__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( TOD_12H__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( TOD_12H__DOLLAR____AnalogSerialOutput__, TOD_12H__DOLLAR__ );
    
    TOD_24H__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( TOD_24H__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( TOD_24H__DOLLAR____AnalogSerialOutput__, TOD_24H__DOLLAR__ );
    
    HISTORY__DOLLAR__ = new InOutArray<StringOutput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        HISTORY__DOLLAR__[i+1] = new Crestron.Logos.SplusObjects.StringOutput( HISTORY__DOLLAR____AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( HISTORY__DOLLAR____AnalogSerialOutput__ + i, HISTORY__DOLLAR__[i+1] );
    }
    
    
    SLEEP_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( SLEEP_PR_OnPush_0, false ) );
    WAKE_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( WAKE_PR_OnPush_1, false ) );
    OFF_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( OFF_PR_OnPush_2, false ) );
    TIME_INC_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( TIME_INC_PR_OnPush_3, false ) );
    TIME_DEC_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( TIME_DEC_PR_OnPush_4, false ) );
    ARG1.OnAnalogChange.Add( new InputChangeHandlerWrapper( ARG1_OnChange_5, false ) );
    ARG2.OnAnalogChange.Add( new InputChangeHandlerWrapper( ARG1_OnChange_5, false ) );
    UCMD__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( UCMD__DOLLAR___OnChange_6, false ) );
    FONTSCALE__POUND__.OnAnalogChange.Add( new InputChangeHandlerWrapper( FONTSCALE__POUND___OnChange_7, false ) );
    
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
const uint FONTSCALE__POUND____AnalogSerialInput__ = 2;
const uint UCMD__DOLLAR____AnalogSerialInput__ = 3;
const uint TOD_PM__DigitalOutput__ = 0;
const uint SINCE_TITLE__DOLLAR____AnalogSerialOutput__ = 0;
const uint SINCE_TIME__DOLLAR____AnalogSerialOutput__ = 1;
const uint DURATION_TITLE__DOLLAR____AnalogSerialOutput__ = 2;
const uint DURATION_H__DOLLAR____AnalogSerialOutput__ = 3;
const uint DURATION_UNIT__DOLLAR____AnalogSerialOutput__ = 4;
const uint DATE__DOLLAR____AnalogSerialOutput__ = 5;
const uint TOD_12H__DOLLAR____AnalogSerialOutput__ = 6;
const uint TOD_24H__DOLLAR____AnalogSerialOutput__ = 7;
const uint SELECT_ITEM__POUND____AnalogSerialOutput__ = 8;
const uint DESELECT_ITEM__POUND____AnalogSerialOutput__ = 9;
const uint FONTSCALE_OUT__POUND____AnalogSerialOutput__ = 10;
const uint HISTORY__DOLLAR____AnalogSerialOutput__ = 11;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    [SplusStructAttribute(0, false, true)]
            public ushort SAVE_SELECT = 0;
            [SplusStructAttribute(1, false, true)]
            public ushort I_CURR = 0;
            [SplusStructAttribute(2, false, true)]
            public ushort SAVE_FONTSCALE = 0;
            [SplusStructAttribute(3, false, true)]
            public CrestronString [] S_HISTORY;
            [SplusStructAttribute(4, true, true)]
            public TIME_ENTRY [] TE;
            
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

[SplusStructAttribute(-1, true, false)]
public class TIME_ENTRY : SplusStructureBase
{

    [SplusStructAttribute(0, false, false)]
    public ushort  H = 0;
    
    [SplusStructAttribute(1, false, false)]
    public ushort  M = 0;
    
    [SplusStructAttribute(2, false, false)]
    public ushort  S = 0;
    
    [SplusStructAttribute(3, false, false)]
    public CrestronString  DUR;
    
    [SplusStructAttribute(4, false, false)]
    public CrestronString  TYPE;
    
    
    public TIME_ENTRY( SplusObject __caller__, bool bIsStructureVolatile ) : base ( __caller__, bIsStructureVolatile )
    {
        DUR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, Owner );
        TYPE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, Owner );
        
        
    }
    
}

}
