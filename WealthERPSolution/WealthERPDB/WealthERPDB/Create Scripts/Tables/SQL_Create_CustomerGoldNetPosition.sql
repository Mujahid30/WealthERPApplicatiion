
GO

/****** Object:  Table [dbo].[CustomerGoldNetPosition]    Script Date: 06/11/2009 13:02:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerGoldNetPosition](
	[CGNP_GoldNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XMC_MeasureCode] [varchar](5) NULL,
	[CGNP_Name] [varchar](50) NULL,
	[CGNP_PurchaseDate] [datetime] NULL,
	[CGNP_PurchasePrice] [numeric](18, 4) NULL,
	[CGNP_Quantity] [numeric](10, 4) NULL,
	[CGNP_PurchaseValue] [numeric](18, 4) NULL,
	[CGNP_CurrentPrice] [numeric](18, 4) NULL,
	[CGNP_CurrentValue] [numeric](18, 4) NULL,
	[CGNP_SellDate] [datetime] NULL,
	[CGNP_SellPrice] [numeric](18, 4) NULL,
	[CGNP_SellValue] [numeric](18, 4) NULL,
	[CGNP_Remark] [varchar](100) NULL,
	[CGNP_CreatedBy] [int] NOT NULL,
	[CGNP_CreatedOn] [datetime] NOT NULL,
	[CGNP_ModifiedBy] [int] NOT NULL,
	[CGNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerInvestmentGoldPortfolio] PRIMARY KEY CLUSTERED 
(
	[CGNP_GoldNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerGoldNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGoldNetPosition_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerGoldNetPosition] CHECK CONSTRAINT [FK_CustomerGoldNetPosition_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerGoldNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGoldNetPosition_XMLMeasureCode] FOREIGN KEY([XMC_MeasureCode])
REFERENCES [dbo].[XMLMeasureCode] ([XMC_MeasureCode])
GO

ALTER TABLE [dbo].[CustomerGoldNetPosition] CHECK CONSTRAINT [FK_CustomerGoldNetPosition_XMLMeasureCode]
GO

ALTER TABLE [dbo].[CustomerGoldNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentGoldPortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerGoldNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentGoldPortfolio_ProductAssetInstrumentCategory]
GO


