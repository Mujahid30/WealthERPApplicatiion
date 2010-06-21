
GO

/****** Object:  Table [dbo].[CustomerPensionandGratuitiesNetPosition]    Script Date: 06/11/2009 16:04:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerPensionandGratuitiesNetPosition](
	[CPGNP_PensionGratutiesNPId] [int] IDENTITY(1000,1) NOT NULL,
	[CPGA_AccountId] [int] NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[XDI_DebtIssuerCode] [varchar](5) NULL,
	[XFY_FiscalYearCode] [varchar](5) NULL,
	[CPGNP_EmployeeContri] [numeric](18, 4) NULL,
	[XF_InterestPayableFrequencyCode] [varchar](5) NULL,
	[XF_CompoundInterestFrequencyCode] [varchar](5) NULL,
	[CPGNP_InterestRate] [numeric](6, 3) NULL,
	[CPGNP_OrganizationName] [varchar](50) NULL,
	[CPGNP_PurchaseDate] [datetime] NULL,
	[CPGNP_DepositAmount] [numeric](18, 4) NULL,
	[CPGNP_EmployerContri] [numeric](18, 4) NULL,
	[CPGNP_MaturityDate] [datetime] NULL,
	[CPGNP_MaturityValue] [numeric](18, 4) NULL,
	[CPGNP_CurrentValue] [numeric](18, 4) NULL,
	[CPGNP_Remark] [varchar](100) NULL,
	[XIB_InterestBasisCode] [varchar](5) NULL,
	[CPGNP_IsInterestAccumalated] [tinyint] NULL,
	[CPGNP_InterestAmtAccumalated] [numeric](18, 4) NULL,
	[CPGNP_InterestAmtPaidOut] [numeric](18, 4) NULL,
	[CPGNP_LoanStartDate] [datetime] NULL,
	[CPGNP_LoanEndDate] [datetime] NULL,
	[CPGNP_LoanOutstandingAmount] [numeric](18, 4) NULL,
	[CPGNP_LoanDescription] [varchar](50) NULL,
	[CPGNP_CreatedBy] [int] NOT NULL,
	[CPGNP_CreatedOn] [datetime] NOT NULL,
	[CPGNP_ModifiedBy] [int] NOT NULL,
	[CPGNP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerPensionandGratuitiesPortfolio] PRIMARY KEY CLUSTERED 
(
	[CPGNP_PensionGratutiesNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_CustomerPensionandGratuitiesAccount] FOREIGN KEY([CPGA_AccountId])
REFERENCES [dbo].[CustomerPensionandGratuitiesAccount] ([CPGA_AccountId])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_CustomerPensionandGratuitiesAccount]
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLDebtIssuer] FOREIGN KEY([XDI_DebtIssuerCode])
REFERENCES [dbo].[XMLDebtIssuer] ([XDI_DebtIssuerCode])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLDebtIssuer]
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFiscalYear] FOREIGN KEY([XFY_FiscalYearCode])
REFERENCES [dbo].[XMLFiscalYear] ([XFY_FiscalYearCode])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFiscalYear]
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency] FOREIGN KEY([XF_CompoundInterestFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency]
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency1] FOREIGN KEY([XF_InterestPayableFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLFrequency1]
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLInterestBasis] FOREIGN KEY([XIB_InterestBasisCode])
REFERENCES [dbo].[XMLInterestBasis] ([XIB_InterestBasisCode])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesNetPosition_XMLInterestBasis]
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGratuitiesPortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerPensionandGratuitiesNetPosition] CHECK CONSTRAINT [FK_CustomerPensionandGratuitiesPortfolio_ProductAssetInstrumentCategory]
GO


