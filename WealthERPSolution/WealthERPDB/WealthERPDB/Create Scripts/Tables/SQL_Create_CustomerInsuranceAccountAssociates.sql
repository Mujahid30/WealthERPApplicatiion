
GO

/****** Object:  Table [dbo].[CustomerInsuranceAccountAssociates]    Script Date: 06/11/2009 13:06:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerInsuranceAccountAssociates](
	[CIAA_AccountassociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CIA_AccountId] [int] NULL,
	[CA_AssociationId] [int] NULL,
	[CIAA_AssociationType] [varchar](30) NULL,
	[CIAA_CreatedBy] [int] NULL,
	[CIAA_CreatedOn] [datetime] NULL,
	[CIAA_ModifiedOn] [datetime] NULL,
	[CIAA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerInsuranceAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CIAA_AccountassociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerInsuranceAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerInsuranceAccountAssociates] CHECK CONSTRAINT [FK_CustomerInsuranceAccountAssociates_CustomerAssociates]
GO

ALTER TABLE [dbo].[CustomerInsuranceAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceAccountAssociates_CustomerInsuranceAccount] FOREIGN KEY([CIA_AccountId])
REFERENCES [dbo].[CustomerInsuranceAccount] ([CIA_AccountId])
GO

ALTER TABLE [dbo].[CustomerInsuranceAccountAssociates] CHECK CONSTRAINT [FK_CustomerInsuranceAccountAssociates_CustomerInsuranceAccount]
GO


