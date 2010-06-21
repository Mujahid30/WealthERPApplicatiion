USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductAssetInstrumentSubCategory]    Script Date: 06/12/2009 18:17:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductAssetInstrumentSubCategory](
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NOT NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NOT NULL,
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PAISC_AssetInstrumentSubCategoryName] [varchar](50) NULL,
	[PAISC_CreatedBy] [int] NOT NULL,
	[PAISC_CreatedOn] [datetime] NOT NULL,
	[PAISC_ModifiedBy] [int] NOT NULL,
	[PAISC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductAssetInstrumnentSubCategory] PRIMARY KEY CLUSTERED 
(
	[PAISC_AssetInstrumentSubCategoryCode] ASC,
	[PAIC_AssetInstrumentCategoryCode] ASC,
	[PAG_AssetGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductAssetInstrumentSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductAssetInstrumentSubCategory_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[ProductAssetInstrumentSubCategory] CHECK CONSTRAINT [FK_ProductAssetInstrumentSubCategory_ProductAssetInstrumentCategory]
GO


