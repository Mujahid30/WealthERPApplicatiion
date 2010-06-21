
GO

/****** Object:  Table [dbo].[CustomerInsuranceAccount]    Script Date: 06/11/2009 13:06:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerInsuranceAccount](
	[CIA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CIA_PolicyNum] [varchar](30) NULL,
	[CIA_AccountNum] [varchar](30) NULL,
	[CIA_CreatedBy] [int] NULL,
	[CIA_CreatedOn] [datetime] NULL,
	[CIA_ModifiedBy] [int] NULL,
	[CIA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerInsuranceAccount] PRIMARY KEY CLUSTERED 
(
	[CIA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerInsuranceAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerInsuranceAccount] CHECK CONSTRAINT [FK_CustomerInsuranceAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerInsuranceAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceAccount_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerInsuranceAccount] CHECK CONSTRAINT [FK_CustomerInsuranceAccount_ProductAssetInstrumentCategory]
GO


