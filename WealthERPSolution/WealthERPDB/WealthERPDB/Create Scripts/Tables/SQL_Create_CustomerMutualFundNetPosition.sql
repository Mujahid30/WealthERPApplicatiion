
GO

/****** Object:  Table [dbo].[CustomerMutualFundNetPosition]    Script Date: 06/11/2009 16:03:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerMutualFundNetPosition](
	[CMFNP_MFNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CMFA_AccountId] [int] NULL,
	[PASP_SchemePlanCode] [int] NULL,
	[CMFNP_NetHoldings] [numeric](18, 4) NULL,
	[CMFNP_MarketPrice] [numeric](18, 4) NULL,
	[CMFNP_ValuationDate] [datetime] NULL,
	[CMFNP_SalesQuantity] [numeric](18, 4) NULL,
	[CMFNP_RealizedSaleProceeds] [numeric](18, 4) NULL,
	[CMFNP_AveragePrice] [numeric](18, 4) NULL,
	[CMFNP_RealizedP/L] [numeric](18, 4) NULL,
	[CMFNP_CostOfSales] [numeric](18, 4) NULL,
	[CMFNP_NetCost] [numeric](18, 4) NULL,
	[CMFNP_CurrentValue] [numeric](18, 4) NULL,
	[CMFNP_DividendIncome] [numeric](18, 4) NULL,
	[CMFNP_CreatedBy] [int] NOT NULL,
	[CMFNP_CreatedOn] [datetime] NOT NULL,
	[CMFNP_ModifiedOn] [datetime] NOT NULL,
	[CMFNP_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerMFPortfolio] PRIMARY KEY CLUSTERED 
(
	[CMFNP_MFNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer MF Portfolio Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundNetPosition'
GO

ALTER TABLE [dbo].[CustomerMutualFundNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundNetPosition_CustomerMutualFundAccount] FOREIGN KEY([CMFA_AccountId])
REFERENCES [dbo].[CustomerMutualFundAccount] ([CMFA_AccountId])
GO

ALTER TABLE [dbo].[CustomerMutualFundNetPosition] CHECK CONSTRAINT [FK_CustomerMutualFundNetPosition_CustomerMutualFundAccount]
GO

ALTER TABLE [dbo].[CustomerMutualFundNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundNetPosition_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundNetPosition] CHECK CONSTRAINT [FK_CustomerMutualFundNetPosition_ProductAMCSchemePlan]
GO


