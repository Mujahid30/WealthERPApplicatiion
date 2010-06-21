
GO

/****** Object:  Table [dbo].[CustomerFixedIncomeAccount]    Script Date: 06/11/2009 12:12:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerFixedIncomeAccount](
	[CFIA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CFIA_AccountNum] [varchar](30) NULL,
	[CFIA_AccountSource] [varchar](30) NULL,
	[CFIA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CFIA_CreatedBy] [int] NULL,
	[CFIA_CreatedOn] [datetime] NULL,
	[CFIA_ModifiedBy] [int] NULL,
	[CFIA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerFixedIncomeAccount_1] PRIMARY KEY CLUSTERED 
(
	[CFIA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAccount] CHECK CONSTRAINT [FK_CustomerFixedIncomeAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAccount] CHECK CONSTRAINT [FK_CustomerFixedIncomeAccount_ProductAssetInstrumentCategory]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAccount] CHECK CONSTRAINT [FK_CustomerFixedIncomeAccount_XMLModeOfHolding]
GO


