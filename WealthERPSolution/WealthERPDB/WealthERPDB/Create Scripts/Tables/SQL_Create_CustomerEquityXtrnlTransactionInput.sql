
GO

/****** Object:  Table [dbo].[CustomerEquityXtrnlTransactionInput]    Script Date: 06/11/2009 12:08:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityXtrnlTransactionInput](
	[CEXTI_Id] [int] IDENTITY(1000,1000) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEXTI_TradeNum] [numeric](15, 0) NULL,
	[CEXTI_TradeAccountNumber] [varchar](20) NULL,
	[CEXTI_OrderNum] [numeric](15, 0) NULL,
	[CEXTI_ScripCode] [varchar](50) NULL,
	[CEXTI_TradeDate] [varchar](50) NULL,
	[CEXTI_Rate] [varchar](50) NULL,
	[CEXTI_Quantity] [varchar](50) NULL,
	[CEXTI_BrokerCode] [varchar](50) NULL,
	[CEXTI_Brokerage] [varchar](50) NULL,
	[CEXTI_ServiceTax] [varchar](50) NULL,
	[CEXTI_EducationCess] [varchar](50) NULL,
	[CEXTI_STT] [varchar](50) NULL,
	[CEXTI_OtherCharges] [varchar](50) NULL,
	[CEXTI_RateInclBrokerage] [varchar](50) NULL,
	[CEXTI_TradeTotal] [varchar](50) NULL,
	[CEXTI_Exchange] [varchar](50) NULL,
	[CEXTI_BuySell] [varchar](50) NULL,
	[CEXTI_CreatedBy] [int] NULL,
	[CEXTI_CreatedOn] [datetime] NULL,
	[CEXTI_ModifiedBy] [int] NULL,
	[CEXTI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


