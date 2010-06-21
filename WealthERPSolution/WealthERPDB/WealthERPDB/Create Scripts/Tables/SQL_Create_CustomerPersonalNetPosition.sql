
GO

/****** Object:  Table [dbo].[CustomerPersonalNetPosition]    Script Date: 06/11/2009 16:05:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerPersonalNetPosition](
	[CPNP_PersonalNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CPNP_Name] [varchar](50) NULL,
	[CPNP_PurchaseDate] [datetime] NULL,
	[CPNP_Quantity] [numeric](5, 0) NULL,
	[CPNP_PurchasePrice] [numeric](18, 3) NULL,
	[CPNP_PurchaseValue] [numeric](18, 3) NULL,
	[CPNP_CurrentPrice] [numeric](18, 3) NULL,
	[CPNP_CurrentValue] [numeric](18, 3) NULL,
	[CPNP_CreatedBy] [int] NULL,
	[CPNP_CreatedOn] [datetime] NULL,
	[CPNP_ModifiedBy] [int] NULL,
	[CPNP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPersonalPortfolio] PRIMARY KEY CLUSTERED 
(
	[CPNP_PersonalNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerPersonalNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPersonalNetPosition_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerPersonalNetPosition] CHECK CONSTRAINT [FK_CustomerPersonalNetPosition_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerPersonalNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPersonalPortfolio_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerPersonalNetPosition] CHECK CONSTRAINT [FK_CustomerPersonalPortfolio_ProductAssetInstrumentSubCategory]
GO


