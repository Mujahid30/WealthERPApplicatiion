USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductAssetInstrumentCategory]    Script Date: 06/12/2009 17:04:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductAssetInstrumentCategory](
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NOT NULL,
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PAIC_AssetInstrumentCategoryName] [varchar](50) NULL,
	[PAIC_CreatedBy] [int] NOT NULL,
	[PAIC_CreatedOn] [datetime] NOT NULL,
	[PAIC_ModifiedBy] [int] NOT NULL,
	[PAIC_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductAssetInstrumnentCategory_1] PRIMARY KEY CLUSTERED 
(
	[PAIC_AssetInstrumentCategoryCode] ASC,
	[PAG_AssetGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProductAssetInstrumentCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductAssetInstrumentCategory_ProductAssetGroup] FOREIGN KEY([PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetGroup] ([PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[ProductAssetInstrumentCategory] CHECK CONSTRAINT [FK_ProductAssetInstrumentCategory_ProductAssetGroup]
GO


