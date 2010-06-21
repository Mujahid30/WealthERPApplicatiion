
GO

/****** Object:  Table [dbo].[CustomerInsuranceNetPosition]    Script Date: 06/11/2009 13:07:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerInsuranceNetPosition](
	[CINP_InsuranceNPId] [int] IDENTITY(1000,1) NOT NULL,
	[PAIC_AssetInstrumentCategoryCode] [varchar](4) NULL,
	[PAG_AssetGroupCode] [varchar](2) NULL,
	[CIA_AccountId] [int] NULL,
	[XII_InsuranceIssuerCode] [varchar](5) NULL,
	[XF_PremiumFrequencyCode] [varchar](5) NULL,
	[CINP_Name] [varchar](50) NULL,
	[CINP_PremiumAmount] [numeric](18, 3) NULL,
	[CINP_PremiumDuration] [numeric](5, 0) NULL,
	[CINP_SumAssured] [numeric](18, 3) NULL,
	[CINP_StartDate] [datetime] NULL,
	[CINP_PolicyPeriod] [numeric](5, 0) NULL,
	[CINP_PremiumAccumalated] [numeric](18, 3) NULL,
	[CINP_PolicyEpisode] [numeric](5, 0) NULL,
	[CINP_BonusAccumalated] [numeric](18, 3) NULL,
	[CINP_SurrenderValue] [numeric](18, 3) NULL,
	[CINP_Remark] [varchar](100) NULL,
	[CINP_MaturityValue] [numeric](18, 3) NULL,
	[CINP_EndDate] [datetime] NULL,
	[CINP_GracePeriod] [numeric](5, 0) NULL,
	[CINP_ULIPCharges] [numeric](18, 3) NULL,
	[CINP_PremiumPaymentDate] [datetime] NULL,
	[CINP_ApplicationNum] [varchar](20) NULL,
	[CINP_ApplicationDate] [datetime] NULL,
	[CINP_CreatedOn] [datetime] NOT NULL,
	[CINP_CreatedBy] [int] NOT NULL,
	[CINP_ModifiedBy] [int] NOT NULL,
	[CINP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerInsurancePortfolio] PRIMARY KEY CLUSTERED 
(
	[CINP_InsuranceNPId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Insurance Portfolio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerInsuranceNetPosition'
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceNetPosition_CustomerInsuranceAccount] FOREIGN KEY([CIA_AccountId])
REFERENCES [dbo].[CustomerInsuranceAccount] ([CIA_AccountId])
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsuranceNetPosition_CustomerInsuranceAccount]
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceNetPosition_XMLFrequency] FOREIGN KEY([XF_PremiumFrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsuranceNetPosition_XMLFrequency]
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceNetPosition_XMLInsuranceIssuer] FOREIGN KEY([XII_InsuranceIssuerCode])
REFERENCES [dbo].[XMLInsuranceIssuer] ([XII_InsuranceIssuerCode])
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsuranceNetPosition_XMLInsuranceIssuer]
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsurancePortfolio_CustomerInsurancePortfolio] FOREIGN KEY([CINP_InsuranceNPId])
REFERENCES [dbo].[CustomerInsuranceNetPosition] ([CINP_InsuranceNPId])
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsurancePortfolio_CustomerInsurancePortfolio]
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsurancePortfolio_ProductAssetInstrumentCategory] FOREIGN KEY([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
REFERENCES [dbo].[ProductAssetInstrumentCategory] ([PAIC_AssetInstrumentCategoryCode], [PAG_AssetGroupCode])
GO

ALTER TABLE [dbo].[CustomerInsuranceNetPosition] CHECK CONSTRAINT [FK_CustomerInsurancePortfolio_ProductAssetInstrumentCategory]
GO


