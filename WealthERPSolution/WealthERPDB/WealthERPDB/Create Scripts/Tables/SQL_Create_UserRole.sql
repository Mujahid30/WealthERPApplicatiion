USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[UserRole]    Script Date: 06/12/2009 18:22:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserRole](
	[UR_RoleName] [varchar](50) NULL,
	[UR_RoleId] [int] NULL,
	[UR_CreatedBy] [int] NULL,
	[UR_CreatedOn] [datetime] NULL,
	[UR_ModifiedBy] [int] NULL,
	[UR_ModifiedOn] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


