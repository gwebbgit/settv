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

namespace UserModule_SG_LIST_SELECTOR_DESELECTOR
{
    public class UserModuleClass_SG_LIST_SELECTOR_DESELECTOR : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        Crestron.Logos.SplusObjects.DigitalInput NEXT_PR;
        Crestron.Logos.SplusObjects.DigitalInput PREV_PR;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> SEL_PR;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> SEL_F;
        Crestron.Logos.SplusObjects.AnalogOutput SELECTED_ITEM;
        Crestron.Logos.SplusObjects.AnalogOutput DESELECTED_ITEM;
        private void UPDATE_F (  SplusExecutionContext __context__ ) 
            { 
            ushort I = 0;
            
            
            __context__.SourceCodeLine = 58;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)4; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 59;
                SEL_F [ I]  .Value = (ushort) ( Functions.BoolToInt (I == SELECTED_ITEM  .Value) ) ; 
                __context__.SourceCodeLine = 58;
                } 
            
            
            }
            
        object NEXT_PR_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 64;
                DESELECTED_ITEM  .Value = (ushort) ( SELECTED_ITEM  .Value ) ; 
                __context__.SourceCodeLine = 65;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( SELECTED_ITEM  .Value < 4 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 66;
                    SELECTED_ITEM  .Value = (ushort) ( (SELECTED_ITEM  .Value + 1) ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 68;
                    SELECTED_ITEM  .Value = (ushort) ( 1 ) ; 
                    } 
                
                __context__.SourceCodeLine = 70;
                UPDATE_F (  __context__  ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object PREV_PR_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 73;
            DESELECTED_ITEM  .Value = (ushort) ( SELECTED_ITEM  .Value ) ; 
            __context__.SourceCodeLine = 74;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( SELECTED_ITEM  .Value > 1 ))  ) ) 
                { 
                __context__.SourceCodeLine = 75;
                SELECTED_ITEM  .Value = (ushort) ( (SELECTED_ITEM  .Value - 1) ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 77;
                SELECTED_ITEM  .Value = (ushort) ( 4 ) ; 
                } 
            
            __context__.SourceCodeLine = 79;
            UPDATE_F (  __context__  ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object SEL_PR_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort I = 0;
        
        
        __context__.SourceCodeLine = 85;
        I = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 86;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (I != SELECTED_ITEM  .Value))  ) ) 
            { 
            __context__.SourceCodeLine = 87;
            DESELECTED_ITEM  .Value = (ushort) ( SELECTED_ITEM  .Value ) ; 
            } 
        
        __context__.SourceCodeLine = 89;
        SELECTED_ITEM  .Value = (ushort) ( I ) ; 
        __context__.SourceCodeLine = 90;
        UPDATE_F (  __context__  ) ; 
        
        
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
    
    NEXT_PR = new Crestron.Logos.SplusObjects.DigitalInput( NEXT_PR__DigitalInput__, this );
    m_DigitalInputList.Add( NEXT_PR__DigitalInput__, NEXT_PR );
    
    PREV_PR = new Crestron.Logos.SplusObjects.DigitalInput( PREV_PR__DigitalInput__, this );
    m_DigitalInputList.Add( PREV_PR__DigitalInput__, PREV_PR );
    
    SEL_PR = new InOutArray<DigitalInput>( 4, this );
    for( uint i = 0; i < 4; i++ )
    {
        SEL_PR[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( SEL_PR__DigitalInput__ + i, SEL_PR__DigitalInput__, this );
        m_DigitalInputList.Add( SEL_PR__DigitalInput__ + i, SEL_PR[i+1] );
    }
    
    SEL_F = new InOutArray<DigitalOutput>( 4, this );
    for( uint i = 0; i < 4; i++ )
    {
        SEL_F[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( SEL_F__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( SEL_F__DigitalOutput__ + i, SEL_F[i+1] );
    }
    
    SELECTED_ITEM = new Crestron.Logos.SplusObjects.AnalogOutput( SELECTED_ITEM__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( SELECTED_ITEM__AnalogSerialOutput__, SELECTED_ITEM );
    
    DESELECTED_ITEM = new Crestron.Logos.SplusObjects.AnalogOutput( DESELECTED_ITEM__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( DESELECTED_ITEM__AnalogSerialOutput__, DESELECTED_ITEM );
    
    
    NEXT_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( NEXT_PR_OnPush_0, false ) );
    PREV_PR.OnDigitalPush.Add( new InputChangeHandlerWrapper( PREV_PR_OnPush_1, false ) );
    for( uint i = 0; i < 4; i++ )
        SEL_PR[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( SEL_PR_OnPush_2, false ) );
        
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SG_LIST_SELECTOR_DESELECTOR ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint NEXT_PR__DigitalInput__ = 0;
const uint PREV_PR__DigitalInput__ = 1;
const uint SEL_PR__DigitalInput__ = 2;
const uint SEL_F__DigitalOutput__ = 0;
const uint SELECTED_ITEM__AnalogSerialOutput__ = 0;
const uint DESELECTED_ITEM__AnalogSerialOutput__ = 1;

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
