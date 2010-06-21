
GO

/****** Object:  Table [dbo].[CustomerFixedIncomeNetPosition]    Script Date: 06/11/2009 12:58:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerFixedIncomeNetPosition](
	[CFINP_FINPId] [int] IDENTITY(1000,1) NOT NULL,
	[CFIA_AccountId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XDI_DebtIssuerCode] [varchar](5) NULL,
	[XIB_InterestBasisCode] [varchar](5) NULL,
	[XF_CompoundInterestFrequencyCode] [varchar](5) NULL,
	[XF_InterestPayableFrequencyCode] [varchar](5) NULL,
	[CFINP_Name] [varchar](50) NULL,
	[CFINP_IssueDate] [datetime] NULL,
	[CFINP_PrincipalAmount] [numeric](18, 4) NULL,
	[CFINP_InterestAmtPaidOut] [numeric](18, 4) NULL,
	[CFINP_InterestAmtAcculumated] [numeric](18, 4) NULL,
	[CFINP_InterestRate] [numeric](10, 0) NULL,
	[CFINP_FaceValue] [numeric](18, 4) NULL,
	[CFINP_PurchasePrice] [numeric](18, 4) NULL,
	[CFINP_SubsequentDepositAmount] [numeric](18, 4) NULL,
	[XF_DepositFrquencycode] [varchar](5) NULL,
	[CFINP_DebentureNum] [numeric](5, 0) NULL,
	[CFINP_PurchaseValue] [numeric](18, 4) NULL,
	[CFINP_PurchaseDate] [datetime] NULL,
	[CFINP_MaturityDate] [datetime] NULL,
	[CFINP_MaturityValue] [numeric](18, 4) NULL,
	[CFINP_IsInterestAccumulated] [tinyint] NULL,
	[CFINP_MaturityFaceValue] [numeric](18, 4) NULL,
	[CFINP_CurrentPrice] [numeric](18, 4) NULL,
	[CFINP_CurrentValue] [numeric](10, 0) NULL,
	[CFINP_Quantity] [numeric](10, 0) NULL,
	[CFINP_Remark] [varchar](100) NULL,
	[CFINP_CreatedBy] [int] NOT NULL,
	[CFINP_CreatedOn] [datetime] NOT NULL,
	[CFINP_ModifiedBy] [int] NOT NULL,
	[CFINP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerFIInvestment] PRIMARY KEY CLUSTERED 
(
	[CFINP_FINPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Investment FI Transaction' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerFixedIncomeNetPosition'
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_CustomerFixedIncomeAccount] FOREIGN KEY([CFIA_AccountId])
REFERENCES [dbo].[CustomerFixedIncomeAccount] ([CFIA_AccountId])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_CustomerFixedIncomeAccount]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLDebtIssuer] FOREIGN KEY([XDI_DebtIssuerCode])
REFERENCES [dbo].[XMLDebtIssuer] ([XDI_DebtIssuerCode])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLDebtIssuer]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency] FOREIGN KEY([XF_CompoundInterestFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency1] FOREIGN KEY([XF_DepositFrquencycode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency1]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency2] FOREIGN KEY([XF_InterestPayableFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLFrequency2]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLInterestBasis] FOREIGN KEY([XIB_InterestBasisCode])
REFERENCES [dbo].[XMLInterestBasis] ([XIB_InterestBasisCode])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerFixedIncomeNetPosition_XMLInterestBasis]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInvestmentFixedIncomePortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] CHECK CONSTRAINT [FK_CustomerInvestmentFixedIncomePortfolio_ProductAssetInstrumentCategory]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PrincipalAmount]  DEFAULT ((0)) FOR [CFINP_PrincipalAmount]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestAmtPaidOut]  DEFAULT ((0)) FOR [CFINP_InterestAmtPaidOut]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestAmtAcculumated]  DEFAULT ((0)) FOR [CFINP_InterestAmtAcculumated]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_InterestRate]  DEFAULT ((0)) FOR [CFINP_InterestRate]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_FaceValue]  DEFAULT ((0)) FOR [CFINP_FaceValue]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PurchasePrice]  DEFAULT ((0)) FOR [CFINP_PurchasePrice]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_SubsequentDepositAmount]  DEFAULT ((0)) FOR [CFINP_SubsequentDepositAmount]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_DebentureNum]  DEFAULT ((0)) FOR [CFINP_DebentureNum]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_PurchaseValue]  DEFAULT ((0)) FOR [CFINP_PurchaseValue]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_MaturityValue]  DEFAULT ((0)) FOR [CFINP_MaturityValue]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_MaturityFaceValue]  DEFAULT ((0)) FOR [CFINP_MaturityFaceValue]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_CurrentPrice]  DEFAULT ((0)) FOR [CFINP_CurrentPrice]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeNetPosition] ADD  CONSTRAINT [DF_CustomerInvestmentFixedIncomePortfolio_CIFIP_CurrentValue]  DEFAULT ((0)) FOR [CFINP_CurrentValue]
GO


