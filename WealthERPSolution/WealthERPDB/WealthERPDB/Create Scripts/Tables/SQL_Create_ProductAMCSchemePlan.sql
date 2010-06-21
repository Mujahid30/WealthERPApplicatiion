
GO

/****** Object:  Table [dbo].[ProductAMCSchemePlan]    Script Date: 06/11/2009 20:34:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductAMCSchemePlan](
	[PASP_SchemePlanCode] [int] IDENTITY(1000,1) NOT NULL,
	[PA_AMCCode] [int] NULL,
	[PASP_SchemePlanName] [varchar](max) NULL,
	[PAISSC_AssetInstrumentSubSubCategoryCode] [varchar](8) NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[PASP_ModifiedBy] [int] NULL,
	[PASP_ModifiedOn] [datetime] NULL,
	[PASP_CreatedBy] [int] NULL,
	[PASP_CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductAMCSchemePlan] PRIMARY KEY CLUSTERED 
(
	[PASP_SchemePlanCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Product AMC SchemePlan Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductAMCSchemePlan'
GO

ALTER TABLE [dbo].[ProductAMCSchemePlan]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemePlan_ProductAMC] FOREIGN KEY([PA_AMCCode])
REFERENCES [dbo].[ProductAMC] ([PA_AMCCode])
GO

ALTER TABLE [dbo].[ProductAMCSchemePlan] CHECK CONSTRAINT [FK_ProductAMCSchemePlan_ProductAMC]
GO

ALTER TABLE [dbo].[ProductAMCSchemePlan]  WITH CHECK ADD  CONSTRAINT [FK_ProductAMCSchemePlan_ProductAssetInstrumentSubSubCategory] FOREIGN KEY([PAISSC_AssetInstrumentSubSubCategoryCode], [PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubSubCategory] ([PAISSC_AssetInstrumentSubSubCategoryCode], [PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[ProductAMCSchemePlan] CHECK CONSTRAINT [FK_ProductAMCSchemePlan_ProductAssetInstrumentSubSubCategory]
GO


