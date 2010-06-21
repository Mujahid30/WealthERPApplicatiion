
GO

/****** Object:  Table [dbo].[CustomerMutualFundSystematicSetup]    Script Date: 06/11/2009 16:03:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMutualFundSystematicSetup](
	[CMFSS_SystematicSetupId] [int] IDENTITY(1000,1) NOT NULL,
	[PASP_SchemePlanCode] [int] NOT NULL,
	[PASP_SchemePlanCodeSwitch] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[XSTT_SystematicTypeCode] [varchar](5) NULL,
	[CMFSS_StartDate] [datetime] NULL,
	[CMFSS_EndDate] [datetime] NULL,
	[CMFSS_SystematicDate] [numeric](2, 0) NULL,
	[CMFSS_Amount] [numeric](10, 5) NULL,
	[CMFSS_IsManual] [tinyint] NULL,
	[XES_SourceCode] [varchar](5) NULL,
	[XF_FrequencyCode] [varchar](5) NULL,
	[XPM_PaymentModeCode] [varchar](5) NULL,
	[CMFSS_CreatedBy] [int] NULL,
	[CMFSS_CreatedOn] [datetime] NULL,
	[CMFSS_ModifiedBy] [int] NULL,
	[CMFSS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerSystematicTransactionSetup_1] PRIMARY KEY CLUSTERED 
(
	[CMFSS_SystematicSetupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Systematic Transaction Setup Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundSystematicSetup'
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_CustomerMutualFundAccount] FOREIGN KEY([CMFA_AccountId])
REFERENCES [dbo].[CustomerMutualFundAccount] ([CMFA_AccountId])
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_CustomerMutualFundAccount]
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_ProductAMCSchemePlan] FOREIGN KEY([PASP_SchemePlanCode])
REFERENCES [dbo].[ProductAMCSchemePlan] ([PASP_SchemePlanCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_ProductAMCSchemePlan]
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLExternalSource]
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLFrequency] FOREIGN KEY([XF_FrequencyCode])
REFERENCES [dbo].[XMLFrequency] ([XF_FrequencyCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLFrequency]
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLPaymentMode] FOREIGN KEY([XPM_PaymentModeCode])
REFERENCES [dbo].[XMLPaymentMode] ([XPM_PaymentModeCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLPaymentMode]
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLSystematicTransactionType] FOREIGN KEY([XSTT_SystematicTypeCode])
REFERENCES [dbo].[XMLSystematicTransactionType] ([XSTT_SystematicTypeCode])
GO

ALTER TABLE [dbo].[CustomerMutualFundSystematicSetup] CHECK CONSTRAINT [FK_CustomerMutualFundSystematicSetup_XMLSystematicTransactionType]
GO


