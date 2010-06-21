
GO

/****** Object:  Table [dbo].[CustomerCollectibleNetPosition]    Script Date: 06/11/2009 11:59:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerCollectibleNetPosition](
	[CCNP_CollectibleNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CCNP_PurchasePrice] [numeric](18, 3) NULL,
	[CCNP_Name] [varchar](50) NULL,
	[CCNP_PurchaseDate] [datetime] NULL,
	[CCNP_PurchaseValue] [numeric](18, 3) NULL,
	[CCNP_CurrentPrice] [numeric](18, 3) NULL,
	[CCNP_CurrentValue] [numeric](18, 3) NOT NULL,
	[CCNP_Remark] [varchar](100) NULL,
	[CCNP_Quantity] [numeric](5, 0) NULL,
	[CCNP_CreatedBy] [int] NOT NULL,
	[CCNP_CreatedOn] [datetime] NOT NULL,
	[CCNP_ModifiedBy] [int] NOT NULL,
	[CCNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerInvestmentCollectiblePortfolio] PRIMARY KEY CLUSTERED 
(
	[CCNP_CollectibleNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerCollectibleNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCollectibleNetPosition_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerCollectibleNetPosition] CHECK CONSTRAINT [FK_CustomerCollectibleNetPosition_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerCollectibleNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentCollectiblePortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerCollectibleNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentCollectiblePortfolio_ProductAssetInstrumentCategory]
GO

ALTER TABLE [dbo].[CustomerCollectibleNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentCollectiblePortfolio_CICP_PurchasePrice]  DEFAULT ((0)) FOR [CCNP_PurchasePrice]
GO

ALTER TABLE [dbo].[CustomerCollectibleNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentCollectiblePortfolio_CICP_PurchaseValue]  DEFAULT ((0)) FOR [CCNP_PurchaseValue]
GO

ALTER TABLE [dbo].[CustomerCollectibleNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentCollectiblePortfolio_CICP_CurrentPrice]  DEFAULT ((0)) FOR [CCNP_CurrentPrice]
GO

ALTER TABLE [dbo].[CustomerCollectibleNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentCollectiblePortfolio_CICP_CurrentValue]  DEFAULT ((0)) FOR [CCNP_CurrentValue]
GO


