
GO

/****** Object:  Table [dbo].[CustomerPropertyAccount]    Script Date: 06/11/2009 17:25:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerPropertyAccount](
	[CPA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[CPA_AccountNum] [varchar](30) NULL,
	[CPA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[PAISC_AssetInstrumentSubCategoryCode] [varchar](6) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CPA_CreatedBy] [int] NULL,
	[CPA_CreatedOn] [datetime] NULL,
	[CPA_ModifiedBy] [int] NULL,
	[CPA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPropertyAccount] PRIMARY KEY CLUSTERED 
(
	[CPA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerPropertyAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerPropertyAccount] CHECK CONSTRAINT [FK_CustomerPropertyAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerPropertyAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccount_ProductAssetInstrumentSubCategory] FOREIGN KEY([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentSubCategory] ([PAISC_AssetInstrumentSubCategoryCode], [PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerPropertyAccount] CHECK CONSTRAINT [FK_CustomerPropertyAccount_ProductAssetInstrumentSubCategory]
GO

ALTER TABLE [dbo].[CustomerPropertyAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO

ALTER TABLE [dbo].[CustomerPropertyAccount] CHECK CONSTRAINT [FK_CustomerPropertyAccount_XMLModeOfHolding]
GO


