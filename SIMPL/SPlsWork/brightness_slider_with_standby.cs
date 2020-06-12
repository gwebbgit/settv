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

namespace UserModule_BRIGHTNESS_SLIDER_WITH_STANDBY
{
    public class UserModuleClass_BRIGHTNESS_SLIDER_WITH_STANDBY : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        Crestron.Logos.SplusObjects.DigitalInput PRESS;
        Crestron.Logos.SplusObjects.DigitalInput BACKLIGHT_IS_ONF;
        Crestron.Logos.SplusObjects.DigitalOutput BACKLIGHT_ON;
        Crestron.Logos.SplusObjects.DigitalOutput BACKLIGHT_OFF;
        Crestron.Logos.SplusObjects.AnalogInput SLIDER;
        Crestron.Logos.SplusObjects.AnalogInput BRIGHTNESSF__POUND__;
        ushort SAVE_BRIGHTNESS__POUND__ = 0;
        object PRESS_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 58;
                SAVE_BRIGHTNESS__POUND__ = (ushort) ( BRIGHTNESSF__POUND__  .UshortValue ) ; 
                __context__.SourceCodeLine = 59;
                Trace( "Setting Save_Brightness to {0:d}\r\n", (ushort)SAVE_BRIGHTNESS__POUND__) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object SLIDER_OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 63;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( SLIDER  .UshortValue < 10000 ))  ) ) 
                { 
                __context__.SourceCodeLine = 64;
                if ( Functions.TestForTrue  ( ( BACKLIGHT_IS_ONF  .Value)  ) ) 
                    { 
                    __context__.SourceCodeLine = 66;
                    Functions.Pulse ( 10, BACKLIGHT_OFF ) ; 
                    __context__.SourceCodeLine = 67;
                    Trace( "NEED TO SET RESTORE BRIGHTNESS TO {0:d}\r\n", (ushort)SAVE_BRIGHTNESS__POUND__) ; 
                    } 
                
                } 
            
            else 
                {
                __context__.SourceCodeLine = 69;
                if ( Functions.TestForTrue  ( ( Functions.Not( BACKLIGHT_IS_ONF  .Value ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 71;
                    Functions.Pulse ( 10, BACKLIGHT_ON ) ; 
                    __context__.SourceCodeLine = 72;
                    Trace( "TURNING ON BACKLIGHT\r\n") ; 
                    } 
                
                }
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    
    PRESS = new Crestron.Logos.SplusObjects.DigitalInput( PRESS__DigitalInput__, this );
    m_DigitalInputList.Add( PRESS__DigitalInput__, PRESS );
    
    BACKLIGHT_IS_ONF = new Crestron.Logos.SplusObjects.DigitalInput( BACKLIGHT_IS_ONF__DigitalInput__, this );
    m_DigitalInputList.Add( BACKLIGHT_IS_ONF__DigitalInput__, BACKLIGHT_IS_ONF );
    
    BACKLIGHT_ON = new Crestron.Logos.SplusObjects.DigitalOutput( BACKLIGHT_ON__DigitalOutput__, this );
    m_DigitalOutputList.Add( BACKLIGHT_ON__DigitalOutput__, BACKLIGHT_ON );
    
    BACKLIGHT_OFF = new Crestron.Logos.SplusObjects.DigitalOutput( BACKLIGHT_OFF__DigitalOutput__, this );
    m_DigitalOutputList.Add( BACKLIGHT_OFF__DigitalOutput__, BACKLIGHT_OFF );
    
    SLIDER = new Crestron.Logos.SplusObjects.AnalogInput( SLIDER__AnalogSerialInput__, this );
    m_AnalogInputList.Add( SLIDER__AnalogSerialInput__, SLIDER );
    
    BRIGHTNESSF__POUND__ = new Crestron.Logos.SplusObjects.AnalogInput( BRIGHTNESSF__POUND____AnalogSerialInput__, this );
    m_AnalogInputList.Add( BRIGHTNESSF__POUND____AnalogSerialInput__, BRIGHTNESSF__POUND__ );
    
    
    PRESS.OnDigitalPush.Add( new InputChangeHandlerWrapper( PRESS_OnPush_0, false ) );
    SLIDER.OnAnalogChange.Add( new InputChangeHandlerWrapper( SLIDER_OnChange_1, true ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_BRIGHTNESS_SLIDER_WITH_STANDBY ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint PRESS__DigitalInput__ = 0;
const uint BACKLIGHT_IS_ONF__DigitalInput__ = 1;
const uint BACKLIGHT_ON__DigitalOutput__ = 0;
const uint BACKLIGHT_OFF__DigitalOutput__ = 1;
const uint SLIDER__AnalogSerialInput__ = 0;
const uint BRIGHTNESSF__POUND____AnalogSerialInput__ = 1;

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
