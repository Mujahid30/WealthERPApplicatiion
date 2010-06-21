
GO

/****** Object:  Table [dbo].[CustomerCashSavingsNetPosition]    Script Date: 06/11/2009 11:59:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerCashSavingsNetPosition](
	[CCSNP_CashSavingsNPId] [int] IDENTITY(1000,1) NOT NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CCSA_AccountId] [int] NULL,
	[XDI_DebtIssuerCode] [varchar](5) NULL,
	[XIB_InterestBasisCode] [varchar](5) NULL,
	[XF_CompoundInterestFrequencyCode] [varchar](5) NULL,
	[XF_InterestPayoutFrequencyCode] [varchar](5) NULL,
	[CCSNP_Name] [varchar](50) NULL,
	[CCSNP_DepositAmount] [numeric](18, 4) NULL,
	[CCSNP_DepositDate] [datetime] NULL,
	[CCSNP_CurrentValue] [numeric](18, 4) NULL,
	[CCSNP_InterestRate] [numeric](10, 5) NULL,
	[CCSNP_InterestAmntPaidOut] [numeric](18, 4) NULL,
	[CCSNP_IsInterestAccumulated] [tinyint] NULL,
	[CCSNP_InterestAmntAccumulated] [numeric](18, 4) NULL,
	[CCSNP_Remark] [varchar](100) NULL,
	[CCSNP_CreatedBy] [int] NOT NULL,
	[CCSNP_CreatedOn] [datetime] NOT NULL,
	[CCSNP_ModifiedBy] [int] NOT NULL,
	[CCSNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerCashSavingsPortfolio] PRIMARY KEY CLUSTERED 
(
	[CCSNP_CashSavingsNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount] FOREIGN KEY([CCSA_AccountId])
REFERENCES [dbo].[CustomerCashSavingsAccount] ([CCSA_AccountId])
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount]
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount1] FOREIGN KEY([CCSA_AccountId])
REFERENCES [dbo].[CustomerCashSavingsAccount] ([CCSA_AccountId])
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_CustomerCashSavingsAccount1]
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLDebtIssuer] FOREIGN KEY([XDI_DebtIssuerCode])
REFERENCES [dbo].[XMLDebtIssuer] ([XDI_DebtIssuerCode])
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLDebtIssuer]
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLFrequency] FOREIGN KEY([XF_InterestPayoutFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLFrequency]
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLFrequency1] FOREIGN KEY([XF_CompoundInterestFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLFrequency1]
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLInterestBasis] FOREIGN KEY([XIB_InterestBasisCode])
REFERENCES [dbo].[XMLInterestBasis] ([XIB_InterestBasisCode])
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsNetPosition_XMLInterestBasis]
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsPortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerCashSavingsNetPosition] CHECK CONSTRAINT [FK_CustomerCashSavingsPortfolio_ProductAssetInstrumentCategory]
GO


