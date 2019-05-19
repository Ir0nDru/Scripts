#include <MDR32F9Qx_uart.h> 
#include <MDR32F9Qx_port.h> 
#include <MDR32F9Qx_rst_clk.h> 
#include "mlt_lcd.h" 
#include "U_Uart.h" 
#include <MDR32F9Qx_adc.h> 

#define DELAY(T) for (i = T; i > 0; i--) 
#define KALIBR 1267 
int i; 



void ADCInit() // general ADC init 
{ 

RST_CLK_PCLKcmd(RST_CLK_PCLK_ADC, ENABLE); 

ADC_InitTypeDef ADC; 

ADC_StructInit(&ADC); 

ADC_Init(&ADC); 
} 
// ADC1 init 
void ADC1Init() 
{ 

ADCx_InitTypeDef ADC1; 

ADCx_StructInit(&ADC1); 

ADC1.ADC_ChannelNumber = ADC_CH_ADC7; 

ADC1_Init(&ADC1); 

ADC1_Cmd(ENABLE); 
} 

void PORTI(void){ 
PORT_InitTypeDef Nastroyka2; 

RST_CLK_PCLKcmd(RST_CLK_PCLK_PORTF, ENABLE); 

Nastroyka2.PORT_Pin = PORT_Pin_5 | PORT_Pin_6 | PORT_Pin_4 | PORT_Pin_3; 
Nastroyka2.PORT_OE = PORT_OE_OUT; 
Nastroyka2.PORT_PULL_UP = PORT_PULL_UP_OFF; 
Nastroyka2.PORT_PULL_DOWN = PORT_PULL_DOWN_OFF; 
Nastroyka2.PORT_PD_SHM = PORT_PD_SHM_OFF; 
Nastroyka2.PORT_PD = PORT_PD_DRIVER; 
Nastroyka2.PORT_GFEN = PORT_GFEN_OFF; 
Nastroyka2.PORT_FUNC = PORT_FUNC_PORT; 
Nastroyka2.PORT_SPEED = PORT_SPEED_SLOW; 
Nastroyka2.PORT_MODE = PORT_MODE_DIGITAL; 

PORT_Init(MDR_PORTF, &Nastroyka2); 

} 


void LedInit(void){ 
PORT_InitTypeDef Nastroyka1; 

RST_CLK_PCLKcmd(RST_CLK_PCLK_PORTC, ENABLE); 

Nastroyka1.PORT_Pin = PORT_Pin_0; 
Nastroyka1.PORT_OE = PORT_OE_OUT; 
Nastroyka1.PORT_PULL_UP = PORT_PULL_UP_OFF; 
Nastroyka1.PORT_PULL_DOWN = PORT_PULL_DOWN_OFF; 
Nastroyka1.PORT_PD_SHM = PORT_PD_SHM_OFF; 
Nastroyka1.PORT_PD = PORT_PD_DRIVER; 
Nastroyka1.PORT_GFEN = PORT_GFEN_OFF; 
Nastroyka1.PORT_FUNC = PORT_FUNC_PORT; 
Nastroyka1.PORT_SPEED = PORT_SPEED_SLOW; 
Nastroyka1.PORT_MODE = PORT_MODE_DIGITAL; 

PORT_Init(MDR_PORTC, &Nastroyka1); 
} 




int main(void) 
{ 
uint16_t a = 'A'; 
uint16_t b = 'B'; 
uint32_t RESULT; 
float U; 
U_MLT_Init(); 
ADCInit(); 
ADC1Init(); 
LedInit(); 
U_MLT_Init(); 
PORTI(); 
UARTInit(); 


char stroka1[17]; 

UARTInit(); 
PORTI(); 
while (1) 
{ 
  /*
if(ch == a)
{ 
UART_SendData(MDR_UART2,'A'); 
PORT_SetBits(MDR_PORTF,PORT_Pin_5); 
DELAY(100000); 
PORT_SetBits(MDR_PORTF,PORT_Pin_6); 
DELAY(100000); 
PORT_SetBits(MDR_PORTF,PORT_Pin_4); 
DELAY(100000); 
PORT_SetBits(MDR_PORTF,PORT_Pin_3); 
DELAY(100000); 
} 
if(ch == b) 
{ 
UART_SendData(MDR_UART2,'B'); 
DELAY(1000); 
PORT_ResetBits(MDR_PORTF,PORT_Pin_5); 
DELAY(100000); 
PORT_ResetBits(MDR_PORTF,PORT_Pin_6); 
DELAY(100000); 
PORT_ResetBits(MDR_PORTF,PORT_Pin_4); 
DELAY(100000); 
PORT_ResetBits(MDR_PORTF,PORT_Pin_3); 
DELAY(100000); 
} 
*/


 ADC1_Start(); 
DELAY(5000); 
DELAY(5000); 

while (ADC1_GetFlagStatus(ADC1_FLAG_END_OF_CONVERSION) == 0); 
RESULT = ADC1_GetResult() & 0x00000FFF; 

U = (float)RESULT / KALIBR; 
snprintf(stroka1, 17, "U = %.2fV", U); 
U_MLT_Put_String(stroka1, 3); 
char dest[10]; 
sprintf(dest, "%f", U); 
for(int j = 0; j<5; j++){ 

UART_SendData(MDR_UART2,dest[j]); 
DELAY(500); 

}

UART_SendData(MDR_UART2,'\n'); 
DELAY(500);

DELAY(1000); 


}
} 
