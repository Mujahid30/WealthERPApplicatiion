USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductEquityMaster]    Script Date: 06/12/2009 18:18:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductEquityMaster](
	[PEM_ScripCode] [int] IDENTITY(1000,1) NOT NULL,
	[PEM_CompanyName] [varchar](255) NULL,
	[PMCC_MarketCapClassificationCode] [int] NULL,
	[PEM_MarketLot] [int] NULL,
	[PEM_FaceValue] [numeric](10, 2) NULL,
	[PEM_BookClosure] [datetime] NULL,
	[PEM_Listing] [varchar](255) NULL,
	[PEM_Incorporation] [datetime] NULL,
	[PEM_PublicIssueDate] [datetime] NULL,
	[PEM_Ticker] [varchar](100) NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[PGSSSC_SectorSubSubCategoryCode] [varchar](9) NULL,
	[PGSSC_SectorSubCategoryCode] [varchar](6) NULL,
	[PGSC_SectorCategoryCode] [varchar](3) NULL,
	[PEM_ModifiedBy] [int] NULL,
	[PEM_CreatedBy] [int] NULL,
	[PEM_ModifiedOn] [datetime] NULL,
	[PEM_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductEquity] PRIMARY KEY CLUSTERED 
(
	[PEM_ScripCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product Equity Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductEquityMaster'
GO

ALTER TABLE [dbo].[ProductEquityMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityMaster_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[ProductEquityMaster] CHECK CONSTRAINT [FK_ProductEquityMaster_ProductAssetInstrumentSubCategory]
GO

ALTER TABLE [dbo].[ProductEquityMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityMaster_ProductCAPClassification] FOREIGN KEY([PMCC_MarketCapClassificationCode])
REFERENCES [dbo].[ProductMarketCapClassification] ([PMCC_MarketCapClassificationCode])
GO

ALTER TABLE [dbo].[ProductEquityMaster] CHECK CONSTRAINT [FK_ProductEquityMaster_ProductCAPClassification]
GO

ALTER TABLE [dbo].[ProductEquityMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductEquityMaster_ProductEquityMaster] FOREIGN KEY([PEM_ScripCode])
REFERENCES [dbo].[ProductEquityMaster] ([PEM_ScripCode])
GO

ALTER TABLE [dbo].[ProductEquityMaster] CHECK CONSTRAINT [FK_ProductEquityMaster_ProductEquityMaster]
GO


