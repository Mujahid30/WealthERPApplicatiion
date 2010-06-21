
GO

/****** Object:  Table [dbo].[CustomerPropertyNetPosition]    Script Date: 06/11/2009 19:55:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerPropertyNetPosition](
	[CPNP_PropertyNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CPA_AccountId] [int] NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XMC_MeasureCode] [varchar](5) NULL,
	[CPNP_Name] [varchar](50) NULL,
	[CPNP_PropertyAdrLine1] [varchar](30) NULL,
	[CPNP_PropertyAdrLine2] [varchar](30) NULL,
	[CPNP_PropertyAdrLine3] [varchar](30) NULL,
	[CPNP_PropertyCity] [varchar](30) NULL,
	[CPNP_PropertyState] [varchar](30) NULL,
	[CPNP_PropertyCountry] [varchar](30) NULL,
	[CPNP_PropertyPinCode] [numeric](6, 0) NULL,
	[CPNP_PurchaseDate] [datetime] NULL,
	[CPNP_PurchasePrice] [numeric](18, 3) NULL,
	[CPNP_Quantity] [numeric](18, 5) NULL,
	[CPNP_CurrentPrice] [numeric](18, 3) NULL,
	[CPNP_CurrentValue] [numeric](18, 3) NULL,
	[CPNP_PurchaseValue] [numeric](18, 3) NULL,
	[CPNP_SellDate] [datetime] NULL,
	[CPNP_SellPrice] [numeric](18, 3) NULL,
	[CPNP_SellValue] [numeric](18, 3) NULL,
	[CPNP_Remark] [varchar](100) NULL,
	[CPNP_CreatedBy] [int] NOT NULL,
	[CPNP_CreatedOn] [datetime] NOT NULL,
	[CPNP_ModifiedOn] [datetime] NOT NULL,
	[CPNP_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerInvestmentPropertyPortfolio] PRIMARY KEY CLUSTERED 
(
	[CPNP_PropertyNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Investment Real Estate Transaction' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPropertyNetPosition'
GO

ALTER TABLE [dbo].[CustomerPropertyNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentPropertyPortfolio_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerPropertyNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentPropertyPortfolio_ProductAssetInstrumentSubCategory]
GO

ALTER TABLE [dbo].[CustomerPropertyNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyNetPosition_CustomerPropertyAccount] FOREIGN KEY([CPA_AccountId])
REFERENCES [dbo].[CustomerPropertyAccount] ([CPA_AccountId])
GO

ALTER TABLE [dbo].[CustomerPropertyNetPosition] CHECK CONSTRAINT [FK_CustomerPropertyNetPosition_CustomerPropertyAccount]
GO

ALTER TABLE [dbo].[CustomerPropertyNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyNetPosition_XMLMeasureCode] FOREIGN KEY([XMC_MeasureCode])
REFERENCES [dbo].[XMLMeasureCode] ([XMC_MeasureCode])
GO

ALTER TABLE [dbo].[CustomerPropertyNetPosition] CHECK CONSTRAINT [FK_CustomerPropertyNetPosition_XMLMeasureCode]
GO


	