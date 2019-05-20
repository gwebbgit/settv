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

namespace UserModule_DJ_SYMNET
{
    public class UserModuleClass_DJ_SYMNET : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        InOutArray<StringParameter> CC_NUM;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> ON_TPR;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> CUE_TPR;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> ON_FB_PULSE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> OFF_FB_PULSE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> CUE_ON_FB_PULSE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> CUE_OFF_FB_PULSE;
        Crestron.Logos.SplusObjects.DigitalOutput TX_BUSY;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> ON_PULSE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> OFF_PULSE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> CUE_ON_PULSE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> CUE_OFF_PULSE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> ON_STATE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> CUE_STATE;
        InOutArray<Crestron.Logos.SplusObjects.AnalogInput> CC_SET;
        Crestron.Logos.SplusObjects.StringOutput TO_SYMNET__DOLLAR__;
        object ON_TPR_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                ushort I = 0;
                
                
                __context__.SourceCodeLine = 53;
                I = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
                __context__.SourceCodeLine = 54;
                if ( Functions.TestForTrue  ( ( ON_STATE[ I ] .Value)  ) ) 
                    { 
                    __context__.SourceCodeLine = 56;
                    Functions.Pulse ( 50, OFF_PULSE [ I] ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 59;
                    Functions.Pulse ( 50, ON_PULSE [ I] ) ; 
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object CUE_TPR_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 64;
            I = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
            __context__.SourceCodeLine = 65;
            if ( Functions.TestForTrue  ( ( CUE_STATE[ I ] .Value)  ) ) 
                { 
                __context__.SourceCodeLine = 67;
                Functions.Pulse ( 50, CUE_OFF_PULSE [ I] ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 70;
                Functions.Pulse ( 50, CUE_ON_PULSE [ I] ) ; 
                } 
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object ON_FB_PULSE_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort I = 0;
        
        
        __context__.SourceCodeLine = 77;
        I = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 78;
        ON_STATE [ I]  .Value = (ushort) ( 1 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object OFF_FB_PULSE_OnPush_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort I = 0;
        
        
        __context__.SourceCodeLine = 82;
        I = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 83;
        ON_STATE [ I]  .Value = (ushort) ( 0 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CUE_ON_FB_PULSE_OnPush_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort I = 0;
        
        
        __context__.SourceCodeLine = 87;
        I = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 88;
        CUE_STATE [ I]  .Value = (ushort) ( 1 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CUE_OFF_FB_PULSE_OnPush_5 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort I = 0;
        
        
        __context__.SourceCodeLine = 92;
        I = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 93;
        CUE_STATE [ I]  .Value = (ushort) ( 0 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CC_SET_OnChange_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort I = 0;
        
        
        __context__.SourceCodeLine = 98;
        I = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 99;
        MakeString ( TO_SYMNET__DOLLAR__ , "CSG {0} {1:d}\r", CC_NUM [ I ] , (ushort)CC_SET[ I ] .UshortValue) ; 
        __context__.SourceCodeLine = 100;
        Functions.Delay (  (int) ( 20 ) ) ; 
        
        
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
    
    ON_TPR = new InOutArray<DigitalInput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        ON_TPR[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( ON_TPR__DigitalInput__ + i, ON_TPR__DigitalInput__, this );
        m_DigitalInputList.Add( ON_TPR__DigitalInput__ + i, ON_TPR[i+1] );
    }
    
    CUE_TPR = new InOutArray<DigitalInput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        CUE_TPR[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( CUE_TPR__DigitalInput__ + i, CUE_TPR__DigitalInput__, this );
        m_DigitalInputList.Add( CUE_TPR__DigitalInput__ + i, CUE_TPR[i+1] );
    }
    
    ON_FB_PULSE = new InOutArray<DigitalInput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        ON_FB_PULSE[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( ON_FB_PULSE__DigitalInput__ + i, ON_FB_PULSE__DigitalInput__, this );
        m_DigitalInputList.Add( ON_FB_PULSE__DigitalInput__ + i, ON_FB_PULSE[i+1] );
    }
    
    OFF_FB_PULSE = new InOutArray<DigitalInput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        OFF_FB_PULSE[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( OFF_FB_PULSE__DigitalInput__ + i, OFF_FB_PULSE__DigitalInput__, this );
        m_DigitalInputList.Add( OFF_FB_PULSE__DigitalInput__ + i, OFF_FB_PULSE[i+1] );
    }
    
    CUE_ON_FB_PULSE = new InOutArray<DigitalInput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        CUE_ON_FB_PULSE[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( CUE_ON_FB_PULSE__DigitalInput__ + i, CUE_ON_FB_PULSE__DigitalInput__, this );
        m_DigitalInputList.Add( CUE_ON_FB_PULSE__DigitalInput__ + i, CUE_ON_FB_PULSE[i+1] );
    }
    
    CUE_OFF_FB_PULSE = new InOutArray<DigitalInput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        CUE_OFF_FB_PULSE[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( CUE_OFF_FB_PULSE__DigitalInput__ + i, CUE_OFF_FB_PULSE__DigitalInput__, this );
        m_DigitalInputList.Add( CUE_OFF_FB_PULSE__DigitalInput__ + i, CUE_OFF_FB_PULSE[i+1] );
    }
    
    TX_BUSY = new Crestron.Logos.SplusObjects.DigitalOutput( TX_BUSY__DigitalOutput__, this );
    m_DigitalOutputList.Add( TX_BUSY__DigitalOutput__, TX_BUSY );
    
    ON_PULSE = new InOutArray<DigitalOutput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        ON_PULSE[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( ON_PULSE__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( ON_PULSE__DigitalOutput__ + i, ON_PULSE[i+1] );
    }
    
    OFF_PULSE = new InOutArray<DigitalOutput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        OFF_PULSE[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( OFF_PULSE__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( OFF_PULSE__DigitalOutput__ + i, OFF_PULSE[i+1] );
    }
    
    CUE_ON_PULSE = new InOutArray<DigitalOutput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        CUE_ON_PULSE[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( CUE_ON_PULSE__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( CUE_ON_PULSE__DigitalOutput__ + i, CUE_ON_PULSE[i+1] );
    }
    
    CUE_OFF_PULSE = new InOutArray<DigitalOutput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        CUE_OFF_PULSE[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( CUE_OFF_PULSE__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( CUE_OFF_PULSE__DigitalOutput__ + i, CUE_OFF_PULSE[i+1] );
    }
    
    ON_STATE = new InOutArray<DigitalOutput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        ON_STATE[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( ON_STATE__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( ON_STATE__DigitalOutput__ + i, ON_STATE[i+1] );
    }
    
    CUE_STATE = new InOutArray<DigitalOutput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        CUE_STATE[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( CUE_STATE__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( CUE_STATE__DigitalOutput__ + i, CUE_STATE[i+1] );
    }
    
    CC_SET = new InOutArray<AnalogInput>( 100, this );
    for( uint i = 0; i < 100; i++ )
    {
        CC_SET[i+1] = new Crestron.Logos.SplusObjects.AnalogInput( CC_SET__AnalogSerialInput__ + i, CC_SET__AnalogSerialInput__, this );
        m_AnalogInputList.Add( CC_SET__AnalogSerialInput__ + i, CC_SET[i+1] );
    }
    
    TO_SYMNET__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( TO_SYMNET__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( TO_SYMNET__DOLLAR____AnalogSerialOutput__, TO_SYMNET__DOLLAR__ );
    
    CC_NUM = new InOutArray<StringParameter>( 100, this );
    for( uint i = 0; i < 100; i++ )
    {
        CC_NUM[i+1] = new StringParameter( CC_NUM__Parameter__ + i, CC_NUM__Parameter__, this );
        m_ParameterList.Add( CC_NUM__Parameter__ + i, CC_NUM[i+1] );
    }
    
    
    for( uint i = 0; i < 10; i++ )
        ON_TPR[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( ON_TPR_OnPush_0, false ) );
        
    for( uint i = 0; i < 10; i++ )
        CUE_TPR[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( CUE_TPR_OnPush_1, false ) );
        
    for( uint i = 0; i < 10; i++ )
        ON_FB_PULSE[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( ON_FB_PULSE_OnPush_2, false ) );
        
    for( uint i = 0; i < 10; i++ )
        OFF_FB_PULSE[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( OFF_FB_PULSE_OnPush_3, false ) );
        
    for( uint i = 0; i < 10; i++ )
        CUE_ON_FB_PULSE[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( CUE_ON_FB_PULSE_OnPush_4, false ) );
        
    for( uint i = 0; i < 10; i++ )
        CUE_OFF_FB_PULSE[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( CUE_OFF_FB_PULSE_OnPush_5, false ) );
        
    for( uint i = 0; i < 100; i++ )
        CC_SET[i+1].OnAnalogChange.Add( new InputChangeHandlerWrapper( CC_SET_OnChange_6, true ) );
        
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_DJ_SYMNET ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint CC_NUM__Parameter__ = 10;
const uint ON_TPR__DigitalInput__ = 0;
const uint CUE_TPR__DigitalInput__ = 10;
const uint ON_FB_PULSE__DigitalInput__ = 20;
const uint OFF_FB_PULSE__DigitalInput__ = 30;
const uint CUE_ON_FB_PULSE__DigitalInput__ = 40;
const uint CUE_OFF_FB_PULSE__DigitalInput__ = 50;
const uint TX_BUSY__DigitalOutput__ = 0;
const uint ON_PULSE__DigitalOutput__ = 1;
const uint OFF_PULSE__DigitalOutput__ = 11;
const uint CUE_ON_PULSE__DigitalOutput__ = 21;
const uint CUE_OFF_PULSE__DigitalOutput__ = 31;
const uint ON_STATE__DigitalOutput__ = 41;
const uint CUE_STATE__DigitalOutput__ = 51;
const uint CC_SET__AnalogSerialInput__ = 0;
const uint TO_SYMNET__DOLLAR____AnalogSerialOutput__ = 0;

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
