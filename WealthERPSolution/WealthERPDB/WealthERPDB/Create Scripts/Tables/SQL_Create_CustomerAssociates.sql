
GO

/****** Object:  Table [dbo].[CustomerAssociates]    Script Date: 06/11/2009 11:56:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerAssociates](
	[CA_AssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[C_AssociateCustomerId] [int] NULL,
	[XR_RelationshipCode] [varchar](5) NULL,
	[CA_CreatedBy] [int] NOT NULL,
	[CA_CreatedOn] [datetime] NOT NULL,
	[CA_ModifiedOn] [datetime] NOT NULL,
	[CA_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerFamily] PRIMARY KEY CLUSTERED 
(
	[CA_AssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Family Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerAssociates'
GO

ALTER TABLE [dbo].[CustomerAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAssociates_Customer] FOREIGN KEY([C_AssociateCustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerAssociates] CHECK CONSTRAINT [FK_CustomerAssociates_Customer]
GO

ALTER TABLE [dbo].[CustomerAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAssociates_XMLRelationship] FOREIGN KEY([XR_RelationshipCode])
REFERENCES [dbo].[XMLRelationship] ([XR_RelationshipCode])
GO

ALTER TABLE [dbo].[CustomerAssociates] CHECK CONSTRAINT [FK_CustomerAssociates_XMLRelationship]
GO

ALTER TABLE [dbo].[CustomerAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFamily_CustomerMaster] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerAssociates] CHECK CONSTRAINT [FK_CustomerFamily_CustomerMaster]
GO


