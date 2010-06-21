
GO

/****** Object:  Table [dbo].[CustomerEquityTradeAccount]    Script Date: 06/11/2009 12:06:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityTradeAccount](
	[CETA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XB_BrokerCode] [varchar](5) NULL,
	[CETA_TradeAccountNum] [varchar](20) NULL,
	[CETA_AccountOpeningDate] [datetime] NULL,
	[CETA_CreatedBy] [int] NULL,
	[CETA_CreatedOn] [datetime] NULL,
	[CETA_ModifiedBy] [int] NULL,
	[CETA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityTradeAccount] PRIMARY KEY CLUSTERED 
(
	[CETA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerEquityTradeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAssetGroupEquityAccount_ProductAssetGroup] FOREIGN KEY([PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetGroup] ([PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerEquityTradeAccount] CHECK CONSTRAINT [FK_CustomerAssetGroupEquityAccount_ProductAssetGroup]
GO

ALTER TABLE [dbo].[CustomerEquityTradeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerEquityTradeAccount] CHECK CONSTRAINT [FK_CustomerEquityTradeAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerEquityTradeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeAccount_XMLBroker] FOREIGN KEY([XB_BrokerCode])
REFERENCES [dbo].[XMLBroker] ([XB_BrokerCode])
GO

ALTER TABLE [dbo].[CustomerEquityTradeAccount] CHECK CONSTRAINT [FK_CustomerEquityTradeAccount_XMLBroker]
GO

ALTER TABLE [dbo].[CustomerEquityTradeAccount] ADD  CONSTRAINT [DF_CustomerAssetGroupEquityAccount_PAG_AssetGroupCode]  DEFAULT ('DE') FOR [PAG_AssetGroupCode]
GO


