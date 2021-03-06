/****** Object:  Table [dbo].[XMLCustomerType]    Script Date: 06/23/2009 19:02:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLCustomerType]') AND type in (N'U'))
DROP TABLE [dbo].[XMLCustomerType]
GO
/****** Object:  Table [dbo].[XMLCustomerType]    Script Date: 06/23/2009 19:02:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLCustomerType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLCustomerType](
	[XCT_CustomerTypeCode] [varchar](5) NOT NULL,
	[XCT_CustomerTypeName] [varchar](20) NULL,
	[XCT_CreatedBy] [int] NULL,
	[XCT_CreatedOn] [datetime] NULL,
	[XCT_ModifiedBy] [int] NULL,
	[XCT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLCustomerType] PRIMARY KEY CLUSTERED 
(
	[XCT_CustomerTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
