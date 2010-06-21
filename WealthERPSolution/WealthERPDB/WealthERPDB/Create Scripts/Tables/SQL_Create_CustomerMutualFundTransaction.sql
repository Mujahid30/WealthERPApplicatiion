
GO

/****** Object:  Table [dbo].[CustomerMutualFundTransaction]    Script Date: 06/11/2009 16:03:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMutualFundTransaction](
	[CMFT_MFTransId] [int] IDENTITY(1000,1) NOT NULL,
	[CMFA_AccountId] [int] NULL,
	[PASP_SchemePlanCode] [int] NULL,
	[CMFT_TransactionDate] [datetime] NOT NULL,
	[CMFT_BuySell] [char](1) NULL,
	[CMFT_DividendRate] [numeric](10, 5) NULL,
	[CMFT_NAV] [numeric](18, 5) NULL,
	[CMFT_Price] [numeric](18, 5) NULL,
	[CMFT_Amount] [numeric](18, 5) NULL,
	[CMFT_Units] [numeric](18, 5) NULL,
	[CMFT_STT] [numeric](10, 5) NULL,
	[CMFT_IsSourceManual] [tinyint] NULL,
	[XES_SourceCode] [varchar](5) NULL,
	[CMFT_SwitchSourceTrxId] [int] NULL,
	[WMTT_TransactionClassificationCode] [varchar](3) NULL,
	[CMFT_ModifiedBy] [int] NOT NULL,
	[CMFT_CreatedBy] [int] NOT NULL,
	[CMFT_CreatedOn] [datetime] NOT NULL,
	[CMFT_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerMFTransaction] PRIMARY KEY CLUSTERED 
(
	[CMFT_MFTransId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer MF Transaction Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundTransaction'
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMFTransaction_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] CHECK CONSTRAINT [FK_CustomerMFTransaction_ProductAMCSchemePlan]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMutualFundTransaction_CustomerMutualFundAccount] FOREIGN KEY([CMFA_AccountId])
REFERENCES [dbo].[CustomerMutualFundAccount] ([CMFA_AccountId])
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] CHECK CONSTRAINT [FK_CustomerMutualFundTransaction_CustomerMutualFundAccount]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMutualFundTransaction_MutualFundTransactionType] FOREIGN KEY([WMTT_TransactionClassificationCode])
REFERENCES [dbo].[WerpMutualFundTransactionType] ([WMTT_TransactionClassificationCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] CHECK CONSTRAINT [FK_CustomerMutualFundTransaction_MutualFundTransactionType]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMutualFundTransaction_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] CHECK CONSTRAINT [FK_CustomerMutualFundTransaction_XMLExternalSource]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentMutualFundTransaction_CIMFT_DividendRate]  DEFAULT ((0)) FOR [CMFT_DividendRate]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CMFT_NAV]  DEFAULT ((0)) FOR [CMFT_NAV]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CMFT_Price]  DEFAULT ((0)) FOR [CMFT_Price]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CMFT_Amount]  DEFAULT ((0)) FOR [CMFT_Amount]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CMFT_Units]  DEFAULT ((0)) FOR [CMFT_Units]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentMutualFundTransaction_CIMFT_STT]  DEFAULT ((0)) FOR [CMFT_STT]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerInvestmentMutualFundTransaction_CIMFT_SwitchSourceTrxId]  DEFAULT ((0)) FOR [CMFT_SwitchSourceTrxId]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CIMFT_ModifiedBy]  DEFAULT ((0)) FOR [CMFT_ModifiedBy]
GO

ALTER TABLE [dbo].[CustomerMutualFundTransaction] ADD  CONSTRAINT [DF_CustomerMutualFundTransaction_CIMFT_CreatedBy]  DEFAULT ((0)) FOR [CMFT_CreatedBy]
GO


