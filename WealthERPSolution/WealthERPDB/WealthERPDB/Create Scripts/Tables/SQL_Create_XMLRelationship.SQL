/****** Object:  Table [dbo].[XMLRelationship]    Script Date: 06/23/2009 19:03:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLRelationship]') AND type in (N'U'))
DROP TABLE [dbo].[XMLRelationship]
GO
/****** Object:  Table [dbo].[XMLRelationship]    Script Date: 06/23/2009 19:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLRelationship]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLRelationship](
	[XR_RelationshipCode] [varchar](5) NOT NULL,
	[XR_Relationship] [varchar](30) NULL,
	[XR_CreatedBy] [int] NULL,
	[XR_ModifiedBy] [int] NULL,
	[XR_CreatedOn] [datetime] NULL,
	[XR_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLRelationship] PRIMARY KEY CLUSTERED 
(
	[XR_RelationshipCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
