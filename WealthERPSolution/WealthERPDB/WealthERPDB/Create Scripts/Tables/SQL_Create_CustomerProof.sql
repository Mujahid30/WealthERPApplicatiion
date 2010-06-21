
GO

/****** Object:  Table [dbo].[CustomerProof]    Script Date: 06/11/2009 16:08:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerProof](
	[CP_CustomerProofId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[XP_ProofCode] [int] NOT NULL,
	[CP_CreatedOn] [datetime] NOT NULL,
	[CP_CreatedBy] [int] NOT NULL,
	[CP_ModifiedOn] [datetime] NOT NULL,
	[CP_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerProofs_1] PRIMARY KEY CLUSTERED 
(
	[CP_CustomerProofId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Proofs Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerProof'
GO

ALTER TABLE [dbo].[CustomerProof]  WITH CHECK ADD  CONSTRAINT [FK_CustomerProof_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerProof] CHECK CONSTRAINT [FK_CustomerProof_Customer]
GO

ALTER TABLE [dbo].[CustomerProof]  WITH CHECK ADD  CONSTRAINT [FK_CustomerProof_XMLProof] FOREIGN KEY([XP_ProofCode])
REFERENCES [dbo].[XMLProof] ([XP_ProofCode])
GO

ALTER TABLE [dbo].[CustomerProof] CHECK CONSTRAINT [FK_CustomerProof_XMLProof]
GO


