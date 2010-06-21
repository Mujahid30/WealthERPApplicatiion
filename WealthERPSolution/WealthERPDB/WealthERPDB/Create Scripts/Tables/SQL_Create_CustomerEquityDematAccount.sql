
GO

/****** Object:  Table [dbo].[CustomerEquityDematAccount]    Script Date: 06/11/2009 12:00:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityDematAccount](
	[CEDA_DematAccountId] [int] IDENTITY(1000,1) NOT NULL,
	[CP_PortfolioId] [int] NULL,
	[CEDA_DPClientId] [varchar](20) NULL,
	[CEDA_DPId] [varchar](20) NULL,
	[CEDA_DPName] [varchar](50) NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CEDA_IsJointlyHeld] [tinyint] NULL,
	[CEDA_AccountOpeningDate] [datetime] NULL,
	[CEDA_CreatedBy] [int] NULL,
	[CEDA_CreatedOn] [datetime] NULL,
	[CEDA_ModifiedBy] [int] NULL,
	[CEDA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityDematAccount] PRIMARY KEY CLUSTERED 
(
	[CEDA_DematAccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerEquityDematAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityDematAccount_CustomerPortfolio] FOREIGN KEY([CP_PortfolioId])
REFERENCES [dbo].[CustomerPortfolio] ([CP_PortfolioId])
GO

ALTER TABLE [dbo].[CustomerEquityDematAccount] CHECK CONSTRAINT [FK_CustomerEquityDematAccount_CustomerPortfolio]
GO

ALTER TABLE [dbo].[CustomerEquityDematAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityDematAccount_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO

ALTER TABLE [dbo].[CustomerEquityDematAccount] CHECK CONSTRAINT [FK_CustomerEquityDematAccount_XMLModeOfHolding]
GO


