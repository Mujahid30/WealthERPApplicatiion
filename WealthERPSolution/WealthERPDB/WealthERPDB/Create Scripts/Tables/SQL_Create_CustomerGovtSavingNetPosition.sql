
GO

/****** Object:  Table [dbo].[CustomerGovtSavingNetPosition]    Script Date: 06/11/2009 13:05:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerGovtSavingNetPosition](
	[CGSNP_GovtSavingNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CGSA_AccountId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XDI_DebtIssuerCode] [varchar](5) NULL,
	[XIB_InterestBasisCode] [varchar](5) NULL,
	[XF_CompoundInterestFrequencyCode] [varchar](5) NULL,
	[XF_InterestPayableFrequencyCode] [varchar](5) NULL,
	[XF_DepositFrequencyCode] [varchar](5) NULL,
	[CGSNP_Name] [varchar](50) NOT NULL,
	[CGSNP_PurchaseDate] [datetime] NULL,
	[CGSNP_Quantity] [numeric](12, 3) NULL,
	[CGSNP_CurrentPrice] [numeric](18, 4) NULL,
	[CGSNP_CurrentValue] [numeric](18, 4) NULL,
	[CGSNP_MaturityDate] [datetime] NULL,
	[CGSNP_DepositAmount] [numeric](18, 4) NULL,
	[CGSNP_MaturityValue] [numeric](18, 4) NULL,
	[CGSNP_IsInterestAccumalated] [tinyint] NULL,
	[CGSNP_InterestAmtAccumalated] [numeric](18, 4) NULL,
	[CGSNP_InterestAmtPaidOut] [numeric](18, 4) NULL,
	[CGSNP_InterestRate] [numeric](7, 4) NULL,
	[CGSNP_Remark] [varchar](100) NULL,
	[CGSNP_SubsqntDepositAmount] [numeric](18, 4) NULL,
	[CGSNP_SubsqntDepositDate] [datetime] NULL,
	[CGSNP_CreatedBy] [int] NOT NULL,
	[CGSNP_CreatedOn] [datetime] NOT NULL,
	[CGSNP_ModifiedBy] [int] NOT NULL,
	[CGSNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerInvestmentGovtSavingPortfolio] PRIMARY KEY CLUSTERED 
(
	[CGSNP_GovtSavingNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_CustomerGovtSavingAccount] FOREIGN KEY([CGSA_AccountId])
REFERENCES [dbo].[CustomerGovtSavingAccount] ([CGSA_AccountId])
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_CustomerGovtSavingAccount]
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLDebtIssuer] FOREIGN KEY([XDI_DebtIssuerCode])
REFERENCES [dbo].[XMLDebtIssuer] ([XDI_DebtIssuerCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLDebtIssuer]
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency] FOREIGN KEY([XF_CompoundInterestFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency]
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency1] FOREIGN KEY([XF_DepositFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency1]
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency2] FOREIGN KEY([XF_InterestPayableFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLFrequency2]
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLInterestBasis] FOREIGN KEY([XIB_InterestBasisCode])
REFERENCES [dbo].[XMLInterestBasis] ([XIB_InterestBasisCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerGovtSavingNetPosition_XMLInterestBasis]
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentGovtSavingPortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerGovtSavingNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentGovtSavingPortfolio_ProductAssetInstrumentCategory]
GO


