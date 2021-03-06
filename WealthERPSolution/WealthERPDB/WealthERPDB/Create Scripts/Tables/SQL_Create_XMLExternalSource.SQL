/****** Object:  Table [dbo].[XMLExternalSource]    Script Date: 06/23/2009 19:03:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLExternalSource]') AND type in (N'U'))
DROP TABLE [dbo].[XMLExternalSource]
GO
/****** Object:  Table [dbo].[XMLExternalSource]    Script Date: 06/23/2009 19:03:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLExternalSource]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLExternalSource](
	[XES_SourceCode] [varchar](5) NOT NULL,
	[XES_SourceName] [varchar](10) NULL,
	[XES_CreatedBy] [int] NULL,
	[XES_CreatedOn] [datetime] NULL,
	[XES_ModifiedBy] [int] NULL,
	[XES_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLExternalSource] PRIMARY KEY CLUSTERED 
(
	[XES_SourceCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLExternalSource_XMLExternalSource]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLExternalSource]'))
ALTER TABLE [dbo].[XMLExternalSource]  WITH CHECK ADD  CONSTRAINT [FK_XMLExternalSource_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLExternalSource_XMLExternalSource]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLExternalSource]'))
ALTER TABLE [dbo].[XMLExternalSource] CHECK CONSTRAINT [FK_XMLExternalSource_XMLExternalSource]
GO
