USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[User]    Script Date: 06/12/2009 18:22:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[User](
	[U_UserId] [int] IDENTITY(1000,1) NOT NULL,
	[U_Password] [varchar](50) NULL,
	[U_FirstName] [varchar](50) NULL,
	[U_MiddleName] [varchar](50) NULL,
	[U_LastName] [varchar](50) NULL,
	[U_Email] [varchar](max) NULL,
	[U_UserType] [varchar](10) NULL,
	[U_LoginId] [varchar](max) NULL,
	[U_CreatedBy] [int] NOT NULL,
	[U_ModifiedBy] [int] NOT NULL,
	[U_CreatedOn] [datetime] NOT NULL,
	[U_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[U_UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'User Master Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO


