USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[UserRoleAssociation]    Script Date: 06/12/2009 18:22:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserRoleAssociation](
	[URA_UserRoleAssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[U_UserId] [int] NULL,
	[UR_RoleId] [int] NULL,
	[URA_CreatedBy] [int] NOT NULL,
	[URA_CreatedOn] [datetime] NOT NULL,
	[URA_ModifiedBy] [int] NOT NULL,
	[URA_ModifiedOn] [datetime] NOT NULL
) ON [PRIMARY]

GO


