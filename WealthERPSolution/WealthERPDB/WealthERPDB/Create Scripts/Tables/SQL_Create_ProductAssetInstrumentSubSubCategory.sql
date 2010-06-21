USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductAssetInstrumentSubSubCategory]    Script Date: 06/12/2009 18:18:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductAssetInstrumentSubSubCategory](
	[PAISSC_AssetInstrumentSubSubCategoryCode] [varchar](8) NOT NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NOT NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NOT NULL,
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PAISSC_AssetInstrumentSubSubCategoryName] [varchar](50) NULL,
	[PAISSC_CreatedBy] [int] NOT NULL,
	[PAISSC_CreatedOn] [datetime] NOT NULL,
	[PAISSC_ModifiedBy] [int] NOT NULL,
	[PAISSC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductAssetInstrumnentSubSubCategory] PRIMARY KEY CLUSTERED 
(
	[PAISSC_AssetInstrumentSubSubCategoryCode] ASC,
	[PAISC_AssetInstrumentSubCategoryCode] ASC,
	[PAIC_AssetInstrumentCategoryCode] ASC,
	[PAG_AssetGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductAssetInstrumentSubSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductAssetInstrumentSubSubCategory_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[ProductAssetInstrumentSubSubCategory] CHECK CONSTRAINT [FK_ProductAssetInstrumentSubSubCategory_ProductAssetInstrumentSubCategory]
GO


