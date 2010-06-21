
GO

/****** Object:  Table [dbo].[WerpProofMandatoryLookup]    Script Date: 06/12/2009 18:36:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpProofMandatoryLookup](
	[WPML_ProofMandatoryId] [int] IDENTITY(1000,1) NOT NULL,
	[WPFC_FilterCategoryCode] [varchar](10) NOT NULL,
	[XP_ProofCode] [int] NOT NULL,
	[WPML_CreatedBy] [int] NULL,
	[WPML_CreatedOn] [datetime] NULL,
	[WPML_ModifiedOn] [datetime] NULL,
	[WPML_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Rules] PRIMARY KEY CLUSTERED 
(
	[WPML_ProofMandatoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Profile Filter Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpProofMandatoryLookup'
GO

ALTER TABLE [dbo].[WerpProofMandatoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_WerpProofMandatoryLookup_WerpProfileFilterCategory] FOREIGN KEY([WPFC_FilterCategoryCode])
REFERENCES [dbo].[WerpProfileFilterCategory] ([WPFC_FilterCategoryCode])
GO

ALTER TABLE [dbo].[WerpProofMandatoryLookup] CHECK CONSTRAINT [FK_WerpProofMandatoryLookup_WerpProfileFilterCategory]
GO

ALTER TABLE [dbo].[WerpProofMandatoryLookup]  WITH CHECK ADD  CONSTRAINT [FK_WerpProofMandatoryLookup_XMLProof] FOREIGN KEY([XP_ProofCode])
REFERENCES [dbo].[XMLProof] ([XP_ProofCode])
GO

ALTER TABLE [dbo].[WerpProofMandatoryLookup] CHECK CONSTRAINT [FK_WerpProofMandatoryLookup_XMLProof]
GO


