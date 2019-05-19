#include <MDR32F9Qx_uart.h>
#include <MDR32F9Qx_port.h>
#include <MDR32F9Qx_rst_clk.h>
#include "U_Uart.h"
#include <stdio.h>
#define DELAY(T) for (i = T; i > 0; i--)
unsigned short int ch = 66;
unsigned short int ch1 = 6;
float U = 1534.6545;
char stroka[17];
void UART2_IRQHandler(void){
	 if(UART_GetITStatusMasked(MDR_UART2,UART_IT_RX) == SET){
		 UART_ClearITPendingBit(MDR_UART2,UART_IT_RX);
		  ch = UART_ReceiveData(MDR_UART2);
		
		
	 }
 }
void UARTInit(void)
{
 PORT_InitTypeDef Nastroyka;
	UART_InitTypeDef uart_user_ini;
 RST_CLK_PCLKcmd(RST_CLK_PCLK_PORTF, ENABLE);
 //PORT_StructInit(&Nastroyka);
 Nastroyka.PORT_Pin = PORT_Pin_1;
	Nastroyka.PORT_OE = PORT_OE_OUT;
	Nastroyka.PORT_PULL_UP = PORT_PULL_UP_OFF;
	Nastroyka.PORT_PULL_DOWN = PORT_PULL_DOWN_OFF;
	Nastroyka.PORT_PD_SHM = PORT_PD_SHM_OFF;
	Nastroyka.PORT_PD = PORT_PD_DRIVER;
	Nastroyka.PORT_GFEN = PORT_GFEN_OFF;
 Nastroyka.PORT_FUNC = PORT_FUNC_OVERRID;
 Nastroyka.PORT_SPEED = PORT_SPEED_MAXFAST;
 Nastroyka.PORT_MODE = PORT_MODE_DIGITAL;
 
	PORT_Init(MDR_PORTF, &Nastroyka);
	
	Nastroyka.PORT_Pin = PORT_Pin_0;
	Nastroyka.PORT_OE = PORT_OE_IN;
	
	PORT_Init(MDR_PORTF, &Nastroyka);
	
 RST_CLK_PCLKcmd(RST_CLK_PCLK_UART2,ENABLE);
	UART_BRGInit(MDR_UART2,UART_HCLKdiv1);
	NVIC_EnableIRQ(UART2_IRQn);
 uart_user_ini.UART_BaudRate = 115200;
 uart_user_ini.UART_FIFOMode = UART_FIFO_OFF;
uart_user_ini.UART_HardwareFlowControl = UART_HardwareFlowControl_TXE | UART_HardwareFlowControl_RXE;
uart_user_ini.UART_Parity =	UART_Parity_No;
	uart_user_ini.UART_StopBits = UART_StopBits1;
	uart_user_ini.UART_WordLength = UART_WordLength8b;
	UART_Init(MDR_UART2, &uart_user_ini);
	UART_ITConfig(MDR_UART2, UART_IT_RX, ENABLE);
	UART_Cmd(MDR_UART2,ENABLE);
}
