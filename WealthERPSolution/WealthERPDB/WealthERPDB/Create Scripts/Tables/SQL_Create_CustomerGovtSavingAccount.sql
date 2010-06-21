
GO

/****** Object:  Table [dbo].[CustomerGovtSavingAccount]    Script Date: 06/11/2009 13:03:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerGovtSavingAccount](
	[CGSA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[CGSA_AccountNum] [varchar](30) NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CGSA_AccountSource] [varchar](30) NULL,
	[CGSA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CGSA_AccountOpeningDate] [datetime] NULL,
	[CGSA_CreatedBy] [int] NULL,
	[CGSA_CreatedOn] [datetime] NULL,
	[CGSA_ModifiedBy] [int] NULL,
	[CGSA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerGovtSavingAccount_1] PRIMARY KEY CLUSTERED 
(
	[CGSA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccount] CHECK CONSTRAINT [FK_CustomerGovtSavingAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccount] CHECK CONSTRAINT [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory]
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory1] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccount] CHECK CONSTRAINT [FK_CustomerGovtSavingAccount_ProductAssetInstrumentCategory1]
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccount] CHECK CONSTRAINT [FK_CustomerGovtSavingAccount_XMLModeOfHolding]
GO


