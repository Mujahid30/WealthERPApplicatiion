/****** Object:  Table [dbo].[XMLNationality]    Script Date: 06/23/2009 19:03:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLNationality]') AND type in (N'U'))
DROP TABLE [dbo].[XMLNationality]
GO
/****** Object:  Table [dbo].[XMLNationality]    Script Date: 06/23/2009 19:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLNationality]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLNationality](
	[XN_NationalityCode] [varchar](5) NOT NULL,
	[XN_Nationality] [varchar](30) NULL,
	[XN_CreatedBy] [int] NULL,
	[XN_CreatedOn] [datetime] NULL,
	[XN_ModifiedBy] [int] NULL,
	[XN_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLNationality] PRIMARY KEY CLUSTERED 
(
	[XN_NationalityCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
