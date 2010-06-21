
GO

/****** Object:  Table [dbo].[CustomerEquityTransaction]    Script Date: 06/11/2009 12:07:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityTransaction](
	[CET_EqTransId] [int] IDENTITY(1000,1) NOT NULL,
	[CETA_AccountId] [int] NULL,
	[PEM_ScripCode] [int] NULL,
	[CET_BuySell] [char](1) NULL,
	[CET_TradeNum] [numeric](15, 0) NULL,
	[CET_OrderNum] [numeric](15, 0) NULL,
	[CET_IsSpeculative] [tinyint] NULL,
	[XE_ExchangeCode] [varchar](5) NULL,
	[CET_TradeDate] [datetime] NULL,
	[CET_Rate] [numeric](18, 4) NULL,
	[CET_Quantity] [numeric](18, 4) NULL,
	[CET_Brokerage] [numeric](18, 4) NULL,
	[CET_ServiceTax] [numeric](18, 4) NULL,
	[CET_EducationCess] [numeric](18, 4) NULL,
	[CET_STT] [numeric](18, 4) NULL,
	[CET_OtherCharges] [numeric](18, 4) NULL,
	[CET_RateInclBrokerage] [numeric](18, 4) NULL,
	[CET_TradeTotal] [numeric](18, 4) NULL,
	[XB_BrokerCode] [varchar](5) NULL,
	[CET_IsSplit] [tinyint] NULL,
	[CET_SplitCustEqTransId] [int] NULL,
	[XES_SourceCode] [varchar](5) NULL,
	[WETT_TransactionCode] [tinyint] NULL,
	[CET_IsSourceManual] [tinyint] NULL,
	[CET_ModifiedBy] [int] NULL,
	[CET_ModifiedOn] [datetime] NULL,
	[CET_CreatedBy] [int] NULL,
	[CET_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityTransaction] PRIMARY KEY CLUSTERED 
(
	[CET_EqTransId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Equity Transaction Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerEquityTransaction'
GO

ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_CustomerEquityTradeAccount] FOREIGN KEY([CETA_AccountId])
REFERENCES [dbo].[CustomerEquityTradeAccount] ([CETA_AccountId])
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_CustomerEquityTradeAccount]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_ProductEquity] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_ProductEquity]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_ProductEquityMaster]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_XMLBroker] FOREIGN KEY([XB_BrokerCode])
REFERENCES [dbo].[XMLBroker] ([XB_BrokerCode])
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_XMLBroker]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_XMLExchange] FOREIGN KEY([XE_ExchangeCode])
REFERENCES [dbo].[XMLExchange] ([XE_ExchangeCode])
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_XMLExchange]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTransaction_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerEquityTransaction_XMLExternalSource]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentEquityTransaction_WerpEquityTransactionType] FOREIGN KEY([WETT_TransactionCode])
REFERENCES [dbo].[WerpEquityTransactionType] ([WETT_TransactionCode])
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] CHECK CONSTRAINT [FK_CustomerInvestmentEquityTransaction_WerpEquityTransactionType]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_TradeNum]  DEFAULT ((0)) FOR [CET_TradeNum]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_OrderNum]  DEFAULT ((0)) FOR [CET_OrderNum]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_Rate]  DEFAULT ((0)) FOR [CET_Rate]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_Quantity]  DEFAULT ((0)) FOR [CET_Quantity]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_Brokerage]  DEFAULT ((0)) FOR [CET_Brokerage]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_ServiceTax]  DEFAULT ((0)) FOR [CET_ServiceTax]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_EducationCess]  DEFAULT ((0)) FOR [CET_EducationCess]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_STT]  DEFAULT ((0)) FOR [CET_STT]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_OtherCharges]  DEFAULT ((0)) FOR [CET_OtherCharges]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_RateInclBrokerage]  DEFAULT ((0)) FOR [CET_RateInclBrokerage]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_TradeTotal]  DEFAULT ((0)) FOR [CET_TradeTotal]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerEquityTransaction_CET_IsSplit]  DEFAULT ((0)) FOR [CET_IsSplit]
GO

ALTER TABLE [dbo].[CustomerEquityTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentEquityTransaction_CIET_SplitCustEqTransId]  DEFAULT ((0)) FOR [CET_SplitCustEqTransId]
GO


