
GO

/****** Object:  Table [dbo].[CustomerEquityNetPosition]    Script Date: 06/11/2009 12:03:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerEquityNetPosition](
	[CENP_EquityNPId] [int] IDENTITY(1000,1) NOT NULL,
	[PEM_ScripCode] [int] NOT NULL,
	[CETA_AccountId] [int] NOT NULL,
	[CENP_ValuationDate] [datetime] NULL,
	[CENP_NetHoldings] [numeric](18, 4) NULL,
	[CENP_AveragePrice] [numeric](18, 4) NULL,
	[CENP_MarketPrice] [numeric](18, 4) NULL,
	[CENP_RealizedP/L] [numeric](18, 4) NULL,
	[CENP_CostOfSales] [numeric](18, 4) NULL,
	[CENP_NetCost] [numeric](18, 4) NULL,
	[CENP_SpeculativeSaleQuantity] [numeric](18, 4) NULL,
	[CENP_DeliverySaleQuantity] [numeric](18, 4) NULL,
	[CENP_SaleQuantity] [numeric](18, 4) NULL,
	[CENP_RealizedP/LForSpeculative] [numeric](18, 4) NULL,
	[CENP_RealizedP/LForDelivery] [numeric](18, 4) NULL,
	[CENP_CostOfSalesForSpeculative] [numeric](18, 4) NULL,
	[CENP_CostofSalesforDelivery] [numeric](18, 4) NULL,
	[CENP_Deliverysaleproceeds] [numeric](18, 4) NULL,
	[CENP_Speculativesalesproceeds] [numeric](18, 4) NULL,
	[CENP_CurrentValue] [numeric](18, 4) NULL,
	[CENP_CreatedBy] [int] NOT NULL,
	[CENP_CreatedOn] [datetime] NOT NULL,
	[CENP_ModifiedBy] [int] NOT NULL,
	[CENP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerEquityPortfolio] PRIMARY KEY CLUSTERED 
(
	[CENP_EquityNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cusotmer Equity Portfolio Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerEquityNetPosition'
GO

ALTER TABLE [dbo].[CustomerEquityNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityNetPosition_CustomerEquityTradeAccount] FOREIGN KEY([CETA_AccountId])
REFERENCES [dbo].[CustomerEquityTradeAccount] ([CETA_AccountId])
GO

ALTER TABLE [dbo].[CustomerEquityNetPosition] CHECK CONSTRAINT [FK_CustomerEquityNetPosition_CustomerEquityTradeAccount]
GO

ALTER TABLE [dbo].[CustomerEquityNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityNetPosition_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO

ALTER TABLE [dbo].[CustomerEquityNetPosition] CHECK CONSTRAINT [FK_CustomerEquityNetPosition_ProductEquityMaster]
GO


