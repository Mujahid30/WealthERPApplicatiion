
GO

/****** Object:  Table [dbo].[AdviserBranch]    Script Date: 06/11/2009 10:47:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdviserBranch](
	[AB_BranchId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NOT NULL,
	[AB_BranchHeadId] [int] NULL,
	[AB_BranchCode] [varchar](10) NULL,
	[AB_BranchName] [varchar](25) NULL,
	[AB_AddressLine1] [varchar](25) NOT NULL,
	[AB_AddressLine2] [varchar](25) NULL,
	[AB_AddressLine3] [varchar](25) NULL,
	[AB_City] [varchar](25) NOT NULL,
	[AB_PinCode] [numeric](6, 0) NOT NULL,
	[AB_State] [varchar](25) NOT NULL,
	[AB_Country] [varchar](25) NOT NULL,
	[AB_Email] [varchar](50) NULL,
	[AB_Phone1ISD] [numeric](4, 0) NOT NULL,
	[AB_Phone2ISD] [numeric](4, 0) NULL,
	[AB_Phone1STD] [numeric](4, 0) NOT NULL,
	[AB_Phone1] [numeric](8, 0) NOT NULL,
	[AB_Phone2STD] [numeric](4, 0) NULL,
	[AB_Phone2] [numeric](8, 0) NULL,
	[AB_FaxISD] [numeric](4, 0) NULL,
	[AB_Fax] [numeric](8, 0) NULL,
	[AB_FaxSTD] [numeric](4, 0) NOT NULL,
	[AB_BranchHeadMobile] [numeric](10, 0) NULL,
	[AB_CreatedBy] [int] NOT NULL,
	[AB_CreatedOn] [datetime] NOT NULL,
	[AB_ModifiedOn] [datetime] NOT NULL,
	[AB_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_AdvisorBranch] PRIMARY KEY CLUSTERED 
(
	[AB_BranchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor Branch Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserBranch'
GO

ALTER TABLE [dbo].[AdviserBranch]  WITH CHECK ADD  CONSTRAINT [FK_AdviserBranch_Adviser] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO

ALTER TABLE [dbo].[AdviserBranch] CHECK CONSTRAINT [FK_AdviserBranch_Adviser]
GO

ALTER TABLE [dbo].[AdviserBranch]  WITH CHECK ADD  CONSTRAINT [FK_AdviserBranch_AdviserRM] FOREIGN KEY([AB_BranchHeadId])
REFERENCES [dbo].[AdviserRM] ([AR_RMId])
GO

ALTER TABLE [dbo].[AdviserBranch] CHECK CONSTRAINT [FK_AdviserBranch_AdviserRM]
GO


