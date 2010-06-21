
GO

/****** Object:  Table [dbo].[CustomerEquityOdinBSEXtrnlTransactionStaging]    Script Date: 06/11/2009 12:04:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransactionStaging](
	[CEOBXTS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEOBXTS_ScripCode] [varchar](50) NULL,
	[CEOBXTS_ScripName] [varchar](30) NULL,
	[CEOBXTS_TradeNumber] [numeric](10, 0) NULL,
	[CEOBXTS_Rate] [numeric](15, 4) NULL,
	[CEOBXTS_Quantity] [numeric](10, 0) NULL,
	[CEOBXTS_Field6] [varchar](20) NULL,
	[CEOBXTS_Field7] [varchar](20) NULL,
	[CEOBXTS_TradeTime] [datetime] NULL,
	[CEOBXTS_TradeDate] [datetime] NULL,
	[CEOBXTS_TradeAccountNumber] [varchar](20) NULL,
	[CEOBXTS_BuySell] [char](1) NULL,
	[CEOBXTS_Field12] [varchar](5) NULL,
	[CEOBXTS_OrderNumber] [numeric](15, 0) NULL,
	[CEOBXTS_Field14] [varchar](5) NULL,
	[CEOBXTS_AccountStatus] [varchar](20) NULL,
	[CEOBXTS_CreatedBy] [int] NULL,
	[CEOBXTS_CreatedOn] [datetime] NULL,
	[CEOBXTS_ModifiedBy] [int] NULL,
	[CEOBXTS_ModifiedOn] [datetime] NULL,
	[CEOBXTS_RejectedRemark] [varchar](100) NULL,
	[CEOBXTS_IsRejected] [tinyint] NULL,
	[CEOBXTS_IsTradeNumberNew] [tinyint] NULL,
	[A_AdviserId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CETA_AccountId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityOdinBSEXtrnlTransactionStaging] PRIMARY KEY CLUSTERED 
(
	[CEOBXTS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


