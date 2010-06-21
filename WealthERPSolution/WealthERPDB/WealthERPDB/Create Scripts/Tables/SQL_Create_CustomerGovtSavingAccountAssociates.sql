
GO

/****** Object:  Table [dbo].[CustomerGovtSavingAccountAssociates]    Script Date: 06/11/2009 13:04:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerGovtSavingAccountAssociates](
	[CGSAA_AccountAssociateId] [int] IDENTITY(1000,1) NOT NULL,
	[CA_AssociationId] [int] NULL,
	[CGSA_AccountId] [int] NULL,
	[CGSAA_AssociationType] [varchar](30) NULL,
	[CGSAA_CreatedBy] [int] NULL,
	[CGSAA_CreatedOn] [datetime] NULL,
	[CGSAA_ModifiedBy] [int] NULL,
	[CGSAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerGovtSavingAccountAssociate] PRIMARY KEY CLUSTERED 
(
	[CGSAA_AccountAssociateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccountAssociate_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccountAssociates] CHECK CONSTRAINT [FK_CustomerGovtSavingAccountAssociate_CustomerAssociates]
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerGovtSavingAccountAssociate_CustomerGovtSavingAccount] FOREIGN KEY([CGSA_AccountId])
REFERENCES [dbo].[CustomerGovtSavingAccount] ([CGSA_AccountId])
GO

ALTER TABLE [dbo].[CustomerGovtSavingAccountAssociates] CHECK CONSTRAINT [FK_CustomerGovtSavingAccountAssociate_CustomerGovtSavingAccount]
GO


