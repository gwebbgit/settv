#ifndef __S2_HH_UI_SYMNET_H__
#define __S2_HH_UI_SYMNET_H__




/*
* Constructor and Destructor
*/

/*
* DIGITAL_INPUT
*/
#define __S2_hh_ui_symnet_REUPDATE_PR_DIG_INPUT 0
#define __S2_hh_ui_symnet_BROWSE_FWD_PR_DIG_INPUT 1
#define __S2_hh_ui_symnet_BROWSE_REV_PR_DIG_INPUT 2
#define __S2_hh_ui_symnet_BROWSE_ENTER_PR_DIG_INPUT 3


/*
* ANALOG_INPUT
*/




/*
* DIGITAL_OUTPUT
*/


/*
* ANALOG_OUTPUT
*/
#define __S2_hh_ui_symnet_BROWSE_CH_ANALOG_OUTPUT 0

#define __S2_hh_ui_symnet_UI_BROWSE_TEXT_STRING_OUTPUT 1


/*
* Direct Socket Variables
*/




/*
* INTEGER_PARAMETER
*/
/*
* SIGNED_INTEGER_PARAMETER
*/
/*
* LONG_INTEGER_PARAMETER
*/
/*
* SIGNED_LONG_INTEGER_PARAMETER
*/
/*
* INTEGER_PARAMETER
*/
/*
* SIGNED_INTEGER_PARAMETER
*/
/*
* LONG_INTEGER_PARAMETER
*/
/*
* SIGNED_LONG_INTEGER_PARAMETER
*/
/*
* STRING_PARAMETER
*/
#define __S2_hh_ui_symnet_SOURCE_NAME_STRING_PARAMETER 10
#define __S2_hh_ui_symnet_SOURCE_NAME_ARRAY_NUM_ELEMS 4
#define __S2_hh_ui_symnet_SOURCE_NAME_ARRAY_NUM_CHARS 200
CREATE_STRING_ARRAY( S2_hh_ui_symnet, __SOURCE_NAME, __S2_hh_ui_symnet_SOURCE_NAME_ARRAY_NUM_ELEMS, __S2_hh_ui_symnet_SOURCE_NAME_ARRAY_NUM_CHARS );


/*
* INTEGER
*/


/*
* LONG_INTEGER
*/


/*
* SIGNED_INTEGER
*/


/*
* SIGNED_LONG_INTEGER
*/


/*
* STRING
*/

/*
* STRUCTURE
*/

START_GLOBAL_VAR_STRUCT( S2_hh_ui_symnet )
{
   void* InstancePtr;
   struct GenericOutputString_s sGenericOutStr;
   unsigned short LastModifiedArrayIndex;

   DECLARE_STRING_ARRAY( S2_hh_ui_symnet, __SOURCE_NAME );
};

START_NVRAM_VAR_STRUCT( S2_hh_ui_symnet )
{
};



#endif //__S2_HH_UI_SYMNET_H__

