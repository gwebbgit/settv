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
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> HISTORY__DOLLAR__;
        CrestronString CURR_DUR;
        TIME_ENTRY NOW;
        private void UPDATE_NOW (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 115;
            NOW . H = (ushort) ( Functions.GetHourNum() ) ; 
            __context__.SourceCodeLine = 116;
            NOW . M = (ushort) ( Functions.GetMinutesNum() ) ; 
            __context__.SourceCodeLine = 117;
            NOW . S = (ushort) ( Functions.GetSecondsNum() ) ; 
            
            }
            
        private void REMAKE_HISTORY_STRING (  SplusExecutionContext __context__ ) 
            { 
            ushort I = 0;
            
            CrestronString COLOR;
            COLOR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 6, this );
            
            
            __context__.SourceCodeLine = 127;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)_SplusNVRAM.I_CURR; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 130;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.TE[ I ].TYPE == "WAKE"))  ) ) 
                    { 
                    __context__.SourceCodeLine = 132;
                    COLOR  .UpdateValue ( "a0ffa0"  ) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 134;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.TE[ I ].TYPE == "SLEEP"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 136;
                        COLOR  .UpdateValue ( "7f7fff"  ) ; 
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 138;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.TE[ I ].TYPE == "OFF"))  ) ) 
                            { 
                            __context__.SourceCodeLine = 140;
                            COLOR  .UpdateValue ( "7f7f7f"  ) ; 
                            } 
                        
                        else 
                            { 
                            __context__.SourceCodeLine = 143;
                            COLOR  .UpdateValue ( "ffffff"  ) ; 
                            } 
                        
                        }
                    
                    }
                
                __context__.SourceCodeLine = 149;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (I == _SplusNVRAM.I_CURR) ) || Functions.TestForTrue ( Functions.BoolToInt (I == 10) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 151;
                    MakeString ( HISTORY__DOLLAR__ [ I] , "<font color=\"#{0}\">{1}: {2:d}:{3:d2} ({4}...</font>", COLOR , _SplusNVRAM.TE [ I] . TYPE , (ushort)_SplusNVRAM.TE[ I ].H, (ushort)_SplusNVRAM.TE[ I ].M, CURR_DUR ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 154;
                    MakeString ( HISTORY__DOLLAR__ [ I] , "<font color=\"#{0}\">{1}: {2:d}:{3:d2} ({4})</font>", COLOR , _SplusNVRAM.TE [ I] . TYPE , (ushort)_SplusNVRAM.TE[ I ].H, (ushort)_SplusNVRAM.TE[ I ].M, _SplusNVRAM.TE [ I] . DUR ) ; 
                    } 
                
                __context__.SourceCodeLine = 127;
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
            
            
            __context__.SourceCodeLine = 172;
            /* Trace( "calc_duration()...\r\n") */ ; 
            __context__.SourceCodeLine = 175;
            LT2 . H = (ushort) ( TE2.H ) ; 
            __context__.SourceCodeLine = 176;
            LT2 . M = (ushort) ( TE2.M ) ; 
            __context__.SourceCodeLine = 177;
            LT2 . S = (ushort) ( TE2.S ) ; 
            __context__.SourceCodeLine = 178;
            LT2 . DUR  .UpdateValue ( TE2 . DUR  ) ; 
            __context__.SourceCodeLine = 179;
            LT2 . TYPE  .UpdateValue ( TE2 . TYPE  ) ; 
            __context__.SourceCodeLine = 182;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LT2.S < TE1.S ))  ) ) 
                { 
                __context__.SourceCodeLine = 183;
                LT2 . S = (ushort) ( (LT2.S + 60) ) ; 
                __context__.SourceCodeLine = 184;
                LT2 . M = (ushort) ( (LT2.M - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 186;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LT2.M < TE1.M ))  ) ) 
                { 
                __context__.SourceCodeLine = 187;
                LT2 . M = (ushort) ( (LT2.M + 60) ) ; 
                __context__.SourceCodeLine = 188;
                LT2 . H = (ushort) ( (LT2.H - 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 190;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LT2.H < TE1.H ))  ) ) 
                { 
                __context__.SourceCodeLine = 191;
                LT2 . H = (ushort) ( (LT2.H + 24) ) ; 
                } 
            
            __context__.SourceCodeLine = 193;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( LT2.M < TE1.M ))  ) ) 
                { 
                __context__.SourceCodeLine = 195;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (TE1.M - LT2.M) < 12 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 197;
                    DUR_H = (short) ( 0 ) ; 
                    __context__.SourceCodeLine = 198;
                    DUR_DEC = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 199;
                    /* Trace( ">>>dur_h within 12 hours earlier... FIXME\r\n") */ ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 205;
                    /* Trace( ">>>dur_h else within 12 hours later, must be after midnight; add 24 to te2.h... FIXME\r\n") */ ; 
                    } 
                
                } 
            
            __context__.SourceCodeLine = 210;
            DUR_H = (short) ( (LT2.H - TE1.H) ) ; 
            __context__.SourceCodeLine = 211;
            DUR_DEC = (ushort) ( ((LT2.M - TE1.M) / 6) ) ; 
            __context__.SourceCodeLine = 212;
            /* Trace( "\tlt2.m={0:d}:{1:d}, te1.m={2:d}:{3:d}  dur_h={4:d}:{5:d}  dur_dec={6:d}\r\n", (ushort)TE2.M, (short)TE2.M, (ushort)TE1.M, (short)TE1.M, (ushort)DUR_H, (short)DUR_H, (ushort)DUR_DEC) */ ; 
            __context__.SourceCodeLine = 214;
            MakeString ( RET , "{0:d}.{1:d}", (short)DUR_H, (ushort)DUR_DEC) ; 
            __context__.SourceCodeLine = 215;
            return ( RET ) ; 
            
            }
            
        private void ADD_START_HISTORY (  SplusExecutionContext __context__, CrestronString NEWTYPE ) 
            { 
            ushort I = 0;
            
            CrestronString PREV_DUR;
            PREV_DUR  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
            
            
            __context__.SourceCodeLine = 228;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.I_CURR >= 10 ))  ) ) 
                { 
                __context__.SourceCodeLine = 230;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 2 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)10; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 231;
                    _SplusNVRAM.TE [ (I - 1)] . H = (ushort) ( _SplusNVRAM.TE[ I ].H ) ; 
                    __context__.SourceCodeLine = 232;
                    _SplusNVRAM.TE [ (I - 1)] . M = (ushort) ( _SplusNVRAM.TE[ I ].M ) ; 
                    __context__.SourceCodeLine = 233;
                    _SplusNVRAM.TE [ (I - 1)] . S = (ushort) ( _SplusNVRAM.TE[ I ].S ) ; 
                    __context__.SourceCodeLine = 234;
                    _SplusNVRAM.TE [ (I - 1)] . DUR  .UpdateValue ( _SplusNVRAM.TE [ I] . DUR  ) ; 
                    __context__.SourceCodeLine = 235;
                    _SplusNVRAM.TE [ (I - 1)] . TYPE  .UpdateValue ( _SplusNVRAM.TE [ I] . TYPE  ) ; 
                    __context__.SourceCodeLine = 230;
                    } 
                
                __context__.SourceCodeLine = 237;
                _SplusNVRAM.TE [ 10] . TYPE  .UpdateValue ( ""  ) ; 
                __context__.SourceCodeLine = 238;
                _SplusNVRAM.I_CURR = (ushort) ( 10 ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 241;
                _SplusNVRAM.I_CURR = (ushort) ( (_SplusNVRAM.I_CURR + 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 244;
            UPDATE_NOW (  __context__  ) ; 
            __context__.SourceCodeLine = 247;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . H = (ushort) ( NOW.H ) ; 
            __context__.SourceCodeLine = 248;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . M = (ushort) ( NOW.M ) ; 
            __context__.SourceCodeLine = 249;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . S = (ushort) ( NOW.S ) ; 
            __context__.SourceCodeLine = 250;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . TYPE  .UpdateValue ( NEWTYPE  ) ; 
            __context__.SourceCodeLine = 251;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . DUR  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 252;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].H, (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M) ; 
            __context__.SourceCodeLine = 253;
            DURATION_TITLE__DOLLAR__  .UpdateValue ( "DURATION"  ) ; 
            __context__.SourceCodeLine = 254;
            DURATION_H__DOLLAR__  .UpdateValue ( "0"  ) ; 
            __context__.SourceCodeLine = 255;
            DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
            __context__.SourceCodeLine = 258;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.I_CURR > 1 ))  ) ) 
                { 
                __context__.SourceCodeLine = 259;
                PREV_DUR  .UpdateValue ( CALC_DURATION (  __context__ , _SplusNVRAM.TE[ (_SplusNVRAM.I_CURR - 1) ], _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ])  ) ; 
                __context__.SourceCodeLine = 260;
                _SplusNVRAM.TE [ (_SplusNVRAM.I_CURR - 1)] . DUR  .UpdateValue ( PREV_DUR  ) ; 
                } 
            
            __context__.SourceCodeLine = 264;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void UPDATE_TIMES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 272;
            UPDATE_NOW (  __context__  ) ; 
            __context__.SourceCodeLine = 273;
            /* Trace( "update_times()...\r\n\ti_curr={0:d}\r\n", (ushort)_SplusNVRAM.I_CURR) */ ; 
            __context__.SourceCodeLine = 276;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (NOW.H == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 278;
                MakeString ( TOD_12H__DOLLAR__ , "{0:d}:{1:d2}<font size=\"80\">AM</font>", (ushort)12, (ushort)NOW.M) ; 
                __context__.SourceCodeLine = 279;
                TOD_PM  .Value = (ushort) ( 0 ) ; 
                } 
            
            else 
                {
                __context__.SourceCodeLine = 280;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NOW.H < 12 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 282;
                    MakeString ( TOD_12H__DOLLAR__ , "{0:d}:{1:d2}<font size=\"80\">AM</font>", (ushort)NOW.H, (ushort)NOW.M) ; 
                    __context__.SourceCodeLine = 283;
                    TOD_PM  .Value = (ushort) ( 0 ) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 284;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (NOW.H == 12))  ) ) 
                        { 
                        __context__.SourceCodeLine = 286;
                        MakeString ( TOD_12H__DOLLAR__ , "{0:d}:{1:d2}<font size=\"80\">PM</font>", (ushort)12, (ushort)NOW.M) ; 
                        __context__.SourceCodeLine = 287;
                        TOD_PM  .Value = (ushort) ( 1 ) ; 
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 288;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NOW.H > 12 ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 290;
                            MakeString ( TOD_12H__DOLLAR__ , "{0:d}:{1:d2}<font size=\"80\">PM</font>", (ushort)(NOW.H - 12), (ushort)NOW.M) ; 
                            __context__.SourceCodeLine = 291;
                            TOD_PM  .Value = (ushort) ( 1 ) ; 
                            } 
                        
                        }
                    
                    }
                
                }
            
            __context__.SourceCodeLine = 293;
            MakeString ( TOD_24H__DOLLAR__ , "{0:d}:{1:d2}", (ushort)NOW.H, (ushort)NOW.M) ; 
            __context__.SourceCodeLine = 294;
            MakeString ( DATE__DOLLAR__ , "{0} {1} {2:d}, {3:d}", Functions.Left ( Functions.Day ( ) ,  (int) ( 3 ) ) , Functions.Left ( Functions.Month ( ) ,  (int) ( 3 ) ) , (ushort)Functions.GetDateNum(), (ushort)Functions.GetYearNum()) ; 
            __context__.SourceCodeLine = 298;
            CURR_DUR  .UpdateValue ( CALC_DURATION (  __context__ , _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ], NOW)  ) ; 
            __context__.SourceCodeLine = 299;
            /* Trace( "\tCurr_Dur= {{{0}}}\r\n", CURR_DUR ) */ ; 
            __context__.SourceCodeLine = 302;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 1) ) || Functions.TestForTrue ( Functions.BoolToInt (SELECT_ITEM__POUND__  .Value == 2) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 304;
                DURATION_H__DOLLAR__  .UpdateValue ( CURR_DUR  ) ; 
                __context__.SourceCodeLine = 305;
                DURATION_UNIT__DOLLAR__  .UpdateValue ( "hr"  ) ; 
                } 
            
            __context__.SourceCodeLine = 309;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        private void SELECT_ITEM (  SplusExecutionContext __context__, ushort SEL ) 
            { 
            
            __context__.SourceCodeLine = 313;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 1))  ) ) 
                {
                __context__.SourceCodeLine = 313;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 314;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 2))  ) ) 
                {
                __context__.SourceCodeLine = 314;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 2 ) ; 
                }
            
            __context__.SourceCodeLine = 315;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SEL != 3))  ) ) 
                {
                __context__.SourceCodeLine = 315;
                DESELECT_ITEM__POUND__  .Value = (ushort) ( 3 ) ; 
                }
            
            __context__.SourceCodeLine = 316;
            SELECT_ITEM__POUND__  .Value = (ushort) ( SEL ) ; 
            __context__.SourceCodeLine = 317;
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
            
            
            __context__.SourceCodeLine = 332;
            SH = (ushort) ( _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].H ) ; 
            __context__.SourceCodeLine = 333;
            SM = (ushort) ( _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M ) ; 
            __context__.SourceCodeLine = 334;
            SS = (ushort) ( _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].S ) ; 
            __context__.SourceCodeLine = 337;
            M = (short) ( (_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M + INC) ) ; 
            __context__.SourceCodeLine = 338;
            /* Trace( "inc_time({0:d}) m={1:d} sh={2:d}, sm={3:d},  m S/ 60={4:d}  m%60={5:d} m%60={6:d}\r\n", (short)INC, (short)M, (ushort)SH, (ushort)SM, (ushort)Divide( M , 60 ), (ushort)Mod( M , 60 ), (short)Mod( M , 60 )) */ ; 
            __context__.SourceCodeLine = 341;
            SH = (ushort) ( (SH + Divide( M , 60 )) ) ; 
            __context__.SourceCodeLine = 342;
            SH = (ushort) ( Mod( SH , 24 ) ) ; 
            __context__.SourceCodeLine = 343;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( M < 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 344;
                /* Trace( "\t\tm=m+60 (m={0:d}:{1:d})\r\n", (ushort)M, (short)M) */ ; 
                __context__.SourceCodeLine = 345;
                M = (short) ( (M + 60) ) ; 
                __context__.SourceCodeLine = 346;
                SH = (ushort) ( (SH - 1) ) ; 
                __context__.SourceCodeLine = 347;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( SH < 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 347;
                    SH = (ushort) ( 23 ) ; 
                    }
                
                __context__.SourceCodeLine = 348;
                /* Trace( "\t\t\tsh <- {0:d}:{1:d}\r\n", (ushort)SH, (short)SH) */ ; 
                __context__.SourceCodeLine = 343;
                } 
            
            __context__.SourceCodeLine = 350;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 351;
            /* Trace( "\t\tm %60 = ({0:d}:{1:d})\r\n", (ushort)M, (short)M) */ ; 
            __context__.SourceCodeLine = 352;
            SM = (ushort) ( M ) ; 
            __context__.SourceCodeLine = 353;
            /* Trace( "\tnewh={0:d} newm={1:d}, m={2:d} m={3:d}\r\n", (ushort)SH, (ushort)SM, (ushort)M, (short)M) */ ; 
            __context__.SourceCodeLine = 354;
            M = (short) ( Mod( M , 60 ) ) ; 
            __context__.SourceCodeLine = 355;
            /* Trace( "\tm={0:d} m={1:d}\r\n", (ushort)M, (short)M) */ ; 
            __context__.SourceCodeLine = 358;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . H = (ushort) ( SH ) ; 
            __context__.SourceCodeLine = 359;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . M = (ushort) ( SM ) ; 
            __context__.SourceCodeLine = 360;
            _SplusNVRAM.TE [ _SplusNVRAM.I_CURR] . S = (ushort) ( SS ) ; 
            __context__.SourceCodeLine = 363;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.I_CURR > 1 ))  ) ) 
                { 
                __context__.SourceCodeLine = 364;
                PREV_DUR  .UpdateValue ( CALC_DURATION (  __context__ , _SplusNVRAM.TE[ (_SplusNVRAM.I_CURR - 1) ], _SplusNVRAM.TE[ _SplusNVRAM.I_CURR ])  ) ; 
                __context__.SourceCodeLine = 365;
                _SplusNVRAM.TE [ (_SplusNVRAM.I_CURR - 1)] . DUR  .UpdateValue ( PREV_DUR  ) ; 
                __context__.SourceCodeLine = 366;
                /* Trace( "\tupdating TE[i_curr-1 ({0:d})].dur = {{{1}}}\r\n", (ushort)(_SplusNVRAM.I_CURR - 1), PREV_DUR ) */ ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 368;
                /* Trace( "\tNOT updating TE[i_curr-1 ({0:d})].dur\r\n", (ushort)(_SplusNVRAM.I_CURR - 1)) */ ; 
                } 
            
            __context__.SourceCodeLine = 372;
            MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)SH, (ushort)SM) ; 
            __context__.SourceCodeLine = 373;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 376;
            REMAKE_HISTORY_STRING (  __context__  ) ; 
            
            }
            
        object SLEEP_PR_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 382;
                SELECT_ITEM (  __context__ , (ushort)( 1 )) ; 
                __context__.SourceCodeLine = 383;
                SINCE_TITLE__DOLLAR__  .UpdateValue ( "SLEEPING SINCE"  ) ; 
                __context__.SourceCodeLine = 384;
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
            
            __context__.SourceCodeLine = 388;
            SELECT_ITEM (  __context__ , (ushort)( 2 )) ; 
            __context__.SourceCodeLine = 389;
            SINCE_TITLE__DOLLAR__  .UpdateValue ( "AWAKE SINCE"  ) ; 
            __context__.SourceCodeLine = 390;
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
        
        __context__.SourceCodeLine = 394;
        SELECT_ITEM (  __context__ , (ushort)( 3 )) ; 
        __context__.SourceCodeLine = 395;
        SINCE_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 396;
        SINCE_TIME__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 397;
        DURATION_TITLE__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 398;
        DURATION_H__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 399;
        DURATION_UNIT__DOLLAR__  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 400;
        ADD_START_HISTORY (  __context__ , "OFF") ; 
        __context__.SourceCodeLine = 401;
        Functions.Delay (  (int) ( 100 ) ) ; 
        __context__.SourceCodeLine = 402;
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
        
        __context__.SourceCodeLine = 405;
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
        
        __context__.SourceCodeLine = 409;
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
        
        __context__.SourceCodeLine = 415;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 416;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( _SplusNVRAM.SAVE_SELECT > 0 ))  ) ) 
            { 
            __context__.SourceCodeLine = 417;
            SELECT_ITEM (  __context__ , (ushort)( _SplusNVRAM.SAVE_SELECT )) ; 
            __context__.SourceCodeLine = 418;
            switch ((int)_SplusNVRAM.SAVE_SELECT)
            
                { 
                case 1 : 
                
                    { 
                    __context__.SourceCodeLine = 420;
                    SINCE_TITLE__DOLLAR__  .UpdateValue ( "*SLEEPING SINCE"  ) ; 
                    __context__.SourceCodeLine = 421;
                    MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].H, (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M) ; 
                    __context__.SourceCodeLine = 422;
                    DURATION_TITLE__DOLLAR__  .UpdateValue ( "*DURATION"  ) ; 
                    __context__.SourceCodeLine = 423;
                    break ; 
                    } 
                
                goto case 2 ;
                case 2 : 
                
                    { 
                    __context__.SourceCodeLine = 426;
                    SINCE_TITLE__DOLLAR__  .UpdateValue ( "*AWAKE SINCE"  ) ; 
                    __context__.SourceCodeLine = 427;
                    MakeString ( SINCE_TIME__DOLLAR__ , "{0:d}:{1:d2}", (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].H, (ushort)_SplusNVRAM.TE[ _SplusNVRAM.I_CURR ].M) ; 
                    __context__.SourceCodeLine = 428;
                    DURATION_TITLE__DOLLAR__  .UpdateValue ( "*DURATION"  ) ; 
                    __context__.SourceCodeLine = 429;
                    break ; 
                    } 
                
                break;
                } 
                
            
            } 
        
        __context__.SourceCodeLine = 435;
        while ( Functions.TestForTrue  ( ( 1)  ) ) 
            { 
            __context__.SourceCodeLine = 436;
            UPDATE_TIMES (  __context__  ) ; 
            __context__.SourceCodeLine = 437;
            Functions.Delay (  (int) ( 200 ) ) ; 
            __context__.SourceCodeLine = 435;
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
        
        
        __context__.SourceCodeLine = 444;
        R1 = (short) ( Mod( ARG1  .ShortValue , ARG2  .ShortValue ) ) ; 
        __context__.SourceCodeLine = 445;
        R2 = (ushort) ( Mod( ARG1  .UshortValue , ARG2  .UshortValue ) ) ; 
        __context__.SourceCodeLine = 446;
        /* Trace( "({0:d}:{1:d}) %({2:d}:{3:d}) = ({4:d}:{5:d})({6:d}:{7:d})\r\n", (ushort)ARG1  .UshortValue, (short)ARG1  .UshortValue, (ushort)ARG2  .UshortValue, (short)ARG2  .UshortValue, (ushort)R1, (short)R1, (ushort)R2, (short)R2) */ ; 
        
        
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
    
    SELECT_ITEM__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( SELECT_ITEM__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( SELECT_ITEM__POUND____AnalogSerialOutput__, SELECT_ITEM__POUND__ );
    
    DESELECT_ITEM__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( DESELECT_ITEM__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( DESELECT_ITEM__POUND____AnalogSerialOutput__, DESELECT_ITEM__POUND__ );
    
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
const uint HISTORY__DOLLAR____AnalogSerialOutput__ = 10;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    [SplusStructAttribute(0, false, true)]
            public ushort SAVE_SELECT = 0;
            [SplusStructAttribute(1, false, true)]
            public ushort I_CURR = 0;
            [SplusStructAttribute(2, false, true)]
            public CrestronString [] S_HISTORY;
            [SplusStructAttribute(3, true, true)]
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
