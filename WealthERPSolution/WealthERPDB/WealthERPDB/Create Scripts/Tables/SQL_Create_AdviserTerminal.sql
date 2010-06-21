
GO

/****** Object:  Table [dbo].[AdviserTerminal]    Script Date: 06/11/2009 10:51:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AdviserTerminal](
	[AT_Id] [int] IDENTITY(1000,1) NOT NULL,
	[AT_TerminalId] [numeric](10, 0) NULL,
	[AB_BranchId] [int] NOT NULL,
	[AT_CreatedBy] [int] NOT NULL,
	[AT_CreatedOn] [datetime] NOT NULL,
	[AT_ModifiedBy] [int] NOT NULL,
	[AT_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AdvisorTerminal] PRIMARY KEY CLUSTERED 
(
	[AT_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Advisor Terminal Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserTerminal'
GO

ALTER TABLE [dbo].[AdviserTerminal]  WITH CHECK ADD  CONSTRAINT [FK_AdvisorTerminal_AdvisorBranch] FOREIGN KEY([AB_BranchId])
REFERENCES [dbo].[AdviserBranch] ([AB_BranchId])
GO

ALTER TABLE [dbo].[AdviserTerminal] CHECK CONSTRAINT [FK_AdvisorTerminal_AdvisorBranch]
GO

ALTER TABLE [dbo].[AdviserTerminal]  WITH CHECK ADD  CONSTRAINT [FK_AdvisorTerminal_AdvisorTerminal] FOREIGN KEY([AT_Id])
REFERENCES [dbo].[AdviserTerminal] ([AT_Id])
GO

ALTER TABLE [dbo].[AdviserTerminal] CHECK CONSTRAINT [FK_AdvisorTerminal_AdvisorTerminal]
GO


