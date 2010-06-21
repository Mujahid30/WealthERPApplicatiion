
GO

/****** Object:  Table [dbo].[CustomerEquityXtrnlTransactionStaging]    Script Date: 06/11/2009 12:11:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityXtrnlTransactionStaging](
	[CEXTS_Id] [int] IDENTITY(1000,1000) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEXTS_TradeNum] [numeric](15, 0) NULL,
	[CEXTS_TradeAccountNumber] [varchar](20) NULL,
	[CEXTS_TradeDate] [datetime] NULL,
	[CEXTS_ScripCode] [varchar](50) NULL,
	[CEXTS_Rate] [numeric](18, 3) NULL,
	[CEXTS_Quantity] [numeric](15, 3) NULL,
	[CEXTS_BrokerCode] [varchar](50) NULL,
	[CEXTS_Exchange] [char](5) NULL,
	[CEXTS_Brokerage] [numeric](18, 3) NULL,
	[CEXTS_ServiceTax] [numeric](18, 3) NULL,
	[CEXTS_EducationCess] [numeric](18, 3) NULL,
	[CEXTS_STT] [numeric](18, 3) NULL,
	[CEXTS_OtherCharges] [numeric](18, 3) NULL,
	[CEXTS_RateInclBrokerage] [numeric](18, 3) NULL,
	[CEXTS_TradeTotal] [numeric](18, 3) NULL,
	[CEXTS_BuySell] [char](1) NULL,
	[CEXTS_OrderNum] [numeric](15, 0) NULL,
	[CETA_AccountId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CEXTS_IsRejected] [tinyint] NULL,
	[CEXTS_RejectedRemark] [varchar](100) NULL,
	[CEXTS_IsTradeAccountNew] [tinyint] NULL,
	[CEXTS_CreatedBy] [int] NULL,
	[CEXTS_CreatedOn] [datetime] NULL,
	[CEXTS_ModifiedBy] [int] NULL,
	[CEXTS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CEXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


