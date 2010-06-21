
GO

/****** Object:  Table [dbo].[CustomerMutualFundAccount]    Script Date: 06/11/2009 16:02:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMutualFundAccount](
	[CMFA_AccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PA_AMCCode] [int] NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CMFA_FolioNum] [varchar](20) NULL,
	[CMFA_AccountOpeningDate] [datetime] NULL,
	[CMFA_IsJointlyHeld] [tinyint] NULL,
	[CMFA_CreatedOn] [datetime] NOT NULL,
	[CMFA_CreatedBy] [int] NOT NULL,
	[CMFA_ModifiedBy] [int] NOT NULL,
	[CMFA_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerMutualFundAccount_1] PRIMARY KEY CLUSTERED 
(
	[CMFA_AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Accounts Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundAccount'
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAssetGroupMutualFundAccount_ProductAssetGroup] FOREIGN KEY([PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetGroup] ([PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount] CHECK CONSTRAINT [FK_CustomerAssetGroupMutualFundAccount_ProductAssetGroup]
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccount_CustomerMutualFundAccount] FOREIGN KEY([PA_AMCCode])
REFERENCES [dbo].[ProductAMC] ([PA_AMCCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount] CHECK CONSTRAINT [FK_CustomerMutualFundAccount_CustomerMutualFundAccount]
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount] CHECK CONSTRAINT [FK_CustomerMutualFundAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount] CHECK CONSTRAINT [FK_CustomerMutualFundAccount_XMLModeOfHolding]
GO

ALTER TABLE [dbo].[CustomerMutualFundAccount] ADD  CONSTRAINT [DF_CustomerAssetGroupMutualFundAccount_PAG_AssetGroupCode]  DEFAULT ('MF') FOR [PAG_AssetGroupCode]
GO


