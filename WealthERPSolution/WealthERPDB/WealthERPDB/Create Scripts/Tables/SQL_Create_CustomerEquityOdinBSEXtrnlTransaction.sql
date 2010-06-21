
GO

/****** Object:  Table [dbo].[CustomerEquityOdinBSEXtrnlTransaction]    Script Date: 06/11/2009 12:04:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransaction](
	[CEOBXT_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CET_EqTransId] [int] NULL,
	[CEOBXT_ScripCode] [numeric](10, 0) NULL,
	[CEOBXT_ScripName] [varchar](30) NULL,
	[CEOBXT_TradeNumber] [numeric](10, 0) NULL,
	[CEOBXT_Rate] [numeric](15, 4) NULL,
	[CEOBXT_Quantity] [numeric](10, 0) NULL,
	[CEOBXT_Field6] [varchar](20) NULL,
	[CEOBXT_Field7] [varchar](20) NULL,
	[CEOBXT_TradeTime] [datetime] NULL,
	[CEOBXT_TradeDate] [datetime] NULL,
	[CEOBXT_TradeAccountNumber] [varchar](20) NULL,
	[CEOBXT_BuySell] [char](1) NULL,
	[CEOBXT_Field12] [varchar](5) NULL,
	[CEOBXT_OrderNumber] [numeric](15, 0) NULL,
	[CEOBXT_Field14] [varchar](5) NULL,
	[CEOBXT_AccountStatus] [varchar](20) NULL,
	[CEOBXT_CreatedBy] [int] NULL,
	[CEOBXT_CreatedOn] [datetime] NULL,
	[CEOBXT_ModifiedBy] [int] NULL,
	[CEOBXT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityOdinBSEXtrnlTransaction] PRIMARY KEY CLUSTERED 
(
	[CEOBXT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityOdinBSEXtrnlTransaction_CustomerEquityTransaction] FOREIGN KEY([CET_EqTransId])
REFERENCES [dbo].[CustomerEquityTransaction] ([CET_EqTransId])
GO

ALTER TABLE [dbo].[CustomerEquityOdinBSEXtrnlTransaction] CHECK CONSTRAINT [FK_CustomerEquityOdinBSEXtrnlTransaction_CustomerEquityTransaction]
GO


