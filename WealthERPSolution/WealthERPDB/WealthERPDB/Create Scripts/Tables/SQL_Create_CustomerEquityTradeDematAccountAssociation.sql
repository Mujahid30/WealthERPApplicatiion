
GO

/****** Object:  Table [dbo].[CustomerEquityTradeDematAccountAssociation]    Script Date: 06/11/2009 12:06:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerEquityTradeDematAccountAssociation](
	[CETDAA_AssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CEDA_DematAccountId] [int] NULL,
	[CETA_AccountId] [int] NULL,
	[CETDAA_IsDefault] [tinyint] NULL,
	[CETDAA_CreatedBy] [int] NULL,
	[CETDAA_CreatedOn] [datetime] NULL,
	[CETDAA_ModifiedBy] [int] NULL,
	[CETDAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerEquityTradeDematAccountAssociation] PRIMARY KEY CLUSTERED 
(
	[CETDAA_AssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityDematAccount] FOREIGN KEY([CEDA_DematAccountId])
REFERENCES [dbo].[CustomerEquityDematAccount] ([CEDA_DematAccountId])
GO

ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation] CHECK CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityDematAccount]
GO

ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeAccount] FOREIGN KEY([CETA_AccountId])
REFERENCES [dbo].[CustomerEquityTradeAccount] ([CETA_AccountId])
GO

ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation] CHECK CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeAccount]
GO

ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeDematAccountAssociation] FOREIGN KEY([CETDAA_AssociationId])
REFERENCES [dbo].[CustomerEquityTradeDematAccountAssociation] ([CETDAA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerEquityTradeDematAccountAssociation] CHECK CONSTRAINT [FK_CustomerEquityTradeDematAccountAssociation_CustomerEquityTradeDematAccountAssociation]
GO


