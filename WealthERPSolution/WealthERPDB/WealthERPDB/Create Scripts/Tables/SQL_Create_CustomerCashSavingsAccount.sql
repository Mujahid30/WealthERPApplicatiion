
GO

/****** Object:  Table [dbo].[CustomerCashSavingsAccount]    Script Date: 06/11/2009 11:57:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerCashSavingsAccount](
	[CCSA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CCSA_AccountNum] [varchar](30) NULL,
	[CCSA_BankName] [varchar](30) NULL,
	[CCSA_AccountOpeningDate] [datetime] NULL,
	[CCSA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CCSA_CreatedBy] [int] NULL,
	[CCSA_CreatedOn] [datetime] NULL,
	[CCSA_ModifiedOn] [datetime] NULL,
	[CCSA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerCashSavingsAccount] PRIMARY KEY CLUSTERED 
(
	[CCSA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccount] CHECK CONSTRAINT [FK_CustomerCashSavingsAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccount] CHECK CONSTRAINT [FK_CustomerCashSavingsAccount_ProductAssetInstrumentCategory]
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccount] CHECK CONSTRAINT [FK_CustomerCashSavingsAccount_XMLModeOfHolding]
GO


