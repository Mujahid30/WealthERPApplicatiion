
GO

/****** Object:  Table [dbo].[CustomerPensionandGratuitiesAccount]    Script Date: 06/11/2009 16:04:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerPensionandGratuitiesAccount](
	[CPGA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CPGA_AccountNum] [varchar](30) NULL,
	[CPGA_AccountSource] [varchar](30) NULL,
	[CPGA_IsHeldJointly] [tinyint] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CPGA_AccountOpeningDate] [datetime] NULL,
	[CPGA_CreatedBy] [int] NULL,
	[CPGA_CreatedOn] [datetime] NULL,
	[CPGA_ModifiedBy] [int] NULL,
	[CPGA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPensionandGratuitiesAccount_1] PRIMARY KEY CLUSTERED 
(
	[CPGA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_ProductAssetInstrumentCategory]
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesAccount] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesAccount_XMLModeOfHolding]
GO


