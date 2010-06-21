
GO

/****** Object:  Table [dbo].[CustomerEquityDematAccountAssociates]    Script Date: 06/11/2009 12:03:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityDematAccountAssociates](
	[CEDAA_AccountAssociationId] [int] NOT NULL,
	[CAS_AssociationId] [int] NULL,
	[CEDAA_AssociationType] [varchar](30) NULL,
	[CEDA_DematAccountId] [int] NULL,
	[CEDAA_CreatedBy] [int] NULL,
	[CEDAA_CreatedOn] [datetime] NULL,
	[CEDAA_ModifiedBy] [int] NULL,
	[CEDAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerDematAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CEDAA_AccountAssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerEquityDematAccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerEquityDematAccountAssociates_CustomerAssociates] FOREIGN KEY([CAS_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerEquityDematAccountAssociates] CHECK CONSTRAINT [FK_CustomerEquityDematAccountAssociates_CustomerAssociates]
GO

ALTER TABLE [dbo].[CustomerEquityDematAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityDematAccountAssociates_CustomerEquityDematAccount] FOREIGN KEY([CEDA_DematAccountId])
REFERENCES [dbo].[CustomerEquityDematAccount] ([CEDA_DematAccountId])
GO

ALTER TABLE [dbo].[CustomerEquityDematAccountAssociates] CHECK CONSTRAINT [FK_CustomerEquityDematAccountAssociates_CustomerEquityDematAccount]
GO


